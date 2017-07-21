using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin
{
    public delegate void PluginReflectionEventHandler(object sender, PluginReflectionEventArgs e);

    public class PluginReflectionEventArgs : System.EventArgs
    {
        public PluginReflectionEventArgs(PluginReflectionStyle pluginReflectionStyle, IPlugin pPlugin, string strInfo)
        {
            this.m_ePluginReflectionStyle = pluginReflectionStyle;
            this.m_pPlugin = pPlugin;
            this.m_Info = strInfo;
        }

        private PluginReflectionStyle m_ePluginReflectionStyle = PluginReflectionStyle.eNone;
        public PluginReflectionStyle ePluginReflectionStyle
        {
            get { return m_ePluginReflectionStyle; }
        }

        private IPlugin m_pPlugin;
        public IPlugin Plugin
        {
            get { return this.m_pPlugin; }
        }

        string m_Info;
        public string Info
        {
            get { return m_Info; }
        }
    }

    ////
    ////
    ////

    //public delegate void CurrentToolChangedEventHandler(object sender, CurrentToolChangedEventArgs e);

    //public class CurrentToolChangedEventArgs : System.EventArgs
    //{
    //    public CurrentToolChangedEventArgs(IBaseTool baseToolNow, IBaseTool baseToolAfter)
    //    {
    //        this.m_pBaseToolNow = baseToolNow;
    //        this.m_pBaseToolAfter = baseToolAfter;
    //    }

    //    private IBaseTool m_pBaseToolNow;
    //    public IBaseTool pBaseToolNow
    //    {
    //        get { return m_pBaseToolNow; }
    //    }

    //    private IBaseTool m_pBaseToolAfter;
    //    public IBaseTool pBaseToolAfter
    //    {
    //        get { return m_pBaseToolAfter; }
    //    }
    //}

}
