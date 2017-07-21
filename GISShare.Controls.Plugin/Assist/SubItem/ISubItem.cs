using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace GISShare.Controls.Plugin
{
    public interface ISubItem
    {
        /// <summary>
        /// 携带的 Item 数量
        /// </summary>
        int ItemCount { get;}

        /// <summary>
        /// 访问快捷菜单中每个Item的方法
        /// </summary>
        /// <param name="iIndex"></param>
        /// <param name="pItemDef"></param>
        void GetItemInfo(int iIndex, IItemDef pItemDef);
    }
}
