using System;
using System.Drawing;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections;

namespace GISShare.Controls.WinForm.WFNew.View
{
    [Serializable, DefaultProperty("Text")]
    public class ViewItem : IViewItem, IMessageChain, ISetViewItemHelper
    {
        public ViewItem() { }

        public ViewItem(string text)
        {
            this.m_Text = text;
        }

        public ViewItem(string name, string text)
            : this(text)
        {
            this.m_Name = name;
        }

        #region IRenderable
        RenderStyle m_eRenderStyle = RenderStyle.eSystem;
        [Browsable(true), DefaultValue(typeof(RenderStyle), "eSystem"), Description("��Ⱦ����"), Category("���")]
        public virtual RenderStyle eRenderStyle
        {
            get { return m_eRenderStyle; }
            set { m_eRenderStyle = value; }
        }
        #endregion

        #region IViewItem
        string m_Name;
        [Browsable(true), Description("����"), Category("����")]
        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        string m_Text = "ViewItem";
        [Browsable(true), Description("�ı�"), Category("���")]
        public virtual string Text
        {
            get { return m_Text; }
            set
            {
                if (m_Text == value) return;
                m_Text = value;
            }
        }

        private object m_Tag;
        [Browsable(true), DefaultValue(null), TypeConverter(typeof(StringConverter)), Description("����Я��������Ϣ"), Category("����")]
        public object Tag
        {
            get { return m_Tag; }
            set { m_Tag = value; }
        }

        /// <summary>
        /// ����������״̬��������¡������á�������
        /// </summary>
        [Browsable(false), Description("����������״̬��������¡������á�������"), Category("״̬")]
        public virtual BaseItemState eBaseItemState
        {
            get
            {
                if (this.m_MouseDown) return BaseItemState.ePressed;
                if (this.m_MouseEnter) return BaseItemState.eHot;
                return BaseItemState.eNormal;
            }
        }
        [Browsable(false), Description("�޸�����������״̬���Ƿ�ˢ�£�SetBaseItemState��"), Category("����")]
        protected virtual bool RefreshBaseItemState
        {
            get { return true; }
        }

        ViewParameterStyle m_eViewParameterStyle = ViewParameterStyle.eNone;
        /// <summary>
        /// ������¼��������Ĳ���
        /// </summary>
        [Browsable(false), DefaultValue(typeof(ViewParameterStyle), "eNone"), Description("��ͼ�������"), Category("����")]
        ViewParameterStyle IViewItem.eViewParameterStyle
        {
            get { return m_eViewParameterStyle; }
        }

        private Rectangle m_DisplayRectangle;
        [Browsable(false), Description("��չ�־���"), Category("����")]
        public Rectangle DisplayRectangle
        {
            get
            {
                return this.m_DisplayRectangle;
            }
        }

