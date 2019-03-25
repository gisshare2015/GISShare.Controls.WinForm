using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    public class RibbonPageItem : BaseItemStackExItem, IRibbonPageItem, ISetTabPageItemHelper
    {
        ITabButtonItem m_pTabButtonItem;
        [Browsable(false), Description("携带的TabButtonItem对象"), Category("关联")]
        public ITabButtonItem pTabButtonItem
        {
            get { return m_pTabButtonItem; }
        }

        #region 构造函数
        public RibbonPageItem()
        {
            this.m_pTabButtonItem = new RibbonPageTabButtonItem(this);
        }

        public RibbonPageItem(Image image)
            : this()
        {
            this.Image = image;
        }

        //public RibbonPageItem(GISShare.Controls.Plugin.WFNew.IRibbonPageItemP pBaseItemP)
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
        //    //IBaseItemStackExItemP
        //    this.LeftTopRadius = pBaseItemP.LeftTopRadius;
        //    this.RightTopRadius = pBaseItemP.RightTopRadius;
        //    this.LeftBottomRadius = pBaseItemP.LeftBottomRadius;
        //    this.RightBottomRadius = pBaseItemP.RightBottomRadius;
        //    this.TopViewItemIndex = pBaseItemP.TopViewItemIndex;
        //    this.PreButtonIncreaseIndex = pBaseItemP.PreButtonIncreaseIndex;
        //    //IRibbonPageItemP
        //    this.Image = pBaseItemP.Image;
        //}
        #endregion

        #region IRibbonPageItem
        [Browsable(false), Description("可见（不要随意使用）"), Category("状态")]
        public override bool Visible
        {
            get 
            {
                //if (this.pTabButtonItem == null || !this.pTabButtonItem.HaveTabButtonContainer) { return base.Visible; }
                //else
                //{
                //    if (this.pTabButtonItem.Visible) return this.pTabButtonItem.IsSelected;
                //    else return false;
                //}
                return base.Visible;
            }
            set
            {
                base.Visible = value; 
            }
        }

        [Browsable(false), Description("可见"), Category("状态")]
        public bool VisibleEx
        {
            get
            {
                if (this.pTabButtonItem == null || !this.pTabButtonItem.HaveTabButtonContainer) return base.Visible;
                else return this.pTabButtonItem.Visible;
            }
            set
            {
                if (this.pTabButtonItem == null || !this.pTabButtonItem.HaveTabButtonContainer) 
                {
                    base.Visible = value;
                }
                else 
                {
                    this.pTabButtonItem.Visible = value;
                    if (this.pTabButtonItem.IsSelected) this.Visible = true;
                    else this.Visible = false;
                }
            }
        }

        [Browsable(true), Description("图片"), Category("外观")]
        public Image Image
        {
            get 
            {
                if (this.pTabButtonItem != null) return this.pTabButtonItem.Image;
                return null;
            }
            set
            {
                if (this.pTabButtonItem != null) this.pTabButtonItem.Image = value;
            }
        }
        #endregion

        #region ISetTabPageItemHelper
        void ISetTabPageItemHelper.SetIsSelected(bool bIsSelected)
        {

            this.Visible = bIsSelected;
        }
        #endregion

        [Browsable(false)]
        public override Orientation eOrientation
        {
            get
            {
                return  Orientation.Horizontal;
            }
            set
            {
                base.eOrientation =  Orientation.Horizontal;
            }
        }

        [Browsable(false)]
        public override bool IsRestrictItems
        {
            get
            {
                return false;
            }
            set
            {
                base.IsRestrictItems = false;
            }
        }

        [Browsable(false)]
        public override bool IsStretchItems
        {
            get
            {
                return true;
            }
            set
            {
                base.IsStretchItems = true;
            }
        }

        #region Clone
        public override object Clone()
        {
            RibbonPageItem baseItem = new RibbonPageItem();
            baseItem.Checked = this.Checked;
            baseItem.Enabled = this.Enabled;
            baseItem.Font = this.Font;
            baseItem.ForeColor = this.ForeColor;
            baseItem.Name = this.Name;
            baseItem.Site = this.Site;
            baseItem.Size = this.Size;
            baseItem.Tag = this.Tag;
            baseItem.Text = this.Text;
            baseItem.LeftBottomRadius = this.LeftBottomRadius;
            baseItem.LeftTopRadius = this.LeftTopRadius;
            baseItem.Padding = this.Padding;
            baseItem.RightBottomRadius = this.RightBottomRadius;
            baseItem.RightTopRadius = this.RightTopRadius;
            baseItem.Visible = this.Visible;
            //
            baseItem.MinSize = this.MinSize;
            baseItem.MaxSize = this.MaxSize;
            baseItem.IsStretchItems = this.IsStretchItems;
            baseItem.IsRestrictItems = this.IsRestrictItems;
            baseItem.LineDistance = this.LineDistance;
            baseItem.ColumnDistance = this.ColumnDistance;
            baseItem.eOrientation = this.eOrientation;
            //
            //baseItem.ShowNomalOutLineState = this.ShowNomalOutLineState;
            //baseItem.ShowNomalBackgroudState = this.ShowNomalBackgroudState;
            //baseItem.ShowOutLineState = this.ShowOutLineState;
            //baseItem.ShowBackgroudState = this.ShowBackgroudState;
            baseItem.PreButtonIncreaseIndex = this.PreButtonIncreaseIndex;
            //
            baseItem.ShowBackgroud = this.ShowBackgroud;
            baseItem.ShowOutLine = this.ShowOutLine;
            foreach (BaseItem one in this.BaseItems)
            {
                baseItem.BaseItems.Add(one.Clone() as BaseItem);
            }
            baseItem.TopViewItemIndex = this.TopViewItemIndex;
            //
            baseItem.VisibleEx = this.VisibleEx;
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
            if (this.GetEventState("TopViewItemIndexChanged") == EventStateStyle.eUsed) baseItem.TopViewItemIndexChanged += new IntValueChangedHandler(baseItem_TopViewItemIndexChanged);
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
        void baseItem_TopViewItemIndexChanged(object sender, IntValueChangedEventArgs e)
        {
            this.RelationEvent("TopViewItemIndexChanged", e);
        }
        #endregion

        protected override void OnDraw(PaintEventArgs e)
        {
            SetRibbonBarMinState();
            //
            this.Relayout(e.Graphics, LayoutStyle.eLayoutPlan, true);
            this.Relayout(e.Graphics, LayoutStyle.eLayoutAuto, false);
            //
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonPage(
                new GISShare.Controls.WinForm.ObjectRenderEventArgs(e.Graphics, this, this.DisplayRectangle));
            ////
            //base.OnDraw(e);
        }
        private void SetRibbonBarMinState()
        {
            int iTemp = 0;
            //
            int iLength = 0;
            int iMinWith = 0;//待调整对象的 最小化 宽度
            int iWidth = 0;
            int iItemsWidth = 0;
            RibbonBarItem ribbonBarItem = null;
            RibbonBarItem ribbonBarItemMin = null;
            RibbonBarItem ribbonBarItemNomal = null;
            foreach (BaseItem one in this.BaseItems)
            {
                iLength += one.Width;
                //
                ribbonBarItem = one as RibbonBarItem;
                if (ribbonBarItem == null) continue;
                iTemp = ribbonBarItem.Width;
                if (iWidth == 0) { iWidth = iTemp; }
                if (iWidth <= iTemp) { iWidth = iTemp; ribbonBarItemNomal = ribbonBarItem; }
                if (ribbonBarItem.IsMinState)
                {
                    iTemp = ribbonBarItem.GetItemsSize().Width;
                    if (iItemsWidth == 0) { iItemsWidth = iTemp; }
                    if (iItemsWidth >= iTemp) { iItemsWidth = iTemp; iMinWith = ribbonBarItem.Width; ribbonBarItemMin = ribbonBarItem; }
                }
                else
                {
                    iTemp = ribbonBarItem.Width;
                    if (iWidth == 0) { iWidth = iTemp; }
                    if (iWidth <= iTemp) { iWidth = iTemp; ribbonBarItemNomal = ribbonBarItem; }
                }
            }
            if (iLength > this.Width)
            {
                if (ribbonBarItemNomal != null)
                {
                    ISetRibbonBarHelper pSetRibbonBarHelper = ribbonBarItemNomal as ISetRibbonBarHelper;
                    if (pSetRibbonBarHelper != null) pSetRibbonBarHelper.SetIsMinState(true);
                }
                //GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show(iLength + "    " + this.Width);
            }
            else if ((this.Width - iLength) > (iItemsWidth - iMinWith + this.PreButtonRectangle.Width))
            {
                if (ribbonBarItemMin != null)
                {
                    ISetRibbonBarHelper pSetRibbonBarHelper = ribbonBarItemMin as ISetRibbonBarHelper;
                    if (pSetRibbonBarHelper != null) pSetRibbonBarHelper.SetIsMinState(false);
                }
            }
        }
    }
}
