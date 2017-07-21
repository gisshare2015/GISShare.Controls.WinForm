using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew.DockPanel
{
    [ToolboxItem(false), Designer(typeof(GISShare.Controls.WinForm.WFNew.DockPanel.Design.BasePanelDesigner))]
    public class BasePanel : WFNew.AreaControl, WFNew.ITabPageItem, WFNew.ISetTabPageItemHelper, IBasePanel, IBasePanel2, IBottomNode, IPanelNode, ISetBasePanelHelper, ISetPanelNodeStateHelper
    {
        #region 私有变量
        private DockPanel m_DependDockPanel;                                //当前所在的DockPanel控件
        #endregion

        [Browsable(true), Description("面板激活状态改变事件"), Category("属性已更改")]
        public event BoolValueChangedEventHandler TabPageActiveChanged;

        #region WFNew.ITabPageItem
        private TabButtonBPItem m_TabButtonItem = null; //它所对应的TabButton
        [Browsable(false), Description("携带的TabButtonItem对象"), Category("关联")]
        public WFNew.ITabButtonItem pTabButtonItem
        {
            get { return m_TabButtonItem; }
        }
        #endregion

        #region ISetTabPageItemHelper
        void WFNew.ISetTabPageItemHelper.SetIsSelected(bool bIsSelected)
        {

            this.Visible = bIsSelected;
        }
        #endregion

        #region 构造函数
        public BasePanel()
            : base()
        { 
            base.Dock = DockStyle.Fill;
            this.m_TabButtonItem = new TabButtonBPItem(this);
            this.m_TabButtonItem.TabButtonActiveChanged += new GISShare.Controls.WinForm.BoolValueChangedEventHandler(TabButtonItem_TabButtonActiveChanged);
        }
        void TabButtonItem_TabButtonActiveChanged(object sender, GISShare.Controls.WinForm.BoolValueChangedEventArgs e)
        {
            this.OnTabPageActiveChanged(e);
            //
            if (e.NewValue) { ((ISetPanelNodeStateHelper)this).SetPanelNodeState(PanelNodeState.eShow); }
            else { ((ISetPanelNodeStateHelper)this).SetPanelNodeState(PanelNodeState.eHide); }
        }

        //public BasePanel(GISShare.Controls.Plugin.WFNew.DockPanel.IBasePanelP pBaseItemP)
        //    : this()
        //{
        //    //IPlugin
        //    this.Name = pBaseItemP.Name;
        //    //ISetEntityObject
        //    GISShare.Controls.Plugin.ISetEntityObject pSetEntityObject = pBaseItemP as GISShare.Controls.Plugin.ISetEntityObject;
        //    if (pSetEntityObject != null) pSetEntityObject.SetEntityObject(this);
        //    //IBaseItemP_
        //    this.Checked = pBaseItemP.Checked;
        //    this.Enabled = pBaseItemP.Enabled;
        //    this.Font = pBaseItemP.Font;
        //    this.ForeColor = pBaseItemP.ForeColor;
        //    //this.LockHeight = pBaseItemP.LockHeight;
        //    //this.LockWith = pBaseItemP.LockWith;
        //    this.Padding = pBaseItemP.Padding;
        //    this.Size = pBaseItemP.Size;
        //    this.Text = pBaseItemP.Text;
        //    //this.Visible = pBaseItemP.Visible;
        //    //this.Category = pBaseItemP.Category;
        //    this.MinimumSize = pBaseItemP.MinimumSize;
        //    //this.UsingViewOverflow = pBaseItemP.UsingViewOverflow;
        //    //IBasePanelP
        //    this.CanDockUp = pBaseItemP.CanDockUp;
        //    this.CanDockLeft = pBaseItemP.CanDockLeft;
        //    this.CanDockRight = pBaseItemP.CanDockRight;
        //    this.CanDockBottom = pBaseItemP.CanDockBottom;
        //    this.CanDockFill = pBaseItemP.CanDockFill;
        //    this.CanFloat = pBaseItemP.CanFloat;
        //    this.CanHide = pBaseItemP.CanHide;
        //    this.CanClose = pBaseItemP.CanClose;
        //    this.IsBasePanel = pBaseItemP.IsBasePanel;
        //    this.IsDocumentPanel = pBaseItemP.IsDocumentPanel;
        //    this.VisibleEx = pBaseItemP.VisibleEx;
        //    this.Image = pBaseItemP.Image;
        //    this.DockPanelFloatFormLocation = pBaseItemP.DockPanelFloatFormLocation;
        //    this.DockPanelFloatFormSize = pBaseItemP.DockPanelFloatFormSize;
        //    if (pBaseItemP.ChildControls != null)
        //    {
        //        for (int i = pBaseItemP.ChildControls.Length - 1; i >= 0; i--)
        //        {
        //            this.Controls.Add(pBaseItemP.ChildControls[i]);
        //        }
        //    }
        //}
        #endregion

        #region WFNew.IBaseItem
        public override object Clone()
        {
            BasePanel item = new BasePanel();
            return item;
        }

        [Browsable(true)]
        public override bool LockHeight
        {
            get { return false; }
        }

        [Browsable(true)]
        public override bool LockWith
        {
            get { return false; }
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

        [Browsable(false), 
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        Description("设置该控件的可视状态，用来替代Visible属性（请不要使用Visible属性）"), Category("状态")]
        public bool VisibleEx//设置该控件的可视状态，用来替代Visible属性（请不要使用Visible属性）
        {
            get
            {
                if (this.m_DependDockPanel == null) return false;
                if (this.m_DependDockPanel.VisibleEx)
                {
                    if (this.m_DependDockPanel.BasePanels.Contains(this)) return base.Visible;
                    else return false;
                }
                else { return false; }
            }
            set
            {
                bool bOldValue = this.VisibleEx;
                //if (this.BeforeVisibleExValueSeted != null) this.BeforeVisibleExValueSeted(this, new BoolValueChangedEventArgs(bOldValue, value));
                this.OnBeforeVisibleExValueSeted(new BoolValueChangedEventArgs(bOldValue, value));
                //
                if (value)//展现
                {
                    if (this.m_DependDockPanel != null)
                    {
                        //int index = -1;
                        //bool bOk = true;
                        this.m_DependDockPanel.BasePanels.Add(this);
                        this.m_DependDockPanel.SetSelectBasePanel(this);
                        this.m_DependDockPanel.VisibleEx = true;
                        //
                        ((ISetPanelNodeStateHelper)this).SetPanelNodeState(PanelNodeState.eShow);
                        //if (this.DependDockPanel.Parent == null) { bOk = this.DependDockPanel.ToDockPanelFloatForm(); }//重复故删除
                        //if (bOk && Opened != null) { this.Opened(this, new EventArgs()); }
                        //if (bOk && index <= 0) { this.OnOpened(this, new EventArgs()); }
                    }
                }
                else//关闭
                {
                    if (this.m_DependDockPanel != null)
                    {
                        if (this.m_DependDockPanel.BasePanels.Count > 1 &&
                            this.m_DependDockPanel.BasePanels.Contains(this))
                        {
                            //if (this.m_DependDockPanel.GetDockPanelContainerStyle() == DockPanelContainerStyle.eDockPanelHidePanel) //如果是 隐藏面板 则 关闭
                            //{ this.m_DependDockPanel.Close(); }
                            //else
                            //{ this.m_DependDockPanel.RemoveBasePanel(this); }
                            //
                            if (this.m_DependDockPanel.IsHideState) //如果是 隐藏面板 则 关闭
                            { this.m_DependDockPanel.Close(); }
                            else //{ this.m_DependDockPanel.RemoveBasePanel(this); }                           
                            { 
                              int index = this.m_DependDockPanel.BasePanels.IndexOf(this);
                              if (index < 0) { this.m_DependDockPanel.BasePanels.Remove(this); }
                              else if (index == 0) { this.m_DependDockPanel.BasePanelSelectedIndex = 1; }
                              else { this.m_DependDockPanel.BasePanelSelectedIndex = 0; }
                            }
                        }
                        else
                        { this.m_DependDockPanel.VisibleEx = false; }
                        //if (Closed != null) { this.Closed(this, new EventArgs()); }
                        //this.OnClosed(this, new EventArgs());
                        ((ISetPanelNodeStateHelper)this).SetPanelNodeState(PanelNodeState.eClose);
                    }
                }
                //
                //if (this.AfterVisibleExValueSeted != null) this.AfterVisibleExValueSeted(this, new BoolValueChangedEventArgs(bOldValue, value));
                this.OnAfterVisibleExValueSeted(new BoolValueChangedEventArgs(bOldValue, value));
            }
        }

        private bool m_CanDockUp = true;                                    //是否可以顶部停靠
        [Browsable(true), DefaultValue(true), Description("是否可以顶部停靠"), Category("状态")]
        public bool CanDockUp
        {
            get { return m_CanDockUp; }
            set { m_CanDockUp = value; }
        }

        private bool m_CanDockLeft = true;                                  //是否可以左边停靠
        [Browsable(true), DefaultValue(true), Description("是否可以左边停靠"), Category("状态")]
        public bool CanDockLeft
        {
            get { return m_CanDockLeft; }
            set { m_CanDockLeft = value; }
        }

        private bool m_CanDockRight = true;                                 //是否可以右边停靠
        [Browsable(true), DefaultValue(true), Description("是否可以右边停靠"), Category("状态")]
        public bool CanDockRight
        {
            get { return m_CanDockRight; }
            set { m_CanDockRight = value; }
        }

        private bool m_CanDockBottom = true;                                //是否可以底部停靠
        [Browsable(true), DefaultValue(true), Description("是否可以底部停靠"), Category("状态")]
        public bool CanDockBottom
        {
            get { return m_CanDockBottom; }
            set { m_CanDockBottom = value; }
        }

        private bool m_CanDockFill = true;                                  //是否可以填充
        [Browsable(true), DefaultValue(true), Description("是否可以填充"), Category("状态")]
        public bool CanDockFill
        {
            get { return m_CanDockFill; }
            set { m_CanDockFill = value; }
        }

        private bool m_CanFloat = true;                                     //是否可以浮动
        [Browsable(true), DefaultValue(true), Description("是否可以浮动"), Category("状态")]
        public bool CanFloat
        {
            get { return m_CanFloat; }
            set { m_CanFloat = value; }
        }

        bool m_CanHide = true;                                              //是否可以隐藏
        [Browsable(true), DefaultValue(true), Description("是否可以隐藏"), Category("状态")]
        public bool CanHide
        {
            get { return m_CanHide; }
            set { m_CanHide = value; }
        }

        bool m_CanClose = true;                                             //是否可以关闭
        [Browsable(true), DefaultValue(true), Description("是否可以关闭"), Category("状态")]
        public bool CanClose
        {
            get { return m_CanClose; }
            set { m_CanClose = value; }
        }

        bool m_IsBasePanel = true;                                          //是否为基础面板
        [Browsable(true), DefaultValue(true), Description("是否为基础面板"), Category("状态")]
        public bool IsBasePanel
        {
            get 
            {
                return (!m_IsBasePanel && !m_IsDocumentPanel) ? true : m_IsBasePanel; 
            }
            set { m_IsBasePanel = value; }
        }

        bool m_IsDocumentPanel = true;                                      //是否为文档面板
        [Browsable(true), DefaultValue(true), Description("是否为文档面板"), Category("状态")]
        public bool IsDocumentPanel
        {
            get { return m_IsDocumentPanel; }
            set { m_IsDocumentPanel = value; }
        }

        [Browsable(false), Description("记录面板类型"), Category("属性")]
        public BasePanelStyle eBasePanelStyle//记录面板类型
        { get { return BasePanelStyle.eBasePanel; } }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Description("记录DependDockPanel转化为停靠窗体后所在的坐标"), Category("布局")]
        public Point DockPanelFloatFormLocation//记录DependDockPanel转化为停靠窗体后所在的坐标
        {
            get
            {
                if (this.m_DependDockPanel == null) return new Point(360, 360);
                return this.m_DependDockPanel.DockPanelFloatFormLocation;
            }
            set { if (this.m_DependDockPanel != null)  this.m_DependDockPanel.DockPanelFloatFormLocation = value; }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Description("记录DependDockPanel转化为停靠窗体后所在的尺寸"), Category("布局")]
        public Size DockPanelFloatFormSize//记录DependDockPanel转化为停靠窗体后所在的尺寸
        {
            get
            {
                if (this.m_DependDockPanel == null) return new Size(260, 260);
                return this.m_DependDockPanel.DockPanelFloatFormSize;
            }
            set
            {
                if (this.m_DependDockPanel == null) return;
                this.m_DependDockPanel.DockPanelFloatFormSize = value;
            }
        }

        [Browsable(false), Description("浮动面板管理器"), Category("关联")]
        public DockPanelManager DockPanelManager//浮动面板管理器
        {
            get
            {
                if (this.m_DependDockPanel == null) return null;
                return this.m_DependDockPanel.DockPanelManager;
            }
        }

        [Browsable(false), Description("描述信息"), Category("属性")]
        public string Describe
        { get { return "【系统辅助管理】基础面板（BasePanel）：用来承载控件的容器，每个停靠面板（DockPanel）至少携带一个基础面板（BasePanel），相当于面板树的最后一个节点。"; } }

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
            //
            int index = this.m_DependDockPanel.BasePanels.IndexOf(this);
            if (index >= 0) { this.m_DependDockPanel.BasePanels.Remove(this); }
        }

        public bool ToDockPanelFloatForm()//转化为浮动窗体
        {
            if (!this.CanFloat)
            {
                GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show("无法实现停靠效果！停靠对象内存在不支持浮动的基础面板项。");
                return false;
            }
            //
            if (this.m_DependDockPanel == null || 
                this.DockPanelManager == null) return false;
            //
            this.LostFocusEx();//使其失去焦点 key
            //
            if (this.m_DependDockPanel.BasePanels.Contains(this))
            {
                if (this.m_DependDockPanel.BasePanels.Count >= 2)
                {
                    this.m_DependDockPanel.BasePanels.Remove(this);
                    DockPanel dockPanel = this.m_DependDockPanel.DockPanelManager.GetEmptyDockPanel();
                    if (dockPanel == null) return false;
                    dockPanel.VisibleEx = true;
                    dockPanel.BasePanels.Add(this);
                    return dockPanel.ToDockPanelFloatForm();
                }
                else if (this.m_DependDockPanel.BasePanels.Count == 1)
                {
                    return this.m_DependDockPanel.ToDockPanelFloatForm();
                }
            }
            else 
            {
                DockPanel dockPanel = this.m_DependDockPanel.DockPanelManager.GetEmptyDockPanel();
                if (dockPanel == null) return false;
                dockPanel.VisibleEx = true;
                dockPanel.BasePanels.Add(this);
                return dockPanel.ToDockPanelFloatForm();
            }
            return false;
        }

        public bool ToDockPanelFloatForm(Point moustPoint)//转化为浮动窗体
        {
            if (!this.CanFloat)
            {
                GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show("无法实现停靠效果！停靠对象内存在不支持浮动的基础面板项。");
                return false;
            }
            //
            if (this.m_DependDockPanel == null ||
                this.DockPanelManager == null) return false;
            //
            this.LostFocusEx();//使其失去焦点 key
            //
            if (this.m_DependDockPanel.BasePanels.Contains(this))
            {
                if (this.m_DependDockPanel.BasePanels.Count >= 2)
                {
                    this.m_DependDockPanel.BasePanels.Remove(this);
                    DockPanel dockPanel = this.m_DependDockPanel.DockPanelManager.GetEmptyDockPanel();
                    if (dockPanel == null) return false;
                    dockPanel.VisibleEx = true;
                    dockPanel.BasePanels.Add(this);
                    return dockPanel.ToDockPanelFloatForm( moustPoint);
                }
                else if (this.m_DependDockPanel.BasePanels.Count == 1)
                {
                    return this.m_DependDockPanel.ToDockPanelFloatForm(moustPoint);
                }
            }
            else
            {
                DockPanel dockPanel = this.m_DependDockPanel.DockPanelManager.GetEmptyDockPanel();
                if (dockPanel == null) return false;
                dockPanel.VisibleEx = true;
                dockPanel.BasePanels.Add(this);
                return dockPanel.ToDockPanelFloatForm( moustPoint);
            }
            return false;
        }
        #endregion

        #region IBasePanel2
        DockPanel IBasePanel2.TryGetDependDockPanel()
        {
            return this.m_DependDockPanel;
        }
        #endregion

        #region ISetBasePanelHelper
        void ISetBasePanelHelper.SetDependDockPanelActiveState()
        {
            if (this.m_DependDockPanel == null) return;
            ((ISetDockPanelHelper)this.m_DependDockPanel).SetActiveState(this.m_DependDockPanel);
        }
        void ISetBasePanelHelper.SetDependDockPanel(DockPanel dockPanel)//设置DockPanel，由系统管理（添加到BasePanelCollection时，调用该函数）
        {
            this.m_DependDockPanel = dockPanel;
        }
        #endregion

        #region IPanelNode
        [Browsable(true), Description("面板节点状态改变事件"), Category("属性已更改")]
        public event PanelNodeStateChangedEventHandler PanelNodeStateChanged; //面板节点状态改变事件

        private PanelNodeState m_ePanelNodeState = PanelNodeState.eRemove;  //记录面板节点状态
        [Browsable(false), Description("记录面板节点状态"), Category("状态")]
        public PanelNodeState ePanelNodeState
        {
            get { return m_ePanelNodeState; }
        }

        [Browsable(false), Description("获取节点类型"), Category("属性")]
        public PanelNodeStyle ePanelNodeStyle
        { get { return PanelNodeStyle.eBasePanel; } }
        #endregion

        #region IBottomNode
        [Browsable(false), Description("获取节点类型"), Category("属性")]
        public NodeStyle eNodeStyle//获取节点类型
        { get { return NodeStyle.eBottomNode; } }

        [Browsable(false), Description("获取其父节点"), Category("关联")]
        public IBaseNode ParentNode//获取其父节点
        {
            get { return this.m_DependDockPanel as IBaseNode; }
        }
        #endregion

        #region ISetPanelNodeStateHelper
        void ISetPanelNodeStateHelper.SetPanelNodeState(PanelNodeState panelNodeState)//设置面板节点状态并激发相应事件
        {
            if (this.m_ePanelNodeState == panelNodeState) return;
            PanelNodeStateChangedEventArgs e = new PanelNodeStateChangedEventArgs(this.m_ePanelNodeState, panelNodeState, this);
            this.m_ePanelNodeState = panelNodeState;
            this.OnPanelNodeStateChanged(e);
            //
            switch (this.m_ePanelNodeState)
            {
                case PanelNodeState.eShow:
                    this.OnOpened(new EventArgs());
                    break;
                case PanelNodeState.eClose:
                case PanelNodeState.eRemove:
                    this.OnClosed(new EventArgs());
                    break;
            }
        }
        #endregion

        [Browsable(false), DefaultValue(DockStyle.Fill), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override DockStyle Dock
        {
            get
            {
                return base.Dock;
            }
            set
            {
                base.Dock = DockStyle.Fill;
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            if (this.m_DependDockPanel != null)
            {
                this.m_DependDockPanel.Invalidate(this.m_DependDockPanel.ItemsRectangle);
            }
            base.OnTextChanged(e);
        }

        #region 属性
        [Browsable(true), Description("图片"), Category("外观")]
        public Image Image
        {
            get
            {
                if (this.pTabButtonItem != null) return this.pTabButtonItem.Image;
                return null;
            }
            set
            {
                if (this.pTabButtonItem != null) this.pTabButtonItem.Image = value;
            }
        }

        [Browsable(false), DefaultValue(true), Category("状态")]
        public new bool Visible//已被VisibleEx替代（请不要使用Visible属性）
        {
            get { return base.Visible; }
            set { base.Visible = value; }
        }

        [Browsable(false), Description("该TabPage是否被选中（TabPage与TabButton 共用 IsSelected）"), Category("状态")]
        public bool IsSelected//该TabPage是否被选中（TabPage与TabButton 共用 IsSelected）
        {
            get
            {
                return this.pTabButtonItem.IsSelected;
            }
        }
        #endregion

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            //
            if (this.VisibleEx) { ((ISetPanelNodeStateHelper)this).SetPanelNodeState(PanelNodeState.eShow); }
            else { ((ISetPanelNodeStateHelper)this).SetPanelNodeState(PanelNodeState.eClose); }
        }

        //事件
        protected virtual void OnTabPageActiveChanged(BoolValueChangedEventArgs e)//设置面板节点状态并激发相应事件
        {
            if (this.TabPageActiveChanged != null) { this.TabPageActiveChanged(this, e); }
        }

        protected virtual void OnOpened(EventArgs e)
        { if (Opened != null) { this.Opened(this, e); } }

        protected virtual void OnClosed(EventArgs e)
        { if (Closed != null) { this.Closed(this, e); } }

        protected virtual void OnBeforeVisibleExValueSeted(BoolValueChangedEventArgs e)
        { if (this.BeforeVisibleExValueSeted != null) this.BeforeVisibleExValueSeted(this, e); }

        protected virtual void OnAfterVisibleExValueSeted(BoolValueChangedEventArgs e)
        { if (this.AfterVisibleExValueSeted != null) this.AfterVisibleExValueSeted(this, e); }

        protected virtual void OnPanelNodeStateChanged(PanelNodeStateChangedEventArgs e)
        { if (this.PanelNodeStateChanged != null) this.PanelNodeStateChanged(this, e); }

        //
        //
        //

        class TabButtonBPItem : WFNew.TabButtonItem
        {
            public TabButtonBPItem(BasePanel basePanel)
                : base(basePanel) { }

            public override bool ShowCloseButton
            {
                get
                {
                    IBasePanel pBasePanel = this.pTabPageItem as IBasePanel;
                    return pBasePanel != null ? pBasePanel.CanClose && base.ShowCloseButton : base.ShowCloseButton;
                }
                set
                {
                    base.ShowCloseButton = value;
                }
            }

            protected override void OnTabButtonMouseUp(MouseEventArgs e)
            {
                base.OnTabButtonMouseUp(e);
                //
                this.Selected();
            }

            protected override void OnCloseButtonMouseUp(MouseEventArgs e)
            {
                base.OnCloseButtonMouseUp(e);
                //
                this.RemoveSelf();
            }
        }
    }
}
