using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin
{
    public sealed class PluginCategory
    {
        internal PluginCategory(int iCategoryIndex)
        {
            this.m_CategoryIndex = iCategoryIndex;
            this.m_PluginCollection = new PluginCollection();
        }

        private int m_CategoryIndex;
        public int CategoryIndex
        {
            get { return m_CategoryIndex; }
        }

        PluginCollection m_PluginCollection;
        public PluginCollection PluginCollection
        {
            get { return m_PluginCollection; }
        }
    }
}
