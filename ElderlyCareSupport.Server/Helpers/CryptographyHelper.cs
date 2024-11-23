using Microsoft.Extensions.Logging;
using System.Security.Cryptography;

namespace ElderlyCareSupport.Server.Helpers
{
    public class CryptographyHelper
    {
        public static byte[] EncryptPassword(string plainText, byte[] Key, byte[] IV)
        {
            byte[] encrypted;
            using (AesManaged aes = new())
            {
                ICryptoTransform encryptor = aes.CreateEncryptor(Key, IV);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                            sw.Write(plainText);
                        encrypted = ms.ToArray();
                    }
                }
            }
            return encrypted;
        }

        public string DecryptPassword(byte[] cipherText, byte[] Key, byte[] IV)
        {
            string plaintext = String.Empty;
            try
            {
                using (AesManaged aes = new())
                {
                    ICryptoTransform decryptor = aes.CreateDecryptor(Key, IV);
                    using (MemoryStream ms = new MemoryStream(cipherText))
                    {
                        using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader reader = new StreamReader(cs))
                                plaintext = reader.ReadToEnd();
                        }
                    }
                }
                return plaintext;
            }
            catch (Exception ex)
            {
                return plaintext = String.Empty;
            }
        }
    }
}
