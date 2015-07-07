using System;
using System.IO;
using System.Windows.Forms;

namespace LogSlicer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string ionicResource = "LogSlicer.Ionic.Zip.dll";
            EmbeddedAssembly.Load(ionicResource, "Ionic.Zip.dll");
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit); 
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Slicer.LoadLogSnip();

            Application.Run(new Main());
        }

        /// <summary>
        /// Cleanup files on exit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnProcessExit(object sender, EventArgs e)
        {
            string directory = Slicer.OutputFolder + "\\temp\\";
            if (Directory.Exists(directory))
            {
                string[] files = Directory.GetFiles(directory);
                string[] dirs = Directory.GetDirectories(directory);

                foreach (string file in files)
                {
                    File.SetAttributes(file, FileAttributes.Normal);
                    File.Delete(file);
                }

                Directory.Delete(directory, true);
            }
        }

        static System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            return EmbeddedAssembly.Get(args.Name);
        }
    }
}
