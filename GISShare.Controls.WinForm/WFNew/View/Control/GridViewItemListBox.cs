using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;
using System.Data;
using System.Collections;

namespace GISShare.Controls.WinForm.WFNew.View
{
    [ToolboxItem(true)]
    public class GridViewItemListBox : BaseItemControl,
        ICollectionItem, ICollectionItem2, ICollectionItem3, IUICollectionItem,
        IScrollableObjectHelper,
        IViewList,
        IViewLayoutList,
        IOwner, IBaseItemOwner,
        IViewItemOwner, IViewItemOwner2,
        IInputObject, IInputObjectHelper,
        IColumnTitleViewObject,
        IColumnViewObject, IColumnViewObjectHelper,
        IRowViewObject,
        IViewItemListBox, IViewItemListBoxEvent,
        IGridViewItemListBox, IGridViewItemListBoxEvent
    {
        private const int CONST_MINROWHEADERWIDTH = 18;
        private const int CONST_MINROWHEADERHEIGHT = 18;
        private const int CONST_SCROLBARBARSIZE = 13;
        //
        private const int CTR_INPUTREGIONOFFSETY = 1;
        private const int CTR_INPUTREGIONOFFSETWIDTH = 1;
        //
        private const int CONST_SHOWSPLITELINEMINHEIGTH = 10;
        private const int CONST_RESIZERECTANGLESIZE = 6;
        private const int CONST_MINCOLUMNVIEWITEMWIDTH = 18;
        //
        private RowCellViewItemCollection m_ViewItemCollection;
        private SelectedIndexCollection m_SelectedIndices;
        //
        private VScrollBarItem m_VScrollBarItem;
        private HScrollBarItem m_HScrollBarItem;
        private BaseItemCollection m_BaseItemCollection;
        //
        private IInputRegion m_InputRegion = null;
        //
        private LockRowColumnTitleViewItem m_LockRowColumnTitleViewItem = null;
        private RowColumnViewItem m_RowColumnViewItem = null;
        //
        private int m_OperationStyle = -1;//0 = CanExchangeColumn, 1 = CanExchangeRow, 2 = CanResizeRowHeaderWidth, 3 = CanResizeRowHeaderHeight, 4 = CanResizeColumnWidth, 5 = CanResizeRowHeight, 6 = CanResizeColumnTitleHeight
        private Point m_MouseDownPoint = Point.Empty;
        private int m_MouseDownSelectedIndex = -1;
        private Cursor m_CursorDefault = Cursors.Default;
        private SplitLineForm m_SplitLineForm = null;
        //
        private int m_KeyDownKeyValue = -1;

        public GridViewItemListBox()
        {
            base.AutoGetFocus = true;
            base.BackColor = System.Drawing.SystemColors.Window;
            //
            this.m_BaseItemCollection = new BaseItemCollection(this);
            this.m_VScrollBarItem = new VScrollBarItem();
            this.m_BaseItemCollection.Add(this.m_VScrollBarItem);
            this.m_HScrollBarItem = new HScrollBarItem();
            this.m_BaseItemCollection.Add(this.m_HScrollBarItem);
            ((ILockCollectionHelper)this.m_BaseItemCollection).SetLocked(false);
            //
            this.m_ViewItemCollection = new RowCellViewItemCollection(this);
            this.m_SelectedIndices = new SelectedIndexCollection(this);
            //
            this.m_InputRegion = new InputRegion(this);
            this.m_InputRegion.InputEnd += new CancelEventHandler(InputRegion_InputEnd);
            this.m_InputRegion.PopupClosed += new EventHandler(InputRegion_PopupClosed);
            //
            this.m_LockRowColumnTitleViewItem = new LockRowColumnTitleViewItem();
            ((ISetOwnerHelper)this.m_LockRowColumnTitleViewItem).SetOwner(this);
            this.m_RowColumnViewItem = new RowColumnViewItem(this);
            ((ISetOwnerHelper)this.m_RowColumnViewItem).SetOwner(this);
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
                case "RowHeaderMouseClick":
                    return this.RowHeaderMouseClick != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "RowHeaderMouseDoubleClick":
                    return this.RowHeaderMouseDoubleClick != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "RowHeaderItemDrawing":
                    return this.RowHeaderItemDrawing != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
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
                case "RowHeaderMouseClick":
                    if (this.RowHeaderMouseClick != null) { this.RowHeaderMouseClick(this, e as MouseEventArgs); }
                    return true;
                case "RowHeaderMouseDoubleClick":
                    if (this.RowHeaderMouseDoubleClick != null) { this.RowHeaderMouseDoubleClick(this, e as MouseEventArgs); }
                    return true;
                case "RowHeaderItemDrawing":
                    if (this.RowHeaderItemDrawing != null) { this.RowHeaderItemDrawing(this, e as RowHeaderItemDrawEventArgs); }
                    return true;
                default:
                    break;
            }
            //
            return base.RelationEventSupplement(strEventName, e);
        }

        #region BaseItemControl
        public override bool LockHeight
        {
            get { return false; }
        }

        public override bool LockWith
        {
            get { return false; }
        }

