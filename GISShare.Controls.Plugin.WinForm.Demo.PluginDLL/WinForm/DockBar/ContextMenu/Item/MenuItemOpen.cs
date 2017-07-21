using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.ContextMenu.Item
{
    class MenuItemOpen : GISShare.Controls.Plugin.WinForm.DockBar.MenuItemP
    {
        public MenuItemOpen()
        {
            this._Name = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.ContextMenu.Item.MenuItemOpen";
            this._Text = "打开";
            this._ToolTipText = this._Text;
            this._Category = "快捷菜单";
            this._Image = new System.Drawing.Bitmap(this.GetType().Assembly.GetManifestResourceStream("GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.Image.Open.ico"));
        }

        Hook.IAppHook m_pAppHook;
        public override void OnCreate(object hook)
        {
            this.m_pAppHook = hook as Hook.IAppHook;
        }

        public override void OnTriggerEvent(int iEventStyle, EventArgs e)
        {
            if (this.m_pAppHook == null) return;
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            openFileDialog.Title = "打开文档";
            openFileDialog.Filter = "TXT文件|*.txt|其它文件|*.*";
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.m_pAppHook.FileName = openFileDialog.FileName;
                this.m_pAppHook.RichTextBox.LoadFile(this.m_pAppHook.FileName, System.Windows.Forms.RichTextBoxStreamType.PlainText);
            }
        }
    }
}
