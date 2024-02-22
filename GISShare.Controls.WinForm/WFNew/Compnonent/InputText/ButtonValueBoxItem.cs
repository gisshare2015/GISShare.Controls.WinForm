using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    public class ButtonValueBoxItem : ButtonTextBoxItem, IButtonValueBoxItem
    {
        private const int CTR_IMAGESIZE = 16;
        private const int CONST_COLORREGIONWIDTH = 28;
        private const int CONST_COLORREGIONHEIGHT = 9;
        private const int CTR_BORDERSPASE = 3;
        private const int CTR_NONELEFTSPACE = 1;//
        private const int CTR_SINGLELEFTSPACE = 0;//
        private const int CTR_MIDDLESPACE = 2;//间距

        public ButtonValueBoxItem() 
        {
            base.CanEdit = false;
        }

        [Browsable(false), Description("文本"), Category("外观")]
        public override string Text
        {
            get
            {
                if (this.m_ValueItem == null) return null;
                return this.m_ValueItem is View.ViewItem ? ((View.ViewItem)this.m_ValueItem).Text : this.m_ValueItem.ToString();
            }
            set
            {

            }
        }
        
        #region ITextBoxItem
        [Browsable(false), DefaultValue(true), Description("是否可以编辑"), Category("状态")]
        public override bool CanEdit
        {
            get { return false; }
            set {  }
        }

        [Browsable(true), DefaultValue(true), Description("是否可以选择"), Category("状态")]
        public override bool CanSelect
        {
            get { return false; }
            set { }
        }
        #endregion

        #region ITextBoxItem2
        [Browsable(false), Description("掩码"), Category("外观")]
        public override char PasswordChar
        {
            get
            {
                return base.PasswordChar;
            }
            set
            {

            }
        }
        #endregion

        #region IButtonTextBoxItem
        public override int OffsetX
        {
            get
            {
                if (this.ValueItem == null) return base.OffsetX;
                //
                if (this.ValueItem is View.IColorViewItem)
                {
                    switch (this.eBorderStyle)
                    {
                        case BorderStyle.eNone:
                            return CTR_NONELEFTSPACE + CONST_COLORREGIONWIDTH + CTR_MIDDLESPACE;
                        case BorderStyle.eSingle:
                        default:
                            return CTR_SINGLELEFTSPACE + CONST_COLORREGIONWIDTH + CTR_MIDDLESPACE;
                    }
                }
                else if (this.ValueItem is View.IImageViewItem)
                {
                    View.IImageViewItem pImageViewItem = this.ValueItem as View.IImageViewItem;
                    if (pImageViewItem.Image == null)
                    {
                        return base.OffsetX;
                    }
                    else
                    {
                        switch (this.eBorderStyle)
                        {
                            case BorderStyle.eNone:
                                return CTR_NONELEFTSPACE + CTR_IMAGESIZE + CTR_MIDDLESPACE;
                            case BorderStyle.eSingle:
                            default:
                                return CTR_SINGLELEFTSPACE + CTR_IMAGESIZE + CTR_MIDDLESPACE;
                        }
                    }
                }
                else
                {
                    return base.OffsetX;
                }
            }
        }
        #endregion

        #region IButtonValueBoxItem
        private object m_ValueItem = null;
        /// <summary>
        /// 值
        /// </summary>
        [Browsable(false), Description("值"), Category("外观")]
        public object ValueItem
        {
            get { return m_ValueItem; }
            set { m_ValueItem = value; }
        }
        #endregion

        protected override void OnDraw(System.Windows.Forms.PaintEventArgs e)
        {
            //if (this.Height != CTR_HEIGHT) { ((ISetBaseItemHelper)this).SetSize(this.Width, CTR_HEIGHT); }
            base.OnDraw(e);
            //
            if (this.ValueItem is View.IColorViewItem)
            {
                using (SolidBrush b = new SolidBrush(((View.IColorViewItem)this.ValueItem).Color))
                {
                    Rectangle rectangle = this.DisplayRectangle;
                    rectangle = new Rectangle
                        (
                        rectangle.Left + (this.eBorderStyle == GISShare.Controls.WinForm.WFNew.BorderStyle.eNone ? CTR_NONELEFTSPACE : (CTR_BORDERSPASE + CTR_SINGLELEFTSPACE)),
                        (rectangle.Top + rectangle.Bottom - CONST_COLORREGIONHEIGHT) / 2,
                        CONST_COLORREGIONWIDTH,
                        CONST_COLORREGIONHEIGHT
                        );
                    e.Graphics.FillRectangle(b, rectangle);
                    using (Pen p = new Pen(Color.Black))
                    {
                        e.Graphics.DrawRectangle(p, rectangle);
                    }
                }
            }
            else if (this.ValueItem is View.IImageViewItem)
            {
                Rectangle rectangle = this.DisplayRectangle;
                GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonImage
                    (
                    new ImageRenderEventArgs
                        (
                        e.Graphics,
                        this,
                        this.Enabled,
                        ((View.IImageViewItem)this.ValueItem).Image,
                        new Rectangle
                            (
                            rectangle.Left + (this.eBorderStyle == GISShare.Controls.WinForm.WFNew.BorderStyle.eNone ? CTR_NONELEFTSPACE : (CTR_BORDERSPASE + CTR_SINGLELEFTSPACE)),
                            (rectangle.Top + rectangle.Bottom - CTR_IMAGESIZE) / 2,
                            CTR_IMAGESIZE,
                            CTR_IMAGESIZE
                            )
                        )
                    );
            }
        }
    }
}
