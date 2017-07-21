using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace GISShare.Controls.WinForm.WFNew.View
{
    public class SelectedIndexCollection : FlexibleList<int>
    {
        private IViewLayoutList m_Owner;
        private List<int> innerList = null;

        internal SelectedIndexCollection(IViewLayoutList owner)
        {
            this.m_Owner = owner;
            this.innerList = new List<int>();
        }

        #region 集合过滤函数
        /// <summary>
        /// 符合条件返回true。如果SetItemAttribute也返回true 则 将value加到集合中去
        /// </summary>
        /// <returns></returns>
        protected virtual bool Filtration(int value)
        {
            return value >= 0 && value < this.m_Owner.List.Count && !this.Contains(value);
            //return true;
        }

        /// <summary>
        /// 设置添加项的部分属性成功返回true。如果Filtration也返回true 则 将value加到集合中去
        /// </summary>
        /// <param name="value"></param>
        protected virtual bool SetItemAttribute(int value)
        {
            if (value >= 0 && value < this.m_Owner.List.Count)
            {
                ISetViewItemHelper pSetViewItemHelper;
                if (this.innerList.Count > 0)
                {
                    pSetViewItemHelper = this.m_Owner.List[this.innerList[this.innerList.Count - 1]] as ISetViewItemHelper;
                    if (pSetViewItemHelper != null) pSetViewItemHelper.SetViewParameterStyle(ViewParameterStyle.eSelected);
                }
                //
                pSetViewItemHelper = this.m_Owner.List[value] as ISetViewItemHelper;
                if (pSetViewItemHelper != null &&
                    pSetViewItemHelper.SetViewParameterStyle(ViewParameterStyle.eFocused) &&
                    value >= this.m_Owner.TopViewItemIndex && value <= this.m_Owner.BottomViewItemIndex)
                {
                    IOwner pOwner = this.m_Owner as IOwner;
                    if (pOwner != null)
                    {
                        ViewItem viewItem = this.m_Owner.List[value] as ViewItem;
                        if (viewItem != null)
                        {
                            Rectangle rectangle = viewItem.DisplayRectangle;
                            rectangle.Inflate(1, 1);
                            pOwner.Invalidate(rectangle);
                        }
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 在移除项时将添加时的部分属性还原，成功返回true。如果成功则移除该项
        /// </summary>
        /// <param name="value"></param>
        protected virtual bool RestoreItemAttribute(int value)
        {
            if (value >= 0 && value < this.m_Owner.List.Count)
            {
                ISetViewItemHelper pSetViewItemHelper = this.m_Owner.List[value] as ISetViewItemHelper;
                if (pSetViewItemHelper != null &&
                    pSetViewItemHelper.SetViewParameterStyle(ViewParameterStyle.eNone) &&
                    value >= this.m_Owner.TopViewItemIndex && value <= this.m_Owner.BottomViewItemIndex)
                {
                    IOwner pOwner = this.m_Owner as IOwner;
                    if (pOwner != null)
                    {
                        ViewItem viewItem = this.m_Owner.List[value] as ViewItem;
                        if (viewItem != null)
                        {
                            Rectangle rectangle = viewItem.DisplayRectangle;
                            rectangle.Inflate(1, 1);
                            pOwner.Invalidate(rectangle);
                        }
                    }
                }
            }
            return true;
        }
        #endregion

        public void Besides(int value)
        {
            if (this.Locked) return;
            //
            bool bContains = false;
            for (int i = 0; i < this.Count; )
            {
                if (this.innerList[i] != value)
                {
                    this.RemoveAt(i);
                }
                else 
                {
                    ISetViewItemHelper pSetViewItemHelper = this.m_Owner.List[value] as ISetViewItemHelper;
                    pSetViewItemHelper.SetViewParameterStyle(ViewParameterStyle.eFocused);
                    bContains = true; 
                    i++;
                }
            }
            //
            if (!bContains)
            {
                this.Clear();
                this.Add(value);
            }
        }

        public override int Add(int value)
        {
            if (this.Locked) return -1;
            //
            if (!this.Filtration(value) ||
                !this.SetItemAttribute(value)) return -1;
            //
            this.innerList.Add(value);
            //
            return this.Count - 1;
        }

        public override void Insert(int index, int value)
        {
            if (this.Locked) return;
            //
            if ((index < 0) || (index > this.innerList.Count)) return;
            //
            if (!this.Filtration(value) ||
                !this.SetItemAttribute(value)) return;
            //
            this.innerList.Insert(index, value);
        }

        public override bool Contains(int value)
        {
            return this.innerList.Contains(value);
        }

        public override int IndexOf(int value)
        {
            return this.innerList.IndexOf(value);
        }

        public override void Remove(int value)
        {
            if (this.Locked) return;
            //
            if (!this.RestoreItemAttribute(value)) return;
            //
            this.innerList.Remove(value);
        }

        public override void RemoveAt(int index)
        {
            if (this.Locked) return;
            //
            int baseItem = this.innerList[index];
            if (!this.RestoreItemAttribute(baseItem)) return;
            //
            this.innerList.RemoveAt(index);
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

        public override int this[int index]
        {
            get
            {
                return (index >= 0 && index < this.Count) ? this.innerList[index] : -1;
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

        public override bool ExchangeItemT(int item1, int item2)
        {
            if (this.Locked) return false;
            //
            if (item1 == item2) return false;
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
    }
}
