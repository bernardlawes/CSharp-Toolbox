using AES_Encryption;
using System;

namespace AES_Encryption_Tools
{
    class Program
    {
        static void Main(string[] args)
        {

            string key = string.Empty;
            string text = string.Empty;
            string keyPath = string.Empty;

            Console.WriteLine("Enter command (encrypt / decrypt / store / fetch / key):");
            string command = Console.ReadLine();
           


            if (command == "encrypt" || command == "decrypt" || command == "store")
            {
                Console.WriteLine("Enter key:");
                key = Console.ReadLine();

                if (command == "encrypt" || command == "decrypt")
                {
                    Console.WriteLine("Enter text:");
                    text = Console.ReadLine();
                }
            }

            if (command == "store" || command == "fetch")
            {
                Console.WriteLine("Enter Path:");
                keyPath = Console.ReadLine();
            }


            Console.WriteLine("\n\n");


            if (command == "encrypt")
            {
                string encryptedText = encryptor.encrypt(text, key);
                Console.WriteLine($"Encrypted Text: {encryptedText}");
            }
            else if (command == "decrypt")
            {
                string decryptedText = encryptor.decrypt(text, key);
                Console.WriteLine($"Decrypted Text: {decryptedText}");
            }
            else if (command == "store")
            {
                keystorage.StoreKey(key, keyPath);

                Console.WriteLine(File.Exists(keyPath) ? "Key Stored Successfully." : "Key Failed to Store.");

            }
            else if (command == "fetch")
            {
                var (isSuccess, result) = keystorage.FetchKey(keyPath);
                Console.WriteLine(isSuccess ? $"Key Retrieved: {result}" : $"Error: {result}");
            }
            else if (command == "key")
            {
                var (generatedBytes, generatedkey) = keymaker.KeyGen();
                Console.WriteLine($"Key String: {generatedkey}");
            }

            else
            {
                Console.WriteLine("Invalid command. Use 'encrypt' or 'decrypt' or 'store' or 'fetch' or 'key'");
            }

            Console.WriteLine("\n\n");
        }
    }
}
