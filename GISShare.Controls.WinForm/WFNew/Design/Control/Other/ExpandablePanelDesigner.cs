using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace GISShare.Controls.WinForm.WFNew.Design
{
    public class ExpandablePanelDesigner : ScrollableControlDesigner
    {
        private ExpandablePanel m_ExpandablePanel = null;

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
            this.m_ExpandablePanel = base.Component as ExpandablePanel;
            if (this.m_ExpandablePanel == null)
            {
                this.DisplayError(new ArgumentException("ExpandablePanel == null"));
                return;
            }
        }

        protected override void OnPaintAdornments(PaintEventArgs pea)
        {
            base.OnPaintAdornments(pea);
            //
            if (!this.m_ExpandablePanel.ShowCaption) { if (this.m_ExpandablePanel.Height < 16) return; }
            else { if (this.m_ExpandablePanel.Height < this.m_ExpandablePanel.CaptionHeight + 16) return; }
            //
            if (m_ExpandablePanel != null && m_ExpandablePanel.IsExpand)
            {
                using (Pen p = new Pen(GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.WFNewColorTable.ArrowDisabled, 1))
                {
                    Rectangle rectangle = this.m_ExpandablePanel.DisplayRectangle;
                    rectangle = Rectangle.FromLTRB(rectangle.Left, rectangle.Top, rectangle.Right - 1, rectangle.Bottom - 1);
                    p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    pea.Graphics.DrawRectangle(p, rectangle);
                    //
                    StringFormat drawFormat = new StringFormat();
                    drawFormat.FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.FitBlackBox;
                    drawFormat.Trimming = StringTrimming.EllipsisCharacter;
                    SizeF size = pea.Graphics.MeasureString(this.m_ExpandablePanel.Text, this.m_ExpandablePanel.Font);
                    int iWidth = (int)(size.Width + 1);
                    int iHeight = (int)(size.Height + 1);
                    pea.Graphics.DrawString(this.m_ExpandablePanel.Text,
                        this.m_ExpandablePanel.Font,
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
                if (this.m_ExpandablePanel.Created)
                {
                    switch (m.Msg)
                    {
                        case (int)GISShare.Win32.Msgs.WM_LBUTTONDOWN://0x201
                            Point point = GISShare.Win32.NativeMethods.LParamToMouseLocation((int)m.LParam);
                            if (
                                (this.m_ExpandablePanel.IsCaptionExpandArea &&
                                this.m_ExpandablePanel.CaptionRectangle.Contains(point))
                                ||
                                (this.m_ExpandablePanel.ShowTreeNodeButton &&
                                this.m_ExpandablePanel.GetTreeNodeButtonRectangle().Contains(point))
                                ||
                                (this.m_ExpandablePanel.ShowExpandButton &&
                                this.m_ExpandablePanel.GetExpandButtonRectangle().Contains(point))
                                ) 
                            {
                                ExpandablePanelContainer expandablePanelContainer = this.m_ExpandablePanel.Parent as ExpandablePanelContainer;
                                if (expandablePanelContainer == null) return;
                                expandablePanelContainer.SetSelectedExpandablePanel(this.m_ExpandablePanel);
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

        private void ShowInfo(object sender, EventArgs e)
        {
            InfoForm infoForm = new InfoForm();
            infoForm.ShowDialog();
        }
    }
}
