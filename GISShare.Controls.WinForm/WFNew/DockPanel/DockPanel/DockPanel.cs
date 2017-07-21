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
    [ToolboxItem(false), Designer(typeof(GISShare.Controls.WinForm.WFNew.DockPanel.Design.DockPanelDesigner))]
    public class DockPanel : WFNew.AreaControl, 
        WFNew.ICollectionItem, WFNew.ICollectionItem2, WFNew.ICollectionItem3, WFNew.IUICollectionItem, 
        WFNew.ITabControl, WFNew.ITabControlHelper, 
        IDockPanel, IDockPanel2, 
        IMultipleNode, IPanelNode, 
        ISetDockPanelManagerHelper, ISetDockPanelHelper, ISetPanelNodeStateHelper
    {
        private const int CRT_DOCKPANELBORBERSPACE = 1;//17
        private const int CRT_CAPTIONHEIGHT = 19;//17
        private const int CRT_TABBUTTONCONTAINERHEIGHT = 21;
        
        #region 私有变量
        private DockPanelButtonStackItemEx m_DockPanelButtonStackItemEx;
        private TabButtonContainerDPItem m_TabButtonContainerDPItem;                         //TabButtonContainerDPItem组件
        private WFNew.BaseItemCollection m_BaseItemCollection;
        //
        private HoldDockPanel m_HoldDockPanel = null;            //记录所对应的停靠面板的占位面板
        private BasePanelCollection m_BasePanelCollection;                 //基础面板收集器
        //
        private Point m_DockPanelFloatFormLocation = new Point(360, 360);  //记录DockPanel转化为停靠窗体后所在的坐标
        private Size m_DockPanelFromSize = new Size(260, 260);             //记录DockPanel转化为停靠窗体后所在的坐标
        //
        private DockPanelManager m_DockPanelManager = null;                //记录其所在的浮动面板管理器
        //
        private SuperToolTip m_SuperToolTip;
        #endregion

        #region 构造函数
        public DockPanel()
            : base()
        {
            this.m_BaseItemCollection = new GISShare.Controls.WinForm.WFNew.BaseItemCollection(this);
            this.m_DockPanelButtonStackItemEx = new DockPanelButtonStackItemEx();
            this.m_TabButtonContainerDPItem = new TabButtonContainerDPItem();
            this.m_TabButtonContainerDPItem.CanExchangeItem = true;
            this.m_TabButtonContainerDPItem.TabAlignment = TabAlignment.Bottom;
            this.m_TabButtonContainerDPItem.TabButtonItemSelectedIndexChanged += new GISShare.Controls.WinForm.IntValueChangedHandler(TabButtonContainerDPItem_TabButtonItemSelectedIndexChanged);
            this.m_BaseItemCollection.Add(this.m_DockPanelButtonStackItemEx);
            this.m_BaseItemCollection.Add(this.m_TabButtonContainerDPItem);
            ((WFNew.ILockCollectionHelper)this.m_BaseItemCollection).SetLocked(true);
            //
            this.Name = "DockPanel";
            this.Dock = DockStyle.Fill;
            this.Size = new Size(260, 260);
            base.Visible = true;
            //
            this.m_BasePanelCollection = new BasePanelCollection(this);
            //
            //
            //
            this.m_SuperToolTip = new SuperToolTip();
            this.m_SuperToolTip.OffsetX = 0;
            this.m_SuperToolTip.OffsetY = 12;
            foreach (BaseItem one in this.m_DockPanelButtonStackItemEx.BaseItems)
            {
                this.m_SuperToolTip.SetToolTip(one);
            }
            BaseItem[] baseItemList = ((IGetPartItemHelper)this.m_TabButtonContainerDPItem).GetPartItems();
            foreach (BaseItem one in baseItemList)
            {
                this.m_SuperToolTip.SetToolTip(one);
            }
        }
        void TabButtonContainerDPItem_TabButtonItemSelectedIndexChanged(object sender, GISShare.Controls.WinForm.IntValueChangedEventArgs e)
        {
            this.Invalidate(this.CaptionRectangle);
        }

        //public DockPanel(GISShare.Controls.Plugin.WFNew.DockPanel.IDockPanelP pBaseItemP)
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
        //    //IDockPanelP
        //    this.VisibleEx = pBaseItemP.VisibleEx;
        //    this.DockPanelFloatFormLocation = pBaseItemP.DockPanelFloatFormLocation;
        //    this.DockPanelFloatFormSize = pBaseItemP.DockPanelFloatFormSize;
        //    this.BasePanelSelectedIndex = pBaseItemP.BasePanelSelectedIndex;
        //}
        #endregion

        #region WFNew.IBaseItem
        public override object Clone()
        {
            return new DockPanel();
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

        #region  WFNew.ICollectionItem
        [Browsable(false), Description("其所携带的子项集合中是否存在可见项（与此类无关）"), Category("状态")]
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

        /// <summary>
        /// 一个零散的组建集合，它是锁定的无法移除和添加，没有需要请不要修改内部成员属性以防出现意外情况
        /// </summary>
        [Browsable(false), Description("其携带的子项（一个零散的组建集合，它是锁定的无法移除和添加，没有需要请不要修改内部成员属性以防出现意外情况）"), Category("子项")]
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

        #region ICollectionItem3
        public IBaseItem GetBaseItem2(string strName)
        {
            return null;
        }
        #endregion

        #region WFNew.IUICollectionItem
        Size WFNew.IUICollectionItem.GetIdealSize(Graphics g)
        {
            return this.Size;
        }
        #endregion

        #region WFNew.ITabControl
        [Browsable(false), Description("TabButtonContainerItem矩形框"), Category("布局")]
        public Rectangle TabButtonContainerRectangle
        {
            get
            {
                Rectangle rectangle = base.DisplayRectangle;
                if (this.GetDockAreaStyle() != DockAreaStyle.eDocumentDockArea)
                {
                    rectangle = new Rectangle
                        (
                        CRT_DOCKPANELBORBERSPACE, 
                        CRT_DOCKPANELBORBERSPACE + CRT_CAPTIONHEIGHT, 
                        rectangle.Width - 2 * CRT_DOCKPANELBORBERSPACE, 
                        rectangle.Height - CRT_CAPTIONHEIGHT - 2 * CRT_DOCKPANELBORBERSPACE
                        );
                }
                //
                switch (this.m_TabButtonContainerDPItem.TabAlignment)
                {
                    case TabAlignment.Top:
                        return new Rectangle(rectangle.Left, rectangle.Top, rectangle.Width, CRT_TABBUTTONCONTAINERHEIGHT);
                    case TabAlignment.Bottom:
                        return new Rectangle(rectangle.Left, rectangle.Bottom - CRT_TABBUTTONCONTAINERHEIGHT, rectangle.Width, CRT_TABBUTTONCONTAINERHEIGHT);
                    case TabAlignment.Left:
                        return new Rectangle(rectangle.Left, rectangle.Top, CRT_TABBUTTONCONTAINERHEIGHT, rectangle.Height);
                    case TabAlignment.Right:
                        return new Rectangle(rectangle.Right - CRT_TABBUTTONCONTAINERHEIGHT, rectangle.Top, CRT_TABBUTTONCONTAINERHEIGHT, rectangle.Height);
                    default:
                        return rectangle;
                }
            }
        }

        [Browsable(false), DefaultValue(true), Description("可交换TabPage"), Category("状态")]
        bool WFNew.ITabControl.CanExchangeItem
        {
            get { return this.m_TabButtonContainerDPItem.CanExchangeItem; }
            set {  }
        }

        [Browsable(false), DefaultValue(true), Description("显示关闭按钮"), Category("布局")]
        bool WFNew.ITabControl.UsingCloseTabButton
        {
            get
            {
                return this.m_TabButtonContainerDPItem.UsingCloseTabButton; 
            }
            set { }
        }

        [Browsable(false), DefaultValue(true), Description("是否自动显示"), Category("布局")]
        bool WFNew.ITabControl.AutoVisibleTabButton//是否自动显示TabButtonContainerDPItem
        {
            get { return this.m_TabButtonContainerDPItem.AutoVisible; }
            set { }
        }

        [Browsable(false), DefaultValue(false), Description("当通过下拉菜单选中的表头隐藏时是否自动置顶显示"), Category("布局")]
        bool WFNew.ITabControl.AutoShowOverflowTabButton
        {
            get { return this.m_TabButtonContainerDPItem.AutoShowOverflowTabButton; }
            set { }
        }

        [Browsable(false), Description("表头类型"), Category("布局")]
        WFNew.TabButtonContainerStyle WFNew.ITabControl.eTabButtonContainerStyle
        {
            get { return this.m_TabButtonContainerDPItem.eTabButtonContainerStyle; }
            set {  }
        }

        [Browsable(false), Description("存在溢出项时PN调节按钮的布局方式"), Category("布局")]
        WFNew.PNLayoutStyle WFNew.ITabControl.ePNLayoutStyle
        {
            get { return this.m_TabButtonContainerDPItem.ePNLayoutStyle; }
            set {  }
        }

        [Browsable(false), DefaultValue(TabAlignment.Bottom), Description("表头选项卡的位置"), Category("布局")]
        TabAlignment WFNew.ITabControl.TabAlignment//表头选项卡的位置
        {
            get { return this.m_TabButtonContainerDPItem.TabAlignment; }
            set {  }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), Description("当前选中的索引"), Category("状态")]
        int WFNew.ITabControl.TabPageSelectedIndex//当前选中的索引（其实质就是TabButton索引）
        {
            get { return this.BasePanelSelectedIndex; }
            set { this.BasePanelSelectedIndex = value; }
        }

        [Browsable(false), Description("选中的TabPage"), Category("状态")]
        WFNew.ITabPageItem WFNew.ITabControl.SelectedTabPage//当前选中的TabPage
        {
            get
            {
                return this.SelectedBasePanel;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Bindable(true), Localizable(true), Description("TabPage收集器"), Category("集合")]
        WFNew.TabPageCollection WFNew.ITabControl.TabPages//TabPage收集器
        {
            get { return this.BasePanels; }
        }

        void WFNew.ITabControl.AddTabPage(WFNew.ITabPageItem pTabPageItem)
        {
            this.BasePanels.Add(pTabPageItem);
        }

        void WFNew.ITabControl.RemoveTabPage(WFNew.ITabPageItem pTabPageItem)
        {
            this.BasePanels.Remove(pTabPageItem);
            if (this.BasePanels.Count < 1)
            {
                ((IDockPanel)this).RemoveFromParent();
            }
        }

        bool WFNew.ITabControl.SetSelectTabPage(WFNew.ITabPageItem pTabPageItem)
        {
            return this.SetSelectBasePanel(pTabPageItem as BasePanel);
        }
        #endregion

        #region WFNew.ITabControlHelper
        IList WFNew.ITabControlHelper.TabPageList
        {
            get { return this.Controls; }
        }

        WFNew.BaseItemCollection WFNew.ITabControlHelper.TabButtonItemCollection
        {
            get { return this.m_TabButtonContainerDPItem.BaseItems; }
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
                foreach (IBasePanel one in this.BasePanels)
                {
                    if (one == null || one.CanDockUp) continue;
                    return false;
                }
                return true;
            }
        }

        [Browsable(false), Description("是否可以左边停靠"), Category("状态")]
        public bool CanDockLeft
        {
            get
            {
                foreach (IBasePanel one in this.BasePanels)
                {
                    if (one == null || one.CanDockLeft) continue;
                    return false;
                }
                return true;
            }
        }

        [Browsable(false), Description("是否可以右边停靠"), Category("状态")]
        public bool CanDockRight
        {
            get
            {
                foreach (IBasePanel one in this.BasePanels)
                {
                    if (one == null || one.CanDockRight) continue;
                    return false;
                }
                return true;
            }
        }

        [Browsable(false), Description("是否可以底部停靠"), Category("状态")]
        public bool CanDockBottom
        {
            get
            {
                foreach (IBasePanel one in this.BasePanels)
                {
                    if (one == null || one.CanDockBottom) continue;
                    return false;
                }
                return true;
            }
        }

        [Browsable(false),  Description("是否可以填充"), Category("状态")]
        public bool CanDockFill
        {
            get
            {
                foreach (IBasePanel one in this.BasePanels)
                {
                    if (one == null || one.CanDockFill) continue;
                    return false;
                }
                return true;
            }
        }

        [Browsable(false),  Description("是否可以浮动"), Category("状态")]
        public bool CanFloat
        {
            get
            {
                foreach (IBasePanel one in this.BasePanels)
                {
                    if (one == null || one.CanFloat) continue;
                    return false;
                }
                return true;
            }
        }

        [Browsable(false),  Description("是否可以隐藏"), Category("状态")]
        public bool CanHide
        {
            get
            {
                foreach (IBasePanel one in this.BasePanels)
                {
                    if (one == null || one.CanHide) continue;
                    return false;
                }
                return true;
            }
        }

        [Browsable(false),  Description("是否可以关闭"), Category("状态")]
        public bool CanClose
        {
            get
            {
                foreach (IBasePanel one in this.BasePanels)
                {
                    if (one == null || one.CanClose) continue;
                    return false;
                }
                return true;
            }
        }

        [Browsable(false),  Description("是否为基础面板"), Category("状态")]
        public bool IsBasePanel
        {
            get
            {
                foreach (IBasePanel one in this.BasePanels)
                {
                    if (one == null || one.IsBasePanel) continue;
                    return false;
                }
                return true;
            }
        }

        [Browsable(false),   Description("是否为文档面板"), Category("状态")]
        public bool IsDocumentPanel
        {
            get
            {
                foreach (IBasePanel one in this.BasePanels)
                {
                    if (one == null || one.IsDocumentPanel) continue;
                    return false;
                }
                return true;
            }
        }

        [Browsable(false), Description("记录面板类型"), Category("属性")]
        public BasePanelStyle eBasePanelStyle//记录面板类型
        { get { return BasePanelStyle.eDockPanel; } }

        [Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        Description("设置该控件的可视状态，用来替代Visible属性（请不要使用Visible属性）"), Category("状态")]
        public bool VisibleEx//设置该控件的可视状态，用来替代Visible属性（请不要使用Visible属性）
        {
            get { return GetVisible(); }
            set
            {
                if (this == null || this.BasePanels.Count < 1) return;
                //
                bool bOldValue = this.GetVisible();
                this.OnBeforeVisibleExValueSeted(new BoolValueChangedEventArgs(bOldValue, value));
                //
                base.Visible = value;//key
                if (value)
                {
                    //bool bOk = true;
                    if (base.Parent is DockPanelHidePanel &&
                        this.m_HoldDockPanel != null &&
                        this.m_HoldDockPanel.HideAreaTabButtonGroupItem != null)
                    {
                        this.m_HoldDockPanel.HideAreaTabButtonGroupItem.ShowDockPanelHidePanel(true);
                    }
                    else if (base.Parent == null)
                    {
                        if (this.m_HoldDockPanel == null || this.m_HoldDockPanel.HideAreaTabButtonGroupItem == null)
                        { /*bOk =*/ this.ToDockPanelFloatForm(); }
                        else
                        { this.m_HoldDockPanel.HideAreaTabButtonGroupItem.ShowDockPanelHidePanel(true); }
                    }
                    //
                    ((ISetPanelNodeStateHelper)this).SetPanelNodeState(PanelNodeState.eShow);
                    //if (bOk)
                    //{
                    //    this.OnOpened(this, new EventArgs());
                    //    //
                    //    BasePanel basePanel = this.SelectedBasePanel;
                    //    if (basePanel != null) { basePanel.SetPanelState(PanelState.eShow); }
                    //}
                    //
                    //this.m_DockPanelCaption.Relayout();
                }
                else
                {
                    ((ISetPanelNodeStateHelper)this).SetPanelNodeState(PanelNodeState.eClose);
                    //this.OnClosed(this, new EventArgs());
                    ////
                    //BasePanel basePanel = this.SelectedBasePanel;
                    //if (basePanel != null) { basePanel.SetPanelState(PanelState.eHide); }
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
            get { return new Size(this.m_DockPanelFromSize.Width /*+ 16*/, this.m_DockPanelFromSize.Height /*+ 52*/); }
            set { this.m_DockPanelFromSize = value; }
        }

        [Browsable(false), Description("浮动面板管理器"), Category("关联")]
        public DockPanelManager DockPanelManager
        {
            get { return m_DockPanelManager; }
        }

        [Browsable(false), Description("描述信息"), Category("属性")]
        public string Describe
        { get { return "【系统辅助管理】停靠面板（DockPanel）：其本身是一个基础面板（BasePanel）容器，相当于面板树的最后一个二叉树节点（排除基础面板（BasePanel）后）。"; } }

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
            DockPanelContainerStyle eDockPanelContainerStyle = this.GetDockPanelContainerStyle();
            if (eDockPanelContainerStyle == DockPanelContainerStyle.eDockPanelFloatForm) return false;
            //
            this.LostFocusEx();//使其失去焦点 key
            //
            DockPanelFloatForm DockPanelFloatForm1 = this.DockPanelManager.GetEmptyDockPanelFloatForm();
            DockPanelFloatForm1.Show(this);
            //
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
            DockPanelContainerStyle eDockPanelContainerStyle = this.GetDockPanelContainerStyle();
            if (eDockPanelContainerStyle == DockPanelContainerStyle.eDockPanelFloatForm) return false;
            //
            this.LostFocusEx();//使其失去焦点 key
            //
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
                            //if (!dockPanel.VisibleEx) dockPanel.VisibleEx = true;
                        }
                        else
                        {
                            dockPanel.AddDockPanel(this, DockStyle.Fill);
                            //if (!dockPanel.VisibleEx) dockPanel.VisibleEx = true;
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
            if (this.Parent != null) this.Parent.Controls.Remove(this);
            this.Dock = DockStyle.Fill;
            dockArea.Controls.Add(this);//.Panel
            dockArea.ResumeLayout(false);//----------------------------------
            //
            dockArea.Visible = false;
            this.DockPanelManager.ParentForm.Controls.Add(dockArea);
            int iIndex = this.DockPanelManager.DocumentAreaIndex + 1;
            if (iIndex < this.DockPanelManager.ParentForm.Controls.Count - 1)
            { this.DockPanelManager.ParentForm.Controls.SetChildIndex(dockArea, iIndex); }
            if (!dockArea.bInteral && this.DockPanelManager.ParentForm.Controls.Count > 1)
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
        {
            get { return base.Parent; }
        }

        [Browsable(false), Description("自身IDockPanel的类型"), Category("状态")]
        public DockPanelStyle eDockPanelStyle//自身IDockPanel的类型
        {
            get { return DockPanelStyle.eDockPanel; }
        }

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

        public void ClearBasePanels()
        {
            this.BasePanels.Clear();
        }

        public void RemoveFromParent()
        {
            if (this.Parent != null)
            {
                this.Parent.Controls.Remove(this);
            }
        }

        public BasePanel[] GetBasePanels()
        {
            return this.BasePanels.ToArray();
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

        public IDockArea GetDockArea()//获取终级停靠区
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
        private IDockArea GetDockArea_DG(Control ctr)//递归 查询获取终级停靠区
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
            ref bool bIsBasePanel, ref bool bIsDocumentPanel)//获取停靠许可
        {
            foreach (IBasePanel one in this.BasePanels)
            {
                if (one == null) continue;
                if (bCanDockUp && !one.CanDockUp) bCanDockUp = false;
                if (bCanDockLeft && !one.CanDockLeft) bCanDockLeft = false;
                if (bCanDockRight && !one.CanDockRight) bCanDockRight = false;
                if (bCanDockBottom && !one.CanDockBottom) bCanDockBottom = false;
                if (bCanDockFill && !one.CanDockFill) bCanDockFill = false;
                if (bCanFloat && !one.CanFloat) bCanFloat = false;
                if (bCanHide && !one.CanHide) bCanHide = false;
                if (bCanClose && !one.CanClose) bCanClose = false;
                if (bCanHide && !one.IsBasePanel) bIsBasePanel = false;
                if (bCanHide && !one.IsDocumentPanel) bIsDocumentPanel = false;
            }
        }

        public bool AddDockPanel(IDockPanel pDockPanel, DockStyle eDockStyle)//向面板内添加IDockPanel实现动态布局（DockStyle.None为无效值）
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
                    bOk = this.AddDockPanelBottom(pDockPanel);
                    break;
                case DockStyle.Fill:
                    bOk = this.AddDockPanelFill(pDockPanel);
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
        private bool AddDockPanelFill(IDockPanel pDockPanel)//将IDockPanel内的所有BasePanel添加到自己的收集器内
        {
            if (!pDockPanel.CanDockFill)
            {
                GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show("无法实现填充停靠效果！停靠对象内存在不支持填充停靠效果的基础面板项。");
                return false;
            }
            //
            BasePanel[] basePanelCol = pDockPanel.GetBasePanels();
            pDockPanel.ClearBasePanels();
            foreach (object one in basePanelCol)
            {
                //this.m_BasePanelCollection.Add(one);
                this.m_BasePanelCollection.Insert(0, one);
            }
            //
            pDockPanel.RemoveFromParent();
            if (pDockPanel.eDockPanelStyle == DockPanelStyle.eDockPanel)
            {
                DockPanel dockPanel = pDockPanel as DockPanel;
                if (dockPanel != null && !this.DockPanelManager.DockPanels.Contains(dockPanel))
                { dockPanel.Dispose(); }
            }
            else
            {
                DockPanelContainer dockPanelContainer = pDockPanel as DockPanelContainer;
                if (dockPanelContainer != null)
                { dockPanelContainer.Dispose(); }
            }
            //
            return true;
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
                //if (dockPanelContainer.GetDockAreaStyle() == DockAreaStyle.eDockPanelDockArea)
                //{ this.SetShowHideButton(true); pDockPanel.SetShowHideButton(true); }
                //else
                //{ this.SetShowHideButton(false); pDockPanel.SetShowHideButton(false); }
                //
                return true;
            }
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
                //if (dockPanelContainer.GetDockAreaStyle() == DockAreaStyle.eDockPanelDockArea)
                //{ this.SetShowHideButton(true); pDockPanel.SetShowHideButton(true); }
                //else
                //{ this.SetShowHideButton(false); pDockPanel.SetShowHideButton(false); }
                //
                return true;
            }
            return false;
        }
        private bool AddDockPanelRight(IDockPanel pDockPanel)//添加其到右边
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
                //if (dockPanelContainer.GetDockAreaStyle() == DockAreaStyle.eDockPanelDockArea)
                //{ this.SetShowHideButton(true); pDockPanel.SetShowHideButton(true); }
                //else
                //{ this.SetShowHideButton(false); pDockPanel.SetShowHideButton(false); }
                //
                return true;
            }
            //
            return false;
        }
        private bool AddDockPanelBottom(IDockPanel pDockPanel)//添加其到底部
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
                //if (dockPanelContainer.GetDockAreaStyle() == DockAreaStyle.eDockPanelDockArea)
                //{ this.SetShowHideButton(true); pDockPanel.SetShowHideButton(true); }
                //else
                //{ this.SetShowHideButton(false); pDockPanel.SetShowHideButton(false); }
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

        #region IDockPanel2
        [Browsable(true), Description("激活状态改变事件"), Category("属性已更改")]
        public event BoolValueChangedEventHandler DockPanelActiveChanged;  //激活状态改变事件

        [Browsable(false), DefaultValue(0), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), Description("选中的BasePanel的索引"), Category("状态")]
        public int BasePanelSelectedIndex//选中的BasePanel的索引
        {
            get { return this.m_TabButtonContainerDPItem.TabButtonItemSelectedIndex; }
            set { this.m_TabButtonContainerDPItem.TabButtonItemSelectedIndex = value; }
        }

        private bool m_bActive = false;                          //记录激活状态
        private void SetActive(bool active)
        {
            if (this.bActive != active)
            {
                this.m_bActive = active;
                this.Refresh();
                this.OnDockPanelActiveChanged(new BoolValueChangedEventArgs(active));
            }
        }
        [Browsable(false), Description("记录激活状态"), Category("状态")]
        public bool bActive
        {
            get { return this.m_bActive; }
        }

        private bool m_bIsHideState = false; //获取隐藏状态                         <不要对其直接赋值>
        [Browsable(false), Description("获取隐藏状态"), Category("状态")]
        public bool IsHideState//获取隐藏状态
        {
            get { return m_bIsHideState; }
        }

        [Browsable(false), Description("用来记录选择的BasePanel的Image"), Category("外观")]
        public Image Image//用来记录选择的BasePanel的Image
        {
            get
            {
                if (this.SelectedBasePanel == null) return null;
                else return this.SelectedBasePanel.Image;
            }
        }

        [Browsable(false), Description("选中的BasePanel"), Category("状态")]
        public BasePanel SelectedBasePanel//选中的BasePanel
        {
            get
            {
                WFNew.ITabButtonItem pTabButtonItem = this.m_TabButtonContainerDPItem.SelectTabButtonItem;
                if (pTabButtonItem == null) return null;
                return pTabButtonItem.pTabPageItem as BasePanel;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Bindable(true), Localizable(true), Description("BasePanel收集器"), Category("集合")]
        public BasePanelCollection BasePanels//BasePanel收集器
        {
            get { return m_BasePanelCollection; }
        }

        [Browsable(false), Description("标题矩形框"), Category("布局")]
        public Rectangle CaptionRectangle
        {
            get
            {
                Rectangle rectangle = base.DisplayRectangle;
                return new Rectangle(rectangle.Left + 1, rectangle.Top + 1, rectangle.Width - 2, CRT_CAPTIONHEIGHT);
            }
        }

        [Browsable(false), Description("图片绘制矩形框"), Category("布局")]
        public Rectangle ImageRectangle
        {
            get
            {
                return new Rectangle(2, 2, 16, 16);
            }
        }

        [Browsable(false), Description("标题绘制矩形框"), Category("布局")]
        public Rectangle TitleRectangle
        {
            get
            {
                Rectangle rectangle = this.CaptionRectangle;
                if (this.Image == null)
                {
                    return Rectangle.FromLTRB
                        (
                        rectangle.Left,
                        rectangle.Top + 1,
                        this.m_DockPanelButtonStackItemEx.Visible ? this.m_DockPanelButtonStackItemEx.DisplayRectangle.Left : rectangle.Right - 1,
                        rectangle.Bottom - 1
                        );
                }
                else
                {
                    return Rectangle.FromLTRB
                        (
                        this.ImageRectangle.Right + 1,
                         rectangle.Top + 1,
                        this.m_DockPanelButtonStackItemEx.Visible ? this.m_DockPanelButtonStackItemEx.DisplayRectangle.Left : rectangle.Right - 1,
                        rectangle.Bottom - 1
                        );
                }
            }
        }

        //[Browsable(false), Description("外框矩形"), Category("布局")]
        //public Rectangle FrameRectangle
        //{
        //    get { return new Rectangle(0, 0, this.Width - 1, this.Height - 1); }
        //}

        /// <summary>
        /// 将basePanel设置为选择SelectedBasePanel
        /// </summary>
        /// <param name="basePanel"></param>
        /// <returns></returns>
        public bool SetSelectBasePanel(BasePanel basePanel)//将basePanel设置为选择SelectBasePanel
        {
            if (basePanel == null) return false;
            int index = this.BasePanels.IndexOf(basePanel);
            if (index < 0 || index >= this.BasePanels.Count) return false;
            this.BasePanelSelectedIndex = index;
            return true;
        }
        #endregion

        #region ISetDockPanelHelper
        void ISetDockPanelHelper.SetActiveState(IDockPanel pDockPanel)//设置停靠面板的激活状态
        {
            if (this.DockPanelManager != null)
            {
                foreach (DockPanel one in this.DockPanelManager.DockPanels)
                {
                    one.SetActive(one == pDockPanel);
                }
            }
        }

        bool ISetDockPanelHelper.SetHideState(bool bIsHideState)//设置隐藏状态
        {
            this.SetActive(!bIsHideState);
            //
            if (this.m_bIsHideState == bIsHideState) return false;
            //
            if (bIsHideState)
            {
                if (!this.CanHide)
                {
                    GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show("无法实现隐藏效果！停靠对象内存在不支持隐藏效果的基础面板项。");
                    return false;
                }
                if (this.GetDockAreaStyle() != DockAreaStyle.eDockPanelDockArea) return false;
            }
            else
            {
                if (this.GetDockAreaStyle() != DockAreaStyle.eNone) return false;
            }
            //
            if (!this.DockPanelManager.CompletedAddSetComponent && !this.DockPanelManager.AddSetComponent())
            {
                GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show("强制加载组件失败！致使缺少隐藏所需的相关组件，无法实现面板的隐藏！系统会自动取消隐藏状态！", "提示");
                return false;
            }
            //
            this.m_bIsHideState = bIsHideState;
            //
            return this.HideDockPanelEx(this.m_bIsHideState);
        }
        private bool HideDockPanelEx(bool bHide)//请不要在其它地方的调用它，它只适用于DockPanelCaption里的HideButton_MouseClick()方法，如果你要隐藏请使用HideDockPanelEx(bool bHide)
        {
            DockAreaStyle eDockAreaStyle = this.GetDockAreaStyle();
            if (this.DockPanelManager == null ||
                this.DockPanelManager.ParentForm == null ||
                eDockAreaStyle == DockAreaStyle.eDockPanelFloatForm) return false;
            //
            if (bHide)
            {
                if (!this.IsHideState ||
                    eDockAreaStyle != DockAreaStyle.eDockPanelDockArea) return false;
                //
                if (this.m_HoldDockPanel != null)
                {
                    this.RemoveFromParent();
                    this.m_HoldDockPanel.Dispose();
                    this.m_HoldDockPanel = null;
                }
                //
                this.m_HoldDockPanel = new HoldDockPanel(this);
                this.m_HoldDockPanel.HoldAndHide();
            }
            else
            {
                if (this.IsHideState ||
                    this.m_HoldDockPanel == null ||
                    eDockAreaStyle == DockAreaStyle.eDockPanelDockArea) return false;
                //
                this.DockPanelManager.DockPanelHidePanel.bHaveAnimation = false;
                this.m_HoldDockPanel.CancelHoldeAndShow();
                this.DockPanelManager.DockPanelHidePanel.bHaveAnimation = true;
                this.m_HoldDockPanel.Dispose();
                this.m_HoldDockPanel = null;
                //
                this.Width++;
                this.Width--;
            }
            //
            return true;
        }
        #endregion

        #region IPanelNode
        [Browsable(true), Description("面板节点状态改变事件"), Category("属性已更改")]
        public event PanelNodeStateChangedEventHandler PanelNodeStateChanged; //面板节点状态改变事件

        private PanelNodeState m_ePanelNodeState = PanelNodeState.eClose;  //记录面板节点状态
        [Browsable(false), Description("记录面板节点状态"), Category("状态")]
        public PanelNodeState ePanelNodeState
        {
            get { return m_ePanelNodeState; }
        }

        [Browsable(false), Description("获取节点类型"), Category("属性")]
        public PanelNodeStyle ePanelNodeStyle
        { 
            get { return PanelNodeStyle.eDockPanel; }
        }
        #endregion

        #region IMultipleNode
        [Browsable(false), Description("获取节点类型"), Category("属性")]
        public NodeStyle eNodeStyle//获取节点类型
        { get { return NodeStyle.eMultipleNode; } }

        [Browsable(false), Description("获取其父节点"), Category("关联")]
        public IBaseNode ParentNode//获取其父节点
        {
            get
            {
                if (this.Parent is System.Windows.Forms.SplitterPanel) { return this.Parent.Parent as IBaseNode; }
                return this.Parent as IBaseNode;
            }
        }

        [Browsable(false), Description("所携带子节点的个数"), Category("属性")]
        public int ChildNodeCount//所携带子节点的个数
        { get { return this.BasePanels.Count; } }

        [Browsable(false), Description("所携带的节点"), Category("集合")]
        public IBaseNode[] ChildNodes//所携带的节点
        {
            get
            {
                List<IBaseNode> baseNodes = new List<IBaseNode>();
                foreach (object one in this.BasePanels)
                {
                    IBaseNode temp = one as IBaseNode;
                    if (temp == null) continue;
                    baseNodes.Add(temp);
                }
                return baseNodes.ToArray();
            }
        }
        #endregion

        #region ISetDockPanelManagerHelper
        void ISetDockPanelManagerHelper.SetDockPanelManager(DockPanelManager dockPanelManager)//设置DockPanelManager，由系统管理（添加到DockPanelCollection时，调用该函数）
        {
            this.m_DockPanelManager = dockPanelManager;
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
                    for (int i = 0; i < this.BasePanels.Count; i++)
                    {
                        BasePanel basePanel = this.BasePanels[i] as BasePanel;
                        if (basePanel != null)
                        {
                            if (i == BasePanelSelectedIndex) { ((ISetPanelNodeStateHelper)basePanel).SetPanelNodeState(PanelNodeState.eShow); }
                            else { ((ISetPanelNodeStateHelper)basePanel).SetPanelNodeState(PanelNodeState.eHide); }
                        }
                    }
                    break;
                case PanelNodeState.eClose:
                    this.OnClosed(new EventArgs());
                    for (int i = 0; i < this.BasePanels.Count; i++)
                    {
                        BasePanel basePanel = this.BasePanels[i] as BasePanel;
                        if (basePanel != null) { ((ISetPanelNodeStateHelper)basePanel).SetPanelNodeState(PanelNodeState.eClose); }
                    }
                    break;
            }
        }
        #endregion
        
        [Browsable(false), DefaultValue(DockStyle.Fill), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new DockStyle Dock//value;
        {
            get { return base.Dock; }
            set { base.Dock = DockStyle.Fill; }
        }

        [Browsable(false), DefaultValue(true)]
        public new bool Visible//已被VisibleEx替代（请不要使用Visible属性）
        {
            get { return base.Visible; }
            set { base.Visible = value; }
        }

        #region 覆盖
        [Browsable(false)]
        public override string Text//用来记录选择的BasePanel的Text
        {
            get
            {
                if (this.SelectedBasePanel == null || this.SelectedBasePanel.Text.Length <= 0) return base.Text;
                else return this.SelectedBasePanel.Text;
            }
            set
            {
                if (this.SelectedBasePanel == null) base.Text = value;
                else this.SelectedBasePanel.Text = value;
            }
        }

        public override Rectangle DisplayRectangle
        {
            get
            {
                Rectangle rectangle = base.DisplayRectangle;
                //
                //System.Diagnostics.Debug.WriteLine(String.Format("{0}----{1}", this.IsHideState, this.m_TabButtonContainerDPItem.Visible));
                //
                if (this.GetDockAreaStyle() != DockAreaStyle.eDocumentDockArea)
                {
                    rectangle = new Rectangle
                        (
                        CRT_DOCKPANELBORBERSPACE, 
                        CRT_DOCKPANELBORBERSPACE + CRT_CAPTIONHEIGHT, 
                        rectangle.Width - 2 * CRT_DOCKPANELBORBERSPACE, 
                        rectangle.Height - CRT_CAPTIONHEIGHT - 2 * CRT_DOCKPANELBORBERSPACE
                        );
                    //
                    if (!this.m_TabButtonContainerDPItem.Visible) return rectangle;
                }
                //
                switch (this.m_TabButtonContainerDPItem.TabAlignment)
                {
                    case TabAlignment.Top:
                        return Rectangle.FromLTRB(rectangle.Left, rectangle.Top + CRT_TABBUTTONCONTAINERHEIGHT, rectangle.Right, rectangle.Bottom);
                    case TabAlignment.Bottom:
                        return Rectangle.FromLTRB(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom - CRT_TABBUTTONCONTAINERHEIGHT);
                    case TabAlignment.Left:
                        return Rectangle.FromLTRB(rectangle.Left + CRT_TABBUTTONCONTAINERHEIGHT, rectangle.Top, rectangle.Right, rectangle.Bottom);
                    case TabAlignment.Right:
                        return Rectangle.FromLTRB(rectangle.Left, rectangle.Top, rectangle.Right - CRT_TABBUTTONCONTAINERHEIGHT, rectangle.Bottom);
                    default:
                        return rectangle;
                }
            }
        }

        public override Rectangle ItemsRectangle
        {
            get
            {
                return base.DisplayRectangle;
            }
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            if (e.Control is BasePanel)
            {
                if (this.m_TabButtonContainerDPItem.AutoVisible)
                {
                    if (this.BasePanels.Count == 2) { this.Refresh(); }
                }
                //System.Diagnostics.Debug.WriteLine(String.Format("{0}----{1}----{2}----{3}----{4}", e.Control.Name, this.BasePanels.Count, this.Controls.Count, "OnControlAdded", this.Text));
                //
                base.OnControlAdded(e);
            }
            else
            {
                this.Controls.Remove(e.Control);
            }
        }

        protected override void OnControlRemoved(ControlEventArgs e)
        {
            if (this.m_TabButtonContainerDPItem.AutoVisible)
            {
                if (this.BasePanels.Count == 1) { this.Refresh(); }
                //System.Diagnostics.Debug.WriteLine(String.Format("{0}----{1}----{2}----{3}----{4}", e.Control.Name, this.BasePanels.Count, this.Controls.Count, "OnControlRemoved", this.Text));
            }
            //
            base.OnControlRemoved(e);
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            //
            if (this.VisibleEx) { ((ISetPanelNodeStateHelper)this).SetPanelNodeState(PanelNodeState.eShow); }
            else { ((ISetPanelNodeStateHelper)this).SetPanelNodeState(PanelNodeState.eClose); }
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case (int)GISShare.Win32.Msgs.WM_MOUSEACTIVATE://0x0021当光标在某个非激活的窗口中而用户正按着鼠标的某个键发送此消息给当前窗口
                    ((ISetDockPanelHelper)this).SetActiveState(this);
                    break;
                default:
                    break;
            }
            base.WndProc(ref m);
        }

        protected override void Dispose(bool disposing)
        {
            if (this == null || this.DockPanelManager == null || !this.DockPanelManager.DockPanels.Contains(this)) { base.Dispose(disposing); }
        }

        bool m_IsCaptionRectangleMouseDown = false;
        WFNew.ITabButtonItem m_pTabButtonItem = null;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (this.TabButtonContainerRectangle.Contains(e.Location))
                {
                    this.m_pTabButtonItem = this.m_TabButtonContainerDPItem.BaseItems.GetBaseItemFromPoint(e.Location) as WFNew.ITabButtonItem;
                }
                else if (this.CaptionRectangle.Contains(e.Location))
                {
                    this.m_IsCaptionRectangleMouseDown = true;
                }
            }
            //
            base.OnMouseDown(e);
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            //
            if (this.m_pTabButtonItem != null && !this.TabButtonContainerRectangle.Contains(e.Location))
            {
                if (this.IsHideState ||
                    //this.BasePanels.Count <= 1 ||
                    this.DockPanelManager == null ||
                    this.DockPanelManager.IsDockLayoutState) return;
                //
                BasePanel basePanel = this.m_pTabButtonItem.pTabPageItem as BasePanel;
                if (basePanel != null && basePanel.CanFloat)
                {
                    //basePanel.ToDockPanelFloatForm(new Point(e.X, -e.Y));
                    basePanel.ToDockPanelFloatForm(new Point(e.X, e.Y));
                    //设置激活状态
                    //this.SetActiveState(false);
                    //if (basePanel.DependDockPanel != null) { ((ISetDockPanelHelper)basePanel.DependDockPanel).SetActiveState(); }
                    ((ISetBasePanelHelper)basePanel).SetDependDockPanelActiveState();
                }
                //
                m_pTabButtonItem = null;
            }
            else if (this.m_IsCaptionRectangleMouseDown && !this.CaptionRectangle.Contains(e.Location))
            {
                if (this.IsHideState ||
                    this.Parent == null ||
                    this.DockPanelManager == null ||
                    this.DockPanelManager.IsDockLayoutState) return;
                //
                if (this.CanFloat && this.Parent.Controls.Contains(this))
                {
                    //basePanel.ToDockPanelFloatForm(new Point(e.X, -e.Y));
                    this.ToDockPanelFloatForm(new Point(e.X, e.Y));
                }
                //
                this.m_IsCaptionRectangleMouseDown = false;
            }
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            m_pTabButtonItem = null;
            this.m_IsCaptionRectangleMouseDown = false;
        }
        #endregion

        protected override void MessageMonitor(MessageInfo messageInfo)
        {
            base.MessageMonitor(messageInfo);
            //
            ((IMessageChain)this.m_TabButtonContainerDPItem).SendMessage(messageInfo);
            ((IMessageChain)this.m_DockPanelButtonStackItemEx).SendMessage(messageInfo);
        }

        public bool ContainsMousePoint(Point point)//是否包含该点（用来判断鼠标是否在面板内）
        {
            if (this.ClientRectangle.Contains(PointToClient(point))) return true;
            else return false;
        }

        public Point GetControlCenterScreenPoint()//获取面板中心点（用来辅助显示停靠按钮组）
        {
            return PointToScreen(new Point(this.Width / 2, this.Height / 2));
        }

        protected override void OnDraw(PaintEventArgs e)
        {
            if (this.GetDockAreaStyle() == DockAreaStyle.eDocumentDockArea)
            {
                base.OnDraw(e);
            }
            else
            {
                WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderDockPanel(
                     new ObjectRenderEventArgs(e.Graphics, this, this.FrameRectangle));
                if (this.Image != null)
                {
                    GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonImage(
                        new GISShare.Controls.WinForm.ImageRenderEventArgs(e.Graphics, this, this.Enabled, this.Image, this.ImageRectangle));
                }
                StringFormat stringFormat = new StringFormat();
                GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonText(
                    new GISShare.Controls.WinForm.TextRenderEventArgs(e.Graphics, this, this.Enabled, this.Text, this.ForeColor, this.Font, this.TitleRectangle));
            }
        }

        //

        //事件
        protected virtual void OnOpened(EventArgs e)
        { if (Opened != null) { this.Opened(this, e); } }

        protected virtual void OnClosed(EventArgs e)
        { if (Closed != null) { this.Closed(this, e); } }

        private void OnDockPanelActiveChanged(BoolValueChangedEventArgs e)//激发DockPanel激活状态改变事件
        {
            if (this.DockPanelActiveChanged != null) { this.DockPanelActiveChanged(this, e); }
        }

        protected virtual void OnBeforeVisibleExValueSeted(BoolValueChangedEventArgs e)
        { if (this.BeforeVisibleExValueSeted != null) this.BeforeVisibleExValueSeted(this, e); }

        protected virtual void OnAfterVisibleExValueSeted(BoolValueChangedEventArgs e)
        { if (this.AfterVisibleExValueSeted != null) this.AfterVisibleExValueSeted(this, e); }

        protected virtual void OnPanelNodeStateChanged(PanelNodeStateChangedEventArgs e)
        { if (this.PanelNodeStateChanged != null) this.PanelNodeStateChanged(this, e); }

        //
        //
        //

        class DockPanelButtonStackItemEx : DockPanelButtonStackItem
        {
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
                    IDockPanel2 pDockPanel = this.pOwner as IDockPanel2;
                    if (pDockPanel == null) return base.DisplayRectangle;
                    Rectangle rectangle = pDockPanel.CaptionRectangle;
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

        class TabButtonContainerDPItem : WFNew.TabButtonContainerItem
        {
            public override bool UsingCloseTabButton
            {
                get
                {
                    IDockPanel pDockPanel = this.pOwner as IDockPanel;
                    if (pDockPanel != null &&
                        pDockPanel.DockPanelManager != null && 
                        pDockPanel.GetDockAreaStyle() == DockAreaStyle.eDocumentDockArea) 
                        return pDockPanel.DockPanelManager.ShowDocumentCloseButton;
                    return base.UsingCloseTabButton;
                }
                set
                {
                    base.UsingCloseTabButton = value;
                }
            }

            public override bool AutoVisible
            {
                get
                {
                    IDockPanel pDockPanel = this.pOwner as IDockPanel;
                    if (pDockPanel == null) return base.AutoVisible;
                    return pDockPanel.GetDockAreaStyle() == DockAreaStyle.eDocumentDockArea ? false : true;
                }
                set
                {
                    base.AutoVisible = value;
                }
            }

            public override bool AutoShowOverflowTabButton
            {
                get
                {
                    return true;
                }
                set
                {
                    base.AutoShowOverflowTabButton = value;
                }
            }

            public override bool Visible
            {
                get
                {
                    DockPanel pDockPanel = this.pOwner as DockPanel;
                    if (pDockPanel == null) return base.Visible;
                    return pDockPanel.IsHideState ? false : base.Visible;
                }
                set
                {
                    base.Visible = value;
                }
            }

            public override TabAlignment TabAlignment
            {
                get
                {
                    IDockPanel pDockPanel = this.pOwner as IDockPanel;
                    if (pDockPanel == null) return base.TabAlignment;
                    return pDockPanel.GetDockAreaStyle() == DockAreaStyle.eDocumentDockArea ? TabAlignment.Top : TabAlignment.Bottom;
                }
                set
                {
                    base.TabAlignment = value;
                }
            }

            public override GISShare.Controls.WinForm.WFNew.TabButtonContainerStyle eTabButtonContainerStyle
            {
                get
                {
                    IDockPanel2 pDockPanel = this.pOwner as IDockPanel2;
                    if (pDockPanel == null) return base.eTabButtonContainerStyle;
                    if (pDockPanel.GetDockAreaStyle() == DockAreaStyle.eDocumentDockArea)
                    {
                        IBasePanel pBasePanel = pDockPanel.SelectedBasePanel;
                        if (pBasePanel == null) return GISShare.Controls.WinForm.WFNew.TabButtonContainerStyle.eContextButtonAndCloseButton;
                        return pBasePanel.CanClose ? GISShare.Controls.WinForm.WFNew.TabButtonContainerStyle.eContextButtonAndCloseButton : GISShare.Controls.WinForm.WFNew.TabButtonContainerStyle.eContextButton;
                    }
                    else
                    {
                        return GISShare.Controls.WinForm.WFNew.TabButtonContainerStyle.ePreButtonAndNextButton;
                    }
                }
                set
                {
                    base.eTabButtonContainerStyle = value;
                }
            }

            public override GISShare.Controls.WinForm.WFNew.PNLayoutStyle ePNLayoutStyle
            {
                get
                {
                    return GISShare.Controls.WinForm.WFNew.PNLayoutStyle.eBothEnds;
                }
                set
                {
                    base.ePNLayoutStyle = value;
                }
            }

            public override int LineDistance
            {
                get
                {
                    return 0;
                }
                set
                {
                    base.LineDistance = value;
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
            public override bool IsRestrictItems
            {
                get
                {
                    return true;
                }
                set
                {
                    base.IsRestrictItems = true;
                }
            }

            public override bool PreButtonIncreaseIndex
            {
                get
                {
                    return false;
                }
                set
                {
                    base.PreButtonIncreaseIndex = false;
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

            public override bool CanExchangeItem
            {
                get
                {
                    return true;
                }
                set
                {
                    base.CanExchangeItem = value;
                }
            }
        }

        /// <summary>
        /// 用来存放DockPanel所包含的BasePanel
        /// </summary>
        public class BasePanelCollection : WFNew.TabPageCollection
        {
            DockPanel m_DockPanel = null;

            public BasePanelCollection(DockPanel dockPanel)
                : base(dockPanel)
            {
                this.m_DockPanel = dockPanel;
            }

            protected override bool SetItemAttribute(GISShare.Controls.WinForm.WFNew.ITabPageItem value)
            {
                BasePanel basePanel = value as BasePanel;
                if (basePanel == null) return false;
                ((ISetBasePanelHelper)basePanel).SetDependDockPanel(this.m_DockPanel);
                ((ISetPanelNodeStateHelper)basePanel).SetPanelNodeState(PanelNodeState.eClose);//每当添加则触发打开事件
                //
                return base.SetItemAttribute(value);
            }

            public int Add(object value)
            {
                return base.Add(value as WFNew.ITabPageItem);
            }

            public void Insert(int index, object value)
            {
                base.Insert(index, value as WFNew.ITabPageItem);
            }

            public void Remove(object value)
            {
                BasePanel basePanel = value as BasePanel;
                if (basePanel == null) return;
                this.m_DockPanel.DockPanelManager.BasePanels.Add(basePanel);
                //
                ((ISetPanelNodeStateHelper)basePanel).SetPanelNodeState(PanelNodeState.eRemove);//每当关闭则触发关闭事件
                //
                base.Remove(basePanel as WFNew.ITabPageItem);//key
            }

            public new void RemoveAt(int index)
            {
                this.Remove(this[index]);
            }

            public BasePanel[] ToArray()
            {
                List<BasePanel> basePanelCol = new List<BasePanel>();
                foreach (object one in this)
                {
                    BasePanel temp = one as BasePanel;
                    if (temp == null) continue;
                    basePanelCol.Add(temp);
                }
                return basePanelCol.ToArray();
            }
        }
    }

}
