using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.MenuBar.Item.MenuItemAbout_Item
{
    class MenuItemLZHControlInfo : GISShare.Controls.Plugin.WinForm.DockBar.MenuItemP
    {
        public MenuItemLZHControlInfo()
        {
            this._Name = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.MenuBar.Item.MenuItemAbout_Item.MenuItemLZHControlInfo";
            this._Text = "关于第三方控件";
            this._ToolTipText = this._Text;
            this._Category = "关于";
        }

        public override void OnTriggerEvent(int iEventStyle, EventArgs e)
        {
            GISShare.Controls.WinForm.InfoForm infoForm = new Controls.WinForm.InfoForm();
            infoForm.ShowDialog();
        }
    }
}
