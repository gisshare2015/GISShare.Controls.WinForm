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
    [Designer(typeof(GISShare.Controls.WinForm.WFNew.Design.RibbonApplicationPopupPanelDesigner)), ToolboxItem(false)]
    public class RibbonApplicationPopupPanel : BaseItemControl, IOwner, IBaseItemOwner, IRibbonApplicationPopupPanelItem, IUICollectionItem, ICollectionObjectDesignHelper
    {
        private const int CRT_TOPHEIGHT = 16;
        private const int CRT_BOTTOMHEIGHT = 26;

        BaseItemCollection m_BaseItemCollection;
        RibbonApplicationPopupPanelMiddleLeftItem m_RibbonApplicationPopupPanelMiddleLeft;
        RibbonApplicationPopupPanelMiddleRightItem m_RibbonApplicationPopupPanelMiddleRight;
        RibbonApplicationPopupPanelBottomItem m_RibbonApplicationPopupPanelBottom;

        public RibbonApplicationPopupPanel()
        {
            this.m_BaseItemCollection = new BaseItemCollection(this);
            //((WFNew.ILockCollectionHelper)this.m_BaseItemCollection).SetLocked(false);//解锁
            this.m_RibbonApplicationPopupPanelMiddleLeft = new RibbonApplicationPopupPanelMiddleLeftItem(this);
            this.m_BaseItemCollection.Add(this.m_RibbonApplicationPopupPanelMiddleLeft);
            //((ISetOwnerHelper)this.m_RibbonApplicationPopupPanelMiddleLeft).SetOwner(this);
            this.m_RibbonApplicationPopupPanelMiddleRight = new RibbonApplicationPopupPanelMiddleRightItem(this);
            this.m_BaseItemCollection.Add(this.m_RibbonApplicationPopupPanelMiddleRight);
            //((ISetOwnerHelper)this.m_RibbonApplicationPopupPanelMiddleRight).SetOwner(this);
            this.m_RibbonApplicationPopupPanelBottom = new RibbonApplicationPopupPanelBottomItem(this);
            this.m_BaseItemCollection.Add(this.m_RibbonApplicationPopupPanelBottom);
            //((ISetOwnerHelper)this.m_RibbonApplicationPopupPanelBottom).SetOwner(this);
            ((WFNew.ILockCollectionHelper)this.m_BaseItemCollection).SetLocked(true);//加锁
        }

        public override DockStyle Dock
        {
            get
            {
                return DockStyle.None;
            }
            set
            {
                base.Dock = DockStyle.None;
            }
        }

        #region ICollectionObjectDesignHelper
        System.Collections.IList ICollectionObjectDesignHelper.List { get { return ((ICollectionItem)this).BaseItems; } }

        bool ICollectionObjectDesignHelper.ExchangeItem(object item1, object item2) { return false; }
        #endregion

        #region BaseItemControl
        public override bool LockHeight
        {
            get { return false; }
        }

        public override bool LockWith
        {
            get { return false; }
        }

        public override Padding Padding
        {
            get
            {
                return new Padding(1);
            }
            set
            {
                base.Padding = new Padding(1);
            }
        }

        #region Clone
        public override object Clone()
        {
            RibbonApplicationPopupPanel baseItem = new RibbonApplicationPopupPanel();
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
        #endregion
        
        #region IPopupPanel
        public void TrySetPopupPanelSize(Size size)
        {
            this.Size = size;
        }
        #endregion

        #region ICollectionItem
        [Browsable(false), Description("其所携带的子项集合中是否存在可见项（与此类无关）"), Category("状态")]
        bool ICollectionItem.HaveVisibleBaseItem
        {
            get
            {
                foreach (BaseItem one in ((ICollectionItem)this).BaseItems)
                {
                    if (one.Visible) return true;
                }
                //
                return false;
            }
        }

        /// <summary>
        /// 一个零散的组建集合，它是锁定的无法移除和添加，没有需要请不要修改内部成员属性以防出现意外情况
        /// </summary>
        [Browsable(false), Description("其携带的子项（一个零散的组建集合，它是锁定的无法移除和添加，没有需要请不要修改内部成员属性以防出现意外情况）"), Category("子项")]
        BaseItemCollection ICollectionItem.BaseItems
        {
            get { return m_BaseItemCollection; }
        }
        #endregion

        #region ICollectionItem2
        IBaseItem WFNew.ICollectionItem2.GetBaseItem(string strName)
        {
            WFNew.IBaseItem pBaseItem = null;
            foreach (WFNew.IBaseItem one in ((WFNew.ICollectionItem)this).BaseItems)
            {
                if (one.Name == strName) pBaseItem = one;
                else
                {
                    WFNew.ICollectionItem2 pCollectionItem2 = one as WFNew.ICollectionItem2;
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
            WFNew.IBaseItem pBaseItem = null;
            foreach (WFNew.IBaseItem one in this.MenuItems)
            {
                if (one.Name == strName) pBaseItem = one;
                else
                {
                    WFNew.ICollectionItem3 pCollectionItem3 = one as WFNew.ICollectionItem3;
                    if (pCollectionItem3 != null)
                    {
                        pBaseItem = pCollectionItem3.GetBaseItem2(strName);
                    }
                }
                //
                if (pBaseItem != null) return pBaseItem;
            }
            //
            foreach (WFNew.IBaseItem one in this.RecordItems)
            {
                if (one.Name == strName) pBaseItem = one;
                else
                {
                    WFNew.ICollectionItem3 pCollectionItem3 = one as WFNew.ICollectionItem3;
                    if (pCollectionItem3 != null)
                    {
                        pBaseItem = pCollectionItem3.GetBaseItem2(strName);
                    }
                }
                //
                if (pBaseItem != null) return pBaseItem;
            }
            //
            foreach (WFNew.IBaseItem one in this.OperationItems)
            {
                if (one.Name == strName) pBaseItem = one;
                else
                {
                    WFNew.ICollectionItem3 pCollectionItem3 = one as WFNew.ICollectionItem3;
                    if (pCollectionItem3 != null)
                    {
                        pBaseItem = pCollectionItem3.GetBaseItem2(strName);
                    }
                }
                //
                if (pBaseItem != null) return pBaseItem;
            }
            //
            return pBaseItem;
        }
        #endregion

        #region IUICollectionItem
        Size IUICollectionItem.GetIdealSize(Graphics g)
        {
            return this.Size;
        }
        #endregion

        #region IRibbonApplicationPopupPanel
        [Browsable(false), Description("框架绘制矩形"), Category("布局")]
        public Rectangle FrameRectangle
        {
            get
            {
                Rectangle rectangle = this.DisplayRectangle;
                return new Rectangle(rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
            }
        }
        [Browsable(false), Description("顶部矩形区"), Category("布局")]
        public Rectangle TopRectangle
        {
            get
            {
                Rectangle rectangle = this.DisplayRectangle;
                return new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, CRT_TOPHEIGHT);
            }
        }
        [Browsable(false), Description("中部矩形区"), Category("布局")]
        public Rectangle MiddleRectangle
        {
            get
            {
                Rectangle rectangle = this.DisplayRectangle;
                return new Rectangle(rectangle.X, rectangle.Y + CRT_TOPHEIGHT, rectangle.Width, rectangle.Height - CRT_TOPHEIGHT - CRT_BOTTOMHEIGHT);
            }
        }
        [Browsable(false), Description("底部矩形区"), Category("布局")]
        public Rectangle BottomRectangle
        {
            get
            {
                Rectangle rectangle = this.DisplayRectangle;
                return new Rectangle(rectangle.X, rectangle.Bottom - CRT_BOTTOMHEIGHT, rectangle.Width, CRT_BOTTOMHEIGHT);
            }
        }
        [Browsable(false), Description("菜单项矩形区"), Category("布局")]
        public Rectangle MenuItemsRectangle
        {
            get
            {
               return this.m_RibbonApplicationPopupPanelMiddleLeft.DisplayRectangle;
            }
        }
        [Browsable(false), Description("记录项矩形区"), Category("布局")]
        public Rectangle RecordItemsRectangle
        {
            get
            {
                return this.m_RibbonApplicationPopupPanelMiddleRight.DisplayRectangle;
            }
        }
        [Browsable(false), Description("底部操作按钮矩形区"), Category("布局")]
        public Rectangle OperationItemsRectangle
        {
            get
            {
                return this.m_RibbonApplicationPopupPanelBottom.DisplayRectangle;
            }
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

        [Browsable(false), Description("其子项展现矩形"), Category("布局")]
        public override Rectangle ItemsRectangle
        {
            get
            {
                Rectangle rectangle = this.DisplayRectangle;
                return new Rectangle(
                    rectangle.Left + this.Padding.Left,
                    rectangle.Top + this.Padding.Top + CRT_TOPHEIGHT,
                    rectangle.Width - this.Padding.Left - this.Padding.Right,
                    rectangle.Height - this.Padding.Top - CRT_TOPHEIGHT - this.Padding.Bottom
                    );
            }
        }

        [Browsable(false), Description("锚点 用来规划菜单按钮的控制弹出菜单的坐落点（屏幕坐标）"), Category("布局")]
        public Point AnchorPoint { get { return this.m_RibbonApplicationPopupPanelMiddleRight.DisplayRectangle.Location; } }

        [Browsable(false), Description("锚区 用来规划菜单按钮的控制弹出菜单的矩形大小"), Category("布局")]
        public Rectangle AnchorRectangle { get { return this.m_RibbonApplicationPopupPanelMiddleRight.DisplayRectangle; } }

        [Browsable(false), Description("指定的顶部高"), Category("布局")]
        public int ToHeight { get { return CRT_TOPHEIGHT; } }

        [Browsable(false), Description("指定的底部高"), Category("布局")]
        public int BottomHeight { get { return CRT_BOTTOMHEIGHT; } }

        [Browsable(false), Description("指定的菜单按钮的统一尺寸"), Category("布局")]
        public int MenuItemHeight { get { return 39; } }

        [Browsable(false), Description("指定的记录区按钮的统一尺寸"), Category("布局")]
        public int RecordItemHeight { get { return 23; } }

        [Browsable(false), Description("指定的菜单按钮最多的可见个数"), Category("布局")]
        public int MaxMenuItemsCount { get { return 10; } }

        [Browsable(false), Description("菜单区、纪录区统一的最大高"), Category("布局")]
        public int MinMRHeight { get { return this.MenuItemHeight; } }

        [Browsable(false), Description("菜单区、纪录区统一的最小高"), Category("布局")]
        public int MaxMRHeight { get { return this.MenuItemHeight * this.MaxMenuItemsCount; } }

        [Browsable(false), Description("顶部、底部、菜单区、纪录区 的间隙"), Category("布局")]
        public int MROMiddleSpace { get { return 1; } }

        [Browsable(false), Description("菜单区的宽度"), Category("布局")]
        public int MenuItemsWidth { get { return this.m_RibbonApplicationPopupPanelMiddleLeft.DisplayRectangle.Width; } }

        [Browsable(false), Description("纪录区相对于菜单区的缩放因子"), Category("布局")]
        public double RecordItemsWidthFactor { get { return 1.6; } }

        Rectangle m_MRRectangle = new Rectangle();
        [Browsable(false), Description("菜单区、纪录区矩形区"), Category("布局")]
        public Rectangle MRRectangle
        {
            get
            {
                return m_MRRectangle;
            }
        }

        [Browsable(true), Description("记录区子项的间隙"), Category("布局")]
        public int OperationItemsSpace
        {
            get { return this.m_RibbonApplicationPopupPanelBottom.ColumnDistance; }
            set { this.m_RibbonApplicationPopupPanelBottom.ColumnDistance = value; }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Description("菜单项携带的子项集合"), Category("子项")]
        public BaseItemCollection MenuItems
        {
            get { return this.m_RibbonApplicationPopupPanelMiddleLeft.BaseItems; }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Description("记录项携带的子项集合"), Category("子项")]
        public BaseItemCollection RecordItems
        {
            get { return this.m_RibbonApplicationPopupPanelMiddleRight.BaseItems; }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Description("操作按钮项携带的子项集合"), Category("子项")]
        public BaseItemCollection OperationItems
        {
            get { return this.m_RibbonApplicationPopupPanelBottom.BaseItems; }
        }

        [Browsable(true), DefaultValue(2), Description("操作按钮的间距"), Category("布局")]
        public int OperationItemsColumnDistance
        {
            get { return this.m_RibbonApplicationPopupPanelBottom.ColumnDistance; }
            set { this.m_RibbonApplicationPopupPanelBottom.ColumnDistance = value; }
        }

        /// <summary>
        /// 获取理想尺寸 即 完全布局后的尺寸
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        public Size GetIdealSize(Graphics g)
        {
            int iMaxWithMiddleLeft = this.GetMaxWithMiddleLeft(g);
            if (this.MRRectangle.Width < iMaxWithMiddleLeft) this.GetMRRectangle(g);
            return new Size(
                this.Padding.Left + iMaxWithMiddleLeft + this.MROMiddleSpace + (int)(iMaxWithMiddleLeft * this.RecordItemsWidthFactor) + this.Padding.Right,
                this.Padding.Top + CRT_TOPHEIGHT + this.MRRectangle.Height + CRT_BOTTOMHEIGHT + this.Padding.Bottom
                );
        }
        private int GetMaxWithMiddleLeft(Graphics g)
        {
            int iSize = 0;
            foreach (BaseItem one in this.m_RibbonApplicationPopupPanelMiddleLeft.BaseItems)
            {
                 if (!one.Visible) continue;
                 //
                 Size size = one.MeasureSize(g);
                 if (!one.LockHeight && !one.LockWith)
                 {
                     one.Size = new Size(size.Width, size.Height);
                 }
                 else if (one.LockHeight && !one.LockWith)
                 {
                     one.Size = new Size(size.Width, one.Height);
                 }
                 else if (!one.LockHeight && one.LockWith)
                 {
                     one.Size = new Size(one.Width, size.Height);
                 }
                //                                
                if (one.Width > iSize) iSize = one.Width;
            }
            return this.m_RibbonApplicationPopupPanelMiddleLeft.Padding.Left + iSize + this.m_RibbonApplicationPopupPanelMiddleLeft.Padding.Right;
        }
        #endregion

        protected override void MessageMonitor(MessageInfo messageInfo)
        {
            base.MessageMonitor(messageInfo);
            //
            BaseItem baseItem;
            for (int i = 0; i < this.m_BaseItemCollection.Count; i++)
            {
                baseItem = this.m_BaseItemCollection[i];
                if (baseItem.pOwner != this) continue;
                //
                IMessageChain pMessageChain = baseItem as IMessageChain;
                if (pMessageChain != null)
                {
                    pMessageChain.SendMessage(messageInfo);
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            this.m_MRRectangle = this.GetMRRectangle(e.Graphics);
            //
            this.OnDraw(e);
            //
            base.OnPaint(e);
            //
            this.Width = this.Padding.Left +
                this.m_RibbonApplicationPopupPanelMiddleLeft.DisplayRectangle.Width +
                this.MROMiddleSpace + 
                this.m_RibbonApplicationPopupPanelMiddleRight.DisplayRectangle.Width + 
                this.Padding.Right;
            this.Height = 
                this.Padding.Top + 
                CRT_TOPHEIGHT + 
                this.MRRectangle.Height + 
                CRT_BOTTOMHEIGHT +
                this.Padding.Bottom;
        }
        private Rectangle GetMRRectangle(Graphics g)
        {
            Rectangle rectangle = this.ItemsRectangle;
            int iHeightL = this.m_RibbonApplicationPopupPanelMiddleLeft.GetIdealSize(g).Height;
            int iHeightR = this.m_RibbonApplicationPopupPanelMiddleRight.GetIdealSize(g).Height;
            return new Rectangle
                (
                rectangle.X,
                rectangle.Y,
                rectangle.Width,
                iHeightL > iHeightR ? iHeightL : iHeightR
                );
        }

        protected virtual void OnDraw(PaintEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonApplicationPopupPanel(
                new GISShare.Controls.WinForm.ObjectRenderEventArgs(e.Graphics, this, this.DisplayRectangle));
        }
    }
}
