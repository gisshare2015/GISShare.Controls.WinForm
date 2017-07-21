using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.DockBar.ToolBarAbout_Item
{
    class ButtonItemLZHPluginInfo : GISShare.Controls.Plugin.WinForm.DockBar.ButtonItemP
    {
        public ButtonItemLZHPluginInfo()
        {
            this._Name = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.MenuBar.Item.MenuItemAbout_Item.ButtonItemLZHPluginInfo";
            this._Text = "关于插件";
            this._ToolTipText = this._Text;
            this._Category = "工具条_关于";
        }

        public override void OnTriggerEvent(int iEventStyle, EventArgs e)
        {
            GISShare.Controls.Plugin.WinForm.InfoForm infoForm = new Plugin.WinForm.InfoForm();
            infoForm.ShowDialog();
        }
    }
}
