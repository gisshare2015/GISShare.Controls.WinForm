using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.Demo.WinForm
{
    public partial class DemoOfDataGridViewXForm : Form
    {
        public DemoOfDataGridViewXForm()
        {
            InitializeComponent();
            //
            this.dataGridViewX1.Rows.Add("µã", "Point", System.DateTime.Now.Year + "-" + System.DateTime.Now.Month + "-" + System.DateTime.Now.Day);
            this.dataGridViewX1.Rows.Add("Ïß", "Polyline", System.DateTime.Now.Year + "-" + System.DateTime.Now.Month + "-" + System.DateTime.Now.Day);
            this.dataGridViewX1.Rows.Add("Ãæ", "Polygon", System.DateTime.Now.Year + "-" + System.DateTime.Now.Month + "-" + System.DateTime.Now.Day);
        }
    }
}