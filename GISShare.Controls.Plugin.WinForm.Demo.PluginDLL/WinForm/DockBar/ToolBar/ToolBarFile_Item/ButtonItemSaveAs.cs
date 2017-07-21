using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.ToolBar.ToolBarFile_Item
{
    class ButtonItemSaveAs : GISShare.Controls.Plugin.WinForm.DockBar.ButtonItemP
    {
        public ButtonItemSaveAs()
        {
            this._Name = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.ToolBar.ToolBarFile_Item.ButtonItemSaveAs";
            this._Text = "另存为";
            this._ToolTipText = this._Text;
            this._Category = "工具条_文件";
            this._Image = new System.Drawing.Bitmap(this.GetType().Assembly.GetManifestResourceStream("GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.Image.SaveAs.ico"));
        }

        Hook.IAppHook m_pAppHook;
        public override void OnCreate(object hook)
        {
            this.m_pAppHook = hook as Hook.IAppHook;
        }

        public override void OnTriggerEvent(int iEventStyle, EventArgs e)
        {
            if (this.m_pAppHook == null) return;
            System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog.Title = "保存文档";
            saveFileDialog.Filter = "TXT文件|*.txt|其它文件|*.*";
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.m_pAppHook.RichTextBox.LoadFile(saveFileDialog.FileName, System.Windows.Forms.RichTextBoxStreamType.PlainText);
            }
        }
    }
}
