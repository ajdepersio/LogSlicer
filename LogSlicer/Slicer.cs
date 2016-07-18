using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;


namespace LogSlicer
{
    /// <summary>
    /// Provides methods for snipping .ininlog files and methods for dealing with logsnip.exe
    /// </summary>
    static class Slicer
    {
        /// <summary>
        /// File path to logsnip.exe
        /// </summary>
        public static string LogSnipPath { get; private set; }

        /// <summary>
        /// Folder to output files to
        /// </summary>
        public static string OutputFolder { get; set; }

        /// <summary>
        /// List of all files that have been outputted from the program (log snips and otherwise)
        /// </summary>
        public static List<string> OutputFilePaths { get; private set; } = new List<string>();

        /// <summary>
        /// Instances of logsnip.exe
        /// </summary>
        public static List<Process> LogSnipProcesses { get; private set; } = new List<Process>();
        
        /// <summary>
        /// Try to find logsnip.exe either based on config, common locations, or manual specification
        /// </summary>
        public static void LoadLogSnip()
        {
            string logSnipPath = Config.Load("LogSnipPath");

            LogSnipPath = logSnipPath;
            if (logSnipPath == null || !(File.Exists(logSnipPath)))
            {
                string foundPath = FindLogSnipPath();
                if (foundPath != null)
                {
                    SetLogSnipPath(foundPath);
                }
                else
                {
                    LogSnipPath = SelectLogSnip();
                }
                
            }
        }

        /// <summary>
        /// Present user with dialog box to manually select logsnip.exe
        /// </summary>
        /// <returns>Selected file path</returns>
        private static string SelectLogSnip()
        {
            string logSnipPath = "";
            //.exe not found, so ask user to manually find it
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "exe files (*.exe)|*.exe|All files (*.*)|*.*",
                InitialDirectory = Path.GetPathRoot(Environment.SystemDirectory),
                Title = "Select logsnip.exe"
            };

            switch (dialog.ShowDialog())
            {
                case (DialogResult.OK):
                    {
                        logSnipPath = dialog.FileName;
                        break;
                    }
                case (DialogResult.Cancel):
                    {
                        Environment.Exit(1);
                        break;
                    }
            }

            //User picked the wrong file
            string fileName = Path.GetFileName(logSnipPath);
            if (fileName != null && !(fileName.StartsWith("logsnip")))
            {
                MessageBox.Show("Please select logsnip.exe (\\Interactive Intelligence\\ININ Trace Initialization\\)");
                LoadLogSnip();
            }
            else
            {
                SetLogSnipPath(logSnipPath);
            }
            return logSnipPath;
        }

        /// <summary>
        /// Find file path to logsnip.exe based off of common locations
        /// </summary>
        /// <returns>File path to logsnip.exe</returns>
        private static string FindLogSnipPath()
        {
            //TODO Update this so logsnip version is considered.

            List<DriveInfo> drives = DriveInfo.GetDrives().Where(x => x.DriveType == DriveType.Fixed).ToList();
            List<string> searchPaths = new List<string>();

            foreach (DriveInfo drive in drives)
            {
                searchPaths.Add(drive.Name + "i3\\ic\\ININ Trace Initialization\\logsnip.exe");
                searchPaths.Add(drive.Name + "ic\\ININ Trace Initialization\\logsnip.exe");
                searchPaths.Add(drive.Name + "Program Files\\Interactive Intelligence\\ININ Trace Initialization\\logsnip.exe");
                searchPaths.Add(drive.Name + "Program Files (x86)\\Interactive Intelligence\\ININ Trace Initialization\\logsnip.exe");
            }

            return searchPaths.FirstOrDefault(path => File.Exists(path));
        }

        /// <summary>
        /// Store logsnip.exe file path to app.config file, and sets property value
        /// </summary>
        /// <param name="logSnipPath">Path to the executable</param>
        private static void SetLogSnipPath(string logSnipPath)
        {
            LogSnipPath = logSnipPath;
            Config.Save("LogSnipPath", logSnipPath);
        }

        /// <summary>
        /// Snip log files based off of start and end time
        /// </summary>
        /// <param name="logs">Logs to process</param>
        /// <param name="start">Start time to snip from</param>
        /// <param name="end">End time to stop snipping</param>
        public static void SnipLogs(List<IninLog> logs, DateTime start, DateTime end)
        {
            OutputFilePaths.Clear();
            List<IninLog> currentLogs = new List<IninLog>();
            foreach(string type in IninLog.SelectedLogTypes)
            {
                List<IninLog> logsOfType = logs.FindAll(x => x.Type == type);

                //No logs for that selected type within the time range
                if (logsOfType.Count == 0)
                {
                    continue;
                }
                
                foreach (IninLog log in logsOfType)
                {
                    //If the log is zipped, we'll need to unzip it
                    if (log.IsZipped)
                    {
                        Packer.UnZip(log.FilePath, OutputFolder + "\\temp\\");
                        //Remove the zip from currentLogs replace with unzipped log

                        IninLog l = new IninLog(OutputFolder + "\\temp\\" + Path.GetFileNameWithoutExtension(log.FilePath) + ".ininlog");
                        currentLogs.Add(l);
                    }
                    else
                    {
                        currentLogs.Add(log);
                    }
                }
                RunLogSnip(currentLogs, start, end);
                currentLogs.Clear();
            }
        }

        /// <summary>
        /// Build process arguments for logsnip.exe with the given parameters and start snipping journals
        /// </summary>
        /// <param name="logFiles">Logs to snip, typically of the same type</param>
        /// <param name="start">Start time to snip from</param>
        /// <param name="end">End time to stop snipping</param>
        private static void RunLogSnip(List<IninLog> logFiles, DateTime start, DateTime end)
        {
            //Get each log file path and combine into one string
            List<string> logFilePaths = new List<string>();
            foreach(IninLog log in logFiles)
            {
                logFilePaths.Add(log.FilePath);
            }
            
            string logFilesString = string.Join("\" \"", logFilePaths);
            string outFile = OutputFolder + "\\" + logFiles[0].Type + "_snip.ininlog";

            //Set process information for logsnip.exe
            string arguments = String.Format(
                    "--log {0} --out {1} --from {2} --to {3}", 
                    "\""+logFilesString+"\"",
                    "\"" + outFile + "\"", 
                    start.ToString("yyyy'-'MM'-'dd'@'HH':'mm':'ss.fff"), 
                    end.ToString("yyyy'-'MM'-'dd'@'HH':'mm':'ss.fff"));

            Process logSnipProcess = new Process();
            logSnipProcess.StartInfo.FileName = LogSnipPath;
            logSnipProcess.StartInfo.Arguments = arguments;
            logSnipProcess.Start();

            //Add process ID and output files to appropriate properties
            LogSnipProcesses.Add(logSnipProcess);
            OutputFilePaths.Add(outFile);
            OutputFilePaths.Add(outFile + ".ininlog_idx");
            OutputFilePaths.Add(outFile + "_journal");
        }
    }
}