using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    public abstract class AreaItem : BaseItem, IArea
    {
        #region IArea
        private bool m_ShowOutLine = false;
        [Browsable(true), DefaultValue(false), Description("��ʾ�����"), Category("���")]
        public virtual bool ShowOutLine
        {
            get { return m_ShowOutLine; }
            set { m_ShowOutLine = value; }
        }

        bool m_ShowBackgroud = false;
        [Browsable(true), DefaultValue(false), Description("��ʾ����ɫ"), Category("���")]
        public virtual bool ShowBackgroud
        {
            get { return m_ShowBackgroud; }
            set { m_ShowBackgroud = value; }
        }

        [Browsable(false), Description("��ܾ���"), Category("����")]
        public virtual Rectangle FrameRectangle
        {
            get
            {
                Rectangle rectangle = this.DisplayRectangle;
                return new Rectangle(rectangle.X,
                    rectangle.Y,
                    rectangle.Width - 1,
                    rectangle.Height - 1);
            }
        }

        [Browsable(false), Description("���ӻ����ο�"), Category("����")]
        public virtual Rectangle AreaRectangle
        {
            get
            {
                return this.DisplayRectangle;
            }
        }
        #endregion

        protected override void OnDraw(PaintEventArgs e)
        {
            WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonArea(new ObjectRenderEventArgs(e.Graphics, this, this.AreaRectangle));
        }
    }
}
