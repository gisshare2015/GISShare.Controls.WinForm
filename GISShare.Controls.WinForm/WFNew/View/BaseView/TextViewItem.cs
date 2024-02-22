using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.ComponentModel;
using System.Collections;

namespace GISShare.Controls.WinForm.WFNew.View
{
    public class TextViewItem : SizeViewItem, ITextViewItem
    {
        private const int CONST_MINNODEWIDTH = 18;
        private const int CONST_MINNODEHEIGHT = 18;

        public TextViewItem() { }

        public TextViewItem(string text)
            : base(text) { }

        public TextViewItem(string name, string text)
            : base(name, text) { }

        public TextViewItem(string name, string text, Font font)
            : base(name, text)
        {
            this.m_Font = font;
        }

        #region ITextViewItem
        private Font m_Font = new Font("宋体", 9f);
        [Browsable(true), DefaultValue(typeof(Font), "\"宋体\", 9f"), Description("字体"), Category("外观")]
        public Font Font
        {
            get { return m_Font; }
            set { m_Font = value; }
        }

        private Color m_ForeColor = System.Drawing.SystemColors.ControlText;
        [Browsable(true), DefaultValue(typeof(Color), "System.Drawing.SystemColors.ControlText"), Description("字体颜色"), Category("外观")]
        public Color ForeColor
        {
            get { return m_ForeColor; }
            set { m_ForeColor = value; }
        }

        private bool m_HaveShadow = true;
        [Browsable(true), DefaultValue(true), Description("是否有字体阴影"), Category("状态")]
        public bool HaveShadow
        {
            get { return m_HaveShadow; }
            set { m_HaveShadow = value; }
        }

        private Color m_ShadowColor = System.Drawing.SystemColors.ControlText;
        [Browsable(true), DefaultValue(typeof(Color), "System.Drawing.SystemColors.ControlText"), Description("字体阴影颜色"), Category("外观")]
        public Color ShadowColor
        {
            get { return m_ShadowColor; }
            set { m_ShadowColor = value; }
        }

        private bool m_ForeCustomize = false;
        [Browsable(true), DefaultValue(false), Description("自定义文本色"), Category("状态")]
        public bool ForeCustomize
        {
            get { return m_ForeCustomize; }
            set { m_ForeCustomize = value; }
        }
        #endregion 

        public override Size MeasureSize(Graphics g)
        {
            SizeF size = g.MeasureString(this.Text, this.Font);
            return new Size(this.Width > 0 ? this.Width : (int)size.Width + 1, this.Height > 0 ? this.Height : (int)size.Height + 1);
        }

        protected override void OnDraw(System.Windows.Forms.PaintEventArgs e)
        {
            #region 已抛弃（该注释请不要删除）
            //Rectangle rectangle = Rectangle.FromLTRB(e.ClipRectangle.Left, e.ClipRectangle.Top, e.ClipRectangle.Right - 1, e.ClipRectangle.Bottom - 1);
            //WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderViewItem
            //    (
            //    new ObjectRenderEventArgs(e.Graphics, this, rectangle)
            //    );
            //rectangle = this.DisplayRectangle;
            //int iH = (int)e.Graphics.MeasureString(this.Text, this.Font).Height + 1;
            //WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonText
            //    (
            //    new TextRenderEventArgs(
            //        e.Graphics,
            //        this,
            //        true,
            //        true,
            //        this.Text,
            //        this.ForeColor,
            //        this.Font,
            //        new Rectangle(rectangle.Left, (rectangle.Top + rectangle.Bottom - iH) / 2, rectangle.Width, rectangle.Height),
            //        new StringFormat() { Trimming = StringTrimming.EllipsisCharacter })
            //    );
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
            //WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderViewItem
            //    (
            //    new ObjectRenderEventArgs(e.Graphics, this, rectangle)
            //    );
            //rectangle = new Rectangle(this.DisplayRectangle.X, this.DisplayRectangle.Y, iW, this.DisplayRectangle.Height);//rectangle.Height
            //int iH = (int)e.Graphics.MeasureString(this.Text, this.Font).Height + 1;
            //WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonText
            //    (
            //    new TextRenderEventArgs(
            //        e.Graphics,
            //        this,
            //        true,
            //        true,
            //        this.Text,
            //        this.ForeColor,
            //        this.Font,
            //        new Rectangle(rectangle.Left, (rectangle.Top + rectangle.Bottom - iH) / 2, rectangle.Width, iH),//rectangle.Height
            //        new StringFormat() { Trimming = StringTrimming.EllipsisCharacter })
            //    );
            #endregion
            //
            Rectangle rectangle = Rectangle.FromLTRB(e.ClipRectangle.Left, e.ClipRectangle.Top, e.ClipRectangle.Right - 1, e.ClipRectangle.Bottom - 1);
            WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderViewItem
                (
                new ObjectRenderEventArgs(e.Graphics, this, rectangle)
                );
            if (String.IsNullOrEmpty(this.Text)) return;
            rectangle = this.DisplayRectangle;
            int iH = (int)e.Graphics.MeasureString(this.Text, this.Font).Height + 1;
            WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonText
                (
                new TextRenderEventArgs(
                    e.Graphics,
                    this,
                    true,
                    this.HaveShadow,
                    this.Text,
                    this.ForeCustomize,
                    this.ForeColor,
                    this.ShadowColor,
                    this.Font,
                    new Rectangle(rectangle.Left, (rectangle.Top + rectangle.Bottom - iH) / 2, rectangle.Width, iH),//rectangle.Height
                    new StringFormat() { Trimming = StringTrimming.EllipsisCharacter })
                );
        }

        //
        //
        //

        public static void InstalledFontViewItem(IList pList)
        {
            System.Drawing.Text.InstalledFontCollection installedFontCollection = new System.Drawing.Text.InstalledFontCollection();
            foreach (System.Drawing.FontFamily one in installedFontCollection.Families)
            {
                if (!one.IsStyleAvailable(FontStyle.Bold) ||
                    !one.IsStyleAvailable(FontStyle.Italic) ||
                    !one.IsStyleAvailable(FontStyle.Regular) ||
                    !one.IsStyleAvailable(FontStyle.Strikeout) ||
                    !one.IsStyleAvailable(FontStyle.Underline)) continue;
                pList.Add(new View.TextViewItem(one.Name, one.Name, new System.Drawing.Font(one, 9f)));
            }
        }
    }
}
