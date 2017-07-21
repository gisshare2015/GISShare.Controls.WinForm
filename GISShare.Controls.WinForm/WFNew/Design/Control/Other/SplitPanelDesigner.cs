using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace GISShare.Controls.WinForm.WFNew.Design
{
    public class SplitPanelDesigner : ParentControlDesigner
    {
        private SplitPanel m_SplitPanel = null;

        private DesignerVerbCollection verbs;

        public override DesignerVerbCollection Verbs
        {
            get
            {
                if (verbs == null)
                {
                    verbs = new DesignerVerbCollection();
                    verbs.Add(new DesignerVerb("顶部停靠", new EventHandler(DockToTop)));
                    verbs.Add(new DesignerVerb("左边停靠", new EventHandler(DockToLeft)));
                    verbs.Add(new DesignerVerb("右边停靠", new EventHandler(DockToRight)));
                    verbs.Add(new DesignerVerb("底部停靠", new EventHandler(DockToBottom)));
                    verbs.Add(new DesignerVerb("关于...", new EventHandler(ShowInfo)));
                }
                return verbs;
            }
        }

        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            this.m_SplitPanel = base.Component as SplitPanel;
        }

        protected override void OnPaintAdornments(PaintEventArgs pea)
        {
            base.OnPaintAdornments(pea);
            //
            if (this.m_SplitPanel.Height < 16) return;
            //
            if (this.m_SplitPanel != null)
            {
                using (Pen p = new Pen(GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.WFNewColorTable.ArrowDisabled, 1))
                {
                    Rectangle rectangle = this.m_SplitPanel.DisplayRectangle;
                    rectangle = Rectangle.FromLTRB(rectangle.Left, rectangle.Top, rectangle.Right - 1, rectangle.Bottom - 1);
                    p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    pea.Graphics.DrawRectangle(p, rectangle);
                    //
                    StringFormat drawFormat = new StringFormat();
                    drawFormat.FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.FitBlackBox;
                    drawFormat.Trimming = StringTrimming.EllipsisCharacter;
                    SizeF size = pea.Graphics.MeasureString(this.m_SplitPanel.Text, this.m_SplitPanel.Font);
                    int iWidth = (int)(size.Width + 1);
                    int iHeight = (int)(size.Height + 1);
                    pea.Graphics.DrawString(this.m_SplitPanel.Text,
                        this.m_SplitPanel.Font,
                        new SolidBrush(GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.WFNewColorTable.ItemText),
                        new Rectangle((rectangle.Left + rectangle.Right - iWidth) / 2, (rectangle.Top + rectangle.Bottom - iHeight) / 2, iWidth, iHeight),
                        drawFormat);
                }
            }
        }

        private void DockToTop(object sender, EventArgs e)
        {
            if (this.m_SplitPanel.Dock == DockStyle.Top) return;
            //
            switch (this.m_SplitPanel.Dock)
            {
                case DockStyle.Left:
                case DockStyle.Right:
                    int iWidth = this.m_SplitPanel.Width;
                    this.m_SplitPanel.Dock = DockStyle.Top;
                    this.m_SplitPanel.Height = iWidth;
                    break;
                default:
                    this.m_SplitPanel.Dock = DockStyle.Top;
                    break;
            }
        }
        private void DockToLeft(object sender, EventArgs e)
        {
            if (this.m_SplitPanel.Dock == DockStyle.Left) return;
            //
            switch (this.m_SplitPanel.Dock)
            {
                case DockStyle.Top:
                case DockStyle.Bottom:
                    int iHeight = this.m_SplitPanel.Height;
                    this.m_SplitPanel.Dock = DockStyle.Left;
                    this.m_SplitPanel.Width = iHeight;
                    break;
                default:
                    this.m_SplitPanel.Dock = DockStyle.Left;
                    break;
            }
        }
        private void DockToRight(object sender, EventArgs e)
        {
            if (this.m_SplitPanel.Dock == DockStyle.Right) return;
            //
            switch (this.m_SplitPanel.Dock)
            {
                case DockStyle.Top:
                case DockStyle.Bottom:
                    int iHeight = this.m_SplitPanel.Height;
                    this.m_SplitPanel.Dock = DockStyle.Right;
                    this.m_SplitPanel.Width = iHeight;
                    break;
                default:
                    this.m_SplitPanel.Dock = DockStyle.Right;
                    break;
            }
        }
        private void DockToBottom(object sender, EventArgs e)
        {
            if (this.m_SplitPanel.Dock == DockStyle.Bottom) return;
            //
            switch (this.m_SplitPanel.Dock)
            {
                case DockStyle.Left:
                case DockStyle.Right:
                    int iWidth = this.m_SplitPanel.Width;
                    this.m_SplitPanel.Dock = DockStyle.Bottom;
                    this.m_SplitPanel.Height = iWidth;
                    break;
                default:
                    this.m_SplitPanel.Dock = DockStyle.Bottom;
                    break;
            }
        }

        private void ShowInfo(object sender, EventArgs e)
        {
            InfoForm infoForm = new InfoForm();
            infoForm.ShowDialog();
        }
    }
}
