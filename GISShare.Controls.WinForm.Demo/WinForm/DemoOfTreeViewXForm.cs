using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.Demo.WinForm
{
    public partial class DemoOfTreeViewXForm : Form
    {
        public DemoOfTreeViewXForm()
        {
            InitializeComponent();
            //
            this.cbNodeRegionStyle.SelectedIndex = 1;
        }

        private void chShowLines_CheckedChanged(object sender, EventArgs e)
        {
            this.treeViewX1.ShowLines = chShowLines.Checked;
        }

        private void chShowRootLines_CheckedChanged(object sender, EventArgs e)
        {
            this.treeViewX1.ShowRootLines = chShowRootLines.Checked;
        }

        private void chShowPlusMinus_CheckedChanged(object sender, EventArgs e)
        {
            this.treeViewX1.ShowPlusMinus = chShowPlusMinus.Checked;
        }

        private void chShowGripRegion_CheckedChanged(object sender, EventArgs e)
        {
            this.treeViewX1.ShowGripRegion = chShowGripRegion.Checked;
            this.treeViewX1.Refresh();
        }

        private void chAutoMouseMoveSelected_CheckedChanged(object sender, EventArgs e)
        {
            this.treeViewX1.AutoMouseMoveSeleced = chAutoMouseMoveSelected.Checked;
        }

        private void cbNodeRegionStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.cbNodeRegionStyle.SelectedIndex) 
            {
                case 0:
                    this.treeViewX1.eNodeRegionStyle = GISShare.Controls.WinForm.NodeRegionStyle.eRow;
                    break;
                case 1:
                    this.treeViewX1.eNodeRegionStyle = GISShare.Controls.WinForm.NodeRegionStyle.ePlusMinusToRight;
                    break;
                case 2:
                    this.treeViewX1.eNodeRegionStyle = GISShare.Controls.WinForm.NodeRegionStyle.eGripToRight;
                    break;
                case 3:
                    this.treeViewX1.eNodeRegionStyle = GISShare.Controls.WinForm.NodeRegionStyle.eTextToRight;
                    break;
                case 4:
                    this.treeViewX1.eNodeRegionStyle = GISShare.Controls.WinForm.NodeRegionStyle.eText;
                    break;
                default:
                    break;
            }
        }
    }
}