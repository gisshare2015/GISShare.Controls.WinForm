using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew.View
{
    public class FlexibleVRowViewItem : TextEditViewItem, 
        IRowViewItem, 
        IFlexibleRowViewItem,
        IViewListEnumerator,
        IOwner, ISetOwnerHelper,
        IViewItemOwner, IViewItemOwner2, IViewList
    {
        private const int CONST_MINNODEWIDTH = 18;
        private const int CONST_MINNODEHEIGHT = 18;
        //
        private const int CONST_RESIZERECTANGLESIZE = 6;
        //
        private int m_OperationStyle = -1;//0 = CanResizeItemWidth, 1 = CanResizeItemHeight, 2 = CanExchangeItem
        private Point m_MouseDownPoint = Point.Empty;
        private int m_MouseDownSelectedIndex = -1;
        private Cursor m_CursorDefault = Cursors.Default;

        public FlexibleVRowViewItem()
        {
            this.m_ViewItemCollection = new SizeViewItemCollection(this);
        }

        public FlexibleVRowViewItem(string text)
            : this()
        {
            this.Text = text;
        }

        public FlexibleVRowViewItem(string name, string text)
            : this()
        {
            this.Name = name;
            this.Text = text;
        }

        public FlexibleVRowViewItem(string name, string text, Font font)
            : this()
        {
            this.Name = name;
            this.Text = text;
            this.Font = font;
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

        #region IResizeViewItem
        private bool m_CanResizeItemWidth = true;
        [Browsable(true), DefaultValue(true), Description("是否可以调节项宽"), Category("状态")]
        public bool CanResizeItemWidth
        {
            get { return m_CanResizeItemWidth; }
            set { m_CanResizeItemWidth = value; }
        }

        private bool m_CanResizeItemHeight = true;
        [Browsable(true), DefaultValue(true), Description("是否可以调节项宽"), Category("状态")]
        public bool CanResizeItemHeight
        {
            get { return m_CanResizeItemHeight; }
            set { m_CanResizeItemHeight = value; }
        }
        #endregion

        #region IFlexibleRowViewItem
        bool m_IsFilled = true;
        [Browsable(true), DefaultValue(true), Description("是否用子项将其撑满"), Category("状态")]
        public bool IsFilled
        {
            get { return m_IsFilled; }
            set { m_IsFilled = value; }
        }

        private bool m_CanExchangeItem = false;
        [Browsable(true), DefaultValue(false), Description("是否可以交换项（CanEdit为false有效）"), Category("状态")]
        public bool CanExchangeItem
        {
            get { return m_CanExchangeItem; }
            set { m_CanExchangeItem = value; }
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

        #region 属性
        private SizeViewItemCollection m_ViewItemCollection;
        [Browsable(true),
        Editor(typeof(GISShare.Controls.WinForm.WFNew.View.Design.SizeViewItemCollectionEditer), typeof(System.Drawing.Design.UITypeEditor)),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
        Description("其所携带的子项集合"),
        Category("子项")]
        public SizeViewItemCollection ViewItems
        {
            get { return m_ViewItemCollection; }
        }
        #endregion

        public override int Width
        {
            get
            {
                return base.Width;
            }
            set
            {
                int iW = value - base.Width;
                if (iW == 0) return;
                base.Width = value;
                //
                if (this.IsFilled)
                {
                    foreach (SizeViewItem one in this.ViewItems)
                    {
                        one.Width += iW;
                    }
                }
            }
        }

        public override int Height
        {
            get
            {
                return base.Height;
            }
            set
            {
                int iH = value - base.Height;
                if (iH == 0) return;
                base.Height = value;
                //
                if (this.IsFilled && this.ViewItems.Count > 0)
                {
                    int iZL = iH / this.ViewItems.Count;
                    int iYS = iH % this.ViewItems.Count;
                    foreach (SizeViewItem one in this.ViewItems)
                    {
                        one.Height += iZL;
                    }
                    this.ViewItems[0].Height += iYS;
                }
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

        protected override bool RefreshBaseItemState
        {
            get
            {
                return base.RefreshBaseItemState && this.ShowBaseItemState;
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
                if (this.CanResizeItemWidth && this.DisplayRectangle.Right - CONST_RESIZERECTANGLESIZE <= mouseEventArgs.Location.X)
                {
                    this.m_OperationStyle = 0;
                    this.m_MouseDownPoint = mouseEventArgs.Location;
                    this.m_MouseDownSelectedIndex = -1;
                    Control ctr = this.TryGetDependControl_DG(this.pOwner);
                    if (ctr != null)
                    {
                        this.m_CursorDefault = ctr.Cursor;
                        ctr.Cursor = Cursors.SizeWE;
                    }
                    //
                    messageInfo.CancelPreEvent = true;//取消事件 1
                }
                else
                {
                    ViewItem viewItem = null;
                    for (int i = this.TopViewItemIndex; i <= this.BottomViewItemIndex; i++)
                    {
                        viewItem = this.ViewItems[i];
                        if (viewItem == null) continue;
                        //
                        if (viewItem.DisplayRectangle.Contains(mouseEventArgs.Location))
                        {
                            if (this.CanResizeItemHeight && viewItem.DisplayRectangle.Bottom - CONST_RESIZERECTANGLESIZE <= mouseEventArgs.Location.Y)
                            {
                                this.m_OperationStyle = 1;
                                this.m_MouseDownPoint = mouseEventArgs.Location;
                                this.m_MouseDownSelectedIndex = i;
                                Control ctr = this.TryGetDependControl_DG(this.pOwner);
                                if (ctr != null)
                                {
                                    this.m_CursorDefault = ctr.Cursor;
                                    ctr.Cursor = Cursors.SizeNS;
                                }
                                messageInfo.CancelPreEvent = true;//取消事件 2
                                //Console.WriteLine(this.ToString() + " - " + this.m_OperationStyle);
                                break;
                            }
                            else
                            {
                                if (this.CanExchangeItem && !this.CanEdit)
                                {
                                    this.m_OperationStyle = 2;
                                    this.m_MouseDownSelectedIndex = i;
                                    Control ctr = this.TryGetDependControl_DG(this.pOwner);
                                    if (ctr != null)
                                    {
                                        this.m_CursorDefault = ctr.Cursor;
                                        ctr.Cursor = Cursors.Hand;
                                    }
                                }
                                else
                                {
                                    IMessageChain pMessageChain = viewItem as IMessageChain;
                                    if (pMessageChain != null)
                                    {
                                        pMessageChain.SendMessage(messageInfo);//new MessageInfo(this, messageInfo.eMessageStyle, messageInfo.MessageParameter)
                                    }
                                }
                                //
                                this.SelectedIndex = i;
                            }
                            //
                            break;
                        }
                    }
                }
            }
        }
        private void MSMouseUp(MessageInfo messageInfo)
        {
            if (this.m_OperationStyle == 0)
            {
                MouseEventArgs mouseEventArgs = messageInfo.MessageParameter as MouseEventArgs;
                if (mouseEventArgs != null)
                {
                    this.Width += mouseEventArgs.Location.X - this.m_MouseDownPoint.X;
                    if (this.DisplayRectangle.Width >= this.Width)
                    {
                        this.Invalidate(this.DisplayRectangle);
                    }
                    else
                    {
                        this.Invalidate(new Rectangle(this.DisplayRectangle.X, this.DisplayRectangle.Y, this.DisplayRectangle.Width, this.Height));
                    }
                }
                //
                Control ctr = this.TryGetDependControl_DG(this.pOwner);
                if (ctr != null)
                {
                    ctr.Cursor = this.m_CursorDefault;
                }
                //
                messageInfo.CancelPreEvent = true;//取消事件 1
            }
            else if (this.m_OperationStyle == 1)//CanResizeItemHeight
            {
                MouseEventArgs mouseEventArgs = messageInfo.MessageParameter as MouseEventArgs;
                if (mouseEventArgs != null)
                {
                    ISizeViewItem pSizeViewItem = this.ViewItems[this.m_MouseDownSelectedIndex];
                    if (pSizeViewItem != null)
                    {
                        //pSizeViewItem.Height += mouseEventArgs.Location.Y - this.m_MouseDownPoint.Y;
                        //this.Invalidate(this.DisplayRectangle);
                        int iH = mouseEventArgs.Location.Y - this.m_MouseDownPoint.Y;
                        int iH_pre = pSizeViewItem.Height;
                        pSizeViewItem.Height += iH;
                        iH = pSizeViewItem.Height - iH_pre;
                        if (iH != 0)
                        {
                            if (this.IsFilled)
                            {
                                pSizeViewItem = this.ViewItems[this.m_MouseDownSelectedIndex + 1];
                                if (pSizeViewItem != null) { pSizeViewItem.Height -= iH; }
                            }
                            this.Invalidate(this.DisplayRectangle);
                        }
                    }
                }
                //
                Control ctr = this.TryGetDependControl_DG(this.pOwner);
                if (ctr != null)
                {
                    ctr.Cursor = this.m_CursorDefault;
                }
                //
                messageInfo.CancelPreEvent = true;//取消事件 2
            }
            else if (this.m_OperationStyle == 2)
            {
                MouseEventArgs mouseEventArgs = messageInfo.MessageParameter as MouseEventArgs;
                if (mouseEventArgs != null)
                {
                    ViewItem viewItem = null;
                    for (int i = this.TopViewItemIndex; i <= this.BottomViewItemIndex; i++)
                    {
                        viewItem = this.ViewItems[i];
                        if (viewItem.DisplayRectangle.Contains(mouseEventArgs.Location))
                        {
                            this.ViewItems.ExchangeItem(this.m_MouseDownSelectedIndex, i);
                        }
                    }
                }
                //
                Control ctr = this.TryGetDependControl_DG(this.pOwner);
                if (ctr != null)
                {
                    ctr.Cursor = this.m_CursorDefault;
                }
            }
            else
            {
                IMessageChain pMessageChain;
                for (int i = this.TopViewItemIndex; i <= this.BottomViewItemIndex; i++)
                {
                    pMessageChain = this.ViewItems[i] as IMessageChain;
                    if (pMessageChain != null)
                    {
                        pMessageChain.SendMessage(messageInfo);//new MessageInfo(this, messageInfo.eMessageStyle, messageInfo.MessageParameter)
                    }
                }
            }
            //
            this.m_OperationStyle = -1;
            this.m_MouseDownPoint = Point.Empty;
            this.m_MouseDownSelectedIndex = -1;
            this.m_CursorDefault = Cursors.Default;
        }
        private void MSMouseMove(MessageInfo messageInfo)
        {
            if (this.m_OperationStyle < 0)
            {
                messageInfo.CancelPreEvent = true;//取消事件 1
            }
            else
            {
                ViewItem viewItem = null;
                for (int i = this.TopViewItemIndex; i <= this.BottomViewItemIndex; i++)
                {
                    viewItem = this.ViewItems[i];
                    if (viewItem == null) continue;
                    //
                    IMessageChain pMessageChain = viewItem as IMessageChain;
                    if (pMessageChain != null)
                    {
                        pMessageChain.SendMessage(messageInfo);//new MessageInfo(this, messageInfo.eMessageStyle, messageInfo.MessageParameter)
                    }
                }
            }
        }
        private Control TryGetDependControl_DG(IOwner owner)
        {
            if (owner == null) return null;
            //
            Control control = owner as Control;
            if (control == null) return this.TryGetDependControl_DG(owner.pOwner);
            return control;
        }
        private void MSMouseClick(MessageInfo messageInfo)
        {
            MouseEventArgs e = messageInfo.MessageParameter as MouseEventArgs;
            if (e == null) return;
            //
            ViewItem viewItem = null;
            for (int i = this.TopViewItemIndex; i <= this.BottomViewItemIndex; i++)
            {
                viewItem = this.ViewItems[i];
                if (viewItem == null) continue;
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
            ViewItem viewItem = null;
            for (int i = this.TopViewItemIndex; i <= this.BottomViewItemIndex; i++)
            {
                viewItem = this.ViewItems[i];
                if (viewItem == null) continue;
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

        public override Size MeasureSize(Graphics g)
        {
            if (this.ViewItems.Count > 0)
            {
                int iH = 0;
                int iW = 0;
                Size size;
                foreach (SizeViewItem one in this.ViewItems)
                {
                    size = one.MeasureSize(g);
                    iH += size.Height;
                    if (iW < size.Width) iW = size.Width;
                }
                return this.IsFilled ? new Size(iW, iH) : new Size(iW > this.Width ? iW : this.Width, iH);
            }
            //
            return base.MeasureSize(g);
        }

        protected override void OnViewParameterStyleChanged(ViewParameterStyle viewParameterStyle)
        {
            if (viewParameterStyle != ViewParameterStyle.eFocused)
            {
                this.SelectedIndex = -1;
            }
            //
            //base.OnViewParameterStyleChanged(viewParameterStyle);
        }

        protected override void OnDraw(System.Windows.Forms.PaintEventArgs e)
        {
            if (this.ViewItems.Count > 0)
            {
                WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderViewItem
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
            GISShare.Controls.WinForm.WFNew.LayoutEngine.LayoutStackV_Row(e.Graphics, this, viewItemsRectangle, this.DisplayRectangle, ref this._TopViewItemIndex, ref this._BottomViewItemIndex);
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
            //IViewItemOwner pViewItemOwner = this.pOwner as IViewItemOwner;
            //Rectangle viewItemsRectangle = pViewItemOwner == null ? rectangle : pViewItemOwner.ViewItemsRectangle;
            ////
            //ViewItem viewItem = null;
            //int iCount = this.ViewItems.Count;
            //int iLeftX = 0;
            //int iRightX = 0;
            //int iLeftIndex = 0;
            //int iRightIndex = iCount;
            //for (int i = 0; i < iCount; i++)
            //{
            //    viewItem = this.ViewItems[i];
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