using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace GISShare.Controls.WinForm.WFNew.Design
{
    public class ExpandableNodePanelDesigner : ScrollableControlDesigner
    {
        private ExpandableNodePanel m_ExpandableNodePanel = null;

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
            this.m_ExpandableNodePanel = base.Component as ExpandableNodePanel;
            if (this.m_ExpandableNodePanel == null)
            {
                this.DisplayError(new ArgumentException("ExpandableNodePanel == null"));
                return;
            }
        }

        protected override void OnPaintAdornments(PaintEventArgs pea)
        {
            base.OnPaintAdornments(pea);
            //
            if (!this.m_ExpandableNodePanel.ShowCaption) { if (this.m_ExpandableNodePanel.Height < 16) return; }
            else { if (this.m_ExpandableNodePanel.Height < this.m_ExpandableNodePanel.CaptionHeight + 16) return; }
            //
            if (m_ExpandableNodePanel != null && m_ExpandableNodePanel.IsExpand)
            {
                using (Pen p = new Pen(GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.WFNewColorTable.ArrowDisabled, 1))
                {
                    Rectangle rectangle = this.m_ExpandableNodePanel.DisplayRectangle;
                    rectangle = Rectangle.FromLTRB(rectangle.Left, rectangle.Top, rectangle.Right - 1, rectangle.Bottom - 1);
                    p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    pea.Graphics.DrawRectangle(p, rectangle);
                    //
                    StringFormat drawFormat = new StringFormat();
                    drawFormat.FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.FitBlackBox;
                    drawFormat.Trimming = StringTrimming.EllipsisCharacter;
                    SizeF size = pea.Graphics.MeasureString(this.m_ExpandableNodePanel.Text, this.m_ExpandableNodePanel.Font);
                    int iWidth = (int)(size.Width + 1);
                    int iHeight = (int)(size.Height + 1);
                    pea.Graphics.DrawString(this.m_ExpandableNodePanel.Text,
                        this.m_ExpandableNodePanel.Font,
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
                if (this.m_ExpandableNodePanel.Created)
                {
                    switch (m.Msg)
                    {
                        case (int)GISShare.Win32.Msgs.WM_LBUTTONDOWN://0x201
                            Point point = GISShare.Win32.NativeMethods.LParamToMouseLocation((int)m.LParam);
                            if (
                                (this.m_ExpandableNodePanel.ShowTreeNodeButton &&
                                this.m_ExpandableNodePanel.GetTreeNodeButtonRectangle().Contains(point))
                                ||
                                (this.m_ExpandableNodePanel.ShowExpandButton &&
                                this.m_ExpandableNodePanel.GetExpandButtonRectangle().Contains(point))
                                ) 
                            {
                                ISetExpandableCaptionPanelHelper pSetExpandableCaptionPanelHelper = this.m_ExpandableNodePanel as ISetExpandableCaptionPanelHelper;
                                if (pSetExpandableCaptionPanelHelper == null) return;
                                pSetExpandableCaptionPanelHelper.SetIsExpand(!this.m_ExpandableNodePanel.IsExpand);
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
