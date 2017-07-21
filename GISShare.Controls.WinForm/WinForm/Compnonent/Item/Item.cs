using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;

namespace GISShare.Controls.WinForm
{
    [Serializable, DefaultProperty("Text")]
    public class Item : IItem
    { 
        public Item()
        { }

        public Item(string name, string text)
        {
            this.m_Name = name;
            this.m_Text = text;
        }

        public Item(string name, string text, bool showBackColor, Color backColor)
        {
            this.m_Name = name;
            this.m_Text = text;
            this.m_ShowBackColor = showBackColor;
            this.m_BackColor = backColor;
        }

        #region IItem
        string m_Name = "Item";
        [Browsable(true), DefaultValue("Item"), Description("����"), Category("����")]
        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        string m_Text = "Item";
        [Browsable(true), DefaultValue("Item"), Description("�ı�"), Category("���")]
        public virtual string Text
        {
            get { return m_Text; }
            set
            {
                if (m_Text == value) return;
                m_Text = value;
            }
        }

        bool m_ShowBackColor = false;
        [Browsable(true), DefaultValue(false), Description("չʾ������ɫ"), Category("���")]
        public virtual bool ShowBackColor
        {
            get { return m_ShowBackColor; }
            set { m_ShowBackColor = value; }
        }

        private Color m_BackColor = System.Drawing.Color.Transparent;
        [Browsable(true), DefaultValue(typeof(Color), "System.Drawing.SystemColors.ControlText"), Description("������ɫ"), Category("���")]
        public virtual Color BackColor
        {
            get { return m_BackColor; }
            set { m_BackColor = value; }
        }

        private object m_Tag;
        [Browsable(true), DefaultValue(""), TypeConverter(typeof(StringConverter)), Description("����Я��������Ϣ"), Category("����")]
        public object Tag
        {
            get { return m_Tag; }
            set { m_Tag = value; }
        }
        #endregion

        public override string ToString()
        {
            if (this.Text != null && this.Text.Length > 0) return this.Text;
            else if (this.Name != null && this.Name.Length > 0) return this.Name;
            else return base.ToString();
        }
    }
}
