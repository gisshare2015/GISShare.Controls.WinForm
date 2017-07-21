using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.Demo.WFNew
{
    public partial class DemoOfViewItemListBoxForm : GISShare.Controls.WinForm.WFNew.Forms.TBForm
    {
        public DemoOfViewItemListBoxForm()
        {
            InitializeComponent();
            //
            //
            //
            #region 添加超级视图项
            GISShare.Controls.WinForm.WFNew.LabelSeparatorItem labelSeparatorItem = new GISShare.Controls.WinForm.WFNew.LabelSeparatorItem();
            labelSeparatorItem.Text = "超级视图项（SuperViewItem）：可以承载一个BaseItem的基类对象，如：LabelSeparatorItem";
            labelSeparatorItem.eVAlignmentStyle = GISShare.Controls.WinForm.WFNew.VAlignmentStyle.eStretch;
            labelSeparatorItem.eHAlignmentStyle = GISShare.Controls.WinForm.WFNew.HAlignmentStyle.eStretch;
            labelSeparatorItem.TextAlign = ContentAlignment.MiddleLeft;
            GISShare.Controls.WinForm.WFNew.View.SuperViewItem superViewItem1 = new GISShare.Controls.WinForm.WFNew.View.SuperViewItem();
            superViewItem1.Height = 26;
            superViewItem1.BaseItemObject = labelSeparatorItem;
            this.viewItemListBox1.ViewItems.Add(superViewItem1);
            //
            GISShare.Controls.WinForm.WFNew.CheckBoxItem checkBoxItem = new GISShare.Controls.WinForm.WFNew.CheckBoxItem();
            checkBoxItem.Text = "超级视图项（SuperViewItem）：可以承载一个BaseItem的基类对象，如：CheckBoxItem";
            checkBoxItem.eVAlignmentStyle = GISShare.Controls.WinForm.WFNew.VAlignmentStyle.eStretch;
            checkBoxItem.eHAlignmentStyle = GISShare.Controls.WinForm.WFNew.HAlignmentStyle.eStretch;
            GISShare.Controls.WinForm.WFNew.View.SuperViewItem superViewItem2 = new GISShare.Controls.WinForm.WFNew.View.SuperViewItem();
            superViewItem2.BaseItemObject = checkBoxItem;
            this.viewItemListBox1.ViewItems.Add(superViewItem2);
            //
            GISShare.Controls.WinForm.WFNew.RadioButtonItem radioButtonItem = new GISShare.Controls.WinForm.WFNew.RadioButtonItem();
            radioButtonItem.Text = "超级视图项（SuperViewItem）：可以承载一个BaseItem的基类对象，如：RadioButtonItem";
            radioButtonItem.eVAlignmentStyle = GISShare.Controls.WinForm.WFNew.VAlignmentStyle.eStretch;
            radioButtonItem.eHAlignmentStyle = GISShare.Controls.WinForm.WFNew.HAlignmentStyle.eStretch;
            GISShare.Controls.WinForm.WFNew.View.SuperViewItem superViewItem3 = new GISShare.Controls.WinForm.WFNew.View.SuperViewItem();
            superViewItem3.BaseItemObject = radioButtonItem;
            this.viewItemListBox1.ViewItems.Add(superViewItem3);
            //
            GISShare.Controls.WinForm.WFNew.ImageRadioButtonItem imageRadioButtonItem = new GISShare.Controls.WinForm.WFNew.ImageRadioButtonItem();
            imageRadioButtonItem.Text = "超级视图项（SuperViewItem）：可以承载一个BaseItem的基类对象，如：ImageRadioButtonItem";
            imageRadioButtonItem.eVAlignmentStyle = GISShare.Controls.WinForm.WFNew.VAlignmentStyle.eStretch;
            imageRadioButtonItem.eHAlignmentStyle = GISShare.Controls.WinForm.WFNew.HAlignmentStyle.eStretch;
            imageRadioButtonItem.TextAlign = ContentAlignment.MiddleLeft;
            imageRadioButtonItem.Image = new System.Drawing.Bitmap(this.GetType().Assembly.GetManifestResourceStream("GISShare.Controls.WinForm.Demo.Image.Image.ico"));
            imageRadioButtonItem.ImageAlign = ContentAlignment.MiddleLeft;
            imageRadioButtonItem.CDSpace = 4;
            GISShare.Controls.WinForm.WFNew.View.SuperViewItem superViewItem4 = new GISShare.Controls.WinForm.WFNew.View.SuperViewItem();
            superViewItem4.BaseItemObject = imageRadioButtonItem;
            this.viewItemListBox1.ViewItems.Add(superViewItem4);
            //
            GISShare.Controls.WinForm.WFNew.ButtonTextBoxItem buttonTextBoxItem = new GISShare.Controls.WinForm.WFNew.ButtonTextBoxItem();
            buttonTextBoxItem.eBorderStyle = GISShare.Controls.WinForm.WFNew.BorderStyle.eSingle;
            buttonTextBoxItem.Text = "超级视图项（SuperViewItem）：可以承载一个BaseItem的基类对象，如：ButtonTextBoxItem";
            buttonTextBoxItem.Margin = new System.Windows.Forms.Padding(0);
            buttonTextBoxItem.eVAlignmentStyle = GISShare.Controls.WinForm.WFNew.VAlignmentStyle.eStretch;
            buttonTextBoxItem.eHAlignmentStyle = GISShare.Controls.WinForm.WFNew.HAlignmentStyle.eStretch;
            GISShare.Controls.WinForm.WFNew.View.SuperViewItem superViewItem5 = new GISShare.Controls.WinForm.WFNew.View.SuperViewItem();
            superViewItem5.BaseItemObject = buttonTextBoxItem;
            superViewItem5.Height = buttonTextBoxItem.Height;
            this.viewItemListBox1.ViewItems.Add(superViewItem5);
            #endregion
            //
            //
            //
            #region 添加行项
            GISShare.Controls.WinForm.WFNew.View.RowViewItem rowViewItem = new Controls.WinForm.WFNew.View.RowViewItem() { Height = 23 };
            this.viewItemListBox1.ViewItems.Add(rowViewItem);
            for (int i = 0; i < 6; i++)
            {
                GISShare.Controls.WinForm.WFNew.RadioButtonItem radioButtonItem3 = new Controls.WinForm.WFNew.RadioButtonItem();
                radioButtonItem3.eHAlignmentStyle = GISShare.Controls.WinForm.WFNew.HAlignmentStyle.eStretch;
                radioButtonItem3.eVAlignmentStyle = GISShare.Controls.WinForm.WFNew.VAlignmentStyle.eStretch;
                radioButtonItem3.Text = "第 " + (i + 1) + " 个";
                rowViewItem.ViewItems.Add(new GISShare.Controls.WinForm.WFNew.View.SuperViewItem(radioButtonItem3) { Width = 120 });//宽度必须设置
            }
            //
            GISShare.Controls.WinForm.WFNew.View.RowViewItem rowViewItem2 = new Controls.WinForm.WFNew.View.RowViewItem() { Height = 23 };
            this.viewItemListBox1.ViewItems.Add(rowViewItem2);
            for (int i = 0; i < 6; i++)
            {
                GISShare.Controls.WinForm.WFNew.TextBoxItem textBoxItem = new Controls.WinForm.WFNew.TextBoxItem();
                textBoxItem.eHAlignmentStyle = GISShare.Controls.WinForm.WFNew.HAlignmentStyle.eStretch;
                textBoxItem.eVAlignmentStyle = GISShare.Controls.WinForm.WFNew.VAlignmentStyle.eCenter;
                textBoxItem.Text = "第 " + (i + 1) + " 个";
                rowViewItem2.ViewItems.Add(new GISShare.Controls.WinForm.WFNew.View.SuperViewItem(textBoxItem) { Width = 120 });//宽度必须设置
            }
            //
            GISShare.Controls.WinForm.WFNew.View.RowViewItem rowViewItem3 = new Controls.WinForm.WFNew.View.RowViewItem() { Height = 23 };
            this.viewItemListBox1.ViewItems.Add(rowViewItem3);
            for (int i = 0; i < 6; i++)
            {
                GISShare.Controls.WinForm.WFNew.CheckBoxItem checkBoxItem2 = new Controls.WinForm.WFNew.CheckBoxItem();
                checkBoxItem2.eHAlignmentStyle = GISShare.Controls.WinForm.WFNew.HAlignmentStyle.eStretch;
                checkBoxItem2.eVAlignmentStyle = GISShare.Controls.WinForm.WFNew.VAlignmentStyle.eStretch;
                checkBoxItem2.Text = "第 " + (i + 1) + " 个";
                rowViewItem3.ViewItems.Add(new GISShare.Controls.WinForm.WFNew.View.SuperViewItem(checkBoxItem2) { Width = 120 });//宽度必须设置
            }
            //
            GISShare.Controls.WinForm.WFNew.View.RowViewItem rowViewItem4 = new Controls.WinForm.WFNew.View.RowViewItem() { Height = 22 };
            this.viewItemListBox1.ViewItems.Add(rowViewItem4);
            for (int i = 0; i < 6; i++)
            {
                rowViewItem4.ViewItems.Add(new GISShare.Controls.WinForm.WFNew.View.TextEditViewItem("可编辑：第 " + (i + 1) + " 个") { Width = 120 });
            }
            //
            GISShare.Controls.WinForm.WFNew.View.RowViewItem rowViewItem5 = new Controls.WinForm.WFNew.View.RowViewItem() { Height = 22 };
            this.viewItemListBox1.ViewItems.Add(rowViewItem5);
            for (int i = 0; i < 6; i++)
            {
                rowViewItem5.ViewItems.Add(new GISShare.Controls.WinForm.WFNew.View.TextEditViewItem("不可编辑：第 " + (i + 1) + " 个") { Width = 120, CanEdit = false });
            }
            //
            GISShare.Controls.WinForm.WFNew.View.FlexibleRowViewItem flexibleRowViewItem = new Controls.WinForm.WFNew.View.FlexibleRowViewItem() { Height = 22, CanExchangeItem = true };
            this.viewItemListBox1.ViewItems.Add(flexibleRowViewItem);
            for (int i = 0; i < 2; i++)
            {
                flexibleRowViewItem.ViewItems.Add(new GISShare.Controls.WinForm.WFNew.View.TextEditViewItem("可调节行高、单元宽度以及交换单元位置：第 " + (i + 1) + " 个") { Width = 290, CanEdit = false });
            }
            #endregion
            //
            //
            //
            #region 嵌入ViewItemListBoxItem
            GISShare.Controls.WinForm.WFNew.View.ViewItemListBoxItem viewItemListBoxItem = new GISShare.Controls.WinForm.WFNew.View.ViewItemListBoxItem();
            //viewItemListBoxItem.Size = new System.Drawing.Size(120,100);
            viewItemListBoxItem.eVAlignmentStyle = GISShare.Controls.WinForm.WFNew.VAlignmentStyle.eStretch;
            viewItemListBoxItem.eHAlignmentStyle = GISShare.Controls.WinForm.WFNew.HAlignmentStyle.eStretch;
            viewItemListBoxItem.Margin = new Padding(10, 10, 10, 0);
            for (int i = 0; i < 5; i++)
            {
                viewItemListBoxItem.ViewItems.Add(new GISShare.Controls.WinForm.WFNew.View.ViewItem("元素计数：" + i));
            }
            GISShare.Controls.WinForm.WFNew.View.ResizeSuperViewItem superViewItem6 = new GISShare.Controls.WinForm.WFNew.View.ResizeSuperViewItem();
            //superViewItem6.Width = 100;
            superViewItem6.Height = 100;
            superViewItem6.BaseItemObject = viewItemListBoxItem;
            this.viewItemListBox1.ViewItems.Add(superViewItem6);
            #endregion
            //
            //
            //
            #region 嵌入NodeViewItemTreeItem
            GISShare.Controls.WinForm.WFNew.View.NodeViewItemTreeItem nodeViewItemTreeItem = new GISShare.Controls.WinForm.WFNew.View.NodeViewItemTreeItem();
            //nodeViewItemTreeItem.Size = new System.Drawing.Size(120,100);
            nodeViewItemTreeItem.eVAlignmentStyle = GISShare.Controls.WinForm.WFNew.VAlignmentStyle.eStretch;
            nodeViewItemTreeItem.eHAlignmentStyle = GISShare.Controls.WinForm.WFNew.HAlignmentStyle.eStretch;
            nodeViewItemTreeItem.Margin = new Padding(10, 10, 10, 0);
            for (int i = 0; i < 5; i++)
            {
                nodeViewItemTreeItem.NodeViewItems.Add(new GISShare.Controls.WinForm.WFNew.View.NodeViewItem("节点计数：" + i));
                for (int j = 0; j < 5; j++)
                {
                    nodeViewItemTreeItem.NodeViewItems[i].NodeViewItems.Add(new GISShare.Controls.WinForm.WFNew.View.NodeViewItem("节点计数：" + j));
                }
            }
            GISShare.Controls.WinForm.WFNew.View.ResizeSuperViewItem superViewItem7 = new GISShare.Controls.WinForm.WFNew.View.ResizeSuperViewItem();
            //superViewItem6.Width = 100;
            superViewItem7.Height = 100;
            superViewItem7.BaseItemObject = nodeViewItemTreeItem;
            this.viewItemListBox1.ViewItems.Add(superViewItem7);
            #endregion
            //
            //
            //
            #region 嵌入ViewItemListBoxItem
            GISShare.Controls.WinForm.WFNew.View.GridViewItemListBoxItem gridViewItemListBoxItem = new GISShare.Controls.WinForm.WFNew.View.GridViewItemListBoxItem();
            //gridViewItemListBoxItem.Size = new System.Drawing.Size(120,100);
            gridViewItemListBoxItem.eVAlignmentStyle = GISShare.Controls.WinForm.WFNew.VAlignmentStyle.eStretch;
            gridViewItemListBoxItem.eHAlignmentStyle = GISShare.Controls.WinForm.WFNew.HAlignmentStyle.eStretch;
            gridViewItemListBoxItem.Margin = new Padding(10, 10, 10, 0);
            for (int i = 0; i < 10; i++)
            {
                gridViewItemListBoxItem.ColumnViewItems.Add(new GISShare.Controls.WinForm.WFNew.View.ColumnViewItem() { Text = "Volumn_" + i, Width = 60 });
            }
            for (int i = 0; i < 100; i++)
            {
                gridViewItemListBoxItem.AddRowViewItem(GISShare.Controls.WinForm.WFNew.View.RowCellViewStyle.eSystemRow, i.ToString());
            }
            GISShare.Controls.WinForm.WFNew.View.ResizeSuperViewItem superViewItem8 = new GISShare.Controls.WinForm.WFNew.View.ResizeSuperViewItem();
            //superViewItem8.Width = 100;
            superViewItem8.Height = 160;
            superViewItem8.BaseItemObject = gridViewItemListBoxItem;
            this.viewItemListBox1.ViewItems.Add(superViewItem8);
            #endregion
        }
    }
}