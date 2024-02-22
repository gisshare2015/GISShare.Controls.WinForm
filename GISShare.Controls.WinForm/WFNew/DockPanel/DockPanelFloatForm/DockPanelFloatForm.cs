using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew.DockPanel
{
    internal class DockPanelFloatForm : BaseItemForm, WFNew.IOwner, WFNew.IBaseItemOwner, WFNew.ICollectionItem, WFNew.ICollectionItem2, IDockPanelFloatForm, IDockArea, IDock, IRootNode, ISetDockPanelManagerHelper//.IBaseItemOwner2
    {
        private const int CRT_FORMBORDERSIZE = 2;
        private const int CRT_CAPTIONHEIGHT = 18;

        #region 私有变量
        private bool m_bIsMouseDown = false;                                 //记录鼠标是否按下
        private Point m_MousePoint = new Point(5, 5);                        //记录鼠标按下时的坐标
        private DockButtonManagerForm m_ToDockManagerBrackgroundForm = null; //布局停靠窗体
        private DockPanelManager m_DockPanelManager = null;                  //记录其所在的浮动面板管理器
        #endregion

        private DockPanelFloatFormButtonStackItemEx m_DockPanelFloatFormButtonStackItemEx;
        private WFNew.BaseItemCollection m_BaseItemCollection;

        public DockPanelFloatForm()
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
            this.m_DockPanelFloatFormButtonStackItemEx = new DockPanelFloatFormButtonStackItemEx();
            this.m_BaseItemCollection.Add(this.m_DockPanelFloatFormButtonStackItemEx);
            ((WFNew.ILockCollectionHelper)this.m_BaseItemCollection).SetLocked(true);
            //
            this.FormBorderStyle = FormBorderStyle.None;
            this.MaximizedBounds = SystemInformation.WorkingArea;
            this.StartPosition = FormStartPosition.Manual;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Name = "RenderableDockPanelFloatForm";
            this.Text = "RenderableDockPanelFloatForm";
            this.Location = new Point(100, 100);
            base.Name = "DockPanelFloatForm";
            base.Text = Language.LanguageStrategy.FloatFormText;// "浮动窗体";
            ////
            //this.Opacity = 0;
            //
            //
            //

        }

        #region WFNew.IBaseItem
        [Browsable(false), Description("自身所处的状态（激活、按下、不可用、正常）"), Category("状态")]
        WFNew.BaseItemState WFNew.IBaseItem.eBaseItemState
        {
            get { return GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal; }
        }
        #endregion

        #region WFNew.ICollectionItem
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

        #region WFNew.ICollectionItem2
        IBaseItem WFNew.ICollectionItem2.GetBaseItem(string strName)
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

        #region IDockPanelContainer
        [Browsable(false), Description("浮动面板管理器"), Category("关联")]
        public DockPanelManager DockPanelManager
        {
            get { return m_DockPanelManager; }
        }

        [Browsable(false), Description("记录面板类型"), Category("属性")]
        public DockPanelContainerStyle eDockPanelContainerStyle//记录自身容器的类型
        { get { return DockPanelContainerStyle.eDockPanelFloatForm; } }

        [Browsable(false), Description("描述信息"), Category("属性")]
        public string Describe
        { get { return "【由系统自动管理】停靠面板浮动窗体（DockPanelFloatForm）：用来停靠继承于停靠面板接口（IDockPanel）的两个控件（即：DockPanel和DockPanelContainer），相当于面板树的一个顶级单根的树节点。"; } }

        public DockPanel[] GetDockPanels()//获取DockPanels
        {
            List<DockPanel> dockPanelCol = new List<DockPanel>();
            if (this.Controls.Count > 0)
            {
                DockPanel dockPanel = this.Controls[0] as DockPanel;
                if (dockPanel != null)
                { dockPanelCol.Add(dockPanel); }
                else
                { this.GetDockPanels(this.Controls[0] as DockPanelContainer, dockPanelCol); }
            }
            return dockPanelCol.ToArray();
        }
        private void GetDockPanels(DockPanelContainer dockPanelContainer, List<DockPanel> dockPanelCol)//递归 获取DockPanels
        {
            if (dockPanelContainer == null) return;
            //
            if (dockPanelContainer.Panel1.Controls.Count > 0)
            {
                DockPanel dockPanel = dockPanelContainer.Panel1.Controls[0] as DockPanel;
                if (dockPanel != null)
                { dockPanelCol.Add(dockPanel); }
                else
                { this.GetDockPanels(dockPanelContainer.Panel1.Controls[0] as DockPanelContainer, dockPanelCol); }
            }
            if (dockPanelContainer.Panel2.Controls.Count > 0)
            {
                DockPanel dockPanel = dockPanelContainer.Panel2.Controls[0] as DockPanel;
                if (dockPanel != null)
                { dockPanelCol.Add(dockPanel); }
                else
                { this.GetDockPanels(dockPanelContainer.Panel2.Controls[0] as DockPanelContainer, dockPanelCol); }
            }
        }
        #endregion

        #region IDockArea
        [Browsable(false)]
        public new DockStyle Dock//被阉割的属性，无法设置
        {
            get { return DockStyle.None; }
            set { }
        }

        [Browsable(false), Description("记录自身停靠区类型"), Category("属性")]
        public DockAreaStyle eDockAreaStyle//记录自身停靠区类型
        { get { return DockAreaStyle.eDockPanelFloatForm; } }

        [Browsable(false), Description("停靠区矩形（屏幕坐标）"), Category("布局")]
        public Rectangle DockAreaRectangle
        {
            get
            {
                Rectangle rectangle = this.DisplayRectangle;
                return new Rectangle(this.PointToScreen(rectangle.Location), rectangle.Size);
            }
        }

        public IDockPanel GetIDockPanel()//获取所包含的IDockPanel节点
        {
            if (this.Controls.Count < 1) return null;
            IDockPanel pDockPanel = this.Controls[0] as IDockPanel;
            if (pDockPanel == null) return null;
            return pDockPanel;
        }

        public DockPanel GetDockPanel()
        {
            if (this.Controls.Count > 0)
            {
                DockPanel dockPanel = this.Controls[0] as DockPanel;
                if (dockPanel != null)
                { return dockPanel; }
                else
                { return this.GetDockPanel(this.Controls[0] as DockPanelContainer); }
            }
            return null;
        }
        private DockPanel GetDockPanel(DockPanelContainer dockPanelContainer)//递归 获取DockPanel
        {
            if (dockPanelContainer == null) return null;
            //
            if (dockPanelContainer.Panel1.Controls.Count > 0)
            {
                DockPanel dockPanel = dockPanelContainer.Panel1.Controls[0] as DockPanel;
                if (dockPanel != null)
                { return dockPanel; }
                else
                { return this.GetDockPanel(dockPanelContainer.Panel1.Controls[0] as DockPanelContainer); }
            }
            if (dockPanelContainer.Panel2.Controls.Count > 0)
            {
                DockPanel dockPanel = dockPanelContainer.Panel2.Controls[0] as DockPanel;
                if (dockPanel != null)
                { return dockPanel; }
                else
                { return this.GetDockPanel(dockPanelContainer.Panel2.Controls[0] as DockPanelContainer); }
            }
            //
            return null;
        }
        #endregion

        #region IDock
        public bool ToDockArea(bool bInteral, DockStyle eDockStyle)
        {
            if (this.Controls.Count < 1) return false;
            IDock pDock = this.Controls[0] as IDock;
            if (pDock == null) return false;
            return pDock.ToDockArea(bInteral, eDockStyle);
        }

        public bool ToDockArea(bool bInteral, DockStyle eDockStyle, Point location)
        {
            if (this.Controls.Count < 1) return false;
            IDock pDock = this.Controls[0] as IDock;
            if (pDock == null) return false;
            return pDock.ToDockArea(bInteral, eDockStyle, location);
        }
        #endregion

        #region IDockPanelFloatForm
        [Browsable(false), Description("记录激活状态"), Category("状态")]
        public bool bActive
        {
            get { return this.ContainsFocus; }
        }

        [Browsable(false), Description("标题矩形框"), Category("布局")]
        public Rectangle CaptionRectangle//标题矩形框
        {
            get
            {
                return new Rectangle(CRT_FORMBORDERSIZE,
                    CRT_FORMBORDERSIZE,
                    base.DisplayRectangle.Width - 2 * CRT_FORMBORDERSIZE,
                    CRT_CAPTIONHEIGHT);
            }
        }

        [Browsable(false), Description("外框矩形"), Category("布局")]
        public Rectangle FrameRectangle
        {
            get
            {
                return new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            }
        }

        [Browsable(false), Description("标题绘制矩形框"), Category("布局")]
        public Rectangle TitleRectangle
        {
            get
            {
                Rectangle rectangle = this.CaptionRectangle;
                return Rectangle.FromLTRB(rectangle.Left,
                    rectangle.Top + 1,
                    this.m_DockPanelFloatFormButtonStackItemEx.Visible ? this.m_DockPanelFloatFormButtonStackItemEx.DisplayRectangle.Left : rectangle.Right - 1,
                    rectangle.Bottom - 1);
            }
        }

        [Browsable(false), Description("平移矩形框"), Category("布局")]
        public Rectangle MoveRectangle
        {
            get
            {
                Rectangle rectangle = this.CaptionRectangle;
                return Rectangle.FromLTRB(
                    rectangle.Left,
                    rectangle.Top,
                    this.m_DockPanelFloatFormButtonStackItemEx.Visible ? this.m_DockPanelFloatFormButtonStackItemEx.DisplayRectangle.Left - 1 : rectangle.Right,
                    rectangle.Bottom);
            }
        }

        public void Show(IDockPanel pDockPanel, Point mousePoint)
        {
            this.ResetDockPanel(pDockPanel);
            //
            base.Show();
            //
            if (this.Controls.Count > 0)
            {
                Size size = this.Controls[0].Size;
                this.Size = new Size(size.Width + 2 * CRT_FORMBORDERSIZE, size.Height + CRT_CAPTIONHEIGHT + 2 * CRT_FORMBORDERSIZE);
            }
            //
            Rectangle rectangle = this.MoveRectangle;
            rectangle.Inflate(-1, -1);
            if (mousePoint.X > rectangle.Right) mousePoint.X = rectangle.Right;
            else if (mousePoint.X < rectangle.Left) mousePoint.X = rectangle.Left;
            //if (mousePoint.Y > rectangle.Bottom) mousePoint.Y = rectangle.Bottom;
            //else if (mousePoint.Y < rectangle.Top) mousePoint.Y = rectangle.Top;
            mousePoint.Y = (rectangle.Top + rectangle.Bottom / 2);
            //
            this.m_MousePoint = mousePoint;
            base.Location = new Point(Form.MousePosition.X - this.m_MousePoint.X, Form.MousePosition.Y + this.m_MousePoint.Y);
            this.Opacity = 1;
            //
            if (!this.DockPanelManager.IsDockLayoutState)
            {
                GISShare.Win32.API.PostMessage
                    (
                    this.Handle,
                    (int)GISShare.Win32.Msgs.WM_NCLBUTTONDOWN,
                    0x02,
                    (uint)GISShare.Win32.NativeMethods.MousePositionToLParam(this.m_MousePoint)
                    );//0x00a1
            }
            //
            //
            //
            this.SetPanelNodeState(pDockPanel, PanelNodeState.eShow);
        }

        public void Show(IDockPanel pDockPanel)
        {
            this.ResetDockPanel(pDockPanel);
            //
            base.Show();
            //
            if (this.Controls.Count > 0)
            {
                Size size = this.Controls[0].Size;
                this.Size = new Size(size.Width + 2 * CRT_FORMBORDERSIZE, size.Height + CRT_CAPTIONHEIGHT + 2 * CRT_FORMBORDERSIZE);
                //IDockPanel pDockPanel = this.Controls[0] as IDockPanel;
                if (pDockPanel != null) this.Location = pDockPanel.DockPanelFloatFormLocation;
            }
            this.Opacity = 1;
            //
            //
            //
            this.SetPanelNodeState(pDockPanel, PanelNodeState.eShow);
        }
        #endregion

        #region IRootNode
        [Browsable(false), Description("获取节点类型"), Category("属性")]
        public NodeStyle eNodeStyle//获取节点类型
        { get { return NodeStyle.eRootNode; } }

        [Browsable(false), Description("获取其父节点"), Category("关联")]
        public IBaseNode ChildNode//获取其唯一的子节点
        {
            get { return this.GetIDockPanel() as IBaseNode; }
        }
        #endregion

        #region ISetDockPanelManagerHelper
        void ISetDockPanelManagerHelper.SetDockPanelManager(DockPanelManager dockPanelManager)//设置DockPanelManager，由系统管理（添加到DockPanelCollection时，调用该函数）
        {
            this.m_DockPanelManager = dockPanelManager;
        }
        #endregion

        //

        #region 覆盖
        public override Rectangle DisplayRectangle
        {
            get
            {
                return new Rectangle(CRT_FORMBORDERSIZE, 
                    CRT_FORMBORDERSIZE + CRT_CAPTIONHEIGHT,
                    base.DisplayRectangle.Width - 2 * CRT_FORMBORDERSIZE, 
                    base.DisplayRectangle.Height - 2 * CRT_FORMBORDERSIZE - CRT_CAPTIONHEIGHT);
            }
        }

        public override Rectangle ItemsRectangle
        {
            get
            {
                return base.DisplayRectangle;
            }
        }

        protected override void OnResizeEnd(EventArgs e)
        {
            base.OnResizeEnd(e);
            if (this.Width < 60) this.Width = 60;
            if (this.Height < 60) this.Height = 60;
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            //
            IDockPanel pDockPanel = e.Control as IDockPanel;
            if (pDockPanel == null) { this.Controls.Remove(e.Control); return; }
            if (pDockPanel.eDockPanelStyle == DockPanelStyle.eHoldDockPanel) { this.Controls.Remove(e.Control); return; }
            //pDockPanel.SetShowHideButton(false);
            pDockPanel.BeforeVisibleExValueSeted += new BoolValueChangedEventHandler(DockPanel_BeforeVisibleExValueSeted);
        }
        private void DockPanel_BeforeVisibleExValueSeted(object sender, BoolValueChangedEventArgs e)
        {
            if (e.NewValue) return;
            //this.Controls.Clear();
            this.Close();
        }

        protected override void OnControlRemoved(ControlEventArgs e)
        {
            base.OnControlRemoved(e);
            //
            IDockPanel pDockPanel = e.Control as IDockPanel;
            if (pDockPanel != null)
            {
                this.SetPanelNodeState(pDockPanel, PanelNodeState.eClose);
                //pDockPanel.SetShowHideButton(false);
                pDockPanel.BeforeVisibleExValueSeted -= new BoolValueChangedEventHandler(DockPanel_BeforeVisibleExValueSeted);
            }
            //
            //
            //
            if (this.Controls.Count > 0) return;
            if(!this.IsDisposed && this.Visible) this.Close();
            this.Dispose();
            GC.Collect();
        }

        protected override void OnLocationChanged(EventArgs e)
        {
            base.OnLocationChanged(e);
            //
            if (this.Controls.Count > 0)
            {
                IDockPanel pDockPanel = this.Controls[0] as IDockPanel;
                if (pDockPanel != null) { pDockPanel.DockPanelFloatFormLocation = this.Location; }
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            //
            if (this.Controls.Count > 0)
            {
                IDockPanel pDockPanel = this.Controls[0] as IDockPanel;
                if (pDockPanel != null) { pDockPanel.DockPanelFloatFormSize = this.Size; }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            //
            if (this.WindowState != FormWindowState.Normal) return;
            //
            if (this.m_ToDockManagerBrackgroundForm != null) this.m_ToDockManagerBrackgroundForm.Close();//key
            //
            if (e.Button == MouseButtons.Left)
            {
                this.m_MousePoint = e.Location;
                this.m_bIsMouseDown = true;
                if (this.MoveRectangle.Contains(e.Location))
                {
                    this.m_ToDockManagerBrackgroundForm = new DockButtonManagerForm();//key
                    this.m_ToDockManagerBrackgroundForm.Show(this);
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                DockPanelFloatFormPopup dockPanelFormPopup = new DockPanelFloatFormPopup(this.GetIDockPanel());
                dockPanelFormPopup.Show(this, e.Location);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (this.m_bIsMouseDown && e.Button == MouseButtons.Left)
            {
                base.Location = new Point(Form.MousePosition.X - this.m_MousePoint.X, Form.MousePosition.Y - this.m_MousePoint.Y);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            this.m_bIsMouseDown = false;
            //
            if (this.m_ToDockManagerBrackgroundForm != null) this.m_ToDockManagerBrackgroundForm.Close();
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case (int)GISShare.Win32.Msgs.WM_NCLBUTTONDOWN://0x00a1
                    if (m.WParam.ToInt32() == 0x02)
                    {
                        //Point pt = PointToClient(new Point(MousePosition.X, MousePosition.Y));
                        GISShare.Win32.API.PostMessage(this.Handle, (int)GISShare.Win32.Msgs.WM_SETFOCUS, 0, 0);//0x0007
                        GISShare.Win32.API.PostMessage
                            (
                            this.Handle,
                            (int)GISShare.Win32.Msgs.WM_LBUTTONDOWN,
                            0,
                            (uint)m.LParam//(uint)GISShare.Win32.NativeMethods.MousePositionToLParam(pt)
                            );//0x00201, pt.Y * 65536 + pt.X
                    }
                    else if (m.WParam.ToInt32() == 0x14)
                    {
                        IDockPanel pDockPanel = GetIDockPanel();
                        if (pDockPanel != null) { pDockPanel.Close(); }
                        else { base.WndProc(ref m); }
                    }
                    else
                    {
                        base.WndProc(ref m);
                    }
                    break;
                case (int)GISShare.Win32.Msgs.WM_NCHITTEST://0x0084
                    base.WndProc(ref m);
                    //
                    Point point = GISShare.Win32.NativeMethods.LParamToMouseLocation((int)m.LParam);
                    point = PointToClient(point);
                    if (point.X <= 3)
                        if (point.Y <= 3)
                            m.Result = (IntPtr)GISShare.Win32.HitTests.HTTOPLEFT;//const int HTTOPLEFT = 13;
                        else if (point.Y >= ClientSize.Height - 3)
                            m.Result = (IntPtr)GISShare.Win32.HitTests.HTBOTTOMLEFT;//const int HTBOTTOMLEFT = 0x10;
                        else m.Result = (IntPtr)GISShare.Win32.HitTests.HTLEFT;//const int HTLEFT = 10;
                    else if (point.X >= ClientSize.Width - 3)
                        if (point.Y <= 3)
                            m.Result = (IntPtr)GISShare.Win32.HitTests.HTTOPRIGHT;//const int HTTOPRIGHT = 14;
                        else if (point.Y >= ClientSize.Height - 3)
                            m.Result = (IntPtr)GISShare.Win32.HitTests.HTBOTTOMRIGHT;//const int HTBOTTOMRIGHT = 17;
                        else m.Result = (IntPtr)GISShare.Win32.HitTests.HTRIGHT;//const int HTRIGHT = 11;
                    else if (point.Y <= 3)
                        m.Result = (IntPtr)GISShare.Win32.HitTests.HTTOP;//const int HTTOP = 12;
                    else if (point.Y >= ClientSize.Height - 3)
                        m.Result = (IntPtr)GISShare.Win32.HitTests.HTBOTTOM;//const int HTBOTTOM = 15;
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.DockPanelManager.DockPanelFloatForms.Contains(this))
                { this.DockPanelManager.DockPanelFloatForms.Remove(this); }
                //
                this.Controls.Clear();
            }
            //
            base.Dispose(disposing);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            this.OnDraw(e);
            //
            base.OnPaint(e);
        }

        protected virtual void OnDraw(PaintEventArgs e)
        {
            WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderDockPanelFloatForm(
                new ObjectRenderEventArgs(e.Graphics, this, this.FrameRectangle));
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonText(
                new GISShare.Controls.WinForm.TextRenderEventArgs(e.Graphics, this, this.Enabled, this.HaveShadow, this.Text, this.ForeCustomize, this.ForeColor, this.ShadowColor, this.Font, this.TitleRectangle));
        }
        #endregion

        protected override void MessageMonitor(MessageInfo messageInfo)
        {
            base.MessageMonitor(messageInfo);
            //
            ((IMessageChain)this.m_DockPanelFloatFormButtonStackItemEx).SendMessage(messageInfo);
        }

        public new void Hide()
        {
            this.Close();
        }

        private void ResetDockPanel(IDockPanel pDockPanel)//将在两个Show(...)函数中被调用
        {
            pDockPanel.RemoveFromParent();
            //
            this.Size = pDockPanel.DockPanelFloatFormSize;
            this.Location = pDockPanel.DockPanelFloatFormLocation;
            this.Controls.Add(pDockPanel as Control);
            //
            this.Owner = pDockPanel.DockPanelManager.ParentForm;
        }

        private void SetPanelNodeState(IDockPanel pDockPanel, PanelNodeState panelNodeState)//设置其所辖的面板节点状态并激发相应事件（将在两个Show(...)和OnFormClosing(...)函数中被调用）
        {
            if (pDockPanel == null) return;
            //
            switch (panelNodeState)
            {
                case PanelNodeState.eShow:
                    switch (pDockPanel.eDockPanelStyle)
                    {
                        case DockPanelStyle.eDockPanel:
                            DockPanel dockPanel = pDockPanel as DockPanel;
                            if (dockPanel != null) { ((ISetPanelNodeStateHelper)dockPanel).SetPanelNodeState(PanelNodeState.eShow); }
                            break;
                        case DockPanelStyle.eDockPanelContainer:
                            DockPanelContainer dockPanelContainer = pDockPanel as DockPanelContainer;
                            if (dockPanelContainer != null) { ((ISetPanelNodeStateHelper)dockPanelContainer).SetPanelNodeState(PanelNodeState.eShow); }
                            break;
                        default:
                            break;
                    }
                    break;
                case PanelNodeState.eClose:
                    switch (pDockPanel.eDockPanelStyle)
                    {
                        case DockPanelStyle.eDockPanel:
                            DockPanel dockPanel = pDockPanel as DockPanel;
                            if (dockPanel != null) { ((ISetPanelNodeStateHelper)dockPanel).SetPanelNodeState(PanelNodeState.eClose); }
                            break;
                        case DockPanelStyle.eDockPanelContainer:
                            DockPanelContainer dockPanelContainer = pDockPanel as DockPanelContainer;
                            if (dockPanelContainer != null) { ((ISetPanelNodeStateHelper)dockPanelContainer).SetPanelNodeState(PanelNodeState.eClose); }
                            break;
                        default:
                            break;
                    }
                    break;
            }
        }

        //
        //
        //

        class DockPanelFloatFormButtonStackItemEx : DockPanelFloatFormButtonStackItem
        {
            public DockPanelFloatFormButtonStackItemEx()
            {
                this.Size =  new Size(45, 23);
            }

            public override bool Visible
            {
                get
                {
                    if (!this.HaveVisibleBaseItem) return false;
                    //
                    IDockPanel pDockPanel = this.pOwner as IDockPanel;
                    if (pDockPanel == null) return base.Visible;
                    return pDockPanel.GetDockAreaStyle() == DockAreaStyle.eDocumentDockArea ? false : base.Visible;
                }
                set
                {
                    base.Visible = value;
                }
            }

            public override Rectangle DisplayRectangle
            {
                get
                {
                    IDockPanelFloatForm pDockPanelFloatForm = this.pOwner as IDockPanelFloatForm;
                    if (pDockPanelFloatForm == null) return base.DisplayRectangle;
                    Rectangle rectangle = pDockPanelFloatForm.CaptionRectangle;
                    return new Rectangle(rectangle.Right - this.Width, rectangle.Top, this.Width, rectangle.Height);
                }
            }

            public override Padding Padding
            {
                get
                {
                    return new Padding(1);
                }
                set
                {
                    base.Padding = value;
                }
            }
        }

        /// <summary>
        /// 浮动窗体上的快捷菜单
        /// </summary>
        [ToolboxItem(false)]
        class DockPanelFloatFormPopup : BaseDockPanelPopup
        {
            public DockPanelFloatFormPopup(IDockPanel pDockPanel)
                : base(pDockPanel)
            { }

            protected override void SetPopupItem()
            {
                this.BaseItems.Clear();
                //
                //
                //
                bool bCanDockUp = true;
                bool bCanDockLeft = true;
                bool bCanDockRight = true;
                bool bCanDockBottom = true;
                bool bCanDockFill = true;
                bool bCanFloat = true;
                bool bCanHide = true;
                bool bCanClose = true;
                bool bIsBasePanel = true;
                bool bIsDocumentPanel = true;
                base._pDockPanel.GetDockLicense(ref bCanDockUp, ref bCanDockLeft, ref bCanDockRight, ref bCanDockBottom, ref bCanDockFill, ref bCanFloat, ref bCanHide, ref bCanClose, ref bIsBasePanel, ref bIsDocumentPanel);
                DockAreaStyle eDockAreaStyle = base._pDockPanel.GetDockAreaStyle();
                DockPanelContainerStyle eDockPanelContainerStyle = base._pDockPanel.GetDockPanelContainerStyle();
                //
                //
                //
                WFNew.DropDownButtonItem ribbonDropDownButtonItemInternal = new WFNew.DropDownButtonItem();
                ribbonDropDownButtonItemInternal.Text = Language.LanguageStrategy.InternalText;//"内部";
                this.Items.Add(ribbonDropDownButtonItemInternal);
                this.CreateAddDockPanelManagerBaseItemInternal(ribbonDropDownButtonItemInternal.BaseItems, bCanDockUp, bCanDockLeft, bCanDockRight, bCanDockBottom, bIsBasePanel);
                //
                WFNew.DropDownButtonItem ribbonDropDownButtonItemOut = new WFNew.DropDownButtonItem();
                ribbonDropDownButtonItemOut.Text = Language.LanguageStrategy.OuterText;//"外围";
                this.Items.Add(ribbonDropDownButtonItemOut);
                this.CreateAddDockPanelManagerBaseItemOut(ribbonDropDownButtonItemOut.BaseItems, bCanDockUp, bCanDockLeft, bCanDockRight, bCanDockBottom, bIsBasePanel);
                //
                this.Items.Add(new WFNew.SeparatorItem());
                //
                WFNew.DropDownButtonItem ribbonDropDownButtonItemDocument = new WFNew.DropDownButtonItem();
                ribbonDropDownButtonItemDocument.Text = Language.LanguageStrategy.DocumentAreaText;//"文档区";
                this.Items.Add(ribbonDropDownButtonItemDocument);
                base.CreateAddDockPanelManagerBaseItemDocument(ribbonDropDownButtonItemDocument.BaseItems, bIsDocumentPanel);
                //
                //
                //
                WFNew.SeparatorItem toolStripSeparator = new WFNew.SeparatorItem();
                this.BaseItems.Add(toolStripSeparator);
                base.CreateDockPanelCustomizeBaseItem(this.BaseItems);
            }
        }
    }
}