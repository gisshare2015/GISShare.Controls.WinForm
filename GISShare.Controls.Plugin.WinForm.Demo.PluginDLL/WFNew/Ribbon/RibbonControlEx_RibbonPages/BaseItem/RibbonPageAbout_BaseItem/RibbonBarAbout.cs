using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonControlEx_RibbonPages.BaseItem.RibbonPageAbout_BaseItem
{
    class RibbonBarAbout : GISShare.Controls.Plugin.WinForm.WFNew.RibbonBarItemP
    {
        public RibbonBarAbout()
        {
            this._Name = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonControlEx_RibbonPages.BaseItem.RibbonPageAbout_BaseItem.RibbonBarAbout";
            this._Text = "关于";
            this._ItemCount = 2;
        }

        public override void GetItemInfo(int iIndex, IItemDef pItemDef)
        {
            switch (iIndex)
            {
                case 0:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonControlEx_RibbonPages.BaseItem.RibbonPageAbout_BaseItem.RibbonBarAbout_BaseItem.BaseButtonLZHPluginInfo";
                    pItemDef.Group = false;
                    break;
                case 1:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonControlEx_RibbonPages.BaseItem.RibbonPageAbout_BaseItem.RibbonBarAbout_BaseItem.BaseButtonLZHControlInfo";
                    pItemDef.Group = false;
                    break;
            }
        }
    }
}
