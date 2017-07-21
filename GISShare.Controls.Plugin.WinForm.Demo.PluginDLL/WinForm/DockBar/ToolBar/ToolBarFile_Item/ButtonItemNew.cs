using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.ToolBar.ToolBarFile_Item
{
    class ButtonItemNew : GISShare.Controls.Plugin.WinForm.DockBar.ButtonItemP
    {
        public ButtonItemNew()
        {
            this._Name = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.ToolBar.ToolBarFile_Item.ButtonItemNew";
            this._Text = "新建";
            this._ToolTipText = this._Text;
            this._Category = "工具条_文件";
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
