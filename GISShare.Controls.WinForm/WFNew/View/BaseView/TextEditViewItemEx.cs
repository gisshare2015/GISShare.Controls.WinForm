using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew.View
{
    public class TextEditViewItemEx : TextEditViewItem
    {
        public TextEditViewItemEx() { }

        public TextEditViewItemEx(string text)
            : base(text) { }

        public TextEditViewItemEx(string name, string text)
            : base(text, name) { }

        public TextEditViewItemEx(string name, string text, Font font)
            : base(name, text, font) { }

        private object m_Tag;
        [Browsable(true), DefaultValue(""), TypeConverter(typeof(StringConverter)), Description("用来携带附加信息"), Category("数据")]
        public object Tag
        {
            get { return m_Tag; }
            set { m_Tag = value; }
        }
    }
}
