using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.MenuBar
{
    class MenuBarMainMenu : GISShare.Controls.Plugin.WinForm.DockBar.MenuBarP
    {
        public MenuBarMainMenu()
        {
            this._Name = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.MenuBar.MenuBarMainMenu";
            this._Text = "主菜单";
            this._ItemCount = 2;
        }

        public override void GetItemInfo(int iIndex, IItemDef pItemDef)
        {
            switch (iIndex)
            {
                case 0:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.MenuBar.Item.MenuItemFile";
                    pItemDef.Group = false;
                    break;
                case 1:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.MenuBar.Item.MenuItemAbout";
                    pItemDef.Group = false;
                    break;
            }
        }
    }
}
