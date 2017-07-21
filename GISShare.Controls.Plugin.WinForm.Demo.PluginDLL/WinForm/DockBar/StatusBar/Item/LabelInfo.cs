using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.StatusBar.Item
{
    class LabelInfo : GISShare.Controls.Plugin.WinForm.DockBar.LabelItemP
    {
        public LabelInfo()
        {
            this._Name = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.StatusBar.Item.LabelInfo";
            this._Text = "当前字数：0";
            this._ToolTipText = this._Text;
            this._Category = "状态栏";
        }

        public override void OnCreate(object hook)
        {
            Hook.IAppHook pAppHook = hook as Hook.IAppHook;
            if (pAppHook != null)
            {
                pAppHook.RichTextBox.TextChanged += new EventHandler(RichTextBox_TextChanged);
            }
        }
        void RichTextBox_TextChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.RichTextBox richTextBox = sender as System.Windows.Forms.RichTextBox;
            if (richTextBox == null) return;
            GISShare.Controls.WinForm.DockBar.IBaseItemDB pBaseItemDB = this.EntityObject as GISShare.Controls.WinForm.DockBar.IBaseItemDB;
            if (pBaseItemDB == null) return;
            pBaseItemDB.Text = "当前字数：" + richTextBox.Text.Length.ToString();
        }
    }
}
