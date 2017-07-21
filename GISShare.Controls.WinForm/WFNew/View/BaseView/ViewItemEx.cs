using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew.View
{
   public  class ViewItemEx : ViewItem
   {
       private object m_Tag;
       [Browsable(true), DefaultValue(""), TypeConverter(typeof(StringConverter)), Description("用来携带附加信息"), Category("数据")]
       public object Tag
       {
           get { return m_Tag; }
           set { m_Tag = value; }
       }
    }
}
