using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.ContextPopup
{
    public class ContextPopupDocument : GISShare.Controls.Plugin.WinForm.WFNew.ContextPopupP
    {
        public ContextPopupDocument()
        {
            this._Name = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.ContextPopup.ContextPopupDocument";
            this._Text = "ContextPopupDocument";
            this._ItemCount = 7;
        }

        Hook.IAppHook m_pAppHook;
        public override void OnCreate(object hook)
        {
            this.m_pAppHook = hook as Hook.IAppHook;
            if (this.m_pAppHook != null) 
            {
                this.m_pAppHook.RichTextBox.MouseDown += new System.Windows.Forms.MouseEventHandler(RichTextBox_MouseDown);
            }
        }
        void RichTextBox_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                GISShare.Controls.WinForm.WFNew.ContextPopup contextPopup = this.EntityObject as GISShare.Controls.WinForm.WFNew.ContextPopup;
                if (contextPopup != null) 
                {
                    contextPopup.Show(this.m_pAppHook.RichTextBox, e.Location);
                }
            }
        }

        public override void GetItemInfo(int iIndex, IItemDef pItemDef)
        {
            switch (iIndex)
            {
                case 0:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.ContextPopup.BaseItem.BaseButtonNew";
                    pItemDef.Group = false;
                    break;
                case 1:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.ContextPopup.BaseItem.BaseButtonOpen";
                    pItemDef.Group = false;
                    break;
                case 2:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.ContextPopup.BaseItem.BaseButtonSave";
                    pItemDef.Group = false;
                    break;
                case 3:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.ContextPopup.BaseItem.SeparatorItem1";
                    pItemDef.Group = false;
                    break;
                case 4:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.ContextPopup.BaseItem.BaseButtonExist";
                    pItemDef.Group = false;
                    break;
                case 5:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.ContextPopup.BaseItem.SeparatorItem2";
                    pItemDef.Group = false;
                    break;
                case 6:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.ContextPopup.BaseItem.BaseButtonAbout";
                    pItemDef.Group = false;
                    break;
            }
        }
    }
}
