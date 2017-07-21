using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew.Forms
{
    public class TBFormSkinHelper : GISShare.Controls.WinForm.ControlSkinHelper,
        IOwner, IBaseItemOwner, IOwnerNC, IBaseItemOwnerNC, IBaseItemOwnerNC2, 
        ITBForm, IFormEvent, 
        IUICollectionItemNC,
        IMessageChain
    {
        private const int CRT_MINCAPTIONTEXTWIDTH = 160;           // 标题文本区高（包含在 标题区 之内）

        public event MouseEventHandler NCMouseDown;
        public event MouseEventHandler NCMouseMove;
        public event MouseEventHandler NCMouseUp;
        public event MouseEventHandler NCMouseClick;
        public event MouseEventHandler NCMouseDoubleClick;

        private readonly BufferedGraphicsContext m_BufferedGraphicsContext;
        private BufferedGraphics m_BufferedGraphics;
        private Size m_CurrentCacheSize = new Size();

        private Form m_HostForm;
        private WFNew.BaseItemCollection m_BaseItemCollection;
        private NCFormButtonStackItemEx m_NCFormButtonStackItemEx;
        private NCQuickAccessToolbarItemEx m_NCQuickAccessToolbarItemEx;

        public TBFormSkinHelper(Form hostForm)
            : base(hostForm)
        {
            this.m_BufferedGraphicsContext = BufferedGraphicsManager.Current;
            //
            //
            //
            this.m_HostForm = hostForm;
            //
            this.m_BaseItemCollection = new GISShare.Controls.WinForm.WFNew.BaseItemCollection(this);
            //((WFNew.ILockCollectionHelper)this.m_BaseItemCollection).SetLocked(false);//解锁
            this.m_NCFormButtonStackItemEx = new NCFormButtonStackItemEx(this);
            this.m_BaseItemCollection.Add(this.m_NCFormButtonStackItemEx);
            this.m_NCQuickAccessToolbarItemEx = new NCQuickAccessToolbarItemEx(this);
            this.m_BaseItemCollection.Add(this.m_NCQuickAccessToolbarItemEx);
            ((WFNew.ILockCollectionHelper)this.m_BaseItemCollection).SetLocked(true);//加锁
        }

        #region 注册
        protected override void RegisterEventHandlers(Control hostControl)
        {
            base.RegisterEventHandlers(hostControl);
            //
            hostControl.Paint += HostControl_Paint;
            hostControl.LostFocus += new EventHandler(hostControl_LostFocus);
            hostControl.KeyDown += new KeyEventHandler(HostControl_KeyDown);
            hostControl.KeyUp += new KeyEventHandler(HostControl_KeyUp);
            hostControl.KeyPress += new KeyPressEventHandler(HostControl_KeyPress);
            hostControl.MouseWheel += new MouseEventHandler(HostControl_MouseWheel);
            hostControl.MouseDown += new MouseEventHandler(HostControl_MouseDown);
            hostControl.MouseUp += new MouseEventHandler(HostControl_MouseUp);
            hostControl.MouseMove += new MouseEventHandler(HostControl_MouseMove);
            hostControl.MouseEnter += new EventHandler(HostControl_MouseEnter);
            hostControl.MouseLeave += new EventHandler(HostControl_MouseLeave);
            hostControl.MouseClick += new MouseEventHandler(HostControl_MouseClick);
            hostControl.MouseDoubleClick += new MouseEventHandler(HostControl_MouseDoubleClick);
            hostControl.EnabledChanged += new EventHandler(HostControl_EnabledChanged);
            hostControl.VisibleChanged += new EventHandler(HostControl_VisibleChanged);
            hostControl.TextChanged += HostControl_TextChanged;
        }
        protected override void UnregisterEventHandlers(Control hostControl)
        {
            base.UnregisterEventHandlers(hostControl);
            //
            hostControl.Paint -= HostControl_Paint;
            hostControl.LostFocus -= new EventHandler(hostControl_LostFocus);
            hostControl.KeyDown -= new KeyEventHandler(HostControl_KeyDown);
            hostControl.KeyUp -= new KeyEventHandler(HostControl_KeyUp);
            hostControl.KeyPress -= new KeyPressEventHandler(HostControl_KeyPress);
            hostControl.MouseWheel -= new MouseEventHandler(HostControl_MouseWheel);
            hostControl.MouseDown -= new MouseEventHandler(HostControl_MouseDown);
            hostControl.MouseUp -= new MouseEventHandler(HostControl_MouseUp);
            hostControl.MouseMove -= new MouseEventHandler(HostControl_MouseMove);
            hostControl.MouseEnter -= new EventHandler(HostControl_MouseEnter);
            hostControl.MouseLeave -= new EventHandler(HostControl_MouseLeave);
            hostControl.MouseClick -= new MouseEventHandler(HostControl_MouseClick);
            hostControl.MouseDoubleClick -= new MouseEventHandler(HostControl_MouseDoubleClick);
            hostControl.EnabledChanged -= new EventHandler(HostControl_EnabledChanged);
            hostControl.VisibleChanged -= new EventHandler(HostControl_VisibleChanged);
            hostControl.TextChanged -= HostControl_TextChanged;
        }
        void HostControl_Paint(object sender, PaintEventArgs e)
        {
            if (this is IUICollectionItem)
            {
                //发送消息
                ((IMessageChain)this).SendMessage(new MessageInfo(this, MessageStyle.eMSViewInfo, new ViewInfo(this.Visible, this.Enabled, false)));
            }
            //发送消息
            ((IMessageChain)this).SendMessage(new MessageInfo(this, MessageStyle.eMSPaint, e));
            this.OnPaint(e);
        }
        void hostControl_LostFocus(object sender, EventArgs e)
        {
            //发送消息
            ((IMessageChain)this).SendMessage(new MessageInfo(this, MessageStyle.eMSLostFocus, e));
        }
        void HostControl_KeyDown(object sender, KeyEventArgs e)
        {
            //发送消息
            ((IMessageChain)this).SendMessage(new MessageInfo(this, MessageStyle.eMSKeyDown, e));
        }
        void HostControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            //发送消息
            ((IMessageChain)this).SendMessage(new MessageInfo(this, MessageStyle.eMSKeyPress, e));
        }
        void HostControl_KeyUp(object sender, KeyEventArgs e)
        {
            //发送消息
            ((IMessageChain)this).SendMessage(new MessageInfo(this, MessageStyle.eMSKeyUp, e));
        }
        void HostControl_MouseWheel(object sender, MouseEventArgs e)
        {
            //发送消息
            ((IMessageChain)this).SendMessage(new MessageInfo(this, MessageStyle.eMSMouseWheel, e));
        }
        void HostControl_MouseDown(object sender, MouseEventArgs e)
        {
            //发送消息
            ((IMessageChain)this).SendMessage(new MessageInfo(this, MessageStyle.eMSMouseDown, e));
        }
        void HostControl_MouseUp(object sender, MouseEventArgs e)
        {
            //发送消息
            ((IMessageChain)this).SendMessage(new MessageInfo(this, MessageStyle.eMSMouseUp, e));
        }
        void HostControl_MouseMove(object sender, MouseEventArgs e)
        {
            //发送消息
            ((IMessageChain)this).SendMessage(new MessageInfo(this, MessageStyle.eMSMouseMove, e));
        }
        void HostControl_MouseEnter(object sender, EventArgs e)
        {
            //发送消息
            ((IMessageChain)this).SendMessage(new MessageInfo(this, MessageStyle.eMSMouseEnter, e));
        }
        void HostControl_MouseLeave(object sender, EventArgs e)
        {
            //发送消息
            ((IMessageChain)this).SendMessage(new MessageInfo(this, MessageStyle.eMSMouseLeave, e));
        }
        void HostControl_MouseClick(object sender, MouseEventArgs e)
        {
            //发送消息
            ((IMessageChain)this).SendMessage(new MessageInfo(this, MessageStyle.eMSMouseClick, e));
        }
        void HostControl_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //发送消息
            ((IMessageChain)this).SendMessage(new MessageInfo(this, MessageStyle.eMSMouseDoubleClick, e));
        }
        void HostControl_EnabledChanged(object sender, EventArgs e)
        {
            //发送消息
            ((IMessageChain)this).SendMessage(new MessageInfo(this, MessageStyle.eMSEnabledChanged, new BoolValueChangedEventArgs(this.Enabled)));
        }
        void HostControl_VisibleChanged(object sender, EventArgs e)
        {
            //发送消息
            ((IMessageChain)this).SendMessage(new MessageInfo(this, MessageStyle.eMSVisibleChanged, new BoolValueChangedEventArgs(this.Visible)));
        }
        void HostControl_TextChanged(object sender, EventArgs e)
        {
            this.OnTextChanged(e);
        }
        #endregion

        #region IOwner
        public WFNew.IOwner pOwner
        {
            get { return null; }
        }

        void WFNew.IOwner.Refresh()
        {
            this.RefreshNC();
        }

        void WFNew.IOwner.Invalidate(Rectangle rectangle)
        {
            this.RefreshNC();
        }

        Point WFNew.IOwner.PointToClient(Point point)
        {
            return this.PointToClientNC(point);
        }

        Point WFNew.IOwner.PointToScreen(Point point)
        {
            return this.PointToScreenNC(point);
        }
        #endregion

        #region IBaseItemOwner
        public bool Overflow
        { 
            get { return false; }
        }

        public Rectangle ItemsRectangle
        { 
            get { return this.ItemsRectangleNC; } 
        }

        [Browsable(false), Description("其子视图项展现矩形"), Category("布局")]
        public Rectangle ItemsViewRectangle
        {
            get
            {
                return this.ItemsRectangle;
            }
        }

        public WFNew.IBaseItemOwner pBaseItemOwner
        { 
            get { return null; } 
        }
        #endregion

        #region IOffsetNC
        public int NCOffsetX
        {
            get { return -this.FrameBorderSize.Width; }
        }

        public int NCOffsetY
        {
            get { return -(this.FrameBorderSize.Height + this.CaptionHeight); } 
        }
        #endregion

        #region IOwnerNC
        public Point PointToClientNC(Point point)
        {
            if (this.m_HostForm == null || this.m_HostForm.IsDisposed) return new Point(-1, -1);
            point = this.m_HostForm.PointToClient(point);
            point.X -= this.NCOffsetX;
            point.Y -= this.NCOffsetY;
            return point;
        }

        public Point PointToScreenNC(Point point)
        {
            if (this.m_HostForm == null || this.m_HostForm.IsDisposed) return new Point(-1, -1);
            point = this.m_HostForm.PointToScreen(point);
            point.X += this.NCOffsetX;
            point.Y += this.NCOffsetY;
            return point;
        }
        #endregion

        #region IBaseItemOwnerNC
        public Rectangle ItemsRectangleNC
        {
            get { return this.CaptionRectangle; } 
        }

        [Browsable(false), Description("非客户区其子项视图展现矩形"), Category("布局")]
        public Rectangle ItemsViewRectangleNC
        {
            get { return this.ItemsRectangleNC; }
        }

        public void RefreshNC()
        {
            if (this.m_HostForm == null || 
                this.m_HostForm.IsDisposed || 
                this.m_HostForm.FormBorderStyle == FormBorderStyle.None ||
                (this.m_HostForm.IsMdiChild && this.m_HostForm.WindowState == FormWindowState.Maximized) ||
                (!this.m_HostForm.IsMdiChild && this.m_HostForm.WindowState == FormWindowState.Minimized)) return;
            //
            this.WndNCPaint(true);
        }

        public bool RefreshExNC()
        {
            return this.WndNCPaint(true);
        }
        #endregion

        #region IBaseItemOwnerNC2
        private bool m_CancelItemsEventNC = false;
        public bool CancelItemsEventNC
        {
            get { return m_CancelItemsEventNC || !this.IsActive; }
            set { m_CancelItemsEventNC = value; }
        }

        public bool CancelItemsDrawEventNC
        {
            get { return false; }
        }
        #endregion

        #region ICollectionItem2
        IBaseItem ICollectionItem2.GetBaseItem(string strName)
        {
            IBaseItem pBaseItem = null;
            foreach (IBaseItem one in ((ICollectionItem)this).BaseItems)
            {
                if (one.Name == strName) pBaseItem = one;
                else
                {
                    ICollectionItem2 pCollectionItem2 = one as ICollectionItem2;
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
        public IBaseItem GetBaseItem2(string strName)
        {
            IBaseItem pBaseItem = null;
            foreach (IBaseItem one in this.ToolbarItems)
            {
                if (one.Name == strName) pBaseItem = one;
                else
                {
                    ICollectionItem3 pCollectionItem3 = one as ICollectionItem3;
                    if (pCollectionItem3 != null)
                    {
                        pBaseItem = pCollectionItem3.GetBaseItem2(strName);
                    }
                }
                //
                if (pBaseItem != null) break;
            }
            //
            return pBaseItem;
        }
        #endregion

        #region IUICollectionItemNC
        bool IUICollectionItemNC.HaveVisibleBaseItemNC
        {
            get
            {
                foreach (WFNew.BaseItem one in ((WFNew.ICollectionItem)this).BaseItems)
                {
                    if (one.Visible) return true;
                }
                //
                return false;
            }
        }

        System.Drawing.Rectangle IUICollectionItemNC.ItemsRectangleNC
        {
            get 
            {
                return this.CaptionRectangle;
            }
        }
        #endregion

        #region IFormX
        bool m_IsMiddleCaptionText = false;
        public bool IsMiddleCaptionText
        {
            get { return m_IsMiddleCaptionText; }
            set { m_IsMiddleCaptionText = value; }
        }

        public bool ShowQuickAccessToolbar
        {
            get { return this.m_NCQuickAccessToolbarItemEx.Visible; }
            set { this.m_NCQuickAccessToolbarItemEx.Visible = value; }
        }

        public WFNew.QuickAccessToolbarStyle eQuickAccessToolbarStyle
        {
            get { return this.m_NCQuickAccessToolbarItemEx.eQuickAccessToolbarStyle; }
            set { this.m_NCQuickAccessToolbarItemEx.eQuickAccessToolbarStyle = value; }
        }

        public BaseItemCollection ToolbarItems
        {
            get { return this.m_NCQuickAccessToolbarItemEx.BaseItems as BaseItemCollection; }
        }

        public Icon Icon
        {
            get { return this.m_HostForm.Icon; }
        }

        private bool m_IsActive = false;
        public bool IsActive
        {
            get { return m_IsActive; }
        }
        protected bool SetIsActive(bool bIsActive)
        {
            if (this.m_IsActive == bIsActive) return false;
            this.m_IsActive = bIsActive;
            return true;
        }
        protected bool SetIsActiveEx(bool bIsActive)
        {
            if (this.m_IsActive == bIsActive) return false;
            this.m_IsActive = bIsActive;
            this.RefreshNC();
            return true;
        }

        public int CaptionHeight
        {
            get
            {
                if (this.m_HostForm == null || this.m_HostForm.IsDisposed) return 0;
                ////
                //switch (this.m_HostForm.FormBorderStyle) 
                //{
                //    case FormBorderStyle.Sizable:
                //    case FormBorderStyle.Fixed3D:
                //    case FormBorderStyle.FixedDialog:
                //    case FormBorderStyle.FixedSingle:
                //        return SystemInformation.CaptionHeight;
                //    case FormBorderStyle.FixedToolWindow:
                //    case FormBorderStyle.SizableToolWindow:
                //        return SystemInformation.ToolWindowCaptionHeight;
                //}
                return FormInformation.GetCaptionHeight(this.m_HostForm);
            }
        }

        public int FormButtonStackItemNCWidth 
        {
            get { return this.m_NCFormButtonStackItemEx.Width; }
        }

        public int MinCaptionTextWidth
        {
            get { return CRT_MINCAPTIONTEXTWIDTH; }
        }

        public Size MinimumSize
        {
            get
            {
                if (this.m_HostForm == null || this.m_HostForm.IsDisposed) return new Size();
                return this.m_HostForm.MinimumSize;
            }
        }

        public Size MaximumSize
        {
            get
            {
                if (this.m_HostForm == null || this.m_HostForm.IsDisposed) return new Size();
                return this.m_HostForm.MaximumSize;
            }
        }

        public bool IsDrawIcon
        {
            get { return this.m_HostForm.ShowIcon && this.m_HostForm.Icon != null; }
        }

        public bool CancelDrawNC
        {
            get { return !this.IsProcessNCArea; }
        }

        public bool IsProcessNCArea
        {
            get
            {
                // check if we should process the nc area
                //return 
                //    !(
                //    this.m_HostForm == null || 
                //    this.m_HostForm.IsDisposed ||
                //    this.m_HostForm.MdiParent != null && this.WindowState == FormWindowState.Maximized
                //    );
                //return !(
                //         this.m_HostForm == null ||
                //         this.m_HostForm.IsDisposed ||
                //         (this.m_HostForm.IsMdiChild && this.m_HostForm.WindowState == FormWindowState.Maximized)
                //        );
                return !(
                         this.m_HostForm == null || this.m_HostForm.IsDisposed
                        );
            }
        }

        public bool IsMdiContainer
        {
            get
            {
                if (this.m_HostForm == null || this.m_HostForm.IsDisposed) return false;
                return this.m_HostForm.IsMdiContainer;
            }
        }

        public bool IsMdiChild
        {
            get
            {
                if (this.m_HostForm == null || this.m_HostForm.IsDisposed) return false;
                return this.m_HostForm.IsMdiChild;
            }
        }

        public bool HasMenu
        {
            get { return FormInformation.HasMenu(this.m_HostForm); }
        }

        public FormWindowState WindowState
        {
            get
            {
                if (this.m_HostForm == null || this.m_HostForm.IsDisposed) return FormWindowState.Minimized;
                return this.m_HostForm.WindowState;
            }
        }

        public FormBorderStyle FormBorderStyle
        {
            get
            {
                if (this.m_HostForm == null || this.m_HostForm.IsDisposed) return FormBorderStyle.None;
                return this.m_HostForm.FormBorderStyle;
            }
        }

        public Size FrameBorderSize
        {
            get
            {
                if (this.m_HostForm == null || this.m_HostForm.IsDisposed) return SystemInformation.FrameBorderSize; 
                ////
                //switch (this.m_HostForm.FormBorderStyle)
                //{
                //    case FormBorderStyle.Sizable:
                //    case FormBorderStyle.SizableToolWindow:
                //        return SystemInformation.FrameBorderSize;
                //    case FormBorderStyle.Fixed3D:
                //        return SystemInformation.Border3DSize;
                //    case FormBorderStyle.FixedDialog:
                //    case FormBorderStyle.FixedSingle:
                //    case FormBorderStyle.FixedToolWindow:
                //        return SystemInformation.FixedFrameBorderSize;
                //}
                return FormInformation.GetBorderSize(this.m_HostForm); 
            }
        }

        public Rectangle RestoreBounds
        {
            get
            {
                if (this.m_HostForm == null || this.m_HostForm.IsDisposed) return new Rectangle();
                return this.m_HostForm.RestoreBounds;
            }
        }

        public Rectangle NCRectangleEx
        {
            get
            {
                return new Rectangle(0, 0, this.m_HostForm.Width, this.FrameBorderSize.Height + this.CaptionHeight);
            }
        }

        public Rectangle FrameRectangle
        {
            get
            {
                return new Rectangle(0, 0, this.m_CurrentCacheSize.Width - 1, this.m_CurrentCacheSize.Height - 1);
            }
        }

        public Rectangle CaptionRectangle
        {
            get
            {
                //if (this.m_HostForm == null || this.m_HostForm.IsDisposed ) return new Rectangle();
                ////
                return new Rectangle(
                    this.FrameBorderSize.Width,
                    this.FrameBorderSize.Height,
                    this.m_CurrentCacheSize.Width - 2 * this.FrameBorderSize.Width, 
                    this.CaptionHeight);
            }
        }

        public Rectangle CaptionIconRectangle
        {
            get
            {
                return new Rectangle
                    (
                    this.FrameBorderSize.Width + 1, 
                    this.FrameBorderSize.Height + 1, 
                    this.CaptionHeight - 2, 
                    this.CaptionHeight - 2
                    );
            }
        }

        public Rectangle CaptionToolbarRectangle 
        {
            get
            {
                return this.m_NCQuickAccessToolbarItemEx.DisplayRectangle; 
            }
        }

        public Rectangle CaptionTextRectangle
        {
            get
            {
                if (this.ShowQuickAccessToolbar)
                {
                    Rectangle rectangle = this.CaptionToolbarRectangle;
                    return Rectangle.FromLTRB
                        (
                        rectangle.Right + 2,
                        rectangle.Top,
                        this.m_NCFormButtonStackItemEx.DisplayRectangle.Location.X - 2,
                        rectangle.Bottom
                        );
                }
                else if (this.IsDrawIcon)
                {
                    Rectangle rectangle = this.CaptionIconRectangle;
                    return Rectangle.FromLTRB
                        (
                        rectangle.Right + 2,
                        rectangle.Top,
                        this.m_NCFormButtonStackItemEx.DisplayRectangle.Location.X - 2,
                        rectangle.Bottom
                        );
                }
                else
                {
                    Rectangle rectangle = this.CaptionIconRectangle;
                    return Rectangle.FromLTRB
                        (
                        rectangle.Left,
                        rectangle.Top,
                        this.m_NCFormButtonStackItemEx.DisplayRectangle.Location.X - 2,
                        rectangle.Bottom
                        );
                }
            }
        }

        public Rectangle ScreenRectangle
        {
            get { return FormInformation.GetScreenRect(this.m_HostForm); }
        }

        public void Activate() 
        {
            if (this.m_HostForm == null || this.m_HostForm.IsDisposed) return;
            //
            this.m_HostForm.Activate();
        }

        public virtual void GetRadiusInfo(out int iLeftTopRadius, out  int iRightTopRadius, out int iLeftBottomRadius, out int iRightBottomRadius)
        {
            iLeftTopRadius = 9;
            iRightTopRadius = 9;
            iLeftBottomRadius = 0;
            iRightBottomRadius = 0;
            //
            if (this.IsMdiChild && this.WindowState == FormWindowState.Minimized) return;
            //
            switch (this.FormBorderStyle)
            {
                case FormBorderStyle.FixedToolWindow:
                case FormBorderStyle.SizableToolWindow:
                    iLeftTopRadius = 0;
                    iRightTopRadius = 0;
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region IFormEvent
        public event PaintEventHandler NCPaint;
        #endregion

        #region IReset
        void IReset.Reset()
        {
            this.m_MouseDown = false;
            this.m_MouseEnter = false;
        }
        #endregion

        #region IMessagePermeate
        bool IMessagePermeate.PermeateCancelEvent(MessageStyle eMessageStyle)
        {
            return false;
        }
        #endregion

        #region IMessageChain
        void IMessageChain.SendMessage(MessageInfo messageInfo)
        {
            switch (messageInfo.eMessageStyle)
            {
                case MessageStyle.eMSViewInfo:
                    this.MSViewInfo(messageInfo);
                    break;
                //
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
                    this.MessageMonitor(messageInfo);
                    break;
            }
        }
        private void MSViewInfo(MessageInfo messageInfo)
        {
            //植入监听
            this.MessageMonitor(messageInfo);
        }
        private void MSPaint(MessageInfo messageInfo)
        {
            //植入监听
            this.MessageMonitor(messageInfo);
        }
        private void MSLostFocus(MessageInfo messageInfo)
        {
            //植入监听
            this.MessageMonitor(messageInfo);
        }
        private void MSKeyDown(MessageInfo messageInfo)
        {
            //植入监听
            this.MessageMonitor(messageInfo);
        }
        private void MSKeyUp(MessageInfo messageInfo)
        {
            //植入监听
            this.MessageMonitor(messageInfo);
        }
        private void MSKeyPress(MessageInfo messageInfo)
        {
            //植入监听
            this.MessageMonitor(messageInfo);
        }
        private void MSMouseWheel(MessageInfo messageInfo)
        {
            //植入监听
            this.MessageMonitor(messageInfo);
        }
        private bool m_MouseDown = false;
        private void MSMouseDown(MessageInfo messageInfo)
        {
            this.m_MouseDown = true;
            //if (this.RefreshBaseItemState) this.Refresh();
            //植入监听
            this.MessageMonitor(messageInfo);
        }
        private void MSMouseUp(MessageInfo messageInfo)
        {
            this.m_MouseDown = false;
            MouseEventArgs mouseEventArgs = messageInfo.MessageParameter as MouseEventArgs;
            if (mouseEventArgs == null || !this.DisplayRectangle.Contains(mouseEventArgs.Location))
            {
                this.m_MouseEnter = false;
            }
            //if (this.RefreshBaseItemState) this.Refresh();
            //植入监听
            this.MessageMonitor(messageInfo);
        }
        private bool m_MouseEnter = false;
        private void MSMouseMove(MessageInfo messageInfo)
        {
            //if (this.m_MouseDown) return;
            //植入监听
            this.MessageMonitor(messageInfo);
        }
        private void MSMouseEnter(MessageInfo messageInfo)
        {
            if (!this.m_MouseEnter)
            {
                this.m_MouseEnter = true;
                //if (this.RefreshBaseItemState) { this.Refresh(); }
            }
        }
        private void MSMouseLeave(MessageInfo messageInfo)
        {
            if (this.m_MouseEnter)
            {
                this.m_MouseEnter = false;
                //if (this.RefreshBaseItemState) { this.Refresh(); }
                //植入监听
                this.MessageMonitor(messageInfo);
            }
        }
        private void MSMouseClick(MessageInfo messageInfo)
        {
            //植入监听
            this.MessageMonitor(messageInfo);
        }
        private void MSMouseDoubleClick(MessageInfo messageInfo)
        {
            //植入监听
            this.MessageMonitor(messageInfo);
        }
        private void MSEnabledChanged(MessageInfo messageInfo)
        {
            this.m_MouseDown = false;
            this.m_MouseEnter = false;
            //植入监听
            this.MessageMonitor(messageInfo);
        }
        private void MSVisibleChanged(MessageInfo messageInfo)
        {
            this.m_MouseDown = false;
            this.m_MouseEnter = false;
            //植入监听
            this.MessageMonitor(messageInfo);
        }

        /// <summary>
        /// 用来监听消息
        /// </summary>
        /// <param name="messageInfo"></param>
        protected virtual void MessageMonitor(MessageInfo messageInfo)
        {
            switch (messageInfo.eMessageStyle)
            {
                case MessageStyle.eMSNCPaint:
                    ((IMessageChain)this.m_NCFormButtonStackItemEx).SendMessage(new MessageInfo(messageInfo.Sender, MessageStyle.eMSPaint, messageInfo.MessageParameter));
                    ((IMessageChain)this.m_NCQuickAccessToolbarItemEx).SendMessage(new MessageInfo(messageInfo.Sender, MessageStyle.eMSPaint, messageInfo.MessageParameter));
                    break;
                case MessageStyle.eMSNCMouseDown:
                    ((IMessageChain)this.m_NCFormButtonStackItemEx).SendMessage(new MessageInfo(messageInfo.Sender, MessageStyle.eMSMouseDown, messageInfo.MessageParameter));
                    ((IMessageChain)this.m_NCQuickAccessToolbarItemEx).SendMessage(new MessageInfo(messageInfo.Sender, MessageStyle.eMSMouseDown, messageInfo.MessageParameter));
                    break;
                case MessageStyle.eMSNCMouseUp:
                    ((IMessageChain)this.m_NCFormButtonStackItemEx).SendMessage(new MessageInfo(messageInfo.Sender, MessageStyle.eMSMouseUp, messageInfo.MessageParameter));
                    ((IMessageChain)this.m_NCQuickAccessToolbarItemEx).SendMessage(new MessageInfo(messageInfo.Sender, MessageStyle.eMSMouseUp, messageInfo.MessageParameter));
                    break;
                case MessageStyle.eMSNCMouseMove:
                    ((IMessageChain)this.m_NCFormButtonStackItemEx).SendMessage(new MessageInfo(messageInfo.Sender, MessageStyle.eMSMouseMove, messageInfo.MessageParameter));
                    ((IMessageChain)this.m_NCQuickAccessToolbarItemEx).SendMessage(new MessageInfo(messageInfo.Sender, MessageStyle.eMSMouseMove, messageInfo.MessageParameter));
                    break;
                case MessageStyle.eMSNCMouseClick:
                    ((IMessageChain)this.m_NCFormButtonStackItemEx).SendMessage(new MessageInfo(messageInfo.Sender, MessageStyle.eMSMouseClick, messageInfo.MessageParameter));
                    ((IMessageChain)this.m_NCQuickAccessToolbarItemEx).SendMessage(new MessageInfo(messageInfo.Sender, MessageStyle.eMSMouseClick, messageInfo.MessageParameter));
                    break;
                case MessageStyle.eMSNCMouseDoubleClick:
                    ((IMessageChain)this.m_NCFormButtonStackItemEx).SendMessage(new MessageInfo(messageInfo.Sender, MessageStyle.eMSMouseDoubleClick, messageInfo.MessageParameter));
                    ((IMessageChain)this.m_NCQuickAccessToolbarItemEx).SendMessage(new MessageInfo(messageInfo.Sender, MessageStyle.eMSMouseDoubleClick, messageInfo.MessageParameter));
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region 覆盖
        /// <summary>
        /// Invokes the default window procedure associated with this window.
        /// </summary>
        /// <param name="m">A <see cref="T:System.Windows.Forms.Message"/> that is associated with the current Windows message.</param>
        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void WndProc(ref Message m)
        {
            if (this.IsProcessNCArea)
            {
                switch (m.Msg)
                {
                    // update form data on style change
                    case (int)GISShare.Win32.Msgs.WM_STYLECHANGED:
                        //System.Diagnostics.Debug.WriteLine(String.Format("{0}----{1}", this.Name, "WM_STYLECHANGED"));
                        this.UpdateStyle();
                        this.SetRegion(this.Size, true);
                        break;

                    #region Handle Form Activation
                    case (int)GISShare.Win32.Msgs.WM_ACTIVATEAPP:
                        //System.Diagnostics.Debug.WriteLine(String.Format("{0}----{1}", this.Name, "WM_ACTIVATEAPP"));
                        this.SetIsActive((int)m.WParam != 0);
                        base.WndProc(ref m);
                        this.WndNCPaint(true);
                        return;
                    // Set active state and redraw
                    case (int)GISShare.Win32.Msgs.WM_ACTIVATE:
                        //System.Diagnostics.Debug.WriteLine(String.Format("{0}----{1}", this.Name, "WM_ACTIVATE"));
                        this.SetIsActive((int)GISShare.Win32.WindowActiveStyles.WA_ACTIVE == (int)m.WParam || (int)GISShare.Win32.WindowActiveStyles.WA_CLICKACTIVE == (int)m.WParam);
                        base.WndProc(ref m);
                        this.WndNCPaint(true);
                        return;
                    case (int)GISShare.Win32.Msgs.WM_NCACTIVATE://
                        //System.Diagnostics.Debug.WriteLine(String.Format("{0}----{1}", this.Name, "WM_NCACTIVATE"));
                        this.SetIsActive((int)GISShare.Win32.WindowActiveStyles.WA_ACTIVE == (int)m.WParam || (int)GISShare.Win32.WindowActiveStyles.WA_CLICKACTIVE == (int)m.WParam);
                        base.WndProc(ref m);
                        this.WndNCPaint(true);
                        return;//key
                    // set active and redraw on activation 
                    case (int)GISShare.Win32.Msgs.WM_MDIACTIVATE:
                        //System.Diagnostics.Debug.WriteLine(String.Format("{0}----{1}", this.Name, "WM_MDIACTIVATE"));
                        base.WndProc(ref m);
                        if (m.WParam == this.DependObjectHandle) this.SetIsActiveEx(false);
                        else if (m.LParam == this.DependObjectHandle) this.SetIsActiveEx(true);
                        //this.WndNCPaint(true);
                        return;
                    #endregion

                    #region Handle Mouse Processing
                    case (int)GISShare.Win32.Msgs.WM_NCLBUTTONDOWN:
                        //System.Diagnostics.Debug.WriteLine(String.Format("{0}----{1}", this.Name, "WM_NCLBUTTONDOWN"));
                        if (this.WndNCMouseDown(m.HWnd, MouseButtons.Left, this.GetPointNC((int)m.LParam))) return;
                        break;
                    case (int)GISShare.Win32.Msgs.WM_NCMBUTTONDOWN:
                        //System.Diagnostics.Debug.WriteLine(String.Format("{0}----{1}", this.Name, "WM_NCMBUTTONDOWN"));
                        if (this.WndNCMouseDown(m.HWnd, MouseButtons.Middle, this.GetPointNC((int)m.LParam))) return;
                        break;
                    case (int)GISShare.Win32.Msgs.WM_NCRBUTTONDOWN:
                        //System.Diagnostics.Debug.WriteLine(String.Format("{0}----{1}", this.Name, "WM_NCRBUTTONDOWN"));
                        if (this.WndNCMouseDown(m.HWnd, MouseButtons.Right, this.GetPointNC((int)m.LParam))) return;
                        break;
                    case (int)GISShare.Win32.Msgs.WM_NCMOUSEMOVE:
                        //System.Diagnostics.Debug.WriteLine(String.Format("{0}----{1}", this.Name, "WM_NCMOUSEMOVE"));
                        this.WndNCMouseMove(m.HWnd, this.GetPointNC((int)m.LParam));
                        break;
                    case (int)GISShare.Win32.Msgs.WM_NCLBUTTONUP:
                        //System.Diagnostics.Debug.WriteLine(String.Format("{0}----{1}", this.Name, "WM_NCLBUTTONUP"));
                        if (this.WndNCMouseUp(m.HWnd, MouseButtons.Left, this.GetPointNC((int)m.LParam))) return;
                        break;
                    case (int)GISShare.Win32.Msgs.WM_NCMBUTTONUP:
                        //System.Diagnostics.Debug.WriteLine(String.Format("{0}----{1}", this.Name, "WM_NCMBUTTONUP"));
                        if (this.WndNCMouseUp(m.HWnd, MouseButtons.Middle, this.GetPointNC((int)m.LParam))) return;
                        break;
                    case (int)GISShare.Win32.Msgs.WM_NCRBUTTONUP:
                        //System.Diagnostics.Debug.WriteLine(String.Format("{0}----{1}", this.Name, "WM_NCRBUTTONUP"));
                        if (this.WndNCMouseUp(m.HWnd, MouseButtons.Right, this.GetPointNC((int)m.LParam))) return;
                        break;
                    case (int)GISShare.Win32.Msgs.WM_NCLBUTTONDBLCLK:
                        //System.Diagnostics.Debug.WriteLine(String.Format("{0}----{1}", this.Name, "WM_NCLBUTTONDBLCLK"));
                        if (this.WndNCMouseDoubleClick(m.HWnd, MouseButtons.Left, this.GetPointNC((int)m.LParam))) return;
                        break;
                    case (int)GISShare.Win32.Msgs.WM_NCMBUTTONDBLCLK:
                        //System.Diagnostics.Debug.WriteLine(String.Format("{0}----{1}", this.Name, "WM_NCMBUTTONDBLCLK"));
                        if (this.WndNCMouseDoubleClick(m.HWnd, MouseButtons.Middle, this.GetPointNC((int)m.LParam))) return;
                        break;
                    case (int)GISShare.Win32.Msgs.WM_NCRBUTTONDBLCLK:
                        //System.Diagnostics.Debug.WriteLine(String.Format("{0}----{1}", this.Name, "WM_NCRBUTTONDBLCLK"));
                        if (this.WndNCMouseDoubleClick(m.HWnd, MouseButtons.Right, this.GetPointNC((int)m.LParam))) return;
                        break;
                    //
                    case (int)GISShare.Win32.Msgs.WM_LBUTTONDOWN:
                        //System.Diagnostics.Debug.WriteLine(String.Format("{0}----{1}", this.Name, "WM_LBUTTONDOWN"));
                        if (this.IsMdiChild && !this.IsActive) this.Activate();
                        break;
                    #endregion

                    #region Size Processing
                    // Set region as window is shown
                    case (int)GISShare.Win32.Msgs.WM_SHOWWINDOW:
                        //System.Diagnostics.Debug.WriteLine(String.Format("{0}----{1}", this.Name, "WM_SHOWWINDOW"));
                        this.SetRegion(this.Size, false);
                        break;
                    // adjust region on resize
                    case (int)GISShare.Win32.Msgs.WM_SIZE:
                        //System.Diagnostics.Debug.WriteLine(String.Format("{0}----{1}", this.Name, "WM_SIZE"));
                        base.WndProc(ref m);
                        this.WndNCPaint(m);
                        return;
                    // ensure that the window doesn't overlap docked toolbars on desktop (like taskbar)
                    case (int)GISShare.Win32.Msgs.WM_GETMINMAXINFO:
                        //System.Diagnostics.Debug.WriteLine(String.Format("{0}----{1}", this.Name, "WM_GETMINMAXINFO"));
                        if (this.CalculateMaxSize(ref m)) return;
                        break;
                    //update region on resize
                    case (int)GISShare.Win32.Msgs.WM_WINDOWPOSCHANGING:
                        //System.Diagnostics.Debug.WriteLine(String.Format("{0}----{1}", this.Name, "WM_WINDOWPOSCHANGING"));
                        GISShare.Win32.WINDOWPOS wndPos = (GISShare.Win32.WINDOWPOS)m.GetLParam(typeof(GISShare.Win32.WINDOWPOS));
                        if ((wndPos.flags & (int)GISShare.Win32.SWPFlags.SWP_NOSIZE) == 0)
                        {
                            this.SetRegion(new Size(wndPos.cx, wndPos.cy), false);
                            //this.OnSetRegion(this.m_HostForm, new Size(wndPos.cx, wndPos.cy));
                        }
                        break;
                    // remove region on maximize or repaint on resize
                    case (int)GISShare.Win32.Msgs.WM_WINDOWPOSCHANGED:
                        //System.Diagnostics.Debug.WriteLine(String.Format("{0}----{1}", this.Name, "WM_WINDOWPOSCHANGED"));
                        if (this.WindowState == FormWindowState.Maximized) this.m_HostForm.Region = null;
                        GISShare.Win32.WINDOWPOS wndPos2 = (GISShare.Win32.WINDOWPOS)m.GetLParam(typeof(GISShare.Win32.WINDOWPOS));
                        if ((wndPos2.flags & (int)GISShare.Win32.SWPFlags.SWP_NOSIZE) == 0)
                        {
                            base.WndProc(ref m);
                            this.WndNCPaint(true);
                            return;
                        }
                        break;
                    #endregion

                    #region Non Client Area Handling
                    // paint the non client area
                    case (int)GISShare.Win32.Msgs.WM_NCPAINT:
                        //System.Diagnostics.Debug.WriteLine(String.Format("{0}----{1}", this.Name, "WM_NCPAINT"));
                        if (this.WndNCPaint(true))
                        {
                            m.Result = (IntPtr)1; return;
                        }
                        break;
                    // calculate the non client area size
                    case (int)GISShare.Win32.Msgs.WM_NCCALCSIZE:
                        //System.Diagnostics.Debug.WriteLine(String.Format("{0}----{1}", this.Name, "WM_NCCALCSIZE"));
                        if (m.WParam == (IntPtr)1)
                        {
                            if (this.IsMdiChild && this.WindowState == FormWindowState.Maximized)
                            {
                                //this.UpdateStyle(); 
                                //this.Refresh(); 
                                break;
                            }
                            // add caption height to non client area
                            GISShare.Win32.NCCALCSIZE_PARAMS p = (GISShare.Win32.NCCALCSIZE_PARAMS)m.GetLParam(typeof(GISShare.Win32.NCCALCSIZE_PARAMS));
                            p.rect0.Top += this.CaptionHeight;// FormInformation.GetCaptionHeight(this.m_HostForm);
                            System.Runtime.InteropServices.Marshal.StructureToPtr(p, m.LParam, true);
                        }
                        break;
                    //non client hit test
                    case (int)GISShare.Win32.Msgs.WM_NCHITTEST:
                        //System.Diagnostics.Debug.WriteLine(String.Format("{0}----{1}", this.Name, "WM_NCHITTEST"));
                        if (this.WndNCHitTest(ref m)) return;
                        break;
                    #endregion
                }
            }
            //
            base.WndProc(ref m);
        }
        private void UpdateStyle()
        {
            // remove the border style
            Int32 currentStyle = GISShare.Win32.API.GetWindowLong(Handle, GISShare.Win32.GWLIndex.GWL_STYLE);
            if ((currentStyle & (int)(GISShare.Win32.WindowStyles.WS_BORDER)) != 0)
            {
                currentStyle &= ~(int)(GISShare.Win32.WindowStyles.WS_BORDER);
                GISShare.Win32.API.SetWindowLong(this.DependObjectHandle, GISShare.Win32.GWLIndex.GWL_STYLE, currentStyle);
                GISShare.Win32.API.SetWindowPos
                    (
                    this.DependObjectHandle, (IntPtr)0, -1, -1, -1, -1,
                    (uint)(GISShare.Win32.SWPFlags.SWP_NOZORDER | GISShare.Win32.SWPFlags.SWP_NOSIZE | GISShare.Win32.SWPFlags.SWP_NOMOVE | GISShare.Win32.SWPFlags.SWP_FRAMECHANGED | GISShare.Win32.SWPFlags.SWP_NOREDRAW | GISShare.Win32.SWPFlags.SWP_NOACTIVATE)
                    );
            }
        }
        private void SetRegion(Size size, bool bInvalidate)
        {
            //if (form == null) return;
            ////
            ////if (form.IsMdiChild) return;
            ////
            //// Create a rounded rectangle using Gdi
            //Size cornerSize = new Size(9, 9);
            //IntPtr hRegion = GISShare.Win32.API.CreateRoundRectRgn(0, 0, size.Width + 1, size.Height + 1, cornerSize.Width, cornerSize.Height);
            //Region region = Region.FromHrgn(hRegion);
            //form.Region = region;
            //region.ReleaseHrgn(hRegion);
            if (this.m_HostForm == null || this.m_HostForm.IsDisposed) return;
            int iLeftTopRadius;
            int iRightTopRadius;
            int iLeftBottomRadius;
            int iRightBottomRadius;
            this.GetRadiusInfo(out iLeftTopRadius, out iRightTopRadius, out iLeftBottomRadius, out  iRightBottomRadius);
            this.m_HostForm.Region = new Region
                (
                GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle
                     (
                     new Rectangle(0, 0, size.Width, size.Height),
                     iLeftTopRadius,
                     iRightTopRadius,
                     iLeftBottomRadius,
                     iRightBottomRadius
                     )
                );
            if (bInvalidate) this.Invalidate(new Rectangle(0, 0, size.Width, size.Height));
        }
        private bool WndNCPaint(bool bInvalidateBuffer)
        {
            if (!this.IsProcessNCArea) return false;

            bool bResult = false;

            IntPtr hdc = (IntPtr)0;
            Graphics g = null;
            Region region = null;
            IntPtr hrgn = (IntPtr)0;

            try
            {
                // no drawing needed
                if (this.IsMdiChild && this.WindowState == FormWindowState.Maximized)
                {
                    m_CurrentCacheSize = Size.Empty;
                    return false;
                }

                // prepare image bounds
                Size borderSize = this.FrameBorderSize;// FormInformation.GetBorderSize(this.m_HostForm);
                int captionHeight = this.CaptionHeight;// FormInformation.GetCaptionHeight(this.m_HostForm);
                //
                GISShare.Win32.RECT rectScreen = new GISShare.Win32.RECT();
                GISShare.Win32.API.GetWindowRect(this.DependObjectHandle, ref rectScreen);
                //
                Rectangle rectBounds = Rectangle.FromLTRB(rectScreen.Left, rectScreen.Top, rectScreen.Right, rectScreen.Bottom);
                rectBounds.Offset(-rectBounds.X, -rectBounds.Y);

                // prepare clipping
                Rectangle rectClip = rectBounds;
                region = new Region(rectClip);
                rectClip.Inflate(-borderSize.Width, -borderSize.Height);
                rectClip.Y += captionHeight;
                rectClip.Height -= captionHeight;

                // create graphics handle
                hdc = GISShare.Win32.API.GetDCEx
                    (
                    this.DependObjectHandle,
                    (IntPtr)0,
                    (GISShare.Win32.DCXFlags.DCX_CACHE | GISShare.Win32.DCXFlags.DCX_CLIPSIBLINGS | GISShare.Win32.DCXFlags.DCX_WINDOW)
                    );
                g = Graphics.FromHdc(hdc);

                // Apply clipping
                region.Exclude(rectClip);
                hrgn = region.GetHrgn(g);
                GISShare.Win32.API.SelectClipRgn(hdc, hrgn);

                // create new buffered graphics if needed
                if (m_BufferedGraphics == null || m_CurrentCacheSize != rectBounds.Size)
                {
                    if (m_BufferedGraphics != null) m_BufferedGraphics.Dispose();
                    m_BufferedGraphics = this.m_BufferedGraphicsContext.Allocate
                        (
                        g, new Rectangle(0, 0, rectBounds.Width, rectBounds.Height)
                        );
                    m_CurrentCacheSize = rectBounds.Size;
                    bInvalidateBuffer = true;
                }
                //
                if (bInvalidateBuffer)
                {
                    this.OnNCPaint(new PaintEventArgs(m_BufferedGraphics.Graphics, this.FrameRectangle));
                    //
                    bResult = true;
                }

                // render buffered graphics 
                if (m_BufferedGraphics != null) m_BufferedGraphics.Render(g);
            }
            catch (Exception)
            {
                // error drawing
                bResult = false;
            }

            // cleanup data
            if (hdc != (IntPtr)0)
            {
                GISShare.Win32.API.SelectClipRgn(hdc, (IntPtr)0);
                GISShare.Win32.API.ReleaseDC(this.DependObjectHandle, hdc);
            }
            if (region != null && hrgn != (IntPtr)0) region.ReleaseHrgn(hrgn);
            if (region != null) region.Dispose();
            if (g != null) g.Dispose();

            return bResult;
        }
        private void WndNCPaint(Message m)
        {
            // update form styles on maximize/restore
            if (this.IsMdiChild)
            {
                //System.Diagnostics.Debug.WriteLine(String.Format("{0}----{1}", this.Name, "WndNCPaint"));
                if ((int)m.WParam == 0) this.UpdateStyle();
                if ((int)m.WParam == 2) this.Refresh();
            }

            //// update region if needed
            //bool wasMaxMin = (this.WindowState == FormWindowState.Maximized || this.WindowState == FormWindowState.Minimized);

            GISShare.Win32.RECT rect1 = new GISShare.Win32.RECT();
            GISShare.Win32.API.GetWindowRect(this.DependObjectHandle, ref rect1);

            //Rectangle rc = new Rectangle(rect1.Left, rect1.Top, rect1.Right - rect1.Left, rect1.Bottom - rect1.Top - 1);
            Rectangle rc = new Rectangle(rect1.Left, rect1.Top, rect1.Right - rect1.Left, rect1.Bottom - rect1.Top);

            //if (wasMaxMin && this.WindowState == FormWindowState.Normal && rc.Size == this.RestoreBounds.Size)
            if (this.WindowState == FormWindowState.Normal && rc.Size == this.RestoreBounds.Size)
            {
                this.SetRegion(new Size(rect1.Right - rect1.Left, rect1.Bottom - rect1.Top), true);
                //this.OnSetRegion(this.m_HostForm, new Size(rect1.Right - rect1.Left, rect1.Bottom - rect1.Top));
                this.WndNCPaint(true);
            }
        }
        private bool CalculateMaxSize(ref Message m)
        {
            //if (this.m_HostForm.Parent == null)
            if (!this.IsMdiChild)
            {
                // create minMax info for maximize data
                GISShare.Win32.MINMAXINFO info = (GISShare.Win32.MINMAXINFO)m.GetLParam(typeof(GISShare.Win32.MINMAXINFO));

                Rectangle rect = SystemInformation.WorkingArea;

                //Size fullBorderSize = new Size(SystemInformation.Border3DSize.Width + SystemInformation.BorderSize.Width, SystemInformation.Border3DSize.Height + SystemInformation.BorderSize.Height);
                Size fullBorderSize = this.FrameBorderSize;
                info.ptMaxPosition.x = rect.Left - fullBorderSize.Width;
                info.ptMaxPosition.y = rect.Top - fullBorderSize.Height;
                info.ptMaxSize.x = rect.Width + fullBorderSize.Width * 2;
                info.ptMaxSize.y = rect.Height + fullBorderSize.Height * 2;

                info.ptMinTrackSize.y += this.CaptionHeight;// FormInformation.GetCaptionHeight(this.m_HostForm);

                if (!this.MaximumSize.IsEmpty)
                {
                    info.ptMaxSize.x = Math.Min(info.ptMaxSize.x, this.MaximumSize.Width);
                    info.ptMaxSize.y = Math.Min(info.ptMaxSize.y, this.MaximumSize.Height);
                    info.ptMaxTrackSize.x = Math.Min(info.ptMaxTrackSize.x, this.MaximumSize.Width);
                    info.ptMaxTrackSize.y = Math.Min(info.ptMaxTrackSize.y, this.MaximumSize.Height);
                }

                if (!this.MinimumSize.IsEmpty)
                {
                    info.ptMinTrackSize.x = Math.Max(info.ptMinTrackSize.x, this.MinimumSize.Width);
                    info.ptMinTrackSize.y = Math.Max(info.ptMinTrackSize.y, this.MinimumSize.Height);
                }

                // set wished maximize size
                System.Runtime.InteropServices.Marshal.StructureToPtr(info, m.LParam, true);

                m.Result = (IntPtr)0;
                return true;
            }
            return false;
        }
        private bool WndNCHitTest(ref Message m)
        {
            if (!this.IsProcessNCArea) return false;

            Point point = new Point(m.LParam.ToInt32());
            Rectangle rectScreen = this.ScreenRectangle;// FormInformation.GetScreenRect(this.m_HostForm);
            Rectangle rect = rectScreen;

            // custom processing
            if (rect.Contains(point))
            {
                Size borderSize = this.FrameBorderSize;// FormInformation.GetBorderSize(this.m_HostForm);
                rect.Inflate(-borderSize.Width, -borderSize.Height);

                // let form handle hittest itself if we are on borders
                if (!rect.Contains(point))
                    return false;

                Rectangle rectCaption = rect;
                rectCaption.Height = this.CaptionHeight;// FormInformation.GetCaptionHeight(this.m_HostForm);

                // not in caption -> client
                if (!rectCaption.Contains(point))
                {
                    m.Result = (IntPtr)(int)GISShare.Win32.HitTests.HTCLIENT;
                    return true;
                }

                // on icon?
                if (this.HasMenu)
                {
                    Rectangle rectSysMenu = rectCaption;
                    rectSysMenu.Size = SystemInformation.SmallIconSize;
                    if (rectSysMenu.Contains(point))
                    {
                        m.Result = (IntPtr)(int)GISShare.Win32.HitTests.HTSYSMENU;
                        return true;
                    }
                }

                // on Button?
                Point pt = new Point(point.X - rectScreen.X, point.Y - rectScreen.Y);
                //CaptionButton sysButton = CommandButtonFromPoint(pt);
                //if (sysButton != null)
                //{
                //    m.Result = (IntPtr)sysButton.HitTest;
                //    return true;
                //}

                // on Caption?
                m.Result = (IntPtr)(int)GISShare.Win32.HitTests.HTCAPTION;
                return true;
            }
            m.Result = (IntPtr)(int)GISShare.Win32.HitTests.HTNOWHERE;
            return true;
        }
        private bool WndNCMouseMove(IntPtr iHWnd, Point point)
        {
            this.OnNCMouseMove
                (
                new MouseEventArgs(MouseButtons.None, 1, point.X, point.Y, -1)
                );
            //
            return false;
        }
        private bool WndNCMouseDown(IntPtr iHWnd, MouseButtons eMouseButtons, Point point)
        {
            this.OnNCMouseDown
                (
                new MouseEventArgs(eMouseButtons, 1, point.X, point.Y, -1)
                );
            //
            return this.ContainsWXQ(point);
        }
        private bool WndNCMouseUp(IntPtr iHWnd, MouseButtons eMouseButtons, Point point)
        {
            this.OnNCMouseClick
                (
                new MouseEventArgs(eMouseButtons, 1, point.X, point.Y, -1)
                );
            this.OnNCMouseUp
                (
                new MouseEventArgs(eMouseButtons, 1, point.X, point.Y, -1)
                );
            //
            return this.ContainsWXQ(point);
        }
        private bool WndNCMouseDoubleClick(IntPtr iHWnd, MouseButtons eMouseButtons, Point point)
        {
            this.OnNCMouseDoubleClick
                (
                new MouseEventArgs(eMouseButtons, 1, point.X, point.Y, -1)
                );
            //
            return this.ContainsWXQ(point);
        }
        private bool ContainsWXQ(Point point)//WXQ无效区
        {
            return this.m_NCQuickAccessToolbarItemEx.DisplayRectangle.Contains(point) ||
                this.m_NCFormButtonStackItemEx.DisplayRectangle.Contains(point);
        }
        private Point GetPointNC(int iLParam)
        {
            //Point point = new Point(iLParam);
            //point.Offset(-this.Left, -this.Top);
            //return point;
            Point point = this.PointToClient(GISShare.Win32.NativeMethods.LParamToMouseLocation(iLParam));
            point.X -= this.NCOffsetX;
            point.Y -= this.NCOffsetY;//if (this.WindowState != FormWindowState.Minimized)
            return point;
        }
        #endregion

        #region 嵌入消息链条
        protected virtual void OnNCPaint(PaintEventArgs e)
        {
            this.OnNCDraw(e);
            //
            if (this.NCPaint != null) { this.NCPaint(this, e); }
            //发送消息
            ((IMessageChain)this).SendMessage(new MessageInfo(this, MessageStyle.eMSNCPaint, e));
        }

        protected virtual void OnNCMouseDown(MouseEventArgs e)
        {
            if (this.NCMouseDown != null) { this.NCMouseDown(this, e); }
            //发送消息
            ((IMessageChain)this).SendMessage(new MessageInfo(this, MessageStyle.eMSNCMouseDown, e));
            //System.Diagnostics.Debug.WriteLine("OnNCMouseDown");
        }

        protected virtual void OnNCMouseMove(MouseEventArgs e)
        {
            if (this.NCMouseMove != null) { this.NCMouseMove(this, e); }
            //发送消息
            ((IMessageChain)this).SendMessage(new MessageInfo(this, MessageStyle.eMSNCMouseMove, e));
            //System.Diagnostics.Debug.WriteLine("OnNCMouseMove");
        }

        protected virtual void OnNCMouseUp(MouseEventArgs e)
        {
            if (this.NCMouseUp != null) { this.NCMouseUp(this, e); }
            //发送消息
            ((IMessageChain)this).SendMessage(new MessageInfo(this, MessageStyle.eMSNCMouseUp, e));
            //System.Diagnostics.Debug.WriteLine("OnNCMouseUp");
        }

        protected virtual void OnNCMouseClick(MouseEventArgs e)
        {
            if (this.NCMouseClick != null) { this.NCMouseClick(this, e); }
            //发送消息
            ((IMessageChain)this).SendMessage(new MessageInfo(this, MessageStyle.eMSNCMouseClick, e));
            //System.Diagnostics.Debug.WriteLine("OnNCMouseClick");
        }

        protected virtual void OnNCMouseDoubleClick(MouseEventArgs e)
        {
            if (this.NCMouseDoubleClick != null) { this.NCMouseDoubleClick(this, e); }
            //发送消息
            ((IMessageChain)this).SendMessage(new MessageInfo(this, MessageStyle.eMSNCMouseDoubleClick, e));
            //System.Diagnostics.Debug.WriteLine("OnNCMouseDoubleClick");
        }
        #endregion

        #region SaveLayoutInfo 保存布局文件
        public void SaveLayoutFile(string strFileName)//保存当前布局状态
        {
            XmlDocument doc = new XmlDocument();
            //
            //
            //
            XmlDeclaration declare = doc.CreateXmlDeclaration("1.0", "UTF-8", "yes");//创建一个声明
            doc.InsertBefore(declare, doc.DocumentElement);//把声明添加到文档元素的顶部
            //
            //
            //
            XmlElement root = doc.CreateElement("TBFormSkinHelper");//添加根节点
            switch (this.m_HostForm.WindowState)
            {
                case FormWindowState.Maximized:
                    root.SetAttribute("Location", "300,80");
                    root.SetAttribute("Size", "800,600");
                    root.SetAttribute("WindowState", "Maximized");
                    break;
                case FormWindowState.Minimized:
                    root.SetAttribute("Location", "300,80");
                    root.SetAttribute("Size", "800,600");
                    root.SetAttribute("WindowState", "Normal");
                    break;
                case FormWindowState.Normal:
                    root.SetAttribute("Location", this.m_HostForm.Location.X.ToString() + "," + this.m_HostForm.Location.Y.ToString());
                    root.SetAttribute("Size", this.m_HostForm.Size.Width.ToString() + "," + this.m_HostForm.Size.Height.ToString());
                    root.SetAttribute("WindowState", "Normal");
                    break;
            }
            doc.AppendChild(root);
            //
            //
            //
            XmlElement elementBasePanels = root.OwnerDocument.CreateElement("ToolbarItems");
            elementBasePanels.SetAttribute("Count", this.ToolbarItems.Count.ToString());
            root.AppendChild(elementBasePanels);
            this.ToolbarItems.SetRecordID();
            foreach (BaseItem one in this.ToolbarItems)
            {
                XmlElement element = elementBasePanels.OwnerDocument.CreateElement("BaseItem");
                element.SetAttribute("RecordID", one.RecordID.ToString());
                element.SetAttribute("Name", one.Name);
                element.SetAttribute("Visible", one.Visible.ToString());
                elementBasePanels.AppendChild(element);
            }
            //
            //
            //
            doc.Save(strFileName);
        }
        #endregion

        #region LoadLayoutFile 加载布局文件
        public void LoadLayoutFile(string strFileName, bool loadFormSizeLayout)//加载布局文件，并根据布局文件进行布局
        {
            #region 读取布局文件 写入相关属性信息并进行布局
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(strFileName);
            //
            XmlNode xmlNode = xmlDoc.SelectSingleNode("TBFormSkinHelper");
            if (xmlNode == null) return;
            //
            if (loadFormSizeLayout) this.SetTBForm((XmlElement)xmlNode);
            //
            XmlNodeList xmlNodeList = xmlNode.ChildNodes;
            if (xmlNodeList == null) return;
            foreach (XmlNode one in xmlNodeList)
            {
                XmlElement xe = (XmlElement)one;
                switch (xe.Name)
                {
                    case "ToolbarItems":
                        if (xe.GetAttribute("Count") != "0") { this.SetToolbarItems(xe); }
                        break;
                    default:
                        break;
                }
            }
            #endregion
        }

        private void SetTBForm(XmlElement xmlElement)
        {
            this.m_HostForm.Location = this.ToPoint(xmlElement.GetAttribute("Location"));
            this.m_HostForm.Size = this.ToSize(xmlElement.GetAttribute("Size"));
            this.m_HostForm.WindowState = this.ToFormWindowState(xmlElement.GetAttribute("WindowState"));
        }

        //

        private void SetToolbarItems(XmlElement xmlElement)
        {
            XmlNodeList xmlNodeList = xmlElement.ChildNodes;
            if (xmlNodeList == null) return;
            foreach (XmlNode one in xmlNodeList)//遍历
            {
                XmlElement xe = (XmlElement)one;//将子节点类型转换为XmlElement类型
                int id = Int32.Parse(xe.GetAttribute("RecordID"));
                string name = xe.GetAttribute("Name");
                bool visible = bool.Parse(xe.GetAttribute("Visible"));
                //
                BaseItem temp = this.ToolbarItems[name];
                if (temp == null) continue;
                WFNew.ISetRecordItemHelper pSetRecordItemHelper = temp as WFNew.ISetRecordItemHelper;
                if (pSetRecordItemHelper != null) pSetRecordItemHelper.SetRecordID(id);
                temp.Visible = visible;
            }
        }

        //

        private FormWindowState ToFormWindowState(string str)
        {
            if (str == FormWindowState.Maximized.ToString()) return FormWindowState.Maximized;
            else if (str == FormWindowState.Minimized.ToString()) return FormWindowState.Minimized;
            else return FormWindowState.Normal;
        }

        private Point ToPoint(string str)
        {
            try
            {
                string[] strList = str.Split(',');
                return new Point(Int32.Parse(strList[0]), Int32.Parse(strList[1]));
            }
            catch { GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show("布局文件损坏！"); return new Point(60, 60); }
        }

        private Size ToSize(string str)
        {
            try
            {
                string[] strList = str.Split(',');
                return new Size(Int32.Parse(strList[0]), Int32.Parse(strList[1]));
            }
            catch { GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show("布局文件损坏！"); return new Size(260, 260); }
        }
        #endregion

        //
        protected virtual void OnTextChanged(EventArgs e)
        {
            this.RefreshNC();
        }

        protected virtual void OnPaint(PaintEventArgs e)
        {
            this.OnDraw(e);
        }

        protected virtual void OnDraw(System.Windows.Forms.PaintEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderForm
                (
                new ObjectRenderEventArgs(e.Graphics, this, this.DisplayRectangle)
                );
        }

        protected virtual void OnNCDraw(System.Windows.Forms.PaintEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderFormNC
                (
                new ObjectRenderEventArgs(e.Graphics, this, e.ClipRectangle)
                );
            //
            if (this.IsDrawIcon)
            {
                GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderFormNCIcon
                    (
                    new IconRenderEventArgs(e.Graphics, this, this.Enabled, this.Icon, this.CaptionIconRectangle)
                    );
            }
            //
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderFormNCCaptionText
                (
                new TextRenderEventArgs
                    (
                    e.Graphics, 
                    this, 
                    this.Enabled,
                    this.Text,
                    this.m_HostForm.ForeColor, 
                    this.m_HostForm.Font, 
                    this.CaptionTextRectangle
                    )
                );
        }
    }
}
