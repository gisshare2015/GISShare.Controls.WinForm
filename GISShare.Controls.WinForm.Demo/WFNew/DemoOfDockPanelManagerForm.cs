using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.Demo.WFNew
{
    public partial class DemoOfDockPanelManagerForm : GISShare.Controls.WinForm.WFNew.Forms.TBForm //Form
    {
        private readonly string LayoutFlie_TBForm = Application.StartupPath + "\\WFNew_DemoOfDockPanelManagerForm_TBForm.xml";
        private readonly string LayoutFlie_DockPanel = Application.StartupPath + "\\WFNew_DemoOfDockPanelManagerForm_DockPanel.xml";

        public DemoOfDockPanelManagerForm()
        {
            InitializeComponent();
            //
            this.dockPanelManager1.AddSetComponent();//ÉèÖÃ×é¼þ
        }

        private void DemoOfDockPanelManagerForm_Load(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(LayoutFlie_DockPanel)) this.dockPanelManager1.LoadLayoutFile(LayoutFlie_DockPanel, true);
            if (System.IO.File.Exists(LayoutFlie_TBForm)) this.LoadLayoutFile(LayoutFlie_TBForm, false);
        }

        private void DemoOfDockPanelManagerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (System.IO.File.Exists(LayoutFlie_TBForm)) this.SaveLayoutFile(LayoutFlie_TBForm);
            if (System.IO.File.Exists(LayoutFlie_DockPanel)) this.dockPanelManager1.SaveLayoutFile(LayoutFlie_DockPanel);
        }

        private void btnOpen_MouseClick(object sender, MouseEventArgs e)
        {
            if (System.IO.File.Exists(LayoutFlie_DockPanel)) this.richTextBox1.LoadFile(LayoutFlie_DockPanel, RichTextBoxStreamType.PlainText);
        }
    }
}