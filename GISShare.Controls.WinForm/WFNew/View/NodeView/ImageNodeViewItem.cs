using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew.View
{
    [Serializable, DefaultProperty("Text"), TypeConverter(typeof(GISShare.Controls.WinForm.WFNew.View.Design.ImageNodeViewItemConverter))]//
    public class ImageNodeViewItem : NodeViewItem, IImageViewItem
    {
        private const int CONST_PTSPACE = 5;//PT = PlusMinus + Text
        private const int CONST_ITSPACE = 3;//IT = Image + Text
        private const int CONST_IMAGESIZE = 16;

        public ImageNodeViewItem()
            : base()
        { }

        public ImageNodeViewItem(string text)
            : base(text)
        { }

        public ImageNodeViewItem(string text, Image image)
            : base(text)
        {
            this.Image = image;
        }

        public ImageNodeViewItem(string name, string text)
            : base(name, text)
        { }

        public ImageNodeViewItem(string name, string text, Image image)
            : base(name, text)
        {
            this.Image = image;
        }

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
                Rectangle rectangle = this.PlusMinusRectangle;
                return new Rectangle
                    (
                    rectangle.Right + CONST_PTSPACE,
                    (rectangle.Top + rectangle.Bottom - CONST_IMAGESIZE) / 2,
                    CONST_IMAGESIZE,
                    CONST_IMAGESIZE
                    );
            }
        }
        #endregion        

        public override int OffsetX
        {
            get
            {
                return this.Image == null ? base.OffsetX : base.OffsetX + CONST_IMAGESIZE + CONST_ITSPACE;
            }
        }

        public override object Clone()
        {
            ImageNodeViewItem node = new ImageNodeViewItem();
            node.CanEdit = this.CanEdit;
            node.Enabled = this.Enabled;
            node.Font = this.Font;
            node.ForeColor = this.ForeColor;
            node.Height = this.Height;
            node.IsExpanded = this.IsExpanded;
            node.Name = this.Name;
            foreach (NodeViewItem one in this.NodeViewItems)
            {
                node.NodeViewItems.Add(one.Clone() as NodeViewItem);
            }
            node.ShowLines = this.ShowLines;
            node.ShowNomalState = this.ShowNomalState;
            node.ShowPlusMinus = this.ShowPlusMinus;
            node.Tag = this.Tag;
            node.Text = this.Text;
            node.Visible = this.Visible;
            node.Width = this.Width;
            //
            node.Image = this.Image;
            //
            return node;
        }

        protected override void OnDraw(PaintEventArgs e)
        {
            base.OnDraw(e);
            //
            if (this.Image == null) return;
            Rectangle rectangle = this.ImageRectangle;
            if (this.Width >= 0 && this.Width < rectangle.Right) return;
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonImage(
                new GISShare.Controls.WinForm.ImageRenderEventArgs(e.Graphics, this, this.Enabled, this.Image, this.ImageRectangle));
        }
    }
}
