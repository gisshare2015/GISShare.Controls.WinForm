using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.ToolBar.ToolBarDockPanelManager_Item
{
    class ButtonItemDockPanelManager : GISShare.Controls.Plugin.WinForm.DockBar.ButtonItemP
    {
        public ButtonItemDockPanelManager()
        {
            this._Name = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.ToolBar.ToolBarDockPanelManager_Item.ButtonItemDockPanelManager";
            this._Text = "浮动面板管理器";
            this._ToolTipText = this._Text;
            this._Category = "工具条_浮动面板管理器";
        }

        Hook.IAppHook m_pAppHook;
        public override void OnCreate(object hook)
        {
            this.m_pAppHook = hook as Hook.IAppHook;
        }

        public override void OnTriggerEvent(int iEventStyle, EventArgs e)
        {
            if (this.m_pAppHook == null) return;
            this.m_pAppHook.DockPanelManager.DockPanelCustomize();
        }
    }
}
