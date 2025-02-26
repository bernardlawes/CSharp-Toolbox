using System;
using System.Drawing;
using smart.stenography.console;

class Program
{
    static void Main(string[] args)
    {
        // Prompt the user to choose an action
        Console.WriteLine("Choose an action:");
        Console.WriteLine("1. Embed text into an image");
        Console.WriteLine("2. Extract text from an image");
        string choice = Console.ReadLine();

        if (choice == "1")
        {
            // Embed text into an image
            // Prompt the user for the text to embed
            Console.WriteLine("Enter the text to embed:");
            string text = Console.ReadLine();

            // Prompt the user for the path to the image file
            Console.WriteLine("Enter the path to the image file:");
            string imagePath = Console.ReadLine();

            // Prompt the user for the path to save the modified image file
            Console.WriteLine("Enter the path to save the modified image file:");
            string savePath = Console.ReadLine();

            try
            {
                // Load the image
                Bitmap bmp = new Bitmap(imagePath);

                // Embed the text into the image
                Bitmap modifiedBmp = SteganographyHelper.embedText(text, bmp);

                // Save the modified image
                modifiedBmp.Save(savePath);

                Console.WriteLine("\n\n");
                Console.WriteLine("Text successfully embedded and image saved.");
                Console.WriteLine("\n\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
        else if (choice == "2")
        {
            // Extract text from an image
            // Prompt the user for the path to the image file
            Console.WriteLine("Enter the path to the image file:");
            string imagePath = Console.ReadLine();

            try
            {
                // Load the image
                Bitmap bmp = new Bitmap(imagePath);

                // Extract the text from the image
                string extractedText = SteganographyHelper.extractText(bmp);

                Console.WriteLine("\n\n");
                Console.WriteLine("Extracted text: " + extractedText);
                Console.WriteLine("\n\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
        else
        {
            Console.WriteLine("Invalid choice. Please restart the program and choose a valid option.");
        }
    }
}
