namespace ElderlyCareSupport.Server.Helpers
{
    public static class CryptographyHelper
    {
        public static string EncryptPassword(string plainText)
        {
            var encrypted = BCrypt.Net.BCrypt.HashPassword(plainText);
            return encrypted;
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
