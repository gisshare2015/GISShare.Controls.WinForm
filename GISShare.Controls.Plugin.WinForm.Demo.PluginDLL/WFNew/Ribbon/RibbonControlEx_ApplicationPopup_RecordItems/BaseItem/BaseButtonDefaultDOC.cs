using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonControlEx_ApplicationPopup_RecordItems.BaseItem
{
    class BaseButtonDefaultDOC : GISShare.Controls.Plugin.WinForm.WFNew.BaseButtonItemP
    {
        public BaseButtonDefaultDOC()
        {
            this._Name = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonControlEx_ApplicationPopup_RecordItems.BaseItem.BaseButtonDefaultDOC";
            this._Text = "1.系统默认文档.txt";
            this._TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._Padding = new System.Windows.Forms.Padding(2);
        }

        Hook.IAppHook m_pAppHook;
        public override void OnCreate(object hook)
        {
            this.m_pAppHook = hook as Hook.IAppHook;
        }

        public override void OnTriggerEvent(int iEventStyle, EventArgs e)
        {
            if (this.m_pAppHook == null) return;
            this.m_pAppHook.FileName = System.Windows.Forms.Application.StartupPath + "\\File\\系统默认文档.txt";
            this.m_pAppHook.RichTextBox.LoadFile(this.m_pAppHook.FileName, System.Windows.Forms.RichTextBoxStreamType.PlainText);
        }
    }
}
