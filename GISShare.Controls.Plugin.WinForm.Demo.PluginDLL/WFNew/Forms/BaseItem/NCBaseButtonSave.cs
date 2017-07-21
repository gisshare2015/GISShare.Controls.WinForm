using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Forms.BaseItem
{
    class BaseButtonSave : GISShare.Controls.Plugin.WinForm.WFNew.BaseButtonItemP
    {
        public BaseButtonSave()
        {
            this._Name = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Forms.BaseItem.BaseButtonSave";
            this._Text = "保存";
            this._Padding = new System.Windows.Forms.Padding(1);
            this._eDisplayStyle = GISShare.Controls.WinForm.WFNew.DisplayStyle.eImage;
            this._Image = new System.Drawing.Bitmap(this.GetType().Assembly.GetManifestResourceStream("GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.Image.Save.ico"));
        }

        Hook.IAppHook m_pAppHook;
        public override void OnCreate(object hook)
        {
            this.m_pAppHook = hook as Hook.IAppHook;
        }

        public override void OnTriggerEvent(int iEventStyle, EventArgs e)
        {
            if (this.m_pAppHook == null) return;
            if (System.IO.File.Exists(this.m_pAppHook.FileName))
            {
                this.m_pAppHook.RichTextBox.LoadFile(this.m_pAppHook.FileName);
            }
            else
            {
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
}
