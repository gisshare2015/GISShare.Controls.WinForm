using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace GISShare.Controls.WinForm.WFNew.Design
{
    public class ExpandablePanelContainerDesigner : ParentControlDesigner
    {
        private ExpandablePanelContainer m_ExpandablePanelContainer = null;

        private DesignerVerbCollection verbs;

        public override DesignerVerbCollection Verbs
        {
            get
            {
                if (verbs == null)
                {
                    verbs = new DesignerVerbCollection();
                    verbs.Add(new DesignerVerb("关系树设计器", new EventHandler(BuildTreeView)));
                    verbs.Add(new DesignerVerb("添加伸展面板（伸展样式）", new EventHandler(AddExpandablePanel1)));
                    verbs.Add(new DesignerVerb("添加伸展面板（节点样式）", new EventHandler(AddExpandablePanel2)));
                    verbs.Add(new DesignerVerb("添加伸展面板（默认样式）", new EventHandler(AddExpandablePanel3)));
                    verbs.Add(new DesignerVerb("顶部停靠", new EventHandler(DockToTop)));
                    verbs.Add(new DesignerVerb("左边停靠", new EventHandler(DockToLeft)));
                    verbs.Add(new DesignerVerb("右边停靠", new EventHandler(DockToRight)));
                    verbs.Add(new DesignerVerb("底部停靠", new EventHandler(DockToBottom)));
                    verbs.Add(new DesignerVerb("填充", new EventHandler(DockToFill)));
                    verbs.Add(new DesignerVerb("不停靠", new EventHandler(DockToNone)));
                    verbs.Add(new DesignerVerb("关于...", new EventHandler(ShowInfo)));
                }
                return verbs;
            }
        }

        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            //
            this.m_ExpandablePanelContainer = base.Component as ExpandablePanelContainer;
            if (this.m_ExpandablePanelContainer == null)
            {
                this.DisplayError(new ArgumentException("ExpandablePanelContainer == null"));
                return;
            }
        }

        protected override void OnPaintAdornments(PaintEventArgs pea)
        {
            base.OnPaintAdornments(pea);
            //
            if (this.m_ExpandablePanelContainer.Height < 16) return;
            //
            if (this.m_ExpandablePanelContainer != null)
            {
                using (Pen p = new Pen(GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.WFNewColorTable.ArrowDisabled, 1))
                {
                    Rectangle rectangle = this.m_ExpandablePanelContainer.DisplayRectangle;
                    rectangle = Rectangle.FromLTRB(rectangle.Left, rectangle.Top, rectangle.Right - 1, rectangle.Bottom - 1);
                    p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    pea.Graphics.DrawRectangle(p, rectangle);
                    //
                    StringFormat drawFormat = new StringFormat();
                    drawFormat.FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.FitBlackBox;
                    drawFormat.Trimming = StringTrimming.EllipsisCharacter;
                    SizeF size = pea.Graphics.MeasureString(this.m_ExpandablePanelContainer.Text, this.m_ExpandablePanelContainer.Font);
                    int iWidth = (int)(size.Width + 1);
                    int iHeight = (int)(size.Height + 1);
                    pea.Graphics.DrawString(this.m_ExpandablePanelContainer.Text,
                        this.m_ExpandablePanelContainer.Font,
                        new SolidBrush(GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.WFNewColorTable.ItemText),
                        new Rectangle((rectangle.Left + rectangle.Right - iWidth) / 2, (rectangle.Top + rectangle.Bottom - iHeight) / 2, iWidth, iHeight),
                        drawFormat);
                }
            }
        }

        private void BuildTreeView(object sender, EventArgs ea)
        {
            ExpandablePanelCollectionDesignerForm expandablePanelCollectionDesignerForm = new ExpandablePanelCollectionDesignerForm(this.m_ExpandablePanelContainer);
            expandablePanelCollectionDesignerForm.GetServiceCallBackEx = new GetServiceCallBack(this.GetService);
            expandablePanelCollectionDesignerForm.TopMost = true;
            expandablePanelCollectionDesignerForm.Location = new Point(360, 150);
            expandablePanelCollectionDesignerForm.Show();
        }

        private void AddExpandablePanel1(object sender, EventArgs ea)
        {
            IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
            if (host != null)
            {
                ExpandablePanel expandablePanel = host.CreateComponent(typeof(ExpandablePanel)) as ExpandablePanel;
                expandablePanel.Text = expandablePanel.Name;
                expandablePanel.Size = new Size(100, 100);
                expandablePanel.ShowCloseButton = false;
                this.m_ExpandablePanelContainer.ExpandablePanels.Add(expandablePanel);
            }
        }

        private void AddExpandablePanel2(object sender, EventArgs ea)
        {
            IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
            if (host != null)
            {
                ExpandablePanel expandablePanel = host.CreateComponent(typeof(ExpandablePanel)) as ExpandablePanel;
                expandablePanel.Text = expandablePanel.Name;
                expandablePanel.Size = new Size(100, 100);
                expandablePanel.ShowCloseButton = false;
                expandablePanel.ShowExpandButton = false;
                expandablePanel.ShowTreeNodeButton = true;
                this.m_ExpandablePanelContainer.ExpandablePanels.Add(expandablePanel);
            }
        }

        private void AddExpandablePanel3(object sender, EventArgs ea)
        {
            IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
            if (host != null)
            {
                ExpandablePanel expandablePanel = host.CreateComponent(typeof(ExpandablePanel)) as ExpandablePanel;
                expandablePanel.Text = expandablePanel.Name;
                expandablePanel.Size = new Size(100, 100);
                expandablePanel.ShowTreeNodeButton = true;
                this.m_ExpandablePanelContainer.ExpandablePanels.Add(expandablePanel);
            }
        }

        private void DockToTop(object sender, EventArgs e)
        {
            if (this.m_ExpandablePanelContainer.Dock == DockStyle.Top) return;
            //
            switch (this.m_ExpandablePanelContainer.Dock)
            {
                case DockStyle.Left:
                case DockStyle.Right:
                    int iWidth = this.m_ExpandablePanelContainer.Width;
                    this.m_ExpandablePanelContainer.Dock = DockStyle.Top;
                    this.m_ExpandablePanelContainer.Height = iWidth;
                    break;
                default:
                    this.m_ExpandablePanelContainer.Dock = DockStyle.Top;
                    break;
            }
        }
        private void DockToLeft(object sender, EventArgs e)
        {
            if (this.m_ExpandablePanelContainer.Dock == DockStyle.Left) return;
            //
            switch (this.m_ExpandablePanelContainer.Dock)
            {
                case DockStyle.Top:
                case DockStyle.Bottom:
                    int iHeight = this.m_ExpandablePanelContainer.Height;
                    this.m_ExpandablePanelContainer.Dock = DockStyle.Left;
                    this.m_ExpandablePanelContainer.Width = iHeight;
                    break;
                default:
                    this.m_ExpandablePanelContainer.Dock = DockStyle.Left;
                    break;
            }
        }
        private void DockToRight(object sender, EventArgs e)
        {
            if (this.m_ExpandablePanelContainer.Dock == DockStyle.Right) return;
            //
            switch (this.m_ExpandablePanelContainer.Dock)
            {
                case DockStyle.Top:
                case DockStyle.Bottom:
                    int iHeight = this.m_ExpandablePanelContainer.Height;
                    this.m_ExpandablePanelContainer.Dock = DockStyle.Right;
                    this.m_ExpandablePanelContainer.Width = iHeight;
                    break;
                default:
                    this.m_ExpandablePanelContainer.Dock = DockStyle.Right;
                    break;
            }
        }
        private void DockToBottom(object sender, EventArgs e)
        {
            if (this.m_ExpandablePanelContainer.Dock == DockStyle.Bottom) return;
            //
            switch (this.m_ExpandablePanelContainer.Dock)
            {
                case DockStyle.Left:
                case DockStyle.Right:
                    int iWidth = this.m_ExpandablePanelContainer.Width;
                    this.m_ExpandablePanelContainer.Dock = DockStyle.Bottom;
                    this.m_ExpandablePanelContainer.Height = iWidth;
                    break;
                default:
                    this.m_ExpandablePanelContainer.Dock = DockStyle.Bottom;
                    break;
            }
        }
        private void DockToFill(object sender, EventArgs e)
        {
            if (this.m_ExpandablePanelContainer.Dock == DockStyle.Fill) return;
            //
            this.m_ExpandablePanelContainer.Dock = DockStyle.Fill;
        }
        private void DockToNone(object sender, EventArgs e)
        {
            if (this.m_ExpandablePanelContainer.Dock == DockStyle.None) return;
            //
            this.m_ExpandablePanelContainer.Dock = DockStyle.None;
        }

        private void ShowInfo(object sender, EventArgs e)
        {
            InfoForm infoForm = new InfoForm();
            infoForm.ShowDialog();
        }

        //
        //
        //

        public class ExpandablePanelCollectionDesignerForm : Design.CollectionDesignerForm
        {
            public ExpandablePanelCollectionDesignerForm(IObjectDesignHelper pObjectDesignHelper)
                : base(pObjectDesignHelper) { }

            /// <summary>
            /// 默认的加载子项的类型数组
            /// </summary>
            /// <returns></returns>
            protected override Type[] CreateNewItemTypes()
            {
                return new Type[] { typeof(ExpandablePanel) };
            }

            /// <summary>
            /// 各类型 对应的加载子项的类型数组
            /// </summary>
            /// <returns></returns>
            protected override Dictionary<string, Type[]> CreateNewItemTypesDictionary()
            {
                Dictionary<string, Type[]> typeCreateNewItemTypesDictionary = new Dictionary<string, Type[]>();
                //
                typeCreateNewItemTypesDictionary.Add
                    (
                    "GISShare.Controls.WinForm.WFNew.ExpandablePanelContainer",
                    new Type[] { typeof(ExpandablePanel) }
                    );
                //
                return typeCreateNewItemTypesDictionary;
            }

            /// <summary>
            /// 创建节点类型
            /// </summary>
            /// <param name="value"></param>
            /// <returns></returns>
            protected override GISShare.Controls.WinForm.WFNew.View.NodeViewItem CreateNode(object value)
            {
                if (value is WFNew.ICollectionObjectDesignHelper)
                {
                    GISShare.Controls.WinForm.WFNew.View.NodeViewItem node = new GISShare.Controls.WinForm.WFNew.View.NodeViewItem();
                    node.ShowNomalState = true;
                    return node;
                }
                //
                return base.CreateNode(value);
            }

            /// <summary>
            /// 用来设置创建实例的属性信息
            /// </summary>
            /// <param name="pComponent"></param>
            /// <returns></returns>
            protected override bool SetCreateTypeInfo(IComponent pComponent)
            {
                Control ctr = pComponent as Control;
                if (ctr != null) ctr.Size = new Size(100, 100);
                return base.SetCreateTypeInfo(pComponent);
            }
        }

    }
}
