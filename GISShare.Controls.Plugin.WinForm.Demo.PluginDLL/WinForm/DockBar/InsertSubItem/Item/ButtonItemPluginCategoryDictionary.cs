using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.InsertSubItem.Item
{
    class ButtonItemPluginCategoryDictionary : GISShare.Controls.Plugin.WinForm.DockBar.ButtonItemP
    {
        public ButtonItemPluginCategoryDictionary()
        {
            this._Name = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.InsertSubItem.Item.ButtonItemPluginCategoryDictionary";
            this._Text = "插件目录字典";
            this._ToolTipText = this._Text;
            this._Category = "插入项";
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
            if (form is GISShare.Controls.Plugin.WinForm.DockBar.HostDockBarTBForm)
            {
                GISShare.Controls.Plugin.WinForm.PluginCategoryDictionaryTBForm pluginCategoryDictionaryTBForm = new PluginCategoryDictionaryTBForm(this.m_pAppHook.Host.PluginCategoryDictionary);
                pluginCategoryDictionaryTBForm.Owner = form;
                pluginCategoryDictionaryTBForm.Show();
            }
            else
            {
                GISShare.Controls.Plugin.WinForm.PluginCategoryDictionaryForm pluginCategoryDictionaryTBForm = new PluginCategoryDictionaryForm(this.m_pAppHook.Host.PluginCategoryDictionary);
                pluginCategoryDictionaryTBForm.Owner = form;
                pluginCategoryDictionaryTBForm.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
                pluginCategoryDictionaryTBForm.Location = new System.Drawing.Point(form.Location.X + (form.Width - pluginCategoryDictionaryTBForm.Width) / 2, form.Location.Y + (form.Height - pluginCategoryDictionaryTBForm.Height) / 2);
                pluginCategoryDictionaryTBForm.Show();
            }
        }
    }
}
