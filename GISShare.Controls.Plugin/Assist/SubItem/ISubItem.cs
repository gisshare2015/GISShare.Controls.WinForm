using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace GISShare.Controls.Plugin
{
    public interface ISubItem
    {
        /// <summary>
        /// Я���� Item ����
        /// </summary>
        int ItemCount { get;}

        /// <summary>
        /// ���ʿ�ݲ˵���ÿ��Item�ķ���
        /// </summary>
        /// <param name="iIndex"></param>
        /// <param name="pItemDef"></param>
        void GetItemInfo(int iIndex, IItemDef pItemDef);
    }
}
