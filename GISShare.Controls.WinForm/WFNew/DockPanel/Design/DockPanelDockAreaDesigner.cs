﻿using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace GISShare.Controls.WinForm.WFNew.DockPanel.Design
{
    public class DockPanelDockAreaDesigner : ParentControlDesigner
    {
        private DockPanelDockArea m_DockPanelDockArea = null;

        private DesignerVerbCollection verbs;

        public override DesignerVerbCollection Verbs
        {
            get
            {
                if (verbs == null)
                {
                    verbs = new DesignerVerbCollection();
                    verbs.Add(new DesignerVerb("关于...", new EventHandler(ShowInfo)));
                }
                return verbs;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                try
                {
                    if (this.m_DockPanelDockArea != null &&
                        !this.m_DockPanelDockArea.IsDisposed &&
                        this.m_DockPanelDockArea.DockPanelManager != null &&
                        this.m_DockPanelDockArea.DockPanelManager.DockPanelDockAreas.Contains(this.m_DockPanelDockArea))
                    {
                        this.m_DockPanelDockArea.DockPanelManager.DockPanelDockAreas.Remove(this.m_DockPanelDockArea);
                    }
                }
                catch { }
            }
            base.Dispose(disposing);
        }

        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            //
            this.m_DockPanelDockArea = base.Component as DockPanelDockArea;
        }

        protected override void OnPaintAdornments(PaintEventArgs pea)
        {
            base.OnPaintAdornments(pea);
            //
            if (this.m_DockPanelDockArea.Height < 16) return;
            //
            if (this.m_DockPanelDockArea != null)
            {
                using (Pen p = new Pen(GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.WFNewColorTable.ArrowDisabled, 1))
                {
                    Rectangle rectangle = this.m_DockPanelDockArea.DisplayRectangle;
                    rectangle = Rectangle.FromLTRB(rectangle.Left, rectangle.Top, rectangle.Right - 1, rectangle.Bottom - 1);
                    p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    pea.Graphics.DrawRectangle(p, rectangle);
                    //
                    StringFormat drawFormat = new StringFormat();
                    drawFormat.FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.FitBlackBox;
                    drawFormat.Trimming = StringTrimming.EllipsisCharacter;
                    SizeF size = pea.Graphics.MeasureString(this.m_DockPanelDockArea.Text, this.m_DockPanelDockArea.Font);
                    int iWidth = (int)(size.Width + 1);
                    int iHeight = (int)(size.Height + 1);
                    pea.Graphics.DrawString(this.m_DockPanelDockArea.Text,
                        this.m_DockPanelDockArea.Font,
                        new SolidBrush(GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.WFNewColorTable.ItemText),
                        new Rectangle((rectangle.Left + rectangle.Right - iWidth) / 2, (rectangle.Top + rectangle.Bottom - iHeight) / 2, iWidth, iHeight),
                        drawFormat);
                }
            }
        }

        private void ShowInfo(object sender, EventArgs e)
        {
            InfoForm infoForm = new InfoForm();
            infoForm.ShowDialog();
        }
    }
}
