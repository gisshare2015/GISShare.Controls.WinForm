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
    [ToolboxItem(false)]
    sealed class HoldDockPanel : WFNew.AreaControl, IDockPanel
    {
        private DockPanel m_DockPanel = null;                            //记录其所对应的实体项
        private HideAreaTabButtonGroupItem m_HideAreaTabButtonGroupItem = null;  //记录所携带的隐藏按钮组

        public HoldDockPanel(DockPanel dockPanel)
        {
            this.m_DockPanel = dockPanel;
            //
            base.Enabled = false;
            base.Visible = false;
            base.Dock = DockStyle.Fill;
        }

        #region WFNew.IBaseItem
        public override bool LockHeight
        {
            get { return false; }
        }

        public override bool LockWith
        {
            get { return false; }
        }

        public override object Clone()
        {
            return new HoldDockPanel(this.m_DockPanel);
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

        [Browsable(false), Description("唯一编号，用来记录布局信息的（由系统管理）"), Category("状态")]
        public new int RecordID//唯一编号，用来记录布局信息的（由系统管理），它记录的是自身所对应的实体项DockPanel的RecordID
        {
            get { return this.m_DockPanel.RecordID; }
        }

        [Browsable(false), Description("是否可以顶部停靠"), Category("状态")]
        public bool CanDockUp//获取实体项DockPanel的CanDockUp属性
        {
            get
            {
                return this.m_DockPanel.CanDockUp;
            }
        }

        [Browsable(false), Description("是否可以左边停靠"), Category("状态")]
        public bool CanDockLeft//获取实体项DockPanel的CanDockLeft属性
        {
            get
            {
                return this.m_DockPanel.CanDockLeft;
            }
        }

        [Browsable(false), Description("是否可以右边停靠"), Category("状态")]
        public bool CanDockRight//获取实体项DockPanel的CanDockRight属性
        {
            get
            {
                return this.m_DockPanel.CanDockRight;
            }
        }

        [Browsable(false), Description("是否可以底部停靠"), Category("状态")]
        public bool CanDockBottom//获取实体项DockPanel的CanDockBottom属性
        {
            get
            {
                return this.m_DockPanel.CanDockBottom;
            }
        }

        [Browsable(false), Description("是否可以填充"), Category("状态")]
        public bool CanDockFill//获取实体项DockPanel的CanDockFill属性
        {
            get
            {
                return this.m_DockPanel.CanDockFill;
            }
        }

        [Browsable(false), Description("是否可以浮动"), Category("状态")]
        public bool CanFloat//获取实体项DockPanel的CanFloat属性
        {
            get
            {
                return this.m_DockPanel.CanFloat;
            }
        }

        [Browsable(false), Description("是否可以隐藏"), Category("状态")]
        public bool CanHide//获取实体项DockPanel的CanHide属性
        {
            get
            {
                return this.m_DockPanel.CanHide;
            }
        }

        [Browsable(false), Description("是否可以关闭"), Category("状态")]
        public bool CanClose//获取实体项DockPanel的CanClose属性
        {
            get
            {
                return this.m_DockPanel.CanClose;
            }
        }

        [Browsable(false), Description("是否为基础面板"), Category("状态")]
        public bool IsBasePanel//获取实体项DockPanel的IsBasePanel属性
        {
            get
            {
                return this.m_DockPanel.IsBasePanel;
            }
        }

        [Browsable(false), Description("是否为文档面板"), Category("状态")]
        public bool IsDocumentPanel//获取实体项DockPanel的CanFloat属性
        {
            get
            {
                return this.m_DockPanel.IsDocumentPanel;
            }
        }

        [Browsable(false), Description("记录面板类型"), Category("属性")]
        public BasePanelStyle eBasePanelStyle//记录面板类型
        { get { return BasePanelStyle.eHoldDockPanel; } }

        [Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        Description("设置该控件的可视状态，用来替代Visible属性（请不要使用Visible属性）"), Category("状态")]
        public bool VisibleEx//设置该控件的可视状态，用来替代Visible属性（请不要使用Visible属性），它记录的是自身所对应的实体项DockPanel的VisibleEx
        {
            get { return false; }
            set
            {
                this.OnBeforeVisibleExValueSeted(new BoolValueChangedEventArgs(false, false));
                this.m_DockPanel.VisibleEx = value;
                this.OnAfterVisibleExValueSeted(new BoolValueChangedEventArgs(false, false));
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Description("记录DockPanel转化为停靠窗体后所在的坐标"), Category("布局")]
        public Point DockPanelFloatFormLocation//记录DockPanel转化为停靠窗体后所在的坐标，它记录的是自身所对应的实体项DockPanel的DockPanelFloatFormLocation
        {
            get { return this.m_DockPanel.DockPanelFloatFormLocation; }
            set { this.m_DockPanel.DockPanelFloatFormLocation = value; }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Description("记录DockPanel转化为停靠窗体后所在的坐标"), Category("布局")]
        public Size DockPanelFloatFormSize//记录DockPanel转化为停靠窗体后所在的坐标，它记录的是自身所对应的实体项DockPanel的DockPanelFloatFormSize
        {
            get { return this.m_DockPanel.DockPanelFloatFormSize; }
            set { this.m_DockPanel.DockPanelFloatFormSize = value; }
        }

        [Browsable(false), Description("浮动面板管理器"), Category("关联")]
        public DockPanelManager DockPanelManager
        {
            get { return this.m_DockPanel.DockPanelManager; }
        }

        [Browsable(false), Description("描述信息"), Category("属性")]
        public string Describe
        { get { return "【由系统自动管理】占位面板（HoldDockPanel）：用来记录停靠面板（DockPanel）隐藏前所在的位置，以便还原后使其展现在原先的相对位置，相当于面板树的最后一个二叉树节点（排除基础面板（BasePanel）后）。帮扶对象名：" + this.m_DockPanel.Name; } }

        public void LostFocusEx()//使控件失去焦点（每次移除前需要调用盖函数）
        {
            if (!this.ContainsFocus) return;
            base.Enabled = false;
        }

        public void Open()//展现，展现实体项DockPanel
        {
            this.m_DockPanel.Open();
            this.OnOpened(new EventArgs());
            //GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show("HoldDockPanel只是一个DockPanel的一个站位项，该函数无法实现！");
        }

        public void Close()//关闭，关闭实体项DockPanel
        {
            this.m_DockPanel.Close();
            this.OnClosed(new EventArgs());
            //GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show("HoldDockPanel只是一个DockPanel的一个站位项，该函数无法实现！");
        }

        public bool ToDockPanelFloatForm()//转化为浮动窗体，将实体项DockPanel转化为浮动窗体
        {
            return this.m_DockPanel.ToDockPanelFloatForm();
            //GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show("HoldDockPanel只是一个DockPanel的一个站位项，该函数无法实现！");
        }

        public bool ToDockPanelFloatForm(Point moustPoint)//转化为浮动窗体，将实体项DockPanel转化为浮动窗体
        {
            return this.m_DockPanel.ToDockPanelFloatForm(moustPoint);
            //GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show("HoldDockPanel只是一个DockPanel的一个站位项，该函数无法实现！");
        }
        #endregion

        #region IDock
        bool IDock.ToDockArea(bool bInteral, DockStyle eDockStyle)//使实体项停靠
        {
            return this.m_DockPanel.ToDockArea(bInteral, eDockStyle);
        }

        bool IDock.ToDockArea(bool bInteral, DockStyle eDockStyle, Point location)//使实体项停靠
        {
            return this.m_DockPanel.ToDockArea(bInteral, eDockStyle, location);
        }
        #endregion

        #region IDockPanel
        [Browsable(false), Description("父控件"), Category("关联")]
        public Control ParentControl 
        { get { return base.Parent; } }

        [Browsable(false), Description("自身IDockPanel的类型"), Category("状态")]
        public DockPanelStyle eDockPanelStyle 
        { get { return DockPanelStyle.eHoldDockPanel; } }
        
        /// <summary>
        /// 获取停靠面板（DockPanel）的可视化状态（只要其父控件存在不可见状态，则视其为不可见，反之亦然。）
        /// </summary>
        /// <returns></returns>
        public bool GetVisible()//只要其父控件存在不可见状态，则视其为不可见
        {
            if (this.Parent != null && !this.Parent.IsDisposed)
            {
                if (this.Parent is IDockArea) { return this.Parent.Visible && base.Visible; }
                //
                if (this.Parent.Visible) { return this.GetVisible_DG(this.Parent) && base.Visible; }
                else { return false; }
            }
            else
            {
                return false;
            }
        }
        private bool GetVisible_DG(Control ctr)//递归 查询可视状态
        {
            if (ctr.Parent != null)
            {
                if (ctr.Parent is IDockArea) { return ctr.Parent.Visible && base.Visible; }
                //
                if (ctr.Parent.Visible) { return this.GetVisible_DG(ctr.Parent) && base.Visible; }
                else { return false; }
            }
            else
            {
                return false;
            }
        }

        public void RemoveFromParent()
        {
            if (this.Parent != null)
            {
                this.Parent.Controls.Remove(this);
            }
        }

        public void ClearBasePanels()//清空实体项的BasePanels
        {
            this.m_DockPanel.ClearBasePanels();
            //GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show("HoldDockPanel只是一个DockPanel的一个站位项，该函数无法实现！");
        }

        public BasePanel[] GetBasePanels()//获取实体项的BasePanels
        {
            return this.m_DockPanel.GetBasePanels();
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

        public DockAreaStyle GetDockAreaStyle()//获取终级停靠区
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
        private DockAreaStyle GetDockAreaStyle(Control ctr)//递归 查询获取终级停靠区
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
        private DockAreaStyle GetDockAreaStyle(Control ctr, ref DockStyle eDockStyle)//递归 查询获取终级停靠区
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
            ref bool bIsBasePanel, ref bool bIsDocumentPanel)//获取实体项DockPanel停靠许可
        {
            this.m_DockPanel.GetDockLicense(ref bCanDockUp, ref bCanDockLeft, ref  bCanDockRight, ref  bCanDockBottom, ref bCanDockFill,
                ref bCanFloat, ref bCanHide, ref bCanClose,
                ref bIsBasePanel, ref bIsDocumentPanel);
        }

        bool IDockPanel.AddDockPanel(IDockPanel pDockPanel, DockStyle eDockStyle)
        {
            return this.m_DockPanel.AddDockPanel(pDockPanel, eDockStyle);
        }
        #endregion

        [Browsable(false), DefaultValue(DockStyle.Fill)]
        public new DockStyle Dock
        {
            get { return base.Dock; }
            set { base.Dock = DockStyle.Fill; }
        }

        [Browsable(false), DefaultValue(false)]
        public new bool Visible
        {
            get { return base.Visible; }
            set { base.Visible = false; }
        }

        [Browsable(false), DefaultValue(false)]
        public new bool Enabled
        {
            get { return base.Enabled; }
            set { base.Enabled = false; }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.m_DockPanel = null;
                this.m_HideAreaTabButtonGroupItem.Dispose();
                this.m_HideAreaTabButtonGroupItem = null;
            }
            //
            base.Dispose(disposing);
        }

        [Browsable(false)]
        public DockPanel DockPanel//其所对应的实体项
        {
            get { return m_DockPanel; }
        }

        [Browsable(false)]
        internal HideAreaTabButtonGroupItem HideAreaTabButtonGroupItem//记录所携带的隐藏按钮组
        {
            get { return m_HideAreaTabButtonGroupItem; }
        }

        public void HoldAndHide()//占位并隐藏实体项
        {
            //DockPanel不是隐藏状态、隐藏区HideAreaTabButtonGroupItem已存在、DockPanel的停靠区不是DockPanelDockArea   跳出
            if (!this.m_DockPanel.IsHideState || 
                this.HideAreaTabButtonGroupItem != null ||
                this.m_DockPanel.GetDockAreaStyle() != DockAreaStyle.eDockPanelDockArea) 
                return;
            //
            //
            //
            this.m_HideAreaTabButtonGroupItem = new HideAreaTabButtonGroupItem(this.m_DockPanel);//创建停靠区内的隐藏选项卡组 key
            //
            //从父容器中移除 -> 加入到DockPanel的父容器中起占位作用   顺序不能变
            this.LostFocusEx();
            this.RemoveFromParent();
            this.m_DockPanel.Parent.Controls.Add(this);
            //使DockPanel失去焦点 -> 从父容器中移除   顺序不能变
            this.m_DockPanel.LostFocusEx();//使其失去焦点 key
            this.m_DockPanel.RemoveFromParent();
            //设置VisibleEx属性 激发相关事件
            this.VisibleEx = false;//key false
        }

        public void CancelHoldeAndShow()//取消占位并展现隐藏的实体项
        {
            //DockPanel是隐藏状态、隐藏区HideAreaTabButtonGroupItem不存在、DockPanel的无停靠区    跳出
            if (this.m_DockPanel.IsHideState ||
                this.m_HideAreaTabButtonGroupItem == null ||
                this.m_DockPanel.GetDockAreaStyle() != DockAreaStyle.eNone) 
                return;
            //
            //
            //
            //使DockPanel失去焦点 -> 从父容器中移除 -> 加入到占位面板HoldDockPanel的父容器中起还原作用  顺序不能变
            this.m_DockPanel.LostFocusEx();//使其失去焦点 key
            this.m_DockPanel.RemoveFromParent();
            this.Parent.Controls.Add(this.m_DockPanel);
            //从父容器中移除占位面板HoldDockPanel -> 移除停靠区内的隐藏选项卡组
            this.LostFocusEx();
            this.RemoveFromParent();
            this.m_HideAreaTabButtonGroupItem.DockPanelHideArea.BaseItems.Remove(this.m_HideAreaTabButtonGroupItem);
            //设置DockPanel的VisibleEx属性 激发相关事件
            this.m_DockPanel.VisibleEx = true;
        }

        //事件
        internal void OnOpened( EventArgs e)
        { if (Opened != null) { this.Opened(this, e); } }

        internal void OnClosed( EventArgs e)
        { if (Closed != null) { this.Closed(this, e); } }

        internal void OnBeforeVisibleExValueSeted( BoolValueChangedEventArgs e)
        { if (this.BeforeVisibleExValueSeted != null) this.BeforeVisibleExValueSeted(this, e); }

        internal void OnAfterVisibleExValueSeted( BoolValueChangedEventArgs e)
        { if (this.AfterVisibleExValueSeted != null) this.AfterVisibleExValueSeted(this, e); }
    }
}
