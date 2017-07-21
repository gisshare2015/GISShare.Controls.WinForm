using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.DockBar
{
    public class FlexibleToolStripItemCollection : WFNew.FlexibleList<ToolStripItem>
    {
        ToolStripItemCollection innerList;

        internal FlexibleToolStripItemCollection(ToolStripItemCollection toolStripItemCollection)
        {
            this.innerList = toolStripItemCollection;
        }

        public override int Add(ToolStripItem value)
        {
            if (this.Locked) return -1;
            //
            return this.innerList.Add(value);
        }

        public override void Insert(int index, ToolStripItem value)
        {
            if (this.Locked) return;
            //
            if ((index < 0) || (index > this.Count)) return;
            //
            this.innerList.Insert(index, value);
        }

        public override bool Contains(ToolStripItem value)
        {
            return this.innerList.Contains(value);
        }

        public override int IndexOf(ToolStripItem value)
        {
            return this.innerList.IndexOf(value);
        }

        public override void Remove(ToolStripItem value)
        {
            if (this.Locked) return;
            //
            this.innerList.Remove(value);
        }

        public override void RemoveAt(int index)
        {
            if (this.Locked) return;
            //
            this.innerList.RemoveAt(index);
        }

        public override void Clear()
        {
            if (this.Locked) return;
            //
            this.innerList.Clear();
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override ToolStripItem this[int index]
        {
            get
            {
                return this.innerList[index];
            }
            set
            {
                if (this.Locked) return;
                //
                this.RemoveAt(index);
                this.Insert(index, value);
            }
        }

        public override bool ExchangeItemT(ToolStripItem item1, ToolStripItem item2)
        {
            if (this.Locked) return false;
            //
            if (item1 == null || item2 == null || item1 == item2) return false;
            //
            int index1 = this.IndexOf(item1);
            int index2 = this.IndexOf(item2);
            //
            if (index1 == index2) return false;
            if ((index1 < 0) || (index1 >= this.Count)) return false;
            if ((index2 < 0) || (index2 >= this.Count)) return false;
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

        public override int Count
        {
            get
            {
                return this.innerList.Count;
            }
        }

        public override IEnumerator GetEnumerator()
        {
            return this.innerList.GetEnumerator();
        }

        public virtual ToolStripItem this[string name]
        {
            get
            {
                foreach (ToolStripItem one in this.innerList)
                {
                    if (one.Name == name) return one;
                }
                //
                return null;
            }
        }
    }
}
