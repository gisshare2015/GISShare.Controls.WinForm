using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace LiuZhenHong.Controls.DockPanel
{
    public class TabPageDesigner : ScrollableControlDesigner
    {
        private TabPage m_TabPage = null;

        private DesignerVerbCollection verbs;

        public override DesignerVerbCollection Verbs
        {
            get
            {
                if (verbs == null)
                {
                    verbs = new DesignerVerbCollection();
                    verbs.Add(new DesignerVerb("关于TabPage控件", new EventHandler(AboutTabPage)));
                    verbs.Add(new DesignerVerb("关于...", new EventHandler(ShowInfo)));
                }
                return verbs;
            }
        }

        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            //
            this.m_TabPage = base.Component as TabPage;
            if (this.m_TabPage == null)
            {
                this.DisplayError(new ArgumentException("TabPage == null"));
                return;
            }
        }

        protected override void OnPaintAdornments(PaintEventArgs pea)
        {
            base.OnPaintAdornments(pea);
            //
            if (this.m_TabPage.Height < 16) return;
            //
            if (m_TabPage != null)
            {
                using (Pen p = new Pen(LiuZhenHong.Controls.Renderer.RendererManager.Renderer.ColorTableEx.BorderLine, 1))
                {
                    p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    pea.Graphics.DrawRectangle(p, m_TabPage.DisplayRectangle.X, m_TabPage.DisplayRectangle.Y, m_TabPage.DisplayRectangle.Width - 1, m_TabPage.DisplayRectangle.Height - 1);
                    StringFormat drawFormat = new StringFormat();
                    drawFormat.FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.FitBlackBox;
                    drawFormat.Trimming = StringTrimming.EllipsisCharacter;
                    SizeF size = pea.Graphics.MeasureString(m_TabPage.Text, m_TabPage.Font);
                    int iWidth = (int)(size.Width + 1);
                    int iHeight = (int)(size.Height + 1);
                    pea.Graphics.DrawString(m_TabPage.Text, 
                        m_TabPage.Font,
                        SystemBrushes.ControlText,
                        new Rectangle(m_TabPage.DisplayRectangle.X + (m_TabPage.DisplayRectangle.Width - iWidth) / 2, m_TabPage.DisplayRectangle.Y + (m_TabPage.DisplayRectangle.Height - iHeight) / 2, iWidth, 21), 
                        drawFormat);
                }
            }
        }

        protected virtual void AboutTabPage(object sender, EventArgs ea)
        {
            MessageBox.Show("选项卡面板控件（TabPage）：用来承载其它控件的容器控件。", "选项卡面板控件");
        }

        private void ShowInfo(object sender, EventArgs e)
        {
            InfoForm infoForm = new InfoForm();
            infoForm.ShowDialog();
        }
    }
}
