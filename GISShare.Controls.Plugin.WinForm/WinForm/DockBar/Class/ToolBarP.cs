using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.DockBar
{
    public class ToolBarP : IPlugin, IPluginInfo, IEventChain, IPlugin2, ISetEntityObject, IDockBarP_, IToolBarP, ISubItem
    {
        #region 私有属性
        private object m_EntityObject;
        #endregion

        #region 受保护的属性
        protected string _Name = "GISShare.Controls.Plugin.WinForm.DockBar.ToolBarP";
        //
        protected string _Text = "GISShare.Controls.Plugin.WinForm.DockBar.ToolBarP";
        protected System.Drawing.Image _Image = null;
        protected bool _Visible = true;
        protected bool _CanFloat = true;
        protected System.Drawing.Point _DockBarFloatFormLocation = new System.Drawing.Point(160, 160);
        protected System.Drawing.Size _DockBarFloatFormSize = new System.Drawing.Size(260, 76);
        protected int _Row = 0;
        protected System.Windows.Forms.DockStyle _DockStyle = System.Windows.Forms.DockStyle.Top;
        protected System.Drawing.Point _Location = new System.Drawing.Point(0, 0);
        protected System.Windows.Forms.Padding _GripMargin = new System.Windows.Forms.Padding(2) ;
        protected System.Windows.Forms.ToolStripGripStyle _GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
        protected GISShare.Controls.WinForm.DockBar.DockBarGripStyles _eDockBarGripStyle = Controls.WinForm.DockBar.DockBarGripStyles.Default;
        protected bool _CanOverflow = true;
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
                return (int)CategoryIndex_5_Style.eToolBar;
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

        #region IDockBarP_
        public string Text
        {
            get { return _Text; }
        }

        public System.Drawing.Image Image
        {
            get { return _Image; }
        }

        public bool Visible
        {
            get { return _Visible; }
        }

        public bool CanFloat
        {
            get { return _CanFloat; }
        }

        public System.Drawing.Point DockBarFloatFormLocation
        {
            get { return _DockBarFloatFormLocation; }
        }

        public System.Drawing.Size DockBarFloatFormSize
        {
            get { return _DockBarFloatFormSize; }
        }

        public int Row
        {
            get { return _Row; }
        }

        public System.Windows.Forms.DockStyle DockStyle
        {
            get { return _DockStyle; }
        }

        public System.Drawing.Point Location
        {
            get { return _Location; }
        }

        public System.Windows.Forms.Padding GripMargin
        {
            get { return _GripMargin; }
        }

        public System.Windows.Forms.ToolStripGripStyle GripStyle
        {
            get { return _GripStyle; }
        }

        public GISShare.Controls.WinForm.DockBar.DockBarGripStyles eDockBarGripStyle
        {
            get { return _eDockBarGripStyle; }
        }

        public bool CanOverflow
        {
            get { return _CanOverflow; }
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

        public static GISShare.Controls.WinForm.DockBar.ToolBar CreateToolBar(GISShare.Controls.Plugin.WinForm.DockBar.IToolBarP pDockBarP)
        {
            GISShare.Controls.WinForm.DockBar.ToolBar baseItem = new Controls.WinForm.DockBar.ToolBar();

            //IPlugin
            baseItem.Name = pDockBarP.Name;
            //ISetEntityObject
            GISShare.Controls.Plugin.ISetEntityObject pSetEntityObject = pDockBarP as GISShare.Controls.Plugin.ISetEntityObject;
            if (pSetEntityObject != null) pSetEntityObject.SetEntityObject(baseItem);
            //IDockBarP_
            baseItem.CanFloat = pDockBarP.CanFloat;
            baseItem.CanOverflow = pDockBarP.CanOverflow;
            baseItem.DockBarFloatFormLocation = pDockBarP.DockBarFloatFormLocation;
            baseItem.DockBarFloatFormSize = pDockBarP.DockBarFloatFormSize;
            baseItem.eDockBarGripStyle = pDockBarP.eDockBarGripStyle;
            baseItem.GripMargin = pDockBarP.GripMargin;
            baseItem.GripStyle = pDockBarP.GripStyle;
            baseItem.Image = pDockBarP.Image;
            baseItem.Location = pDockBarP.Location;
            baseItem.Text = pDockBarP.Text;
            baseItem.Visible = pDockBarP.Visible;

            return baseItem;
        }
    }
}
