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
        [Browsable(false), Description("�����ID�ţ���ϵͳ������Ҫ�ڼ�¼�����ļ�ʱʹ�ã�����״̬��������Ч�ģ�"), Category("����")]
        public int RecordID
        {
            get { return m_RecordID; }
        }
        #endregion

        #region ISetRecordItemHelper
        void ISetRecordItemHelper.SetRecordID(int id)//����RecordID����ϵͳ�����ڱ��沼��ʱ���ø����ԣ�
        {
            this.m_RecordID = id;
        }
        #endregion

        #region ICategoryItem
        string m_Category = "Ĭ��";
        [Browsable(false), DefaultValue("Ĭ��"), Description("���������ö��������ķ���"), Category("����")]
        public string Category
        {
            get { return m_Category; }
            set { m_Category = value; }
        }
        #endregion

        #region IBaseItem
        [Browsable(true), DefaultValue(typeof(Padding), "0"), Description("�ߴ�"), Category("����")]
        public new virtual Padding Padding
        {
            get { return base.Padding; }
            set { base.Padding = value; }
        }

        RenderStyle m_eRenderStyle = RenderStyle.eSystem;
        [Browsable(true), DefaultValue(typeof(RenderStyle), "eSystem"), Description("��Ⱦ����"), Category("���")]
        public virtual RenderStyle eRenderStyle
        {
            get { return m_eRenderStyle; }
            set { m_eRenderStyle = value; }
        }

        [Browsable(false), Description("����������״̬��������¡������á�������"), Category("״̬")]
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
        [Browsable(false), Description("�޸�����������״̬���Ƿ�ˢ�£�SetBaseItemState��"), Category("����")]
        protected virtual bool RefreshBaseItemState { get { return false; } }
        #endregion

        #region IBaseItem2
        [Browsable(true), Description("ѡ�����Ըı�󴥷�"), Category("�����Ѹ���")]
        public event EventHandler CheckedChanged;

        [Browsable(false), Description("�����Ƿ����������������������չ�־���"), Category("״̬")]
        public bool Overflow
        { get { return false; } }

        [Browsable(false), Description("����ĸ߶��Ƿ��������类������߶Ƚ����ᱻ�Զ�����"), Category("����")]
        public abstract bool LockHeight { get; }

        [Browsable(false), Description("����Ŀ���Ƿ��������类�������Ƚ����ᱻ�Զ�����"), Category("����")]
        public abstract bool LockWith { get; }

        bool m_Checked = false;
        [Browsable(true), DefaultValue(false), Description("ѡ��"), Category("״̬")]
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
        [Browsable(false), Description("��������״̬"), Category("����")]
        BaseItemStyle IBaseItemProperty.eBaseItemStyle
        {
            get { return BaseItemStyle.eIndependentBaseItemControl; }
        }

        [Browsable(false), Description("��ȡ������������Ϊ������������Ϊ������"), Category("����")]
        IBaseItem3 IBaseItemProperty.DependObject
        {
            get { return this; }
        }
        #endregion

        #region IBaseItem3
        [Browsable(false), Description("��ʶ���Ƿ�Ϊ������������IBaseBarItem�������һ����Ա"), Category("��Ա��ʶ")]
        public bool IsBaseBarItem
        {
            get
            {
                return false;
            }
        }

        [Browsable(false), Description("��ʶ���Ƿ�Ϊ�����˵���壨IContextPopupPanelItem�������һ����Ա�����������ϼ���BasePopup"), Category("��Ա��ʶ")]
        public bool IsPopupItem
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// ֻ�� �� BasePopup ������һ����Ա  �Żᱻ�Ͽ�
        /// </summary>
        [Browsable(false), Description("��ʶ���Ƿ�Ϊ�����˵���BasePopup�������һ����Ա���������ϼ����ϼ���this.pOwner.pOwner����BasePopup"), Category("��Ա��ʶ")]
        public bool IsBasePopupItem
        {
            get
            {
                return false;
            }
        }

        [Browsable(false), Description("��ʶ���Ƿ�Ϊ�����˵���BasePopup����ĳ�Ա"), Category("��Ա��ʶ")]
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
        /// ���Ի�ȡ�����ڵ�BasePopup
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
        [Browsable(true), DefaultValue(false), Description("�Ƿ������Զ���ȡ����"), Category("����")]
        public bool AutoGetFocus
        {
            get { return m_AutoGetFocus; }
            set { m_AutoGetFocus = value; }
        }
        #endregion

        #region IBaseItem4
        /// <summary>
        /// ��ȡ�����ڵĶ��� IOwner
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
        /// ��ȡ�����ڵĿؼ�ʵ��
        /// </summary>
        /// <returns></returns>
        public Control TryGetDependControl()
        {
            return this.Parent;
        }

        /// <summary>
        /// ��ȡ�����ڴ������
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
        /// ������Ч���£��������ˢ��
        /// </summary>
        public void UIUpdate()
        {
            if (this.Parent != null) this.Parent.Refresh();
        }
        #endregion

        #region IBaseItem5
        /// <summary>
        /// �Ǽ�������ſ���ʹ��
        /// </summary>
        /// <returns></returns>
        public int TryGetIndex()
        {
            if (this.Parent == null) return -1;
            //
            return this.Parent.Controls.IndexOf(this);
        }

        /// <summary>
        /// �Ǽ�������ſ���ʹ��
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
        /// �Ǽ�������ſ���ʹ��
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
        [Browsable(false), Description("��ȡ��ӵ����"), Category("����")]
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
        [Browsable(false), Description("������չ�־���"), Category("����")]
        public virtual Rectangle ItemsRectangle
        {
            get { return this.DisplayRectangle; }
        }

        [Browsable(false), Description("������ͼ��չ�־���"), Category("����")]
        public Rectangle ItemsViewRectangle
        {
            get
            {
                return this.ItemsRectangle;
            }
        }

        [Browsable(false), Description("��ȡ������ӵ����"), Category("����")]
        public IBaseItemOwner pBaseItemOwner
        {
            get { return pOwner as IBaseItemOwner; }
        }
        #endregion

        #region IBaseItemOwner2
        [Browsable(false), Description("ȡ��������Ļ����¼�"), Category("״̬")]
        public virtual bool CancelItemsDrawEvent
        {
            get
            {
                return false;
            }
        }

        [Browsable(false), Description("ȡ��������������¼�����������¼�"), Category("״̬")]
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

        #region Ƕ����Ϣ����
        protected override void OnPaint(PaintEventArgs e)
        {
            if (this is IUICollectionItem)
            {
                //������Ϣ
                ((IMessageChain)this).SendMessage(new MessageInfo(this, MessageStyle.eMSViewInfo, new ViewInfo(this.Visible, this.Enabled, this.Overflow)));
            }
            //������Ϣ
            ((IMessageChain)this).SendMessage(new MessageInfo(this, MessageStyle.eMSPaint, e));
            //
            base.OnPaint(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            //������Ϣ
            ((IMessageChain)this).SendMessage(new MessageInfo(this, MessageStyle.eMSLostFocus, e));
            //
            base.OnLostFocus(e);
        }

        //
        protected override void OnKeyDown(KeyEventArgs e)
        {
            MessageInfo messageInfo = new MessageInfo(this, MessageStyle.eMSKeyDown, e);
            //������Ϣ
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
            //������Ϣ
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
            //������Ϣ
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
            //������Ϣ
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
            //������Ϣ
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
            //������Ϣ
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
            //������Ϣ
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
            //������Ϣ
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
            //������Ϣ
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
            //������Ϣ
            ((IMessageChain)this).SendMessage(new MessageInfo(this, MessageStyle.eMSMouseEnter, e));
            //
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            //������Ϣ
            ((IMessageChain)this).SendMessage(new MessageInfo(this, MessageStyle.eMSMouseLeave, e));
            //
            base.OnMouseLeave(e);
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            //������Ϣ
            ((IMessageChain)this).SendMessage(new MessageInfo(this, MessageStyle.eMSEnabledChanged, new BoolValueChangedEventArgs(this.Enabled)));
            //
            base.OnEnabledChanged(e);
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            //������Ϣ
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
            //ֲ�����
            this.MessageMonitor(messageInfo);
        }
        private void MSPaint(MessageInfo messageInfo)
        {
            //ֲ�����
            this.MessageMonitor(messageInfo);
        }
        private void MSLostFocus(MessageInfo messageInfo)
        {
            //ֲ�����
            this.MessageMonitor(messageInfo);
        }
        private void MSKeyDown(MessageInfo messageInfo)
        {
            //ֲ�����
            this.MessageMonitor(messageInfo);
        }
        private void MSKeyUp(MessageInfo messageInfo)
        {
            //ֲ�����
            this.MessageMonitor(messageInfo);
        }
        private void MSKeyPress(MessageInfo messageInfo)
        {
            //ֲ�����
            this.MessageMonitor(messageInfo);
        }
        private void MSMouseWheel(MessageInfo messageInfo)
        {
            //ֲ�����
            this.MessageMonitor(messageInfo);
        }
        private bool m_MouseDown = false;
        private void MSMouseDown(MessageInfo messageInfo)
        {
            this.m_MouseDown = true;
            if (this.RefreshBaseItemState) this.Refresh();
            //ֲ�����
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
            //ֲ�����
            this.MessageMonitor(messageInfo);
        }
        private bool m_MouseEnter = false;
        private void MSMouseMove(MessageInfo messageInfo)
        {
            //if (this.m_MouseDown) return;
            //ֲ�����
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
                //ֲ�����
                this.MessageMonitor(messageInfo);
            }
        }
        private void MSMouseClick(MessageInfo messageInfo)
        {
            //ֲ�����
            this.MessageMonitor(messageInfo);
        }
        private void MSMouseDoubleClick(MessageInfo messageInfo)
        {
            //ֲ�����
            this.MessageMonitor(messageInfo);
        }
        private void MSEnabledChanged(MessageInfo messageInfo)
        {
            this.m_MouseDown = false;
            this.m_MouseEnter = false;
            //ֲ�����
            this.MessageMonitor(messageInfo);
        }
        private void MSVisibleChanged(MessageInfo messageInfo)
        {
            this.m_MouseDown = false;
            this.m_MouseEnter = false;
            //ֲ�����
            this.MessageMonitor(messageInfo);
        }

        /// <summary>
        /// ����������Ϣ
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
