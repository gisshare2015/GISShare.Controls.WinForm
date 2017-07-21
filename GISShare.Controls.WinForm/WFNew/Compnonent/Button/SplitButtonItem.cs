using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    [DefaultEvent("ButtonMouseClick")]
    public class SplitButtonItem : DropDownButtonItem, ISplitButtonItem, ISplitButtonItemEvent
    {
        #region 构造函数
        public SplitButtonItem(ISimplyPopup pSimplyPopup)
            : base(pSimplyPopup) { }

        public SplitButtonItem()
            : base(new DropDownPopup()) { }

        public SplitButtonItem(string strText)
            : base(strText) { }

        public SplitButtonItem(string strName, string strText)
            : base(strName, strText) { }

        public SplitButtonItem(string strText, Image image)
            : base(strText, image) { }

        public SplitButtonItem(string strName, string strText, Image image)
            : base(strName, strText, image) { }

        //public SplitButtonItem(GISShare.Controls.Plugin.WFNew.ISplitButtonItemP pBaseItemP)
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
        //    //IDropDownButtonItemP
        //    this.DropDownDistance = pBaseItemP.DropDownDistance;
        //    this.eArrowStyle = pBaseItemP.eArrowStyle;
        //    this.eArrowDock = pBaseItemP.eArrowDock;
        //    this.eContextPopupStyle = pBaseItemP.eContextPopupStyle;
        //    this.ArrowSize = pBaseItemP.ArrowSize;
        //    this.PopupSpace = pBaseItemP.PopupSpace;
        //    //ISplitButtonItemP
        //    this.ShowNomalSplitLine = pBaseItemP.ShowNomalSplitLine;
        //}
        #endregion

        //private const int CTR_SPACE = 3;

        protected override EventStateStyle GetEventStateSupplement(string strEventName)
        {
            switch (strEventName)
            {
                case "ButtonMouseDown":
                    return this.ButtonMouseDown != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "SplitMouseDown":
                    return this.SplitMouseDown != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "ButtonMouseMove":
                    return this.ButtonMouseMove != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "SplitMouseMove":
                    return this.SplitMouseMove != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "ButtonMouseUp":
                    return this.ButtonMouseUp != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "SplitMouseUp":
                    return this.SplitMouseUp != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "ButtonMouseClick":
                    return this.ButtonMouseClick != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "SplitMouseClick":
                    return this.SplitMouseClick != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                case "ButtonMouseDoubleClick":
                    return this.ButtonMouseDoubleClick != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
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
                case "ButtonMouseDown":
                    if (this.ButtonMouseDown != null) { this.ButtonMouseDown(this, e as MouseEventArgs); }
                    return true;
                case "SplitMouseDown":
                    if (this.SplitMouseDown != null) { this.SplitMouseDown(this, e as MouseEventArgs); }
                    return true;
                case "ButtonMouseMove":
                    if (this.ButtonMouseMove != null) { this.ButtonMouseMove(this, e as MouseEventArgs); }
                    return true;
                case "SplitMouseMove":
                    if (this.SplitMouseMove != null) { this.SplitMouseMove(this, e as MouseEventArgs); }
                    return true;
                case "ButtonMouseUp":
                    if (this.ButtonMouseUp != null) { this.ButtonMouseUp(this, e as MouseEventArgs); }
                    return true;
                case "SplitMouseUp":
                    if (this.SplitMouseUp != null) { this.SplitMouseUp(this, e as MouseEventArgs); }
                    return true;
                case "ButtonMouseClick":
                    if (this.ButtonMouseClick != null) { this.ButtonMouseClick(this, e as MouseEventArgs); }
                    return true;
                case "SplitMouseClick":
                    if (this.SplitMouseClick != null) { this.SplitMouseClick(this, e as MouseEventArgs); }
                    return true;
                case "ButtonMouseDoubleClick":
                    if (this.ButtonMouseDoubleClick != null) { this.ButtonMouseDoubleClick(this, e as MouseEventArgs); }
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

        #region ISplitButtonItem
        private bool m_ShowNomalSplitLine = true;
        [Browsable(true), DefaultValue(true), Description("是否显示分隔条"), Category("状态")]
        public bool ShowNomalSplitLine
        {
            get { return m_ShowNomalSplitLine; }
            set { m_ShowNomalSplitLine = value; }
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
        public virtual Rectangle SplitRectangle
        {
            get
            {
                return this.DropDownRectangle;
            }
        }
        #endregion

        #region ISplitButtonItemEvent
        [Browsable(true), Description("鼠标在按钮区可见部分按下时触发"), Category("鼠标")]
        public event MouseEventHandler ButtonMouseDown;
        [Browsable(true), Description("鼠标在分割区可见部分按下时触发"), Category("鼠标")]
        public event MouseEventHandler SplitMouseDown;
        [Browsable(true), Description("鼠标在按钮区可见部分按下时触发"), Category("鼠标")]
        public event MouseEventHandler ButtonMouseMove;
        [Browsable(true), Description("鼠标在分割区可见部分按下时触发"), Category("鼠标")]
        public event MouseEventHandler SplitMouseMove;
        [Browsable(true), Description("鼠标在按钮区可见部分按下时触发"), Category("鼠标")]
        public event MouseEventHandler ButtonMouseUp;
        [Browsable(true), Description("鼠标在分割区可见部分按下时触发"), Category("鼠标")]
        public event MouseEventHandler SplitMouseUp;
        [Browsable(true), Description("鼠标在按钮区可见部分按下时触发"), Category("鼠标")]
        public event MouseEventHandler ButtonMouseClick;
        [Browsable(true), Description("鼠标在分割区可见部分按下时触发"), Category("鼠标")]
        public event MouseEventHandler SplitMouseClick;
        [Browsable(true), Description("鼠标在按钮区可见部分按下时触发"), Category("鼠标")]
        public event MouseEventHandler ButtonMouseDoubleClick;
        [Browsable(true), Description("鼠标在分割区可见部分按下时触发"), Category("鼠标")]
        public event MouseEventHandler SplitMouseDoubleClick;
        ////
        //[Browsable(true), Description("单击分割区可见部分按下时触发"), Category("点击")]
        //public event EventHandler SplitClick;
        //[Browsable(true), Description("单击按钮区可见部分按下时触发"), Category("点击")]
        //public event EventHandler ButtonClick;
        #endregion

        public override bool IsAutoMouseTrigger
        {
            get { return false; }
        }

        public override Rectangle ButtonTriggerRectangle
        {
            get { return this.ButtonRectangle; }
        }

        public override Rectangle PopupTriggerRectangle
        {
            get
            {
                return this.SplitRectangle;
            }
        }

        #region Clone
        public override object Clone()
        {
            SplitButtonItem baseItem = new SplitButtonItem();
            baseItem.Checked = this.Checked;
            baseItem.Enabled = this.Enabled;
            baseItem.Font = this.Font;
            baseItem.ForeColor = this.ForeColor;
            baseItem.Name = this.Name;
            baseItem.Site = this.Site;
            baseItem.Size = this.Size;
            baseItem.Tag = this.Tag;
            baseItem.Text = this.Text;
            baseItem.ArrowSize = this.ArrowSize;
            baseItem.DropDownDistance = this.DropDownDistance;
            baseItem.eArrowDock = this.eArrowDock;
            baseItem.eArrowStyle = this.eArrowStyle;
            baseItem.eDisplayStyle = this.eDisplayStyle;
            baseItem.eImageSizeStyle = this.eImageSizeStyle;
            baseItem.Image = this.Image;
            baseItem.ImageAlign = this.ImageAlign;
            baseItem.ImageSize = this.ImageSize;
            baseItem.LeftBottomRadius = this.LeftBottomRadius;
            baseItem.LeftTopRadius = this.LeftTopRadius;
            baseItem.Padding = this.Padding;
            baseItem.PopupSpace = this.PopupSpace;
            baseItem.RightBottomRadius = this.RightBottomRadius;
            baseItem.RightTopRadius = this.RightTopRadius;
            baseItem.ShowNomalSplitLine = this.ShowNomalSplitLine;
            baseItem.ShowNomalState = this.ShowNomalState;
            baseItem.TextAlign = this.TextAlign;
            baseItem.TextLeftSpace = this.TextLeftSpace;
            baseItem.TextRightSpace = this.TextRightSpace;
            baseItem.Visible = this.Visible;
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
            if (this.GetEventState("SplitMouseUp") == EventStateStyle.eUsed) baseItem.SplitMouseUp += new MouseEventHandler(baseItem_SplitMouseUp);
            if (this.GetEventState("SplitMouseMove") == EventStateStyle.eUsed) baseItem.SplitMouseMove += new MouseEventHandler(baseItem_SplitMouseMove);
            if (this.GetEventState("SplitMouseDown") == EventStateStyle.eUsed) baseItem.SplitMouseDown += new MouseEventHandler(baseItem_SplitMouseDown);
            if (this.GetEventState("SplitMouseDoubleClick") == EventStateStyle.eUsed) baseItem.SplitMouseDoubleClick += new MouseEventHandler(baseItem_SplitMouseDoubleClick);
            if (this.GetEventState("SplitMouseClick") == EventStateStyle.eUsed) baseItem.SplitMouseClick += new MouseEventHandler(baseItem_SplitMouseClick);
            if (this.GetEventState("ButtonMouseUp") == EventStateStyle.eUsed) baseItem.ButtonMouseUp += new MouseEventHandler(baseItem_ButtonMouseUp);
            if (this.GetEventState("ButtonMouseMove") == EventStateStyle.eUsed) baseItem.ButtonMouseMove += new MouseEventHandler(baseItem_ButtonMouseMove);
            if (this.GetEventState("ButtonMouseDown") == EventStateStyle.eUsed) baseItem.ButtonMouseDown += new MouseEventHandler(baseItem_ButtonMouseDown);
            if (this.GetEventState("ButtonMouseDoubleClick") == EventStateStyle.eUsed) baseItem.ButtonMouseDoubleClick += new MouseEventHandler(baseItem_ButtonMouseDoubleClick);
            if (this.GetEventState("ButtonMouseClick") == EventStateStyle.eUsed) baseItem.ButtonMouseClick += new MouseEventHandler(baseItem_ButtonMouseClick);
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
        void baseItem_ButtonMouseClick(object sender, MouseEventArgs e)
        {
            this.RelationEvent("ButtonMouseClick", e);
        }
        void baseItem_ButtonMouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.RelationEvent("ButtonMouseDoubleClick", e);
        }
        void baseItem_ButtonMouseDown(object sender, MouseEventArgs e)
        {
            this.RelationEvent("ButtonMouseDown", e);
        }
        void baseItem_ButtonMouseMove(object sender, MouseEventArgs e)
        {
            this.RelationEvent("ButtonMouseMove", e);
        }
        void baseItem_ButtonMouseUp(object sender, MouseEventArgs e)
        {
            this.RelationEvent("ButtonMouseUp", e);
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

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            if (this.SplitRectangle.Contains(mevent.Location))
            {
                this.m_eSplitState = BaseItemState.ePressed;
                this.OnSplitMouseDown(mevent);
            }
            else
            {
                this.OnButtonMouseDown(mevent);
            }
            //
            base.OnMouseDown(mevent);
        }

        protected override void OnMouseMove(MouseEventArgs mevent)
        {
            if (this.SplitRectangle.Contains(mevent.Location))
            {
                if (this.eSplitState != BaseItemState.ePressed &&
                    this.eSplitState != BaseItemState.eHot) { this.m_eSplitState = BaseItemState.eHot; this.Refresh(); }
                this.OnSplitMouseMove(mevent);
            }
            else
            {
                if (this.eSplitState != BaseItemState.ePressed &&
                    this.eSplitState != BaseItemState.eNormal) { this.m_eSplitState = BaseItemState.eNormal; this.Refresh(); }
                this.OnButtonMouseMove(mevent);
            }
            //
            base.OnMouseMove(mevent);
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            if (this.DisplayRectangle.Contains(mevent.Location))
            {
                if (this.SplitRectangle.Contains(mevent.Location))
                {
                    this.m_eSplitState = BaseItemState.eHot;
                    this.OnSplitMouseUp(mevent);
                }
                else
                {
                    this.OnButtonMouseUp(mevent);
                }
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
            if (this.SplitRectangle.Contains(e.Location))
            { this.OnSplitMouseClick(e); }
            else
            { this.OnButtonMouseClick(e); }
            ////
            //if (this.ButtonTriggerRectangle.Contains(e.Location))
            //{
            //    BasePopup basePopup = this.TryGetDependBasePopup();
            //    if (basePopup != null) basePopup.DismissPopup();
            //}
            //
            base.OnMouseClick(e);
            //base.RelationEvent("MouseClick", e);
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            if (this.SplitRectangle.Contains(e.Location))
            { this.OnSplitMouseDoubleClick(e); }
            else
            { this.OnButtonMouseDoubleClick(e); }
            //
            base.OnMouseDoubleClick(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            this.m_eSplitState = BaseItemState.eNormal;
            //this.SetBaseItemStateEx(BaseItemState.eNormal); 
            //
            base.OnMouseLeave(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            this.m_eSplitState = BaseItemState.eHot;
            //this.SetBaseItemStateEx(BaseItemState.eHot); 
            //
            base.OnMouseEnter(e);
        }

        protected override void OnDraw(PaintEventArgs pevent)
        {
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderSplitButton(
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
                            new GISShare.Controls.WinForm.TextRenderEventArgs(pevent.Graphics, this, this.Enabled, this.Text, this.ForeColor, this.Font, this.TextRectangle));
                        break;
                    case DisplayStyle.eImageAndText:
                        GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonImage(
                            new GISShare.Controls.WinForm.ImageRenderEventArgs(pevent.Graphics, this, this.Enabled, this.Image, this.ImageRectangle));
                        GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonText(
                            new GISShare.Controls.WinForm.TextRenderEventArgs(pevent.Graphics, this, this.Enabled, this.Text, this.ForeColor, this.Font, this.TextRectangle));
                        break;
                    default:
                        break;
                }
                //
                GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonArrow(
                    new GISShare.Controls.WinForm.ArrowRenderEventArgs(pevent.Graphics, this, this.Enabled, ArrowStyle.eToRight, this.ForeColor, this.ArrowRectangle));
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
                            new GISShare.Controls.WinForm.TextRenderEventArgs(pevent.Graphics, this, this.Enabled, this.Text, this.ForeColor, this.Font, this.TextRectangle));
                        break;
                    case DisplayStyle.eImageAndText:
                        GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonImage(
                            new GISShare.Controls.WinForm.ImageRenderEventArgs(pevent.Graphics, this, this.Enabled, this.Image, this.ImageRectangle));
                        GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonText(
                            new GISShare.Controls.WinForm.TextRenderEventArgs(pevent.Graphics, this, this.Enabled, this.Text, this.ForeColor, this.Font, this.TextRectangle));
                        break;
                    default:
                        break;
                }
                //
                GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonArrow(
                    new GISShare.Controls.WinForm.ArrowRenderEventArgs(pevent.Graphics, this, this.Enabled, this.eArrowStyle, this.ForeColor, this.ArrowRectangle));
            }
        }

        //

        protected virtual void OnButtonMouseDown(MouseEventArgs e)
        {
            if (this.ButtonMouseDown != null) { this.ButtonMouseDown(this, e); }
        }

        protected virtual void OnSplitMouseDown(MouseEventArgs e)
        {
            if (this.SplitMouseDown != null) { this.SplitMouseDown(this, e); }
        }

        protected virtual void OnButtonMouseMove(MouseEventArgs e)
        {
            if (this.ButtonMouseMove != null) { this.ButtonMouseMove(this, e); }
        }

        protected virtual void OnSplitMouseMove(MouseEventArgs e)
        {
            if (this.SplitMouseMove != null) { this.SplitMouseMove(this, e); }
        }

        protected virtual void OnButtonMouseUp(MouseEventArgs e)
        {
            if (this.ButtonMouseUp != null) { this.ButtonMouseUp(this, e); }
        }

        protected virtual void OnSplitMouseUp(MouseEventArgs e)
        {
            if (this.SplitMouseUp != null) { this.SplitMouseUp(this, e); }
        }

        protected virtual void OnButtonMouseClick(MouseEventArgs e)
        {
            if (this.ButtonMouseClick != null) { this.ButtonMouseClick(this, e); }
        }

        protected virtual void OnSplitMouseClick(MouseEventArgs e)
        {
            if (this.SplitMouseClick != null) { this.SplitMouseClick(this, e); }
        }

        protected virtual void OnButtonMouseDoubleClick(MouseEventArgs e)
        {
            if (this.ButtonMouseDoubleClick != null) { this.ButtonMouseDoubleClick(this, e); }
        }

        protected virtual void OnSplitMouseDoubleClick(MouseEventArgs e)
        {
            if (this.SplitMouseDoubleClick != null) { this.SplitMouseDoubleClick(this, e); }
        }

        //protected virtual void OnSplitClick(EventArgs e)
        //{
        //    if (this.SplitClick != null) { this.SplitClick(this, e); }
        //}

        //protected virtual void OnButtonClick(EventArgs e)
        //{
        //    if (this.ButtonClick != null) { this.ButtonClick(this, e); }
        //}



    }
}
