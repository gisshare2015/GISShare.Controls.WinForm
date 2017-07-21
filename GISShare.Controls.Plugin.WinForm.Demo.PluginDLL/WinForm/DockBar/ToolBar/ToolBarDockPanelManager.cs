using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.ToolBar
{
    class ToolBarDockPanelManager : GISShare.Controls.Plugin.WinForm.DockBar.ToolBarP
    {
        public ToolBarDockPanelManager()
        {
            this._Name = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.ToolBar.ToolBarDockPanelManager";
            this._Text = "管理";
            this._ItemCount = 1;
        }

        public override void GetItemInfo(int iIndex, IItemDef pItemDef)
        {
            switch (iIndex)
            {
                case 0:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.ToolBar.ToolBarDockPanelManager_Item.ButtonItemDockPanelManager";
                    pItemDef.Group = false;
                    break;
            }
        }
    }
}
