using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace GISShare.Controls.Plugin
{
    /// <summary>
    /// 定义菜单栏和工具条中的命令项（Item）
    /// </summary>
    public interface IItemDef
    {
        /// <summary>
        /// 是否存在新组
        /// </summary>
        bool Group { get; set; }

        /// <summary>
        /// Item ID号
        /// </summary>
        string ID { get; set; }
    }
}
