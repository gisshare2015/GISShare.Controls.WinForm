using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew.View
{
    public class TextViewItemEx : TextViewItem
    {
        public TextViewItemEx() { }

        public TextViewItemEx(string text)
            : base(text) { }

        public TextViewItemEx(string name, string text)
            : base(name, text) { }

        public TextViewItemEx(string name, string text, Font font)
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
