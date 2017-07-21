using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.Plugin.WinForm
{
    public partial class UIViewTBForm : GISShare.Controls.WinForm.WFNew.Forms.TBForm// Form
    {
        public UIViewTBForm(GISShare.Controls.WinForm.WFNew.View.NodeViewItem nodeViewItem)
        {
            InitializeComponent();
            //
            //
            //
            this.nodeViewItemTree1.NodeViewItems.Add(nodeViewItem);
        }

        private void btnExpandAll_MouseClick(object sender, MouseEventArgs e)
        {
            this.nodeViewItemTree1.ExpandAll();
        }

        private void btnCollapseAll_MouseClick(object sender, MouseEventArgs e)
        {
            this.nodeViewItemTree1.CollapseAll();
        }

        private void btnInfo_MouseClick(object sender, MouseEventArgs e)
        {
            GISShare.Controls.Plugin .WinForm.InfoForm infoForm = new GISShare.Controls.Plugin.WinForm.InfoForm();
            infoForm.ShowDialog();
        }
    }
}
