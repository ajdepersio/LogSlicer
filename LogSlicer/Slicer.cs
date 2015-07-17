using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;


namespace LogSlicer
{
    /// <summary>
    /// Provides methods for snipping .ininlog files and methods for dealing with logsnip.exe
    /// </summary>
    static class Slicer
    {
        private static string _pathToLogSnip;
        private static string _outputFolder;
        private static List<string> _outputFilePaths = new List<string>();
        private static List<Process> _logSnipProcesses = new List<Process>();

        /// <summary>
        /// File path to logsnip.exe
        /// </summary>
        public static string PathToLogSnip
        {
            get
            {
                return _pathToLogSnip;
            }
            private set
            {
                _pathToLogSnip = value;
            }
        }

        /// <summary>
        /// Folder to output files to
        /// </summary>
        public static string OutputFolder
        {
            get
            {
                return _outputFolder;
            }
            set
            {
                //do some stuff to ensure we can write to folder
                _outputFolder = value;
            }
        }

        /// <summary>
        /// List of all files that have been outputted from the program (log snips and otherwise)
        /// </summary>
        public static List<string> OutputFilePaths
        {
            get
            {
                return _outputFilePaths;
            }
            private set
            {
                _outputFilePaths = value;
            }
        }

        /// <summary>
        /// Instances of logsnip.exe
        /// </summary>
        public static List<Process> LogSnipProcesses
        {
            get
            {
                return _logSnipProcesses;
            }
            private set
            {
                _logSnipProcesses = value;
            }
        }

        /// <summary>
        /// Try to find logsnip.exe either based on config, common locations, or manual specification
        /// </summary>
        public static void LoadLogSnip()
        {
            string logSnipPath = Config.Load("LogSnipPath");

            PathToLogSnip = logSnipPath;
            if (logSnipPath == null || !(File.Exists(logSnipPath)))
            {
                string foundPath = FindLogSnipPath();
                if (foundPath != null)
                {
                    SetLogSnipPath(foundPath);
                    return;
                }
                else
                {
                    logSnipPath = SelectLogSnip();
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
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "exe files (*.exe)|*.exe|All files (*.*)|*.*";
            dialog.InitialDirectory = "C:";
            dialog.Title = "Select logsnip.exe";

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
            if (!(Path.GetFileName(logSnipPath).StartsWith("logsnip")))
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
            //TODO Update this so logsnip version is considered.  Also so it's not as janky...

            List<string> searchPaths = new List<string> 
            { 
                "D:\\i3\\ic\\ININ Trace Initialization\\logsnip.exe",
                "D:\\ic\\ININ Trace Initialization\\logsnip.exe",
                "E:\\i3\\ic\\ININ Trace Initialization\\logsnip.exe",
                "E:\\ic\\ININ Trace Initialization\\logsnip.exe",
                "C:\\i3\\ic\\ININ Trace Initialization\\logsnip.exe",
                "C:\\ic\\ININ Trace Initialization\\logsnip.exe",
                "C:\\Program Files (x86)\\Interactive Intelligence\\ININ Trace Initialization\\logsnip.exe",
                "C:\\Program Files\\Interactive Intelligence\\ININ Trace Initialization\\logsnip.exe"
            };
            
            foreach (string path in searchPaths)
            {
                if(File.Exists(path))
                {
                    return path;
                }
            }
            return null;
        }

        /// <summary>
        /// Store logsnip.exe file path to app.config file, and sets property value
        /// </summary>
        /// <param name="logSnipPath">Path to the executable</param>
        private static void SetLogSnipPath(string logSnipPath)
        {
            PathToLogSnip = logSnipPath;
            Config.Save("LogSnipPath", logSnipPath);
        }

        /// <summary>
        /// Snip log files based off of start and end time
        /// </summary>
        /// <param name="journals">Logs to process</param>
        /// <param name="start">Start time to snip from</param>
        /// <param name="end">End time to stop snipping</param>
        public static void SnipLogs(List<ININLog> logs, DateTime start, DateTime end)
        {
            OutputFilePaths.Clear();
            List<ININLog> currentLogs = new List<ININLog>();
            foreach(string type in ININLog.SelectedLogTypes)
            {
                List<ININLog> logsOfType = logs.FindAll(x => x.Type == type);

                //No logs for that selected type within the time range
                if (logsOfType.Count == 0)
                {
                    continue;
                }
                
                foreach (ININLog log in logsOfType)
                {
                    //If the log is zipped, we'll need to unzip it
                    if (log.IsZipped)
                    {
                        Packer.UnZip(log.FilePath, OutputFolder + "\\temp\\");
                        //Remove the zip from currentLogs replace with unzipped log

                        ININLog l = new ININLog(OutputFolder + "\\temp\\" + Path.GetFileNameWithoutExtension(log.FilePath) + ".ininlog");
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
        /// <param name="journalFiles">Logs to snip, typically of the same type</param>
        /// <param name="start">Start time to snip from</param>
        /// <param name="end">End time to stop snipping</param>
        private static void RunLogSnip(List<ININLog> logFiles, DateTime start, DateTime end)
        {
            //Get each log file path and combine into one string
            List<string> logFilePaths = new List<string>();
            foreach(ININLog log in logFiles)
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

            Process logSnipProcess = new System.Diagnostics.Process();
            logSnipProcess.StartInfo.FileName = PathToLogSnip;
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