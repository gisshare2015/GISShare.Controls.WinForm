using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace GISShare.Controls.Plugin
{
    public class ItemDef : IItemDef
    {
        /// <summary>
        /// 是否存在新组
        /// </summary>
        bool m_Group;
        public bool Group
        {
            get { return m_Group; }
            set { m_Group = value; }
        }

        /// <summary>
        /// Item ID号
        /// </summary>
        string m_ID;
        public string ID
        {
            get { return m_ID; }
            set { m_ID = value; }
        }
    }
}
