using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew.View
{
    public class RowCellViewItemCollection : FlexibleList<RowCellViewItem>
    {
        private IOwner m_Owner = null;
        private List<RowCellViewItem> innerList = null;

        internal RowCellViewItemCollection(IOwner pOwner)
        {
            this.m_Owner = pOwner;
            this.innerList = new List<RowCellViewItem>();
        }

        #region ���Ϲ��˺���
        /// <summary>
        /// ������������true�����SetItemAttributeҲ����true �� ��value�ӵ�������ȥ
        /// </summary>
        /// <returns></returns>
        protected virtual bool Filtration(RowCellViewItem value)
        {
            return !this.Contains(value);
            //return true;
        }

        /// <summary>
        /// ���������Ĳ������Գɹ�����true�����FiltrationҲ����true �� ��value�ӵ�������ȥ
        /// </summary>
        /// <param name="value"></param>
        protected virtual bool SetItemAttribute(RowCellViewItem value)
        {
            ISetOwnerHelper pSetOwnerHelper = value as ISetOwnerHelper;
            if (pSetOwnerHelper != null) pSetOwnerHelper.SetOwner(this.m_Owner);
            ISetViewItemHelper pSetViewItemHelper = value as ISetViewItemHelper;
            if (pSetViewItemHelper != null) pSetViewItemHelper.SetViewParameterStyle(ViewParameterStyle.eNone);
            return true;
        }
        /// <summary>
        /// ����ע�뵫�ǲ��ȶ�
        /// </summary>
        /// <param name="value"></param>
        protected void SetItemAttribute2(RowCellViewItem value)
        {
            ((ISetOwnerHelper)value).SetOwner(this.m_Owner);
            ((ISetViewItemHelper)value).SetViewParameterStyle(ViewParameterStyle.eNone);
        }

        /// <summary>
        /// ���Ƴ���ʱ�����ʱ�Ĳ������Ի�ԭ���ɹ�����true������ɹ����Ƴ�����
        /// </summary>
        /// <param name="value"></param>
        protected virtual bool RestoreItemAttribute(RowCellViewItem value)
        {
            IOwner pOwner = value as IOwner;
            if (pOwner != null && 
                this.m_Owner == pOwner)
            {
                ISetOwnerHelper pSetOwnerHelper = value as ISetOwnerHelper;
                if (pSetOwnerHelper != null) { pSetOwnerHelper.SetOwner(null); }
                ISetViewItemHelper pSetViewItemHelper = value as ISetViewItemHelper;
                if (pSetViewItemHelper != null) pSetViewItemHelper.SetViewParameterStyle(ViewParameterStyle.eNone);
            }
            return true;
        }
        #endregion

        public override int Add(RowCellViewItem value)
        {
            if (this.Locked) return -1;
            //
            if (value == null) return -1;
            //
            if (!this.Filtration(value) ||
                !this.SetItemAttribute(value)) return -1;
            //
            this.innerList.Add(value);
            //
            int index = this.Count - 1;
            this.RefreshOwner(index, value);
            //
            return index;
        }
        public void Add2(RowCellViewItem value)
        {
            if (this.Locked) return;
            //
            this.SetItemAttribute2(value);
            this.innerList.Add(value);
        }

        public override void Insert(int index, RowCellViewItem value)
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
            //
            this.RefreshOwner(index, value);
        }

        public override bool Contains(RowCellViewItem value)
        {
            if (value == null) return false;
            //
            return this.innerList.Contains(value);
        }

        public override int IndexOf(RowCellViewItem value)
        {
            if (value == null) return -1;
            //
            return this.innerList.IndexOf(value);
        }

        public override void Remove(RowCellViewItem value)
        {
            if (this.Locked) return;
            //
            if (value == null) return;
            //
            if (!this.RestoreItemAttribute(value)) return;
            //
            this.innerList.Remove(value);
            //
            this.RefreshOwner(this.IndexOf(value), value);
        }

        public override void RemoveAt(int index)
        {
            if (this.Locked) return;
            //
            RowCellViewItem baseItem = this.innerList[index];
            if (baseItem == null || !this.RestoreItemAttribute(baseItem)) return;
            //
            this.innerList.RemoveAt(index);
            //
            this.RefreshOwner(index, baseItem);
        }

        public override void Clear()
        {
            if (this.Locked) return;
            //
            for (int i = this.innerList.Count - 1; i >= 0; i--)
            {
                this.RemoveAt(i);
            }
        }

        public override int Count
        {
            get { return this.innerList.Count; }
        }

        public override RowCellViewItem this[int index]
        {
            get
            {
                return (index >= 0 && index < this.Count) ? this.innerList[index] : null;
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

        public override System.Collections.IEnumerator GetEnumerator()
        {
            return this.innerList.GetEnumerator();
        }

        public override bool ExchangeItemT(RowCellViewItem item1, RowCellViewItem item2)
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

        private void RefreshOwner(int index, ViewItem value)
        {
            IViewLayoutList pViewLayoutList = this.m_Owner as IViewLayoutList;
            if (pViewLayoutList != null)
            {
                if (pViewLayoutList.TopViewItemIndex >= index && pViewLayoutList.BottomViewItemIndex <= index) this.m_Owner.Refresh();
            }
            else
            {
                this.m_Owner.Refresh();
            }
        }
    }
}
