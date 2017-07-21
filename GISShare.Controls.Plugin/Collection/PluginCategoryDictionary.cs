using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace GISShare.Controls.Plugin
{
    public sealed class PluginCategoryDictionary : IEnumerable
    {
        private List<PluginCategory> m_PluginCategoryList;

        public PluginCategoryDictionary()
        {
            this.m_PluginCategoryList = new List<PluginCategory>();
        }

        public int Count
        {
            get 
            {
                return this.m_PluginCategoryList.Count;
            }
        }

        public PluginCategory this[int index]
        {
            get
            {
                if (index < this.m_PluginCategoryList.Count && index >= 0)
                {
                    return this.m_PluginCategoryList[index];
                }
                //
                return null;
            }
        }

        public PluginCategory GetPluginCategory(int iCategoryIndex)
        {
            foreach (PluginCategory one in this.m_PluginCategoryList)
            {
                if (one.CategoryIndex == iCategoryIndex)
                {
                    return one;
                }
            }
            //
            return null;
        }

        public bool ContainsPlugin(string strName)
        {
            foreach (PluginCategory one in this.m_PluginCategoryList)
            {
                if (one.PluginCollection.Contains(strName)) return true;
            }
            //
            return false;
        }

        public bool ContainsPlugin(IPlugin pPlugin)
        {
            foreach (PluginCategory one in this.m_PluginCategoryList)
            {
                if (one.PluginCollection.Contains(pPlugin)) return true;
            }
            //
            return false;
        }

        public bool AddPlugin(IPlugin pPlugin)
        {
            if (pPlugin == null || this.ContainsPlugin(pPlugin.Name)) return false;
            //
            this.GetOrCreatePluginCategory(pPlugin.CategoryIndex).PluginCollection.Add(pPlugin);
            return true;
        }

        public int GetPluginCount()
        {
            int iCount = 0;
            foreach (PluginCategory one in this.m_PluginCategoryList) 
            {
                iCount += one.PluginCollection.Count;
            }
            return iCount;
        }

        public IPlugin GetPlugin(string strName)
        {
            foreach (PluginCategory one in this.m_PluginCategoryList)
            {
                foreach (IPlugin one2 in one.PluginCollection)
                {
                    if (one2.Name == strName) return one2;
                }
            }
            //
            return null;
        }

        public IPlugin GetPlugin(int iCategoryIndex, string strName)
        {
            foreach (PluginCategory one in this.m_PluginCategoryList)
            {
                if (one.CategoryIndex == iCategoryIndex)
                {
                    foreach (IPlugin one2 in one.PluginCollection)
                    {
                        if (one2.Name == strName) return one2;
                    }
                    //
                    return null;
                }
            }
            //
            return null;
        }

        //public void Clear()
        //{
        //    this.m_PluginCategoryList.Clear();
        //}

        public PluginCategory GetOrCreatePluginCategory(int iCategoryIndex)
        {
            foreach (PluginCategory one in this.m_PluginCategoryList)
            {
                if (one.CategoryIndex == iCategoryIndex)
                {
                    return one;
                }
            }
            //
            PluginCategory pluginCategory = new PluginCategory(iCategoryIndex);
            this.m_PluginCategoryList.Add(pluginCategory);
            //
            return pluginCategory;
        }

        public IEnumerator GetEnumerator()
        {
            return this.m_PluginCategoryList.GetEnumerator();
        }

        public void Add(PluginCategoryDictionary pluginCategoryDictionary)
        {
            if (pluginCategoryDictionary == null) return;
            //
            foreach (PluginCategory one in pluginCategoryDictionary)
            {
                foreach (IPlugin one2 in one.PluginCollection)
                {
                    this.AddPlugin(one2);
                }
            }
        }

        public PluginCategoryDictionary GetDifferent(PluginCategoryDictionary pluginCategoryDictionary)
        {
            if (pluginCategoryDictionary == null || pluginCategoryDictionary.Count == 0) return this;
            //
            PluginCategoryDictionary pluginCategoryDictionaryDifferent = new PluginCategoryDictionary();
            foreach (PluginCategory one in this.m_PluginCategoryList)
            {
                foreach (IPlugin one2 in one.PluginCollection)
                {
                    if (one2 == null || pluginCategoryDictionary.ContainsPlugin(one2.Name)) continue;
                    //
                    pluginCategoryDictionaryDifferent.GetOrCreatePluginCategory(one2.CategoryIndex).PluginCollection.Add(one2);
                }
            }
            return pluginCategoryDictionaryDifferent;
        }

    }
}
