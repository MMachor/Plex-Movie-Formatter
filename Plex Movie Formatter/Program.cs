using System;
using System.Windows.Forms;

namespace Plex_Movie_Formatter
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Plex_Movie_Formatter());
        }
    }
}
