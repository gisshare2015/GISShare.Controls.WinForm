using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GISShare.Controls.WinForm.DockBar
{
    public class DockBarFloatFormButtonItem : WFNew.BaseButtonItem, IDockBarFloatFormButton
    {
        public DockBarFloatFormButtonItem(DockBarFloatFormButtonStyle dockBarFloatFormButtonStyle)
        {
            this.m_eDockBarFloatFormButtonStyle = dockBarFloatFormButtonStyle;
        }

        public System.Windows.Forms.Form OperationForm
        {
            get 
            {
                return this.TryGetDependParentForm(); 
            }
        }

        private DockBarFloatFormButtonStyle m_eDockBarFloatFormButtonStyle = DockBarFloatFormButtonStyle.eCloseButton;
        public DockBarFloatFormButtonStyle eDockBarFloatFormButtonStyle
        {
            get
            {
                return m_eDockBarFloatFormButtonStyle;
            }
        }

        public Rectangle GlyphRectangle
        {
            get
            {
                return DisplayRectangle;
            }
        }

        public override bool Visible
        {
            get
            {
                return true;
            }
            set
            {
                base.Visible = value;
            }
        }

        public override WFNew.DisplayStyle eDisplayStyle
        {
            get
            {
                return WFNew.DisplayStyle.eNone;
            }
            set
            {
                base.eDisplayStyle = WFNew.DisplayStyle.eNone;
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

        public override Rectangle DisplayRectangle
        {
            get
            {
                return new Rectangle(this.Location, new System.Drawing.Size(15, 15));
            }
        }

        public override bool ShowNomalState
        {
            get
            {
                return false;
            }
            set
            {
                base.ShowNomalState = value;
            }
        }

        protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            //
            if (this.OperationForm == null) return;
            //
            DockBarFloatForm form = this.TryGetDependParentForm() as DockBarFloatForm;
            if (form == null) return;
            switch (this.eDockBarFloatFormButtonStyle)
            {
                case DockBarFloatFormButtonStyle.eCloseButton:
                    form.Close();
                    break;
                case DockBarFloatFormButtonStyle.eContextButton:
                    AddOrRemoveDropDownList addOrRemoveDropDownList = new AddOrRemoveDropDownList(form.pDockBar);
                    addOrRemoveDropDownList.Show(form, new Point(this.DisplayRectangle.Left, this.DisplayRectangle.Bottom));
                    break;
                default:
                    break;
            }
        }

        protected override void OnDraw(System.Windows.Forms.PaintEventArgs e)
        {
            //base.OnDraw(e);
            //
            GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderFloatFormButton(
                new GISShare.Controls.WinForm.ObjectRenderEventArgs(e.Graphics, this, this.DisplayRectangle));
        }
    }

    
}
