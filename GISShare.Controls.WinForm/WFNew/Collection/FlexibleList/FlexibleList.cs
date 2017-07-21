using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    public abstract class FlexibleList<T> : WFNew.IFlexibleList, IList, ICollection, IEnumerable, WFNew.ILockCollectionHelper
    {
        public abstract int Add(T value);

        public abstract bool Contains(T value);

        public abstract void Insert(int index, T value);

        public abstract int IndexOf(T value);

        public abstract void Remove(T value);

        public abstract void RemoveAt(int index);

        public abstract void Clear();

        public abstract T this[int index] { get; set; }

        /// <summary>
        /// item1 与 item2 互换位置
        /// </summary>
        /// <param name="item1"></param>
        /// <param name="item2"></param>
        public abstract bool ExchangeItemT(T item1, T item2);

        #region ICollection
        public abstract int Count { get; }

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
        public abstract IEnumerator GetEnumerator();
        #endregion

        #region IList
        int IList.Add(object value)
        {
            return this.Add((T)value);
        }

        bool IList.Contains(object value)
        {
            return this.Contains((T)value);
        }

        int IList.IndexOf(object value)
        {
            return this.IndexOf((T)value);
        }

        void IList.Insert(int index, object value)
        {
            this.Insert(index, (T)value);
        }

        void IList.Remove(object value)
        {
            this.Remove((T)value);
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
                this[index] = (T)value;
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
            return this.ExchangeItemT(this[index1], this[index2]);
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
            return this.ExchangeItemT((T)item1, (T)item2);
        }
        #endregion
    }
}