        public override object Clone()
        {
            GridViewItemListBox baseItem = new GridViewItemListBox();

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
                IInputObject pInputObject = this.m_ViewItemCollection[this.SelectedIndex] as IInputObject;
                if (pInputObject == null) return this.Text;
                return pInputObject.InputText;
            }
            set
            {
                IInputObject pInputObject = this.m_ViewItemCollection[this.SelectedIndex] as IInputObject;
                if (pInputObject == null) this.Text = value;
                pInputObject.InputText = value;
            }
        }

        [Browsable(false), Description("输入框文本字体"), Category("外观")]
        Font IInputObject.InputFont
        {
            get
            {
                IInputObject pInputObject =  this.m_ViewItemCollection[this.SelectedIndex] as IInputObject;
                if (pInputObject == null) return this.Font;
                return pInputObject.InputFont;
            }
        }

        [Browsable(false), Description("输入框文本颜色"), Category("外观")]
        Color IInputObject.InputForeColor
        {
            get
            {
                IInputObject pInputObject =  this.m_ViewItemCollection[this.SelectedIndex] as IInputObject;
                if (pInputObject == null) return this.ForeColor;
                return pInputObject.InputForeColor;
            }
        }

        [Browsable(false), Description("输入区矩形框（屏幕坐标）"), Category("布局")]
        Rectangle IInputObject.InputRegionRectangle
        {
            get
            {
                IInputObject pInputObject =  this.m_ViewItemCollection[this.SelectedIndex] as IInputObject;
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
            get { return this.m_ViewItemCollection; }
        }
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
            RowCellViewItem viewItem;
            for (int i = this.TopViewItemIndex; i <= this.BottomViewItemIndex; i++)
            {
                viewItem = this.m_ViewItemCollection[i];
                if (viewItem != null && viewItem.DisplayRectangle.Contains(point))
                {
                    return viewItem.TryGetViewItemFromPoint(point);
                }
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
                    this.ShowRowHeader ? rectangle.Left + this.RowHeaderWidth : rectangle.Left,
                    rectangle.Top,
                    rectangle.Right - (this.VScrollBarVisible ? CONST_SCROLBARBARSIZE : 0) - 1,//
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
            RowCellViewItem viewItem =  this.m_ViewItemCollection[this.SelectedIndex];
            //
            if (viewItem.ViewItems.Count > 0)
            {
                ViewItem viewItem2 = viewItem.ViewItems[viewItem.SelectedIndex] as ViewItem;
                return viewItem2 != null ? viewItem2 : viewItem;
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

        [Browsable(false), DefaultValue(true), Description("是否显示水平滚动条"), Category("属性")]
        bool IViewItemListBox.ShowHScrollBar
        {
            get { return true; }
            set { }
        }
        #endregion

        #region IViewItemListBoxEvent
        [Browsable(true), Description("选择索引改变后触发"), Category("属性已更改")]
        public event IntValueChangedHandler SelectedIndexChanged;
        #endregion

        #region IGridViewItemListBox
        bool m_ShowRowHeaderID = false;
        [Browsable(true), DefaultValue(false), Description("显示行头ID"), Category("外观")]
        public bool ShowRowHeaderID
        {
            get { return m_ShowRowHeaderID; }
            set { m_ShowRowHeaderID = value; }
        }

        int m_RowHeaderStartID = 0;
        [Browsable(true), DefaultValue(0), Description("显示行头ID，起始编号"), Category("外观")]
        public int RowHeaderStartID
        {
            get { return m_RowHeaderStartID; }
            set { m_RowHeaderStartID = value; }
        }

        int m_RowHeaderWidth = 18;
        [Browsable(true), DefaultValue(18), Description("行头宽"), Category("外观")]
        public int RowHeaderWidth
        {
            get { return m_RowHeaderWidth; }
            set
            {
                if (value < CONST_MINROWHEADERWIDTH) value = CONST_MINROWHEADERWIDTH;
                if (value == m_RowHeaderWidth) return;
                m_RowHeaderWidth = value;
                this.Refresh();
            }
        }

        bool m_ShowRowHeader = true;
        [Browsable(true), DefaultValue(true), Description("显示行头"), Category("外观")]
        public bool ShowRowHeader
        {
            get { return m_ShowRowHeader; }
            set
            {
                if (value == m_ShowRowHeader) return;
                m_ShowRowHeader = value;
                this.Refresh();
            }
        }

        private bool m_CanResizeRowHeaderWidth = true;
        [Browsable(true), DefaultValue(true), Description("是否可以调节行头宽"), Category("状态")]
        public bool CanResizeRowHeaderWidth
        {
            get { return m_CanResizeRowHeaderWidth; }
            set { m_CanResizeRowHeaderWidth = value; }
        }

        private bool m_CanResizeRowHeaderHeight = true;
        [Browsable(true), DefaultValue(true), Description("是否可以调节行头高"), Category("状态")]
        public bool CanResizeRowHeaderHeight
        {
            get { return m_CanResizeRowHeaderHeight; }
            set { m_CanResizeRowHeaderHeight = value; }
        }

        [Browsable(false), Description("表头矩形框"), Category("布局")]
        public Rectangle RowHeaderRectangle
        {
            get
            {
                Rectangle rectangle = this.ItemsRectangle;
                return Rectangle.FromLTRB
                    (
                    rectangle.Left,
                    rectangle.Top,
                    this.ShowRowHeader ? rectangle.Left + this.RowHeaderWidth : rectangle.Left,
                    this.HScrollBarVisible ? rectangle.Bottom - CONST_SCROLBARBARSIZE : rectangle.Bottom
                    );
            }
        }

        [Browsable(false), DefaultValue(-1), Description("焦点列索引"), Category("属性")]
        public int FocusColumnIndex
        {
            get { return this.m_RowColumnViewItem.SelectedIndex; }
        }

        [Browsable(false), DefaultValue(-1), Description("焦点行索引"), Category("属性")]
        public int FocusRowIndex
        {
            get { return this.SelectedIndex; }
        }

        [Browsable(false), DefaultValue(-1), Description("总行数"), Category("属性")]
        public int RowCount { get { return this.m_ViewItemCollection.Count; } }

        /// <summary>
        /// 设置列宽
        /// </summary>
        /// <param name="index"></param>
        /// <param name="iColumnWidth"></param>
        public void SetColumnViewItemWidth(int index, int iColumnWidth)
        {
            ColumnViewItem columnViewItem = this.ColumnViewItems[index];
            if (columnViewItem == null || columnViewItem.Width == iColumnWidth) return;
            columnViewItem.Width = iColumnWidth;
            this.Refresh();
        }

        /// <summary>
        /// 添加行
        /// </summary>
        /// <param name="eRowCellViewStyle"></param>
        /// <param name="argsobj"></param>
        public void AddRowViewItem(RowCellViewStyle eRowCellViewStyle, params object[] argsobj)
        {
            this.AddRowViewItem(eRowCellViewStyle, this.DefaultRowHeight, argsobj);
        }

        /// <summary>
        /// 添加行
        /// </summary>
        /// <param name="eRowCellViewStyle"></param>
        /// <param name="iRowHeight"></param>
        /// <param name="argsobj"></param>
        public void AddRowViewItem(RowCellViewStyle eRowCellViewStyle, int iRowHeight, params object[] argsobj)
        {
            DataRowCellViewItem rowViewItem = new DataRowCellViewItem(eRowCellViewStyle, null);
            rowViewItem.Height = iRowHeight;
            if (argsobj == null || argsobj.Length <= 0)
            {
                switch (rowViewItem.eRowCellViewStyle)
                {
                    case RowCellViewStyle.eSystemRow:
                        for (int i = 0; i < this.ColumnViewItems.Count; i++)
                        {
                            CellViewItem cellViewItem = this.CreateCellViewItem(CellViewStyle.eSystem, iRowHeight, i);
                            cellViewItem.Name = this.ColumnViewItems[i].FieldName;
                            rowViewItem.ViewItems.Add(cellViewItem);
                        }
                        break;
                    case RowCellViewStyle.eSingleCellRow:
                        rowViewItem.ViewItems.Add(this.CreateCellViewItem(CellViewStyle.eSingleCell, iRowHeight, 0));
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (rowViewItem.eRowCellViewStyle)
                {
                    case RowCellViewStyle.eSystemRow:
                        {
                            for (int i = 0; i < this.ColumnViewItems.Count; i++)
                            {
                                CellViewItem cellViewItem = this.CreateCellViewItem(CellViewStyle.eSystem, iRowHeight, i);
                                cellViewItem.Name = this.ColumnViewItems[i].FieldName;
                                if (i < argsobj.Length)
                                {
                                    cellViewItem.Value = argsobj[i];
                                }
                                rowViewItem.ViewItems.Add(cellViewItem);
                            }
                        }
                        break;
                    case RowCellViewStyle.eSingleCellRow:
                        {
                            CellViewItem cellViewItem = this.CreateCellViewItem(CellViewStyle.eSingleCell, iRowHeight, 0);
                            if (0 < argsobj.Length) cellViewItem.Value = argsobj[0];
                            rowViewItem.ViewItems.Add(cellViewItem);
                        }
                        break;
                    default:
                        break;
                }
            }
            this.m_ViewItemCollection.Add(rowViewItem);
        }

        /// <summary>
        /// 插入行
        /// </summary>
        /// <param name="index"></param>
        /// <param name="eRowCellViewStyle"></param>
        /// <param name="argsobj"></param>
        public void InsertRowViewItem(int index, RowCellViewStyle eRowCellViewStyle, params object[] argsobj)
        {
            this.InsertRowViewItem(index, eRowCellViewStyle, this.DefaultRowHeight, argsobj);
        }

        /// <summary>
        /// 插入行
        /// </summary>
        /// <param name="index"></param>
        /// <param name="eRowCellViewStyle"></param>
        /// <param name="iRowHeight"></param>
        /// <param name="argsobj"></param>
        public void InsertRowViewItem(int index, RowCellViewStyle eRowCellViewStyle, int iRowHeight, params object[] argsobj)
        {
            DataRowCellViewItem rowViewItem = new DataRowCellViewItem(eRowCellViewStyle, null);
            if (argsobj == null || argsobj.Length <= 0)
            {
                switch (rowViewItem.eRowCellViewStyle)
                {
                    case RowCellViewStyle.eSystemRow:
                        for (int i = 0; i < this.ColumnViewItems.Count; i++)
                        {
                            CellViewItem cellViewItem = this.CreateCellViewItem(CellViewStyle.eSystem, iRowHeight, i);
                            cellViewItem.Name = this.ColumnViewItems[i].FieldName;
                            rowViewItem.ViewItems.Add(cellViewItem);
                        }
                        break;
                    case RowCellViewStyle.eSingleCellRow:
                        rowViewItem.ViewItems.Add(this.CreateCellViewItem(CellViewStyle.eSingleCell, iRowHeight, 0));
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (rowViewItem.eRowCellViewStyle)
                {
                    case RowCellViewStyle.eSystemRow:
                        {
                            for (int i = 0; i < this.ColumnViewItems.Count; i++)
                            {
                                CellViewItem cellViewItem = this.CreateCellViewItem(CellViewStyle.eSystem, iRowHeight, i);
                                cellViewItem.Name = this.ColumnViewItems[i].FieldName;
                                if (i < argsobj.Length)
                                {
                                    cellViewItem.Value = argsobj[i];
                                }
                                rowViewItem.ViewItems.Add(cellViewItem);
                            }
                        }
                        break;
                    case RowCellViewStyle.eSingleCellRow:
                        {
                            CellViewItem cellViewItem = this.CreateCellViewItem(CellViewStyle.eSingleCell, iRowHeight, 0);
                            if (0 < argsobj.Length) cellViewItem.Value = argsobj[0];
                            rowViewItem.ViewItems.Add(cellViewItem);
                        }
                        break;
                    default:
                        break;
                }
            }
            this.m_ViewItemCollection.Insert(index, rowViewItem);
        }

        /// <summary>
        /// 删除行
        /// </summary>
        /// <param name="index"></param>
        public void DeletetRowViewItem(int index)
        {
            this.m_ViewItemCollection.RemoveAt(index);
        }

        /// <summary>
        /// 清空行
        /// </summary>
        public void ClearRowViewItem()
        {
            this.m_ViewItemCollection.Clear();
        }

        /// <summary>
        /// 交换行
        /// </summary>
        /// <param name="index1"></param>
        /// <param name="index2"></param>
        /// <returns></returns>
        public bool ExchangeRowViewItem(int index1, int index2)
        {
            return this.m_ViewItemCollection.ExchangeItem(index1, index2);
        }

        /// <summary>
        /// 设置行高
        /// </summary>
        /// <param name="index"></param>
        /// <param name="iRowHeight"></param>
        public void SetRowViewItemHeight(int index, int iRowHeight)
        {
            RowCellViewItem viewItem = this.m_ViewItemCollection[index];
            if (viewItem == null && viewItem.Height == iRowHeight) return;
            viewItem.Height = iRowHeight;
            this.Refresh();
        }

        /// <summary>
        /// 给属性赋值
        /// </summary>
        /// <param name="iRowIndex">行索引</param>
        /// <param name="strPropertyName">属性名称（Name/Text/Height/Width/SelectedIndex/ForeColor/Font）</param>
        /// <param name="objValue">值</param>
        public void SetRowViewItem(int iRowIndex, string strPropertyName, object objValue)
        {
            RowCellViewItem viewItem = this.m_ViewItemCollection[iRowIndex];
            if (viewItem == null) return;
            //
            Type type = typeof(RowCellViewItem);
            if (type == null) return;
            PropertyInfo propertyInfo = type.GetProperty(strPropertyName);
            if (propertyInfo == null) return;
            propertyInfo.SetValue(viewItem, objValue, null);
        }

        /// <summary>
        /// 获取行高
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public int GetRowViewItemHeight(int index)
        {
            RowCellViewItem viewItem = this.m_ViewItemCollection[index];
            if (viewItem == null) return -1;
            return viewItem.Height;
        }

        /// <summary>
        /// 获取属性赋值
        /// </summary>
        /// <param name="iRowIndex">行索引</param>
        /// <param name="strPropertyName">属性名称（Name/Text/Height/Width/SelectedIndex/ForeColor/Font）</param>
        public object GetRowViewItem(int iRowIndex, string strPropertyName)
        {
            RowCellViewItem viewItem = this.m_ViewItemCollection[iRowIndex];
            if (viewItem == null) return null;
            //
            Type type = typeof(RowCellViewItem);
            if (type == null) return null;
            PropertyInfo propertyInfo = type.GetProperty(strPropertyName);
            if (propertyInfo == null) return null;
            return propertyInfo.GetValue(viewItem, null);
        }

        /// <summary>
        /// 获取单元
        /// </summary>
        /// <param name="iRow"></param>
        /// <param name="iColumn"></param>
        /// <returns></returns>
        public ICellViewItem GetCellViewItem(int iRow, int iColumn)
        {
            RowCellViewItem viewItem = this.m_ViewItemCollection[iRow];
            if (viewItem == null) return null;
            switch (viewItem.eRowCellViewStyle) 
            {
                case RowCellViewStyle.eSystemRow:
                    return viewItem.ViewItems[iColumn];
                case RowCellViewStyle.eSingleCellRow:
                    return viewItem.ViewItems[0];
                default:
                    return null;
            }
        }

        /// <summary>
        /// 获取焦点单元
        /// </summary>
        /// <returns></returns>
        public ICellViewItem GetFocusCellViewItem()
        {
            RowCellViewItem viewItem = this.m_ViewItemCollection[this.SelectedIndex];
            if (viewItem == null) return null;
            return viewItem.ViewItems[this.m_RowColumnViewItem.SelectedIndex];
        }
        #endregion

        #region IGridViewItemListBoxEvent
        public event MouseEventHandler RowHeaderMouseClick;

        public event MouseEventHandler RowHeaderMouseDoubleClick;

        public event RowHeaderItemDrawEventHandler RowHeaderItemDrawing;
        #endregion

        #region IColumnTitleViewObject
        bool m_ShowColumnTitle = false;
        [Browsable(true), DefaultValue(false), Description("显示列"), Category("外观")]
        public bool ShowColumnTitle
        {
            get { return m_ShowColumnTitle; }
            set
            {
                if (this.m_ShowColumnTitle == value) return;
                m_ShowColumnTitle = value;
                this.Refresh();
            }
        }

        private bool m_CanResizeColumnTitleHeight = true;
        [Browsable(true), DefaultValue(true), Description("是否可以调节行头标题高"), Category("状态")]
        public bool CanResizeColumnTitleHeight
        {
            get { return m_CanResizeColumnTitleHeight; }
            set { m_CanResizeColumnTitleHeight = value; }
        }

        private int m_ColumnTitleHeight = 0;
        [Browsable(false), Description("列标题高（它是只读的，每次绘制时记录下的计算尺寸）"), Category("外观")]
        public int ColumnTitleHeight
        {
            get { return m_ColumnTitleHeight; }
        }

        [Browsable(false), Description("列视图矩形框"), Category("布局")]
        public Rectangle ColumnTitleViewItemsRectangle
        {
            get
            {
                Rectangle rectangle = this.ItemsRectangle;
                return Rectangle.FromLTRB
                    (
                    this.ShowRowHeader ? rectangle.Left + this.RowHeaderWidth : rectangle.Left,
                    rectangle.Top,
                    rectangle.Right - (this.VScrollBarVisible ? CONST_SCROLBARBARSIZE : 0) - 1,
                    rectangle.Top + this.ColumnTitleHeight
                    );
            }
        }
        
        [Browsable(true),
        Editor(typeof(GISShare.Controls.WinForm.WFNew.View.Design.TitleViewItemCollectionEditer), typeof(System.Drawing.Design.UITypeEditor)),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
        Description("其所携带的列集合"),
        Category("子项")]
        public TitleViewItemCollection TitleViewItems
        {
            get { return this.m_LockRowColumnTitleViewItem.ViewItems; }
        }

        /// <summary>
        /// 根据文本获取一个标题视图项
        /// </summary>
        /// <param name="strText">文本</param>
        /// <returns></returns>
        public TitleViewItem GetTitleViewItemByText(string strText)
        {
            return this.GetTitleViewItemByText_DG(this.m_LockRowColumnTitleViewItem, strText);
        }
        public TitleViewItem GetTitleViewItemByText_DG(IRowViewItem pRowViewItem, string strText)
        {
            TitleViewItem titleViewItem = null;
            for (int i = 0; i < pRowViewItem.List.Count;i++ )
            {
                titleViewItem = pRowViewItem.List[i] as TitleViewItem;
                if (titleViewItem == null) continue;
                if (titleViewItem.Text == strText) break;
                titleViewItem = this.GetTitleViewItemByText_DG(titleViewItem as IRowViewItem, strText);
                if (titleViewItem != null) break;
            }
            return titleViewItem;
        }

        /// <summary>
        /// 根据名称获取一个标题视图项
        /// </summary>
        /// <param name="strName">名称</param>
        /// <returns></returns>
        public TitleViewItem GetTitleViewItemByName(string strName)
        {
            return this.GetTitleViewItemByName_DG(this.m_LockRowColumnTitleViewItem, strName);
        }
        public TitleViewItem GetTitleViewItemByName_DG(IRowViewItem pRowViewItem, string strName)
        {
            TitleViewItem titleViewItem = null;
            for (int i = 0; i < pRowViewItem.List.Count; i++)
            {
                titleViewItem = pRowViewItem.List[i] as TitleViewItem;
                if (titleViewItem == null) continue;
                if (titleViewItem.Name == strName) break;
                titleViewItem = this.GetTitleViewItemByText_DG(titleViewItem as IRowViewItem, strName);
                if (titleViewItem != null) break;
            }
            return titleViewItem;
        }
        #endregion

        #region IColumnViewObject
        private int m_DefaultColumnWidth = 60;
        [Browsable(true), DefaultValue(60), Description("默认列宽"), Category("外观")]
        public int DefaultColumnWidth
        {
            get { return m_DefaultColumnWidth; }
            set { m_DefaultColumnWidth = value; }
        }

        bool m_ShowColumn = true;
        [Browsable(true), DefaultValue(true), Description("显示列"), Category("外观")]
        public bool ShowColumn
        {
            get { return m_ShowColumn; }
            set
            {
                if (this.m_ShowColumn == value) return;
                m_ShowColumn = value;
                this.Refresh();
            }
        }

        [Browsable(true), DefaultValue(18), Description("列高"), Category("外观")]
        public int ColumnHeight
        {
            get { return this.m_RowColumnViewItem.Height; }
            set
            {
                if (value < CONST_MINROWHEADERHEIGHT) value = CONST_MINROWHEADERHEIGHT;
                if (value == this.m_RowColumnViewItem.Height) return;
                this.m_RowColumnViewItem.Height = value;
                this.Refresh();
            }
        }

        private bool m_CanResizeColumnWidth = true;
        [Browsable(true), DefaultValue(true), Description("是否可以调节列宽"), Category("状态")]
        public bool CanResizeColumnWidth
        {
            get { return m_CanResizeColumnWidth; }
            set { m_CanResizeColumnWidth = value; }
        }

        private bool m_CanExchangeColumn = false;
        [Browsable(true), DefaultValue(false), Description("是否可以交换列（TitleViewItems.Count <= 0）"), Category("状态")]
        public bool CanExchangeColumn
        {
            get { return m_CanExchangeColumn && this.TitleViewItems.Count <= 0; }
            set { m_CanExchangeColumn = value; }
        }

        [Browsable(true),
        Editor(typeof(GISShare.Controls.WinForm.WFNew.View.Design.ColumnViewItemCollectionEditer), typeof(System.Drawing.Design.UITypeEditor)),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
        Description("其所携带的列集合"),
        Category("子项")]
        public ColumnViewItemCollection ColumnViewItems
        {
            get { return this.m_RowColumnViewItem.ViewItems; }
        }
        #endregion

        #region IColumnViewObjectHelper
        void IColumnViewObjectHelper.InsertColumnViewItem(int index)
        {
            //foreach (IRowCellViewItem one in this.m_ViewItemCollection)
            for (int i = 0; i < this.m_ViewItemCollection.Count; i++)
            {
                IRowCellViewItem one = this.m_ViewItemCollection[i];
                switch (one.eRowCellViewStyle)
                {
                    case RowCellViewStyle.eSystemRow:
                        one.Items.Insert(index, this.CreateCellViewItem(CellViewStyle.eSystem, i, index));
                        break;
                    case RowCellViewStyle.eSingleCellRow:
                        if (one.List.Count <= 0)
                        {
                            one.Items.Insert(index, this.CreateCellViewItem(CellViewStyle.eSingleCell, i, index));
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        void IColumnViewObjectHelper.RemoveColumnViewItem(int index)
        {
            foreach (IRowCellViewItem one in this.m_ViewItemCollection)
            {
                switch (one.eRowCellViewStyle)
                {
                    case RowCellViewStyle.eSystemRow:
                        one.Items.RemoveAt(index);
                        break;
                    case RowCellViewStyle.eSingleCellRow:
                        break;
                    default:
                        break;
                }
            }
        }

        void IColumnViewObjectHelper.ExchangeColumnViewItem(int index1, int index2)
        {
            foreach (IRowCellViewItem one in this.m_ViewItemCollection)
            {
                switch (one.eRowCellViewStyle)
                {
                    case RowCellViewStyle.eSystemRow:
                        one.Items.ExchangeItem(index1, index2);
                        break;
                    case RowCellViewStyle.eSingleCellRow:
                        break;
                    default:
                        break;
                }
            }
        }

        [Browsable(false), Description("列视图矩形框"), Category("布局")]
        public Rectangle ColumnViewItemsRectangle
        {
            get
            {
                Rectangle rectangle = this.ItemsRectangle;
                return Rectangle.FromLTRB
                    (
                    this.ShowRowHeader ? rectangle.Left + this.RowHeaderWidth : rectangle.Left,
                    this.ShowColumnTitle ? this.ColumnTitleHeight + rectangle.Top : rectangle.Top,
                    rectangle.Right - (this.VScrollBarVisible ? CONST_SCROLBARBARSIZE : 0) - 1,//
                    rectangle.Top + (this.ShowColumnTitle ? (this.ColumnTitleHeight + this.m_RowColumnViewItem.Height) : this.m_RowColumnViewItem.Height)
                    );
            }
        }
        #endregion

        #region IRowViewObject
        private int m_DefaultRowHeight = 18;
        [Browsable(true), DefaultValue(18), Description("默认行高"), Category("外观")]
        public int DefaultRowHeight
        {
            get { return m_DefaultRowHeight; }
            set { m_DefaultRowHeight = value; }
        }

        private bool m_CanResizeRowHeight = true;
        [Browsable(true), DefaultValue(true), Description("是否可以调节行高"), Category("状态")]
        public bool CanResizeRowHeight
        {
            get { return m_CanResizeRowHeight; }
            set { m_CanResizeRowHeight = value; }
        }

        private bool m_CanExchangeRow = false;
        [Browsable(true), DefaultValue(false), Description("是否可以交换行"), Category("状态")]
        public bool CanExchangeRow
        {
            get { return m_CanExchangeRow; }
            set { m_CanExchangeRow = value; }
        }

        [Browsable(false), Description("系统行宽"), Category("外观")]
        public int RowWidth
        {
            get { return this.m_RowColumnViewItem.Width; }
        }

        [Browsable(false), Description("ViewItems显示矩形框"), Category("布局")]
        public Rectangle RowViewItemsRectangle
        {
            get
            {
                Rectangle rectangle = this.ItemsRectangle;
                //Console.WriteLine(this.RowHeaderWidth);
                //Console.WriteLine(rectangle.Left + this.RowHeaderWidth);
                int iTop = rectangle.Top;
                if (this.ShowColumnTitle) iTop += this.ColumnTitleHeight;
                if (this.ShowColumn) iTop += this.m_RowColumnViewItem.Height;
                return Rectangle.FromLTRB
                    (
                    this.ShowRowHeader ? (rectangle.Left + this.RowHeaderWidth) : rectangle.Left,
                    iTop,
                    rectangle.Right - (this.VScrollBarVisible ? CONST_SCROLBARBARSIZE : 0) - 1,//
                    rectangle.Bottom - (this.HScrollBarVisible ? CONST_SCROLBARBARSIZE : 0) - 1
                    );
            }
        }
        #endregion

        #region 属性
        [Browsable(false), Description("多选项索引集合"), Category("属性")]
        public SelectedIndexCollection SelectedIndices
        {
            get { return m_SelectedIndices; }
        }

        [Browsable(false), Description("选中的数据源项"), Category("属性")]
        public object SelectedDataItem
        {
            get
            {
                DataRowCellViewItem dataRowCellViewItem = this.m_ViewItemCollection[this.SelectedIndex] as DataRowCellViewItem;
                return dataRowCellViewItem == null ? null : dataRowCellViewItem.DataItem;
            }
        }

        public object GetDataItemByRowIndex(int iIndex)
        {
            DataRowCellViewItem dataRowCellViewItem = this.m_ViewItemCollection[iIndex] as DataRowCellViewItem;
            return dataRowCellViewItem == null ? null : dataRowCellViewItem.DataItem;
        }

        object m_DataSource = null;
        [Browsable(false), Description("数据源"), Category("属性")]
        public object DataSource
        {
            get { return m_DataSource; }
            set
            {
                //if (this.m_DataSource == value) return;
                m_DataSource = value;
                this.ClearRowViewItem();
                //this.ColumnViewItems.Clear();
                if (this.SetDataSource(this.m_DataSource as DataTable)) return;
                if (this.SetDataSource(this.m_DataSource as IList)) return;
            }
        }
        private bool SetDataSource(DataTable dataTable)
        {
            if (dataTable == null) return false;
            //
            if (this.ColumnViewItems.Count <= 0)
            {
                foreach (DataColumn one in dataTable.Columns)
                {
                    this.ColumnViewItems.Add(
                        new ColumnViewItem()
                        {
                            Name = one.ColumnName,
                            FieldName = one.ColumnName,
                            Text = one.Caption.Length > 0 ? one.Caption : one.ColumnName,
                            Width = this.DefaultColumnWidth
                        }
                            );
                }
            }
            //
            //foreach (DataRow one in dataTable.Rows)
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow one = dataTable.Rows[i];
                DataRowCellViewItem rowViewItem = new DataRowCellViewItem(RowCellViewStyle.eSystemRow, one);
                rowViewItem.Height = this.DefaultRowHeight;
                //foreach (ColumnViewItem one2 in this.ColumnViewItems)
                for (int j = 0; j < this.ColumnViewItems.Count; j++)
                {
                    ColumnViewItem one2 = this.ColumnViewItems[j];
                    if (dataTable.Columns.Contains(one2.FieldName))
                    {
                        CellViewItem cellViewItem = this.CreateCellViewItem(CellViewStyle.eSystem, i, j);
                        cellViewItem.Name = one2.FieldName;
                        cellViewItem.Value = one[one2.FieldName];
                        rowViewItem.ViewItems.Add(cellViewItem);
                    }
                }
                this.m_ViewItemCollection.Add(rowViewItem);
            }
            //
            return true;
        }
        private bool SetDataSource(IList list)
        {
            if (list == null) return false;
            //
            Type[] typeArray = list.GetType().GetGenericArguments();
            if (typeArray.Length > 0)
            {
                Type type = typeArray[0];
                PropertyInfo[] propertyInfoArray = type.GetProperties();
                if (this.ColumnViewItems.Count <= 0)
                {
                    foreach (PropertyInfo one in propertyInfoArray)
                    {
                        this.ColumnViewItems.Add(
                            new ColumnViewItem()
                            {
                                Name = one.Name,
                                FieldName = one.Name,
                                Text = one.Name,
                                Width = this.DefaultColumnWidth
                            }
                            );
                    }
                }
                //
                //foreach (object one in list)
                for (int i = 0; i < list.Count; i++)
                {
                    object one = list[i];
                    DataRowCellViewItem rowViewItem = new DataRowCellViewItem(RowCellViewStyle.eSystemRow, one);
                    rowViewItem.Height = this.DefaultRowHeight;
                    //foreach (ColumnViewItem one2 in this.ColumnViewItems)
                    for (int j = 0; j < this.ColumnViewItems.Count; j++)
                    {
                        ColumnViewItem one2 = this.ColumnViewItems[j];
                        PropertyInfo propertyInfo = type.GetProperty(one2.FieldName);
                        if (propertyInfo != null)
                        {
                            CellViewItem cellViewItem = this.CreateCellViewItem(CellViewStyle.eSystem, i, j);
                            cellViewItem.Name = one2.FieldName;
                            cellViewItem.Value = propertyInfo.GetValue(one, null);
                            rowViewItem.ViewItems.Add(cellViewItem);
                        }
                    }
                    this.m_ViewItemCollection.Add(rowViewItem);
                }
            }
            return true;
        }

        public void RefreshDataSource()
        {
            if (this.RefreshDataSource(this.m_DataSource as DataTable)) return;
            if (this.RefreshDataSource(this.m_DataSource as IList)) return;
        }
        private bool RefreshDataSource(DataTable dataTable)
        {
            if (dataTable == null) return false;
            //
            for (int i = this.m_ViewItemCollection.Count - 1; i >= 0; i--)
            {
                DataRowCellViewItem dataRowCellViewItem = this.m_ViewItemCollection[i] as DataRowCellViewItem;
                if (dataRowCellViewItem != null)
                {
                    System.Data.DataRow dataRow = dataRowCellViewItem.DataItem as System.Data.DataRow;
                    if (dataRow != null)
                    {
                        if (!this.DataTableContains(dataTable, dataRow))
                        {
                            this.m_ViewItemCollection.RemoveAt(i);
                        }
                    }
                }
            }
            //
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                System.Data.DataRow dataRow = dataTable.Rows[i];
                if (!this.UpdateViewItemsContains(dataTable, dataRow))
                {
                    DataRowCellViewItem rowViewItem = new DataRowCellViewItem(RowCellViewStyle.eSystemRow, dataRow);
                    rowViewItem.Height = this.DefaultRowHeight;
                    //foreach (ColumnViewItem one2 in this.ColumnViewItems)
                    for (int j = 0; j < this.ColumnViewItems.Count; j++)
                    {
                        ColumnViewItem one2 = this.ColumnViewItems[j];
                        if (dataTable.Columns.Contains(one2.FieldName))
                        {
                            CellViewItem cellViewItem = this.CreateCellViewItem(CellViewStyle.eSystem, i, j);
                            cellViewItem.Name = one2.FieldName;
                            cellViewItem.Value = dataRow[one2.FieldName];
                            rowViewItem.ViewItems.Add(cellViewItem);
                        }
                    }
                    this.m_ViewItemCollection.Insert(i, rowViewItem);
                }
            }
            //
            this.Refresh();
            //
            return true;
        }
        private bool DataTableContains(DataTable dataTable, System.Data.DataRow dataRow)
        {
            foreach( System.Data.DataRow one in dataTable.Rows  )
            {
                if (one == dataRow) return true;
            }
            return false;
        }
        private bool UpdateViewItemsContains(DataTable dataTable, System.Data.DataRow dataRow)
        {
            foreach(RowCellViewItem one in this.m_ViewItemCollection)
            {
                DataRowCellViewItem dataRowCellViewItem = one as DataRowCellViewItem;
                if (dataRowCellViewItem != null) 
                {
                    System.Data.DataRow dataRow2 = dataRowCellViewItem.DataItem as System.Data.DataRow;
                    if (dataRow2 == dataRow)
                    {
                        foreach (DataColumn one2 in dataTable.Columns)
                        {
                            CellViewItem cellViewItem = dataRowCellViewItem.ViewItems[one2.ColumnName];
                            if (cellViewItem != null)
                            {
                                cellViewItem.Value = dataRow[one2.ColumnName];
                            }
                        }
                        return true;
                    }
                }
            }
            return false;
        }
        private bool RefreshDataSource(IList list)
        {
            if (list == null) return false;
            //
            for (int i = this.m_ViewItemCollection.Count - 1; i >= 0; i--)
            {
                DataRowCellViewItem dataRowCellViewItem = this.m_ViewItemCollection[i] as DataRowCellViewItem;
                if (dataRowCellViewItem != null)
                {
                    object obj = dataRowCellViewItem.DataItem;
                    if (obj != null)
                    {
                        if (!list.Contains(obj))
                        {
                            this.m_ViewItemCollection.RemoveAt(i);
                        }
                    }
                }
            }
            //
            Type[] typeArray = list.GetType().GetGenericArguments();
            if (typeArray.Length > 0)
            {
                Type type = typeArray[0];
                for (int i = 0; i < list.Count; i++)
                {
                    object obj = list[i];
                    if (!this.UpdateViewItemsContains(list, obj, type))
                    {
                        DataRowCellViewItem rowViewItem = new DataRowCellViewItem(RowCellViewStyle.eSystemRow, obj);
                        rowViewItem.Height = this.DefaultRowHeight;
                        //foreach (ColumnViewItem one2 in this.ColumnViewItems)
                        for (int j = 0; j < this.ColumnViewItems.Count; j++)
                        {
                            ColumnViewItem one2 = this.ColumnViewItems[j];
                            PropertyInfo propertyInfo = type.GetProperty(one2.FieldName);
                            if (propertyInfo != null)
                            {
                                CellViewItem cellViewItem = this.CreateCellViewItem(CellViewStyle.eSystem, i, j);
                                cellViewItem.Name = one2.FieldName;
                                cellViewItem.Value = propertyInfo.GetValue(obj, null);
                                rowViewItem.ViewItems.Add(cellViewItem);
                            }
                        }
                        this.m_ViewItemCollection.Insert(i, rowViewItem);
                    }
                }
            }
            //
            this.Refresh();
            //
            return true;
        }
        private bool DataTableContains(IList list, object obj)
        {
            foreach (object one in list)
            {
                if (one == obj) return true;
            }
            return false;
        }
        private bool UpdateViewItemsContains(IList list, object obj, Type type)
        {
            foreach (RowCellViewItem one in this.m_ViewItemCollection)
            {
                DataRowCellViewItem dataRowCellViewItem = one as DataRowCellViewItem;
                if (dataRowCellViewItem != null)
                {
                    object obj2 = dataRowCellViewItem.DataItem;
                    if (obj2 == obj)
                    {
                        foreach (ColumnViewItem one2 in this.ColumnViewItems)
                        {
                            PropertyInfo propertyInfo = type.GetProperty(one2.FieldName);
                            if (propertyInfo != null)
                            {
                                CellViewItem cellViewItem = dataRowCellViewItem.ViewItems[one2.FieldName];
                                if (cellViewItem != null)
                                {
                                    cellViewItem.Value = propertyInfo.GetValue(obj, null); ;
                                }
                            }
                        }
                        return true;
                    }
                }
            }
            return false;
        }
        #endregion

        protected virtual CellViewItem CreateCellViewItem(CellViewStyle eCellViewStyle, int iRowIndex, int iColumnIndex)
        {
            return new CellViewItem(eCellViewStyle);
        }
        
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
            this.OnDraw(e);
            //
            int iLeftOffset = this.LeftOffset;
            //int iRightOffset = this.HScrollBarMaximum - iLeftOffset;
            //if (iRightOffset == 0) iRightOffset = 0;
            //
            this.m_HScrollBarMaximum = 0;
            //
            Rectangle viewItemsRectangle = this.ViewItemsRectangle;
            //
            ISetViewItemHelper pSetViewItemHelper;
            IMessageChain pMessageChain;
            //
            Rectangle clipRectangle;
            //
            Size viewItemSize = this.m_RowColumnViewItem.MeasureSize(e.Graphics);//key
            //
            #region 绘制列标题
            this.m_ColumnTitleHeight = this.m_LockRowColumnTitleViewItem.MeasureSize(e.Graphics).Height;//key
            //
            Rectangle rectangle = this.ColumnTitleViewItemsRectangle;
            if (this.ShowColumnTitle)
            {
                pSetViewItemHelper = this.m_LockRowColumnTitleViewItem as ISetViewItemHelper;
                if (pSetViewItemHelper != null)
                {
                    pSetViewItemHelper.SetViewItemDisplayRectangle
                        (
                        Rectangle.FromLTRB
                           (
                           rectangle.Left - iLeftOffset,
                           rectangle.Top,
                           rectangle.Left + viewItemSize.Width, //rectangle.Right + iRightOffset,
                           rectangle.Top + m_ColumnTitleHeight
                           )
                        );
                }
                //
                if (this.ShowRowHeader)
                {
                    Rectangle rectangle2 = this.RowHeaderRectangle;
                    IRowHeaderItemHelper pRowHeaderItemHelper = (IRowHeaderItemHelper)this.m_LockRowColumnTitleViewItem;
                    pRowHeaderItemHelper.SetIndex(-2);
                    clipRectangle = Rectangle.FromLTRB
                        (
                        rectangle2.Left, 
                        this.m_LockRowColumnTitleViewItem.DisplayRectangle.Top, 
                        rectangle2.Right, 
                        this.m_LockRowColumnTitleViewItem.DisplayRectangle.Bottom > viewItemsRectangle.Bottom ? viewItemsRectangle.Bottom : this.m_LockRowColumnTitleViewItem.DisplayRectangle.Bottom
                        );
                    pRowHeaderItemHelper.SetRowHeaderRectangle(clipRectangle);
                    e.Graphics.SetClip(clipRectangle);
                    pRowHeaderItemHelper.DrawRowHeader(new PaintEventArgs(e.Graphics, clipRectangle), this.ShowRowHeaderID, this.RowHeaderStartID);
                }
                //
                pMessageChain = this.m_LockRowColumnTitleViewItem as IMessageChain;
                if (pMessageChain != null)
                {
                    clipRectangle = Rectangle.Intersect(rectangle, this.m_LockRowColumnTitleViewItem.DisplayRectangle);
                    if (clipRectangle.Bottom > viewItemsRectangle.Bottom) 
                    {
                        clipRectangle = Rectangle.FromLTRB(clipRectangle.Left, clipRectangle.Top, clipRectangle.Right, viewItemsRectangle.Bottom);
                    }
                    e.Graphics.SetClip(clipRectangle);
                    //pMessageChain.SendMessage(messageInfo);//new MessageInfo(this, MessageStyle.eMSPaint, new PaintEventArgs(e.Graphics, clipRectangle))
                    pMessageChain.SendMessage(new MessageInfo(this, MessageStyle.eMSPaint, new PaintEventArgs(e.Graphics, clipRectangle)));//messageInfo
                }
            }
            #endregion
            //
            #region 绘制列
            rectangle = this.ColumnViewItemsRectangle;
            int iW_W = viewItemSize.Width - rectangle.Width;
            if (iW_W > this.m_HScrollBarMaximum) this.m_HScrollBarMaximum = iW_W;
            if (this.ShowColumn && this.m_RowColumnViewItem.ViewItems.Count > 0)
            {
                pSetViewItemHelper = this.m_RowColumnViewItem as ISetViewItemHelper;
                if (pSetViewItemHelper != null)
                {
                    pSetViewItemHelper.SetViewItemDisplayRectangle
                        (
                        Rectangle.FromLTRB
                           (
                           rectangle.Left - iLeftOffset,
                           rectangle.Top,
                           rectangle.Left + viewItemSize.Width, //rectangle.Right + iRightOffset,
                           rectangle.Top + viewItemSize.Height
                           )
                        );
                }
                //
                if (this.ShowRowHeader)
                {
                    Rectangle rectangle2 = this.RowHeaderRectangle;
                    IRowHeaderItemHelper pRowHeaderItemHelper = (IRowHeaderItemHelper)this.m_RowColumnViewItem;
                    pRowHeaderItemHelper.SetIndex(-1);
                    clipRectangle = Rectangle.FromLTRB
                        (
                        rectangle2.Left, 
                        this.m_RowColumnViewItem.DisplayRectangle.Top, 
                        rectangle2.Right, 
                        this.m_RowColumnViewItem.DisplayRectangle.Bottom > viewItemsRectangle.Bottom ? viewItemsRectangle.Bottom : this.m_RowColumnViewItem.DisplayRectangle.Bottom
                        );
                    pRowHeaderItemHelper.SetRowHeaderRectangle(clipRectangle);
                    e.Graphics.SetClip(clipRectangle);
                    pRowHeaderItemHelper.DrawRowHeader(new PaintEventArgs(e.Graphics, clipRectangle), this.ShowRowHeaderID, this.RowHeaderStartID);
                }
                //
                pMessageChain = this.m_RowColumnViewItem as IMessageChain;
                if (pMessageChain != null)
                {
                    clipRectangle = Rectangle.Intersect(rectangle, this.m_RowColumnViewItem.DisplayRectangle);
                    if (clipRectangle.Bottom > viewItemsRectangle.Bottom)
                    {
                        clipRectangle = Rectangle.FromLTRB(clipRectangle.Left, clipRectangle.Top, clipRectangle.Right, viewItemsRectangle.Bottom);
                    }
                    e.Graphics.SetClip(clipRectangle);
                    //pMessageChain.SendMessage(messageInfo);//new MessageInfo(this, MessageStyle.eMSPaint, new PaintEventArgs(e.Graphics, clipRectangle))
                    pMessageChain.SendMessage(new MessageInfo(this, MessageStyle.eMSPaint, new PaintEventArgs(e.Graphics, clipRectangle)));//messageInfo
                }
            }
            #endregion
            //
            #region 绘制子项
            rectangle = this.RowViewItemsRectangle;
            //
            ViewItem viewItem = null;
            int iItemHeight = 0;
            int iNum = this.TopViewItemIndex;
            int iCount = this.m_ViewItemCollection.Count;
            for (; iNum < iCount; iNum++)
            {
                viewItem = this.m_ViewItemCollection[iNum];
                if (viewItem != null)
                {
                    pSetViewItemHelper = viewItem as ISetViewItemHelper;
                    if (pSetViewItemHelper != null)
                    {
                        pSetViewItemHelper.SetViewItemDisplayRectangle
                            (
                            Rectangle.FromLTRB
                               (
                               rectangle.Left - iLeftOffset,
                               rectangle.Top + iItemHeight,
                               rectangle.Left + viewItemSize.Width, //rectangle.Right + iRightOffset,
                               rectangle.Top + iItemHeight + viewItem.MeasureSize(e.Graphics).Height
                               )
                            );
                    }
                    iItemHeight += viewItem.DisplayRectangle.Height;
                    if (iItemHeight >= rectangle.Height) break;
                }
            }
            //
            this.m_VScrollBarMaximum = iCount - (iNum - this.TopViewItemIndex);
            //
            this.SetHVScrollBarVisible(this.HScrollBarMaximum > this.HScrollBarMinimum, this.VScrollBarMaximum > this.VScrollBarMinimum);
            //
            if (iNum < iCount) iNum++;
            if (this.ShowRowHeader)
            {
                Rectangle rectangle2 = this.RowHeaderRectangle;
                IRowHeaderItemHelper pRowHeaderItemHelper;
                for (int i = this.TopViewItemIndex; i < iNum; i++)
                {
                    viewItem = this.m_ViewItemCollection[i];
                    if (viewItem != null)
                    {
                        pRowHeaderItemHelper = (IRowHeaderItemHelper)viewItem;
                        pRowHeaderItemHelper.SetIndex(i);
                        clipRectangle = Rectangle.FromLTRB
                            (
                            rectangle2.Left,
                            viewItem.DisplayRectangle.Top, 
                            rectangle2.Right,
                            viewItem.DisplayRectangle.Bottom > viewItemsRectangle.Bottom ? viewItemsRectangle.Bottom : viewItem.DisplayRectangle.Bottom
                            );
                        pRowHeaderItemHelper.SetRowHeaderRectangle(clipRectangle);
                        e.Graphics.SetClip(clipRectangle);
                        pRowHeaderItemHelper.DrawRowHeader(new PaintEventArgs(e.Graphics, clipRectangle), this.ShowRowHeaderID, this.RowHeaderStartID);
                        this.OnRowHeaderItemDrawing(new RowHeaderItemDrawEventArgs(e.Graphics, clipRectangle, (IRowHeaderItem)viewItem));
                        //
                        pMessageChain = viewItem as IMessageChain;
                        if (pMessageChain != null)
                        {
                            clipRectangle = Rectangle.Intersect(rectangle, viewItem.DisplayRectangle);
                            e.Graphics.SetClip(clipRectangle);
                            //pMessageChain.SendMessage(messageInfo);//new MessageInfo(this, MessageStyle.eMSPaint, new PaintEventArgs(e.Graphics, clipRectangle))
                            pMessageChain.SendMessage(new MessageInfo(this, MessageStyle.eMSPaint, new PaintEventArgs(e.Graphics, clipRectangle)));//messageInfo
                        }
                    }
                }
            }
            else
            {
                for (int i = this.TopViewItemIndex; i < iNum; i++)
                {
                    viewItem = this.m_ViewItemCollection[i];
                    if (viewItem != null)
                    {
                        pMessageChain = viewItem as IMessageChain;
                        if (pMessageChain != null)
                        {
                            clipRectangle = Rectangle.Intersect(rectangle, viewItem.DisplayRectangle);
                            e.Graphics.SetClip(clipRectangle);
                            //pMessageChain.SendMessage(messageInfo);//new MessageInfo(this, MessageStyle.eMSPaint, new PaintEventArgs(e.Graphics, clipRectangle))
                            pMessageChain.SendMessage(new MessageInfo(this, MessageStyle.eMSPaint, new PaintEventArgs(e.Graphics, clipRectangle)));//messageInfo
                        }
                    }
                }
            }
            //
            this.m_BottomViewItemIndex = iNum;
            #endregion
            //
            e.Graphics.SetClip(e.ClipRectangle);
        }
        private void SetHVScrollBarVisible(bool bHScrollBarVisible, bool bVScrollBarVisible)
        {
            bool bH = this.m_HScrollBarVisible == bHScrollBarVisible;
            bool bV = this.m_VScrollBarVisible == bVScrollBarVisible;
            if (bH && bV) return;
            if (!bH) this.m_HScrollBarVisible = bHScrollBarVisible;
            if (!bV) this.m_VScrollBarVisible = bVScrollBarVisible;
            this.Invalidate(this.ItemsRectangle);
        }
        private void MSLostFocus(MessageInfo messageInfo)
        {
            ViewItem viewItem = this.m_ViewItemCollection[this.SelectedIndex];
            if (viewItem != null)
            {
                IMessageChain pMessageChain = viewItem as IMessageChain;
                if (pMessageChain != null)
                {
                    pMessageChain.SendMessage(messageInfo);
                }
            }
        }
        private void MSKeyDown(MessageInfo messageInfo)
        {
            KeyEventArgs e = messageInfo.MessageParameter as KeyEventArgs;
            if (e == null) return;
            //
            ViewItem viewItem = this.m_ViewItemCollection[this.SelectedIndex];
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
            //Console.WriteLine(this.m_KeyDownKeyValue.ToString());
        }
        private void MSKeyUp(MessageInfo messageInfo)
        {
            ViewItem viewItem = this.m_ViewItemCollection[this.SelectedIndex];
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
            ViewItem viewItem = this.m_ViewItemCollection[this.SelectedIndex];
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
            ViewItem viewItem = this.m_ViewItemCollection[this.SelectedIndex];
            if (viewItem != null)
            {
                IMessageChain pMessageChain = viewItem as IMessageChain;
                if (pMessageChain != null)
                {
                    pMessageChain.SendMessage(messageInfo);
                }
            }
            //
            if (messageInfo.CancelPreEvent) return;
            //
            MouseEventArgs e = messageInfo.MessageParameter as MouseEventArgs;
            if (e == null) return;
            //
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
        }
        private void MSMouseDown(MessageInfo messageInfo)
        {
            MouseEventArgs e = messageInfo.MessageParameter as MouseEventArgs;
            if (e == null) return;
            //
            ViewItem viewItem = null;
            IMessageChain pMessageChain;
            Rectangle rectangle = this.RowHeaderRectangle;
            Rectangle rectangle2 = this.RowViewItemsRectangle;
            if (this.ShowColumnTitle && this.ColumnTitleViewItemsRectangle.Contains(e.Location))
            {
                pMessageChain = this.m_LockRowColumnTitleViewItem as IMessageChain;
                if (pMessageChain != null)
                {
                    pMessageChain.SendMessage(messageInfo);
                }
                //
                return;
            }
            else if (this.ShowRowHeader && rectangle.Contains(e.Location))
            {
                #region 行头
                //
                #region 调节行头标题高
                if (this.ShowColumnTitle && this.CanResizeColumnTitleHeight)
                {
                    if (this.m_LockRowColumnTitleViewItem.DisplayRectangle.Bottom - CONST_RESIZERECTANGLESIZE <= e.Location.Y && this.m_LockRowColumnTitleViewItem.DisplayRectangle.Bottom >= e.Location.Y)
                    {
                        this.m_OperationStyle = 6;
                        this.m_MouseDownPoint = e.Location;
                        this.m_CursorDefault = this.Cursor;
                        this.Cursor = Cursors.SizeNS;
                        if (rectangle2.Width > CONST_SHOWSPLITELINEMINHEIGTH)
                        {
                            if (this.m_SplitLineForm == null) this.m_SplitLineForm = new SplitLineForm();
                            this.m_SplitLineForm.Show(
                                DockStyle.Left,
                                Rectangle.FromLTRB(rectangle.Left, this.m_LockRowColumnTitleViewItem.DisplayRectangle.Bottom - 1, rectangle2.Right, this.m_LockRowColumnTitleViewItem.DisplayRectangle.Bottom + 1)
                                );
                        }
                        messageInfo.CancelPreEvent = true;//取消事件 0
                        return;
                    }
                }
                #endregion
                //
                #region 调节行头宽
                if (this.CanResizeRowHeaderWidth)
                {
                    if (rectangle.Right - CONST_RESIZERECTANGLESIZE <= e.Location.X)
                    {
                        this.m_OperationStyle = 2;
                        this.m_MouseDownPoint = e.Location;
                        this.m_CursorDefault = this.Cursor;
                        this.Cursor = Cursors.SizeWE;
                        if (rectangle2.Height > CONST_SHOWSPLITELINEMINHEIGTH)
                        {
                            if (this.m_SplitLineForm == null) this.m_SplitLineForm = new SplitLineForm();
                            this.m_SplitLineForm.Show(
                                DockStyle.Left,
                                Rectangle.FromLTRB(rectangle.Right - 1, rectangle.Top, rectangle.Right + 1, rectangle.Bottom - 1)
                                );
                        }
                        messageInfo.CancelPreEvent = true;//取消事件 1
                        return;
                    }
                }
                #endregion
                //
                #region 调节行头高
                if (this.ShowColumn && this.CanResizeRowHeaderHeight)
                {
                    if (this.m_RowColumnViewItem.DisplayRectangle.Bottom - CONST_RESIZERECTANGLESIZE <= e.Location.Y && this.m_RowColumnViewItem.DisplayRectangle.Bottom >= e.Location.Y)
                    {
                        this.m_OperationStyle = 3;
                        this.m_MouseDownPoint = e.Location;
                        this.m_CursorDefault = this.Cursor;
                        this.Cursor = Cursors.SizeNS;
                        if (rectangle2.Width > CONST_SHOWSPLITELINEMINHEIGTH)
                        {
                            if (this.m_SplitLineForm == null) this.m_SplitLineForm = new SplitLineForm();
                            this.m_SplitLineForm.Show(
                                DockStyle.Left,
                                Rectangle.FromLTRB(rectangle.Left, this.m_RowColumnViewItem.DisplayRectangle.Bottom - 1, rectangle2.Right, this.m_RowColumnViewItem.DisplayRectangle.Bottom + 1)
                                );
                        }
                        messageInfo.CancelPreEvent = true;//取消事件 2
                        return;
                    }
                }
                #endregion
                //
                #region 调节行高/交换
                for (int i = this.TopViewItemIndex; i <= this.BottomViewItemIndex; i++)
                {
                    viewItem = this.m_ViewItemCollection[i];
                    if (viewItem != null && viewItem.DisplayRectangle.Top <= e.Location.Y && viewItem.DisplayRectangle.Bottom >= e.Location.Y)
                    {
                        this.SelectedIndex = i;
                        this.m_RowColumnViewItem.SelectedIndex = -1;
                        //
                        if (this.CanResizeRowHeight &&
                            viewItem.DisplayRectangle.Bottom - CONST_RESIZERECTANGLESIZE <= e.Location.Y && viewItem.DisplayRectangle.Bottom + CONST_RESIZERECTANGLESIZE >= e.Location.Y)
                        {
                            this.m_OperationStyle = 5;
                            this.m_MouseDownSelectedIndex = i;
                            this.m_MouseDownPoint = e.Location;
                            this.m_CursorDefault = this.Cursor;
                            this.Cursor = Cursors.SizeNS;
                            if (rectangle2.Width > CONST_SHOWSPLITELINEMINHEIGTH)
                            {
                                if (this.m_SplitLineForm == null) this.m_SplitLineForm = new SplitLineForm();
                                this.m_SplitLineForm.Show(
                                    DockStyle.Left,
                                    Rectangle.FromLTRB(rectangle2.Left, viewItem.DisplayRectangle.Bottom - 1, rectangle2.Right, viewItem.DisplayRectangle.Bottom + 1)
                                    );
                            }
                            messageInfo.CancelPreEvent = true;//取消事件 3
                        }
                        else if (this.CanExchangeRow)
                        {
                            this.m_OperationStyle = 1;
                            this.m_MouseDownSelectedIndex = i;
                            this.m_MouseDownPoint = e.Location;
                            this.m_CursorDefault = this.Cursor;
                            this.Cursor = Cursors.Hand;
                        }
                        break;
                    }
                }
                #endregion
                //
                #region 全选
                if (this.MultipleSelect && this.m_RowColumnViewItem.DisplayRectangle.Top <= e.Location.Y && this.m_RowColumnViewItem.DisplayRectangle.Bottom >= e.Location.Y)
                {
                    int iRow = this.SelectedIndex;
                    if (iRow >= 0)
                    {
                        int iColumn = this.m_ViewItemCollection[iRow].SelectedIndex;
                        //Console.WriteLine(this.m_ViewItemCollection[iRow].SelectedIndex);
                        this.SelectedIndices.Clear();
                        for (int i = 0; i < this.m_ViewItemCollection.Count; i++)
                        {
                            if (i != iRow) this.SelectedIndices.Add(i);
                        }
                        this.SelectedIndices.Add(iRow);
                        this.m_ViewItemCollection[iRow].SelectedIndex = iColumn;
                        //Console.WriteLine(this.m_ViewItemCollection[iRow].SelectedIndex);
                    }
                    else
                    {
                        for (int i = 0; i < this.m_ViewItemCollection.Count; i++)
                        {
                            this.SelectedIndices.Add(i);
                        }
                    }
                    return;
                }
                #endregion
                //
                #endregion
                //
                return;
            }
            else if (this.ShowColumn && this.ColumnViewItemsRectangle.Contains(e.Location))
            {
                #region 调节列宽 和 交换
                if (this.CanResizeColumnWidth && this.CanExchangeColumn)
                {
                    for (int i = this.m_RowColumnViewItem.TopViewItemIndex; i <= this.m_RowColumnViewItem.BottomViewItemIndex; i++)
                    {
                        viewItem = this.m_RowColumnViewItem.ViewItems[i];
                        if (viewItem.DisplayRectangle.Right - CONST_RESIZERECTANGLESIZE <= e.Location.X && viewItem.DisplayRectangle.Right + CONST_RESIZERECTANGLESIZE >= e.Location.X)
                        {
                            this.m_OperationStyle = 4;
                            this.m_MouseDownPoint = e.Location;
                            this.m_MouseDownSelectedIndex = i;
                            this.m_CursorDefault = this.Cursor;
                            this.Cursor = Cursors.SizeWE;
                            if (rectangle2.Height > CONST_SHOWSPLITELINEMINHEIGTH)
                            {
                                if (this.m_SplitLineForm == null) this.m_SplitLineForm = new SplitLineForm();
                                this.m_SplitLineForm.Show(
                                    DockStyle.Left,
                                    Rectangle.FromLTRB(viewItem.DisplayRectangle.Right - 1, viewItem.DisplayRectangle.Bottom, viewItem.DisplayRectangle.Right + 1, rectangle2.Bottom - 1)
                                    );
                            }
                            messageInfo.CancelPreEvent = true;//取消事件 4
                            return;
                        }
                        else if (viewItem.DisplayRectangle.Contains(e.Location))
                        {
                            this.m_OperationStyle = 0;
                            this.m_MouseDownSelectedIndex = i;
                            this.m_CursorDefault = this.Cursor;
                            this.Cursor = Cursors.Hand;
                            break;
                        }
                    }
                }
                else if (this.CanResizeColumnWidth)
                {
                    for (int i = this.m_RowColumnViewItem.TopViewItemIndex; i <= this.m_RowColumnViewItem.BottomViewItemIndex; i++)
                    {
                        viewItem = this.m_RowColumnViewItem.ViewItems[i];
                        if (viewItem == null) continue;
                        if (viewItem.DisplayRectangle.Right - CONST_RESIZERECTANGLESIZE <= e.Location.X && viewItem.DisplayRectangle.Right + CONST_RESIZERECTANGLESIZE >= e.Location.X)
                        {
                            this.m_OperationStyle = 4;
                            this.m_MouseDownPoint = e.Location;
                            this.m_MouseDownSelectedIndex = i;
                            this.m_CursorDefault = this.Cursor;
                            this.Cursor = Cursors.SizeWE;
                            if (rectangle2.Height > CONST_SHOWSPLITELINEMINHEIGTH)
                            {
                                if (this.m_SplitLineForm == null) this.m_SplitLineForm = new SplitLineForm();
                                this.m_SplitLineForm.Show(
                                    DockStyle.Left,
                                    Rectangle.FromLTRB(viewItem.DisplayRectangle.Right - 1, viewItem.DisplayRectangle.Bottom, viewItem.DisplayRectangle.Right + 1, rectangle2.Bottom - 1)
                                    );
                            }
                            messageInfo.CancelPreEvent = true;//取消事件 5
                            return;
                        }
                    }
                }
                else if (this.CanExchangeColumn)
                {
                    for (int i = this.m_RowColumnViewItem.TopViewItemIndex; i <= this.m_RowColumnViewItem.BottomViewItemIndex; i++)
                    {
                        viewItem = this.m_RowColumnViewItem.ViewItems[i];
                        if (viewItem.DisplayRectangle.Contains(e.Location))
                        {
                            this.m_OperationStyle = 0;
                            this.m_MouseDownSelectedIndex = i;
                            this.m_CursorDefault = this.Cursor;
                            this.Cursor = Cursors.Hand;
                            break;
                        }
                    }
                }
                else
                {

                }
                #endregion
                //
                pMessageChain = this.m_RowColumnViewItem as IMessageChain;
                if (pMessageChain != null)
                {
                    pMessageChain.SendMessage(messageInfo);
                }
                //
                return;
            }
            //
            if (this.Enabled && this.CanEdit)
            {
                ITextEditViewItem pTextEditViewItem = this.m_ViewItemCollection[this.SelectedIndex];
                if (pTextEditViewItem != null &&
                    pTextEditViewItem.CanEdit &&
                    pTextEditViewItem.InputRegionRectangle.Contains(e.Location))
                {
                    rectangle = ((IInputObject)this).InputRegionRectangle;
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
            if (rectangle2.Contains(e.Location))
            {
                //if (!this.Focused) this.Focus();
                ////
                for (int i = this.TopViewItemIndex; i <= this.BottomViewItemIndex; i++)
                {
                    viewItem = this.m_ViewItemCollection[i];
                    if (viewItem != null && viewItem.DisplayRectangle.Contains(e.Location))
                    {
                        pMessageChain = viewItem as IMessageChain;
                        if (pMessageChain != null)
                        {
                            pMessageChain.SendMessage(messageInfo);
                        }
                        //
                        this.SelectedIndex = i;
                        IRowViewItem pRowViewItem = viewItem as IRowViewItem;
                        if (pRowViewItem != null)
                        {
                            this.m_RowColumnViewItem.SelectedIndex = pRowViewItem.SelectedIndex;
                        }
                        //
                        break;
                    }
                }
            }
        }
        private void MSMouseUp(MessageInfo messageInfo)
        {
            MouseEventArgs e = messageInfo.MessageParameter as MouseEventArgs;
            if (e == null) return;
            //
            ISizeViewItem viewItem = null;
            IMessageChain pMessageChain;
            if (this.ShowColumn)
            {
                #region 调节列
                if (this.m_OperationStyle == 0)//CanExchangeColumn
                {
                    for (int i = this.m_RowColumnViewItem.TopViewItemIndex; i <= this.m_RowColumnViewItem.BottomViewItemIndex; i++)
                    {
                        viewItem = this.m_RowColumnViewItem.ViewItems[i];
                        if (viewItem.DisplayRectangle.Contains(e.Location))
                        {
                            this.m_RowColumnViewItem.ViewItems.ExchangeItem(this.m_MouseDownSelectedIndex, i);
                            break;
                        }
                    }
                    this.Cursor = this.m_CursorDefault;
                    //
                    this.m_OperationStyle = -1;
                    this.m_MouseDownPoint = Point.Empty;
                    this.m_MouseDownSelectedIndex = -1;
                    this.m_CursorDefault = this.Cursor;
                    //
                    return;
                }
                else if (this.m_OperationStyle == 4)//CanResizeColumnWidth
                {
                    ColumnViewItem columnViewItem = this.m_RowColumnViewItem.ViewItems[this.m_MouseDownSelectedIndex];
                    if (columnViewItem != null) { columnViewItem.Width += e.Location.X - this.m_MouseDownPoint.X; }
                    if (this.m_SplitLineForm != null)
                    {
                        this.m_SplitLineForm.Close();
                        this.m_SplitLineForm = null;
                    }
                    this.Cursor = this.m_CursorDefault;
                    //
                    this.m_OperationStyle = -1;
                    this.m_MouseDownPoint = Point.Empty;
                    this.m_MouseDownSelectedIndex = -1;
                    this.m_CursorDefault = Cursors.Default;
                    //
                    messageInfo.CancelPreEvent = true;//取消事件 1
                    return;
                }
                #endregion
            }
            //
            if (this.ShowRowHeader)
            {
                #region 调节行
                if (this.m_OperationStyle == 1)//CanExchangeRow
                {
                    Rectangle rectangle = this.RowHeaderRectangle;
                    for (int i = this.TopViewItemIndex; i <= this.BottomViewItemIndex; i++)
                    {
                        viewItem = this.m_ViewItemCollection[i];
                        if (viewItem != null &&
                            rectangle.Left <= e.Location.X && rectangle.Right >= e.Location.X &&
                            viewItem.DisplayRectangle.Top <= e.Location.Y && viewItem.DisplayRectangle.Bottom >= e.Location.Y)
                        {
                            this.m_ViewItemCollection.ExchangeItem(this.m_MouseDownSelectedIndex, i);
                            break;
                        }
                    }
                    this.Cursor = this.m_CursorDefault;
                    //
                    this.m_OperationStyle = -1;
                    this.m_MouseDownPoint = Point.Empty;
                    this.m_MouseDownSelectedIndex = -1;
                    this.m_CursorDefault = Cursors.Default;
                    //
                    return;
                }
                else if (this.m_OperationStyle == 2)//CanResizeRowHeaderWidth
                {
                    Rectangle rectangle = this.RowHeaderRectangle;
                    Rectangle rectangle2 = this.ViewItemsRectangle;
                    int iW = this.RowHeaderWidth + e.Location.X - this.m_MouseDownPoint.X;
                    if (iW > rectangle2.Right - rectangle.Left - CONST_MINROWHEADERWIDTH)
                    {
                        iW = rectangle2.Right - rectangle.Left - CONST_MINROWHEADERWIDTH;
                    }
                    this.RowHeaderWidth = iW;
                    if (this.m_SplitLineForm != null)
                    {
                        this.m_SplitLineForm.Close();
                        this.m_SplitLineForm = null;
                    }
                    this.Cursor = this.m_CursorDefault;
                    //
                    this.m_OperationStyle = -1;
                    this.m_MouseDownPoint = Point.Empty;
                    this.m_MouseDownSelectedIndex = -1;
                    this.m_CursorDefault = Cursors.Default;
                    //
                    messageInfo.CancelPreEvent = true;//取消事件 2
                    return;
                }
                else if (this.m_OperationStyle == 3)//CanResizeRowHeaderHeight
                {
                    Rectangle rectangle = this.ColumnViewItemsRectangle;
                    Rectangle rectangle2 = this.ViewItemsRectangle;
                    int iH = this.m_RowColumnViewItem.Height + e.Location.Y - this.m_MouseDownPoint.Y;
                    if (iH > rectangle2.Bottom - rectangle.Top - CONST_MINROWHEADERHEIGHT)
                    {
                        iH = rectangle2.Bottom - rectangle.Top - CONST_MINROWHEADERHEIGHT;
                    }
                    if (this.m_RowColumnViewItem.Height != iH)
                    {
                        this.m_RowColumnViewItem.Height = iH;
                        this.Refresh();
                    }
                    if (this.m_SplitLineForm != null)
                    {
                        this.m_SplitLineForm.Close();
                        this.m_SplitLineForm = null;
                    }
                    this.Cursor = this.m_CursorDefault;
                    //
                    this.m_OperationStyle = -1;
                    this.m_MouseDownPoint = Point.Empty;
                    this.m_MouseDownSelectedIndex = -1;
                    this.m_CursorDefault = Cursors.Default;
                    //
                    messageInfo.CancelPreEvent = true;//取消事件 3
                    return;
                }
                else if (this.m_OperationStyle == 5)//CanResizeRowHeight
                {
                    viewItem = this.m_ViewItemCollection[this.m_MouseDownSelectedIndex];
                    if (viewItem != null)
                    {
                        Rectangle rectangle = viewItem.DisplayRectangle;
                        Rectangle rectangle2 = this.ViewItemsRectangle;
                        int iH = viewItem.Height + e.Location.Y - this.m_MouseDownPoint.Y;
                        if (iH > rectangle2.Bottom - rectangle.Top)
                        {
                            iH = rectangle2.Bottom - rectangle.Top;
                        }
                        viewItem.Height = iH;
                        this.Refresh();
                    }
                    if (this.m_SplitLineForm != null)
                    {
                        this.m_SplitLineForm.Close();
                        this.m_SplitLineForm = null;
                    }
                    this.Cursor = this.m_CursorDefault;
                    //
                    this.m_OperationStyle = -1;
                    this.m_MouseDownPoint = Point.Empty;
                    this.m_MouseDownSelectedIndex = -1;
                    this.m_CursorDefault = Cursors.Default;
                    //
                    messageInfo.CancelPreEvent = true;//取消事件 4
                    return;
                }
                else if (this.m_OperationStyle == 6)//CanResizeColumnTitleHeight
                {
                    //Rectangle rectangle = this.ColumnTitleViewItemsRectangle;
                    Rectangle rectangle2 = this.ViewItemsRectangle;
                    int iH = this.m_LockRowColumnTitleViewItem.Height + e.Location.Y - this.m_MouseDownPoint.Y;//this.m_LockRowColumnTitleViewItem.Height key
                    //Console.WriteLine("1." + (rectangle2.Bottom - rectangle2.Top - CONST_MINROWHEADERHEIGHT) + " - " + iH);
                    if (iH > rectangle2.Bottom - rectangle2.Top - CONST_MINROWHEADERHEIGHT)
                    {
                        iH = rectangle2.Bottom - rectangle2.Top - CONST_MINROWHEADERHEIGHT;
                    }
                    //Console.WriteLine("2." + (rectangle2.Bottom - rectangle2.Top - CONST_MINROWHEADERHEIGHT) + " - " + iH);
                    if (this.ColumnTitleHeight != iH)
                    {
                        this.m_LockRowColumnTitleViewItem.Height = iH; 
                        this.Refresh();
                    }
                    if (this.m_SplitLineForm != null)
                    {
                        this.m_SplitLineForm.Close();
                        this.m_SplitLineForm = null;
                    }
                    this.Cursor = this.m_CursorDefault;
                    //
                    this.m_OperationStyle = -1;
                    this.m_MouseDownPoint = Point.Empty;
                    this.m_MouseDownSelectedIndex = -1;
                    this.m_CursorDefault = Cursors.Default;
                    //
                    messageInfo.CancelPreEvent = true;//取消事件 0
                    return;
                }
                #endregion
            }
            //
            if (this.ShowColumnTitle)
            {
                pMessageChain = this.m_LockRowColumnTitleViewItem as IMessageChain;
                if (pMessageChain != null)
                {
                    pMessageChain.SendMessage(messageInfo);
                }
            }
            if (this.ShowColumn)
            {
                pMessageChain = this.m_RowColumnViewItem as IMessageChain;
                if (pMessageChain != null)
                {
                    pMessageChain.SendMessage(messageInfo);
                }
            }
            //
            for (int i = this.TopViewItemIndex; i <= this.BottomViewItemIndex; i++)
            {
                viewItem = this.m_ViewItemCollection[i];
                if (viewItem != null)
                {
                    pMessageChain = viewItem as IMessageChain;
                    if (pMessageChain != null)
                    {
                        pMessageChain.SendMessage(messageInfo);
                    }
                }
            }
            //
            Rectangle rectangle3 = this.RowHeaderRectangle;
            rectangle3 = Rectangle.FromLTRB(rectangle3.Left, rectangle3.Top + this.ColumnHeight, rectangle3.Right, rectangle3.Bottom);
            if (this.ShowRowHeader && rectangle3.Contains(e.Location))
            {
                this.OnRowHeaderMouseClick(e);
            }
            //
            this.m_OperationStyle = -1;
            this.Cursor = this.m_CursorDefault;
        }
        private void MSMouseMove(MessageInfo messageInfo)
        {
            MouseEventArgs e = messageInfo.MessageParameter as MouseEventArgs;
            if (e == null) return;
            //
            ViewItem viewItem = null;
            IMessageChain pMessageChain;
            if (this.ShowColumn)
            {
                #region 调节列
                if (this.m_OperationStyle == 4)//CanResizeColumnWidth
                {
                    if (this.m_SplitLineForm != null)
                    {
                        ColumnViewItem columnViewItem = this.m_RowColumnViewItem.ViewItems[this.m_MouseDownSelectedIndex];
                        if (columnViewItem != null)
                        {
                            this.m_SplitLineForm.Location =
                                this.PointToScreen
                                (
                                new Point
                                    (
                                    (columnViewItem.Width - this.m_MouseDownPoint.X + e.Location.X) >= CONST_MINCOLUMNVIEWITEMWIDTH ? e.Location.X : columnViewItem.DisplayRectangle.Left + CONST_MINCOLUMNVIEWITEMWIDTH,
                                    columnViewItem.DisplayRectangle.Bottom + 1
                                    )
                                );
                        }
                    }
                    messageInfo.CancelPreEvent = true;//取消事件 1
                    return;
                }
                #endregion
            }
            //
            if (this.ShowRowHeader)
            {
                #region 调节行
                if (this.m_OperationStyle == 2)//CanResizeRowHeaderWidth
                {
                    if (this.m_SplitLineForm != null)
                    {
                        Rectangle rectangle = this.RowHeaderRectangle;
                        Rectangle rectangle2 = this.ViewItemsRectangle;
                        int iX = e.Location.X;
                        int iW = this.RowHeaderWidth + iX - this.m_MouseDownPoint.X;
                        if (iW < CONST_MINROWHEADERWIDTH)
                        {
                            iX = rectangle.Left + CONST_MINROWHEADERWIDTH;
                        }
                        else if (iW > rectangle2.Right - rectangle.Left - CONST_MINROWHEADERWIDTH)
                        {
                            iX = rectangle2.Right - CONST_MINROWHEADERWIDTH;
                        }
                        this.m_SplitLineForm.Location = this.PointToScreen(new Point(iX, rectangle.Top));
                    }
                    messageInfo.CancelPreEvent = true;//取消事件 2
                    return;
                }
                else if (this.m_OperationStyle == 3)//CanResizeRowHeaderHeight
                {
                    if (this.m_SplitLineForm != null)
                    {
                        Rectangle rectangle = this.ColumnViewItemsRectangle;
                        Rectangle rectangle2 = this.ViewItemsRectangle;
                        int iY = e.Location.Y;
                        int iH = this.m_RowColumnViewItem.Height + iY - this.m_MouseDownPoint.Y;
                        if (iH < CONST_MINROWHEADERHEIGHT)
                        {
                            iY = rectangle.Top + CONST_MINROWHEADERHEIGHT;
                        }
                        else if (iH > rectangle2.Bottom - rectangle.Top - CONST_MINROWHEADERHEIGHT)
                        {
                            iY = rectangle2.Bottom - CONST_MINROWHEADERHEIGHT;
                        }
                        this.m_SplitLineForm.Location = this.PointToScreen(new Point(this.RowHeaderRectangle.Left, iY));
                    }
                    messageInfo.CancelPreEvent = true;//取消事件 3
                    return;
                }
                else if (this.m_OperationStyle == 5)//CanResizeRowHeight
                {
                    if (this.m_SplitLineForm != null)
                    {
                        viewItem = this.m_ViewItemCollection[this.m_MouseDownSelectedIndex];
                        if (viewItem != null)
                        {
                            Rectangle rectangle = viewItem.DisplayRectangle;
                            Rectangle rectangle2 = this.ViewItemsRectangle;
                            int iY = e.Location.Y;
                            int iH = viewItem.DisplayRectangle.Height + iY - this.m_MouseDownPoint.Y;
                            if (iH < CONST_MINROWHEADERHEIGHT)
                            {
                                iY = rectangle.Top + CONST_MINROWHEADERHEIGHT;
                            }
                            else if (iH > rectangle2.Bottom - rectangle.Top)
                            {
                                iY = rectangle2.Bottom;
                            }
                            this.m_SplitLineForm.Location = this.PointToScreen(new Point(this.RowHeaderRectangle.Right, iY));
                        }
                    }
                    messageInfo.CancelPreEvent = true;//取消事件 4
                    return;
                }
                else if (this.m_OperationStyle == 6)//CanResizeColumnTitleHeight
                {
                    if (this.m_SplitLineForm != null)
                    {
                        Rectangle rectangle = this.ColumnTitleViewItemsRectangle;
                        Rectangle rectangle2 = this.ViewItemsRectangle;
                        int iY = e.Location.Y;
                        int iH = this.ColumnTitleHeight + iY - this.m_MouseDownPoint.Y;
                        if (iH < CONST_MINROWHEADERHEIGHT)
                        {
                            iY = rectangle.Top + CONST_MINROWHEADERHEIGHT;
                        }
                        else if (iH > rectangle2.Bottom - rectangle.Top - CONST_MINROWHEADERHEIGHT)
                        {
                            iY = rectangle2.Bottom - CONST_MINROWHEADERHEIGHT;
                        }
                        this.m_SplitLineForm.Location = this.PointToScreen(new Point(this.RowHeaderRectangle.Left, iY));
                    }
                    messageInfo.CancelPreEvent = true;//取消事件 0
                    return;
                }
                #endregion
            }
            //
            if (this.ShowColumnTitle)
            {
                pMessageChain = this.m_LockRowColumnTitleViewItem as IMessageChain;
                if (pMessageChain != null)
                {
                    pMessageChain.SendMessage(messageInfo);
                }
            }
            if (this.ShowColumn)
            {
                pMessageChain = this.m_RowColumnViewItem as IMessageChain;
                if (pMessageChain != null)
                {
                    pMessageChain.SendMessage(messageInfo);
                }
            }
            //
            for (int i = this.TopViewItemIndex; i <= this.BottomViewItemIndex; i++)
            {
                viewItem = this.m_ViewItemCollection[i];
                if (viewItem != null)
                {
                    pMessageChain = viewItem as IMessageChain;
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
            IMessageChain pMessageChain;
            if (this.ShowColumnTitle && this.ColumnTitleViewItemsRectangle.Contains(e.Location))
            {
                pMessageChain = this.m_LockRowColumnTitleViewItem as IMessageChain;
                if (pMessageChain != null)
                {
                    pMessageChain.SendMessage(messageInfo);
                }
                //
                return;
            }
            else if (this.ShowColumn && this.ColumnViewItemsRectangle.Contains(e.Location))
            {
                pMessageChain = this.m_RowColumnViewItem as IMessageChain;
                if (pMessageChain != null)
                {
                    pMessageChain.SendMessage(messageInfo);
                }
                return;
            }
            //
            if (this.RowViewItemsRectangle.Contains(e.Location))
            {
                ViewItem viewItem = null;
                for (int i = this.TopViewItemIndex; i <= this.BottomViewItemIndex; i++)
                {
                    viewItem = this.m_ViewItemCollection[i];
                    if (viewItem != null && viewItem.DisplayRectangle.Contains(e.Location))
                    {
                        pMessageChain = viewItem as IMessageChain;
                        if (pMessageChain != null)
                        {
                            pMessageChain.SendMessage(messageInfo);
                        }
                        //
                        break;
                    }
                }
                //
                return;
            }
        }
        private void MSMouseDoubleClick(MessageInfo messageInfo)
        {
            MouseEventArgs e = messageInfo.MessageParameter as MouseEventArgs;
            if (e == null) return;
            //
            IMessageChain pMessageChain;
            if (this.ShowColumnTitle && this.ColumnTitleViewItemsRectangle.Contains(e.Location))
            {
                pMessageChain = this.m_LockRowColumnTitleViewItem as IMessageChain;
                if (pMessageChain != null)
                {
                    pMessageChain.SendMessage(messageInfo);
                }
                //
                return;
            }
            else if (this.ShowColumn && this.ColumnViewItemsRectangle.Contains(e.Location))
            {
                pMessageChain = this.m_RowColumnViewItem as IMessageChain;
                if (pMessageChain != null)
                {
                    pMessageChain.SendMessage(messageInfo);
                }
                return;
            }
            //
            if (this.RowViewItemsRectangle.Contains(e.Location))
            {
                ViewItem viewItem = null;
                for (int i = this.TopViewItemIndex; i <= this.BottomViewItemIndex; i++)
                {
                    viewItem = this.m_ViewItemCollection[i];
                    if (viewItem != null && viewItem.DisplayRectangle.Contains(e.Location))
                    {
                        pMessageChain = viewItem as IMessageChain;
                        if (pMessageChain != null)
                        {
                            pMessageChain.SendMessage(messageInfo);
                        }
                        //
                        break;
                    }
                }
                return;
            }
            //
            Rectangle rectangle = this.RowHeaderRectangle;
            rectangle = Rectangle.FromLTRB(rectangle.Left, rectangle.Top + this.ColumnHeight, rectangle.Right, rectangle.Bottom);
            if (this.ShowRowHeader && rectangle.Contains(e.Location))
            {
                this.OnRowHeaderMouseDoubleClick(e);
            }
        }
        private void MSMouseEnter(MessageInfo messageInfo)
        {

        }
        private void MSMouseLeave(MessageInfo messageInfo)
        {
            IMessageChain pMessageChain;
            if (this.ShowColumnTitle)
            {
                pMessageChain = this.m_LockRowColumnTitleViewItem as IMessageChain;
                if (pMessageChain != null)
                {
                    pMessageChain.SendMessage(messageInfo);
                }
            }
            if (this.ShowColumn)
            {
                pMessageChain = this.m_RowColumnViewItem as IMessageChain;
                if (pMessageChain != null)
                {
                    pMessageChain.SendMessage(messageInfo);
                }
            }
            //
            ViewItem viewItem = null;
            for (int i = this.TopViewItemIndex; i <= this.BottomViewItemIndex; i++)
            {
                viewItem = this.m_ViewItemCollection[i];
                if (viewItem != null)
                {
                    pMessageChain = viewItem as IMessageChain;
                    if (pMessageChain != null)
                    {
                        pMessageChain.SendMessage(messageInfo);
                    }
                }
            }
        }
        private void MSEnabledChanged(MessageInfo messageInfo)
        {
            IMessageChain pMessageChain;
            if (this.ShowColumnTitle)
            {
                pMessageChain = this.m_LockRowColumnTitleViewItem as IMessageChain;
                if (pMessageChain != null)
                {
                    pMessageChain.SendMessage(messageInfo);
                }
            }
            if (this.ShowColumn)
            {
                pMessageChain = this.m_RowColumnViewItem as IMessageChain;
                if (pMessageChain != null)
                {
                    pMessageChain.SendMessage(messageInfo);
                }
            }
            //
            ViewItem viewItem = null;
            for (int i = this.TopViewItemIndex; i <= this.BottomViewItemIndex; i++)
            {
                viewItem = this.m_ViewItemCollection[i];
                if (viewItem != null)
                {
                    pMessageChain = viewItem as IMessageChain;
                    if (pMessageChain != null)
                    {
                        pMessageChain.SendMessage(messageInfo);
                    }
                }
            }
        }
        private void MSVisibleChanged(MessageInfo messageInfo)
        {
            IMessageChain pMessageChain;
            if (this.ShowColumnTitle)
            {
                pMessageChain = this.m_LockRowColumnTitleViewItem as IMessageChain;
                if (pMessageChain != null)
                {
                    pMessageChain.SendMessage(messageInfo);
                }
            }
            if (this.ShowColumn)
            {
                pMessageChain = this.m_RowColumnViewItem as IMessageChain;
                if (pMessageChain != null)
                {
                    pMessageChain.SendMessage(messageInfo);
                }
            }
            //
            ViewItem viewItem = null;
            for (int i = this.TopViewItemIndex; i <= this.BottomViewItemIndex; i++)
            {
                viewItem = this.m_ViewItemCollection[i];
                if (viewItem != null)
                {
                    pMessageChain = viewItem as IMessageChain;
                    if (pMessageChain != null)
                    {
                        pMessageChain.SendMessage(messageInfo);
                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// 根据关系树建立多表头
        /// </summary>
        /// <param name="nodeViewItemArray">表头节点数组</param>
        public void FillColumnTitle(NodeViewItem[] nodeViewItemArray)
        {
            int iIndex = 0;
            this.TitleViewItems.Clear();
            foreach (NodeViewItem one in nodeViewItemArray)
            {
                if (one.NodeViewItems.Count <= 0)
                {
                    this.TitleViewItems.Add(new ColumnTitleViewItem(this.ColumnViewItems[iIndex], Name = one.Name, Text = one.Text));
                    iIndex++;
                }
                else if (one.NodeViewItems.Count == 1)
                {
                    VRowColumnTitleViewItem vRowColumnTitleViewItem = new VRowColumnTitleViewItem(one.Name + "_VRowColumnTitleViewItem");
                    this.TitleViewItems.Add(vRowColumnTitleViewItem);
                    vRowColumnTitleViewItem.ViewItems.Add(new TitleViewItem(Name = one.Name, Text = one.Text));
                    //
                    this.FillColumnTitle_DG(vRowColumnTitleViewItem.ViewItems, one.NodeViewItems, ref iIndex);
                }
                else
                {
                    VRowColumnTitleViewItem vRowColumnTitleViewItem = new VRowColumnTitleViewItem(one.Name + "_VRowColumnTitleViewItem");
                    this.TitleViewItems.Add(vRowColumnTitleViewItem);
                    vRowColumnTitleViewItem.ViewItems.Add(new TitleViewItem(Name = one.Name, Text = one.Text));
                    //
                    RowColumnTitleViewItem rowColumnTitleViewItem = new RowColumnTitleViewItem(vRowColumnTitleViewItem.Name + "_RowColumnTitleViewItem");
                    vRowColumnTitleViewItem.ViewItems.Add(rowColumnTitleViewItem);
                    this.FillColumnTitle_DG(rowColumnTitleViewItem.ViewItems, one.NodeViewItems, ref iIndex);
                }
            }
        }
        private void FillColumnTitle_DG(TitleViewItemCollection viewItems, NodeViewItemCollection nodeViewItems, ref int iIndex)
        {
            foreach (NodeViewItem one in nodeViewItems)
            {
                if (one.NodeViewItems.Count <= 0)
                {
                    viewItems.Add(new ColumnTitleViewItem(this.ColumnViewItems[iIndex], Name = one.Name, Text = one.Text));
                    iIndex++;
                }
                else if (one.NodeViewItems.Count == 1)
                {
                    VRowColumnTitleViewItem vRowColumnTitleViewItem = new VRowColumnTitleViewItem(one.Name + "_VRowColumnTitleViewItem");
                    viewItems.Add(vRowColumnTitleViewItem);
                    vRowColumnTitleViewItem.ViewItems.Add(new TitleViewItem(Name = one.Name, Text = one.Text));
                    //
                    this.FillColumnTitle_DG(vRowColumnTitleViewItem.ViewItems, one.NodeViewItems, ref iIndex);
                }
                else
                {
                    VRowColumnTitleViewItem vRowColumnTitleViewItem = new VRowColumnTitleViewItem(one.Name + "_VRowColumnTitleViewItem");
                    viewItems.Add(vRowColumnTitleViewItem);
                    vRowColumnTitleViewItem.ViewItems.Add(new TitleViewItem(Name = one.Name, Text = one.Text));
                    //
                    RowColumnTitleViewItem rowColumnTitleViewItem = new RowColumnTitleViewItem(vRowColumnTitleViewItem.Name + "_RowColumnTitleViewItem");
                    vRowColumnTitleViewItem.ViewItems.Add(rowColumnTitleViewItem);
                    this.FillColumnTitle_DG(rowColumnTitleViewItem.ViewItems, one.NodeViewItems, ref iIndex);
                }
            }
        }

        //
        protected virtual void OnDraw(PaintEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderViewItemList(
                new ObjectRenderEventArgs(e.Graphics, this, this.DisplayRectangle));
        }

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

        protected virtual void OnRowHeaderMouseClick(MouseEventArgs e)
        {
            if (this.RowHeaderMouseClick != null) { this.RowHeaderMouseClick(this, e); }
        }

        protected virtual void OnRowHeaderMouseDoubleClick(MouseEventArgs e)
        {
            if (this.RowHeaderMouseDoubleClick != null) { this.RowHeaderMouseDoubleClick(this, e); }
        }

        protected virtual void OnRowHeaderItemDrawing(RowHeaderItemDrawEventArgs e)
        {
            if (this.RowHeaderItemDrawing != null) { this.RowHeaderItemDrawing(this, e); }
        }

    }
}
