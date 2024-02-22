using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    [ToolboxItem(true), Designer(typeof(GISShare.Controls.WinForm.WFNew.Design.TabControlDesigner)), ToolboxBitmap(typeof(TabControl), "TabControl.bmp")]
    public class TabControl : WFNew.AreaControl, WFNew.ITabControl, WFNew.ITabControlHelper, WFNew.ICollectionItem, WFNew.ICollectionItem2, WFNew.ICollectionItem3, WFNew.IUICollectionItem, ICollectionObjectDesignHelper
    {
        private const int CRT_TABBUTTONCONTAINERHEIGHT = 21;

        [Browsable(true), Description("TabPage选择索引改变事件"), Category("属性已更改")]
        public event IntValueChangedHandler TabPageSelectedIndexChanged;                  //TabPage选择索引改变事件

        private TabButtonContainerTCItem m_TabButtonContainerItem = null;                     //TabButtonList组件
        private WFNew.TabPageCollection m_TabPageCollection;                                 //TabPage收集器
        private WFNew.BaseItemCollection m_BaseItemCollection;

        public TabControl()
        {
            this.m_BaseItemCollection = new GISShare.Controls.WinForm.WFNew.BaseItemCollection(this);
            this.m_TabButtonContainerItem = new TabButtonContainerTCItem();
            this.m_BaseItemCollection.Add(this.m_TabButtonContainerItem);
            ((WFNew.ILockCollectionHelper)this.m_BaseItemCollection).SetLocked(true);
            //
            //
            //
            this.m_TabPageCollection = new WFNew.TabPageCollection(this);
            //
            //
            //
            this.m_TabButtonContainerItem.TabButtonItemSelectedIndexChanged += new GISShare.Controls.WinForm.IntValueChangedHandler(TabButtonList_TabButtonItemSelectedIndexChanged);
        }
        void TabButtonList_TabButtonItemSelectedIndexChanged(object sender, GISShare.Controls.WinForm.IntValueChangedEventArgs e)
        {
            this.OnTabPageSelectedIndexChanged(e);
        }

        protected override EventStateStyle GetEventStateSupplement(string strEventName)
        {
            switch (strEventName)
            {
                case "TabPageSelectedIndexChanged":
                    return this.TabPageSelectedIndexChanged != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
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
                case "TabPageSelectedIndexChanged":
                    if (this.TabPageSelectedIndexChanged != null) { this.TabPageSelectedIndexChanged(this, e as IntValueChangedEventArgs); }
                    return true;
                default:
                    break;
            }
            //
            return base.RelationEventSupplement(strEventName, e);
        }

        #region WFNew.IBaseItem
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
            TabControl baseItem = new TabControl();
            baseItem.CanExchangeItem = this.CanExchangeItem;
            baseItem.UsingCloseTabButton = this.UsingCloseTabButton;
            baseItem.AutoVisibleTabButton = this.AutoVisibleTabButton;
            baseItem.TabAlignment = this.TabAlignment;
            baseItem.eTabButtonContainerStyle = this.eTabButtonContainerStyle;
            baseItem.ePNLayoutStyle = this.ePNLayoutStyle;
            baseItem.TabPageSelectedIndex = this.TabPageSelectedIndex;
            foreach(WFNew.ITabPageItem one in this.TabPages)
            {
                ICloneable pCloneable = one as ICloneable;
                if (pCloneable == null) continue;
                this.TabPages.Add(pCloneable.Clone() as ITabPageItem);
            }
            if (this.GetEventState("TabPageSelectedIndexChanged") == EventStateStyle.eUsed) baseItem.TabPageSelectedIndexChanged += new IntValueChangedHandler(baseItem_TabPageSelectedIndexChanged);
            return baseItem;
        }
        void baseItem_TabPageSelectedIndexChanged(object sender, IntValueChangedEventArgs e)
        {
            this.RelationEvent("TabPageSelectedIndexChanged", e);
        }
        #endregion

        #region WFNew.ITabControl
        [Browsable(true), DefaultValue(true), Description("可交换TabPage"), Category("状态")]
        public bool CanExchangeItem
        {
            get { return this.m_TabButtonContainerItem.CanExchangeItem; }
            set { this.m_TabButtonContainerItem.CanExchangeItem = value; }
        }

        [Browsable(true), DefaultValue(true), Description("显示关闭按钮"), Category("布局")]
        public bool UsingCloseTabButton
        {
            get { return this.m_TabButtonContainerItem.UsingCloseTabButton; }
            set { this.m_TabButtonContainerItem.UsingCloseTabButton = value; }
        }

        [Browsable(true), DefaultValue(true), Description("是否自动显示"), Category("布局")]
        public bool AutoVisibleTabButton//是否自动显示TabButtonList
        {
            get { return this.m_TabButtonContainerItem.AutoVisible; }
            set { this.m_TabButtonContainerItem.AutoVisible = value; }
        }

        [Browsable(true), DefaultValue(false), Description("当通过下拉菜单选中的表头隐藏时是否自动置顶显示"), Category("布局")]
        public bool AutoShowOverflowTabButton
        {
            get { return this.m_TabButtonContainerItem.AutoShowOverflowTabButton; }
            set { this.m_TabButtonContainerItem.AutoShowOverflowTabButton = value; }
        }

        [Browsable(true), DefaultValue(TabAlignment.Top), Description("表头选项卡的位置"), Category("布局")]
        public TabAlignment TabAlignment//表头选项卡的位置
        {
            get { return this.m_TabButtonContainerItem.TabAlignment; }
            set { this.m_TabButtonContainerItem.TabAlignment = value; }
        }

        [Browsable(true), DefaultValue(TabButtonContainerStyle.ePreButtonAndNextButton), Description("表头类型"), Category("布局")]
        public WFNew.TabButtonContainerStyle eTabButtonContainerStyle
        {
            get { return this.m_TabButtonContainerItem.eTabButtonContainerStyle; }
            set { this.m_TabButtonContainerItem.eTabButtonContainerStyle = value; }
        }

        [Browsable(true), DefaultValue(typeof(PNLayoutStyle), "eBothEnds"), Description("存在溢出项时PN调节按钮的布局方式"), Category("布局")]
        public WFNew.PNLayoutStyle ePNLayoutStyle
        {
            get { return this.m_TabButtonContainerItem.ePNLayoutStyle; }
            set { this.m_TabButtonContainerItem.ePNLayoutStyle = value; }
        }

        [Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), Description("当前选中的索引"), Category("状态")]
        public int TabPageSelectedIndex//当前选中的索引（其实质就是TabButton索引）
        {
            get { return this.m_TabButtonContainerItem.TabButtonItemSelectedIndex; }
            set { this.m_TabButtonContainerItem.TabButtonItemSelectedIndex = value; }
        }

        [Browsable(false), Description("选中的TabPage"), Category("状态")]
        public WFNew.ITabPageItem SelectedTabPage//当前选中的TabPage
        {
            get 
            {
                WFNew.ITabButtonItem pTabButtonItem = this.m_TabButtonContainerItem.SelectTabButtonItem;
                if (pTabButtonItem == null) return null;
                return pTabButtonItem.pTabPageItem;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Bindable(true), Localizable(true), Description("TabPage收集器"), Category("集合")]
        public WFNew.TabPageCollection TabPages//TabPage收集器
        {
            get { return this.m_TabPageCollection; }
        }

        private int m_TabButtonContainerSize = CRT_TABBUTTONCONTAINERHEIGHT;
        [Browsable(true), DefaultValue(21), Description("TabButton容器区尺寸（最小值21）"), Category("布局")]
        public int TabButtonContainerSize
        {
            get { return m_TabButtonContainerSize; }
            set { m_TabButtonContainerSize = value < CRT_TABBUTTONCONTAINERHEIGHT ? CRT_TABBUTTONCONTAINERHEIGHT : value; }
        }

        [Browsable(false), Description("TabButtonContainerItem矩形框"), Category("布局")]
        public Rectangle TabButtonContainerRectangle
        {
            get
            {
                Rectangle rectangle = this.ShowOutLine ? new Rectangle(1, 1, base.DisplayRectangle.Width - 2, base.DisplayRectangle.Height - 2) : base.DisplayRectangle;
                //
                switch (this.TabAlignment)
                {
                    case TabAlignment.Top:
                        return new Rectangle(rectangle.Left, rectangle.Top, rectangle.Width, this.TabButtonContainerSize);
                    case TabAlignment.Bottom:
                        return new Rectangle(rectangle.Left, rectangle.Bottom - this.TabButtonContainerSize, rectangle.Width, this.TabButtonContainerSize);
                    case TabAlignment.Left:
                        return new Rectangle(rectangle.Left, rectangle.Top, this.TabButtonContainerSize, rectangle.Height);
                    case TabAlignment.Right:
                        return new Rectangle(rectangle.Right - this.TabButtonContainerSize, rectangle.Top, this.TabButtonContainerSize, rectangle.Height);
                    default:
                        return rectangle;
                }
            }
        }

        public void AddTabPage(WFNew.ITabPageItem pTabPageItem)
        {
            this.TabPages.Add(pTabPageItem);
        }

        public void RemoveTabPage(WFNew.ITabPageItem pTabPageItem)
        {
            this.TabPages.Remove(pTabPageItem);
        }

        public bool SetSelectTabPage(WFNew.ITabPageItem pTabPageItem)//根据tabPage设置选择的TabPage
        {
            if (pTabPageItem == null) return false;
            int index = this.TabPages.IndexOf(pTabPageItem);
            if (index < 0 || index >= this.TabPages.Count) return false;
            this.TabPageSelectedIndex = index;
            return true;
        }
        #endregion
        
        #region WFNew.ITabControlHelper
        IList WFNew.ITabControlHelper.TabPageList
        {
            get { return this.Controls; }
        }

        WFNew.BaseItemCollection WFNew.ITabControlHelper.TabButtonItemCollection
        {
            get { return this.m_TabButtonContainerItem.BaseItems; }
        }
        #endregion

        #region ICollectionItem
        [Browsable(false), Description("其所携带的子项集合中是否存在可见项（与此类无关）"), Category("状态")]
        bool WFNew.ICollectionItem.HaveVisibleBaseItem
        {
            get
            {
                foreach (WFNew.BaseItem one in this.m_BaseItemCollection)
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
        WFNew.BaseItemCollection WFNew.ICollectionItem.BaseItems
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
        IBaseItem WFNew.ICollectionItem3.GetBaseItem2(string strName)
        {
            return null;
        }
        #endregion

        #region IUICollectionItem
        Size IUICollectionItem.GetIdealSize(Graphics g)
        {
            return this.Size;
        }
        #endregion

        #region ICollectionObjectDesignHelper
        System.Collections.IList ICollectionObjectDesignHelper.List { get { return this.TabPages; } }

        bool ICollectionObjectDesignHelper.ExchangeItem(object item1, object item2) { return this.TabPages.ExchangeItem(item1, item2); }
        #endregion

        protected override void MessageMonitor(MessageInfo messageInfo)
        {
            base.MessageMonitor(messageInfo);
            //
            ((IMessageChain)this.m_TabButtonContainerItem).SendMessage(messageInfo);
        }

        [Browsable(true)]
        public override bool ShowOutLine
        {
            get
            {
                return base.ShowOutLine;
            }
            set
            {
                base.ShowOutLine = value;
            }
        }

        public override Rectangle DisplayRectangle
        {
            get
            {
                Rectangle rectangle = this.ShowOutLine ? new Rectangle(1, 1, base.DisplayRectangle.Width - 2, base.DisplayRectangle.Height - 2) : base.DisplayRectangle;
                //
                if (!this.m_TabButtonContainerItem.Visible) return rectangle;
                //
                switch (this.TabAlignment)
                {
                    case TabAlignment.Top:
                        return Rectangle.FromLTRB(rectangle.Left, rectangle.Top + this.TabButtonContainerSize, rectangle.Right, rectangle.Bottom);
                    case TabAlignment.Bottom:
                        return Rectangle.FromLTRB(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom - this.TabButtonContainerSize);
                    case TabAlignment.Left:
                        return Rectangle.FromLTRB(rectangle.Left + this.TabButtonContainerSize, rectangle.Top, rectangle.Right, rectangle.Bottom);
                    case TabAlignment.Right:
                        return Rectangle.FromLTRB(rectangle.Left, rectangle.Top, rectangle.Right - this.TabButtonContainerSize, rectangle.Bottom);
                    default:
                        return rectangle;
                }
            }
        }

        public override Rectangle ItemsRectangle
        {
            get
            {
                return base.DisplayRectangle;
            }
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            if (e.Control is TabPage)
            {
                base.OnControlAdded(e);
                //
                if (this.AutoVisibleTabButton)
                {
                    if (this.TabPages.Count == 2) { this.Refresh(); }
                }
            }
            else
            {
                this.Controls.Remove(e.Control);
            }
        }

        protected override void OnControlRemoved(ControlEventArgs e)
        {
            if (this.AutoVisibleTabButton)
            {
                if (this.TabPages.Count == 1) { this.Refresh(); }
            }
            //
            base.OnControlRemoved(e);
        }

        //事件
        protected virtual void OnTabPageSelectedIndexChanged(IntValueChangedEventArgs e)
        {
            if (this.TabPageSelectedIndexChanged != null) { this.TabPageSelectedIndexChanged(this, e); }
        }

        //
        //
        //

        class TabButtonContainerTCItem : WFNew.TabButtonContainerItem
        {
            public TabButtonContainerTCItem()
            {
                this.LineDistance = 0;
                this.ColumnDistance = 0;
                this.IsRestrictItems = true;
                this.PreButtonIncreaseIndex = false;
                this.IsStretchItems = true;
            }

            //public override int LineDistance
            //{
            //    get
            //    {
            //        return 0;
            //    }
            //    set
            //    {
            //        base.LineDistance = value;
            //    }
            //}

            //public override int ColumnDistance
            //{
            //    get
            //    {
            //        return 0;
            //    }
            //    set
            //    {
            //        base.ColumnDistance = value;
            //    }
            //}

            //public override bool IsRestrictItems
            //{
            //    get
            //    {
            //        return true;
            //    }
            //    set
            //    {
            //        base.IsRestrictItems = true;
            //    }
            //}

            //public override bool PreButtonIncreaseIndex
            //{
            //    get
            //    {
            //        return false;
            //    }
            //    set
            //    {
            //        base.PreButtonIncreaseIndex = false;
            //    }
            //}

            //public override bool IsStretchItems
            //{
            //    get
            //    {
            //        return true;
            //    }
            //    set
            //    {
            //        base.IsStretchItems = true;
            //    }
            //}
        }
    }

    
}
