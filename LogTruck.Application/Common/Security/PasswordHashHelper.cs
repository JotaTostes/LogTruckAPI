using Microsoft.AspNetCore.Identity;

namespace LogTruck.Application.Common.Security
{
    public static class PasswordHashHelper
    {
        private static readonly PasswordHasher<object> _passwordHasher = new();

        public static string Hash(string password)
        {
            return _passwordHasher.HashPassword(null, password);
        }

        public static bool Verify(string hashedPassword, string providedPassword)
        {
            var result = _passwordHasher.VerifyHashedPassword(null, hashedPassword, providedPassword);
            return result == PasswordVerificationResult.Success;
        }
    }
}
