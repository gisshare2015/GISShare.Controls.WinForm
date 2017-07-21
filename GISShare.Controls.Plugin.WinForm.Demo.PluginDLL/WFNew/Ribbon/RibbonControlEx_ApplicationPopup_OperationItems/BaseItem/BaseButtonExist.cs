using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonControlEx_ApplicationPopup_OperationItems.BaseItem
{
    class BaseButtonExist : GISShare.Controls.Plugin.WinForm.WFNew.BaseButtonItemP
    {
        public BaseButtonExist()
        {
            this._Name = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonControlEx_ApplicationPopup_OperationItems.BaseItem.BaseButtonExist";
            this._Text = "退 出";
            this._Padding = new System.Windows.Forms.Padding(2);
            this._Size = new System.Drawing.Size(90,21);
            this._ShowNomalState = true;
            this._eDisplayStyle = GISShare.Controls.WinForm.WFNew.DisplayStyle.eImageAndText;
            //this._Image = new System.Drawing.Bitmap(this.GetType().Assembly.GetManifestResourceStream("GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.Image.Exist.ico"));
        }

        Hook.IAppHook m_pAppHook;
        public override void OnCreate(object hook)
        {
            this.m_pAppHook = hook as Hook.IAppHook;
        }

        public override void OnTriggerEvent(int iEventStyle, EventArgs e)
        {
            if (this.m_pAppHook == null) return;
            System.Windows.Forms.Form form = this.m_pAppHook.Host as System.Windows.Forms.Form;
            if (form == null) return;
            form.Close();
        }
    }
}
