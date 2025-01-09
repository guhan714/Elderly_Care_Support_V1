namespace ElderlyCareSupport.Application.Helpers
{
    public static class BCryptEncryptionService
    {
        public static string EncryptPassword(string plainText)
        {
            return BCrypt.Net.BCrypt.HashPassword(plainText);
        }

        public static bool VerifyPassword(string password, string? hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
