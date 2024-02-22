using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.Demo.WFNew
{
    public partial class DemoOfGridViewItemListBoxForm : GISShare.Controls.WinForm.WFNew.Forms.TBForm
    {
        public DemoOfGridViewItemListBoxForm()
        {
            InitializeComponent();
            //
            //
            //
            List<RowData> rowDataList = new List<RowData>();
            for (int i = 1; i <= 100000; i++)
            {
                rowDataList.Add(new RowData() { ID = i.ToString(), Name = "Name_" + i, Tag1 = "Tag_" + i, Tag2 = "Tag_" + i, Tag3 = "Tag_" + i, Tag4 = "Tag_" + i, Tag5 = "Tag_" + i, Tag6 = "Tag_" + i });
            }
            this.gridViewItemListBoxItem1.DataSource = rowDataList;
            //
            GridViewItemListBoxItemHelper.AddColumnViewItem(this.gridViewItemListBoxItem1, "选择", 39, (sender, e) => {
                Controls.WinForm.WFNew.BaseItem baseItem = (Controls.WinForm.WFNew.BaseItem)sender;
                RowData dataItem = (RowData)baseItem.Tag;
                MessageBox.Show(dataItem.Name);
            }, "CheckBoxItem");
            GridViewItemListBoxItemHelper.AddColumnViewItem(this.gridViewItemListBoxItem1, "链接", 39, (sender, e) =>
            {
                Controls.WinForm.WFNew.BaseItem baseItem = (Controls.WinForm.WFNew.BaseItem)sender;
                RowData dataItem = (RowData)baseItem.Tag;
                MessageBox.Show(dataItem.Name);
            }, "LinkLabelItem");
            GridViewItemListBoxItemHelper.AddColumnViewItem(this.gridViewItemListBoxItem1, "编辑", 39, (sender, e) =>
            {
                Controls.WinForm.WFNew.BaseItem baseItem = (Controls.WinForm.WFNew.BaseItem)sender;
                RowData dataItem = (RowData)baseItem.Tag;
                MessageBox.Show(dataItem.Name);
            }, "BaseButtonItem");
        }

        class RowData
        {
            public string ID { get; set; }
            public string Name { get; set; }
            public string Tag1 { get; set; }
            public string Tag2 { get; set; }
            public string Tag3 { get; set; }
            public string Tag4 { get; set; }
            public string Tag5 { get; set; }
            public string Tag6 { get; set; }
        }


        class GridViewItemListBoxItemHelper
        {
            public static void AddColumnViewItem(Controls.WinForm.WFNew.View.GridViewItemListBoxItem gridViewItemListBoxItem, string strName, int iWidth, EventHandler eventHandler, string strType = "LinkLabelItem")
            {
                int iIndex = -1;
                for (int i = gridViewItemListBoxItem.ColumnViewItems.Count - 1; i >= 0; i--)
                {
                    if (gridViewItemListBoxItem.ColumnViewItems[i].Name == strName)
                    {
                        iIndex = i;
                        break;
                    }
                }
                if (iIndex < 0)
                {
                    iIndex = gridViewItemListBoxItem.ColumnViewItems.Add(NewColumnViewItem(gridViewItemListBoxItem, strName, iWidth));
                }
                for (int i = 0, iLen = gridViewItemListBoxItem.RowCount; i < iLen; i++)
                {
                    Controls.WinForm.WFNew.View.ICellViewItem pCellViewItem = gridViewItemListBoxItem.GetCellViewItem(i, iIndex);
                    pCellViewItem.Text = strName;
                    Controls.WinForm.WFNew.BaseItem baseItem;
                    switch (strType)
                    {
                        case "CheckBoxItem":
                            baseItem = new Controls.WinForm.WFNew.CheckBoxItem()
                            {
                                Name = pCellViewItem.Text,
                                Text = "",
                                eHAlignmentStyle = GISShare.Controls.WinForm.WFNew.HAlignmentStyle.eStretch,
                                eVAlignmentStyle = GISShare.Controls.WinForm.WFNew.VAlignmentStyle.eStretch,
                                Tag = ((Controls.WinForm.WFNew.View.IDataRowCellViewItem)gridViewItemListBoxItem.GetRow(i)).DataItem
                            };
                            baseItem.CheckedChanged += eventHandler;
                            break;
                        case "BaseButtonItem":
                            baseItem = new Controls.WinForm.WFNew.BaseButtonItem()
                            {
                                Name = pCellViewItem.Text,
                                Text = pCellViewItem.Text,
                                eHAlignmentStyle = GISShare.Controls.WinForm.WFNew.HAlignmentStyle.eStretch,
                                eVAlignmentStyle = GISShare.Controls.WinForm.WFNew.VAlignmentStyle.eStretch,
                                ShowNomalState = true,
                                Tag = ((Controls.WinForm.WFNew.View.IDataRowCellViewItem)gridViewItemListBoxItem.GetRow(i)).DataItem
                            };
                            baseItem.Click += eventHandler;
                            break;
                        case "LinkLabelItem":
                        default:
                            baseItem = new Controls.WinForm.WFNew.LinkLabelItem()
                            {
                                Name = pCellViewItem.Text,
                                Text = pCellViewItem.Text,
                                eHAlignmentStyle = GISShare.Controls.WinForm.WFNew.HAlignmentStyle.eStretch,
                                eVAlignmentStyle = GISShare.Controls.WinForm.WFNew.VAlignmentStyle.eStretch,
                                Tag = ((Controls.WinForm.WFNew.View.IDataRowCellViewItem)gridViewItemListBoxItem.GetRow(i)).DataItem
                            };
                            baseItem.Click += eventHandler;
                            break;
                    }
                    pCellViewItem.BaseItemObject = baseItem;
                }
            }
            public static GISShare.Controls.WinForm.WFNew.View.ColumnViewItem NewColumnViewItem(Controls.WinForm.WFNew.View.IColumnViewObject pColumnViewObject, string strName, int iWidth)
            {
                GISShare.Controls.WinForm.WFNew.View.ColumnViewItem columnViewItem = new Controls.WinForm.WFNew.View.ColumnViewItem();
                columnViewItem.Width = iWidth > 0 ? iWidth : pColumnViewObject.DefaultColumnWidth;
                columnViewItem.Name = strName;
                columnViewItem.Text = strName;
                return columnViewItem;
            }
        }
    }
}
