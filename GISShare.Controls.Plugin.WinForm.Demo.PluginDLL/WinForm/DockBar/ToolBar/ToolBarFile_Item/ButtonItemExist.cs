using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.ToolBar.ToolBarFile_Item
{
    class ButtonItemExist : GISShare.Controls.Plugin.WinForm.DockBar.ButtonItemP
    {
        public ButtonItemExist()
        {
            this._Name = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.ToolBar.ToolBarFile_Item.ButtonItemExist";
            this._Text = "退出";
            this._ToolTipText = this._Text;
            this._Category = "工具条_文件";
            this._Image = new System.Drawing.Bitmap(this.GetType().Assembly.GetManifestResourceStream("GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.Image.Exist.ico"));
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
