using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    [Designer(typeof(GISShare.Controls.WinForm.WFNew.Design.RibbonBarDesigner)), ToolboxItem(false)]
    class RibbonBarMirrorPopupPanel : BaseItemStack, IRibbonBarItem, IPopupPanel//, ISetRibbonBarHelper, IPopupOwner, IPopupOwnerHelper
    {
        private const int CRT_HEIGHT = 76;             //
        private const int CRT_MINWIDTH = 49;           //
        private const int TITLEAREA_HEIGHT = 17;       //
        private const int GLYPH_HEIGHT = 14;           //
        private const int GLYPH_WIDTH = 15;            //
        //private const int MINWIDTH = 49;             //

        private IRibbonBarItem m_pRibbonBarItem;

        #region IEventHelper
        protected override EventStateStyle GetEventStateSupplement(string strEventName)
        {
            switch (strEventName)
            {
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

        public RibbonBarMirrorPopupPanel(IRibbonBarItem ribbonBarItem)
        {
            this.m_pRibbonBarItem = ribbonBarItem;
            ((ISetOwnerHelper)this).SetOwner(ribbonBarItem as IOwner);
            //
            base.Width = this.m_pRibbonBarItem.Width;
            base.Height = this.m_pRibbonBarItem.Height;
            base.BackColor = System.Drawing.Color.Transparent;
        }

        #region IPopupPanel
        public void TrySetPopupPanelSize(Size size)
        {
            this.Size = size;
        }
        #endregion

        protected override bool RefreshBaseItemState
        {
            get
            {
                return true;
            }
        }

        [Browsable(false), Description("����Я��������Ĳ��ַ�ʽ"), Category("����")]
        public override Orientation eOrientation
        {
            get { return this.m_pRibbonBarItem.eOrientation; }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Description("����Я���������"), Category("����")]
        public override BaseItemCollection BaseItems
        {
            get
            {
                return this.m_pRibbonBarItem.BaseItems;
            }
        }

        public override Padding Padding
        {
            get
            {
                return this.m_pRibbonBarItem.Padding;
            }
            set { base.Padding = value; }
        }

        public override Font Font
        {
            get
            {
                return this.m_pRibbonBarItem.Font;
            }
            set
            {
                base.Font = value;
            }
        }

        public override Color ForeColor
        {
            get
            {
                return this.m_pRibbonBarItem.ForeColor;
            }
            set
            {
                base.ForeColor = value;
            }
        }

        public override string Text
        {
            get
            {
                return this.m_pRibbonBarItem.Text;
            }
            set
            {
                base.Text = value;
            }
        }

        #region IRibbonBarItem
        [Browsable(true), Description("�������Glyph������ʱ����"), Category("���")]
        public event MouseEventHandler GlyphMouseDown;
        [Browsable(true), Description("�������Glyph���ƶ�ʱ����"), Category("���")]
        public event MouseEventHandler GlyphMouseMove;
        [Browsable(true), Description("�������Glyph������ʱ����"), Category("���")]
        public event MouseEventHandler GlyphMouseUp;
        [Browsable(true), Description("�������Glyph������ʱ����"), Category("���")]
        public event MouseEventHandler GlyphMouseClick;
        [Browsable(true), Description("�������Glyph��˫��ʱ����"), Category("���")]
        public event MouseEventHandler GlyphMouseDoubleClick;

        [Browsable(true), DefaultValue(true), Description("�Ƿ���������"), Category("����")]
        public override bool IsRestrictItems
        {
            get 
            {
                return this.m_pRibbonBarItem.IsRestrictItems;
            }
            set { base.IsRestrictItems = value; }
        }

        [Browsable(true), DefaultValue(true), Description("�Ƿ���������"), Category("����")]
        public override  bool IsStretchItems
        {
            get
            {
                return this.m_pRibbonBarItem.IsStretchItems;
            }
            set { base.IsStretchItems = value; }
        }

        [Browsable(true), Description("��С��ʱ��ʾ��ͼƬ"), Category("���")]
        public Image Image
        {
            get
            {
                return this.m_pRibbonBarItem.Image; 
            }
            set {  }
        }

        [Browsable(false), Description("�Ƿ�����С��״̬"), Category("״̬")]
        public bool IsMinState
        {
            get { return false; }
        }

        [Browsable(true), DefaultValue(1), Description("��������ʱ���м��"), Category("����")]
        public override int ColumnDistance
        {
            get
            {
                return this.m_pRibbonBarItem.ColumnDistance;         
            }
            set { base.ColumnDistance = value; }
        }

        #region Radius
        public virtual int LeftTopRadius
        {
            get
            {
                return this.m_pRibbonBarItem.LeftTopRadius;
            }
            set { }
        }

        public virtual int RightTopRadius
        {
            get
            {
                return this.m_pRibbonBarItem.RightTopRadius;
            }
            set { }
        }

        public virtual int LeftBottomRadius
        {
            get
            {
                return this.m_pRibbonBarItem.LeftBottomRadius;
            }
            set { }
        }

        public virtual int RightBottomRadius
        {
            get
            {
                return this.m_pRibbonBarItem.RightBottomRadius;
            }
            set { }
        }
        #endregion

        [Browsable(true), DefaultValue(true), Description("�Ƿ���ʾ����״̬�µ�״̬"), Category("����")]
        public bool ShowNomalState
        {
            get
            {
                return this.m_pRibbonBarItem.ShowNomalState;
            }
            set {  }
        }

        [Browsable(true), DefaultValue(true), Description("Glyph���Ƿ����"), Category("״̬")]
        public bool GlyphEnabled
        {
            get
            {
                return this.m_pRibbonBarItem.GlyphEnabled;
            }
            set { }
        }

        [Browsable(true), DefaultValue(true), Description("Glyph���Ƿ�ɼ�"), Category("״̬")]
        public bool GlyphVisible
        {
            get
            {
                return this.m_pRibbonBarItem.GlyphVisible;
            }
            set { }
        }

        private BaseItemState m_eGlyphState = BaseItemState.eNormal;
        [Browsable(false), Description("�Ƿ�����С��״̬"), Category("״̬")]
        public BaseItemState eGlyphState
        {
            get { return m_eGlyphState; }
        }
        private void SetGlyphState(BaseItemState glyphState)
        {
            if (this.eGlyphState == glyphState) return;
            //
            this.m_eGlyphState = glyphState;
        }
        private void SetGlyphStateEx(BaseItemState glyphState)
        {
            if (this.eGlyphState == glyphState) return;
            //
            this.m_eGlyphState = glyphState;
            //
            this.Invalidate(this.GlyphRectangle);
        }

        [Browsable(false), Description("����ƾ��ο�"), Category("����")]
        public Rectangle DrawRectangle
        {
            get 
            {
                return this.DisplayRectangle;
            }
        }

        [Browsable(false), Description("������չ�־���"), Category("����")]
        public override Rectangle ItemsRectangle
        {
            get
            {
                Rectangle rectangle = this.DrawRectangle;
                return new Rectangle(rectangle.X + this.Padding.Left,
                    rectangle.Y + this.Padding.Top,
                    rectangle.Width - this.Padding.Left - this.Padding.Right,
                    rectangle.Height - this.Padding.Top - this.Padding.Bottom - TITLEAREA_HEIGHT);
            }
        }

        [Browsable(false), Description("���²�������ο�"), Category("����")]
        public Rectangle TitleRectangle
        {
            get
            {
                Rectangle rectangle = this.DrawRectangle;
                return new Rectangle(rectangle.X, rectangle.Bottom - TITLEAREA_HEIGHT, rectangle.Width, TITLEAREA_HEIGHT);
            }
        }

        [Browsable(false), Description("��Glyph���ο�"), Category("����")]
        public Rectangle GlyphRectangle
        {
            get
            {
                Rectangle rectangle = this.DrawRectangle;
                return new Rectangle(rectangle.Right - GLYPH_WIDTH - 2, rectangle.Bottom - TITLEAREA_HEIGHT + 1, GLYPH_WIDTH, GLYPH_HEIGHT);
            }
        }
        #endregion

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

        #region Clone
        public override object Clone()
        {
            RibbonBarMirrorPopupPanel baseItem = new RibbonBarMirrorPopupPanel(this.m_pRibbonBarItem);
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
            //foreach (BaseItem one in this.BaseItems)
            //{
            //    baseItem.BaseItems.Add(one.Clone() as BaseItem);
            //}
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

        public virtual Size MeasureSize(Graphics g)
        {
            return this.DisplayRectangle.Size;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            //
            if (!this.IsMinState)
            {
                if (this.GlyphVisible && this.GlyphEnabled && this.GlyphRectangle.Contains(e.Location))
                {
                    this.SetGlyphState(BaseItemState.ePressed);
                    this.OnGlyphMouseDown(e);
                    return;
                }
                if (this.DrawRectangle.Contains(e.Location))
                {
                    this.SetGlyphStateEx(BaseItemState.eNormal);
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
            if (!this.IsMinState)
            {
                this.SetGlyphState(BaseItemState.eNormal);
            }
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            //
            //if (this.Height < CRT_HEIGHT) this.Height = CRT_HEIGHT;
            this.Height = this.m_pRibbonBarItem.Height;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            this.OnDraw(e);
            //
            base.OnPaint(e);
        }

        protected override void OnDraw(PaintEventArgs e)
        {
            Rectangle rectangleText;
            Rectangle rectangle = this.TitleRectangle;
            Size size = e.Graphics.MeasureString(this.Text, this.Font).ToSize();
            int iWidth = (int)size.Width + 1;
            if (this.IsMinState)
            {
                if (iWidth < CRT_MINWIDTH) this.Width = CRT_MINWIDTH;
                else this.Width = this.Padding.Left + iWidth + this.Padding.Right;
                //
                rectangleText =
                    new Rectangle(rectangle.X + (rectangle.Width - iWidth) / 2, rectangle.Y - size.Height - 10, iWidth, rectangle.Height - 1);
                //
                rectangleText = this.GetRectangle(rectangleText, this.DisplayRectangle);
            }
            else
            {
                this.Relayout(e.Graphics, LayoutStyle.eLayoutPlan, true);
                this.Relayout(e.Graphics, LayoutStyle.eLayoutAuto, false);
                //
                rectangleText =
                    new Rectangle(rectangle.X + (rectangle.Width - iWidth) / 2, rectangle.Y + 1, iWidth, rectangle.Height - 1);
                //
                rectangleText = this.GetRectangle(rectangleText, this.TitleRectangle);
            }
            //
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonBar(
                new GISShare.Controls.WinForm.ObjectRenderEventArgs(e.Graphics, this, this.DisplayRectangle));
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonText(
                new GISShare.Controls.WinForm.TextRenderEventArgs(e.Graphics, this, this.Enabled, this.Text, this.ForeColor, this.Font, rectangleText));
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
            if (iR > rectangleObject.Right) iR= rectangleObject.Right;
            if (iT < rectangleObject.Top) iT = rectangleObject.Top;
            if (iB > rectangleObject.Bottom) iB = rectangleObject.Bottom;
            //
            return Rectangle.FromLTRB(iL, iT, iR, iB);
        }
        protected override Size Relayout(Graphics g, LayoutStyle eLayoutStyle, bool bSetSize)
        {
            if (this.BaseItems.Count <= 0) return this.Size;
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
            if (!size.IsEmpty)
            {
                if (this.LockWith && this.Dock != DockStyle.Top && this.Dock != DockStyle.Bottom && this.Dock != DockStyle.Fill)
                {
                    this.Width = size.Width;
                }
                if (this.LockHeight && this.Dock != DockStyle.Left && this.Dock != DockStyle.Right && this.Dock != DockStyle.Fill)
                {
                    this.Height = size.Height;
                }
            }
            //
            return size;
        }
        
        //

        protected virtual void OnGlyphMouseDown(MouseEventArgs e)
        {
            if (this.GlyphMouseDown != null)
            {
                this.GlyphMouseDown(this, e);
                //
                IEventHelper pEventHelper = this.m_pRibbonBarItem as IEventHelper;
                if (pEventHelper != null)
                {
                    pEventHelper.RelationEvent("GlyphMouseDown", e);
                }
            }
        }

        protected virtual void OnGlyphMouseMove(MouseEventArgs e)
        {
            if (this.GlyphMouseMove != null)
            {
                this.GlyphMouseMove(this, e);
            }
        }

        protected virtual void OnGlyphMouseUp(MouseEventArgs e)
        {
            if (this.GlyphMouseUp != null)
            {
                this.GlyphMouseUp(this, e);
                //
                IEventHelper pEventHelper = this.m_pRibbonBarItem as IEventHelper;
                if (pEventHelper != null)
                {
                    pEventHelper.RelationEvent("GlyphMouseUp", e);
                }
            }
        }

        protected virtual void OnGlyphMouseClick(MouseEventArgs e)
        {
            if (this.GlyphMouseClick != null)
            {
                this.GlyphMouseClick(this, e);
                //
                IEventHelper pEventHelper = this.m_pRibbonBarItem as IEventHelper;
                if (pEventHelper != null)
                {
                    pEventHelper.RelationEvent("GlyphMouseClick", e);
                }
            }
        }

        protected virtual void OnGlyphMouseDoubleClick(MouseEventArgs e)
        {
            if (this.GlyphMouseDoubleClick != null)
            {
                this.GlyphMouseDoubleClick(this, e);
                //
                IEventHelper pEventHelper = this.m_pRibbonBarItem as IEventHelper;
                if (pEventHelper != null)
                {
                    pEventHelper.RelationEvent("GlyphMouseDoubleClick", e);
                }
            }
        }
    }
}

