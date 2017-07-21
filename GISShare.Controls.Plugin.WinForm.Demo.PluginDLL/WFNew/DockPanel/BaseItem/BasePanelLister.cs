using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.DockPanel.BaseItem
{
    class BasePanelLister : GISShare.Controls.Plugin.WinForm.WFNew.DockPanel.BasePanelP
    {
        public BasePanelLister()
        {
            this._Name = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.DockPanel.BaseItem.BasePanelLister";
            this._Text = "文本同步窗口（测试）";
            this._ChildControls = new System.Windows.Forms.Control[]
            {
                new System.Windows.Forms.RichTextBox()
                {
                    Dock = System.Windows.Forms.DockStyle.Fill,
                    ReadOnly = true,
                    BackColor = System.Drawing.Color.White,
                    BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
                }
            };
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
            this._ChildControls[0].Text = richTextBox.Text;
        }
    }
}
