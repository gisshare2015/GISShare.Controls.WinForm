using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    class TreeNodeButtonECPItem : WFNew.BaseButtonItem, IExpandableCaptionPanelButtonItem
    {
        public TreeNodeButtonECPItem()
        {
            base.Size = new Size(17, 17);
        }

        public ExpandableCaptionPanelButtonItemStyle eExpandableCaptionPanelButtonItemStyle
        {
            get { return ExpandableCaptionPanelButtonItemStyle.eTreeNodeButton; }
        }

        public bool IsExpand
        {
            get
            {
                if (this.pOwner == null) return false;
                IExpandableCaptionPanel pExpandableCaptionPanel = this.pOwner as IExpandableCaptionPanel;
                if (pExpandableCaptionPanel == null) return false;
                return pExpandableCaptionPanel.IsExpand;
            }
        }

        public bool UseMaxMinStyle
        {
            get
            {
                if (this.pOwner == null) return false;
                IExpandableCaptionPanel pExpandableCaptionPanel = this.pOwner as IExpandableCaptionPanel;
                if (pExpandableCaptionPanel == null) return false;
                return pExpandableCaptionPanel.UseMaxMinStyle;
            }
        }

        public ExpandButtonStyle eExpandButtonStyle
        {
            get
            {
                if (this.pOwner == null) return ExpandButtonStyle.eLeftToRight;
                IExpandableCaptionPanel pExpandableCaptionPanel = this.pOwner as IExpandableCaptionPanel;
                if (pExpandableCaptionPanel == null) return ExpandButtonStyle.eLeftToRight;
                return pExpandableCaptionPanel.eExpandButtonStyle;
            }
        }

        public TabAlignment CaptionAlignment
        {
            get
            {
                if (this.pOwner == null) return TabAlignment.Top;
                IExpandableCaptionPanel pExpandableCaptionPanel = this.pOwner as IExpandableCaptionPanel;
                if (pExpandableCaptionPanel == null) return TabAlignment.Top;
                return pExpandableCaptionPanel.eCaptionAlignment;
            }
        }

        public Rectangle GlyphRectangle
        {
            get
            {
                return Util.UtilTX.CreateRectangle(this.DisplayRectangle, 15, 15);
            }
        }

        public override bool Visible
        {
            get
            {
                if (this.pOwner == null) return base.Visible;
                IExpandableCaptionPanel pExpandableCaptionPanel = this.pOwner as IExpandableCaptionPanel;
                if (pExpandableCaptionPanel == null) return base.Visible;
                switch (pExpandableCaptionPanel.eCaptionAlignment)
                {
                    case TabAlignment.Top:
                    case TabAlignment.Bottom:
                        switch (pExpandableCaptionPanel.eExpandButtonStyle)
                        {
                            case ExpandButtonStyle.eTopToBottom:
                            case ExpandButtonStyle.eBottomToTop:
                                return pExpandableCaptionPanel.ShowCaption && pExpandableCaptionPanel.ShowTreeNodeButton;
                            case ExpandButtonStyle.eLeftToRight:
                            case ExpandButtonStyle.eRightToLeft:
                                if (pExpandableCaptionPanel.ShowExpandButton)
                                {
                                    return pExpandableCaptionPanel.ShowCaption && pExpandableCaptionPanel.IsExpand && pExpandableCaptionPanel.ShowTreeNodeButton;
                                }
                                return pExpandableCaptionPanel.ShowCaption && pExpandableCaptionPanel.ShowTreeNodeButton;
                        }
                        return pExpandableCaptionPanel.ShowCaption && pExpandableCaptionPanel.ShowTreeNodeButton;
                    case TabAlignment.Left:
                    case TabAlignment.Right:
                        switch (pExpandableCaptionPanel.eExpandButtonStyle)
                        {
                            case ExpandButtonStyle.eTopToBottom:
                            case ExpandButtonStyle.eBottomToTop:
                                if (pExpandableCaptionPanel.ShowExpandButton)
                                {
                                    return pExpandableCaptionPanel.ShowCaption && pExpandableCaptionPanel.IsExpand && pExpandableCaptionPanel.ShowTreeNodeButton;
                                }
                                return pExpandableCaptionPanel.ShowCaption && pExpandableCaptionPanel.ShowTreeNodeButton;
                            case ExpandButtonStyle.eLeftToRight:
                            case ExpandButtonStyle.eRightToLeft:
                                return pExpandableCaptionPanel.ShowCaption && pExpandableCaptionPanel.ShowTreeNodeButton;
                        }
                        return pExpandableCaptionPanel.ShowCaption && pExpandableCaptionPanel.ShowTreeNodeButton;
                    default:
                        return pExpandableCaptionPanel.ShowCaption && pExpandableCaptionPanel.ShowTreeNodeButton;
                }
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
                if (this.pOwner == null) return base.DisplayRectangle;
                IExpandableCaptionPanel pExpandableCaptionPanel = this.pOwner as IExpandableCaptionPanel;
                if (pExpandableCaptionPanel == null) return base.DisplayRectangle;
                if (!pExpandableCaptionPanel.ShowCaption) return base.DisplayRectangle;
                Rectangle rectangle = pExpandableCaptionPanel.CaptionRectangle;
                switch (pExpandableCaptionPanel.eCaptionAlignment) 
                {
                    case TabAlignment.Left:
                    case TabAlignment.Right:
                        return new Rectangle((rectangle.Left + rectangle.Right - this.Width) / 2, rectangle.Top + 1, 17, 17);
                    case TabAlignment.Top:
                    case TabAlignment.Bottom:
                    default:
                        return new Rectangle(rectangle.Left + 1, (rectangle.Top + rectangle.Bottom - this.Height) / 2, 17, 17);
                }
            }
        }
        
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            //
            if (this.pOwner == null) return;
            IExpandableCaptionPanel pExpandableCaptionPanel = this.pOwner as IExpandableCaptionPanel;
            if (pExpandableCaptionPanel == null) return;
            if (!pExpandableCaptionPanel.AutoSetIsExpand) return;
            ((ISetExpandableCaptionPanelHelper)pExpandableCaptionPanel).SetIsExpand(!pExpandableCaptionPanel.IsExpand);
        }

        protected override void OnDraw(PaintEventArgs pevent)
        {
            //base.OnDraw(pevent);
            WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderExpandableCaptionPanelButton(new ObjectRenderEventArgs(pevent.Graphics, this, this.DisplayRectangle));
        }
    }
}
