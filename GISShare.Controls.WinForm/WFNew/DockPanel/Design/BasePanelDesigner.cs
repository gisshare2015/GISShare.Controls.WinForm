using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Runtime.InteropServices;

namespace GISShare.Controls.WinForm.WFNew.DockPanel.Design
{
    public class BasePanelDesigner : ScrollableControlDesigner
    {
        private BasePanel m_BasePanel = null;

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

        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            //
            this.m_BasePanel = base.Component as BasePanel;
            if (this.m_BasePanel == null)
            {
                this.DisplayError(new ArgumentException("BasePanel == null"));
                return;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                try
                {
                    if (this.m_BasePanel != null &&
                        !this.m_BasePanel.IsDisposed &&
                        this.m_BasePanel.DockPanelManager != null &&
                        this.m_BasePanel.DockPanelManager.BasePanels.Contains(this.m_BasePanel))
                    {
                        this.m_BasePanel.DockPanelManager.BasePanels.Remove(this.m_BasePanel);
                        //
                        ITabControlHelper pTabControlHelper = this.m_BasePanel.Parent as ITabControlHelper;
                        if (pTabControlHelper != null)
                        {
                            pTabControlHelper.TabButtonItemCollection.Remove(this.m_BasePanel.pTabButtonItem as BaseItem);
                        }
                    }
                }
                catch { }
            }
            base.Dispose(disposing);
        }

        protected override void OnPaintAdornments(PaintEventArgs pea)
        {
            base.OnPaintAdornments(pea);
            //
            if (this.m_BasePanel.Height < 16) return;
            //
            if (m_BasePanel != null)
            {
                using (Pen p = new Pen(GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.WFNewColorTable.ArrowDisabled, 1))
                {
                    Rectangle rectangle = this.m_BasePanel.DisplayRectangle;
                    rectangle = Rectangle.FromLTRB(rectangle.Left, rectangle.Top, rectangle.Right - 1, rectangle.Bottom - 1);
                    p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    pea.Graphics.DrawRectangle(p, rectangle);
                    //
                    StringFormat drawFormat = new StringFormat();
                    drawFormat.FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.FitBlackBox;
                    drawFormat.Trimming = StringTrimming.EllipsisCharacter;
                    SizeF size = pea.Graphics.MeasureString(this.m_BasePanel.Text, this.m_BasePanel.Font);
                    int iWidth = (int)(size.Width + 1);
                    int iHeight = (int)(size.Height + 1);
                    pea.Graphics.DrawString(this.m_BasePanel.Text,
                        this.m_BasePanel.Font,
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