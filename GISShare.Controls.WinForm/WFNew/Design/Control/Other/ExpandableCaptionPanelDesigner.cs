using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace GISShare.Controls.WinForm.WFNew.Design
{
    public class ExpandableCaptionPanelDesigner : ParentControlDesigner
    {
        private ExpandableCaptionPanel m_ExpandableCaptionPanel = null;

        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            this.m_ExpandableCaptionPanel = base.Component as ExpandableCaptionPanel;
        }

        public override DesignerVerbCollection Verbs
        {
            get
            {
                DesignerVerbCollection verbs = new DesignerVerbCollection();
                //
                verbs.Add(new DesignerVerb("顶部停靠", new EventHandler(DockToTop)));
                verbs.Add(new DesignerVerb("左边停靠", new EventHandler(DockToLeft)));
                verbs.Add(new DesignerVerb("右边停靠", new EventHandler(DockToRight)));
                verbs.Add(new DesignerVerb("底部停靠", new EventHandler(DockToBottom)));
                verbs.Add(new DesignerVerb("关于...", new EventHandler(ShowInfo)));
                //
                return verbs;
            }
        }

        protected override void OnPaintAdornments(PaintEventArgs pea)
        {
            base.OnPaintAdornments(pea);
            //
            if (!this.m_ExpandableCaptionPanel.ShowCaption) { if (this.m_ExpandableCaptionPanel.Height < 16) return; }
            else { if (this.m_ExpandableCaptionPanel.Height < this.m_ExpandableCaptionPanel.CaptionHeight + 16) return; }
            //
            if (m_ExpandableCaptionPanel != null && m_ExpandableCaptionPanel.IsExpand)
            {
                using (Pen p = new Pen(GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.WFNewColorTable.ArrowDisabled, 1))
                {
                    Rectangle rectangle = this.m_ExpandableCaptionPanel.DisplayRectangle;
                    rectangle = Rectangle.FromLTRB(rectangle.Left, rectangle.Top, rectangle.Right - 1, rectangle.Bottom - 1);
                    p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    pea.Graphics.DrawRectangle(p, rectangle);
                    //
                    StringFormat drawFormat = new StringFormat();
                    drawFormat.FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.FitBlackBox;
                    drawFormat.Trimming = StringTrimming.EllipsisCharacter;
                    SizeF size = pea.Graphics.MeasureString(this.m_ExpandableCaptionPanel.Text, this.m_ExpandableCaptionPanel.Font);
                    int iWidth = (int)(size.Width + 1);
                    int iHeight = (int)(size.Height + 1);
                    pea.Graphics.DrawString(this.m_ExpandableCaptionPanel.Text,
                        this.m_ExpandableCaptionPanel.Font,
                        new SolidBrush(GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.WFNewColorTable.ItemText),
                        new Rectangle((rectangle.Left + rectangle.Right - iWidth) / 2, (rectangle.Top + rectangle.Bottom - iHeight) / 2, iWidth, iHeight),
                        drawFormat);
                }
            }
        }

        protected override void WndProc(ref Message m)
        {
            try
            {
                if (this.m_ExpandableCaptionPanel.Created)
                {
                    switch (m.Msg)
                    {
                        case (int)GISShare.Win32.Msgs.WM_LBUTTONDOWN://0x201
                            Point point = GISShare.Win32.NativeMethods.LParamToMouseLocation((int)m.LParam);
                            if (
                                (this.m_ExpandableCaptionPanel.ShowTreeNodeButton &&
                                this.m_ExpandableCaptionPanel.GetTreeNodeButtonRectangle().Contains(point))
                                ||
                                (this.m_ExpandableCaptionPanel.ShowExpandButton &&
                                this.m_ExpandableCaptionPanel.GetExpandButtonRectangle().Contains(point))
                                ) 
                            {
                                ISetExpandableCaptionPanelHelper pSetExpandableCaptionPanelHelper = this.m_ExpandableCaptionPanel as ISetExpandableCaptionPanelHelper;
                                if (pSetExpandableCaptionPanelHelper == null) return;
                                pSetExpandableCaptionPanelHelper.SetIsExpand(!this.m_ExpandableCaptionPanel.IsExpand);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            finally
            {
                base.WndProc(ref m);
            }
        }

        private void DockToTop(object sender, EventArgs e)
        {
            if (this.m_ExpandableCaptionPanel.Dock == DockStyle.Top) return;
            //
            switch (this.m_ExpandableCaptionPanel.Dock)
            {
                case DockStyle.Left:
                case DockStyle.Right:
                    int iWidth = this.m_ExpandableCaptionPanel.Width;
                    this.m_ExpandableCaptionPanel.Dock = DockStyle.Top;
                    this.m_ExpandableCaptionPanel.Height = iWidth;
                    break;
                default:
                    this.m_ExpandableCaptionPanel.Dock = DockStyle.Top;
                    break;
            }
        }
        private void DockToLeft(object sender, EventArgs e)
        {
            if (this.m_ExpandableCaptionPanel.Dock == DockStyle.Left) return;
            //
            switch (this.m_ExpandableCaptionPanel.Dock)
            {
                case DockStyle.Top:
                case DockStyle.Bottom:
                    int iHeight = this.m_ExpandableCaptionPanel.Height;
                    this.m_ExpandableCaptionPanel.Dock = DockStyle.Left;
                    this.m_ExpandableCaptionPanel.Width = iHeight;
                    break;
                default:
                    this.m_ExpandableCaptionPanel.Dock = DockStyle.Left;
                    break;
            }
        }
        private void DockToRight(object sender, EventArgs e)
        {
            if (this.m_ExpandableCaptionPanel.Dock == DockStyle.Right) return;
            //
            switch (this.m_ExpandableCaptionPanel.Dock)
            {
                case DockStyle.Top:
                case DockStyle.Bottom:
                    int iHeight = this.m_ExpandableCaptionPanel.Height;
                    this.m_ExpandableCaptionPanel.Dock = DockStyle.Right;
                    this.m_ExpandableCaptionPanel.Width = iHeight;
                    break;
                default:
                    this.m_ExpandableCaptionPanel.Dock = DockStyle.Right;
                    break;
            }
        }
        private void DockToBottom(object sender, EventArgs e)
        {
            if (this.m_ExpandableCaptionPanel.Dock == DockStyle.Bottom) return;
            //
            switch (this.m_ExpandableCaptionPanel.Dock)
            {
                case DockStyle.Left:
                case DockStyle.Right:
                    int iWidth = this.m_ExpandableCaptionPanel.Width;
                    this.m_ExpandableCaptionPanel.Dock = DockStyle.Bottom;
                    this.m_ExpandableCaptionPanel.Height = iWidth;
                    break;
                default:
                    this.m_ExpandableCaptionPanel.Dock = DockStyle.Bottom;
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
