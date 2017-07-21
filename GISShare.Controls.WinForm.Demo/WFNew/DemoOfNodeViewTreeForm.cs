using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.Demo.WFNew
{
    public partial class DemoOfNodeViewItemTreeForm : GISShare.Controls.WinForm.WFNew.Forms.TBForm
    {
        public DemoOfNodeViewItemTreeForm()
        {
            InitializeComponent();
            //
            //
            //
            #region 添加超级节点视图项
            GISShare.Controls.WinForm.WFNew.LabelSeparatorItem labelSeparatorItem = new GISShare.Controls.WinForm.WFNew.LabelSeparatorItem();
            labelSeparatorItem.Text = "超级节点视图项（SuperNodeViewItem）：可以承载一个BaseItem的基类对象，如：LabelSeparatorItem";
            labelSeparatorItem.eVAlignmentStyle = GISShare.Controls.WinForm.WFNew.VAlignmentStyle.eStretch;
            labelSeparatorItem.eHAlignmentStyle = GISShare.Controls.WinForm.WFNew.HAlignmentStyle.eStretch;
            labelSeparatorItem.TextAlign = ContentAlignment.MiddleLeft;
            labelSeparatorItem.Margin = new System.Windows.Forms.Padding(0, 1, 1, 1);
            GISShare.Controls.WinForm.WFNew.View.SuperNodeViewItem superNodeViewItem1 = new GISShare.Controls.WinForm.WFNew.View.SuperNodeViewItem();
            superNodeViewItem1.Height = 26;
            //superNodeViewItem1.ShowBaseItemState = false;
            superNodeViewItem1.eNodeViewStyle = GISShare.Controls.WinForm.WFNew.View.NodeViewStyle.eTitleNodeView;
            superNodeViewItem1.BaseItemObject = labelSeparatorItem;
            this.nodeViewItemTree1.NodeViewItems.Add(superNodeViewItem1);
            //
            #region 子节点
            GISShare.Controls.WinForm.WFNew.CheckBoxItem checkBoxItem = new GISShare.Controls.WinForm.WFNew.CheckBoxItem();
            checkBoxItem.Text = "超级节点视图项（SuperNodeViewItem）：可以承载一个BaseItem的基类对象，如：CheckBoxItem";
            checkBoxItem.eVAlignmentStyle = GISShare.Controls.WinForm.WFNew.VAlignmentStyle.eStretch;
            checkBoxItem.eHAlignmentStyle = GISShare.Controls.WinForm.WFNew.HAlignmentStyle.eStretch;
            GISShare.Controls.WinForm.WFNew.View.SuperNodeViewItem superNodeViewItem2 = new GISShare.Controls.WinForm.WFNew.View.SuperNodeViewItem();
            superNodeViewItem2.BaseItemObject = checkBoxItem;
            superNodeViewItem1.NodeViewItems.Add(superNodeViewItem2);
            //
            GISShare.Controls.WinForm.WFNew.RadioButtonItem radioButtonItem = new GISShare.Controls.WinForm.WFNew.RadioButtonItem();
            radioButtonItem.Text = "超级节点视图项（SuperNodeViewItem）：可以承载一个BaseItem的基类对象，如：RadioButtonItem";
            radioButtonItem.eVAlignmentStyle = GISShare.Controls.WinForm.WFNew.VAlignmentStyle.eStretch;
            radioButtonItem.eHAlignmentStyle = GISShare.Controls.WinForm.WFNew.HAlignmentStyle.eStretch;
            GISShare.Controls.WinForm.WFNew.View.SuperNodeViewItem superNodeViewItem3 = new GISShare.Controls.WinForm.WFNew.View.SuperNodeViewItem();
            superNodeViewItem3.BaseItemObject = radioButtonItem;
            superNodeViewItem1.NodeViewItems.Add(superNodeViewItem3);
            //
            GISShare.Controls.WinForm.WFNew.ImageRadioButtonItem imageRadioButtonItem = new GISShare.Controls.WinForm.WFNew.ImageRadioButtonItem();
            imageRadioButtonItem.Text = "超级节点视图项（SuperNodeViewItem）：可以承载一个BaseItem的基类对象，如：ImageRadioButtonItem";
            imageRadioButtonItem.eVAlignmentStyle = GISShare.Controls.WinForm.WFNew.VAlignmentStyle.eStretch;
            imageRadioButtonItem.eHAlignmentStyle = GISShare.Controls.WinForm.WFNew.HAlignmentStyle.eStretch;
            imageRadioButtonItem.TextAlign = ContentAlignment.MiddleLeft;
            imageRadioButtonItem.Image = new System.Drawing.Bitmap(this.GetType().Assembly.GetManifestResourceStream("GISShare.Controls.WinForm.Demo.Image.Image.ico"));
            imageRadioButtonItem.ImageAlign = ContentAlignment.MiddleLeft;
            imageRadioButtonItem.CDSpace = 4;
            GISShare.Controls.WinForm.WFNew.View.SuperNodeViewItem superNodeViewItem4 = new GISShare.Controls.WinForm.WFNew.View.SuperNodeViewItem();
            superNodeViewItem4.BaseItemObject = imageRadioButtonItem;
            superNodeViewItem1.NodeViewItems.Add(superNodeViewItem4);
            //
            GISShare.Controls.WinForm.WFNew.ButtonTextBoxItem buttonTextBoxItem = new GISShare.Controls.WinForm.WFNew.ButtonTextBoxItem();
            buttonTextBoxItem.eBorderStyle = GISShare.Controls.WinForm.WFNew.BorderStyle.eSingle;
            buttonTextBoxItem.Text = "超级节点视图项（SuperNodeViewItem）：可以承载一个BaseItem的基类对象，如：ButtonTextBoxItem";
            buttonTextBoxItem.Margin = new System.Windows.Forms.Padding(0);
            buttonTextBoxItem.eVAlignmentStyle = GISShare.Controls.WinForm.WFNew.VAlignmentStyle.eStretch;
            buttonTextBoxItem.eHAlignmentStyle = GISShare.Controls.WinForm.WFNew.HAlignmentStyle.eStretch;
            GISShare.Controls.WinForm.WFNew.View.SuperNodeViewItem superNodeViewItem5 = new GISShare.Controls.WinForm.WFNew.View.SuperNodeViewItem();
            superNodeViewItem5.BaseItemObject = buttonTextBoxItem;
            superNodeViewItem5.Height = buttonTextBoxItem.Height;
            superNodeViewItem1.NodeViewItems.Add(superNodeViewItem5);
            #endregion
            //
            #region 添加行项
            GISShare.Controls.WinForm.WFNew.View.RowNodeViewItem rowNodeViewItem = new Controls.WinForm.WFNew.View.RowNodeViewItem() { Height = 23 };
            superNodeViewItem1.NodeViewItems.Add(rowNodeViewItem);
            for (int i = 0; i < 6; i++)
            {
                GISShare.Controls.WinForm.WFNew.RadioButtonItem radioButtonItem3 = new Controls.WinForm.WFNew.RadioButtonItem();
                radioButtonItem3.eHAlignmentStyle = GISShare.Controls.WinForm.WFNew.HAlignmentStyle.eStretch;
                radioButtonItem3.eVAlignmentStyle = GISShare.Controls.WinForm.WFNew.VAlignmentStyle.eStretch;
                radioButtonItem3.Text = "第 " + (i + 1) + " 个";
                rowNodeViewItem.ViewItems.Add(new GISShare.Controls.WinForm.WFNew.View.SuperViewItem(radioButtonItem3) { Width = 120 });//宽度必须设置
            }
            //
            GISShare.Controls.WinForm.WFNew.View.RowNodeViewItem rowNodeViewItem2 = new Controls.WinForm.WFNew.View.RowNodeViewItem() { Height = 23 };
            superNodeViewItem1.NodeViewItems.Add(rowNodeViewItem2);
            for (int i = 0; i < 6; i++)
            {
                GISShare.Controls.WinForm.WFNew.TextBoxItem textBoxItem = new Controls.WinForm.WFNew.TextBoxItem();
                textBoxItem.eHAlignmentStyle = GISShare.Controls.WinForm.WFNew.HAlignmentStyle.eStretch;
                textBoxItem.eVAlignmentStyle = GISShare.Controls.WinForm.WFNew.VAlignmentStyle.eCenter;
                textBoxItem.Text = "第 " + (i + 1) + " 个";
                rowNodeViewItem2.ViewItems.Add(new GISShare.Controls.WinForm.WFNew.View.SuperViewItem(textBoxItem) { Width = 120 });//宽度必须设置
            }
            //
            GISShare.Controls.WinForm.WFNew.View.RowNodeViewItem rowNodeViewItem3 = new Controls.WinForm.WFNew.View.RowNodeViewItem() { Height = 23 };
            superNodeViewItem1.NodeViewItems.Add(rowNodeViewItem3);
            for (int i = 0; i < 6; i++)
            {
                GISShare.Controls.WinForm.WFNew.CheckBoxItem checkBoxItem2 = new Controls.WinForm.WFNew.CheckBoxItem();
                checkBoxItem2.eHAlignmentStyle = GISShare.Controls.WinForm.WFNew.HAlignmentStyle.eStretch;
                checkBoxItem2.eVAlignmentStyle = GISShare.Controls.WinForm.WFNew.VAlignmentStyle.eStretch;
                checkBoxItem2.Text = "第 " + (i + 1) + " 个";
                rowNodeViewItem3.ViewItems.Add(new GISShare.Controls.WinForm.WFNew.View.SuperViewItem(checkBoxItem2) { Width = 120 });//宽度必须设置
            }
            //
            GISShare.Controls.WinForm.WFNew.View.RowNodeViewItem rowNodeViewItem4 = new Controls.WinForm.WFNew.View.RowNodeViewItem() { Height = 22 };
            superNodeViewItem1.NodeViewItems.Add(rowNodeViewItem4);
            for (int i = 0; i < 6; i++)
            {
                rowNodeViewItem4.ViewItems.Add(new GISShare.Controls.WinForm.WFNew.View.TextEditViewItem("可编辑：第 " + (i + 1) + " 个") { Width = 120 });
            }
            //
            GISShare.Controls.WinForm.WFNew.View.RowNodeViewItem rowNodeViewItem5 = new Controls.WinForm.WFNew.View.RowNodeViewItem() { Height = 22 };
            superNodeViewItem1.NodeViewItems.Add(rowNodeViewItem5);
            for (int i = 0; i < 6; i++)
            {
                rowNodeViewItem5.ViewItems.Add(new GISShare.Controls.WinForm.WFNew.View.TextEditViewItem("不可编辑：第 " + (i + 1) + " 个") { Width = 120, CanEdit = false });
            }
            //
            GISShare.Controls.WinForm.WFNew.View.FlexibleRowNodeViewItem flexibleRowNodeViewItem = new Controls.WinForm.WFNew.View.FlexibleRowNodeViewItem() { Height = 22, CanExchangeItem = true };
            superNodeViewItem1.NodeViewItems.Add(flexibleRowNodeViewItem);
            for (int i = 0; i < 2; i++)
            {
                flexibleRowNodeViewItem.ViewItems.Add(new GISShare.Controls.WinForm.WFNew.View.TextEditViewItem("可调节行高、单元宽度以及交换单元位置：第 " + (i + 1) + " 个") { Width = 290, CanEdit = false });
            }
            #endregion
            #endregion
            //
            //
            //
            #region 嵌入NodeViewItemTreeItem
            GISShare.Controls.WinForm.WFNew.View.ViewItemListBoxItem viewItemListBoxItem = new GISShare.Controls.WinForm.WFNew.View.ViewItemListBoxItem();
            //viewItemListBoxItem.Size = new System.Drawing.Size(120,100);
            viewItemListBoxItem.eVAlignmentStyle = GISShare.Controls.WinForm.WFNew.VAlignmentStyle.eStretch;
            viewItemListBoxItem.eHAlignmentStyle = GISShare.Controls.WinForm.WFNew.HAlignmentStyle.eStretch;
            viewItemListBoxItem.Margin = new Padding(10, 10, 10, 0);
            for (int i = 0; i < 10; i++)
            {
                viewItemListBoxItem.ViewItems.Add(new GISShare.Controls.WinForm.WFNew.View.ViewItem("元素计数：" + i));
            }
            GISShare.Controls.WinForm.WFNew.View.ResizeSuperNodeViewItem superNodeViewItem6 = new GISShare.Controls.WinForm.WFNew.View.ResizeSuperNodeViewItem();
            superNodeViewItem6.ShowBaseItemState = false;
            //superNodeViewItem6.Width = 100;
            superNodeViewItem6.Height = 100;
            superNodeViewItem6.BaseItemObject = viewItemListBoxItem;
            this.nodeViewItemTree1.NodeViewItems.Add(superNodeViewItem6);
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
            GISShare.Controls.WinForm.WFNew.View.ResizeSuperNodeViewItem superNodeViewItem7 = new GISShare.Controls.WinForm.WFNew.View.ResizeSuperNodeViewItem();
            superNodeViewItem7.ShowBaseItemState = false;
            //superViewItem6.Width = 100;
            superNodeViewItem7.Height = 100;
            superNodeViewItem7.BaseItemObject = nodeViewItemTreeItem;
            this.nodeViewItemTree1.NodeViewItems.Add(superNodeViewItem7);
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
            GISShare.Controls.WinForm.WFNew.View.ResizeSuperNodeViewItem superNodeViewItem8 = new GISShare.Controls.WinForm.WFNew.View.ResizeSuperNodeViewItem();
            superNodeViewItem8.ShowBaseItemState = false;
            //superViewItem8.Width = 100;
            superNodeViewItem8.Height = 160;
            superNodeViewItem8.BaseItemObject = gridViewItemListBoxItem;
            this.nodeViewItemTree1.NodeViewItems.Add(superNodeViewItem8);
            #endregion
        }
    }
}