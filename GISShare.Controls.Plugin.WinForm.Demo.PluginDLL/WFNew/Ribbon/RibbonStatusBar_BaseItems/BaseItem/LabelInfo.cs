using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonStatusBar_BaseItems.BaseItem
{
    class LabelInfo : GISShare.Controls.Plugin.WinForm.WFNew.LabelItemP
    {
        public LabelInfo()
        {
            this._Name = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonStatusBar_BaseItems.BaseItem.LabelInfo";
            this._Text = "当前字数：0";
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
            GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = this.EntityObject as GISShare.Controls.WinForm.WFNew.IBaseItem;
            if (pBaseItem == null) return;
            pBaseItem.Text = "当前字数：" + richTextBox.Text.Length.ToString();
            pBaseItem.Refresh();
        }
    }
}
