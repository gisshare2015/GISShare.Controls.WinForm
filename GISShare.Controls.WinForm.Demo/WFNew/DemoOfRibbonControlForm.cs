using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.Demo.WFNew
{
    public partial class DemoOfRibbonControlForm : GISShare.Controls.WinForm.WFNew.RibbonForm //Form
    {
        private readonly string LayoutFlie = Application.StartupPath + "\\WFNew_DemoOfRibbonControlForm_RibbonControl.xml";

        public DemoOfRibbonControlForm()
        {
            InitializeComponent();
        }

        private void cbSkin1_SelectedIndexChanged(object sender, GISShare.Controls.WinForm.IntValueChangedEventArgs e)
        {
            switch (cbSkin1.SelectedIndex) 
            {
                case 0:
                    this.ribbonControl1.eRibbonStyle = GISShare.Controls.WinForm.WFNew.RibbonStyle.eOffice2007;
                    break;
                case 1:
                    this.ribbonControl1.eRibbonStyle = GISShare.Controls.WinForm.WFNew.RibbonStyle.eOffice2010;
                    break;
                default:
                    break;
            }
            this.cbSkin2.SelectedIndex = this.cbSkin1.SelectedIndex;
        }

        private void cbSkin2_SelectedIndexChanged(object sender, GISShare.Controls.WinForm.IntValueChangedEventArgs e)
        {
            switch (cbSkin2.SelectedIndex)
            {
                case 0:
                    this.ribbonControl1.eRibbonStyle = GISShare.Controls.WinForm.WFNew.RibbonStyle.eOffice2007;
                    break;
                case 1:
                    this.ribbonControl1.eRibbonStyle = GISShare.Controls.WinForm.WFNew.RibbonStyle.eOffice2010;
                    break;
                default:
                    break;
            }
            this.cbSkin1.SelectedIndex = this.cbSkin2.SelectedIndex;
        }

        private void btnOffice2007_MouseClick(object sender, MouseEventArgs e)
        {
            this.cbSkin1.SelectedIndex = 0;
            this.cbSkin2.SelectedIndex = 0;
        }

        private void btnOffice2010_MouseClick(object sender, MouseEventArgs e)
        {
            this.cbSkin1.SelectedIndex = 1;
            this.cbSkin2.SelectedIndex = 1;
        }

        private void btnInfo_MouseClick(object sender, MouseEventArgs e)
        {
            GISShare.Controls.WinForm.InfoForm infoForm = new GISShare.Controls.WinForm.InfoForm();
            infoForm.ShowDialog();
        }

        private void btnExist_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            this.lblNumInfo.Text = this.richTextBox1.TextLength.ToString();
            this.lblNumInfo.Refresh();
        }

        private void btnAddControlHost_MouseClick(object sender, MouseEventArgs e)
        {
            Button btn = new Button();
            btn.Text = "我是被嵌入的按钮";
            GISShare.Controls.WinForm.WFNew.ControlHostItem ribbonControlHostItem = new GISShare.Controls.WinForm.WFNew.ControlHostItem();
            ribbonControlHostItem.ControlObject = btn;
            ribbonControlHostItem.Size = new Size(70, 23);
            this.rbAddControlHost.BaseItems.Add(ribbonControlHostItem);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //
            if (System.IO.File.Exists(LayoutFlie)) this.ribbonControl1.LoadLayoutFile(LayoutFlie, true);
        }

        protected override void OnClosed(EventArgs e)
        {
            if (System.IO.File.Exists(LayoutFlie)) this.ribbonControl1.SaveLayoutFile(LayoutFlie);
            //
            base.OnClosed(e);
        }


    }
}