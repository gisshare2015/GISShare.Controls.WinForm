using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.Demo
{
    public partial class Form4 : Form
    {
        Controls.WinForm.WFNew.View.NodeViewItem m_A = new Controls.WinForm.WFNew.View.NodeViewItem("A");
        Controls.WinForm.WFNew.View.NodeViewItem m_AA = new Controls.WinForm.WFNew.View.NodeViewItem("AA");
        Controls.WinForm.WFNew.View.NodeViewItem m_AAA = new Controls.WinForm.WFNew.View.NodeViewItem("AAA");
        public Form4()
        {
            GISShare.Controls.WinForm.WFNew.Forms.TBFormSkinHelper v =
                new Controls.WinForm.WFNew.Forms.TBFormSkinHelper(this);

            InitializeComponent();
            this.comboTree1.NodeViewItems.Add(m_A);
            m_A.NodeViewItems.Add(m_AA);
            m_AA.NodeViewItems.Add(m_AAA);


            this.zoomableImageBox1.DefaultExtent(true);
            //this.zoomableImageBox1.FullExtent(true);

           
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            System.Console.WriteLine(m_A.NodeDepth);
            System.Console.WriteLine(m_AA.NodeDepth);
            System.Console.WriteLine(m_AAA.NodeDepth);
        }

        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    //base.OnPaint(e);
        //}
    }
}
