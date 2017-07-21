using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.InsertSubItem.BaseItem
{
    class BaseButtonPluginCategoryDictionary : GISShare.Controls.Plugin.WinForm.WFNew.BaseButtonItemP
    {
        public BaseButtonPluginCategoryDictionary()
        {
            this._Name = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.InsertSubItem.BaseItem.BaseButtonPluginCategoryDictionary";
            this._Text = "插件目录字典";
            this._Padding = new System.Windows.Forms.Padding(1);
        }

        Hook.IAppHook m_pAppHook;
        public override void OnCreate(object hook)
        {
            this.m_pAppHook = hook as Hook.IAppHook;
        }

        public override void OnTriggerEvent(int iEventStyle, EventArgs e)
        {
            if (this.m_pAppHook == null || 
                this.m_pAppHook.Host == null) return;
            System.Windows.Forms.Form form = this.m_pAppHook.Host as System.Windows.Forms.Form;
            if (form == null) return;
            GISShare.Controls.Plugin.WinForm.PluginCategoryDictionaryTBForm pluginCategoryDictionaryTBForm = new PluginCategoryDictionaryTBForm(this.m_pAppHook.Host.PluginCategoryDictionary);
            pluginCategoryDictionaryTBForm.Owner = form;
            pluginCategoryDictionaryTBForm.Show();

        }
    }
}
