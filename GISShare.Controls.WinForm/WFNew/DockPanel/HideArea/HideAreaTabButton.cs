using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace LiuZhenHong.Controls.DockPanel
{
    [ToolboxItem(false)]
    class HideAreaTabButton : RenderableButton
    {
        #region 尺寸常量
        private const int CRT_HEIGHT = 23;
        #endregion

        #region 私有变量
        private int m_iID = 0;                                                                                 //记录所对应的BasePanel的索引（便于展现隐藏面板）
        private System.Windows.Forms.TabAlignment m_AlignmentStyle = System.Windows.Forms.TabAlignment.Bottom; //记录隐藏区按钮的绘制状态
        #endregion

        public HideAreaTabButton()
            : base()
        {
            this.eAlignmentStyle = TabAlignment.Bottom;
        }

        public HideAreaTabButton(int id, string text, Image image, TabAlignment eAlignment)
            : base()
        {
            base.Text = text;
            base.Image = image;
            this.m_iID = id;
            this.eAlignmentStyle = eAlignment;
        }

        #region 覆盖
        protected override void OnLayout(LayoutEventArgs levent)
        {
            switch (this.eAlignmentStyle)
            {
                case TabAlignment.Top:
                case TabAlignment.Bottom:
                    this.Height = CRT_HEIGHT;
                    break;
                case TabAlignment.Left:
                case TabAlignment.Right:
                    this.Width = CRT_HEIGHT;
                    break;
            }
            base.OnLayout(levent);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
            if (this.Text == null || this.Text.Length <= 0) this.Text = " ";
            //
            SizeF size = e.Graphics.MeasureString(this.Text, this.Font);
            switch (this.eAlignmentStyle)
            {
                case TabAlignment.Top:
                case TabAlignment.Bottom:
                    this.Height = CRT_HEIGHT;
                    if (this.Image == null)
                    {
                        this.Width = (int)(size.Width) + 4;//this.Width = 2 + (int)(size.Width + 1) + 2;
                    }
                    else { this.Width = (int)(size.Width) + 20; }//this.Width = 2 + 16 + (int)(size.Width + 1) + 2;
                    break;
                case TabAlignment.Left:
                case TabAlignment.Right:
                    this.Width = CRT_HEIGHT;
                    if (this.Image == null)
                    {
                        this.Height = (int)(size.Width) + 4;//this.Width = 2 + (int)(size.Width + 1) + 2;
                    }
                    else { this.Height = (int)(size.Width) + 20; }//this.Width = 2 + 16 + (int)(size.Width + 1) + 2;
                    break;
            }
            //
            DrawTabButton(e.Graphics);
        }
        #endregion

        #region Property 属性
        [Browsable(false), DefaultValue(DockStyle.None)]
        public override DockStyle Dock
        {
            get
            {
                return base.Dock;
            }
            set
            {
                base.Dock = DockStyle.None;
            }
        }

        [Browsable(false)]
        public int iID//记录所对应的BasePanel的索引
        {
            get { return m_iID; }
        }

        [Browsable(false)]
        public System.Windows.Forms.TabAlignment eAlignmentStyle//记录隐藏区按钮的绘制状态
        {
            get { return m_AlignmentStyle; }
            set { m_AlignmentStyle = value; }
        }
        #endregion

        internal int LayoutSize()//获取布局寸（活动尺寸）
        {
            switch (this.eAlignmentStyle)
            {
                case TabAlignment.Top:
                case TabAlignment.Bottom:
                    return this.Width;
                case TabAlignment.Left:
                case TabAlignment.Right:
                    return this.Height;
                default:
                    return 0;
            }
        }

        #region Draw 绘制函数
        private void DrawTabButton(Graphics g)
        {
            g.FillRectangle(new SolidBrush(LiuZhenHong.Controls.Renderer.RendererManager.Renderer.ColorTableEx.PanelBackgroundMiddle), this.DisplayRectangle);
            //
            switch (this.eAlignmentStyle)
            {
                case TabAlignment.Top:
                    DrawTabButtonTop(g, new Pen(LiuZhenHong.Controls.Renderer.RendererManager.Renderer.ColorTableEx.BorderLine), new Rectangle(0, 0, this.Width - 1, this.Height - 3));
                    break;
                case TabAlignment.Bottom:
                    DrawTabButtonBottom(g, new Pen(LiuZhenHong.Controls.Renderer.RendererManager.Renderer.ColorTableEx.BorderLine), new Rectangle(0, 3, this.Width - 1, this.Height));
                    break;
                case TabAlignment.Left:
                    DrawTabButtonLeft(g, new Pen(LiuZhenHong.Controls.Renderer.RendererManager.Renderer.ColorTableEx.BorderLine), new Rectangle(0, 1, this.Width - 2, this.Height - 1));
                    break;
                case TabAlignment.Right:
                    DrawTabButtonRight(g, new Pen(LiuZhenHong.Controls.Renderer.RendererManager.Renderer.ColorTableEx.BorderLine), new Rectangle(2, 1, this.Width, this.Height - 1));
                    break;
            }
        }

        private void DrawTabButtonTop(Graphics g, Pen pen, Rectangle rectangle)
        {
            switch (this._RenderableButtonState)
            {
                case RenderableButtonState.eHot:
                    g.FillRectangle(new LinearGradientBrush(this.DisplayRectangle,
                        LiuZhenHong.Controls.Renderer.RendererManager.Renderer.ColorTableEx.ButtonCheckedGradientEnd,
                        LiuZhenHong.Controls.Renderer.RendererManager.Renderer.ColorTableEx.ButtonCheckedGradientBegin,
                        LinearGradientMode.Vertical),
                        rectangle);
                    break;
                case RenderableButtonState.ePressed:
                    g.FillRectangle(new LinearGradientBrush(this.DisplayRectangle,
                        LiuZhenHong.Controls.Renderer.RendererManager.Renderer.ColorTableEx.ButtonNormalEnd,
                        LiuZhenHong.Controls.Renderer.RendererManager.Renderer.ColorTableEx.ButtonNormalBegin,
                        LinearGradientMode.Vertical),
                        rectangle);
                    break;
                case RenderableButtonState.eDisable:
                case RenderableButtonState.eNormal:
                    g.FillRectangle(new LinearGradientBrush(this.DisplayRectangle,
                        LiuZhenHong.Controls.Renderer.RendererManager.Renderer.ColorTableEx.ButtonNormalEnd,
                        LiuZhenHong.Controls.Renderer.RendererManager.Renderer.ColorTableEx.ButtonNormalBegin,
                        LinearGradientMode.Vertical),
                        rectangle);
                    break;
            }
            g.DrawLine(pen, 0, 0, 0, 17);
            g.DrawArc(pen, new Rectangle(-1, 10, 10, 10), 90, 90);
            g.DrawLine(pen, 3, 20, this.Width - 6, 20);
            g.DrawArc(pen, new Rectangle(this.Width - 10, 10, 10, 10), 360, 90);
            g.DrawLine(pen, this.Width - 1, 0, this.Width - 1, 17);
            //
            StringFormat drawFormat = new StringFormat();
            drawFormat.FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.FitBlackBox;
            drawFormat.Trimming = StringTrimming.EllipsisCharacter;
            if (this.Image == null)
            {
                DrawString(g, this.Text, new Rectangle(2, 5, this.Width, 14), drawFormat);
            }
            else
            {
                g.DrawImage(this.Image, new Rectangle(2, 2, 16, 16));
                DrawString(g, this.Text, new Rectangle(18, 5, this.Width, 14), drawFormat);
            }
        }

        private void DrawTabButtonBottom(Graphics g, Pen pen, Rectangle rectangle)
        {
            switch (this._RenderableButtonState)
            {
                case RenderableButtonState.eHot:
                    g.FillRectangle(new LinearGradientBrush(this.DisplayRectangle,
                        LiuZhenHong.Controls.Renderer.RendererManager.Renderer.ColorTableEx.ButtonCheckedGradientEnd,
                        LiuZhenHong.Controls.Renderer.RendererManager.Renderer.ColorTableEx.ButtonCheckedGradientBegin,
                        LinearGradientMode.Vertical),
                        rectangle);
                    break;
                case RenderableButtonState.ePressed:
                    g.FillRectangle(new LinearGradientBrush(this.DisplayRectangle,
                        LiuZhenHong.Controls.Renderer.RendererManager.Renderer.ColorTableEx.ButtonNormalEnd,
                        LiuZhenHong.Controls.Renderer.RendererManager.Renderer.ColorTableEx.ButtonNormalBegin,
                        LinearGradientMode.Vertical),
                        rectangle);
                    break;
                case RenderableButtonState.eDisable:
                case RenderableButtonState.eNormal:
                    g.FillRectangle(new LinearGradientBrush(this.DisplayRectangle,
                        LiuZhenHong.Controls.Renderer.RendererManager.Renderer.ColorTableEx.ButtonNormalEnd,
                        LiuZhenHong.Controls.Renderer.RendererManager.Renderer.ColorTableEx.ButtonNormalBegin,
                        LinearGradientMode.Vertical),
                        rectangle);
                    break;
            }
            g.DrawLine(pen, 0, 3, 0, 23);
            g.DrawArc(pen, new Rectangle(-1, 2, 10, 10), 180, 90);
            g.DrawLine(pen, 4, 2, this.Width - 6, 2);
            g.DrawArc(pen, new Rectangle(this.Width - 10, 2, 10, 10), 270, 90);
            g.DrawLine(pen, this.Width - 1, 3, this.Width - 1, 23);
            //
            StringFormat drawFormat = new StringFormat();
            drawFormat.FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.FitBlackBox;
            drawFormat.Trimming = StringTrimming.EllipsisCharacter;
            if (this.Image == null)
            {
                DrawString(g, this.Text, new Rectangle(2, 7, this.Width, 14), drawFormat);
            }
            else
            {
                g.DrawImage(this.Image, new Rectangle(2, 5, 16, 16));
                DrawString(g, this.Text, new Rectangle(18, 7, this.Width, 14), drawFormat);
            }
        }

        private void DrawTabButtonLeft(Graphics g, Pen pen, Rectangle rectangle)
        {
            switch (this._RenderableButtonState)
            {
                case RenderableButtonState.eHot:
                    g.FillRectangle(new LinearGradientBrush(this.DisplayRectangle,
                        LiuZhenHong.Controls.Renderer.RendererManager.Renderer.ColorTableEx.ButtonCheckedGradientEnd,
                        LiuZhenHong.Controls.Renderer.RendererManager.Renderer.ColorTableEx.ButtonCheckedGradientBegin,
                        LinearGradientMode.Horizontal),
                        rectangle);
                    break;
                case RenderableButtonState.ePressed:
                    g.FillRectangle(new LinearGradientBrush(this.DisplayRectangle,
                        LiuZhenHong.Controls.Renderer.RendererManager.Renderer.ColorTableEx.ButtonNormalEnd,
                        LiuZhenHong.Controls.Renderer.RendererManager.Renderer.ColorTableEx.ButtonNormalBegin,
                        LinearGradientMode.Horizontal),
                        rectangle);
                    break;
                case RenderableButtonState.eDisable:
                case RenderableButtonState.eNormal:
                    g.FillRectangle(new LinearGradientBrush(this.DisplayRectangle,
                        LiuZhenHong.Controls.Renderer.RendererManager.Renderer.ColorTableEx.ButtonNormalEnd,
                        LiuZhenHong.Controls.Renderer.RendererManager.Renderer.ColorTableEx.ButtonNormalBegin,
                        LinearGradientMode.Horizontal),
                        rectangle);
                    break;
            }
            g.DrawLine(pen, 0, 0, this.Width - 6, 0);
            g.DrawArc(pen, new Rectangle(this.Width - 13, -1, 10, 10), 270, 90);
            g.DrawLine(pen, this.Width - 3, 4, this.Width - 3, this.Height - 6);
            g.DrawArc(pen, new Rectangle(this.Width - 13, this.Height - 10, 10, 10), 0, 90);
            g.DrawLine(pen, 0, this.Height - 1, this.Width - 6, this.Height - 1);
            //
            StringFormat drawFormat = new StringFormat();
            drawFormat.FormatFlags = StringFormatFlags.DirectionVertical;
            drawFormat.Trimming = StringTrimming.EllipsisCharacter;
            if (this.Image == null)
            {
                DrawString(g, this.Text, new Rectangle(2, 2, 14, this.Height), drawFormat);
            }
            else
            {
                g.DrawImage(this.Image, new Rectangle(2, 2, 16, 16));
                DrawString(g, this.Text, new Rectangle(2, 18, 14, this.Height - 16), drawFormat);
            }
        }

        private void DrawTabButtonRight(Graphics g, Pen pen, Rectangle rectangle)
        {
            switch (this._RenderableButtonState)
            {
                case RenderableButtonState.eHot:
                    g.FillRectangle(new LinearGradientBrush(this.DisplayRectangle,
                        LiuZhenHong.Controls.Renderer.RendererManager.Renderer.ColorTableEx.ButtonCheckedGradientEnd,
                        LiuZhenHong.Controls.Renderer.RendererManager.Renderer.ColorTableEx.ButtonCheckedGradientBegin,
                        LinearGradientMode.Horizontal),
                        rectangle);
                    break;
                case RenderableButtonState.ePressed:
                    g.FillRectangle(new LinearGradientBrush(this.DisplayRectangle,
                        LiuZhenHong.Controls.Renderer.RendererManager.Renderer.ColorTableEx.ButtonNormalEnd,
                        LiuZhenHong.Controls.Renderer.RendererManager.Renderer.ColorTableEx.ButtonNormalBegin,
                        LinearGradientMode.Horizontal),
                        rectangle);
                    break;
                case RenderableButtonState.eDisable:
                case RenderableButtonState.eNormal:
                    g.FillRectangle(new LinearGradientBrush(this.DisplayRectangle,
                        LiuZhenHong.Controls.Renderer.RendererManager.Renderer.ColorTableEx.ButtonNormalEnd,
                        LiuZhenHong.Controls.Renderer.RendererManager.Renderer.ColorTableEx.ButtonNormalBegin,
                        LinearGradientMode.Horizontal),
                        rectangle);
                    break;
            }
            g.DrawLine(pen, 4, 0, this.Width, 0);
            g.DrawArc(pen, new Rectangle(2, -1, 10, 10), 180, 90);
            g.DrawLine(pen, 2, 3, 2, this.Height - 6);
            g.DrawArc(pen, new Rectangle(2, this.Height - 10, 10, 10), 90, 90);
            g.DrawLine(pen, 4, this.Height - 1, this.Width, this.Height - 1);
            //
            StringFormat drawFormat = new StringFormat();
            drawFormat.FormatFlags = StringFormatFlags.DirectionVertical;
            drawFormat.Trimming = StringTrimming.EllipsisCharacter;
            if (this.Image == null)
            {
                base.DrawString(g, this.Text, new Rectangle(5, 2, 14, this.Height), drawFormat);
            }
            else
            {
                g.DrawImage(this.Image, new Rectangle(5, 2, 16, 16));
                DrawString(g, this.Text, new Rectangle(5, 18, 14, this.Height - 16), drawFormat);
            }
        }
        #endregion

    }
}
