using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin
{
    public interface IPlugin2 : IPlugin, ISetEntityObject, IEventChain, IInteraction
    {
        /// <summary>
        /// 反射后的实体对象
        /// </summary>
        object EntityObject { get; }

        /// <summary>
        /// 传递钩子
        /// </summary>
        /// <param name="hook">钩子</param>
        void OnCreate(object hook);
    }
}
