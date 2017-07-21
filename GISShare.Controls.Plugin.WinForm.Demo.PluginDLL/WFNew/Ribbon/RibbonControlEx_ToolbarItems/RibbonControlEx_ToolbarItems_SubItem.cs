using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonControlEx_ToolbarItems
{
    class RibbonControlEx_ToolbarItems_SubItem : GISShare.Controls.Plugin.WinForm.WFNew.Ribbon.ToolbarItemsSubItem
    {
        public RibbonControlEx_ToolbarItems_SubItem() 
        {
            this._Name = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonControlEx_ToolbarItems.RibbonControlEx_ToolbarItems_SubItem";
            this._ItemCount = 7;
        }

        public override void GetItemInfo(int iIndex, IItemDef pItemDef)
        {
            switch (iIndex)
            {
                case 0:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonControlEx_ToolbarItems.BaseItem.BaseButtonNew";
                    pItemDef.Group = false;
                    break;
                case 1:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonControlEx_ToolbarItems.BaseItem.BaseButtonOpen";
                    pItemDef.Group = false;
                    break;
                case 2:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonControlEx_ToolbarItems.BaseItem.BaseButtonSave";
                    pItemDef.Group = false;
                    break;
                case 3:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonControlEx_ToolbarItems.BaseItem.SeparatorItem1";
                    pItemDef.Group = false;
                    break;
                case 4:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonControlEx_ToolbarItems.BaseItem.BaseButtonExist";
                    pItemDef.Group = false;
                    break;
                case 5:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonControlEx_ToolbarItems.BaseItem.SeparatorItem2";
                    pItemDef.Group = false;
                    break;
                case 6:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonControlEx_ToolbarItems.BaseItem.BaseButtonAbout";
                    pItemDef.Group = false;
                    break;
            }
        }
    }
}
