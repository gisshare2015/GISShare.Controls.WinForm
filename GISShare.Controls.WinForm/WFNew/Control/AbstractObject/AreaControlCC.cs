using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    public abstract class AreaControlCC : BaseItemControlCC, IArea
    {
        #region IArea
        private bool m_ShowOutLine = false;
        [Browsable(true), DefaultValue(false), Description("显示外框线"), Category("外观")]
        public virtual bool ShowOutLine
        {
            get { return m_ShowOutLine; }
            set { m_ShowOutLine = value; }
        }

        bool m_ShowBackgroud = true;
        [Browsable(true), DefaultValue(true), Description("显示背景色"), Category("外观")]
        public virtual bool ShowBackgroud
        {
            get { return m_ShowBackgroud; }
            set { m_ShowBackgroud = value; }
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
