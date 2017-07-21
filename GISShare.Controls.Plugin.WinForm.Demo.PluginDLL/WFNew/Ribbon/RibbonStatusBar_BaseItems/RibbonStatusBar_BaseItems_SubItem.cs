using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonStatusBar_BaseItems
{
    class RibbonStatusBar_BaseItems_SubItem : GISShare.Controls.Plugin.WinForm.WFNew.Ribbon.RibbonStatusBarSubItem
    {
        public RibbonStatusBar_BaseItems_SubItem() 
        {
            this._Name = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonStatusBar_BaseItems.RibbonStatusBar_BaseItems_SubItem";
            this._ItemCount = 1;
        }

        public override void GetItemInfo(int iIndex, IItemDef pItemDef)
        {
            switch (iIndex)
            {
                case 0:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonStatusBar_BaseItems.BaseItem.LabelInfo";
                    pItemDef.Group = false;
                    break;
            }
        }
    }
}
