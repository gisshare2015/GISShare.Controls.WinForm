using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace GISShare.Controls.Plugin
{
    public interface IBaseHost
    {
        /// <summary>
        /// ������Ϣ
        /// </summary>
        object Hook { get; }

        /// <summary>
        /// ���Ŀ¼�ֵ�
        /// </summary>
        PluginCategoryDictionary PluginCategoryDictionary { get; }

        /// <summary>
        /// �ṩ��������
        /// </summary>
        /// <param name="obj">����</param>
        /// <returns>����</returns>
        object OtherOperation(object obj);
    }
}
