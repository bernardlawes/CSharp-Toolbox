using OSGeo.GDAL;
using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Geospatial_GDAL_Console
{
    class DataHandler
    {
        // Constructor to initialize the GDAL library
        public DataHandler()
        {
            Gdal.AllRegister();
            Console.WriteLine("GDAL version: " + Gdal.VersionInfo(""));
        }

        // Method to open a GeoTIFF file and return the dataset
        public Dataset OpenGeoTiff(string filePath)
        {
            Dataset dataset = Gdal.Open(filePath, Access.GA_ReadOnly);
            if (dataset == null)
            {
                throw new Exception("Failed to open the file.");
            }
            return dataset;
        }

        // Method to get the coordinate bounds of the GeoTIFF
        public (double minX, double minY, double maxX, double maxY) GetCoordinateBounds(Dataset dataset)
        {
            double[] geoTransform = new double[6];
            dataset.GetGeoTransform(geoTransform);

            double minX = geoTransform[0];
            double maxY = geoTransform[3];
            double maxX = geoTransform[0] + geoTransform[1] * dataset.RasterXSize;
            double minY = geoTransform[3] + geoTransform[5] * dataset.RasterYSize;

            return (minX, minY, maxX, maxY);
        }

        // Method to get the first three bands of the GeoTIFF as an image
        public Bitmap GetFirstThreeBandsAsImage(Dataset dataset)
        {
            int width = dataset.RasterXSize;
            int height = dataset.RasterYSize;
            int bandCount = 3; // We are only interested in the first three bands

            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format24bppRgb);

            for (int i = 1; i <= bandCount; i++)
            {
                Band band = dataset.GetRasterBand(i);
                int[] buffer = new int[width * height];
                band.ReadRaster(0, 0, width, height, buffer, width, height, 0, 0);

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        Color color = bitmap.GetPixel(x, y);
                        int value = buffer[y * width + x];
                        if (i == 1)
                            color = Color.FromArgb(value, color.G, color.B);
                        else if (i == 2)
                            color = Color.FromArgb(color.R, value, color.B);
                        else if (i == 3)
                            color = Color.FromArgb(color.R, color.G, value);

                        bitmap.SetPixel(x, y, color);
                    }
                }
            }

            return bitmap;
        }

        // Method to save the image as a JPEG file
        public void SaveImageAsJpeg(Bitmap bitmap, string outputPath)
        {
            bitmap.Save(outputPath, ImageFormat.Jpeg);
        }
    }
}
