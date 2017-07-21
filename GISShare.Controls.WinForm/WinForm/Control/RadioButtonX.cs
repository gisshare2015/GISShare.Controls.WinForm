using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;
using System.ComponentModel;

namespace GISShare.Controls.WinForm
{
    public class RadioButtonX : RadioButton, IRadioButtonX
    {
        private const int CRT_CHECKSIZE = 11;
        private const int CRT_CHECKSPACE = 3;

        public RadioButtonX()
        {
            this.SetStyle(ControlStyles.Opaque, false);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.Selectable, false);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.UpdateStyles();
        }

        #region WFNew.IBaseItem
        WFNew.RenderStyle m_eRenderStyle = WFNew.RenderStyle.eSystem;
        [Browsable(true), DefaultValue(typeof(WFNew.RenderStyle), "eSystem"), Description("渲染类型"), Category("外观")]
        public virtual WFNew.RenderStyle eRenderStyle
        {
            get { return m_eRenderStyle; }
            set { m_eRenderStyle = value; }
        }

        private WFNew.BaseItemState m_eBaseItemState = WFNew.BaseItemState.eNormal;
        protected virtual bool SetBaseItemState(WFNew.BaseItemState baseItemState)
        {
            if (this.m_eBaseItemState == baseItemState) return false;
            this.m_eBaseItemState = baseItemState;
            return true;
        }
        protected virtual bool SetBaseItemStateEx(WFNew.BaseItemState baseItemState)
        {
            if (this.m_eBaseItemState == baseItemState) return false;
            this.m_eBaseItemState = baseItemState;
            this.Refresh();
            return true;
        }
        [Browsable(false), Description("自身所处的状态（激活、按下、不可用、正常）"), Category("状态")]
        public virtual WFNew.BaseItemState eBaseItemState
        {
            get
            {
                return m_eBaseItemState;
            }
        }
        #endregion
        
        #region IRadioButtonX
        int m_VOffset = -1;
        public int VOffset
        {
            get { return m_VOffset; }
            set { m_VOffset = value; }
        }

        [Browsable(false), Description("勾选区绘制矩形框"), Category("布局")]
        public virtual Rectangle CheckRectangle
        {
            get
            {
                Rectangle rectangle = this.DisplayRectangle;
                return new Rectangle
                    (
                    rectangle.X + this.Padding.Left,
                    rectangle.Y + this.Padding.Top + (rectangle.Height - this.Padding.Top - this.Padding.Bottom - CRT_CHECKSIZE) / 2,
                    CRT_CHECKSIZE,
                    CRT_CHECKSIZE
                    );
            }
        }

        [Browsable(false), Description("文本绘制矩形框"), Category("布局")]
        public virtual Rectangle TextRectangle
        {
            get
            {
                Rectangle rectangle = this.DisplayRectangle;
                return Rectangle.FromLTRB(this.CheckRectangle.Right + CRT_CHECKSPACE, rectangle.Top + this.VOffset, rectangle.Right, rectangle.Bottom);
            }
        }
        #endregion

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            this.SetBaseItemStateEx(WFNew.BaseItemState.ePressed);
            base.OnMouseDown(mevent);
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            if (this.DisplayRectangle.Contains(mevent.Location)) { this.SetBaseItemStateEx(WFNew.BaseItemState.eHot); }
            else { this.SetBaseItemStateEx(WFNew.BaseItemState.eNormal); }
            base.OnMouseUp(mevent);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            this.SetBaseItemState(WFNew.BaseItemState.eNormal);
            base.OnMouseLeave(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            this.SetBaseItemState(WFNew.BaseItemState.eHot);
            base.OnMouseEnter(e);
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            //base.OnPaint(pevent);
            //
            this.OnDraw(pevent);
        }

        protected virtual void OnDraw(PaintEventArgs e)
        {
            GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderRadioButton(
                new GISShare.Controls.WinForm.ObjectRenderEventArgs(e.Graphics, this, this.DisplayRectangle));
            GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderText(
                new GISShare.Controls.WinForm.TextRenderEventArgs(e.Graphics, this, this.Enabled, true, this.Text, this.ForeColor, this.Font, this.TextRectangle));
        }
    }
}

