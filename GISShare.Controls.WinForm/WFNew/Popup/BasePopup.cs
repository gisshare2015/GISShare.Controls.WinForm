using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    public abstract class BasePopup : GISShare.Controls.WinForm.Popup.BasePopup, 
        IReset,
        IBaseItem3, 
        IBasePopup2, IBasePopupEvent, IBasePopupEvent2, IBaseItemEvent,
        IOwner, ISetOwnerHelper, IObjectDesignHelper, IPopupObjectDesignHelper//, ICloneable
    {
        public BasePopup()
            : base()
        {
            //base.AllowTransparency = true;
            //base.BackColor = System.Drawing.Color.Transparent;
        }

        #region WFNew.IPopupObjectDesignHelper
        void WFNew.IPopupObjectDesignHelper.ShowPopup()
        {
            this.Show(3, 3);
        }

        void WFNew.IPopupObjectDesignHelper.ClosePopup()
        {
            this.Close();
        }
        #endregion

        public EventStateStyle GetEventState(string strEventName)
        {
            switch (strEventName)
            {
                case "Paint":
                    return EventStateStyle.eUnknown;
                case "MouseEnter":
                    return EventStateStyle.eUnknown;
                case "MouseLeave":
                    return EventStateStyle.eUnknown;
                case "SizeChanged":
                    return EventStateStyle.eUnknown;
                case "LocationChanged":
                    return EventStateStyle.eUnknown;
                case "CheckedChanged":
                    return this.CheckedChanged != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "EnabledChanged":
                    return EventStateStyle.eUnknown;
                case "VisibleChanged":
                    return EventStateStyle.eUnknown;
                case "MouseDown":
                    return EventStateStyle.eUsed;
                case "MouseMove":
                    return EventStateStyle.eUnknown;
                case "MouseUp":
                    return EventStateStyle.eUsed;
                case "MouseClick":
                    return EventStateStyle.eUsed;
                case "MouseDoubleClick":
                    return EventStateStyle.eUsed;
                default:
                    return this.GetEventStateSupplement(strEventName);
            }
        }
        protected virtual EventStateStyle GetEventStateSupplement(string strEventName)
        {
            return EventStateStyle.eNotExist;
        }

        public bool RelationEvent(string strEventName, EventArgs e)
        {
            switch (strEventName)
            {
                case "Paint":
                    this.OnPaint(e as PaintEventArgs);
                    return true;
                case "MouseEnter":
                    this.OnMouseEnter(e as EventArgs);
                    return true;
                case "MouseLeave":
                    this.OnMouseLeave(e as EventArgs);
                    return true;
                case "SizeChanged":
                    this.OnSizeChanged(e as EventArgs);
                    return true;
                case "LocationChanged":
                    this.OnLocationChanged(e as EventArgs);
                    return true;
                case "CheckedChanged":
                    if (this.CheckedChanged != null) { this.CheckedChanged(this, e as EventArgs); }
                    return true;
                case "EnabledChanged":
                    this.OnEnabledChanged(e as EventArgs);
                    return true;
                case "VisibleChanged":
                    this.OnVisibleChanged(e as EventArgs);
                    return true;
                case "MouseDown":
                    this.OnMouseDown(e as MouseEventArgs);
                    return true;
                case "MouseMove":
                    this.OnMouseMove(e as MouseEventArgs);
                    return true;
                case "MouseUp":
                    this.OnMouseUp(e as MouseEventArgs);
                    return true;
                case "MouseClick":
                    this.OnMouseClick(e as MouseEventArgs);
                    return true;
                case "MouseDoubleClick":
                    this.OnMouseDoubleClick(e as MouseEventArgs);
                    return true;
                case "KeyDown":
                    this.OnKeyDown(e as KeyEventArgs);
                    return true;
                case "KeyPress":
                    this.OnKeyPress(e as KeyPressEventArgs);
                    return true;
                case "KeyUp":
                    this.OnKeyUp(e as KeyEventArgs);
                    return true;
                default:
                    return this.RelationEventSupplement(strEventName, e);
            }
        }
        protected virtual bool RelationEventSupplement(string strEventName, EventArgs e)
        {
            return false;
        }

        public override GISShare.Controls.WinForm.Popup.BasePopup TryGetParentBasePopup()
        {
            if (this.pOwner == null) return null;
            GISShare.Controls.WinForm.Popup.BasePopup basePopup = this.pOwner as GISShare.Controls.WinForm.Popup.BasePopup;
            if (basePopup != null) return basePopup;
            //
            return this.TryGetParentBasePopup_DG(this.pOwner.pOwner);
        }
        private GISShare.Controls.WinForm.Popup.BasePopup TryGetParentBasePopup_DG(IOwner owner) 
        {
            if (owner == null) return null;
            GISShare.Controls.WinForm.Popup.BasePopup basePopup = owner as GISShare.Controls.WinForm.Popup.BasePopup;
            if (basePopup != null) return basePopup;
            //
            return this.TryGetParentBasePopup_DG(owner.pOwner);
        }

        #region IBaseItem
        RenderStyle m_eRenderStyle = RenderStyle.eSystem;
        [Browsable(true), DefaultValue(typeof(RenderStyle), "eSystem"), Description("渲染类型"), Category("外观")]
        public virtual RenderStyle eRenderStyle
        {
            get { return m_eRenderStyle; }
            set { m_eRenderStyle = value; }
        }

        private BaseItemState m_eBaseItemState = BaseItemState.eNormal;
        protected virtual void SetBaseItemState(BaseItemState baseItemState)
        {
            if (this.m_eBaseItemState == baseItemState) return;
            this.m_eBaseItemState = baseItemState;
        }
        protected virtual void SetBaseItemStateEx(BaseItemState baseItemState)
        {
            if (this.m_eBaseItemState == baseItemState) return;
            this.m_eBaseItemState = baseItemState;
            this.Refresh();
        }
        [Browsable(false), Description("自身所处的状态（激活、按下、不可用、正常）"), Category("状态")]
        public virtual BaseItemState eBaseItemState
        {
            get
            {
                return m_eBaseItemState;
            }
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
            get { return false; }
        }

        [Browsable(false), Description("自身的高度是否被锁定，如被锁定其高度将不会被自动拉伸"), Category("布局")]
        public bool LockHeight
        {
            get { return true; }
        }

        [Browsable(false), Description("自身的宽度是否被锁定，如被锁定其宽度将不会被自动拉伸"), Category("布局")]
        public bool LockWith
        {
            get { return true; }
        }

        public abstract object Clone();
        #endregion

        #region IBaseItemProperty
        [Browsable(false), Description("自身所属状态"), Category("属性")]
        BaseItemStyle IBaseItemProperty.eBaseItemStyle
        {
            get { return BaseItemStyle.eIndependentBasePopup; }
        }

        [Browsable(false), Description("获取其依附项（如果，为独立项依附项为其自身）"), Category("关联")]
        IBaseItem3 IBaseItemProperty.DependObject
        {
            get { return this; }
        }
        #endregion

        #region IBaseItem3
        [Browsable(false), Description("标识其是否为基础工具条（IBaseBarItem）里的下一级成员"), Category("成员标识")]
        public bool IsBaseBarItem
        {
            get
            {
                return false;
            }
        }

        [Browsable(false), Description("标识其是否为弹出菜单面板（IContextPopupPanelItem）里的下一级成员，不代表其上级是BasePopup"), Category("成员标识")]
        public bool IsPopupItem
        {
            get
            {
                return false;
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
                return false;
            }
        }

        [Browsable(false), Description("标识其是否为弹出菜单（BasePopup）里的成员"), Category("成员标识")]
        public bool IsDependBasePopup
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// 尝试获取其所在的BasePopup
        /// </summary>
        /// <returns></returns>
        public BasePopup TryGetDependBasePopup()
        {
            return this;
        }

        bool m_AutoGetFocus = false;
        [Browsable(true), DefaultValue(false), Description("是否允许自动获取焦点"), Category("属性")]
        public bool AutoGetFocus
        {
            get { return m_AutoGetFocus; }
            set { m_AutoGetFocus = value; }
        }
        #endregion

        #region IBaseItemEvent
        [Browsable(true), Description("选中属性改变后触发"), Category("属性已更改")]
        public event EventHandler CheckedChanged;
        #endregion

        #region IOwner
        private IOwner m_pOwner;
        [Browsable(false), Description("获取其拥有者"), Category("关联")]
        public IOwner pOwner
        {
            get { return m_pOwner; }
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
            //
            //
            //
            return true;
        }
        #endregion

        #region IReset
        void IReset.Reset() { }
        #endregion

        #region 隐藏并修改 Show 函数
        /// <summary>
        /// 用来过滤是否显示 Popup
        /// </summary>
        /// <returns></returns>
        protected virtual bool Filtration()
        {
            return true;
        }

        public new void Show()
        {
            if (!this.Filtration()) return;
            //
            base.Show();
        }

        public new void Show(Point screenLocation)
        {
            if (!this.Filtration()) return;
            //
            base.Show(screenLocation);
        }

        public new void Show(Control control, Point position)
        {
            if (!this.Filtration()) return;
            //
            base.Show(control, position);
        }

        public new void Show(int x, int y)
        {
            if (!this.Filtration()) return;
            //
            base.Show(x, y);
        }

        public new void Show(Control control, int x, int y)
        {
            if (!this.Filtration()) return;
            //
            base.Show(control, x, y);
        }

        public new void Show(Point position, ToolStripDropDownDirection direction)
        {
            if (!this.Filtration()) return;
            //
            base.Show(position, direction);
        }

        public new void Show(Control control, Point position, ToolStripDropDownDirection direction)
        {
            if (!this.Filtration()) return;
            //
            base.Show(control, position, direction);
        }
        #endregion

        #region IBasePopup2
        [Browsable(false), Description("是否使用圆角"), Category("圆角")]
        public virtual bool UseRadius
        {
            get
            {
                if (
                    (this.LeftTopRadius < 0 || this.RightTopRadius < 0 || this.LeftBottomRadius < 0 || this.RightBottomRadius < 0) ||
                    (this.LeftTopRadius == 0 && this.RightTopRadius == 0 && this.LeftBottomRadius == 0 && this.RightBottomRadius == 0)
                    )
                    return false;
                else 
                    return true;
            }
        }

        [Browsable(false), Description("左顶部圆角值"), Category("圆角")]
        public abstract int LeftTopRadius { get; }

        [Browsable(false), Description("右顶部圆角值"), Category("圆角")]
        public abstract int RightTopRadius { get; }

        [Browsable(false), Description("左底部圆角值"), Category("圆角")]
        public abstract int LeftBottomRadius { get; }

        [Browsable(false), Description("右底部圆角值"), Category("圆角")]
        public abstract int RightBottomRadius { get; }
        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            //
            this.OnDraw(e);
        }

        protected virtual void OnDraw(PaintEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderCustomizePopup(
                new GISShare.Controls.WinForm.ObjectRenderEventArgs(e.Graphics, this, this.DisplayRectangle));
        }

        //

        protected virtual void OnCheckedChanged(EventArgs e)
        {
            if (this.CheckedChanged != null) { this.CheckedChanged(this, e); }
        }
    }
}
