using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonControlEx_ApplicationPopup_MenuItems
{
    class RibbonControlEx_ApplicationPopup_MenuItems_SubItem : GISShare.Controls.Plugin.WinForm.WFNew.Ribbon.MenuItemsSubItem
    {
        public RibbonControlEx_ApplicationPopup_MenuItems_SubItem() 
        {
            this._Name = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonControlEx_ApplicationPopup_MenuItems.RibbonControlEx_ApplicationPopup_MenuItems_SubItem";
            this._ItemCount = 7;
        }

        public override void GetItemInfo(int iIndex, IItemDef pItemDef)
        {
            switch (iIndex)
            {
                case 0:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonControlEx_ApplicationPopup_MenuItems.BaseItem.MenuButtonNew";
                    pItemDef.Group = false;
                    break;
                case 1:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonControlEx_ApplicationPopup_MenuItems.BaseItem.MenuButtonOpen";
                    pItemDef.Group = false;
                    break;
                case 2:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonControlEx_ApplicationPopup_MenuItems.BaseItem.MenuButtonSave";
                    pItemDef.Group = false;
                    break;
                case 3:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonControlEx_ApplicationPopup_MenuItems.BaseItem.MenuButtonSaveAs";
                    pItemDef.Group = false;
                    break;
                case 4:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonControlEx_ApplicationPopup_MenuItems.BaseItem.SeparatorItem1";
                    pItemDef.Group = false;
                    break;
                case 5:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonControlEx_ApplicationPopup_MenuItems.BaseItem.MenuButtonAbout";
                    pItemDef.Group = false;
                    break;
                case 6:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonControlEx_ApplicationPopup_MenuItems.BaseItem.MenuButtonExist";
                    pItemDef.Group = false;
                    break;
            }
        }
    }
}
