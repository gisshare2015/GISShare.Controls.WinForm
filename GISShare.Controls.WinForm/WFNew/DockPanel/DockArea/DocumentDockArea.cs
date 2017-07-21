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
    [Designer(typeof(GISShare.Controls.WinForm.WFNew.DockPanel.Design.DocumentDockAreaDesigner))]
    public class DocumentDockArea : DocumentArea, IDockArea, IRootNode
    {
        public DocumentDockArea()
            : base()
        {
            base.Name = "DocumentDockArea";
            base.Dock = DockStyle.Fill;
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            //
            if (e.Control is IDockPanel) return;
            //
            this.Controls.Remove(e.Control);
        }

        public override object Clone()
        {
            return new DocumentDockArea();
        }

        #region IDockPanelContainer
        [Browsable(false), Description("浮动面板管理器"), Category("关联")]
        public DockPanelManager DockPanelManager
        {
            get
            {
                DockPanel dockPanel = GetDockPanel();
                if (dockPanel == null) return null;
                return dockPanel.DockPanelManager;
            }
        }

        [Browsable(false), Description("记录自身容器的类型"), Category("属性")]
        public DockPanelContainerStyle eDockPanelContainerStyle//记录自身容器的类型
        { get { return DockPanelContainerStyle.eDocumentDockArea; } }

        [Browsable(false), Description("描述信息"), Category("属性")]
        public string Describe
        { get { return "【由系统辅助管理】文档区面板（DocumentDockArea）： 用来管理整个单文档区内的控件，以减少布局问题。"; } }

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
        [Browsable(false), DefaultValue(DockStyle.Fill), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new DockStyle Dock
        {
            get { return base.Dock; }
            set
            {
                base.Dock = DockStyle.Fill;
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
        { get { return DockAreaStyle.eDocumentDockArea; } }

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
    }
}
