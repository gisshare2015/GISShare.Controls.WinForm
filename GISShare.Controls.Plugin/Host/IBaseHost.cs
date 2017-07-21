using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace GISShare.Controls.Plugin
{
    public interface IBaseHost
    {
        /// <summary>
        /// 钩子信息
        /// </summary>
        object Hook { get; }

        /// <summary>
        /// 插件目录字典
        /// </summary>
        PluginCategoryDictionary PluginCategoryDictionary { get; }

        /// <summary>
        /// 提供其它操作
        /// </summary>
        /// <param name="obj">参数</param>
        /// <returns>返回</returns>
        object OtherOperation(object obj);
    }
}
