using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.Demo.WFNew
{
    public partial class DemoOfTabControlForm : GISShare.Controls.WinForm.WFNew.Forms.TBForm // Form
    {
        public DemoOfTabControlForm()
        {
            InitializeComponent();
        }

        private void chCanExchangeItem_CheckedChanged(object sender, EventArgs e)
        {
            this.tabControl1.CanExchangeItem = this.chCanExchangeItem.Checked;
        }

        private void chUsingCloseTabButton_CheckedChanged(object sender, EventArgs e)
        {
            this.tabControl1.UsingCloseTabButton = this.chUsingCloseTabButton.Checked;
            this.tabControl1.Refresh();
        }

        private void chAutoShowOverflowTabButton_CheckedChanged(object sender, EventArgs e)
        {
            this.tabControl1.AutoShowOverflowTabButton = this.chAutoShowOverflowTabButton.Checked;
        }

        private void cbePNLayoutStyle_SelectedIndexChanged(object sender, GISShare.Controls.WinForm.IntValueChangedEventArgs e)
        {
            switch (this.cbePNLayoutStyle.SelectedIndex) 
            {
                case 0:
                    this.tabControl1.ePNLayoutStyle = GISShare.Controls.WinForm.WFNew.PNLayoutStyle.eHead;
                    this.tabControl1.Refresh();
                    break;
                case 1:
                    this.tabControl1.ePNLayoutStyle = GISShare.Controls.WinForm.WFNew.PNLayoutStyle.eTail;
                    this.tabControl1.Refresh();
                    break;
                case 2:
                    this.tabControl1.ePNLayoutStyle = GISShare.Controls.WinForm.WFNew.PNLayoutStyle.eBothEnds;
                    this.tabControl1.Refresh();
                    break;
                default:
                    break;
            }
        }

        private void cbeTabButtonContainerStyle_SelectedIndexChanged(object sender, GISShare.Controls.WinForm.IntValueChangedEventArgs e)
        {
            switch (this.cbeTabButtonContainerStyle.SelectedIndex)
            {
                case 0:
                    this.tabControl1.eTabButtonContainerStyle = GISShare.Controls.WinForm.WFNew.TabButtonContainerStyle.eCloseButton;
                    this.tabControl1.Refresh();
                    break;
                case 1:
                    this.tabControl1.eTabButtonContainerStyle = GISShare.Controls.WinForm.WFNew.TabButtonContainerStyle.eContextButton;
                    this.tabControl1.Refresh();
                    break;
                case 2:
                    this.tabControl1.eTabButtonContainerStyle = GISShare.Controls.WinForm.WFNew.TabButtonContainerStyle.eContextButtonAndCloseButton;
                    this.tabControl1.Refresh();
                    break;
                case 3:
                    this.tabControl1.eTabButtonContainerStyle = GISShare.Controls.WinForm.WFNew.TabButtonContainerStyle.ePreButtonAndNextButton;
                    this.tabControl1.Refresh();
                    break;
                default:
                    break;
            }
        }

    }
}