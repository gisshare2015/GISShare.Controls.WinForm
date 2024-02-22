using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;

namespace GISShare.Controls.WinForm.WFNew.View
{
    public class TextEditViewItem : TextViewItem, ITextEditViewItem, IInputObject
    {
        private const int CONST_INPUTREGIONSPACE = 1;

        public TextEditViewItem() { }

        public TextEditViewItem(string text)
            : base(text) { }

        public TextEditViewItem(string name, string text)
            : base(name, text) { }

        public TextEditViewItem(string name, string text, Font font)
            : base(name, text, font) { }

        #region ITextEditViewItem
        bool m_CanEdit = true;
        [Browsable(true), DefaultValue(false), Description("是否可以编辑"), Category("状态")]
        public virtual bool CanEdit
        {
            get { return m_CanEdit; }
            set { m_CanEdit = value; }
        }

        bool m_CanSelect = true;
        [Browsable(true), DefaultValue(true), Description("是否可以选择"), Category("状态")]
        public virtual bool CanSelect
        {
            get { return m_CanSelect; }
            set { m_CanSelect = value; }
        }

        [Browsable(false), Description("当前编辑对象"), Category("属性")]
        object ITextEditViewItem.EditObject
        {
            get { return this.ITextEditViewItem_EditObject; }
        }
        internal protected virtual object ITextEditViewItem_EditObject 
        {
            get { return this; }
        }
        #endregion

        #region IInputObject
        string IInputObject.InputText
        {
            get { return this.IInputObject_InputText; }
            set { this.IInputObject_InputText = value; }
        }
        internal protected virtual string IInputObject_InputText
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        Font IInputObject.InputFont
        {
            get { return this.IInputObject_InputFont; }
        }
        internal protected virtual Font IInputObject_InputFont
        {
            get { return base.Font; }
        }

        Color IInputObject.InputForeColor
        {
            get { return this.IInputObject_ForeColor; }
        }
        internal protected virtual Color IInputObject_ForeColor
        {
            get { return base.ForeColor; }
        }

        [Browsable(false), Description("输入区矩形框（客户区矩形）"), Category("布局")]
        Rectangle IInputObject.InputRegionRectangle
        {
            get { return this.IInputObject_InputRegionRectangle; }
        }
        internal protected virtual Rectangle IInputObject_InputRegionRectangle
        {
            get
            {
                Rectangle rectangle = this.DisplayRectangle;
                return new Rectangle
                    (
                    rectangle.Left + CONST_INPUTREGIONSPACE,
                    (rectangle.Top + rectangle.Bottom - this.m_TextSize.Height) / 2,
                    rectangle.Width - 2 * CONST_INPUTREGIONSPACE,
                    this.m_TextSize.Height
                    );
            }
        }

        [Browsable(false), Description("是否正在输入（与此类无关，请从寄宿者上查看状态）"), Category("状态")]
        bool IInputObject.IsInputing
        {
            get
            {
                return false;
            }
        }
        #endregion

        private Size m_TextSize = new Size(21, 21);
        protected override void OnDraw(System.Windows.Forms.PaintEventArgs e)
        {
            if (this.Text.Length <= 0)
            {
                this.m_TextSize = new Size(21, 21);
            }
            else
            {
                SizeF size = e.Graphics.MeasureString(this.Text, this.Font);
                this.m_TextSize = new Size((int)size.Width + 1, (int)size.Height + 1);
            }
            //
            base.OnDraw(e);
        }
    }
}
