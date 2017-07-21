using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.Plugin.WinForm
{
    public partial class PluginCategoryDictionaryTBForm : GISShare.Controls.WinForm.WFNew.Forms.TBForm // Form
    {
        private PluginCategoryDictionary m_PluginCategoryDictionary;

        public PluginCategoryDictionaryTBForm(PluginCategoryDictionary pluginCategoryDictionary)
        {
            InitializeComponent();
            //
            //
            //
            this.m_PluginCategoryDictionary = pluginCategoryDictionary;
            //
            GISShare.Controls.WinForm.WFNew.View.NodeViewItem nodeViewItem = new Controls.WinForm.WFNew.View.NodeViewItem("CategoryIndex", "目录索引（CategoryIndex）");
            nodeViewItem.IsExpanded = true;
            foreach (PluginCategory one in this.m_PluginCategoryDictionary)
            {
                nodeViewItem.NodeViewItems.Add(new Controls.WinForm.WFNew.View.NodeViewItem(one.CategoryIndex.ToString(), "目录索引（CategoryIndex）：" + one.CategoryIndex));
            }
            this.ctCategory.NodeViewItems.Add(nodeViewItem);
            this.ctCategory.SelectedNode = nodeViewItem;
        }

        private void ctCategory_SelectedNodeChanged(object sender, Controls.WinForm.PropertyChangedEventArgs e)
        {
            this.btnShow_MouseClick(null, null);
        }

        private void btnShow_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.ctCategory.SelectedNode == null) return;
            this.nodeViewItemTree1.NodeViewItems.Clear();
            int iNum = 0;
            if (this.ctCategory.SelectedNode.Name == "CategoryIndex")
            {
                foreach (PluginCategory one in this.m_PluginCategoryDictionary)
                {
                    GISShare.Controls.WinForm.WFNew.View.NodeViewItem nodeViewItem = new Controls.WinForm.WFNew.View.NodeViewItem
                        (
                        one.CategoryIndex.ToString(),
                        "目录索引（CategoryIndex）：" + one.CategoryIndex + "；成员数（Count）：" + one.PluginCollection.Count.ToString()
                        );
                    foreach (IPlugin one2 in one.PluginCollection)
                    {
                        IPluginInfo pPluginInfo = one2 as IPluginInfo;
                        if (pPluginInfo == null)
                        {
                            nodeViewItem.NodeViewItems.Add
                                (
                                new Controls.WinForm.WFNew.View.NodeViewItem(one2.Name, "目录索引（CategoryIndex）：" + one2.CategoryIndex + "；名称（Name）：" + one2.Name)
                                );
                        }
                        else
                        {
                            nodeViewItem.NodeViewItems.Add
                                (
                                new Controls.WinForm.WFNew.View.NodeViewItem(one2.Name, "目录索引（CategoryIndex）：" + one2.CategoryIndex + "；描述（Describe）：" + pPluginInfo.GetDescribe() + "；名称（Name）：" + one2.Name)
                                );
                        }
                        iNum++;
                    }
                    this.nodeViewItemTree1.NodeViewItems.Add(nodeViewItem);
                }
            }
            else
            {
                int iIndex;
                if (int.TryParse(this.ctCategory.SelectedNode.Name, out iIndex))
                {
                    PluginCategory pluginCategory = this.m_PluginCategoryDictionary.GetPluginCategory(iIndex);
                    if (pluginCategory != null)
                    {
                        GISShare.Controls.WinForm.WFNew.View.NodeViewItem nodeViewItem = new Controls.WinForm.WFNew.View.NodeViewItem
                            (
                            pluginCategory.CategoryIndex.ToString(),
                            "目录索引（CategoryIndex）：" + pluginCategory.CategoryIndex + "；成员数（Count）：" + pluginCategory.PluginCollection.Count.ToString()
                            );
                        nodeViewItem.IsExpanded = true;
                        foreach (IPlugin one in pluginCategory.PluginCollection)
                        {
                            IPluginInfo pPluginInfo = one as IPluginInfo;
                            if (pPluginInfo == null)
                            {
                                nodeViewItem.NodeViewItems.Add
                                    (
                                    new Controls.WinForm.WFNew.View.NodeViewItem(one.Name, "目录索引（CategoryIndex）：" + one.CategoryIndex + "；名称（Name）：" + one.Name)
                                    );
                            }
                            else
                            {
                                nodeViewItem.NodeViewItems.Add
                                    (
                                    new Controls.WinForm.WFNew.View.NodeViewItem(one.Name, "目录索引（CategoryIndex）：" + one.CategoryIndex + "；描述（Describe）：" + pPluginInfo.GetDescribe() + "；名称（Name）：" + one.Name)
                                    );
                            }
                            iNum++;
                        }
                        this.nodeViewItemTree1.NodeViewItems.Add(nodeViewItem);
                    }
                }
            }
            this.lblNum.Text = "当前插件：" + iNum.ToString();
            this.lblNum.Refresh();
        }

        private void btnExpandAll_MouseClick(object sender, MouseEventArgs e)
        {
            this.nodeViewItemTree1.ExpandAll();
        }

        private void btnCollapseAll_MouseClick(object sender, MouseEventArgs e)
        {
            this.nodeViewItemTree1.CollapseAll();
        }

        private void btnInfo_MouseClick(object sender, MouseEventArgs e)
        {
            GISShare.Controls.Plugin.WinForm.InfoForm infoForm = new GISShare.Controls.Plugin.WinForm.InfoForm();
            infoForm.ShowDialog();
        }
    }
}
