using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSH_Console
{
    class ssh_generator
    {

        public static void Print_SSH_KeyPair_Basic(int strength = 2048, string comment = "user@host", String Foldername = "", bool overwrite = false)
        {

            var keygen = new SshKeyGenerator.SshKeyGenerator(strength);

            var privateKey = keygen.ToPrivateKey();
            Console.WriteLine("---------------------------------------------------------------------------------------\nPrivate Key:\n\n" + privateKey + "\n");

            var publicKey = keygen.ToPublicKey();
            Console.WriteLine("---------------------------------------------------------------------------------------\nPublic Non RFC Key:\n\n" + publicKey + "\n");

            var publicSshKey = keygen.ToRfcPublicKey(comment);
            Console.WriteLine("---------------------------------------------------------------------------------------\nPublic RFC Key:\n\n" + publicSshKey + "\n");




            if (System.IO.Directory.Exists(Foldername))
            {
                Console.WriteLine("Folder exist: " + Foldername);
                string[] files = { System.IO.Path.Combine(Foldername, "ssh_private.key"),
                               System.IO.Path.Combine(Foldername, "ssh_public.key"),
                               System.IO.Path.Combine(Foldername, "ssh_public_rfc.key\"") };

                foreach (string file in files)
                {
                    if (!overwrite && System.IO.File.Exists(file))
                    {
                        Console.WriteLine("A file with the name: " + file + "already exists in this path.  To overwrite files, set the Overwrite (/overwrite)");
                        return;
                    }
                }

                System.IO.File.WriteAllText(System.IO.Path.Combine(Foldername, "ssh_private.key"), privateKey);
                System.IO.File.WriteAllText(System.IO.Path.Combine(Foldername, "ssh_public.key"), publicKey);
                System.IO.File.WriteAllText(System.IO.Path.Combine(Foldername, "ssh_public_rfc.key"), publicSshKey);
            } else
            {
                Console.WriteLine("Folder does not exist: "+Foldername); 
            }

        }
        public static void Print_SSH_KeyPair_Base64(int strength = 2048, string comment = "user@host", String Foldername = "", bool overwrite = false)
        {
            // Create a new instance 
            var keygen = new SshKeyGenerator.SshKeyGenerator(strength);

            var privateKey = keygen.ToPrivateKey();
            Console.WriteLine("---------------------------------------------------------------------------------------\nPrivate Key:\n\n" + privateKey + "\n");

            var publicKey = keygen.ToPublicKey();
            Console.WriteLine("---------------------------------------------------------------------------------------\nPublic Non RFC Key:\n\n" + publicKey + "\n");

            var publicSshKey = keygen.ToRfcPublicKey();
            Console.WriteLine("---------------------------------------------------------------------------------------\nPublic RFC Key:\n\n" + publicSshKey + "\n");

            var publicSshKeyWithComment = keygen.ToRfcPublicKey(comment);
            Console.WriteLine("---------------------------------------------------------------------------------------\nPublic Key with Comment:\n\n" + publicSshKeyWithComment + "\n");

            // Get Base64 encoded keys with private key
            var sshBase64Keys = keygen.ToB64Blob(true);
            Console.WriteLine("---------------------------------------------------------------------------------------\nBase64 Encoded Keys:\n\n" + sshBase64Keys + "\n");

            var blobKeys = keygen.ToXml();
            Console.WriteLine("---------------------------------------------------------------------------------------\nXML Keys:\n\n" + blobKeys + "\n");

            if (System.IO.Directory.Exists(Foldername))
            {

                string[] files = { System.IO.Path.Combine(Foldername, "ssh_private.key"),
                               System.IO.Path.Combine(Foldername, "ssh_public.key"),
                               System.IO.Path.Combine(Foldername, "ssh_public_rfc.key"),
                               System.IO.Path.Combine(Foldername, "ssh_public_comment.key"),
                               System.IO.Path.Combine(Foldername, "ssh_base64.key"),
                             };

                foreach (string file in files)
                {
                    if (!overwrite && System.IO.File.Exists(file))
                    {
                        Console.WriteLine("\nFile Write Cancelled\nA file with the name: " + file + "already exists in this folder.\nTo overwrite existing files, set the Overwrite (/overwrite).  ");
                        return;
                    }
                }

                System.IO.File.WriteAllText(System.IO.Path.Combine(Foldername, "ssh_private.key"), privateKey);
                System.IO.File.WriteAllText(System.IO.Path.Combine(Foldername, "ssh_public.key"), publicKey);
                System.IO.File.WriteAllText(System.IO.Path.Combine(Foldername, "ssh_public_rfc.key"), publicSshKey);
                System.IO.File.WriteAllText(System.IO.Path.Combine(Foldername, "ssh_public_comment.key"), publicSshKeyWithComment);
                System.IO.File.WriteAllText(System.IO.Path.Combine(Foldername, "ssh_base64.key"), sshBase64Keys);
            }


        }

        public static void Print_SSH_KeyPair_All(int strength = 2048, string comment = "user@host", String Foldername = "", bool overwrite=false)
        {
            // Create a new instance 
            var keygen = new SshKeyGenerator.SshKeyGenerator(strength);

            var privateKey = keygen.ToPrivateKey();
            Console.WriteLine("---------------------------------------------------------------------------------------\nPrivate Key:\n\n" + privateKey + "\n");

            var publicKey = keygen.ToPublicKey();
            Console.WriteLine("---------------------------------------------------------------------------------------\nPublic Non RFC Key:\n\n" + publicKey + "\n");

            var publicSshKey = keygen.ToRfcPublicKey();
            Console.WriteLine("---------------------------------------------------------------------------------------\nPublic RFC Key:\n\n" + publicSshKey + "\n");

            var publicSshKeyWithComment = keygen.ToRfcPublicKey(comment);
            Console.WriteLine("---------------------------------------------------------------------------------------\nPublic Key with Comment:\n\n" + publicSshKeyWithComment + "\n");

            // Get Base64 encoded keys with private key
            var sshBase64Keys = keygen.ToB64Blob(true);
            Console.WriteLine("---------------------------------------------------------------------------------------\nBase64 Encoded Keys:\n\n" + sshBase64Keys + "\n");

            var sshXmlKeys = keygen.ToXml();
            Console.WriteLine("---------------------------------------------------------------------------------------\nXML Keys:\n\n" + sshXmlKeys + "\n");


            if (System.IO.Directory.Exists(Foldername))
            {

                string[] files = { System.IO.Path.Combine(Foldername, "ssh_private.key"),
                               System.IO.Path.Combine(Foldername, "ssh_public.key"),
                               System.IO.Path.Combine(Foldername, "ssh_public_rfc.key"),
                               System.IO.Path.Combine(Foldername, "ssh_public_comment.key"),
                               System.IO.Path.Combine(Foldername, "ssh_base64.key"),
                               System.IO.Path.Combine(Foldername, "ssh_xml.key")
                             };

                foreach (string file in files)
                {
                    if (!overwrite && System.IO.File.Exists(file))
                    {
                        Console.WriteLine("\nFile Write Cancelled\nA file with the name: " + file + "already exists in this folder.\nTo overwrite existing files, set the Overwrite (/overwrite).  ");
                        return;
                    }
                }

                System.IO.File.WriteAllText(System.IO.Path.Combine(Foldername, "ssh_private.key"), privateKey);
                System.IO.File.WriteAllText(System.IO.Path.Combine(Foldername, "ssh_public.key"), publicKey);
                System.IO.File.WriteAllText(System.IO.Path.Combine(Foldername, "ssh_public_rfc.key"), publicSshKey);
                System.IO.File.WriteAllText(System.IO.Path.Combine(Foldername, "ssh_public_comment.key"), publicSshKeyWithComment);
                System.IO.File.WriteAllText(System.IO.Path.Combine(Foldername, "ssh_base64.key"), sshBase64Keys);
                System.IO.File.WriteAllText(System.IO.Path.Combine(Foldername, "ssh_xml.key"), sshXmlKeys);
            }


        }
    }
}
