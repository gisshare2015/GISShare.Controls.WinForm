using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin
{
    public interface IBaseHost2 : IBaseHost
    {
        void AppendPlugin();

        PluginCategoryDictionary AppendPluginObject(string pluginDLLFile);
    }
}
