using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    public abstract class AreaControlCC : BaseItemControlCC, IArea, IArea2
    {
        #region IArea
        private bool m_ShowOutLine = false;
        [Browsable(true), DefaultValue(false), Description("显示外框线"), Category("外观")]
        public virtual bool ShowOutLine
        {
            get { return m_ShowOutLine; }
            set { m_ShowOutLine = value; }
        }

        bool m_ShowBackground = true;
        [Browsable(true), DefaultValue(true), Description("显示背景色"), Category("外观")]
        public virtual bool ShowBackground
        {
            get { return m_ShowBackground; }
            set { m_ShowBackground = value; }
        }

        [Browsable(false), Description("框架矩形"), Category("布局")]
        public virtual Rectangle FrameRectangle
        {
            get
            {
                return new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            }
        }

        [Browsable(false), Description("可视化矩形框"), Category("布局")]
        public virtual Rectangle AreaRectangle
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
        public virtual bool AreaCustomize
        {
            get { return m_AreaCustomize; }
            set { m_AreaCustomize = value; }
        }

        private Color m_OutLineColor = System.Drawing.Color.Transparent;
        [Browsable(true), DefaultValue(typeof(Color), "System.Drawing.Color.Transparent"), Description("外框线颜色"), Category("外观")]
        public virtual Color OutLineColor
        {
            get { return m_OutLineColor; }
            set { m_OutLineColor = value; }
        }

        private Color m_BackgroundColor = System.Drawing.Color.Transparent;
        [Browsable(true), DefaultValue(typeof(Color), "System.Drawing.Color.Transparent"), Description("背景颜色"), Category("外观")]
        public virtual Color BackgroundColor
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

        protected virtual void OnDraw(PaintEventArgs e)
        {
            WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonArea(new ObjectRenderEventArgs(e.Graphics, this, this.AreaRectangle));
        }
    }
}
