using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Runtime.InteropServices;

namespace GISShare.Controls.WinForm.Demo
{
    public partial class Form1 : GISShare.Controls.WinForm.WFNew.Forms.TBForm// Form
    {
        public Form1()
        {
            this.InitializeComponent();
            //
            this.gridViewItemListBox1.CanExchangeColumn = true;
            //GISShare.Controls.WinForm.WFNew.View.FlexibleVRowViewItem rowViewItem = new Controls.WinForm.WFNew.View.FlexibleVRowViewItem() { Height = 21 };
            //this.viewItemListBox1.ViewItems.Add(rowViewItem);
            //rowViewItem.Width = 100;
            ////
            //rowViewItem.ViewItems.Add(
            //    new GISShare.Controls.WinForm.WFNew.View.SuperViewItem(
            //        new GISShare.Controls.WinForm.WFNew.TextBoxItem()
            //        {
            //            eVAlignmentStyle = GISShare.Controls.WinForm.WFNew.VAlignmentStyle.eStretch,
            //            eHAlignmentStyle = GISShare.Controls.WinForm.WFNew.HAlignmentStyle.eStretch
            //        }));
            //rowViewItem.ViewItems.Add(new GISShare.Controls.WinForm.WFNew.View.TextEditViewItem("AAAAAA") { Width = 100 });
            //rowViewItem.ViewItems.Add(new GISShare.Controls.WinForm.WFNew.View.TextEditViewItem("BBBBBB") { Width = 100 });
            //rowViewItem.ViewItems.Add(new GISShare.Controls.WinForm.WFNew.View.TextEditViewItem("CCCCCC") { Width = 100 });
            //this.viewItemListBox1.ViewItems.Add(new GISShare.Controls.WinForm.WFNew.View.ResizeViewItem("FFFFFF") { Width = 100 });
            //GISShare.Controls.WinForm.WFNew.View.SuperViewItem superViewItem1 = new Controls.WinForm.WFNew.View.SuperViewItem() { Height = 100 };
            //GISShare.Controls.WinForm.WFNew.View.ViewItemListBoxItem viewItemListBoxItem1 = new Controls.WinForm.WFNew.View.ViewItemListBoxItem();
            //viewItemListBoxItem1.ShowHScrollBar = true;
            //viewItemListBoxItem1.eHAlignmentStyle = GISShare.Controls.WinForm.WFNew.HAlignmentStyle.eCenter;
            //viewItemListBoxItem1.Size = new Size(100, 100);
            //for (int i = 0; i < 5; i++) { viewItemListBoxItem1.ViewItems.Add(new GISShare.Controls.WinForm.WFNew.View.TextViewItem(i.ToString())); }
            //superViewItem1.BaseItemObject = viewItemListBoxItem1;
            //viewItemListBox1.ViewItems.Add(superViewItem1);
            ////
            //GISShare.Controls.WinForm.WFNew.View.SuperViewItem superViewItem2 = new Controls.WinForm.WFNew.View.SuperViewItem() { Height = 70, Width = 160 };
            //GISShare.Controls.WinForm.WFNew.View.ViewItemListBoxItem viewItemListBoxItem2 = new Controls.WinForm.WFNew.View.ViewItemListBoxItem();
            //viewItemListBoxItem2.eHAlignmentStyle = GISShare.Controls.WinForm.WFNew.HAlignmentStyle.eStretch;
            //viewItemListBoxItem2.Size = new Size(100, 100);
            //for (int i = 0; i < 5; i++) { viewItemListBoxItem2.ViewItems.Add(new GISShare.Controls.WinForm.WFNew.View.TextViewItem(i.ToString())); }
            //superViewItem2.BaseItemObject = viewItemListBoxItem2;
            //viewItemListBoxItem1.ViewItems.Add(superViewItem2);

            //this.gridViewItemListBox1.Visible = false;
            for (int i = 0; i < 10; i++)
            {
                this.gridViewItemListBox1.ColumnViewItems.Add(new GISShare.Controls.WinForm.WFNew.View.ColumnViewItem() { Text = "Volumn_" + i, Width = 60 });
            }
            //this.gridViewItemListBox1.SimpleFillColumnTitle();
            //this.gridViewItemListBox1.ShowColumn = false;
            //this.gridViewItemListBox1.ColumnViewItems.Add(new GISShare.Controls.WinForm.WFNew.View.ColumnViewItem() { Text = "Addd", Width = 60 });
            //this.gridViewItemListBox1.ColumnViewItems.Add(new GISShare.Controls.WinForm.WFNew.View.ColumnViewItem() { Text = "Bddd", Width = 60 });
            //this.gridViewItemListBox1.ColumnViewItems.Add(new GISShare.Controls.WinForm.WFNew.View.ColumnViewItem() { Text = "Cddd", Width = 60 });
            ////
            //GISShare.Controls.WinForm.WFNew.View.NodeViewItem nodeViewItem1 = new GISShare.Controls.WinForm.WFNew.View.NodeViewItem("AAA");
            //GISShare.Controls.WinForm.WFNew.View.NodeViewItem nodeViewItem2_1 = new GISShare.Controls.WinForm.WFNew.View.NodeViewItem("BBB");
            //nodeViewItem1.NodeViewItems.Add(nodeViewItem2_1);
            //GISShare.Controls.WinForm.WFNew.View.NodeViewItem nodeViewItem2_2 = new GISShare.Controls.WinForm.WFNew.View.NodeViewItem("C");
            //nodeViewItem1.NodeViewItems.Add(nodeViewItem2_2);
            //GISShare.Controls.WinForm.WFNew.View.NodeViewItem nodeViewItem3_1 = new GISShare.Controls.WinForm.WFNew.View.NodeViewItem("D");
            //nodeViewItem2_1.NodeViewItems.Add(nodeViewItem3_1);
            //GISShare.Controls.WinForm.WFNew.View.NodeViewItem nodeViewItem3_2 = new GISShare.Controls.WinForm.WFNew.View.NodeViewItem("F");
            //nodeViewItem2_1.NodeViewItems.Add(nodeViewItem3_2);
            //this.gridViewItemListBox1.FillColumnTitle(new GISShare.Controls.WinForm.WFNew.View.NodeViewItem[] { nodeViewItem1 });
            //return;
            ////
            //GISShare.Controls.WinForm.WFNew.View.VRowColumnTitleViewItem vRowColumnTitleViewItem1 = new Controls.WinForm.WFNew.View.VRowColumnTitleViewItem();
            ////GISShare.Controls.WinForm.WFNew.View.LockRowColumnTitleViewItem lockRowColumnTitleViewItem = new GISShare.Controls.WinForm.WFNew.View.LockRowColumnTitleViewItem();
            ////this.viewItemListBox1.ViewItems.Add(lockRowColumnTitleViewItem);
            ////lockRowColumnTitleViewItem.ViewItems.Add(vRowColumnTitleViewItem1);
            //this.gridViewItemListBox1.TitleViewItems.Add(vRowColumnTitleViewItem1);
            ////
            //vRowColumnTitleViewItem1.ViewItems.Add(new GISShare.Controls.WinForm.WFNew.View.TitleViewItem("AAA"));
            //GISShare.Controls.WinForm.WFNew.View.RowColumnTitleViewItem rowColumnTitleViewItem2 = new Controls.WinForm.WFNew.View.RowColumnTitleViewItem();
            //vRowColumnTitleViewItem1.ViewItems.Add(rowColumnTitleViewItem2);
            ////
            //GISShare.Controls.WinForm.WFNew.View.VRowColumnTitleViewItem vRowColumnTitleViewItem3 = new Controls.WinForm.WFNew.View.VRowColumnTitleViewItem();
            //rowColumnTitleViewItem2.ViewItems.Add(vRowColumnTitleViewItem3);
            //rowColumnTitleViewItem2.ViewItems.Add(new GISShare.Controls.WinForm.WFNew.View.ColumnTitleViewItem(this.gridViewItemListBox1.ColumnViewItems[2]) { Text = "CCC" });
            ////
            //GISShare.Controls.WinForm.WFNew.View.RowColumnTitleViewItem rowColumnTitleViewItem4 = new Controls.WinForm.WFNew.View.RowColumnTitleViewItem();
            //vRowColumnTitleViewItem3.ViewItems.Add(new GISShare.Controls.WinForm.WFNew.View.TitleViewItem("BBB"));
            //vRowColumnTitleViewItem3.ViewItems.Add(rowColumnTitleViewItem4);
            ////
            //rowColumnTitleViewItem4.ViewItems.Add(new GISShare.Controls.WinForm.WFNew.View.ColumnTitleViewItem(this.gridViewItemListBox1.ColumnViewItems[0]) { Text = "DDD", BaseItemObject = new GISShare.Controls.WinForm.WFNew.CheckBoxItem() { eHAlignmentStyle = GISShare.Controls.WinForm.WFNew.HAlignmentStyle.eStretch, Text = "DDD" } });
            //rowColumnTitleViewItem4.ViewItems.Add(new GISShare.Controls.WinForm.WFNew.View.ColumnTitleViewItem(this.gridViewItemListBox1.ColumnViewItems[1]) { Text = "FFF" });
        }

