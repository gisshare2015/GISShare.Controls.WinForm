using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    public class ComboSearchBoxItem : ComboBoxItem, IComboSearchBoxItem
    {
        private View.ViewItemSearchListBoxItem m_ViewItemSearchListBoxItem;
        public ComboSearchBoxItem()
            : base(new View.ViewItemSearchListBoxItem()) 
        {
            this.m_ViewItemSearchListBoxItem = (View.ViewItemSearchListBoxItem)((IBaseItemHost)((ICustomizePopup)((IPopupOwnerHelper)this).GetBasePopup()).ControlObject).BaseItemObject;
        }

        #region IComboSearchBoxItem
        [Browsable(true), DefaultValue(true), Description("是否显示搜索框"), Category("布局")]
        public bool ShowSearchBox
        {
            get { return this.m_ViewItemSearchListBoxItem.Visible; }
            set { this.m_ViewItemSearchListBoxItem.Visible = value; }
        }

        [Browsable(true), DefaultValue(true), Description("顶部搜索框"), Category("布局")]
        public bool SearchBoxIsTop
        {
            get { return this.m_ViewItemSearchListBoxItem.SearchBoxIsTop; }
            set { this.m_ViewItemSearchListBoxItem.SearchBoxIsTop = value; }
        }

        [Browsable(true),
        Editor(typeof(GISShare.Controls.WinForm.WFNew.View.Design.ViewItemCollectionEditer), typeof(System.Drawing.Design.UITypeEditor)),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
        Description("其所携带的子项集合（变更后需要执行更新函数）"),
        Category("子项")]
        public new List<View.ViewItem> Items
        {
            get { return this.m_ViewItemSearchListBoxItem.ViewItems; }
        }

        public void UpdateItems()
        {
            this.m_ViewItemSearchListBoxItem.UpdateViewItems();
        }
        #endregion
    }
}
