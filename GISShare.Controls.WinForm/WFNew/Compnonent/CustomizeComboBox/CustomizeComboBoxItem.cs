using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    public class CustomizeComboBoxItem : BaseItem, ICustomizeComboBoxItem, ICustomizeComboBoxItemEvent, IInputObject, IInputObjectHelper, IPopupOwnerHelper
    {
        private const int CTR_BORDERSPASE = 3;
        private const int CTR_SPASE_LR = 1;
        //
        private const int CTR_DROPDOWNBUTTONWIDTH = 16;
        private const int CTR_ARROWWIDTH = 6;
        private const int CTR_ARROWHEIGHT = 6;

        private IInputRegion m_InputRegion;
        private ICustomizePopup m_pCustomizePopup;

        public CustomizeComboBoxItem(ICustomizePopup pCustomizePopup)
            : base()
        {
            this.m_InputRegion = new InputRegion(this);
            this.m_InputRegion.PopupClosed += new EventHandler(InputRegion_PopupClosed);
            //
            this.m_pCustomizePopup = pCustomizePopup;
            ((ISetOwnerHelper)this.m_pCustomizePopup).SetOwner(this);
            this.m_pCustomizePopup.PopupOpened += new EventHandler(CustomizePopup_PopupOpened);
            this.m_pCustomizePopup.PopupClosed += new EventHandler(CustomizePopup_PopupClosed);
        }
        void InputRegion_PopupClosed(object sender, EventArgs e)
        {
            this.Refresh();
        }
        void CustomizePopup_PopupOpened(object sender, EventArgs e)
        {
            this.Refresh();
            this.OnPopupOpened(e);
            //
            this.m_ShowDropDownNum++;
        }
        void CustomizePopup_PopupClosed(object sender, EventArgs e)
        {
            this.OnPopupClosed(e);
            //
            if (!this.Contains(this.PointToClient(System.Windows.Forms.Form.MousePosition)))
            {
                this.Refresh();
                //发送消息
                ((IMessageChain)this).SendMessage(new MessageInfo(this, MessageStyle.eMSMouseLeave, e));
            }
        }

        public CustomizeComboBoxItem(Control ctr)
            : this(new CustomizePopup(ctr) as ICustomizePopup)
        { }

        protected override EventStateStyle GetEventStateSupplement(string strEventName)
        {
            switch (strEventName)
            {
                case "PopupOpened":
                    return this.PopupOpened != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "PopupClosed":
                    return this.PopupClosed != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "TextBoxMouseDown":
                    return this.TextBoxMouseDown != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "SplitMouseDown":
                    return this.SplitMouseDown != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "TextBoxMouseMove":
                    return this.TextBoxMouseMove != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "SplitMouseMove":
                    return this.SplitMouseMove != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "TextBoxMouseUp":
                    return this.TextBoxMouseUp != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "SplitMouseUp":
                    return this.SplitMouseUp != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "TextBoxMouseClick":
                    return this.TextBoxMouseClick != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "SplitMouseClick":
                    return this.SplitMouseClick != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "TextBoxMouseDoubleClick":
                    return this.TextBoxMouseDoubleClick != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "SplitMouseDoubleClick":
                    return this.SplitMouseDoubleClick != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
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
                case "PopupOpened":
                    if (this.PopupOpened != null) { this.PopupOpened(this, e as PaintEventArgs); }
                    return true;
                case "PopupClosed":
                    if (this.PopupClosed != null) { this.PopupClosed(this, e as EventArgs); }
                    return true;
                case "TextBoxMouseDown":
                    if (this.TextBoxMouseDown != null) { this.TextBoxMouseDown(this, e as MouseEventArgs); }
                    return true;
                case "SplitMouseDown":
                    if (this.SplitMouseDown != null) { this.SplitMouseDown(this, e as MouseEventArgs); }
                    return true;
                case "TextBoxMouseMove":
                    if (this.TextBoxMouseMove != null) { this.TextBoxMouseMove(this, e as MouseEventArgs); }
                    return true;
                case "SplitMouseMove":
                    if (this.SplitMouseMove != null) { this.SplitMouseMove(this, e as MouseEventArgs); }
                    return true;
                case "TextBoxMouseUp":
                    if (this.TextBoxMouseUp != null) { this.TextBoxMouseUp(this, e as MouseEventArgs); }
                    return true;
                case "SplitMouseUp":
                    if (this.SplitMouseUp != null) { this.SplitMouseUp(this, e as MouseEventArgs); }
                    return true;
                case "TextBoxMouseClick":
                    if (this.TextBoxMouseClick != null) { this.TextBoxMouseClick(this, e as MouseEventArgs); }
                    return true;
                case "SplitMouseClick":
                    if (this.SplitMouseClick != null) { this.SplitMouseClick(this, e as MouseEventArgs); }
                    return true;
                case "TextBoxMouseDoubleClick":
                    if (this.TextBoxMouseDoubleClick != null) { this.TextBoxMouseDoubleClick(this, e as MouseEventArgs); }
                    return true;
                case "SplitMouseDoubleClick":
                    if (this.SplitMouseDoubleClick != null) { this.SplitMouseDoubleClick(this, e as MouseEventArgs); }
                    return true;
                default:
                    break;
            }
            //
            return base.RelationEventSupplement(strEventName, e);
        }

        #region IInputObjectHelper
        string IInputObject.InputText
        {
            get { return this.Text; }
            set { this.Text = value; }
        }

        Font IInputObject.InputFont
        {
            get { return base.Font; }
        }

        Color IInputObject.InputForeColor
        {
            get { return base.ForeColor; }
        }

        IInputRegion IInputObjectHelper.GetInputRegion()
        {
            return this.m_InputRegion;
        }
        #endregion

        #region IPopupOwnerHelper
        IBasePopup IPopupOwnerHelper.GetBasePopup()
        {
            return this.m_pCustomizePopup;
        }
        #endregion

        #region IPopupOwner2
        [Browsable(true), Description("当弹出Popup触发"), Category("弹出菜单")]
        public event EventHandler PopupOpened;
        [Browsable(true), Description("当关闭Popup触发"), Category("弹出菜单")]
        public event EventHandler PopupClosed;

        private int m_PopupSpace = 0;
        [Browsable(true), DefaultValue(0), Description("弹出菜单与其携带者的间距"), Category("布局")]
        public int PopupSpace
        {
            get { return m_PopupSpace; }
            set { m_PopupSpace = value; }
        }

        [Browsable(false), Description("弹出菜单的坐标点（屏幕坐标）"), Category("布局")]
        public Point PopupLoction
        {
            get
            {
                Rectangle rectangle = this.DisplayRectangle;
                if (this.ShowDropDownNum < 1) this.m_pCustomizePopup.SetSize(new Size(this.Width, this.DropDownHeight));
                return this.PointToScreen(new Point(rectangle.Left, rectangle.Bottom + this.PopupSpace));
            }
        }

        [Browsable(false), Description("是否有自动触发Popup的展现"), Category("行为")]
        public virtual bool IsAutoMouseTrigger
        {
            get { return true; }
        }

        [Browsable(false), Description("弹出菜单的激活区"), Category("布局")]
        public virtual Rectangle PopupTriggerRectangle
        {
            get { return this.SplitRectangle; }
        }

        [Browsable(false), Description("是否已展开弹出项"), Category("状态")]
        public bool IsOpened
        {
            get { return this.m_pCustomizePopup.IsOpened; }
        }

        public void ShowPopup()
        {
            if (this.IsOpened) return;
            //
            this.m_pCustomizePopup.Show(this.PopupLoction);
        }

        public void ClosePopup()
        {
            if (!this.IsOpened) return;
            //
            this.m_pCustomizePopup.Close();
        }
        #endregion

        #region ITextBoxItem
        [Browsable(false), DefaultValue(true), Description("是否可以编辑"), Category("状态")]
        public bool CanEdit
        {
            get { return this.eCustomizeComboBoxStyle == CustomizeComboBoxStyle.eDropDown; }
            set { }
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
        public virtual Rectangle TextRectangle
        {
            get
            {
                //Rectangle rectangle = this.DisplayRectangle;
                //return new Rectangle
                //    (
                //    rectangle.X + CTR_TEXTOFFSETX + this.OffsetX,
                //    rectangle.Y + CTR_TEXTOFFSETY,
                //    rectangle.Width + CTR_TEXTINFLATEWIDTH - CTR_DROPDOWNBUTTONWIDTH - 1 - this.OffsetX,
                //    CTR_TEXTHEIGHT
                //    );
                Rectangle rectangle = this.DisplayRectangle;
                int iH = this.m_InputRegion.GetInputRegionSize().Height;
                switch (this.eBorderStyle)
                {
                    case BorderStyle.eNone:
                        return new Rectangle
                            (
                            rectangle.X + this.OffsetX,
                            (rectangle.Top + rectangle.Bottom - iH) / 2,
                            rectangle.Width - CTR_DROPDOWNBUTTONWIDTH - this.OffsetX,
                            iH
                            );
                    case BorderStyle.eSingle:
                    default:
                        return new Rectangle
                            (
                            rectangle.X + CTR_SPASE_LR + this.OffsetX,
                            (rectangle.Top + rectangle.Bottom - iH) / 2,
                            rectangle.Width - 2 * CTR_SPASE_LR - CTR_DROPDOWNBUTTONWIDTH - this.OffsetX,
                            iH
                            );
                }
            }
        }
        #endregion

        #region ICustomizeComboBoxItem
        bool m_AutoClosePopup = true;
        [Browsable(true), DefaultValue(true), Description("表示弹出框是否自动关闭"), Category("状态")]
        public virtual bool AutoClosePopup
        {
            get { return m_AutoClosePopup; }
            set { m_AutoClosePopup = value; }
        }

        [Browsable(true), DefaultValue(26), Description("最小高度"), Category("布局")]
        public int MinHeight
        {
            get
            {
                if (this.m_pCustomizePopup == null) return 26;
                return this.m_pCustomizePopup.MinHeight;
            }
            set
            {
                if (this.m_pCustomizePopup == null) return;
                this.m_pCustomizePopup.MinHeight = value;
            }
        }

        [Browsable(true), DefaultValue(26), Description("最小宽度"), Category("布局")]
        public int MinWidth
        {
            get
            {
                if (this.m_pCustomizePopup == null) return 26;
                return this.m_pCustomizePopup.MinWidth;
            }
            set
            {
                if (this.m_pCustomizePopup == null) return;
                this.m_pCustomizePopup.MinWidth = value;
            }
        }

        [Browsable(true), Description("弹出框的宽度"), Category("布局")]//DefaultValue(120),
        public int DropDownWidth
        {
            get
            {
                if (this.m_pCustomizePopup == null) return -1;
                return this.m_pCustomizePopup.Size.Width;
            }
            set
            {
                if (this.m_pCustomizePopup == null) return;
                if (this.m_pCustomizePopup.Width == value) return;
                this.m_pCustomizePopup.SetSize(new Size(value, this.m_pCustomizePopup.Size.Height));
            }
        }

        [Browsable(true), Description("弹出框的高度"), Category("布局")]//DefaultValue(120), 
        public int DropDownHeight
        {
            get
            {
                if (this.m_pCustomizePopup == null) return -1;
                return this.m_pCustomizePopup.Size.Height;
            }
            set
            {
                if (this.m_pCustomizePopup == null) return;
                if (this.m_pCustomizePopup.Height == value) return;
                this.m_pCustomizePopup.SetSize(new Size(this.m_pCustomizePopup.Size.Width, value));
            }
        }

        [Browsable(true), DefaultValue(typeof(ModifySizeStyle), "eNone"), Description("下拉菜单修改尺寸的类型"), Category("布局")]
        public ModifySizeStyle eModifySizeStyle
        {
            get
            {
                if (this.m_pCustomizePopup == null) return ModifySizeStyle.eNone;
                return this.m_pCustomizePopup.eModifySizeStyle;
            }
            set
            {
                if (this.m_pCustomizePopup == null) return;
                this.m_pCustomizePopup.eModifySizeStyle = value;
            }
        }

        private CustomizeComboBoxStyle m_eCustomizeComboBoxStyle = CustomizeComboBoxStyle.eDropDown;
        [Browsable(true), DefaultValue(typeof(CustomizeComboBoxStyle), "eDropDown"), Description("文本框的编辑状态"), Category("外观")]
        public virtual CustomizeComboBoxStyle eCustomizeComboBoxStyle
        {
            get { return m_eCustomizeComboBoxStyle; }
            set { m_eCustomizeComboBoxStyle = value; }
        }

        [Browsable(false), Description("文本框的值"), Category("数据")]
        public virtual object Value
        {
            get { return this.Text; }
        }

        [Browsable(false), Description("X轴偏移量，用来重新规划文本输入区"), Category("布局")]
        public virtual int OffsetX { get { return 0; } }

        private int m_ShowDropDownNum = 0;
        [Browsable(false), Description("记录下拉框展现的次数"), Category("状态")]
        public int ShowDropDownNum
        {
            get { return m_ShowDropDownNum; }
            set { m_ShowDropDownNum = value; }
        }

        private BaseItemState m_eSplitState = BaseItemState.eNormal;
        [Browsable(false), Description("分割区的状态（激活、按下、不可用、正常）"), Category("状态")]
        public BaseItemState eSplitState
        {
            get
            {
                if (this.IsOpened) return BaseItemState.ePressed;
                return m_eSplitState;
            }
        }

        [Browsable(false), Description("分割区矩形框"), Category("布局")]
        public Rectangle SplitRectangle
        {
            get
            {
                Rectangle rectangle = this.DisplayRectangle;
                return new Rectangle(rectangle.Right - CTR_DROPDOWNBUTTONWIDTH - 1, rectangle.Top, CTR_DROPDOWNBUTTONWIDTH, rectangle.Height);
            }
        }

        private Size m_ArrowSize = new Size(5, 4);
        [Browsable(true), DefaultValue(typeof(Size), "5, 4"), Description("箭头尺寸"), Category("布局")]
        public Size ArrowSize
        {
            get { return m_ArrowSize; }
            set
            {
                if (value.Width <= 0 || value.Height <= 0) return;
                //
                m_ArrowSize = value;
            }
        }

        [Browsable(false), Description("箭头区矩形框"), Category("布局")]
        public Rectangle ArrowRectangle
        {
            get
            {
                Rectangle splitRectangle = this.SplitRectangle;
                return new Rectangle
                    (
                    new Point
                        (
                        (splitRectangle.Left + splitRectangle.Right - this.ArrowSize.Width) / 2 + 1,
                        (splitRectangle.Top + splitRectangle.Bottom - this.ArrowSize.Height) / 2 + 1
                        ),
                    this.ArrowSize
                    );
            }
        }

        [Browsable(false), Description("获取携带的控件对象"), Category("布局")]
        System.Windows.Forms.Control ICustomizeComboBoxItem.ControlObject
        {
            get { return this.m_pCustomizePopup.ControlObject; }
        }
        #endregion

        #region ICustomizeComboBoxItemEvent
        [Browsable(true), Description("鼠标在文本输入区可见部分按下时触发"), Category("鼠标")]
        public event MouseEventHandler TextBoxMouseDown;
        [Browsable(true), Description("鼠标在分割区可见部分按下时触发"), Category("鼠标")]
        public event MouseEventHandler SplitMouseDown;
        [Browsable(true), Description("鼠标在文本输入区可见部分按下时触发"), Category("鼠标")]
        public event MouseEventHandler TextBoxMouseMove;
        [Browsable(true), Description("鼠标在分割区可见部分按下时触发"), Category("鼠标")]
        public event MouseEventHandler SplitMouseMove;
        [Browsable(true), Description("鼠标在文本输入区可见部分按下时触发"), Category("鼠标")]
        public event MouseEventHandler TextBoxMouseUp;
        [Browsable(true), Description("鼠标在分割区可见部分按下时触发"), Category("鼠标")]
        public event MouseEventHandler SplitMouseUp;
        [Browsable(true), Description("鼠标在文本输入区可见部分按下时触发"), Category("鼠标")]
        public event MouseEventHandler TextBoxMouseClick;
        [Browsable(true), Description("鼠标在分割区可见部分按下时触发"), Category("鼠标")]
        public event MouseEventHandler SplitMouseClick;
        [Browsable(true), Description("鼠标在文本输入区可见部分按下时触发"), Category("鼠标")]
        public event MouseEventHandler TextBoxMouseDoubleClick;
        [Browsable(true), Description("鼠标在分割区可见部分按下时触发"), Category("鼠标")]
        public event MouseEventHandler SplitMouseDoubleClick;
        #endregion

        #region IInputObject
        [Browsable(false), Description("输入区矩形框（屏幕坐标）"), Category("布局")]
        Rectangle IInputObject.InputRegionRectangle
        {
            get
            {
                //Rectangle rectangle = this.DisplayRectangle;
                //return new Rectangle
                //    (
                //    this.PointToScreen(new Point(rectangle.Location.X + CTR_INPUTREGIONOFFSETX + this.OffsetX, rectangle.Location.Y + CTR_INPUTREGIONOFFSETY)),
                //    new Size(this.DisplayRectangle.Width - 2 * CTR_INPUTREGIONOFFSETX - CTR_DROPDOWNBUTTONWIDTH - this.OffsetX, CTR_INPUTREGIONHEIGHT)
                //    );
                Rectangle rectangle = this.DisplayRectangle;
                int iH = this.m_InputRegion.GetInputRegionSize().Height;
                switch (this.eBorderStyle)
                {
                    case BorderStyle.eNone:
                        return new Rectangle
                            (
                            this.PointToScreen(new Point(rectangle.Location.X + this.OffsetX, (rectangle.Top + rectangle.Bottom - iH) / 2)),
                            new Size(rectangle.Width - CTR_DROPDOWNBUTTONWIDTH - this.OffsetX, iH)
                            );
                    case BorderStyle.eSingle:
                    default:
                        return new Rectangle
                            (
                            this.PointToScreen(new Point(rectangle.Location.X + CTR_BORDERSPASE + this.OffsetX, (rectangle.Top + rectangle.Bottom - iH) / 2)),
                            new Size(rectangle.Width - 2 * CTR_BORDERSPASE - CTR_DROPDOWNBUTTONWIDTH - this.OffsetX, iH)
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

        #region Clone
        public override object Clone()
        {
            CustomizeComboBoxItem baseItem = new CustomizeComboBoxItem(this.m_pCustomizePopup);
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
            baseItem.MinHeight = this.MinHeight;
            baseItem.MinWidth = this.MinWidth;
            baseItem.DropDownHeight = this.DropDownHeight;
            baseItem.DropDownWidth = this.DropDownWidth;
            baseItem.eModifySizeStyle = this.eModifySizeStyle;
            baseItem.eCustomizeComboBoxStyle = this.eCustomizeComboBoxStyle;
            baseItem.ArrowSize = this.ArrowSize;
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
            if (this.GetEventState("PopupOpened") == EventStateStyle.eUsed) baseItem.PopupOpened += new EventHandler(baseItem_PopupOpened);
            if (this.GetEventState("PopupClosed") == EventStateStyle.eUsed) baseItem.PopupClosed += new EventHandler(baseItem_PopupClosed);
            if (this.GetEventState("SplitMouseUp") == EventStateStyle.eUsed) baseItem.SplitMouseUp += new MouseEventHandler(baseItem_SplitMouseUp);
            if (this.GetEventState("SplitMouseMove") == EventStateStyle.eUsed) baseItem.SplitMouseMove += new MouseEventHandler(baseItem_SplitMouseMove);
            if (this.GetEventState("SplitMouseDown") == EventStateStyle.eUsed) baseItem.SplitMouseDown += new MouseEventHandler(baseItem_SplitMouseDown);
            if (this.GetEventState("SplitMouseDoubleClick") == EventStateStyle.eUsed) baseItem.SplitMouseDoubleClick += new MouseEventHandler(baseItem_SplitMouseDoubleClick);
            if (this.GetEventState("SplitMouseClick") == EventStateStyle.eUsed) baseItem.SplitMouseClick += new MouseEventHandler(baseItem_SplitMouseClick);
            if (this.GetEventState("TextBoxMouseUp") == EventStateStyle.eUsed) baseItem.TextBoxMouseUp += new MouseEventHandler(baseItem_TextBoxMouseUp);
            if (this.GetEventState("TextBoxMouseMove") == EventStateStyle.eUsed) baseItem.TextBoxMouseMove += new MouseEventHandler(baseItem_TextBoxMouseMove);
            if (this.GetEventState("TextBoxMouseDown") == EventStateStyle.eUsed) baseItem.TextBoxMouseDown += new MouseEventHandler(baseItem_TextBoxMouseDown);
            if (this.GetEventState("TextBoxMouseDoubleClick") == EventStateStyle.eUsed) baseItem.TextBoxMouseDoubleClick += new MouseEventHandler(baseItem_TextBoxMouseDoubleClick);
            if (this.GetEventState("TextBoxMouseClick") == EventStateStyle.eUsed) baseItem.TextBoxMouseClick += new MouseEventHandler(baseItem_TextBoxMouseClick);

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
        void baseItem_PopupClosed(object sender, EventArgs e)
        {
            this.RelationEvent("PopupClosed", e);
        }
        void baseItem_PopupOpened(object sender, EventArgs e)
        {
            this.RelationEvent("PopupOpened", e);
        }
        void baseItem_TextBoxMouseClick(object sender, MouseEventArgs e)
        {
            this.RelationEvent("TextBoxMouseClick", e);
        }
        void baseItem_TextBoxMouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.RelationEvent("TextBoxMouseDoubleClick", e);
        }
        void baseItem_TextBoxMouseDown(object sender, MouseEventArgs e)
        {
            this.RelationEvent("TextBoxMouseDown", e);
        }
        void baseItem_TextBoxMouseMove(object sender, MouseEventArgs e)
        {
            this.RelationEvent("TextBoxMouseMove", e);
        }
        void baseItem_TextBoxMouseUp(object sender, MouseEventArgs e)
        {
            this.RelationEvent("TextBoxMouseUp", e);
        }
        void baseItem_SplitMouseClick(object sender, MouseEventArgs e)
        {
            this.RelationEvent("SplitMouseClick(", e);
        }
        void baseItem_SplitMouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.RelationEvent("SplitMouseDoubleClick", e);
        }
        void baseItem_SplitMouseDown(object sender, MouseEventArgs e)
        {
            this.RelationEvent("SplitMouseDown", e);
        }
        void baseItem_SplitMouseMove(object sender, MouseEventArgs e)
        {
            this.RelationEvent("SplitMouseMove", e);
        }
        void baseItem_SplitMouseUp(object sender, MouseEventArgs e)
        {
            this.RelationEvent("SplitMouseUp", e);
        }
        #endregion

        protected override bool RefreshBaseItemState
        {
            get
            {
                return true;
            }
        }

        public override Size MeasureSize(Graphics g)
        {
            return this.DisplayRectangle.Size;
        }

        public override BaseItemState eBaseItemState
        {
            get
            {
                if (this.IsInputing || this.IsOpened) return BaseItemState.eHot;
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

        protected override void OnDraw(System.Windows.Forms.PaintEventArgs e)
        {
            //if (this.Height != CTR_HEIGHT) { ((ISetBaseItemHelper)this).SetSize(this.Width, CTR_HEIGHT); }
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
            //
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderCustomizeComboBox(
                new GISShare.Controls.WinForm.ObjectRenderEventArgs(e.Graphics, this, this.DisplayRectangle));
            if (!this.IsInputing)
            {
                GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderTextBoxText(
                    new TextRenderEventArgs(e.Graphics, this, this.Enabled, this.Text, this.ForeColor, this.Font, this.TextRectangle));
            }
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonArrow(
                new GISShare.Controls.WinForm.ArrowRenderEventArgs(e.Graphics, this, this.Enabled, ArrowStyle.eToDown, this.ForeColor, this.ArrowRectangle));
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            if (this.SplitRectangle.Contains(mevent.Location))
            {
                this.m_eSplitState = BaseItemState.ePressed;
                if (this.IsOpened) this.ClosePopup();
                else this.ShowPopup();
                this.OnSplitMouseDown(mevent);
            }
            else
            {
                if (this.eCustomizeComboBoxStyle == CustomizeComboBoxStyle.eDropDown &&
                    this.TextRectangle.Contains(mevent.Location))
                {
                    this.m_InputRegion.Show();
                }
                else if (this.eCustomizeComboBoxStyle == CustomizeComboBoxStyle.eDropDownList)
                {
                    if (!this.IsOpened) this.ShowPopup();
                }
            }
            base.OnMouseDown(mevent);
        }

        protected override void OnMouseMove(MouseEventArgs mevent)
        {
            if (this.SplitRectangle.Contains(mevent.Location))
            {
                if (this.eSplitState != BaseItemState.ePressed &&
                    this.eSplitState != BaseItemState.eHot) 
                { 
                    this.m_eSplitState = BaseItemState.eHot; 
                    this.Invalidate(this.SplitRectangle);
                }
                this.OnSplitMouseMove(mevent);
            }
            else
            {
                if (this.eSplitState != BaseItemState.ePressed &&
                    this.eSplitState != BaseItemState.eNormal)
                { 
                    this.m_eSplitState = BaseItemState.eNormal; 
                    this.Invalidate(this.SplitRectangle); 
                }
                this.OnTextBoxMouseMove(mevent);
            }
            //
            base.OnMouseMove(mevent);
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            if (this.DisplayRectangle.Contains(mevent.Location))
            {
                if (this.SplitRectangle.Contains(mevent.Location))
                { this.m_eSplitState = BaseItemState.eHot; this.OnSplitMouseUp(mevent); }
                else { this.OnTextBoxMouseUp(mevent); }
            }
            else
            {
                this.m_eSplitState = BaseItemState.eNormal;
            }
            //
            base.OnMouseUp(mevent);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (this.SplitRectangle.Contains(e.Location)) { this.OnSplitMouseClick(e); }
            else { this.OnTextBoxMouseClick(e); }
            //
            base.OnMouseClick(e);
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            if (this.SplitRectangle.Contains(e.Location))
            { this.OnSplitMouseDoubleClick(e); }
            else
            { this.OnTextBoxMouseDoubleClick(e); }
            //
            base.OnMouseDoubleClick(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            this.m_eSplitState = BaseItemState.eNormal;
            //
            base.OnMouseLeave(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            this.m_eSplitState = BaseItemState.eHot;
            //
            base.OnMouseEnter(e);
        }

        //
        protected virtual void OnPopupOpened(EventArgs e)
        {
            if (this.PopupOpened != null) this.PopupOpened(this, e);
        }

        protected virtual void OnPopupClosed(EventArgs e)
        {
            if (this.PopupClosed != null) this.PopupClosed(this, e);
        }

        protected virtual void OnTextBoxMouseDown(MouseEventArgs e)
        {
            if (this.TextBoxMouseDown != null) { this.TextBoxMouseDown(this, e); }
        }

        protected virtual void OnSplitMouseDown(MouseEventArgs e)
        {
            if (this.SplitMouseDown != null) { this.SplitMouseDown(this, e); }
        }

        protected virtual void OnTextBoxMouseMove(MouseEventArgs e)
        {
            if (this.TextBoxMouseMove != null) { this.TextBoxMouseMove(this, e); }
        }

        protected virtual void OnSplitMouseMove(MouseEventArgs e)
        {
            if (this.SplitMouseMove != null) { this.SplitMouseMove(this, e); }
        }

        protected virtual void OnTextBoxMouseUp(MouseEventArgs e)
        {
            if (this.TextBoxMouseUp != null) { this.TextBoxMouseUp(this, e); }
        }

        protected virtual void OnSplitMouseUp(MouseEventArgs e)
        {
            if (this.SplitMouseUp != null) { this.SplitMouseUp(this, e); }
        }

        protected virtual void OnTextBoxMouseClick(MouseEventArgs e)
        {
            if (this.TextBoxMouseClick != null) { this.TextBoxMouseClick(this, e); }
        }

        protected virtual void OnSplitMouseClick(MouseEventArgs e)
        {
            if (this.SplitMouseClick != null) { this.SplitMouseClick(this, e); }
        }

        protected virtual void OnTextBoxMouseDoubleClick(MouseEventArgs e)
        {
            if (this.TextBoxMouseDoubleClick != null) { this.TextBoxMouseDoubleClick(this, e); }
        }

        protected virtual void OnSplitMouseDoubleClick(MouseEventArgs e)
        {
            if (this.SplitMouseDoubleClick != null) { this.SplitMouseDoubleClick(this, e); }
        }
    }
}
