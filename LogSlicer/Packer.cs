using Ionic.Zip;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;


namespace LogSlicer
{
    /// <summary>
    /// Provides methods to processing output files.  Compression and FTP support
    /// </summary>
    class Packer
    {
        
        private static readonly string _FTPAddress = "ftp://ftp.inin.com/upload/";  //TODO - Make this configurable from app.config
        private static string _ticket;

        /// <summary>
        /// Base FTP address location
        /// </summary>
        public static string FTPAddress
        {
            get
            {
                return _FTPAddress;
            }
        }

        /// <summary>
        /// "Ticket Number" Really is just used as the folder in ftp/upload/"Ticket"
        /// </summary>
        public static string Ticket
        {
            get
            {
                return _ticket;
            }
            set
            {
                _ticket = value;
            }
        }

        /// <summary>
        /// Compresses list of file paths into .zip file
        /// </summary>
        /// <param name="files">List of file paths to compress</param>
        /// <param name="outFile">Output file path of .zip file</param>
        public static void Zip(List<string> files, string outFile)
        {
            ZipFile zip = new ZipFile();

            foreach (Process p in Slicer.LogSnipProcesses)
            {
                p.WaitForExit();
            }

            foreach (string file in files)
            {
                zip.AddFile(file, "\\");
            }

            zip.Save(outFile);
            
            foreach (string file in files)
            {
                File.Delete(file);
            }

            Slicer.OutputFilePaths.Clear();
            Slicer.OutputFilePaths.Add(outFile);
        }

        /// <summary>
        /// Extracts contents of .zip file
        /// </summary>
        /// <param name="file">.zip file to extract</param>
        /// <param name="outFolder">folder to extract contents to</param>
        public static void UnZip(string file, string outFolder)
        {
            ZipFile zip = ZipFile.Read(file);

            foreach (ZipEntry e in zip)
            {
                e.Extract(outFolder, ExtractExistingFileAction.OverwriteSilently);
            }
        }

        /// <summary>
        /// Creates FTP session
        /// </summary>
        /// <param name="username">FTP username</param>
        /// <param name="password">FTP password</param>
        /// <param name="FTPAddress">FTP address</param>
        /// <param name="requestMethod">Type of method to be used</param>
        /// <returns></returns>
        private static FtpWebRequest Login(string username, string password, string FTPAddress, string requestMethod)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(FTPAddress);

            request.Proxy = new WebProxy(); //-----The requested FTP command is not supported when using HTTP proxy.
            request.Method = requestMethod;
            request.Credentials = new NetworkCredential(username, password);
            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = false;

            return request;
        }

        /// <summary>
        /// Uploads files to FTP
        /// </summary>
        /// <param name="request">FTP session</param>
        /// <param name="file">File to upload</param>
        /// <param name="bw">Background worker to track process</param>
        /// <returns></returns>
        private static string Upload(FtpWebRequest request, string file, BackgroundWorker bw = null)
        {
            string results;
            try
            {
                using (var input = File.OpenRead(file))
                using (var requestStream = request.GetRequestStream())
                {
                    //increase this if needed
                    var buffer = new byte[4096 * 4096];
                    int totalReadBytesCount = 0;
                    int readBytesCount;
                    while ((readBytesCount = input.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        requestStream.Write(buffer, 0, readBytesCount);
                        totalReadBytesCount += readBytesCount;
                        var progress = totalReadBytesCount * 100.0 / input.Length;

                        if (bw != null)
                        {
                            bw.ReportProgress((int)progress);
                        }
                    }

                }
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                
                results = "Upload File Complete, status: " + response.StatusDescription;
                response.Close();
            }
            catch (System.Net.WebException e)
            {
                results = e.Message;
            }
            return results;
        }

        /// <summary>
        /// Creates FTP sessions and uploads logs
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="ticket"></param>
        /// <param name="filePath"></param>
        /// <param name="bw"></param>
        /// <returns></returns>
        public static string SendToSupport(string username, string password, string ticket, string filePath, BackgroundWorker bw = null)
        {
            string FTPRequestPath = FTPAddress + ticket + "/" + Path.GetFileName(filePath);
            FtpWebRequest request = Login(username, password, FTPRequestPath, WebRequestMethods.Ftp.UploadFile);
            return Upload(request, filePath, bw);
        }
    }
}
