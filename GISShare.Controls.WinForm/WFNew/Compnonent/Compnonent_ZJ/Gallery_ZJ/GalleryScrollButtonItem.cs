using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public class GalleryScrollButtonItem : BaseButtonItem, IGalleryScrollButton, IViewDepend
    {
        public GalleryScrollButtonItem(GalleryScrollButtonStyle galleryScrollButtonStyle)
        {
            base.Name = "GISShare.Controls.WinForm.WFNew.GalleryScrollButtonItem";
            base.Text = "画廊滚动按钮[上/下]";
            base.UsingViewOverflow = false;
            
            //
            this.m_eGalleryScrollButtonStyle = galleryScrollButtonStyle;
        }

        private GalleryScrollButtonStyle m_eGalleryScrollButtonStyle = GalleryScrollButtonStyle.eScrollUpButton;
        public GalleryScrollButtonStyle eGalleryScrollButtonStyle
        {
            get { return m_eGalleryScrollButtonStyle; }
        }

        public override DismissPopupStyle eDismissPopupStyle
        {
            get
            {
                return DismissPopupStyle.eNoDismiss;
            }
        }

        ViewDependStyle IViewDepend.eViewDependStyle
        {
            get
            {
                return ViewDependStyle.eInOwnerDisplayRectangle;
            }
        }

        public override DisplayStyle eDisplayStyle
        {
            get
            {
                return DisplayStyle.eNone;
            }
            set
            {
                base.eDisplayStyle = DisplayStyle.eNone;
            }
        }

        public override System.Drawing.ContentAlignment TextAlign
        {
            get
            {
                return System.Drawing.ContentAlignment.MiddleCenter;
            }
            set
            {
                base.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            }
        }

        public override bool ShowNomalState
        {
            get
            {
                return true;
            }
            set
            {
                base.ShowNomalState = true;
            }
        }

        protected override void OnDraw(System.Windows.Forms.PaintEventArgs e)
        {
            //base.OnDraw(e);
            //
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderGalleryScrollButton(
                new GISShare.Controls.WinForm.ObjectRenderEventArgs(e.Graphics, this, this.DisplayRectangle));
        }
    }
}
