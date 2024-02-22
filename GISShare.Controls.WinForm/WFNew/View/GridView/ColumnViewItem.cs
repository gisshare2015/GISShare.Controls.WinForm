using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;

namespace GISShare.Controls.WinForm.WFNew.View
{
    public class ColumnViewItem : SuperViewItem, IColumnViewItem
    {
        #region IVisibleViewItem
        bool m_Visible = true;
        [Browsable(true), DefaultValue(true), Description("可见"), Category("状态")]
        public bool Visible
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

        #region IColumnViewItem
        string m_FieldName;
        [Browsable(true), Description("字段名称"), Category("描述")]
        public string FieldName
        {
            get
            {
                if (this.BaseItemObject != null) return this.BaseItemObject.Name; 
                return m_FieldName;
            }
            set
            {
                if (this.BaseItemObject != null) this.BaseItemObject.Name = value; 
                m_FieldName = value;
            }
        }
        #endregion

        public override Size MeasureSize(Graphics g)
        {
            return new Size(this.Width, this.Height);
        }

        protected override void OnDraw(System.Windows.Forms.PaintEventArgs e)
        {
            //base.OnDraw(e);
            Rectangle rectangle = Rectangle.FromLTRB(e.ClipRectangle.Left, e.ClipRectangle.Top, e.ClipRectangle.Right - 1, e.ClipRectangle.Bottom - 1);
            WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderColumnViewItem(new ObjectRenderEventArgs(e.Graphics, this, rectangle));
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
