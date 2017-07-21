using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew.Forms
{
    public class TBMessageBox
    {
        public static DialogResult Show(string text)
        {
            TBMessageBoxForm tbMessageBoxForm = new TBMessageBoxForm(text);
            return tbMessageBoxForm.ShowDialog();
        }

        public static DialogResult Show(string text, string caption)
        {
            TBMessageBoxForm tbMessageBoxForm = new TBMessageBoxForm(text, caption);
            return tbMessageBoxForm.ShowDialog();
        }

        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons)
        {
            TBMessageBoxForm tbMessageBoxForm = new TBMessageBoxForm(text, caption, buttons);
            return tbMessageBoxForm.ShowDialog();
        }

        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            TBMessageBoxForm tbMessageBoxForm = new TBMessageBoxForm(text, caption, buttons, icon);
            return tbMessageBoxForm.ShowDialog();
        }

        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
        {
            TBMessageBoxForm tbMessageBoxForm = new TBMessageBoxForm(text, caption, buttons, icon, defaultButton);
            return tbMessageBoxForm.ShowDialog();
        }
    }
}
