using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    public class BaseButtonItem : ImageLabelItem, IBaseButtonItem, IDismissPopupObject
    {
        #region 构造函数
        public BaseButtonItem() { }

        public BaseButtonItem(string strText)
            : base(strText) { }

        public BaseButtonItem(string strName, string strText)
            : base(strName, strText) { }

        public BaseButtonItem(string strText, Image image)
            : base(strText, image) { }

        public BaseButtonItem(string strName, string strText, Image image)
            : base(strName, strText, image) { }

        //public BaseButtonItem(GISShare.Controls.Plugin.WFNew.IBaseButtonItemP pBaseItemP)
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
        //    //ILabelItemP
        //    this.TextAlign = pBaseItemP.TextAlign;
        //    //IImageBoxItemP
        //    this.eImageSizeStyle = pBaseItemP.eImageSizeStyle;
        //    this.Image = pBaseItemP.Image;
        //    this.ImageAlign = pBaseItemP.ImageAlign;
        //    this.ImageSize = pBaseItemP.ImageSize;
        //    //IImageLabelItemP
        //    this.AutoPlanTextRectangle = pBaseItemP.AutoPlanTextRectangle;
        //    this.ITSpace = pBaseItemP.ITSpace;
        //    this.eDisplayStyle = pBaseItemP.eDisplayStyle;
        //    //IBaseButtonItemP
        //    this.LeftBottomRadius = pBaseItemP.LeftBottomRadius;
        //    this.LeftTopRadius = pBaseItemP.LeftTopRadius;
        //    this.RightBottomRadius = pBaseItemP.RightBottomRadius;
        //    this.RightTopRadius = pBaseItemP.RightTopRadius;
        //    this.ShowNomalState = pBaseItemP.ShowNomalState;
        //}
        #endregion

        private int m_TextLeftSpace = 5;
        [Browsable(true), DefaultValue(5), Description("文本左间距（当其为Popup的一级子项是使用）"), Category("布局")]
        public int TextLeftSpace
        {
            get { return m_TextLeftSpace; }
            set
            {
                if (value < 1) return;
                m_TextLeftSpace = value;
            }
        }

        private int m_TextRightSpace = 5;
        [Browsable(true), DefaultValue(5), Description("文本右间距（当其为Popup的一级子项是使用）"), Category("布局")]
        public int TextRightSpace
        {
            get { return m_TextRightSpace; }
            set
            {
                if (value < 1) return;
                m_TextRightSpace = value;
            }
        }

        #region IBaseButtonItem
        [Browsable(false), Description("是否为正常的选中（当其为Popup的一级子项是使用）"), Category("状态")]
        public bool NomalChecked
        {
            get
            {
                IContextPopupPanelItem pContextPopupPanelItem = this.pBaseItemOwner as IContextPopupPanelItem;
                if (pContextPopupPanelItem == null) return true;
                else
                {
                    switch (pContextPopupPanelItem.eContextPopupStyle)
                    {
                        case ContextPopupStyle.eSuper:
                        case ContextPopupStyle.eNormal:
                            return false;
                        default:
                            return true;
                    }
                }
            }
        }

        private bool m_ShowNomalState = false;
        [Browsable(true), DefaultValue(false), Description("是否显示正常状态下的状态"), Category("状态")]
        public virtual bool ShowNomalState
        {
            get
            {
                if (this.IsPopupItem) return false;
                return m_ShowNomalState;
            }
            set { m_ShowNomalState = value; }
        }

        #region Radius
        private int m_LeftTopRadius = 6;
        [Browsable(true), DefaultValue(6), Description("左顶部圆角值"), Category("圆角")]
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

        private int m_RightTopRadius = 6;
        [Browsable(true), DefaultValue(6), Description("右顶部圆角值"), Category("圆角")]
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

        private int m_LeftBottomRadius = 6;
        [Browsable(true), DefaultValue(6), Description("左底部圆角值"), Category("圆角")]
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

        private int m_RightBottomRadius = 6;
        [Browsable(true), DefaultValue(6), Description("右底部圆角值"), Category("圆角")]
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

        [Browsable(false), Description("勾选区矩形框（的一级子项是使用）"), Category("布局")]
        public Rectangle CheckRectangle
        {
            get
            {
                IContextPopupPanelItem pContextPopupPanelItem = this.pBaseItemOwner as IContextPopupPanelItem;
                if (pContextPopupPanelItem == null)
                {
                    return this.DisplayRectangle;
                }
                else
                {
                    Rectangle rectangle = this.DisplayRectangle;
                    switch (pContextPopupPanelItem.eContextPopupStyle)
                    {
                        case ContextPopupStyle.eSuper:
                            return new Rectangle(rectangle.X, rectangle.Y, pContextPopupPanelItem.CheckRegionWidth, pContextPopupPanelItem.CheckRegionWidth);
                        case ContextPopupStyle.eNormal:
                            return new Rectangle(rectangle.X, rectangle.Y, pContextPopupPanelItem.ImageRegionWidth, pContextPopupPanelItem.ImageRegionWidth);
                        case ContextPopupStyle.eSimply:
                        default:
                            return rectangle;
                    }
                }
            }
        }

        [Browsable(false), Description("按钮实体矩形框"), Category("布局")]
        public virtual Rectangle ButtonRectangle//IT + DropDown
        {
            get
            {
                return this.DisplayRectangle;
            }
        }
        #endregion

        #region IDismissPopupObject
        [Browsable(false), DefaultValue(typeof(DismissPopupStyle), "eIsDependBasePopup"), Description("解散popup的类型"), Category("状态")]
        public virtual DismissPopupStyle eDismissPopupStyle
        {
            get { return DismissPopupStyle.eIsDependBasePopup; }
        }

        [Browsable(false), Description("解散popup的触发区"), Category("属性")]
        public virtual Rectangle DismissTriggerRectangle
        {
            get { return this.ButtonRectangle; }
        }
        #endregion

        private ImageSizeStyle m_eImageSizeStyle = ImageSizeStyle.eDefault;
        [Browsable(true), DefaultValue(typeof(ImageSizeStyle), "eDefault"), Description("图片尺寸的展现方式"), Category("布局")]
        public override ImageSizeStyle eImageSizeStyle//BH
        {
            get
            {
                if (this.IsBaseBarItem) return ImageSizeStyle.eSystem;
                return m_eImageSizeStyle;
            }
            set { m_eImageSizeStyle = value; }
        }

        [Browsable(false), Description("文本绘制矩形框"), Category("布局")]
        public override Rectangle TextRectangle//BH
        {
            get
            {
                IContextPopupPanelItem pContextPopupPanelItem = this.pBaseItemOwner as IContextPopupPanelItem;
                if (pContextPopupPanelItem == null)
                {
                    return base.TextRectangle;
                }
                else
                {
                    Rectangle textRectangle = base.TextRectangle;
                    Rectangle rectangle = this.DisplayRectangle;
                    Rectangle displayRectangle = rectangle;
                    switch (pContextPopupPanelItem.eContextPopupStyle)
                    {
                        case ContextPopupStyle.eSuper:
                            rectangle = new Rectangle(rectangle.X + pContextPopupPanelItem.CheckRegionWidth + pContextPopupPanelItem.ImageRegionWidth + this.TextLeftSpace,
                                (rectangle.Top + rectangle.Bottom - textRectangle.Height) / 2,
                                textRectangle.Width,
                                textRectangle.Height);
                            break;
                        case ContextPopupStyle.eNormal:
                            rectangle = new Rectangle(rectangle.X + pContextPopupPanelItem.ImageRegionWidth + this.TextLeftSpace,
                                (rectangle.Top + rectangle.Bottom - textRectangle.Height) / 2,
                                textRectangle.Width,
                                textRectangle.Height);
                            break;
                        case ContextPopupStyle.eSimply:
                            if (this.Image == null)
                            {
                                rectangle = new Rectangle(rectangle.X + this.TextLeftSpace,
                                    (rectangle.Top + rectangle.Bottom - textRectangle.Height) / 2,
                                    textRectangle.Width,
                                    textRectangle.Height);
                            }
                            else
                            {
                                rectangle = new Rectangle(rectangle.X + this.TextLeftSpace + pContextPopupPanelItem.ImageRegionWidth,
                                    (rectangle.Top + rectangle.Bottom - textRectangle.Height) / 2,
                                    textRectangle.Width + pContextPopupPanelItem.ImageRegionWidth,
                                    textRectangle.Height);
                            }
                            break;
                        default:
                            return textRectangle;
                    }
                    //
                    int iTop = rectangle.Top;
                    int iLeft = rectangle.Left;
                    int iRight = rectangle.Right;
                    int iBottom = rectangle.Bottom;
                    if (displayRectangle.Top > iTop) iTop = displayRectangle.Top;
                    if (displayRectangle.Left > iLeft) iLeft = displayRectangle.Left;
                    if (displayRectangle.Right < iRight) iRight = displayRectangle.Right;
                    if (displayRectangle.Bottom < iBottom) iBottom = displayRectangle.Bottom;
                    return Rectangle.FromLTRB(iLeft, iTop, iRight, iBottom);
                }
            }
        }

        [Browsable(false), Description("图片绘制矩形框"), Category("布局")]
        public override Rectangle ImageRectangle//BH
        {
            get
            {
                IContextPopupPanelItem pContextPopupPanelItem = this.pBaseItemOwner as IContextPopupPanelItem;
                if (pContextPopupPanelItem == null)
                {
                    return base.ImageRectangle;
                }
                else
                {
                    Rectangle rectangle = this.DisplayRectangle;
                    Rectangle displayRectangle = rectangle;
                    switch (pContextPopupPanelItem.eContextPopupStyle)
                    {
                        case ContextPopupStyle.eSuper:
                            rectangle = new Rectangle(rectangle.X + pContextPopupPanelItem.CheckRegionWidth + this.Padding.Left,
                                rectangle.Y + this.Padding.Top,
                                pContextPopupPanelItem.ImageRegionWidth - this.Padding.Left - this.Padding.Right,
                                pContextPopupPanelItem.ImageRegionWidth - this.Padding.Top - this.Padding.Bottom);
                            break;
                        case ContextPopupStyle.eNormal:
                            if (this.Checked)
                            {
                                rectangle = new Rectangle(rectangle.X + this.Padding.Left + 2,
                                    rectangle.Y + 2,
                                    pContextPopupPanelItem.ImageRegionWidth - this.Padding.Left - 4,
                                    pContextPopupPanelItem.ImageRegionWidth - 4);
                            }
                            else
                            {
                                rectangle = new Rectangle(rectangle.X + this.Padding.Left,
                                    rectangle.Y + this.Padding.Top,
                                    pContextPopupPanelItem.ImageRegionWidth - this.Padding.Left - this.Padding.Right,
                                    pContextPopupPanelItem.ImageRegionWidth - this.Padding.Top - this.Padding.Bottom);
                            }
                            break;
                        case ContextPopupStyle.eSimply:
                            if (this.Image == null) return new Rectangle(rectangle.X + this.Padding.Left, rectangle.Y + this.Padding.Top, 0, 0);
                            //
                            rectangle = new Rectangle(rectangle.X + this.Padding.Left,
                                rectangle.Y + this.Padding.Top,
                                pContextPopupPanelItem.ImageRegionWidth - this.Padding.Left - this.Padding.Right,
                                pContextPopupPanelItem.ImageRegionWidth - this.Padding.Top - this.Padding.Bottom);
                            break;
                        default:
                            return base.ImageRectangle;
                    }
                    //
                    int iTop = rectangle.Top;
                    int iLeft = rectangle.Left;
                    int iRight = rectangle.Right;
                    int iBottom = rectangle.Bottom;
                    if (displayRectangle.Top > iTop) iTop = displayRectangle.Top;
                    if (displayRectangle.Left > iLeft) iLeft = displayRectangle.Left;
                    if (displayRectangle.Right < iRight) iRight = displayRectangle.Right;
                    if (displayRectangle.Bottom < iBottom) iBottom = displayRectangle.Bottom;
                    return Rectangle.FromLTRB(iLeft, iTop, iRight, iBottom);
                }
            }
        }

        public override Rectangle DrawRectangle
        {
            get
            {
                return this.ButtonRectangle;
            }
        }

        protected override bool RefreshBaseItemState
        {
            get
            {
                return true;
            }
        }

        public override Padding Padding
        {
            get
            {
                if (this.IsPopupItem) return new Padding(1);
                return base.Padding;
            }
            set { base.Padding = value; }
        }

        #region Clone
        public override object Clone()
        {
            BaseButtonItem baseItem = new BaseButtonItem();
            baseItem.Checked = this.Checked;
            baseItem.Enabled = this.Enabled;
            baseItem.Font = this.Font;
            baseItem.ForeColor = this.ForeColor;
            baseItem.Name = this.Name;
            baseItem.Site = this.Site;
            baseItem.Size = this.Size;
            baseItem.Tag = this.Tag;
            baseItem.Text = this.Text;
            baseItem.eDisplayStyle = this.eDisplayStyle;
            baseItem.eImageSizeStyle = this.eImageSizeStyle;
            baseItem.Image = this.Image;
            baseItem.ImageAlign = this.ImageAlign;
            baseItem.ImageSize = this.ImageSize;
            baseItem.LeftBottomRadius = this.LeftBottomRadius;
            baseItem.LeftTopRadius = this.LeftTopRadius;
            baseItem.Padding = this.Padding;
            baseItem.RightBottomRadius = this.RightBottomRadius;
            baseItem.RightTopRadius = this.RightTopRadius;
            baseItem.ShowNomalState = this.ShowNomalState;
            baseItem.TextAlign = this.TextAlign;
            baseItem.TextLeftSpace = this.TextLeftSpace;
            baseItem.TextRightSpace = this.TextRightSpace;
            baseItem.Visible = this.Visible;
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
        #endregion

        public override Size MeasureSize(Graphics g)//有待完善
        {
            int iWidth = 0;
            int iHeight = 0;
            SizeF size;
            Rectangle rectangle;
            switch (this.eDisplayStyle)
            {
                case DisplayStyle.eText:
                    size = g.MeasureString(this.Text, this.Font);
                    iWidth = (int)(size.Width + 1);
                    iHeight = (int)(size.Height + 1);
                    break;
                case DisplayStyle.eImage:
                    if (this.Image != null)
                    {
                        rectangle = this.ImageRectangle;
                        iWidth += rectangle.Width;
                        iHeight += rectangle.Height;
                    }
                    break;
                case DisplayStyle.eImageAndText:
                    size = g.MeasureString(this.Text, this.Font);
                    iWidth = (int)(size.Width + 1);
                    iHeight = (int)(size.Height + 1);
                    if (this.Image != null)
                    {
                        switch (this.ImageAlign)
                        {
                            case ContentAlignment.BottomLeft:
                            case ContentAlignment.BottomRight:
                            case ContentAlignment.TopLeft:
                            case ContentAlignment.TopRight:
                                rectangle = this.ImageRectangle;
                                iWidth += rectangle.Width + this.ITSpace;
                                iHeight += rectangle.Height;
                                break;
                            case ContentAlignment.BottomCenter:
                            case ContentAlignment.TopCenter:
                                iHeight += this.ImageRectangle.Height + this.ITSpace;
                                break;
                            case ContentAlignment.MiddleLeft:
                            case ContentAlignment.MiddleRight:
                                iWidth += this.ImageRectangle.Width + this.ITSpace;
                                break;
                            case ContentAlignment.MiddleCenter:
                                rectangle = this.ImageRectangle;
                                if (iWidth < rectangle.Width) iWidth = rectangle.Width;
                                if (iHeight < rectangle.Height) iHeight = rectangle.Height;
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                default:
                    break;

            }
            iWidth += this.Padding.Left + this.Padding.Right;
            iHeight += this.Padding.Top + this.Padding.Bottom;
            //
            IContextPopupPanelItem pContextPopupPanelItem = this.pBaseItemOwner as IContextPopupPanelItem;
            if (pContextPopupPanelItem != null)
            {
                switch (pContextPopupPanelItem.eContextPopupStyle)
                {
                    case ContextPopupStyle.eSuper:
                        iHeight = pContextPopupPanelItem.CheckRegionWidth > pContextPopupPanelItem.ImageRegionWidth ? pContextPopupPanelItem.CheckRegionWidth : pContextPopupPanelItem.ImageRegionWidth;
                        iWidth += pContextPopupPanelItem.CheckRegionWidth + pContextPopupPanelItem.ImageRegionWidth;
                        break;
                    case ContextPopupStyle.eNormal:
                        iHeight = pContextPopupPanelItem.ImageRegionWidth;
                        iWidth += pContextPopupPanelItem.ImageRegionWidth;
                        break;
                    case ContextPopupStyle.eSimply:
                    default:
                        break;
                }
                iWidth += this.TextLeftSpace + this.TextRightSpace;
            }
            //
            if (iWidth < 16) iWidth = 16;
            if (iHeight < 16) iHeight = 16;
            //
            return new Size(iWidth, iHeight);
        }

        protected override void OnDraw(PaintEventArgs pevent)
        {
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderBaseButton(
                new GISShare.Controls.WinForm.ObjectRenderEventArgs(pevent.Graphics, this, this.DisplayRectangle));
            //
            IContextPopupPanelItem pContextPopupPanelItem = this.pBaseItemOwner as IContextPopupPanelItem;
            if (pContextPopupPanelItem != null)
            {
                switch (pContextPopupPanelItem.eContextPopupStyle)
                {
                    case ContextPopupStyle.eSuper:
                    case ContextPopupStyle.eNormal:
                        GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderContextPopupItemButtonChecked(
                            new GISShare.Controls.WinForm.CheckedRenderEventArgs(pevent.Graphics, this, this.Enabled, this.ForeColor, this.Checked, this.CheckRectangle));
                        break;
                    default:
                        break;
                }
                //
                //switch (this.eDisplayStyle)
                //{
                //    case DisplayStyle.eText:
                //        GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonText(
                //            new GISShare.Controls.WinForm.TextRenderEventArgs(pevent.Graphics, this, this.Enabled, this.Text, this.ForeColor, this.Font, this.TextRectangle));
                //        break;
                //    default:
                //        GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonImage(
                //            new GISShare.Controls.WinForm.ImageRenderEventArgs(pevent.Graphics, this, this.Enabled, this.Image, this.ImageRectangle));
                //        GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonText(
                //            new GISShare.Controls.WinForm.TextRenderEventArgs(pevent.Graphics, this, this.Enabled, this.Text, this.ForeColor, this.Font, this.TextRectangle));
                //        break;
                //}
                switch (this.eDisplayStyle)
                {
                    case DisplayStyle.eImage:
                        GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonImage(
                            new GISShare.Controls.WinForm.ImageRenderEventArgs(pevent.Graphics, this, this.Enabled, this.Image, this.ImageRectangle));
                        break;
                    case DisplayStyle.eText:
                        GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonText(
                            new GISShare.Controls.WinForm.TextRenderEventArgs(pevent.Graphics, this, this.Enabled, this.HaveShadow, this.Text, this.ForeCustomize,  this.ForeColor, this.ShadowColor, this.Font, this.TextRectangle));
                        break;
                    case DisplayStyle.eImageAndText:
                        GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonImage(
                            new GISShare.Controls.WinForm.ImageRenderEventArgs(pevent.Graphics, this, this.Enabled, this.Image, this.ImageRectangle));
                        GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonText(
                            new GISShare.Controls.WinForm.TextRenderEventArgs(pevent.Graphics, this, this.Enabled, this.HaveShadow, this.Text, this.ForeCustomize,  this.ForeColor, this.ShadowColor, this.Font, this.TextRectangle));
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (this.eDisplayStyle)
                {
                    case DisplayStyle.eImage:
                        GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonImage(
                            new GISShare.Controls.WinForm.ImageRenderEventArgs(pevent.Graphics, this, this.Enabled, this.Image, this.ImageRectangle));
                        break;
                    case DisplayStyle.eText:
                        GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonText(
                            new GISShare.Controls.WinForm.TextRenderEventArgs(pevent.Graphics, this, this.Enabled, this.HaveShadow, this.Text, this.ForeCustomize,  this.ForeColor, this.ShadowColor, this.Font, this.TextRectangle));
                        break;
                    case DisplayStyle.eImageAndText:
                        GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonImage(
                            new GISShare.Controls.WinForm.ImageRenderEventArgs(pevent.Graphics, this, this.Enabled, this.Image, this.ImageRectangle));
                        GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonText(
                            new GISShare.Controls.WinForm.TextRenderEventArgs(pevent.Graphics, this, this.Enabled, this.HaveShadow, this.Text, this.ForeCustomize,  this.ForeColor, this.ShadowColor, this.Font, this.TextRectangle));
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
