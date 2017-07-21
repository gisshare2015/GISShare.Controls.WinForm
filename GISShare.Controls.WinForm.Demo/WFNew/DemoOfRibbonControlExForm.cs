using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.Demo.WFNew
{
    public partial class DemoOfRibbonControlExForm : GISShare.Controls.WinForm.WFNew.RibbonForm //Form
    {
        private readonly string LayoutFlie = Application.StartupPath + "\\WFNew_DemoOfRibbonControlExForm_RibbonControlEx.xml";

        public DemoOfRibbonControlExForm()
        {
            InitializeComponent();
            //
            this.MainMenuStrip = this.ribbonControlEx1.MenuStrip;
            //
            RichTextBox richTextBox = new RichTextBox();
            richTextBox.Dock = DockStyle.Fill;
            richTextBox.TextChanged += new EventHandler(richTextBox_TextChanged);
            richTextBox.Text = "RibbonControl与RibbonControlEx最大的差别在于其RibbonPages集合接纳的是组件（RibbonPageItem）还是控件（RibbonPage）";
            GISShare.Controls.WinForm.WFNew.Forms.TBForm form = new GISShare.Controls.WinForm.WFNew.Forms.TBForm();
            form.Name = "Form1";
            form.Text = "文本窗体";
            form.MdiParent = this;
            form.Size = new Size(600, 360);
            form.Controls.Add(richTextBox);
            form.Show();
        }
        private void richTextBox_TextChanged(object sender, EventArgs e)
        {
            RichTextBox richTextBox = sender as RichTextBox;
            this.lblNumInfo.Text = richTextBox.TextLength.ToString();
            this.lblNumInfo.Refresh();
        }

        private void cbSkin1_SelectedIndexChanged(object sender, GISShare.Controls.WinForm.IntValueChangedEventArgs e)
        {
            switch (cbSkin1.SelectedIndex) 
            {
                case 0:
                    this.ribbonControlEx1.eRibbonStyle = GISShare.Controls.WinForm.WFNew.RibbonStyle.eOffice2007;
                    break;
                case 1:
                    this.ribbonControlEx1.eRibbonStyle = GISShare.Controls.WinForm.WFNew.RibbonStyle.eOffice2010;
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
                    this.ribbonControlEx1.eRibbonStyle = GISShare.Controls.WinForm.WFNew.RibbonStyle.eOffice2007;
                    break;
                case 1:
                    this.ribbonControlEx1.eRibbonStyle = GISShare.Controls.WinForm.WFNew.RibbonStyle.eOffice2010;
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
            if (System.IO.File.Exists(LayoutFlie)) this.ribbonControlEx1.LoadLayoutFile(LayoutFlie, true);
        }

        protected override void OnClosed(EventArgs e)
        {
            if (System.IO.File.Exists(LayoutFlie)) this.ribbonControlEx1.SaveLayoutFile(LayoutFlie);
            //
            base.OnClosed(e);
        }
    }
}