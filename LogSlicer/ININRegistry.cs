using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Windows.Forms;
using System.Diagnostics;

namespace LogSlicer
{
    static class ININRegistry
    {
        private static readonly string ININRegPath = @"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Interactive Intelligence";

        /// <summary>
        /// Exports the ININ Registry branch and adds it to the list of output files
        /// </summary>
        public static void ExportININRegistry()
        {    
            string exportPath = Slicer.OutputFolder + "\\registry.reg";
            ExportKey(ININRegPath, exportPath);
            Slicer.OutputFilePaths.Add(exportPath);
        }

        /// <summary>
        /// Exports registry key to selected file location
        /// </summary>
        /// <param name="RegKey">Key to export</param>
        /// <param name="SavePath">File to save export to</param>
        private static void ExportKey(string RegKey, string SavePath)
        {
            string path = "\"" + SavePath + "\"";
            string key = "\"" + RegKey + "\"";

            Process proc = new Process();
            try
            {
                proc.StartInfo.FileName = "regedit.exe";
                proc.StartInfo.UseShellExecute = false;
                proc = Process.Start("regedit.exe", "/e " + path + " " + key + "");

                if (proc != null) proc.WaitForExit();
            }
            finally
            {
                if (proc != null) proc.Dispose();
            }
        }
    }
}
