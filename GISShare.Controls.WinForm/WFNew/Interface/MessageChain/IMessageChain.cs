using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    internal interface IMessageChain : IReset, IMessagePermeate
    {
        void SendMessage(MessageInfo messageInfo);
    }

    public sealed class ViewInfo
    {
        public ViewInfo(bool bVisible, bool bEnabled, bool bOverflow)
        {
            this.m_Visible = bVisible;
            this.m_Enabled = bEnabled;
            this.m_Overflow = bOverflow;
        }

        bool m_Visible;
        public bool Visible
        {
            get { return m_Visible; }
        }

        bool m_Enabled;
        public bool Enabled
        {
            get { return m_Enabled; }
        }

        bool m_Overflow;
        public bool Overflow
        {
            get { return m_Overflow; }
        }
    }

    public sealed class MessageInfo
    {
        /// <summary>
        /// 消息信息
        /// </summary>
        /// <param name="objSender">发送对象</param>
        /// <param name="eMessageStyle">消息类型</param>
        /// <param name="objMessageParameter">消息参数</param>
        public MessageInfo(object objSender, MessageStyle eMessageStyle, object objMessageParameter)
        {
            this.m_Sender = objSender;
            this.m_eMessageStyle = eMessageStyle;
            this.m_MessageParameter = objMessageParameter;
        }

        object m_Sender;
        /// <summary>
        /// 消息的最初发送者
        /// </summary>
        public object Sender
        {
            get { return m_Sender; }
        }

        MessageStyle m_eMessageStyle;
        /// <summary>
        /// 消息类型
        /// </summary>
        public MessageStyle eMessageStyle
        {
            get { return m_eMessageStyle; }
        }

        object m_MessageParameter;
        /// <summary>
        /// 消息参数
        /// </summary>
        public object MessageParameter
        {
            get { return m_MessageParameter; }
        }

        bool m_CancelPreEvent = false;
        public bool CancelPreEvent
        {
            get { return m_CancelPreEvent; }
            set { m_CancelPreEvent = value; }
        }

        object m_Now;
        public object Now
        {
            get { return m_Now; }
            set { m_Now = value; }
        }
    }

    public enum MessageStyle
    {
        eMSNCPaint,
        eMSNCMouseDown,
        eMSNCMouseUp,
        eMSNCMouseMove,
        eMSNCMouseClick,
        eMSNCMouseDoubleClick,
        //
        eMSViewInfo,
        eMSPaint,// 
        //
        eMSLostFocus,//
        eMSKeyDown,//
        eMSKeyUp,//
        eMSKeyPress,//
        eMSMouseWheel,//
        //
        eMSMouseDown,// 
        eMSMouseUp,// 
        eMSMouseMove,// 
        eMSMouseEnter,//
        eMSMouseLeave,// 
        eMSMouseClick,// 
        eMSMouseDoubleClick,// 
        eMSEnabledChanged,// 
        eMSVisibleChanged
        
    }
 }