        /// <summary>
        /// ����Ӧ�еĳߴ� �ܶ�ʱ�򷵻� DisplayRectangle
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        public virtual Size MeasureSize(Graphics g)
        {
            SizeF size = g.MeasureString(this.Text, new Font("����", 9f));
            return new Size((int)size.Width + 1, (int)size.Height + 1);
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
        private bool m_MouseDown = false;
        private bool m_MouseEnter = false;
        void IMessageChain.SendMessage(MessageInfo messageInfo)
        {
            switch (messageInfo.eMessageStyle)
            {
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
        private void MSPaint(MessageInfo messageInfo)
        {
            this.OnDraw(messageInfo.MessageParameter as PaintEventArgs);
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
        private void MSMouseDown(MessageInfo messageInfo)
        {
            IOwner pOwner = messageInfo.Now as IOwner;
            if (pOwner != null)
            {
                MouseEventArgs mouseEventArgs = messageInfo.MessageParameter as MouseEventArgs;
                if (mouseEventArgs != null && this.DisplayRectangle.Contains(mouseEventArgs.Location))
                {
                    this.m_MouseDown = true;
                    if (this.RefreshBaseItemState) { pOwner.Refresh(); }
                    //ֲ�����
                    this.MessageMonitor(messageInfo);//new MessageInfo(this, messageInfo.eMessageStyle, messageInfo.MessageParameter)
                }
            }
        }
        private void MSMouseUp(MessageInfo messageInfo)
        {
            this.m_MouseDown = false;
            IViewItemOwner pViewItemOwner = messageInfo.Now as IViewItemOwner;
            if (pViewItemOwner != null)
            {
                MouseEventArgs mouseEventArgs = messageInfo.MessageParameter as MouseEventArgs;
                if (mouseEventArgs == null || this.DisplayRectangle.Contains(mouseEventArgs.Location))
                {
                    this.m_MouseEnter = false;
                }
                if (this.RefreshBaseItemState) { pViewItemOwner.Refresh(); }
            }
            //ֲ�����
            this.MessageMonitor(messageInfo);//new MessageInfo(this, messageInfo.eMessageStyle, messageInfo.MessageParameter)
        }
        private void MSMouseMove(MessageInfo messageInfo)//
        {
            //if (this.m_MouseDown) return;
            //
            IViewItemOwner pViewItemOwner = messageInfo.Now as IViewItemOwner;
            if (pViewItemOwner != null)
            {
                MouseEventArgs mouseEventArgs = messageInfo.MessageParameter as MouseEventArgs;
                if (mouseEventArgs != null)
                {
                    Rectangle rectangle = Rectangle.Intersect(pViewItemOwner.ViewItemsRectangle, this.DisplayRectangle);
                    if (this.m_MouseDown && !rectangle.Contains(mouseEventArgs.Location)) return;//key
                    //
                    bool bMouseMove = Form.MouseButtons == MouseButtons.None ? rectangle.Contains(mouseEventArgs.Location) : this.m_MouseDown;
                    //if (this.DisplayRectangle.Contains(mouseEventArgs.Location))
                    if (bMouseMove)
                    {
                        if (!this.m_MouseEnter)
                        {
                            this.m_MouseEnter = true;
                            //Console.WriteLine(this.Text + "|true|" + this.eBaseItemState);
                            if (this.RefreshBaseItemState) { pViewItemOwner.Refresh(); }
                            //ֲ�����
                            this.MessageMonitor(new MessageInfo(this, MessageStyle.eMSMouseEnter, messageInfo.MessageParameter));
                        }
                        //ֲ�����
                        this.MessageMonitor(messageInfo);//new MessageInfo(this, messageInfo.eMessageStyle, messageInfo.MessageParameter)
                    }
                    else
                    {
                        if (this.m_MouseEnter)
                        {
                            this.m_MouseEnter = false;
                            //Console.WriteLine(this.Text + "|2 false|" + this.eBaseItemState);
                            if (this.RefreshBaseItemState) { pViewItemOwner.Refresh(); }
                            //ֲ�����
                            this.MessageMonitor(new MessageInfo(this, MessageStyle.eMSMouseLeave, messageInfo.MessageParameter));
                        }
                    }
                }
            }
        }
        private void MSMouseEnter(MessageInfo messageInfo)
        {
            if (!this.m_MouseEnter)
            {
                this.m_MouseEnter = true;
                //Console.WriteLine(this.Text + "|2 true|" + this.eBaseItemState);
                IViewItemOwner pViewItemOwner = messageInfo.Now as IViewItemOwner;
                if (pViewItemOwner != null)
                {
                    if (this.RefreshBaseItemState) { pViewItemOwner.Refresh(); }
                }
            }
        }
        private void MSMouseLeave(MessageInfo messageInfo)
        {
            if (this.m_MouseEnter)
            {
                this.m_MouseEnter = false;
                //Console.WriteLine(this.Text + "|1 false|" + this.eBaseItemState);
                IViewItemOwner pViewItemOwner = messageInfo.Now as IViewItemOwner;
                if (pViewItemOwner != null)
                {
                    if (this.RefreshBaseItemState) { pViewItemOwner.Refresh(); }
                }
                //ֲ�����
                this.MessageMonitor(messageInfo);//new MessageInfo(this, messageInfo.eMessageStyle, messageInfo.MessageParameter)
            }
        }
        private void MSMouseClick(MessageInfo messageInfo)
        {
            IViewItemOwner pViewItemOwner = messageInfo.Now as IViewItemOwner;
            if (pViewItemOwner != null)
            {
                MouseEventArgs mouseEventArgs = messageInfo.MessageParameter as MouseEventArgs;
                if (mouseEventArgs != null && this.DisplayRectangle.Contains(mouseEventArgs.Location))
                {
                    //ֲ�����
                    this.MessageMonitor(messageInfo);//new MessageInfo(this, messageInfo.eMessageStyle, messageInfo.MessageParameter)
                }
            }
        }
        private void MSMouseDoubleClick(MessageInfo messageInfo)
        {
            IViewItemOwner pViewItemOwner = messageInfo.Now as IViewItemOwner;
            if (pViewItemOwner != null)
            {
                MouseEventArgs mouseEventArgs = messageInfo.MessageParameter as MouseEventArgs;
                if (mouseEventArgs != null && this.DisplayRectangle.Contains(mouseEventArgs.Location))
                {
                    //ֲ�����
                    this.MessageMonitor(messageInfo);//new MessageInfo(this, messageInfo.eMessageStyle, messageInfo.MessageParameter)
                }
            }
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
        
        protected virtual void MessageMonitor(MessageInfo messageInfo)
        {

        }
        #endregion

        #region ISetViewItemHelper
        bool ISetViewItemHelper.SetViewParameterStyle(ViewParameterStyle viewParameterStyle)
        {
            if (this.m_eViewParameterStyle == viewParameterStyle) return false;
            this.m_eViewParameterStyle = viewParameterStyle;
            this.OnViewParameterStyleChanged(viewParameterStyle);
            return true;
        }
        protected virtual void OnViewParameterStyleChanged(ViewParameterStyle viewParameterStyle)
        {

        }

        bool ISetViewItemHelper.SetViewItemDisplayRectangle(Rectangle rectangle)
        {
            if (this.DisplayRectangle == rectangle) return false;
            this.m_DisplayRectangle = rectangle;
            return true;
        }
        #endregion

        protected virtual void OnDraw(PaintEventArgs e)
        {
            Rectangle rectangle = Rectangle.FromLTRB(e.ClipRectangle.Left, e.ClipRectangle.Top, e.ClipRectangle.Right - 1, e.ClipRectangle.Bottom - 1);
            WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderViewItem(new ObjectRenderEventArgs(e.Graphics, this, rectangle));
            if (this.Text.Length <= 0) return;
            Font font = new Font("����", 9f);
            rectangle = this.DisplayRectangle;
            int iH = (int)e.Graphics.MeasureString(this.Text, font).Height + 1;
            WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonText
                (
                new TextRenderEventArgs(
                    e.Graphics,
                    this,
                    true,
                    true,
                    this.Text,
                    System.Drawing.SystemColors.ControlText,
                    font,
                    new Rectangle(rectangle.Left, (rectangle.Top + rectangle.Bottom - iH) / 2, rectangle.Width, iH),//rectangle.Height
                    new StringFormat())
                );
        }
    }
}
