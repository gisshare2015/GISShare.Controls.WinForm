using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.Demo
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
            //
            for (int i = 0; i < 1000; i++) 
            {
                //this.viewItemListBoxExItem1.AddCheckedItem(false, i.ToString(), i.ToString(), i, null);
                //this.comboSearchBoxItem1.AddCheckedItem(false, i.ToString(), i.ToString(), i, null);
                this.gridViewItemListBoxItem1.AddRowViewItem(GISShare.Controls.WinForm.WFNew.View.RowCellViewStyle.eSystemRow, 18, i, i);
            }
            //this.viewItemListBoxExItem1.UpdateViewItems();
            //this.comboSearchBoxItem1.UpdateItems();

            this.gridViewItemListBoxItem1.ClearRowViewItem();
            this.gridViewItemListBoxItem1.ColumnViewItems.Clear();

            IList<Item> list = new List<Item>();
            //list.Add(new Item() { Name = "qqq", Text = "1111" });
            this.gridViewItemListBoxItem1.DataSource = list;
        }

        Controls.WinForm.WFNew.CustomizePopup pop;
        private void button1_Click(object sender, EventArgs e)
        {

            if (pop == null)
            {
                var p = new Panel () { Width = 100, Height = 100, AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink };
                pop = new Controls.WinForm.WFNew.CustomizePopup(p);
                p.Width = 100;
                p.Height = 100;
                pop.SetSize(new Size(100, 100));
                //pop = new Controls.WinForm.WFNew.CustomizePopup(new Controls.WinForm.WFNew.BaseItemHost()
                //{
                //    Width = 100,
                //    Height = 300,
                //    BaseItemObject = new Controls.WinForm.WFNew.ControlHostItem() 
                //    {   
                //        eHAlignmentStyle = GISShare.Controls.WinForm.WFNew.HAlignmentStyle.eStretch,
                //        eVAlignmentStyle = GISShare.Controls.WinForm.WFNew.VAlignmentStyle.eStretch,
                //        ControlObject = new Button()
                //        {
                //            Dock = DockStyle.Fill
                //        }
                //    }
                //});
            }
            pop.Show(this, 100, 100);
        }
    }
}
