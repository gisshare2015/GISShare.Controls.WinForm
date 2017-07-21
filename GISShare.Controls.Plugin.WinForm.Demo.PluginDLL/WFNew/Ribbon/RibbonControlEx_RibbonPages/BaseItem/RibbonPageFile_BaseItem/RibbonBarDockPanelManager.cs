using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonControlEx_RibbonPages.BaseItem.RibbonPageFile_BaseItem
{
    class RibbonBarDockPanelManager : GISShare.Controls.Plugin.WinForm.WFNew.RibbonBarItemP
    {
        public RibbonBarDockPanelManager()
        {
            this._Name = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonControlEx_RibbonPages.BaseItem.RibbonPageFile_BaseItem.RibbonBarDockPanelManager";
            this._Text = "面板管理";
            this._ItemCount = 1;
        }

        public override void GetItemInfo(int iIndex, IItemDef pItemDef)
        {
            switch (iIndex)
            {
                case 0:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonControlEx_RibbonPages.BaseItem.RibbonPageFile_BaseItem.RibbonBarDockPanelManager_BaseItem.BaseButtonDockPanelManager";
                    pItemDef.Group = false;
                    break;
            }
        }
    }
}
