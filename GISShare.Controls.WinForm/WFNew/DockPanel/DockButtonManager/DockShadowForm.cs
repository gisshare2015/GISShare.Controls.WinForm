using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew.DockPanel
{
    sealed class DockShadowForm : Form, IArea, IArea2
    {
        public DockShadowForm()
        {
            this.BackColor = GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.WFNewColorTable.RibbonAreaBackground;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Enabled = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DockShadowForm";
            this.Opacity = 0.6;
            this.TopMost = true;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "DockShadowForm";
        }

        public void Show(Rectangle rectangle)
        {
            base.Show();
            this.Location = rectangle.Location;
            this.Size = rectangle.Size;
        }

        public new void Close()
        {
            base.Hide();
        }

        #region IArea
        [Browsable(false), Description("显示外框线"), Category("外观")]
        public bool ShowOutLine
        {
            get { return false; }
        }

        [Browsable(false), Description("显示背景色"), Category("外观")]
        public bool ShowBackground
        {
            get { return false; }
        }

        [Browsable(false), Description("框架矩形"), Category("布局")]
        public Rectangle FrameRectangle
        {
            get
            {
                return new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            }
        }

        public Rectangle AreaRectangle
        {
            get
            {
                return new Rectangle(0, 0, this.Width, this.Height);
            }
        }
        #endregion

        #region IArea2
        private bool m_AreaCustomize = false;
        [Browsable(true), DefaultValue(false), Description("自定义区域"), Category("状态")]
        public bool AreaCustomize
        {
            get { return m_AreaCustomize; }
            set { m_AreaCustomize = value; }
        }

        private Color m_OutLineColor = System.Drawing.Color.Transparent;
        [Browsable(true), DefaultValue(typeof(Color), "System.Drawing.Color.Transparent"), Description("外框线颜色"), Category("外观")]
        public Color OutLineColor
        {
            get { return m_OutLineColor; }
            set { m_OutLineColor = value; }
        }

        private Color m_BackgroundColor = System.Drawing.Color.Transparent;
        [Browsable(true), DefaultValue(typeof(Color), "System.Drawing.Color.Transparent"), Description("背景颜色"), Category("外观")]
        public Color BackgroundColor
        {
            get { return m_BackgroundColor; }
            set { m_BackgroundColor = value; }
        }
        #endregion

        protected override void OnPaint(PaintEventArgs pevent)
        {
            this.OnDraw(pevent);
            //
            base.OnPaint(pevent);
        }

        private void OnDraw(PaintEventArgs e)
        {
            WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonArea(new ObjectRenderEventArgs(e.Graphics, this, this.AreaRectangle));
        }
    }
}