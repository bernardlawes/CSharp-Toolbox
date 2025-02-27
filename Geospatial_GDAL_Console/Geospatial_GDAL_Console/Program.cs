using Geospatial_GDAL_Console;
using OSGeo.GDAL;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;


// This program processes a GeoTIFF raster file using the GDAL library. It performs the following steps:
// 1. Prompts the user for the input file path and output folder if not provided as command-line arguments.
// 2. Validates the existence of the input file and output folder.
// 3. Initializes the GDAL library and creates an instance of the DataHandler class.
// 4. Opens the specified GeoTIFF file and retrieves its coordinate bounds.
// 5. Extracts the first three bands of the GeoTIFF as an image.
// 6. Saves the extracted image as a JPEG file in the specified output folder.
// 7. Outputs the coordinate bounds and the path to the saved JPEG image to the console.

// Built using 1930-x64-gdal-3-10-0
// https://download.osgeo.org/gdal/3.1.0/gdal-310-1930-x64-core.msi
// Steps to setup GDAL in Visual Studio for C#
// 1. Download and GDAL binaries for Windows from the official website.
// 2. Install the GDAL binaries on your system (e.g., C:\GDAL).
// 3. Set Up Environment Variables:
// 3.0. Add the GDAL apps directory to the system PATH variable (e.g., C:\GDAL\apps). (these are for executables)
// 3.1. Add the GDAL bin directory to the system PATH variable (e.g., C:\GDAL\bin). ( these are for DLLs)
// 3.2. Add the GDAL data directory to the GDAL_DATA environment variable (e.g., C:\GDAL\bin\gdal\-data).
// 3.3. Add the GDAL DLL directory to the GDAL_DRIVER_PATH environment variable (e.g., C:\GDAL\bin\gdal\plugins).
// 3. Open your Visual Studio project.
// 4. Install the GDAL.Native Nuget Package
// 5. Copy gdal.dll from the GDAL bin directory to the output directory of your project.
// 6. Copy osr_wrap.dlll, org_wrap.dll, gdal_wrap.dll from the bin\gdal\csharp directory to the output directory of your project.
// 7. Confirm that gdal_csharp.dll, ogr_csharp.dll, osr_csharp.dll are also in the output directory of the project.  They should have been created when you installed the GDAL.Native Nuget package.
// 8. Run the Program and enjoy

class Program
{
    static void Main(string[] args)
    {

        // Variables to store the input file path and output folder
        string inputFilePath = string.Empty;
        string outputFolder = string.Empty;

        // Check if the required command-line arguments are provided
        if (args.Length == 0)
        {
            // Prompt the user to enter the input file path if not provided
            Console.WriteLine("Enter path to raster file:");
            inputFilePath = Console.ReadLine();

            // Prompt the user to enter the output folder if not provided
            Console.WriteLine("Enter path to output folder:");
            outputFolder = Console.ReadLine();

        }
        else if (args.Length < 2)
        {
            // Display usage information and exit if arguments are not provided
            Console.WriteLine("Usage: Geospatial_GDAL_Console <input_file> <output_folder>");
            return;
        }
        else
        {
            // Assign command-line arguments to variables
            inputFilePath = args[0];
            outputFolder = args[1];
        }

        // Check if the input file exists
        if (!File.Exists(inputFilePath))
        {
            Console.WriteLine($"Error: The file '{inputFilePath}' does not exist.");
            return;
        }

        // Check if the output folder exists
        if (!Directory.Exists(outputFolder))
        {
            Console.WriteLine($"Error: The folder '{outputFolder}' does not exist.");
            return;
        }

        // Create an instance of the DataHandler class
        DataHandler dataHandler = new DataHandler();

        // Open the GeoTIFF file
        Dataset dataset = dataHandler.OpenGeoTiff(inputFilePath);

        // Get the coordinate bounds of the GeoTIFF
        var (minX, minY, maxX, maxY) = dataHandler.GetCoordinateBounds(dataset);
        Console.WriteLine($"Coordinate Bounds: MinX: {minX}, MinY: {minY}, MaxX: {maxX}, MaxY: {maxY}");

        // Get the first three bands of the GeoTIFF as an image
        Bitmap bitmap = dataHandler.GetFirstThreeBandsAsImage(dataset);

        // Save the image as a JPEG in the specified output folder
        string outputFilePath = Path.Combine(outputFolder, "output.jpg");
        dataHandler.SaveImageAsJpeg(bitmap, outputFilePath);

        Console.WriteLine($"JPEG image saved successfully at '{outputFilePath}'.");

        // Dispose of the dataset to free up resources
        dataset.Dispose();
    }
}
