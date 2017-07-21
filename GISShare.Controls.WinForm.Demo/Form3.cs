using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace GISShare.Controls.WinForm.Demo
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            //
            //
            //
            GISShare.Controls.WinForm.WFNew.View.FlexibleRowViewItem flexibleRowViewItem = 
                new Controls.WinForm.WFNew.View.FlexibleRowViewItem();
            flexibleRowViewItem.Width = 300;
            //
            GISShare.Controls.WinForm.WFNew.View.FlexibleVRowViewItem flexibleVRowViewItem = 
                new Controls.WinForm.WFNew.View.FlexibleVRowViewItem();
            flexibleVRowViewItem.ViewItems.Add(new GISShare.Controls.WinForm.WFNew.View.TextViewItem());
            flexibleVRowViewItem.ViewItems.Add(new GISShare.Controls.WinForm.WFNew.View.TextViewItem());
            flexibleVRowViewItem.Width = 150;
            GISShare.Controls.WinForm.WFNew.View.TextViewItem textViewItem = 
                new Controls.WinForm.WFNew.View.TextViewItem();
            //
            flexibleRowViewItem.ViewItems.Add(flexibleVRowViewItem);
            flexibleRowViewItem.ViewItems.Add(textViewItem);
            viewItemListBox1.ViewItems.Add(flexibleRowViewItem);
            this.viewItemListBox1.ShowHScrollBar = true;
        }

        private void baseButtonN1_Click(object sender, EventArgs e)
        {
            Size size = new System.Drawing.Size(10, 10);
            Size size2 = new System.Drawing.Size(10, 10);
            //
            Stopwatch watch = new Stopwatch();
            watch.Start();
            size = size2;
            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds);
            //
            watch.Start();
            if (size == size2) { size = size2; }
            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds);

            //M m = new M();
            //M2 m2 = new M2();
            //MessageBox.Show(m.Info2 + " - " + m2.Info2);
        }

        //
        //
        //

        interface IM
        {
            string Info { get; }
        }

        class M : IM
        {
            string IM.Info { get { return "AAA"; } }

            public string Info2 { get { return ((IM)this).Info; } }
        }

        class M2 : M, IM
        {
            public string Info { get { return "BBB"; } }
        }

        private void baseItemStackItem1_MouseDown(object sender, MouseEventArgs e)
        {
            Console.WriteLine("baseItemStackItem1_MouseDown");
        }

        private void buttonItem1_MouseDown(object sender, MouseEventArgs e)
        {
            Console.WriteLine("buttonItem1_MouseDown");
        }

        private void baseItemStackItem1_MouseLeave(object sender, EventArgs e)
        {

        }

        private void buttonItem1_MouseLeave(object sender, EventArgs e)
        {

        }

        private void baseItemStackItem1_KeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine("baseItemStackItem1_KeyDown");
        }

        private void buttonItem1_KeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine("buttonItem1_KeyDown");
        }

        private void viewItemListBox1_MouseDown(object sender, MouseEventArgs e)
        {
            Console.WriteLine("viewItemListBox1_MouseDown");
        }

        private void viewItemListBox1_MouseUp(object sender, MouseEventArgs e)
        {
            Console.WriteLine("viewItemListBox1_MouseUp");
        }

        //private void viewItemListBox1_MouseMove(object sender, MouseEventArgs e)
        //{
        //    Console.WriteLine("viewItemListBox1_MouseMove");
        //}
    }
}
