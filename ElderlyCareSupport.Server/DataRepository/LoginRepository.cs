using ElderlyCareSupport.Server.Interfaces;
using ElderlyCareSupport.Server.Models;
using ElderlyCareSupport.Server.ViewModels;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using System.Web.Http.ModelBinding;

namespace ElderlyCareSupport.Server.DataRepository
{
    public class LoginRepository : ILoginRepository
    {
        ElderlyCareSupportContext context;
        ILogger<LoginRepository> logger;
        private string decrypted = String.Empty;

        public LoginRepository(ElderlyCareSupportContext elderlyCareSupport, ILogger<LoginRepository> logger)
        {
            context = elderlyCareSupport;
            this.logger = logger;
        }

        public async Task<bool> AuthenticateLogin(LoginViewModel loginViewModel)
        {
            if (String.IsNullOrWhiteSpace(loginViewModel.UserName) || String.IsNullOrWhiteSpace(loginViewModel.Password))
            {
                return false;
            }

            var result = new LoginViewModel
            {
                UserName = "Ash",
                Password = "AshleyOne",
            };
            try
            {
                using (Aes aes = Aes.Create())
                {
                    aes.GenerateKey();
                    aes.GenerateIV();

                    byte[] encrypted = EncryptPassword(result.Password, aes.Key, aes.IV);

                    decrypted = PasswordDecrypt(encrypted, aes.Key, aes.IV);
                }

                if (loginViewModel.UserName == result.UserName && loginViewModel.Password == decrypted)
                {
                    return true;
                }
                return false;
            }
            catch (Exception exp)
            {
                logger.LogError($"Exception occurred {exp.Message}.\nClass: {nameof(LoginRepository)}\tMethod: {nameof(AuthenticateLogin)}");
                return false;
            }
        }

        public static byte[] EncryptPassword(string plainText, byte[] Key, byte[] IV)
        {
            byte[] encrypted;
            // Create a new AesManaged.
            using (AesManaged aes = new())
            {
                // Create encryptor
                ICryptoTransform encryptor = aes.CreateEncryptor(Key, IV);
                // Create MemoryStream
                using (MemoryStream ms = new MemoryStream())
                {
                    // Create crypto stream using the CryptoStream class. This class is the key to encryption
                    // and encrypts and decrypts data from any given stream. In this case, we will pass a memory stream
                    // to encrypt
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        // Create StreamWriter and write data to a stream
                        using (StreamWriter sw = new StreamWriter(cs))
                            sw.Write(plainText);
                        encrypted = ms.ToArray();
                    }
                }
            }
            // Return encrypted data
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
                logger.LogError($"Exception Occured: {ex.Message}\nClass: {nameof(LoginRepository)}\tMethod: {nameof(PasswordDecrypt)}");
                return plaintext = String.Empty;
            }
        }
    }
}
