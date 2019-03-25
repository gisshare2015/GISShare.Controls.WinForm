using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.Demo
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form5());
            Application.Run(new DemoCenterForm());
            //Application.Run(new RibbonControlForm());
        }
    }
}