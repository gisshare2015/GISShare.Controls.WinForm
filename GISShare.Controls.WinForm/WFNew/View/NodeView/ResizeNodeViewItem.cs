using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew.View
{
    public class ResizeNodeViewItem : NodeViewItem, IResizeViewItem
    {
        private const int CONST_RESIZERECTANGLESIZE = 6;
        //
        private int m_OperationStyle = -1;//0 = CanResizeItemWidth, 1 = CanResizeItemHeight
        private Point m_MouseDownPoint = Point.Empty;
        private Cursor m_CursorDefault = Cursors.Default;

        public ResizeNodeViewItem()
            : base()
        { }

        public ResizeNodeViewItem(string text)
            : base(text)
        { }

        public ResizeNodeViewItem(string name, string text)
            : base(name, text)
        { }

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

        #region 修改消息链条
        protected override void MessageMonitor(MessageInfo messageInfo)
        {
            switch (messageInfo.eMessageStyle)
            {
                case MessageStyle.eMSMouseDown:
                    if (this.MSMouseDown(messageInfo)) return;
                    break;
                case MessageStyle.eMSMouseUp:
                    if (this.MSMouseUp(messageInfo)) return;
                    break;
                case MessageStyle.eMSMouseMove:
                    if (this.MSMouseMove(messageInfo)) return;
                    break;
                default:
                    break;
            }
            //
            base.MessageMonitor(messageInfo);
        }
        private bool MSMouseDown(MessageInfo messageInfo)
        {
            MouseEventArgs mouseEventArgs = messageInfo.MessageParameter as MouseEventArgs;
            if (mouseEventArgs != null)
            {
                if (this.CanResizeItemWidth && this.DisplayRectangle.Right - CONST_RESIZERECTANGLESIZE <= mouseEventArgs.Location.X)
                {
                    this.m_OperationStyle = 0;
                    this.m_MouseDownPoint = mouseEventArgs.Location;
                    Control ctr = this.TryGetDependControl_DG(messageInfo.Sender as IOwner);
                    if (ctr != null)
                    {
                        this.m_CursorDefault = ctr.Cursor;
                        ctr.Cursor = Cursors.SizeWE;
                    }
                    return true;
                }
                else if (this.CanResizeItemHeight && this.DisplayRectangle.Bottom - CONST_RESIZERECTANGLESIZE <= mouseEventArgs.Location.Y)
                {
                    this.m_OperationStyle = 1;
                    this.m_MouseDownPoint = mouseEventArgs.Location;
                    Control ctr = this.TryGetDependControl_DG(messageInfo.Sender as IOwner);
                    if (ctr != null)
                    {
                        this.m_CursorDefault = ctr.Cursor;
                        ctr.Cursor = Cursors.SizeNS;
                    }
                    return true;
                }
            }
            return false;
        }
        private bool MSMouseUp(MessageInfo messageInfo)
        {
            if (this.m_OperationStyle == 0)
            {
                IOwner pOwner = messageInfo.Sender as IOwner;
                if (pOwner != null)
                {
                    MouseEventArgs mouseEventArgs = messageInfo.MessageParameter as MouseEventArgs;
                    if (mouseEventArgs != null)
                    {
                        this.Width += mouseEventArgs.Location.X - this.m_MouseDownPoint.X;
                        if (this.DisplayRectangle.Width >= this.Width)
                        {
                            pOwner.Invalidate(this.DisplayRectangle);
                        }
                        else
                        {
                            pOwner.Invalidate(new Rectangle(this.DisplayRectangle.X, this.DisplayRectangle.Y, this.DisplayRectangle.Width, this.Height));
                        }
                    }
                    //
                    Control ctr = this.TryGetDependControl_DG(pOwner);
                    if (ctr != null)
                    {
                        ctr.Cursor = this.m_CursorDefault;
                    }
                    //
                    this.m_OperationStyle = -1;
                    this.m_MouseDownPoint = Point.Empty;
                    this.m_CursorDefault = Cursors.Default;
                    //
                    return true;
                }
            }
            else if (this.m_OperationStyle == 1)//CanResizeItemHeight
            {
                IOwner pOwner = messageInfo.Sender as IOwner;
                if (pOwner != null)
                {
                    MouseEventArgs mouseEventArgs = messageInfo.MessageParameter as MouseEventArgs;
                    if (mouseEventArgs != null)
                    {
                        this.Height += mouseEventArgs.Location.Y - this.m_MouseDownPoint.Y;
                        if (this.DisplayRectangle.Height >= this.Height)
                        {
                            pOwner.Invalidate(this.DisplayRectangle);
                        }
                        else
                        {
                            pOwner.Invalidate(new Rectangle(this.DisplayRectangle.X, this.DisplayRectangle.Y, this.DisplayRectangle.Width, this.Height));
                        }
                    }
                    //
                    Control ctr = this.TryGetDependControl_DG(pOwner);
                    if (ctr != null)
                    {
                        ctr.Cursor = this.m_CursorDefault;
                    }
                    //
                    this.m_OperationStyle = -1;
                    this.m_MouseDownPoint = Point.Empty;
                    this.m_CursorDefault = Cursors.Default;
                    //
                    return true;
                }
            }
            //
            this.m_OperationStyle = -1;
            this.m_MouseDownPoint = Point.Empty;
            this.m_CursorDefault = Cursors.Default;
            //
            return false;
        }
        private bool MSMouseMove(MessageInfo messageInfo)
        {
            return this.m_OperationStyle > 0;
        }
        private Control TryGetDependControl_DG(IOwner owner)
        {
            if (owner == null) return null;
            //
            Control control = owner as Control;
            if (control == null) return this.TryGetDependControl_DG(owner.pOwner);
            return control;
        }
        #endregion
    }
}
