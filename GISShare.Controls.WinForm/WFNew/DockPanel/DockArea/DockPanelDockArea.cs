using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Threading;

namespace GISShare.Controls.WinForm.WFNew.DockPanel
{
    [Designer(typeof(GISShare.Controls.WinForm.WFNew.DockPanel.Design.DockPanelDockAreaDesigner)), ToolboxItem(false)]
    public class DockPanelDockArea : GISShare.Controls.WinForm.WFNew.SplitPanel, IDockArea, IRootNode, ISetDockPanelManagerHelper
    {
        public DockPanelDockArea()
            : base()
        {
            base.Text = "面板停靠区";
            base.Name = "DockPanelDockArea";
            base.Dock = DockStyle.Left;
            base.Size = new Size(160, 160);
        }

        #region 覆盖
        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            //
            IDockPanel pDockPanel = e.Control as IDockPanel;
            if (pDockPanel == null) { this.Controls.Remove(e.Control); return; }
            pDockPanel.BeforeVisibleExValueSeted += new BoolValueChangedEventHandler(DockPanel_BeforeVisibleExValueSeted);
        }
        private void DockPanel_BeforeVisibleExValueSeted(object sender, BoolValueChangedEventArgs e)
        {
            Control ctr = sender as Control;
            if (this.Controls.Contains(ctr)) { this.Visible = e.NewValue; }
        }

        protected override void OnControlRemoved(ControlEventArgs e)
        {
            base.OnControlRemoved(e);
            //
            IDockPanel pDockPanel = e.Control as IDockPanel;
            if (pDockPanel != null)
            {
                //pDockPanel.SetShowHideButton(false);
                pDockPanel.BeforeVisibleExValueSeted -= new BoolValueChangedEventHandler(DockPanel_BeforeVisibleExValueSeted);
            }
            //
            //
            //
            if (this.Controls.Count > 0) return;
            //
            if (this.Parent != null) this.Parent.Controls.Remove(this);
            this.Dispose();
            GC.Collect();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            //
            DockPanelManager dockPanelManager = this.DockPanelManager;
            if (dockPanelManager != null)
            {
                Size size = dockPanelManager.GetParentFormWorkRegionSize(false);
                this.OutSize = new Size(size.Width + this.Width - (this.OuterMinWidth + this.SplitLineWidth), size.Height + this.Height - (this.OuterMinWidth + this.SplitLineWidth));
            }
            //else { this.OutSize = new Size(this.Parent.Width - (this.OuterMinWidth + this.SplitLineWidth), this.Parent.Height - (this.OuterMinWidth + this.SplitLineWidth)); }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.DockPanelManager.DockPanelDockAreas.Contains(this))
                { this.DockPanelManager.DockPanelDockAreas.Remove(this); }
            }
            base.Dispose(disposing);
        }
        #endregion

        private bool m_bInteral = true;                     //是否为内部停靠
        [Browsable(false), DefaultValue(true)]
        internal bool bInteral//是否为内部停靠
        {
            get { return m_bInteral; }
            set { m_bInteral = value; }
        }

        internal void Relayout()//布局管理，防止停靠时出现遮盖情况
        {
            DockPanelManager dockPanelManager = this.DockPanelManager;
            if (dockPanelManager == null) return;
            Size size = dockPanelManager.GetParentFormWorkRegionSize(false);
            //
            switch (this.Dock)
            {
                case DockStyle.Top:
                case DockStyle.Bottom:
                    if (size.Height < this.OuterMinWidth) { dockPanelManager.RelayoutDockAreas(Orientation.Vertical, this.OuterMinWidth); }
                    break;
                case DockStyle.Left:
                case DockStyle.Right:
                    if (size.Width < this.OuterMinWidth) { dockPanelManager.RelayoutDockAreas(Orientation.Horizontal, this.OuterMinWidth); }
                    break;
                default:
                    break;
            }
        }

        public IDockPanel GetIDockPanel()//获取所包含的IDockPanel节点
        {
            IDockPanel pDockPanel = null;
            for (int i = 0; i < this.Controls.Count; i++)
            {
                pDockPanel = this.Controls[0] as IDockPanel;
                if (pDockPanel != null) { return pDockPanel; }
            }
            return pDockPanel;
        }

        //

        #region IDockPanelContainer
        private DockPanelManager m_DockPanelManager = null; //停靠面板管器
        [Browsable(false), Description("浮动面板管理器"), Category("关联")]
        public DockPanelManager DockPanelManager
        {
            get { return m_DockPanelManager; }
        }

        [Browsable(false), Description("记录自身容器的类型"), Category("属性")]
        public DockPanelContainerStyle eDockPanelContainerStyle//记录自身容器的类型
        { get { return DockPanelContainerStyle.eDockPanelDockArea; } }

        [Browsable(false), Description("描述信息"), Category("属性")]
        public string Describe
        { get { return "【由系统自动管理】面板停靠区（DockPanelDockArea）： 用来停靠所有继承于停靠面板接口（IDockPanel）的控件（包括：DockPanel、HoldDockPanel和DockPanelContainer），相当于面板树的一个顶级单根的树节点。"; } }

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
        [Browsable(false), DefaultValue(DockStyle.Left)]
        public new DockStyle Dock//属性不能为DockStyle.None
        {
            get { return base.Dock; }
            set
            {
                if (value == DockStyle.None) return;
                base.Dock = value;
            }
        }

        [Browsable(false), Description("停靠区矩形（屏幕坐标）"), Category("布局")]
        public Rectangle DockAreaRectangle
        {
            get
            {
                Rectangle rectangle = this.DisplayRectangle;
                return new Rectangle(this.PointToScreen(rectangle.Location), rectangle.Size);
            }
        }

        [Browsable(false), Description("记录自身停靠区类型"), Category("属性")]
        public DockAreaStyle eDockAreaStyle//记录自身停靠区类型
        { get { return DockAreaStyle.eDockPanelDockArea; } }

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
        void ISetDockPanelManagerHelper.SetDockPanelManager(DockPanelManager dockPanelManager)//设置DockPanelManager，由系统管理（在添加到DockPanelDockAreaCollection时设置该属性）
        {
            this.m_DockPanelManager = dockPanelManager;
        }
        #endregion

    }
}
