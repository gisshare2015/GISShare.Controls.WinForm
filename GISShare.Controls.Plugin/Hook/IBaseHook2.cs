using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin
{
    public interface IBaseHook2 : IBaseHook
    {
        event CurrentToolChangedEventHandler CurrentToolChanged;

        /// <summary>
        /// 用于独占操作
        /// </summary>
        IBaseTool CurrentTool { get; set; }
    }
}
