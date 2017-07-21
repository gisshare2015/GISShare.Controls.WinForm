using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonControlEx_RibbonPages.BaseItem.RibbonPageAbout_BaseItem.RibbonBarAbout_BaseItem
{
    class BaseButtonLZHPluginInfo : GISShare.Controls.Plugin.WinForm.WFNew.BaseButtonItemP
    {
        public BaseButtonLZHPluginInfo()
        {
            this._Name = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonControlEx_RibbonPages.BaseItem.RibbonPageAbout_BaseItem.RibbonBarAbout_BaseItem.BaseButtonLZHPluginInfo";
            this._Text = "关于插件";
            this._Padding = new System.Windows.Forms.Padding(1);
        }

        public override void OnTriggerEvent(int iEventStyle, EventArgs e)
        {
            GISShare.Controls.Plugin.WinForm.InfoForm infoForm = new Plugin.WinForm.InfoForm();
            infoForm.ShowDialog();
        }
    }
}
