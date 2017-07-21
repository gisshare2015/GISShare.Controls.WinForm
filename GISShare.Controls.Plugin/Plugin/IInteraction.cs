using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace GISShare.Controls.Plugin
{
    public interface IInteraction
    {
        /// <summary>
        /// 相互操作的的函数
        /// </summary>
        /// <param name="iOperationStyle">操作类型标识</param>
        /// <param name="obj">参数</param>
        /// <returns>返回值</returns>
        object CommandOperation(int iOperationStyle, params object[] obj);
    }
}
