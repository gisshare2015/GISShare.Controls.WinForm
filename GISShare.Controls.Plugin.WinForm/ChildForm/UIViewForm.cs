using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.Plugin.WinForm
{
    public partial class UIViewForm : Form
    {
        public UIViewForm(GISShare.Controls.WinForm.WFNew.View.NodeViewItem nodeViewItem)
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
    }
}
