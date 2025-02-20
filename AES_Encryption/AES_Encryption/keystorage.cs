using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AES_Encryption_Tools
{
    class keystorage
    {
        public static void StoreKey(string key, string filePath)
        {

            byte[] keyBytes = Encoding.UTF8.GetBytes(key);

            // Ensure the key is 16 bytes long
            if (keyBytes.Length != 16)
            {
                Array.Resize(ref keyBytes, 16);
            }

            // Encrypt the key using DPAPI
            byte[] encryptedKey = ProtectedData.Protect(keyBytes, null, DataProtectionScope.CurrentUser);

            // Write the encrypted key to a file
            //File.WriteAllBytes(filePath, encryptedKey);

            // Write the encrypted key to a file
            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                fs.Write(encryptedKey, 0, encryptedKey.Length);
                fs.Flush(); // Ensure all data is written to disk
            }

        }

        public static (bool,string) FetchKey(string filePath)
        {

            // Check if the file exists
            if (!File.Exists(filePath))
            {
                return (false,"The specified key file does not exist.");
            }

            // Read the encrypted key from the file
            byte[] encryptedKey = File.ReadAllBytes(filePath);

            // Decrypt the key using DPAPI
            byte[] keyBytes = ProtectedData.Unprotect(encryptedKey, null, DataProtectionScope.CurrentUser);

            string key = Encoding.UTF8.GetString(keyBytes);

            return (true,key);
        }
    }
}
