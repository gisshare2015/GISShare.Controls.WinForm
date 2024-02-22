using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;

namespace GISShare.Controls.WinForm.WFNew.View
{
    public class TitleViewItem : TextViewItem, IVisibleViewItem, IViewItem
    {
        public TitleViewItem() { }

        public TitleViewItem(string text)
            : base(text) { }

        public TitleViewItem(string name, string text)
            : base(name, text) { }

        public TitleViewItem(string name, string text, Font font)
            : base(name, text, font) { }

        #region IVisibleViewItem
        bool m_Visible = true;
        [Browsable(true), DefaultValue(true), Description("可见"), Category("状态")]
        public virtual bool Visible
        {
            get { return m_Visible; }
            set
            {
                if (m_Visible == value) return;
                ((IMessageChain)this).SendMessage(new MessageInfo(this, MessageStyle.eMSVisibleChanged, new BoolValueChangedEventArgs(value)));
                m_Visible = value;
            }
        }
        #endregion

        //public override BaseItemState eBaseItemState
        //{
        //    get
        //    {
        //        return BaseItemState.eNormal;
        //    }
        //}

        //protected override bool RefreshBaseItemState
        //{
        //    get
        //    {
        //        return false;
        //    }
        //}

        [Browsable(false), DefaultValue(typeof(ViewParameterStyle), "eNone"), Description("视图伴随参数"), Category("属性")]
        ViewParameterStyle IViewItem.eViewParameterStyle
        {
            get { return ViewParameterStyle.eNone; }
        }
        
        protected override void OnDraw(System.Windows.Forms.PaintEventArgs e)
        {
            //base.OnDraw(e);
            Rectangle rectangle = Rectangle.FromLTRB(e.ClipRectangle.Left, e.ClipRectangle.Top, e.ClipRectangle.Right - 1, e.ClipRectangle.Bottom - 1);
            WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderTitleViewItem(new ObjectRenderEventArgs(e.Graphics, this, rectangle));
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
                    new StringFormat())
                );
        }
    }
}
