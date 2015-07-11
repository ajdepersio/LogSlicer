using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;
using LogSlicer.UI;

namespace LogSlicer
{
    /**
     * ********************************Future Improvements**********************************
     * 
     *  1. Auto-find logsnip.exe based on common locations                      DONE
     *  2. Compression support                                                  DONE
     *  2.5. Run Snipping on separate thread                                    DONE
     *  3. Set default and default start browsing In/Out folders                DONE
     *  4. Ability to Export ININ Registry                                      DONE
     *  5. FTP support                                                          DONE
     *  version 1
     *  6.0 Auto detect log start/end time                                      DONE
     *  6.1 Read into zipped log files                                          DONE
     *  version 2
     *  7.0 Move Quick Select info into app.config                              DONE
     *  7.1 Custom Quick Search Lists                                           DONE
     *  8. Implement better encryption for storing FTP credentials
     *  9. Snip based on Interaction ID
     *  version 3
     *
     */


    public partial class Main : Form
    {
        BackgroundWorker bwSnipLogs = new BackgroundWorker();
        
        public Main()
        {
            InitializeComponent();
            bwSnipLogs.WorkerReportsProgress = true;
            bwSnipLogs.DoWork += new DoWorkEventHandler(bw_SnipLogs);
            bwSnipLogs.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_SnipComplete);
            bwSnipLogs.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            LoadDefaultPaths();
            LoadQuickSelects();
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Background worker process to snip logs
        /// </summary>
        /// <param name="sender">Sender of background worker</param>
        /// <param name="e">Event arguments of worker</param>
        private void bw_SnipLogs(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            ININLog.SelectedLogTypes = logListBox.CheckedItems.Cast<string>().ToList();
            //Return logs of selected types
            List<ININLog> selectedLogs = ININLog.FindSelectedLogs();

            //If there's zipped logs, we'll want to filter the logs by time
            if (selectedLogs.Find(x => x.IsZipped == true) != null)
            {
                selectedLogs = ININLog.FilterLogsByTime(selectedLogs, startDateTimePicker.Value, endDateTimePicker.Value);
            }
            
            //No logs selected
            if (selectedLogs.Count == 0)
            {
                e.Result = 1;
                return;
            }
            //No output folder set
            if (outputTextBox.Text == "")
            {
                e.Result = 2;
                return;
            }

            Slicer.SnipLogs(selectedLogs, startDateTimePicker.Value, endDateTimePicker.Value);
            //Export registry with logs
            if (registryCheckBox.Checked == true)
            {
                ININRegistry.ExportININRegistry();
            }
            //Compress output if necissary
            if (zipCheckBox.Checked == true)
            {
                Packer.Zip(Slicer.OutputFilePaths, Slicer.OutputFolder + "//Logs" + DateTime.Now.ToString("_yyyyMMdd_HHmmss")+ ".zip");
            }
            e.Result = 0;
        }

        /// <summary>
        /// Cleanup for after snipping background worker
        /// </summary>
        /// <param name="sender">Sender of background worker</param>
        /// <param name="e">Event arguments of worker</param>
        private void bw_SnipComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            pbSnipping.Visible = false;

            //No logs selected
            if (e.Result.Equals(1))
            {
                MessageBox.Show("No valid logs found.  Please try again.");
                return;
            }
            //No output folder selected
            else if (e.Result.Equals(2))
            {
                MessageBox.Show("Please specify an output directory.");
                return;
            }

            //If support ticket given, upload to ftp.inin.com
            if (ticketTextBox.Text != "")
            {
                Packer.Ticket = ticketTextBox.Text;
                FTPLogin ftpLogin = new FTPLogin();
                ftpLogin.ShowDialog();
                //wait for snipping to stop before trying to upload logs to FTP
            }
        }

        

        /// <summary>
        /// Load default Log and Output folders
        /// </summary>
        private void LoadDefaultPaths()
        {
            //Get the current days logs folder based off of registry settings
            string logRegistryPath = "HKEY_LOCAL_MACHINE\\SYSTEM\\ControlSet001\\Control\\Session Manager\\Environment";

            string logFolderPath = (string)Registry.GetValue(logRegistryPath, "ININ_TRACE_ROOT", "");
            if (!(logFolderPath.EndsWith("\\")))
            {
                logFolderPath += "\\";
            }
            logFolderPath += System.DateTime.Now.ToString("yyyy'-'MM'-'dd");

            //If found, set the Logs folder path
            if (Directory.Exists(logFolderPath))
            {
                logsTextBox.Text = logFolderPath;
                LogJournal.Journals = LogJournal.LoadJournals(logFolderPath);
                ININLog.Logs = ININLog.LoadLogs(logFolderPath);

                if (ININLog.Logs.Count == 0)
                {
                    MessageBox.Show("No valid Log files found.  Please check the folder path and try again.");
                    return;
                }
                
                SetLogListData(ININLog.Logs);
            }

            //Load Output folder path from app.config
            string outFolderPath = Config.Load("DefaultOutFolderPath");            
            outputTextBox.Text = outFolderPath;
            Slicer.OutputFolder = outFolderPath;
        }

