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
    public class ExpandableNodeCollectionPanelDesigner : ParentControlDesigner
    {
        private ExpandableNodeCollectionPanel m_ExpandableNodeCollectionPanel = null;

        private DesignerVerbCollection verbs;

        public override DesignerVerbCollection Verbs
        {
            get
            {
                if (verbs == null)
                {
                    verbs = new DesignerVerbCollection();
                    verbs.Add(new DesignerVerb("关系树设计器", new EventHandler(BuildTreeView)));
                    verbs.Add(new DesignerVerb("添加伸展节点面板（伸展样式）", new EventHandler(AddExpandableNodePanel1)));
                    verbs.Add(new DesignerVerb("添加伸展节点面板（节点样式）", new EventHandler(AddExpandableNodePanel2)));
                    verbs.Add(new DesignerVerb("添加伸展节点面板（默认样式）", new EventHandler(AddExpandableNodePanel3)));
                    verbs.Add(new DesignerVerb("添加伸展节点收集器面板（伸展样式）", new EventHandler(AddExpandableNodeCollectionPanel1)));
                    verbs.Add(new DesignerVerb("添加伸展节点收集器面板（节点样式）", new EventHandler(AddExpandableNodeCollectionPanel2)));
                    verbs.Add(new DesignerVerb("添加伸展节点收集器面板（默认样式）", new EventHandler(AddExpandableNodeCollectionPanel3)));
                    verbs.Add(new DesignerVerb("关于...", new EventHandler(ShowInfo)));
                }
                return verbs;
            }
        }

        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            //
            this.m_ExpandableNodeCollectionPanel = base.Component as ExpandableNodeCollectionPanel;
            if (this.m_ExpandableNodeCollectionPanel == null)
            {
                this.DisplayError(new ArgumentException("ExpandableNodeCollectionPanel == null"));
                return;
            }
        }

        protected override void OnPaintAdornments(PaintEventArgs pea)
        {
            base.OnPaintAdornments(pea);
            //
            if (!this.m_ExpandableNodeCollectionPanel.ShowCaption) { if (this.m_ExpandableNodeCollectionPanel.Height < 16) return; }
            else { if (this.m_ExpandableNodeCollectionPanel.Height < this.m_ExpandableNodeCollectionPanel.CaptionHeight + 16) return; }
            //
            if (m_ExpandableNodeCollectionPanel != null && m_ExpandableNodeCollectionPanel.IsExpand)
            {
                using (Pen p = new Pen(GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.WFNewColorTable.ArrowDisabled, 1))
                {
                    Rectangle rectangle = this.m_ExpandableNodeCollectionPanel.DisplayRectangle;
                    rectangle = Rectangle.FromLTRB(rectangle.Left, rectangle.Top, rectangle.Right - 1, rectangle.Bottom - 1);
                    p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    pea.Graphics.DrawRectangle(p, rectangle);
                    //
                    StringFormat drawFormat = new StringFormat();
                    drawFormat.FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.FitBlackBox;
                    drawFormat.Trimming = StringTrimming.EllipsisCharacter;
                    SizeF size = pea.Graphics.MeasureString(this.m_ExpandableNodeCollectionPanel.Text, this.m_ExpandableNodeCollectionPanel.Font);
                    int iWidth = (int)(size.Width + 1);
                    int iHeight = (int)(size.Height + 1);
                    pea.Graphics.DrawString(this.m_ExpandableNodeCollectionPanel.Text,
                        this.m_ExpandableNodeCollectionPanel.Font,
                        new SolidBrush(GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.WFNewColorTable.ItemText),
                        new Rectangle((rectangle.Left + rectangle.Right - iWidth) / 2, (rectangle.Top + rectangle.Bottom - iHeight) / 2, iWidth, iHeight),
                        drawFormat);
                }
            }
        }

        protected override void WndProc(ref Message m)
        {
            try
            {
                if (this.m_ExpandableNodeCollectionPanel.Created)
                {
                    switch (m.Msg)
                    {
                        case (int)GISShare.Win32.Msgs.WM_LBUTTONDOWN://0x201
                            Point point = GISShare.Win32.NativeMethods.LParamToMouseLocation((int)m.LParam);
                            if (
                                (this.m_ExpandableNodeCollectionPanel.ShowTreeNodeButton &&
                                this.m_ExpandableNodeCollectionPanel.GetTreeNodeButtonRectangle().Contains(point))
                                ||
                                (this.m_ExpandableNodeCollectionPanel.ShowExpandButton &&
                                this.m_ExpandableNodeCollectionPanel.GetExpandButtonRectangle().Contains(point))
                                ) 
                            {
                                ISetExpandableCaptionPanelHelper pSetExpandableCaptionPanelHelper = this.m_ExpandableNodeCollectionPanel as ISetExpandableCaptionPanelHelper;
                                if (pSetExpandableCaptionPanelHelper == null) return;
                                pSetExpandableCaptionPanelHelper.SetIsExpand(!this.m_ExpandableNodeCollectionPanel.IsExpand);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            finally
            {
                base.WndProc(ref m);
            }
        }

        private void BuildTreeView(object sender, EventArgs ea)
        {
            ExpandableNodePanelCollectionDesignerForm expandableNodePanelCollectionDesignerForm = new ExpandableNodePanelCollectionDesignerForm(this.m_ExpandableNodeCollectionPanel);
            expandableNodePanelCollectionDesignerForm.GetServiceCallBackEx = new GetServiceCallBack(this.GetService);
            expandableNodePanelCollectionDesignerForm.TopMost = true;
            expandableNodePanelCollectionDesignerForm.Location = new Point(360, 150);
            expandableNodePanelCollectionDesignerForm.Show();
        }

        private void AddExpandableNodePanel1(object sender, EventArgs ea)
        {
            IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
            if (host != null)
            {
                ExpandableNodePanel expandablePanel = host.CreateComponent(typeof(ExpandableNodePanel)) as ExpandableNodePanel;
                expandablePanel.Text = expandablePanel.Name;
                expandablePanel.Size = new Size(100, 100);
                expandablePanel.ShowCloseButton = false;
                this.m_ExpandableNodeCollectionPanel.ExpandableNodePanels.Add(expandablePanel);
            }
        }

        private void AddExpandableNodePanel2(object sender, EventArgs ea)
        {
            IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
            if (host != null)
            {
                ExpandableNodePanel expandablePanel = host.CreateComponent(typeof(ExpandableNodePanel)) as ExpandableNodePanel;
                expandablePanel.Text = expandablePanel.Name;
                expandablePanel.Size = new Size(100, 100);
                expandablePanel.ShowCloseButton = false;
                expandablePanel.ShowExpandButton = false;
                expandablePanel.ShowTreeNodeButton = true;
                this.m_ExpandableNodeCollectionPanel.ExpandableNodePanels.Add(expandablePanel);
            }
        }

        private void AddExpandableNodePanel3(object sender, EventArgs ea)
        {
            IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
            if (host != null)
            {
                ExpandableNodePanel expandablePanel = host.CreateComponent(typeof(ExpandableNodePanel)) as ExpandableNodePanel;
                expandablePanel.Text = expandablePanel.Name;
                expandablePanel.Size = new Size(100, 100);
                expandablePanel.ShowTreeNodeButton = true;
                this.m_ExpandableNodeCollectionPanel.ExpandableNodePanels.Add(expandablePanel);
            }
        }

        private void AddExpandableNodeCollectionPanel1(object sender, EventArgs ea)
        {
            IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
            if (host != null)
            {
                ExpandableNodeCollectionPanel expandablePanel = host.CreateComponent(typeof(ExpandableNodeCollectionPanel)) as ExpandableNodeCollectionPanel;
                expandablePanel.Text = expandablePanel.Name;
                expandablePanel.Size = new Size(100, 100);
                expandablePanel.ShowCloseButton = false;
                this.m_ExpandableNodeCollectionPanel.ExpandableNodePanels.Add(expandablePanel);
            }
        }

        private void AddExpandableNodeCollectionPanel2(object sender, EventArgs ea)
        {
            IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
            if (host != null)
            {
                ExpandableNodeCollectionPanel expandablePanel = host.CreateComponent(typeof(ExpandableNodeCollectionPanel)) as ExpandableNodeCollectionPanel;
                expandablePanel.Text = expandablePanel.Name;
                expandablePanel.Size = new Size(100, 100);
                expandablePanel.ShowCloseButton = false;
                expandablePanel.ShowExpandButton = false;
                expandablePanel.ShowTreeNodeButton = true;
                this.m_ExpandableNodeCollectionPanel.ExpandableNodePanels.Add(expandablePanel);
            }
        }

        private void AddExpandableNodeCollectionPanel3(object sender, EventArgs ea)
        {
            IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
            if (host != null)
            {
                ExpandableNodeCollectionPanel expandablePanel = host.CreateComponent(typeof(ExpandableNodeCollectionPanel)) as ExpandableNodeCollectionPanel;
                expandablePanel.Text = expandablePanel.Name;
                expandablePanel.Size = new Size(100, 100);
                expandablePanel.ShowTreeNodeButton = true;
                this.m_ExpandableNodeCollectionPanel.ExpandableNodePanels.Add(expandablePanel);
            }
        }

        private void ShowInfo(object sender, EventArgs e)
        {
            InfoForm infoForm = new InfoForm();
            infoForm.ShowDialog();
        }

        //
        //
        //

        public class ExpandableNodePanelCollectionDesignerForm : Design.CollectionDesignerForm
        {
            public ExpandableNodePanelCollectionDesignerForm(IObjectDesignHelper pObjectDesignHelper)
                : base(pObjectDesignHelper) { }

            /// <summary>
            /// 默认的加载子项的类型数组
            /// </summary>
            /// <returns></returns>
            protected override Type[] CreateNewItemTypes()
            {
                return new Type[] { typeof(ExpandableNodePanel) };
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
                    "GISShare.Controls.WinForm.WFNew.ExpandableNodeCollectionPanel",
                    new Type[] { typeof(ExpandableNodePanel), typeof(ExpandableNodeCollectionPanel) }
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
