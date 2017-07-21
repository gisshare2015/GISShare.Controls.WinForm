using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.InsertSubItem
{
    class InsertSubItem_RibbonBarDockPanelManager : GISShare.Controls.Plugin.InsertSubItem
    {
        public InsertSubItem_RibbonBarDockPanelManager()
        {
            this._Name = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.InsertSubItem.InsertSubItem_RibbonBarDockPanelManager";
            this._ItemCount = 4;
            //
            this._DependItem = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonControlEx_RibbonPages.BaseItem.RibbonPageFile_BaseItem.RibbonBarDockPanelManager";
            this._InsertCategoryIndex = (int)GISShare.Controls.Plugin.WinForm.WFNew.CategoryIndex_1_Style.eRibbonBarItem;
            this._InsertIndex = 0;
            this._LoadingIndex = 0;
        }

        public override void GetItemInfo(int iIndex, IItemDef pItemDef)
        {
            switch (iIndex)
            {
                case 0:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.InsertSubItem.BaseItem.BaseButtonAppendPlugin";
                    pItemDef.Group = false;
                    break;
                case 1:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.InsertSubItem.BaseItem.BaseButtonPluginCategoryDictionary";
                    pItemDef.Group = false;
                    break;
                case 2:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.InsertSubItem.BaseItem.BaseButtonUIView";
                    pItemDef.Group = false;
                    break;
                case 3:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.InsertSubItem.BaseItem.SeparatorItem1";
                    pItemDef.Group = false;
                    break;
            }
        }
    }
}
