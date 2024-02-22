using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    /// <summary>
    /// 所有绘制对象的基类
    /// </summary>
    [ToolboxItem(false), DesignTimeVisible(false), DefaultEvent("Click"), ToolboxItemFilter("GISShare.Controls.WinForm.WFNew")]
    public abstract class BaseItem : Component, 
        IRecordItem, ISetRecordItemHelper,
        IReset,
        ICloneable,
        ICategoryItem,
        IBaseItem, IBaseItem2, IBaseItem3, IBaseItem4, IBaseItem5, IBaseItem6, IBaseItem7, ISetBaseItemHelper,
        IBaseItemProperty,
        ITranslation,
        IViewDepend,
        IOwner, IBaseItemOwner, IBaseItemOwner2, ISetOwnerHelper,
        IBaseItemEvent, IBaseItemEvent2,
        IDismissPopupObject,
        IMessageChain, IMessagePermeate,
        IEventHelper,
        View.IViewItem, View.ISetViewItemHelper,
        IDesignHelper, IObjectDesignHelper
    {
        private const int CONST_MINVISIBLESIZE = 10;

        #region IRecordItem
        private int m_RecordID = 0;
        [Browsable(false), Description("自身的ID号（由系统管理，主要在记录布局文件时使用，常规状态下它是无效的）"), Category("属性")]
        public int RecordID
        {
            get { return m_RecordID; }
        }
        #endregion

        #region ISetRecordItemHelper
        void ISetRecordItemHelper.SetRecordID(int id)//设置RecordID，由系统管理（在保存布局时设置该属性）
        {
            this.m_RecordID = id;
        }
        #endregion

        #region ICloneable
        /// <summary>
        /// 获取一个完全的副本
        /// </summary>
        /// <returns></returns>
        public abstract object Clone();
        #endregion

        #region ICategoryItem
        string m_Category = "默认";
        [Browsable(true), DefaultValue("默认"), Description("用来描述该对象所属的分类"), Category("描述")]
        public string Category
        {
            get { return m_Category; }
            set { m_Category = value; }
        }
        #endregion

        #region IRenderable
        RenderStyle m_eRenderStyle = RenderStyle.eSystem;
        [Browsable(true), DefaultValue(typeof(RenderStyle), "eSystem"), Description("渲染类型"), Category("外观")]
        public virtual RenderStyle eRenderStyle
        {
            get { return m_eRenderStyle; }
            set { m_eRenderStyle = value; }
        }
        #endregion

        #region IBaseItem
        string m_Name;
        [Browsable(true), Description("名称"), Category("描述")]
        //[DefaultValue((string)null), Browsable(false)]
        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        string m_Text;
        [Browsable(true), Description("文本"), Category("外观")]
        public virtual string Text
        {
            get { return m_Text; }
            set
            {
                if (m_Text == value) return;
                m_Text = value;
                //this.Refresh();
                this.OnTextChanged(new EventArgs());
            }
        }

        bool m_Visible = true;
        [Browsable(true), DefaultValue(true), Description("可见"), Category("状态")]
        public virtual bool Visible
        {
            get { return m_Visible; }
            set
            {
                if (m_Visible == value) return;
                m_Visible = value;
                ((IMessageChain)this).SendMessage(new MessageInfo(this, MessageStyle.eMSVisibleChanged, new BoolValueChangedEventArgs(value)));
                //this.OnVisibleChanged(new EventArgs());
                this.UIUpdate();
            }
        }

        bool m_Enabled = true;
        [Browsable(true), DefaultValue(true), Description("可用"), Category("状态")]
        public virtual bool Enabled
        {
            get { return m_Enabled; }
            set
            {
                if (m_Enabled == value) return;
                //
                m_Enabled = value;
                ((IMessageChain)this).SendMessage(new MessageInfo(this, MessageStyle.eMSEnabledChanged, new BoolValueChangedEventArgs(value)));
                //this.OnEnabledChanged(new EventArgs());
                //
                this.Refresh();
            }
        }

        private Font m_Font = new Font("宋体", 9f);
        [Browsable(true), DefaultValue(typeof(Font), "\"宋体\", 9f"), Description("字体"), Category("外观")]
        public virtual Font Font
        {
            get { return m_Font; }
            set { m_Font = value; }
        }

        private Color m_ForeColor = System.Drawing.SystemColors.ControlText;
        [Browsable(true), DefaultValue(typeof(Color), "System.Drawing.SystemColors.ControlText"), Description("字体颜色"), Category("外观")]
        public virtual Color ForeColor
        {
            get { return m_ForeColor; }
            set { m_ForeColor = value; }
        }

        private bool m_HaveShadow = true;
        [Browsable(true), DefaultValue(true), Description("是否有字体阴影"), Category("状态")]
        public virtual bool HaveShadow
        {
            get { return m_HaveShadow; }
            set { m_HaveShadow = value; }
        }

        private Color m_ShadowColor = System.Drawing.SystemColors.ControlText;
        [Browsable(true), DefaultValue(typeof(Color), "System.Drawing.SystemColors.ControlText"), Description("字体阴影颜色"), Category("外观")]
        public virtual Color ShadowColor
        {
            get { return m_ShadowColor; }
            set { m_ShadowColor = value; }
        }

        private bool m_ForeCustomize = false;
        [Browsable(true), DefaultValue(false), Description("自定义文本色"), Category("状态")]
        public virtual bool ForeCustomize
        {
            get { return m_ForeCustomize; }
            set { m_ForeCustomize = value; }
        }

        [Browsable(false), Description("高度（与Size属性有关）"), Category("布局")]
        public int Height
        {
            get { return this.Size.Height; }
        }

        [Browsable(false), Description("宽度（与Size属性有关）"), Category("布局")]
        public int Width
        {
            get { return this.Size.Width; }
        }

        private Padding m_Padding = new Padding(0);
        [Browsable(true), DefaultValue(typeof(Padding), "0,0,0,0"), Description("获取或设置控件内的空白"), Category("布局")]
        public virtual Padding Padding
        {
            get { return m_Padding; }
            set { m_Padding = value; }
        }

        private Point m_Location = new Point(0, 0);
        [Browsable(false), Description("坐落点（如果是系统管理请不要尝试手动对其赋值！如：栈成员（IBaseItemStackItem））"), Category("布局")]
        public Point Location
        {
            get { return m_Location; }
            set
            {
                if (this.m_Location == value) return;
                PropertyChangedEventArgs e = new PropertyChangedEventArgs(typeof(Point), m_Location, value);
                m_Location = value;
                this.OnLocationChanged(e);
                this.UIUpdate();
            }
        }

        private Size m_Size = new Size(21, 21);
        [Browsable(true), DefaultValue(typeof(Size), "21,21"), Description("尺寸"), Category("布局")]
        public Size Size
        {
            get { return m_Size; }
            set
            {
                if (this.m_Size == value) return;
                PropertyChangedEventArgs e = new PropertyChangedEventArgs(typeof(Size), m_Size, value);
                m_Size = value;
                this.OnSizeChanged(e);
                this.UIUpdate();
            }
        }

        [Browsable(false), Description("其展现矩形"), Category("布局")]
        public virtual Rectangle DisplayRectangle
        {
            get
            {
                return new Rectangle(this.Location, this.Size); 
            }
        }

        /// <summary>
        /// 转化为客户区坐标
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public Point PointToClient(Point point)
        {
            if (this.pOwner == null) return new Point(-1, -1);
            Forms.IOwnerNC pOwnerNC = this.pOwner as Forms.IOwnerNC;
            if (pOwnerNC != null) return pOwnerNC.PointToClientNC(point);
            return this.pOwner.PointToClient(point);
            //Control ctr = this.Parent;
            //if (ctr == null) return new Point(-1,-1);
            //return ctr.PointToClient(point);
        }

        /// <summary>
        /// 转化为屏幕坐标
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public Point PointToScreen(Point point)
        {
            if (this.pOwner == null) return new Point(-1, -1);
            Forms.IOwnerNC pOwnerNC = this.pOwner as Forms.IOwnerNC;
            if (pOwnerNC != null) return pOwnerNC.PointToScreenNC(point);
            return this.pOwner.PointToScreen(point);
            //Control ctr = this.Parent;
            //if (ctr == null) return new Point(-1, -1);
            //return  ctr.PointToScreen(point);
        }

        /// <summary>
        /// 自身所处的状态（激活、按下、不可用、正常）
        /// </summary>
        [Browsable(false), Description("自身所处的状态（激活、按下、不可用、正常）"), Category("状态")]
        public virtual BaseItemState eBaseItemState
        {
            get
            {
                if (!this.Enabled) return BaseItemState.eDisabled;
                if (this.m_MouseDown) return BaseItemState.ePressed;
                if (this.m_MouseEnter && this.DisplayRectangle.Contains(this.PointToClient(Form.MousePosition))) return BaseItemState.eHot;
                return BaseItemState.eNormal;
            }
        }
        [Browsable(false), Description("修改自身所处的状态后是否刷新（SetBaseItemState）"), Category("属性")]
        protected virtual bool RefreshBaseItemState
        {
            get { return false; } 
        }

        /// <summary>
        /// 刷新自身
        /// </summary>
        public virtual void Refresh()
        {
            if (this.pOwner == null) return;
            //if (!pBaseItemOwner.ItemsRectangle.Contains(this.DisplayRectangle)) return;
            this.pOwner.Invalidate(this.DisplayRectangle);
        }

        /// <summary>
        /// 刷新指定的矩形区域
        /// </summary>
        /// <param name="rectangle"></param>
        public virtual void Invalidate(Rectangle rectangle)
        {
            if (this.pOwner == null) return;
            //if (!pBaseItemOwner.ItemsRectangle.Contains(this.DisplayRectangle)) return;
            this.pOwner.Invalidate(rectangle);
        }
        #endregion

        #region IBaseItem2
        bool m_Checked = false;
        [Browsable(true), DefaultValue(false), Description("选中"), Category("状态")]
        public virtual bool Checked
        {
            get { return m_Checked; }
            set
            {
                if (m_Checked == value) return;
                m_Checked = value;
                this.OnCheckedChanged(new EventArgs());
            }
        }

        [Browsable(false), Description("自身是否溢出于其所在容器的子项展现矩形"), Category("状态")]
        public virtual bool Overflow
        {
            get
            {
                switch (((IViewDepend)this).eViewDependStyle)
                {
                    case ViewDependStyle.eInOwnerItemsRectangle:
                        if (this.pBaseItemOwner == null) return true;
                        return !this.pBaseItemOwner.ItemsRectangle.Contains(this.DisplayRectangle);
                    case ViewDependStyle.eInOwnerDisplayRectangle:
                        if (this.pOwner == null) return true;
                        return !this.pOwner.DisplayRectangle.Contains(this.DisplayRectangle);
                    case ViewDependStyle.eUnrestraint:
                        return false;
                    default:
                        return true;
                }
                //if (this.pBaseItemOwner == null) return true;
                //return !pBaseItemOwner.ItemsRectangle.Contains(this.DisplayRectangle);
            }
        }

        bool m_LockWith = false;
        [Browsable(true), DefaultValue(false), Description("自身的宽度是否被锁定，如被锁定其宽度将不会被自动拉伸"), Category("布局")]
        public virtual bool LockWith
        {
            get { return this.m_LockWith; }
            set { this.m_LockWith = value; }
        }

        bool m_LockHeight = false;
        [Browsable(true), DefaultValue(false), Description("自身的高度是否被锁定，如被锁定其高度将不会被自动拉伸"), Category("布局")]
        public virtual bool LockHeight
        {
            get { return this.m_LockHeight; }
            set { this.m_LockHeight = value; }
        }

        private object m_Tag;
        [Browsable(true), DefaultValue(""), TypeConverter(typeof(StringConverter)), Description("用来携带附加信息"), Category("数据")]
        public object Tag
        {
            get { return m_Tag; }
            set { m_Tag = value; }
        }

        [Browsable(false), Description("获取其依托的父控件"), Category("关联")]
        public Control Parent
        {
            get { return this.TryGetDependControl(); }
        }
        #endregion

        #region IBaseItemProperty
        [Browsable(false), Description("自身所属状态"), Category("属性")]
        BaseItemStyle IBaseItemProperty.eBaseItemStyle
        {
            get { return BaseItemStyle.eIndependentBaseItem; }
        }

        [Browsable(false), Description("获取其依附项（如果，为独立项依附项为其自身）"), Category("关联")]
        IBaseItem3 IBaseItemProperty.DependObject
        {
            get { return this; }
        }
        #endregion

        #region IBaseItem3
        /// <summary>
        /// 只有 是 IBaseBarItem 的下一级成员  才会被认可
        /// </summary>
        [Browsable(false), Description("标识其是否为基础工具条（IBaseBarItem）里的下一级成员"), Category("成员标识")]
        public bool IsBaseBarItem
        {
            get
            {
                return this.pOwner is IBaseBarItem;
                //IBaseBarItem pBaseBarItem = this.pOwner as IBaseBarItem;
                //if (pBaseBarItem == null) return false;
                //return true;
            }
        }

        /// <summary>
        /// 只有 是 IContextPopupPanelItem 的下一级成员  才会被认可
        /// </summary>
        [Browsable(false), Description("标识其是否为弹出菜单面板（IContextPopupPanelItem）里的下一级成员，不代表其上级是BasePopup"), Category("成员标识")]
        public bool IsPopupItem
        {
            get
            {
                return this.pOwner is IContextPopupPanelItem;
                //IContextPopupPanelItem pContextPopupPanelItem = this.pOwner as IContextPopupPanelItem;
                //if (pContextPopupPanelItem == null) return false;
                //return true;
            }
        }

        /// <summary>
        /// 只有 是 BasePopup 的下下一级成员  才会被认可
        /// </summary>
        [Browsable(false), Description("标识其是否为弹出菜单（BasePopup）里的下一级成员，便是其上级的上级（this.pOwner.pOwner）是BasePopup"), Category("成员标识")]
        public bool IsBasePopupItem
        {
            get
            {
                return this.pOwner == null ? false : this.pOwner.pOwner is BasePopup;
            }
        }

        /// <summary>
        /// 只要 是 BasePopup 的成员  都将被认可
        /// </summary>
        [Browsable(false), Description("标识其是否为弹出菜单（BasePopup）里的成员"), Category("成员标识")]
        public bool IsDependBasePopup
        {
            get
            {
                return IsDependBasePopup_DG(this.pOwner);
            }
        }
        private bool IsDependBasePopup_DG(IOwner owner)
        {
            if (owner == null) return false;
            if (owner is BasePopup) return true;
            //
            return this.IsDependBasePopup_DG(owner.pOwner);
        }

        /// <summary>
        /// 尝试获取其所在的BasePopup
        /// </summary>
        /// <returns></returns>
        public BasePopup TryGetDependBasePopup()
        {
            return this.TryGetDependBasePopup_DG(this.pOwner);
        }
        private BasePopup TryGetDependBasePopup_DG(IOwner owner)
        {
            if (owner == null) return null;
            //
            BasePopup basePopup = owner as BasePopup;
            if (basePopup == null) return this.TryGetDependBasePopup_DG(owner.pOwner);
            return basePopup;
        }

        bool m_AutoGetFocus = false;
        [Browsable(true), DefaultValue(false), Description("是否允许自动获取焦点"), Category("属性")]
        public bool AutoGetFocus
        {
            get { return m_AutoGetFocus; }
            set { m_AutoGetFocus = value; }
        }

        /// <summary>
        /// 设置焦点
        /// </summary>
        /// <returns></returns>
        public bool Focus()
        {
            Control ctr = this.TryGetDependControl();
            if (ctr == null || !ctr.CanFocus) return false;
            if (!ctr.Focused && !ctr.Focus()) return false;
            //
            IMessageChain pMessageChain = ctr as IMessageChain;
            if (pMessageChain == null) return false;
            //
            if (!((View.ISetViewItemHelper)this).SetViewParameterStyle(ViewParameterStyle.eFocused))
            {
                pMessageChain.SendMessage(new MessageInfo(this, MessageStyle.eMSLostFocus, this));
            }
            //
            return true;
        }
        #endregion

        #region IBaseItem4
        /// <summary>
        /// 获取其所在的顶部 IOwner
        /// </summary>
        /// <returns></returns>
        public virtual IOwner GetTopOwner()
        {
            return this.GetTopOwner_DG(this.pOwner);
        }
        private IOwner GetTopOwner_DG(IOwner owner)
        {
            if (owner == null) return null;
            if (owner.pOwner == null) return owner;
            //
            return this.GetTopOwner_DG(owner.pOwner);
        }

        /// <summary>
        /// 获取其所在的控件实体
        /// </summary>
        /// <returns></returns>
        public Control TryGetDependControl()
        {
            return this.TryGetDependControl_DG(this.pOwner);
        }
        private Control TryGetDependControl_DG(IOwner owner)
        {
            if (owner == null) return null;
            //
            Control control = owner as Control;
            if (control == null) return this.TryGetDependControl_DG(owner.pOwner);
            return control;
        }

        /// <summary>
        /// 获取其所在窗体对象
        /// </summary>
        /// <returns></returns>
        public Form TryGetDependParentForm()
        {
            Form form = this.TryGetDependParentForm_DG(this.TryGetDependControl());
            return form != null ? form : this.TryGetDependParentForm_DG(this.GetTopOwner() as Control);
        }
        private Form TryGetDependParentForm_DG(Control control)
        {
            if (control == null) return null;
            //
            Form form = control as Form;
            if (form == null) return this.TryGetDependParentForm_DG(control.Parent); 
            return form;
        }

        public IRibbonControl TryGetDependRibbonControl()
        {
            return this.TryGetDependRibbonControl_DG(this.pOwner);
        }
        private IRibbonControl TryGetDependRibbonControl_DG(IOwner owner)
        {
            if (owner == null) return null;
            //
            IRibbonControl ribbonControl = owner as IRibbonControl;
            if (ribbonControl == null) return this.TryGetDependRibbonControl_DG(owner.pOwner);
            return ribbonControl;
        }

        /// <summary>
        /// 界面有效更新，即大面积刷新，主要在有子项被移除后使用，系统自助调用
        /// </summary>
        public void UIUpdate()
        {
            //if (this.Overflow) return;
            ////
            //IBaseItemOwner pBaseItemOwner = this.TryGetDependControl() as IBaseItemOwner;
            //if (pBaseItemOwner != null)
            //{
            //    //Rectangle rectangle = pBaseItemOwner.ItemsRectangle;
            //    //pBaseItemOwner.Invalidate(Rectangle.FromLTRB(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom));
            //    pBaseItemOwner.Invalidate(pBaseItemOwner.ItemsRectangle);
            //}
            //Control ctr = this.TryGetDependControl();
            //if (ctr != null) ctr.Invalidate(new Rectangle(0, 0, ctr.Width, ctr.Height));
            if (this.pOwner == null) return;
            this.pOwner.Refresh();
        }
        #endregion

        #region IBaseItem5
        /// <summary>
        /// 是集合子项才可以使用
        /// </summary>
        /// <returns></returns>
        public int TryGetIndex()
        {
            ICollectionItem pCollectionItem = this.pOwner as ICollectionItem;
            if (pCollectionItem == null) return -1;
            //
            return pCollectionItem.BaseItems.IndexOf(this);
        }

        /// <summary>
        /// 是集合子项才可以使用
        /// </summary>
        /// <returns></returns>
        public virtual bool RemoveSelf()
        {
            ICollectionItem pCollectionItem = this.pOwner as ICollectionItem;
            if (pCollectionItem == null || 
                !pCollectionItem.BaseItems.Contains(this)) return false;
            //
            pCollectionItem.BaseItems.Remove(this);
            //
            return true;
        }

        /// <summary>
        /// 是集合子项才可以使用
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool MoveTo(int index)
        {
            ICollectionItem pCollectionItem = this.pOwner as ICollectionItem;
            if (pCollectionItem == null) return false;
            //
            return pCollectionItem.BaseItems.ExchangeItem(pCollectionItem.BaseItems.IndexOf(this), index);
        }
        #endregion

        #region IViewDepend
        [Browsable(false), DefaultValue(typeof(ViewDependStyle), "eInOwnerItemsRectangle"), Description("指示该对象在它父类的约束条件"), Category("属性")]
        ViewDependStyle IViewDepend.eViewDependStyle
        {
            get { return  ViewDependStyle.eInOwnerItemsRectangle; }
        }
        #endregion

        #region IBaseItem6
        private Size m_MinimumSize = new Size(10, 10);
        [Browsable(false), DefaultValue(typeof(Size), "10,10"), Description("使用视图溢出后最小显示约束"), Category("显示")]
        public virtual Size MinimumSize
        {
            get { return m_MinimumSize; }
            set { m_MinimumSize = value; }
        }

        [Browsable(false), Description("自身视图范围是否溢出于其所在容器的子项展现矩形"), Category("状态")]
        public bool ViewOverflow
        {
            get
            {
                return this.TryGetViewRectangle(out this.m_ViewRectangle);
            }
        }

        Rectangle m_ViewRectangle = Rectangle.Empty;
        [Browsable(false), Description("其当前视图区矩形"), Category("布局")]
        public Rectangle ViewRectangle
        {
            get { return m_ViewRectangle; }
        }

        public bool TryGetViewRectangle(out Rectangle viewRectangle)
        {
            viewRectangle = Rectangle.Empty;
            switch (((IViewDepend)this).eViewDependStyle)
            {
                case ViewDependStyle.eInOwnerItemsRectangle:
                    if (this.pBaseItemOwner != null) viewRectangle = Rectangle.Intersect(this.pBaseItemOwner.ItemsViewRectangle, this.DisplayRectangle);
                    break;
                case ViewDependStyle.eInOwnerDisplayRectangle:
                    if (this.pOwner != null) viewRectangle = Rectangle.Intersect(this.pOwner.DisplayRectangle, this.DisplayRectangle);
                    break;
                case ViewDependStyle.eUnrestraint:
                    viewRectangle = this.DisplayRectangle;
                    break;
                default:
                    break;
            }
            return viewRectangle.IsEmpty || viewRectangle.Width < MinimumSize.Width || viewRectangle.Height < MinimumSize.Height;
        }

        private bool m_UsingViewOverflow = true;
        [Browsable(true), DefaultValue(true), Description("是否使用视图溢出"), Category("属性")]
        public bool UsingViewOverflow
        {
            get { return this.m_UsingViewOverflow && !this.IsBaseBarItem; }
            set { m_UsingViewOverflow = value; }
        }

        public bool GetOverflowState()
        {
            return this.UsingViewOverflow ? this.ViewOverflow : this.Overflow;
        }

        public bool Contains(Point point)
        {
            if (!this.Visible) return false;
            //
            if (this.UsingViewOverflow)
            {
                if (this.ViewOverflow) return false;
                return this.ViewRectangle.Contains(point);
            }
            else
            {
                if (this.Overflow) return false;
                return this.DisplayRectangle.Contains(point);
            }
        }
        #endregion

        #region IBaseItem7
        Padding m_Margin = new Padding(0);
        [Browsable(true), DefaultValue(typeof(Padding), "0,0,0,0"), Description("其在父容器中布局的周间距"), Category("布局")]
        public virtual Padding Margin
        {
            get { return m_Margin; }
            set { m_Margin = value; }
        }

        HAlignmentStyle m_eHAlignmentStyle = HAlignmentStyle.eLeft;
        [Browsable(true), DefaultValue(typeof(HAlignmentStyle), "eLeft"), Description("其在父容器中水平方向上布局的方式"), Category("布局")]
        public virtual HAlignmentStyle eHAlignmentStyle
        {
            get { return m_eHAlignmentStyle; }
            set { m_eHAlignmentStyle = value; }
        }

        VAlignmentStyle m_eVAlignmentStyle = VAlignmentStyle.eTop;
        [Browsable(true), DefaultValue(typeof(VAlignmentStyle), "eTop"), Description("其在父容器中竖直方向上布局的方式"), Category("布局")]
        public virtual VAlignmentStyle eVAlignmentStyle
        {
            get { return m_eVAlignmentStyle; }
            set { m_eVAlignmentStyle = value; }
        }
        #endregion

        #region ITranslation
        public void Translation(int iX, int iY)
        {
            this.m_Location.X += iX;
            this.m_Location.Y += iY;
        }

        public void Translation(Point fromPoint, Point toPoint)
        {
            this.m_Location.X += toPoint.X - fromPoint.X;
            this.m_Location.Y += toPoint.Y - fromPoint.Y;
        }
        #endregion

        #region IBaseItemEvent
        [Browsable(true), Description("绘制触发"), Category("外观")]
        public event PaintEventHandler Paint;
        [Browsable(true), Description("选中属性改变后触发"), Category("属性已更改")]
        public event EventHandler CheckedChanged;
        [Browsable(true), Description("尺寸属性改变后触发"), Category("属性已更改")]
        public event EventHandler SizeChanged;
        [Browsable(true), Description("坐落属性改变后触发"), Category("属性已更改")]
        public event EventHandler LocationChanged;
        [Browsable(true), Description("可用属性改变后触发"), Category("属性已更改")]
        public event EventHandler EnabledChanged;
        [Browsable(true), Description("可见属性改变后触发"), Category("属性已更改")]
        public event EventHandler VisibleChanged;
        [Browsable(true), Description("文本属性改变后触发"), Category("属性已更改")]
        public event EventHandler TextChanged;
        [Browsable(true), Description("键盘按下时触发"), Category("键盘")]
        public event KeyEventHandler KeyDown;
        [Browsable(true), Description("键盘弹起时触发"), Category("键盘")]
        public event KeyEventHandler KeyUp;
        [Browsable(true), Description("键盘按下时触发"), Category("键盘")]
        public event KeyPressEventHandler KeyPress;
        [Browsable(true), Description("鼠标进入其可见部分时触发"), Category("鼠标")]
        public event EventHandler MouseEnter;
        [Browsable(true), Description("鼠标离开其可见部分时触发"), Category("鼠标")]
        public event EventHandler MouseLeave;
        [Browsable(true), Description("鼠标在其可见部分按下时触发"), Category("鼠标")]
        public event MouseEventHandler MouseDown;
        [Browsable(true), Description("鼠标在其可见部分移动时触发"), Category("鼠标")]
        public event MouseEventHandler MouseMove;
        [Browsable(true), Description("鼠标在其可见部分弹起时触发"), Category("鼠标")]
        public event MouseEventHandler MouseUp;
        [Browsable(true), Description("鼠标在其可见部分单机时触发"), Category("鼠标")]
        public event MouseEventHandler MouseClick;
        [Browsable(true), Description("鼠标在其可见部分双击时触发"), Category("鼠标")]
        public event MouseEventHandler MouseDoubleClick;
        #endregion

        #region IBaseItemEvent2
        [Browsable(true), Description("单击事件"), Category("点击")]
        public event EventHandler Click;
        #endregion

        #region IEventHelper
        /// <summary>
        /// 获取事件的状态 即 是否存在
        /// </summary>
        /// <param name="strEventName">事件名</param>
        /// <returns></returns>
        public EventStateStyle GetEventState(string strEventName)
        {
            switch (strEventName)
            {
                case "Paint":
                    return this.Paint != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "SizeChanged":
                    return this.SizeChanged != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "LocationChanged":
                    return this.LocationChanged != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "CheckedChanged":
                    return this.CheckedChanged != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "EnabledChanged":
                    return this.EnabledChanged != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "VisibleChanged":
                    return this.VisibleChanged != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "TextChanged":
                    return this.TextChanged != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "KeyDown":
                    return this.KeyDown != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "KeyPress":
                    return this.KeyPress != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "KeyUp":
                    return this.KeyUp != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "MouseEnter":
                    return this.MouseEnter != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "MouseLeave":
                    return this.MouseLeave != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "MouseDown":
                    return this.MouseDown != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "MouseMove":
                    return this.MouseMove != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "MouseUp":
                    return this.MouseUp != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "MouseClick":
                    return this.MouseClick != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "MouseDoubleClick":
                    return this.MouseDoubleClick != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                default:
                    return this.GetEventStateSupplement(strEventName);
            }
        }
        protected virtual EventStateStyle GetEventStateSupplement(string strEventName)
        {
            return EventStateStyle.eNotExist;
        }

        /// <summary>
        /// 关联最基础的事件
        /// </summary>
        /// <param name="strEventName">事件名</param>
        /// <param name="e"></param>
        /// <returns></returns>
        public bool RelationEvent(string strEventName, EventArgs e)
        {
            switch (strEventName)
            {
                case "Paint":
                    if (this.Paint != null) { this.Paint(this, e as PaintEventArgs); }
                    return true;
                case "SizeChanged":
                    if (this.SizeChanged != null) { this.SizeChanged(this, e as EventArgs); }
                    return true;
                case "LocationChanged":
                    if (this.LocationChanged != null) { this.LocationChanged(this, e as EventArgs); }
                    return true;
                case "CheckedChanged":
                    if (this.CheckedChanged != null) { this.CheckedChanged(this, e as EventArgs); }
                    return true;
                case "EnabledChanged":
                    if (this.EnabledChanged != null) { this.EnabledChanged(this, e as EventArgs); }
                    return true;
                case "VisibleChanged":
                    if (this.VisibleChanged != null) { this.VisibleChanged(this, e as EventArgs); }
                    return true;
                case "TextChanged":
                    if (this.TextChanged != null) { this.TextChanged(this, e as EventArgs); }
                    return true;
                case "KeyDown":
                    if (this.KeyDown != null) { this.KeyDown(this, e as KeyEventArgs); }
                    return true;
                case "KeyPress":
                    if (this.KeyPress != null) { this.KeyPress(this, e as KeyPressEventArgs); }
                    return true;
                case "KeyUp":
                    if (this.KeyUp != null) { this.KeyUp(this, e as KeyEventArgs); }
                    return true;
                case "MouseEnter":
                    if (this.MouseEnter != null) { this.MouseEnter(this, e as EventArgs); }
                    return true;
                case "MouseLeave":
                    if (this.MouseLeave != null) { this.MouseLeave(this, e as EventArgs); }
                    return true;
                case "MouseDown":
                    if (this.MouseDown != null) { this.MouseDown(this, e as MouseEventArgs); }
                    return true;
                case "MouseMove":
                    if (this.MouseMove != null) { this.MouseMove(this, e as MouseEventArgs); }
                    return true;
                case "MouseUp":
                    if (this.MouseUp != null) { this.MouseUp(this, e as MouseEventArgs); }
                    return true;
                case "MouseClick":
                    if (this.MouseClick != null) { this.MouseClick(this, e as MouseEventArgs); }
                    return true;
                case "MouseDoubleClick":
                    if (this.MouseDoubleClick != null) { this.MouseDoubleClick(this, e as MouseEventArgs); }
                    return true;
                default:
                    return this.RelationEventSupplement(strEventName, e);
            }
        }
        protected virtual bool RelationEventSupplement(string strEventName, EventArgs e)
        {
            return false;
        }
        #endregion

        #region IOwner
        private IOwner m_pOwner;
        [Browsable(false), Description("获取其拥有者"), Category("关联")]
        public IOwner pOwner
        {
            get { return m_pOwner; }
        }
        #endregion

        #region IBaseItemOwner
        [Browsable(false), Description("其子项展现矩形"), Category("布局")]
        public virtual Rectangle ItemsRectangle
        {
            get { return this.DisplayRectangle; }
        }

        [Browsable(false), Description("其子视图项展现矩形"), Category("布局")]
        public virtual Rectangle ItemsViewRectangle
        {
            get
            {
                IBaseItemOwner pBaseItemOwner = pOwner as IBaseItemOwner;
                return pBaseItemOwner == null ? this.ItemsRectangle : Rectangle.Intersect(pBaseItemOwner.ItemsViewRectangle, this.ItemsRectangle);
            }
        }

        [Browsable(false), Description("获取其子项拥有者"), Category("关联")]
        public IBaseItemOwner pBaseItemOwner
        {
            get { return pOwner as IBaseItemOwner; }
        }
        #endregion

        #region IBaseItemOwner2
        [Browsable(false), Description("取消其子项的绘制事件"), Category("状态")]
        public virtual bool CancelItemsDrawEvent
        {
            get
            {
                return false;
            }
        }

        [Browsable(false), Description("取消其子项除绘制事件以外的所有事件"), Category("状态")]
        public virtual bool CancelItemsEvent
        {
            get
            {
                return false;
            }
        }
        #endregion

        #region IDesignHelper
        public virtual bool DesignMouseClickRectangleContainsEx(Point point) 
        {
            return (this.Visible && this.DesignMouseClickRectangle.Contains(point));
        }

        [Browsable(false), Description("设计视图状态下鼠标点击选中的有效的矩形区域"), Category("设计")]
        public virtual Rectangle DesignMouseClickRectangle
        {
            get
            {
                Rectangle rectangle = this.DisplayRectangle;
                int iW = rectangle.Width / 4;
                iW = iW > 20 ? 20 : iW;
                int iH = rectangle.Height / 4;
                iH = iH > 20 ? 20 : iH;
                return new Rectangle(rectangle.Left + iW, rectangle.Top + iH, rectangle.Width - 2 * iW, rectangle.Height - 2 * iH);
            }
        }

        [Browsable(false), Description("设计视图状态下鼠标点击选中绘制的外框矩形"), Category("设计")]
        public virtual Rectangle DesignMouseSelectedRectangle//用于绘制
        {
            get
            {
                Rectangle rectangle = this.DisplayRectangle;
                return new Rectangle(rectangle.Left, rectangle.Top, rectangle.Width - 1, rectangle.Height - 1);
            }
        }
        #endregion

        #region ISetOwnerHelper
        bool ISetOwnerHelper.SetOwner(IOwner owner)
        {
            if (this.m_pOwner == owner) return false;
            //
            //
            //
            this.m_pOwner = owner;
            ((IReset)this).Reset();
            this.OnBaseItemOwnerChanged();
            //
            //
            //
            return true;
        }
        #endregion

        #region ISetBaseItemHelper
        bool ISetBaseItemHelper.SetLocation(Point point)
        {
            if (this.m_Location == point) return false;
            this.m_Location = point;
            return true;
        }

        bool ISetBaseItemHelper.SetLocation(int x, int y)
        {
            #region 刷新（已抛弃）
            //if (this.m_Location.X == x && this.m_Location.Y == y) return;
            //this.m_Location = new Point(x, y);
            //this.OnLocationChanged(new EventArgs());
            //this.UIUpdate();
            #endregion
            //
            if (this.m_Location.X == x && this.m_Location.Y == y) return false;
            this.m_Location.X = x;
            this.m_Location.Y = y;
            return true;
        }

        bool ISetBaseItemHelper.SetDisplayRectangle(Rectangle rectangle)
        {
            #region 刷新（已抛弃）
            //bool bLocationChanged = false;
            //if (this.m_Location != rectangle.Location)
            //{
            //    this.m_Location = rectangle.Location; 
            //    this.OnLocationChanged(new EventArgs());
            //    bLocationChanged = true;
            //}
            ////
            //bool bSizeChanged = false;
            //if (!this.LockWith && this.m_Size.Width != rectangle.Size.Width)
            //{
            //    this.m_Size = new Size(rectangle.Size.Width, this.m_Size.Height);
            //    bSizeChanged = true;
            //}
            //if (!this.LockHeight && this.m_Size.Height != rectangle.Size.Height)
            //{
            //    this.m_Size = new Size(this.m_Size.Width, rectangle.Size.Height);
            //    bSizeChanged = true;
            //}
            //if(bSizeChanged) this.OnSizeChanged(new EventArgs());
            ////
            //if (bLocationChanged || bSizeChanged) this.UIUpdate();
            #endregion
            //
            bool bOk = false;
            if (this.m_Location != rectangle.Location)
            {
                this.m_Location = rectangle.Location;
                bOk = true;
            }
            if (!this.LockWith && !this.LockHeight) 
            {
                if (this.m_Size != rectangle.Size)
                {
                    this.m_Size = rectangle.Size;
                    bOk = true;
                }
            }
            else if (!this.LockWith)
            {
                if (this.m_Size.Width != rectangle.Size.Width)
                {
                    this.m_Size = new Size(rectangle.Size.Width, this.m_Size.Height);
                    bOk = true;
                }
            }
            else if (!this.LockHeight)
            {
                if (this.m_Size.Height != rectangle.Size.Height)
                {
                    this.m_Size = new Size(this.m_Size.Width, rectangle.Size.Height);
                    bOk = true;
                }
            }
            return bOk;
        }

        bool ISetBaseItemHelper.SetDisplayRectangle(int x, int y, int width, int height)
        {
            #region 刷新（已抛弃）
            //bool bLocationChanged = false;
            //if (this.m_Location.X != x || this.m_Location.Y != y)
            //{
            //    this.m_Location = new Point(x, y);
            //    this.OnLocationChanged(new EventArgs());
            //    bLocationChanged = true;
            //}
            ////
            //bool bSizeChanged = false;
            //if (!this.LockWith && this.m_Size.Width != width)
            //{
            //    this.m_Size = new Size(width, this.m_Size.Height);
            //    bSizeChanged = true;
            //}
            //if (!this.LockHeight && this.m_Size.Height != height)
            //{
            //    this.m_Size = new Size(this.m_Size.Width, height);
            //    bSizeChanged = true;
            //}
            //if (bSizeChanged) this.OnSizeChanged(new EventArgs());
            ////
            //if (bLocationChanged || bSizeChanged) this.UIUpdate();
            #endregion
            //
            bool bOk = false;
            if (this.m_Location.X != x || this.m_Location.Y != y)
            {
                this.m_Location = new Point(x, y);
                bOk = true;
            }
            //
            if (!this.LockWith && !this.LockHeight)
            {
                if (this.m_Size.Width != width || this.m_Size.Height != height)
                {
                    this.m_Size = new Size(width, height);
                    bOk = true;
                }
            }
            else if (!this.LockWith)
            {
                if (this.m_Size.Width != width)
                {
                    this.m_Size = new Size(width, this.m_Size.Height);
                    bOk = true;
                }
            }
            else if (!this.LockHeight)
            {
                if (this.m_Size.Height != height)
                {
                    this.m_Size = new Size(this.m_Size.Width, height);
                    bOk = true;
                }
            }
            return bOk;
        }
        #endregion

        #region IViewItem
        ViewParameterStyle m_eViewParameterStyle = ViewParameterStyle.eNone;
        /// <summary>
        /// 用来记录自身情况的参数
        /// </summary>
        [Browsable(false), DefaultValue(typeof(ViewParameterStyle), "eNone"), Description("视图伴随参数"), Category("属性")]
        ViewParameterStyle View.IViewItem.eViewParameterStyle
        {
            get { return m_eViewParameterStyle; }
        }
        #endregion

        #region ISetViewItemHelper
        bool View.ISetViewItemHelper.SetViewParameterStyle(ViewParameterStyle viewParameterStyle)
        {
            if (this.m_eViewParameterStyle == viewParameterStyle) return false;
            this.m_eViewParameterStyle = viewParameterStyle;
            return true;
        }

        bool View.ISetViewItemHelper.SetViewItemDisplayRectangle(Rectangle rectangle)
        {
            #region 有事件（已抛弃）
            //if (this.m_Location != rectangle.Location)
            //{
            //    this.m_Location = rectangle.Location;
            //    this.OnLocationChanged(new EventArgs());
            //}
            ////
            //bool bSizeChanged = false;
            //if (!this.LockWith && this.m_Size.Width != rectangle.Size.Width)
            //{
            //    this.m_Size = new Size(rectangle.Size.Width, this.m_Size.Height);
            //    bSizeChanged = true;
            //}
            //if (!this.LockHeight && this.m_Size.Height != rectangle.Size.Height)
            //{
            //    this.m_Size = new Size(this.m_Size.Width, rectangle.Size.Height);
            //    bSizeChanged = true;
            //}
            //if (bSizeChanged) this.OnSizeChanged(new EventArgs());
            #endregion
            //
            bool bOk = false;
            if (this.m_Location != rectangle.Location)
            {
                this.m_Location = rectangle.Location;
                bOk = true;
            }
            if (!this.LockWith && !this.LockHeight)
            {
                if (this.m_Size != rectangle.Size)
                {
                    this.m_Size = rectangle.Size;
                    bOk = true;
                }
            }
            else if (!this.LockWith)
            {
                if (this.m_Size.Width != rectangle.Size.Width)
                {
                    this.m_Size = new Size(rectangle.Size.Width, this.m_Size.Height);
                    bOk = true;
                }
            }
            else if (!this.LockHeight)
            {
                if (this.m_Size.Height != rectangle.Size.Height)
                {
                    this.m_Size = new Size(this.m_Size.Width, rectangle.Size.Height);
                    bOk = true;
                }
            }
            return bOk;
        }
        #endregion

        #region IReset
        void IReset.Reset()
        {
            this.m_eViewParameterStyle = ViewParameterStyle.eNone;
            this.m_MouseDown = false;
            this.m_MouseEnter = false;
        }
        #endregion

        #region IMessagePermeate
        bool IMessagePermeate.PermeateCancelEvent(MessageStyle eMessageStyle)
        {
            return false;
        }
        #endregion

        #region IMessageChain
        private bool m_MouseDown = false;
        private bool m_MouseEnter = false;
        void IMessageChain.SendMessage(MessageInfo messageInfo)
        {
            //注入当前对象
            messageInfo.Now = this;
            //
            switch (messageInfo.eMessageStyle)
            {
                case MessageStyle.eMSViewInfo:
                    this.MSViewInfo(messageInfo);
                    break;
                    //
                case MessageStyle.eMSPaint:
                    this.MSPaint(messageInfo);
                    break;
                    //
                case MessageStyle.eMSKeyDown:
                    this.MSKeyDown(messageInfo);
                    break;
                case MessageStyle.eMSKeyUp:
                    this.MSKeyUp(messageInfo);
                    break;
                case MessageStyle.eMSKeyPress:
                    this.MSKeyPress(messageInfo);
                    break;
                    //
                case MessageStyle.eMSMouseWheel:
                    this.MSMouseWheel(messageInfo);
                    break;
                    //
                case MessageStyle.eMSMouseDown:
                    this.MSMouseDown(messageInfo);
                    break;
                case MessageStyle.eMSMouseUp:
                    this.MSMouseUp(messageInfo);
                    break;
                case MessageStyle.eMSMouseMove:
                    this.MSMouseMove(messageInfo);
                    break;
                case MessageStyle.eMSMouseClick:
                    this.MSMouseClick(messageInfo);
                    break;
                case MessageStyle.eMSMouseDoubleClick:
                    this.MSMouseDoubleClick(messageInfo);
                    break;
                case MessageStyle.eMSMouseEnter:
                    this.MSMouseEnter(messageInfo);
                    break;
                case MessageStyle.eMSMouseLeave:
                    this.MSMouseLeave(messageInfo);
                    break;
                    //
                case MessageStyle.eMSEnabledChanged:
                    this.MSEnabledChanged(messageInfo);
                    break;
                case MessageStyle.eMSVisibleChanged:
                    this.MSVisibleChanged(messageInfo);
                    break;
                case MessageStyle.eMSLostFocus:
                    this.MSLostFocus(messageInfo);
                    break;
                default:
                    this.MessageMonitor(messageInfo);
                    break;
            }
        }
        private void MSViewInfo(MessageInfo messageInfo)//永久传递的消息不受CancelItemsDrawEvent属性控制，一般情况下总是发生在MSPaint之前
        {
            ViewInfo viewInfo = messageInfo.MessageParameter as ViewInfo;
            if (viewInfo != null)
            {
                //植入监听
                this.MessageMonitor
                    (
                    new MessageInfo
                        (
                        this, 
                        MessageStyle.eMSViewInfo, 
                        new ViewInfo(viewInfo.Visible && this.Visible, viewInfo.Enabled && this.Enabled, viewInfo.Overflow && this.Overflow)
                        )
                    );
            }
        }
        private void MSPaint(MessageInfo messageInfo)//传递绘制消息受CancelItemsDrawEvent属性控制，一般情况下总是发生在MSViewInfo之后，所关联的事件顺序发生，不受CancelPreEvent属性影响
        {
            if (this.CancelItemsDrawEvent) return;
            //
            if (this.Visible && !this.GetOverflowState())
            {
                PaintEventArgs paintEventArgs = messageInfo.MessageParameter as PaintEventArgs;
                if (paintEventArgs != null)
                {
                    Rectangle clipRectangle = Rectangle.Intersect
                        (
                        this.UsingViewOverflow ? this.ViewRectangle : this.DisplayRectangle, 
                        paintEventArgs.ClipRectangle
                        );
                    paintEventArgs.Graphics.SetClip(clipRectangle);
                    //关联对应事件
                    this.OnPaint(new PaintEventArgs(paintEventArgs.Graphics, clipRectangle));
                    //
                    //
                    //
                    //植入监听
                    this.MessageMonitor(messageInfo);
                    //
                    paintEventArgs.Graphics.SetClip(paintEventArgs.ClipRectangle);
                }
            }
        }
        private void MSKeyDown(MessageInfo messageInfo)//传递键盘按下消息受CancelItemsDrawEvent属性控制，当该对象有焦点时触发事件停止消息传递，否则继续传送向下消息，所关联的事件逆序发生，受CancelPreEvent属性影响
        {
            if (this.CancelItemsEvent) return;
            //
            if (this.Visible && this.Enabled && !this.GetOverflowState())
            {
                if (this.m_eViewParameterStyle != ViewParameterStyle.eFocused)
                {
                    //植入监听
                    this.MessageMonitor(messageInfo);
                }
                else
                {
                    //关联对应事件
                    if (!messageInfo.CancelPreEvent)
                    {
                        this.OnKeyDown(messageInfo.MessageParameter as KeyEventArgs);
                        //取消回传事件
                        messageInfo.CancelPreEvent = true;
                    }
                }
            }
        }
        private void MSKeyUp(MessageInfo messageInfo)//传递键盘弹起消息受CancelItemsDrawEvent属性控制，当该对象有焦点时触发事件停止消息传递，否则继续传送向下消息，所关联的事件逆序发生，受CancelPreEvent属性影响
        {
            if (this.CancelItemsEvent) return;
            //
            if (this.Visible && this.Enabled && !this.GetOverflowState())
            {
                if (this.m_eViewParameterStyle != ViewParameterStyle.eFocused)
                {
                    //植入监听
                    this.MessageMonitor(messageInfo);
                }
                else
                {
                    //关联对应事件
                    if (!messageInfo.CancelPreEvent)
                    {
                        this.OnKeyUp(messageInfo.MessageParameter as KeyEventArgs);
                        //取消回传事件
                        messageInfo.CancelPreEvent = true;
                    }
                }
            }
        }
        private void MSKeyPress(MessageInfo messageInfo)//传递键盘输入消息受CancelItemsDrawEvent属性控制，当该对象有焦点时触发事件停止消息传递，否则继续传送向下消息，所关联的事件逆序发生，受CancelPreEvent属性影响
        {
            if (this.CancelItemsEvent) return;
            //
            if (this.Visible && this.Enabled && !this.GetOverflowState())
            {
                if (this.m_eViewParameterStyle != ViewParameterStyle.eFocused)
                {
                    //植入监听
                    this.MessageMonitor(messageInfo);
                }
                else
                {
                    //关联对应事件
                    if (!messageInfo.CancelPreEvent)
                    {
                        this.OnKeyPress(messageInfo.MessageParameter as KeyPressEventArgs);
                        //取消回传事件
                        messageInfo.CancelPreEvent = true;
                    }
                }
            }
        }
        private void MSMouseWheel(MessageInfo messageInfo)//传递鼠标滚动消息受CancelItemsDrawEvent属性控制，当该对象有焦点时触发事件停止消息传递，否则继续传送向下消息，所关联的事件逆序发生，受CancelPreEvent属性影响
        {
            if (this.CancelItemsEvent) return;
            //
            if (this.Visible && this.Enabled && !this.GetOverflowState())
            {
                //植入监听
                this.MessageMonitor(messageInfo);
                //
                if (this.m_eViewParameterStyle == ViewParameterStyle.eFocused)
                {
                    //关联对应事件
                    if (!messageInfo.CancelPreEvent)
                    {
                        this.OnMouseWheel(messageInfo.MessageParameter as MouseEventArgs);
                        //取消回传事件
                        messageInfo.CancelPreEvent = true;
                    }
                }
            }
        }
        private void MSMouseDown(MessageInfo messageInfo)//传递鼠标按下消息受CancelItemsDrawEvent属性控制，所关联的事件逆序发生，使用消息参数中的CancelPreEvent属性，已取消回传所引发的父类事件
        {
            if (this.CancelItemsEvent) return;
            //
            if (this.Visible && this.Enabled)
            {
                MouseEventArgs mouseEventArgs = messageInfo.MessageParameter as MouseEventArgs;
                if (mouseEventArgs != null && this.Contains(mouseEventArgs.Location))
                {
                    this.m_MouseDown = true;
                    //植入监听
                    this.MessageMonitor(messageInfo);//new MessageInfo(this, messageInfo.eMessageStyle, messageInfo.MessageParameter)
                    //关联对应事件
                    if (!messageInfo.CancelPreEvent)
                    {
                        //设置焦点
                        if (this.AutoGetFocus) this.SetFocus(messageInfo.Sender as Control);
                        //启动事件
                        this.OnMouseDown(mouseEventArgs);
                        //取消回传事件
                        messageInfo.CancelPreEvent = this.CancelPreEvent_MouseDown;
                    }
                    else if (((IMessagePermeate)this).PermeateCancelEvent(messageInfo.eMessageStyle))
                    {
                        //启动事件
                        this.OnMouseDown(mouseEventArgs);
                    }
                    if (this.RefreshBaseItemState) this.Refresh();
                }
            }
        }
        private void SetFocus(Control ctr)
        {
            IBaseItemProperty pBaseItemProperty = (IBaseItemProperty)this;
            switch (pBaseItemProperty.eBaseItemStyle)
            {
                case BaseItemStyle.eComponentBaseItem:
                    this.SetFocus_DG(pBaseItemProperty);
                    break;
                default:
                    if (ctr == null) ctr = TryGetDependControl();
                    if (ctr == null || !ctr.CanFocus) return;
                    if (!ctr.Focused && !ctr.Focus()) return;
                    //
                    IMessageChain pMessageChain = ctr as IMessageChain;
                    if (pMessageChain == null) return;
                    //
                    if (((View.ISetViewItemHelper)this).SetViewParameterStyle(ViewParameterStyle.eFocused))
                    {
                        //Console.WriteLine(this.m_eViewParameterStyle);
                        pMessageChain.SendMessage(new MessageInfo(this, MessageStyle.eMSLostFocus, this));
                    }
                    break;
            }
        }
        private void SetFocus_DG(IBaseItemProperty pBaseItemProperty)
        {
            if (pBaseItemProperty == null) return;
            //
            switch (pBaseItemProperty.eBaseItemStyle)
            {
                case BaseItemStyle.eComponentBaseItem:
                    this.SetFocus_DG(pBaseItemProperty.DependObject as IBaseItemProperty);
                    break;
                case BaseItemStyle.eIndependentBaseItem:
                case BaseItemStyle.eIndependentBaseItemControl:
                case BaseItemStyle.eIndependentBasePopup:
                default:
                    IBaseItem3 pBaseItem3 = pBaseItemProperty as IBaseItem3;
                    if (pBaseItem3 != null) pBaseItem3.Focus();
                    break;
            }
        }
        private void MSMouseUp(MessageInfo messageInfo)//传递鼠标弹起消息受CancelItemsDrawEvent属性控制，所关联的事件逆序发生并派生出Click事件，使用消息参数中的CancelPreEvent属性，已取消回传所引发的父类事件
        {
            if (this.CancelItemsEvent) return;
            //
            if (this.m_MouseDown)
            {
                this.m_MouseDown = false;
                if (this.RefreshBaseItemState) this.Refresh();
                //植入监听
                if (this.Visible && this.Enabled)
                {
                    this.MessageMonitor(messageInfo);//new MessageInfo(this, messageInfo.eMessageStyle, messageInfo.MessageParameter)
                }
                //关联对应事件
                if (!messageInfo.CancelPreEvent)
                {
                    //启动事件
                    this.OnClick(messageInfo.MessageParameter as EventArgs);
                    this.OnMouseUp(messageInfo.MessageParameter as MouseEventArgs);
                    //取消回传事件
                    messageInfo.CancelPreEvent = this.CancelPreEvent_MouseUp;
                }
                else if (((IMessagePermeate)this).PermeateCancelEvent(messageInfo.eMessageStyle))
                {
                    //启动事件
                    this.OnClick(messageInfo.MessageParameter as EventArgs);
                    this.OnMouseUp(messageInfo.MessageParameter as MouseEventArgs);
                }
            }
        }
        private void MSMouseMove(MessageInfo messageInfo)//传递鼠标移动消息受CancelItemsDrawEvent属性控制（期间会派生出MSMouseEnter和MSMouseLeave消息），所关联的事件逆序发生，使用消息参数中的CancelPreEvent属性，已取消回传所引发的父类事件
        {
            //if (this.m_MouseDown) return;
            //
            if (this.CancelItemsEvent) return;
            //
            if (!this.Visible || !this.Enabled)
            {
                this.m_MouseEnter = false;
                return;
            }
            //
            MouseEventArgs mouseEventArgs = messageInfo.MessageParameter as MouseEventArgs;
            bool bMouseMove = Form.MouseButtons == MouseButtons.None ? this.Contains(mouseEventArgs.Location) : this.m_MouseDown;
            if (mouseEventArgs != null && bMouseMove)
            {
                if (!this.m_MouseEnter)
                {
                    this.m_MouseEnter = true;
                    if (this.RefreshBaseItemState) this.Refresh();
                    //植入监听
                    this.MessageMonitor(new MessageInfo(this, MessageStyle.eMSMouseEnter, messageInfo.MessageParameter));
                    //关联对应事件
                    this.OnMouseEnter(mouseEventArgs);
                }
                //植入监听
                this.MessageMonitor(messageInfo);//new MessageInfo(this, messageInfo.eMessageStyle, messageInfo.MessageParameter)
                //关联对应事件
                if (!messageInfo.CancelPreEvent)
                {
                    //启动事件
                    this.OnMouseMove(mouseEventArgs);
                    //取消回传事件
                    messageInfo.CancelPreEvent = this.CancelPreEvent_MouseMove;
                }
                else if (((IMessagePermeate)this).PermeateCancelEvent(messageInfo.eMessageStyle))
                {
                    //启动事件
                    this.OnMouseMove(mouseEventArgs);
                }
            }
            else
            {
                if (this.m_MouseEnter)
                {
                    this.m_MouseEnter = false;
                    if (this.RefreshBaseItemState) this.Refresh();
                    //植入监听
                    this.MessageMonitor(new MessageInfo(this, MessageStyle.eMSMouseLeave, messageInfo.MessageParameter));
                    //关联对应事件
                    this.OnMouseLeave(mouseEventArgs);
                }
            }
        }
        private void MSMouseEnter(MessageInfo messageInfo)//传递绘制消息不受CancelItemsDrawEvent属性控制，如果信息来自依附的控件不会向后发送消息，来源于MSMouseMove则在MSMouseMove中激发事件并向下传递，不受CancelPreEvent属性影响
        {
            //if (!this.m_MouseEnter)
            //{
            //    this.m_MouseEnter = true;
            //    if (this.RefreshBaseItemState) { this.Refresh(); }
            //}
        }
        private void MSMouseLeave(MessageInfo messageInfo)//传递绘制消息不受CancelItemsDrawEvent属性控制，依据m_MouseEnter判断是否激发事件并向下传递，不受CancelPreEvent属性影响
        {
            if (this.m_MouseEnter)
            {
                this.m_MouseEnter = false;
                //
                if (this.RefreshBaseItemState) { this.Refresh(); }
                if (this.CancelItemsEvent) return;
                //植入监听
                this.MessageMonitor(messageInfo);//new MessageInfo(this, messageInfo.eMessageStyle, messageInfo.MessageParameter)
                //关联对应事件
                this.OnMouseLeave(messageInfo.MessageParameter as MouseEventArgs);
            }
        }
        private void MSMouseClick(MessageInfo messageInfo)//传递鼠标单击消息受CancelItemsDrawEvent属性控制，所关联的事件逆序发生，使用消息参数中的CancelPreEvent属性，已取消回传所引发的父类事件
        {
            if (this.CancelItemsEvent) return;
            //
            if (!this.m_MouseDown) return;
            //
            if (this.Visible && this.Enabled)
            {
                MouseEventArgs mouseEventArgs = messageInfo.MessageParameter as MouseEventArgs;
                if (mouseEventArgs != null && this.Contains(mouseEventArgs.Location))
                {
                    //解散Popup过滤
                    this.DismissPopupFilter(mouseEventArgs);
                    //植入监听
                    this.MessageMonitor(messageInfo);//new MessageInfo(this, messageInfo.eMessageStyle, messageInfo.MessageParameter)
                    //关联对应事件
                    if (!messageInfo.CancelPreEvent)
                    {
                        //启动事件
                        this.OnMouseClick(mouseEventArgs);
                        //取消回传事件
                        messageInfo.CancelPreEvent = true;
                    }
                    else if (((IMessagePermeate)this).PermeateCancelEvent(messageInfo.eMessageStyle))
                    {
                        //启动事件
                        this.OnMouseClick(mouseEventArgs);
                    }
                }
            }
        }
        internal protected virtual void DismissPopupFilter(MouseEventArgs e)
        {
            IDismissPopupObject pDismissPopupObject = (IDismissPopupObject)this;
            if (pDismissPopupObject.eDismissPopupStyle == DismissPopupStyle.eIsDependBasePopup)
            {
                if (pDismissPopupObject.DismissTriggerRectangle.Contains(e.Location))
                {
                    BasePopup basePopup = this.TryGetDependBasePopup();
                    if (basePopup != null) basePopup.DismissPopup();
                }
            }
            else if (pDismissPopupObject.eDismissPopupStyle == DismissPopupStyle.eIsBasePopupItem)
            {
                if (pDismissPopupObject.DismissTriggerRectangle.Contains(e.Location) && this.pOwner != null)
                {
                    BasePopup basePopup = this.pOwner.pOwner as BasePopup;
                    if (basePopup != null) basePopup.DismissPopup();
                }
            }
        }
        private void MSMouseDoubleClick(MessageInfo messageInfo)//传递鼠标双击消息受CancelItemsDrawEvent属性控制，所关联的事件逆序发生，使用消息参数中的CancelPreEvent属性，已取消回传所引发的父类事件
        {
            if (this.CancelItemsEvent) return;
            //
            if (!this.m_MouseDown) return;
            //
            if (this.Visible && this.Enabled)
            {
                MouseEventArgs mouseEventArgs = messageInfo.MessageParameter as MouseEventArgs;
                if (mouseEventArgs != null && this.Contains(mouseEventArgs.Location))
                {
                    //植入监听
                    this.MessageMonitor(messageInfo);//new MessageInfo(this, messageInfo.eMessageStyle, messageInfo.MessageParameter)
                    //关联对应事件
                    if (!messageInfo.CancelPreEvent)
                    {
                        //启动事件
                        this.OnMouseDoubleClick(mouseEventArgs);
                        //取消回传事件
                        messageInfo.CancelPreEvent = true;
                    }
                    else if (((IMessagePermeate)this).PermeateCancelEvent(messageInfo.eMessageStyle))
                    {
                        //启动事件
                        this.OnMouseDoubleClick(mouseEventArgs);
                    }
                }
            }
        }
        private void MSEnabledChanged(MessageInfo messageInfo)//传递鼠标双击消息受CancelItemsDrawEvent属性控制，所关联的事件逆序发生，不受CancelPreEvent属性影响
        {
            this.m_MouseDown = false;
            this.m_MouseEnter = false;
            //
            if (this.CancelItemsEvent) return;
            //
            BoolValueChangedEventArgs boolValueChangedEventArgs = messageInfo.MessageParameter as BoolValueChangedEventArgs;
            if (boolValueChangedEventArgs != null &&  boolValueChangedEventArgs.NewValue != this.Enabled)
            {
                //植入监听
                this.MessageMonitor(messageInfo);
            }
            //关联对应事件
            if (messageInfo.Sender == this) this.OnEnabledChanged(messageInfo.MessageParameter as EventArgs);
        }
        private void MSVisibleChanged(MessageInfo messageInfo)//传递鼠标双击消息受CancelItemsDrawEvent属性控制，所关联的事件逆序发生，不受CancelPreEvent属性影响
        {
            this.m_MouseDown = false;
            this.m_MouseEnter = false;
            //
            if (this.CancelItemsEvent) return;
            //
            BoolValueChangedEventArgs boolValueChangedEventArgs = messageInfo.MessageParameter as BoolValueChangedEventArgs;
            if (boolValueChangedEventArgs != null && boolValueChangedEventArgs.NewValue != this.Visible)
            {
                //植入监听
                this.MessageMonitor(messageInfo);
            }
            //关联对应事件
            if (messageInfo.Sender == this) this.OnVisibleChanged(messageInfo.MessageParameter as EventArgs);
        }
        private void MSLostFocus(MessageInfo messageInfo)
        {
            if (messageInfo.MessageParameter == this) return;
            //
            if (((View.ISetViewItemHelper)this).SetViewParameterStyle(ViewParameterStyle.eNone)) return;
            //
            //Console.WriteLine(this.ToString() + this.m_eViewParameterStyle);
            this.MessageMonitor(messageInfo);
        }

        /// <summary>
        /// 消息监控者
        /// </summary>
        /// <param name="messageInfo"></param>
        protected virtual void MessageMonitor(MessageInfo messageInfo)
        {

        }

        protected virtual bool CancelPreEvent_MouseDown { get { return true; } }
        protected virtual bool CancelPreEvent_MouseMove { get { return true; } }
        protected virtual bool CancelPreEvent_MouseUp { get { return true; } }
        #endregion

        #region IDismissPopupObject
        [Browsable(false), DefaultValue(typeof(DismissPopupStyle), "eNoDismiss"), Description("解散popup的类型"), Category("状态")]
        DismissPopupStyle IDismissPopupObject.eDismissPopupStyle
        {
            get { return DismissPopupStyle.eNoDismiss; } 
        }

        [Browsable(false), Description("解散popup的触发区"), Category("属性")]
        Rectangle IDismissPopupObject.DismissTriggerRectangle
        {
            get { return this.DisplayRectangle; }
        }
        #endregion

        /// <summary>
        /// 测量应有的尺寸 很多时候返回 DisplayRectangle
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        public abstract Size MeasureSize(Graphics g);

        //

        /// <summary>
        /// 重设拥有者后调用
        /// </summary>
        protected virtual void OnBaseItemOwnerChanged()
        {

        }

        protected virtual void OnPaint(PaintEventArgs e)
        {
            //this.OnLayout(e);
            this.OnDraw(e);
            //
            if (this.Paint != null) { this.Paint(this, e); }
        }

        ///// <summary>
        ///// 书写布局函数
        ///// </summary>
        ///// <param name="e"></param>
        //protected virtual void OnLayout(PaintEventArgs e)
        //{ }

        /// <summary>
        /// 书写绘制函数
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnDraw(PaintEventArgs e)
        { }

        protected virtual void OnCheckedChanged(EventArgs e)
        {
            if (this.CheckedChanged != null) { this.CheckedChanged(this, e); }
        }

        protected virtual void OnSizeChanged(EventArgs e)
        {
            if (this.SizeChanged != null) { this.SizeChanged(this, e); }
        }

        protected virtual void OnLocationChanged(EventArgs e)
        {
            if (this.LocationChanged != null) { this.LocationChanged(this, e); }
        }

        protected virtual void OnEnabledChanged(EventArgs e)
        {
            if (this.EnabledChanged != null) { this.EnabledChanged(this, e); }
        }

        protected virtual void OnVisibleChanged(EventArgs e)
        {
            if (this.VisibleChanged != null) { this.VisibleChanged(this, e); }
        }

        protected virtual void OnTextChanged(EventArgs e)
        {
            if (this.TextChanged != null) { this.TextChanged(this, e); }
        }

        protected virtual void OnKeyDown(KeyEventArgs e)
        {
            if (this.KeyDown != null) { this.KeyDown(this, e); }
        }

        protected virtual void OnKeyPress(KeyPressEventArgs e)
        {
            if (this.KeyPress != null) { this.KeyPress(this, e); }
        }

        protected virtual void OnKeyUp(KeyEventArgs e)
        {
            if (this.KeyUp != null) { this.KeyUp(this, e); }
        }

        protected virtual void OnMouseWheel(MouseEventArgs e)
        {
            //if (this.KeyUp != null) { this.KeyUp(this, e); }
        }

        protected virtual void OnMouseEnter(EventArgs e)
        {
            if (this.MouseEnter != null) { this.MouseEnter(this, e);  }
        }

        protected virtual void OnMouseLeave(EventArgs e)
        {
            if (this.MouseLeave != null) { this.MouseLeave(this, e); }
        }

        protected virtual void OnMouseDown(MouseEventArgs e)
        {
            if (this.MouseDown != null) { this.MouseDown(this, e); }
        }

        protected virtual void OnMouseMove(MouseEventArgs e)
        {
            if (this.MouseMove != null) { this.MouseMove(this, e); }
        }

        protected virtual void OnMouseUp(MouseEventArgs e)
        {
            if (this.MouseUp != null) { this.MouseUp(this, e); }
        }

        protected virtual void OnMouseClick(MouseEventArgs e)
        {
            if (this.MouseClick != null) { this.MouseClick(this, e); }
        }

        protected virtual void OnMouseDoubleClick(MouseEventArgs e)
        {
            if (this.MouseDoubleClick != null) { this.MouseDoubleClick(this, e); }
        }

        protected virtual void OnClick(EventArgs e)
        {
            if (this.Click != null) { this.Click(this, e); }
        }
        
    }
}
