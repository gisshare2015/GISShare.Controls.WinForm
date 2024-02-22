using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew.DockPanel
{
    [ToolboxItem(false)]
    class DockPanelHidePanel : GISShare.Controls.WinForm.WFNew.SplitPanel
    {
        private int NonClientAreaHorizontalWidth//水平方向的8个无效区 4 + 4
        {
            get
            {
                return this.m_ParentForm == null ? 8 : this.m_ParentForm.Width - this.m_ParentForm.DisplayRectangle.Width;
                ////
                //switch (this.m_ParentForm.FormBorderStyle) 
                //{
                //    case FormBorderStyle.None:
                //        return 0;
                //    case FormBorderStyle.Sizable:
                //    case FormBorderStyle.SizableToolWindow:
                //        return 2 * SystemInformationX.FrameBorderSize.Width;
                //    case FormBorderStyle.Fixed3D:
                //    case FormBorderStyle.FixedDialog:
                //    case FormBorderStyle.FixedSingle:
                //    case FormBorderStyle.FixedToolWindow:
                //        return 2 * SystemInformation.FixedFrameBorderSize.Width;
                //}
                //return 8;
            }
        }
        private int NonClientAreaVerticalHeight//竖直方向的34个无效区 30 + 4
        {
            get
            {
                return this.m_ParentForm == null ? 34 : this.m_ParentForm.Height - this.m_ParentForm.DisplayRectangle.Height;
                //switch (this.m_ParentForm.FormBorderStyle)
                //{
                //    case FormBorderStyle.None:
                //        return 0;
                //    case FormBorderStyle.Sizable:
                //    case FormBorderStyle.Fixed3D:
                //    case FormBorderStyle.FixedDialog:
                //    case FormBorderStyle.FixedSingle:
                //        return SystemInformation.CaptionHeight + 2 * SystemInformationX.FrameBorderSize.Height;
                //    case FormBorderStyle.FixedToolWindow:
                //    case FormBorderStyle.SizableToolWindow:
                //        return SystemInformation.ToolWindowCaptionHeight + 2 * SystemInformation.FixedFrameBorderSize.Height;
                //}
                //return 34;
            }
        }

        #region 私有变量
        private bool m_bHaveAnimation = true;                           //是否存在动画效果                              <不要对其直接赋值>
        private DockStyle m_SplitPanelDock = DockStyle.Left;            //SplitPanelDock记录面板的停靠方式              <不要对其直接赋值>
        private Timer m_Timer = null;                                   //用来检测鼠标指针是否存在非激活的展开停靠面板内（存在则不隐藏，不存在则隐藏）
        private Form m_ParentForm = null;                               //停靠面板的父窗体
        private DockPanel m_DockPanel = null;                           //其所承载的停靠面板
        private HideAreaTabButtonGroupItem m_HideAreaTabButtonGroupItem = null; //其所对应的隐藏按钮组
        #endregion

        public DockPanelHidePanel(Form parentForm)
            : base()
        {
            base.Visible = false;
            base.Dock = DockStyle.None;
            base.BackColor = GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.WFNewColorTable.RibbonAreaBackground;
            //
            this.m_Timer = new Timer();
            this.m_Timer.Interval = 1000;
            this.m_Timer.Stop();
            this.m_Timer.Tick += new EventHandler(Timer_Tick);
            //
            this.m_ParentForm = parentForm;
            this.m_ParentForm.Resize += new EventHandler(ParentForm_Resize);
        }

        #region 覆盖
        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            //
            DockPanel dockPanel = e.Control as DockPanel;
            if (dockPanel == null) { this.Controls.Remove(e.Control); return; }
            //dockPanel.SetShowHideButton(true);
            //dockPanel.TabButtonList.AutoVisible = false;
            dockPanel.BeforeVisibleExValueSeted += new BoolValueChangedEventHandler(DockPanel_BeforeVisibleExValueSeted);
            dockPanel.DockPanelActiveChanged += new BoolValueChangedEventHandler(DockPanel_DockPanelActiveChanged);
        }

        protected override void OnControlRemoved(ControlEventArgs e)
        {
            base.OnControlRemoved(e);
            //
            //
            //
            DockPanel dockPanel = e.Control as DockPanel;
            if (dockPanel == null) return;
            //dockPanel.SetShowHideButton(false);
            //dockPanel.TabButtonList.AutoVisible = true;
            dockPanel.BeforeVisibleExValueSeted -= new BoolValueChangedEventHandler(DockPanel_BeforeVisibleExValueSeted);
            dockPanel.DockPanelActiveChanged -= new BoolValueChangedEventHandler(DockPanel_DockPanelActiveChanged);
            //
            if (this.Controls.Count > 0) return;
            //
            this.Close();
            //
            this.m_DockPanel = null;
            this.m_HideAreaTabButtonGroupItem = null;
        }

        public override DockStyle SplitPanelDock//SplitPanelDock记录面板的停靠方式（不再依托于Dock属性，有独立的私有变量记录该值）
        {
            get { return this.m_SplitPanelDock; }
            set
            {
                if (value == DockStyle.None) return;
                this.m_SplitPanelDock = value;
            }
        }

        protected override void SetOutSize()//在鼠标按下时计算OutSize
        {
            if (this.m_DockPanel != null) ((ISetDockPanelHelper)this.m_DockPanel).SetActiveState(this.m_DockPanel);
            //
            DockPanelManager dockPanelManager = this.DockPanelManager;
            if (dockPanelManager != null)
            {
                Size size = dockPanelManager.GetParentFormWorkRegionSize(false, this.SplitPanelDock);
                this.OutSize = new Size(size.Width /*+ this.Width*/ - (this.OuterMinWidth + this.SplitLineWidth), size.Height /*+ this.Height*/  - (this.OuterMinWidth + this.SplitLineWidth));
            }
            //else { this.OutSize = new Size(this.Parent.Width - (this.OuterMinWidth + this.SplitLineWidth), this.Parent.Height - (this.OuterMinWidth + this.SplitLineWidth)); }
        }

        protected override void SetSplitPanelSize(Point point)//在鼠标弹起后设置SplitPanel的尺寸
        {
            switch (this.SplitPanelDock)
            {
                case DockStyle.Top:
                    if (point.Y >= this.InternalMinWidth && point.Y <= this.OutSize.Height) { this.Height = point.Y; }
                    else if (point.Y <= this.InternalMinWidth) { this.Height = this.InternalMinWidth; }
                    else if (point.Y >= this.OutSize.Height) { this.Height = this.OutSize.Height; }
                    break;
                case DockStyle.Left:
                    if (point.X >= this.InternalMinWidth && point.X <= this.OutSize.Width) { this.Width = point.X; }
                    else if (point.X <= this.InternalMinWidth) { this.Width = this.InternalMinWidth; }
                    else if (point.X >= this.OutSize.Width) { this.Width = this.OutSize.Width; }
                    break;
                case DockStyle.Right:
                    if (point.X <= this.Width - this.InternalMinWidth && point.X >= -(this.OutSize.Width - this.Width)) { this.Left += point.X; this.Width -= point.X; }
                    else if (point.X >= this.Width - this.InternalMinWidth) { this.Left += this.Width - this.InternalMinWidth; this.Width = this.InternalMinWidth; }
                    else if (point.X <= -(this.OutSize.Width - this.Width)) { this.Left += this.Width - this.OutSize.Width; this.Width = this.OutSize.Width; }
                    break;
                case DockStyle.Bottom:
                    if (point.Y <= this.Height - this.InternalMinWidth && point.Y >= -(this.OutSize.Height - this.Height)) { this.Top += point.Y; this.Height -= point.Y; }
                    else if (point.Y >= this.Height - this.InternalMinWidth) { this.Top += this.Height - this.InternalMinWidth; this.Height = this.InternalMinWidth; }
                    else if (point.Y <= -(this.OutSize.Height - this.Height)) { this.Top += this.Height - this.OutSize.Height; this.Height = this.OutSize.Height; }
                    break;
                default:
                    break;
            }
            //
            this.Refresh();
        }
        #endregion

        #region 关联事件
        private void DockPanel_BeforeVisibleExValueSeted(object sender, BoolValueChangedEventArgs e)
        {
            if (e.NewValue) { this.Show(this.m_DockPanel, this.m_HideAreaTabButtonGroupItem); }
            else { this.Close(); }
        }

        private void DockPanel_DockPanelActiveChanged(object sender, BoolValueChangedEventArgs e)
        {
            if (e.NewValue) { this.StopTimer(); }
            else { this.Close(); }
        }

        private void ParentForm_Resize(object sender, EventArgs e)
        {
            this.UpdateLayout();
        }
        private void UpdateLayout()//更新布局（当窗体尺寸调整时，将会被调用）
        {
            if (this.DockPanel == null || this.DockPanel.DockPanelManager == null) return;
            //
            Rectangle rectangleExceptDockArea = this.DockPanel.DockPanelManager.GetParentFormWorkRegionRectangle(true);
            Rectangle rectangle = new Rectangle();
            //
            switch (this.SplitPanelDock)
            {
                case DockStyle.Top:
                    rectangle = new Rectangle(rectangleExceptDockArea.X,
                        rectangleExceptDockArea.Y,
                        rectangleExceptDockArea.Width - NonClientAreaHorizontalWidth,
                        this.DockPanel.Height);
                    base.Size = new Size(rectangle.Size.Width, rectangle.Size.Height + this.SplitLineWidth);//this.SplitLineWidth为无效区Splitter
                    base.Location = rectangle.Location;
                    break;
                case DockStyle.Left:
                    rectangle = new Rectangle(rectangleExceptDockArea.X,
                        rectangleExceptDockArea.Y,
                        this.DockPanel.Width, 
                        rectangleExceptDockArea.Height - NonClientAreaVerticalHeight);
                    base.Size = new Size(rectangle.Size.Width + this.SplitLineWidth, rectangle.Size.Height);//this.SplitLineWidth为无效区Splitter
                    base.Location = rectangle.Location;
                    break;
                case DockStyle.Right:
                    rectangle = new Rectangle(rectangleExceptDockArea.X + (rectangleExceptDockArea.Width - NonClientAreaHorizontalWidth - this.DockPanel.Width),
                        rectangleExceptDockArea.Y,
                        this.DockPanel.Width, 
                        rectangleExceptDockArea.Height - NonClientAreaVerticalHeight);
                    base.Size = new Size(rectangle.Size.Width + this.SplitLineWidth, rectangle.Size.Height);//this.SplitLineWidth为无效区Splitter
                    base.Location = new Point(rectangle.Location.X - this.SplitLineWidth, rectangle.Location.Y);
                    break;
                case DockStyle.Bottom:
                    rectangle = new Rectangle(rectangleExceptDockArea.X, 
                        rectangleExceptDockArea.Y + (rectangleExceptDockArea.Height - NonClientAreaVerticalHeight - this.DockPanel.Height),
                        rectangleExceptDockArea.Width - NonClientAreaHorizontalWidth, 
                        this.DockPanel.Height);
                    base.Size = new Size(rectangle.Size.Width, rectangle.Size.Height + this.SplitLineWidth);//this.SplitLineWidth为无效区Splitter
                    base.Location = new Point(rectangle.Location.X, rectangle.Location.Y - this.SplitLineWidth);
                    break;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (!this.ContainsMousePoint(MousePosition))
            { this.Close(); }
        }
        #endregion

        #region 属性
        [Browsable(false)]
        internal bool bHaveAnimation//是否存在动画效果（当点击隐藏按钮时调用，使展现时不出现动画效果以减少闪烁）
        {
            get { return m_bHaveAnimation; }
            set { m_bHaveAnimation = value; }
        }

        [Browsable(false)]
        public new DockStyle Dock//停靠状态为只能为 DockStyle.None
        {
            get { return DockStyle.None; }
            set { base.Dock = DockStyle.None; }
        }

        [Browsable(false)]
        public new bool Visible//被阉割，使之不能实现修改可视状态
        {
            get { return base.Visible; }
            set { }
        }

        [Browsable(false)]
        public DockPanel DockPanel
        {
            get { return m_DockPanel; }
        }

        [Browsable(false)]
        public DockPanelManager DockPanelManager
        {
            get
            {
                if (this.DockPanel == null) return null;
                return this.DockPanel.DockPanelManager;
            }
        }
        #endregion

        #region 内部公开函数
        internal void StartTimer()//开始计时（当非激活的展开停靠面板展现时）
        {
            if (!this.Visible ||
                this.m_DockPanel == null ||
                this.m_DockPanel.bActive) return;
            //
            this.m_Timer.Start();
        }

        internal bool ContainsMousePoint(Point point)//鼠标是否存在于隐藏面板内（开始计时将被调用，会被外部调用即鼠标离开TabButton时）
        {
            return this.ClientRectangle.Contains(PointToClient(point));
        }
        #endregion

        #region 公开函数
        public void Show(DockPanel dockPanel, HideAreaTabButtonGroupItem hideAreaTabButtonGroupItem)//展现隐藏面板
        {
            if (dockPanel == null) return;
            //
            if (base.Visible) { this.Close(); }
            //
            if (!this.Controls.Contains(dockPanel))
            {
                this.Controls.Clear();
                this.Size = dockPanel.Size;//key
                //dockPanel.Dock = DockStyle.Fill;
                this.Controls.Add(dockPanel);
                this.m_DockPanel = dockPanel;
                this.m_HideAreaTabButtonGroupItem = hideAreaTabButtonGroupItem;
            }
            //
            switch (this.m_HideAreaTabButtonGroupItem.TabAlignment)
            {
                case TabAlignment.Top:
                    this.SplitPanelDock = DockStyle.Top;
                    break;
                case TabAlignment.Left:
                    this.SplitPanelDock = DockStyle.Left;
                    break;
                case TabAlignment.Right:
                    this.SplitPanelDock = DockStyle.Right;
                    break;
                case TabAlignment.Bottom:
                    this.SplitPanelDock = DockStyle.Bottom;
                    break;
            }
            //
            if (!base.Visible)
            {
                //this.UpdateLayout();
                this.ShowAnimation(this.bHaveAnimation);
                if (!this.m_DockPanel.Visible) this.m_DockPanel.Visible = true;//key 
                //base.Visible = true;
            }
            //
            //
            //
            if (this.DockPanel != null) { ((ISetPanelNodeStateHelper)this.DockPanel).SetPanelNodeState(PanelNodeState.eShow); }
        }
        private void ShowAnimation(bool haveAnimation)//展现动画
        {
            Rectangle layoutRectangle = this.GetLayoutRectangle();
            if (!haveAnimation)
            {
                this.Width = layoutRectangle.Width;//key
                this.Height = layoutRectangle.Height;
                this.Location = layoutRectangle.Location;
                base.Visible = true;
                //
                return;
            }
            //
            //
            //
            int increment = 10;
            int iNum = 10;
            switch (this.SplitPanelDock)
            {
                case DockStyle.Top:
                    increment = layoutRectangle.Height / iNum;
                    this.Width = layoutRectangle.Width;//key
                    this.Height = layoutRectangle.Height - (increment * iNum);
                    this.Location = layoutRectangle.Location;
                    break;
                case DockStyle.Left:
                    increment = layoutRectangle.Width / iNum;
                    this.Height = layoutRectangle.Height;//key
                    this.Width = layoutRectangle.Width - (increment * iNum);
                    this.Location = layoutRectangle.Location;
                    break;
                case DockStyle.Right:
                    increment = layoutRectangle.Width / iNum;
                    this.Height = layoutRectangle.Height;//key
                    this.Width = layoutRectangle.Width - (increment * iNum);
                    this.Location = new Point(layoutRectangle.Location.X + (increment * iNum), layoutRectangle.Location.Y);
                    break;
                case DockStyle.Bottom:
                    increment = layoutRectangle.Height / iNum;
                    this.Width = layoutRectangle.Width;//key
                    this.Height = layoutRectangle.Height - (increment * iNum);
                    this.Location = new Point(layoutRectangle.Location.X, layoutRectangle.Location.Y + (increment * iNum));
                    break;
            }
            base.Visible = true;
            //
            for (int i = 0; i < iNum; i++)
            {
                for (int j = 0; j < 2000; j++) ;//延时
                //
                switch (this.SplitPanelDock)
                {
                    case DockStyle.Top:
                        this.Height += increment;
                        break;
                    case DockStyle.Left:
                        this.Width += increment;
                        break;
                    case DockStyle.Right:
                        this.Left -= increment; this.Width += increment; //key 顺序不能变
                        break;
                    case DockStyle.Bottom:
                        this.Top -= increment; this.Height += increment; //key 顺序不能变
                        break;
                }
            }
        }
        private Rectangle GetLayoutRectangle()//获取隐藏面板展现时的布局矩形框
        {
            Rectangle rectangle = new Rectangle();
            //
            if (this.DockPanel == null || this.DockPanel.DockPanelManager == null) return rectangle;
            //
            Rectangle rectangleExceptDockArea = this.DockPanel.DockPanelManager.GetParentFormWorkRegionRectangle(true);
            //
            switch (this.SplitPanelDock)
            {
                case DockStyle.Top:
                    rectangle = new Rectangle(rectangleExceptDockArea.X,
                        rectangleExceptDockArea.Y,
                        rectangleExceptDockArea.Width - NonClientAreaHorizontalWidth, 
                        this.DockPanel.Height);
                    rectangle = new Rectangle(rectangle.Location.X, rectangle.Location.Y, rectangle.Size.Width, rectangle.Size.Height + this.SplitLineWidth);//this.SplitLineWidth为无效区Splitter
                    break;
                case DockStyle.Left:
                    rectangle = new Rectangle(rectangleExceptDockArea.X, 
                        rectangleExceptDockArea.Y,
                        this.DockPanel.Width,
                        rectangleExceptDockArea.Height - NonClientAreaVerticalHeight);
                    rectangle = new Rectangle(rectangle.Location.X, rectangle.Location.Y, rectangle.Size.Width + this.SplitLineWidth, rectangle.Size.Height);//this.SplitLineWidth为无效区Splitter
                    break;
                case DockStyle.Right:
                    rectangle = new Rectangle(rectangleExceptDockArea.X + (rectangleExceptDockArea.Width - NonClientAreaHorizontalWidth - this.DockPanel.Width),
                        rectangleExceptDockArea.Y,
                        this.DockPanel.Width, 
                        rectangleExceptDockArea.Height - NonClientAreaVerticalHeight);
                    rectangle = new Rectangle(rectangle.Location.X - this.SplitLineWidth, rectangle.Location.Y, rectangle.Size.Width + this.SplitLineWidth, rectangle.Size.Height);//this.SplitLineWidth为无效区Splitter
                    break;
                case DockStyle.Bottom:
                    rectangle = new Rectangle(rectangleExceptDockArea.X, 
                        rectangleExceptDockArea.Y + (rectangleExceptDockArea.Height - NonClientAreaVerticalHeight - this.DockPanel.Height),
                        rectangleExceptDockArea.Width - NonClientAreaHorizontalWidth,
                        this.DockPanel.Height);
                    rectangle = new Rectangle(rectangle.Location.X, rectangle.Location.Y - this.SplitLineWidth, rectangle.Size.Width, rectangle.Size.Height + this.SplitLineWidth);//this.SplitLineWidth为无效区Splitter
                    break;
            }
            //
            return rectangle;
        }

        public void Close()//收起隐藏面板
        {
            this.StopTimer();
            this.HideAnimation(this.bHaveAnimation);
            //
            //
            //
            if (this.DockPanel != null) { ((ISetPanelNodeStateHelper)this.DockPanel).SetPanelNodeState(PanelNodeState.eClose); }
        }
        private void HideAnimation(bool haveAnimation)//隐藏动画
        {
            if (!this.bHaveAnimation) { base.Visible = false; return; }
            //
            //
            //
            int increment = 10;
            int iNum = 10;
            Rectangle layoutRectangle = this.Bounds;
            switch (this.SplitPanelDock)
            {
                case DockStyle.Top:
                case DockStyle.Bottom:
                    increment = layoutRectangle.Height / iNum;
                    break;
                case DockStyle.Left:
                case DockStyle.Right:
                    increment = layoutRectangle.Width / iNum;
                    break;
            }
            base.Visible = true;
            //
            for (int i = 0; i < iNum; i++)
            {
                for (int j = 0; j < 3000; j++) ; //延时
                //
                switch (this.SplitPanelDock)
                {
                    case DockStyle.Top:
                        this.Height -= increment;
                        break;
                    case DockStyle.Left:
                        this.Width -= increment;
                        break;
                    case DockStyle.Right:
                        this.Width -= increment; this.Left += increment; //key 顺序不能变
                        break;
                    case DockStyle.Bottom:
                        this.Height -= increment; this.Top += increment; //key 顺序不能变
                        break;
                }
            }
            //
            base.Visible = false;
            this.Bounds = layoutRectangle;
        }

        public void RefreshLayout()
        {
            Rectangle layoutRectangle = this.GetLayoutRectangle();
            this.Width = layoutRectangle.Width;//key
            this.Height = layoutRectangle.Height;
            this.Location = layoutRectangle.Location;
        }
        #endregion

        private void StopTimer()//停止计时（隐藏或激活时则停止计时，在DockPanel_DockPanelActiveChanged(...)和Close()函数中被调用）
        {
            this.m_Timer.Stop();
        }
        
        
    }
}
