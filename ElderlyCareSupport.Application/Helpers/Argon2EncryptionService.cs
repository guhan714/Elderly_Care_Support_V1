using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;

namespace ElderlyCareSupport.Application.Helpers
{
    public static class Argon2EncryptionService
    {

        public static string HashPassword(string password, byte[] salt)
        {
            using var hasher = new Argon2id(Encoding.UTF8.GetBytes(password));
            hasher.Salt = salt;
            hasher.DegreeOfParallelism = 2; // Number of threads
            hasher.MemorySize = 65536;      // 64 MB
            hasher.Iterations = 4;         // Number of iterations

            return Convert.ToBase64String(hasher.GetBytes(32)); // 32-byte hash
        }

        [Obsolete("Obsolete")]
        public static byte[] GenerateSalt()
        {
            var salt = new byte[16];
            using var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(salt);
            return salt;
        }

        public static bool VerifyPassword(string password, string hash, byte[] salt)
        {
            var hashedPassword = HashPassword(password, salt);
            return hashedPassword == hash;
        }

    }
}
