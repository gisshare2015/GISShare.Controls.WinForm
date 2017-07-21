using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;

namespace GISShare.Controls.WinForm.WFNew.View
{
    public class SizeViewItem : ViewItem, ISizeViewItem
    {
        private const int CONST_MINNODEWIDTH = 18;
        private const int CONST_MINNODEHEIGHT = 18;

        public SizeViewItem() { }

        public SizeViewItem(string text)
            : base(text) { }

        public SizeViewItem(string name, string text)
            : base(text)
        {
            this.m_Name = name;
        }

        #region ISizeViewItem
        string m_Name;
        [Browsable(true), Description("名称"), Category("描述")]
        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        int m_Width = -1;
        [Browsable(true), DefaultValue(-1), Description("宽度（小于零时，由系统操作）"), Category("布局")]
        public virtual int Width
        {
            get { return m_Width; }
            set
            {
                if (value < CONST_MINNODEWIDTH) value = CONST_MINNODEWIDTH;
                m_Width = value;
            }
        }

        int m_Height = 18;
        [Browsable(true), DefaultValue(18), Description("高度（小于零时，由系统操作）"), Category("布局")]
        public virtual int Height
        {
            get { return m_Height; }
            set
            {
                if (value < CONST_MINNODEHEIGHT) value = CONST_MINNODEHEIGHT;
                m_Height = value;
            }
        }
        #endregion

        public override Size MeasureSize(Graphics g)
        {
            Size size = base.MeasureSize(g);
            return new Size(this.Width > 0 ? this.Width : size.Width, this.Height > 0 ? this.Height : size.Height);
        }
    }
}
