using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace LogSlicer
{
    class LogJournal
    {
        public static List<LogJournal> Journals;

        public string FilePath { get; set; }

        public DateTime[] Times
        {
            get 
            { 
                string[] lines = File.ReadAllLines(this.FilePath);
                DateTime[] results = new DateTime[lines.Length];

                for (int i = 0; i < lines.Length; i++)
                {
                    DateTime dt = Convert.ToDateTime(lines[i].Substring(0, 20));
                    results[i] = dt;
                }
                return results;
            }
        }
        

        public string Name
        {
            get
            {
                return Path.GetFileName(this.FilePath);
            }
        }

        public string Type
        {
            get
            {
                Regex rgx = new Regex(".ininlog_journal");
                return rgx.Replace(this.Name, "");
            }
        }

        

        public LogJournal(string filePath)
        {
            this.FilePath = filePath;
        }

        public static List<LogJournal> LoadJournals(string folderName)
        {
            string[] journalFiles = Directory.GetFiles(folderName, "*.ininlog_journal");
            
            if (journalFiles.Length == 0)
            {
                return new List<LogJournal>();
            }

            return journalFiles.Select(journalFile => new LogJournal(journalFile)).ToList();
        }

        public DateTime[] TimesForIndex(int index)
        {
            string[] lines = File.ReadAllLines(this.FilePath);
            List<DateTime> dtList = new List<DateTime>();

            for (int i = 0; i < lines.Length; i++)
            {
                string indexString = (index > 0) ? "_" + index.ToString() : "";

                if (lines[i].EndsWith(this.Type + indexString + ".ininlog"))
                {
                    DateTime dt = Convert.ToDateTime(lines[i].Substring(0, 20));
                    dtList.Add(dt);
                }
            }

            DateTime[] results = dtList.ToArray();

            return results;
        }

        public DateTime GetStartTime(IninLog log)
        {
            //Get first 20 characters for
            //Start.*this.Name ie Start    Path/To/Log/mylogfile.ininlog

            string logName = log.Name.Replace(".zip", ".ininlog");
            DateTime results;

            string[] lines = File.ReadAllLines(this.FilePath);

            Regex rgx = new Regex("Start.*" + logName);
            try
            {
                string startLine = lines.First(x => rgx.Match(x).Success);
                results = Convert.ToDateTime(startLine.Substring(0, 20));
            }
            //if we can't find the time from the Journal, use the min of the file create time and earliest time in journal
            catch (InvalidOperationException)
            {
                DateTime firstEntry = Convert.ToDateTime(lines[0].Substring(0, 20));
                DateTime fileCreation = File.GetCreationTime(log.FilePath);
                results = new DateTime(Math.Min(firstEntry.Ticks, fileCreation.Ticks));
            }
            
            return results;
        }

        /// <summary>
        /// Gets the end time for a particular log file
        /// </summary>
        /// <param name="log">Log file to get end time for</param>
        /// <returns></returns>
        public DateTime GetEndTime(IninLog log)
        {
            //Similar to GetStartTime but for End

            string logName = log.Name.Replace(".zip", ".ininlog");
            DateTime results;
            string[] lines = File.ReadAllLines(this.FilePath);

            Regex rgx = new Regex("End.*" + logName);
            try
            {
                string endLine = lines.First(x => rgx.Match(x).Success);
                results = Convert.ToDateTime(endLine.Substring(0, 20));
            }
            //if we can't find the time from the Journal, use the max of the file last modified time and latest time in journal
            catch(InvalidOperationException)
            {
                DateTime lastEntry = Convert.ToDateTime(lines.Last().Substring(0, 20));
                DateTime fileLastModified = File.GetLastWriteTime(log.FilePath);
                results = new DateTime(Math.Max(lastEntry.Ticks, fileLastModified.Ticks));
            }
            
            return results;
        }
    }
}
