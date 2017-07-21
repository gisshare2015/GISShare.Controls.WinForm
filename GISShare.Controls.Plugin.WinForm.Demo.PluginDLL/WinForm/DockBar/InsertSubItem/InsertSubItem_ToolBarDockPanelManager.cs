using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.InsertSubItem
{
    class InsertSubItem_ToolBarDockPanelManager : GISShare.Controls.Plugin.InsertSubItem
    {
        public InsertSubItem_ToolBarDockPanelManager()
        {
            this._Name = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.InsertSubItem.InsertSubItem_ToolBarDockPanelManager";
            this._ItemCount = 4;
            //
            this._DependItem = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.ToolBar.ToolBarDockPanelManager";
            this._InsertCategoryIndex = (int)GISShare.Controls.Plugin.WinForm.DockBar.CategoryIndex_5_Style.eToolBar;
            this._InsertIndex = 0;
            this._LoadingIndex = 0;
        }

        public override void GetItemInfo(int iIndex, IItemDef pItemDef)
        {
            switch (iIndex)
            {
                case 0:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.InsertSubItem.Item.ButtonItemAppendPlugin";
                    pItemDef.Group = false;
                    break;
                case 1:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.InsertSubItem.Item.ButtonItemPluginCategoryDictionary";
                    pItemDef.Group = false;
                    break;
                case 2:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.InsertSubItem.Item.ButtonItemUIView";
                    pItemDef.Group = false;
                    break;
                case 3:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.InsertSubItem.Item.SeparatorItem1";
                    pItemDef.Group = false;
                    break;
            }
        }
    }
}
