using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace Audio_Video_FFMPEG
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter path to video file:");
            string videoFile = Console.ReadLine();
            Console.WriteLine("Please enter output path");
            string outputFolder = Console.ReadLine();
            Console.WriteLine("Please enter path to FFMPEG Library");
            string ffmpegFolder = Console.ReadLine();
            Console.WriteLine("Please enter start time in minutes and seconds 00:00");
            string startTime = Console.ReadLine();
            Console.WriteLine("Please enter end time in minutes and seconds 00:00");
            string endTime = Console.ReadLine();
            
            // Loop until a valid integer is entered for duration
            int duration;
            while (true)
            {
                Console.WriteLine("Please enter duration in seconds");
                string durationStr = Console.ReadLine();
                if (int.TryParse(durationStr, out duration))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid duration. Please enter a valid integer.");
                }
            }

            // Loop until a valid integer is entered for quality
            int quality;
            while (true)
            {
                Console.WriteLine("Please enter quality (default is 0)");
                string qualityStr = Console.ReadLine();
                if (int.TryParse(qualityStr, out quality))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid quality. Please enter a valid integer.");
                }
            }

            Console.WriteLine("You entered: \n" + 
                            "Video File: "+ videoFile + "\n"+ 
                            "Output Folder: " + outputFolder + "\n" +
                            "FFMPEG Folder: " + ffmpegFolder + "\n" +
                            "Start Time: " + startTime + "\n" +
                            "End Time: " + endTime + "\n" +
                            "Duration: " + duration + "\n" +
                            "Quality: " + quality + "\n");


            String msg = ffmpeg_utils.ExtractAudio(videoFile, outputFolder, ffmpegFolder, startTime, endTime, duration, quality);

            String title = msg.ToLower().Contains("error") || msg.ToLower().Contains("fail") ? "Error" : "Information";

        }
    }
}