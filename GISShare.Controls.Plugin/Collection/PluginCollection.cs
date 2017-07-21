using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace GISShare.Controls.Plugin
{
    public sealed class PluginCollection : CollectionBase
    {
        #region 构造函数
        internal PluginCollection()
        { }

        internal PluginCollection(PluginCollection value)
        {
            this.AddRange(value);
        }

        internal PluginCollection(IPlugin[] value)
        {
            this.AddRange(value);
        }
        #endregion

        public IPlugin this[int index]
        {
            get 
            {
                if (index < this.List.Count && index >= 0)
                {
                    return (IPlugin)(this.List[index]);
                }
                //
                return null;
            }
        }

        public IPlugin this[string strName]
        {
            get
            {
                foreach (object one in this.List)
                {
                    IPlugin pPlugin = one as IPlugin;
                    if (pPlugin != null && pPlugin.Name == strName) { return pPlugin; }
                }
                //
                return null;
            }
        }

        public int Add(IPlugin value)
        {
            if (value == null) return -1;
            //
            return this.List.Add(value);
        }

        public void AddRange(PluginCollection value)
        {
            if (value == null || value.Count <= 0) return;
            //
            for (int i = 0; i < value.Count; i++)
            {
                this.Add(value[i]);
            }
        }

        public void AddRange(IPlugin[] value)
        {
            if (value == null || value.Length <= 0) return;
            //
            for (int i = 0; i < value.Length; i++)
            {
                this.Add(value[i]);
            }
        }

        public bool Contains(string strName)
        {
            foreach (object one in this.List)
            {
                IPlugin pPlugin = one as IPlugin;
                if (pPlugin != null && pPlugin.Name == strName) { return true; }
            }
            //
            return false;
        }

        public bool Contains(IPlugin value)
        {
            return this.List.Contains(value);
        }

        public int IndexOf(string strName)
        {
            int i = 0;
            foreach (object one in this.List)
            {
                IPlugin pPlugin = one as IPlugin;
                if (pPlugin != null && pPlugin.Name == strName) { return i; }
                //
                i++;
            }
            //
            return -1;
        }

        public int IndexOf(IPlugin value)
        {
            return this.List.IndexOf(value);
        }

        public void CopyTo(IPlugin[] array, int index)
        {
            this.List.CopyTo(array, index);
        }

        public IPlugin[] ToArray()
        {
            IPlugin[] array = new IPlugin[this.Count];
            this.CopyTo(array, 0);
            return array;
        }

        public void Insert(int index, IPlugin value)
        {
            this.List.Insert(index, value);
        }

        public void Remove(IPlugin value)
        {
            this.Remove(value);
        }

        public new PluginCollectionEnumerator GetEnumerator()
        {
            return new PluginCollectionEnumerator(this);
        }
    }

    public class PluginCollectionEnumerator : IEnumerator
    {
        private IEnumerable m_pEnumerable;
        private IEnumerator m_pEnumerator;

        public PluginCollectionEnumerator(PluginCollection mappings)
        {
            this.m_pEnumerable = (IEnumerable)mappings;
            this.m_pEnumerator = this.m_pEnumerable.GetEnumerator();
        }

        #region IEnumerator 成员
        public object Current
        {
            get { return this.m_pEnumerator.Current; }
        }

        public bool MoveNext()
        {
            return this.m_pEnumerator.MoveNext();
        }

        public void Reset()
        {
            this.m_pEnumerator.Reset();
        }
        #endregion
    }
}
