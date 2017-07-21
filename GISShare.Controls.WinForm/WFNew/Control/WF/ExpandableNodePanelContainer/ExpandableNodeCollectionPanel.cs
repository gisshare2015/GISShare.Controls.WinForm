using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    [Designer(typeof(GISShare.Controls.WinForm.WFNew.Design.ExpandableNodeCollectionPanelDesigner)), ToolboxItem(false)]
    public class ExpandableNodeCollectionPanel : ExpandableNodePanel, ICollectionObjectDesignHelper
    {
        public ExpandableNodeCollectionPanel()
        {
            this.m_ExpandableNodePanelCollection = new ExpandableNodePanelContainer.ExpandableNodePanelCollection(this);
        }

        #region ICollectionObjectDesignHelper
        System.Collections.IList ICollectionObjectDesignHelper.List { get { return this.ExpandableNodePanels; } }

        bool ICollectionObjectDesignHelper.ExchangeItem(object item1, object item2) { return this.ExpandableNodePanels.ExchangeItem(item1, item2); }
        #endregion

        private bool m_AutoResize = true;
        [Browsable(true), DefaultValue(true), Description("自动调节尺寸"), Category("布局")]
        public bool AutoResize
        {
            get
            {
                switch (this.Dock)
                {
                    case DockStyle.Left:
                    case DockStyle.Right:
                    case DockStyle.Fill:
                        return false;
                }
                //
                return m_AutoResize;
            }
            set
            {
                if (m_AutoResize == value) return;
                //
                m_AutoResize = value;
                this.UpdatePanelPositions();
            }
        }

        private Size m_Space = new Size(10, 10);
        [Browsable(true), DefaultValue(typeof(Size), "10, 10"), Description("面板间距"), Category("布局")]
        public Size Space
        {
            get { return m_Space; }
            set
            {
                if (m_Space == value) return;
                //
                m_Space = value;
                this.UpdatePanelPositions();
            }
        }

        private ExpandableNodePanelContainer.ExpandableNodePanelCollection m_ExpandableNodePanelCollection = null;
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Bindable(true), Localizable(true), Description("ExpandableNodePanel收集器"), Category("集合")]
        public ExpandableNodePanelContainer.ExpandableNodePanelCollection ExpandableNodePanels
        {
            get { return m_ExpandableNodePanelCollection; }
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            //
            ExpandableNodePanel expandablePanel = e.Control as ExpandableNodePanel;
            if (expandablePanel == null) this.Controls.Remove(e.Control);
            //
            this.UpdatePanelPositions();
        }

        protected override void OnControlRemoved(ControlEventArgs e)
        {
            base.OnControlRemoved(e);
            this.UpdatePanelPositions();
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            if(this.IsExpand) this.UpdatePanelPositions();
            base.OnLayout(levent);
        }

        protected virtual void UpdatePanelPositions()
        {
            int width = this.Space.Width;
            //int y = this.Space.Height + this.CaptionHeight + base.AutoScrollPosition.Y;
            int y = this.Space.Height + this.CaptionHeight;
            foreach (ExpandableNodePanel one in this.ExpandableNodePanels)
            {
                if (!one.Visible) continue;
                //
                one.SetBounds(width, y, 0, 0, BoundsSpecified.Location);
                one.SetBounds(0, 0, base.ClientSize.Width - (2 * this.Space.Width), 0, BoundsSpecified.Width);
                y = (y + one.Height) + this.Space.Height;
            }
            //
            if (this.AutoResize) this.Height = y;
            //
            base.Invalidate();
        }
    }
}
