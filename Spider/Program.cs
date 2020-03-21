using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Spider
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //ShowResultSearch showRes = new ShowResultSearch();
            //HWInfoGeneral showPC = new HWInfoGeneral();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new form1());
        }
    }
}
