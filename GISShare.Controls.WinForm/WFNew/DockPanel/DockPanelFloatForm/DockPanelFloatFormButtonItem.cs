using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew.DockPanel
{
    class DockPanelFloatFormButtonItem : WFNew.BaseButtonItem, IDockPanelFloatFormButtonItem
    {
        public DockPanelFloatFormButtonItem(DockPanelFloatFormButtonItemStyle dockPanelFloatFormButtonItem)
        {
            this.m_eDockPanelFloatFormButtonItemStyle = dockPanelFloatFormButtonItem;
            this.Size = new Size(21, 21);
        }

        public System.Windows.Forms.Form OperationForm
        {
            get
            {
                return this.TryGetDependParentForm();
            }
        }

        private DockPanelFloatFormButtonItemStyle m_eDockPanelFloatFormButtonItemStyle = DockPanelFloatFormButtonItemStyle.eCloseButton;
        public DockPanelFloatFormButtonItemStyle eDockPanelFloatFormButtonItemStyle
        {
            get
            {
                return m_eDockPanelFloatFormButtonItemStyle;
            }
        }

        public override bool Visible
        {
            get
            {
                switch (this.eDockPanelFloatFormButtonItemStyle)
                {
                    case  DockPanelFloatFormButtonItemStyle.eCloseButton:
                        IDockPanelFloatForm pDockPanelFloatForm = this.OperationForm as IDockPanelFloatForm;
                        if (pDockPanelFloatForm == null) return base.Visible;
                        IDockPanel pDockPanel = pDockPanelFloatForm.GetDockPanel();
                        if (pDockPanel == null) return base.Visible;
                        return pDockPanel.CanClose;
                    case DockPanelFloatFormButtonItemStyle.eMaxButton:
                    default:
                        return base.Visible;
                }
            }
            set
            {
                base.Visible = value;
            }
        }

        public Rectangle GlyphRectangle
        {
            get
            {
                switch (this.eDockPanelFloatFormButtonItemStyle) 
                {
                    case DockPanelFloatFormButtonItemStyle.eMaxButton:
                        return Util.UtilTX.CreateRectangle(this.DisplayRectangle, 9, 9);
                    case DockPanelFloatFormButtonItemStyle.eCloseButton:
                    default:
                        return Util.UtilTX.CreateRectangle(this.DisplayRectangle, 15, 15);
                }
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

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            //
            switch (this.eDockPanelFloatFormButtonItemStyle)
            {
                case DockPanelFloatFormButtonItemStyle.eMaxButton:
                    System.Windows.Forms.Form form = this.OperationForm;
                    if (form == null) return;
                    if (form.WindowState == FormWindowState.Maximized) { form.WindowState = FormWindowState.Normal; }
                    else { form.WindowState = FormWindowState.Maximized; }
                    break;
                case DockPanelFloatFormButtonItemStyle.eCloseButton:
                    System.Windows.Forms.Form form2 = this.OperationForm;
                    if (form2 != null) { form2.Close(); }
                    break;
            }
        }

        protected override void OnDraw(PaintEventArgs pevent)
        {
            //base.OnDraw(pevent);
            WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderDockPanelFloatFormButton(new ObjectRenderEventArgs(pevent.Graphics, this, this.DisplayRectangle));
        }
    }


}
