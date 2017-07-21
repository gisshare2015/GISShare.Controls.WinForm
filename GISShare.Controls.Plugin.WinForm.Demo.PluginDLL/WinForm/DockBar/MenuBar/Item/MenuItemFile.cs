using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.MenuBar.Item
{
    class MenuItemFile : GISShare.Controls.Plugin.WinForm.DockBar.MenuItemP
    {
        public MenuItemFile()
        {
            this._Name = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.MenuBar.Item.MenuItemFile";
            this._Text = "文件";
            this._ToolTipText = this._Text;
            this._Category = "主菜单";
            this._ItemCount = 6;
        }

        public override void GetItemInfo(int iIndex, IItemDef pItemDef)
        {
            switch (iIndex)
            {
                case 0:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.MenuBar.Item.MenuItemFile_Item.MenuItemNew";
                    pItemDef.Group = false;
                    break;
                case 1:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.MenuBar.Item.MenuItemFile_Item.MenuItemOpen";
                    pItemDef.Group = false;
                    break;
                case 2:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.MenuBar.Item.MenuItemFile_Item.MenuItemSave";
                    pItemDef.Group = false;
                    break;
                case 3:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.MenuBar.Item.MenuItemFile_Item.MenuItemSaveAs";
                    pItemDef.Group = false;
                    break;
                case 4:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.MenuBar.Item.MenuItemFile_Item.SeparatorItem1";
                    pItemDef.Group = false;
                    break;
                case 5:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.MenuBar.Item.MenuItemFile_Item.MenuItemExist";
                    pItemDef.Group = false;
                    break;
            }
        }
    }
}
