using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    public abstract class AreaItem : BaseItem, IArea, IArea2
    {
        #region IArea
        private bool m_ShowOutLine = false;
        [Browsable(true), DefaultValue(false), Description("显示外框线"), Category("外观")]
        public virtual bool ShowOutLine
        {
            get { return m_ShowOutLine; }
            set { m_ShowOutLine = value; }
        }

        private bool m_ShowBackground = false;
        [Browsable(true), DefaultValue(false), Description("显示背景色"), Category("外观")]
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
                //return new Rectangle(0, 0, this.Width - 1, this.Height - 1);
                Rectangle rectangle = this.AreaRectangle;// this.DisplayRectangle;
                return new Rectangle(rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
            }
        }

        [Browsable(false), Description("可视化矩形框"), Category("布局")]
        public virtual Rectangle AreaRectangle
        {
            get
            {
                return this.DisplayRectangle;
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
        private Image m_BackgroundImage = null;
        [Browsable(true), Description("背景图片"), Category("外观")]
        public virtual Image BackgroundImage
        {
            get { return m_BackgroundImage; }
            set { m_BackgroundImage = value; }
        }

        private ImageLayout m_BackgroundImageLayout = ImageLayout.Tile;
        [Browsable(true), DefaultValue(typeof(ImageLayout), "Tile"), Description("背景图片布局的方式"), Category("外观")]
        public virtual ImageLayout BackgroundImageLayout//BH
        {
            get
            {
                return m_BackgroundImageLayout;
            }
            set { m_BackgroundImageLayout = value; }
        }
        #endregion

        protected override void OnDraw(PaintEventArgs e)
        {
            WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonArea(new ObjectRenderEventArgs(e.Graphics, this, this.AreaRectangle));
        }
    }
}
