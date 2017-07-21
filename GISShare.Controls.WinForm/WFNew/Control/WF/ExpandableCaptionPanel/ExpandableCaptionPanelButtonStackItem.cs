using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    class ExpandableCaptionPanelButtonStackItem : WFNew.BaseItemStackItem
    {
        ExpandButtonECPItem m_ExpandButtonECPItem;
        CloseButtonECPItem m_CloseButtonECPItem;

        public ExpandableCaptionPanelButtonStackItem()
        {
            base.Name = "ExpandableCaptionPanelButtonStackItem";
            base.Text = "ExpandableCaptionPanelButtonStackItem";
            //
            this.m_ExpandButtonECPItem = new ExpandButtonECPItem();
            this.BaseItems.Add(this.m_ExpandButtonECPItem);
            this.m_CloseButtonECPItem = new CloseButtonECPItem();
            this.BaseItems.Add(this.m_CloseButtonECPItem);
            ((WFNew.ILockCollectionHelper)this.BaseItems).SetLocked(true);
        }

        public override System.Windows.Forms.Orientation eOrientation
        {
            get
            {
                IExpandableCaptionPanel pExpandableCaptionPanel = this.pOwner as IExpandableCaptionPanel;
                if (pExpandableCaptionPanel == null) return base.eOrientation;
                switch (pExpandableCaptionPanel.eCaptionAlignment)
                {
                    case TabAlignment.Top:
                    case TabAlignment.Bottom:
                        return Orientation.Horizontal;
                    case TabAlignment.Left:
                    case TabAlignment.Right:
                        return Orientation.Vertical;
                    default:
                        return base.eOrientation;
                }
            }
            set
            {
                base.eOrientation =  System.Windows.Forms.Orientation.Horizontal;
            }
        }

        public override bool LockWith
        {
            get
            {
                return true;
            }
            set
            {
                base.LockWith = value;
            }
        }

        public override int ColumnDistance
        {
            get
            {
                return 1;
            }
            set
            {
                base.ColumnDistance = 1;
            }
        }

        public override bool Visible
        {
            get
            {
                //if (this.pOwner == null) return base.Visible;
                IExpandableCaptionPanel pExpandableCaptionPanel = this.pOwner as IExpandableCaptionPanel;
                if (pExpandableCaptionPanel == null) return base.Visible;
                return pExpandableCaptionPanel.ShowCaption && this.HaveVisibleBaseItem;
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
                if (pExpandableCaptionPanel.IsExpand)
                {
                    switch (pExpandableCaptionPanel.eCaptionAlignment)
                    {
                        case TabAlignment.Left:
                        case TabAlignment.Right:
                            return new Rectangle(new Point((rectangle.Left + rectangle.Right - this.Width) / 2, rectangle.Bottom - this.Height - 1), this.Size);
                        case TabAlignment.Top:
                        case TabAlignment.Bottom:
                        default:
                            return new Rectangle(new Point(rectangle.Right - this.Width - 1, (rectangle.Top + rectangle.Bottom - this.Height) / 2), this.Size);
                    }
                }
                else
                {
                    switch (pExpandableCaptionPanel.eCaptionAlignment)
                    {
                        case TabAlignment.Left:
                        case TabAlignment.Right:
                            switch (pExpandableCaptionPanel.eExpandButtonStyle)
                            {
                                case ExpandButtonStyle.eTopToBottom:
                                case ExpandButtonStyle.eBottomToTop:
                                    return new Rectangle(new Point((rectangle.Left + rectangle.Right - 17) / 2, (rectangle.Top + rectangle.Bottom - 17) / 2), new Size(17, 17));
                                case ExpandButtonStyle.eLeftToRight:
                                case ExpandButtonStyle.eRightToLeft:
                                    return new Rectangle(new Point((rectangle.Left + rectangle.Right - this.Width) / 2, rectangle.Bottom - this.Height - 1), this.Size);
                            }
                            return new Rectangle(new Point((rectangle.Left + rectangle.Right - this.Width) / 2, rectangle.Bottom - this.Height - 1), this.Size);
                        case TabAlignment.Top:
                        case TabAlignment.Bottom:
                        default:
                            switch (pExpandableCaptionPanel.eExpandButtonStyle)
                            {
                                case ExpandButtonStyle.eTopToBottom:
                                case ExpandButtonStyle.eBottomToTop:
                                    return new Rectangle(new Point(rectangle.Right - this.Width - 1, (rectangle.Top + rectangle.Bottom - this.Height) / 2), this.Size);
                                case ExpandButtonStyle.eLeftToRight:
                                case ExpandButtonStyle.eRightToLeft:
                                    return new Rectangle(new Point((rectangle.Left + rectangle.Right - 17) / 2, (rectangle.Top + rectangle.Bottom - 17) / 2), new Size(17, 17));
                            }
                            return new Rectangle(new Point(rectangle.Right - this.Width - 1, (rectangle.Top + rectangle.Bottom - this.Height) / 2), this.Size);
                    }
                }
            }
        }

        protected override void OnDraw(PaintEventArgs e)
        {
            this.Relayout(e.Graphics, LayoutStyle.eLayoutPlan, true);
            this.Relayout(e.Graphics, LayoutStyle.eLayoutAuto, false);
            //base.OnDraw(e);
        }
    }
}
