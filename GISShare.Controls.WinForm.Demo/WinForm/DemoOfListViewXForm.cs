using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.Demo.WinForm
{
    public partial class DemoOfListViewXForm : Form
    {
        public DemoOfListViewXForm()
        {
            InitializeComponent();
            //
            this.cbView.SelectedIndex = 0;
        }

        private void cbView_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.cbView.SelectedIndex) 
            {
                case 0:
                    this.listViewX1.View = View.LargeIcon;
                    break;
                case 1:
                    this.listViewX1.View = View.Details;
                    break;
                case 2:
                    this.listViewX1.View = View.SmallIcon;
                    break;
                case 3:
                    this.listViewX1.View = View.List;
                    break;
                case 4:
                    if (this.listViewX1.CheckBoxes)
                    {
                        MessageBox.Show("显示复选框后，将不支持“Tile”视图！");
                    }
                    else
                    {
                        this.listViewX1.View = View.Tile;
                    }
                    break;
                default:
                    break;
            }
        }

        private void chShowCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chShowCheckBox.Checked && 
                this.listViewX1.View == View.Tile)
            {
                MessageBox.Show("“Tile”视图，将不支持显示复选框！");
                this.chShowCheckBox.Checked = false;
            }
            else
            {
                this.listViewX1.CheckBoxes = this.chShowCheckBox.Checked;
            }
        }
    }
}