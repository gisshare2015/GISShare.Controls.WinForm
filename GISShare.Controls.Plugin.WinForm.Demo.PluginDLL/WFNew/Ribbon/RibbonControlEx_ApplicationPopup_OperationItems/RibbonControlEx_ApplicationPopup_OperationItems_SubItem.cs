using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonControlEx_ApplicationPopup_OperationItems
{
    class RibbonControlEx_ApplicationPopup_OperationItems_SubItem : GISShare.Controls.Plugin.WinForm.WFNew.Ribbon.OperationItemsSubItem
    {
        public RibbonControlEx_ApplicationPopup_OperationItems_SubItem() 
        {
            this._Name = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonControlEx_ApplicationPopup_OperationItems.RibbonControlEx_ApplicationPopup_OperationItems_SubItem";
            this._ItemCount = 1;
        }

        public override void GetItemInfo(int iIndex, IItemDef pItemDef)
        {
            switch (iIndex)
            {
                case 0:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonControlEx_ApplicationPopup_OperationItems.BaseItem.BaseButtonExist";
                    pItemDef.Group = false;
                    break;
            }
        }
    }
}
