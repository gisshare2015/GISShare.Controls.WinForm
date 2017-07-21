using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace GISShare.Controls.WinForm.WFNew.Forms
{
    public class TBForm : BaseItemForm, IDependItem, 
        WFNew.IOwner, IOwnerNC, IBaseItemOwnerNC, IBaseItemOwnerNC2, ITBForm, IFormEvent, IUICollectionItemNC
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

        private SuperToolTip m_SuperToolTip;
        //
        private WFNew.BaseItemCollection m_BaseItemCollection;
        private NCFormButtonStackItemEx m_NCFormButtonStackItemEx;
        private NCQuickAccessToolbarItemEx m_NCQuickAccessToolbarItemEx;

        public TBForm()
            : base()
        {
            this.m_BufferedGraphicsContext = BufferedGraphicsManager.Current;
            //
            //
            //
            this.m_BaseItemCollection = new GISShare.Controls.WinForm.WFNew.BaseItemCollection(this);
            //((WFNew.ILockCollectionHelper)this.m_BaseItemCollection).SetLocked(false);//解锁
            this.m_NCFormButtonStackItemEx = new NCFormButtonStackItemEx(this);
            this.m_BaseItemCollection.Add(this.m_NCFormButtonStackItemEx);
            this.m_NCQuickAccessToolbarItemEx = new NCQuickAccessToolbarItemEx(this);
            this.m_BaseItemCollection.Add(this.m_NCQuickAccessToolbarItemEx);
            ((WFNew.ILockCollectionHelper)this.m_BaseItemCollection).SetLocked(true);//加锁
            //
            //
            //
            this.BackColor = GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.WFNewColorTable.RibbonAreaDisabledBackground;
            //
            //
            //
            this.m_SuperToolTip = new SuperToolTip();
            this.m_SuperToolTip.OffsetX = 0;
            this.m_SuperToolTip.OffsetY = 12;
            foreach (BaseItem one in this.m_NCFormButtonStackItemEx.BaseItems)
            {
                this.m_SuperToolTip.SetToolTip(one as INCBaseItem);
            }
            //
            this.m_NCQuickAccessToolbarItemEx.BaseItems.ItemAdded += new ItemEventHandler(BaseItems_ItemAdded);
            this.m_NCQuickAccessToolbarItemEx.BaseItems.ItemRemoved += new ItemEventHandler(BaseItems_ItemRemoved);
        }
        void BaseItems_ItemAdded(object sender, ItemEventArgs e)
        {
            if (e.Item is IUICollectionItem) return;
            this.m_SuperToolTip.SetToolTip(e.Item as IBaseItem2);
        }
        void BaseItems_ItemRemoved(object sender, ItemEventArgs e)
        {
            if (e.Item is IUICollectionItem) return;
            this.m_SuperToolTip.RemoveToolTip(e.Item as IBaseItem2);
        }

        #region IDependItem
        [Browsable(false), Description("获取其依附对象（与此类无关）"), Category("关联")]
        public System.Windows.Forms.Control DependObject
        {
            get { return this; }
        }

        [Browsable(false), Description("获取其依附对象的句柄（与此类无关）"), Category("关联")]
        public IntPtr DependObjectHandle
        {
            get { return this.Handle; }
        }
        #endregion

        #region IOffsetNC
        [Browsable(false), Description("非客户区X轴的偏移量"), Category("布局")]
        public int NCOffsetX
        {
            get { return -this.FrameBorderSize.Width; }
        }

        [Browsable(false), Description("非客户区Y轴的偏移量"), Category("布局")]
        public int NCOffsetY
        {
            get { return -(this.FrameBorderSize.Height + this.CaptionHeight); }
        }
        #endregion

        #region IOwnerNC
        public Point PointToClientNC(Point point)
        {
            if (this.IsDisposed) return new Point(-1, -1);
            //
            point = this.PointToClient(point);
            point.X -= this.NCOffsetX;
            point.Y -= this.NCOffsetY;
            return point;
        }

        public Point PointToScreenNC(Point point)
        {
            if (this.IsDisposed) return new Point(-1, -1);
            //
            point = this.PointToScreen(point);
            point.X += this.NCOffsetX;
            point.Y += this.NCOffsetY;
            return point;
        }
        #endregion

        #region IBaseItemOwnerNC
        [Browsable(false), Description("非客户区其子项展现矩形"), Category("布局")]
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
            if (this.IsDisposed ||
                this.FormBorderStyle == FormBorderStyle.None ||
                (this.IsMdiChild && this.WindowState == FormWindowState.Maximized) ||
                (!this.IsMdiChild && this.WindowState == FormWindowState.Minimized)) return;
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
        [Browsable(false), DefaultValue(false), Description("取消非客户区其子项的所有事件"), Category("状态")]
        public bool CancelItemsEventNC
        {
            get { return m_CancelItemsEventNC; }
            set { m_CancelItemsEventNC = value; }
        }

        [Browsable(false), Description("取消非客户区其子项的绘制事件"), Category("状态")]
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
        [Browsable(false), Description("其所携带的子项集合中是否存在可见项"), Category("状态")]
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
        #endregion

        #region ITBForm
        bool m_IsMiddleCaptionText = false;
        [Browsable(true), DefaultValue(false), Description("标题文本居中"), Category("布局")]
        public bool IsMiddleCaptionText
        {
            get { return m_IsMiddleCaptionText; }
            set { m_IsMiddleCaptionText = value; }
        }

        [Browsable(true), DefaultValue(true), Description("显示快捷工具条"), Category("状态")]
        public bool ShowQuickAccessToolbar
        {
            get { return this.m_NCQuickAccessToolbarItemEx.Visible; }
            set { this.m_NCQuickAccessToolbarItemEx.Visible = value; }
        }
        
        [Browsable(true), DefaultValue(typeof(WFNew.QuickAccessToolbarStyle), "eAllRound"), Description("快捷工具条的展现方式"), Category("外观")]
        public WFNew.QuickAccessToolbarStyle eQuickAccessToolbarStyle
        {
            get { return this.m_NCQuickAccessToolbarItemEx.eQuickAccessToolbarStyle; }
            set { this.m_NCQuickAccessToolbarItemEx.eQuickAccessToolbarStyle = value; }
        }

        [Browsable(true),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
        Editor(typeof(GISShare.Controls.WinForm.WFNew.Design.BaseItemCollectionEditer), typeof(System.Drawing.Design.UITypeEditor)), 
        Description("其快捷工具条所携带的子项集合"), Category("子项")]
        public BaseItemCollection ToolbarItems
        {
            get { return this.m_NCQuickAccessToolbarItemEx.BaseItems; }
        }

        private bool m_IsActive = false;
        [Browsable(false), Description("是否激活"), Category("状态")]
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

        [Browsable(false), Description("标题区高"), Category("布局")]
        public int CaptionHeight
        {
            get
            {
                return FormInformation.GetCaptionHeight(this);
            }
        }

        [Browsable(false), Description("窗体按钮区宽度"), Category("布局")]
        public int FormButtonStackItemNCWidth
        {
            get { return this.m_NCFormButtonStackItemEx.Width; }
        }

        [Browsable(false), Description("最小标题区文本宽度"), Category("布局")]
        public int MinCaptionTextWidth
        {
            get { return CRT_MINCAPTIONTEXTWIDTH; }
        }

        [Browsable(false), Description("是否绘制标题ICON"), Category("状态")]
        public bool IsDrawIcon
        {
            get { return this.ShowIcon && this.Icon != null; }
        }

        [Browsable(false), Description("取消非客户区的绘制"), Category("状态")]
        public bool CancelDrawNC
        {
            get { return !this.IsProcessNCArea; }
        }

        [Browsable(false), Description("进行非客户区绘制"), Category("状态")]
        public bool IsProcessNCArea
        {
            get
            {
                // check if we should process the nc area
                //return 
                //    !(
                //    this.MdiParent != null && 
                //    this.WindowState == FormWindowState.Maximized
                //    );
                //return !(
                //         this.IsDisposed ||
                //         (this.IsMdiChild && this.WindowState == FormWindowState.Maximized)
                //        );
                return !this.IsDisposed;
            }
        }

        [Browsable(false), Description("携带系统菜单"), Category("状态")]
        public bool HasMenu
        {
            get { return FormInformation.HasMenu(this); }
        }

        [Browsable(false), Description("窗体外轮廓的边框尺寸"), Category("布局")]
        public Size FrameBorderSize
        {
            get
            {
                return FormInformation.GetBorderSize(this);
            }
        }

        [Browsable(false), Description("非客户区矩形"), Category("布局")]
        public Rectangle NCRectangleEx
        {
            get
            {
                return new Rectangle(0, 0, this.Width, this.FrameBorderSize.Height + this.CaptionHeight);
            }
        }

        [Browsable(false), Description("框架矩形"), Category("布局")]
        public Rectangle FrameRectangle
        {
            get
            {
                return new Rectangle(0, 0, this.m_CurrentCacheSize.Width - 1, this.m_CurrentCacheSize.Height - 1);
            }
        }

        [Browsable(false), Description("标题区矩形"), Category("布局")]
        public Rectangle CaptionRectangle
        {
            get
            {
                return new Rectangle(
                    this.FrameBorderSize.Width,
                    this.FrameBorderSize.Height,
                    this.m_CurrentCacheSize.Width - 2 * this.FrameBorderSize.Width,
                    this.CaptionHeight);
            }
        }

        [Browsable(false), Description("标题区ICON矩形"), Category("布局")]
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

        [Browsable(false), Description("非客户区快捷工具条矩形"), Category("布局")]
        public Rectangle CaptionToolbarRectangle
        {
            get
            {
                return this.m_NCQuickAccessToolbarItemEx.DisplayRectangle;
            }
        }

        [Browsable(false), Description("标题区文本布局矩形"), Category("布局")]
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
                        this.CaptionIconRectangle.Top + 1,
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

        [Browsable(false), Description("屏幕矩形矩形"), Category("布局")]
        public Rectangle ScreenRectangle
        {
            get { return FormInformation.GetScreenRect(this); }
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

        #region 覆盖
        /// <summary>
        /// Invokes the default window procedure associated with this window.
        /// </summary>
        /// <param name="m">A <see cref="T:System.Windows.Forms.Message"/> that is associated with the current Windows message.</param>
        //[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
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
                        if (m.WParam == this.DependObjectHandle) this.SetIsActive(false);
                        else if (m.LParam == this.DependObjectHandle) this.SetIsActive(true);
                        this.WndNCPaint(true);
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
                        //if (this.WindowState == FormWindowState.Maximized) this.Region = null;
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
                        if (this.WindowState == FormWindowState.Maximized) this.Region = null;
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
            Int32 currentStyle = GISShare.Win32.API.GetWindowLong(this.DependObjectHandle, GISShare.Win32.GWLIndex.GWL_STYLE);
            if ((currentStyle & (int)(GISShare.Win32.WindowStyles.WS_BORDER)) != 0)
            {
                currentStyle &= ~(Int32)(GISShare.Win32.WindowStyles.WS_BORDER);
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
            //// Create a rounded rectangle using Gdi
            //Size cornerSize = new Size(9, 9);
            //IntPtr hRegion = GISShare.Win32.API.CreateRoundRectRgn(0, 0, size.Width + 1, size.Height + 1, cornerSize.Width, cornerSize.Height);
            //Region region = Region.FromHrgn(hRegion);
            //this.Region = region;
            //region.ReleaseHrgn(hRegion);
            int iLeftTopRadius;
            int iRightTopRadius;
            int iLeftBottomRadius;
            int iRightBottomRadius;
            this.GetRadiusInfo(out iLeftTopRadius, out iRightTopRadius, out iLeftBottomRadius, out  iRightBottomRadius);
            //this.Region = new Region
            //    (
            //    GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle
            //         (
            //         new Rectangle(0, 0, size.Width, size.Height),
            //         iLeftTopRadius,
            //         iRightTopRadius,
            //         iLeftBottomRadius,
            //         iRightBottomRadius
            //         )
            //    );
            this.Region = GISShare.Controls.WinForm.Util.UtilTX.CreateRoundRectangle2
                     (
                     new Rectangle(0, 0, size.Width + 1, size.Height + 1),
                     iLeftTopRadius,
                     iRightTopRadius,
                     iLeftBottomRadius,
                     iRightBottomRadius
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
        private void WndNCPaint(Message message)
        {
            // update form styles on maximize/restore
            if (this.IsMdiChild)
            {
                if ((int)message.WParam == 0) this.UpdateStyle();
                if ((int)message.WParam == 2) this.Refresh();
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
                Size fullBorderSize = this.FrameBorderSize;
                info.ptMaxPosition.x = rect.Left - fullBorderSize.Width;
                info.ptMaxPosition.y = rect.Top - fullBorderSize.Height;
                info.ptMaxSize.x = rect.Width + 2 * fullBorderSize.Width;
                info.ptMaxSize.y = rect.Height + 2 * fullBorderSize.Height;

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

        protected override void OnLoad(EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                switch (this.StartPosition)
                {
                    case FormStartPosition.CenterParent:
                        if (this.Owner != null)
                        {
                            this.Location = new Point
                                (
                                (this.Owner.Bounds.Left + this.Owner.Bounds.Right - this.Width) / 2,
                                (this.Owner.Bounds.Top + this.Owner.Bounds.Bottom - this.Height) / 2
                                );
                        }
                        else
                        {
                            this.Location = new Point
                                (
                                (SystemInformation.WorkingArea.Left + SystemInformation.WorkingArea.Right - this.Width) / 2,
                                (SystemInformation.WorkingArea.Top + SystemInformation.WorkingArea.Bottom - this.Height) / 2
                                );
                        }
                        break;
                    case FormStartPosition.CenterScreen:
                        this.Location = new Point
                            (
                            (SystemInformation.WorkingArea.Left + SystemInformation.WorkingArea.Right - this.Width) / 2, 
                            (SystemInformation.WorkingArea.Top + SystemInformation.WorkingArea.Bottom - this.Height) / 2
                            );
                        break;
                    case FormStartPosition.WindowsDefaultLocation:
                        break;
                    default:
                        break;
                }
            }
            //
            base.OnLoad(e);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            this.RefreshNC();
            //
            base.OnTextChanged(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            this.OnDraw(e);
            //
            base.OnPaint(e);
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

        protected override void MessageMonitor(MessageInfo messageInfo)
        {
            base.MessageMonitor(messageInfo);
            //
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
            XmlElement root = doc.CreateElement("TBForm");//添加根节点
            switch (this.WindowState)
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
                    root.SetAttribute("Location", this.Location.X.ToString() + "," + this.Location.Y.ToString());
                    root.SetAttribute("Size", this.Size.Width.ToString() + "," + this.Size.Height.ToString());
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
            XmlNode xmlNode = xmlDoc.SelectSingleNode("TBForm");
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
            this.Location = this.ToPoint(xmlElement.GetAttribute("Location"));
            this.Size = this.ToSize(xmlElement.GetAttribute("Size"));
            this.WindowState = this.ToFormWindowState(xmlElement.GetAttribute("WindowState"));
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
                    new IconRenderEventArgs(e.Graphics, this, this.Enabled && this.IsActive, this.Icon, this.CaptionIconRectangle)
                    );
            }
            //
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderFormNCCaptionText
                (
                new TextRenderEventArgs
                    (
                    e.Graphics,
                    this,
                    this.Enabled && this.IsActive,
                    false,
                    this.IsMiddleCaptionText,
                    this.Text,
                    this.ForeColor,
                    this.Font,
                    this.CaptionTextRectangle,
                    new StringFormat()
                    )
                );
        }
    }

    //
    //
    //

    #region 辅助类
    class NCFormButtonStackItemEx : NCFormButtonStackItem
    {
        private ITBForm m_Owner;

        public NCFormButtonStackItemEx(ITBForm pFormEx)
            : base()
        {
            this.Padding = new Padding(1, 0, 1, 2);
            this.m_Owner = pFormEx;
        }

        public override Rectangle DisplayRectangle
        {
            get
            {
                if (this.m_Owner == null) return base.DisplayRectangle;
                Rectangle rectangle = this.m_Owner.CaptionRectangle;
                return new Rectangle(rectangle.Right - this.Size.Width, rectangle.Top, this.Size.Width, rectangle.Height);
            }
        }
    }

    class NCQuickAccessToolbarItemEx : NCQuickAccessToolbarItem
    {
        private ITBForm m_Owner;

        public NCQuickAccessToolbarItemEx(ITBForm pFormX)
        {
            this.Padding = new Padding(1, 0, 1, 1);
            this.m_Owner = pFormX;
        }

        public override bool CancelItemsEvent
        {
            get
            {
                return !this.m_Owner.IsActive;
            }
        }

        public override bool Visible
        {
            get
            {
                if (this.BaseItems.Count < 1 ||
                    this.m_Owner.FormBorderStyle == FormBorderStyle.None ||
                    this.m_Owner.WindowState == FormWindowState.Minimized ||
                    (this.m_Owner.IsMdiChild && this.m_Owner.WindowState != FormWindowState.Normal))
                    return false;
                //
                return base.Visible;
            }
            set
            {
                base.Visible = value;
            }
        }

        //public override Size Size
        //{
        //    get
        //    {
        //        //if (this.BaseItems.Count <= 0)
        //        if (!this.HaveVisibleBaseItem)
        //        {
        //            if (this.m_Owner == null) return base.Size;
        //            Rectangle rectangle = this.m_Owner.CaptionRectangle;
        //            return new Size(this.Padding.Left + 11 + this.Padding.Right, rectangle.Height);
        //        }
        //        if (base.Size.Width == 0) return new Size(1, base.Size.Height);
        //        return base.Size;
        //    }
        //    set
        //    {
        //        base.Size = value;
        //    }
        //}

        public override Rectangle DisplayRectangle
        {
            get
            {
                if (this.m_Owner == null) return base.DisplayRectangle;
                //
                int iW = this.Size.Width;
                if (!this.HaveVisibleBaseItem)
                {
                    if (this.m_Owner != null)
                    {
                        iW = this.Padding.Left + 11 + this.Padding.Right;
                    }
                    else
                    {
                        iW = this.Size.Width;
                    }
                }
                if (iW < 1) iW = 1;
                //
                Rectangle rectangle = this.m_Owner.CaptionRectangle;
                if (this.m_Owner.IsDrawIcon)
                {
                    return new Rectangle(this.m_Owner.CaptionIconRectangle.Right + 2, rectangle.Top, iW, rectangle.Height);
                }
                else
                {
                    return new Rectangle(rectangle.Left, rectangle.Top, iW, rectangle.Height);
                }
            }
        }

        public override int ColumnDistance
        {
            get
            {
                return 0;
            }
            set
            {
                base.ColumnDistance = value;
            }
        }

        public override bool ShowNomalState
        {
            get
            {
                return false;
            }
            set
            {
                base.ShowNomalState = false;
            }
        }

        public override bool IsStretchItems
        {
            get
            {
                return true;
            }
            set
            {
                base.IsStretchItems = true;
            }
        }

        public override bool LockWith
        {
            get
            {
                return true;
            }
            set
            {
                base.LockWith = value;
            }
        }

        public override bool IsRestrictItems
        {
            get
            {
                return true;
            }
            set
            {
                base.IsRestrictItems = value;
            }
        }

        public override bool Overflow
        {
            get
            {
                return false;
            }
        }

        public override int MinSize
        {
            get
            {
                Padding p = this.Padding;
                return p.Left + p.Right;
            }
            set
            {
                base.MinSize = 11;
            }
        }

        public override int MaxSize
        {
            get
            {
                if (this.m_Owner == null) return base.MaxSize;
                int iW = base.MaxSize;
                iW = this.m_Owner.CaptionRectangle.Width - this.m_Owner.FormButtonStackItemNCWidth - this.m_Owner.MinCaptionTextWidth - 2;
                switch (this.eQuickAccessToolbarStyle)
                {
                    case WFNew.QuickAccessToolbarStyle.eAllRound:
                    case WFNew.QuickAccessToolbarStyle.eNormal:
                        return this.MinSize < iW ? iW : this.MinSize + 30;
                    case WFNew.QuickAccessToolbarStyle.eNone:
                    default:
                        return this.MinSize < iW ? iW : this.MinSize + 20;
                }
            }
            set
            {
                base.MaxSize = 600;
            }
        }
    }
    #endregion
}
