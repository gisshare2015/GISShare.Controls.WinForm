using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    /// <summary>
    /// 绘制型 功能区工具条
    /// </summary>
    public class RibbonBarItem : BaseItemStackItem, 
        IRibbonBarItem, IRibbonBarItemEvent, ISetRibbonBarHelper, IPopupOwner, IPopupOwnerHelper
    {
        //private const int CRT_HEIGHT = 76;           //
        private const int CRT_MINWIDTH = 49;           //
        private const int TITLEAREA_HEIGHT = 17;       //
        private const int GLYPH_HEIGHT = 14;           //
        private const int GLYPH_WIDTH = 15;            //
        private const int MINWIDTH = 49;               //

        private RibbonBarPopup m_RibbonBarPopup;

        #region IEventHelper
        protected override EventStateStyle GetEventStateSupplement(string strEventName)
        {
            switch (strEventName)
            {
                case "PopupOpened":
                    return this.PopupOpened != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "PopupClosed":
                    return this.PopupClosed != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "GlyphMouseDown":
                    return this.GlyphMouseDown != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "GlyphMouseMove":
                    return this.GlyphMouseMove != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "GlyphMouseUp":
                    return this.GlyphMouseUp != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "GlyphMouseClick":
                    return this.GlyphMouseClick != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "GlyphMouseDoubleClick":
                    return this.GlyphMouseDoubleClick != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
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
                case "GlyphMouseDown":
                    if (this.GlyphMouseDown != null) { this.GlyphMouseDown(this, e as MouseEventArgs); }
                    return true;
                case "GlyphMouseMove":
                    if (this.GlyphMouseMove != null) { this.GlyphMouseMove(this, e as MouseEventArgs); }
                    return true;
                case "GlyphMouseUp":
                    if (this.GlyphMouseUp != null) { this.GlyphMouseUp(this, e as MouseEventArgs); }
                    return true;
                case "GlyphMouseClick":
                    if (this.GlyphMouseClick != null) { this.GlyphMouseClick(this, e as MouseEventArgs); }
                    return true;
                case "GlyphMouseDoubleClick":
                    if (this.GlyphMouseDoubleClick != null) { this.GlyphMouseDoubleClick(this, e as MouseEventArgs); }
                    return true;
                default:
                    break;
            }
            //
            return base.RelationEventSupplement(strEventName, e);
        }
        #endregion

        #region 构造函数
        public RibbonBarItem()
        {
            this.m_Image = new System.Drawing.Bitmap(this.GetType().Assembly.GetManifestResourceStream("GISShare.Controls.WinForm.WFNew.Image.RibbonBarOverflow.ico"));
            //
            this.m_RibbonBarPopup = new RibbonBarPopup(this);
            ((ISetOwnerHelper)this.m_RibbonBarPopup).SetOwner(this);
            //
            this.m_RibbonBarPopup.PopupOpened += new EventHandler(RibbonBarPopup_PopupOpened);
            this.m_RibbonBarPopup.PopupClosed += new EventHandler(RibbonBarPopup_PopupClosed);
            //
            this.Padding = new Padding(3, 3, 3, 2);
        }
        void RibbonBarPopup_PopupOpened(object sender, EventArgs e)
        {
            this.Refresh();
            this.OnPopupOpened(e);
        }
        void RibbonBarPopup_PopupClosed(object sender, EventArgs e)
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

        public RibbonBarItem(Image image)
            : this()
        {
            this.Image = image;
        }

        //public RibbonBarItem(GISShare.Controls.Plugin.WFNew.IRibbonBarItemP pBaseItemP)
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
        //    //IBaseItemStackItemP
        //    //this.eOrientation = pBaseItemP.eOrientation;
        //    this.CanExchangeItem = pBaseItemP.CanExchangeItem;
        //    this.ReverseLayout = pBaseItemP.ReverseLayout;
        //    this.IsStretchItems = pBaseItemP.IsStretchItems;
        //    this.IsRestrictItems = pBaseItemP.IsRestrictItems;
        //    this.RestrictItemsWidth = pBaseItemP.RestrictItemsWidth;
        //    this.RestrictItemsHeight = pBaseItemP.RestrictItemsHeight;
        //    this.LineDistance = pBaseItemP.LineDistance;
        //    this.ColumnDistance = pBaseItemP.ColumnDistance;
        //    this.MinSize = pBaseItemP.MinSize;
        //    this.MaxSize = pBaseItemP.MaxSize;
        //    //IRibbonBarItemP
        //    this.LeftTopRadius = pBaseItemP.LeftTopRadius;
        //    this.RightTopRadius = pBaseItemP.RightTopRadius;
        //    this.LeftBottomRadius = pBaseItemP.LeftBottomRadius;
        //    this.RightBottomRadius = pBaseItemP.RightBottomRadius;
        //    this.ShowNomalState = pBaseItemP.ShowNomalState;
        //    this.Image = pBaseItemP.Image;
        //    this.GlyphEnabled = pBaseItemP.GlyphEnabled;
        //    this.GlyphVisible = pBaseItemP.GlyphVisible;
        //}
        #endregion

        #region IPopupOwnerHelper
        /// <summary>
        /// 获取其携带的 弹出项
        /// </summary>
        /// <returns></returns>
        IBasePopup IPopupOwnerHelper.GetBasePopup()
        {
            return this.m_RibbonBarPopup;
        }
        #endregion

        #region IPopupOwner
        [Browsable(true), Description("当其最小化时弹出其镜像Popup触发"), Category("弹出菜单")]
        public event EventHandler PopupOpened;
        [Browsable(true), Description("当其最小化时关闭其镜像Popup触发"), Category("弹出菜单")]
        public event EventHandler PopupClosed;

        private int m_PopupSpace = 0;
        [Browsable(true), DefaultValue(0), Description("弹出菜单与其携带者的间距"), Category("布局")]
        public int PopupSpace
        {
            get { return m_PopupSpace; }
            set { m_PopupSpace = value; }
        }

        [Browsable(false), Description("弹出菜单的坐标点（屏幕坐标）"), Category("布局")]
        public virtual Point PopupLoction
        {
            get
            {
                this.m_RibbonBarPopup.GetPopupPanel().TrySetPopupPanelSize(this.m_RibbonBarPopup.GetIdealSize());
                Rectangle rectangle = this.DisplayRectangle;
                Point point = this.PointToScreen(new Point(rectangle.Left, rectangle.Bottom));
                point.Y += PopupSpace;
                return point;
            }
        }

        [Browsable(false), Description("是否已展开弹出项"), Category("状态")]
        public bool IsOpened
        {
            get { return this.m_RibbonBarPopup.IsOpened; }
        }

        [Browsable(false), Description("是否自动触发弹出项"), Category("状态")]
        public bool IsAutoMouseTrigger
        {
            get { return false; }
        }

        [Browsable(false), Description("弹出菜单的激活区"), Category("布局")]
        public virtual Rectangle PopupTriggerRectangle
        {
            get { return this.DrawRectangle; }
        }

        /// <summary>
        /// 展开弹出项
        /// </summary>
        public void ShowPopup()
        {
            if (this.IsMinState && this.IsOpened) return;
            //
            this.m_RibbonBarPopup.Show(this.PopupLoction);
        }

        /// <summary>
        /// 关闭弹出项
        /// </summary>
        public void ClosePopup()
        {
            if (!this.IsOpened) return;
            //
            this.m_RibbonBarPopup.Close();
        }
        #endregion

        #region ISetRibbonBarHelper
        void ISetRibbonBarHelper.SetIsMinState(bool isMinState)
        { this.m_IsMinState = isMinState; }
        #endregion

        /// <summary>
        /// 布局方式 横向
        /// </summary>
        [Browsable(false), Description("其所携带的子项的布局方式"), Category("布局")]
        public override Orientation eOrientation
        {
            get { return Orientation.Horizontal; }
        }

        #region IRibbonBarItemEvent
        [Browsable(true), Description("鼠标在其Glyph区按下时触发"), Category("鼠标")]
        public event MouseEventHandler GlyphMouseDown;
        [Browsable(true), Description("鼠标在其Glyph区移动时触发"), Category("鼠标")]
        public event MouseEventHandler GlyphMouseMove;
        [Browsable(true), Description("鼠标在其Glyph区弹起时触发"), Category("鼠标")]
        public event MouseEventHandler GlyphMouseUp;
        [Browsable(true), Description("鼠标在其Glyph区单击时触发"), Category("鼠标")]
        public event MouseEventHandler GlyphMouseClick;
        [Browsable(true), Description("鼠标在其Glyph区双击时触发"), Category("鼠标")]
        public event MouseEventHandler GlyphMouseDoubleClick;
        #endregion

        #region IRibbonBarItem
        private Image m_Image = null;
        [Browsable(true), Description("最小化时显示的图片"), Category("外观")]
        public Image Image
        {
            get { return this.m_Image;}
            set { this.m_Image = value; }
        }

        private bool m_IsMinState = false;
        [Browsable(false), Description("是否处在最小化状态"), Category("状态")]
        public bool IsMinState
        {
            get { return m_IsMinState; }
        }

        #region Radius
        private int m_LeftTopRadius = 3;
        [Browsable(true), DefaultValue(3), Description("左顶部圆角值"), Category("圆角")]
        public virtual int LeftTopRadius
        {
            get { return m_LeftTopRadius; }
            set
            {
                if (value < 0) return;
                //
                m_LeftTopRadius = value;
            }
        }

        private int m_RightTopRadius = 3;
        [Browsable(true), DefaultValue(3), Description("右顶部圆角值"), Category("圆角")]
        public virtual int RightTopRadius
        {
            get { return m_RightTopRadius; }
            set
            {
                if (value < 0) return;
                //
                m_RightTopRadius = value;
            }
        }

        private int m_LeftBottomRadius = 3;
        [Browsable(true), DefaultValue(3), Description("左底部圆角值"), Category("圆角")]
        public virtual int LeftBottomRadius
        {
            get { return m_LeftBottomRadius; }
            set
            {
                if (value < 0) return;
                //
                m_LeftBottomRadius = value;
            }
        }

        private int m_RightBottomRadius = 3;
        [Browsable(true), DefaultValue(3), Description("右底部圆角值"), Category("圆角")]
        public virtual int RightBottomRadius
        {
            get { return m_RightBottomRadius; }
            set
            {
                if (value < 0) return;
                //
                m_RightBottomRadius = value;
            }
        }
        #endregion

        private bool m_ShowNomalState = false;
        [Browsable(true), DefaultValue(false), Description("是否显示正常状态下的状态"), Category("布局")]
        public bool ShowNomalState
        {
            get { return m_ShowNomalState; }
            set { m_ShowNomalState = value; }
        }

        private bool m_GlyphEnabled = true;
        [Browsable(true), DefaultValue(true), Description("Glyph区是否可用"), Category("状态")]
        public bool GlyphEnabled
        {
            get { return m_GlyphEnabled; }
            set { m_GlyphEnabled = value; }
        }

        private bool m_GlyphVisible = true;
        [Browsable(true), DefaultValue(true), Description("Glyph区是否可见"), Category("状态")]
        public bool GlyphVisible
        {
            get { return m_GlyphVisible; }
            set { m_GlyphVisible = value; }
        }

        private BaseItemState m_eGlyphState = BaseItemState.eNormal;
        [Browsable(false), Description("Glyph所处的状态（激活、按下、不可用、正常）"), Category("状态")]
        public virtual BaseItemState eGlyphState
        {
            get { return m_eGlyphState; }
        }
        protected virtual void SetGlyphState(BaseItemState glyphState)
        {
            if (this.eGlyphState == glyphState) return;
            //
            this.m_eGlyphState = glyphState;
        }
        protected virtual void SetGlyphStateEx(BaseItemState glyphState)
        {
            if (this.eGlyphState == glyphState) return;
            //
            this.m_eGlyphState = glyphState;
            //
            this.Invalidate(this.GlyphRectangle);
        }

        [Browsable(false), Description("其绘制矩形框"), Category("布局")]
        public Rectangle DrawRectangle
        {
            get
            {
                return this.DisplayRectangle;
            }
        }

        [Browsable(false), Description("其下部标题矩形框"), Category("布局")]
        public Rectangle TitleRectangle
        {
            get
            {
                Rectangle rectangle = this.DrawRectangle;
                return new Rectangle(rectangle.Left, rectangle.Bottom - TITLEAREA_HEIGHT, rectangle.Width, TITLEAREA_HEIGHT);
            }
        }

        [Browsable(false), Description("其Glyph矩形框"), Category("布局")]
        public Rectangle GlyphRectangle
        {
            get
            {
                Rectangle rectangle = this.DrawRectangle;
                return new Rectangle(rectangle.Right - GLYPH_WIDTH - 2, rectangle.Bottom - TITLEAREA_HEIGHT + 1, GLYPH_WIDTH, GLYPH_HEIGHT);
            }
        }

        /// <summary>
        /// 计算子项的合理尺寸
        /// </summary>
        /// <returns></returns>
        public Size GetItemsSize()
        {
            Size size = new Size(0, 0);
            foreach (BaseItem one in this.BaseItems)
            {
                size.Width += one.Width;
                if (size.Height < one.Height) size.Height = one.Height;
            }
            return size;
        }
        #endregion

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
                if (this.IsOpened) return BaseItemState.ePressed;
                return base.eBaseItemState;
            }
        }

        public override Rectangle ItemsRectangle
        {
            get
            {
                if (this.IsMinState) return Rectangle.Empty;
                //
                Rectangle rectangle = this.DrawRectangle;
                return new Rectangle(rectangle.X + this.Padding.Left,
                    rectangle.Y + this.Padding.Top,
                    rectangle.Width - this.Padding.Left - this.Padding.Right,
                    rectangle.Height - this.Padding.Top - this.Padding.Bottom - TITLEAREA_HEIGHT);
            }
        }

        [Browsable(false)]
        public override bool ReverseLayout
        {
            get
            {
                return false;
            }
            set
            {
                base.ReverseLayout = value;
            }
        }

        [Browsable(false)]
        public override bool LockWith
        {
            get
            {
                return true;
            }
            set
            {
                base.LockWith = value;
            }
        }

        public override bool Overflow
        {
            get
            {
                if (this.IsMinState) this.SetMinStateSize();
                return base.Overflow;
            }
        }

        #region Clone
        public override object Clone()
        {
            RibbonBarItem baseItem = new RibbonBarItem();
            baseItem.Checked = this.Checked;
            baseItem.Enabled = this.Enabled;
            baseItem.Font = this.Font;
            baseItem.ForeColor = this.ForeColor;
            baseItem.Name = this.Name;
            baseItem.Site = this.Site;
            baseItem.Size = this.Size;
            baseItem.Tag = this.Tag;
            baseItem.Text = this.Text;
            baseItem.Image = this.Image;
            baseItem.LeftBottomRadius = this.LeftBottomRadius;
            baseItem.LeftTopRadius = this.LeftTopRadius;
            baseItem.Padding = this.Padding;
            baseItem.RightBottomRadius = this.RightBottomRadius;
            baseItem.RightTopRadius = this.RightTopRadius;
            baseItem.ShowNomalState = this.ShowNomalState;
            baseItem.Visible = this.Visible;
            //
            baseItem.ColumnDistance = this.ColumnDistance;
            baseItem.GlyphEnabled = this.GlyphEnabled;
            baseItem.GlyphVisible = this.GlyphVisible;
            baseItem.IsStretchItems = this.IsStretchItems;
            baseItem.IsRestrictItems = this.IsRestrictItems;
            baseItem.PopupSpace = this.PopupSpace;
            foreach (BaseItem one in this.BaseItems)
            {
                baseItem.BaseItems.Add(one.Clone() as BaseItem);
            }
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
            if (this.GetEventState("PopupOpened") == EventStateStyle.eUsed) baseItem.PopupOpened += new EventHandler(baseItem_PopupOpened);
            if (this.GetEventState("PopupClosed") == EventStateStyle.eUsed) baseItem.PopupClosed += new EventHandler(baseItem_PopupClosed);
            if (this.GetEventState("GlyphMouseDown") == EventStateStyle.eUsed) baseItem.GlyphMouseDown += new MouseEventHandler(baseItem_GlyphMouseDown);
            if (this.GetEventState("GlyphMouseMove") == EventStateStyle.eUsed) baseItem.GlyphMouseMove += new MouseEventHandler(baseItem_GlyphMouseMove);
            if (this.GetEventState("GlyphMouseUp") == EventStateStyle.eUsed) baseItem.GlyphMouseUp += new MouseEventHandler(baseItem_GlyphMouseUp);
            if (this.GetEventState("GlyphMouseClick") == EventStateStyle.eUsed) baseItem.GlyphMouseClick += new MouseEventHandler(baseItem_GlyphMouseClick);
            if (this.GetEventState("GlyphMouseDoubleClick") == EventStateStyle.eUsed) baseItem.GlyphMouseDoubleClick += new MouseEventHandler(baseItem_GlyphMouseDoubleClick);
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
        void baseItem_PopupClosed(object sender, EventArgs e)
        {
            this.RelationEvent("PopupClosed", e);
        }
        void baseItem_PopupOpened(object sender, EventArgs e)
        {
            this.RelationEvent("PopupOpened", e);
        }
        void baseItem_GlyphMouseClick(object sender, MouseEventArgs e)
        {
            this.RelationEvent("GlyphMouseClick", e);
        }
        void baseItem_GlyphMouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.RelationEvent("GlyphMouseDoubleClick", e);
        }
        void baseItem_GlyphMouseDown(object sender, MouseEventArgs e)
        {
            this.RelationEvent("GlyphMouseDown", e);
        }
        void baseItem_GlyphMouseMove(object sender, MouseEventArgs e)
        {
            this.RelationEvent("GlyphMouseMove", e);
        }
        void baseItem_GlyphMouseUp(object sender, MouseEventArgs e)
        {
            this.RelationEvent("GlyphMouseUp", e);
        }
        #endregion

        public override Size MeasureSize(Graphics g)//有待完善
        {
            return DisplayRectangle.Size;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            //
            if (this.IsMinState)
            {
                if (this.PopupTriggerRectangle.Contains(e.Location))
                {
                    this.ShowPopup();
                    return;
                }
            }
            else
            {
                if (this.GlyphVisible && this.GlyphEnabled && this.GlyphRectangle.Contains(e.Location))
                {
                    this.SetGlyphState(BaseItemState.ePressed);
                    this.OnGlyphMouseDown(e);
                    return;
                }
                if (this.DrawRectangle.Contains(e.Location))
                {
                    this.SetGlyphState(BaseItemState.eNormal);
                    return;
                }
                this.SetGlyphState(BaseItemState.eNormal);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            //
            if (!this.IsMinState)
            {
                if (this.GlyphVisible && this.GlyphEnabled && this.GlyphRectangle.Contains(e.Location))
                {
                    this.SetGlyphStateEx(BaseItemState.eHot);
                    this.OnGlyphMouseMove(e);
                    return;
                }
                if (this.DrawRectangle.Contains(e.Location))
                {
                    this.SetGlyphStateEx(BaseItemState.eNormal);
                    return;
                }
                this.SetGlyphStateEx(BaseItemState.eNormal);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            //
            if (!this.IsMinState)
            {
                if (this.GlyphVisible && this.GlyphEnabled && this.GlyphRectangle.Contains(e.Location))
                {
                    this.SetGlyphState(BaseItemState.eHot);
                    this.OnGlyphMouseUp(e);
                    return;
                }
                if (this.DrawRectangle.Contains(e.Location))
                {
                    this.SetGlyphState(BaseItemState.eNormal);
                    return;
                }
                this.SetGlyphState(BaseItemState.eNormal);
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (!this.IsMinState)
            {
                if (this.GlyphVisible && this.GlyphEnabled && this.GlyphRectangle.Contains(e.Location))
                {
                    this.OnGlyphMouseClick(e);
                }
            }
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            if (!this.IsMinState)
            {
                if (this.GlyphVisible && this.GlyphEnabled && this.GlyphRectangle.Contains(e.Location))
                {
                    this.OnGlyphMouseDoubleClick(e);
                }
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            //
            this.SetGlyphState(BaseItemState.eNormal);
        }

        protected override void MessageMonitor(MessageInfo messageInfo)
        {
            switch (messageInfo.eMessageStyle)
            {
                case MessageStyle.eMSViewInfo: 
                    ViewInfo viewInfo = messageInfo.MessageParameter as ViewInfo;
                    if (viewInfo != null)
                    {
                        base.MessageMonitor
                            (
                            new MessageInfo
                                (
                                this,
                                MessageStyle.eMSViewInfo,
                                new ViewInfo(viewInfo.Visible && !this.IsMinState, viewInfo.Enabled, viewInfo.Overflow)
                                )
                            );
                    }
                    break;
                default:
                    if (!this.IsMinState) base.MessageMonitor(messageInfo);
                    break;
            }
        }

        protected override void OnDraw(PaintEventArgs e)
        {
            Rectangle rectangleText;
            Rectangle rectangle = this.TitleRectangle;
            Size size = e.Graphics.MeasureString(this.Text, this.Font).ToSize();
            this.m_TempWidth = (int)size.Width + 1;
            if (this.IsMinState)
            {
                this.SetMinStateSize();
                //
                rectangleText = new Rectangle(rectangle.X + (rectangle.Width - this.m_TempWidth) / 2, rectangle.Y - size.Height - 10, this.m_TempWidth, rectangle.Height - 1);
                //
                rectangleText = this.GetRectangle(rectangleText, this.DisplayRectangle);
            }
            else
            {
                this.Relayout(e.Graphics, LayoutStyle.eLayoutPlan, true);
                this.Relayout(e.Graphics, LayoutStyle.eLayoutAuto, false);
                //
                rectangleText = new Rectangle(rectangle.X + (rectangle.Width - this.m_TempWidth) / 2, rectangle.Y, this.m_TempWidth, rectangle.Height - 1);
                //
                rectangleText = this.GetRectangle(rectangleText, this.TitleRectangle);
            }
            //
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonBar(
                new GISShare.Controls.WinForm.ObjectRenderEventArgs(e.Graphics, this, this.DisplayRectangle));
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonText(
                new GISShare.Controls.WinForm.TextRenderEventArgs(e.Graphics, this, this.Enabled, this.HaveShadow, this.Text, this.ForeCustomize,  this.ForeColor, this.ShadowColor, this.Font, rectangleText));
            if (this.IsMinState)
            {
                GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonArrow(
                    new GISShare.Controls.WinForm.ArrowRenderEventArgs(
                    e.Graphics,
                    this,
                    this.Enabled,
                    ArrowStyle.eToDown,
                    this.ForeColor,
                    new Rectangle(rectangle.X + (rectangle.Width - 5) / 2, rectangle.Y - 2, 5, 4))
                    );
            }
        }
        private Rectangle GetRectangle(Rectangle rectangleObject, Rectangle rectangleStandard)
        {
            int iL = rectangleStandard.Left;
            int iR = rectangleStandard.Right;
            int iT = rectangleStandard.Top;
            int iB = rectangleStandard.Bottom;
            //
            if (iL < rectangleObject.Left) iL = rectangleObject.Left;
            if (iR > rectangleObject.Right) iR = rectangleObject.Right;
            if (iT < rectangleObject.Top) iT = rectangleObject.Top;
            if (iB > rectangleObject.Bottom) iB = rectangleObject.Bottom;
            //
            return Rectangle.FromLTRB(iL, iT, iR, iB);
        }
        private int m_TempWidth = 0;
        private void SetMinStateSize()
        {
            if (this.m_TempWidth < MINWIDTH)
            {
                this.Size = new Size(MINWIDTH, this.Height);
            }
            else
            {
                this.Size = new Size(this.Padding.Left + this.m_TempWidth + this.Padding.Right, this.Height);
            }
        }
        protected override Size Relayout(Graphics g, LayoutStyle eLayoutStyle, bool bSetSize)
        {
            if (this.BaseItems.Count <= 0) return this.DisplayRectangle.Size;
            //
            if (!this.BaseItems.OwnerEquals(this)) ((ISetOwnerHelper)this.BaseItems).SetOwner(this);//key
            //
            Rectangle rectangle = this.DisplayRectangle;
            Rectangle itemsRectangle = this.ItemsRectangle;
            //
            Size size = GISShare.Controls.WinForm.WFNew.LayoutEngine.LayoutStackH_LT(g, this, 0, this.IsStretchItems, this.IsRestrictItems,
                this.ColumnDistance, -1, -1,
                itemsRectangle.Left - rectangle.Left, itemsRectangle.Top - rectangle.Top, rectangle.Right - itemsRectangle.Right, rectangle.Bottom - itemsRectangle.Bottom,
                //this.Padding.Left, this.Padding.Top, this.Padding.Right, this.Padding.Bottom,
                CRT_MINWIDTH, -1, eLayoutStyle, ref this._OverflowItemsCount, ref this._DrawItemsCount);
            //
            if (!bSetSize) return size;
            //
            this.Size = new Size(this.LockWith ? size.Width : this.DisplayRectangle.Width, this.LockHeight ? size.Height : this.DisplayRectangle.Height);
            //
            return size;
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

        protected virtual void OnGlyphMouseDown(MouseEventArgs e)
        {
            if (this.GlyphMouseDown != null) { this.GlyphMouseDown(this, e); }
        }

        protected virtual void OnGlyphMouseMove(MouseEventArgs e)
        {
            if (this.GlyphMouseMove != null) { this.GlyphMouseMove(this, e); }
        }

        protected virtual void OnGlyphMouseUp(MouseEventArgs e)
        {
            if (this.GlyphMouseUp != null) { this.GlyphMouseUp(this, e); }
        }

        protected virtual void OnGlyphMouseClick(MouseEventArgs e)
        {
            if (this.GlyphMouseClick != null) { this.GlyphMouseClick(this, e); }
        }

        protected virtual void OnGlyphMouseDoubleClick(MouseEventArgs e)
        {
            if (this.GlyphMouseDoubleClick != null) { this.GlyphMouseDoubleClick(this, e); }
        }

    }
}
