using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.WFNew.DockPanel
{
    public class BasePanelP : IPlugin, IPluginInfo , IEventChain, IPlugin2, ISetEntityObject, IBaseItemP_, IBasePanelP
    {
        #region 私有属性
        private object m_EntityObject;
        #endregion

        #region 受保护的属性
        protected string _Name = "GISShare.Controls.Plugin.WinForm.WFNew.DockPanel.BasePanelP";
        //
        protected string _Text = "GISShare.Controls.Plugin.WinForm.WFNew.DockPanel.BasePanelP";
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
        protected bool _CanDockUp = true;
        protected bool _CanDockLeft = true;
        protected bool _CanDockRight = true;
        protected bool _CanDockBottom = true;
        protected bool _CanDockFill = true;
        protected bool _CanFloat = true;
        protected bool _CanHide = true;
        protected bool _CanClose = true;
        protected bool _IsBasePanel = true;
        protected bool _IsDocumentPanel = true;
        protected bool _VisibleEx = true;
        protected System.Drawing.Image _Image = null;
        protected System.Drawing.Point _DockPanelFloatFormLocation = new System.Drawing.Point(60, 60);
        protected System.Drawing.Size _DockPanelFloatFormSize = new System.Drawing.Size(260, 260);
        protected System.Windows.Forms.Control[] _ChildControls = null;
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
                return (int)CategoryIndex_2_Style.eBasePanel;
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

        #region IBasePanelP
        public bool CanDockUp
        {
            get { return _CanDockUp; }
        }

        public bool CanDockLeft
        {
            get { return _CanDockLeft; }
        }

        public bool CanDockRight
        {
            get { return _CanDockRight; }
        }

        public bool CanDockBottom
        {
            get { return _CanDockBottom; }
        }

        public bool CanDockFill
        {
            get { return _CanDockFill; }
        }

        public bool CanFloat
        {
            get { return _CanFloat; }
        }

        public bool CanHide
        {
            get { return _CanHide; }
        }

        public bool CanClose
        {
            get { return _CanClose; }
        }

        public bool IsBasePanel
        {
            get { return _IsBasePanel; }
        }

        public bool IsDocumentPanel
        {
            get { return _IsDocumentPanel; }
        }

        public bool VisibleEx
        {
            get { return _VisibleEx; }
        }
        public System.Drawing.Image Image
        {
            get { return _Image; }
        }

        public System.Drawing.Point DockPanelFloatFormLocation
        {
            get { return _DockPanelFloatFormLocation; }
        }

        public System.Drawing.Size DockPanelFloatFormSize
        {
            get { return _DockPanelFloatFormSize; }
        }

        public System.Windows.Forms.Control[] ChildControls
        {
            get { return _ChildControls; }
        }
        #endregion

        //
        //
        //

        public static GISShare.Controls.WinForm.WFNew.DockPanel.BasePanel CreateBasePanel(DockPanel.IBasePanelP pBaseItemP)
        {
            GISShare.Controls.WinForm.WFNew.DockPanel.BasePanel baseItem = new Controls.WinForm.WFNew.DockPanel.BasePanel();

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
            //IBasePanelP
            baseItem.CanDockUp = pBaseItemP.CanDockUp;
            baseItem.CanDockLeft = pBaseItemP.CanDockLeft;
            baseItem.CanDockRight = pBaseItemP.CanDockRight;
            baseItem.CanDockBottom = pBaseItemP.CanDockBottom;
            baseItem.CanDockFill = pBaseItemP.CanDockFill;
            baseItem.CanFloat = pBaseItemP.CanFloat;
            baseItem.CanHide = pBaseItemP.CanHide;
            baseItem.CanClose = pBaseItemP.CanClose;
            baseItem.IsBasePanel = pBaseItemP.IsBasePanel;
            baseItem.IsDocumentPanel = pBaseItemP.IsDocumentPanel;
            baseItem.VisibleEx = pBaseItemP.VisibleEx;
            baseItem.Image = pBaseItemP.Image;
            baseItem.DockPanelFloatFormLocation = pBaseItemP.DockPanelFloatFormLocation;
            baseItem.DockPanelFloatFormSize = pBaseItemP.DockPanelFloatFormSize;
            if (pBaseItemP.ChildControls != null)
            {
                for (int i = pBaseItemP.ChildControls.Length - 1; i >= 0; i--)
                {
                    baseItem.Controls.Add(pBaseItemP.ChildControls[i]);
                }
            }

            return baseItem;
        }
    }
}
