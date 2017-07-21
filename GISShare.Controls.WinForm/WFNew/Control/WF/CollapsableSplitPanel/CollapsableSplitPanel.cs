using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace GISShare.Controls.WinForm.WFNew
{
    [ToolboxItem(true), Designer(typeof(GISShare.Controls.WinForm.WFNew.Design.CollapsableSplitPanelDesigner)), ToolboxBitmap(typeof(CollapsableSplitPanel), "CollapsableSplitPanel.bmp")]
    public class CollapsableSplitPanel : GISShare.Controls.WinForm.WFNew.SplitPanel, ICollapsableSplitPanel
    {
        public event EventHandler CollapseChanged = null;

        private Size m_PreCollapseSize;

        public CollapsableSplitPanel()
            : base()
        {
            base.OuterMinWidth = 50;
            base.SplitLineWidth = 6;
            this.m_Cursor = Cursors.Default;
            this.m_PreCollapseSize = this.Size;
        }

        protected override EventStateStyle GetEventStateSupplement(string strEventName)
        {
            switch (strEventName)
            {
                case "CollapseChanged":
                    return this.CollapseChanged != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                default:
                    break;
            }
            //
            return base.GetEventStateSupplement(strEventName);
        }

        protected override bool RelationEventSupplement(string strEventName, EventArgs e)
        {
            switch (strEventName)
            {
                case "CollapseChanged":
                    if (this.CollapseChanged != null) { this.CollapseChanged(this, e as PaintEventArgs); }
                    return true;
                default:
                    break;
            }
            //
            return base.RelationEventSupplement(strEventName, e);
        }

        public override object Clone()
        {
            CollapsableSplitPanel baseItem = new CollapsableSplitPanel();
            baseItem.Name = this.Name;
            baseItem.Text = this.Text;
            baseItem.Dock = this.Dock;
            baseItem.Collapse = this.Collapse;
            baseItem.eCollapseSplitPanelStyles = this.eCollapseSplitPanelStyles; 
            if (this.GetEventState("CollapseChanged") == EventStateStyle.eUsed) baseItem.CollapseChanged += new EventHandler(baseItem_CollapseChanged);
            return baseItem;
        }
        void baseItem_CollapseChanged(object sender, EventArgs e)
        {
            this.RelationEvent("CollapseChanged", e);
        }

        #region ICollapsableSplitPanel
        private WFNew.BaseItemState m_eCollapseButtonState = BaseItemState.eNormal;
        [Browsable(false), Description("折叠按钮所处的状态（激活、按下、不可用、正常）"), Category("状态")]
        public virtual WFNew.BaseItemState eCollapseButtonState
        {
            get { return m_eCollapseButtonState; }
        }
        protected virtual void SetCollapseButtonState(WFNew.BaseItemState collapseButtonState)
        {
            if (m_eCollapseButtonState == collapseButtonState) return;
            //
            m_eCollapseButtonState = collapseButtonState;
        }
        protected virtual void SetCollapseButtonStateEx(WFNew.BaseItemState collapseButtonState)
        {
            if (m_eCollapseButtonState == collapseButtonState) return;
            //
            m_eCollapseButtonState = collapseButtonState;
            this.Invalidate(this.CollapseButtonRectangle);
        }

        private bool m_Collapse = false;
        [Browsable(true), DefaultValue(false), Description("折叠"), Category("状态")]
        public bool Collapse
        {
            get { return m_Collapse; }
            set
            {
                if (this.eCollapseSplitPanelStyles == CollapseSplitPanelStyles.SplitPanel || m_Collapse == value) return;
                //
                m_Collapse = value;
                //
                if (value)
                {
                    this.m_PreCollapseSize = this.Size;
                    //
                    if (this.Collapse)
                    {
                        switch (this.SplitPanelDock)
                        {
                            case DockStyle.Left:
                            case DockStyle.Right:
                                this.Width = this.SplitLineWidth;
                                break;
                            case DockStyle.Top:
                            case DockStyle.Bottom:
                                this.Height = this.SplitLineWidth;
                                break;
                            default:
                                break;
                        }
                    }
                }
                else
                {
                    this.Size = this.m_PreCollapseSize;
                }
                //
                this.OnCollapseChanged(new EventArgs());
                //
                if(!value) this.Refresh();
            }
        }

        private CollapseSplitPanelStyles m_eCollapseSplitPanelStyles = CollapseSplitPanelStyles.CollapsableSplitPanel;
        [Browsable(true), DefaultValue(CollapseSplitPanelStyles.CollapsableSplitPanel), Description("折叠面板类型"), Category("状态")]
        public virtual CollapseSplitPanelStyles eCollapseSplitPanelStyles
        {
            get { return m_eCollapseSplitPanelStyles; }
            set 
            {
                if (value == CollapseSplitPanelStyles.SplitPanel) this.Collapse = false;
                //
                m_eCollapseSplitPanelStyles = value; 
            }
        }

        [Browsable(false), Description("折叠按钮矩形框"), Category("布局")]
        public Rectangle CollapseButtonRectangle
        {
            get
            {
                switch (this.SplitPanelDock)
                {
                    case DockStyle.Top:
                    case DockStyle.Bottom:
                        return new Rectangle(new Point((this.Width - 60) / 2, this.SplitterRectangle.Y), new Size(60, this.SplitLineWidth));
                    case DockStyle.Left:
                    case DockStyle.Right:
                        return new Rectangle(new Point(this.SplitterRectangle.X, (this.Height - 60) / 2), new Size(this.SplitLineWidth, 60));
                    default:
                        return new Rectangle(0, 0, 60, this.SplitLineWidth);
                }
            }
        }
        #endregion

        [Browsable(true), DefaultValue(DockStyle.Left)]
        public override DockStyle Dock//请不要随意设置DockStyle.None值
        {
            get { return base.Dock; }
            set
            {
                if (value == DockStyle.None || value == DockStyle.Fill) return;
                base.Dock = value;
            }
        }

        private Cursor m_Cursor;
        [Browsable(false)]
        public override Cursor Cursor//显示鼠标指针状态
        {
            get
            {
                Point point = this.PointToClient(MousePosition);
                switch (this.eCollapseSplitPanelStyles)
                {
                    case CollapseSplitPanelStyles.SplitPanel:
                        if (base.DisplayRectangle.Contains(point)) return this.m_Cursor;
                        switch (this.SplitPanelDock)
                        {
                            case DockStyle.Top:
                            case DockStyle.Bottom:
                                return Cursors.HSplit;
                            case DockStyle.Left:
                            case DockStyle.Right:
                                return Cursors.VSplit;
                            default:
                                return this.m_Cursor;
                        }
                    case CollapseSplitPanelStyles.CollapsablePanel:
                        if (this.CollapseButtonRectangle.Contains(point)) return Cursors.Hand;
                        if (this.Collapse || base.DisplayRectangle.Contains(point)) return this.m_Cursor;
                        return this.m_Cursor;
                    case CollapseSplitPanelStyles.CollapsableSplitPanel:
                        if (this.CollapseButtonRectangle.Contains(point)) return Cursors.Hand;
                        if (this.Collapse || base.DisplayRectangle.Contains(point)) return this.m_Cursor;
                        switch (this.SplitPanelDock)
                        {
                            case DockStyle.Top:
                            case DockStyle.Bottom:
                                return Cursors.HSplit;
                            case DockStyle.Left:
                            case DockStyle.Right:
                                return Cursors.VSplit;
                            default:
                                return this.m_Cursor;
                        }
                    default:
                        return this.m_Cursor;
                }
            }
            set
            {
                this.m_Cursor = value;
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            switch (this.eCollapseSplitPanelStyles)
            {
                case CollapseSplitPanelStyles.SplitPanel:
                    base.OnMouseDown(e);
                    break;
                case CollapseSplitPanelStyles.CollapsablePanel:
                    if (this.Cursor == Cursors.Hand) { this.Collapse = !this.Collapse; return; }
                    break;
                case CollapseSplitPanelStyles.CollapsableSplitPanel:
                    if (this.Cursor == Cursors.Hand) { this.Collapse = !this.Collapse; return; }
                    if (this.Collapse) return;
                    base.OnMouseDown(e);
                    break;
                default:
                    break;
            } 
            //
            if (this.CollapseButtonRectangle.Contains(e.Location)) { this.SetCollapseButtonStateEx(BaseItemState.ePressed); }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (this.CollapseButtonRectangle.Contains(e.Location)) { this.SetCollapseButtonStateEx(BaseItemState.eHot); }
            else { this.SetCollapseButtonStateEx(BaseItemState.eNormal); }
            //
            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (this.CollapseButtonRectangle.Contains(e.Location)) { this.SetCollapseButtonStateEx(BaseItemState.eHot); }
            else { this.SetCollapseButtonStateEx(BaseItemState.eNormal); }
            //
            base.OnMouseUp(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            this.SetCollapseButtonStateEx(BaseItemState.eNormal);
            //
            base.OnMouseLeave(e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            //
            if (this.Collapse)
            {
                switch (this.SplitPanelDock)
                {
                    case DockStyle.Left:
                    case DockStyle.Right:
                        this.Width = this.SplitLineWidth;
                        break;
                    case DockStyle.Top:
                    case DockStyle.Bottom:
                        this.Height = this.SplitLineWidth;
                        break;
                    default:
                        break;
                }
            }
        }

        protected override void OnDraw(PaintEventArgs e)
        {
            WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderCollapsableSplitPanel(new ObjectRenderEventArgs(e.Graphics, this, this.AreaRectangle));
        }

        //事件
        protected virtual void OnCollapseChanged(EventArgs e)
        { if (this.CollapseChanged != null) { this.CollapseChanged(this, e); } }
    }
}
