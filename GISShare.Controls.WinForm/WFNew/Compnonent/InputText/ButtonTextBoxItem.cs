﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    [DefaultEvent("ButtonClick")]
    public class ButtonTextBoxItem : BaseItem, ITextBoxItem, ITextBoxItem2, IButtonTextBoxItem, IButtonTextBoxItemEvent, IInputObject, IInputObjectHelper
    {
        private const int CTR_GLYPHBUTTONWIDTH = 19;
        private const int CTR_BORDERSPASE = 3;
        private const int CTR_SPASE_LR = 1;

        private TextBoxButtonItem m_TextBoxButtonItem;
        private IInputRegion m_InputRegion = null;

        #region 构造函数
        public ButtonTextBoxItem()
        {
            this.m_TextBoxButtonItem = new TextBoxButtonItem();
            ((ISetOwnerHelper)this.m_TextBoxButtonItem).SetOwner(this);
            this.m_TextBoxButtonItem.Click += new EventHandler(TextBoxButtonItem_Click);
            //
            this.m_InputRegion = new InputRegion(this);
            this.m_InputRegion.PopupClosed += new EventHandler(InputRegion_PopupClosed);
            //
            //
            //
            this.Size = new Size(120, 20);
        }
        void TextBoxButtonItem_Click(object sender, EventArgs e)
        {
            if (this.Enabled)
            {
                this.OnButtonClick(e);
            }
        }
        void InputRegion_PopupClosed(object sender, EventArgs e)
        {
            this.Refresh();
        }

        //public ButtonTextBoxItem(GISShare.Controls.Plugin.WFNew.IButtonTextBoxItemP pBaseItemP)
        //    : this()
        //{
        //    //IPlugin
        //    this.Name = pBaseItemP.Name;
        //    //ISetEntityObject
        //    GISShare.Controls.Plugin.ISetEntityObject pSetEntityObject = pBaseItemP as GISShare.Controls.Plugin.ISetEntityObject;
        //    if (pSetEntityObject != null) pSetEntityObject.SetEntityObject(this);
        //    //IBaseItemP_
        //    this.Checked = pBaseItemP.Checked;
        //    this.Enabled = pBaseItemP.Enabled;
        //    this.Font = pBaseItemP.Font;
        //    this.ForeColor = pBaseItemP.ForeColor;
        //    this.LockHeight = pBaseItemP.LockHeight;
        //    this.LockWith = pBaseItemP.LockWith;
        //    this.Padding = pBaseItemP.Padding;
        //    this.Size = pBaseItemP.Size;
        //    this.Text = pBaseItemP.Text;
        //    this.Visible = pBaseItemP.Visible;
        //    this.Category = pBaseItemP.Category;
        //    this.MinimumSize = pBaseItemP.MinimumSize;
        //    this.UsingViewOverflow = pBaseItemP.UsingViewOverflow;
        //    //ITextBoxItemP
        //    this.eBorderStyle = pBaseItemP.eBorderStyle;
        //    this.PasswordChar = pBaseItemP.PasswordChar;
        //    this.CanEdit = pBaseItemP.CanEdit;
        //    //IButtonTextBoxItemP
        //    this.eGlyphStyle = pBaseItemP.eGlyphStyle;
        //}
        #endregion

        protected override EventStateStyle GetEventStateSupplement(string strEventName)
        {
            switch (strEventName)
            {
                case "ButtonClick":
                    return this.ButtonClick != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                default:
                    break;
            }
            //
            return base.GetEventStateSupplement(strEventName);
        }

        protected override bool RelationEventSupplement(string strEventName, EventArgs e)
        {
            switch (strEventName)
            {
                case "ButtonClick":
                    if (this.ButtonClick != null) { this.ButtonClick(this, e); }
                    return true;
                default:
                    break;
            }
            //
            return base.RelationEventSupplement(strEventName, e);
        }

        #region IInputObjectHelper
        IInputRegion IInputObjectHelper.GetInputRegion()
        {
            return this.m_InputRegion;
        }
        #endregion

        #region ITextBoxItem
        bool m_CanEdit = true;
        [Browsable(true), DefaultValue(true), Description("是否可以编辑"), Category("状态")]
        public virtual bool CanEdit
        {
            get { return m_CanEdit; }
            set { m_CanEdit = value; }
        }

        bool m_CanSelect = true;
        [Browsable(true), DefaultValue(true), Description("是否可以选择"), Category("状态")]
        public virtual bool CanSelect
        {
            get { return m_CanSelect; }
            set { m_CanSelect = value; }
        }

        GISShare.Controls.WinForm.WFNew.BorderStyle m_eBorderStyle = GISShare.Controls.WinForm.WFNew.BorderStyle.eSingle;
        [Browsable(true), DefaultValue(typeof(GISShare.Controls.WinForm.WFNew.BorderStyle), "eSingle"), Description("外框类型"), Category("外观")]
        public GISShare.Controls.WinForm.WFNew.BorderStyle eBorderStyle
        {
            get { return m_eBorderStyle; }
            set
            {
                if (m_eBorderStyle == value) return;
                m_eBorderStyle = value;
                //
                this.SizePlanning();
                //
                this.Refresh();
            }
        }

        [Browsable(false), Description("其外框矩形"), Category("布局")]
        public Rectangle FrameRectangle
        {
            get
            {
                Rectangle rectangle = this.DisplayRectangle;
                return new Rectangle(rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
            }
        }

        [Browsable(false), Description("文本绘制矩形框"), Category("布局")]
        public Rectangle TextRectangle
        {
            get
            {
                Rectangle rectangle = this.DisplayRectangle;
                int iH = this.m_InputRegion.GetInputRegionSize().Height;
                switch (this.eBorderStyle)
                {
                    case BorderStyle.eNone:
                        return new Rectangle
                            (
                            rectangle.X,
                            (rectangle.Top + rectangle.Bottom - iH) / 2,
                            rectangle.Width - CTR_GLYPHBUTTONWIDTH,
                            iH
                            );
                    case BorderStyle.eSingle:
                    default:
                        return new Rectangle
                            (
                            rectangle.X + CTR_SPASE_LR,
                            (rectangle.Top + rectangle.Bottom - iH) / 2,
                            rectangle.Width - 2 * CTR_SPASE_LR - CTR_GLYPHBUTTONWIDTH,
                            iH
                            );
                }
            }
        }
        #endregion

        #region ITextBoxItem2
        [Browsable(true), Description("掩码"), Category("外观")]
        public virtual char PasswordChar
        {
            get
            {
                return this.m_InputRegion.PasswordChar;
            }
            set
            {
                this.m_InputRegion.PasswordChar = value;
            }
        }
        #endregion

        #region IButtonTextBoxItemEvent
        public event EventHandler ButtonClick;
        #endregion

        #region IButtonTextBoxItem
        [Browsable(true), DefaultValue(typeof(GISShare.Controls.WinForm.WFNew.GlyphStyle), "ePointsHorizontal"), Description("图案制类型"), Category("外观")]
        public GlyphStyle eGlyphStyle
        {
            get { return this.m_TextBoxButtonItem.eGlyphStyle; }
            set { this.m_TextBoxButtonItem.eGlyphStyle = value; }
        }

        [Browsable(true), DefaultValue(typeof(Padding), "0,0,0,0"), Description("获取或设置控件内的空白"), Category("布局")]
        public Padding ButtonPadding 
        {
            get { return this.m_TextBoxButtonItem.Padding; }
            set { this.m_TextBoxButtonItem.Padding = value; }
        }

        [Browsable(false), Description("按钮矩形框"), Category("布局")]
        public Rectangle GlyphButtonRectangle
        {
            get
            {
                Rectangle rectangle = this.DisplayRectangle;
                switch (this.eBorderStyle)
                {
                    case BorderStyle.eNone:
                        return new Rectangle(rectangle.Right - CTR_GLYPHBUTTONWIDTH,
                            rectangle.Y,
                            CTR_GLYPHBUTTONWIDTH,
                            rectangle.Height);
                    case BorderStyle.eSingle:
                    default:
                        return new Rectangle(rectangle.Right - CTR_SPASE_LR - CTR_GLYPHBUTTONWIDTH,
                            rectangle.Y + 2 * CTR_SPASE_LR,
                            CTR_GLYPHBUTTONWIDTH - 1,
                            rectangle.Height - 4 * CTR_SPASE_LR);
                }
            }
        }

        [Browsable(false), Description("X轴偏移量，用来重新规划文本输入区"), Category("布局")]
        public virtual int OffsetX { get { return 0; } }
        #endregion

        public override string Text
        {
            get
            {
                return this.IsInputing ? this.m_InputRegion.Text : base.Text;
            }
            set
            {
                base.Text = value;
            }
        }

        protected override bool RefreshBaseItemState
        {
            get
            {
                return true;
            }
        }

        public override BaseItemState eBaseItemState
        {
            get
            {
                if (this.IsInputing) return BaseItemState.eHot;
                return base.eBaseItemState;
            }
        }

        public override bool LockHeight
        {
            get { return true; }
        }

        public override bool LockWith
        {
            get { return false; }
        }

        #region Clone
        public override object Clone()
        {
            TextBoxItem baseItem = new TextBoxItem();
            baseItem.Checked = this.Checked;
            baseItem.Enabled = this.Enabled;
            baseItem.Font = this.Font;
            baseItem.ForeColor = this.ForeColor;
            baseItem.Name = this.Name;
            baseItem.Site = this.Site;
            baseItem.Size = this.Size;
            baseItem.Tag = this.Tag;
            baseItem.Text = this.Text;
            baseItem.Visible = this.Visible;
            baseItem.CanEdit = this.CanEdit;
            if (this.GetEventState("VisibleChanged") == EventStateStyle.eUsed) baseItem.VisibleChanged += new EventHandler(baseItem_VisibleChanged);
            if (this.GetEventState("SizeChanged") == EventStateStyle.eUsed) baseItem.SizeChanged += new EventHandler(baseItem_SizeChanged);
            if (this.GetEventState("Paint") == EventStateStyle.eUsed) baseItem.Paint += new PaintEventHandler(baseItem_Paint);
            if (this.GetEventState("MouseUp") == EventStateStyle.eUsed) baseItem.MouseUp += new MouseEventHandler(baseItem_MouseUp);
            if (this.GetEventState("MouseMove") == EventStateStyle.eUsed) baseItem.MouseMove += new MouseEventHandler(baseItem_MouseMove);
            if (this.GetEventState("MouseLeave") == EventStateStyle.eUsed) baseItem.MouseLeave += new EventHandler(baseItem_MouseLeave);
            if (this.GetEventState("MouseEnter") == EventStateStyle.eUsed) baseItem.MouseEnter += new EventHandler(baseItem_MouseEnter);
            if (this.GetEventState("MouseDown") == EventStateStyle.eUsed) baseItem.MouseDown += new MouseEventHandler(baseItem_MouseDown);
            if (this.GetEventState("MouseDoubleClick") == EventStateStyle.eUsed) baseItem.MouseDoubleClick += new MouseEventHandler(baseItem_MouseDoubleClick);
            if (this.GetEventState("MouseClick") == EventStateStyle.eUsed) baseItem.MouseClick += new MouseEventHandler(baseItem_MouseClick);
            if (this.GetEventState("LocationChanged") == EventStateStyle.eUsed) baseItem.LocationChanged += new EventHandler(baseItem_LocationChanged);
            if (this.GetEventState("EnabledChanged") == EventStateStyle.eUsed) baseItem.EnabledChanged += new EventHandler(baseItem_EnabledChanged);
            if (this.GetEventState("CheckedChanged") == EventStateStyle.eUsed) baseItem.CheckedChanged += new EventHandler(baseItem_CheckedChanged);
            if (this.GetEventState("TextChanged") == EventStateStyle.eUsed) baseItem.TextChanged += new EventHandler(baseItem_TextChanged);
            if (this.GetEventState("KeyDown") == EventStateStyle.eUsed) baseItem.KeyDown += new KeyEventHandler(baseItem_KeyDown);
            if (this.GetEventState("KeyPress") == EventStateStyle.eUsed) baseItem.KeyPress += new KeyPressEventHandler(baseItem_KeyPress);
            if (this.GetEventState("KeyUp") == EventStateStyle.eUsed) baseItem.KeyUp += new KeyEventHandler(baseItem_KeyUp);
            return baseItem;
        }
        void baseItem_CheckedChanged(object sender, EventArgs e)
        {
            this.RelationEvent("CheckedChanged", e);
        }
        void baseItem_EnabledChanged(object sender, EventArgs e)
        {
            this.RelationEvent("EnabledChanged", e);
        }
        void baseItem_LocationChanged(object sender, EventArgs e)
        {
            this.RelationEvent("LocationChanged", e);
        }
        void baseItem_MouseClick(object sender, MouseEventArgs e)
        {
            this.RelationEvent("MouseClick", e);
        }
        void baseItem_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.RelationEvent("MouseDoubleClick", e);
        }
        void baseItem_MouseDown(object sender, MouseEventArgs e)
        {
            this.RelationEvent("MouseDown", e);
        }
        void baseItem_MouseEnter(object sender, EventArgs e)
        {
            this.RelationEvent("MouseEnter", e);
        }
        void baseItem_MouseLeave(object sender, EventArgs e)
        {
            this.RelationEvent("MouseLeave", e);
        }
        void baseItem_MouseMove(object sender, MouseEventArgs e)
        {
            this.RelationEvent("MouseMove", e);
        }
        void baseItem_MouseUp(object sender, MouseEventArgs e)
        {
            this.RelationEvent("MouseUp", e);
        }
        void baseItem_Paint(object sender, PaintEventArgs e)
        {
            this.RelationEvent("Paint", e);
        }
        void baseItem_SizeChanged(object sender, EventArgs e)
        {
            this.RelationEvent("SizeChanged", e);
        }
        void baseItem_VisibleChanged(object sender, EventArgs e)
        {
            this.RelationEvent("VisibleChanged", e);
        }
        void baseItem_KeyUp(object sender, KeyEventArgs e)
        {
            this.RelationEvent("KeyUp", e);
        }
        void baseItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.RelationEvent("KeyPress", e);
        }
        void baseItem_KeyDown(object sender, KeyEventArgs e)
        {
            this.RelationEvent("KeyDown", e);
        }
        void baseItem_TextChanged(object sender, EventArgs e)
        {
            this.RelationEvent("TextChanged", e);
        }
        #endregion

        public override Size MeasureSize(Graphics g)//有待完善
        {
            return DisplayRectangle.Size;
        }

        #region IInputObject
        string IInputObject.InputText
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        Font IInputObject.InputFont
        {
            get { return base.Font; }
        }

        Color IInputObject.InputForeColor
        {
            get { return base.ForeColor; }
        }

        [Browsable(false), Description("输入区矩形框（屏幕坐标）"), Category("布局")]
        Rectangle IInputObject.InputRegionRectangle
        {
            get
            {
                Rectangle rectangle = this.DisplayRectangle;
                int iH = this.m_InputRegion.GetInputRegionSize().Height;
                switch (this.eBorderStyle)
                {
                    case BorderStyle.eNone:
                        return new Rectangle
                            (
                            this.PointToScreen(rectangle.Location),
                            new Size(rectangle.Width - CTR_GLYPHBUTTONWIDTH, iH)
                            );
                    case BorderStyle.eSingle:
                    default:
                        return new Rectangle
                            (
                            this.PointToScreen(new Point(rectangle.Location.X + CTR_BORDERSPASE, (rectangle.Top + rectangle.Bottom - iH) / 2)),
                            new Size(rectangle.Width - 2 * CTR_BORDERSPASE - CTR_GLYPHBUTTONWIDTH, iH)
                            );
                }
            }
        }

        [Browsable(false), Description("是否正在输入"), Category("状态")]
        public bool IsInputing
        {
            get { return this.m_InputRegion.IsOpened; } 
        }
        #endregion

        /// <summary>
        /// 用来向下传递消息
        /// </summary>
        /// <param name="messageInfo"></param>
        protected override void MessageMonitor(MessageInfo messageInfo)
        {
            base.MessageMonitor(messageInfo);
            //
            ((IMessageChain)this.m_TextBoxButtonItem).SendMessage(messageInfo);
        }

        protected override void OnDraw(PaintEventArgs e)
        {
            //if (this.Height != CTR_HEIGHT) { ((ISetBaseItemHelper)this).SetSize(this.Width, CTR_HEIGHT); }
            this.SizePlanning();
            //
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderTextBox(
                new GISShare.Controls.WinForm.ObjectRenderEventArgs(e.Graphics, this, this.DisplayRectangle));
            if (!this.IsInputing)
            {
                if (this.PasswordChar == '\0')
                {
                    int iOffsetX = this.OffsetX;
                    if (iOffsetX > 0) iOffsetX += CTR_BORDERSPASE;
                    Rectangle rectangle = this.TextRectangle;
                    rectangle = Rectangle.FromLTRB(rectangle.Left + iOffsetX, rectangle.Top, rectangle.Right, rectangle.Bottom);
                    GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderTextBoxText(
                        new TextRenderEventArgs(e.Graphics, this, this.Enabled, this.HaveShadow, this.Text, this.ForeCustomize, this.ForeColor, this.ShadowColor, this.Font, rectangle));
                }
                else
                {
                    string strText = "";
                    for (int i = 0; i < this.Text.Length; i++)
                    {
                        strText += this.PasswordChar;
                    }
                    GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderTextBoxText(
                        new TextRenderEventArgs(e.Graphics, this, this.Enabled, this.HaveShadow, strText, this.ForeCustomize, this.ForeColor, this.ShadowColor, this.Font, this.TextRectangle));
                }
            }
        }
        private void SizePlanning()
        {
            int iH = this.m_InputRegion.GetInputRegionSize().Height;
            switch (this.eBorderStyle)
            {
                case BorderStyle.eNone:
                    if (this.Height != iH) this.Size = new Size(this.Width, iH);
                    break;
                case BorderStyle.eSingle:
                default:
                    iH += 2 * CTR_BORDERSPASE;
                    if (this.Height != iH) this.Size = new Size(this.Width, iH);
                    break;
            }
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);
            //
            if (this.Enabled && this.TextRectangle.Contains(mevent.Location)) this.m_InputRegion.Show();
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            //
            this.Invalidate(this.TextRectangle);
        }

        // 重写
        protected virtual void OnButtonClick(EventArgs e)
        {
            if (this.ButtonClick != null) this.ButtonClick(this, e);
        }

    }
}

