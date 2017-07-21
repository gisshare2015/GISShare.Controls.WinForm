using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonControlEx_RibbonPages.BaseItem.RibbonPageAbout_BaseItem.RibbonBarAbout_BaseItem
{
    class BaseButtonLZHControlInfo : GISShare.Controls.Plugin.WinForm.WFNew.BaseButtonItemP
    {
        public BaseButtonLZHControlInfo()
        {
            this._Name = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonControlEx_RibbonPages.BaseItem.RibbonPageAbout_BaseItem.RibbonBarAbout_BaseItem.BaseButtonLZHControlInfo";
            this._Text = "关于第三方控件";
            this._Padding = new System.Windows.Forms.Padding(1);
        }

        public override void OnTriggerEvent(int iEventStyle, EventArgs e)
        {
            GISShare.Controls.WinForm.InfoForm infoForm = new Controls.WinForm.InfoForm();
            infoForm.ShowDialog();
        }
    }
}
