using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.DockPanel
{
    class DockPanelLister : GISShare.Controls.Plugin.WinForm.WFNew.DockPanel.DockPanelP
    {
        public DockPanelLister() 
        {
            this._Name = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.DockPanel.DockPanelLister";
            this._Text = "文本同步窗口（测试）";
            this._ItemCount = 1;
            this._BasePanelSelectedIndex = 0;
            this._DockStyle = System.Windows.Forms.DockStyle.Bottom;
        }

        public override void GetItemInfo(int iIndex, IItemDef pItemDef)
        {
            switch (iIndex)
            {
                case 0:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.DockPanel.BaseItem.BasePanelLister";
                    pItemDef.Group = false;
                    break;
            }
        }
    }
}
