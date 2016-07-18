using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Security.Permissions;
using System.Threading;
using System.Windows.Forms;
using LogSlicer.UI;

namespace LogSlicer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread, SecurityPermission(SecurityAction.Demand, Flags=SecurityPermissionFlag.ControlAppDomain)]
        static void Main()
        {

            // Add the event handler for handling UI thread exceptions to the event.
            Application.ThreadException += new ThreadExceptionEventHandler(ErrorReporting.UIThreadException);

            // Set the unhandled exception mode to force all Windows Forms errors to go through
            // our handler.
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            // Add the event handler for handling non-UI thread exceptions to the event. 
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(ErrorReporting.CurrentDomain_UnhandledException);


            //Load embedded assembly used for zipping/unzipping
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

                foreach (string file in files)
                {
                    File.SetAttributes(file, FileAttributes.Normal);
                    File.Delete(file);
                }

                Directory.Delete(directory, true);
            }
        }

        static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            return EmbeddedAssembly.Get(args.Name);
        }
    }
}
