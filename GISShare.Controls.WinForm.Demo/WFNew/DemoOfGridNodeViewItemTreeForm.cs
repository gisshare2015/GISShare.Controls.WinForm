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
    public partial class DemoOfGridNodeViewItemTreeForm : GISShare.Controls.WinForm.WFNew.Forms.TBForm
    {
        public DemoOfGridNodeViewItemTreeForm()
        {
            InitializeComponent();
            //
            //
            //
            List<NodeRowData> rowDataList = new List<NodeRowData>();
            for (int i = 1; i <= 1000; i++)
            {
                rowDataList.Add(new NodeRowData() { ParentID = null, ID = i.ToString(), Name = "Name_" + i, Tag1 = "Tag_" + i, Tag2 = "Tag_" + i, Tag3 = "Tag_" + i, Tag4 = "Tag_" + i, Tag5 = "Tag_" + i, Tag6 = "Tag_" + i });
                rowDataList.Add(new NodeRowData() { ParentID = i.ToString(), ID = i + " - 1", Name = "Name_" + i, Tag1 = "Tag_" + i, Tag2 = "Tag_" + i, Tag3 = "Tag_" + i, Tag4 = "Tag_" + i, Tag5 = "Tag_" + i, Tag6 = "Tag_" + i });
                rowDataList.Add(new NodeRowData() { ParentID = i.ToString(), ID = i + " - 2", Name = "Name_" + i, Tag1 = "Tag_" + i, Tag2 = "Tag_" + i, Tag3 = "Tag_" + i, Tag4 = "Tag_" + i, Tag5 = "Tag_" + i, Tag6 = "Tag_" + i });
                rowDataList.Add(new NodeRowData() { ParentID = i.ToString(), ID = i + " - 3", Name = "Name_" + i, Tag1 = "Tag_" + i, Tag2 = "Tag_" + i, Tag3 = "Tag_" + i, Tag4 = "Tag_" + i, Tag5 = "Tag_" + i, Tag6 = "Tag_" + i });
            }
            this.gridNodeViewItemTreeItem1.SetDataSource(rowDataList, "ID", "ParentID");
            this.gridNodeViewItemTreeItem1.Refresh();
            //
            GridNodeViewItemTreeItemHelper.AddColumnViewItem(this.gridNodeViewItemTreeItem1, "选择", 39, (sender, e) =>
            {
                Controls.WinForm.WFNew.BaseItem baseItem = (Controls.WinForm.WFNew.BaseItem)sender;
                NodeRowData dataItem = (NodeRowData)baseItem.Tag;
                MessageBox.Show(dataItem.Name);
            }, "CheckBoxItem");
            GridNodeViewItemTreeItemHelper.AddColumnViewItem(this.gridNodeViewItemTreeItem1, "链接", 39, (sender, e) =>
            {
                Controls.WinForm.WFNew.BaseItem baseItem = (Controls.WinForm.WFNew.BaseItem)sender;
                NodeRowData dataItem = (NodeRowData)baseItem.Tag;
                MessageBox.Show(dataItem.Name);
            }, "LinkLabelItem");
            GridNodeViewItemTreeItemHelper.AddColumnViewItem(this.gridNodeViewItemTreeItem1, "编辑", 39, (sender, e) =>
            {
                Controls.WinForm.WFNew.BaseItem baseItem = (Controls.WinForm.WFNew.BaseItem)sender;
                NodeRowData dataItem = (NodeRowData)baseItem.Tag;
                MessageBox.Show(dataItem.Name);
            }, "BaseButtonItem");
        }

        class NodeRowData
        {
            public string ID { get; set; }
            public string ParentID { get; set; }
            public string Name { get; set; }
            public string Tag1 { get; set; }
            public string Tag2 { get; set; }
            public string Tag3 { get; set; }
            public string Tag4 { get; set; }
            public string Tag5 { get; set; }
            public string Tag6 { get; set; }
        }

        public class GridNodeViewItemTreeItemHelper
        {
            public static GISShare.Controls.WinForm.WFNew.View.ColumnViewItem NewColumnViewItem(Controls.WinForm.WFNew.View.IColumnViewObject pColumnViewObject, string strName, int iWidth)
            {
                GISShare.Controls.WinForm.WFNew.View.ColumnViewItem columnViewItem = new Controls.WinForm.WFNew.View.ColumnViewItem();
                columnViewItem.Width = iWidth > 0 ? iWidth : pColumnViewObject.DefaultColumnWidth;
                columnViewItem.Name = strName;
                columnViewItem.Text = strName;
                return columnViewItem;
            }
            public static void AddColumnViewItem(Controls.WinForm.WFNew.View.GridNodeViewItemTreeItem gridNodeViewItemTreeItem, string strName, int iWidth, EventHandler eventHandler, string strType = "LinkLabelItem")
            {
                int iIndex = -1;
                for (int i = gridNodeViewItemTreeItem.ColumnViewItems.Count - 1; i >= 0; i--)
                {
                    if (gridNodeViewItemTreeItem.ColumnViewItems[i].Name == strName)
                    {
                        iIndex = i;
                        break;
                    }
                }
                if (iIndex < 0)
                {
                    iIndex = gridNodeViewItemTreeItem.ColumnViewItems.Add(NewColumnViewItem(gridNodeViewItemTreeItem, strName, iWidth));
                }
                Controls.WinForm.WFNew.View.INodeViewList pNodeViewList = (Controls.WinForm.WFNew.View.INodeViewList)gridNodeViewItemTreeItem;
                AddColumnViewItem_DG((Controls.WinForm.WFNew.View.INodeViewList)gridNodeViewItemTreeItem, iIndex, strName, iWidth, eventHandler, strType);
            }
            private static void AddColumnViewItem_DG(Controls.WinForm.WFNew.View.INodeViewList pNodeViewList, int iIndex, string strName, int iWidth, EventHandler eventHandler, string strType)
            {
                Controls.WinForm.WFNew.View.IDataRowCellViewItem pRowNodeCellViewItem;
                for (int i = 0, iLen = pNodeViewList.NodeViewItems.Count; i < iLen; i++)
                {
                    pRowNodeCellViewItem = (Controls.WinForm.WFNew.View.IDataRowCellViewItem)pNodeViewList.NodeViewItems[i];
                    Controls.WinForm.WFNew.View.ICellViewItem pCellViewItem = (Controls.WinForm.WFNew.View.ICellViewItem)pRowNodeCellViewItem.Items[iIndex];
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
                                Tag = pRowNodeCellViewItem.DataItem
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
                                Tag = pRowNodeCellViewItem.DataItem
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
                                Tag = pRowNodeCellViewItem.DataItem
                            };
                            baseItem.Click += eventHandler;
                            break;
                    }
                    pCellViewItem.BaseItemObject = baseItem;
                    //
                    AddColumnViewItem_DG((Controls.WinForm.WFNew.View.INodeViewList)pRowNodeCellViewItem, iIndex, strName, iWidth, eventHandler, strType);
                }
            }
        }
    }
}
