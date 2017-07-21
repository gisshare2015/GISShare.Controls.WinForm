using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.WFNew
{
    public class MenuButtonItemP : IPlugin, IPluginInfo, IEventChain, IPlugin2, ISetEntityObject, IBaseItemP_, ILabelItemP, IImageLabelItemP, IImageBoxItemP, IBaseButtonItemP, IDropDownButtonItemP, ISplitButtonItemP, IButtonItemP, IMenuButtonItemP, ISubItem
    {
        #region 私有属性
        private object m_EntityObject;
        #endregion

        #region 受保护的属性
        protected string _Name = "GISShare.Controls.Plugin.WinForm.WFNew.MenuButtonItemP";
        //
        protected string _Text = "GISShare.Controls.Plugin.WinForm.WFNew.MenuButtonItemP";
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
        protected System.Drawing.ContentAlignment _TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        //
        protected bool _AutoPlanTextRectangle = true;
        protected int _ITSpace = 1;
        protected GISShare.Controls.WinForm.WFNew.DisplayStyle _eDisplayStyle = Controls.WinForm.WFNew.DisplayStyle.eImageAndText;
        //
        protected System.Drawing.Image _Image = null;
        protected System.Drawing.ContentAlignment _ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
        protected GISShare.Controls.WinForm.WFNew.ImageSizeStyle _eImageSizeStyle = Controls.WinForm.WFNew.ImageSizeStyle.eDefault;
        protected System.Drawing.Size _ImageSize = new System.Drawing.Size(30, 30);
        //
        protected bool _ShowNomalState = false;
        protected int _LeftTopRadius = 3;
        protected int _RightTopRadius = 3;
        protected int _LeftBottomRadius = 3;
        protected int _RightBottomRadius = 3;
        //
        protected int _DropDownDistance = 23;
        protected GISShare.Controls.WinForm.WFNew.ArrowStyle _eArrowStyle = GISShare.Controls.WinForm.WFNew.ArrowStyle.eToRight;
        protected GISShare.Controls.WinForm.WFNew.ArrowDock _eArrowDock = GISShare.Controls.WinForm.WFNew.ArrowDock.eRight;
        protected GISShare.Controls.WinForm.WFNew.ContextPopupStyle _eContextPopupStyle = GISShare.Controls.WinForm.WFNew.ContextPopupStyle.eNormal;
        protected System.Drawing.Size _ArrowSize = new System.Drawing.Size(4, 6);
        protected int _PopupSpace = 0;
        //
        protected int _ItemCount = 0;
        //
        protected bool _ShowNomalSplitLine = true;
        //
        protected GISShare.Controls.WinForm.WFNew.ButtonStyle _eButtonStyle = Controls.WinForm.WFNew.ButtonStyle.eButton;
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
                return (int)CategoryIndex_1_Style.eMenuButtonItem;
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

        #region ILabelItemP
        public System.Drawing.ContentAlignment TextAlign
        {
            get { return _TextAlign; }
        }
        #endregion

        #region IImageLabelItemP
        public bool AutoPlanTextRectangle
        {
            get { return _AutoPlanTextRectangle; }
        }

        public int ITSpace
        {
            get { return _ITSpace; }
        }

        public GISShare.Controls.WinForm.WFNew.DisplayStyle eDisplayStyle
        {
            get { return _eDisplayStyle; }
        }
        #endregion

        #region IImageBoxItemP
        public System.Drawing.Image Image
        {
            get { return _Image; }
        }

        public System.Drawing.ContentAlignment ImageAlign
        {
            get { return _ImageAlign; }
        }

        public GISShare.Controls.WinForm.WFNew.ImageSizeStyle eImageSizeStyle
        {
            get { return _eImageSizeStyle; }
        }

        public System.Drawing.Size ImageSize
        {
            get { return _ImageSize; }
        }
        #endregion

        #region IBaseButtonItemP
        public bool ShowNomalState
        {
            get { return _ShowNomalState; }
        }

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
        #endregion

        #region IDropDownButtonItemP
        public int DropDownDistance
        {
            get { return _DropDownDistance; }
        }

        public GISShare.Controls.WinForm.WFNew.ArrowStyle eArrowStyle
        {
            get { return _eArrowStyle; }
        }

        public GISShare.Controls.WinForm.WFNew.ArrowDock eArrowDock
        {
            get { return _eArrowDock; }
        }

        public GISShare.Controls.WinForm.WFNew.ContextPopupStyle eContextPopupStyle
        {
            get { return _eContextPopupStyle; }
        }

        public System.Drawing.Size ArrowSize
        {
            get { return _ArrowSize; }
        }

        public int PopupSpace
        {
            get { return _PopupSpace; }
        }
        #endregion

        #region ISplitButtonItemP
        public bool ShowNomalSplitLine
        {
            get { return _ShowNomalSplitLine; }
        }
        #endregion

        #region IButtonItemP
        public GISShare.Controls.WinForm.WFNew.ButtonStyle eButtonStyle
        {
            get { return _eButtonStyle; }
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

        public static GISShare.Controls.WinForm.WFNew.MenuButtonItem CreateMenuButtonItem(IMenuButtonItemP pBaseItemP)
        {
            GISShare.Controls.WinForm.WFNew.MenuButtonItem baseItem = new Controls.WinForm.WFNew.MenuButtonItem();

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
            //ILabelItemP
            baseItem.TextAlign = pBaseItemP.TextAlign;
            //IImageBoxItemP
            baseItem.eImageSizeStyle = pBaseItemP.eImageSizeStyle;
            baseItem.Image = pBaseItemP.Image;
            baseItem.ImageAlign = pBaseItemP.ImageAlign;
            baseItem.ImageSize = pBaseItemP.ImageSize;
            //IImageLabelItemP
            baseItem.AutoPlanTextRectangle = pBaseItemP.AutoPlanTextRectangle;
            baseItem.ITSpace = pBaseItemP.ITSpace;
            baseItem.eDisplayStyle = pBaseItemP.eDisplayStyle;
            //IBaseButtonItemP
            baseItem.LeftBottomRadius = pBaseItemP.LeftBottomRadius;
            baseItem.LeftTopRadius = pBaseItemP.LeftTopRadius;
            baseItem.RightBottomRadius = pBaseItemP.RightBottomRadius;
            baseItem.RightTopRadius = pBaseItemP.RightTopRadius;
            baseItem.ShowNomalState = pBaseItemP.ShowNomalState;
            //IDropDownButtonItemP
            baseItem.DropDownDistance = pBaseItemP.DropDownDistance;
            baseItem.eArrowStyle = pBaseItemP.eArrowStyle;
            baseItem.eArrowDock = pBaseItemP.eArrowDock;
            baseItem.eContextPopupStyle = pBaseItemP.eContextPopupStyle;
            baseItem.ArrowSize = pBaseItemP.ArrowSize;
            baseItem.PopupSpace = pBaseItemP.PopupSpace;
            //ISplitButtonItemP
            baseItem.ShowNomalSplitLine = pBaseItemP.ShowNomalSplitLine;
            //IButtonItemP
            //baseItem.eButtonStyle = pBaseItemP.eButtonStyle;

            return baseItem;
        }
    }
}

