using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel;
using System.Collections;

namespace GISShare.Controls.WinForm.WFNew.View
{
    public class ColorViewItem : TextViewItem, IColorViewItem
    {
        private const int CONST_COLORREGIONWIDTH = 28;
        private const int CONST_COLORREGIONLEFTRIGHTSPACE = 2;
        private const int CONST_COLORREGIONHEIGHT = 9;

        public ColorViewItem() { }

        public ColorViewItem(string text)
            : base(text) { }

        public ColorViewItem(string name, string text)
            : base(name, text) { }

        public ColorViewItem(string name, string text, Color color)
            : base(name, text) 
        {
            this.m_Color = color;
        }

        public ColorViewItem(string name, string text, Font font)
            : base(name, text, font) { }

        public ColorViewItem(string name, string text, Font font, Color color)
            : base(name, text, font)
        {
            this.m_Color = color;
        }

        #region IColorViewItem
        Color m_Color = Color.Red;
        [Browsable(true), Description("颜色"), Category("外观")]
        public Color Color
        {
            get { return m_Color; }
            set { m_Color = value; }
        }

        [Browsable(false), Description("颜色绘制矩形"), Category("布局")]
        public Rectangle ColorRectangle
        {
            get
            {
                Rectangle rectangle = this.DisplayRectangle;
                return new Rectangle
                    (
                    rectangle.Left + CONST_COLORREGIONLEFTRIGHTSPACE,
                    (rectangle.Top + rectangle.Bottom - CONST_COLORREGIONHEIGHT) / 2,
                    CONST_COLORREGIONWIDTH - 2 * CONST_COLORREGIONLEFTRIGHTSPACE,
                    CONST_COLORREGIONHEIGHT
                    );
            }
        }
        #endregion

        public override Size MeasureSize(Graphics g)
        {
            if (this.Color.IsEmpty) return base.MeasureSize(g);
            SizeF size = g.MeasureString(this.Text, this.Font);
            return new Size(this.Width > 0 ? this.Width : CONST_COLORREGIONWIDTH + (int)size.Width + 1, this.Height > 0 ? this.Height : (int)size.Height + 1);
        }

