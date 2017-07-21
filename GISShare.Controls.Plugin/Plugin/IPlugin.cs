using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin
{
    public interface IPlugin : IPluginInfo
    {
        /// <summary>
        /// 唯一编码
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 接口所在的分类（用于标识自身反射对象的分类）
        /// </summary>
        int CategoryIndex { get; }
    }
}
