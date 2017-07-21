using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonControlEx_ApplicationPopup_MenuItems.BaseItem.SaveAs_BaseItem
{
    class DescriptionButtonSaveAsOther : GISShare.Controls.Plugin.WinForm.WFNew.DescriptionButtonItemP
    {
        public DescriptionButtonSaveAsOther()
        {
            this._Name = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonControlEx_ApplicationPopup_MenuItems.BaseItem.SaveAs_BaseItem.DescriptionButtonSaveAsOther";
            this._Text = "另存为其它格式";
            this._Description = "打开“另存为”对话框从所有可能的文件类型中选择";
            this._Padding = new System.Windows.Forms.Padding(2);
            this._Image = new System.Drawing.Bitmap(this.GetType().Assembly.GetManifestResourceStream("GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.Image.SaveAs32.ico"));
            this._ImageAlign = System.Drawing.ContentAlignment.TopLeft;
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
            saveFileDialog.FilterIndex = 1;
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.m_pAppHook.RichTextBox.LoadFile(saveFileDialog.FileName, System.Windows.Forms.RichTextBoxStreamType.PlainText);
            }
        }
    }
}
