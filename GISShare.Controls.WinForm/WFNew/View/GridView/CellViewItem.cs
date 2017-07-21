using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;

namespace GISShare.Controls.WinForm.WFNew.View
{
    public class CellViewItem : TextEditViewItem,
        IVisibleViewItem,
        ISuperViewItem,
        ICellViewItem,
        IOwner, IBaseItemOwner, ISetOwnerHelper,
        IViewItemOwner, IViewItemOwner2
    {
        public CellViewItem(CellViewStyle cellViewStyle)
            : base("CellViewItem", "")
        {
            this.m_eCellViewStyle = cellViewStyle;
        }

        #region IOwner
        IOwner m_pOwner = null;
        IOwner IOwner.pOwner
        {
            get { return m_pOwner; }
        }

        void IOwner.Refresh()
        {
            if (this.m_pOwner != null) this.m_pOwner.Refresh();
        }

        void IOwner.Invalidate(Rectangle rectangle)
        {
            if (this.m_pOwner != null) this.m_pOwner.Invalidate(rectangle);
        }

        Point IOwner.PointToClient(Point point)
        {
            if (this.m_pOwner != null) return this.m_pOwner.PointToClient(point);
            return Point.Empty;
        }

        Point IOwner.PointToScreen(Point point)
        {
            if (this.m_pOwner != null) return this.m_pOwner.PointToScreen(point);
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

        #region IBaseItemOwner
        [Browsable(false), Description("其子项展现矩形"), Category("布局")]
        Rectangle IBaseItemOwner.ItemsRectangle
        {
            get
            {
                return this.DisplayRectangle;
            }
        }

        Rectangle m_ItemsViewRectangle;
        [Browsable(false), Description("其子视图项展现矩形"), Category("布局")]
        Rectangle IBaseItemOwner.ItemsViewRectangle
        {
            get
            {
                return this.m_ItemsViewRectangle;
            }
        }

        [Browsable(false), Description("获取其子项拥有者"), Category("关联")]
        WFNew.IBaseItemOwner WFNew.IBaseItemOwner.pBaseItemOwner
        {
            get { return null; }
        }
        #endregion

        #region IViewItemOwner
        Rectangle IViewItemOwner.ViewItemsRectangle
        {
            get
            {
                IViewItemOwner pViewItemOwner = this.m_pOwner as IViewItemOwner;
                return pViewItemOwner == null ? this.DisplayRectangle : pViewItemOwner.ViewItemsRectangle;
            }
        }
        #endregion

        #region IViewItemOwner2
        IViewItemOwner2 IViewItemOwner2.GetTopViewItemOwner()
        {
            IViewItemOwner2 pViewItemOwner2 = this.m_pOwner as IViewItemOwner2;
            return pViewItemOwner2 == null ? null : pViewItemOwner2.GetTopViewItemOwner();
        }
        #endregion

        #region ISizeViewItem
        [Browsable(true), DefaultValue(-1), Description("宽度（小于零时，由系统操作）"), Category("布局")]
        public override int Width
        {
            get
            {
                if (this.eCellViewStyle == CellViewStyle.eSystem)
                {
                    IViewItemOwner2 pViewItemOwner2 = this.m_pOwner as IViewItemOwner2;
                    if (pViewItemOwner2 == null) return base.Width;
                    IColumnViewObject pColumnViewObject = pViewItemOwner2.GetTopViewItemOwner() as IColumnViewObject;
                    if (pColumnViewObject == null) return base.Width;
                    int iIndex = ((IRowViewItem)pViewItemOwner2).List.IndexOf(this);
                    if (iIndex < 0) return base.Width;
                    IColumnViewItem pColumnViewItem = pColumnViewObject.ColumnViewItems[iIndex];
                    //int iNodeTextOffset = 0;
                    //if (iIndex == 0)
                    //{
                    //   if (pViewItemOwner2.pOwner is INodeViewItem)
                    //   {
                    //       iNodeTextOffset = ((INodeViewItem)pViewItemOwner2.pOwner).NodeTextOffset;
                    //   }
                    //}
                    //return pColumnViewItem == null ? base.Width : pColumnViewItem.Width - iNodeTextOffset;
                    return pColumnViewItem == null ? base.Width : pColumnViewItem.Width;
                }
                else if (this.eCellViewStyle == CellViewStyle.eSingleCell)
                {
                    IViewItemOwner2 pViewItemOwner2 = this.m_pOwner as IViewItemOwner2;
                    if (pViewItemOwner2 == null) return base.Width;
                    IColumnViewObject pColumnViewObject = pViewItemOwner2.GetTopViewItemOwner() as IColumnViewObject;
                    if (pColumnViewObject == null && pColumnViewObject.ColumnViewItems.Count <= 0) return base.Width;
                    int iW = 0;
                    foreach(ISizeViewItem one in pColumnViewObject.ColumnViewItems)
                    {
                        iW += one.Width;
                    }
                    return iW;
                }
                //
                return base.Width;
            }
            set 
            {
                base.Width = value;
            }
        }

        [Browsable(true), DefaultValue(18), Description("高度（小于零时，由系统操作）"), Category("布局")]
        public override int Height
        {
            get
            {
                IRowViewItem pRowViewItem = this.m_pOwner as IRowViewItem;
                return pRowViewItem == null ? base.Height : pRowViewItem.Height;
            }
            set
            {
                base.Height = value;
            }
        }
        #endregion

        #region ISuperViewItem
        BaseItem m_BaseItemObject = null;
        [Browsable(true), Description("其所携带的BaseItemObject"), Category("属性")]
        public BaseItem BaseItemObject
        {
            get { return m_BaseItemObject; }
            set 
            {
                m_BaseItemObject = value;
                if (this.m_BaseItemObject != null) ((ISetOwnerHelper)m_BaseItemObject).SetOwner(this);
            }
        }

        [Browsable(false), Description("其子项展现矩形"), Category("布局")]
        public virtual Rectangle BaseItemRectangle
        {
            get
            {
                if (this.BaseItemObject == null) return this.DisplayRectangle;
                //
                Rectangle rectangle = this.BaseItemObject.DisplayRectangle;
                Rectangle viewItemRectangle = Rectangle.FromLTRB(this.DisplayRectangle.Left, this.DisplayRectangle.Top, this.DisplayRectangle.Right - 1, this.DisplayRectangle.Bottom - 1);
                switch (this.BaseItemObject.eHAlignmentStyle)
                {
                    case HAlignmentStyle.eLeft:
                        switch (this.BaseItemObject.eVAlignmentStyle)
                        {
                            case VAlignmentStyle.eTop:
                                rectangle = new Rectangle
                                    (
                                    viewItemRectangle.Left + this.BaseItemObject.Margin.Left,
                                    viewItemRectangle.Top + this.BaseItemObject.Margin.Top,
                                    rectangle.Width,
                                    rectangle.Height
                                    );
                                break;
                            case VAlignmentStyle.eBottom:
                                rectangle = new Rectangle
                                    (
                                    viewItemRectangle.Left + this.BaseItemObject.Margin.Left,
                                    viewItemRectangle.Bottom + this.BaseItemObject.Margin.Bottom - rectangle.Height,
                                    rectangle.Width,
                                    rectangle.Height
                                    );
                                break;
                            case VAlignmentStyle.eCenter:
                                rectangle = new Rectangle
                                    (
                                    viewItemRectangle.Left + this.BaseItemObject.Margin.Left,
                                    (viewItemRectangle.Top + viewItemRectangle.Bottom - rectangle.Height) / 2,
                                    rectangle.Width,
                                    rectangle.Height
                                    );
                                break;
                            case VAlignmentStyle.eStretch:
                            default:
                                rectangle = new Rectangle
                                    (
                                    viewItemRectangle.Left + this.BaseItemObject.Margin.Left,
                                    viewItemRectangle.Top + this.BaseItemObject.Margin.Top,
                                    rectangle.Width,
                                    viewItemRectangle.Height - this.BaseItemObject.Margin.Top - this.BaseItemObject.Margin.Bottom
                                    );
                                break;
                        }
                        break;
                    case HAlignmentStyle.eRight:
                        switch (this.BaseItemObject.eVAlignmentStyle)
                        {
                            case VAlignmentStyle.eTop:
                                rectangle = new Rectangle
                                    (
                                    viewItemRectangle.Right + this.BaseItemObject.Margin.Right - rectangle.Width,
                                    viewItemRectangle.Top + this.BaseItemObject.Margin.Top,
                                    rectangle.Width,
                                    rectangle.Height
                                    );
                                break;
                            case VAlignmentStyle.eBottom:
                                rectangle = new Rectangle
                                    (
                                    viewItemRectangle.Right + this.BaseItemObject.Margin.Right - rectangle.Width,
                                    viewItemRectangle.Bottom + this.BaseItemObject.Margin.Bottom - rectangle.Height,
                                    rectangle.Width,
                                    rectangle.Height
                                    );
                                break;
                            case VAlignmentStyle.eCenter:
                                rectangle = new Rectangle
                                    (
                                    viewItemRectangle.Right + this.BaseItemObject.Margin.Right - rectangle.Width,
                                    (viewItemRectangle.Top + viewItemRectangle.Bottom - rectangle.Height) / 2,
                                    rectangle.Width,
                                    rectangle.Height
                                    );
                                break;
                            case VAlignmentStyle.eStretch:
                            default:
                                rectangle = new Rectangle
                                    (
                                    viewItemRectangle.Right + this.BaseItemObject.Margin.Right - rectangle.Width,
                                    viewItemRectangle.Top + this.BaseItemObject.Margin.Top,
                                    rectangle.Width,
                                    viewItemRectangle.Height - this.BaseItemObject.Margin.Top - this.BaseItemObject.Margin.Bottom
                                    );
                                break;
                        }
                        break;
                    case HAlignmentStyle.eCenter:
                        switch (this.BaseItemObject.eVAlignmentStyle)
                        {
                            case VAlignmentStyle.eTop:
                                rectangle = new Rectangle
                                    (
                                    (viewItemRectangle.Left + viewItemRectangle.Right - rectangle.Width) / 2,
                                    viewItemRectangle.Top + this.BaseItemObject.Margin.Top,
                                    rectangle.Width,
                                    rectangle.Height
                                    );
                                break;
                            case VAlignmentStyle.eBottom:
                                rectangle = new Rectangle
                                    (
                                    (viewItemRectangle.Left + viewItemRectangle.Right - rectangle.Width) / 2,
                                    viewItemRectangle.Bottom + this.BaseItemObject.Margin.Bottom - rectangle.Height,
                                    rectangle.Width,
                                    rectangle.Height
                                    );
                                break;
                            case VAlignmentStyle.eCenter:
                                rectangle = new Rectangle
                                    (
                                    (viewItemRectangle.Left + viewItemRectangle.Right - rectangle.Width) / 2,
                                    (viewItemRectangle.Top + viewItemRectangle.Bottom - rectangle.Height) / 2,
                                    rectangle.Width,
                                    rectangle.Height
                                    );
                                break;
                            case VAlignmentStyle.eStretch:
                            default:
                                rectangle = new Rectangle
                                    (
                                    (viewItemRectangle.Left + viewItemRectangle.Right - rectangle.Width) / 2,
                                    viewItemRectangle.Top + this.BaseItemObject.Margin.Top,
                                    rectangle.Width,
                                    viewItemRectangle.Height - this.BaseItemObject.Margin.Top - this.BaseItemObject.Margin.Bottom
                                    );
                                break;
                        }
                        break;
                    case HAlignmentStyle.eStretch:
                    default:
                        switch (this.BaseItemObject.eVAlignmentStyle)
                        {
                            case VAlignmentStyle.eTop:
                                rectangle = new Rectangle
                                    (
                                    viewItemRectangle.Left + this.BaseItemObject.Margin.Left,
                                    viewItemRectangle.Top + this.BaseItemObject.Margin.Top,
                                    viewItemRectangle.Width - this.BaseItemObject.Margin.Left - this.BaseItemObject.Margin.Right,
                                    rectangle.Height
                                    );
                                break;
                            case VAlignmentStyle.eBottom:
                                rectangle = new Rectangle
                                    (
                                    viewItemRectangle.Left + this.BaseItemObject.Margin.Left,
                                    viewItemRectangle.Bottom + this.BaseItemObject.Margin.Bottom - rectangle.Height,
                                    viewItemRectangle.Width - this.BaseItemObject.Margin.Left - this.BaseItemObject.Margin.Right,
                                    rectangle.Height
                                    );
                                break;
                            case VAlignmentStyle.eCenter:
                                rectangle = new Rectangle
                                    (
                                    viewItemRectangle.Left + this.BaseItemObject.Margin.Left,
                                    (viewItemRectangle.Top + viewItemRectangle.Bottom - rectangle.Height) / 2,
                                    viewItemRectangle.Width - this.BaseItemObject.Margin.Left - this.BaseItemObject.Margin.Right,
                                    rectangle.Height
                                    );
                                break;
                            case VAlignmentStyle.eStretch:
                            default:
                                rectangle = new Rectangle
                                    (
                                    viewItemRectangle.Left + this.BaseItemObject.Margin.Left,
                                    viewItemRectangle.Top + this.BaseItemObject.Margin.Top,
                                    viewItemRectangle.Width - this.BaseItemObject.Margin.Left - this.BaseItemObject.Margin.Right,
                                    viewItemRectangle.Height - this.BaseItemObject.Margin.Top - this.BaseItemObject.Margin.Bottom
                                    );
                                break;
                        }
                        break;
                }
                //
                return Rectangle.Intersect(rectangle, viewItemRectangle);
            }
        }
        #endregion
        
        #region IVisibleViewItem
        [Browsable(false), DefaultValue(true), Description("可见"), Category("状态")]
        bool IVisibleViewItem.Visible
        {
            get
            {
                IViewItemOwner2 pViewItemOwner2 = this.m_pOwner as IViewItemOwner2;
                if (pViewItemOwner2 == null) return false;
                IColumnViewObject pColumnViewObject = pViewItemOwner2.GetTopViewItemOwner() as IColumnViewObject;
                if (pColumnViewObject == null) return false;
                int iIndex = ((IRowViewItem)pViewItemOwner2).List.IndexOf(this);
                if (iIndex < 0) return false;
                IColumnViewItem pColumnViewItem = pColumnViewObject.ColumnViewItems[iIndex];
                return pColumnViewItem == null ? false : pColumnViewItem.Visible;
            }
            set { }
        }
        #endregion

        #region ICellViewItem
        CellViewStyle m_eCellViewStyle = CellViewStyle.eSystem;
        [Browsable(false), DefaultValue(typeof(CellViewStyle), "eSystem"), Description("单元类型"), Category("属性")]
        public CellViewStyle eCellViewStyle
        {
            get { return m_eCellViewStyle; }
        }

        private object m_Value;
        [Browsable(false), Description("携带的值"), Category("属性")]
        public object Value
        {
            get { return m_Value; }
            set 
            {
                if (m_Value == value) return;
                m_Value = value;
                if (m_Value != null) this.Text = m_Value.ToString();
            }
        }
        #endregion

        public override string Text
        {
            get
            {
                if (this.BaseItemObject != null) return this.BaseItemObject.Text; 
                return base.Text;                
            }
            set
            {
                if (this.BaseItemObject != null) this.BaseItemObject.Text = value;
                base.Text = value;
            }
        }

        public override System.Drawing.Size MeasureSize(System.Drawing.Graphics g)
        {
            return new Size(this.Width, this.Height);
        }

        protected override void OnDraw(System.Windows.Forms.PaintEventArgs e)
        {
            Rectangle rectangle = Rectangle.FromLTRB(e.ClipRectangle.Left, e.ClipRectangle.Top, e.ClipRectangle.Right - 1, e.ClipRectangle.Bottom - 1);
            WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderCellViewItem
                (
                new ObjectRenderEventArgs(e.Graphics, this, rectangle)
                );
            //
            if (this.BaseItemObject != null) return;
            //
            if (this.Text.Length <= 0) return;
            rectangle = this.DisplayRectangle;
            int iH = (int)e.Graphics.MeasureString(this.Text, this.Font).Height + 1;
            WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonText
                (
                new TextRenderEventArgs(
                    e.Graphics,
                    this,
                    true,
                    true,
                    this.Text,
                    this.ForeColor,
                    this.Font,
                    new Rectangle(rectangle.Left, (rectangle.Top + rectangle.Bottom - iH) / 2, rectangle.Width, iH),//rectangle.Height
                    new StringFormat() { Trimming = StringTrimming.EllipsisCharacter })
                );
        }

        protected override void MessageMonitor(MessageInfo messageInfo)
        {
            if (this.BaseItemObject == null) return;
            //
            switch (messageInfo.eMessageStyle)
            {
                case MessageStyle.eMSPaint:
                    ISetViewItemHelper pSetViewItemHelper = this.BaseItemObject as ISetViewItemHelper;
                    if (pSetViewItemHelper != null)
                    {
                        pSetViewItemHelper.SetViewItemDisplayRectangle(this.BaseItemRectangle);
                    }
                    //
                    System.Windows.Forms.PaintEventArgs e = messageInfo.MessageParameter as System.Windows.Forms.PaintEventArgs;
                    if (e != null) 
                    {
                        this.m_ItemsViewRectangle = e.ClipRectangle;
                    }
                    break;
                default:
                    base.MessageMonitor(messageInfo);
                    break;
            }
            //
            IMessageChain pMessageChain = this.BaseItemObject as IMessageChain;
            if (pMessageChain != null)
            {
                if (messageInfo.eMessageStyle == MessageStyle.eMSMouseWheel)
                {
                    pMessageChain.SendMessage(messageInfo);//new MessageInfo(this, messageInfo.eMessageStyle, messageInfo.MessageParameter)
                }
                else
                {
                    pMessageChain.SendMessage(new MessageInfo(this, messageInfo.eMessageStyle, messageInfo.MessageParameter));//messageInfo
                }
            }
        }

    }
}
