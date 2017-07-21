using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Forms.BaseItem
{
    class BaseButtonAbout : GISShare.Controls.Plugin.WinForm.WFNew.BaseButtonItemP
    {
        public BaseButtonAbout()
        {
            this._Name = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Forms.BaseItem.BaseButtonAbout";
            this._Text = "关于";
            this._Padding = new System.Windows.Forms.Padding(1);
            this._eDisplayStyle = GISShare.Controls.WinForm.WFNew.DisplayStyle.eImage;
            this._Image = new System.Drawing.Bitmap(this.GetType().Assembly.GetManifestResourceStream("GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.Image.Info.ico"));
        }

        public override void OnTriggerEvent(int iEventStyle, EventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show(
                "这是基于GISShare.Controls.WinForm控件，插件结构开发的小例子！", 
                "信息", 
                System.Windows.Forms.MessageBoxButtons.OK, 
                System.Windows.Forms.MessageBoxIcon.Information);
        }
    }
}
