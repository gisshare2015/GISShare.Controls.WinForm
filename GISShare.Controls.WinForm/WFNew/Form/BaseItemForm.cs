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
        #endregion

        #region IBaseItem
        WFNew.RenderStyle m_eRenderStyle = WFNew.RenderStyle.eSystem;
        [Browsable(true), DefaultValue(typeof(WFNew.RenderStyle), "eSystem"), Description("渲染类型"), Category("外观")]
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
        [Browsable(true), DefaultValue(true), Description("是否有字体阴影"), Category("状态")]
        public bool HaveShadow
        {
            get { return m_HaveShadow; }
            set { m_HaveShadow = value; }
        }

        private Color m_ShadowColor = System.Drawing.SystemColors.ControlText;
        [Browsable(true), DefaultValue(typeof(Color), "System.Drawing.SystemColors.ControlText"), Description("字体阴影颜色"), Category("外观")]
        public Color ShadowColor
        {
            get { return m_ShadowColor; }
            set { m_ShadowColor = value; }
        }

        private bool m_ForeCustomize = false;
        [Browsable(true), DefaultValue(false), Description("自定义文本色"), Category("状态")]
        public bool ForeCustomize
        {
            get { return m_ForeCustomize; }
            set { m_ForeCustomize = value; }
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

        #region 嵌入消息链条
        protected override void OnPaint(PaintEventArgs e)
        {
            if (this is IUICollectionItem)
            {
                //发送消息
                ((IMessageChain)this).SendMessage(new MessageInfo(this, MessageStyle.eMSViewInfo, new ViewInfo(this.Visible, this.Enabled, false)));
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
            //if (this.AutoGetFocus && !this.Focused) this.Focus();
            ////
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
            //if (this.RefreshBaseItemState) this.Refresh();
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
            //if (this.RefreshBaseItemState) this.Refresh();
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
                //if (this.RefreshBaseItemState) { this.Refresh(); }
            }
        }
        private void MSMouseLeave(MessageInfo messageInfo)
        {
            if (this.m_MouseEnter)
            {
                this.m_MouseEnter = false;
                //if (this.RefreshBaseItemState) { this.Refresh(); }
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
    }
}
