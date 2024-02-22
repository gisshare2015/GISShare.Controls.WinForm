using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew.View
{
    public class ViewItemSearchListBoxItem : ViewItemListBoxItem, IViewItemSearchListBox
    {
        private const int CONST_SEACHBOXSIZE = 21;
        private const int CONST_SEACHBOXSIZE_SPACE = 1;
        //
        private SearchBoxItem m_SearchBoxItem;
        private List<View.ViewItem> m_ViewItemList = new List<ViewItem>();

        public ViewItemSearchListBoxItem()
        {
            ((ILockCollectionHelper)((ICollectionItem)this).BaseItems).SetLocked(false);
            this.m_SearchBoxItem = new SearchBoxItem(this);
            this.m_SearchBoxItem.eGlyphStyle = GlyphStyle.eCross;
            this.m_SearchBoxItem.ButtonPadding = new System.Windows.Forms.Padding(5);
            this.m_SearchBoxItem.ButtonClick += new EventHandler(SearchBoxItem_ButtonClick);
            ((ICollectionItem)this).BaseItems.Add(this.m_SearchBoxItem);
            this.m_SearchBoxItem.KeyUp += new System.Windows.Forms.KeyEventHandler(SearchBoxItem_KeyUp);
            IInputRegion pInputRegion = ((IInputObjectHelper)this.m_SearchBoxItem).GetInputRegion();
            ((ILockCollectionHelper)((ICollectionItem)this).BaseItems).SetLocked(true);
        }
        void SearchBoxItem_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            this.UpdateViewItems();
        }
        void SearchBoxItem_ButtonClick(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(this.m_SearchBoxItem.Text)) return;
            this.m_SearchBoxItem.Text = "";
            this.UpdateViewItems();
        }

        #region IViewItemSearchListBox
        [Browsable(true), DefaultValue(true), Description("是否显示搜索框"), Category("布局")]
        public bool ShowSearchBox
        {
            get { return this.m_SearchBoxItem.Visible; }
            set { this.m_SearchBoxItem.Visible = value; }
        }

        private bool m_SearchBoxIsTop = true;
        [Browsable(true), DefaultValue(true), Description("顶部搜索框"), Category("布局")]
        public bool SearchBoxIsTop
        {
            get { return m_SearchBoxIsTop; }
            set { m_SearchBoxIsTop = value; }
        }

        [Browsable(false), Description("搜索框展现矩形"), Category("布局")]
        public Rectangle SearchBoxRectangle
        {
            get
            {
                Rectangle rectangle = base.AreaRectangle;
                return this.SearchBoxIsTop ? Rectangle.FromLTRB(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Top + CONST_SEACHBOXSIZE) : Rectangle.FromLTRB(rectangle.Left, rectangle.Bottom - CONST_SEACHBOXSIZE, rectangle.Right, rectangle.Bottom);
            }
        }

        [Browsable(true),
        Editor(typeof(GISShare.Controls.WinForm.WFNew.View.Design.ViewItemCollectionEditer), typeof(System.Drawing.Design.UITypeEditor)),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
        Description("其所携带的子项集合（变更后需要执行更新函数）"),
        Category("子项")]
        public new List<View.ViewItem> ViewItems
        {
            get { return this.m_ViewItemList; }
        }

        public void UpdateViewItems()
        {
            base.ViewItems.Clear();
            foreach (View.ViewItem one in this.m_ViewItemList)
            {
                if (String.IsNullOrEmpty(this.m_SearchBoxItem.Text) || one.Text.Contains(this.m_SearchBoxItem.Text))
                {
                    base.ViewItems.Add2(one);
                }
            }
            this.Refresh();
        }
        #endregion
        
        public override Rectangle AreaRectangle
        {
            get
            {
                if (this.ShowSearchBox)
                {
                    Rectangle rectangle = base.AreaRectangle;
                    return this.SearchBoxIsTop ? Rectangle.FromLTRB(rectangle.Left, rectangle.Top + CONST_SEACHBOXSIZE + CONST_SEACHBOXSIZE_SPACE, rectangle.Right, rectangle.Bottom) : Rectangle.FromLTRB(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom - CONST_SEACHBOXSIZE_SPACE - CONST_SEACHBOXSIZE);
                }
                return base.AreaRectangle;
            }
        }

        public override Rectangle ItemsViewRectangle
        {
            get
            {
                if (this.ShowSearchBox)
                {
                    return base.AreaRectangle;
                }
                return base.ItemsViewRectangle;
            }
        }
                
        #region 属性
        /// <summary>
        /// 添加复选框
        /// </summary>
        /// <param name="bChecked">状态</param>
        /// <param name="strName">名称</param>
        /// <param name="strText">文本</param>
        /// <param name="objValue">属性</param>
        /// <param name="image">图片</param>
        /// <returns>返回复选框</returns>
        public new ICheckBoxItem AddCheckedItem(bool bChecked, string strName, string strText, object objValue, System.Drawing.Image image)
        {
            ICheckBoxItem pCheckBoxItem = null;
            if (image != null)
            {
                pCheckBoxItem = new ImageCheckBoxItem()
                {
                    Checked = bChecked,
                    Name = strName,
                    Text = strText,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Image = image,
                    ImageAlign = ContentAlignment.MiddleLeft,
                    eImageSizeStyle = ImageSizeStyle.eSystem,
                    eHAlignmentStyle = GISShare.Controls.WinForm.WFNew.HAlignmentStyle.eStretch,
                    eVAlignmentStyle = GISShare.Controls.WinForm.WFNew.VAlignmentStyle.eStretch,
                    Tag = objValue
                };
            }
            else
            {
                pCheckBoxItem = new CheckBoxItem()
                {
                    Checked = bChecked,
                    Name = strName,
                    Text = strText,
                    TextAlign = ContentAlignment.MiddleLeft,
                    eHAlignmentStyle = GISShare.Controls.WinForm.WFNew.HAlignmentStyle.eStretch,
                    eVAlignmentStyle = GISShare.Controls.WinForm.WFNew.VAlignmentStyle.eStretch,
                    Tag = objValue
                };
            }
            this.ViewItems.Add(new SuperViewItem() { Name = strName, Text = strText, BaseItemObject = (GISShare.Controls.WinForm.WFNew.BaseItem)pCheckBoxItem });
            return pCheckBoxItem;
        }

        /// <summary>
        /// 添加单选框
        /// </summary>
        /// <param name="bChecked">状态</param>
        /// <param name="strName">名称</param>
        /// <param name="strText">文本</param>
        /// <param name="objValue">属性</param>
        /// <param name="image">图片</param>
        /// <returns>返回单选框</returns>
        public new IRadioButtonItem AddRadioItem(bool bChecked, string strName, string strText, object objValue, System.Drawing.Image image)
        {
            IRadioButtonItem pRadioButtonItem = null;
            if (image != null)
            {
                pRadioButtonItem = new ImageRadioButtonItem
                {
                    Checked = bChecked,
                    Name = strName,
                    Text = strText,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Image = image,
                    ImageAlign = ContentAlignment.MiddleLeft,
                    eImageSizeStyle = ImageSizeStyle.eSystem,
                    eHAlignmentStyle = GISShare.Controls.WinForm.WFNew.HAlignmentStyle.eStretch,
                    eVAlignmentStyle = GISShare.Controls.WinForm.WFNew.VAlignmentStyle.eStretch,
                    Tag = objValue
                };
            }
            else
            {
                pRadioButtonItem = new RadioButtonItem()
                {
                    Checked = bChecked,
                    Name = strName,
                    Text = strText,
                    TextAlign = ContentAlignment.MiddleLeft,
                    eHAlignmentStyle = GISShare.Controls.WinForm.WFNew.HAlignmentStyle.eStretch,
                    eVAlignmentStyle = GISShare.Controls.WinForm.WFNew.VAlignmentStyle.eStretch,
                    Tag = objValue
                };
            }
            this.ViewItems.Add(new SuperViewItem() { Name = strName, Text = strText, BaseItemObject = (GISShare.Controls.WinForm.WFNew.BaseItem)pRadioButtonItem });
            return pRadioButtonItem;
        }

        /// <summary>
        /// 获取复选对象集合
        /// </summary>
        /// <param name="bChecked">状态</param>
        /// <returns></returns>
        public new IList<ICheckBoxItem> GetCheckedItems(bool bChecked)
        {
            IList<ICheckBoxItem> checkBoxItemList = new List<ICheckBoxItem>();
            foreach (GISShare.Controls.WinForm.WFNew.View.ViewItem one in this.ViewItems)
            {
                if (one is ISuperViewItem)
                {
                    ISuperViewItem pSuperViewItem = (ISuperViewItem)one;
                    if (pSuperViewItem.BaseItemObject is ICheckBoxItem)
                    {
                        ICheckBoxItem pCheckBoxItem = (ICheckBoxItem)pSuperViewItem.BaseItemObject;
                        if (pCheckBoxItem.Checked == bChecked)
                        {
                            checkBoxItemList.Add(pCheckBoxItem);
                        }
                    }
                }
            }
            return checkBoxItemList;
        }
        #endregion

        //
        //
        //

        public class SearchBoxItem : ButtonTextBoxItem
        {
            IViewItemSearchListBox m_pViewItemSearchListBox;
            public SearchBoxItem(IViewItemSearchListBox pViewItemSearchListBox)
            {
                this.m_pViewItemSearchListBox = pViewItemSearchListBox;

            }

            //#region IViewDepend
            //ViewDependStyle IViewDepend.eViewDependStyle
            //{
            //    get { return ViewDependStyle.eInOwnerItemsRectangle; }
            //}
            //#endregion

            public override Rectangle DisplayRectangle
            {
                get
                {
                    return this.m_pViewItemSearchListBox.SearchBoxRectangle;
                }
            }
        }

    }
}
