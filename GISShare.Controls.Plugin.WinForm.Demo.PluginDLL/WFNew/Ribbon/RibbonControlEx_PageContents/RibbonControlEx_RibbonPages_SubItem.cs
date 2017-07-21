using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonControlEx_PageContents
{
    public class RibbonControlEx_PageContents_SubItem : GISShare.Controls.Plugin.WinForm.WFNew.Ribbon.PageContentsSubItem
    {
        public RibbonControlEx_PageContents_SubItem() 
        {
            this._Name = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonControlEx_PageContents.RibbonControlEx_PageContents_SubItem";
            this._ItemCount = 2;
        }

        public override void GetItemInfo(int iIndex, IItemDef pItemDef)
        {
            switch (iIndex)
            {
                case 0:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonControlEx_PageContents.BaseItem.LabelSkinInfo";
                    pItemDef.Group = false;
                    break;
                case 1:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonControlEx_PageContents.BaseItem.ComboBoxItemSkin";
                    pItemDef.Group = false;
                    break;
            }
        }
    }
}
