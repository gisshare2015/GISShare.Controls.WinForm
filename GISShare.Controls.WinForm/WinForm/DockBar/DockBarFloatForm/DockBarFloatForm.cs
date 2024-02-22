using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.DockBar
{
    class DockBarFloatForm : WFNew.BaseItemForm, WFNew.IOwner, WFNew.IBaseItemOwner, WFNew.ICollectionItem, WFNew.ICollectionItem2, ICollectionItemDB, IDockBarFloatForm, IDockArea, IDock, WFNew.IRecordItem, WFNew.ISetRecordItemHelper, ISetDockBarManagerHelper//, WFNew.IBaseItemOwner2
    {
        private const int CRT_BOUNDSSPACE = 3;
        private const int CRT_CAPTIONHEIGHT = 17;
        private const int CRT_MINWIDTH = 50;

        private int m_RecordID = 0;
        private bool m_bIsMouseDown = false;                                 //记录鼠标是否按下
        private bool m_bIsMouseDownResize = false;
        private Point m_MousePoint = new Point(5, 5);                        //记录鼠标按下时的坐标
        private IDockBar m_pDockBar = null;
        private DockBarManager m_DockBarManager = null;
        private StringFormat m_DrawFormat = null;

        private DockBarFloatFormButtonStackItemEx m_DockBarFloatFormButtonStackItemEx;
        private WFNew.BaseItemCollection m_BaseItemCollection;

        public DockBarFloatForm()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.Selectable, false);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();
            //
            //
            //
            this.m_BaseItemCollection = new GISShare.Controls.WinForm.WFNew.BaseItemCollection(this);
            //
            this.m_DockBarFloatFormButtonStackItemEx = new DockBarFloatFormButtonStackItemEx(this);
            this.m_BaseItemCollection.Add(this.m_DockBarFloatFormButtonStackItemEx);//-
            ((WFNew.ILockCollectionHelper)this.m_BaseItemCollection).SetLocked(true);//加锁
            //
            //
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.BackColor = GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.WFNewColorTable.RibbonAreaBackground;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(60, 60);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DockBarFloatForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "DockBarFloatForm";
            //
            this.m_DrawFormat = new StringFormat();
            this.m_DrawFormat.FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.FitBlackBox;
            this.m_DrawFormat.Trimming = StringTrimming.EllipsisCharacter;
            
        }

        //#region IOwner
        //[Browsable(false), Description("获取其拥有者"), Category("关联")]
        //public WFNew.IOwner pOwner
        //{
        //    get { return null; }
        //}
        //#endregion

        //#region IBaseItemOwner
        //[Browsable(false), Description("自身是否溢出于其所在容器的子项展现矩形"), Category("状态")]
        //public bool Overflow
        //{
        //    get { return false; }
        //}

        //[Browsable(false), Description("其子项展现矩形"), Category("布局")]
        //public Rectangle ItemsRectangle
        //{
        //    get { return new Rectangle(0, 0, this.Width, this.Height); }
        //}

        //[Browsable(false), Description("获取其子项拥有者"), Category("关联")]
        //public WFNew.IBaseItemOwner pBaseItemOwner
        //{
        //    get { return this.pOwner as WFNew.IBaseItemOwner; }
        //}
        //#endregion

        //#region IBaseItemOwner2
        //[Browsable(false), Description("取消其子项的绘制事件"), Category("状态")]
        //public virtual bool CancelItemsDrawEvent
        //{
        //    get
        //    {
        //        return false;
        //    }
        //}

        //[Browsable(false), Description("取消其子项除绘制事件以外的所有事件"), Category("状态")]
        //public virtual bool CancelItemsEvent
        //{
        //    get
        //    {
        //        return false;
        //    }
        //}
        //#endregion

        [Browsable(false), Description("其子项展现矩形"), Category("布局")]
        public override Rectangle ItemsRectangle
        {
            get { return new Rectangle(0, 0, this.Width, this.Height); }
        }

        #region ICollectionItem
        [Browsable(false), Description("其所携带的子项集合中是否存在可见项"), Category("状态")]
        bool WFNew.ICollectionItem.HaveVisibleBaseItem
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

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Description("其所携带的子项集合"), Category("子项")]
        WFNew.BaseItemCollection WFNew.ICollectionItem.BaseItems
        {
            get { return m_BaseItemCollection; }
        }
        #endregion

        #region ICollectionItem2
        WFNew.IBaseItem WFNew.ICollectionItem2.GetBaseItem(string strName)
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

        //

        #region IDock
        public void ToDockArea(DockStyle eDockStyle)
        {
            this.Close();
            this.pDockBar.ToDockArea(eDockStyle);
        }

        public void ToDockArea(DockStyle eDockStyle, int row)
        {
            this.Close();
            this.pDockBar.ToDockArea(eDockStyle, row);
            this.MouseDownGrip();
        }

        public void ToDockArea(DockStyle eDockStyle, Point location)
        {
            this.Close();
            this.pDockBar.ToDockArea(eDockStyle, location);
            this.MouseDownGrip();
        }

        public void ToDockArea(DockStyle eDockStyle, int row, Point location)
        {
            this.Close();
            this.pDockBar.ToDockArea(eDockStyle, row, location);
            this.MouseDownGrip();
        }
        private void MouseDownGrip()
        {
            GISShare.Win32.API.SendMessage(this.m_pDockBar.Handle,
                (int)GISShare.Win32.Msgs.WM_LBUTTONDOWN,
                0,
                (uint)GISShare.Win32.NativeMethods.MousePositionToLParam(new Point(5, 5)));
        }
        #endregion

        #region IDockArea
        public DockAreaStyle eDockAreaStyle
        {
            get { return DockAreaStyle.eDockBarFloatForm; }
        }

        public DockBarManager DockBarManager
        { get { return this.m_DockBarManager; } }
        #endregion

        #region IRecordItem
        //[Browsable(false)]
        //public int RecordID
        //{
        //    get { return m_RecordID; }
        //}

        public new string Text
        {
            get
            {
                if (this.pDockBar != null) { return this.pDockBar.Text; }
                else { return base.Text; }
            }
            set { base.Text = value; }
        }
        #endregion

        #region ICollectionItemDB
        public ToolStripItem GetItem(string strName)
        {
            return this.m_pDockBar == null ? null : this.m_pDockBar.GetItem(strName);
        }
        #endregion

        #region IDockBarFloatForm
        public Image Image
        {
            get
            {
                if (this.pDockBar != null) { return this.pDockBar.Image; }
                else { return null; } 
            }
            set
            {
                if (this.pDockBar != null) { this.pDockBar.Image = value; }
            }
        }

        [Browsable(false)]
        public IDockBar pDockBar
        {
            get { return m_pDockBar; }
        }

        public override Rectangle DisplayRectangle
        {
            get
            {
                return new Rectangle(CRT_BOUNDSSPACE, CRT_CAPTIONHEIGHT + CRT_BOUNDSSPACE, base.DisplayRectangle.Width - 2 * CRT_BOUNDSSPACE, base.DisplayRectangle.Height - CRT_CAPTIONHEIGHT - CRT_BOUNDSSPACE);
            }
        }

        [Browsable(false)]
        public Rectangle CaptionRectangle//标题矩形框
        {
            get
            {
                return new Rectangle(CRT_BOUNDSSPACE, CRT_BOUNDSSPACE, base.DisplayRectangle.Width - 2 * CRT_BOUNDSSPACE, CRT_CAPTIONHEIGHT);
            }
        }

        [Browsable(false), Description("图片绘制矩形框"), Category("布局")]
        public Rectangle ImageRectangle
        {
            get
            {
                return new Rectangle(4, 4, 16, 16);
            }
        }

        [Browsable(false), Description("标题绘制矩形框"), Category("布局")]
        public Rectangle TitleRectangle
        {
            get
            {
                if (this.Image == null)
                {
                    return Rectangle.FromLTRB
                        (
                        4,
                        5, 
                        this.m_DockBarFloatFormButtonStackItemEx.DisplayRectangle.Left,
                        this.CaptionRectangle.Bottom
                        );
                }
                else
                {
                    return Rectangle.FromLTRB
                        (
                        20,
                        5,
                        this.m_DockBarFloatFormButtonStackItemEx.DisplayRectangle.Left,
                        this.CaptionRectangle.Bottom
                        );
                }
            }
        }

        [Browsable(false)]
        public Rectangle FrameRectangle
        {
            get
            {
                return new Rectangle(0, 0, this.Width, this.Height);
            }
        }

        public Rectangle MoveRectangle
        {
            get 
            {
                Rectangle rectangle = this.CaptionRectangle;
                return Rectangle.FromLTRB(rectangle.Left, rectangle.Top, this.m_DockBarFloatFormButtonStackItemEx.DisplayRectangle.Left, rectangle.Bottom);
            }
        }

        [Browsable(false)]
        public Rectangle ResizeRectangle
        {
            get
            {
                return Rectangle.FromLTRB(this.CaptionRectangle.Right, CRT_BOUNDSSPACE, this.Width, this.Height - CRT_BOUNDSSPACE);
            }
        }

        public void Show(IDockBar dockBar)
        {
            this.m_pDockBar = dockBar;
            //
            pDockBar.RemoveFromParent();
            pDockBar.Visible = true;
            //pDockBar.Dock = DockStyle.None;
            pDockBar.GripStyle = ToolStripGripStyle.Hidden; //if (pDockBar.eDockBarStyle == DockBarStyle.eToolBar) 
            pDockBar.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.Controls.Add(pDockBar as Control);
            this.Owner = pDockBar.DockBarManager.ParentForm;
            this.Location = pDockBar.DockBarFloatFormLocation;
            if (this.IsDisposed) { return; }
            base.Show();
            //
            pDockBar.Dock = DockStyle.Top;
            pDockBar.LayoutStyle = ToolStripLayoutStyle.Flow;
            //
            this.pDockBar.Resize += new EventHandler(DockBar_Resize);
            this.pDockBar.BeforeVisibleExValueSeted += new BoolValueChangedEventHandler(DockBar_BeforeVisibleExValueSeted);
            //
            //if (pDockBar.eDockBarStyle == DockBarStyle.eToolBar) this.Width = pDockBar.Size.Width + 3;// 6 - 3
            //else this.Width = pDockBar.Size.Width;
            if (pDockBar.DockBarFloatFormSize.Width <= 0)
            {
                this.Width = pDockBar.MaxWidth + 2 * CRT_BOUNDSSPACE;
            }
            else {this.Width = pDockBar.DockBarFloatFormSize.Width; }
            //this.Height = pDockBar.Size.Height + 20;
            //this.m_MaxWidth = this.Width;
            //this.m_MinHeight = this.Height;
        }

        public void Show(IDockBar dockBar, Point location)
        {
            this.m_pDockBar = dockBar;
            //
            pDockBar.RemoveFromParent();
            pDockBar.Visible = true;
            //pDockBar.Dock = DockStyle.None;
            pDockBar.GripStyle = ToolStripGripStyle.Hidden;//if (pDockBar.eDockBarStyle == DockBarStyle.eToolBar) 
            pDockBar.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.Controls.Add(pDockBar as Control);
            this.Owner = pDockBar.DockBarManager.ParentForm;
            this.Location = location;
            if (this.IsDisposed) { return; }
            base.Show();
            //
            GISShare.Win32.API.SendMessage(this.Handle,
                (int)GISShare.Win32.Msgs.WM_LBUTTONDOWN, 
                0,
                (uint)GISShare.Win32.NativeMethods.MousePositionToLParam(new Point(10, 10)));
            //
            this.pDockBar.Resize += new EventHandler(DockBar_Resize);
            this.pDockBar.BeforeVisibleExValueSeted += new BoolValueChangedEventHandler(DockBar_BeforeVisibleExValueSeted);
            //
            //if (pDockBar.eDockBarStyle == DockBarStyle.eToolBar) this.Width = pDockBar.Size.Width + 3;// 6 - 3
            //else this.Width = pDockBar.Size.Width;
            //this.Width = pDockBar.MaxLength;
            if (pDockBar.DockBarFloatFormSize.Width <= 0)
            {
                this.Width = pDockBar.MaxWidth + 2 * CRT_BOUNDSSPACE;
            }
            else { this.Width = pDockBar.DockBarFloatFormSize.Width; }
            //this.Height = pDockBar.Size.Height + 20;
            //this.m_MaxWidth = this.Width;
            //this.m_MinHeight = this.Height;
            //
            pDockBar.Dock = DockStyle.Top;
            pDockBar.LayoutStyle = ToolStripLayoutStyle.Flow;
        }

        public bool ResetSize()
        {
            if (pDockBar == null) return false;
            //
            this.Width = this.pDockBar.LineMaxWidth + 2 * CRT_BOUNDSSPACE;
            this.Height = this.pDockBar.Size.Height + CRT_CAPTIONHEIGHT + 2 * CRT_BOUNDSSPACE;
            //
            return true;
        }

        private void DockBar_Resize(object sender, EventArgs e)
        {
            int iHeight = this.pDockBar.Size.Height;
            if (iHeight < 1) iHeight = 25;
            //
            this.Height = iHeight + CRT_CAPTIONHEIGHT + 2 * CRT_BOUNDSSPACE; 
            //System.Diagnostics.Debug.WriteLine(String.Format("{0}----{1}", this.Name, "DockBar_Resize"));
        }
        private void DockBar_BeforeVisibleExValueSeted(object sender, BoolValueChangedEventArgs e)
        {
            if(!e.NewValue) this.Close();
        }
        #endregion

        #region WFNew.ISetRecordItemHelper
        void WFNew.ISetRecordItemHelper.SetRecordID(int id)//设置RecordID，由系统管理（在保存布局时设置该属性）
        {
            this.m_RecordID = id;
        }
        #endregion

        #region ISetDockBarManagerHelper
        void ISetDockBarManagerHelper.SetDockBarManager(DockBarManager dockBarManager)//设置DockBarManager，由系统管理（添加到DockBarCollection时，调用该函数）
        {
            this.m_DockBarManager = dockBarManager;
        }
        #endregion

        protected override void MessageMonitor(WFNew.MessageInfo messageInfo)
        {
            base.MessageMonitor(messageInfo);
            //
            foreach (WFNew.BaseItem one in this.m_BaseItemCollection)
            {
                ((WFNew.IMessageChain)one).SendMessage(messageInfo);
            }
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            //
            GISShare.Win32.API.SendMessage(this.m_DockBarManager.ParentForm.Handle,
                (int)GISShare.Win32.Msgs.WM_NCACTIVATE,
                (uint)GISShare.Win32.WindowActiveStyles.WA_ACTIVE,
                0);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderFloatForm(
                new ObjectRenderEventArgs(e.Graphics, this, this.FrameRectangle));
            if (this.Image != null)
            {
                GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderImage(
                    new ImageRenderEventArgs(e.Graphics, this, this.Enabled, this.Image, this.ImageRectangle));
            }
            GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderText(
                new TextRenderEventArgs(e.Graphics, this, this.Enabled, this.HaveShadow, this.Text, this.ForeCustomize, this.ForeColor, this.ShadowColor, this.Font, this.TitleRectangle));
            //
            base.OnPaint(e);
        }

        protected override void OnLocationChanged(EventArgs e)
        {
            base.OnLocationChanged(e);
            //
            if (this.pDockBar == null) return;
            Point point;
            if (this.pDockBar.DockBarManager.DockBarDockAreaTop != null)
            {
                point = this.pDockBar.DockBarManager.DockBarDockAreaTop.PointToClient(MousePosition);
                if (this.pDockBar.DockBarManager.DockBarDockAreaTop.MaxBounds.Contains(point))
                {
                    this.ToDockArea(DockStyle.Top, point);
                    return;
                }
            }
            if (this.pDockBar.DockBarManager.DockBarDockAreaLeft != null)
            {
                point = this.pDockBar.DockBarManager.DockBarDockAreaLeft.PointToClient(MousePosition);
                if (this.pDockBar.DockBarManager.DockBarDockAreaLeft.MaxBounds.Contains(point))
                {
                    this.ToDockArea(DockStyle.Left, point);
                    return;
                }
            }
            if (this.pDockBar.DockBarManager.DockBarDockAreaRight != null)
            {
                point = this.pDockBar.DockBarManager.DockBarDockAreaRight.PointToClient(MousePosition);
                point = new Point(point.X + 10, point.Y);
                if (this.pDockBar.DockBarManager.DockBarDockAreaRight.MaxBounds.Contains(point))
                {
                    this.ToDockArea(DockStyle.Right, point);
                    return;
                }
            }
            if (this.pDockBar.DockBarManager.DockBarDockAreaBottom != null)
            {
                point = this.pDockBar.DockBarManager.DockBarDockAreaBottom.PointToClient(MousePosition);
                point = new Point(point.X, point.Y + 10);
                if (this.pDockBar.DockBarManager.DockBarDockAreaBottom.MaxBounds.Contains(point))
                {
                    this.ToDockArea(DockStyle.Bottom, point);
                    return;
                }
            }
        }
        
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            //
            if (e.Button == MouseButtons.Left)
            {
                if (this.MoveRectangle.Contains(e.Location))
                {
                    this.m_MousePoint = e.Location;
                    this.m_bIsMouseDown = true;
                    this.Cursor = Cursors.SizeAll;
                }
                else if (this.ResizeRectangle.Contains(e.Location))
                {
                    this.m_bIsMouseDownResize = true;
                    this.Cursor = Cursors.SizeWE;
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.Button == MouseButtons.Left)
            {
                if (this.m_bIsMouseDown)
                {
                    base.Location = new Point(Form.MousePosition.X - this.m_MousePoint.X, Form.MousePosition.Y - this.m_MousePoint.Y);
                }
                else if (this.m_bIsMouseDownResize)
                {
                    int iWidth = Form.MousePosition.X - this.Location.X;
                    if (iWidth >= CRT_MINWIDTH)
                    {
                        this.Width = iWidth <= this.pDockBar.MaxWidth + 2 * CRT_BOUNDSSPACE ? iWidth : this.pDockBar.MaxWidth + 2 * CRT_BOUNDSSPACE;
                    }
                    //System.Diagnostics.Debug.WriteLine(String.Format("{0}----{1}", this.Name, iWidth));
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            this.m_bIsMouseDown = false;
            if (this.m_bIsMouseDownResize) { this.ResetSize(); }
            this.m_bIsMouseDownResize = false;
            this.Cursor = Cursors.Default;
        }

        protected override void OnClosed(EventArgs e)
        {
            this.pDockBar.DockBarFloatFormLocation = this.Location;
            this.pDockBar.DockBarFloatFormSize = this.Size;
            //
            this.Visible = false;
            base.OnClosed(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.DockBarManager.DockBarFloatForms.Remove(this);
                this.pDockBar.GripStyle = ToolStripGripStyle.Visible;
                this.pDockBar.Dock = DockStyle.None;
                this.pDockBar.BeforeVisibleExValueSeted -= new BoolValueChangedEventHandler(DockBar_BeforeVisibleExValueSeted);
                this.Controls.Clear();
            }
            //
            base.Dispose(disposing);
        }

        //
        //
        //

        class DockBarFloatFormButtonStackItemEx : DockBarFloatFormButtonStackItem
        {
            IDockBarFloatForm m_Owner;
            public DockBarFloatFormButtonStackItemEx(IDockBarFloatForm owner)
            {
                this.m_Owner = owner;
                this.Size = new Size(31, 15);
            }

            public override Rectangle DisplayRectangle
            {
                get
                {
                    Rectangle displayRectangle = base.DisplayRectangle;
                    if (this.m_Owner == null) return new Rectangle(base.Location, displayRectangle.Size);
                    Rectangle rectangle = this.m_Owner.CaptionRectangle;
                    return new Rectangle(rectangle.Right - this.Width - 1, rectangle.Top + 1, displayRectangle.Width, displayRectangle.Height);
                }
            }

            //public override Point Location
            //{
            //    get
            //    {
            //        if (this.m_Owner == null) return base.Location;
            //        Rectangle rectangle = this.m_Owner.CaptionRectangle;
            //        return new Point(rectangle.Right - this.Width - 1, rectangle.Top + 1);
            //    }
            //}

            public override Padding Padding
            {
                get
                {
                    return new Padding(0);
                }
                set
                {
                    base.Padding = new Padding(0);
                }
            }

        }
    }
}