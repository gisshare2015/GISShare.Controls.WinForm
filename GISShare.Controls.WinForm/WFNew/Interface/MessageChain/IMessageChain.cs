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
        /// ��Ϣ��Ϣ
        /// </summary>
        /// <param name="objSender">���Ͷ���</param>
        /// <param name="eMessageStyle">��Ϣ����</param>
        /// <param name="objMessageParameter">��Ϣ����</param>
        public MessageInfo(object objSender, MessageStyle eMessageStyle, object objMessageParameter)
        {
            this.m_Sender = objSender;
            this.m_eMessageStyle = eMessageStyle;
            this.m_MessageParameter = objMessageParameter;
        }

        object m_Sender;
        /// <summary>
        /// ��Ϣ�����������
        /// </summary>
        public object Sender
        {
            get { return m_Sender; }
        }

        MessageStyle m_eMessageStyle;
        /// <summary>
        /// ��Ϣ����
        /// </summary>
        public MessageStyle eMessageStyle
        {
            get { return m_eMessageStyle; }
        }

        object m_MessageParameter;
        /// <summary>
        /// ��Ϣ����
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
