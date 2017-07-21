using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    public class LTBRButtonItem : BaseButtonItem, ILTBRButton, IViewDepend
    {
        public LTBRButtonItem(LTBRButtonStyle lTBRButtonStyle)
        {
            base.Name = "GISShare.Controls.WinForm.WFNew.LTBRButtonItem";
            base.Text = "翻页按钮[上/下/左/右]";
            base.UsingViewOverflow = false;
            
            //
            this.m_eLTBRButtonStyle = lTBRButtonStyle;
        }

        private LTBRButtonStyle m_eLTBRButtonStyle = LTBRButtonStyle.eTopButton;
        public virtual LTBRButtonStyle eLTBRButtonStyle
        {
            get { return m_eLTBRButtonStyle; }
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
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderLTBRButton(
                new GISShare.Controls.WinForm.ObjectRenderEventArgs(e.Graphics, this, this.DisplayRectangle));
        }
    }
}
