using System.Diagnostics;

namespace LogSlicer
{
    static class IninRegistry
    {
        private static readonly string ININRegPath = @"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Interactive Intelligence";

        /// <summary>
        /// Exports the ININ Registry branch and adds it to the list of output files
        /// </summary>
        public static void ExportIninRegistry()
        {    
            string exportPath = Slicer.OutputFolder + "\\registry.reg";
            ExportKey(ININRegPath, exportPath);
            Slicer.OutputFilePaths.Add(exportPath);
        }

        /// <summary>
        /// Exports registry key to selected file location
        /// </summary>
        /// <param name="regKey">Key to export</param>
        /// <param name="savePath">File to save export to</param>
        private static void ExportKey(string regKey, string savePath)
        {
            string path = "\"" + savePath + "\"";
            string key = "\"" + regKey + "\"";

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
