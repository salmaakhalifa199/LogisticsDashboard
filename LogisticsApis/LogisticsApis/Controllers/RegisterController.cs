using LogisticsApis.Configuration;
using LogisticsApis.Data;
using LogisticsApis.Models; // Assuming User model is in Models folder
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace LogisticsApis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterController : ControllerBase
    {
        private readonly LogisticsAPIDbContext _context;
        private readonly JwtSettings _jwtSettings;
        private readonly PasswordHasher<User> passwordHasher;

        public RegisterController(LogisticsAPIDbContext context, IOptions<JwtSettings> jwtSettings)
        {
            _context = context;
            _jwtSettings = jwtSettings.Value;
            passwordHasher = new PasswordHasher<User>(); // Create PasswordHasher instance
        }



    [HttpPost]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            user.Id = Guid.NewGuid(); // Generate a new ID for the user
            user.Password = passwordHasher.HashPassword(user , user.Password); // Hash password
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            // Generate JWT token on successful registration  

            var token = GenerateToken(user);
            return Ok(new { token = token });

        }

        private string GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.TokenLifetimeInMinutes),
                SigningCredentials = creds,
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

