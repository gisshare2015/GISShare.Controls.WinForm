using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    [ToolboxItem(false), DefaultEvent("MouseClick"), Category("GISShare.Controls.WinForm.WFNew")]
    public abstract class BaseItemControlCC : System.Windows.Forms.ContainerControl,
        IRecordItem, ISetRecordItemHelper,
        IBaseItemProperty,
        ICategoryItem,
        IBaseItem, IBaseItem2, IBaseItem3, IBaseItem4, IBaseItem5,
        IOwner, IBaseItemOwner, IBaseItemOwner2, ISetOwnerHelper,
        IBaseItemEvent,
        IMessageChain,
        IMessagePermeate,
        IEventHelper, IObjectDesignHelper
    {
        public BaseItemControlCC()
        {
            this.SetStyle(ControlStyles.Opaque, false);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();
        }

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

        #region ICategoryItem
        string m_Category = "默认";
        [Browsable(false), DefaultValue("默认"), Description("用来描述该对象所属的分类"), Category("描述")]
        public string Category
        {
            get { return m_Category; }
            set { m_Category = value; }
        }
        #endregion

        #region IBaseItem
        [Browsable(true), DefaultValue(typeof(Padding), "0"), Description("尺寸"), Category("布局")]
        public new virtual Padding Padding
        {
            get { return base.Padding; }
            set { base.Padding = value; }
        }

        RenderStyle m_eRenderStyle = RenderStyle.eSystem;
        [Browsable(true), DefaultValue(typeof(RenderStyle), "eSystem"), Description("渲染类型"), Category("外观")]
        public virtual RenderStyle eRenderStyle
        {
            get { return m_eRenderStyle; }
            set { m_eRenderStyle = value; }
        }

        [Browsable(false), Description("自身所处的状态（激活、按下、不可用、正常）"), Category("状态")]
        public virtual BaseItemState eBaseItemState
        {
            get
            {
                if (!this.Enabled) return BaseItemState.eDisabled;
                if (this.m_MouseDown) return BaseItemState.ePressed;
                if (this.m_MouseEnter) return BaseItemState.eHot;
                return BaseItemState.eNormal;
            }
        }
        [Browsable(false), Description("修改自身所处的状态后是否刷新（SetBaseItemState）"), Category("属性")]
        protected virtual bool RefreshBaseItemState { get { return false; } }
        #endregion

        #region IBaseItem2
        [Browsable(true), Description("选中属性改变后触发"), Category("属性已更改")]
        public event EventHandler CheckedChanged;

        [Browsable(false), Description("自身是否溢出于其所在容器的子项展现矩形"), Category("状态")]
        public bool Overflow
        { get { return false; } }

        [Browsable(false), Description("自身的高度是否被锁定，如被锁定其高度将不会被自动拉伸"), Category("布局")]
        public abstract bool LockHeight { get; }

        [Browsable(false), Description("自身的宽度是否被锁定，如被锁定其宽度将不会被自动拉伸"), Category("布局")]
        public abstract bool LockWith { get; }

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

        public abstract object Clone();
        #endregion

        #region IBaseItemProperty
        [Browsable(false), Description("自身所属状态"), Category("属性")]
        BaseItemStyle IBaseItemProperty.eBaseItemStyle
        {
            get { return BaseItemStyle.eIndependentBaseItemControl; }
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
            return this.Parent;
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

        /// <summary>
        /// 界面有效更新，即大面积刷新
        /// </summary>
        public void UIUpdate()
        {
            if (this.Parent != null) this.Parent.Refresh();
        }
        #endregion

        #region IBaseItem5
        /// <summary>
        /// 是集合子项才可以使用
        /// </summary>
        /// <returns></returns>
        public int TryGetIndex()
        {
            if (this.Parent == null) return -1;
            //
            return this.Parent.Controls.IndexOf(this);
        }

        /// <summary>
        /// 是集合子项才可以使用
        /// </summary>
        /// <returns></returns>
        public virtual bool RemoveSelf()
        {
            if (this.Parent == null || !this.Parent.Controls.Contains(this)) return false;
            //
            this.Parent.Controls.Remove(this);
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
            if (this.Parent == null || !this.Parent.Controls.Contains(this)) return false;
            //
            this.Parent.Controls.SetChildIndex(this, index);
            //
            return true;
        }
        #endregion

        #region IEventHelper
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
                    base.OnPaint(e as PaintEventArgs);
                    return true;
                case "MouseEnter":
                    base.OnMouseEnter(e as EventArgs);
                    return true;
                case "MouseLeave":
                    base.OnMouseLeave(e as EventArgs);
                    return true;
                case "SizeChanged":
                    base.OnSizeChanged(e as EventArgs);
                    return true;
                case "LocationChanged":
                    base.OnLocationChanged(e as EventArgs);
                    return true;
                case "CheckedChanged":
                    if (this.CheckedChanged != null) { this.CheckedChanged(this, e as EventArgs); }
                    return true;
                case "EnabledChanged":
                    base.OnEnabledChanged(e as EventArgs);
                    return true;
                case "VisibleChanged":
                    base.OnVisibleChanged(e as EventArgs);
                    return true;
                case "MouseDown":
                    base.OnMouseDown(e as MouseEventArgs);
                    return true;
                case "MouseMove":
                    base.OnMouseMove(e as MouseEventArgs);
                    return true;
                case "MouseUp":
                    base.OnMouseUp(e as MouseEventArgs);
                    return true;
                case "MouseClick":
                    base.OnMouseClick(e as MouseEventArgs);
                    return true;
                case "MouseDoubleClick":
                    base.OnMouseDoubleClick(e as MouseEventArgs);
                    return true;
                case "KeyDown":
                    base.OnKeyDown(e as KeyEventArgs);
                    return true;
                case "KeyPress":
                    base.OnKeyPress(e as KeyPressEventArgs);
                    return true;
                case "KeyUp":
                    base.OnKeyUp(e as KeyEventArgs);
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

        #region IOwner
        private IOwner m_pOwner;
        [Browsable(false), Description("获取其拥有者"), Category("关联")]
        public IOwner pOwner
        {
            get { return m_pOwner; }
        }

        void IOwner.Refresh()
        {
            this.Invalidate(new Rectangle(0, 0, this.Width, this.Height));
        }
        #endregion

        #region IBaseItemOwner
        [Browsable(false), Description("其子项展现矩形"), Category("布局")]
        public virtual Rectangle ItemsRectangle
        {
            get { return this.DisplayRectangle; }
        }

        [Browsable(false), Description("其子视图项展现矩形"), Category("布局")]
        public Rectangle ItemsViewRectangle
        {
            get
            {
                return this.ItemsRectangle;
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

        #region IMessagePermeate
        bool IMessagePermeate.PermeateCancelEvent(MessageStyle eMessageStyle)
        {
            return false;
        }
        #endregion

        #region 嵌入消息链条
        protected override void OnPaint(PaintEventArgs e)
        {
            if (this is IUICollectionItem)
            {
                //发送消息
                ((IMessageChain)this).SendMessage(new MessageInfo(this, MessageStyle.eMSViewInfo, new ViewInfo(this.Visible, this.Enabled, this.Overflow)));
            }
            //发送消息
            ((IMessageChain)this).SendMessage(new MessageInfo(this, MessageStyle.eMSPaint, e));
            //
            base.OnPaint(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            //发送消息
            ((IMessageChain)this).SendMessage(new MessageInfo(this, MessageStyle.eMSLostFocus, e));
            //
            base.OnLostFocus(e);
        }

        //
        protected override void OnKeyDown(KeyEventArgs e)
        {
            MessageInfo messageInfo = new MessageInfo(this, MessageStyle.eMSKeyDown, e);
            //发送消息
            ((IMessageChain)this).SendMessage(messageInfo);
            //
            if (!messageInfo.CancelPreEvent)
            {
                base.OnKeyDown(e);
            }
            else if (((IMessagePermeate)this).PermeateCancelEvent(messageInfo.eMessageStyle))
            {
                base.OnKeyDown(e);
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            MessageInfo messageInfo = new MessageInfo(this, MessageStyle.eMSKeyUp, e);
            //发送消息
            ((IMessageChain)this).SendMessage(messageInfo);
            //
            if (!messageInfo.CancelPreEvent)
            {
                base.OnKeyUp(e);
            }
            else if (((IMessagePermeate)this).PermeateCancelEvent(messageInfo.eMessageStyle))
            {
                base.OnKeyUp(e);
            }
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            MessageInfo messageInfo = new MessageInfo(this, MessageStyle.eMSKeyPress, e);
            //发送消息
            ((IMessageChain)this).SendMessage(messageInfo);
            //
            if (!messageInfo.CancelPreEvent)
            {
                base.OnKeyPress(e);
            }
            else if (((IMessagePermeate)this).PermeateCancelEvent(messageInfo.eMessageStyle))
            {
                base.OnKeyPress(e);
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            MessageInfo messageInfo = new MessageInfo(this, MessageStyle.eMSMouseWheel, e);
            //发送消息
            ((IMessageChain)this).SendMessage(messageInfo);
            //
            if (!messageInfo.CancelPreEvent)
            {
                base.OnMouseWheel(e);
            }
            else if (((IMessagePermeate)this).PermeateCancelEvent(messageInfo.eMessageStyle))
            {
                base.OnMouseWheel(e);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (this.AutoGetFocus && !this.Focused) this.Focus();
            //
            MessageInfo messageInfo = new MessageInfo(this, MessageStyle.eMSMouseDown, e);
            //发送消息
            ((IMessageChain)this).SendMessage(messageInfo);
            //
            if (!messageInfo.CancelPreEvent)
            {
                base.OnMouseDown(e);
            }
            else if (((IMessagePermeate)this).PermeateCancelEvent(messageInfo.eMessageStyle))
            {
                base.OnMouseDown(e);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            MessageInfo messageInfo = new MessageInfo(this, MessageStyle.eMSMouseUp, e);
            //发送消息
            ((IMessageChain)this).SendMessage(messageInfo);
            //
            if (!messageInfo.CancelPreEvent)
            {
                base.OnMouseUp(e);
            }
            else if (((IMessagePermeate)this).PermeateCancelEvent(messageInfo.eMessageStyle))
            {
                base.OnMouseUp(e);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            MessageInfo messageInfo = new MessageInfo(this, MessageStyle.eMSMouseMove, e);
            //发送消息
            ((IMessageChain)this).SendMessage(messageInfo);
            //
            if (!messageInfo.CancelPreEvent)
            {
                base.OnMouseMove(e);
            }
            else if (((IMessagePermeate)this).PermeateCancelEvent(messageInfo.eMessageStyle))
            {
                base.OnMouseMove(e);
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            MessageInfo messageInfo = new MessageInfo(this, MessageStyle.eMSMouseClick, e);
            //发送消息
            ((IMessageChain)this).SendMessage(messageInfo);
            //
            if (!messageInfo.CancelPreEvent)
            {
                base.OnMouseClick(e);
            }
            else if (((IMessagePermeate)this).PermeateCancelEvent(messageInfo.eMessageStyle))
            {
                base.OnMouseClick(e);
            }
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            MessageInfo messageInfo = new MessageInfo(this, MessageStyle.eMSMouseDoubleClick, e);
            //发送消息
            ((IMessageChain)this).SendMessage(messageInfo);
            //
            if (!messageInfo.CancelPreEvent)
            {
                base.OnMouseDoubleClick(e);
            }
            else if (((IMessagePermeate)this).PermeateCancelEvent(messageInfo.eMessageStyle))
            {
                base.OnMouseDoubleClick(e);
            }
        }
        //

        protected override void OnMouseEnter(EventArgs e)
        {
            //发送消息
            ((IMessageChain)this).SendMessage(new MessageInfo(this, MessageStyle.eMSMouseEnter, e));
            //
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            //发送消息
            ((IMessageChain)this).SendMessage(new MessageInfo(this, MessageStyle.eMSMouseLeave, e));
            //
            base.OnMouseLeave(e);
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            //发送消息
            ((IMessageChain)this).SendMessage(new MessageInfo(this, MessageStyle.eMSEnabledChanged, new BoolValueChangedEventArgs(this.Enabled)));
            //
            base.OnEnabledChanged(e);
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            //发送消息
            ((IMessageChain)this).SendMessage(new MessageInfo(this, MessageStyle.eMSVisibleChanged, new BoolValueChangedEventArgs(this.Visible)));
            //
            base.OnVisibleChanged(e);
        }
        #endregion

        #region IReset
        void IReset.Reset()
        {
            this.m_MouseDown = false;
            this.m_MouseEnter = false;
        }
        #endregion
        
        #region IMessageChain
        void IMessageChain.SendMessage(MessageInfo messageInfo)
        {
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
                case MessageStyle.eMSLostFocus:
                    this.MSLostFocus(messageInfo);
                    break;
                case MessageStyle.eMSKeyDown:
                    this.MSKeyDown(messageInfo);
                    break;
                case MessageStyle.eMSKeyUp:
                    this.MSKeyUp(messageInfo);
                    break;
                case MessageStyle.eMSKeyPress:
                    this.MSKeyPress(messageInfo);
                    break;
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
                default:
                    this.MessageMonitor(messageInfo);
                    break;
            }
        }
        private void MSViewInfo(MessageInfo messageInfo)
        {
            //植入监听
            this.MessageMonitor(messageInfo);
        }
        private void MSPaint(MessageInfo messageInfo)
        {
            //植入监听
            this.MessageMonitor(messageInfo);
        }
        private void MSLostFocus(MessageInfo messageInfo)
        {
            //植入监听
            this.MessageMonitor(messageInfo);
        }
        private void MSKeyDown(MessageInfo messageInfo)
        {
            //植入监听
            this.MessageMonitor(messageInfo);
        }
        private void MSKeyUp(MessageInfo messageInfo)
        {
            //植入监听
            this.MessageMonitor(messageInfo);
        }
        private void MSKeyPress(MessageInfo messageInfo)
        {
            //植入监听
            this.MessageMonitor(messageInfo);
        }
        private void MSMouseWheel(MessageInfo messageInfo)
        {
            //植入监听
            this.MessageMonitor(messageInfo);
        }
        private bool m_MouseDown = false;
        private void MSMouseDown(MessageInfo messageInfo)
        {
            this.m_MouseDown = true;
            if (this.RefreshBaseItemState) this.Refresh();
            //植入监听
            this.MessageMonitor(messageInfo);
        }
        private void MSMouseUp(MessageInfo messageInfo)
        {
            this.m_MouseDown = false;
            MouseEventArgs mouseEventArgs = messageInfo.MessageParameter as MouseEventArgs;
            if (mouseEventArgs == null || !this.DisplayRectangle.Contains(mouseEventArgs.Location))
            {
                this.m_MouseEnter = false;
            }
            if (this.RefreshBaseItemState) this.Refresh();
            //植入监听
            this.MessageMonitor(messageInfo);
        }
        private bool m_MouseEnter = false;
        private void MSMouseMove(MessageInfo messageInfo)
        {
            //if (this.m_MouseDown) return;
            //植入监听
            this.MessageMonitor(messageInfo);
        }
        private void MSMouseEnter(MessageInfo messageInfo)
        {
            if (!this.m_MouseEnter)
            {
                this.m_MouseEnter = true;
                if (this.RefreshBaseItemState) { this.Refresh(); }
            }
        }
        private void MSMouseLeave(MessageInfo messageInfo)
        {
            if (this.m_MouseEnter)
            {
                this.m_MouseEnter = false;
                if (this.RefreshBaseItemState) { this.Refresh(); }
                //植入监听
                this.MessageMonitor(messageInfo);
            }
        }
        private void MSMouseClick(MessageInfo messageInfo)
        {
            //植入监听
            this.MessageMonitor(messageInfo);
        }
        private void MSMouseDoubleClick(MessageInfo messageInfo)
        {
            //植入监听
            this.MessageMonitor(messageInfo);
        }
        private void MSEnabledChanged(MessageInfo messageInfo)
        {
            this.m_MouseDown = false;
            this.m_MouseEnter = false;
            //植入监听
            this.MessageMonitor(messageInfo);
        }
        private void MSVisibleChanged(MessageInfo messageInfo)
        {
            this.m_MouseDown = false;
            this.m_MouseEnter = false;
            //植入监听
            this.MessageMonitor(messageInfo);
        }

        /// <summary>
        /// 用来监听消息
        /// </summary>
        /// <param name="messageInfo"></param>
        protected virtual void MessageMonitor(MessageInfo messageInfo)
        {

        }
        #endregion

        public bool Contains(Point point)
        {
            return this.DisplayRectangle.Contains(point);
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            //
            ISetOwnerHelper pSetOwnerHelper = e.Control as ISetOwnerHelper;
            if (pSetOwnerHelper != null) pSetOwnerHelper.SetOwner(this);
        }

        protected override void OnControlRemoved(ControlEventArgs e)
        {
            base.OnControlRemoved(e);
            //
            ISetOwnerHelper pSetOwnerHelper = e.Control as ISetOwnerHelper;
            if (pSetOwnerHelper != null) pSetOwnerHelper.SetOwner(null);
        }

        //

        protected virtual void OnCheckedChanged(EventArgs e)
        {
            if (this.CheckedChanged != null) { this.CheckedChanged(this, e); }
        }
    }
}
