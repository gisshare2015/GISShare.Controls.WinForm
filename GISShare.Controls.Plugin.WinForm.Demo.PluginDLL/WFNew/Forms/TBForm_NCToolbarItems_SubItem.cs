using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Forms
{
    class TBForm_NCToolbarItems_SubItem : GISShare.Controls.Plugin.WinForm.WFNew.Forms.NCToolbarItemsSubItem
    {
        public TBForm_NCToolbarItems_SubItem() 
        {
            this._Name = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Forms.TBForm_NCToolbarItems_SubItem";
            this._ItemCount = 7;
        }

        public override void GetItemInfo(int iIndex, IItemDef pItemDef)
        {
            switch (iIndex)
            {
                case 0:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Forms.BaseItem.BaseButtonNew";
                    pItemDef.Group = false;
                    break;
                case 1:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Forms.BaseItem.BaseButtonOpen";
                    pItemDef.Group = false;
                    break;
                case 2:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Forms.BaseItem.BaseButtonSave";
                    pItemDef.Group = false;
                    break;
                case 3:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Forms.BaseItem.SeparatorItem1";
                    pItemDef.Group = false;
                    break;
                case 4:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Forms.BaseItem.BaseButtonExist";
                    pItemDef.Group = false;
                    break;
                case 5:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Forms.BaseItem.SeparatorItem2";
                    pItemDef.Group = false;
                    break;
                case 6:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Forms.BaseItem.BaseButtonAbout";
                    pItemDef.Group = false;
                    break;
            }
        }
    }
}
