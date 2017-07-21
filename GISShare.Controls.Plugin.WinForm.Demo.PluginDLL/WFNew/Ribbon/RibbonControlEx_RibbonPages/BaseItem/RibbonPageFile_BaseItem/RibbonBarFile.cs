using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonControlEx_RibbonPages.BaseItem.RibbonPageFile_BaseItem
{
    class RibbonBarFile : GISShare.Controls.Plugin.WinForm.WFNew.RibbonBarItemP
    {
        public RibbonBarFile()
        {
            this._Name = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonControlEx_RibbonPages.BaseItem.RibbonPageFile_BaseItem.RibbonBarFile";
            this._Text = "文件";
            this._ItemCount = 6;
        }

        public override void GetItemInfo(int iIndex, IItemDef pItemDef)
        {
            switch (iIndex)
            {
                case 0:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonControlEx_RibbonPages.BaseItem.RibbonPageFile_BaseItem.RibbonBarFile_BaseItem.BaseButtonNew";
                    pItemDef.Group = false;
                    break;
                case 1:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonControlEx_RibbonPages.BaseItem.RibbonPageFile_BaseItem.RibbonBarFile_BaseItem.BaseButtonOpen";
                    pItemDef.Group = false;
                    break;
                case 2:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonControlEx_RibbonPages.BaseItem.RibbonPageFile_BaseItem.RibbonBarFile_BaseItem.BaseButtonSave";
                    pItemDef.Group = false;
                    break;
                case 3:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonControlEx_RibbonPages.BaseItem.RibbonPageFile_BaseItem.RibbonBarFile_BaseItem.BaseButtonSaveAs";
                    pItemDef.Group = false;
                    break;
                case 4:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonControlEx_RibbonPages.BaseItem.RibbonPageFile_BaseItem.RibbonBarFile_BaseItem.SeparatorItem1";
                    pItemDef.Group = false;
                    break;
                case 5:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonControlEx_RibbonPages.BaseItem.RibbonPageFile_BaseItem.RibbonBarFile_BaseItem.BaseButtonExist";
                    pItemDef.Group = false;
                    break;
            }
        }
    }
}
