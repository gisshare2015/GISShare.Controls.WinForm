using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    class SplitLineForm : Form, GISShare.Controls.WinForm.WFNew.ISplitLine
    {
        private DockStyle m_SplitPanelDock = DockStyle.Bottom; //分割面板停靠区
        public DockStyle SplitPanelDock
        {
            get { return m_SplitPanelDock; }
        }

        public SplitLineForm()
        {
            SetStyle(ControlStyles.Selectable, false);
            //
            this.Name = "SplitLineForm";
            this.Text = "SplitLineForm";
            this.Enabled = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.BackColor = GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.WFNewColorTable.RibbonAreaBackground;
            this.Opacity = 0;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
        }

        #region ISplitLine
        WFNew.RenderStyle m_eRenderStyle = WFNew.RenderStyle.eSystem;
        [Browsable(true), DefaultValue(typeof(WFNew.RenderStyle), "eSystem"), Description("渲染类型"), Category("外观")]
        public virtual WFNew.RenderStyle eRenderStyle
        {
            get { return m_eRenderStyle; }
            set { m_eRenderStyle = value; }
        }

        public WFNew.BaseItemState eBaseItemState
        {
            get { return GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal; }
        }
        #endregion

        protected override void OnPaint(PaintEventArgs pevent)
        {
            this.OnDraw(pevent);
            //
            base.OnPaint(pevent);
        }

        protected virtual void OnDraw(PaintEventArgs e)
        {
            WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderSplitLine(new ObjectRenderEventArgs(e.Graphics, this, this.DisplayRectangle));
        }

        private void DrawSplitLine(Graphics g)//绘制分割线
        {
            Pen p = new Pen(SystemColors.ControlText, 1);
            p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            switch (this.m_SplitPanelDock)
            {
                case DockStyle.Top:
                    for (int i = 0; i <= this.Height; i++)
                    {
                        if (i % 2 == 0) { g.DrawLine(p, 1, this.Height - i, this.Width - 1, this.Height - i); }
                        else { g.DrawLine(p, 0, this.Height - i, this.Width, this.Height - i); }
                    }
                    break;
                case DockStyle.Bottom:
                    for (int i = 0; i <= this.Height; i++)
                    {
                        if (i % 2 == 0) { g.DrawLine(p, 1, i, this.Width - 1, i); }
                        else { g.DrawLine(p, 0, i, this.Width, i); }
                    }
                    break;
                case DockStyle.Left:
                    for (int i = 0; i <= this.Width; i++)
                    {
                        if (i % 2 == 0) { g.DrawLine(p, this.Width - i, 1, this.Width - i, this.Height - 1); }
                        else { g.DrawLine(p, this.Width - i, 0, this.Width - i, this.Height); }
                    }
                    break;
                case DockStyle.Right:
                    for (int i = 0; i <= this.Width; i++)
                    {
                        if (i % 2 == 0) { g.DrawLine(p, i, 1, i, this.Height - 1); }
                        else { g.DrawLine(p, i, 0, i, this.Height); }
                    }
                    break;
                default:
                    break;
            }
        }

        public void Show(DockStyle eDockStyle, Rectangle splitterRectangle)//展现分割线窗体
        {
            this.m_SplitPanelDock = eDockStyle;
            GISShare.Win32.API.ShowWindow(Handle, (short)GISShare.Win32.CmdShowStyles.SW_SHOWNOACTIVATE);//无焦点窗体
            this.Bounds = splitterRectangle;
            this.Opacity = 0.7;
        }
    }
}