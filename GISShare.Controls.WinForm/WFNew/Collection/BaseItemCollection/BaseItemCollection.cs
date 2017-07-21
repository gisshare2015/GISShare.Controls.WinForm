using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    public class BaseItemCollection : IFlexibleList, IList, ICollection, IEnumerable, ILockCollectionHelper, ISetOwnerHelper
    {
        public event ItemEventHandler ItemAdded;
        public event ItemEventHandler ItemRemoved;

        private IOwner owner = null;
        private List<BaseItem> innerList = null;

        protected IOwner _Owner
        {
            get { return owner; }
        }

        public BaseItemCollection(IOwner pOwner)
        {
            this.owner = pOwner;
            this.innerList = new List<BaseItem>();
        }

        #region 集合过滤函数
        /// <summary>
        /// 符合条件返回true。如果SetItemAttribute也返回true 则 将value加到集合中去
        /// </summary>
        /// <returns></returns>
        protected virtual bool Filtration(BaseItem value)
        {
            return !this.Contains(value);
            //return true;
        }

        /// <summary>
        /// 设置添加项的部分属性成功返回true。如果Filtration也返回true 则 将value加到集合中去
        /// </summary>
        /// <param name="value"></param>
        protected virtual bool SetItemAttribute(BaseItem value)
        {
            ((ISetOwnerHelper)value).SetOwner(this.owner);
            View.ISetViewItemHelper pSetViewItemHelper = value as View.ISetViewItemHelper;
            if (pSetViewItemHelper != null) pSetViewItemHelper.SetViewParameterStyle(ViewParameterStyle.eNone);
            return true;
        }

        /// <summary>
        /// 在移除项时将添加时的部分属性还原，成功返回true。如果成功则移除该项
        /// </summary>
        /// <param name="value"></param>
        protected virtual bool RestoreItemAttribute(BaseItem value)
        {
            if (this.owner == value.pOwner) ((ISetOwnerHelper)value).SetOwner(null);
            View.ISetViewItemHelper pSetViewItemHelper = value as View.ISetViewItemHelper;
            if (pSetViewItemHelper != null) pSetViewItemHelper.SetViewParameterStyle(ViewParameterStyle.eNone);
            return true;
        }
        #endregion

        #region 常规集合函数
        public int Add(BaseItem value)
        {
            if (this.Locked) return -1;
            //
            if (value == null) return -1;
            //
            if (!this.Filtration(value) || 
                !this.SetItemAttribute(value)) return -1;
            //
            this.innerList.Add(value);
            //this.UIUpdata(value.Location);
            if (this._Owner != null) this._Owner.Refresh();
            this.OnItemAdded(new ItemEventArgs(value));
            //
            return this.Count - 1;
        }

        public void Insert(int index, BaseItem value)
        {
            if (this.Locked) return;
            //
            if (value == null) return;
            //
            if ((index < 0) || (index > this.innerList.Count)) return;
            //
            if (!this.Filtration(value) ||
                !this.SetItemAttribute(value)) return;
            //
            this.innerList.Insert(index, value);
            //this.UIUpdata(value.Location);
            if (this._Owner != null) this._Owner.Refresh();
            this.OnItemAdded(new ItemEventArgs(value));
        }

        public bool Contains(BaseItem value)
        {
            if (value == null) return false;
            //
            return this.innerList.Contains(value);
        }

        public int IndexOf(BaseItem value)
        {
            if (value == null) return -1;
            //
            return this.innerList.IndexOf(value);
        }

        public void Remove(BaseItem value)
        {
            if (this.Locked) return;
            //
            if (value == null) return;
            //
            if (!this.RestoreItemAttribute(value)) return;
            //
            this.innerList.Remove(value);
            //this.UIUpdata(value.Location);
            if (this._Owner != null) this._Owner.Refresh();
            this.OnItemRemoved(new ItemEventArgs(value));
        }

        public void RemoveAt(int index)
        {
            if (this.Locked) return;
            //
            BaseItem baseItem = this.innerList[index];
            if (baseItem == null || !this.RestoreItemAttribute(baseItem)) return;
            //
            this.innerList.RemoveAt(index);
            //this.UIUpdata(baseItem.Location);
            if (this._Owner != null) this._Owner.Refresh();
            this.OnItemRemoved(new ItemEventArgs(baseItem));
        }

        public void Clear()
        {
            if (this.Locked) return;
            //
            for (int i = this.innerList.Count - 1; i >= 0; i--)
            {
                this.RemoveAt(i);
            }
        }

        public virtual BaseItem this[int index]
        {
            get
            {
                return this.innerList[index];
            }
            set
            {
                if (this.Locked) return;
                //
                if (!this.Filtration(value)) return;
                //
                if (!this.SetItemAttribute(value)) return;
                if (!this.RestoreItemAttribute(this.innerList[index])) return;
                this.innerList[index] = value;
            }
        }

        public virtual BaseItem this[string name]
        {
            get
            {
                foreach(BaseItem one in this.innerList)
                {
                    if (one.Name == name) return one;
                }
                //
                return null;
            }
        }
        #endregion

        #region 集合辅助方法
        /// <summary>
        /// 是否与集合的拥有者相同
        /// </summary>
        /// <param name="pOwner"></param>
        /// <returns></returns>
        public bool OwnerEquals(IOwner pOwner)
        {
            return pOwner == this.owner;
        }

        /// <summary>
        /// item1 与 item2 互换位置
        /// </summary>
        /// <param name="item1"></param>
        /// <param name="item2"></param>
        public bool ExchangeItemT(BaseItem item1, BaseItem item2)
        {
            if (this.Locked) return false;
            //
            if (item1 == null || item2 == null || item1 == item2) return false;
            //
            int index1 = this.innerList.IndexOf(item1);
            int index2 = this.innerList.IndexOf(item2);
            //
            if (index1 == index2) return false;
            if ((index1 < 0) || (index1 >= this.innerList.Count)) return false;
            if ((index2 < 0) || (index2 >= this.innerList.Count)) return false;
            //
            if (index1 < index2)
            {
                this.innerList.Remove(item2);
                this.innerList.Remove(item1);
                this.innerList.Insert(index1, item2);
                this.innerList.Insert(index2, item1);
            }
            else
            {
                this.innerList.Remove(item1);
                this.innerList.Remove(item2);
                this.innerList.Insert(index2, item1);
                this.innerList.Insert(index1, item2);
            }
            //
            return true;
        }

        /// <summary>
        /// 获取可见项总数
        /// </summary>
        /// <param name="bVisible"></param>
        /// <returns></returns>
        public int GetItemCount(bool bVisible)
        {
            int iCount = 0;
            foreach (BaseItem one in this.innerList)
            {
                if (one.Visible == bVisible) iCount++;
            }
            return iCount;
        }

        /// <summary>
        /// 获取可见项总数
        /// </summary>
        /// <param name="bVisible"></param>
        /// <param name="iFormIndex"></param>
        /// <param name="iEndIndex"></param>
        /// <returns></returns>
        public int GetItemCount(bool bVisible, int iFormIndex , int iEndIndex)
        {
            if (iFormIndex < 0) iFormIndex = 0;
            if (iEndIndex >= this.Count) iEndIndex = this.Count - 1;
            //
            int iCount = 0;
            for (int i = iFormIndex; i <= iEndIndex; i++)
            {
                if (this[i].Visible == bVisible) iCount++;
            }
            return iCount;
        }

        /// <summary>
        /// 获取第一个可见项
        /// </summary>
        /// <param name="bVisible"></param>
        /// <returns></returns>
        public BaseItem GetStartItem(bool bVisible)
        {
            foreach (BaseItem one in this.innerList)
            {
                if (one.Visible == bVisible) return one;
            }
            return null;
        }

        /// <summary>
        /// 获取最后一个可见项
        /// </summary>
        /// <param name="bVisible"></param>
        /// <returns></returns>
        public BaseItem GetLastItem(bool bVisible)
        {
            BaseItem baseItem = null;
            foreach (BaseItem one in this.innerList)
            {
                if (one.Visible == bVisible) baseItem = one;
            }
            return baseItem;
        }

        /// <summary>
        /// 根据坐标点获取子项
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public BaseItem GetBaseItemFromPoint(Point point)
        {
            foreach (BaseItem one in this.innerList)
            {
                if (one.Contains(point)) return one;
            }
            return null;
        }

        /// <summary>
        /// 根据坐标点获取子项
        /// </summary>
        /// <param name="iX"></param>
        /// <param name="iY"></param>
        /// <returns></returns>
        public BaseItem GetBaseItemFromPoint(int iX, int iY) 
        {
            return GetBaseItemFromPoint(new Point(iX, iY));
        }
        #endregion

        #region ICollection
        public int Count
        {
            get
            {
                return this.innerList.Count;
            }
        }

        object ICollection.SyncRoot
        {
            get
            {
                return this;
            }
        }

        bool ICollection.IsSynchronized
        {
            get
            {
                return false;
            }
        }

        void ICollection.CopyTo(Array destination, int index)
        {
            BaseItem[] destinationArray = new BaseItem[this.Count];
            if (this.Count > 0)
            {
                this.innerList.CopyTo(destinationArray, index);
            }
            for (int i = 0; i < destinationArray.Length; i++)
            {
                destination.SetValue(destinationArray[i], i);
            }
        }
        #endregion

        #region IEnumerable
        public IEnumerator GetEnumerator()
        {
            return this.innerList.GetEnumerator();
        }
        #endregion

        #region IList
        int IList.Add(object value)
        {
            return this.Add(value as BaseItem);
        }

        bool IList.Contains(object value)
        {
            return this.Contains(value as BaseItem);
        }

        int IList.IndexOf(object value)
        {
            return this.IndexOf(value as BaseItem);
        }

        void IList.Insert(int index, object value)
        {
            this.Insert(index, value as BaseItem);
        }

        void IList.Remove(object value)
        {
            this.Remove(value as BaseItem);
        }

        bool IList.IsReadOnly
        {
            get
            {
                return this.Locked;
            }
        }

        bool IList.IsFixedSize
        {
            get
            {
                return false;
            }
        }

        object IList.this[int index]
        {
            get
            {
                return this[index];
            }
            set
            {
                BaseItem item = value as BaseItem;
                if (item == null)
                {
                    throw new ArgumentException("必须是IItem类型！", "value");
                }
                this[index] = item;
            }
        }
        #endregion

        #region ILockCollectionHelper
        private bool m_Locked = false;
        /// <summary>
        /// 是否已锁定（默认未加锁 false）
        /// </summary>
        public bool Locked
        {
            get { return m_Locked; }
        }

        void ILockCollectionHelper.SetLocked(bool locked)
        {
            this.m_Locked = locked;
        }
        #endregion

        #region IFlexibleList
        /// <summary>
        /// index1 与 index2 互换Index
        /// </summary>
        /// <param name="index1"></param>
        /// <param name="index2"></param>
        public bool ExchangeItem(int index1, int index2)
        {
            if (this.Locked) return false;
            //
            return this.ExchangeItemT(this.innerList[index1], this.innerList[index2]);
        }

        /// <summary>
        /// item1 与 item2 互换位置
        /// </summary>
        /// <param name="item1"></param>
        /// <param name="item2"></param>
        /// <returns></returns>
        public bool ExchangeItem(object item1, object item2)
        {
            if (this.Locked) return false;
            //
            return this.ExchangeItemT(item1 as BaseItem, item2 as BaseItem);
        }
        #endregion

        #region ISetOwnerHelper
        bool ISetOwnerHelper.SetOwner(IOwner pOwner)
        {
            if (this.owner == pOwner) return false;
            //
            this.owner = pOwner;
            foreach (ISetOwnerHelper one in this.innerList)
            {
                one.SetOwner(this.owner);
            }
            //
            return true;
        }
        #endregion

        public void SetRecordID()
        {
            for (int i = 0; i < this.innerList.Count; i++)
            {
                WFNew.ISetRecordItemHelper pSetRecordItemHelper = this.innerList[i] as WFNew.ISetRecordItemHelper;
                if (pSetRecordItemHelper != null) pSetRecordItemHelper.SetRecordID(i);
            }
        }

        //public List<string> GetCategoryList()
        //{
        //    List<string> strCategoryList = new List<string>();
        //    foreach (BaseItem one in this)
        //    {
        //        if (!strCategoryList.Contains(one.Category))
        //        {
        //            strCategoryList.Add(one.Category);
        //        }
        //    }
        //    return strCategoryList;
        //}

        //public List<BaseItem> GetCategoryItemList(string strCategory)
        //{
        //    List<BaseItem> categoryItemList = new List<BaseItem>();
        //    foreach (BaseItem one in this)
        //    {
        //        if (one.Category == strCategory)
        //        {
        //            categoryItemList.Add(one);
        //        }
        //    }
        //    return categoryItemList;
        //}

        //public Dictionary<string, List<BaseItem>> GetCategoryItemDictionary()
        //{
        //    Dictionary<string, List<BaseItem>> categoryItemDictionary = new Dictionary<string, List<BaseItem>>();
        //    foreach (BaseItem one in this)
        //    {
        //        if (categoryItemDictionary.ContainsKey(one.Category))
        //        {
        //            categoryItemDictionary[one.Category].Add(one);
        //        }
        //        else
        //        {
        //            categoryItemDictionary.Add(one.Category, new List<BaseItem> { one });
        //        }
        //    }
        //    return categoryItemDictionary;
        //}

        //

        protected void OnItemAdded(ItemEventArgs e)
        {
            if (this.ItemAdded != null) { this.ItemAdded(this, e); }
        }

        protected void OnItemRemoved(ItemEventArgs e)
        {
            if (this.ItemRemoved != null) { this.ItemRemoved(this, e); }
        }
    }
}