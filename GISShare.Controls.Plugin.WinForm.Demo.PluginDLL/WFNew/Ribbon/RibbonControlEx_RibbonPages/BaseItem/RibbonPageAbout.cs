using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonControlEx_RibbonPages.BaseItem
{
    class RibbonPageAbout : GISShare.Controls.Plugin.WinForm.WFNew.RibbonPageItemP
    {
        public RibbonPageAbout()
        {
            this._Name = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonControlEx_RibbonPages.BaseItem.RibbonPageAbout";
            this._Text = "关于";
            this._ItemCount = 1;
        }

        public override void GetItemInfo(int iIndex, IItemDef pItemDef)
        {
            switch (iIndex)
            {
                case 0:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonControlEx_RibbonPages.BaseItem.RibbonPageAbout_BaseItem.RibbonBarAbout";
                    pItemDef.Group = false;
                    break;
            }
        }

    }
}
