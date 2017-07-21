using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew.View
{
    public class NodeViewItemCollection : FlexibleList<NodeViewItem>
    {
        private IOwner m_Owner = null;
        private List<NodeViewItem> innerList = null;

        internal NodeViewItemCollection(IOwner pOwner)
        {
            this.m_Owner = pOwner;
            this.innerList = new List<NodeViewItem>();
        }

        #region ���Ϲ��˺���
        /// <summary>
        /// ������������true�����SetItemAttributeҲ����true �� ��value�ӵ�������ȥ
        /// </summary>
        /// <returns></returns>
        protected virtual bool Filtration(NodeViewItem value)
        {
            return !this.Contains(value);
            //return true;
        }

        /// <summary>
        /// ���������Ĳ������Գɹ�����true�����FiltrationҲ����true �� ��value�ӵ�������ȥ
        /// </summary>
        /// <param name="value"></param>
        protected virtual bool SetItemAttribute(NodeViewItem value)
        {
            ((ISetOwnerHelper)value).SetOwner(this.m_Owner);
            ISetViewItemHelper pSetViewItemHelper = value as ISetViewItemHelper;
            if (pSetViewItemHelper != null) pSetViewItemHelper.SetViewParameterStyle(ViewParameterStyle.eNone);
            return true;
        }

        /// <summary>
        /// ���Ƴ���ʱ�����ʱ�Ĳ������Ի�ԭ���ɹ�����true������ɹ����Ƴ�����
        /// </summary>
        /// <param name="value"></param>
        protected virtual bool RestoreItemAttribute(NodeViewItem value)
        {
            if (this.m_Owner == value.pOwner) ((ISetOwnerHelper)value).SetOwner(null);
            ISetViewItemHelper pSetViewItemHelper = value as ISetViewItemHelper;
            if (pSetViewItemHelper != null) pSetViewItemHelper.SetViewParameterStyle(ViewParameterStyle.eNone);
            return true;
        }
        #endregion

        public bool Contains(string strName)
        {
            foreach (NodeViewItem one in this.innerList) 
            {
                if (one.Name == strName) return true;
            }
            //
            return false;
        }

        public NodeViewItem this[string strName]
        {
            get
            {
                foreach (NodeViewItem one in this.innerList)
                {
                    if (one.Name == strName) return one;
                }
                //
                return null;
            }
        }

        public bool ContainsEx(string strName)
        {
            foreach (NodeViewItem one in this.innerList)
            {
                if (one.Name == strName || 
                    this.ContainsEx_DG(strName, one.NodeViewItems)) return true;
            }
            //
            return false;
        }
        private bool ContainsEx_DG(string strName, GISShare.Controls.WinForm.WFNew.View.NodeViewItemCollection nodes)
        {
            foreach (NodeViewItem one in nodes)
            {
                if (one.Name == strName) return true;
            }
            //
            return false;
        }

        public bool ContainsEx(NodeViewItem value)
        {
            foreach (NodeViewItem one in this.innerList)
            {
                if (one == value ||
                    this.ContainsEx_DG(value, one.NodeViewItems)) return true;
            }
            //
            return false;
        }
        private bool ContainsEx_DG(NodeViewItem value, GISShare.Controls.WinForm.WFNew.View.NodeViewItemCollection nodes)
        {
            foreach (NodeViewItem one in nodes)
            {
                if (one == value) return true;
            }
            //
            return false;
        }

        public NodeViewItem GetNodeViewItemEx(string strName)
        {
            foreach (NodeViewItem one in this.innerList)
            {
                if (one.Name == strName) return one;
                NodeViewItem nodeViewItem = this.GetNodeViewItemEx_DG(strName, one.NodeViewItems);
                if (nodeViewItem != null) return nodeViewItem;
            }
            //
            return null;
        }
        private NodeViewItem GetNodeViewItemEx_DG(string strName, GISShare.Controls.WinForm.WFNew.View.NodeViewItemCollection nodes)
        {
            foreach (NodeViewItem one in nodes)
            {
                if (one.Name == strName) return one;
            }
            //
            return null;
        }

        public override int Add(NodeViewItem value)
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

        public override void Insert(int index, NodeViewItem value)
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

        public override bool Contains(NodeViewItem value)
        {
            if (value == null) return false;
            //
            return this.innerList.Contains(value);
        }

        public override int IndexOf(NodeViewItem value)
        {
            if (value == null) return -1;
            //
            return this.innerList.IndexOf(value);
        }

        public override void Remove(NodeViewItem value)
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
            NodeViewItem baseItem = this.innerList[index];
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

        public override NodeViewItem this[int index]
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

        public override bool ExchangeItemT(NodeViewItem item1, NodeViewItem item2)
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

        private void RefreshOwner(int index, NodeViewItem value)
        {
            if (!value.Visible) return;
            INodeViewItemTree pNodeViewItemTree = value.TryGetNodeViewItemTree();
            if (pNodeViewItemTree == null) return;
            int iNum = 0;
            iNum = this.GetNodeViewItemIndex_DG(pNodeViewItemTree.NodeViewItems, value, ref iNum);
            if (pNodeViewItemTree.TopViewItemIndex <= iNum && pNodeViewItemTree.BottomViewItemIndex >= iNum)
            {
                this.m_Owner.Refresh();
            }
        }
        private int GetNodeViewItemIndex_DG(NodeViewItemCollection nodeViewItems, NodeViewItem nodeViewItem, ref int iNum)
        {
            foreach (NodeViewItem one in nodeViewItems)
            {
                if (one.Visible)
                {
                    if (one == nodeViewItem) return iNum;
                    iNum++;
                    if (one.IsExpanded)
                    {
                        int iIndex = this.GetNodeViewItemIndex_DG(one.NodeViewItems, nodeViewItem, ref iNum);
                        if (iIndex >= 0) { return iIndex; }
                    }
                }
            }
            return -1;
        }
    }
}
