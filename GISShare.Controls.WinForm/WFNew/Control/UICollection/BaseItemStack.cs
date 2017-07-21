using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    [Designer(typeof(GISShare.Controls.WinForm.WFNew.Design.BaseItemStackDesigner)), ToolboxItem(true)]
    public class BaseItemStack : AreaControl, 
        IOwner, IBaseItemOwner, IBaseItemOwner2, 
        IBaseItemStackItem, ICollectionObjectDesignHelper//, ISetOwnerHelper
    {
        private BaseItem m_BaseItemMouseDown = null;
        private BaseItem m_BaseItemMouseUp = null;

        public BaseItemStack()
            : base()
        {
            base.BackColor = System.Drawing.Color.Transparent;
            //
            this.m_BaseItemCollection = new BaseItemCollection(this);
        }

        #region ICollectionObjectDesignHelper
        System.Collections.IList ICollectionObjectDesignHelper.List { get { return this.BaseItems; } }

        bool ICollectionObjectDesignHelper.ExchangeItem(object item1, object item2) { return this.BaseItems.ExchangeItem(item1, item2); }
        #endregion

        #region ICollectionItem2
        public IBaseItem GetBaseItem(string strName)
        {
            IBaseItem pBaseItem = null;
            foreach (IBaseItem one in this.BaseItems)
            {
                if (one.Name == strName) pBaseItem = one;
                else
                {
                    ICollectionItem2 pCollectionItem2 = one as ICollectionItem2;
                    if (pCollectionItem2 != null)
                    {
                        pBaseItem = pCollectionItem2.GetBaseItem(strName);
                    }
                }
                //
                if (pBaseItem != null) break;
            }
            //
            return pBaseItem;
        }
        #endregion

        #region ICollectionItem3
        public IBaseItem GetBaseItem2(string strName)
        {
            IBaseItem pBaseItem = null;
            foreach (IBaseItem one in this.BaseItems)
            {
                if (one.Name == strName) pBaseItem = one;
                else
                {
                    ICollectionItem3 pCollectionItem3 = one as ICollectionItem3;
                    if (pCollectionItem3 != null)
                    {
                        pBaseItem = pCollectionItem3.GetBaseItem2(strName);
                    }
                }
                //
                if (pBaseItem != null) break;
            }
            //
            return pBaseItem;
        }
        #endregion

        #region IUICollectionItem
        [Browsable(false), Description("其所携带的子项集合中是否存在可见项"), Category("状态")]
        public bool HaveVisibleBaseItem
        {
            get
            {
                foreach (BaseItem one in this.BaseItems)
                {
                    if (one.Visible) return true;
                }
                //
                return false;
            }
        }

        private Orientation m_eOrientation = Orientation.Vertical;
        [Browsable(true), DefaultValue(typeof(Orientation), "Vertical"), Description("子项的布局方式"), Category("布局")]
        public virtual Orientation eOrientation
        {
            get { return m_eOrientation; }
            set { m_eOrientation = value; }
        }

        private BaseItemCollection m_BaseItemCollection = null;
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Description("其所携带的子项集合"), Category("子项")]
        public virtual BaseItemCollection BaseItems
        {
            get { return m_BaseItemCollection; }
        }

        /// <summary>
        /// 获取理想状态下的布局尺寸
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        public Size GetIdealSize(Graphics g)
        {
            return this.Relayout(g, LayoutStyle.eLayoutPlan, true);
        }
        #endregion

        [Browsable(false), Description("其子项展现矩形"), Category("布局")]
        public override Rectangle ItemsRectangle
        {
            get
            {
                Rectangle rectangle = this.DisplayRectangle;
                return new Rectangle(rectangle.X + this.Padding.Left,
                    rectangle.Y + this.Padding.Top, 
                    rectangle.Width - this.Padding.Left - this.Padding.Right,
                    rectangle.Height - this.Padding.Top - this.Padding.Bottom);
            }
        }

        #region IBaseItemStackItem
        bool m_ReverseLayout = false;
        [Browsable(true), DefaultValue(false), Description("是否实现反转布局"), Category("布局")]
        public virtual bool ReverseLayout
        {
            get { return m_ReverseLayout; }
            set { m_ReverseLayout = value; }
        }

        private int m_RestrictItemsWidth = -1;
        [Browsable(true), DefaultValue(false), Description("尝试锁定子项宽度（“-1”为不执行）"), Category("布局")]
        public virtual int RestrictItemsWidth
        {
            get { return m_RestrictItemsWidth; }
            set { m_RestrictItemsWidth = value; }
        }

        private int m_RestrictItemsHeight = -1;
        [Browsable(true), DefaultValue(false), Description("尝试锁定子项高度（“-1”为不执行）"), Category("布局")]
        public virtual int RestrictItemsHeight
        {
            get { return m_RestrictItemsHeight; }
            set { m_RestrictItemsHeight = value; }
        }

        private bool m_CanExchangeItem = false;
        [Browsable(true), DefaultValue(false), Description("鼠标按下抬起时交换位置"), Category("状态")]
        public virtual bool CanExchangeItem
        {
            get { return m_CanExchangeItem; }
            set { m_CanExchangeItem = value; }
        }

        private int m_MinSize = -1;
        [Browsable(true), DefaultValue(-1), Description("最小尺寸"), Category("布局")]
        public virtual int MinSize
        {
            get { return m_MinSize; }
            set
            {
                if (value < 10) return;
                m_MinSize = value;
            }
        }

        private int m_MaxSize = -1;
        [Browsable(true), DefaultValue(-1), Description("最大尺寸"), Category("布局")]
        public virtual int MaxSize
        {
            get { return m_MaxSize; }
            set
            {
                if (value - this.MinSize < 20) return;
                m_MaxSize = value;
            }
        }

        private bool m_IsStretchItems = true;
        [Browsable(true), DefaultValue(true), Description("拉伸子项"), Category("布局")]
        public virtual bool IsStretchItems
        {
            get { return m_IsStretchItems; }
            set { m_IsStretchItems = value; }
        }

        private bool m_IsRestrictItems = false;
        [Browsable(true), DefaultValue(false), Description("限制子项尺寸"), Category("布局")]
        public virtual bool IsRestrictItems
        {
            get { return m_IsRestrictItems; }
            set { m_IsRestrictItems = value; }
        }

        private int m_LineDistance = 1;
        [Browsable(true), DefaultValue(1), Description("竖向排列时的列间距"), Category("布局")]
        public virtual int LineDistance
        {
            get { return m_LineDistance; }
            set { m_LineDistance = value; }
        }

        private int m_ColumnDistance = 1;
        [Browsable(true), DefaultValue(1), Description("横向排列时的列间距"), Category("布局")]
        public virtual int ColumnDistance
        {
            get { return m_ColumnDistance; }
            set { m_ColumnDistance = value; }
        }

        internal protected int _OverflowItemsCount = 0;
        [Browsable(false), Description("是否存在溢出项"), Category("状态")]
        public int OverflowItemsCount
        {
            get
            {
                return this._OverflowItemsCount;
            }
        }

        internal protected int _DrawItemsCount = 0;
        [Browsable(false), Description("完整绘制项个数"), Category("状态")]
        public int DrawItemsCount
        {
            get
            {
                return this._DrawItemsCount;
            }
        }
        #endregion

        #region Clone
        public override object Clone()
        {
            BaseItemStack baseItem = new BaseItemStack();
            baseItem.Checked = this.Checked;
            baseItem.Enabled = this.Enabled;
            baseItem.Font = this.Font;
            baseItem.ForeColor = this.ForeColor;
            baseItem.Name = this.Name;
            baseItem.Site = this.Site;
            baseItem.Size = this.Size;
            baseItem.Tag = this.Tag;
            baseItem.Text = this.Text;
            baseItem.Padding = this.Padding;
            //
            baseItem.MinSize = this.MinSize;
            baseItem.MaxSize = this.MaxSize;
            baseItem.IsStretchItems = this.IsStretchItems;
            baseItem.IsRestrictItems = this.IsRestrictItems;
            baseItem.LineDistance = this.LineDistance;
            baseItem.ColumnDistance = this.ColumnDistance;
            baseItem.eOrientation = this.eOrientation;
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

        protected override void MessageMonitor(MessageInfo messageInfo)
        {
            base.MessageMonitor(messageInfo);
            //
            BaseItem baseItem;
            for (int i = 0; i < this.BaseItems.Count; i++)
            {
                baseItem = this.BaseItems[i];
                if (baseItem.pOwner != this) continue;
                //
                IMessageChain pMessageChain = baseItem as IMessageChain;
                if (pMessageChain != null)
                {
                    pMessageChain.SendMessage(messageInfo);
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (this.CanExchangeItem)
            {
                this.m_BaseItemMouseDown = this.BaseItems.GetBaseItemFromPoint(e.Location);
            }
            //
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (this.CanExchangeItem)
            {
                this.m_BaseItemMouseUp = this.BaseItems.GetBaseItemFromPoint(e.Location);
                if (this.BaseItems.ExchangeItem(this.m_BaseItemMouseDown, this.m_BaseItemMouseUp))
                {
                    this.m_BaseItemMouseDown.Refresh();
                    this.m_BaseItemMouseUp.Refresh();
                }
            }
            //
            base.OnMouseUp(e);
        }

        protected override void OnDraw(PaintEventArgs e)
        {
            this.Relayout(e.Graphics, LayoutStyle.eLayoutPlan, true);
            this.Relayout(e.Graphics, LayoutStyle.eLayoutAuto, false);
            //
            base.OnDraw(e);
        }
        protected virtual Size Relayout(Graphics g, LayoutStyle eLayoutStyle, bool bSetSize)
        {
            if (this.BaseItems.Count <= 0) return this.Size;
            //
            Rectangle rectangle = this.DisplayRectangle;
            Rectangle itemsRectangle = this.ItemsRectangle;
            //
            Size size = Size.Empty;
            if (this.ReverseLayout)
            {
                switch (this.eOrientation)
                {
                    case Orientation.Horizontal:
                        size = GISShare.Controls.WinForm.WFNew.LayoutEngine.LayoutStackH_RB(g, this, 0, this.IsStretchItems, this.IsRestrictItems,
                            this.ColumnDistance, this.RestrictItemsWidth, this.RestrictItemsHeight,
                            itemsRectangle.Left - rectangle.Left, itemsRectangle.Top - rectangle.Top, rectangle.Right - itemsRectangle.Right, rectangle.Bottom - itemsRectangle.Bottom,
                            this.MinSize, this.MaxSize, eLayoutStyle, ref this._OverflowItemsCount, ref this._DrawItemsCount);
                        break;
                    case Orientation.Vertical:
                        size = GISShare.Controls.WinForm.WFNew.LayoutEngine.LayoutStackV_RB(g, this, 0, this.IsStretchItems, this.IsRestrictItems,
                            this.LineDistance, this.RestrictItemsWidth, this.RestrictItemsHeight,
                            itemsRectangle.Left - rectangle.Left, itemsRectangle.Top - rectangle.Top, rectangle.Right - itemsRectangle.Right, rectangle.Bottom - itemsRectangle.Bottom,
                            this.MinSize, this.MaxSize, eLayoutStyle, ref this._OverflowItemsCount, ref this._DrawItemsCount);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (this.eOrientation)
                {
                    case Orientation.Horizontal:
                        size = GISShare.Controls.WinForm.WFNew.LayoutEngine.LayoutStackH_LT(g, this, 0, this.IsStretchItems, this.IsRestrictItems,
                            this.ColumnDistance, this.RestrictItemsWidth, this.RestrictItemsHeight,
                            itemsRectangle.Left - rectangle.Left, itemsRectangle.Top - rectangle.Top, rectangle.Right - itemsRectangle.Right, rectangle.Bottom - itemsRectangle.Bottom,
                            this.MinSize, this.MaxSize, eLayoutStyle, ref this._OverflowItemsCount, ref this._DrawItemsCount);
                        break;
                    case Orientation.Vertical:
                        size = GISShare.Controls.WinForm.WFNew.LayoutEngine.LayoutStackV_LT(g, this, 0, this.IsStretchItems, this.IsRestrictItems,
                            this.LineDistance, this.RestrictItemsWidth, this.RestrictItemsHeight,
                            itemsRectangle.Left - rectangle.Left, itemsRectangle.Top - rectangle.Top, rectangle.Right - itemsRectangle.Right, rectangle.Bottom - itemsRectangle.Bottom,
                            this.MinSize, this.MaxSize, eLayoutStyle, ref this._OverflowItemsCount, ref this._DrawItemsCount);
                        break;
                    default:
                        break;
                }
            }
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
    }
}
