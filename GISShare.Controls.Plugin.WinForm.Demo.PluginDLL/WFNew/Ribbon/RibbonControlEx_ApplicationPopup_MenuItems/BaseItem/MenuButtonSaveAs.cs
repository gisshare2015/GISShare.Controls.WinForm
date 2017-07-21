using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonControlEx_ApplicationPopup_MenuItems.BaseItem
{
    class MenuButtonSaveAs : GISShare.Controls.Plugin.WinForm.WFNew.MenuButtonItemP
    {
        public MenuButtonSaveAs()
        {
            this._Name = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonControlEx_ApplicationPopup_MenuItems.BaseItem.MenuButtonSaveAs";
            this._Text = "另存为";
            this._Padding = new System.Windows.Forms.Padding(2);
            this._Image = new System.Drawing.Bitmap(this.GetType().Assembly.GetManifestResourceStream("GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.Image.SaveAs32.ico"));
            this._ItemCount = 2;
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

        public override void GetItemInfo(int iIndex, IItemDef pItemDef)
        {
            switch (iIndex)
            {
                case 0:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonControlEx_ApplicationPopup_MenuItems.BaseItem.SaveAs_BaseItem.DescriptionButtonSaveAsTXT";
                    pItemDef.Group = false;
                    break;
                case 1:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonControlEx_ApplicationPopup_MenuItems.BaseItem.SaveAs_BaseItem.DescriptionButtonSaveAsOther";
                    pItemDef.Group = false;
                    break;
            }
        }
    }
}
