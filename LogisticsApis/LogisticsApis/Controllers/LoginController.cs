using LogisticsApis.Configuration;
using LogisticsApis.Data;
using LogisticsApis.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsApis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly LogisticsAPIDbContext _context;
        private readonly JwtSettings _jwtSettings;
        private readonly PasswordHasher<User> passwordHasher;

        public LoginController(LogisticsAPIDbContext context, IOptions<JwtSettings> jwtSettings)
        {
            _context = context;
            _jwtSettings = jwtSettings.Value;
            passwordHasher = new PasswordHasher<User>();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] User login)
        {
            if (string.IsNullOrEmpty(login.UserName) || string.IsNullOrEmpty(login.Password))
            {
                return BadRequest("Username and password are required");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == login.UserName);

            if (user == null)
            {
                return BadRequest("Invalid username or password");
            }

            // Secure password verification using PasswordHasher
            bool passwordMatches = passwordHasher.VerifyHashedPassword(user, user.Password, login.Password) == PasswordVerificationResult.Success;

            if (!passwordMatches)
            {
                return BadRequest("Invalid username or password");
            }

            // Generate JWT token on successful login
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