        private void nodeViewItemTree1_MouseMove(object sender, MouseEventArgs e)
        {
            //Console.WriteLine( ((GISShare.Controls.WinForm.WFNew.View.RowNodeViewItem)this.nodeViewItemTree1.NodeViewItems[0]).ViewItems[0].eBaseItemState.ToString());
            //           兰  亭
            //秋落兰亭着墨色，流觞曲水自回折。
            //1 4 2 1 2 4 4  2 1 1 3 4 1 2
            //游池竞娱闲闲意，谁是当年换白鹅。
            //2 2 4 1 2 2 4  1 4 1 1 4 1 1
            //
            //        五泄湖
            //碧水天接处，浮船远去无。
            //4 3 1 2 4  2 2 3 4 1
            //翠峰折云扇，飞鸟出平湖。
            //4 2 2 2 4  2 3 2 2 2
            //
            //           故  居
            //灰檐浊瓦老病墙，中间多少泪沧桑。
            //1 2 2 3 3 4 2  1 4 2 3 4 2 1
            //何辞牵难先投笔，沃血倾尽劲草强。
            //2 2 2 4 1 2 3  4 3 1 4 4 3 2
        }

        private void viewItemListBox1_ViewItemEdited(object sender, Controls.WinForm.WFNew.View.ViewItemEventArgs e)
        {
            //Console.WriteLine(e.pViewItem.Text);
        }

