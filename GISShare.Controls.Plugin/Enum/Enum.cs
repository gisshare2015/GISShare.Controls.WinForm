using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin
{
    public enum CategoryIndex_0_Style : int 
    {
        eInsertSubItem = 1000
    }

    public enum PluginReflectionStyle
    {
        /// <summary>
        /// 未知
        /// </summary>
        eNone = -1,//未知
        /// <summary>
        /// 异常
        /// </summary>
        eException,//异常
        /// <summary>
        /// 反射类型加载异常
        /// </summary>
        eLoadException,//反射类型加载异常
        /// <summary>
        /// 创建插件对象异常
        /// </summary>
        eCreateException,//创建插件对象异常
        /// <summary>
        /// 排除的插件对象
        /// </summary>
        eExceptPluginObject,//排除的插件对象
        /// <summary>
        /// 冲突的插件对象
        /// </summary>
        eConflictPluginObject,//冲突的插件对象
        /// <summary>
        /// 创建有效地插件对象
        /// </summary>
        eEfficientlyPluginObject,//创建有效地插件对象
        /// <summary>
        /// 依据插件创建实体对象
        /// </summary>
        eEfficientlyEntityObject,//依据插件创建实体对象
        /// <summary>
        /// 依据实体对象组织界面布局
        /// </summary>
        eLayoutEfficientlyEntityObject,//依据实体对象组织界面布局
        /// <summary>
        /// 关联实体对象传入通信钩子
        /// </summary>
        eLinkHookEfficientlyEntityObject//关联实体对象传入通信钩子
    }
}
