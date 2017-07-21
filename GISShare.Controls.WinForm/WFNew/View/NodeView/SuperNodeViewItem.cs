using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew.View
{
    public class SuperNodeViewItem : NodeViewItem, ISuperViewItem
    {
        public SuperNodeViewItem()
            : base()
        { }

        public SuperNodeViewItem(BaseItem baseItem)
            : base()
        {
            this.BaseItemObject = baseItem;
        }

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
                viewItemRectangle = Rectangle.FromLTRB(this.TextRectangle.Left, this.DisplayRectangle.Top, this.DisplayRectangle.Right, this.DisplayRectangle.Bottom);
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

        public override bool CanEdit
        {
            get
            {
                return false;
            }
            set
            {
                base.CanEdit = value;
            }
        }

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

        public override Size MeasureSize(Graphics g)
        {
            return new Size(this.Width < 0 ? 0 : this.Width, this.Height < 0 ? 0 : this.Height);
            //if (this.BaseItemObject == null)
            //{
            //    return base.MeasureSize(g);
            //}
            //else
            //{
            //    //int iOffset = this.TextRectangle.Left;
            //    int iOffset = this.NodeTextOffset;
            //    //
            //    Size size = this.BaseItemObject.MeasureSize(g);
            //    return new Size
            //        (
            //        this.Width > 0 ? this.Width : iOffset + this.BaseItemObject.Margin.Left + size.Width + this.BaseItemObject.Margin.Right,
            //        this.Height > 0 ? this.Height : this.BaseItemObject.Margin.Top + size.Height + this.BaseItemObject.Margin.Bottom
            //        );
            //}
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
            if (this.BaseItemObject != null)
            {
                Rectangle rectangle = Rectangle.FromLTRB(e.ClipRectangle.Left, e.ClipRectangle.Top, e.ClipRectangle.Right - 1, e.ClipRectangle.Bottom - 1);
                WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderNodeViewItem(new ObjectRenderEventArgs(e.Graphics, this, rectangle));
                return;
            }
            base.OnDraw(e);
        }
    }
}
