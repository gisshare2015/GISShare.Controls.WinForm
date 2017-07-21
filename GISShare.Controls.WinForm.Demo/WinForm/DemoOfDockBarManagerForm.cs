using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.Demo.WinForm
{
    public partial class DemoOfDockBarManagerForm : Form
    {
        private readonly string LayoutFlie = Application.StartupPath + "\\WinForm_DemoOfDockBarManagerForm_DockBar.xml";

        public DemoOfDockBarManagerForm()
        {
            InitializeComponent();
            //
            #region 添加自定义项
            this.dockBarManager1.BaseItems.Add(this.btniOpen);
            this.dockBarManager1.BaseItems.Add(this.btniAbout);
            this.dockBarManager1.BaseItems.Add(this.separatorItem2);
            this.dockBarManager1.BaseItems.Add(this.btniExist);
            #endregion
        }

        private void DemoOfDockBarManagerForm_Load(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(LayoutFlie)) this.dockBarManager1.LoadLayoutFile(LayoutFlie, true);
        }

        private void DemoOfDockBarManagerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (System.IO.File.Exists(LayoutFlie)) this.dockBarManager1.SaveLayoutFile(LayoutFlie);
        }

        private void miOpen_Click(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(LayoutFlie)) this.richTextBox1.LoadFile(LayoutFlie, RichTextBoxStreamType.PlainText);
        }

        private void miAbout_Click(object sender, EventArgs e)
        {
            GISShare.Controls.WinForm.InfoForm infoForm = new GISShare.Controls.WinForm.InfoForm();
            infoForm.ShowDialog();
        }

        private void miExist_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            this.lbliNum.Text = "当前行数：" + this.richTextBox1.Lines.Length;
        }
    }
}