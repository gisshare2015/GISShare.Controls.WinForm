using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew.DockPanel
{
    [Designer(typeof(GISShare.Controls.WinForm.WFNew.DockPanel.Design.DocumentAreaDesigner))]
    public class DocumentArea : WFNew.AreaControlCC
    {
        private const int CTR_TOPSPACE = 0;

        public DocumentArea()
            : base()
        {
            base.Name = "DocumentArea";
            base.Dock = DockStyle.Fill;
        }

        public override Rectangle DisplayRectangle
        {
            get
            {
                Rectangle rectangle = base.DisplayRectangle;
                return Rectangle.FromLTRB(rectangle.Left, rectangle.Top + CTR_TOPSPACE, rectangle.Right, rectangle.Bottom);
            }
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case (int)GISShare.Win32.Msgs.WM_MOUSEACTIVATE://0x0021当光标在某个非激活的窗口中而用户正按着鼠标的某个键发送此消息给当前窗口
                    Form parentForm = this.ParentForm;
                    if (parentForm != null)
                    {
                        for (int i = 0; i < parentForm.Controls.Count; i++)
                        {
                            DockPanelHidePanel dockPanelHidePanel = parentForm.Controls[i] as DockPanelHidePanel;
                            if (dockPanelHidePanel == null) continue;
                            if (!dockPanelHidePanel.Visible) continue;
                            if (dockPanelHidePanel.DockPanel == null) continue;
                            ((ISetDockPanelHelper)dockPanelHidePanel.DockPanel).SetActiveState(null);
                            break;
                        }
                    }
                    break;
                default:
                    break;
            }
            base.WndProc(ref m);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.Refresh();
        }

        public override object Clone()
        {
            return new DocumentArea();
        }

        public override bool LockHeight
        {
            get { return false; }
        }

        public override bool LockWith
        {
            get { return false; }
        }
    }
}
