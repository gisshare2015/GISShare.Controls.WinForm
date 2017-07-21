using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.InsertSubItem.Item
{
    class ButtonItemUIView : GISShare.Controls.Plugin.WinForm.DockBar.ButtonItemP
    {
        public ButtonItemUIView()
        {
            this._Name = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.InsertSubItem.Item.ButtonItemUIView";
            this._Text = "界面视图";
            this._ToolTipText = this._Text;
            this._Category = "插入项";
        }

        Hook.IAppHook m_pAppHook;
        public override void OnCreate(object hook)
        {
            this.m_pAppHook = hook as Hook.IAppHook;
        }

        public override void OnTriggerEvent(int iEventStyle, EventArgs e)
        {
            if (this.m_pAppHook == null) return;
            this.m_pAppHook.UIView();
        }
    }
}
