using LogisticsApis.Models;
using Microsoft.AspNetCore.Identity;

namespace LogisticsApis.Services
{
    public class PasswordHasherService
    {
        private readonly IPasswordHasher<User> _passwordHasher;

        public PasswordHasherService(IPasswordHasher<User> passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }

        public string HashPassword(User user)
        {
            return _passwordHasher.HashPassword(user, user.Password); // Password from request body
        }

        public IPasswordHasher<User> Get_passwordHasher()
        {
            return _passwordHasher;
        }

        public PasswordVerificationResult VerifyPassword(User user, string providedPassword)
        {
            return _passwordHasher.VerifyHashedPassword(user, user.Password, providedPassword);
        }
    }
}
