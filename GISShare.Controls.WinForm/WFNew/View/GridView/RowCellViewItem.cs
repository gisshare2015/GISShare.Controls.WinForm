using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew.View
{
    public class RowCellViewItem : TextEditViewItem, 
        IRowViewItem, 
        IViewListEnumerator,
        IRowCellViewItem,
        IRowHeaderItem, IRowHeaderItemHelper,
        IOwner, ISetOwnerHelper,
        IViewItemOwner, IViewItemOwner2, IViewList
    {
        private const int CONST_MINNODEWIDTH = 18;
        private const int CONST_MINNODEHEIGHT = 18;
        private const int CONST_ROWIDOFFSET_X = 5;

        internal RowCellViewItem(RowCellViewStyle rowCellViewStyle)
        {
            this.m_ViewItemCollection = new CellViewItemCollection(this);
            this.m_eRowCellViewStyle = rowCellViewStyle;
        }

        #region IOwner
        IOwner m_pOwner = null;
        public IOwner pOwner
        {
            get { return m_pOwner; }
        }

        public void Refresh()
        {
            if (this.pOwner != null)
            {
                this.pOwner.Invalidate(this.DisplayRectangle);
                // this.pOwner.Refresh();
            }
        }

        public void Invalidate(Rectangle rectangle)
        {
            if (this.pOwner != null) this.pOwner.Invalidate(rectangle);
        }

        public Point PointToClient(Point point)
        {
            if (this.pOwner != null) return this.pOwner.PointToClient(point);
            return Point.Empty;
        }

        public Point PointToScreen(Point point)
        {
            if (this.pOwner != null) return this.pOwner.PointToScreen(point);
            return Point.Empty;
        }
        #endregion

        #region ISetOwnerHelper
        bool ISetOwnerHelper.SetOwner(IOwner owner)
        {
            if (this.m_pOwner == owner) return false;
            //
            //
            //
            this.m_pOwner = owner;
            ((IReset)this).Reset();
            //
            //
            //
            return true;
        }
        #endregion

        #region IViewItemOwner
        Rectangle IViewItemOwner.ViewItemsRectangle
        {
            get
            {
                IViewItemOwner pViewItemOwner = this.pOwner as IViewItemOwner;
                return pViewItemOwner == null ? this.DisplayRectangle : pViewItemOwner.ViewItemsRectangle;
            }
        }
        #endregion

        #region IViewItemOwner2
        IViewItemOwner2 IViewItemOwner2.GetTopViewItemOwner()
        {
            IViewItemOwner2 pViewItemOwner2 = this.pOwner as IViewItemOwner2;
            return pViewItemOwner2 == null ? null : pViewItemOwner2.GetTopViewItemOwner();
        }
        #endregion

        #region IRowViewItem
        private int m_SelectedIndex = -1;
        [Browsable(false), DefaultValue(-1), Description("选择ViewItem索引"), Category("属性")]
        public int SelectedIndex
        {
            get { return m_SelectedIndex; }
            set
            {
                if (this.ViewItems.Count <= 0) value = -1;
                else if (value > this.ViewItems.Count) value = this.ViewItems.Count - 1;
                //
                if (this.m_SelectedIndex == value) return;
                m_SelectedIndex = value;
                for (int i = 0; i < this.ViewItems.Count; i++)
                {
                    ISetViewItemHelper pSetViewItemHelper = this.ViewItems[i] as ISetViewItemHelper;
                    if (pSetViewItemHelper != null)
                    {
                        pSetViewItemHelper.SetViewParameterStyle(i == this.m_SelectedIndex ? ViewParameterStyle.eFocused : ViewParameterStyle.eNone);
                    }
                }
            }
        }

        bool m_ShowBaseItemState = true;
        [Browsable(false), DefaultValue(true), Description("携带子项时是否显示背景状态"), Category("属性")]
        public bool ShowBaseItemState
        {
            get { return m_ShowBaseItemState; }
            set { m_ShowBaseItemState = value; }
        }

        IFlexibleList IRowViewItem.Items { get { return this.ViewItems; } }
        #endregion

        #region IRowCellViewItem
        RowCellViewStyle m_eRowCellViewStyle = RowCellViewStyle.eSystemRow;
        [Browsable(false), DefaultValue(typeof(RowCellViewStyle), "eSystemRow"), Description("行类型"), Category("属性")]        
        public RowCellViewStyle eRowCellViewStyle
        {
            get { return m_eRowCellViewStyle; }
        }

        public bool ExchangeCellViewItem(int index1, int index2) { return this.ViewItems.ExchangeItem(index1, index2); }
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
        protected internal int _TopViewItemIndex = 0;
        [Browsable(false), Description("最左边ViewItem索引号"), Category("布局")]
        public int TopViewItemIndex
        {
            get { return this._TopViewItemIndex; }
        }

        protected internal int _BottomViewItemIndex = 0;
        [Browsable(false), Description("最右边ViewItem索引号"), Category("布局")]
        public int BottomViewItemIndex
        {
            get { return _BottomViewItemIndex; }
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

        #region IRowHeaderItem
        int m_RowIndex = -1;
        int IRowHeaderItem.RowIndex
        {
            get { return this.m_RowIndex; }
        }

        Rectangle m_RowHeaderRectangle;
        Rectangle IRowHeaderItem.RowHeaderRectangle
        {
            get { return this.m_RowHeaderRectangle; }
        }

        ViewParameterStyle IRowHeaderItem.eViewParameterStyle
        {
            get { return ((IViewItem)this).eViewParameterStyle; }
        }
        #endregion

        #region ISetRowHeaderItemHelper
        void IRowHeaderItemHelper.SetIndex(int index)
        {
            this.m_RowIndex = index;
        }

        void IRowHeaderItemHelper.SetRowHeaderRectangle(Rectangle rectangle)
        {
            this.m_RowHeaderRectangle = rectangle;
        }

        void IRowHeaderItemHelper.DrawRowHeader(System.Windows.Forms.PaintEventArgs e, bool ShowRowIndex, int iRowHeaderStartID)
        {
            WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRowHeaderItem
                        (
                        new ObjectRenderEventArgs(
                            e.Graphics, this, Rectangle.FromLTRB(e.ClipRectangle.Left, e.ClipRectangle.Top, e.ClipRectangle.Right - 1, e.ClipRectangle.Bottom - 1))
                        );
            //
            if (ShowRowIndex)
            {
                string strID = (this.m_RowIndex + iRowHeaderStartID).ToString();
                SizeF size = e.Graphics.MeasureString(strID, this.Font);
                int iH = (int)size.Height + 1;
                int iW = (int)size.Width + 1;
                WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonText
                    (
                    new TextRenderEventArgs(
                        e.Graphics,
                        this,
                        true,
                        false,
                        strID,
                        this.ForeColor,
                        this.Font,
                        new Rectangle(((IViewItem)this).eViewParameterStyle == ViewParameterStyle.eFocused ? CONST_ROWIDOFFSET_X + (e.ClipRectangle.Left + e.ClipRectangle.Right - iW) / 2 : (e.ClipRectangle.Left + e.ClipRectangle.Right - iW) / 2, (e.ClipRectangle.Top + e.ClipRectangle.Bottom - iH) / 2, iW, iH),
                        new StringFormat() { Trimming = StringTrimming.EllipsisCharacter })
                    );
            }
        }
        #endregion

        #region 属性
        private CellViewItemCollection m_ViewItemCollection;
        [Browsable(true),
        Editor(typeof(GISShare.Controls.WinForm.WFNew.View.Design.ViewItemCollectionEditer), typeof(System.Drawing.Design.UITypeEditor)),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
        Description("其所携带的子项集合"),
        Category("子项")]
        public CellViewItemCollection ViewItems
        {
            get { return m_ViewItemCollection; }
        }
        #endregion

        protected override bool RefreshBaseItemState
        {
            get
            {
                return false;
            }
        }

        public override bool CanEdit
        {
            get
            {
                if (this.ViewItems.Count > 0)
                {
                    ITextEditViewItem pTextEditViewItem = this.ViewItems[this.SelectedIndex] as ITextEditViewItem;
                    return pTextEditViewItem == null ? false : pTextEditViewItem.CanEdit;
                }
                else
                {
                    return base.CanEdit;
                }
            }
            set
            {
                base.CanEdit = value;
            }
        }

        protected internal override object ITextEditViewItem_EditObject
        {
            get
            {
                if (this.ViewItems.Count > 0 && this.SelectedIndex >= 0)
                {
                    IInputObject pInputObject = this.ViewItems[this.SelectedIndex] as IInputObject;
                    if (pInputObject != null)
                    {
                        return pInputObject;
                    }
                    else
                    {
                        return base.ITextEditViewItem_EditObject;
                    }
                }
                else
                {
                    return base.ITextEditViewItem_EditObject;
                }
            }
        }

        protected internal override string IInputObject_InputText
        {
            get
            {
                if (this.ViewItems.Count > 0 && this.SelectedIndex >= 0)
                {
                    IInputObject pInputObject = this.ViewItems[this.SelectedIndex] as IInputObject;
                    if (pInputObject != null)
                    {
                        return pInputObject.InputText;
                    }
                    else
                    {
                        return base.IInputObject_InputText;
                    }
                }
                else
                {
                    return base.IInputObject_InputText;
                }
            }
            set
            {
                if (this.ViewItems.Count > 0 && this.SelectedIndex >= 0)
                {
                    IInputObject pInputObject = this.ViewItems[this.SelectedIndex] as IInputObject;
                    if (pInputObject != null)
                    {
                        pInputObject.InputText = value;
                    }
                    else
                    {
                        base.IInputObject_InputText = value;
                    }
                }
                else
                {
                    base.IInputObject_InputText = value;
                }
            }
        }

        protected internal override Color IInputObject_ForeColor
        {
            get
            {
                if (this.ViewItems.Count > 0 && this.SelectedIndex >= 0)
                {
                    IInputObject pInputObject = this.ViewItems[this.SelectedIndex] as IInputObject;
                    if (pInputObject != null)
                    {
                        return pInputObject.InputForeColor;
                    }
                    else
                    {
                        return base.IInputObject_ForeColor;
                    }
                }
                else
                {
                    return base.IInputObject_ForeColor;
                }
            }
        }

        protected internal override Font IInputObject_InputFont
        {
            get
            {
                if (this.ViewItems.Count > 0 && this.SelectedIndex >= 0)
                {
                    IInputObject pInputObject = this.ViewItems[this.SelectedIndex] as IInputObject;
                    if (pInputObject != null)
                    {
                        return pInputObject.InputFont;
                    }
                    else
                    {
                        return base.IInputObject_InputFont;
                    }
                }
                else
                {
                    return base.IInputObject_InputFont;
                }
            }
        }

        protected internal override Rectangle IInputObject_InputRegionRectangle
        {
            get
            {
                if (this.ViewItems.Count > 0 && this.SelectedIndex >= 0)
                {
                    IInputObject pInputObject = this.ViewItems[this.SelectedIndex] as IInputObject;
                    if (pInputObject != null)
                    {
                        return pInputObject.InputRegionRectangle;
                    }
                    else
                    {
                        return base.IInputObject_InputRegionRectangle;
                    }
                }
                else
                {
                    return base.IInputObject_InputRegionRectangle;
                }
            }
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
        }
        private void MSViewInfo(MessageInfo messageInfo)
        {

        }
        private void MSPaint(MessageInfo messageInfo)
        {

        }
        private void MSLostFocus(MessageInfo messageInfo)
        {
            ViewItem viewItem = this.ViewItems[this.SelectedIndex];
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
            ViewItem viewItem = this.ViewItems[this.SelectedIndex];
            if (viewItem != null)
            {
                IMessageChain pMessageChain = viewItem as IMessageChain;
                if (pMessageChain != null)
                {
                    pMessageChain.SendMessage(messageInfo);
                }
            }
        }
        private void MSKeyUp(MessageInfo messageInfo)
        {
            ViewItem viewItem = this.ViewItems[this.SelectedIndex];
            if (viewItem != null)
            {
                IMessageChain pMessageChain = viewItem as IMessageChain;
                if (pMessageChain != null)
                {
                    pMessageChain.SendMessage(messageInfo);
                }
            }
        }
        private void MSKeyPress(MessageInfo messageInfo)
        {
            ViewItem viewItem = this.ViewItems[this.SelectedIndex];
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
            ViewItem viewItem = this.ViewItems[this.SelectedIndex];
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
            MouseEventArgs mouseEventArgs = messageInfo.MessageParameter as MouseEventArgs;
            if (mouseEventArgs != null)
            {
                ICellViewItem viewItem;
                for (int i = this.TopViewItemIndex; i <= this.BottomViewItemIndex; i++)
                {
                    viewItem = this.ViewItems[i];
                    if (viewItem == null) continue;
                    //
                    if (!viewItem.Visible) continue;
                    //
                    if (viewItem.DisplayRectangle.Contains(mouseEventArgs.Location))
                    {
                        IMessageChain pMessageChain = viewItem as IMessageChain;
                        if (pMessageChain != null)
                        {
                            pMessageChain.SendMessage(messageInfo);//new MessageInfo(this, messageInfo.eMessageStyle, messageInfo.MessageParameter)
                        }
                        //
                        this.SelectedIndex = i;
                        //Console.WriteLine("SelectedIndex:" + i);
                        //
                        break;
                    }
                }
            }
        }
        private void MSMouseUp(MessageInfo messageInfo)
        {
            ICellViewItem viewItem;
            for (int i = this.TopViewItemIndex; i <= this.BottomViewItemIndex; i++)
            {
                viewItem = this.ViewItems[i];
                if (viewItem == null) continue;
                //
                if (!viewItem.Visible) continue;
                //
                IMessageChain pMessageChain = viewItem as IMessageChain;
                if (pMessageChain != null)
                {
                    pMessageChain.SendMessage(messageInfo);//new MessageInfo(this, messageInfo.eMessageStyle, messageInfo.MessageParameter)
                }
            }
        }
        private void MSMouseMove(MessageInfo messageInfo)
        {
            ICellViewItem viewItem;
            for (int i = this.TopViewItemIndex; i <= this.BottomViewItemIndex; i++)
            {
                viewItem = this.ViewItems[i];
                if (viewItem == null) continue;
                //
                if (!viewItem.Visible) continue;
                //
                IMessageChain pMessageChain = viewItem as IMessageChain;
                if (pMessageChain != null)
                {
                    pMessageChain.SendMessage(messageInfo);//new MessageInfo(this, messageInfo.eMessageStyle, messageInfo.MessageParameter)
                }
            }
        }
        private void MSMouseClick(MessageInfo messageInfo)
        {
            MouseEventArgs e = messageInfo.MessageParameter as MouseEventArgs;
            if (e == null) return;
            //
            ICellViewItem viewItem = null;
            for (int i = this.TopViewItemIndex; i <= this.BottomViewItemIndex; i++)
            {
                viewItem = this.ViewItems[i];
                if (viewItem == null) continue;
                //
                if (!viewItem.Visible) continue;
                //
                if (viewItem.DisplayRectangle.Contains(e.Location))
                {
                    IMessageChain pMessageChain = viewItem as IMessageChain;
                    if (pMessageChain != null)
                    {
                        pMessageChain.SendMessage(messageInfo);//new MessageInfo(this, messageInfo.eMessageStyle, messageInfo.MessageParameter)
                    }
                    //
                    break;
                }
            }
        }
        private void MSMouseDoubleClick(MessageInfo messageInfo)
        {
            MouseEventArgs e = messageInfo.MessageParameter as MouseEventArgs;
            if (e == null) return;
            //
            ICellViewItem viewItem = null;
            for (int i = this.TopViewItemIndex; i <= this.BottomViewItemIndex; i++)
            {
                viewItem = this.ViewItems[i];
                if (viewItem == null) continue;
                //
                if (!viewItem.Visible) continue;
                //
                if (viewItem.DisplayRectangle.Contains(e.Location))
                {
                    IMessageChain pMessageChain = viewItem as IMessageChain;
                    if (pMessageChain != null)
                    {
                        pMessageChain.SendMessage(messageInfo);//new MessageInfo(this, messageInfo.eMessageStyle, messageInfo.MessageParameter)
                    }
                    //
                    break;
                }
            }
        }
        private void MSMouseEnter(MessageInfo messageInfo)
        {

        }
        private void MSMouseLeave(MessageInfo messageInfo)
        {
            ICellViewItem viewItem;
            for (int i = this.TopViewItemIndex; i <= this.BottomViewItemIndex; i++)
            {
                viewItem = this.ViewItems[i];
                if (viewItem == null) continue;
                //
                if (!viewItem.Visible) continue;
                //
                IMessageChain pMessageChain = viewItem as IMessageChain;
                if (pMessageChain != null)
                {
                    pMessageChain.SendMessage(messageInfo);//new MessageInfo(this, messageInfo.eMessageStyle, messageInfo.MessageParameter)
                }
            }
        }
        private void MSEnabledChanged(MessageInfo messageInfo)
        {
            ICellViewItem viewItem;
            for (int i = this.TopViewItemIndex; i <= this.BottomViewItemIndex; i++)
            {
                viewItem = this.ViewItems[i];
                if (viewItem == null) continue;
                //
                if (!viewItem.Visible) continue;
                //
                IMessageChain pMessageChain = viewItem as IMessageChain;
                if (pMessageChain != null)
                {
                    pMessageChain.SendMessage(messageInfo);//new MessageInfo(this, messageInfo.eMessageStyle, messageInfo.MessageParameter)
                }
            }
        }
        private void MSVisibleChanged(MessageInfo messageInfo)
        {
            ICellViewItem viewItem;
            for (int i = this.TopViewItemIndex; i <= this.BottomViewItemIndex; i++)
            {
                viewItem = this.ViewItems[i];
                if (viewItem == null) continue;
                //
                if (!viewItem.Visible) continue;
                //
                IMessageChain pMessageChain = viewItem as IMessageChain;
                if (pMessageChain != null)
                {
                    pMessageChain.SendMessage(messageInfo);//new MessageInfo(this, messageInfo.eMessageStyle, messageInfo.MessageParameter)
                }
            }
        }
        #endregion
        
        public override Size MeasureSize(Graphics g)
        {
            if (this.ViewItems.Count > 0)
            {
                //int iW = 0;
                //foreach (ICellViewItem one in this.ViewItems)
                //{
                //    if (!one.Visible) continue;
                //    //
                //    iW += one.MeasureSize(g).Width;
                //}
                //return new Size(iW, this.Height);
                IViewItemOwner2 pViewItemOwner2 = this.m_pOwner as IViewItemOwner2;
                if (pViewItemOwner2 == null) return base.MeasureSize(g);
                IRowViewObject pRowViewObject = pViewItemOwner2.GetTopViewItemOwner() as IRowViewObject;
                if (pRowViewObject == null) return base.MeasureSize(g);
                return new Size(pRowViewObject.RowWidth, this.Height);
            }
            //
            return base.MeasureSize(g);
        }

        protected override void OnViewParameterStyleChanged(ViewParameterStyle viewParameterStyle)
        {
            if (viewParameterStyle != ViewParameterStyle.eFocused)
            {
                this.SelectedIndex = -1;
                //Console.WriteLine("-1");
            }
            //
            //base.OnViewParameterStyleChanged(viewParameterStyle);
        }

        protected override void OnDraw(System.Windows.Forms.PaintEventArgs e)
        {
            if (this.ViewItems.Count > 0)
            {
                WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRowCellViewItem
                    (
                    new ObjectRenderEventArgs(
                        e.Graphics, this, Rectangle.FromLTRB(e.ClipRectangle.Left, e.ClipRectangle.Top, e.ClipRectangle.Right - 1, e.ClipRectangle.Bottom - 1))
                    );
                //
                this.DrawItem(e);
            }
            else
            {
                base.OnDraw(e);
            }
        }
        protected virtual void DrawItem(System.Windows.Forms.PaintEventArgs e)
        {
            //Rectangle rectangle = this.DisplayRectangle;
            IViewItemOwner pViewItemOwner = this.pOwner as IViewItemOwner;
            Rectangle viewItemsRectangle = e.ClipRectangle;// pViewItemOwner == null ? rectangle : pViewItemOwner.ViewItemsRectangle;
            GISShare.Controls.WinForm.WFNew.LayoutEngine.LayoutStackH_Row(e.Graphics, this, viewItemsRectangle, this.DisplayRectangle, ref this._TopViewItemIndex, ref this._BottomViewItemIndex);
            ViewItem viewItem = null;
            IMessageChain pMessageChain;
            for (int i = this.TopViewItemIndex; i <= this.BottomViewItemIndex; i++)
            {
                viewItem = this.ViewItems[i];
                if (viewItem == null) continue;
                Rectangle clipRectangle = Rectangle.Intersect(viewItemsRectangle, viewItem.DisplayRectangle);
                if (clipRectangle.Width > 0 && clipRectangle.Height > 0)
                {
                    pMessageChain = viewItem as IMessageChain;
                    if (pMessageChain != null)
                    {
                        e.Graphics.SetClip(clipRectangle);
                        MessageInfo messageInfo = new MessageInfo(this, MessageStyle.eMSPaint, new PaintEventArgs(e.Graphics, clipRectangle));
                        pMessageChain.SendMessage(messageInfo);
                    }
                }
            }
            #region 已抛弃（布局移植到LayoutEngine）
            //Rectangle rectangle = this.DisplayRectangle;
            //int iW = 0;
            //int iOffsetX = 0;
            ////
            //Rectangle viewItemsRectangle = ((IViewItemOwner)this).ViewItemsRectangle;
            ////
            //ICellViewItem viewItem = null;
            //int iCount = this.ViewItems.Count;
            //int iLeftX = 0;
            //int iRightX = 0;
            //int iLeftIndex = 0;
            //int iRightIndex = iCount;
            //for (int i = 0; i < iCount; i++)
            //{
            //    viewItem = this.ViewItems[i];
            //    //
            //    if (!viewItem.Visible) continue;
            //    //
            //    iW = viewItem.MeasureSize(e.Graphics).Width;
            //    iLeftX = rectangle.Left + iOffsetX;
            //    iRightX = iLeftX + iW;
            //    if (iRightX <= viewItemsRectangle.Left)
            //    {
            //        iLeftIndex++;
            //    }
            //    else if (iLeftX >= viewItemsRectangle.Right)
            //    {
            //        iRightIndex--;
            //    }
            //    else
            //    {
            //        ISetViewItemHelper pSetViewItemHelper = viewItem as ISetViewItemHelper;
            //        if (pSetViewItemHelper != null)
            //        {
            //            pSetViewItemHelper.SetViewItemDisplayRectangle(new Rectangle(iLeftX, rectangle.Top, iW, rectangle.Height));
            //            //
            //            Rectangle clipRectangle = Rectangle.Intersect(viewItemsRectangle, viewItem.DisplayRectangle);
            //            if (clipRectangle.Width > 0 && clipRectangle.Height > 0)
            //            {
            //                IMessageChain pMessageChain = viewItem as IMessageChain;
            //                if (pMessageChain != null)
            //                {
            //                    e.Graphics.SetClip(clipRectangle);
            //                    MessageInfo messageInfo = new MessageInfo(this, MessageStyle.eMSPaint, new PaintEventArgs(e.Graphics, clipRectangle));
            //                    pMessageChain.SendMessage(messageInfo);
            //                }
            //            }
            //        }
            //    }
            //    //
            //    iOffsetX += iW;
            //}
            ////
            //this.m_TopViewItemIndex = iLeftIndex;
            //this.m_BottomViewItemIndex = iRightIndex - 1;
            #endregion
        }
    }
}