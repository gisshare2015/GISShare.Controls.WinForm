using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GISShare.Controls.Plugin
{
    [System.Serializable]
    public class SubItemSerializableObject : ItemDefSerializableObject, ISubItem
    {
        List<ItemDefSerializableObject> m_Items = new List<ItemDefSerializableObject>();
        public List<ItemDefSerializableObject> Items
        {
            get { return m_Items; }
            set { m_Items = value; }
        }

        #region ISubItem
        /// <summary>
        /// 携带的 Item 数量
        /// </summary>
        public int ItemCount
        {
            get
            {
                return this.Items.Count;
            }
        }

        /// <summary>
        /// 访问快捷菜单中每个Item的方法
        /// </summary>
        /// <param name="iIndex"></param>
        /// <param name="pItemDef"></param>
        public virtual void GetItemInfo(int iIndex, IItemDef pItemDef)
        {
            pItemDef.ID = this.Items[iIndex].ID;
            pItemDef.Group = this.Items[iIndex].Group;
        }
        #endregion
    }
}
