using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonControlEx_RibbonPages.BaseItem.RibbonPageFile_BaseItem.RibbonBarDockPanelManager_BaseItem
{
    class BaseButtonDockPanelManager : GISShare.Controls.Plugin.WinForm.WFNew.BaseButtonItemP
    {
        public BaseButtonDockPanelManager()
        {
            this._Name = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonControlEx_RibbonPages.BaseItem.RibbonPageFile_BaseItem.RibbonBarDockPanelManager_BaseItem.BaseButtonDockPanelManager";
            this._Text = "浮动面板管理器";
            this._Padding = new System.Windows.Forms.Padding(1);
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
