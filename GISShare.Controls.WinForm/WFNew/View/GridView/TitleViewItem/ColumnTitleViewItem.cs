using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Collections;
using System.Drawing;

namespace GISShare.Controls.WinForm.WFNew.View
{
    public class ColumnTitleViewItem : TitleViewItem,
        IViewItem,
        ISuperViewItem,
        IOwner, IBaseItemOwner, ISetOwnerHelper,
        IViewItemOwner, IViewItemOwner2
    {
        public ColumnTitleViewItem()
        { }

        public ColumnTitleViewItem(string text)
            : base(text) { }

        public ColumnTitleViewItem(string name, string text)
            : base(name, text) { }

        public ColumnTitleViewItem(string name, string text, Font font)
            : base(name, text, font) { }

        public ColumnTitleViewItem(ColumnViewItem columnViewItem)
        {
            this.m_LinkColumnViewItem = columnViewItem;
        }

        public ColumnTitleViewItem(ColumnViewItem columnViewItem, string text)
            : base(text) { this.m_LinkColumnViewItem = columnViewItem; }

        public ColumnTitleViewItem(ColumnViewItem columnViewItem, string name, string text)
            : base(name, text) { this.m_LinkColumnViewItem = columnViewItem; }

        public ColumnTitleViewItem(ColumnViewItem columnViewItem, string name, string text, Font font)
            : base(name, text, font) { this.m_LinkColumnViewItem = columnViewItem; }

        #region IViewItem
        [Browsable(false), DefaultValue(typeof(ViewParameterStyle), "eNone"), Description("视图伴随参数"), Category("属性")]
        ViewParameterStyle IViewItem.eViewParameterStyle
        {
            get
            {
                if (this.LinkColumnViewItem == null) return ViewParameterStyle.eNone;
                return ((IViewItem)this.LinkColumnViewItem).eViewParameterStyle;
            }
        }
        #endregion

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

        private ColumnViewItem m_LinkColumnViewItem;
        public ColumnViewItem LinkColumnViewItem
        {
            get { return m_LinkColumnViewItem; }
            set
            {
                if (this.LinkColumnViewItem == value) return;
                this.m_LinkColumnViewItem = value;
            }
        }

        public override int Width
        {
            get
            {
                return this.LinkColumnViewItem == null ? base.Width : this.LinkColumnViewItem.Width;
            }
            set
            {
                base.Width = value;
                if (this.LinkColumnViewItem != null) this.LinkColumnViewItem.Width = value;
            }
        }

        public override int Height
        {
            get
            {
                return this.LinkColumnViewItem == null ? base.Height : this.LinkColumnViewItem.Height;
            }
            set
            {
                base.Height = value;
                if (this.LinkColumnViewItem != null) this.LinkColumnViewItem.Height = value;
            }
        }

        //public override string Text
        //{
        //    get
        //    {
        //        return this.LinkColumnViewItem == null ? base.Text : this.LinkColumnViewItem.Text;
        //    }
        //    set
        //    {
        //        base.Text = value;
        //        if (this.LinkColumnViewItem != null) this.LinkColumnViewItem.Text = value;
        //    }
        //}

        [Browsable(true), DefaultValue(true), Description("可见"), Category("状态")]
        public override bool Visible
        {
            get
            {
                return this.LinkColumnViewItem == null ? base.Visible : this.LinkColumnViewItem.Visible;
            }
            set
            {
                base.Visible = value;
                this.LinkColumnViewItem.Visible = value;
            }
        }

        protected override bool RefreshBaseItemState
        {
            get
            {
                return true;
            }
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

        protected override void OnDraw(System.Windows.Forms.PaintEventArgs e)
        {
            //base.OnDraw(e);
            Rectangle rectangle = Rectangle.FromLTRB(e.ClipRectangle.Left, e.ClipRectangle.Top, e.ClipRectangle.Right - 1, e.ClipRectangle.Bottom - 1);
            WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderTitleViewItem(new ObjectRenderEventArgs(e.Graphics, this, rectangle));
            if (this.BaseItemObject != null) return;
            if (String.IsNullOrEmpty(this.Text)) return;
            rectangle = this.DisplayRectangle;
            int iH = (int)e.Graphics.MeasureString(this.Text, this.Font).Height + 1;
            WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonText
                (
                new TextRenderEventArgs(
                    e.Graphics,
                    this,
                    true,
                    this.HaveShadow,
                    this.Text,
                    this.ForeCustomize,
                    this.ForeColor,
                    this.ShadowColor,
                    this.Font,
                    new Rectangle(rectangle.Left, (rectangle.Top + rectangle.Bottom - iH) / 2, rectangle.Width, iH),//rectangle.Height
                    new StringFormat())
                );
        }
    }
}
