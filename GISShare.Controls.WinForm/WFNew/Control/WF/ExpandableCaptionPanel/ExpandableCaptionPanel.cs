using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    [ToolboxItem(true), Designer(typeof(GISShare.Controls.WinForm.WFNew.Design.ExpandableCaptionPanelDesigner)), ToolboxBitmap(typeof(ExpandableCaptionPanel), "ExpandableCaptionPanel.bmp")]
    public class ExpandableCaptionPanel : WFNew.AreaControl, WFNew.IOwner, WFNew.ICollectionItem, IExpandableCaptionPanel, ISetExpandableCaptionPanelHelper//WFNew.IBaseItemOwner2
    {
        private const int CTR_IMAGESIZE = 16;
        private const int CTR_IMAGESPACE = 1;

        public event EventHandler ExpandChanged = null;

        //
        private TreeNodeButtonECPItem m_TreeNodeButtonItem;
        ExpandableCaptionPanelButtonStackItem m_ExpandableCaptionPanelButtonStackItem;

        private WFNew.BaseItemCollection m_BaseItemCollection;
        public ExpandableCaptionPanel()
            : base()
        {
            this.m_BaseItemCollection = new BaseItemCollection(this);
            //
            this.m_TreeNodeButtonItem = new TreeNodeButtonECPItem();
            this.m_BaseItemCollection.Add(this.m_TreeNodeButtonItem);
            this.m_ExpandableCaptionPanelButtonStackItem = new ExpandableCaptionPanelButtonStackItem();
            this.m_BaseItemCollection.Add(this.m_ExpandableCaptionPanelButtonStackItem);
            ((WFNew.ILockCollectionHelper)this.m_BaseItemCollection).SetLocked(true);
            //
            base.ShowOutLine = true;
            base.ShowBackground = true;
            base.Dock = DockStyle.Left;
            this.m_ExpandSize = this.Size;
        }

        #region IArea
        [Browsable(true), DefaultValue(true), Description("显示外框线"), Category("外观")]
        public override bool ShowOutLine
        {
            get { return base.ShowOutLine; }
            set { base.ShowOutLine = value; }
        }

        [Browsable(true), DefaultValue(true), Description("显示背景色"), Category("外观")]
        public override bool ShowBackground
        {
            get { return base.ShowBackground; }
            set { base.ShowBackground = value; }
        }
        #endregion

        #region IOwner
        public override bool LockHeight
        {
            get { return false; }
        }

        public override bool LockWith
        {
            get { return false; }
        }

        public override object Clone()
        {
            return new ExpandableCaptionPanel();
        }
        #endregion

        #region IBaseItemOwner
        [Browsable(false), Description("其子项展现矩形"), Category("布局")]
        public override Rectangle ItemsRectangle
        {
            get { return new Rectangle(0, 0, this.Width, this.Height); }
        }
        #endregion

        #region ICollectionItem
        [Browsable(false), Description("其所携带的子项集合中是否存在可见项"), Category("状态")]
        bool WFNew.ICollectionItem.HaveVisibleBaseItem
        {
            get
            {
                foreach (WFNew.BaseItem one in ((WFNew.ICollectionItem)this).BaseItems)
                {
                    if (one.Visible) return true;
                }
                //
                return false;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Description("其所携带的子项集合"), Category("子项")]
        WFNew.BaseItemCollection WFNew.ICollectionItem.BaseItems
        {
            get { return m_BaseItemCollection; }
        }
        #endregion

        #region IExpandableCaptionPanel
        [Browsable(false), DefaultValue(false), Description("是否激活"), Category("状态")]
        public virtual bool bActive
        {
            get 
            {
                return this.Enabled;
            }
        }
        
        bool m_IsExpand = true;
        [Browsable(false), DefaultValue(true), Description("是否展开"), Category("状态")]
        public bool IsExpand
        {
            get { return m_IsExpand; }
        }

        [Browsable(false), Description("自动管理展开功能"), Category("状态")]
        public virtual bool AutoSetIsExpand
        {
            get { return true; }
        }

        [Browsable(false), Description("是否存在展开按钮"), Category("状态")]
        public bool HaveExpandButton
        {
            get
            {
                return this.ShowTreeNodeButton || this.ShowExpandButton;
            }
        }

        private bool m_ShowCaption = true;
        [Browsable(true), DefaultValue(true), Description("显示标题区"), Category("布局")]
        public virtual bool ShowCaption
        {
            get { return m_ShowCaption; }
            set
            {
                if (!this.IsExpand) return;
                //
                if (m_ShowCaption == value) return;
                //
                m_ShowCaption = value;
            }
        }

        private bool m_IsSimplyDrawCaption = false;
        [Browsable(true), DefaultValue(false), Description("简单绘制标题区"), Category("外观")]
        public bool IsSimplyDrawCaption
        {
            get { return m_IsSimplyDrawCaption; }
            set { m_IsSimplyDrawCaption = value; }
        }

        private int m_CaptionHeight = 21;
        [Browsable(true), DefaultValue(21), Description("显示标题区高"), Category("布局")]
        public int CaptionHeight
        {
            get { return m_CaptionHeight; }
            set
            {
                if (value < 17) return;
                //switch (this.eCaptionAlignment)
                //{
                //    case TabAlignment.Bottom:
                //    case TabAlignment.Top:
                //        if (this.ShowFrame) { if (value > this.Height - 2) return; }
                //        break;
                //    case TabAlignment.Left:
                //    case TabAlignment.Right:
                //    default:
                //        if (this.ShowFrame) { if (value > this.Width - 2) return; }
                //        break;
                //}
                ////
                m_CaptionHeight = value;
            }
        }

        //private bool m_ShowFrame = true;
        //[Browsable(true), DefaultValue(true)]
        //public bool ShowFrame
        //{
        //    get { return m_ShowFrame; }
        //    set { m_ShowFrame = value; }
        //}

        bool m_ShowCloseButton = true;
        [Browsable(true), DefaultValue(true), Description("显示关闭按钮"), Category("状态")]
        public bool ShowCloseButton
        {
            get { return m_ShowCloseButton; }
            set
            {
                if (!this.ShowCaption) return;
                //
                if (m_ShowCloseButton == value) return;
                //
                m_ShowCloseButton = value;
            }
        }

        bool m_ShowExpandButton = true;
        [Browsable(true), DefaultValue(true), Description("显示展开区"), Category("状态")]
        public bool ShowExpandButton
        {
            get 
            {
                return m_ShowExpandButton; 
            }
            set
            {
                if (!this.ShowCaption) return;
                //
                if (m_ShowExpandButton == value) return;
                //
                m_ShowExpandButton = value;
            }
        }

        bool m_ShowTreeNodeButton = true;
        [Browsable(true), DefaultValue(false), Description("显示节点区"), Category("状态")]
        public bool ShowTreeNodeButton
        {
            get
            {
                return m_ShowTreeNodeButton; 
            }
            set
            {
                if (!this.ShowCaption) return;
                //
                if (m_ShowTreeNodeButton == value) return;
                //
                m_ShowTreeNodeButton = value;
            }
        }

        #region Radius
        [Browsable(false), Description("是否使用圆角"), Category("圆角")]
        public bool UseRadius
        {
            get
            {
                if (
                    (this.LeftTopRadius < 0 || this.RightTopRadius < 0 || this.LeftBottomRadius < 0 || this.RightBottomRadius < 0) ||
                    (this.LeftTopRadius == 0 && this.RightTopRadius == 0 && this.LeftBottomRadius == 0 && this.RightBottomRadius == 0)
                    )
                    return false;
                else
                    return true;
            }
        }

        private int m_LeftTopRadius = 0;
        [Browsable(true), DefaultValue(0), Description("左顶部圆角值"), Category("圆角")]
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

        private int m_RightTopRadius = 0;
        [Browsable(true), DefaultValue(0), Description("右顶部圆角值"), Category("圆角")]
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

        private int m_LeftBottomRadius = 0;
        [Browsable(true), DefaultValue(0), Description("左底部圆角值"), Category("圆角")]
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

        private int m_RightBottomRadius = 0;
        [Browsable(true), DefaultValue(0), Description("右底部圆角值"), Category("圆角")]
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

        bool m_UseMaxMinStyle = false;
        [Browsable(true), DefaultValue(false), Description("展开节点区显示为最大最小按钮类型"), Category("状态")]
        public bool UseMaxMinStyle
        {
            get { return m_UseMaxMinStyle; }
            set { m_UseMaxMinStyle = value; }
        }

        private bool m_DisplayCaptionState = false;     //记录激活状态
        [Browsable(false), DefaultValue(false), Description("启用标题区状态"), Category("状态")]
        public bool DisplayCaptionState
        {
            get { return m_DisplayCaptionState; }
            set { m_DisplayCaptionState = value; }
        }

        private Image m_Image = null;
        [Browsable(true), Description("图片"), Category("外观")]
        public Image Image//图标
        {
            get { return m_Image; }
            set { m_Image = value; }
        }

        private Size m_ExpandSize = new Size(120, 120);
        protected virtual Size GetExpandSize()
        {
            return m_ExpandSize;
        }

        private WFNew.BaseItemState m_eCaptionState = WFNew.BaseItemState.eNormal;
        protected virtual bool SetCaptionState(BaseItemState baseItemState)
        {
            if (this.m_eCaptionState == baseItemState) return false;
            this.m_eCaptionState = baseItemState;
            return true;
        }
        protected virtual bool SetCaptionStateEx(BaseItemState baseItemState)
        {
            if (this.m_eCaptionState == baseItemState) return false;
            this.m_eCaptionState = baseItemState;
            this.Invalidate(this.CaptionRectangle);
            return true;
        }
        [Browsable(false), Description("标题区所处的状态（激活、按下、不可用、正常）"), Category("状态")]
        public BaseItemState eCaptionState
        {
            get { return m_eCaptionState; }
        }

        private TabAlignment m_eCaptionAlignment = TabAlignment.Top;
        [Browsable(true), DefaultValue(TabAlignment.Top), Description("标题区布局"), Category("布局")]
        public virtual TabAlignment eCaptionAlignment
        {
            get { return m_eCaptionAlignment; }
            set { m_eCaptionAlignment = value; }
        }

        ExpandButtonStyle m_eExpandButtonStyle = ExpandButtonStyle.eLeftToRight;
        [Browsable(true), DefaultValue(ExpandButtonStyle.eLeftToRight), Description("伸展按钮类型"), Category("属性")]
        public virtual ExpandButtonStyle eExpandButtonStyle
        {
            get { return m_eExpandButtonStyle; }
            set
            {
                if (this.Dock == DockStyle.None || this.Dock == DockStyle.Fill)
                {
                    m_eExpandButtonStyle = value;
                }
            }
        }

        //[Browsable(false)]
        //public Rectangle FrameRectangle
        //{
        //    get
        //    {
        //        return new Rectangle(0, 0, base.DisplayRectangle.Width - 1, base.DisplayRectangle.Height - 1);
        //    }
        //}

        [Browsable(false), Description("标题矩形框"), Category("布局")]
        public Rectangle CaptionRectangle//标题矩形框
        {
            get
            {
                if (!this.ShowCaption) { return new Rectangle(0, 0, 0, 0); }
                //
                //if (this.ShowOutLine)
                //{
                //    switch (this.eCaptionAlignment)
                //    {
                //        case TabAlignment.Left:
                //            return new Rectangle(1, 1, this.CaptionHeight, this.Height - 2);
                //        case TabAlignment.Right:
                //            return new Rectangle(this.Width - 1 - this.CaptionHeight, 1, this.CaptionHeight, this.Height - 2);
                //        case TabAlignment.Bottom:
                //            return new Rectangle(1, this.Height - 1 - this.CaptionHeight, this.Width - 2, this.CaptionHeight);
                //        case TabAlignment.Top:
                //        default:
                //            return new Rectangle(1, 1, this.Width - 2, this.CaptionHeight);
                //    }
                //}
                //else
                //{
                //    switch (this.eCaptionAlignment)
                //    {
                //        case TabAlignment.Left:
                //            return new Rectangle(0, 0, this.CaptionHeight, this.Height);
                //        case TabAlignment.Right:
                //            return new Rectangle(this.Width - this.CaptionHeight, 0, this.CaptionHeight, this.Height);
                //        case TabAlignment.Bottom:
                //            return new Rectangle(0, this.Height - this.CaptionHeight, this.Width, this.CaptionHeight);
                //        case TabAlignment.Top:
                //        default:
                //            return new Rectangle(0, 0, this.Width, this.CaptionHeight);
                //    }
                //}
                Rectangle rectangle = this.ShowOutLine ? new Rectangle(1, 1, base.DisplayRectangle.Width - 2, base.DisplayRectangle.Height - 2) : base.DisplayRectangle;
                switch (this.eCaptionAlignment)
                {
                    case TabAlignment.Left:
                        return new Rectangle(rectangle.Left, rectangle.Top, this.CaptionHeight, rectangle.Height);
                    case TabAlignment.Right:
                        return new Rectangle(rectangle.Right - this.CaptionHeight, rectangle.Top, this.CaptionHeight, rectangle.Height);
                    case TabAlignment.Bottom:
                        return new Rectangle(rectangle.Left, rectangle.Bottom - this.CaptionHeight, rectangle.Width, this.CaptionHeight);
                    case TabAlignment.Top:
                    default:
                        return new Rectangle(rectangle.Left, rectangle.Top, rectangle.Width, this.CaptionHeight);
                }
            }
        }

        [Browsable(false), Description("图片矩形"), Category("布局")]
        public Rectangle ImageRectangle
        {
            get
            {
                if (!this.ShowCaption) { return new Rectangle(0, 0, 0, 0); }
                //
                Rectangle rectangle = this.CaptionRectangle;
                if (this.ShowTreeNodeButton)
                {
                    switch (this.eCaptionAlignment)
                    {
                        case TabAlignment.Left:
                        case TabAlignment.Right:
                            return new Rectangle(
                                (rectangle.Left + rectangle.Right - CTR_IMAGESIZE) / 2,
                                this.m_TreeNodeButtonItem.DisplayRectangle.Bottom + CTR_IMAGESPACE,
                                CTR_IMAGESIZE, 
                                CTR_IMAGESIZE);
                        case TabAlignment.Top:
                        case TabAlignment.Bottom:
                        default:
                            return new Rectangle(
                                this.m_TreeNodeButtonItem.DisplayRectangle.Right + CTR_IMAGESPACE,
                                (rectangle.Top + rectangle.Bottom - CTR_IMAGESIZE) / 2,
                                CTR_IMAGESIZE, 
                                CTR_IMAGESIZE);
                    }
                }
                else
                {
                    switch (this.eCaptionAlignment)
                    {
                        case TabAlignment.Left:
                        case TabAlignment.Right:
                            return new Rectangle((rectangle.Left + rectangle.Right - CTR_IMAGESIZE) / 2, CTR_IMAGESPACE, CTR_IMAGESIZE, CTR_IMAGESIZE);
                        case TabAlignment.Top:
                        case TabAlignment.Bottom:
                        default:
                            return new Rectangle(CTR_IMAGESPACE, (rectangle.Top + rectangle.Bottom - CTR_IMAGESIZE) / 2, CTR_IMAGESIZE, CTR_IMAGESIZE);
                    }
                }
            }
        }

        [Browsable(false), Description("标题文本矩形"), Category("布局")]
        public Rectangle TitleRectangle
        {
            get
            {
                if (!this.ShowCaption) { return new Rectangle(0, 0, 0, 0); }
                //
                Rectangle rectangle = this.CaptionRectangle;
                if (this.Image != null)
                {
                    switch (this.eCaptionAlignment)
                    {
                        case TabAlignment.Left:
                        case TabAlignment.Right:
                            return Rectangle.FromLTRB(
                                rectangle.Left,
                                this.ImageRectangle.Bottom,
                                rectangle.Right,
                                this.m_ExpandableCaptionPanelButtonStackItem.Visible ? this.m_ExpandableCaptionPanelButtonStackItem.DisplayRectangle.Top : rectangle.Bottom);
                        case TabAlignment.Top:
                        case TabAlignment.Bottom:
                        default:
                            return Rectangle.FromLTRB(
                                this.ImageRectangle.Right,
                                rectangle.Top,
                                this.m_ExpandableCaptionPanelButtonStackItem.Visible ? this.m_ExpandableCaptionPanelButtonStackItem.DisplayRectangle.Left : rectangle.Right,
                                rectangle.Bottom);
                    }
                }
                else
                {
                    if (this.ShowTreeNodeButton)
                    {
                        switch (this.eCaptionAlignment)
                        {
                            case TabAlignment.Left:
                            case TabAlignment.Right:
                                return Rectangle.FromLTRB(
                                    rectangle.Left,
                                    this.m_TreeNodeButtonItem.DisplayRectangle.Bottom,
                                    rectangle.Right,
                                    this.m_ExpandableCaptionPanelButtonStackItem.Visible ? this.m_ExpandableCaptionPanelButtonStackItem.DisplayRectangle.Top : rectangle.Bottom);
                            case TabAlignment.Top:
                            case TabAlignment.Bottom:
                            default:
                                return Rectangle.FromLTRB(
                                    this.m_TreeNodeButtonItem.DisplayRectangle.Right,
                                    rectangle.Top,
                                    this.m_ExpandableCaptionPanelButtonStackItem.Visible ? this.m_ExpandableCaptionPanelButtonStackItem.DisplayRectangle.Left : rectangle.Right,
                                    rectangle.Bottom);
                        }
                    }
                    else
                    {
                        switch (this.eCaptionAlignment)
                        {
                            case TabAlignment.Left:
                            case TabAlignment.Right:
                                return Rectangle.FromLTRB(
                                    rectangle.Left,
                                    rectangle.Top,
                                    rectangle.Right,
                                    this.m_ExpandableCaptionPanelButtonStackItem.Visible ? this.m_ExpandableCaptionPanelButtonStackItem.DisplayRectangle.Top : rectangle.Bottom);
                            case TabAlignment.Top:
                            case TabAlignment.Bottom:
                            default:
                                return Rectangle.FromLTRB(
                                    rectangle.Left,
                                    rectangle.Top,
                                    this.m_ExpandableCaptionPanelButtonStackItem.Visible ? this.m_ExpandableCaptionPanelButtonStackItem.DisplayRectangle.Left : rectangle.Right,
                                    rectangle.Bottom);
                        }
                    }
                }
            }
        }

        public Rectangle GetTreeNodeButtonRectangle()
        {
            return this.m_TreeNodeButtonItem.DisplayRectangle;
        }
        public Rectangle GetExpandButtonRectangle()
        {
            return this.m_ExpandableCaptionPanelButtonStackItem.BaseItems[0].DisplayRectangle;
        }
        public Rectangle GetCloseButtonRectangle()
        {
            return this.m_ExpandableCaptionPanelButtonStackItem.BaseItems[1].DisplayRectangle;
        }
        #endregion

        #region ISetExpandableCaptionPanelHelper
        void ISetExpandableCaptionPanelHelper.ResetSize()
        {
            this.SetSize(false);
        }

        void ISetExpandableCaptionPanelHelper.SetIsExpand(bool bIsExpand)
        {
            if (!this.ShowCaption) { m_IsExpand = true; return; }
            //
            if (m_IsExpand == bIsExpand) return;
            //
            m_IsExpand = bIsExpand;
            this.SetSize(true);
            this.OnExpandChanged(new EventArgs());
        }
        private void SetSize(bool bSaveSize)
        {
            if (this.IsExpand)
            {
                Size size = this.GetExpandSize();
                //
                if (this.Size == size) return;
                //
                //if (bSaveSize) this.Size = size;
                this.Size = size;
            }
            else
            {
                if (bSaveSize) this.m_ExpandSize = this.Size;
                //
                switch (this.eExpandButtonStyle)
                {
                    case ExpandButtonStyle.eTopToBottom:
                    case ExpandButtonStyle.eBottomToTop:
                        int iHeight = this.CaptionHeight;
                        if (this.ShowOutLine) iHeight += 2;
                        if (this.Height != iHeight) this.Height = iHeight;
                        break;
                    case ExpandButtonStyle.eLeftToRight:
                    case ExpandButtonStyle.eRightToLeft:
                        int iWidth = this.CaptionHeight;
                        if (this.ShowOutLine) iWidth += 2;
                        if (this.Width != iWidth) { this.Width = iWidth; }
                        break;
                }
            }
        }

        void ISetExpandableCaptionPanelHelper.SetDockStyle(DockStyle eDockStyle)
        {
            if (base.Dock == eDockStyle) return;
            //
            base.Dock = eDockStyle;
        }
        #endregion

        public override Rectangle DisplayRectangle
        {
            get
            {
                Rectangle rectangle = this.ShowOutLine ? new Rectangle(1, 1, base.DisplayRectangle.Width - 2, base.DisplayRectangle.Height - 2) : base.DisplayRectangle;
                if (!this.ShowCaption) { return rectangle; }
                //
                if (this.HaveExpandButton && !this.IsExpand)
                {
                    if (this.eCaptionAlignment == TabAlignment.Top)
                    {
                        switch (this.eExpandButtonStyle)
                        {
                            case ExpandButtonStyle.eLeftToRight:
                            case ExpandButtonStyle.eRightToLeft:
                                return new Rectangle(rectangle.Left, rectangle.Top + this.CaptionHeight, rectangle.Width - this.CaptionHeight, rectangle.Height - this.CaptionHeight);
                        }
                    }
                    else if (this.eCaptionAlignment == TabAlignment.Bottom)
                    {
                        switch (this.eExpandButtonStyle)
                        {
                            case ExpandButtonStyle.eLeftToRight:
                            case ExpandButtonStyle.eRightToLeft:
                                return new Rectangle(rectangle.Left, rectangle.Top, rectangle.Width - this.CaptionHeight, rectangle.Height - this.CaptionHeight);
                        }
                    }
                    else if (this.eCaptionAlignment == TabAlignment.Left)
                    {
                        switch (this.eExpandButtonStyle)
                        {
                            case ExpandButtonStyle.eTopToBottom:
                            case ExpandButtonStyle.eBottomToTop:
                                return new Rectangle(rectangle.Left + this.CaptionHeight, rectangle.Top, rectangle.Width - this.CaptionHeight, rectangle.Height - this.CaptionHeight);
                        }
                    }
                    else if (this.eCaptionAlignment == TabAlignment.Right)
                    {
                        switch (this.eExpandButtonStyle)
                        {
                            case ExpandButtonStyle.eTopToBottom:
                            case ExpandButtonStyle.eBottomToTop:
                                return new Rectangle(rectangle.Left, rectangle.Top, rectangle.Width - this.CaptionHeight, rectangle.Height - this.CaptionHeight);
                        }
                    }
                }
                //
                switch (this.eCaptionAlignment)
                {
                    case TabAlignment.Left:
                        return new Rectangle(rectangle.Left + this.CaptionHeight, rectangle.Top, rectangle.Width - this.CaptionHeight, rectangle.Height);
                    case TabAlignment.Right:
                        return new Rectangle(rectangle.Left, rectangle.Top, rectangle.Width - this.CaptionHeight, rectangle.Height);
                    case TabAlignment.Bottom:
                        return new Rectangle(rectangle.Left, rectangle.Top, rectangle.Width, rectangle.Height - this.CaptionHeight);
                    case TabAlignment.Top:
                    default:
                        return new Rectangle(rectangle.Left, rectangle.Top + this.CaptionHeight, rectangle.Width, rectangle.Height - this.CaptionHeight);
                }
            }
        }

        [Browsable(true), DefaultValue(DockStyle.Left)]
        public override DockStyle Dock
        {
            get { return base.Dock; }
            set
            {
                switch (value) 
                {
                    case DockStyle.Top:
                        this.m_eExpandButtonStyle = ExpandButtonStyle.eTopToBottom;
                        break;
                    case DockStyle.Left:
                        this.m_eExpandButtonStyle = ExpandButtonStyle.eLeftToRight;
                        break;
                    case DockStyle.Right:
                        this.m_eExpandButtonStyle = ExpandButtonStyle.eRightToLeft;
                        break;
                    case DockStyle.Bottom:
                        this.m_eExpandButtonStyle = ExpandButtonStyle.eBottomToTop;
                        break;
                }
                //
                base.Dock = value;
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            //
            if(!this.IsExpand) this.SetSize(false);
        }

        protected override void MessageMonitor(MessageInfo messageInfo)
        {
            base.MessageMonitor(messageInfo);
            //
            ((IMessageChain)this.m_TreeNodeButtonItem).SendMessage(messageInfo);
            ((IMessageChain)this.m_ExpandableCaptionPanelButtonStackItem).SendMessage(messageInfo);
        }

        protected override void OnDraw(PaintEventArgs e)
        {
            //base.OnDraw(e);
            WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderExpandableCaptionPanel(new ObjectRenderEventArgs(e.Graphics, this, this.AreaRectangle));
            //
            if (!this.ShowCaption) return;
            //
            bool bDrawImage = false;
            StringFormat stringFormat = new StringFormat();
            stringFormat.Trimming = StringTrimming.EllipsisCharacter;
            Rectangle rectangle = this.GetTextRectangle(e.Graphics, stringFormat, ref bDrawImage);
            if (bDrawImage)
            {
                if (this.Image != null)
                {
                    GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonImage(
                        new GISShare.Controls.WinForm.ImageRenderEventArgs(e.Graphics, this, this.Enabled, this.Image, this.ImageRectangle));
                }
            }
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonText(
                new GISShare.Controls.WinForm.TextRenderEventArgs(e.Graphics, this, this.Enabled, this.HaveShadow, this.Text, this.ForeCustomize, this.ForeColor, this.ShadowColor, this.Font, rectangle, stringFormat));
        }
        private Rectangle GetTextRectangle(Graphics g, StringFormat stringFormat, ref bool bDrawImage)
        {
            SizeF size = g.MeasureString(this.Text, this.Font);
            int iWidth = (int)(size.Width + 1);
            int iHeight = (int)(size.Height + 1);
            Rectangle rectangle = this.TitleRectangle;
            if (this.IsExpand)
            {
                bDrawImage = true;
                switch (this.eCaptionAlignment)
                {
                    case TabAlignment.Left:
                    case TabAlignment.Right:
                        stringFormat.FormatFlags = StringFormatFlags.DirectionVertical;
                        return new Rectangle((rectangle.Left + rectangle.Right - iHeight) / 2, rectangle.Top, iHeight, rectangle.Height);
                    case TabAlignment.Top:
                    case TabAlignment.Bottom:
                    default:
                        return new Rectangle(rectangle.Left, (rectangle.Top + rectangle.Bottom - iHeight) / 2, rectangle.Width, iHeight);
                }
            }
            else
            {
                Rectangle areaRectangle = this.AreaRectangle;
                switch (this.eCaptionAlignment)
                {
                    case TabAlignment.Left:
                        switch (this.eExpandButtonStyle)
                        {
                            case ExpandButtonStyle.eTopToBottom:
                            case ExpandButtonStyle.eBottomToTop:
                                bDrawImage = false;
                                return new Rectangle((areaRectangle.Left + this.CaptionHeight + areaRectangle.Right - iWidth) / 2, (areaRectangle.Top + areaRectangle.Bottom - iHeight) / 2, iWidth, iHeight);
                            case ExpandButtonStyle.eLeftToRight:
                            case ExpandButtonStyle.eRightToLeft:
                            default:
                                bDrawImage = true;
                                stringFormat.FormatFlags = StringFormatFlags.DirectionVertical;
                                return new Rectangle((rectangle.Left + rectangle.Right - iHeight) / 2, rectangle.Top, iHeight, rectangle.Height);
                        }
                    case TabAlignment.Right:
                        switch (this.eExpandButtonStyle)
                        {
                            case ExpandButtonStyle.eTopToBottom:
                            case ExpandButtonStyle.eBottomToTop:
                                bDrawImage = false;
                                return new Rectangle((areaRectangle.Left + areaRectangle.Right - this.CaptionHeight - iWidth) / 2, (areaRectangle.Top + areaRectangle.Bottom - iHeight) / 2, iWidth, iHeight);
                            case ExpandButtonStyle.eLeftToRight:
                            case ExpandButtonStyle.eRightToLeft:
                            default:
                                bDrawImage = true;
                                stringFormat.FormatFlags = StringFormatFlags.DirectionVertical;
                                return new Rectangle((rectangle.Left + rectangle.Right - iHeight) / 2, rectangle.Top, iHeight, rectangle.Height);
                        }
                    case TabAlignment.Bottom:
                        switch (this.eExpandButtonStyle)
                        {
                            case ExpandButtonStyle.eLeftToRight:
                            case ExpandButtonStyle.eRightToLeft:
                                bDrawImage = false;
                                stringFormat.FormatFlags = StringFormatFlags.DirectionVertical;
                                return new Rectangle((areaRectangle.Left + areaRectangle.Right - iHeight) / 2, (areaRectangle.Top + areaRectangle.Bottom - this.CaptionHeight - iWidth) / 2, iHeight, iWidth);
                            case ExpandButtonStyle.eTopToBottom:
                            case ExpandButtonStyle.eBottomToTop:
                            default:
                                bDrawImage = true;
                                return new Rectangle(rectangle.Left, (rectangle.Top + rectangle.Bottom - iHeight) / 2, rectangle.Width, iHeight);
                        }
                    case TabAlignment.Top:
                    default:
                        switch (this.eExpandButtonStyle)
                        {
                            case ExpandButtonStyle.eLeftToRight:
                            case ExpandButtonStyle.eRightToLeft:
                                bDrawImage = false;
                                stringFormat.FormatFlags = StringFormatFlags.DirectionVertical;
                                return new Rectangle((areaRectangle.Left + areaRectangle.Right - iHeight) / 2, (areaRectangle.Top + this.CaptionHeight + areaRectangle.Bottom - iWidth) / 2, iHeight, iWidth);
                            case ExpandButtonStyle.eTopToBottom:
                            case ExpandButtonStyle.eBottomToTop:
                            default:
                                bDrawImage = true;
                                return new Rectangle(rectangle.Left, (rectangle.Top + rectangle.Bottom - iHeight) / 2, rectangle.Width, iHeight);
                        }
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (this.CaptionRectangle.Contains(e.Location))
            {
                this.SetCaptionStateEx(BaseItemState.ePressed);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (this.CaptionRectangle.Contains(e.Location))
            {
                this.SetCaptionStateEx(BaseItemState.eHot);
            }
            else
            {
                this.SetCaptionStateEx(BaseItemState.eNormal);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (this.CaptionRectangle.Contains(e.Location))
            {
                this.SetCaptionStateEx(BaseItemState.eNormal);
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.SetCaptionStateEx(BaseItemState.eNormal);
        }

        //事件
        protected virtual void OnExpandChanged(EventArgs e)
        { if (this.ExpandChanged != null) { this.ExpandChanged(this, e); } }
    }
}
