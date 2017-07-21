using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonControlEx_PageContents.BaseItem
{
    class ComboBoxItemSkin : GISShare.Controls.Plugin.WinForm.WFNew.ComboBoxItemP
    {
        public ComboBoxItemSkin()
        {
            this._Name = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonControlEx_PageContents.BaseItem.ComboBoxItemSkin";
            this._Items = new GISShare.Controls.WinForm.WFNew.View.ViewItem[] 
            {
                new  GISShare.Controls.WinForm.WFNew.View.ViewItem("Office2007外观"),
                new  GISShare.Controls.WinForm.WFNew.View.ViewItem("Office2010外观")
            };
            this._SelectedIndex = 0;
            this._eCustomizeComboBoxStyle = GISShare.Controls.WinForm.WFNew.CustomizeComboBoxStyle.eDropDownList;
        }

        Hook.IAppHook m_pAppHook;
        public override void OnCreate(object hook)
        {
            this.m_pAppHook = hook as Hook.IAppHook;
        }

        public override void OnTriggerEvent(int iEventStyle, EventArgs e)
        {
            if (this.m_pAppHook == null) return;
            GISShare.Controls.Plugin.WinForm.WFNew.Ribbon.HostRibbonForm hostRibbonForm = this.m_pAppHook.Host as GISShare.Controls.Plugin.WinForm.WFNew.Ribbon.HostRibbonForm;
            if (hostRibbonForm == null) return;
            GISShare.Controls.WinForm.WFNew.IComboBoxItem pComboBoxItem = this.EntityObject as GISShare.Controls.WinForm.WFNew.IComboBoxItem;
            if (pComboBoxItem == null) return;
            switch (pComboBoxItem.SelectedIndex)
            {
                case 0:
                    hostRibbonForm.RibbonControl.eRibbonStyle = GISShare.Controls.WinForm.WFNew.RibbonStyle.eOffice2007;
                    break;
                case 1:
                    hostRibbonForm.RibbonControl.eRibbonStyle = GISShare.Controls.WinForm.WFNew.RibbonStyle.eOffice2010;
                    break;
                default:
                    break;
            }
        }
    }
}
