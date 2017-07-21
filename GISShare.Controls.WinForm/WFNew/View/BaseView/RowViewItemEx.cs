using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew.View
{
    public class RowViewItemEx : RowViewItem
    {
        public RowViewItemEx() { }

        public RowViewItemEx(string text)
            : base(text) { }

        public RowViewItemEx(string name, string text)
            : base(name, text) { }

        public RowViewItemEx(string name, string text, Font font)
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