using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin
{
    public interface IBaseHost3 : IBaseHost2
    {
        event PluginReflectionEventHandler PluginReflection;

        string PluginDLLFolder { get; }

        string[] FilterFilePathArray { get; }

        string[] FilterObjectNameArray { get; }
    }
}
