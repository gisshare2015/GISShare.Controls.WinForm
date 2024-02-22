using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.Demo.WFNew.WFNew_JDKJJH
{
    public partial class DemoOfButtonValueBoxForm : GISShare.Controls.WinForm.WFNew.Forms.TBForm //Form
    {
        public DemoOfButtonValueBoxForm()
        {
            InitializeComponent();
        }

        private void buttonValueBoxItem1_ButtonClick(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) 
                {
                    this.buttonValueBoxItem1.ValueItem = new GISShare.Controls.WinForm.WFNew.View.ColorViewItem() { Color = colorDialog.Color };
                }                
            }
        }
    }
}