        protected override void OnDraw(PaintEventArgs e)
        {
            #region 已抛弃（该注释请不要删除）
            //Rectangle rectangle = Rectangle.FromLTRB(e.ClipRectangle.Left, e.ClipRectangle.Top, e.ClipRectangle.Right - 1, e.ClipRectangle.Bottom - 1);
            //WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderColorViewItem(new ObjectRenderEventArgs(e.Graphics, this, rectangle));
            //rectangle = this.DisplayRectangle;
            //int iH = (int)e.Graphics.MeasureString(this.Text, this.Font).Height + 1;
            //if (this.Color.IsEmpty)
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
            //            new StringFormat())
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
            //            new Rectangle(rectangle.Left + CONST_COLORREGIONWIDTH, (rectangle.Top + rectangle.Bottom - iH) / 2, rectangle.Width - CONST_COLORREGIONWIDTH, rectangle.Height),
            //            new StringFormat())
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
            //WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderColorViewItem(new ObjectRenderEventArgs(e.Graphics, this, rectangle));
            //rectangle = new Rectangle(this.DisplayRectangle.X, this.DisplayRectangle.Y, iW, this.DisplayRectangle.Height);//rectangle.Height
            //int iH = (int)e.Graphics.MeasureString(this.Text, this.Font).Height + 1;
            //if (this.Color.IsEmpty)
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
            //            new StringFormat())
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
            //            new Rectangle(rectangle.Left + CONST_COLORREGIONWIDTH, (rectangle.Top + rectangle.Bottom - iH) / 2, rectangle.Width - CONST_COLORREGIONWIDTH, iH),//rectangle.Height
            //            new StringFormat())
            //        );
            //}
            #endregion
            //
            Rectangle rectangle = Rectangle.FromLTRB(e.ClipRectangle.Left, e.ClipRectangle.Top, e.ClipRectangle.Right - 1, e.ClipRectangle.Bottom - 1);
            WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderColorViewItem(new ObjectRenderEventArgs(e.Graphics, this, rectangle));
            if (String.IsNullOrEmpty(this.Text)) return;
            rectangle = this.DisplayRectangle;
            int iH = (int)e.Graphics.MeasureString(this.Text, this.Font).Height + 1;
            if (this.Color.IsEmpty)
            {
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
                        new StringFormat())
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
                        this.HaveShadow,
                        this.Text,
                        this.ForeCustomize,
                        this.ForeColor,
                        this.ShadowColor,
                        this.Font,
                        new Rectangle(rectangle.Left + CONST_COLORREGIONWIDTH, (rectangle.Top + rectangle.Bottom - iH) / 2, rectangle.Width - CONST_COLORREGIONWIDTH, iH),//rectangle.Height
                        new StringFormat())
                    );
            }
            //
            //base.OnDraw(e);
        }

        //
        //
        //

        public static void KnownColorViewItem(IList pList)
        {
            string[] colors = System.Enum.GetNames(typeof(KnownColor));
            foreach (string one in colors)
            {
                pList.Add(new View.ColorViewItem(one, one, Color.FromName(one)));
            }
        }

        public static void SystemColorViewItem(IList pList)
        {
            pList.Add(new View.ColorViewItem("ActiveBorder", "ActiveBorder", SystemColors.ActiveBorder));
            pList.Add(new View.ColorViewItem("ActiveCaption", "ActiveCaption", SystemColors.ActiveCaption));
            pList.Add(new View.ColorViewItem("ActiveCaptionText", "ActiveCaptionText", SystemColors.ActiveCaptionText));
            pList.Add(new View.ColorViewItem("AppWorkspace", "AppWorkspace", SystemColors.AppWorkspace));
            pList.Add(new View.ColorViewItem("ButtonFace", "ButtonFace", SystemColors.ButtonFace));
            pList.Add(new View.ColorViewItem("ButtonHighlight", "ButtonHighlight", SystemColors.ButtonHighlight));
            pList.Add(new View.ColorViewItem("ButtonShadow", "ButtonShadow", SystemColors.ButtonShadow));
            pList.Add(new View.ColorViewItem("Control", "Control", SystemColors.Control));
            pList.Add(new View.ColorViewItem("ControlDark", "ControlDark", SystemColors.ControlDark));
            pList.Add(new View.ColorViewItem("ControlDarkDark", "ControlDarkDark", SystemColors.ControlDarkDark));
            pList.Add(new View.ColorViewItem("ControlLight", "ControlLight", SystemColors.ControlLight));
            pList.Add(new View.ColorViewItem("ControlLightLight", "ControlLightLight", SystemColors.ControlLightLight));
            pList.Add(new View.ColorViewItem("ControlText", "ControlText", SystemColors.ControlText));
            pList.Add(new View.ColorViewItem("Desktop", "Desktop", SystemColors.Desktop));
            pList.Add(new View.ColorViewItem("GradientActiveCaption", "GradientActiveCaption", SystemColors.GradientActiveCaption));
            pList.Add(new View.ColorViewItem("GradientInactiveCaption", "GradientInactiveCaption", SystemColors.GradientInactiveCaption));
            pList.Add(new View.ColorViewItem("GrayText", "GrayText", SystemColors.GrayText));
            pList.Add(new View.ColorViewItem("Highlight", "Highlight", SystemColors.Highlight));
            pList.Add(new View.ColorViewItem("HighlightText", "HighlightText", SystemColors.HighlightText));
            pList.Add(new View.ColorViewItem("HotTrack", "HotTrack", SystemColors.HotTrack));
            pList.Add(new View.ColorViewItem("InactiveBorder", "InactiveBorder", SystemColors.InactiveBorder));
            pList.Add(new View.ColorViewItem("InactiveCaption", "InactiveCaption", SystemColors.InactiveCaption));
            pList.Add(new View.ColorViewItem("InactiveCaptionText", "InactiveCaptionText", SystemColors.InactiveCaptionText));
            pList.Add(new View.ColorViewItem("Info", "Info", SystemColors.Info));
            pList.Add(new View.ColorViewItem("InfoText", "InfoText", SystemColors.InfoText));
            pList.Add(new View.ColorViewItem("Menu", "Menu", SystemColors.Menu));
            pList.Add(new View.ColorViewItem("MenuBar", "MenuBar", SystemColors.MenuBar));
            pList.Add(new View.ColorViewItem("MenuHighlight", "MenuHighlight", SystemColors.MenuHighlight));
            pList.Add(new View.ColorViewItem("MenuText", "MenuText", SystemColors.MenuText));
            pList.Add(new View.ColorViewItem("ScrollBar", "ScrollBar", SystemColors.ScrollBar));
            pList.Add(new View.ColorViewItem("Window", "Window", SystemColors.Window));
            pList.Add(new View.ColorViewItem("WindowFrame", "WindowFrame", SystemColors.WindowFrame));
            pList.Add(new View.ColorViewItem("WindowText", "WindowText", SystemColors.WindowText));
        }
    }
}
