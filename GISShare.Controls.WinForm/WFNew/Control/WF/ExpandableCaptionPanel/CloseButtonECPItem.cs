using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    class CloseButtonECPItem : WFNew.BaseButtonItem, IExpandableCaptionPanelButtonItem
    {
        public CloseButtonECPItem()
        {
            base.Size = new Size(17, 17);
            base.LockWith = true;
            base.LockHeight = true;
        }

        public ExpandableCaptionPanelButtonItemStyle eExpandableCaptionPanelButtonItemStyle
        {
            get { return ExpandableCaptionPanelButtonItemStyle.eCloseButton; }
        }

        public bool IsExpand
        {
            get
            {
                if (this.pOwner == null) return false;
                IExpandableCaptionPanel pExpandableCaptionPanel = this.pOwner.pOwner as IExpandableCaptionPanel;
                if (pExpandableCaptionPanel == null) return false;
                return pExpandableCaptionPanel.IsExpand;
            }
        }

        public bool UseMaxMinStyle
        {
            get
            {
                if (this.pOwner == null) return false;
                IExpandableCaptionPanel pExpandableCaptionPanel = this.pOwner.pOwner as IExpandableCaptionPanel;
                if (pExpandableCaptionPanel == null) return false;
                return pExpandableCaptionPanel.UseMaxMinStyle;
            }
        }

        public ExpandButtonStyle eExpandButtonStyle
        {
            get
            {
                if (this.pOwner == null) return ExpandButtonStyle.eLeftToRight;
                IExpandableCaptionPanel pExpandableCaptionPanel = this.pOwner.pOwner as IExpandableCaptionPanel;
                if (pExpandableCaptionPanel == null) return ExpandButtonStyle.eLeftToRight;
                return pExpandableCaptionPanel.eExpandButtonStyle;
            }
        }

        public TabAlignment CaptionAlignment
        {
            get
            {
                if (this.pOwner == null) return TabAlignment.Top;
                IExpandableCaptionPanel pExpandableCaptionPanel = this.pOwner.pOwner as IExpandableCaptionPanel;
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
                IExpandableCaptionPanel pExpandableCaptionPanel = this.pOwner.pOwner as IExpandableCaptionPanel;
                if (pExpandableCaptionPanel == null) return base.Visible;
                switch (pExpandableCaptionPanel.eCaptionAlignment)
                {
                    case TabAlignment.Top:
                    case TabAlignment.Bottom:
                        switch (pExpandableCaptionPanel.eExpandButtonStyle)
                        {
                            case ExpandButtonStyle.eTopToBottom:
                            case ExpandButtonStyle.eBottomToTop:
                                return pExpandableCaptionPanel.ShowCloseButton;
                            case ExpandButtonStyle.eLeftToRight:
                            case ExpandButtonStyle.eRightToLeft:
                                return pExpandableCaptionPanel.IsExpand && pExpandableCaptionPanel.ShowCloseButton;
                        }
                        return pExpandableCaptionPanel.ShowCloseButton;
                    case TabAlignment.Left:
                    case TabAlignment.Right:
                        switch (pExpandableCaptionPanel.eExpandButtonStyle)
                        {
                            case ExpandButtonStyle.eTopToBottom:
                            case ExpandButtonStyle.eBottomToTop:
                                return pExpandableCaptionPanel.IsExpand && pExpandableCaptionPanel.ShowCloseButton;
                            case ExpandButtonStyle.eLeftToRight:
                            case ExpandButtonStyle.eRightToLeft:
                                return pExpandableCaptionPanel.ShowCloseButton;
                        }
                        return pExpandableCaptionPanel.ShowCloseButton;
                    default:
                        return pExpandableCaptionPanel.ShowCloseButton;
                }
            }
            set
            {
                base.Visible = value;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            //
            if (this.pOwner == null) return;
            IExpandableCaptionPanel pExpandableCaptionPanel = this.pOwner.pOwner as IExpandableCaptionPanel;
            if (pExpandableCaptionPanel == null) return;
            pExpandableCaptionPanel.Visible = false;
        }

        protected override void OnDraw(PaintEventArgs pevent)
        {
            //base.OnDraw(pevent);
            WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderExpandableCaptionPanelButton(new ObjectRenderEventArgs(pevent.Graphics, this, this.DisplayRectangle));
        }
    }
}
