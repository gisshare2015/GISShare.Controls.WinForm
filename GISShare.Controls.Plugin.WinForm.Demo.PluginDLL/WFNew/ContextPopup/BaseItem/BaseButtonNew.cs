using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.ContextPopup.BaseItem
{
    class BaseButtonNew : GISShare.Controls.Plugin.WinForm.WFNew.BaseButtonItemP
    {
        public BaseButtonNew()
        {
            this._Name = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.ContextPopup.BaseItem.BaseButtonNew";
            this._Text = "新建";
            //this._Padding = new System.Windows.Forms.Padding(2);
            //this._eDisplayStyle = GISShare.Controls.WinForm.WFNew.DisplayStyle.eImage;
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
