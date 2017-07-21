using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LiuZhenHong.Controls.DockPanel
{
    [ToolboxItem(false), Designer(typeof(TabControlDesigner))]
    public class TabControl : Ribbon.BaseItemControl, Ribbon.ITabControl, Ribbon.ITabControlHelper, Ribbon.ICollectionItem, Ribbon.IBaseItemOwner2//System.Windows.Forms.Control
    {
        private const int CRT_TABBUTTONCONTAINERHEIGHT = 21;

        public event TabPageSelectIndexChangedEventHandler TabPageSelectIndexChanged;     //TabPage选择索引改变事件

        private TabButtonContainerItem m_TabButtonList = null;                         //TabButtonList组件
        internal TabButtonContainerItem TabButtonList
        {
            get { return m_TabButtonList; }
        }
        private TabPageCollection m_TabPages;                                 //TabPage收集器

        private Ribbon.BaseItemCollection m_BaseItemCollection;
        public TabControl()
        {
            this.m_BaseItemCollection = new LiuZhenHong.Controls.Ribbon.BaseItemCollection(this);

            this.m_TabButtonList = new TabButtonContainerItem();
            this.m_TabButtonList.TabAlignment = TabAlignment.Bottom;
            this.m_BaseItemCollection.Add(this.m_TabButtonList);
            //
            this.m_TabPages = new TabPageCollection(this);
            //
            this.m_TabButtonList.TabButtonItemSelectedIndexChanged += new LiuZhenHong.Controls.Ribbon.IndexChangedHandler(m_TabButtonList_TabButtonItemSelectedIndexChanged);
        }
        void m_TabButtonList_TabButtonItemSelectedIndexChanged(object sender, LiuZhenHong.Controls.Ribbon.IndexChangedEventArgs e)
        {
            if (this.TabPageSelectIndexChanged != null)
            {
                this.OnTabPageSelectIndexChanged(new TabPageEventArgs(this.TabPageSelectIndex, this.SelectTabPage, sender));
            }
        }

        #region Ribbon.ITabControl
        void Ribbon.ITabControl.RemoveTabPage(Ribbon.ITabPageItem pTabPageItem) { }
        #endregion
        
        #region Ribbon.ITabControlHelper
        IList Ribbon.ITabControlHelper.TabPageList
        {
            get { return this.Controls; }
        }

        Ribbon.BaseItemCollection Ribbon.ITabControlHelper.TabButtonItemCollection
        {
            get { return this.m_TabButtonList.BaseItems; }
        }
        #endregion

        #region ICollectionItem
        [Browsable(false), Description("其所携带的子项集合中是否存在可见项（与此类无关）"), Category("状态")]
        bool Ribbon.ICollectionItem.HaveVisibleBaseItem
        {
            get
            {
                foreach (Ribbon.BaseItem one in ((Ribbon.ICollectionItem)this).BaseItems)
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
        Ribbon.BaseItemCollection Ribbon.ICollectionItem.BaseItems
        {
            get { return m_BaseItemCollection; }
        }
        #endregion

        public override object Clone()
        {
            return new TabPage();
        }

        public override bool LockHeight
        {
            get { return false; }
        }

        public override bool LockWith
        {
            get { return false; }
        }

        #region IBaseItemOwner
        [Browsable(false), Description("取消其子项的所有事件"), Category("状态")]
        public virtual bool CancelItemsEvent
        {
            get
            {
                return false;
            }
        }

        [Browsable(false), Description("零散的子项布局矩形框"), Category("布局")]
        public Rectangle ItemsRectangle
        {
            get
            {
                return base.DisplayRectangle;
            }
        }

        [Browsable(false), Description("获取其子项拥有者"), Category("关联")]
        public Ribbon.IBaseItemOwner pBaseItemOwner
        {
            get { return pOwner as Ribbon.IBaseItemOwner; }
        }

        public bool RefreshEx()
        {
            this.Refresh();
            return true;
        }
        #endregion

        #region 属性
        [Browsable(true), DefaultValue(TabAlignment.Bottom)]
        public TabAlignment Alignment//表头选项卡的位置
        {
            get { return this.m_TabButtonList.TabAlignment; }
            set { this.m_TabButtonList.TabAlignment = value; }
        }

        [Browsable(true), DefaultValue(true)]
        public bool AutoVisibleTabButton//是否自动显示TabButtonList
        {
            get { return this.m_TabButtonList.AutoVisible; }
            set { this.m_TabButtonList.AutoVisible = value; }
        }

        [Browsable(true), DefaultValue(true)]
        public bool ShowTabButtonList//是否显示TabButtonList
        {
            get { return this.m_TabButtonList.Visible; }
            set { this.m_TabButtonList.Visible = value; }
        }

        [Browsable(false)]
        public TabPage SelectTabPage//当前选中的TabPage
        {
            get 
            {
                Ribbon.ITabButtonItem pTabButtonItem = this.m_TabButtonList.SelectTabButtonItem;
                if (pTabButtonItem == null) return null;
                return pTabButtonItem.pTabPageItem as TabPage;
            }
        }

        [Browsable(false)]
        public int TabPageCount
        { get { return this.Controls.Count; } }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int TabPageSelectIndex//当前选中的索引（其实质就是TabButton索引）
        {
            get { return this.m_TabButtonList.TabButtonItemSelectedIndex; }
            set { this.m_TabButtonList.TabButtonItemSelectedIndex = value; }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Bindable(true), Localizable(true)]
        public TabPageCollection TabPages//TabPage收集器
        {
            get { return this.m_TabPages; }
        }
        #endregion

        #region 公开函数
        public void ClearTabPages()
        {
            this.m_TabPages.Clear();
        }

        public bool Contains(TabPage tabPage)
        {
            return this.m_TabButtonList.BaseItems.Contains(tabPage.pTabButtonItem as Ribbon.BaseItem);
        }

        public int AddTabPage(TabPage tabPage)
        {
            return this.m_TabPages.Add(tabPage);
        }

        public void RemoveTabPageAt(int index)
        {
            this.m_TabPages.RemoveAt(index);
        }

        public void RemoveTabPage(TabPage tabPage)
        {
            this.m_TabPages.Remove(tabPage);
        }

        public int IndexOfTabPage(TabPage tabPage)//获取TabPage索引（其实质就是TabButton索引）
        {
            return this.m_TabButtonList.BaseItems.IndexOf(tabPage.TabButton);
        }

        public void Insert(int index, TabPage tabPage)
        {
            this.m_TabPages.Insert(index,tabPage);
        }

        public void ExchangeTabButton(int index1, int index2)//根据索引交换两个TabButton的位置
        {
            this.m_TabButtonList.BaseItems.ExchangeItem(index1, index2);
        }

        public bool SetSelectTabPage(TabPage tabPage)//根据tabPage设置选择的TabPage
        {
            if (tabPage == null) return false;
            int index = IndexOfTabButton(tabPage.TabButton);
            if (index < 0 || index >= this.TabPages.Count) return false;
            this.TabPageSelectIndex = index;
            return true;
        }
        #endregion

        internal int IndexOfTabButton(TabButton tabButton)//获取TabButton索引
        {
            return this.m_TabButtonList.BaseItems.IndexOf(tabButton);
        }

        //事件
        protected virtual void OnTabPageSelectIndexChanged(TabPageEventArgs e)
        {
            if (this.TabPageSelectIndexChanged != null) { this.TabPageSelectIndexChanged(this, e); }
        }

        //
        //
        //

        public override Rectangle DisplayRectangle
        {
            get
            {
                if (!this.m_TabButtonList.Visible) return base.DisplayRectangle;
                //
                Rectangle rectangle = base.DisplayRectangle;
                switch (this.Alignment)
                {
                    case TabAlignment.Top:
                        return Rectangle.FromLTRB(rectangle.Left, rectangle.Top + CRT_TABBUTTONCONTAINERHEIGHT, rectangle.Right, rectangle.Bottom);
                    case TabAlignment.Bottom:
                        return Rectangle.FromLTRB(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom - CRT_TABBUTTONCONTAINERHEIGHT);
                    case TabAlignment.Left:
                        return Rectangle.FromLTRB(rectangle.Left + CRT_TABBUTTONCONTAINERHEIGHT, rectangle.Top, rectangle.Right, rectangle.Bottom);
                    case TabAlignment.Right:
                        return Rectangle.FromLTRB(rectangle.Left, rectangle.Top, rectangle.Right - CRT_TABBUTTONCONTAINERHEIGHT, rectangle.Bottom);
                    default:
                        return rectangle;
                }
            }
        }

        public Rectangle TabButtonContainerRectangle 
        {
            get 
            {
                Rectangle rectangle = base.DisplayRectangle;
                switch (this.Alignment)
                {
                    case TabAlignment.Top:
                        return new Rectangle(rectangle.Left, rectangle.Top, rectangle.Width, CRT_TABBUTTONCONTAINERHEIGHT);
                    case TabAlignment.Bottom:
                        return new Rectangle(rectangle.Left, rectangle.Bottom - CRT_TABBUTTONCONTAINERHEIGHT, rectangle.Width, CRT_TABBUTTONCONTAINERHEIGHT);
                    case TabAlignment.Left:
                        return new Rectangle(rectangle.Left, rectangle.Top, CRT_TABBUTTONCONTAINERHEIGHT, rectangle.Height);
                    case TabAlignment.Right:
                        return new Rectangle(rectangle.Right - CRT_TABBUTTONCONTAINERHEIGHT, rectangle.Top, CRT_TABBUTTONCONTAINERHEIGHT, rectangle.Height);
                    default:
                        return rectangle;
                }
            }
        }

        //
        //
        //

        /// <summary>
        /// TabPage收集器
        /// </summary>
        public class TabPageCollection : Ribbon.TabPageCollection //IList, ICollection, IEnumerable
        {
            internal TabPageCollection(Ribbon.ITabControlHelper pTabControlHelper) : base(pTabControlHelper) { }

            public int Add(object value) { return base.Add(value as Ribbon.ITabPageItem); }

            public void Insert(int index, object value) { base.Insert(index, value as Ribbon.ITabPageItem); }

            //private TabControl owner;

            //public TabPageCollection(TabControl tabControl)
            //{
            //    this.owner = tabControl;
            //}

            //public int Add(object value)
            //{
            //    TabPage tabPage = value as TabPage;
            //    if (tabPage == null) return -1;
            //    if (this.owner.TabButtonList.BaseItems.Add(tabPage.TabButton) >= 0)
            //    {
            //        this.owner.Controls.Add(tabPage);
            //        return this.Count - 1;
            //    }
            //    return -1;
            //}

            //public void Clear()
            //{
            //    this.owner.Controls.Clear();
            //    this.owner.TabButtonList.BaseItems.Clear();
            //}

            //public bool Contains(object value)
            //{
            //    return this.owner.Controls.Contains(value as Control);
            //}

            //public int IndexOf(object value)
            //{
            //    return this.owner.Controls.IndexOf(value as Control);
            //}

            //public void Insert(int index, object value)
            //{
            //    TabPage tabPage = value as TabPage;
            //    if (tabPage == null) return;
            //    this.owner.Controls.Add(tabPage);
            //    this.owner.Controls.SetChildIndex(tabPage, index);
            //    this.owner.TabButtonList.BaseItems.Insert(index, tabPage.TabButton);
            //}

            //public void Remove(object value)
            //{
            //    TabPage tabPage = value as TabPage;
            //    if (tabPage == null) return;
            //    this.owner.TabButtonList.BaseItems.Remove(tabPage.TabButton);
            //    this.owner.Controls.Remove(tabPage);
            //}

            //public void RemoveAt(int index)
            //{
            //    this.Remove(this[index]);
            //}

            //public int Count
            //{
            //    get
            //    {
            //        return this.owner.Controls.Count;
            //    }
            //}

            //public IEnumerator GetEnumerator()
            //{
            //    return this.owner.Controls.GetEnumerator();
            //}

            //void ICollection.CopyTo(Array destination, int index)
            //{
            //    this.owner.Controls.CopyTo(destination, 0);
            //}

            //protected virtual object[] GetItems()
            //{
            //    TabPage[] destinationArray = new TabPage[this.Count];
            //    if (this.Count > 0)
            //    {
            //        this.owner.Controls.CopyTo(destinationArray, 0);
            //    }
            //    return destinationArray;
            //}

            //public bool IsReadOnly
            //{
            //    get
            //    {
            //        return false;
            //    }
            //}

            //// Properties
            //[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            //public virtual object this[int index]
            //{
            //    get
            //    {
            //        if ((index < 0) || (index >= this.Count))
            //        {
            //            return null;
            //        }
            //        return this.owner.Controls[index];
            //    }
            //    set
            //    {
            //        this.RemoveAt(index);
            //        this.Insert(index, value);
            //        //this.owner.Controls[index] = value as Control;
            //    }
            //}

            //bool ICollection.IsSynchronized
            //{
            //    get
            //    {
            //        return false;
            //    }
            //}

            //object ICollection.SyncRoot
            //{
            //    get
            //    {
            //        return this;
            //    }
            //}

            //bool IList.IsFixedSize
            //{
            //    get
            //    {
            //        return false;
            //    }
            //}
        }
    }

    /// <summary>
    /// 委托 TabPage选择索引改变
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void TabPageSelectIndexChangedEventHandler(object sender, TabPageEventArgs e);

    /// <summary>
    /// TabPage选择索引改变事件参数
    /// </summary>
    public class TabPageEventArgs : EventArgs
    {
        public TabPageEventArgs(int selectIndex, TabPage tabPage)
        {
            this._SelectIndex = selectIndex;
            this._TabPage = tabPage;
        }

        public TabPageEventArgs(int selectIndex, TabPage tabPage, object other)
        {
            this._SelectIndex = selectIndex;
            this._TabPage = tabPage;
            this._Other = other;
        }

        int _SelectIndex;
        public int SelectIndex
        {
            get { return _SelectIndex; }
        }

        TabPage _TabPage;
        public TabPage TabPage
        {
            get { return _TabPage; }
        }

        object _Other;
        public object Other
        {
            get { return _Other; }
            set { _Other = value; }
        }
    } 


    class TabButtonContainerItem : Ribbon.RibbonTabButtonContainerItem
    {
        public override bool IsRestrictItems
        {
            get
            {
                return true;
            }
            set
            {
                base.IsRestrictItems = true;
            }
        }

        public override bool PreButtonIncreaseIndex
        {
            get
            {
                return false;
            }
            set
            {
                base.PreButtonIncreaseIndex = false;
            }
        }

        public override Orientation eOrientation
        {
            get
            {
                return Orientation.Horizontal;
            }
            set
            {
                base.eOrientation = Orientation.Horizontal;
            }
        }

        public override bool IsRestrictSize
        {
            get
            {
                return false;
            }
            set
            {
                base.IsRestrictSize = false;
            }
        }

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

        public override bool UseMaxMinSize
        {
            get
            {
                return false;
            }
            set
            {
                base.UseMaxMinSize = false;
            }
        }
    }
}
