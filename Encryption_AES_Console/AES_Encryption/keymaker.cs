using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES_Encryption
{
    class keymaker
    {
        public static (byte[], string) KeyGen()
        {
            
            // Generate a random 16-byte key
            byte[] keyBytes = new byte[16];
            using (var rng = new System.Security.Cryptography.RNGCryptoServiceProvider())
            {
                rng.GetBytes(keyBytes);
            }

            return(keyBytes, Convert.ToBase64String(keyBytes));

        }
        
    }
}
