using System;
using System.Data;
using ScottPlot;
using System.Windows;

namespace sandbox.scottplot.console.app
{
    internal class Program
    {

        static void simpleLinePlot(string filepath)
        {
            // Line Plot
            double[] dataX = new double[] { 1, 2, 3, 4, 5 };
            double[] dataY = new double[] { 1, 4, 9, 16, 25 };
            ScottPlot.Plot plt = new();
            plt.Add.Scatter(dataX, dataY);
            plt.SavePng(filepath, 400, 300);
            Console.Write("Result saved to: " + filepath);
        }

        static void basicPlot(string filepath)
        {
            // Scatter Plot
            ScottPlot.Plot plt = new();

            // sample data
            double[] xs = Generate.Consecutive(51);
            double[] sin = Generate.Sin(51);
            double[] cos = Generate.Cos(51);

            // plot the data
            plt.Add.Scatter(xs, sin);
            plt.Add.Scatter(xs, cos);

            // customize the axis labels
            plt.Title("ScottPlot Quickstart");
            plt.XLabel("Horizontal Axis");
            plt.YLabel("Vertical Axis");
            plt.SavePng(filepath, 400, 300);
            Console.Write("Result saved to: " + filepath);
        }

        static void Main(string[] args)
        {

            int choice = 0;
            string filepath = string.Empty;

            // Validate choice input
            while (true)
            {
                Console.WriteLine("Choose a plot type:");
                Console.WriteLine("1. Simple Line Plot");
                Console.WriteLine("2. Basic Scatter Plot");
                Console.Write("Enter your choice (1 or 2): ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out choice) && (choice == 1 || choice == 2))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please enter 1 or 2.");
                }
            }

            // Validate filepath input
            while (true)
            {
                Console.Write("Enter a filepath to save the plot image: ");
                filepath = Console.ReadLine();

                if (Path.IsPathFullyQualified(filepath))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid filepath. Please enter a fully qualified path.");
                }
            }

            switch (choice)
            {
                case 1:
                    simpleLinePlot(filepath);
                    break;
                case 2:
                    basicPlot(filepath);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please enter 1 or 2.");
                    break;
            }

            Console.WriteLine("\n\nPress any key to exit...\n\n");
        }
    }
}