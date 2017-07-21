using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    [Designer(typeof(GISShare.Controls.WinForm.WFNew.Design.ExpandablePanelDesigner)), ToolboxItem(false)]
    public class ExpandablePanel : ExpandableCaptionPanel
    {
        bool m_IsCaptionExpandArea = false;
        [Browsable(true), DefaultValue(false), Description("点击标题区可实现折叠"), Category("属性")]
        public bool IsCaptionExpandArea
        {
            get
            {
                ExpandablePanelContainer expandablePanelContainer = this.Parent as ExpandablePanelContainer;
                if (expandablePanelContainer == null) return m_IsCaptionExpandArea;
                return expandablePanelContainer.IsCaptionExpandArea;
            }
            set { m_IsCaptionExpandArea = value; }
        }

        public override bool AutoSetIsExpand
        {
            get
            {
                return false;
            }
        }

        public override DockStyle Dock
        {
            get
            {
                return base.Dock;
            }
            set
            {
            }
        }

        public override bool bActive
        {
            get
            {
                return this.IsExpand;
            }
        }

        public override bool ShowCaption
        {
            get
            {
                return true;
            }
            set
            {
                base.ShowCaption = value;
            }
        }

        public override System.Windows.Forms.TabAlignment eCaptionAlignment
        {
            get
            {
                switch (base.Dock)
                {
                    case DockStyle.Left:
                    case DockStyle.Right:
                        return TabAlignment.Left;
                    case DockStyle.Top:
                    case DockStyle.Bottom:
                        return TabAlignment.Top;
                    default:
                        return base.eCaptionAlignment;
                }
            }
            set
            {
                base.eCaptionAlignment = value;
            }
        }

        public override ExpandButtonStyle eExpandButtonStyle
        {
            get
            {
                switch (base.Dock)
                {
                    case DockStyle.Left:
                    case DockStyle.Right:
                        return ExpandButtonStyle.eRightToLeft;
                    case DockStyle.Top:
                    case DockStyle.Bottom:
                        return ExpandButtonStyle.eBottomToTop;
                    default:
                        return base.eExpandButtonStyle;
                }
            }
            set
            {
                base.eExpandButtonStyle = value;
            }
        }

        public override object Clone()
        {
            return new ExpandablePanel();
        }

        protected override System.Drawing.Size GetExpandSize()
        {
            ExpandablePanelContainer expandablePanelContainer = this.Parent as ExpandablePanelContainer;
            if (expandablePanelContainer == null) return base.GetExpandSize();
            return expandablePanelContainer.GetWorkRegionSize();
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            //
            ExpandablePanelContainer expandablePanelContainer = this.Parent as ExpandablePanelContainer;
            if (expandablePanelContainer != null) expandablePanelContainer.ResetDefaultIndex();
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            //
            if (e.Button == MouseButtons.Left) 
            {
                if ((this.IsCaptionExpandArea && this.TitleRectangle.Contains(e.Location)) ||
                    (this.ShowTreeNodeButton && this.GetTreeNodeButtonRectangle().Contains(e.Location)) || 
                    (this.ShowExpandButton && this.GetExpandButtonRectangle().Contains(e.Location))) 
                {
                    ExpandablePanelContainer expandablePanelContainer = this.Parent as ExpandablePanelContainer;
                    if (expandablePanelContainer == null) return;
                    expandablePanelContainer.SetSelectedExpandablePanel(this);
                }
            }
        }
    }
}
