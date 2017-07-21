using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonControlEx_ApplicationPopup_MenuItems.BaseItem
{
    class MenuButtonNew : GISShare.Controls.Plugin.WinForm.WFNew.MenuButtonItemP
    {
        public MenuButtonNew()
        {
            this._Name = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonControlEx_ApplicationPopup_MenuItems.BaseItem.MenuButtonNew";
            this._Text = "新建";
            this._Padding = new System.Windows.Forms.Padding(2);
            this._Image = new System.Drawing.Bitmap(this.GetType().Assembly.GetManifestResourceStream("GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.Image.New32.ico"));
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
