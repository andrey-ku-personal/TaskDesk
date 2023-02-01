using Microsoft.AspNetCore.Identity;
using TaskDesk.SharedModel.Account.Models;

namespace TaskDesk.Identity.Services
{
    public class BCryptPasswordHasher : IPasswordHasher<UserModel>
    {
        private const int WorkFactor = 10;

        public string HashPassword(UserModel user, string password)
        {
            var hash = BCrypt.Net.BCrypt.HashPassword(password, GenerateSalt());
            return hash;
        }

        public PasswordVerificationResult VerifyHashedPassword(UserModel user, string hashedPassword, string providedPassword)
        {
            if (BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword))
                return PasswordVerificationResult.Success;

            return PasswordVerificationResult.Failed;
        }

        private string GenerateSalt()
        {
            var salt = BCrypt.Net.BCrypt.GenerateSalt(WorkFactor);
            return salt;
        }
    }
}
