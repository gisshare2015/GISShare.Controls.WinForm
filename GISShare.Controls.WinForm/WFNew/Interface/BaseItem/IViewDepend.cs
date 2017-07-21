using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace GISShare.Controls.WinForm.WFNew
{
    /// <summary>
    /// 用来确定对象视图依附的类型
    /// </summary>
    public interface IViewDepend
    {
        ViewDependStyle eViewDependStyle { get; }
    }
}
