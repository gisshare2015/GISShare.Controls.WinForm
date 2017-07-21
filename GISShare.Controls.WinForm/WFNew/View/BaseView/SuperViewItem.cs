using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew.View
{
    public class SuperViewItem : SizeViewItem,
        ISizeViewItem, ISuperViewItem,
        IOwner, IBaseItemOwner, ISetOwnerHelper,
        IViewItemOwner, IViewItemOwner2
    {
        private const int CONST_MINNODEWIDTH = 18;
        private const int CONST_MINNODEHEIGHT = 18;

        public SuperViewItem()
            : base() { }

        public SuperViewItem(BaseItem baseItem)
            : base() 
        {
            this.BaseItemObject = baseItem;
        }

        #region IOwner
        IOwner m_pOwner = null;
        public IOwner pOwner
        {
            get { return m_pOwner; }
        }

        public void Refresh()
        {
            if (this.pOwner != null) this.pOwner.Refresh();
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
                Rectangle viewItemRectangle = this.DisplayRectangle;
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
            return new Size(this.Width < 0 ? 0 : this.Width, this.Height < 0 ? 0 : this.Height);
            //if (this.BaseItemObject == null)
            //{
            //    return base.MeasureSize(g); 
            //}
            //else
            //{
            //    Size displayRectangleSize = this.BaseItemObject.MeasureSize(g);
            //    return new Size
            //              (
            //              this.Width > 0 ? this.Width : this.BaseItemObject.Margin.Left + displayRectangleSize.Width + this.BaseItemObject.Margin.Right,
            //              this.Height > 0 ? this.Height : this.BaseItemObject.Margin.Top + displayRectangleSize.Height + this.BaseItemObject.Margin.Bottom
            //              );
            //}
        }

        protected override void OnDraw(System.Windows.Forms.PaintEventArgs e)
        {
            if (this.BaseItemObject != null) return;
            base.OnDraw(e);
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