        private void nodeViewItemTree1_ViewItemEdited(object sender, Controls.WinForm.WFNew.View.ViewItemEventArgs e)
        {
            //Console.WriteLine(e.pViewItem.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //this.gridViewItemListBox1.CanEdit = false;
            for (int i = 0; i < 1000; i++)
            {
                if (i % 2 == 1) this.gridViewItemListBox1.AddRowViewItem(GISShare.Controls.WinForm.WFNew.View.RowCellViewStyle.eSystemRow, i.ToString());
                else this.gridViewItemListBox1.AddRowViewItem(GISShare.Controls.WinForm.WFNew.View.RowCellViewStyle.eSingleCellRow, i.ToString());
            }
            this.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.View.ICellViewItem pCellViewItem = this.gridViewItemListBox1.GetCellViewItem(1, 1);
            if (pCellViewItem == null) return;
            //
            GISShare.Controls.WinForm.WFNew.View.ViewItemListBoxItem baseItem = new Controls.WinForm.WFNew.View.ViewItemListBoxItem();
            for (int i = 0; i < 10; i++)
            {
                baseItem.ViewItems.Add(new GISShare.Controls.WinForm.WFNew.View.TextEditViewItem("计数：" + i));
            }
            baseItem.ViewItems.Add(new GISShare.Controls.WinForm.WFNew.View.SuperViewItem(
                new GISShare.Controls.WinForm.WFNew.BaseButtonItem("F") { eHAlignmentStyle = GISShare.Controls.WinForm.WFNew.HAlignmentStyle.eStretch }));
            baseItem.CanEdit = true;
            baseItem.ShowOutLine = false;
            baseItem.eHAlignmentStyle = GISShare.Controls.WinForm.WFNew.HAlignmentStyle.eStretch;
            baseItem.eVAlignmentStyle = GISShare.Controls.WinForm.WFNew.VAlignmentStyle.eStretch;
            pCellViewItem.BaseItemObject = baseItem;
            //
            this.gridViewItemListBox1.SetColumnViewItemWidth(1, 100);
            this.gridViewItemListBox1.SetRowViewItemHeight(1, 100);
            //
            //-------------------------------------------------------------------------------------------------------------------------------
            //
            GISShare.Controls.WinForm.WFNew.View.ICellViewItem pCellViewItem2 = this.gridViewItemListBox1.GetCellViewItem(3, 3);
            if (pCellViewItem2 == null) return;
            //
            GISShare.Controls.WinForm.WFNew.View.NodeViewItemTreeItem baseItem2 = new Controls.WinForm.WFNew.View.NodeViewItemTreeItem();
            for (int i = 0; i < 5; i++)
            {
                baseItem2.NodeViewItems.Add(new GISShare.Controls.WinForm.WFNew.View.NodeViewItem("计数：" + i));
                for (int j = 0; j < 5; j++)
                {
                    baseItem2.NodeViewItems[i].NodeViewItems.Add(new GISShare.Controls.WinForm.WFNew.View.NodeViewItem("计数：" + j));
                }
            }
            baseItem2.ShowOutLine = false;
            baseItem2.eHAlignmentStyle = GISShare.Controls.WinForm.WFNew.HAlignmentStyle.eStretch;
            baseItem2.eVAlignmentStyle = GISShare.Controls.WinForm.WFNew.VAlignmentStyle.eStretch;
            pCellViewItem2.BaseItemObject = baseItem2;
            //
            this.gridViewItemListBox1.SetColumnViewItemWidth(3, 100);
            this.gridViewItemListBox1.SetRowViewItemHeight(3, 100);
            //
            //-------------------------------------------------------------------------------------------------------------------------------
            //
            GISShare.Controls.WinForm.WFNew.View.ICellViewItem pCellViewItem3 = this.gridViewItemListBox1.GetCellViewItem(5, 5);
            if (pCellViewItem3 == null) return;
            GISShare.Controls.WinForm.WFNew.View.GridViewItemListBoxItem baseItem3 = new Controls.WinForm.WFNew.View.GridViewItemListBoxItem();
            for (int i = 0; i < 10; i++)
            {
                baseItem3.ColumnViewItems.Add(new GISShare.Controls.WinForm.WFNew.View.ColumnViewItem() { Text = "Volumn_" + i, Width = 60 });
            }
            for (int i = 0; i < 100; i++)
            {
                baseItem3.AddRowViewItem(GISShare.Controls.WinForm.WFNew.View.RowCellViewStyle.eSystemRow, i.ToString());
            }
            baseItem3.ShowOutLine = false;
            baseItem3.eHAlignmentStyle = GISShare.Controls.WinForm.WFNew.HAlignmentStyle.eStretch;
            baseItem3.eVAlignmentStyle = GISShare.Controls.WinForm.WFNew.VAlignmentStyle.eStretch;
            pCellViewItem3.BaseItemObject = baseItem3;
            //
            this.gridViewItemListBox1.SetColumnViewItemWidth(5, 100);
            this.gridViewItemListBox1.SetRowViewItemHeight(5, 100);
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {

        }

        private void textBoxN1_TextChanged(object sender, EventArgs e)
        {
            Console.WriteLine("textBoxN1_TextChanged");
        }

        private void textBoxN1_KeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine("textBoxN1_KeyDown");
        }

        private void buttonN1_Click(object sender, EventArgs e)
        {
           GISShare.Controls.WinForm.WFNew.View.ViewItem v = this.viewItemListBox1.TryGetFocusViewItem();
        }

        private void gridViewItemListBox1_MouseDown(object sender, MouseEventArgs e)
        {
            Console.WriteLine("gridViewItemListBox1_MouseDown");
        }

    }

}
