using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.WFNew
{
    public class BaseItemStackExItemP : IPlugin, IPluginInfo , IEventChain, IPlugin2, ISetEntityObject, IBaseItemP_, IBaseItemStackItemP, IBaseItemStackExItemP, ISubItem
    {
        #region 私有属性
        private object m_EntityObject;
        #endregion

        #region 受保护的属性
        protected string _Name = "GISShare.Controls.Plugin.WinForm.WFNew.BaseItemStackExItemP";
        //
        protected string _Text = "GISShare.Controls.Plugin.WinForm.WFNew.BaseItemStackExItemP";
        protected bool _Visible = true;
        protected bool _Enabled = true;
        protected System.Windows.Forms.Padding _Padding = new System.Windows.Forms.Padding(0);
        protected System.Drawing.Font _Font = new System.Drawing.Font("宋体", 9f);
        protected System.Drawing.Color _ForeColor = System.Drawing.SystemColors.ControlText;
        protected System.Drawing.Size _Size = new System.Drawing.Size(21, 21);
        protected bool _Checked = false;
        protected bool _LockHeight = false;
        protected bool _LockWith = false;
        protected string _Category = "默认";
        protected System.Drawing.Size _MinimumSize = new System.Drawing.Size(10, 10);
        protected bool _UsingViewOverflow = true;
        //
        protected bool _CanExchangeItem = false;
        protected bool _ReverseLayout = false;
        protected bool _IsStretchItems = true;
        protected bool _IsRestrictItems = false;
        protected int _RestrictItemsWidth = -1;
        protected int _RestrictItemsHeight = -1;
        protected int _LineDistance = 1;
        protected int _ColumnDistance = 1;
        protected int _MinSize = 30;
        protected int _MaxSize = 30;
        protected System.Windows.Forms.Orientation _eOrientation = System.Windows.Forms.Orientation.Vertical;
        //
        protected int _LeftTopRadius = 0;
        protected int _RightTopRadius = 0;
        protected int _LeftBottomRadius = 0;
        protected int _RightBottomRadius = 0;
        protected int _TopViewItemIndex = 0;
        protected bool _PreButtonIncreaseIndex = true;
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
                return (int)CategoryIndex_1_Style.eBaseItemStackExItem;
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

        #region IBaseItemStackItemP
        public bool CanExchangeItem
        {
            get { return _CanExchangeItem; }
        }

        public bool ReverseLayout
        {
            get { return _ReverseLayout; }
        }

        public bool IsStretchItems
        {
            get { return _IsStretchItems; }
        }

        public bool IsRestrictItems
        {
            get { return _IsRestrictItems; }
        }

        public int RestrictItemsWidth
        {
            get { return _RestrictItemsWidth; }
        }

        public int RestrictItemsHeight
        {
            get { return _RestrictItemsHeight; }
        }

        public int LineDistance
        {
            get { return _LineDistance; }
        }

        public int ColumnDistance
        {
            get { return _ColumnDistance; }
        }

        public int MinSize
        {
            get { return _MinSize; }
        }

        public int MaxSize
        {
            get { return _MaxSize; }
        }

        public System.Windows.Forms.Orientation eOrientation
        {
            get { return _eOrientation; }
        }
        #endregion

        #region IBaseItemStackExItemP
        public int LeftTopRadius
        {
            get { return _LeftTopRadius; }
        }

        public int RightTopRadius
        {
            get { return _RightTopRadius; }
        }

        public int LeftBottomRadius
        {
            get { return _LeftBottomRadius; }
        }

        public int RightBottomRadius
        {
            get { return _RightBottomRadius; }
        }

        public int TopViewItemIndex
        {
            get { return _TopViewItemIndex; }
        }

        public bool PreButtonIncreaseIndex
        {
            get { return _PreButtonIncreaseIndex; }
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

        public static GISShare.Controls.WinForm.WFNew.BaseItemStackExItem CreateBaseItemStackExItem(IBaseItemStackExItemP pBaseItemP)
        {
            GISShare.Controls.WinForm.WFNew.BaseItemStackExItem baseItem = new Controls.WinForm.WFNew.BaseItemStackExItem();

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
            baseItem.LockHeight = pBaseItemP.LockHeight;
            baseItem.LockWith = pBaseItemP.LockWith;
            baseItem.Padding = pBaseItemP.Padding;
            baseItem.Size = pBaseItemP.Size;
            baseItem.Text = pBaseItemP.Text;
            baseItem.Visible = pBaseItemP.Visible;
            baseItem.Category = pBaseItemP.Category;
            baseItem.MinimumSize = pBaseItemP.MinimumSize;
            baseItem.UsingViewOverflow = pBaseItemP.UsingViewOverflow;
            //IBaseItemStackItemP
            baseItem.eOrientation = pBaseItemP.eOrientation;
            baseItem.CanExchangeItem = pBaseItemP.CanExchangeItem;
            baseItem.ReverseLayout = pBaseItemP.ReverseLayout;
            baseItem.IsStretchItems = pBaseItemP.IsStretchItems;
            baseItem.IsRestrictItems = pBaseItemP.IsRestrictItems;
            baseItem.RestrictItemsWidth = pBaseItemP.RestrictItemsWidth;
            baseItem.RestrictItemsHeight = pBaseItemP.RestrictItemsHeight;
            baseItem.LineDistance = pBaseItemP.LineDistance;
            baseItem.ColumnDistance = pBaseItemP.ColumnDistance;
            baseItem.MinSize = pBaseItemP.MinSize;
            baseItem.MaxSize = pBaseItemP.MaxSize;
            //IBaseItemStackExItemP
            baseItem.LeftTopRadius = pBaseItemP.LeftTopRadius;
            baseItem.RightTopRadius = pBaseItemP.RightTopRadius;
            baseItem.LeftBottomRadius = pBaseItemP.LeftBottomRadius;
            baseItem.RightBottomRadius = pBaseItemP.RightBottomRadius;
            baseItem.TopViewItemIndex = pBaseItemP.TopViewItemIndex;
            baseItem.PreButtonIncreaseIndex = pBaseItemP.PreButtonIncreaseIndex;

            return baseItem;
        }
    }
}
