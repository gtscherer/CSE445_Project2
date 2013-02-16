using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CSE445_Project2
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
            Application.Run(new Form1());
            /* 
             * Start Hotel Supplier Threads
             * Create buffer classes
             * Instantiate objects
             * Create Threads
             * Start Threads
             */ 

        }
    }
}
