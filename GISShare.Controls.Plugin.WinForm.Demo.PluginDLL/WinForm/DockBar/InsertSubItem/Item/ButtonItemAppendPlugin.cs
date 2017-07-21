using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.InsertSubItem.Item
{
    class ButtonItemAppendPlugin : GISShare.Controls.Plugin.WinForm.DockBar.ButtonItemP
    {
        public ButtonItemAppendPlugin()
        {
            this._Name = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.InsertSubItem.Item.ButtonItemAppendPlugin";
            this._Text = "加载插件";
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
            GISShare.Controls.Plugin.IBaseHost2 pBaseHost2 = this.m_pAppHook.Host as GISShare.Controls.Plugin.IBaseHost2;
            if (pBaseHost2 == null) return;
            pBaseHost2.AppendPlugin();
        }
    }
}
