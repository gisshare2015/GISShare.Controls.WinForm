using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.InsertSubItem.BaseItem
{
    class BaseButtonAppendPlugin : GISShare.Controls.Plugin.WinForm.WFNew.BaseButtonItemP
    {
        public BaseButtonAppendPlugin()
        {
            this._Name = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.InsertSubItem.BaseItem.BaseButtonAppendPlugin";
            this._Text = "加载插件";
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
            GISShare.Controls.Plugin.IBaseHost2 pBaseHost2 = this.m_pAppHook.Host as GISShare.Controls.Plugin.IBaseHost2;
            if (pBaseHost2 == null) return;
            pBaseHost2.AppendPlugin();
        }
    }
}
