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
    public class BaseItemForm : System.Windows.Forms.Form,
        IRecordItem, ISetRecordItemHelper,
        IBaseItem,
        IOwner, IBaseItemOwner, IBaseItemOwner2, ISetOwnerHelper,
        IMessageChain
    {
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
        #endregion

        #region IBaseItem
        WFNew.RenderStyle m_eRenderStyle = WFNew.RenderStyle.eSystem;
        [Browsable(true), DefaultValue(typeof(WFNew.RenderStyle), "eSystem"), Description("��Ⱦ����"), Category("���")]
        public virtual WFNew.RenderStyle eRenderStyle
        {
            get { return m_eRenderStyle; }
            set { m_eRenderStyle = value; }
        }

        BaseItemState IBaseItem.eBaseItemState
        {
            get { return this.Focused ?  BaseItemState.eHot : BaseItemState.eNormal; }
        }

        private bool m_HaveShadow = true;
        [Browsable(true), DefaultValue(true), Description("�Ƿ���������Ӱ"), Category("״̬")]
        public bool HaveShadow
        {
            get { return m_HaveShadow; }
            set { m_HaveShadow = value; }
        }

        private Color m_ShadowColor = System.Drawing.SystemColors.ControlText;
        [Browsable(true), DefaultValue(typeof(Color), "System.Drawing.SystemColors.ControlText"), Description("������Ӱ��ɫ"), Category("���")]
        public Color ShadowColor
        {
            get { return m_ShadowColor; }
            set { m_ShadowColor = value; }
        }

        private bool m_ForeCustomize = false;
        [Browsable(true), DefaultValue(false), Description("�Զ����ı�ɫ"), Category("״̬")]
        public bool ForeCustomize
        {
            get { return m_ForeCustomize; }
            set { m_ForeCustomize = value; }
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

        #region Ƕ����Ϣ����
        protected override void OnPaint(PaintEventArgs e)
        {
            if (this is IUICollectionItem)
            {
                //������Ϣ
                ((IMessageChain)this).SendMessage(new MessageInfo(this, MessageStyle.eMSViewInfo, new ViewInfo(this.Visible, this.Enabled, false)));
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
            //if (this.AutoGetFocus && !this.Focused) this.Focus();
            ////
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

        #region IMessagePermeate
        bool IMessagePermeate.PermeateCancelEvent(MessageStyle eMessageStyle)
        {
            return false;
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
            //if (this.RefreshBaseItemState) this.Refresh();
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
            //if (this.RefreshBaseItemState) this.Refresh();
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
                //if (this.RefreshBaseItemState) { this.Refresh(); }
            }
        }
        private void MSMouseLeave(MessageInfo messageInfo)
        {
            if (this.m_MouseEnter)
            {
                this.m_MouseEnter = false;
                //if (this.RefreshBaseItemState) { this.Refresh(); }
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
    }
}