        /// <summary>
        /// Used to populate preconfigured log selects
        /// </summary>
        private void LoadQuickSelects()
        {
            List<QuickSelect> quickSelects = QuickSelect.LoadQuickSelectsFromConfig();

            foreach(QuickSelect quickSelect in quickSelects)
            {
                AddQuickSelect(quickSelect);
            }
        }

        /// <summary>
        /// Adds QuickSelect to the Menu bar
        /// </summary>
        /// <param name="quickSelect">Item to add</param>
        public void AddQuickSelect(QuickSelect quickSelect)
        {
            ToolStripMenuItem item = new ToolStripMenuItem(quickSelect.Name);
            item.Click += new EventHandler(quickSelect_Click);
            this.quickSelectToolStripMenuItem.DropDownItems.Add(item);
        }

        /// <summary>
        /// Populate logListBox with ININLog items
        /// </summary>
        /// <param name="logs">Logs to populate List Box with</param>
        private void SetLogListData(List<ININLog> logs)
        {
            if (logs == null)
            {
                logListBox.Items.Clear();
                return;
            }
            foreach (ININLog l in logs)
            {
                //Only add one item for each type
                if (logListBox.Items.Contains(l.Type))
                {
                    continue;
                }
                else
                {
                    logListBox.Items.Add(l.Type);
                }
            }
            SetDefaultDateTimes(logs);
        }

        /// <summary>
        /// Sets the default start/end times of logs based on earliest and latest create and last write time
        /// </summary>
        /// <param name="logs">Logs to get times from</param>
        private void SetDefaultDateTimes(List<ININLog> logs)
        {
            DateTime min = logs.Min(x => x.CreateDate);
            DateTime max = logs.Max(x => x.EndDate);

            //Set CreateDate to the beginning of the day if > EndDate (file moved to new location)
            if (min > max)
            {
                min = max.Date;
            }

            startDateTimePicker.Value = min;
            endDateTimePicker.Value = max;
        }

        /// <summary>
        /// Select items in logListBox of input types
        /// </summary>
        /// <param name="types">Types of logs to select</param>
        private void SelectLogs(List<String> types)
        {
            //Uncheck any currently selected logs
            foreach (int checkedItemIndex in logListBox.CheckedIndices)
            {
                logListBox.SetItemChecked(checkedItemIndex, false);
            }

            string missingLogsMessage = "The following logs were not found: \r";
            bool missingLogs = false;

            //Iterate over input list and check each type
            foreach (string type in types)
            {
                int i = logListBox.FindStringExact(type);
                if (i != -1)
                {
                    logListBox.SetItemChecked(i, true);
                }
                //if type isn't in logListBox, build message to alert user
                else
                {
                    missingLogs = true;
                    missingLogsMessage += type + "\r";
                }
            }
            //alert user of any log types not found in logListBox
            if (missingLogs)
            {
                MessageBox.Show(missingLogsMessage);
            }
        }

        /// <summary>
        /// Presents dialog to select log folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogsFolderBrowseButton_Click(object sender, EventArgs e)
        {
            ININLog.Logs = null;
            LogJournal.Journals = null;
            SetLogListData(null);
            DialogResult result = logsBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string folderName = logsBrowserDialog.SelectedPath;

                LogJournal.Journals = LogJournal.LoadJournals(folderName);
                ININLog.Logs = ININLog.LoadLogs(folderName);

                if (ININLog.Logs.Count == 0)
                {
                    MessageBox.Show("No valid Log files found.  Please check the folder path and try again.");
                    return;
                }
                
                SetLogListData(ININLog.Logs);
                logsTextBox.Text = folderName;
            }
        }

        /// <summary>
        /// Presents dialog to select output folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OutputFolderBrowseButton_Click(object sender, EventArgs e)
        {
            DialogResult result = outputBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string folderName = outputBrowserDialog.SelectedPath;
                Slicer.OutputFolder = folderName;
                outputTextBox.Text = folderName;

                Config.Save("DefaultOutFolderPath", folderName);
            }
        }

        /// <summary>
        /// Snips selected logs with specified settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sliceButton_Click(object sender, EventArgs e)
        {
            if (!(bwSnipLogs.IsBusy))
            {
                pbSnipping.Visible = true;
                bwSnipLogs.RunWorkerAsync();
            }
        }

        /// <summary>
        /// Close the program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
        
        /// <summary>
        /// Event handler for Quick Select Menu Item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void quickSelect_Click(object sender, EventArgs e)
        {
            SelectLogs(QuickSelect.QuickSelects.Find(x => x.Name == sender.ToString()).Types);
        }

        /// <summary>
        /// Creates new quick select set from current selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addCurrentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (logListBox.CheckedItems.Cast<string>().ToList().Count != 0)
            {
                LogSetTextBox popup = new LogSetTextBox(this, this.logListBox.CheckedItems.Cast<string>().ToList());
                popup.Text = "Enter Name For Log Set";
                popup.inputLabel.Text = "Name";
                popup.ShowDialog();
            }
            else
            {
                MessageBox.Show("You need to select some logs before trying to save the selection.");
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QuickSelectEditor editor = new QuickSelectEditor(this);
            editor.ShowDialog();
        }
    }
}
