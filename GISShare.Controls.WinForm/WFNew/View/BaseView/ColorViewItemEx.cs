using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew.View
{
    public class ColorViewItemEx : ColorViewItem
    {
        public ColorViewItemEx() { }

        public ColorViewItemEx(string text)
            : base(text) { }

        public ColorViewItemEx(string name, string text)
            : base(name, text) { }

        private object m_Tag;
        [Browsable(true), DefaultValue(""), TypeConverter(typeof(StringConverter)), Description("����Я��������Ϣ"), Category("����")]
        public object Tag
        {
            get { return m_Tag; }
            set { m_Tag = value; }
        }
    }
}
