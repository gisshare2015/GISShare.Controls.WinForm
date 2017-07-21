using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.DockBar
{
    public class ContextMenuP : IPlugin, IPluginInfo , IEventChain, IPlugin2, ISetEntityObject, IContextMenuP, ISubItem
    {
        #region 私有属性
        private object m_EntityObject;
        #endregion

        #region 受保护的属性
        protected string _Name = "GISShare.Controls.Plugin.WinForm.DockBar.ContextMenuP";
        //
        protected string _Text = "GISShare.Controls.Plugin.WinForm.DockBar.ContextMenuP";
        protected bool _Visible = true;
        protected bool _Enabled = true;
        protected bool _DropShadowEnabled = false;
        //
        protected int _ItemCount = 0;
        #endregion

        #region IPlugin
        /// <summary>
        /// 唯一编码（必须设置）
        /// </summary>
        public string Name
        {
            get
            {
                return _Name;
            }
        }

        /// <summary>
        /// 接口所在的分类（用于标识自身反射对象的分类）
        /// </summary>
        public int CategoryIndex
        {
            get
            {
                return (int)CategoryIndex_5_Style.eContextMenu;
            }
        }
        #endregion

        #region IPluginInfo
        public virtual string GetDescribe()
        {
            return this.Text;
        }
        #endregion

        #region IInteraction
        public virtual object CommandOperation(int iOperationStyle, params object[] objs)
        {
            return null;
        }
        #endregion

        #region IEventChain
        /// <summary>
        /// 事件触发
        /// </summary>
        /// <param name="iEventStyle">事件类型</param>
        /// <param name="e">事件参数</param>
        public virtual void OnTriggerEvent(int iEventStyle, EventArgs e) { }
        #endregion

        #region IPlugin2
        public object EntityObject
        {
            get { return m_EntityObject; }
        }

        public virtual void OnCreate(object hook) { }
        #endregion

        #region ISetEntityObject
        void ISetEntityObject.SetEntityObject(object obj)
        {
            this.m_EntityObject = obj;
        }
        #endregion

        #region IContextMenuP
        public bool Enabled
        {
            get { return _Enabled; }
        }

        public string Text
        {
            get { return _Text; }
        }

        public bool Visible
        {
            get { return _Visible; }
        }

        public bool DropShadowEnabled
        {
            get { return _DropShadowEnabled; }
        }
        #endregion

        #region ISubItem
        /// <summary>
        /// 携带的 Item 数量
        /// </summary>
        public int ItemCount
        {
            get
            {
                return _ItemCount;
            }
        }

        /// <summary>
        /// 访问快捷菜单中每个Item的方法
        /// </summary>
        /// <param name="iIndex"></param>
        /// <param name="pItemDef"></param>
        public virtual void GetItemInfo(int iIndex, IItemDef pItemDef) { }
        #endregion

        //
        //
        //

        public static GISShare.Controls.WinForm.DockBar.ContextMenu CreateContextMenu(GISShare.Controls.Plugin.WinForm.DockBar.IContextMenuP pContextMenuP)
        {
            GISShare.Controls.WinForm.DockBar.ContextMenu baseItem = new Controls.WinForm.DockBar.ContextMenu();

            //IPlugin
            baseItem.Name = pContextMenuP.Name;
            //ISetEntityObject
            GISShare.Controls.Plugin.ISetEntityObject pSetEntityObject = pContextMenuP as GISShare.Controls.Plugin.ISetEntityObject;
            if (pSetEntityObject != null) pSetEntityObject.SetEntityObject(baseItem);
            //IContextMenuP
            baseItem.Text = pContextMenuP.Text;
            baseItem.Enabled = pContextMenuP.Enabled;
            baseItem.Visible = pContextMenuP.Visible;
            baseItem.DropShadowEnabled = pContextMenuP.DropShadowEnabled;

            return baseItem;
        }
    }
}
