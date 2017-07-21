using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace GISShare.Controls.WinForm.WFNew.DockPanel
{
    [Designer(typeof(GISShare.Controls.WinForm.WFNew.DockPanel.Design.DockPanelContainerDesigner)), ToolboxItem(false)]
    public class DockPanelContainer : SplitContainer, WFNew.IRecordItem, WFNew.ISetRecordItemHelper, WFNew.IArea, GISShare.Controls.WinForm.WFNew.ISplitPanel, IDockPanel, IDockPanelContainer, IDockPanelContainer2, IBinaryNode, ISetDockPanelManagerHelper, ISetPanelNodeStateHelper
    {
        #region 私有变量
        private bool m_Panel1Collapsed = false;                             //记录Panel1是否展开
        private bool m_Panel2Collapsed = false;                             //记录Panel2是否展开
        //
        private Point m_DockPanelFloatFormLocation = new Point(360, 360);   //记录DockPanel转化为停靠窗体后所在的坐标
        private Size m_DockPanelFloatFormSize = new Size(260, 260);         //记录DockPanel转化为停靠窗体后所在的坐标
        //
        private DockPanelManager m_DockPanelManager = null;                 //记录其所在的浮动面板管理器
        #endregion

        public DockPanelContainer()
            : base()
        {
            this.SetStyle(ControlStyles.Opaque, false);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();
            //
            //
            //
            base.Name = "DockPanelContainer";
            base.Dock = DockStyle.Fill;
            base.Size = new Size(160, 160);
            base.SplitterDistance = (base.Width / 2) - 4;
            //
            this.Panel1.ControlAdded += new ControlEventHandler(Panel1_ControlAdded);
            this.Panel1.ControlRemoved += new ControlEventHandler(Panel1_ControlRemoved);
            this.Panel2.ControlAdded += new ControlEventHandler(Panel2_ControlAdded);
            this.Panel2.ControlRemoved += new ControlEventHandler(Panel2_ControlRemoved);
        }
        private void Panel1_ControlAdded(object sender, ControlEventArgs e)
        {
            //this.Panel1Collapsed = !e.Control.Visible;//??????
            //
            IDockPanel pDockPanel = e.Control as IDockPanel;
            if (pDockPanel == null) { this.Panel1.Controls.Remove(e.Control); return; }
            //DockAreaStyle enumDockAreaStyle = this.GetDockAreaStyle();
            //switch (enumDockAreaStyle)
            //{
            //    case DockAreaStyle.eDockPanelDockArea:
            //        pDockPanel.SetShowHideButton(true); 
            //        break;
            //    default:
            //        pDockPanel.SetShowHideButton(false);
            //        break;
            //}
            pDockPanel.BeforeVisibleExValueSeted += new BoolValueChangedEventHandler(Panel1DockPanel_BeforeVisibleExValueSeted);
        }
        private void Panel1_ControlRemoved(object sender, ControlEventArgs e)
        {
            IDockPanel pDockPanel = e.Control as IDockPanel;
            if (pDockPanel != null)
            {
                //pDockPanel.SetShowHideButton(false);
                pDockPanel.BeforeVisibleExValueSeted -= new BoolValueChangedEventHandler(Panel1DockPanel_BeforeVisibleExValueSeted);
            }
            //
            //
            //
            if (this.Panel1.Controls.Count > 0) return;
            //this.Visible = false;
            //
            Control parentControl = this.Parent;
            if (parentControl == null || this.Panel2.Controls.Count <= 0) { return; } //this.Dispose();
            //
            IDockPanel pDockPanelInPanel2 = GetIDockPanelFromPanel2();
            if (pDockPanelInPanel2 == null) { this.Panel2.Controls.Clear(); return; } //this.Dispose();
            //
            parentControl.SuspendLayout();//--------------------------------
            this.Panel2.SuspendLayout();//----------------------------------
            Control ctr = pDockPanelInPanel2 as Control;
            ctr.Dock = this.Dock;
            ctr.Location = this.Location;
            ctr.Size = this.Size;
            int index = this.Parent.Controls.IndexOf(this);
            this.Panel2.Controls.Remove(ctr);
            parentControl.Controls.Add(ctr);
            parentControl.Controls.SetChildIndex(ctr, index);
            parentControl.Controls.Remove(this);
            pDockPanelInPanel2.VisibleEx = pDockPanelInPanel2.VisibleEx;//key
            this.Panel2.ResumeLayout(false);//----------------------------------
            parentControl.ResumeLayout(false);//--------------------------------
            //
            this.Dispose();
            GC.Collect();
        }
        private void Panel1DockPanel_BeforeVisibleExValueSeted(object sender, BoolValueChangedEventArgs e)
        {
            Control ctr = sender as Control;
            if (this.Panel1.Controls.Contains(ctr)) { this.Panel1Collapsed = !e.NewValue; }
        }
        private void Panel2_ControlAdded(object sender, ControlEventArgs e)
        {
            //this.Panel2Collapsed = !e.Control.Visible;//??????
            //
            IDockPanel pDockPanel = e.Control as IDockPanel;
            if (pDockPanel == null) { this.Panel2.Controls.Remove(e.Control); return; }
            //DockAreaStyle enumDockAreaStyle = this.GetDockAreaStyle();
            //switch (enumDockAreaStyle)
            //{
            //    case DockAreaStyle.eDockPanelDockArea:
            //        pDockPanel.SetShowHideButton(true);
            //        break;
            //    default:
            //        pDockPanel.SetShowHideButton(false);
            //        break;
            //}
            pDockPanel.BeforeVisibleExValueSeted += new BoolValueChangedEventHandler(Panel2DockPanel_BeforeVisibleExValueSeted);
        }
        private void Panel2_ControlRemoved(object sender, ControlEventArgs e)
        {
            IDockPanel pDockPanel = e.Control as IDockPanel;
            if (pDockPanel != null)
            {
                //pDockPanel.SetShowHideButton(false);
                pDockPanel.BeforeVisibleExValueSeted -= new BoolValueChangedEventHandler(Panel2DockPanel_BeforeVisibleExValueSeted);
            }
            //
            //
            //
            if (this.Panel2.Controls.Count > 0) return;
            //this.Visible = false;
            //
            Control parentControl = this.Parent;
            if (parentControl == null || this.Panel1.Controls.Count <= 0) { return; }//this.Dispose();
            //
            IDockPanel pDockPanelInPanel1 = GetIDockPanelFromPanel1();
            if (pDockPanelInPanel1 == null) { this.Panel1.Controls.Clear(); return; }//this.Dispose();
            //
            parentControl.SuspendLayout();//--------------------------------
            this.Panel1.SuspendLayout();//----------------------------------
            Control ctr = pDockPanelInPanel1 as Control;
            ctr.Dock = this.Dock;
            ctr.Location = this.Location;
            ctr.Size = this.Size;
            int index = this.Parent.Controls.IndexOf(this);
            this.Panel1.Controls.Remove(ctr);
            parentControl.Controls.Add(ctr);
            parentControl.Controls.SetChildIndex(ctr, index);
            parentControl.Controls.Remove(this);
            pDockPanelInPanel1.VisibleEx = pDockPanelInPanel1.VisibleEx;//key
            this.Panel1.ResumeLayout(false);//----------------------------------
            parentControl.ResumeLayout(false);//--------------------------------
            //
            this.Dispose();
            GC.Collect();
        }
        private void Panel2DockPanel_BeforeVisibleExValueSeted(object sender, BoolValueChangedEventArgs e)
        {
            Control ctr = sender as Control;
            if (this.Panel2.Controls.Contains(ctr)) { this.Panel2Collapsed = !e.NewValue; }
        }

        #region IRecordItem
        private int m_RecordID = 0;
        [Browsable(false), Description("自身的ID号（由系统管理，主要在记录布局文件时使用，常规状态下它是无效的）"), Category("属性")]
        public int RecordID
        {
            get { return m_RecordID; }
        }
        #endregion

        #region ISetRecordItemHelper
        void ISetRecordItemHelper.SetRecordID(int id)//设置RecordID，由系统管理（在保存布局时设置该属性）
        {
            this.m_RecordID = id;
        }
        #endregion

        #region WFNew.IBaseItem
        WFNew.RenderStyle m_eRenderStyle = WFNew.RenderStyle.eSystem;
        [Browsable(true), DefaultValue(typeof(WFNew.RenderStyle), "eSystem"), Description("渲染类型"), Category("外观")]
        public virtual WFNew.RenderStyle eRenderStyle
        {
            get { return m_eRenderStyle; }
            set { m_eRenderStyle = value; }
        }

        [Browsable(false), Description("自身所处的状态（激活、按下、不可用、正常）"), Category("状态")]
        public WFNew.BaseItemState eBaseItemState
        {
            get { return GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal; }
        }
        #endregion

        #region WFNew.IArea
        [Browsable(false), Description("显示外框线"), Category("外观")]
        public virtual bool ShowOutLine
        {
            get { return false; }
        }

        [Browsable(false), Description("显示背景色"), Category("外观")]
        public bool ShowBackgroud
        {
            get { return true; }
        }

        [Browsable(false), Description("框架矩形"), Category("布局")]
        public virtual Rectangle FrameRectangle
        {
            get
            {
                return new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            }
        }

        [Browsable(false), Description("可视化矩形框"), Category("布局")]
        public virtual Rectangle AreaRectangle
        {
            get
            {
                return new Rectangle(0, 0, this.Width, this.Height);
            }
        }
        #endregion

        #region ISplitPanel
        [Browsable(false), Description("面板矩形框"), Category("布局")]
        public Rectangle PanelRectangle
        {
            get { return new Rectangle(0, 0, this.Width, this.Height); }
        }

        [Browsable(false), Description("分割区屏幕区矩形框（x、y 为屏幕坐标）"), Category("布局")]
        public virtual Rectangle SplitterScreenRectangle//分割区屏幕区矩形框（x、y 为屏幕坐标）
        {
            get 
            {
                Rectangle rectangle = this.SplitterRectangle;
                return new Rectangle(this.PointToScreen(rectangle.Location), rectangle.Size);
            }
        }
        #endregion

        #region IBasePanel
        [Browsable(true), Description("当向DockPanel里添加BasePanel时触发 或 DockPanel的VisibleEx设为true且BasePanel为当前选择项时触发"), Category("状态已更改")]
        public event EventHandler Opened;                                     //当向DockPanel里添加BasePanel时触发 或 DockPanel的VisibleEx设为true且BasePanel为当前选择项时触发
        [Browsable(true), Description("当从DockPanel里移除BasePanel时触发 或 DockPanel的VisibleEx设为false且BasePanel为当前选择项时触发"), Category("状态已更改")]
        public event EventHandler Closed;                                     //当从DockPanel里移除BasePanel时触发 或 DockPanel的VisibleEx设为false且BasePanel为当前选择项时触发
        [Browsable(true), Description("VisibleEx设置前触发"), Category("属性更改前")]
        public event BoolValueChangedEventHandler BeforeVisibleExValueSeted; //VisibleEx设置前触发
        [Browsable(true), Description("VisibleEx设置后触发"), Category("属性已更改")]
        public event BoolValueChangedEventHandler AfterVisibleExValueSeted;  //VisibleEx设置后触发

        [Browsable(false), Description("是否可以顶部停靠"), Category("状态")]
        public bool CanDockUp
        {
            get
            {
                return this.GetCanDockUp();
            }
        }
        public bool GetCanDockUp()//获取CanDockUp
        {
            if (this.Panel1.Controls.Count > 0)
            {
                DockPanel dockPanel = this.Panel1.Controls[0] as DockPanel;
                if (dockPanel != null)
                { if (!dockPanel.CanDockUp) return false; }
                else
                { return this.GetCanDockUp(this.Panel1.Controls[0] as DockPanelContainer); }
            }

            if (this.Panel2.Controls.Count > 0)
            {
                DockPanel dockPanel = this.Panel2.Controls[0] as DockPanel;
                if (dockPanel != null)
                { if (!dockPanel.CanDockUp) return false; }
                else
                { return this.GetCanDockUp(this.Panel2.Controls[0] as DockPanelContainer); }
            }

            return true;
        }
        private bool GetCanDockUp(DockPanelContainer dockPanelContainer)//递归 获取CanDockUp
        {
            if (dockPanelContainer == null) return true;

            if (dockPanelContainer.Panel1.Controls.Count > 0)
            {
                DockPanel dockPanel = dockPanelContainer.Panel1.Controls[0] as DockPanel;
                if (dockPanel != null)
                { if (!dockPanel.CanDockUp) return false; }
                else
                { return this.GetCanDockUp(dockPanelContainer.Panel1.Controls[0] as DockPanelContainer); }
            }

            if (dockPanelContainer.Panel2.Controls.Count > 0)
            {
                DockPanel dockPanel = dockPanelContainer.Panel2.Controls[0] as DockPanel;
                if (dockPanel != null)
                { if (!dockPanel.CanDockUp) return false; }
                else
                { return this.GetCanDockUp(dockPanelContainer.Panel2.Controls[0] as DockPanelContainer); }
            }

            return true;
        }

        [Browsable(false), Description("是否可以左边停靠"), Category("状态")]
        public bool CanDockLeft
        {
            get
            {
                return this.GetCanDockLeft();
            }
        }
        public bool GetCanDockLeft()//获取CanDockLeft
        {
            if (this.Panel1.Controls.Count > 0)
            {
                DockPanel dockPanel = this.Panel1.Controls[0] as DockPanel;
                if (dockPanel != null)
                { if (!dockPanel.CanDockLeft) return false; }
                else
                { return this.GetCanDockLeft(this.Panel1.Controls[0] as DockPanelContainer); }
            }

            if (this.Panel2.Controls.Count > 0)
            {
                DockPanel dockPanel = this.Panel2.Controls[0] as DockPanel;
                if (dockPanel != null)
                { if (!dockPanel.CanDockLeft) return false; }
                else
                { return this.GetCanDockLeft(this.Panel2.Controls[0] as DockPanelContainer); }
            }

            return true;
        }
        private bool GetCanDockLeft(DockPanelContainer dockPanelContainer)//递归 获取CanDockLeft
        {
            if (dockPanelContainer == null) return true;

            if (dockPanelContainer.Panel1.Controls.Count > 0)
            {
                DockPanel dockPanel = dockPanelContainer.Panel1.Controls[0] as DockPanel;
                if (dockPanel != null)
                { if (!dockPanel.CanDockLeft) return false; }
                else
                { return this.GetCanDockLeft(dockPanelContainer.Panel1.Controls[0] as DockPanelContainer); }
            }

            if (dockPanelContainer.Panel2.Controls.Count > 0)
            {
                DockPanel dockPanel = dockPanelContainer.Panel2.Controls[0] as DockPanel;
                if (dockPanel != null)
                { if (!dockPanel.CanDockLeft) return false; }
                else
                { return this.GetCanDockLeft(dockPanelContainer.Panel2.Controls[0] as DockPanelContainer); }
            }

            return true;
        }

        [Browsable(false), Description("是否可以右边停靠"), Category("状态")]
        public bool CanDockRight
        {
            get
            {
                return this.GetCanDockRight();
            }
        }
        public bool GetCanDockRight()//获取CanDockRight
        {
            if (this.Panel1.Controls.Count > 0)
            {
                DockPanel dockPanel = this.Panel1.Controls[0] as DockPanel;
                if (dockPanel != null)
                { if (!dockPanel.CanDockRight) return false; }
                else
                { return this.GetCanDockRight(this.Panel1.Controls[0] as DockPanelContainer); }
            }

            if (this.Panel2.Controls.Count > 0)
            {
                DockPanel dockPanel = this.Panel2.Controls[0] as DockPanel;
                if (dockPanel != null)
                { if (!dockPanel.CanDockRight) return false; }
                else
                { return this.GetCanDockRight(this.Panel2.Controls[0] as DockPanelContainer); }
            }

            return true;
        }
        private bool GetCanDockRight(DockPanelContainer dockPanelContainer)//递归 获取CanDockRight
        {
            if (dockPanelContainer == null) return true;

            if (dockPanelContainer.Panel1.Controls.Count > 0)
            {
                DockPanel dockPanel = dockPanelContainer.Panel1.Controls[0] as DockPanel;
                if (dockPanel != null)
                { if (!dockPanel.CanDockRight) return false; }
                else
                { return this.GetCanDockRight(dockPanelContainer.Panel1.Controls[0] as DockPanelContainer); }
            }

            if (dockPanelContainer.Panel2.Controls.Count > 0)
            {
                DockPanel dockPanel = dockPanelContainer.Panel2.Controls[0] as DockPanel;
                if (dockPanel != null)
                { if (!dockPanel.CanDockRight) return false; }
                else
                { return this.GetCanDockRight(dockPanelContainer.Panel2.Controls[0] as DockPanelContainer); }
            }

            return true;
        }

        [Browsable(false), Description("是否可以底部停靠"), Category("状态")]
        public bool CanDockBottom
        {
            get
            {
                return this.GetCanDockBottom();
            }
        }
        public bool GetCanDockBottom()//获取CanDockBottom
        {
            if (this.Panel1.Controls.Count > 0)
            {
                DockPanel dockPanel = this.Panel1.Controls[0] as DockPanel;
                if (dockPanel != null)
                { if (!dockPanel.CanDockBottom) return false; }
                else
                { return this.GetCanDockBottom(this.Panel1.Controls[0] as DockPanelContainer); }
            }

            if (this.Panel2.Controls.Count > 0)
            {
                DockPanel dockPanel = this.Panel2.Controls[0] as DockPanel;
                if (dockPanel != null)
                { if (!dockPanel.CanDockBottom) return false; }
                else
                { return this.GetCanDockBottom(this.Panel2.Controls[0] as DockPanelContainer); }
            }

            return true;
        }
        private bool GetCanDockBottom(DockPanelContainer dockPanelContainer)//递归 获取CanDockBottom
        {
            if (dockPanelContainer == null) return true;

            if (dockPanelContainer.Panel1.Controls.Count > 0)
            {
                DockPanel dockPanel = dockPanelContainer.Panel1.Controls[0] as DockPanel;
                if (dockPanel != null)
                { if (!dockPanel.CanDockBottom) return false; }
                else
                { return this.GetCanDockBottom(dockPanelContainer.Panel1.Controls[0] as DockPanelContainer); }
            }

            if (dockPanelContainer.Panel2.Controls.Count > 0)
            {
                DockPanel dockPanel = dockPanelContainer.Panel2.Controls[0] as DockPanel;
                if (dockPanel != null)
                { if (!dockPanel.CanDockBottom) return false; }
                else
                { return this.GetCanDockBottom(dockPanelContainer.Panel2.Controls[0] as DockPanelContainer); }
            }

            return true;
        }

        [Browsable(false), Description("是否可以填充"), Category("状态")]
        public bool CanDockFill
        {
            get
            {
                return this.GetCanDockFill();
            }
        }
        public bool GetCanDockFill()//获取CanDockFill
        {
            if (this.Panel1.Controls.Count > 0)
            {
                DockPanel dockPanel = this.Panel1.Controls[0] as DockPanel;
                if (dockPanel != null)
                { if (!dockPanel.CanDockFill) return false; }
                else
                { return this.GetCanDockFill(this.Panel1.Controls[0] as DockPanelContainer); }
            }

            if (this.Panel2.Controls.Count > 0)
            {
                DockPanel dockPanel = this.Panel2.Controls[0] as DockPanel;
                if (dockPanel != null)
                { if (!dockPanel.CanDockFill) return false; }
                else
                { return this.GetCanDockFill(this.Panel2.Controls[0] as DockPanelContainer); }
            }

            return true;
        }
        private bool GetCanDockFill(DockPanelContainer dockPanelContainer)//递归 获取CanDockFill
        {
            if (dockPanelContainer == null) return true;

            if (dockPanelContainer.Panel1.Controls.Count > 0)
            {
                DockPanel dockPanel = dockPanelContainer.Panel1.Controls[0] as DockPanel;
                if (dockPanel != null)
                { if (!dockPanel.CanDockFill) return false; }
                else
                { return this.GetCanDockFill(dockPanelContainer.Panel1.Controls[0] as DockPanelContainer); }
            }

            if (dockPanelContainer.Panel2.Controls.Count > 0)
            {
                DockPanel dockPanel = dockPanelContainer.Panel2.Controls[0] as DockPanel;
                if (dockPanel != null)
                { if (!dockPanel.CanDockFill) return false; }
                else
                { return this.GetCanDockFill(dockPanelContainer.Panel2.Controls[0] as DockPanelContainer); }
            }

            return true;
        }

        [Browsable(false), Description("是否可以浮动"), Category("状态")]
        public bool CanFloat
        {
            get
            {
                return this.GetCanFloat();
            }
        }
        public bool GetCanFloat()//获取CanFloat
        {
            if (this.Panel1.Controls.Count > 0)
            {
                DockPanel dockPanel = this.Panel1.Controls[0] as DockPanel;
                if (dockPanel != null)
                { if (!dockPanel.CanFloat) return false; }
                else
                { return this.GetCanFloat(this.Panel1.Controls[0] as DockPanelContainer); }
            }

            if (this.Panel2.Controls.Count > 0)
            {
                DockPanel dockPanel = this.Panel2.Controls[0] as DockPanel;
                if (dockPanel != null)
                { if (!dockPanel.CanFloat) return false; }
                else
                { return this.GetCanFloat(this.Panel2.Controls[0] as DockPanelContainer); }
            }

            return true;
        }
        private bool GetCanFloat(DockPanelContainer dockPanelContainer)//递归 获取CanFloat
        {
            if (dockPanelContainer == null) return true;

            if (dockPanelContainer.Panel1.Controls.Count > 0)
            {
                DockPanel dockPanel = dockPanelContainer.Panel1.Controls[0] as DockPanel;
                if (dockPanel != null)
                { if (!dockPanel.CanFloat) return false; }
                else
                { return this.GetCanFloat(dockPanelContainer.Panel1.Controls[0] as DockPanelContainer); }
            }

            if (dockPanelContainer.Panel2.Controls.Count > 0)
            {
                DockPanel dockPanel = dockPanelContainer.Panel2.Controls[0] as DockPanel;
                if (dockPanel != null)
                { if (!dockPanel.CanFloat) return false; }
                else
                { return this.GetCanFloat(dockPanelContainer.Panel2.Controls[0] as DockPanelContainer); }
            }

            return true;
        }

        [Browsable(false), Description("是否可以隐藏"), Category("状态")]
        public bool CanHide
        {
            get
            {
                return this.GetCanHide();
            }
        }
        public bool GetCanHide()//获取CanHide
        {
            if (this.Panel1.Controls.Count > 0)
            {
                DockPanel dockPanel = this.Panel1.Controls[0] as DockPanel;
                if (dockPanel != null)
                { if (!dockPanel.CanHide) return false; }
                else
                { return this.GetCanHide(this.Panel1.Controls[0] as DockPanelContainer); }
            }

            if (this.Panel2.Controls.Count > 0)
            {
                DockPanel dockPanel = this.Panel2.Controls[0] as DockPanel;
                if (dockPanel != null)
                { if (!dockPanel.CanHide) return false; }
                else
                { return this.GetCanHide(this.Panel2.Controls[0] as DockPanelContainer); }
            }

            return true;
        }
        private bool GetCanHide(DockPanelContainer dockPanelContainer)//递归 获取CanHide
        {
            if (dockPanelContainer == null) return true;

            if (dockPanelContainer.Panel1.Controls.Count > 0)
            {
                DockPanel dockPanel = dockPanelContainer.Panel1.Controls[0] as DockPanel;
                if (dockPanel != null)
                { if (!dockPanel.CanHide) return false; }
                else
                { return this.GetCanHide(dockPanelContainer.Panel1.Controls[0] as DockPanelContainer); }
            }

            if (dockPanelContainer.Panel2.Controls.Count > 0)
            {
                DockPanel dockPanel = dockPanelContainer.Panel2.Controls[0] as DockPanel;
                if (dockPanel != null)
                { if (!dockPanel.CanHide) return false; }
                else
                { return this.GetCanHide(dockPanelContainer.Panel2.Controls[0] as DockPanelContainer); }
            }

            return true;
        }

        [Browsable(false), Description("是否可以关闭"), Category("状态")]
        public bool CanClose
        {
            get
            {
                return this.GetCanClose();
            }
        }
        public bool GetCanClose()//获取GetCanClose
        {
            if (this.Panel1.Controls.Count > 0)
            {
                DockPanel dockPanel = this.Panel1.Controls[0] as DockPanel;
                if (dockPanel != null)
                { if (!dockPanel.CanClose) return false; }
                else
                { return this.GetCanClose(this.Panel1.Controls[0] as DockPanelContainer); }
            }

            if (this.Panel2.Controls.Count > 0)
            {
                DockPanel dockPanel = this.Panel2.Controls[0] as DockPanel;
                if (dockPanel != null)
                { if (!dockPanel.CanClose) return false; }
                else
                { return this.GetCanClose(this.Panel2.Controls[0] as DockPanelContainer); }
            }

            return true;
        }
        private bool GetCanClose(DockPanelContainer dockPanelContainer)//递归 获取GetCanClose
        {
            if (dockPanelContainer == null) return true;

            if (dockPanelContainer.Panel1.Controls.Count > 0)
            {
                DockPanel dockPanel = dockPanelContainer.Panel1.Controls[0] as DockPanel;
                if (dockPanel != null)
                { if (!dockPanel.CanClose) return false; }
                else
                { return this.GetCanClose(dockPanelContainer.Panel1.Controls[0] as DockPanelContainer); }
            }

            if (dockPanelContainer.Panel2.Controls.Count > 0)
            {
                DockPanel dockPanel = dockPanelContainer.Panel2.Controls[0] as DockPanel;
                if (dockPanel != null)
                { if (!dockPanel.CanClose) return false; }
                else
                { return this.GetCanClose(dockPanelContainer.Panel2.Controls[0] as DockPanelContainer); }
            }

            return true;
        }

        [Browsable(false), Description("是否为基础面板"), Category("状态")]
        public bool IsBasePanel
        {
            get
            {
                return this.GetIsBasePanel();
            }
        }
        public bool GetIsBasePanel()//获取IsBasePanel
        {
            if (this.Panel1.Controls.Count > 0)
            {
                DockPanel dockPanel = this.Panel1.Controls[0] as DockPanel;
                if (dockPanel != null)
                { if (!dockPanel.IsBasePanel) return false; }
                else
                { return this.GetIsBasePanel(this.Panel1.Controls[0] as DockPanelContainer); }
            }

            if (this.Panel2.Controls.Count > 0)
            {
                DockPanel dockPanel = this.Panel2.Controls[0] as DockPanel;
                if (dockPanel != null)
                { if (!dockPanel.IsBasePanel) return false; }
                else
                { return this.GetIsBasePanel(this.Panel2.Controls[0] as DockPanelContainer); }
            }

            return true;
        }
        private bool GetIsBasePanel(DockPanelContainer dockPanelContainer)//递归 获取IsBasePanel
        {
            if (dockPanelContainer == null) return true;

            if (dockPanelContainer.Panel1.Controls.Count > 0)
            {
                DockPanel dockPanel = dockPanelContainer.Panel1.Controls[0] as DockPanel;
                if (dockPanel != null)
                { if (!dockPanel.IsBasePanel) return false; }
                else
                { return this.GetIsBasePanel(dockPanelContainer.Panel1.Controls[0] as DockPanelContainer); }
            }

            if (dockPanelContainer.Panel2.Controls.Count > 0)
            {
                DockPanel dockPanel = dockPanelContainer.Panel2.Controls[0] as DockPanel;
                if (dockPanel != null)
                { if (!dockPanel.IsBasePanel) return false; }
                else
                { return this.GetIsBasePanel(dockPanelContainer.Panel2.Controls[0] as DockPanelContainer); }
            }

            return true;
        }

        [Browsable(false), Description("是否为文档面板"), Category("状态")]
        public bool IsDocumentPanel
        {
            get
            {
                return this.GetIsDocumentPanel();
            }
        }
        public bool GetIsDocumentPanel()//获取IsDocumentPanel
        {
            if (this.Panel1.Controls.Count > 0)
            {
                DockPanel dockPanel = this.Panel1.Controls[0] as DockPanel;
                if (dockPanel != null)
                { if (!dockPanel.IsDocumentPanel) return false; }
                else
                { return this.GetIsDocumentPanel(this.Panel1.Controls[0] as DockPanelContainer); }
            }

            if (this.Panel2.Controls.Count > 0)
            {
                DockPanel dockPanel = this.Panel2.Controls[0] as DockPanel;
                if (dockPanel != null)
                { if (!dockPanel.IsDocumentPanel) return false; }
                else
                { return this.GetIsDocumentPanel(this.Panel2.Controls[0] as DockPanelContainer); }
            }

            return true;
        }
        private bool GetIsDocumentPanel(DockPanelContainer dockPanelContainer)//递归 获取IsDocumentPanel
        {
            if (dockPanelContainer == null) return true;

            if (dockPanelContainer.Panel1.Controls.Count > 0)
            {
                DockPanel dockPanel = dockPanelContainer.Panel1.Controls[0] as DockPanel;
                if (dockPanel != null)
                { if (!dockPanel.IsDocumentPanel) return false; }
                else
                { return this.GetIsDocumentPanel(dockPanelContainer.Panel1.Controls[0] as DockPanelContainer); }
            }

            if (dockPanelContainer.Panel2.Controls.Count > 0)
            {
                DockPanel dockPanel = dockPanelContainer.Panel2.Controls[0] as DockPanel;
                if (dockPanel != null)
                { if (!dockPanel.IsDocumentPanel) return false; }
                else
                { return this.GetIsDocumentPanel(dockPanelContainer.Panel2.Controls[0] as DockPanelContainer); }
            }

            return true;
        }

        [Browsable(false), Description("记录面板类型"), Category("属性")]
        public BasePanelStyle eBasePanelStyle//记录面板类型
        { get { return BasePanelStyle.eDockPanelContainer; } }

        [Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        Description("设置该控件的可视状态，用来替代Visible属性（请不要使用Visible属性）"), Category("状态")]
        public bool VisibleEx//设置该控件的可视状态，用来替代Visible属性（请不要使用Visible属性）
        {
            get { return GetVisible(); }
            set
            {
                bool bOldValue = this.GetVisible();
                this.OnBeforeVisibleExValueSeted(new BoolValueChangedEventArgs(bOldValue, value));
                //
                base.Visible = value;//key
                if (value)
                {
                    ((ISetPanelNodeStateHelper)this).SetPanelNodeState(PanelNodeState.eShow);
                    if (base.Parent == null) { this.ToDockPanelFloatForm(); }
                    //bool bOk = true;
                    //if (base.Parent == null) { bOk = this.ToDockPanelFloatForm(); }
                    //if (bOk) { this.OnOpened(this, new EventArgs()); }
                }
                else
                {
                    ((ISetPanelNodeStateHelper)this).SetPanelNodeState(PanelNodeState.eClose);
                    //this.SetPanelState(PanelState.eHide);
                    //this.OnClosed(this, new EventArgs());
                }
                //
                this.OnAfterVisibleExValueSeted(new BoolValueChangedEventArgs(bOldValue, value));
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Description("记录DockPanel转化为停靠窗体后所在的坐标"), Category("布局")]
        public Point DockPanelFloatFormLocation//记录DockPanel转化为停靠窗体后所在的坐标
        {
            get { return m_DockPanelFloatFormLocation; }
            set { m_DockPanelFloatFormLocation = value; }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Description("记录DockPanel转化为停靠窗体后所在的坐标"), Category("布局")]
        public Size DockPanelFloatFormSize//记录DockPanel转化为停靠窗体后所在的坐标
        {
            get { return new Size(this.m_DockPanelFloatFormSize.Width /*+ 16*/, this.m_DockPanelFloatFormSize.Height /*+ 52*/); }
            set { this.m_DockPanelFloatFormSize = value; }
        }

        [Browsable(false), Description("浮动面板管理器"), Category("关联")]
        public DockPanelManager DockPanelManager
        {
            get { return m_DockPanelManager; }
        }

        [Browsable(false), Description("描述信息"), Category("属性")]
        public string Describe
        { get { return "【由系统自动管理】停靠面板容器（DockPanelContainer）： 用来停靠所有继承于停靠面板接口（IDockPanel）的控件（包括：DockPanel、HoldDockPanel和DockPanelContainer），相当于面板树的一个二叉树节点。"; } }

        public void LostFocusEx()//使控件失去焦点（每次移除前需要调用盖函数）
        {
            if (!this.ContainsFocus) return;

            bool bOld = base.Enabled;
            base.Enabled = false;
            base.Enabled = bOld;
        }

        public void Open()//展现
        {
            this.VisibleEx = true;
        }

        public void Close()//关闭
        {
            this.VisibleEx = false;
        }

        public bool ToDockPanelFloatForm()//转化为浮动窗体
        {
            if (!this.CanFloat)
            {
                GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show("无法实现停靠效果！停靠对象内存在不支持浮动的基础面板项。");
                return false;
            }
            //
            //if (this.Parent is DockPanelFloatForm) return false;
            DockPanelContainerStyle eDockPanelContainerStyle = this.GetDockPanelContainerStyle();
            if (eDockPanelContainerStyle == DockPanelContainerStyle.eDockPanelFloatForm) return false;
            //
            this.LostFocusEx();//使其失去焦点 key
            //
            //DockPanelFloatForm DockPanelFloatForm1 = new DockPanelFloatForm(this);
            DockPanelFloatForm DockPanelFloatForm1 = this.DockPanelManager.GetEmptyDockPanelFloatForm();
            DockPanelFloatForm1.StartPosition = FormStartPosition.CenterParent;
            DockPanelFloatForm1.Show(this);
            return true;
        }

        public bool ToDockPanelFloatForm(Point moustPoint)//转化为浮动窗体
        {
            if (!this.CanFloat)
            {
                GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show("无法实现停靠效果！停靠对象内存在不支持浮动的基础面板项。");
                return false;
            }
            //
            //if (this.Parent is DockPanelFloatForm) return false;
            DockPanelContainerStyle eDockPanelContainerStyle = this.GetDockPanelContainerStyle();
            if (eDockPanelContainerStyle == DockPanelContainerStyle.eDockPanelFloatForm) return false;
            //
            this.LostFocusEx();//使其失去焦点 key
            //
            //DockPanelFloatForm DockPanelFloatForm1 = new DockPanelFloatForm(this);
            DockPanelFloatForm DockPanelFloatForm1 = this.DockPanelManager.GetEmptyDockPanelFloatForm();
            DockPanelFloatForm1.Show(this, moustPoint);
            return true;
        }
        #endregion

        #region IDock
        public bool ToDockArea(bool bInteral, DockStyle eDockStyle)
        {
            return this.ToDockArea(bInteral, eDockStyle, new Point(0, 0));
        }

        public bool ToDockArea(bool bInteral, DockStyle eDockStyle, Point location)
        {
            if (this.DockPanelManager == null || this.DockPanelManager.ParentForm == null) return false;
            //
            switch (eDockStyle)
            {
                case DockStyle.Fill:
                    if (!this.IsDocumentPanel)
                    {
                        GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show("无法停靠至文档区内部！停靠对象内存在不支持停靠至文档区的基础面板项。");
                        return false;
                    }
                    if (this.DockPanelManager.DocumentArea != null && 
                        this.m_DockPanelManager.DocumentArea is DocumentDockArea)
                    {
                        DockPanel dockPanel = ((DocumentDockArea)this.DockPanelManager.DocumentArea).GetDockPanel();
                        if (dockPanel == null)
                        {
                            if (this.Parent != null) this.Parent.Controls.Remove(this);
                            this.Dock = DockStyle.Fill;
                            this.DockPanelManager.DocumentArea.Controls.Add(this);//.Panel
                        }
                        else
                        {
                            dockPanel.AddDockPanel(this, DockStyle.Fill);
                        }
                        return true;
                    }
                    return false;
                case DockStyle.Top:
                    if (!this.CanDockUp)
                    {
                        GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show("无法实现顶部停靠效果！停靠对象内存在不支持顶部停靠效果的基础面板项。");
                        return false;
                    }
                    break;
                case DockStyle.Left:
                    if (!this.CanDockLeft)
                    {
                        GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show("无法实现左边停靠效果！停靠对象内存在不支持左边停靠效果的基础面板项。");
                        return false;
                    }
                    break;
                case DockStyle.Right:
                    if (!this.CanDockRight)
                    {
                        GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show("无法实现右边停靠效果！停靠对象内存在不支持右边停靠效果的基础面板项。");
                        return false;
                    }
                    break;
                case DockStyle.Bottom:
                    if (!this.CanDockBottom)
                    {
                        GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show("无法实现底部停靠效果！停靠对象内存在不支持底部停靠效果的基础面板项。");
                        return false;
                    }
                    break;
                default:
                    GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show("未知的停靠类型！");
                    return false;
            }
            //
            this.LostFocusEx();//使其失去焦点 key
            //
            //DockPanelDockArea dockArea = new DockPanelDockArea();
            DockPanelDockArea dockArea = this.DockPanelManager.GetEmptyDockPanelDockArea();
            dockArea.bInteral = bInteral;
            dockArea.SuspendLayout();//----------------------------------
            dockArea.Location = location;
            dockArea.Dock = eDockStyle;
            dockArea.Size = this.Size;
            DockPanelFloatForm parentDockPanelFloatForm = this.Parent as DockPanelFloatForm;//key
            if (this.Parent != null) this.Parent.Controls.Remove(this);
            this.Dock = DockStyle.Fill;
            dockArea.Controls.Add(this);//.Panel
            dockArea.ResumeLayout(false);//----------------------------------
            //
            if (parentDockPanelFloatForm != null) { parentDockPanelFloatForm.Close(); parentDockPanelFloatForm.Dispose(); }//key
            dockArea.Visible = false;
            this.DockPanelManager.ParentForm.Controls.Add(dockArea);
            int iIndex = this.DockPanelManager.DocumentAreaIndex + 1;
            if (iIndex < this.DockPanelManager.ParentForm.Controls.Count - 1)
            { this.DockPanelManager.ParentForm.Controls.SetChildIndex(dockArea, iIndex); }
            if (!bInteral && this.DockPanelManager.ParentForm.Controls.Count > 1)
            {
                int indexOfTop = iIndex;
                for (int i = this.DockPanelManager.ParentForm.Controls.Count - 1; i > 0; i--)
                {
                    if (this.DockPanelManager.ParentForm.Controls[i] is DockPanelDockArea)
                    { indexOfTop = i; break; }
                }
                if (indexOfTop > 0 &&
                    indexOfTop < this.DockPanelManager.ParentForm.Controls.Count - 1)
                {
                    this.DockPanelManager.ParentForm.Controls.SetChildIndex(dockArea, indexOfTop);
                    //this.DockPanelManager.ParentForm.Refresh();
                }
            }
            dockArea.Visible = true;
            //
            dockArea.Relayout();
            //
            return true;
        }
        #endregion

        #region IDockPanel
        [Browsable(false), Description("父控件"), Category("关联")]
        public Control ParentControl
        { get { return base.Parent; } }

        [Browsable(false), Description("自身IDockPanel的类型"), Category("状态")]
        public DockPanelStyle eDockPanelStyle //自身IDockPanel的类型
        { get { return DockPanelStyle.eDockPanelContainer; } }
        
        public bool GetVisible()//只要其父控件存在不可见状态，则视其为不可见
        {
            if (this.Parent != null)
            {
                if (this.Parent is IDockArea) { return this.Parent.Visible; }
                //
                if (this.Parent.Visible) { return this.GetVisible(this.Parent); }
                else { return false; }
            }
            else
            {
                return false;
            }
        }
        private bool GetVisible(Control ctr)//递归 查询可视状态
        {
            if (ctr.Parent != null)
            {
                if (ctr.Parent is IDockArea) { return ctr.Parent.Visible; }
                //
                if (ctr.Parent.Visible) { return this.GetVisible(ctr.Parent); }
                else { return false; }
            }
            else
            {
                return false;
            }
        }

        public void RemoveFromParent()
        {
            if (this.Parent != null) { this.Parent.Controls.Remove(this); }
        }

        public void ClearBasePanels()//清除BasePanels
        {
            if (this.Panel1.Controls.Count > 0)
            {
                DockPanel dockPanel = this.Panel1.Controls[0] as DockPanel;
                if (dockPanel != null)
                {
                    dockPanel.ClearBasePanels();
                    this.Panel1.ControlAdded -= new ControlEventHandler(Panel1_ControlAdded);
                    this.Panel1.ControlRemoved -= new ControlEventHandler(Panel1_ControlRemoved);
                    this.Panel1.Controls.Clear();
                }
                else
                { this.ClearBasePanels(this.Panel1.Controls[0] as DockPanelContainer); }
            }

            if (this.Panel2.Controls.Count > 0)
            {
                DockPanel dockPanel = this.Panel2.Controls[0] as DockPanel;
                if (dockPanel != null)
                {
                    dockPanel.ClearBasePanels();
                    this.Panel2.ControlAdded -= new ControlEventHandler(Panel2_ControlAdded);
                    this.Panel2.ControlRemoved -= new ControlEventHandler(Panel2_ControlRemoved);
                    this.Panel2.Controls.Clear();
                }
                else
                { this.ClearBasePanels(this.Panel2.Controls[0] as DockPanelContainer); }
            }
        }
        private void ClearBasePanels(DockPanelContainer dockPanelContainer)//递归 清除BasePanels
        {
            if (dockPanelContainer == null) return;
            //
            if (dockPanelContainer.Panel1.Controls.Count > 0)
            {
                DockPanel dockPanel = dockPanelContainer.Panel1.Controls[0] as DockPanel;
                if (dockPanel != null)
                {
                    dockPanel.ClearBasePanels();
                    this.Panel1.ControlAdded -= new ControlEventHandler(Panel1_ControlAdded);
                    this.Panel1.ControlRemoved -= new ControlEventHandler(Panel1_ControlRemoved);
                    dockPanelContainer.Panel1.Controls.Clear();
                }
                else
                { this.ClearBasePanels(dockPanelContainer.Panel1.Controls[0] as DockPanelContainer); }
            }

            if (dockPanelContainer.Panel2.Controls.Count > 0)
            {
                DockPanel dockPanel = dockPanelContainer.Panel2.Controls[0] as DockPanel;
                if (dockPanel != null)
                {
                    dockPanel.ClearBasePanels();
                    this.Panel2.ControlAdded -= new ControlEventHandler(Panel2_ControlAdded);
                    this.Panel2.ControlRemoved -= new ControlEventHandler(Panel2_ControlRemoved);
                    dockPanelContainer.Panel2.Controls.Clear();
                }
                else
                { this.ClearBasePanels(dockPanelContainer.Panel2.Controls[0] as DockPanelContainer); }
            }
        }

        public BasePanel[] GetBasePanels()//获取BasePanels
        {
            List<BasePanel> basePanelCol = new List<BasePanel>();
            if (this.Panel1.Controls.Count > 0)
            {
                DockPanel dockPanel = this.Panel1.Controls[0] as DockPanel;
                if (dockPanel != null && dockPanel.BasePanels.Count > 0)
                { basePanelCol.AddRange(dockPanel.GetBasePanels()); }
                else
                { this.GetBasePanels(this.Panel1.Controls[0] as DockPanelContainer, basePanelCol); }
            }
            if (this.Panel2.Controls.Count > 0)
            {
                DockPanel dockPanel = this.Panel2.Controls[0] as DockPanel;
                if (dockPanel != null && dockPanel.BasePanels.Count > 0)
                { basePanelCol.AddRange(dockPanel.GetBasePanels()); }
                else
                { this.GetBasePanels(this.Panel2.Controls[0] as DockPanelContainer, basePanelCol); }
            }
            return basePanelCol.ToArray();
        }
        private void GetBasePanels(DockPanelContainer dockPanelContainer, List<BasePanel> basePanelCol)//递归 获取BasePanels
        {
            if (dockPanelContainer == null) return;
            //
            if (dockPanelContainer.Panel1.Controls.Count > 0)
            {
                DockPanel dockPanel = dockPanelContainer.Panel1.Controls[0] as DockPanel;
                if (dockPanel != null && dockPanel.BasePanels.Count > 0)
                { basePanelCol.AddRange(dockPanel.GetBasePanels()); }
                else
                { this.GetBasePanels(dockPanelContainer.Panel1.Controls[0] as DockPanelContainer, basePanelCol); }
            }

            if (dockPanelContainer.Panel2.Controls.Count > 0)
            {
                DockPanel dockPanel = dockPanelContainer.Panel2.Controls[0] as DockPanel;
                if (dockPanel != null && dockPanel.BasePanels.Count > 0)
                { basePanelCol.AddRange(dockPanel.GetBasePanels()); }
                else
                { this.GetBasePanels(dockPanelContainer.Panel2.Controls[0] as DockPanelContainer, basePanelCol); }
            }
        }

        public IDockPanelContainer GetDockPanelContainer()//获取上容器的类型
        {
            if (this.Parent == null) return null;
            if (this.Parent is System.Windows.Forms.SplitterPanel)
            {
                return this.Parent.Parent as IDockPanelContainer;
            }
            else
            {
                return this.Parent as IDockPanelContainer;
            }
        }

        public DockPanelContainerStyle GetDockPanelContainerStyle()//获取上容器的类型
        {
            if (this.Parent == null) return DockPanelContainerStyle.eNone;
            if (this.Parent is System.Windows.Forms.SplitterPanel)
            {
                IDockPanelContainer pDockPanelContainer = this.Parent.Parent as IDockPanelContainer;
                if (pDockPanelContainer == null) return DockPanelContainerStyle.eNone;
                return pDockPanelContainer.eDockPanelContainerStyle;
            }
            else
            {
                IDockPanelContainer pDockPanelContainer = this.Parent as IDockPanelContainer;
                if (pDockPanelContainer == null) return DockPanelContainerStyle.eNone;
                return pDockPanelContainer.eDockPanelContainerStyle;
            }
        }

        public IDockArea GetDockArea()//获取中级停靠区
        {
            if (this.Parent == null) return null;
            IDockArea pDockArea = null;
            if (this.Parent is System.Windows.Forms.SplitterPanel)
            {
                pDockArea = this.Parent.Parent as IDockArea;
                if (pDockArea != null) { return pDockArea; }
                else { return GetDockArea_DG(this.Parent.Parent); }
            }
            else
            {
                pDockArea = this.Parent as IDockArea;
                if (pDockArea != null) { return pDockArea; }
                else { return GetDockArea_DG(this.Parent); }
            }
        }
        private IDockArea GetDockArea_DG(Control ctr)//递归 获取中级停靠区
        {
            if (ctr.Parent == null) return null;
            IDockArea pDockArea = null;
            if (ctr.Parent is System.Windows.Forms.SplitterPanel)
            {
                pDockArea = ctr.Parent.Parent as IDockArea;
                if (pDockArea != null) { return pDockArea; }
                else { return GetDockArea_DG(ctr.Parent.Parent); }
            }
            else
            {
                pDockArea = ctr.Parent as IDockArea;
                if (pDockArea != null) { return pDockArea; }
                else { return GetDockArea_DG(ctr.Parent); }
            }
        }

        public DockAreaStyle GetDockAreaStyle()//获取中级停靠区
        {
            if (this.Parent == null) return DockAreaStyle.eNone;
            IDockArea pDockArea = null;
            if (this.Parent is System.Windows.Forms.SplitterPanel)
            {
                pDockArea = this.Parent.Parent as IDockArea;
                if (pDockArea != null) { return pDockArea.eDockAreaStyle; }
                else { return GetDockAreaStyle(this.Parent.Parent); }
            }
            else
            {
                pDockArea = this.Parent as IDockArea;
                if (pDockArea != null) { return pDockArea.eDockAreaStyle; }
                else { return GetDockAreaStyle(this.Parent); }
            }
        }
        private DockAreaStyle GetDockAreaStyle(Control ctr)//递归 获取中级停靠区
        {
            if (ctr.Parent == null) return DockAreaStyle.eNone;
            IDockArea pDockArea = null;
            if (ctr.Parent is System.Windows.Forms.SplitterPanel)
            {
                pDockArea = ctr.Parent.Parent as IDockArea;
                if (pDockArea != null) { return pDockArea.eDockAreaStyle; }
                else { return GetDockAreaStyle(ctr.Parent.Parent); }
            }
            else
            {
                pDockArea = ctr.Parent as IDockArea;
                if (pDockArea != null) { return pDockArea.eDockAreaStyle; }
                else { return GetDockAreaStyle(ctr.Parent); }
            }
        }

        public DockAreaStyle GetDockAreaStyle(out DockStyle eDockStyle)//获取终级停靠区
        {
            eDockStyle = DockStyle.None;
            if (this.Parent == null) return DockAreaStyle.eNone;
            IDockArea pDockArea = null;
            if (this.Parent is System.Windows.Forms.SplitterPanel)
            {
                pDockArea = this.Parent.Parent as IDockArea;
                if (pDockArea != null) { eDockStyle = pDockArea.Dock; return pDockArea.eDockAreaStyle; }
                else { return GetDockAreaStyle(this.Parent.Parent, ref eDockStyle); }
            }
            else
            {
                pDockArea = this.Parent as IDockArea;
                if (pDockArea != null) { eDockStyle = pDockArea.Dock; return pDockArea.eDockAreaStyle; }
                else { return GetDockAreaStyle(this.Parent, ref eDockStyle); }
            }
        }
        private DockAreaStyle GetDockAreaStyle(Control ctr, ref DockStyle eDockStyle)//递归 获取终级停靠区
        {
            if (ctr.Parent == null) return DockAreaStyle.eNone;
            IDockArea pDockArea = null;
            if (ctr.Parent is System.Windows.Forms.SplitterPanel)
            {
                pDockArea = ctr.Parent.Parent as IDockArea;
                if (pDockArea != null) { eDockStyle = pDockArea.Dock; return pDockArea.eDockAreaStyle; }
                else { return GetDockAreaStyle(ctr.Parent.Parent, ref eDockStyle); }
            }
            else
            {
                pDockArea = ctr.Parent as IDockArea;
                if (pDockArea != null) { eDockStyle = pDockArea.Dock; return pDockArea.eDockAreaStyle; }
                else { return GetDockAreaStyle(ctr.Parent, ref eDockStyle); }
            }
        }
        
        public void GetDockLicense(ref bool bCanDockUp, ref bool bCanDockLeft, ref bool bCanDockRight, ref bool bCanDockBottom, ref bool bCanDockFill,
            ref bool bCanFloat, ref bool bCanHide, ref bool bCanClose,
            ref bool bIsBasePanel, ref bool bIsDocumentPanel)//获取停靠许可
        {
            if (this.Panel1.Controls.Count > 0)
            {
                DockPanel dockPanel = this.Panel1.Controls[0] as DockPanel;
                if (dockPanel != null)
                {
                    dockPanel.GetDockLicense(ref bCanDockUp, ref bCanDockLeft, ref bCanDockRight, ref bCanDockBottom, ref bCanDockFill,
                      ref bCanFloat, ref bCanHide, ref bCanClose,
                      ref bIsBasePanel, ref bIsDocumentPanel);
                }
                else
                {
                    this.GetDockLicense(this.Panel1.Controls[0] as DockPanelContainer, ref bCanDockUp, ref bCanDockLeft, ref bCanDockRight, ref bCanDockBottom, ref bCanDockFill,
                        ref bCanFloat, ref bCanHide, ref bCanClose,
                        ref bIsBasePanel, ref bIsDocumentPanel); 
                }
            }
            if (this.Panel2.Controls.Count > 0)
            {
                DockPanel dockPanel = this.Panel2.Controls[0] as DockPanel;
                if (dockPanel != null)
                { 
                    dockPanel.GetDockLicense(ref bCanDockUp, ref bCanDockLeft, ref bCanDockRight, ref bCanDockBottom, ref bCanDockFill,
                        ref bCanFloat, ref bCanHide, ref bCanClose,
                        ref bIsBasePanel, ref bIsDocumentPanel);
                }
                else
                { 
                    this.GetDockLicense(this.Panel2.Controls[0] as DockPanelContainer, ref bCanDockUp, ref bCanDockLeft, ref bCanDockRight, ref bCanDockBottom, ref bCanDockFill,
                        ref bCanFloat, ref bCanHide, ref bCanClose,
                        ref bIsBasePanel, ref bIsDocumentPanel);
                }
            }
        }
        private void GetDockLicense(DockPanelContainer dockPanelContainer,
            ref bool bCanDockUp, ref bool bCanDockLeft, ref bool bCanDockRight, ref bool bCanDockBottom, ref bool bCanDockFill,
            ref bool bCanFloat, ref bool bCanHide, ref bool bCanClose,
            ref bool bIsBasePanel, ref bool bIsDocumentPanel)//递归 获取停靠许可
        {
            if (dockPanelContainer.Panel1.Controls.Count > 0)
            {
                DockPanel dockPanel = dockPanelContainer.Panel1.Controls[0] as DockPanel;
                if (dockPanel != null)
                {
                    dockPanel.GetDockLicense(ref bCanDockUp, ref bCanDockLeft, ref bCanDockRight, ref bCanDockBottom, ref bCanDockFill,
                        ref bCanFloat, ref bCanHide, ref bCanClose,
                        ref bIsBasePanel, ref bIsDocumentPanel);
                }
                else
                {
                    this.GetDockLicense(dockPanelContainer.Panel1.Controls[0] as DockPanelContainer, ref bCanDockUp, ref bCanDockLeft, ref bCanDockRight, ref bCanDockBottom, ref bCanDockFill,
                        ref bCanFloat, ref bCanHide, ref bCanClose,
                        ref bIsBasePanel, ref bIsDocumentPanel);
                }
            }
            if (dockPanelContainer.Panel2.Controls.Count > 0)
            {
                DockPanel dockPanel = dockPanelContainer.Panel2.Controls[0] as DockPanel;
                if (dockPanel != null)
                {
                    dockPanel.GetDockLicense(ref bCanDockUp, ref bCanDockLeft, ref bCanDockRight, ref bCanDockBottom, ref bCanDockFill,
                        ref bCanFloat, ref bCanHide, ref bCanClose,
                        ref bIsBasePanel, ref bIsDocumentPanel);
                }
                else
                { 
                    this.GetDockLicense(dockPanelContainer.Panel2.Controls[0] as DockPanelContainer, ref bCanDockUp, ref bCanDockLeft, ref bCanDockRight, ref bCanDockBottom, ref bCanDockFill,
                        ref bCanFloat, ref bCanHide, ref bCanClose,
                        ref bIsBasePanel, ref bIsDocumentPanel);
                }
            }
        }

        public bool AddDockPanel(IDockPanel pDockPanel, DockStyle eDockStyle)//向面板内添加IDockPanel实现动态布局（DockStyle.Fill和DockStyle.None为无效值）
        {
            if (pDockPanel == null || this.Parent == null) return false;
            //
            DockAreaStyle eDockAreaStyle = this.GetDockAreaStyle();
            if (eDockAreaStyle == DockAreaStyle.eDocumentDockArea)
            {
                if (!pDockPanel.IsDocumentPanel)
                {
                    GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show("无法停靠至文档区内部！停靠对象内存在不支持停靠至文档区的基础面板项。");
                    return false;
                }
            }
            else
            {
                if (!pDockPanel.IsBasePanel)
                {
                    GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show("无法停靠至停靠面板内部！停靠对象内存在不支持停靠至停靠面板的基础面板项。");
                    return false;
                }
            }
            //
            //使其失去焦点 key
            this.LostFocusEx();
            pDockPanel.LostFocusEx();
            //
            bool bOk = false;
            switch (eDockStyle)
            {
                case DockStyle.Top:
                    bOk = this.AddDockPanelTop(pDockPanel);
                    break;
                case DockStyle.Left:
                    bOk = this.AddDockPanelLeft(pDockPanel);
                    break;
                case DockStyle.Right:
                    bOk = this.AddDockPanelRight(pDockPanel);
                    break;
                case DockStyle.Bottom:
                case DockStyle.Fill:
                    bOk = this.AddDockPanelBottom(pDockPanel);
                    break;
                case DockStyle.None:
                default:
                    bOk = pDockPanel.ToDockPanelFloatForm();
                    break;
            }
            //
            IDockPanelFloatForm pDockPanelFloatForm = this.GetDockArea() as IDockPanelFloatForm;
            if (pDockPanelFloatForm != null) { pDockPanelFloatForm.Invalidate(pDockPanelFloatForm.CaptionRectangle); }
            //
            return bOk;
        }
        private bool AddDockPanelTop(IDockPanel pDockPanel)//添加到其顶部
        {
            if (!pDockPanel.CanDockUp)
            {
                GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show("无法实现顶部停靠效果！停靠对象内存在不支持顶部停靠效果的基础面板项。");
                return false;
            }
            //
            if (this.Parent != null)
            {
                DockPanelContainer dockPanelContainer = CreateDockPanelContainer();
                dockPanelContainer.SuspendLayout();//--------------------------------------
                dockPanelContainer.Orientation = Orientation.Horizontal;
                dockPanelContainer.SplitterDistance = dockPanelContainer.Height / 2;
                //int index = this.Parent.Controls.IndexOf(this);
                this.Parent.Controls.Add(dockPanelContainer);
                //this.Parent.Controls.SetChildIndex(dockPanelContainer, index);
                this.Parent.Controls.Remove(this);
                //
                //pDockPanel.Dock = DockStyle.Fill;
                pDockPanel.RemoveFromParent();
                dockPanelContainer.Panel1.Controls.Add(pDockPanel as Control);
                this.Dock = DockStyle.Fill;
                dockPanelContainer.Panel2.Controls.Add(this);
                dockPanelContainer.ResumeLayout(false);//----------------------------------
                //
                return true;
            }
            //
            return false;
        }
        private bool AddDockPanelLeft(IDockPanel pDockPanel)//添加到其左边
        {
            if (!pDockPanel.CanDockLeft)
            {
                GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show("无法实现左边停靠效果！停靠对象内存在不支持左边停靠效果的基础面板项。");
                return false;
            }
            //
            if (this.Parent != null)
            {
                DockPanelContainer dockPanelContainer = CreateDockPanelContainer();
                dockPanelContainer.SuspendLayout();//--------------------------------------
                dockPanelContainer.Orientation = Orientation.Vertical;
                dockPanelContainer.SplitterDistance = dockPanelContainer.Width / 2;
                //int index = this.Parent.Controls.IndexOf(this);
                this.Parent.Controls.Add(dockPanelContainer);
                //this.Parent.Controls.SetChildIndex(dockPanelContainer, index);
                this.Parent.Controls.Remove(this);
                //
                //pDockPanel.Dock = DockStyle.Fill;
                pDockPanel.RemoveFromParent();
                dockPanelContainer.Panel1.Controls.Add(pDockPanel as Control);
                this.Dock = DockStyle.Fill;
                dockPanelContainer.Panel2.Controls.Add(this);
                dockPanelContainer.ResumeLayout(false);//----------------------------------
                //
                return true;
            }
            //
            return false;
        }
        private bool AddDockPanelRight(IDockPanel pDockPanel)//添加到其右边部
        {
            if (!pDockPanel.CanDockRight)
            {
                GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show("无法实现右边停靠效果！停靠对象内存在不支持右边停靠效果的基础面板项。");
                return false;
            }
            //
            if (this.Parent != null)
            {
                DockPanelContainer dockPanelContainer = CreateDockPanelContainer();
                dockPanelContainer.SuspendLayout();//--------------------------------------
                dockPanelContainer.Orientation = Orientation.Vertical;
                dockPanelContainer.SplitterDistance = dockPanelContainer.Width / 2;
                //int index = this.Parent.Controls.IndexOf(this);
                this.Parent.Controls.Add(dockPanelContainer);
                //this.Parent.Controls.SetChildIndex(dockPanelContainer, index);
                this.Parent.Controls.Remove(this);
                //
                this.Dock = DockStyle.Fill;
                dockPanelContainer.Panel1.Controls.Add(this);
                //pDockPanel.Dock = DockStyle.Fill;
                pDockPanel.RemoveFromParent();
                dockPanelContainer.Panel2.Controls.Add(pDockPanel as Control);
                dockPanelContainer.ResumeLayout(false);//----------------------------------
                //
                return true;
            }
            //
            return false;
        }
        private bool AddDockPanelBottom(IDockPanel pDockPanel)//添加到其底部
        {
            if (!pDockPanel.CanDockBottom)
            {
                GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show("无法实现底部停靠效果！停靠对象内存在不支持底部停靠效果的基础面板项。");
                return false;
            }
            //
            if (this.Parent != null)
            {
                DockPanelContainer dockPanelContainer = CreateDockPanelContainer();
                dockPanelContainer.SuspendLayout();//--------------------------------------
                dockPanelContainer.Orientation = Orientation.Horizontal;
                dockPanelContainer.SplitterDistance = dockPanelContainer.Height / 2;
                //int index = this.Parent.Controls.IndexOf(this);
                this.Parent.Controls.Add(dockPanelContainer);
                //this.Parent.Controls.SetChildIndex(dockPanelContainer, index);
                this.Parent.Controls.Remove(this);
                //
                this.Dock = DockStyle.Fill;
                dockPanelContainer.Panel1.Controls.Add(this);
                //pDockPanel.Dock = DockStyle.Fill;
                pDockPanel.RemoveFromParent();
                dockPanelContainer.Panel2.Controls.Add(pDockPanel as Control);
                dockPanelContainer.ResumeLayout(false);//----------------------------------
                //
                return true;
            }
            //
            return false;
        }
        private DockPanelContainer CreateDockPanelContainer()//自动创建DockPanelContainer（在AddDockPanelFill、AddDockPanelLeft、AddDockPanelRight、AddDockPanelBottom）
        {
            DockPanelContainer dockPanelContainer = this.DockPanelManager.GetEmptyDockPanelContainer();
            dockPanelContainer.Orientation = Orientation.Horizontal;
            dockPanelContainer.Location = this.Location;
            dockPanelContainer.Size = this.Size;
            dockPanelContainer.Dock = this.Dock;
            dockPanelContainer.DockPanelFloatFormLocation = this.DockPanelFloatFormLocation;
            dockPanelContainer.DockPanelFloatFormSize = this.DockPanelFloatFormSize;
            return dockPanelContainer;
        }
        #endregion

        #region IDockPanelContainer
        public DockPanel[] GetDockPanels()//获取DockPanels
        {
            List<DockPanel> dockPanelCol = new List<DockPanel>();
            if (this.Panel1.Controls.Count > 0)
            {
                DockPanel dockPanel = this.Panel1.Controls[0] as DockPanel;
                if (dockPanel != null)
                { dockPanelCol.Add(dockPanel); }
                else
                { this.GetDockPanels(this.Panel1.Controls[0] as DockPanelContainer, dockPanelCol); }
            }
            if (this.Panel2.Controls.Count > 0)
            {
                DockPanel dockPanel = this.Panel2.Controls[0] as DockPanel;
                if (dockPanel != null)
                { dockPanelCol.Add(dockPanel); }
                else
                { this.GetDockPanels(this.Panel2.Controls[0] as DockPanelContainer, dockPanelCol); }
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

        [Browsable(false), Description("记录面板类型"), Category("属性")]
        public DockPanelContainerStyle eDockPanelContainerStyle { get { return DockPanelContainerStyle.eDockPanelContainer; } }
        #endregion

        #region IDockPanelContainer2
        public IDockPanel GetIDockPanelFromPanel1()//获取Panel1里的IDockPanel
        {
            IDockPanel pDockPanel = null;
            for (int i = 0; i < this.Panel1.Controls.Count; i++)
            {
                pDockPanel = this.Panel1.Controls[0] as IDockPanel;
                if (pDockPanel != null) { return pDockPanel; }
            }
            return pDockPanel;
        }

        public IDockPanel GetIDockPanelFromPanel2()//获取Panel2里的IDockPanel
        {
            IDockPanel pDockPanel = null;
            for (int i = 0; i < this.Panel2.Controls.Count; i++)
            {
                pDockPanel = this.Panel2.Controls[0] as IDockPanel;
                if (pDockPanel != null) { return pDockPanel; }
            }
            return pDockPanel;
        }
        #endregion

        #region IBinaryNode
        [Browsable(false), Description("获取节点类型"), Category("属性")]
        public NodeStyle eNodeStyle//获取节点类型
        { get { return NodeStyle.eBinaryNode; } }

        [Browsable(false), Description("获取其父节点"), Category("属性")]
        public IBaseNode ParentNode//获取其父节点
        {
            get
            {
                if (this.Parent is System.Windows.Forms.SplitterPanel) { return this.Parent.Parent as IBaseNode; }
                return this.Parent as IBaseNode;
            }
        }

        [Browsable(false), Description("获取左节点（Panel1）"), Category("属性")]
        public IBaseNode LeftNode//获取左节点（Panel1）
        {
            get { return this.GetIDockPanelFromPanel1() as IBaseNode; }
        }

        [Browsable(false), Description("获取左节点（Panel2）"), Category("属性")]
        public IBaseNode RightNode//获取右节点（Panel2）
        {
            get { return this.GetIDockPanelFromPanel2() as IBaseNode; }
        }
        #endregion

        #region ISetDockPanelManagerHelper
        void ISetDockPanelManagerHelper.SetDockPanelManager(DockPanelManager dockPanelManager)//设置DockPanelManager，由系统管理（添加到DockPanelCollection时，调用该函数）
        {
            this.m_DockPanelManager = dockPanelManager;
        }
        #endregion

        #region ISetPanelNodeStateHelper
        void ISetPanelNodeStateHelper.SetPanelNodeState(PanelNodeState panelNodeState)//设置其所辖的面板节点状态并激发相应事件
        {
            IDockPanel pDockPanel1 = this.GetIDockPanelFromPanel1();
            IDockPanel pDockPanel2 = this.GetIDockPanelFromPanel2();
            //
            switch (panelNodeState)
            {
                case PanelNodeState.eShow:
                    this.OnOpened(new EventArgs());
                    //
                    if (pDockPanel1 != null)
                    {
                        switch (pDockPanel1.eDockPanelStyle)
                        {
                            case DockPanelStyle.eDockPanel:
                                DockPanel dockPanel1 = pDockPanel1 as DockPanel;
                                if (dockPanel1 != null)
                                {
                                    if (!pDockPanel1.VisibleEx) { ((ISetPanelNodeStateHelper)dockPanel1).SetPanelNodeState(PanelNodeState.eClose); }
                                    else { ((ISetPanelNodeStateHelper)dockPanel1).SetPanelNodeState(PanelNodeState.eShow); }
                                }
                                break;
                            case DockPanelStyle.eDockPanelContainer:
                                DockPanelContainer dockPanelContainer1 = pDockPanel1 as DockPanelContainer;
                                if (dockPanelContainer1 != null)
                                {
                                    if (!pDockPanel1.VisibleEx) { ((ISetPanelNodeStateHelper)dockPanelContainer1).SetPanelNodeState(PanelNodeState.eClose); }
                                    else { ((ISetPanelNodeStateHelper)dockPanelContainer1).SetPanelNodeState(PanelNodeState.eShow); }
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    //
                    if (pDockPanel2 != null)
                    {
                        switch (pDockPanel2.eDockPanelStyle)
                        {
                            case DockPanelStyle.eDockPanel:
                                DockPanel dockPanel2 = pDockPanel2 as DockPanel;
                                if (dockPanel2 != null)
                                {
                                    if (!pDockPanel2.VisibleEx) { ((ISetPanelNodeStateHelper)dockPanel2).SetPanelNodeState(PanelNodeState.eClose); }
                                    else { ((ISetPanelNodeStateHelper)dockPanel2).SetPanelNodeState(PanelNodeState.eShow); }
                                }
                                break;
                            case DockPanelStyle.eDockPanelContainer:
                                DockPanelContainer dockPanelContainer2 = pDockPanel2 as DockPanelContainer;
                                if (dockPanelContainer2 != null)
                                {
                                    if (!pDockPanel2.VisibleEx) { ((ISetPanelNodeStateHelper)dockPanelContainer2).SetPanelNodeState(PanelNodeState.eClose); }
                                    else { ((ISetPanelNodeStateHelper)dockPanelContainer2).SetPanelNodeState(PanelNodeState.eShow); }
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case PanelNodeState.eClose:
                    this.OnClosed(new EventArgs());
                    //
                    if (pDockPanel1 != null)
                    {
                        switch (pDockPanel1.eDockPanelStyle)
                        {
                            case DockPanelStyle.eDockPanel:
                                DockPanel dockPanel1 = pDockPanel1 as DockPanel;
                                if (dockPanel1 != null) { ((ISetPanelNodeStateHelper)dockPanel1).SetPanelNodeState(PanelNodeState.eClose); }
                                break;
                            case DockPanelStyle.eDockPanelContainer:
                                DockPanelContainer dockPanelContainer1 = pDockPanel1 as DockPanelContainer;
                                if (dockPanelContainer1 != null) { ((ISetPanelNodeStateHelper)dockPanelContainer1).SetPanelNodeState(PanelNodeState.eClose); }
                                break;
                            default:
                                break;
                        }
                    }
                    //
                    if (pDockPanel2 != null)
                    {
                        switch (pDockPanel2.eDockPanelStyle)
                        {
                            case DockPanelStyle.eDockPanel:
                                DockPanel dockPanel2 = pDockPanel2 as DockPanel;
                                if (dockPanel2 != null) { ((ISetPanelNodeStateHelper)dockPanel2).SetPanelNodeState(PanelNodeState.eClose); }
                                break;
                            case DockPanelStyle.eDockPanelContainer:
                                DockPanelContainer dockPanelContainer2 = pDockPanel2 as DockPanelContainer;
                                if (dockPanelContainer2 != null) { ((ISetPanelNodeStateHelper)dockPanelContainer2).SetPanelNodeState(PanelNodeState.eClose); }
                                break;
                            default:
                                break;
                        }
                    }
                    break;
            }
        }
        #endregion

        [Browsable(false)]
        public new bool Panel1Collapsed//修改Panel1Collapsed属性（当Panel1不可见且Panel2不可见时该控件不可见，当控件不可见时Panel1可见则控件也可见，其它不变）
        {
            get
            {
                return this.m_Panel1Collapsed;
            }
            set
            {
                this.m_Panel1Collapsed = value;
                //
                base.Panel1Collapsed = value;
                if (this.m_Panel1Collapsed && this.m_Panel2Collapsed) { this.VisibleEx = false; }
                else
                {
                    if (!this.VisibleEx) { base.Panel2Collapsed = !base.Panel1Collapsed; this.VisibleEx = true; }
                    if (!value) { this.VisibleEx = true; }
                }
            }
        }

        [Browsable(false)]
        public new bool Panel2Collapsed//修改Panel2Collapsed属性（当Panel1不可见且Panel2不可见时该控件不可见，当控件不可见时Panel2可见则控件也可见，其它不变）
        {
            get
            {
                return this.m_Panel2Collapsed;
            }
            set
            {
                this.m_Panel2Collapsed = value;
                //
                base.Panel2Collapsed = value;
                if (this.m_Panel1Collapsed && this.m_Panel2Collapsed) { this.VisibleEx = false; }
                else
                {
                    if (!this.VisibleEx) { base.Panel1Collapsed = !base.Panel2Collapsed; this.VisibleEx = true; }
                    if (!value) { this.VisibleEx = true; }
                }
            }
        }

        [Browsable(false), DefaultValue(true)]
        public new bool Visible
        {
            get { return base.Visible; }
            set { base.Visible = value; }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new DockStyle Dock//DockStyle.Fill
        {
            get { return base.Dock; }
            set { base.Dock = DockStyle.Fill; }
        }

        #region 覆盖
        [Browsable(false)]
        public override Cursor Cursor//显示鼠标指针状态
        {
            get
            {
                if (!this.SplitterRectangle.Contains(this.PointToClient(MousePosition))) return base.Cursor;
                //
                switch (this.Orientation)
                {
                    case Orientation.Horizontal:
                        return Cursors.HSplit;
                    case  Orientation.Vertical:
                        return Cursors.VSplit;
                    default:
                        return base.Cursor;
                }
            }
            set
            {
                base.Cursor = value;
            }
        }

        private bool m_bIsMouseDown = false;         //记录鼠标按下状态
        private GISShare.Controls.WinForm.WFNew.SplitLineForm m_SplitLineForm = null;//分割线窗体
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (this.SplitterRectangle.Contains(e.Location))
            {
                if (this.m_SplitLineForm != null)
                {
                    this.m_SplitLineForm.Close();
                    this.m_SplitLineForm = null;
                }
                this.m_bIsMouseDown = true;
                this.m_SplitLineForm = new GISShare.Controls.WinForm.WFNew.SplitLineForm();//key
                this.m_SplitLineForm.Show
                    (
                    this.Orientation == Orientation.Horizontal ? DockStyle.Top : DockStyle.Left, 
                    new Rectangle(PointToScreen(this.SplitterRectangle.Location), this.SplitterRectangle.Size)
                    );
            }
            //base.OnMouseDown(e);
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (this.m_bIsMouseDown)
            {
                this.ShowSplitLine(this.m_SplitLineForm, e.Location);
            }
        }
        private void ShowSplitLine(Form splitterForm, Point location)//在鼠标运动时显示SplitLine
        {
            Point point = PointToScreen(location);
            switch (this.Orientation)
            {
                case Orientation.Horizontal:
                    int iH = location.Y - this.SplitterRectangle.Y;
                    if (iH > 0) 
                    {
                        iH = this.DisplayRectangle.Bottom - location.Y;
                        if (iH > this.Panel2MinSize) { splitterForm.Top = point.Y; }
                    }
                    else
                    {
                        iH = location.Y - this.DisplayRectangle.Top;
                        if (iH > this.Panel1MinSize) { splitterForm.Top = point.Y; }
                    }
                    break;
                case Orientation.Vertical:
                    int iW = location.X - this.SplitterRectangle.X;
                    if (iW > 0)
                    {
                        iW = this.DisplayRectangle.Right - location.X;
                        if (iW > this.Panel2MinSize) { splitterForm.Left = point.X; }
                    }
                    else
                    {
                        iW = location.X - this.DisplayRectangle.Left;
                        if (iW > this.Panel1MinSize) { splitterForm.Left = point.X; }
                    }
                    break;
            }
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            //base.OnMouseUp(e);
            //
            if (this.m_SplitLineForm != null)
            {
                this.m_SplitLineForm.Close();
                this.m_SplitLineForm = null;
            }
            if (!this.m_bIsMouseDown) return;
            this.m_bIsMouseDown = false;
            //
            this.SetSplitPanelSize(e.Location);
        }
        private void SetSplitPanelSize(Point location)
        {
            switch (this.Orientation)
            {
                case Orientation.Horizontal:
                    int iH = location.Y - this.DisplayRectangle.Y;
                    this.SplitterDistance = iH > this.Panel1MinSize ? iH : this.Panel1MinSize;
                    break;
                case Orientation.Vertical:
                    int iW = location.X - this.DisplayRectangle.X;
                    this.SplitterDistance = iW > this.Panel1MinSize ? iW : this.Panel1MinSize;
                    break;
            }
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            //
            if (this.VisibleEx) { ((ISetPanelNodeStateHelper)this).SetPanelNodeState(PanelNodeState.eShow); }
            else { ((ISetPanelNodeStateHelper)this).SetPanelNodeState(PanelNodeState.eClose); }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.DockPanelManager.DockPanelContainers.Contains(this))
                { this.DockPanelManager.DockPanelContainers.Remove(this); }
                //
                this.Panel1.ControlAdded -= new ControlEventHandler(Panel1_ControlAdded);
                this.Panel1.ControlRemoved -= new ControlEventHandler(Panel1_ControlRemoved);
                this.Panel2.ControlAdded -= new ControlEventHandler(Panel2_ControlAdded);
                this.Panel2.ControlRemoved -= new ControlEventHandler(Panel2_ControlRemoved);
            }
            //
            base.Dispose(disposing);
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            this.OnDraw(pevent);
            //
            base.OnPaint(pevent);
        }

        protected virtual void OnDraw(PaintEventArgs e)
        {
            WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonArea(new ObjectRenderEventArgs(e.Graphics, this, this.AreaRectangle));
        }
        #endregion

        //事件
        protected void OnOpened(EventArgs e)
        { if (Opened != null) { this.Opened(this, e); } }

        protected void OnClosed(EventArgs e)
        { if (Closed != null) { this.Closed(this, e); } }

        protected void OnBeforeVisibleExValueSeted(BoolValueChangedEventArgs e)
        { if (this.BeforeVisibleExValueSeted != null) this.BeforeVisibleExValueSeted(this, e); }

        protected void OnAfterVisibleExValueSeted(BoolValueChangedEventArgs e)
        { if (this.AfterVisibleExValueSeted != null) this.AfterVisibleExValueSeted(this, e); }
    }
}
