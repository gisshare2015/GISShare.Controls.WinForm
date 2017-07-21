using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace GISShare.Controls.Plugin.WinForm.WFNew.Ribbon
{
    public class HostRibbonForm : GISShare.Controls.WinForm.WFNew.RibbonForm, GISShare.Controls.Plugin.IBaseHost, GISShare.Controls.Plugin.IBaseHost2
    {
        /// <summary>
        /// 记录插件加载和反射的事件
        /// </summary>
        public event PluginReflectionEventHandler PluginReflection;

        private HostRibbonObject m_HostRibbonObject;

        /// <summary>
        /// 一旦装载成功再次调用则无效
        /// </summary>
        /// <param name="strHostFrameworkFileName">装载宿主可序列化的框架对象</param>
        /// <param name="pluginDLLFolder"></param>
        /// <param name="strFilterFilePathArray"></param>
        /// <param name="strFilterObjectNameArray"></param>
        /// <param name="hook"></param>
        /// <param name="ribbonControl"></param>
        /// <param name="ribbonStatusBar"></param>
        /// <param name="contextPopupManager"></param>
        /// <param name="dockPanelManager"></param>
        /// <returns></returns>
        public bool RunPluginEngine(string strHostFrameworkFileName,
            string pluginDLLFolder,
            string[] strFilterFilePathArray,
            bool bFilterFilePathArrayTypeRemove,
            string[] strFilterObjectNameArray,
            bool bFilterObjectNameArrayTypeRemove,
            object hook,
            GISShare.Controls.WinForm.WFNew.RibbonControl ribbonControl,
            GISShare.Controls.WinForm.WFNew.RibbonStatusBar ribbonStatusBar,
            GISShare.Controls.WinForm.WFNew.ContextPopupManager contextPopupManager,
            GISShare.Controls.WinForm.WFNew.DockPanel.DockPanelManager dockPanelManager)
        {
            if (this.m_HostRibbonObject == null)
            {
                this.m_HostRibbonObject = new HostRibbonObject(strHostFrameworkFileName, pluginDLLFolder, strFilterFilePathArray, bFilterFilePathArrayTypeRemove, strFilterObjectNameArray, bFilterObjectNameArrayTypeRemove, hook, ribbonControl, ribbonStatusBar, contextPopupManager, dockPanelManager);
                this.m_HostRibbonObject.PluginReflection += new PluginReflectionEventHandler(HostRibbonObject_PluginReflection);
            }
            //
            return this.m_HostRibbonObject.RunPluginEngine();
        }
        void HostRibbonObject_PluginReflection(object sender, PluginReflectionEventArgs e)
        {
            this.OnPluginReflection(e);
        }

        #region IBaseHost
        /// <summary>
        /// 钩子信息
        /// </summary>
        public object Hook
        {
            get { return this.m_HostRibbonObject.Hook; }
        }

        /// <summary>
        /// 插件目录字典
        /// </summary>
        public PluginCategoryDictionary PluginCategoryDictionary
        {
            get { return this.m_HostRibbonObject.PluginCategoryDictionary; }
        }

        /// <summary>
        /// 提供其它操作
        /// </summary>
        /// <param name="obj">参数</param>
        /// <returns>返回</returns>
        public virtual object OtherOperation(object obj)
        {
            return this.m_HostRibbonObject.OtherOperation(obj);
        }
        #endregion

        #region IBaseHost2
        /// <summary>
        /// 追加插件对象
        /// </summary>
        public void AppendPlugin()
        {
            if (this.m_HostRibbonObject != null)
            {
                AppendPluginTBForm appendPluginTBForm = new AppendPluginTBForm(this.m_HostRibbonObject);
                appendPluginTBForm.Owner = this;
                appendPluginTBForm.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                appendPluginTBForm.ShowDialog();
            }
        }

        /// <summary>
        /// 追加插件对象
        /// </summary>
        /// <param name="pluginDLLFile"></param>
        public PluginCategoryDictionary AppendPluginObject(string pluginDLLFile)
        {
            if (this.m_HostRibbonObject != null)
            {
                return this.m_HostRibbonObject.AppendPluginObject(pluginDLLFile);
            }
            //
            return null;
        }
        #endregion

        public void UIView()
        {
            if (this.m_HostRibbonObject != null)
            {
                UIViewTBForm uiViewForm = new UIViewTBForm(this.m_HostRibbonObject.UIView());
                uiViewForm.Owner = this;
                uiViewForm.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
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
