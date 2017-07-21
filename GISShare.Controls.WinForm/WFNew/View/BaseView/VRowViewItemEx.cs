using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew.View
{
    public class VRowViewItemEx : VRowViewItem
    {
        public VRowViewItemEx() { }

        public VRowViewItemEx(string text)
            : base(text) { }

        public VRowViewItemEx(string name, string text)
            : base(name, text) { }

        public VRowViewItemEx(string name, string text, Font font)
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
