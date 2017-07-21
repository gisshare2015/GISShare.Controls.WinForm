using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.InsertSubItem.BaseItem
{
    class BaseButtonUIView : GISShare.Controls.Plugin.WinForm.WFNew.BaseButtonItemP
    {
        public BaseButtonUIView()
        {
            this._Name = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.InsertSubItem.BaseItem.BaseButtonUIView";
            this._Text = "界面视图";
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
            this.m_pAppHook.UIView();
        }
    }
}
