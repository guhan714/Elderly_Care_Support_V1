using ElderlyCareSupport.Server.DataRepository;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;

namespace ElderlyCareSupport.Server.Helpers
{
    public class EncryptionHelper
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

        public string PasswordDecrypt(byte[] cipherText, byte[] Key, byte[] IV)
        {
            string plaintext = String.Empty;

            try
            {
                using (AesManaged aes = new AesManaged())
                {
                    // Create a decryptor
                    ICryptoTransform decryptor = aes.CreateDecryptor(Key, IV);
                    // Create the streams used for decryption.
                    using (MemoryStream ms = new MemoryStream(cipherText))
                    {
                        // Create crypto stream
                        using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                        {
                            // Read crypto stream
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
