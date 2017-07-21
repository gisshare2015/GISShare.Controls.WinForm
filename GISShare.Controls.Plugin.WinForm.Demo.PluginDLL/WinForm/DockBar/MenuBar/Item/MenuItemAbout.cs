using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.MenuBar.Item
{
    class MenuItemAbout : GISShare.Controls.Plugin.WinForm.DockBar.MenuItemP
    {
        public MenuItemAbout()
        {
            this._Name = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.MenuBar.Item.MenuItemAbout";
            this._Text = "关于";
            this._ToolTipText = this._Text;
            this._Category = "主菜单";
            this._ItemCount = 2;
        }

        public override void GetItemInfo(int iIndex, IItemDef pItemDef)
        {
            switch (iIndex)
            {
                case 0:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.MenuBar.Item.MenuItemAbout_Item.MenuItemLZHPluginInfo";
                    pItemDef.Group = false;
                    break;
                case 1:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.MenuBar.Item.MenuItemAbout_Item.MenuItemLZHControlInfo";
                    pItemDef.Group = false;
                    break;
            }
        }
    }
}
