using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.MenuBar.Item.MenuItemAbout_Item
{
    class MenuItemLZHPluginInfo : GISShare.Controls.Plugin.WinForm.DockBar.MenuItemP
    {
        public MenuItemLZHPluginInfo()
        {
            this._Name = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.MenuBar.Item.MenuItemAbout_Item.MenuItemLZHPluginInfo";
            this._Text = "关于插件";
            this._ToolTipText = this._Text;
            this._Category = "关于";
        }

        public override void OnTriggerEvent(int iEventStyle, EventArgs e)
        {
            GISShare.Controls.Plugin.WinForm.InfoForm infoForm = new Plugin.WinForm.InfoForm();
            infoForm.ShowDialog();
        }
    }
}
