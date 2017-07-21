using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.Demo
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            //this.nodeViewItemTree1.CanEdit = true;

            ////this.gridNodeViewItemTree1.ShowOutLine = false;
            //this.gridNodeViewItemTree1.CanExchangeColumn = true;
            //this.gridNodeViewItemTree1.CanEdit = true;
            //this.gridNodeViewItemTree1.CanResizeRowHeight = true;

            //this.gridNodeViewItemTree1.ColumnViewItems.Add(new Controls.WinForm.WFNew.View.ColumnViewItem() { Text = "AAA", Width = 100 });
            //this.gridNodeViewItemTree1.ColumnViewItems.Add(new Controls.WinForm.WFNew.View.ColumnViewItem() { Text = "BBB", Width = 100 });
            //this.gridNodeViewItemTree1.ColumnViewItems.Add(new Controls.WinForm.WFNew.View.ColumnViewItem() { Text = "CCC", Width = 100 });
            //for (int i = 0; i < 10; i++)
            //{
            //    GISShare.Controls.WinForm.WFNew.View.NodeViewItem nodexxx = new GISShare.Controls.WinForm.WFNew.View.NodeViewItem() { CanEdit = true, Text = i.ToString() };
            //    this.nodeViewItemTree1.NodeViewItems.Add(nodexxx);


            //    GISShare.Controls.WinForm.WFNew.View.IRowNodeCellViewItem node = this.gridNodeViewItemTree1.AddRowViewItem(null, GISShare.Controls.WinForm.WFNew.View.RowCellViewStyle.eSystemRow, "A" + i, "B" + i, "C" + i);
            //    //GISShare.Controls.WinForm.WFNew.View.IRowNodeCellViewItem node2 = this.gridNodeViewItemTree1.AddRowViewItem(null, GISShare.Controls.WinForm.WFNew.View.RowCellViewStyle.eSystemRow, "A_" + i, "B_" + i, "C_" + i);                
            //    for (int j = 0; j < 10; j++)
            //    {
            //        nodexxx.NodeViewItems.Add(new GISShare.Controls.WinForm.WFNew.View.NodeViewItem() { CanEdit = true, Text = j.ToString() });

            //        this.gridNodeViewItemTree1.AddRowViewItem(node, GISShare.Controls.WinForm.WFNew.View.RowCellViewStyle.eSystemRow, "A_J" + j, "B_J" + j, "C_J" + j);
            //        //this.gridNodeViewItemTree1.AddRowViewItem(node, GISShare.Controls.WinForm.WFNew.View.RowCellViewStyle.eSystemRow, "A_J_" + j, "B_J_" + j, "C_J_" + j);                                   
            //    }
            //    node.ForeColor = System.Drawing.Color.Black;
            //}
            //this.gridNodeViewItemTree1.ColumnViewItems.Add(new Controls.WinForm.WFNew.View.ColumnViewItem() { Text = "DDD", Width = 100 });

            //this.gridViewItemListBox1.CanExchangeColumn = true;
            //this.gridViewItemListBox1.ColumnViewItems.Add(new Controls.WinForm.WFNew.View.ColumnViewItem() { Text = "AAA", Width = 100 });
            //this.gridViewItemListBox1.ColumnViewItems.Add(new Controls.WinForm.WFNew.View.ColumnViewItem() { Text = "BBB", Width = 100 });
            //this.gridViewItemListBox1.ColumnViewItems.Add(new Controls.WinForm.WFNew.View.ColumnViewItem() { Text = "CCC", Width = 100 });
            //this.gridViewItemListBox1.AddRowViewItem(GISShare.Controls.WinForm.WFNew.View.RowCellViewStyle.eSystemRow, "A", "B", "C");
            //this.gridViewItemListBox1.ColumnViewItems.Add(new Controls.WinForm.WFNew.View.ColumnViewItem() { Text = "DDD", Width = 100 });


            //this.comboBoxN1.eModifySizeStyle = GISShare.Controls.WinForm.WFNew.ModifySizeStyle.eAll;
            ////this.comboBoxN1.
            //for (int i = 0; i < 10; i++)
            //{
            //    GISShare.Controls.WinForm.WFNew.View.SuperViewItem item = new Controls.WinForm.WFNew.View.SuperViewItem();
            //    item.BaseItemObject = new GISShare.Controls.WinForm.WFNew.ImageCheckBoxItem() {
            //        Text = i.ToString(), 
            //        TextAlign = ContentAlignment.MiddleLeft,
            //        eHAlignmentStyle = GISShare.Controls.WinForm.WFNew.HAlignmentStyle.eStretch, 
            //        eVAlignmentStyle = GISShare.Controls.WinForm.WFNew.VAlignmentStyle.eStretch
            //    };
            //    this.comboBoxN1.Items.Add(item );
            //}

            //System.Data.DataTable tb = new DataTable();
            //tb.Columns.Add("A").Caption = "1";
            //tb.Columns.Add("B");
            //tb.Columns.Add("C");
            //tb.Columns.Add("D");
            //tb.Columns.Add("E");
            //tb.Rows.Add("A", "B", "C", "D", "E");
            //tb.Rows.Add("A", "B", "C", "D", "E");
            //tb.Rows.Add("A", "B", "C", "D", "E");
            //tb.Rows.Add("A", "B", "C", "D", "E");
            //tb.Rows.Add("A", "B", "C", "D", "E");
            //tb.Rows.Add("A", "B", "C", "D", "E");
            //tb.Rows.Add("A", "B", "C", "D", "E");
            //this.gridViewItemListBox1.DataSource = tb;

            //List<Button> list = new List<Button>();
            //list.Add(new GISShare.Controls.WinForm.WFNew.View.ViewItem() { Text = "aa" });

            //this.gridViewItemListBox1.DataSource = list ;

            //for (int i = 0; i < 10; i++)
            //{
            //    this.comboTree1.AddCheckedItem(null, false, i.ToString(), i.ToString(), null, null);
            //}

            //System.Data.DataTable tb = new DataTable();
            //tb.Columns.Add("A");
            //tb.Columns.Add("B");
            //tb.Columns.Add("C");
            //tb.Columns.Add("D");
            //tb.Columns.Add("E");
            //tb.Columns.Add("ID");
            //tb.Columns.Add("ParentID");
            //tb.Rows.Add("A", "B", "C", "D", "E", "1", "");
            //tb.Rows.Add("A", "B", "C", "D", "E", "2", "");
            //tb.Rows.Add("A", "B", "C", "D", "E", "3", "");
            //tb.Rows.Add("A", "B", "C", "D", "E", "1.1", "1");
            //tb.Rows.Add("A", "B", "C", "D", "E", "1.1.1", "1.1");
            //tb.Rows.Add("A", "B", "C", "D", "E", "2.1", "2");
            //tb.Rows.Add("A", "B", "C", "D", "E", "3.1", "3");
            //this.gridNodeViewItemTree1.SetDataSource(tb, "ID", "ParentID");

            List<NodeItem> nodeItemList = new List<NodeItem>();
            nodeItemList.Add(new NodeItem() { A = "A", B = "B", C = "C", D = "D", E = "E", ID = "1", ParentID = "" });
            nodeItemList.Add(new NodeItem() { A = "A", B = "B", C = "C", D = "D", E = "E", ID = "2", ParentID = "" });
            nodeItemList.Add(new NodeItem() { A = "A", B = "B", C = "C", D = "D", E = "E", ID = "3", ParentID = "" });
            nodeItemList.Add(new NodeItem() { A = "A", B = "B", C = "C", D = "D", E = "E", ID = "1.1", ParentID = "1" });
            nodeItemList.Add(new NodeItem() { A = "A", B = "B", C = "C", D = "D", E = "E", ID = "1.1.1", ParentID = "1.1" });
            nodeItemList.Add(new NodeItem() { A = "A", B = "B", C = "C", D = "D", E = "E", ID = "2.1", ParentID = "2" });
            nodeItemList.Add(new NodeItem() { A = "A", B = "B", C = "C", D = "D", E = "E", ID = "3.1", ParentID = "3" });
            //this.gridNodeViewItemTree1.SetDataSource(nodeItemList, "ID", "ParentID");
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            //
            //Console.WriteLine("ssssssssssssss");
        }

        //
        //
        //

        class FormShow : Form 
        {
            public new void Show()
            {
                base.Show();
                this.TopMost = true;
                //
                GISShare.Win32.API.SendMessage(
                    this.Handle,
                    (int)GISShare.Win32.Msgs.WM_LBUTTONDOWN,
                    0,
                    (uint)GISShare.Win32.NativeMethods.MousePositionToLParam(new Point(10, 10))
                    );
            }

            protected override void OnMouseMove(MouseEventArgs e)
            {
                this.Location = this.PointToScreen( e.Location);
                //base.OnMouseMove(e);
            }

            protected override void OnMouseUp(MouseEventArgs e)
            {
                this.Close();
                base.OnMouseUp(e);

                //GISShare.Win32.API.SendMessage(
                //    this.Handle,
                //    (int)GISShare.Win32.Msgs.WM_MBUTTONUP,
                //    0,
                //    (uint)GISShare.Win32.NativeMethods.MousePositionToLParam(new Point(10, 10))
                //    );
            }
        }

        FormShow m_FormShow = null;
        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            this.m_FormShow = new FormShow();
            this.m_FormShow.Show();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        //

        class NodeItem
        {
            public string A { get; set; }
            public string B { get; set; }
            public string C { get; set; }
            public string D { get; set; }
            public string E { get; set; }
            public string ID { get; set; }
            public string ParentID { get; set; }
        }
    }
}
