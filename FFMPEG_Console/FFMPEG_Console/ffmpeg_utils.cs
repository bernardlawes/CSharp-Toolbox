using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Audio_Video_FFMPEG
{
    class ffmpeg_utils
    {
        public static String ExtractAudio(String FileIn, String FolderOut, String Folder_ffmpeg, String StartTime, String EndTime, Int32 Duration, Int32 Quality)
        {

            String msg = String.Empty;


            // ----------------- Validate Mandatory Parameters --------------------------------



            if (!System.IO.File.Exists(FileIn))
            {
                msg = "Error: Input File Not Found, " + FileIn;
            }

            if (System.IO.Path.GetExtension(FileIn).ToLower() != ".mp4")
            {
                msg = "Error: Input File must be of type (.mp4)";
            }

            if (!System.IO.Path.IsPathRooted(FolderOut))
            {
                msg = "Output Folder must be Fully Rooted: " + FolderOut;
            }

            if (!System.IO.File.Exists(Folder_ffmpeg + "bin\\" + "ffmpeg.exe"))
            {
                msg = "Error: Please provide a valid path to your FFMPEG directory: " + Folder_ffmpeg;
            }

            if (msg.Length > 0)
            {
                return msg;
            }

            string FileOut = System.IO.Path.Combine(FolderOut, System.IO.Path.GetFileNameWithoutExtension(FileIn) + ".mp3");

            // ----------------- Validate Optional Parameters --------------------------------

            StartTime = (StartTime.Length > 0) ? " -ss " + StartTime : "";
            EndTime = (EndTime.Length > 0) ? " -to " + EndTime : "";
            String DurationStr = (EndTime.Length == 0 && Duration > 0) ? " -t " + TimeSpan.FromSeconds(Duration).ToString(@"hh\:mm\:ss") : "";
            String QualityStr = (Quality < 0) ? "0" : Quality.ToString();

            // ----------------- Execute Process via SHELL --------------------------------
            System.Diagnostics.Process process = new System.Diagnostics.Process();

            // Configure the process using the StartInfo properties.
            process.StartInfo.FileName = Folder_ffmpeg + "bin\\" + "ffmpeg";
            process.StartInfo.Arguments = " -i \"" + FileIn + "\" " + StartTime + " " + EndTime + " " + DurationStr + " -q:a " + QualityStr + " -map a -y \"" + FileOut + "\"";
            process.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
            process.Start();
            process.WaitForExit();

            bool result = System.IO.File.Exists(FileOut);

            msg = (result) ? "Audio Extraction Successful" : "Audio Extraction Failed";

            return msg;




        }
    }
}
