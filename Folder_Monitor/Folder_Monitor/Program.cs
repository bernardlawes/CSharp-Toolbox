using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Serilog;

namespace Folder_Monitor
{
    /// <summary>
    /// The main class for the Folder Monitor application.
    /// </summary>
    class Program
    {
        // Dictionary to keep track of the last event time for each file
        private static readonly Dictionary<string, DateTime> LastEventTime = new Dictionary<string, DateTime>();
        // Time threshold to filter out duplicate events
        private static readonly TimeSpan EventThreshold = TimeSpan.FromMilliseconds(500);

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <param name="args">The command-line arguments.</param>
        static void Main(string[] args)
        {
            // Initialize Serilog
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            Log.Information("Application Starting");

            // Start monitoring and transferring files
            MonitorAndTransferFiles();
            Console.ReadLine();

            Log.Information("Application Ending");
            Log.CloseAndFlush();
        }

        // The destination path for file transfers
        static string destinationPath = @"D:\_WORKING\Destination";

        /// <summary>
        /// Monitors the source path and transfers files to the destination path.
        /// </summary>
        /// <param name="sourcePath">The source path to monitor. Defaults to an empty string.</param>
        static void MonitorAndTransferFiles(string sourcePath = "")
        {
            // sourcePath = Directory.GetCurrentDirectory();
            sourcePath = @"D:\_WORKING\Source";
            WatchFiles(sourcePath);
        }

        /// <summary>
        /// Sets up a file system watcher to monitor the specified path.
        /// </summary>
        /// <param name="path">The path to monitor.</param>
        static void WatchFiles(string path)
        {
            FileSystemWatcher sentry = new FileSystemWatcher();
            sentry.Path = path;
            sentry.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.CreationTime;
            sentry.Filter = "*.*";
            sentry.Changed += sentry_Changed;
            sentry.Created += sentry_Created;
            sentry.EnableRaisingEvents = true;

            Log.Information("Started watching path: {Path}", path);
        }

        /// <summary>
        /// Handles the Created event of the FileSystemWatcher.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FileSystemEventArgs"/> instance containing the event data.</param>
        private static void sentry_Created(object sender, FileSystemEventArgs e)
        {
            Log.Information("sentry_Created event triggered for {FullPath}", e.FullPath);

            if (IsDuplicateEvent(e.FullPath))
            {
                Log.Information("Duplicate event detected for {FullPath}", e.FullPath);
                return;
            }

            try
            {
                Log.Information("Created | FullPath: {FullPath}, ChangeType: {ChangeType}", e.FullPath, e.ChangeType);
                File.Copy(e.FullPath, Path.Combine(destinationPath, Path.GetFileName(e.FullPath)), true);
                Log.Information("File copied successfully after creation: {FullPath}", e.FullPath);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error copying file after File Creation | {FullPath}", e.FullPath);
            }
        }

        /// <summary>
        /// Handles the Changed event of the FileSystemWatcher.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FileSystemEventArgs"/> instance containing the event data.</param>
        private static void sentry_Changed(object sender, FileSystemEventArgs e)
        {
            Log.Information("sentry_Changed event triggered for {FullPath}", e.FullPath);

            if (IsDuplicateEvent(e.FullPath))
            {
                Log.Information("Duplicate event detected for {FullPath}", e.FullPath);
                return;
            }

            try
            {
                Log.Information("Changed | FullPath: {FullPath}, ChangeType: {ChangeType}", e.FullPath, e.ChangeType);
                File.Copy(e.FullPath, Path.Combine(destinationPath, Path.GetFileName(e.FullPath)), true);
                Log.Information("File copied successfully after change: {FullPath}", e.FullPath);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error copying file after File Change | {FullPath}", e.FullPath);
            }
        }

        /// <summary>
        /// Checks if the event is a duplicate based on the time threshold.
        /// </summary>
        /// <param name="fullPath">The full path of the file.</param>
        /// <returns>True if the event is a duplicate, otherwise false.</returns>
        private static bool IsDuplicateEvent(string fullPath)
        {
            var now = DateTime.Now;
            if (LastEventTime.TryGetValue(fullPath, out var lastEventTime))
            {
                if ((now - lastEventTime) < EventThreshold)
                {
                    return true;
                }
            }
            LastEventTime[fullPath] = now;
            return false;
        }
    }
}