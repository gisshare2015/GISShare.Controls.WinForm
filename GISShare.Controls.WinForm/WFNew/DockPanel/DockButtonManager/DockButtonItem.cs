using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew.DockPanel
{
    class DockButtonItem : WFNew.BaseButtonItem, IDockButtonItem
    {
        public DockButtonItem(DockButtonStyle dockButtonStyle)
        {
            this.m_eDockButtonStyle = dockButtonStyle;
            //
            switch (this.eDockButtonStyle) 
            {
                case DockButtonStyle.eCenterToDockFill:
                    base.Image = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("GISShare.Controls.WinForm.WFNew.DockPanel.Image.CenterToDockFill.png"));
                    base.Size = new Size(43, 43);
                    break;
                    //
                case DockButtonStyle.eCenterToDocumentUp:
                    base.Image = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("GISShare.Controls.WinForm.WFNew.DockPanel.Image.CenterToDocumentUp.bmp"));
                    base.Size = new Size(30, 29);
                    break;
                case DockButtonStyle.eCenterToDocumentLeft:
                    base.Image = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("GISShare.Controls.WinForm.WFNew.DockPanel.Image.CenterToDocumentLeft.bmp"));
                    base.Size = new Size(29, 30);
                    break;
                case DockButtonStyle.eCenterToDocumentRight:
                    base.Image = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("GISShare.Controls.WinForm.WFNew.DockPanel.Image.CenterToDocumentRight.bmp"));
                    base.Size = new Size(29, 30);
                    break;
                case DockButtonStyle.eCenterToDocumentBottom:
                    base.Image = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("GISShare.Controls.WinForm.WFNew.DockPanel.Image.CenterToDocumentBottom.bmp"));
                    base.Size = new Size(30, 29);
                    break;
                    //
                case DockButtonStyle.eCenterToDockUp:
                    base.Image = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("GISShare.Controls.WinForm.WFNew.DockPanel.Image.CenterToDockUp.bmp"));
                    base.Size = new Size(30, 29);
                    break;
                case DockButtonStyle.eCenterToDockLeft:
                    base.Image = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("GISShare.Controls.WinForm.WFNew.DockPanel.Image.CenterToDockLeft.bmp"));
                    base.Size = new Size(29, 30);
                    break;
                case DockButtonStyle.eCenterToDockRight:
                    base.Image = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("GISShare.Controls.WinForm.WFNew.DockPanel.Image.CenterToDockRight.bmp"));
                    base.Size = new Size(29, 30);
                    break;
                case DockButtonStyle.eCenterToDockBottom:
                    base.Image = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("GISShare.Controls.WinForm.WFNew.DockPanel.Image.CenterToDockBottom.bmp"));
                    base.Size = new Size(30, 29);
                    break;
                    //
                case DockButtonStyle.eToDockUp:
                    base.Image = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("GISShare.Controls.WinForm.WFNew.DockPanel.Image.ToDockUp.bmp"));
                    base.Size = new Size(30, 29);
                    break;
                case DockButtonStyle.eToDockLeft:
                    base.Image = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("GISShare.Controls.WinForm.WFNew.DockPanel.Image.ToDockLeft.bmp"));
                    base.Size = new Size(29, 30);
                    break;
                case DockButtonStyle.eToDockRight:
                    base.Image = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("GISShare.Controls.WinForm.WFNew.DockPanel.Image.ToDockRight.bmp"));
                    base.Size = new Size(29, 30);
                    break;
                case DockButtonStyle.eToDockBottom:
                    base.Image = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("GISShare.Controls.WinForm.WFNew.DockPanel.Image.ToDockBottom.bmp"));
                    base.Size = new Size(30, 29);
                    break;
                default:
                    break;
            }
        }

        #region IDockButtonItem
        DockButtonStyle m_eDockButtonStyle = DockButtonStyle.eCenterToDockFill;
        public DockButtonStyle eDockButtonStyle
        {
            get { return m_eDockButtonStyle; }
        }
        #endregion

        private BaseItemState m_eBaseItemState = BaseItemState.eNormal;
        public override BaseItemState eBaseItemState
        {
            get
            {
                if (!this.Visible) return GISShare.Controls.WinForm.WFNew.BaseItemState.eDisabled;
                return m_eBaseItemState;
            }
        }

        internal void SetBaseItemStateValue(GISShare.Controls.WinForm.WFNew.BaseItemState baseItemState)
        {
            if (this.m_eBaseItemState == baseItemState) return;
            this.m_eBaseItemState = baseItemState;
            this.Refresh();
        }

        public override bool Overflow
        {
            get
            {
                return false;
            }
        }

        public override GISShare.Controls.WinForm.WFNew.DisplayStyle eDisplayStyle
        {
            get
            {
                return GISShare.Controls.WinForm.WFNew.DisplayStyle.eNone;
            }
            set
            {
                base.eDisplayStyle = value;
            }
        }

        protected override void OnDraw(System.Windows.Forms.PaintEventArgs pevent)
        {
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderDockButton(new ObjectRenderEventArgs(pevent.Graphics, this, this.DisplayRectangle));
        }
    }
}
