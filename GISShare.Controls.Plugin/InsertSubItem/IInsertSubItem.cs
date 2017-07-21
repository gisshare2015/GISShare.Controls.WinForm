using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin
{
    public interface IInsertSubItem : IPlugin, IPluginInfo, ISubItem
    {
        /// <summary>
        /// 依附项的键值
        /// </summary>
        string DependItem { get; }

        /// <summary>
        /// 插入索引
        /// </summary>
        int InsertIndex { get; }

        /// <summary>
        /// 插入对象的目录索引
        /// </summary>
        int InsertCategoryIndex { get; }

        /// <summary>
        /// 加载索引（用于控制排序）
        /// </summary>
        int LoadingIndex { get; }
    }
}
