using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using SSH_Console;
using SshKeyGenerator;



class smartSSH
{

    public static bool overwrite = false;
    static void Main(string[] args)
    {


        string type = "basic";
        int strength = 2048;
        string comment = string.Empty;
        string foldername = "Not Set";


        int[] validstrengths = { 1024, 2048, 4096 };
        string[] validTypes = { "basic", "base64", "all" };

        int i = 0;

        foreach (string arg in args)
        {



            switch (arg)
            {
                case "-strength":
                    try
                    {
                        strength = int.Parse(args[i + 1]);

                    }
                    catch
                    {
                        Console.WriteLine("\nERROR : Strength value expected (-s).");
                        return;
                    }
                    break;
                case "-folder":
                    try
                    {
                        foldername = args[i + 1];
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("\nERROR: Folder name expected (-d)...: \nFolder: " + foldername + "\n");
                        return;
                    }
                    break;
                case "-type":
                    try
                    {
                        type = args[i + 1];

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("\nERROR: SSH Key Type expected (-o)...: \nDefaulting to 'basic'\n");
                        type = "Basic";
                    }
                    type = char.ToUpper(type[0]) + type.Substring(1);
                    break;
                case "-comment":
                    try
                    {
                        comment = args[i + 1];
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("\nERROR: Comments expected (-d)...: \nComment: " + comment + "\n");
                        return;
                    }
                    break;

                case "/overwrite":
                    overwrite = true;
                    break;

                default:
                    break;
            }
            i++;
        }


        // Validate Strength
        if (!validstrengths.Contains(strength)) strength = 2048;

        // Validate Options
        if (!validTypes.Contains(type.ToLower())) type = "Basic";

        // Validate Folder
        if (!(foldername == "Not Set") && !System.IO.Directory.Exists(foldername))
        {
            if (System.IO.Path.IsPathFullyQualified(foldername)) System.IO.Directory.CreateDirectory(foldername);
            if (!System.IO.Directory.Exists(foldername))
            {
                Console.WriteLine("\nInvalid Folder: " + foldername + "\n");
                return;
            }
        }



        Console.WriteLine("\nStrength = " + strength.ToString());
        Console.WriteLine("Folder = " + foldername);
        Console.WriteLine("Type = " + type);
        Console.WriteLine("Comment = " + comment);
        Console.WriteLine("Overwrite = " + overwrite);


        switch (type)
        {
            /*
            case "Basic":
                Print_SSH_KeyPair(strength);
                break;
            */
            case "All":
                ssh_generator.Print_SSH_KeyPair_All(strength, comment, foldername, overwrite);
                break;
            case "Base64":
                ssh_generator.Print_SSH_KeyPair_Base64(strength, comment, foldername, overwrite);
                break;
            default:
                ssh_generator.Print_SSH_KeyPair_Basic(strength, comment, foldername, overwrite);
                break;
        }

    }

}