using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace LogSlicer
{
    /// <summary>
    /// Form to provide login information for ftp.inin.com
    /// </summary>
    public partial class FTPLogin : Form
    {
        private static string _username = Config.Load("FTPUserName", true);
        private static string _password = Config.Load("FTPPassword", true);
        BackgroundWorker bwFTPUpload = new BackgroundWorker();
        
        /// <summary>
        /// FTP Username
        /// </summary>
        public static string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
            }
        }

        /// <summary>
        /// FTP Password
        /// </summary>
        public static string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
            }
        }
        

        public FTPLogin()
        {
            InitializeComponent();

            //Create background worker to do upload and report progress
            bwFTPUpload.WorkerReportsProgress = true;
            bwFTPUpload.DoWork += new DoWorkEventHandler(bw_UploadLogs);
            bwFTPUpload.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_UploadLogsComplete);
            bwFTPUpload.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            
            userTextBox.Text = Username;
            passwordTextBox.Text = Password;
        }

        /// <summary>
        /// Background worker method to upload journals to ftp.inin.com
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bw_UploadLogs(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            Username = userTextBox.Text;
            Password = passwordTextBox.Text;

            foreach (string file in Slicer.OutputFilePaths)
            {
                e.Result = Packer.SendToSupport(FTPLogin.Username, FTPLogin.Password, Packer.Ticket, file, worker);
            }
        }

        /// <summary>
        /// Background Worker progress monitor to update progress bar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        /// <summary>
        /// Background Worker cleanup method.  Saves FTP credentials if selected+successful
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bw_UploadLogsComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show(e.Result.ToString());
            if (e.Result.ToString().StartsWith("Upload File Complete"))
            {
                if (saveCredsCheckBox.Checked)
                {
                    saveCredentials();
                }
                this.Close();
                return;
            }
            else
            {
                //TODO error handling
            }
        }

        /// <summary>
        /// Runs background worker to upload journals
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loginButton_Click(object sender, EventArgs e)
        {
            if (!(bwFTPUpload.IsBusy))
            {
                bwFTPUpload.RunWorkerAsync();
            }
        }

        /// <summary>
        /// Give message and close the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Credentials are needed to upload to ftp.inin.com");
            this.Close();
        }

        /// <summary>
        /// Save the FTP credentials to the app.config file encrypted
        /// </summary>
        private void saveCredentials()
        {
            Config.Save("FTPUserName", userTextBox.Text, true);
            Config.Save("FTPPassword", passwordTextBox.Text, true);
        }
    }
}
