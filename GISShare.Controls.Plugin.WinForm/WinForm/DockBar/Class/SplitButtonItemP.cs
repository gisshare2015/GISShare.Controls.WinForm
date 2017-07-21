using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.DockBar
{
    public class SplitButtonItemP : IPlugin, IPluginInfo, IEventChain, IPlugin2, ISetEntityObject, IBaseItemDBP_, ISplitButtonItemP, ISubItem
    {
        #region 私有属性
        private object m_EntityObject;
        #endregion

        #region 受保护的属性
        protected string _Name = "GISShare.Controls.Plugin.WinForm.DockBar.SplitButtonItemP";
        //
        protected string _Text = "GISShare.Controls.Plugin.WinForm.DockBar.SplitButtonItemP";
        protected string _ToolTipText = "GISShare.Controls.Plugin.WinForm.DockBar.SplitButtonItemP";
        protected int _MergeIndex = -1;
        protected bool _Visible = true;
        protected string _Category = "";
        protected System.Drawing.Image _Image = null;
        //protected int _ImageIndex = -1;
        //protected string _ImageKey = "";
        protected bool _DoubleClickEnabled = false;
        protected bool _Enabled = true;
        protected System.Drawing.Font _Font = new System.Drawing.Font("微软雅黑", 9f);
        protected System.Drawing.Color _ForeColor = System.Drawing.SystemColors.ControlText;
        protected System.Windows.Forms.ToolStripItemDisplayStyle _DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText;
        protected System.Drawing.ContentAlignment _ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
        protected System.Windows.Forms.TextImageRelation _TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
        protected System.Windows.Forms.ToolStripTextDirection _TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
        protected System.Drawing.Size _Size = new System.Drawing.Size(21, 21);
        protected System.Windows.Forms.RightToLeft _RightToLeft = System.Windows.Forms.RightToLeft.No;
        protected System.Windows.Forms.ToolStripItemOverflow _Overflow = System.Windows.Forms.ToolStripItemOverflow.AsNeeded;
        protected System.Windows.Forms.ToolStripItemImageScaling _ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.SizeToFit;
        protected System.Drawing.Color _ImageTransparentColor = System.Drawing.Color.White;
        protected System.Windows.Forms.Padding _Margin = new System.Windows.Forms.Padding(0, 1, 0, 2);
        protected System.Windows.Forms.MergeAction _MergeAction = System.Windows.Forms.MergeAction.Append;
        protected System.Drawing.ContentAlignment _TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        protected System.Windows.Forms.Padding _Padding = new System.Windows.Forms.Padding(0);
        protected bool _RightToLeftAutoMirrorImage = false;
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
                return (int)CategoryIndex_5_Style.eSplitButtonItem;
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

        #region IBaseItemDBP_
        public System.Windows.Forms.ToolStripItemDisplayStyle DisplayStyle
        {
            get { return _DisplayStyle; }
        }

        public bool DoubleClickEnabled
        {
            get { return _DoubleClickEnabled; }
        }

        public bool Enabled
        {
            get { return _Enabled; }
        }

        public System.Drawing.Font Font
        {
            get { return _Font; }
        }

        public System.Drawing.Color ForeColor
        {
            get { return _ForeColor; }
        }

        public System.Drawing.Image Image
        {
            get { return _Image; }
        }

        public System.Drawing.ContentAlignment ImageAlign
        {
            get { return _ImageAlign; }
        }

        //public int ImageIndex
        //{
        //    get { return _ImageIndex; }
        //}

        //public string ImageKey
        //{
        //    get { return _ImageKey; }
        //}

        public System.Windows.Forms.ToolStripItemImageScaling ImageScaling
        {
            get { return _ImageScaling; }
        }

        public System.Drawing.Color ImageTransparentColor
        {
            get { return _ImageTransparentColor; }
        }

        public System.Windows.Forms.Padding Margin
        {
            get { return _Margin; }
        }

        public System.Windows.Forms.MergeAction MergeAction
        {
            get { return _MergeAction; }
        }

        public int MergeIndex
        {
            get { return _MergeIndex; }
        }

        public System.Windows.Forms.ToolStripItemOverflow Overflow
        {
            get { return _Overflow; }
        }

        public System.Windows.Forms.Padding Padding
        {
            get { return _Padding; }
        }

        public System.Windows.Forms.RightToLeft RightToLeft
        {
            get { return _RightToLeft; }
        }

        public System.Drawing.Size Size
        {
            get { return _Size; }
        }

        public string Text
        {
            get { return _Text; }
        }

        public System.Drawing.ContentAlignment TextAlign
        {
            get { return _TextAlign; }
        }

        public System.Windows.Forms.ToolStripTextDirection TextDirection
        {
            get { return _TextDirection; }
        }

        public System.Windows.Forms.TextImageRelation TextImageRelation
        {
            get { return _TextImageRelation; }
        }

        public string ToolTipText
        {
            get { return _ToolTipText; }
        }

        public bool Visible
        {
            get { return _Visible; }
        }

        public bool RightToLeftAutoMirrorImage
        {
            get { return _RightToLeftAutoMirrorImage; }
        }
        //

        public string Category
        {
            get { return _Category; }
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

        public static GISShare.Controls.WinForm.DockBar.SplitButtonItem CreateSplitButtonItem(GISShare.Controls.Plugin.WinForm.DockBar.ISplitButtonItemP pBaseItemDBP)
        {
            GISShare.Controls.WinForm.DockBar.SplitButtonItem baseItem = new Controls.WinForm.DockBar.SplitButtonItem();

            //IPlugin
            baseItem.Name = pBaseItemDBP.Name;
            //ISetEntityObject
            GISShare.Controls.Plugin.ISetEntityObject pSetEntityObject = pBaseItemDBP as GISShare.Controls.Plugin.ISetEntityObject;
            if (pSetEntityObject != null) pSetEntityObject.SetEntityObject(baseItem);
            //IBaseItemP_
            baseItem.Category = pBaseItemDBP.Category;
            baseItem.DisplayStyle = pBaseItemDBP.DisplayStyle;
            baseItem.DoubleClickEnabled = pBaseItemDBP.DoubleClickEnabled;
            baseItem.Enabled = pBaseItemDBP.Enabled;
            baseItem.Font = pBaseItemDBP.Font;
            baseItem.ForeColor = pBaseItemDBP.ForeColor;
            baseItem.Image = pBaseItemDBP.Image;
            baseItem.ImageAlign = pBaseItemDBP.ImageAlign;
            //baseItem.ImageIndex = pBaseItemDBP.ImageIndex;
            //baseItem.ImageKey = pBaseItemDBP.ImageKey;
            baseItem.ImageScaling = pBaseItemDBP.ImageScaling;
            baseItem.ImageTransparentColor = pBaseItemDBP.ImageTransparentColor;
            baseItem.Margin = pBaseItemDBP.Margin;
            baseItem.MergeAction = pBaseItemDBP.MergeAction;
            baseItem.MergeIndex = pBaseItemDBP.MergeIndex;
            baseItem.Overflow = pBaseItemDBP.Overflow;
            baseItem.Padding = pBaseItemDBP.Padding;
            baseItem.RightToLeft = pBaseItemDBP.RightToLeft;
            baseItem.RightToLeftAutoMirrorImage = pBaseItemDBP.RightToLeftAutoMirrorImage;
            baseItem.Size = pBaseItemDBP.Size;
            baseItem.Text = pBaseItemDBP.Text;
            baseItem.TextAlign = pBaseItemDBP.TextAlign;
            baseItem.TextDirection = pBaseItemDBP.TextDirection;
            baseItem.TextImageRelation = pBaseItemDBP.TextImageRelation;
            baseItem.ToolTipText = pBaseItemDBP.ToolTipText;
            baseItem.Visible = pBaseItemDBP.Visible;

            return baseItem;
        }
    }
}
