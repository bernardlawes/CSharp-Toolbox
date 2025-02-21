using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Security.Cryptography;
using System.IO;

namespace AES_Encryption_Tools
{
    class encryptor
    {
        public static string encrypt(string plaintext, string key)
        {
            // Convert the plaintext and key to byte arrays
            byte[] plaintextBytes = Encoding.UTF8.GetBytes(plaintext);
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);

            // Create a new AES object
            using (var aes = System.Security.Cryptography.Aes.Create())
            {
                // Set the key and IV
                aes.Key = keyBytes;
                aes.IV = new byte[16]; // Use a zero IV for simplicity
                // Create an encryptor
                var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                // Encrypt the data
                using (var ms = new System.IO.MemoryStream())
                {
                    using (var cs = new System.Security.Cryptography.CryptoStream(ms, encryptor, System.Security.Cryptography.CryptoStreamMode.Write))
                    {
                        cs.Write(plaintextBytes, 0, plaintextBytes.Length);
                        cs.FlushFinalBlock();
                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
        }
        public static string decrypt(string ciphertext, string key)
        {
            // Convert the ciphertext and key to byte arrays
            byte[] ciphertextBytes = Convert.FromBase64String(ciphertext);
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);

            if (keyBytes.Length != 16)
            {
                Array.Resize(ref keyBytes, 16);
            }

            // Create a new AES object
            using (var aes = System.Security.Cryptography.Aes.Create())
            {
                // Set the key and IV
                aes.Key = keyBytes;
                aes.IV = new byte[16]; // Use a zero IV for simplicity
                // Create a decryptor
                var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                // Decrypt the data
                using (var ms = new System.IO.MemoryStream(ciphertextBytes))
                {
                    using (var cs = new System.Security.Cryptography.CryptoStream(ms, decryptor, System.Security.Cryptography.CryptoStreamMode.Read))
                    {
                        using (var sr = new System.IO.StreamReader(cs))
                        {
                            return sr.ReadToEnd();
                        }
                    }
                }
            }
        }

    }
}
