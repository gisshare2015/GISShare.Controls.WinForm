using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.WFNew.DockPanel
{
    public class DockPanelP : IPlugin, IPluginInfo , IEventChain, IPlugin2, ISetEntityObject, IBaseItemP_, IDockPanelP, ISubItem
    {
        #region 私有属性
        private object m_EntityObject;
        #endregion

        #region 受保护的属性
        protected string _Name = "GISShare.Controls.Plugin.WinForm.WFNew.DockPanel.DockPanelP";
        //
        protected string _Text = "GISShare.Controls.Plugin.WinForm.WFNew.DockPanel.DockPanelP";
        protected bool _Visible = true;
        protected bool _Enabled = true;
        protected System.Windows.Forms.Padding _Padding = new System.Windows.Forms.Padding(0);
        protected System.Drawing.Font _Font = new System.Drawing.Font("宋体", 9f);
        protected System.Drawing.Color _ForeColor = System.Drawing.SystemColors.ControlText;
        protected System.Drawing.Size _Size = new System.Drawing.Size(120, 120);
        protected bool _Checked = false;
        protected bool _LockHeight = false;
        protected bool _LockWith = false;
        protected string _Category = "默认";
        protected System.Drawing.Size _MinimumSize = new System.Drawing.Size(10, 10);
        protected bool _UsingViewOverflow = true;
        //
        protected GISShare.Controls.WinForm.WFNew.ContextPopupStyle _eContextPopupStyle = Controls.WinForm.WFNew.ContextPopupStyle.eNormal;
        //
        protected bool _Interal = true;
        protected System.Windows.Forms.DockStyle _DockStyle = System.Windows.Forms.DockStyle.None;
        protected bool _VisibleEx = true;
        protected System.Drawing.Point _DockPanelFloatFormLocation = new System.Drawing.Point(60, 60);
        protected System.Drawing.Size _DockPanelFloatFormSize = new System.Drawing.Size(260, 260);
        protected int _BasePanelSelectedIndex = 0;
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
                return (int)CategoryIndex_2_Style.eDockPanel;
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

        #region IBaseItemP_
        public string Text
        {
            get { return _Text; }
        }

        public bool Visible
        {
            get { return _Visible; }
        }

        public bool Enabled
        {
            get { return _Enabled; }
        }

        public System.Windows.Forms.Padding Padding
        {
            get { return _Padding; }
        }

        public System.Drawing.Font Font
        {
            get { return _Font; }
        }

        public System.Drawing.Color ForeColor
        {
            get { return _ForeColor; }
        }

        public System.Drawing.Size Size
        {
            get { return _Size; }
        }

        public bool Checked
        {
            get { return _Checked; }
        }

        public bool LockHeight
        {
            get { return _LockHeight; }
        }

        public bool LockWith
        {
            get { return _LockWith; }
        }

        public string Category
        {
            get { return _Category; }
        }

        public System.Drawing.Size MinimumSize
        {
            get { return _MinimumSize; }
        }

        public bool UsingViewOverflow
        {
            get { return _UsingViewOverflow; }
        }
        #endregion

        #region IDockPanelP
        public bool VisibleEx
        {
            get { return _VisibleEx; }
        }

        public bool Interal
        {
            get { return _Interal; }
        }

        public System.Windows.Forms.DockStyle DockStyle
        {
            get { return _DockStyle; }
        }

        public System.Drawing.Point DockPanelFloatFormLocation
        {
            get { return _DockPanelFloatFormLocation; }
        }

        public System.Drawing.Size DockPanelFloatFormSize
        {
            get { return _DockPanelFloatFormSize; }
        }

        public int BasePanelSelectedIndex
        {
            get { return _BasePanelSelectedIndex; }
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

        public static GISShare.Controls.WinForm.WFNew.DockPanel.DockPanel CreateDockPanel(WFNew.DockPanel.IDockPanelP pBaseItemP)
        {
            GISShare.Controls.WinForm.WFNew.DockPanel.DockPanel baseItem = new Controls.WinForm.WFNew.DockPanel.DockPanel();

            //IPlugin
            baseItem.Name = pBaseItemP.Name;
            //ISetEntityObject
            GISShare.Controls.Plugin.ISetEntityObject pSetEntityObject = pBaseItemP as GISShare.Controls.Plugin.ISetEntityObject;
            if (pSetEntityObject != null) pSetEntityObject.SetEntityObject(baseItem);
            //IBaseItemP_
            baseItem.Checked = pBaseItemP.Checked;
            baseItem.Enabled = pBaseItemP.Enabled;
            baseItem.Font = pBaseItemP.Font;
            baseItem.ForeColor = pBaseItemP.ForeColor;
            //baseItem.LockHeight = pBaseItemP.LockHeight;
            //baseItem.LockWith = pBaseItemP.LockWith;
            baseItem.Padding = pBaseItemP.Padding;
            baseItem.Size = pBaseItemP.Size;
            baseItem.Text = pBaseItemP.Text;
            //baseItem.Visible = pBaseItemP.Visible;
            //baseItem.Category = pBaseItemP.Category;
            baseItem.MinimumSize = pBaseItemP.MinimumSize;
            //baseItem.UsingViewOverflow = pBaseItemP.UsingViewOverflow;
            //IDockPanelP
            baseItem.VisibleEx = pBaseItemP.VisibleEx;
            baseItem.DockPanelFloatFormLocation = pBaseItemP.DockPanelFloatFormLocation;
            baseItem.DockPanelFloatFormSize = pBaseItemP.DockPanelFloatFormSize;
            baseItem.BasePanelSelectedIndex = pBaseItemP.BasePanelSelectedIndex;

            return baseItem;
        }
    }
}
