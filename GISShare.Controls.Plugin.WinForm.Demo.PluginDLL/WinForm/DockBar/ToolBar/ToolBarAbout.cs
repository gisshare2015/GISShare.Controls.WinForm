using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.ToolBar
{
    class ToolBarAbout : GISShare.Controls.Plugin.WinForm.DockBar.ToolBarP
    {
        public ToolBarAbout()
        {
            this._Name = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.ToolBar.ToolBarAbout";
            this._Text = "关于";
            this._ItemCount = 2;
        }

        public override void GetItemInfo(int iIndex, IItemDef pItemDef)
        {
            switch (iIndex)
            {
                case 0:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.MenuBar.Item.MenuItemAbout_Item.ButtonItemLZHPluginInfo";
                    pItemDef.Group = false;
                    break;
                case 1:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.MenuBar.Item.MenuItemAbout_Item.ButtonItemLZHControlInfo";
                    pItemDef.Group = false;
                    break;
            }
        }
    }
}
