using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.ContextMenu.Item
{
    class MenuItemNew : GISShare.Controls.Plugin.WinForm.DockBar.MenuItemP
    {
        public MenuItemNew()
        {
            this._Name = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.ContextMenu.Item.MenuItemNew";
            this._Text = "新建";
            this._ToolTipText = this._Text;
            this._Category = "快捷菜单";
            this._Image = new System.Drawing.Bitmap(this.GetType().Assembly.GetManifestResourceStream("GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.Image.New.ico"));
        }

        Hook.IAppHook m_pAppHook;
        public override void OnCreate(object hook)
        {
            this.m_pAppHook = hook as Hook.IAppHook;
        }

        public override void OnTriggerEvent(int iEventStyle, EventArgs e)
        {
            if (this.m_pAppHook == null) return;
            this.m_pAppHook.FileName = null;
            this.m_pAppHook.RichTextBox.Text = "";
        }
    }
}
