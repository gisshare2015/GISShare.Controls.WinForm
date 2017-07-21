using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew.View
{
    public class ImageViewItem : TextViewItem, IImageViewItem
    {
        private const int CONST_IMAGEREGIONSPACE = 1;

        public ImageViewItem() { }

        public ImageViewItem(string text)
            : base(text) { }

        public ImageViewItem(string name, string text)
            : base(name, text) { }

        #region IImageViewItem
        private Image m_Image = null;
        [Browsable(true), Description("图片"), Category("外观")]
        public virtual Image Image
        {
            get { return m_Image; }
            set { m_Image = value; }
        }

        [Browsable(false), Description("图片绘制矩形"), Category("布局")]
        public Rectangle ImageRectangle
        {
            get
            {
                int iSize = this.DisplayRectangle.Height - 2 * CONST_IMAGEREGIONSPACE;
                Rectangle rectangle = this.DisplayRectangle;
                return new Rectangle
                    (
                    rectangle.Left + CONST_IMAGEREGIONSPACE,
                    (rectangle.Top + rectangle.Bottom - iSize) / 2,
                    iSize,
                    iSize
                    );
            }
        }
        #endregion

        public override Size MeasureSize(Graphics g)
        {
            if (this.Image == null) return base.MeasureSize(g);
            SizeF size = g.MeasureString(this.Text, this.Font);
            return new Size
                (
                this.Width > 0 ? this.Width : this.DisplayRectangle.Height + (int)size.Width + 1, 
                this.Height > 0 ? this.Height : (int)size.Height + 1
                );
        }

        protected override void OnDraw(PaintEventArgs e)
        {
            #region 已抛弃（该注释请不要删除）
            //Rectangle rectangle = Rectangle.FromLTRB(e.ClipRectangle.Left, e.ClipRectangle.Top, e.ClipRectangle.Right - 1, e.ClipRectangle.Bottom - 1);
            //WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderImageViewItem(new ObjectRenderEventArgs(e.Graphics, this, rectangle));
            //rectangle = this.DisplayRectangle;
            //int iH = (int)e.Graphics.MeasureString(this.Text, this.Font).Height + 1;
            //if (this.Image == null)
            //{
            //    WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonText
            //        (
            //        new TextRenderEventArgs(
            //            e.Graphics,
            //            this,
            //            true,
            //            true,
            //            this.Text,
            //            this.ForeColor,
            //            this.Font,
            //            new Rectangle(rectangle.Left, (rectangle.Top + rectangle.Bottom - iH) / 2, rectangle.Width, rectangle.Height),
            //            new StringFormat(){ Trimming = StringTrimming.EllipsisCharacter })
            //        );
            //}
            //else
            //{
            //    WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonText
            //        (
            //        new TextRenderEventArgs(
            //            e.Graphics,
            //            this,
            //            true,
            //            true,
            //            this.Text,
            //            this.ForeColor,
            //            this.Font,
            //            new Rectangle(rectangle.Left + rectangle.Height, (rectangle.Top + rectangle.Bottom - iH) / 2, rectangle.Width - rectangle.Height, rectangle.Height),
            //            new StringFormat(){ Trimming = StringTrimming.EllipsisCharacter })
            //        );
            //}
            #endregion
            //
            #region 已抛弃（该注释请不要删除）
            //Rectangle rectangle = Rectangle.FromLTRB(e.ClipRectangle.Left, e.ClipRectangle.Top, e.ClipRectangle.Right - 1, e.ClipRectangle.Bottom - 1);
            //int iW = this.DisplayRectangle.Width;
            //if (this.Width >= 0 && this.DisplayRectangle.X >= 0)
            //{
            //    iW = this.Width > rectangle.Width ? rectangle.Width : this.Width;
            //    //rectangle = new Rectangle(rectangle.X, rectangle.Y, iW - (rectangle.X - this.DisplayRectangle.X), rectangle.Height);
            //}
            //WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderImageViewItem(new ObjectRenderEventArgs(e.Graphics, this, rectangle));
            //rectangle = new Rectangle(this.DisplayRectangle.X, this.DisplayRectangle.Y, iW, this.DisplayRectangle.Height);//rectangle.Height
            //int iH = (int)e.Graphics.MeasureString(this.Text, this.Font).Height + 1;
            //if (this.Image == null)
            //{
            //    WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonText
            //        (
            //        new TextRenderEventArgs(
            //            e.Graphics,
            //            this,
            //            true,
            //            true,
            //            this.Text,
            //            this.ForeColor,
            //            this.Font,
            //            new Rectangle(rectangle.Left, (rectangle.Top + rectangle.Bottom - iH) / 2, rectangle.Width, iH),//rectangle.Height
            //            new StringFormat() { Trimming = StringTrimming.EllipsisCharacter })
            //        );
            //}
            //else
            //{
            //    WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonText
            //        (
            //        new TextRenderEventArgs(
            //            e.Graphics,
            //            this,
            //            true,
            //            true,
            //            this.Text,
            //            this.ForeColor,
            //            this.Font,
            //            new Rectangle(rectangle.Left + rectangle.Height, (rectangle.Top + rectangle.Bottom - iH) / 2, rectangle.Width - rectangle.Height, iH),//rectangle.Height
            //            new StringFormat() { Trimming = StringTrimming.EllipsisCharacter })
            //        );
            //}
            #endregion
            //
            Rectangle rectangle = Rectangle.FromLTRB(e.ClipRectangle.Left, e.ClipRectangle.Top, e.ClipRectangle.Right - 1, e.ClipRectangle.Bottom - 1);
            WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderImageViewItem(new ObjectRenderEventArgs(e.Graphics, this, rectangle));
            if (this.Text.Length <= 0) return;
            rectangle = this.DisplayRectangle;
            int iH = (int)e.Graphics.MeasureString(this.Text, this.Font).Height + 1;
            if (this.Image == null)
            {
                WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonText
                    (
                    new TextRenderEventArgs(
                        e.Graphics,
                        this,
                        true,
                        true,
                        this.Text,
                        this.ForeColor,
                        this.Font,
                        new Rectangle(rectangle.Left, (rectangle.Top + rectangle.Bottom - iH) / 2, rectangle.Width, iH),//rectangle.Height
                        new StringFormat() { Trimming = StringTrimming.EllipsisCharacter })
                    );
            }
            else
            {
                WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonText
                    (
                    new TextRenderEventArgs(
                        e.Graphics,
                        this,
                        true,
                        true,
                        this.Text,
                        this.ForeColor,
                        this.Font,
                        new Rectangle(rectangle.Left + rectangle.Height, (rectangle.Top + rectangle.Bottom - iH) / 2, rectangle.Width - rectangle.Height, iH),//rectangle.Height
                        new StringFormat() { Trimming = StringTrimming.EllipsisCharacter })
                    );
            }
            //
            //base.OnDraw(e);
        }
    }
}
