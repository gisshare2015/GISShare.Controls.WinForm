using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew.View
{
    public class ViewItemListBoxItem : BaseItem,
        ICollectionItem, ICollectionItem2, ICollectionItem3, IUICollectionItem,
        IScrollableObjectHelper,
        IViewList,
        IViewListEnumerator,
        IViewLayoutList,
        IViewItemList, IViewItemListEvent, IViewItemList2,
        IViewItemListBox, IViewItemListBoxEvent, IViewItemListBox2,
        IOwner, IBaseItemOwner,
        IViewItemOwner, IViewItemOwner2,
        IInputObject, IInputObjectHelper
    {
        private const int CONST_SCROLBARBARSIZE = 13;
        private const int CONST_VIEWRECTANGLESPACE = 1;
        //
        private const int CTR_INPUTREGIONOFFSETY = 1;
        private const int CTR_INPUTREGIONOFFSETWIDTH = 1;
        //
        private ViewItemCollection m_ViewItemCollection;
        private SelectedIndexCollection m_SelectedIndices;
        //
        private VScrollBarItem m_VScrollBarItem;
        private HScrollBarItem m_HScrollBarItem;
        private BaseItemCollection m_BaseItemCollection;
        //
        private IInputRegion m_InputRegion = null;

        public ViewItemListBoxItem()
        {
            base.AutoGetFocus = true;
            //base.BackColor = System.Drawing.SystemColors.Window;
            //
            this.m_BaseItemCollection = new BaseItemCollection(this);
            this.m_VScrollBarItem = new VScrollBarItem();
            this.m_BaseItemCollection.Add(this.m_VScrollBarItem);
            this.m_HScrollBarItem = new HScrollBarItem();
            this.m_BaseItemCollection.Add(this.m_HScrollBarItem);
            ((ILockCollectionHelper)this.m_BaseItemCollection).SetLocked(false);
            //
            this.m_ViewItemCollection = new ViewItemCollection(this);
            this.m_SelectedIndices = new SelectedIndexCollection(this);
            //
            this.m_InputRegion = new InputRegion(this);
            this.m_InputRegion.InputEnd += new CancelEventHandler(InputRegion_InputEnd);
            this.m_InputRegion.PopupClosed += new EventHandler(InputRegion_PopupClosed);
        }
        void InputRegion_InputEnd(object sender, CancelEventArgs e)
        {
            this.OnInputEnd(e);
        }
        void InputRegion_PopupClosed(object sender, EventArgs e)
        {
            this.OnViewItemEdited(new ViewItemEventArgs(this.m_InputRegion.Tag as IViewItem));
            //this.Refresh();
            if (this.m_KeyDownKeyValue == 16 || this.m_KeyDownKeyValue == 17)
            {
                this.m_KeyDownKeyValue = -1;
            }
        }

        protected override EventStateStyle GetEventStateSupplement(string strEventName)
        {
            switch (strEventName)
            {
                case "SelectedIndexChanged":
                    return this.SelectedIndexChanged != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                default:
                    break;
            }
            //
            return base.GetEventStateSupplement(strEventName);
        }

        protected override bool RelationEventSupplement(string strEventName, EventArgs e)
        {
            switch (strEventName)
            {
                case "SelectedIndexChanged":
                    if (this.SelectedIndexChanged != null) { this.SelectedIndexChanged(this, e as IntValueChangedEventArgs); }
                    return true;
                default:
                    break;
            }
            //
            return base.RelationEventSupplement(strEventName, e);
        }

        #region BaseItem
        public override bool LockHeight
        {
            get { return false; }
        }

        public override bool LockWith
        {
            get { return false; }
        }

        public override System.Drawing.Size MeasureSize(System.Drawing.Graphics g)
        {
            if (this.ViewItems.Count <= 0) return this.Size;
            //
            int iW = 0, iH = 0;
            System.Drawing.Size size;
            foreach (ViewItem one in this.ViewItems) 
            {
                size = one.MeasureSize(g);
                iW += size.Width;
                iH += size.Height;
            }
            return new System.Drawing.Size(iW, iH);
        }

        public override object Clone()
        {
            ViewItemListBoxItem baseItem = new ViewItemListBoxItem();
            
            return baseItem;
        }
        #endregion

        #region IBaseItemOwner
        [Browsable(false), Description("Items显示矩形框"), Category("布局")]
        public override Rectangle ItemsRectangle
        {
            get
            {
                Rectangle rectangle = this.DisplayRectangle;
                if (this.ShowOutLine) return Rectangle.FromLTRB(rectangle.Left + 1, rectangle.Top + 1, rectangle.Right - 1, rectangle.Bottom - 1);
                return rectangle;
            }
        }
        #endregion

        #region ICollectionItem
        [Browsable(false), Description("其所携带的子项集合中是否存在可见项（与此类无关）"), Category("状态")]
        bool ICollectionItem.HaveVisibleBaseItem
        {
            get
            {
                foreach (BaseItem one in ((ICollectionItem)this).BaseItems)
                {
                    if (one.Visible) return true;
                }
                //
                return false;
            }
        }

        /// <summary>
        /// 一个零散的组建集合，它是锁定的无法移除和添加，没有需要请不要修改内部成员属性以防出现意外情况
        /// </summary>
        [Browsable(false), Description("其携带的子项（一个零散的组建集合，它是锁定的无法移除和添加，没有需要请不要修改内部成员属性以防出现意外情况）"), Category("子项")]
        BaseItemCollection ICollectionItem.BaseItems
        {
            get { return m_BaseItemCollection; }
        }
        #endregion

        #region ICollectionItem2
        IBaseItem WFNew.ICollectionItem2.GetBaseItem(string strName)
        {
            WFNew.IBaseItem pBaseItem = null;
            foreach (WFNew.IBaseItem one in ((WFNew.ICollectionItem)this).BaseItems)
            {
                if (one.Name == strName) pBaseItem = one;
                else
                {
                    WFNew.ICollectionItem2 pCollectionItem2 = one as WFNew.ICollectionItem2;
                    if (pCollectionItem2 != null)
                    {
                        pBaseItem = pCollectionItem2.GetBaseItem(strName);
                    }
                }
                //
                if (pBaseItem != null) break;
            }
            //
            return pBaseItem;
        }
        #endregion

        #region ICollectionItem3
        IBaseItem WFNew.ICollectionItem3.GetBaseItem2(string strName)
        {
            return null;
        }
        #endregion

        #region IUICollectionItem
        Size IUICollectionItem.GetIdealSize(Graphics g)
        {
            return this.Size;
        }
        #endregion

        #region IScrollableObjectHelper
        [Browsable(false), Description("水平滚动条最小值"), Category("外观")]
        public int HScrollBarMinimum
        {
            get { return 0; }
        }

        int m_HScrollBarMaximum = 0;
        [Browsable(false), Description("水平滚动条最大值"), Category("外观")]
        public int HScrollBarMaximum
        {
            get { return this.m_HScrollBarMaximum; }
        }

        private bool m_HScrollBarVisible = false;//不要对其直接赋值
        [Browsable(false), Description("水平滚动条是否可见"), Category("外观")]
        public bool HScrollBarVisible
        {
            get
            {
                return this.m_HScrollBarVisible;
            }
        }

        [Browsable(false), Description("水平滚动条显示矩形框"), Category("布局")]
        Rectangle IScrollableObjectHelper.HScrollBarDisplayRectangle
        {
            get
            {
                Rectangle rectangle = this.ItemsRectangle;
                return Rectangle.FromLTRB
                    (
                    rectangle.Left,
                    rectangle.Bottom - (this.HScrollBarVisible ? CONST_SCROLBARBARSIZE : 0),
                    rectangle.Right - (this.VScrollBarVisible ? CONST_SCROLBARBARSIZE : 0),
                    rectangle.Bottom
                    );
            }
        }

        //

        [Browsable(false), Description("竖直滚动条最小值"), Category("外观")]
        public int VScrollBarMinimum
        {
            get { return 0; }
        }

        private int m_VScrollBarMaximum = 0;
        [Browsable(false), Description("竖直滚动条最大值"), Category("外观")]
        public int VScrollBarMaximum
        {
            get { return this.m_VScrollBarMaximum; }
        }

        private bool m_VScrollBarVisible = false;//不要对其直接赋值
        [Browsable(false), Description("竖直滚动条是否可见"), Category("外观")]
        public bool VScrollBarVisible
        {
            get
            {
                return this.m_VScrollBarVisible;
            }
        }

        [Browsable(false), Description("竖直滚动条显示矩形框"), Category("布局")]
        Rectangle IScrollableObjectHelper.VScrollBarDisplayRectangle
        {
            get
            {
                Rectangle rectangle = this.ItemsRectangle;
                return Rectangle.FromLTRB
                    (
                    rectangle.Right - (this.VScrollBarVisible ? CONST_SCROLBARBARSIZE : 0),
                    rectangle.Top,
                    rectangle.Right,
                    rectangle.Bottom - (this.HScrollBarVisible ? CONST_SCROLBARBARSIZE : 0)
                    );
            }
        }

        //

        void IScrollableObjectHelper.ScrollValueRefresh()
        {
            this.Invalidate(this.DisplayRectangle);
        }
        #endregion

        #region IInputObject
        [Browsable(false), Description("输入框文本"), Category("外观")]
        string IInputObject.InputText
        {
            get
            {
                IInputObject pInputObject = this.SelectedItem as IInputObject;
                if (pInputObject == null) return this.Text;
                return pInputObject.InputText;
            }
            set
            {
                IInputObject pInputObject = this.SelectedItem as IInputObject;
                if (pInputObject == null) this.Text = value;
                pInputObject.InputText = value;
            }
        }

        [Browsable(false), Description("输入框文本字体"), Category("外观")]
        Font IInputObject.InputFont
        {
            get
            {
                IInputObject pInputObject = this.SelectedItem as IInputObject;
                if (pInputObject == null) return this.Font;
                return pInputObject.InputFont;
            }
        }

        [Browsable(false), Description("输入框文本颜色"), Category("外观")]
        Color IInputObject.InputForeColor
        {
            get
            {
                IInputObject pInputObject = this.SelectedItem as IInputObject;
                if (pInputObject == null) return this.ForeColor;
                return pInputObject.InputForeColor;
            }
        }

        [Browsable(false), Description("输入区矩形框（屏幕坐标）"), Category("布局")]
        Rectangle IInputObject.InputRegionRectangle
        {
            get
            {
                IInputObject pInputObject = this.SelectedItem as IInputObject;
                if (pInputObject == null) return Rectangle.Empty;
                //
                Rectangle rectangle = Rectangle.Intersect(this.ViewItemsRectangle, pInputObject.InputRegionRectangle);
                int iH = this.m_InputRegion.GetInputRegionSize().Height;
                return new Rectangle
                    (
                    this.PointToScreen(new Point(rectangle.Left, (rectangle.Top + rectangle.Bottom - iH) / 2 + CTR_INPUTREGIONOFFSETY)),
                    new Size(rectangle.Width - CTR_INPUTREGIONOFFSETWIDTH, iH)
                    );
                //return new Rectangle
                //    (
                //    this.PointToScreen(new Point(rectangle.Left, (rectangle.Top + rectangle.Bottom - iH) / 2)),
                //    new Size(rectangle.Width - CTR_INPUTREGIONOFFSETWIDTH, iH)
                //    );
            }
        }

        [Browsable(false), Description("是否正在输入"), Category("状态")]
        public bool IsInputing
        {
            get { return this.m_InputRegion.IsOpened; }
        }
        #endregion

        #region IInputObjectHelper
        IInputRegion IInputObjectHelper.GetInputRegion()
        {
            return this.m_InputRegion;
        }
        #endregion

        #region IViewList
        WFNew.IFlexibleList IViewList.List
        {
            get { return this.ViewItems; }
        }
        #endregion

        #region IViewListEnumerator
        IViewItem IViewListEnumerator.GetViewItem(int index) { return this.ViewItems[index]; }

        int IViewListEnumerator.GetViewItemCount() { return this.ViewItems.Count; }
        #endregion

        #region IViewLayoutList
        [Browsable(false), Description("顶部ViewItem索引号"), Category("布局")]
        public int TopViewItemIndex
        {
            get { return this.m_VScrollBarItem.GetEffectiveValue(); }
        }

        private int m_BottomViewItemIndex = 0;
        [Browsable(false), Description("底部部ViewItem索引号"), Category("布局")]
        public int BottomViewItemIndex
        {
            get { return m_BottomViewItemIndex; }
        }

        public ViewItem TryGetViewItemFromPoint(Point point)
        {
            ViewItem viewItem;
            for (int i = this.TopViewItemIndex; i <= this.BottomViewItemIndex; i++)
            {
                viewItem = this.ViewItems[i];
                if (viewItem != null && viewItem.DisplayRectangle.Contains(point)) return viewItem;
            }
            //
            return null;
        }
        #endregion

        #region IViewItemOwner
        [Browsable(false), Description("ViewItems显示矩形框"), Category("布局")]
        public Rectangle ViewItemsRectangle
        {
            get
            {
                Rectangle rectangle = this.ItemsRectangle;
                return Rectangle.FromLTRB
                    (
                    rectangle.Left,
                    rectangle.Top,
                    rectangle.Right - (this.VScrollBarVisible ? CONST_SCROLBARBARSIZE : 0) - 1,
                    rectangle.Bottom - (this.HScrollBarVisible ? CONST_SCROLBARBARSIZE : 0) - 1
                    );
            }
        }
        #endregion

        #region IViewItemOwner2
        IViewItemOwner2 IViewItemOwner2.GetTopViewItemOwner()
        {
            return this;
        }
        #endregion

        #region IViewItemList
        private bool m_ShowOutLine = true;
        [Browsable(true), DefaultValue(true), Description("显示外框线"), Category("外观")]
        public virtual bool ShowOutLine
        {
            get { return m_ShowOutLine; }
            set { m_ShowOutLine = value; }
        }

        private Color m_BackColor = System.Drawing.SystemColors.Window;
        [Browsable(true), Description("背景色"), Category("外观")]
        public Color BackColor
        {
            get { return m_BackColor; }
            set { m_BackColor = value; }
        }

        [Browsable(false), Description("左侧偏移量"), Category("布局")]
        public int LeftOffset
        {
            get { return this.m_HScrollBarItem.GetEffectiveValue(); }
        }

        [Browsable(false), Description("框架矩形框"), Category("布局")]
        public Rectangle FrameRectangle
        {
            get
            {
                Rectangle rectangle = this.DisplayRectangle;
                return new Rectangle(rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
            }
        }
        #endregion

        #region IViewItemList2
        /// <summary>
        /// 获取输入状态的文本（结合InputEnd事件用）
        /// </summary>
        /// <returns></returns>
        public string TryGetInputingText()
        {
            return this.m_InputRegion.TryGetInputingText();
        }

        /// <summary>
        /// 尝试获取焦点对象
        /// </summary>
        /// <returns></returns>
        public ViewItem TryGetFocusViewItem()
        {
            ViewItem viewItem = this.SelectedItem;
            //
            if (viewItem is IRowViewItem)
            {
                IRowViewItem pRowViewItem = (IRowViewItem)viewItem;
                if (pRowViewItem.List.Count > 0)
                {
                    ViewItem viewItem2 = pRowViewItem.List[pRowViewItem.SelectedIndex] as ViewItem;
                    return viewItem2 != null ? viewItem2 : viewItem;
                }
                else
                {
                    return viewItem;
                }
            }
            else
            {
                return viewItem;
            }
        }
        #endregion

        #region IViewItemListEvent
        [Browsable(true), Description("编辑结束后触发（InputingFilterText == false 时有效）"), Category("属性已更改")]
        public event CancelEventHandler InputEnd;

        [Browsable(true), Description("ViewItem编辑结束后触发"), Category("属性已更改")]
        public event ViewItemEventHandler ViewItemEdited;
        #endregion

        #region IViewItemListBox
        bool m_CanEdit = false;
        [Browsable(true), DefaultValue(false), Description("是否可以编辑"), Category("状态")]
        public virtual bool CanEdit
        {
            get { return m_CanEdit; }
            set { m_CanEdit = value; }
        }

        [Browsable(false), DefaultValue(-1), Description("选择ViewItem索引"), Category("属性")]
        public int SelectedIndex
        {
            get
            {
                //return this.SelectedIndices.Count > 0 ? this.SelectedIndices[this.SelectedIndices.Count - 1] : -1;
                if (this.SelectedIndices.Count > 0)
                {
                    return this.SelectedIndices[this.SelectedIndices.Count - 1];
                }
                else
                {
                    this.SelectedIndices.Clear();
                    return -1;
                }
            }
            set
            {
                if (this.MultipleSelect)
                {
                    if (this.m_KeyDownKeyValue == 16)
                    {
                        if (this.SelectedIndices.Count > 0)
                        {
                            int iIndex = this.SelectedIndices[this.SelectedIndices.Count - 1];
                            this.SelectedIndices.Clear();
                            if (value >= iIndex)
                            {
                                for (int i = iIndex; i <= value; i++)
                                {
                                    this.SelectedIndices.Add(i);
                                }
                            }
                            else
                            {
                                for (int i = value; i <= iIndex; i++)
                                {
                                    this.SelectedIndices.Add(i);
                                }
                            }
                        }
                        else
                        {
                            this.SelectedIndices.Add(value);
                        }
                    }
                    else if (this.m_KeyDownKeyValue == 17)
                    {
                        if (this.SelectedIndices.Contains(value))
                        {
                            this.SelectedIndices.Remove(value);
                        }
                        else
                        {
                            this.SelectedIndices.Add(value);
                        }
                    }
                    else
                    {
                        //if (!this.SelectedIndices.Contains(value))
                        //{
                        //    this.SelectedIndices.Clear();
                        //    this.SelectedIndices.Add(value);
                        //}
                        this.SelectedIndices.Besides(value);
                    }
                }
                else
                {
                    int iSelectedIndex = this.SelectedIndex;
                    //
                    if (iSelectedIndex == value) return;
                    //if (!this.SelectedIndices.Contains(value))
                    //{
                    //    this.SelectedIndices.Clear();
                    //    this.SelectedIndices.Add(value);
                    //}
                    this.SelectedIndices.Besides(value);
                    //
                    OnSelectedIndexChanged(new IntValueChangedEventArgs(iSelectedIndex, value));
                    if (value >= this.TopViewItemIndex && value <= this.m_BottomViewItemIndex) return;
                    //
                    if (this.VScrollBarMaximum > 0)
                    {
                        this.m_VScrollBarItem.Value = value;
                    }
                }
            }
        }

        private bool m_MultipleSelect = false;
        [Browsable(true), DefaultValue(false), Description("是否支持多选"), Category("属性")]
        public bool MultipleSelect
        {
            get { return m_MultipleSelect; }
            set { m_MultipleSelect = value; }
        }

        private bool m_ShowHScrollBar = false;
        [Browsable(true), DefaultValue(false), Description("是否显示水平滚动条"), Category("属性")]
        public bool ShowHScrollBar
        {
            get { return m_ShowHScrollBar; }
            set { m_ShowHScrollBar = value; }
        }
        #endregion

        #region IViewItemListBoxEvent
        [Browsable(true), Description("选择索引改变后触发"), Category("属性已更改")]
        public event IntValueChangedHandler SelectedIndexChanged;
        #endregion

        #region IViewItemListBox2
        [Browsable(false), Description("选择ViewItem"), Category("属性")]
        public ViewItem SelectedItem
        {
            get
            {
                return this.ViewItems[this.SelectedIndex];
                //return (this.SelectedIndex >= 0 && this.SelectedIndex < this.ViewItems.Count) ? this.ViewItems[this.SelectedIndex] : null;
            }
        }

        [Browsable(true),
        Editor(typeof(GISShare.Controls.WinForm.WFNew.View.Design.ViewItemCollectionEditer), typeof(System.Drawing.Design.UITypeEditor)),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
        Description("其所携带的子项集合"),
        Category("子项")]
        public ViewItemCollection ViewItems
        {
            get { return this.m_ViewItemCollection; }
        }
        #endregion

        #region 属性
        [Browsable(false), Description("多选项索引集合"), Category("属性")]
        public SelectedIndexCollection SelectedIndices
        {
            get { return m_SelectedIndices; }
        }

        /// <summary>
        /// 添加复选框
        /// </summary>
        /// <param name="bChecked">状态</param>
        /// <param name="strName">名称</param>
        /// <param name="strText">文本</param>
        /// <param name="objValue">属性</param>
        /// <param name="image">图片</param>
        /// <returns>返回复选框</returns>
        public ICheckBoxItem AddCheckedItem(bool bChecked, string strName, string strText, object objValue, System.Drawing.Image image)
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
        public IRadioButtonItem AddRadioItem(bool bChecked, string strName, string strText, object objValue, System.Drawing.Image image)
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
        public IList<ICheckBoxItem> GetCheckedItems(bool bChecked)
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

        //public SuperViewItem AddRadioButtonViewItem(string strName, string strText)
        //{
        //    RadioButtonItem baseItem = new RadioButtonItem(strName, strText);
        //    baseItem.eHAlignmentStyle = HAlignmentStyle.eStretch;
        //    baseItem.eVAlignmentStyle = VAlignmentStyle.eStretch;
        //    SuperViewItem superViewItem = new SuperViewItem() { BaseItemObject = baseItem, Height = 21 };
        //    this.ViewItems.Add(superViewItem);
        //    //
        //    return superViewItem;
        //}

        //public SuperViewItem AddCheckBoxItemViewItem(string strName, string strText)
        //{
        //    CheckBoxItem baseItem = new CheckBoxItem(strName, strText);
        //    baseItem.eHAlignmentStyle = HAlignmentStyle.eStretch;
        //    baseItem.eVAlignmentStyle = VAlignmentStyle.eStretch;
        //    SuperViewItem superViewItem = new SuperViewItem() { BaseItemObject = baseItem, Height = 21 };
        //    this.ViewItems.Add(superViewItem);
        //    //
        //    return superViewItem;
        //}

        #region 修改消息链条
        protected override void MessageMonitor(MessageInfo messageInfo)
        {
            switch (messageInfo.eMessageStyle)
            {
                case MessageStyle.eMSViewInfo:
                    this.MSViewInfo(messageInfo);
                    break;
                case MessageStyle.eMSPaint:
                    this.MSPaint(messageInfo);
                    break;
                //
                case MessageStyle.eMSLostFocus:
                    this.MSLostFocus(messageInfo);
                    break;
                case MessageStyle.eMSKeyDown:
                    this.MSKeyDown(messageInfo);
                    break;
                case MessageStyle.eMSKeyUp:
                    this.MSKeyUp(messageInfo);
                    break;
                case MessageStyle.eMSKeyPress:
                    this.MSKeyPress(messageInfo);
                    break;
                case MessageStyle.eMSMouseWheel:
                    this.MSMouseWheel(messageInfo);
                    break;
                //
                case MessageStyle.eMSMouseDown:
                    this.MSMouseDown(messageInfo);
                    break;
                case MessageStyle.eMSMouseUp:
                    this.MSMouseUp(messageInfo);
                    break;
                case MessageStyle.eMSMouseMove:
                    this.MSMouseMove(messageInfo);
                    break;
                case MessageStyle.eMSMouseClick:
                    this.MSMouseClick(messageInfo);
                    break;
                case MessageStyle.eMSMouseDoubleClick:
                    this.MSMouseDoubleClick(messageInfo);
                    break;
                case MessageStyle.eMSMouseEnter:
                    this.MSMouseEnter(messageInfo);
                    break;
                case MessageStyle.eMSMouseLeave:
                    this.MSMouseLeave(messageInfo);
                    break;
                //
                case MessageStyle.eMSEnabledChanged:
                    this.MSEnabledChanged(messageInfo);
                    break;
                case MessageStyle.eMSVisibleChanged:
                    this.MSVisibleChanged(messageInfo);
                    break;
                default:
                    base.MessageMonitor(messageInfo);
                    break;
            }
            //
            BaseItem baseItem;
            for (int i = 0; i < this.m_BaseItemCollection.Count; i++)
            {
                baseItem = this.m_BaseItemCollection[i];
                if (baseItem.pOwner != this) continue;
                //
                IMessageChain pMessageChain = baseItem as IMessageChain;
                if (pMessageChain != null)
                {
                    pMessageChain.SendMessage(messageInfo);
                }
            }
        }
        private void MSViewInfo(MessageInfo messageInfo)
        {

        }
        private void MSPaint(MessageInfo messageInfo)
        {
            PaintEventArgs e = messageInfo.MessageParameter as PaintEventArgs;
            if (e == null) return;
            //
            #region 绘制子项
            Rectangle rectangle = this.ViewItemsRectangle;
            rectangle = Rectangle.FromLTRB(rectangle.Left + CONST_VIEWRECTANGLESPACE, rectangle.Top + CONST_VIEWRECTANGLESPACE, rectangle.Right, rectangle.Bottom);
            //
            int iLeftOffset = this.LeftOffset;
            //
            int iCount = GISShare.Controls.WinForm.WFNew.LayoutEngine.LayoutStackV_ListBox
                (e.Graphics,
                this,
                rectangle,
                this.TopViewItemIndex,
                iLeftOffset,
                this.m_HScrollBarMaximum - iLeftOffset,
                ref this.m_BottomViewItemIndex,
                ref this.m_HScrollBarMaximum
                );
            //
            rectangle = Rectangle.Intersect(rectangle, e.ClipRectangle);//Key
            //
            this.m_VScrollBarMaximum = iCount - (this.m_BottomViewItemIndex - this.TopViewItemIndex);
            //
            this.SetHVScrollBarVisible(this.HScrollBarMaximum > this.HScrollBarMinimum, this.VScrollBarMaximum > this.VScrollBarMinimum);
            //
            if (this.m_BottomViewItemIndex < iCount) this.m_BottomViewItemIndex++;
            ViewItem viewItem;
            IMessageChain pMessageChain;
            for (int i = this.TopViewItemIndex; i < this.m_BottomViewItemIndex; i++)
            {
                viewItem = this.ViewItems[i];
                if (viewItem != null)
                {
                    pMessageChain = viewItem as IMessageChain;
                    if (pMessageChain != null)
                    {
                        Rectangle clipRectangle = Rectangle.Intersect(rectangle, viewItem.DisplayRectangle);
                        e.Graphics.SetClip(clipRectangle);
                        //Console.WriteLine(clipRectangle + " - " + e.Graphics.ClipBounds);
                        //pMessageChain.SendMessage(messageInfo);//new MessageInfo(this, MessageStyle.eMSPaint, new PaintEventArgs(e.Graphics, clipRectangle))
                        pMessageChain.SendMessage(new MessageInfo(this, MessageStyle.eMSPaint, new PaintEventArgs(e.Graphics, clipRectangle)));//messageInfo
                    }
                }
            }
            #region 已抛弃（布局移植到LayoutEngine）
            //Rectangle rectangle = this.ViewItemsRectangle;
            //rectangle = Rectangle.FromLTRB(rectangle.Left + CONST_VIEWRECTANGLESPACE, rectangle.Top + CONST_VIEWRECTANGLESPACE, rectangle.Right, rectangle.Bottom);
            ////
            //int iLeftOffset = this.LeftOffset;
            //int iRightOffset = this.HScrollBarMaximum - iLeftOffset;
            //if (iRightOffset == 0) iRightOffset = 0;
            ////
            //this.m_HScrollBarMaximum = 0;
            ////
            //int iW_W;
            //ViewItem viewItem = null;
            //Size viewItemSize;
            //int iItemHeight = 0;
            //int iNum = this.TopViewItemIndex;
            //int iCount = this.ViewItems.Count;
            //ISetViewItemHelper pSetViewItemHelper;
            //IMessageChain pMessageChain;
            //for (; iNum < iCount; iNum++)
            //{
            //    viewItem = this.ViewItems[iNum];
            //    if (viewItem != null)
            //    {
            //        viewItemSize = viewItem.MeasureSize(e.Graphics);
            //        iW_W = viewItemSize.Width - rectangle.Width;
            //        if (iW_W > this.m_HScrollBarMaximum) this.m_HScrollBarMaximum = iW_W;
            //        //
            //        pSetViewItemHelper = viewItem as ISetViewItemHelper;
            //        if (pSetViewItemHelper != null)
            //        {
            //            pSetViewItemHelper.SetViewItemDisplayRectangle
            //                (
            //                Rectangle.FromLTRB
            //                   (
            //                   rectangle.Left - iLeftOffset,
            //                   rectangle.Top + iItemHeight,
            //                   rectangle.Right + iRightOffset,
            //                   rectangle.Top + iItemHeight + viewItemSize.Height
            //                   )
            //                );
            //        }
            //        //
            //        iItemHeight += viewItem.DisplayRectangle.Height;
            //        if (iItemHeight >= rectangle.Height) break;
            //    }
            //}
            //for (int i = iNum; i < iCount; i++)
            //{
            //    viewItem = this.ViewItems[i];
            //    if (viewItem != null)
            //    {
            //        viewItemSize = viewItem.MeasureSize(e.Graphics);
            //        iW_W = viewItemSize.Width - rectangle.Width;
            //        if (iW_W > this.m_HScrollBarMaximum) this.m_HScrollBarMaximum = iW_W;
            //    }
            //}
            //this.m_VScrollBarMaximum = iCount - (iNum - this.TopViewItemIndex);
            ////
            //this.SetHVScrollBarVisible(this.HScrollBarMaximum > this.HScrollBarMinimum, this.VScrollBarMaximum > this.VScrollBarMinimum);
            ////
            //if (iNum < iCount) iNum++;
            //for (int i = this.TopViewItemIndex; i < iNum; i++)
            //{
            //    viewItem = this.ViewItems[i];
            //    if (viewItem != null)
            //    {
            //        pMessageChain = viewItem as IMessageChain;
            //        if (pMessageChain != null)
            //        {
            //            Rectangle clipRectangle = Rectangle.Intersect(rectangle, viewItem.DisplayRectangle);
            //            e.Graphics.SetClip(clipRectangle);
            //            MessageInfo messageInfo = new MessageInfo(this, MessageStyle.eMSPaint, new PaintEventArgs(e.Graphics, clipRectangle));
            //            pMessageChain.SendMessage(messageInfo);
            //        }
            //    }
            //}
            ////
            //this.m_BottomViewItemIndex = iNum;
            #endregion
            #endregion
            //
            e.Graphics.SetClip(e.ClipRectangle);
        }
        private void SetHVScrollBarVisible(bool bHScrollBarVisible, bool bVScrollBarVisible)
        {
            bool bH = this.m_HScrollBarVisible == (this.ShowHScrollBar && bHScrollBarVisible);
            bool bV = this.m_VScrollBarVisible == bVScrollBarVisible;
            if (bH && bV) return;
            if (!bH) this.m_HScrollBarVisible = this.ShowHScrollBar && bHScrollBarVisible;
            if (!bV) this.m_VScrollBarVisible = bVScrollBarVisible;
            this.Invalidate(this.ItemsRectangle);
        }
        private void MSLostFocus(MessageInfo messageInfo)
        {
            ViewItem viewItem = this.SelectedItem;
            if (viewItem != null)
            {
                IMessageChain pMessageChain = viewItem as IMessageChain;
                if (pMessageChain != null)
                {
                    pMessageChain.SendMessage(messageInfo);
                }
            }
        }
        private int m_KeyDownKeyValue = -1;
        private void MSKeyDown(MessageInfo messageInfo)
        {
            KeyEventArgs e = messageInfo.MessageParameter as KeyEventArgs;
            if (e == null) return;
            //
            ViewItem viewItem = this.SelectedItem;
            if (viewItem != null)
            {
                IMessageChain pMessageChain = viewItem as IMessageChain;
                if (pMessageChain != null)
                {
                    pMessageChain.SendMessage(messageInfo);
                }
            }
            //
            if (this.MultipleSelect)
            {
                this.m_KeyDownKeyValue = e.KeyValue;
            }
        }
        private void MSKeyUp(MessageInfo messageInfo)
        {
            ViewItem viewItem = this.SelectedItem;
            if (viewItem != null)
            {
                IMessageChain pMessageChain = viewItem as IMessageChain;
                if (pMessageChain != null)
                {
                    pMessageChain.SendMessage(messageInfo);
                }
            }
            //
            this.m_KeyDownKeyValue = -1;
        }
        private void MSKeyPress(MessageInfo messageInfo)
        {
            ViewItem viewItem = this.SelectedItem;
            if (viewItem != null)
            {
                IMessageChain pMessageChain = viewItem as IMessageChain;
                if (pMessageChain != null)
                {
                    pMessageChain.SendMessage(messageInfo);
                }
            }
        }
        private void MSMouseWheel(MessageInfo messageInfo)
        {
            ViewItem viewItem = this.SelectedItem;
            if (viewItem != null)
            {
                IMessageChain pMessageChain = viewItem as IMessageChain;
                if (pMessageChain != null)
                {
                    pMessageChain.SendMessage(messageInfo);
                }
            }
        }
        private void MSMouseDown(MessageInfo messageInfo)
        {
            MouseEventArgs e = messageInfo.MessageParameter as MouseEventArgs;
            if (e == null) return;
            //
            if (this.ViewItemsRectangle.Contains(e.Location))
            {
                //Control ctr = this.TryGetDependControl();
                //if (ctr != null && !ctr.Focused) ctr.Focus();
                ////
                if (this.Enabled && this.CanEdit)
                {
                    ITextEditViewItem pTextEditViewItem = this.SelectedItem as ITextEditViewItem;
                    if (pTextEditViewItem != null &&
                        pTextEditViewItem.CanEdit &&
                        pTextEditViewItem.InputRegionRectangle.Contains(e.Location))
                    {
                        Rectangle rectangle = ((IInputObject)this).InputRegionRectangle;
                        if (rectangle.Width > 6 && rectangle.Height > 6)
                        {
                            this.m_InputRegion.Tag = pTextEditViewItem.EditObject;
                            if (this.m_InputRegion.Tag is IViewItem)
                            {
                                this.m_InputRegion.ShowInputRegion();
                                return;
                            }
                        }
                    }
                }
                //
                ViewItem viewItem = null;
                for (int i = this.TopViewItemIndex; i <= this.BottomViewItemIndex; i++)
                {
                    viewItem = this.ViewItems[i];
                    if (viewItem != null && viewItem.DisplayRectangle.Contains(e.Location))
                    {
                        IMessageChain pMessageChain = viewItem as IMessageChain;
                        if (pMessageChain != null)
                        {
                            pMessageChain.SendMessage(messageInfo);
                        }
                        //
                        this.SelectedIndex = i;
                        //
                        break;
                    }
                }
            }
        }
        private void MSMouseUp(MessageInfo messageInfo)
        {
            ViewItem viewItem = null;
            for (int i = this.TopViewItemIndex; i <= this.BottomViewItemIndex; i++)
            {
                viewItem = this.ViewItems[i];
                if (viewItem != null)
                {
                    IMessageChain pMessageChain = viewItem as IMessageChain;
                    if (pMessageChain != null)                    
                    {
                        pMessageChain.SendMessage(messageInfo);
                    }
                }
            }
        }
        private void MSMouseMove(MessageInfo messageInfo)
        {
            ViewItem viewItem = null;
            for (int i = this.TopViewItemIndex; i <= this.BottomViewItemIndex; i++)
            {
                viewItem = this.ViewItems[i];
                if (viewItem != null)
                {
                    IMessageChain pMessageChain = viewItem as IMessageChain;
                    if (pMessageChain != null)
                    {
                        pMessageChain.SendMessage(messageInfo);
                    }
                }
            }
        }
        private void MSMouseClick(MessageInfo messageInfo)
        {
            MouseEventArgs e = messageInfo.MessageParameter as MouseEventArgs;
            if (e == null) return;
            //
            if (this.ViewItemsRectangle.Contains(e.Location))
            {
                ViewItem viewItem = null;
                for (int i = this.TopViewItemIndex; i <= this.BottomViewItemIndex; i++)
                {
                    viewItem = this.ViewItems[i];
                    if (viewItem != null && viewItem.DisplayRectangle.Contains(e.Location))
                    {
                        IMessageChain pMessageChain = viewItem as IMessageChain;
                        if (pMessageChain != null)
                        {
                            pMessageChain.SendMessage(messageInfo);
                        }
                        //
                        break;
                    }
                }
            }
        }
        private void MSMouseDoubleClick(MessageInfo messageInfo)
        {
            MouseEventArgs e = messageInfo.MessageParameter as MouseEventArgs;
            if (e == null) return;
            //
            if (this.ViewItemsRectangle.Contains(e.Location))
            {
                ViewItem viewItem = null;
                for (int i = this.TopViewItemIndex; i <= this.BottomViewItemIndex; i++)
                {
                    viewItem = this.ViewItems[i];
                    if (viewItem != null && viewItem.DisplayRectangle.Contains(e.Location))
                    {
                        IMessageChain pMessageChain = viewItem as IMessageChain;
                        if (pMessageChain != null)
                        {
                            pMessageChain.SendMessage(messageInfo);
                        }
                        //
                        break;
                    }
                }
            }
        }
        private void MSMouseEnter(MessageInfo messageInfo)
        {

        }
        private void MSMouseLeave(MessageInfo messageInfo)
        {
            ViewItem viewItem = null;
            for (int i = this.TopViewItemIndex; i <= this.BottomViewItemIndex; i++)
            {
                viewItem = this.ViewItems[i];
                if (viewItem != null)
                {
                    IMessageChain pMessageChain = viewItem as IMessageChain;
                    if (pMessageChain != null)
                    {
                        pMessageChain.SendMessage(messageInfo);
                    }
                }
            }
        }
        private void MSEnabledChanged(MessageInfo messageInfo)
        {
            ViewItem viewItem = null;
            for (int i = this.TopViewItemIndex; i <= this.BottomViewItemIndex; i++)
            {
                viewItem = this.ViewItems[i];
                if (viewItem != null)
                {
                    IMessageChain pMessageChain = viewItem as IMessageChain;
                    if (pMessageChain != null)
                    {
                        pMessageChain.SendMessage(messageInfo);
                    }
                }
            }
        }
        private void MSVisibleChanged(MessageInfo messageInfo)
        {
            ViewItem viewItem = null;
            for (int i = this.TopViewItemIndex; i <= this.BottomViewItemIndex; i++)
            {
                viewItem = this.ViewItems[i];
                if (viewItem != null)
                {
                    IMessageChain pMessageChain = viewItem as IMessageChain;
                    if (pMessageChain != null)
                    {
                        pMessageChain.SendMessage(messageInfo);
                    }
                }
            }
        }
        #endregion

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (this.m_VScrollBarVisible &&
                this.m_VScrollBarMaximum > this.VScrollBarMinimum &&
                this.m_VScrollBarItem.Value >= this.VScrollBarMinimum &&
                this.m_VScrollBarItem.Value <= this.m_VScrollBarMaximum)
            {
                if (e.Delta < 0) this.m_VScrollBarItem.Value++;
                else if (e.Delta > 0) this.m_VScrollBarItem.Value--;
            }
            else if (this.m_HScrollBarVisible &&
                this.m_HScrollBarMaximum > this.HScrollBarMinimum &&
                this.m_HScrollBarItem.Value >= this.HScrollBarMinimum &&
                this.m_HScrollBarItem.Value <= this.m_HScrollBarMaximum)
            {
                if (e.Delta < 0) this.m_HScrollBarItem.Value++;
                else if (e.Delta > 0) this.m_HScrollBarItem.Value--;
            }
            //
            base.OnMouseWheel(e);
        }

        protected override void OnDraw(PaintEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderViewItemList(
                new ObjectRenderEventArgs(e.Graphics, this, this.DisplayRectangle));
        }
        
        //
        protected virtual void OnSelectedIndexChanged(IntValueChangedEventArgs e)
        {
            if (this.SelectedIndexChanged != null) { this.SelectedIndexChanged(this, e); }
        }

        protected virtual void OnInputEnd(CancelEventArgs e)
        {
            if (this.InputEnd != null) { this.InputEnd(this, e); }
        }

        protected virtual void OnViewItemEdited(ViewItemEventArgs e)
        {
            if (this.ViewItemEdited != null) { this.ViewItemEdited(this, e); }
        }
    }
}
