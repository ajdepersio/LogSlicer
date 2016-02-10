using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace LogSlicer
{
    /// <summary>
    /// Object representing a .ininlog file and various other properties unique to such
    /// </summary>
    class IninLog
    {
        public static List<IninLog> Logs;
        public static List<string> SelectedLogTypes;

        /// <summary>
        /// Filepath to .ininlog file
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Name of the log file
        /// </summary>
        public string Name
        {
            get
            {
                return Path.GetFileName(this.FilePath);
            }
        }

        /// <summary>
        /// The log type (ip, acdserver, notifier, etc)
        /// </summary>
        public string Type
        {
            get
            {
                Regex rgx;
                if (this.IsZipped)
                {
                    rgx = new Regex("_?\\d*.zip");
                }
                else
                {
                    rgx = new Regex("_?\\d*.ininlog");
                }
                
                return rgx.Replace(this.Name, "");
            }
        }

        /// <summary>
        /// Is the log compressed?
        /// </summary>
        public bool IsZipped
        {
            get
            {
                return this.Name.EndsWith(".zip");
            }
        }

        /// <summary>
        /// Index of log for it's type
        /// </summary>
        private int TypeIndex
        {
            get
            {
                int index;
                Int32.TryParse(Regex.Match(Path.GetFileName(this.FilePath), @"_([^\.]*)\.").Groups[1].Value, out index);
                
                return (index > 0) ? index : 0;
            }
        }

        /// <summary>
        /// Creation Time of the log file.  Uses journal file if available
        /// </summary>
        public DateTime CreateDate 
        { 
            get
            {
                if (this.Journal != null)
                {
                    DateTime dt = this.Journal.GetStartTime(this);
                    return dt;
                }
                else
                {
                    return File.GetCreationTime(this.FilePath);
                }
            }
        }

        /// <summary>
        /// Last write time of the log file.  Uses journal file if available
        /// </summary>
        public DateTime EndDate
        {
            get
            {
                if (this.Journal != null)
                {
                    DateTime dt = this.Journal.GetEndTime(this);
                    return dt;
                }
                else
                {
                    return File.GetLastWriteTime(this.FilePath);
                }
            }
        }

        /// <summary>
        /// Corresponding .ininlog_journal file that contains start and end times of logs
        /// </summary>
        public LogJournal Journal
        {
            get
            {
                return LogJournal.Journals.Find(x => x.Type == this.Type);
            }
        }

        /// <summary>
        /// Creates a new IninLog object
        /// </summary>
        /// <param name="filePath">Filepath to .ininlog file</param>
        public IninLog(string filePath)
        {
            this.FilePath = filePath;
        }

        /// <summary>
        /// Creates IninLog objects for each .ininlog file in a given directory
        /// </summary>
        /// <param name="folderName">Folder containing .ininlog files</param>
        /// <returns>List of IninLog objects from the folder</returns>
        public static List<IninLog> LoadLogs(string folderName)
        {
            string[] logFiles = Directory.GetFiles(folderName, "*.ininlog");
            string[] zipFiles = Directory.GetFiles(folderName, "*.zip");
            
            if (logFiles.Length == 0 && zipFiles.Length == 0)
            {
                return new List<IninLog>();
            }
            List<IninLog> logs = logFiles.Select(logFile => new IninLog(logFile)).ToList();

            //Add zip files if there's a corresponding .ininlog_journal file
            Regex rgx = new Regex("_?\\d*.zip");
            logs.AddRange(from zipFile in zipFiles let type = rgx.Replace(Path.GetFileName(zipFile), "") 
                          where LogJournal.Journals.Find(x => x.Type == type) != null 
                          select new IninLog(zipFile));

            return logs;
        }

        /// <summary>
        /// Returns all logs in the SelectedLogTypes property
        /// </summary>
        /// <returns></returns>
        public static List<IninLog> FindSelectedLogs()
        {
            List<IninLog> results = new List<IninLog>();
            foreach (string type in SelectedLogTypes)
            {
                List<IninLog> logsOfType = LogsOfType(type);
                results.AddRange(logsOfType);
            }
            return results;
        }

        /// <summary>
        /// Return list of input logs within start and end time-range
        /// </summary>
        /// <param name="logs">Logs to filter</param>
        /// <param name="start">Beginning Datetime</param>
        /// <param name="end">Ending Datetime</param>
        /// <returns></returns>
        public static List<IninLog> FilterLogsByTime(List<IninLog> logs, DateTime start, DateTime end)
        {
            return logs.Where(log => !(log.CreateDate >= end || log.EndDate <= start)).ToList();
        }

        /// <summary>
        /// Returns all logs of a given type
        /// </summary>
        /// <param name="type">Type of logs to return</param>
        /// <returns></returns>
        private static List<IninLog> LogsOfType(string type)
        {
            return Logs.Where(log => log.Type == type).ToList();
        }

        /// <summary>
        /// Returns start and end time for an Interaction ID
        /// </summary>
        /// <param name="interactionId">Interaction ID to get start/end time</param>
        /// <returns>Start and End time of Interaction</returns>
        public Tuple<DateTime, DateTime> GetInteractionTime(string interactionId)
        {
            //TODO Provide implementation
            //Read from CallLog log for start/end time.  Investigate snapshot source for implementation of log reading API
            return Tuple.Create(DateTime.Now, DateTime.Now);
        }
    }
}