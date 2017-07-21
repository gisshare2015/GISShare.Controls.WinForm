using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.DockBar
{
    public class HostDockBarForm : System.Windows.Forms.Form, GISShare.Controls.Plugin.IBaseHost, GISShare.Controls.Plugin.IBaseHost2
    {
        /// <summary>
        /// 记录插件加载和反射的事件
        /// </summary>
        public event PluginReflectionEventHandler PluginReflection;

        private HostDockBarObject m_HostDockBarObject;

        /// <summary>
        /// 一旦装载成功再次调用则无效
        /// </summary>
        /// <param name="pluginDLLFolder"></param>
        /// <param name="strRemoveFilePathArray"></param>
        /// <param name="removeObjectNameArray"></param>
        /// <param name="hook"></param>
        /// <param name="dockBarManager"></param>
        /// <param name="dockPanelManager"></param>
        /// <returns></returns>
        public bool RunPluginEngine(string pluginDLLFolder,
            string[] strFilterFilePathArray,
            bool bFilterFilePathArrayTypeRemove,
            string[] strFilterObjectNameArray,
            bool bFilterObjectNameArrayTypeRemove,
            object hook,
            GISShare.Controls.WinForm.DockBar.DockBarManager dockBarManager,
            GISShare.Controls.WinForm.WFNew.DockPanel.DockPanelManager dockPanelManager)
        {
            if (this.m_HostDockBarObject == null)
            {
                this.m_HostDockBarObject = new HostDockBarObject(pluginDLLFolder, strFilterFilePathArray, bFilterFilePathArrayTypeRemove, strFilterObjectNameArray, bFilterObjectNameArrayTypeRemove, hook, null, dockBarManager, dockPanelManager);
                this.m_HostDockBarObject.PluginReflection += new PluginReflectionEventHandler(HostDockBarObject_PluginReflection);
            }
            //
            return this.m_HostDockBarObject.RunPluginEngine();
        }
        void HostDockBarObject_PluginReflection(object sender, PluginReflectionEventArgs e)
        {
            this.OnPluginReflection(e);
        }

        #region IBaseHost
        /// <summary>
        /// 钩子信息
        /// </summary>
        public object Hook
        {
            get { return this.m_HostDockBarObject.Hook; }
        }

        /// <summary>
        /// 插件目录字典
        /// </summary>
        public PluginCategoryDictionary PluginCategoryDictionary
        {
            get { return this.m_HostDockBarObject.PluginCategoryDictionary; }
        }

        /// <summary>
        /// 提供其它操作
        /// </summary>
        /// <param name="obj">参数</param>
        /// <returns>返回</returns>
        public virtual object OtherOperation(object obj)
        {
            return this.m_HostDockBarObject.OtherOperation(obj);
        }
        #endregion

        #region IBaseHost2
        /// <summary>
        /// 追加插件对象
        /// </summary>
        public void AppendPlugin()
        {
            if (this.m_HostDockBarObject != null)
            {
                AppendPluginForm appendPluginForm = new AppendPluginForm(this.m_HostDockBarObject);
                appendPluginForm.Owner = this;
                appendPluginForm.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                appendPluginForm.ShowDialog();
            }
        }

        /// <summary>
        /// 追加插件对象
        /// </summary>
        /// <param name="pluginDLLFile"></param>
        public PluginCategoryDictionary AppendPluginObject(string pluginDLLFile)
        {
            if (this.m_HostDockBarObject != null)
            {
                return this.m_HostDockBarObject.AppendPluginObject(pluginDLLFile);
            }
            //
            return null;
        }
        #endregion

        public void UIView()
        {
            if (this.m_HostDockBarObject != null)
            {
                UIViewForm uiViewForm = new UIViewForm(this.m_HostDockBarObject.UIView());
                uiViewForm.Owner = this;
                uiViewForm.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
                uiViewForm.Location = new System.Drawing.Point(this.Location.X + (this.Width - uiViewForm.Width) / 2, this.Location.Y + (this.Height - uiViewForm.Height) / 2);
                uiViewForm.Show();
            }
        }

        //受保护
        protected virtual void OnPluginReflection(PluginReflectionEventArgs e)
        {
            if (this.PluginReflection != null) this.PluginReflection(this, e);
        }
    }
}
