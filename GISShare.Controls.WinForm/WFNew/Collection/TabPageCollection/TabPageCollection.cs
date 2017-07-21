using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    public class TabPageCollection : WFNew.IFlexibleList, IList, ICollection, IEnumerable, WFNew.ILockCollectionHelper
    {
        public event ItemEventHandler ItemAdded;
        public event ItemEventHandler ItemRemoved;

        private ITabControlHelper m_Owner = null;

        internal TabPageCollection(ITabControlHelper pTabControlHelper)
        {
            this.m_Owner = pTabControlHelper;
        }

        #region ILockCollectionHelper
        /// <summary>
        /// 是否已锁定（默认未加锁 false）
        /// </summary>
        public bool Locked
        {
            get { return this.m_Owner.TabButtonItemCollection.Locked; }
        }

        void WFNew.ILockCollectionHelper.SetLocked(bool locked)
        {
            ((WFNew.ILockCollectionHelper)this.m_Owner.TabButtonItemCollection).SetLocked(locked);
        }
        #endregion

        /// <summary>
        /// 符合条件返回true。如果SetItemAttribute也返回true 则 将value加到集合中去
        /// </summary>
        /// <returns></returns>
        protected virtual bool Filtration(ITabPageItem value)
        {
            return !this.Contains(value);
        }

        /// <summary>
        /// 设置添加项的部分属性成功返回true。如果Filtration也返回true 则 将value加到集合中去
        /// </summary>
        /// <param name="value"></param>
        protected virtual bool SetItemAttribute(ITabPageItem value)
        {
            ((WFNew.ISetOwnerHelper)value).SetOwner(this.m_Owner as WFNew.IOwner);
            return true;
        }

        /// <summary>
        /// 在移除项时将添加时的部分属性还原，成功返回true。如果成功则移除该项
        /// </summary>
        /// <param name="value"></param>
        protected virtual bool RestoreItemAttribute(ITabPageItem value)
        {
            //if (this.m_Owner == value.pOwner) ((WFNew.ISetOwnerHelper)value).SetOwner(null);
            return true;
        }

        public int Add(ITabPageItem value)
        {
            if (this.Locked) return -1;
            //
            if (value == null) return -1;
            //
            if (!this.Filtration(value) ||
                !this.SetItemAttribute(value)) return -1;
            //
            int iReturn = this.m_Owner.TabButtonItemCollection.Add(value.pTabButtonItem as BaseItem);
            this.m_Owner.TabPageList.Add(value);
            //
            this.OnItemAdded(new ItemEventArgs(value));
            //
            return iReturn;
        }

        public bool Contains(ITabPageItem value)
        {
            if (value == null) return false;
            //
            return this.m_Owner.TabPageList.Contains(value) &&
                this.m_Owner.TabButtonItemCollection.Contains(value.pTabButtonItem as BaseItem);
        }

        public void Insert(int index, ITabPageItem value)
        {
            if (this.Locked) return;
            //
            if (value == null) return;
            //
            if ((index < 0) || (index >= this.Count)) return;
            //
            if (!this.Filtration(value) ||
                !this.SetItemAttribute(value)) return;
            //
            this.m_Owner.TabButtonItemCollection.Insert(index, value.pTabButtonItem as BaseItem);
            this.m_Owner.TabPageList.Add(value);
            //
            this.OnItemAdded(new ItemEventArgs(value));
        }

        public int IndexOf(ITabPageItem value)
        {
            if (value == null) return -1;
            //
            return this.m_Owner.TabButtonItemCollection.IndexOf(value.pTabButtonItem as BaseItem);
        }

        public void Remove(ITabPageItem value)
        {
            if (this.Locked) return;
            //
            if (value == null) return;
            //
            if (!this.RestoreItemAttribute(value)) return;
            //
            this.m_Owner.TabButtonItemCollection.Remove(value.pTabButtonItem as BaseItem);
            this.m_Owner.TabPageList.Remove(value);
            //
            this.OnItemRemoved(new ItemEventArgs(value));
        }

        public void RemoveAt(int index)
        {
            if (this.Locked) return;
            //
            ITabButtonItem item = this.m_Owner.TabButtonItemCollection[index] as ITabButtonItem;
            if (item == null) return;
            //
            this.m_Owner.TabButtonItemCollection.RemoveAt(index);
            this.m_Owner.TabPageList.Remove(item.pTabPageItem);
            //
            this.OnItemRemoved(new ItemEventArgs(item.pTabPageItem));
        }

        public void Clear()
        {
            if (this.Locked) return;
            // 
            for (int i = this.Count - 1; i >= 0; i--)
            {
                this.RemoveAt(i);
            }
        }

        public virtual ITabPageItem this[int index]
        {
            get
            {
                ITabButtonItem item = this.m_Owner.TabButtonItemCollection[index] as ITabButtonItem;
                if (item == null) return null;
                return item.pTabPageItem;
            }
            set
            {
                if (this.Locked) return;
                //
                if (!this.Filtration(value)) return;
                //
                this.RemoveAt(index);
                this.Insert(index, value);
            }
        }

        public virtual ITabPageItem this[string name]
        {
            get
            {
                foreach (ITabButtonItem one in this.m_Owner.TabButtonItemCollection)
                {
                    if (one != null && one.Name == name) return one.pTabPageItem;
                }
                //
                return null;
            }
        }

        /// <summary>
        /// 用来检测和修复该集合
        /// </summary>
        public void CheckAndRepair()
        {
            for (int i = this.m_Owner.TabButtonItemCollection.Count - 1; i >= 0; i--)
            {
                ITabButtonItem pTabButtonItem = this.m_Owner.TabButtonItemCollection[i] as ITabButtonItem;
                if (pTabButtonItem != null && 
                    !this.m_Owner.TabPageList.Contains(pTabButtonItem.pTabPageItem))
                {
                    this.m_Owner.TabButtonItemCollection.RemoveAt(i);
                }
            }
            //
            for (int i = this.m_Owner.TabPageList.Count - 1; i >= 0; i--)
            {
                ITabPageItem pTabPageItem = this.m_Owner.TabPageList[i] as ITabPageItem;
                if (pTabPageItem != null &&
                    !this.m_Owner.TabButtonItemCollection.Contains(pTabPageItem.pTabButtonItem as BaseItem))
                {
                    this.m_Owner.TabPageList.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// item1 与 item2 互换位置
        /// </summary>
        /// <param name="item1"></param>
        /// <param name="item2"></param>
        public virtual bool ExchangeItem(ITabPageItem item1, ITabPageItem item2)
        {
            if (this.Locked) return false;
            //
            if (item1 == null || item2 == null || item1 == item2) return false;
            //
            return this.m_Owner.TabButtonItemCollection.ExchangeItem(item1.pTabButtonItem as BaseItem, item2.pTabButtonItem as BaseItem);
        }

        #region ICollection
        public int Count
        {
            get
            {
                return this.m_Owner.TabButtonItemCollection.Count;
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
            for (int i = index; i < this.Count; i++)
            {
                destination.SetValue(this[i], i - index);
            }
        }
        #endregion

        #region IEnumerable
        public IEnumerator GetEnumerator()
        {
            return new TabPageCollectionEnumerator(this);
        }
        #endregion

        #region IList
        int IList.Add(object value)
        {
            return this.Add(value as ITabPageItem);
        }

        bool IList.Contains(object value)
        {
            return this.Contains(value as ITabPageItem);
        }

        int IList.IndexOf(object value)
        {
            return this.IndexOf(value as ITabPageItem);
        }

        void IList.Insert(int index, object value)
        {
            this.Insert(index, value as ITabPageItem);
        }

        void IList.Remove(object value)
        {
            this.Remove(value as ITabPageItem);
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
                ITabPageItem item = value as ITabPageItem;
                if (item == null)
                {
                    throw new ArgumentException("必须是ITabPageItem类型！", "value");
                }
                this[index] = item;
            }
        }
        #endregion

        #region WFNew.IFlexibleList
        /// <summary>
        /// index1 与 index2 互换Index
        /// </summary>
        /// <param name="index1"></param>
        /// <param name="index2"></param>
        public bool ExchangeItem(int index1, int index2)
        {
            if (this.Locked) return false;
            //
            return this.ExchangeItem(this[index1], this[index2]);
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
            return this.ExchangeItem(item1 as ITabPageItem, item2 as ITabPageItem);
        }
        #endregion

        //

        protected void OnItemAdded(ItemEventArgs e)
        {
            if (this.ItemAdded != null) { this.ItemAdded(this, e); }
        }

        protected void OnItemRemoved(ItemEventArgs e)
        {
            if (this.ItemRemoved != null) { this.ItemRemoved(this, e); }
        }

        //
        //
        //

        class TabPageCollectionEnumerator : IEnumerator
        {
            public IList innerList;

            int m_Position = -1;

            public TabPageCollectionEnumerator(IList list)
            {
                innerList = list;
            }

            public bool MoveNext()
            {
                m_Position++;
                return m_Position < innerList.Count;
            }

            public void Reset()
            {
                m_Position = -1;
            }

            public object Current
            {
                get
                {
                    try
                    {
                        return innerList[m_Position];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }
        }
    }

    #region 已抛弃
    //public class TabPageCollection : FlexibleList<ITabPageItem>, ILockCollectionHelper
    //{
    //    public event ItemEventHandler ItemAdded;
    //    public event ItemEventHandler ItemRemoved;

    //    private ITabControlHelper m_Owner = null;

    //    internal TabPageCollection(ITabControlHelper pTabControlHelper)
    //    {
    //        this.m_Owner = pTabControlHelper;
    //    }

    //    /// <summary>
    //    /// 符合条件返回true。如果SetItemAttribute也返回true 则 将value加到集合中去
    //    /// </summary>
    //    /// <returns></returns>
    //    protected virtual bool Filtration(ITabPageItem value)
    //    {
    //        return !this.Contains(value);
    //    }

    //    /// <summary>
    //    /// 设置添加项的部分属性成功返回true。如果Filtration也返回true 则 将value加到集合中去
    //    /// </summary>
    //    /// <param name="value"></param>
    //    protected virtual bool SetItemAttribute(ITabPageItem value)
    //    {
    //        ((WFNew.ISetOwnerHelper)value).SetOwner(this.m_Owner as WFNew.IOwner);
    //        return true;
    //    }

    //    /// <summary>
    //    /// 在移除项时将添加时的部分属性还原，成功返回true。如果成功则移除该项
    //    /// </summary>
    //    /// <param name="value"></param>
    //    protected virtual bool RestoreItemAttribute(ITabPageItem value)
    //    {
    //        //if (this.m_Owner == value.pOwner) ((WFNew.ISetOwnerHelper)value).SetOwner(null);
    //        return true;
    //    }

    //    #region ILockCollectionHelper
    //    /// <summary>
    //    /// 是否已锁定（默认未加锁 false）
    //    /// </summary>
    //    public new bool Locked
    //    {
    //        get { return this.m_Owner.TabButtonItemCollection.Locked; }
    //    }

    //    void ILockCollectionHelper.SetLocked(bool locked)
    //    {
    //        ((WFNew.ILockCollectionHelper)this.m_Owner.TabButtonItemCollection).SetLocked(locked);
    //    }
    //    #endregion

    //    public override int Add(ITabPageItem value)
    //    {
    //        if (this.Locked) return -1;
    //        //
    //        if (value == null) return -1;
    //        //
    //        if (!this.Filtration(value) ||
    //            !this.SetItemAttribute(value)) return -1;
    //        //
    //        int iReturn = this.m_Owner.TabButtonItemCollection.Add(value.pTabButtonItem as BaseItem);
    //        this.m_Owner.TabPageList.Add(value);
    //        //
    //        this.OnItemAdded(new ItemEventArgs(value));
    //        //
    //        return iReturn;
    //    }

    //    public override bool Contains(ITabPageItem value)
    //    {
    //        if (value == null) return false;
    //        //
    //        return this.m_Owner.TabPageList.Contains(value) &&
    //            this.m_Owner.TabButtonItemCollection.Contains(value.pTabButtonItem as BaseItem);
    //    }

    //    public override void Insert(int index, ITabPageItem value)
    //    {
    //        if (this.Locked) return;
    //        //
    //        if (value == null) return;
    //        //
    //        if ((index < 0) || (index >= this.Count)) return;
    //        //
    //        if (!this.Filtration(value) ||
    //            !this.SetItemAttribute(value)) return;
    //        //
    //        this.m_Owner.TabButtonItemCollection.Insert(index, value.pTabButtonItem as BaseItem);
    //        this.m_Owner.TabPageList.Add(value);
    //        //
    //        this.OnItemAdded(new ItemEventArgs(value));
    //    }

    //    public override int IndexOf(ITabPageItem value)
    //    {
    //        if (value == null) return -1;
    //        //
    //        return this.m_Owner.TabButtonItemCollection.IndexOf(value.pTabButtonItem as BaseItem);
    //    }

    //    public override void Remove(ITabPageItem value)
    //    {
    //        if (this.Locked) return;
    //        //
    //        if (value == null) return;
    //        //
    //        if (!this.RestoreItemAttribute(value)) return;
    //        //
    //        this.m_Owner.TabButtonItemCollection.Remove(value.pTabButtonItem as BaseItem);
    //        this.m_Owner.TabPageList.Remove(value);
    //        //
    //        this.OnItemRemoved(new ItemEventArgs(value));
    //    }

    //    public override void RemoveAt(int index)
    //    {
    //        if (this.Locked) return;
    //        //
    //        ITabButtonItem item = this.m_Owner.TabButtonItemCollection[index] as ITabButtonItem;
    //        if (item == null) return;
    //        //
    //        this.m_Owner.TabButtonItemCollection.RemoveAt(index);
    //        this.m_Owner.TabPageList.Remove(item.pTabPageItem);
    //        //
    //        this.OnItemRemoved(new ItemEventArgs(item.pTabPageItem));
    //    }

    //    public override void Clear()
    //    {
    //        if (this.Locked) return;
    //        // 
    //        for (int i = this.Count - 1; i >= 0; i--)
    //        {
    //            this.RemoveAt(i);
    //        }
    //    }

    //    public override ITabPageItem this[int index]
    //    {
    //        get
    //        {
    //            ITabButtonItem item = this.m_Owner.TabButtonItemCollection[index] as ITabButtonItem;
    //            if (item == null) return null;
    //            return item.pTabPageItem;
    //        }
    //        set
    //        {
    //            if (this.Locked) return;
    //            //
    //            if (!this.Filtration(value)) return;
    //            //
    //            this.RemoveAt(index);
    //            this.Insert(index, value);
    //        }
    //    }

    //    public override int Count
    //    {
    //        get
    //        {
    //            return this.m_Owner.TabButtonItemCollection.Count;
    //        }
    //    }

    //    public override IEnumerator GetEnumerator()
    //    {
    //        return new TabPageCollectionEnumerator(this);
    //    }

    //    /// <summary>
    //    /// item1 与 item2 互换位置
    //    /// </summary>
    //    /// <param name="item1"></param>
    //    /// <param name="item2"></param>
    //    public override bool ExchangeItem(ITabPageItem item1, ITabPageItem item2)
    //    {
    //        if (this.Locked) return false;
    //        //
    //        if (item1 == null || item2 == null || item1 == item2) return false;
    //        //
    //        return this.m_Owner.TabButtonItemCollection.ExchangeItem(item1.pTabButtonItem as BaseItem, item2.pTabButtonItem as BaseItem);
    //    }

    //    public ITabPageItem this[string name]
    //    {
    //        get
    //        {
    //            foreach (ITabButtonItem one in this.m_Owner.TabButtonItemCollection)
    //            {
    //                if (one != null && one.Name == name) return one.pTabPageItem;
    //            }
    //            //
    //            return null;
    //        }
    //    }

    //    //

    //    protected void OnItemAdded(ItemEventArgs e)
    //    {
    //        if (this.ItemAdded != null) { this.ItemAdded(this, e); }
    //    }

    //    protected void OnItemRemoved(ItemEventArgs e)
    //    {
    //        if (this.ItemRemoved != null) { this.ItemRemoved(this, e); }
    //    }

    //    //
    //    //
    //    //

    //    class TabPageCollectionEnumerator : IEnumerator
    //    {
    //        public IList innerList;

    //        int m_Position = -1;

    //        public TabPageCollectionEnumerator(IList list)
    //        {
    //            innerList = list;
    //        }

    //        public bool MoveNext()
    //        {
    //            m_Position++;
    //            return m_Position < innerList.Count;
    //        }

    //        public void Reset()
    //        {
    //            m_Position = -1;
    //        }

    //        public object Current
    //        {
    //            get
    //            {
    //                try
    //                {
    //                    return innerList[m_Position];
    //                }
    //                catch (IndexOutOfRangeException)
    //                {
    //                    throw new InvalidOperationException();
    //                }
    //            }
    //        }
    //    }
    //}
    #endregion
}