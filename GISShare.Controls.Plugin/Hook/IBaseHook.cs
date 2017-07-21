using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace GISShare.Controls.Plugin
{
    public interface IBaseHook
    {
        IBaseHost Host { get; }
    }
}
