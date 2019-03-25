using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew.DockPanel
{
    [ToolboxItem(false)]
    class DockPanelHideArea : WFNew.BaseItemHost
    {
        private const int CRT_HEIGHT = 23;

        private DockPanelHideAreaItem m_DockPanelHideAreaItem;

        public event EventHandler ExistHideAreaTabButtonGroupItem;  //当存在隐藏按钮组时事件（即：0->1时触发）
        public event EventHandler EmptyHideAreaTabButtonGroupItem;  //当隐藏按钮组为空时事件（即：1->0时触发）

        public DockPanelHideArea(DockStyle eDockStyle, DockPanelManager dockPanelManager)
            : base(new DockPanelHideAreaItem(dockPanelManager))
        {
            this.Dock = eDockStyle;
            this.m_DockPanelHideAreaItem = (DockPanelHideAreaItem)this.BaseItemObject;
            //
            this.BaseItems.ItemAdded += new GISShare.Controls.WinForm.WFNew.ItemEventHandler(BaseItems_ItemAdded);
            this.BaseItems.ItemRemoved += new GISShare.Controls.WinForm.WFNew.ItemEventHandler(BaseItems_ItemRemoved);
        }
        void BaseItems_ItemAdded(object sender, GISShare.Controls.WinForm.WFNew.ItemEventArgs e)
        {
            switch (this.Dock)
            {
                case DockStyle.Top:
                case DockStyle.Bottom:
                    if (this.BaseItems.Count == 1)
                    {
                        if (this.Height != CRT_HEIGHT) this.Height = CRT_HEIGHT;
                        this.OnExistHideAreaTabButtonGroupItem(new EventArgs());
                    }
                    break;
                case DockStyle.Left:
                case DockStyle.Right:
                    if (this.BaseItems.Count == 1)
                    {
                        if (this.Width != CRT_HEIGHT) this.Width = CRT_HEIGHT;
                        this.OnExistHideAreaTabButtonGroupItem(new EventArgs());
                    }
                    break;
                default:
                    break;
            }
        }
        void BaseItems_ItemRemoved(object sender, GISShare.Controls.WinForm.WFNew.ItemEventArgs e)
        {
            switch (this.Dock)
            {
                case DockStyle.Top:
                case DockStyle.Bottom:
                    if (this.BaseItems.Count == 0)
                    {
                        if (this.Height != 0) this.Height = 0;
                        this.OnEmptyHideAreaTabButtonGroupItem(new EventArgs());
                    }
                    break;
                case DockStyle.Left:
                case DockStyle.Right:
                    if (this.BaseItems.Count == 0)
                    {
                        if (this.Width != 0) this.Width = 0;
                        this.OnEmptyHideAreaTabButtonGroupItem(new EventArgs());
                    }
                    break;
                default:
                    break;
            }
        }

        public BaseItemCollection BaseItems
        {
            get { return this.m_DockPanelHideAreaItem.BaseItems; }
        }

        public HideAreaTabButtonGroupItem GetHideAreaTabButtonGroupItem(int index)
        {
            return this.BaseItems[index] as HideAreaTabButtonGroupItem;
        }

        [Browsable(false)]
        public new bool Visible//被阉割的属性，无法为其赋值
        {
            get
            {
                return base.Visible;
            }
            set { }
        }

        [Browsable(false), DefaultValue(DockStyle.Left)]
        public override DockStyle Dock//值DockStyle.Fill和DockStyle.None无效
        {
            get
            {
                return base.Dock;
            }
            set
            {
                if (value == DockStyle.Fill || value == DockStyle.None) return;
                //
                base.Dock = value;
            }
        }

        //事件
        protected void OnExistHideAreaTabButtonGroupItem(EventArgs e)
        { if (ExistHideAreaTabButtonGroupItem != null) { this.ExistHideAreaTabButtonGroupItem(this, e); } }

        protected void OnEmptyHideAreaTabButtonGroupItem(EventArgs e)
        { if (EmptyHideAreaTabButtonGroupItem != null) { this.EmptyHideAreaTabButtonGroupItem(this, e); } }

        //
        //
        //

        class DockPanelHideAreaItem : WFNew.BaseItemStackExItem, WFNew.IArea
        {
            private DockPanelManager m_DockPanelManager = null;                        //记录其所在的浮动面板管理器

            public DockPanelHideAreaItem(DockPanelManager dockPanelManager)
            {
                this.ShowBackgroud = true;
                this.m_DockPanelManager = dockPanelManager;
            }

            #region WFNew.BaseItemStackEx
            public override bool CanExchangeItem
            {
                get
                {
                    return false;
                }
                set
                {
                    base.CanExchangeItem = value;
                }
            }

            public override bool IsRestrictItems
            {
                get
                {
                    return false;
                }
                set
                {
                    base.IsRestrictItems = value;
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
                    base.IsStretchItems = value;
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
                    base.PreButtonIncreaseIndex = value;
                }
            }

            public override Orientation eOrientation
            {
                get
                {
                    Control control = this.Parent;
                    if (control == null) return base.eOrientation;
                    //
                    switch (control.Dock)
                    {
                        case DockStyle.Top:
                        case DockStyle.Bottom:
                            return Orientation.Horizontal;
                        case DockStyle.Left:
                        case DockStyle.Right:
                            return Orientation.Vertical;
                        default:
                            return base.eOrientation;
                    }
                }
                set
                {
                    base.eOrientation = value;
                }
            }

            public override Padding Padding
            {
                get
                {
                    Control control = this.Parent;
                    if (control == null) return base.Padding;
                    //
                    switch (control.Dock)
                    {
                        case DockStyle.Top:
                            return new Padding(0, 1, 0, 1);
                        case DockStyle.Bottom:
                            return new Padding(0, 1, 0, 1);
                        case DockStyle.Left:
                            return new Padding(1, 0, 1, 0);
                        case DockStyle.Right:
                            return new Padding(1, 0, 1, 0);
                        default:
                            return base.Padding;
                    }
                }
                set
                {
                    base.Padding = value;
                }
            }

            protected override void OnDraw(PaintEventArgs e)
            {
                Control control = this.Parent;
                if (control != null)
                {
                    switch (control.Dock)
                    {
                        case DockStyle.Top:
                        case DockStyle.Bottom:
                            if (this.BaseItems.Count < 1) { if (this.Height != 0) control.Height = 0; }
                            else { if (this.Height != CRT_HEIGHT)  control.Height = CRT_HEIGHT; }
                            break;
                        case DockStyle.Left:
                        case DockStyle.Right:
                            if (this.BaseItems.Count < 1) { if (this.Width != 0) control.Width = 0; }
                            else { if (this.Width != CRT_HEIGHT)control.Width = CRT_HEIGHT; }
                            break;
                        default:
                            break;
                    }
                }
                //
                //base.OnDraw(e); 
                this.Relayout(e.Graphics, LayoutStyle.eLayoutPlan, true);
                this.Relayout(e.Graphics, LayoutStyle.eLayoutAuto, false);
                //
                WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonArea(new ObjectRenderEventArgs(e.Graphics, this, this.AreaRectangle));
            }
            #endregion
        }
    }


}
