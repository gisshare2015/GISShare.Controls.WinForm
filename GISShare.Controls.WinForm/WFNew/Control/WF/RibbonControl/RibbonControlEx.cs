using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace GISShare.Controls.WinForm.WFNew
{
    [Designer(typeof(GISShare.Controls.WinForm.WFNew.Design.RibbonControlExDesigner)), DefaultEvent("RibbonPageSelectedIndexChanged"), ToolboxItem(true)]
    public class RibbonControlEx : AreaControl, ITabControl, IRibbonControl, IRibbonControlEvent, ICollectionItem, ICollectionItem2, ICollectionItem3, IUICollectionItem, ICollectionObjectDesignHelper
    {
        #region ˽�г�Ա
        private const int CRT_TOPCOMPNONENTHEIGHT = 47;            // �����������
        private const int CRT_MIDDLECOMPNONENTHEIGHT = 93;         // �м䲿���������   -5
        private const int CRT_BOTTOMCOMPNONENTHEIGHT = 25;         // �ײ��������
        private const int CRT_CAPTIONHEIGHT = 24;                  // �������ߣ������� ��������� ֮�ڣ�
        private const int CRT_MINCAPTIONTEXTWIDTH = 160;           // �����ı����ߣ������� ������ ֮�ڣ�
        private const int CRT_TABBUTTONCONTAINERHEIGHT = 22;       // TabButton������
        private readonly int CRT_NORMALHEIGHT_TOPTOOLBAR = CRT_TOPCOMPNONENTHEIGHT + CRT_MIDDLECOMPNONENTHEIGHT + 1;                                // �ؼ��ߣ�������������
        private readonly int CRT_NORMALHEIGHT_BOTTOMTOOLBAR = CRT_TOPCOMPNONENTHEIGHT + CRT_MIDDLECOMPNONENTHEIGHT + CRT_BOTTOMCOMPNONENTHEIGHT + 1;// �ؼ��ߣ��ײ���������
        private readonly int CRT_HIDERIBBONPAGEHEIGHT_TOPTOOLBAR = CRT_TOPCOMPNONENTHEIGHT + 2;                                                     // ���ع�������ؼ��ߣ�������������
        private readonly int CRT_HIDERIBBONPAGEHEIGHT_BOTTOMTOOLBAR = CRT_TOPCOMPNONENTHEIGHT + CRT_BOTTOMCOMPNONENTHEIGHT + 2;                     // ���ع�������ؼ��ߣ��ײ���������
        //
        private BaseItemCollection m_BaseItemCollection;
        private RibbonApplicationPopup m_RibbonApplicationPopup;
        private RibbonStartButtonItem2007Ex m_RibbonStartButtonItem2007Ex;
        private RibbonStartButtonItem2010Ex m_RibbonStartButtonItem2010Ex;
        private RibbonQuickAccessToolbarItemEx m_RibbonQuickAccessToolbarItemEx;
        private RibbonFormButtonStackItem m_RibbonFormButtonStackItem;
        private RibbonPageContentContainerItem m_RibbonPageContentContainerItem;
        private RibbonMdiFormButtonStackItem m_RibbonMdiFormButtonStackItem;
        private RibbonPageTabButtonContainerItem m_RibbonPageTabButtonContainerItem;
        private RibbonPageContainerControl m_RibbonPageContainerControl;
        private RibbonPageContainerPopup m_RibbonPageContainerPopup;
        //
        private SuperToolTip m_SuperToolTip;
        #endregion

        protected override EventStateStyle GetEventStateSupplement(string strEventName)
        {
            switch (strEventName)
            {
                case "RibbonPageSelectedIndexChanged":
                    return this.RibbonPageSelectedIndexChanged != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
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
                case "RibbonPageSelectedIndexChanged":
                    if (this.RibbonPageSelectedIndexChanged != null) { this.RibbonPageSelectedIndexChanged(this, e as IntValueChangedEventArgs); }
                    return true;
                default:
                    break;
            }
            //
            return base.RelationEventSupplement(strEventName, e);
        }

        #region ICollectionItem
        [Browsable(false), Description("����Я������������Ƿ���ڿɼ��������޹أ�"), Category("״̬")]
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
        /// һ����ɢ���齨���ϣ������������޷��Ƴ�����ӣ�û����Ҫ�벻Ҫ�޸��ڲ���Ա�����Է������������
        /// </summary>
        [Browsable(false), Description("��Я�������һ����ɢ���齨���ϣ������������޷��Ƴ�����ӣ�û����Ҫ�벻Ҫ�޸��ڲ���Ա�����Է��������������"), Category("����")]
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
            foreach (WFNew.IBaseItem one in this.ToolbarItems)
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
            foreach (WFNew.IBaseItem one in this.PageContents)
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
            foreach (WFNew.IBaseItem one in this.RibbonPages)
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
            foreach (WFNew.IBaseItem one in this.ApplicationPopup.MenuItems)
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
            foreach (WFNew.IBaseItem one in this.ApplicationPopup.RecordItems)
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
            foreach (WFNew.IBaseItem one in this.ApplicationPopup.OperationItems)
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

        public RibbonControlEx()
        {
            base.Dock = DockStyle.Top;
            base.BackColor = System.Drawing.Color.Transparent;
            //
            //
            //
            this.m_RibbonApplicationPopup = new RibbonApplicationPopup();
            //
            this.m_BaseItemCollection = new BaseItemCollection(this);
            //((WFNew.ILockCollectionHelper)this.m_BaseItemCollection).SetLocked(false);//����
            this.m_RibbonStartButtonItem2007Ex = new RibbonStartButtonItem2007Ex(this.m_RibbonApplicationPopup, this);
            this.m_BaseItemCollection.Add(this.m_RibbonStartButtonItem2007Ex);//-
            this.m_RibbonStartButtonItem2010Ex = new RibbonStartButtonItem2010Ex(this.m_RibbonApplicationPopup, this);
            this.m_BaseItemCollection.Add(this.m_RibbonStartButtonItem2010Ex);//-
            this.m_RibbonQuickAccessToolbarItemEx = new RibbonQuickAccessToolbarItemEx(this);
            this.m_BaseItemCollection.Add(this.m_RibbonQuickAccessToolbarItemEx);//-
            this.m_RibbonFormButtonStackItem = new RibbonFormButtonStackItem(this);
            this.m_BaseItemCollection.Add(this.m_RibbonFormButtonStackItem);//-
            this.m_RibbonPageContentContainerItem = new RibbonPageContentContainerItem(this);
            this.m_BaseItemCollection.Add(this.m_RibbonPageContentContainerItem);//-
            this.m_RibbonMdiFormButtonStackItem = new RibbonMdiFormButtonStackItem(this);
            this.m_BaseItemCollection.Add(this.m_RibbonMdiFormButtonStackItem);//-
            this.m_RibbonPageTabButtonContainerItem = new RibbonPageTabButtonContainerItem(this);
            this.m_RibbonPageTabButtonContainerItem.TabButtonItemSelectedIndexChanged += new IntValueChangedHandler(RibbonPageTabButtonContainerItem_TabButtonItemSelectedIndexChanged);
            this.m_BaseItemCollection.Add(this.m_RibbonPageTabButtonContainerItem);//-
            ((WFNew.ILockCollectionHelper)this.m_BaseItemCollection).SetLocked(true);//����
            //
            this.m_RibbonPageContainerControl = new RibbonPageContainerControl(this.m_RibbonPageTabButtonContainerItem);
            //((ISetOwnerHelper)this.m_RibbonPageContainerControl).SetOwner(this);
            this.Controls.Add(this.m_RibbonPageContainerControl);
            //
            this.m_RibbonPageContainerPopup = new RibbonPageContainerPopup(this.m_RibbonPageContainerControl);
            ((ISetOwnerHelper)this.m_RibbonPageContainerPopup).SetOwner(this);
            //
            //
            //
            this.m_SuperToolTip = new SuperToolTip();
            this.m_SuperToolTip.OffsetX = 0;
            this.m_SuperToolTip.OffsetY = 12;
            foreach (BaseItem one in this.m_RibbonFormButtonStackItem.BaseItems)
            {
                this.m_SuperToolTip.SetToolTip(one as IBaseItem2);
            }
            //
            this.m_RibbonQuickAccessToolbarItemEx.BaseItems.ItemAdded += new ItemEventHandler(BaseItems_ItemAdded);
            this.m_RibbonQuickAccessToolbarItemEx.BaseItems.ItemRemoved += new ItemEventHandler(BaseItems_ItemRemoved);
        }
        void BaseItems_ItemAdded(object sender, ItemEventArgs e)
        {
            if (e.Item is IUICollectionItem) return;
            this.m_SuperToolTip.SetToolTip(e.Item as IBaseItem2);
        }
        void BaseItems_ItemRemoved(object sender, ItemEventArgs e)
        {
            if (e.Item is IUICollectionItem) return;
            this.m_SuperToolTip.RemoveToolTip(e.Item as IBaseItem2);
        }
        void RibbonPageTabButtonContainerItem_TabButtonItemSelectedIndexChanged(object sender, IntValueChangedEventArgs e)
        {
            this.OnRibbonPageSelectedIndexChanged(e);
        }

        #region �ڲ���Ա
        internal bool RibbonStartButtonItem2007Visible { get { return this.m_RibbonStartButtonItem2007Ex.Visible; } }
        internal bool RibbonStartButtonItem2010Visible { get { return this.m_RibbonStartButtonItem2010Ex.Visible; } }
        internal bool RibbonQuickAccessToolbarItemVisible { get { return this.m_RibbonQuickAccessToolbarItemEx.Visible; } }
        internal bool RibbonFormButtonStackItemVisible { get { return this.m_RibbonFormButtonStackItem.Visible; } }
        internal bool RibbonPageContentContainerItemVisible { get { return this.m_RibbonPageContentContainerItem.Visible; } }
        internal bool RibbonMdiFormButtonStackItemVisible { get { return this.m_RibbonMdiFormButtonStackItem.Visible; } }
        internal bool RibbonPageTabButtonContainerItemVisible { get { return this.m_RibbonPageTabButtonContainerItem.Visible; } }
        //
        internal int RibbonStartButtonItem2007Width { get { return this.m_RibbonStartButtonItem2007Ex.Width; } }
        internal int RibbonStartButtonItem2010Width { get { return this.m_RibbonStartButtonItem2010Ex.Width; } }
        internal int RibbonQuickAccessToolbarItemWidth { get { return this.m_RibbonQuickAccessToolbarItemEx.DisplayRectangle.Width; } }
        internal int RibbonFormButtonStackItemWidth { get { return this.m_RibbonFormButtonStackItem.DisplayRectangle.Width; } }
        internal int RibbonPageContentContainerItemWidth { get { return this.m_RibbonPageContentContainerItem.DisplayRectangle.Width; } }
        internal int RibbonMdiFormButtonStackItemWidth { get { return this.m_RibbonMdiFormButtonStackItem.DisplayRectangle.Width; } }
        internal int RibbonPageTabButtonContainerItemWidth { get { return this.m_RibbonPageTabButtonContainerItem.DisplayRectangle.Width; } }
        //
        internal int MinCaptionTextWidth { get { return CRT_MINCAPTIONTEXTWIDTH; } }
        #endregion

        #region ICollectionObjectDesignHelper
        System.Collections.IList ICollectionObjectDesignHelper.List { get { return ((ICollectionItem)this).BaseItems; } }

        bool ICollectionObjectDesignHelper.ExchangeItem(object item1, object item2) { return false; }
        #endregion

        #region ITabControl
        [Browsable(true), DefaultValue(true), Description("�ɽ���TabPage"), Category("״̬")]
        public bool CanExchangeItem
        {
            get { return this.m_RibbonPageTabButtonContainerItem.CanExchangeItem; }
            set { this.m_RibbonPageTabButtonContainerItem.CanExchangeItem = value; }
        }

        [Browsable(true), DefaultValue(true), Description("��ʾ�رհ�ť"), Category("����")]
        public bool UsingCloseTabButton
        {
            get { return this.m_RibbonPageTabButtonContainerItem.UsingCloseTabButton; }
            set { this.m_RibbonPageTabButtonContainerItem.UsingCloseTabButton = value; }
        }

        [Browsable(true), DefaultValue(false), Description("��ͨ�������˵�ѡ�еı�ͷ����ʱ�Ƿ��Զ��ö���ʾ"), Category("����")]
        public bool AutoShowOverflowTabButton
        {
            get { return this.m_RibbonPageTabButtonContainerItem.AutoShowOverflowTabButton; }
            set { this.m_RibbonPageTabButtonContainerItem.AutoShowOverflowTabButton = value; }
        }

        [Browsable(true), DefaultValue(TabButtonContainerStyle.ePreButtonAndNextButton), Description("��ͷ����"), Category("����")]
        public WFNew.TabButtonContainerStyle eTabButtonContainerStyle
        {
            get { return this.m_RibbonPageTabButtonContainerItem.eTabButtonContainerStyle; }
            set { this.m_RibbonPageTabButtonContainerItem.eTabButtonContainerStyle = value; }
        }

        [Browsable(true), DefaultValue(typeof(PNLayoutStyle), "eBothEnds"), Description("���������ʱPN���ڰ�ť�Ĳ��ַ�ʽ"), Category("����")]
        public WFNew.PNLayoutStyle ePNLayoutStyle
        {
            get { return this.m_RibbonPageTabButtonContainerItem.ePNLayoutStyle; }
            set { this.m_RibbonPageTabButtonContainerItem.ePNLayoutStyle = value; }
        }

        [Browsable(false), DefaultValue(true), Description("�Ƿ��Զ���ʾ"), Category("����")]
        bool ITabControl.AutoVisibleTabButton//�Ƿ��Զ���ʾTabButtonList
        {
            get { return this.m_RibbonPageTabButtonContainerItem.AutoVisible; }
            set {  }
        }

        [Browsable(false), Description("TabButtonContainerItem���ο�"), Category("����")]
        Rectangle ITabControl.TabButtonContainerRectangle
        {
            get
            {
                Rectangle rectangle = this.CaptionRectangle;
                return new Rectangle(
                    rectangle.Left,
                    rectangle.Bottom + 1,
                    rectangle.Width - (this.RibbonMdiFormButtonStackItemVisible ? this.RibbonPageContentContainerItemWidth + this.RibbonMdiFormButtonStackItemWidth : this.RibbonPageContentContainerItemWidth),
                    CRT_TABBUTTONCONTAINERHEIGHT);
            }
        }
        
        [Browsable(false), DefaultValue(TabAlignment.Top), Description("��ͷѡ���λ��"), Category("����")]
        TabAlignment ITabControl.TabAlignment//��ͷѡ���λ��
        {
            get { return  TabAlignment.Top; }
            set { this.m_RibbonPageTabButtonContainerItem.TabAlignment = TabAlignment.Top ; }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), Description("��ǰѡ�е�����"), Category("״̬")]
        int ITabControl.TabPageSelectedIndex//��ǰѡ�е���������ʵ�ʾ���TabButton������
        {
            get { return this.RibbonPageSelectedIndex; }
            set { this.RibbonPageSelectedIndex = value; }
        }

        [Browsable(false), Description("ѡ�е�TabPage"), Category("״̬")]
        WFNew.ITabPageItem ITabControl.SelectedTabPage//��ǰѡ�е�TabPage
        {
            get
            {
                return this.SelectedRibbonPage;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Bindable(true), Localizable(true), Description("TabPage�ռ���"), Category("����")]
        WFNew.TabPageCollection ITabControl.TabPages//TabPage�ռ���
        {
            get { return this.RibbonPages; }
        }

        void ITabControl.AddTabPage(WFNew.ITabPageItem pTabPageItem)
        {
            this.RibbonPages.Add(pTabPageItem);
        }

        void ITabControl.RemoveTabPage(WFNew.ITabPageItem pTabPageItem)
        {
            this.RibbonPages.Remove(pTabPageItem);
        }

        bool ITabControl.SetSelectTabPage(WFNew.ITabPageItem pTabPageItem)
        {
            return this.SetSelectRibbonPage(pTabPageItem as RibbonPage);
        }
        #endregion

        #region IRibbonControlEvent
        [Browsable(true), Description("�����������ͼ�����ı�󴥷�"), Category("�����Ѹ���")]
        public event IntValueChangedHandler RibbonPageSelectedIndexChanged;
        #endregion

        #region IRibbonControl
        [Browsable(false), Description("�Ƿ񼤻�"), Category("״̬")]
        public bool IsActive
        {
            get
            {
                if (this.RibbonForm == null) return true;
                return this.RibbonForm.IsActive;
            }
        }

        [Browsable(false), Description("��������"), Category("����")]
        public int CaptionHeight
        {
            get { return CRT_CAPTIONHEIGHT; }
        }

        [Browsable(false), Description("�ɼ�"), Category("״̬")]
        public Point AnchorPoint { get { return this.MiddleCompnonentRectangle.Location; } }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Description("Ӧ�ó����ݲ˵�"), Category("����")]
        public IApplicationPopup ApplicationPopup
        {
            get { return this.m_RibbonApplicationPopup; }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Description("��ݹ�����Я���������"), Category("����")]
        public BaseItemCollection ToolbarItems
        {
            get { return this.m_RibbonQuickAccessToolbarItemEx.BaseItems; }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Description("����������Ҳఴť�б���Я���������"), Category("����")]
        public BaseItemCollection PageContents
        {
            get { return this.m_RibbonPageContentContainerItem.BaseItems; }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Description("��������弯��"), Category("����")]
        public RibbonPageCollection RibbonPages
        {
            get { return this.m_RibbonPageContainerControl.RibbonPageCollection; }
        }

        [Browsable(true), DefaultValue(-1), Description("�����������ͼ����"), Category("����")]
        public int RibbonPageSelectedIndex
        {
            get 
            {
                return this.m_RibbonPageTabButtonContainerItem.TabButtonItemSelectedIndex;
            }
            set
            {
                this.m_RibbonPageTabButtonContainerItem.TabButtonItemSelectedIndex = value;
            }
        }

        #region Radius
        private int m_LeftTopRadius = 0;
        [Browsable(true), DefaultValue(0), Description("�󶥲�Բ��ֵ"), Category("Բ��")]
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
        [Browsable(true), DefaultValue(0), Description("�Ҷ���Բ��ֵ"), Category("Բ��")]
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
        [Browsable(true), DefaultValue(0), Description("��ײ�Բ��ֵ"), Category("Բ��")]
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
        [Browsable(true), DefaultValue(0), Description("�ҵײ�Բ��ֵ"), Category("Բ��")]
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

        private Form m_ParentForm;
        [Browsable(true), Description("���������Ĵ���"), Category("����")]
        public Form ParentForm
        {
            get { return this.m_ParentForm; }
            set { this.m_ParentForm = value; }
        }

        [Browsable(false), Description("����������RibbonForm�ӿ�"), Category("����")]
        public IRibbonForm RibbonForm
        {
            get { return this.ParentForm as IRibbonForm; }
        }

        private bool m_HideRibbonPage = false;
        [Browsable(true), DefaultValue(false), Description("���ع�����"), Category("״̬")]
        public bool HideRibbonPage
        {
            get { return m_HideRibbonPage; }
            set
            {
                if (m_HideRibbonPage == value) return;
                m_HideRibbonPage = value;
                if (m_HideRibbonPage) this.m_RibbonPageContainerPopup.HideRibbonPageContainer();
                else this.m_RibbonPageContainerPopup.DisplayRibbonPageContainer();
                this.Refresh();
            }
        }

        private bool m_IsTopToolbar = true;
        [Browsable(true), DefaultValue(true), Description("��ʾ������ݹ�����"), Category("״̬")]
        public bool IsTopToolbar
        {
            get { return m_IsTopToolbar; }
            set
            {
                if(m_IsTopToolbar == value) return;
                m_IsTopToolbar = value;
                this.Refresh();
            }
        }

        private RibbonStyle m_eRibbonStyle = RibbonStyle.eOffice2007;
        [Browsable(true), DefaultValue(typeof(RibbonStyle), "eOffice2007"), Description("�������ؼ�����"), Category("״̬")]
        public RibbonStyle eRibbonStyle
        {
            get { return m_eRibbonStyle; }
            set
            {
                if (m_eRibbonStyle == value) return;
                m_eRibbonStyle = value;
                this.Invalidate(this.TopCompnonentRectangle);
            }
        }

        [Browsable(true), DefaultValue(typeof(QuickAccessToolbarStyle), "eNone"), Description("��ݹ�����������ͣ���������ܵ�eRibbonStyle��Ӱ�죩"), Category("���")]
        public QuickAccessToolbarStyle eQuickAccessToolbarStyle 
        {
            get { return this.m_RibbonQuickAccessToolbarItemEx.eQuickAccessToolbarStyle; }
            set { this.m_RibbonQuickAccessToolbarItemEx.eQuickAccessToolbarStyle = value; }
        }

        [Browsable(false), Description("��ǰ��ʹ�õľ��ο�"), Category("����")]
        public Rectangle UsingRectangle
        {
            get { return new Rectangle(0, 0, this.Width, this.Height); }
        }

        [Browsable(false), Description("��ɢ������־��ο�"), Category("����")]
        public override Rectangle ItemsRectangle
        {
            get
            {
                Rectangle rectangle = this.UsingRectangle;
                return new Rectangle(
                    rectangle.Left + this.Padding.Left,
                    rectangle.Top + this.Padding.Top,
                    rectangle.Width - this.Padding.Left - this.Padding.Right,
                    rectangle.Height - this.Padding.Top - this.Padding.Bottom
                    );
            }
        }

        [Browsable(false), Description("����������ο�"), Category("����")]
        public Rectangle TopCompnonentRectangle
        {
            get
            {
                Rectangle rectangle = this.ItemsRectangle;
                return new Rectangle(rectangle.Left, rectangle.Top, rectangle.Width, CRT_TOPCOMPNONENTHEIGHT);
            }
        }

        [Browsable(false), Description("�м�������ο�"), Category("����")]
        public Rectangle MiddleCompnonentRectangle
        {
            get
            {
                Rectangle rectangle = this.ItemsRectangle;
                return new Rectangle(
                    rectangle.Left + 1, 
                    rectangle.Top + CRT_TOPCOMPNONENTHEIGHT, 
                    rectangle.Width - 3, 
                    this.HideRibbonPage ? 0 : CRT_MIDDLECOMPNONENTHEIGHT
                    );
            }
        }

        [Browsable(false), Description("��������岼�ֵľ��ο�"), Category("����")]
        public Rectangle PageDisplayRectangle
        {
            get
            {
                if (this.HideRibbonPage)
                {
                    return new Rectangle(0, 0, this.DisplayRectangle.Width, CRT_MIDDLECOMPNONENTHEIGHT + 1);
                }
                else
                {
                    Rectangle rectangle = this.MiddleCompnonentRectangle;
                    return new Rectangle(rectangle.Left + 2, rectangle.Top + 2, rectangle.Width - 4, rectangle.Height - 4);
                }
            }
        }

        [Browsable(false), Description("���صײ����������ο�"), Category("����")]
        public Rectangle BottomCompnonentRectangle
        {
            get
            {
                Rectangle rectangle = this.ItemsRectangle;
                if (this.HideRibbonPage) return new Rectangle(rectangle.Left, rectangle.Top + CRT_TOPCOMPNONENTHEIGHT, rectangle.Width, CRT_BOTTOMCOMPNONENTHEIGHT);
                else return new Rectangle(rectangle.Left, rectangle.Top + CRT_TOPCOMPNONENTHEIGHT + CRT_MIDDLECOMPNONENTHEIGHT - 1, rectangle.Width, CRT_BOTTOMCOMPNONENTHEIGHT);
            }
        }

        [Browsable(false), Description("������ƾ��ο�"), Category("����")]
        public Rectangle CaptionRectangle//������ο�
        {
            get
            {
                Rectangle rectangle = this.ItemsRectangle;
                return new Rectangle(rectangle.Left, rectangle.Top, rectangle.Width, CRT_CAPTIONHEIGHT);
            }
        }

        [Browsable(false), Description("�����ı����ƾ��ο�"), Category("����")]
        public Rectangle CaptionTextRectangle//���ƶ��ı�����ο�
        {
            get
            {
                Rectangle rectangle = this.CaptionRectangle;
                if (this.IsTopToolbar && this.m_RibbonQuickAccessToolbarItemEx.Visible)
                {
                    //return new Rectangle(
                    //    this.m_RibbonQuickAccessToolbarItemEx.DisplayRectangle.Right + 1,
                    //    rectangle.Top,
                    //    this.RibbonFormButtonStackItemVisible ? rectangle.Width - this.m_RibbonQuickAccessToolbarItemEx.Width - this.m_RibbonFormButtonStackItem.Width - 2 : rectangle.Width - this.m_RibbonQuickAccessToolbarItemEx.Width - 2,
                    //    rectangle.Height);
                    return Rectangle.FromLTRB(
                        this.m_RibbonQuickAccessToolbarItemEx.DisplayRectangle.Right + 1,
                        rectangle.Top,
                        this.RibbonFormButtonStackItemVisible ? this.m_RibbonFormButtonStackItem.DisplayRectangle.Left : this.ItemsRectangle.Right,
                        rectangle.Bottom
                        );
                }
                else
                {
                    if (this.RibbonStartButtonItem2007Visible)
                    {
                        Rectangle rectangle2 = this.m_RibbonStartButtonItem2007Ex.DisplayRectangle;
                        return new Rectangle(
                            rectangle2.Right + 1,
                            rectangle.Top,
                            this.RibbonFormButtonStackItemVisible ? rectangle.Width - (rectangle2.Right - rectangle2.Left) - this.m_RibbonFormButtonStackItem.Width - 2 : rectangle.Width - (rectangle2.Right - rectangle2.Left) - 2,
                            rectangle.Height);
                    }
                    else
                    {
                        if (this.Icon != null)
                        {
                            Rectangle rectangle2 = this.IconRectangle;
                            return new Rectangle(
                                rectangle2.Right + 1,
                                rectangle.Top,
                                this.RibbonFormButtonStackItemVisible ? rectangle.Width - (rectangle2.Right - rectangle2.Left) - this.m_RibbonFormButtonStackItem.Width - 2 : rectangle.Width - (rectangle2.Right - rectangle2.Left) - 2,
                                rectangle.Height);
                        }
                        else
                        {
                            return new Rectangle(
                                rectangle.Left + 1,
                                rectangle.Top,
                                this.RibbonFormButtonStackItemVisible ? rectangle.Width - this.m_RibbonFormButtonStackItem.Width - 2 : rectangle.Width - 2,
                                rectangle.Height);
                        }
                    }
                }
            }
        }

        [Browsable(false), Description("ͼƬ���ƾ��ο�"), Category("����")]
        public Rectangle IconRectangle
        {
            get
            {
                if (this.RibbonStartButtonItem2007Visible) return this.m_RibbonStartButtonItem2007Ex.ImageRectangle;
                //
                Rectangle rectangle = this.CaptionRectangle;
                if (this.Icon == null) return new Rectangle(rectangle.X, rectangle.Y, 0, 0);
                //
                return new Rectangle(rectangle.Left + 3, rectangle.Top + 2, 20, 20);
            }
        }

        [Browsable(false), Description("ͼƬ"), Category("���")]
        public Icon Icon
        {
            get
            {
                if (this.ParentForm != null &&
                    this.ParentForm.ShowIcon &&
                    this.ParentForm.Icon != null) return this.ParentForm.Icon;
                //
                return null;
            }
        }

        [Browsable(false), Description("��ȡ�ڲ�Я����MenuStrip�������������Ӵ���"), Category("����")]
        public MenuStrip MenuStrip
        {
            get { return this.m_RibbonMdiFormButtonStackItem.MenuStrip; }
        }

        [Browsable(false), Description("ApplicationPopup�Ƿ���չ��������"), Category("״̬")]
        public bool IsApplicationPopupOpened
        {
            get
            {
                return this.m_RibbonApplicationPopup.IsOpened;
            }
        }

        public void ShowApplicationPopup()
        {
            if (this.IsPagesOpened) return;
            //
            switch (this.eRibbonStyle) 
            {
                case RibbonStyle.eOffice2007:
                    this.m_RibbonStartButtonItem2007Ex.ShowPopup();
                    break;
                case RibbonStyle.eOffice2010:
                    this.m_RibbonStartButtonItem2010Ex.ShowPopup();
                    break;
            }
        }

        public void CloseApplicationPopup()
        {
            if (!this.IsPagesOpened) return;
            //
            switch (this.eRibbonStyle)
            {
                case RibbonStyle.eOffice2007:
                    this.m_RibbonStartButtonItem2007Ex.ClosePopup();
                    break;
                case RibbonStyle.eOffice2010:
                    this.m_RibbonStartButtonItem2010Ex.ClosePopup();
                    break;
            }
        }

        private int m_PagesPopupSpace = 0;
        [Browsable(true), DefaultValue(0), Description("Pages�����˵�����Я���ߵļ��"), Category("����")]
        public int PagesPopupSpace
        {
            get { return m_PagesPopupSpace; }
            set { m_PagesPopupSpace = value; }
        }

        [Browsable(false), Description("Pages�����˵�������㣨��Ļ���꣩"), Category("����")]
        public Point PagesPopupLoction
        {
            get
            {
                return this.PointToScreen(new Point(0, CRT_TOPCOMPNONENTHEIGHT + PagesPopupSpace));
            }
        }

        [Browsable(false), Description("PagesPopup�Ƿ���չ��������"), Category("״̬")]
        public bool IsPagesOpened
        {
            get { return this.m_RibbonPageContainerPopup.IsOpened; }
        }

        public void ShowPagesPopup()
        {
            if (!this.HideRibbonPage || this.IsPagesOpened) return;
            //
            this.m_RibbonPageContainerPopup.SetEnvelope(this.PageDisplayRectangle);
            this.m_RibbonPageContainerPopup.Show(this.PagesPopupLoction);
        }

        public void ClosePagesPopup()
        {
            if (!this.HideRibbonPage || !this.IsPagesOpened) return;
            //
            this.m_RibbonPageContainerPopup.Close();
        }
        #endregion

        public override void Refresh()
        {
            int iValue = this.RibbonPageSelectedIndex;
            this.RibbonPageSelectedIndex = iValue < 0 ? 0 : iValue;
            base.Refresh();
        }

        [Browsable(false)]
        public RibbonPage SelectedRibbonPage
        {
            get
            {
                WFNew.ITabButtonItem pTabButtonItem = this.m_RibbonPageTabButtonContainerItem.SelectTabButtonItem;
                if (pTabButtonItem == null) return null;
                return pTabButtonItem.pTabPageItem as RibbonPage;
            }
        }

        public bool SetSelectRibbonPage(RibbonPage ribbonPage)
        {
            if (ribbonPage == null) return false;
            int index = this.RibbonPages.IndexOf(ribbonPage);
            if (index < 0 || index >= this.RibbonPages.Count) return false;
            this.RibbonPageSelectedIndex = index;
            return true;
        }

        public override DockStyle Dock
        {
            get
            {
                switch (base.Dock)
                {
                    case DockStyle.None:
                        return DockStyle.None;
                    default:
                        return DockStyle.Top;
                }
            }
            set
            {
                switch (value)
                {
                    case DockStyle.None:
                        base.Dock = DockStyle.None;
                        break;
                    default:
                        base.Dock = DockStyle.Top;
                        break;
                }
            }
        }

        public override string Text
        {
            get
            {
                if (this.ParentForm != null) return this.ParentForm.Text;
                //
                return base.Text;
            }
            set
            {
                if (this.ParentForm != null) { this.ParentForm.Text = value; return; }
                //
                base.Text = value;
            }
        }

        public override bool LockHeight
        {
            get { return true; }
        }

        public override bool LockWith
        {
            get { return false; }
        }

        public override object Clone()
        {
            return null;
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case (int)GISShare.Win32.Msgs.WM_LBUTTONDOWN:
                    if (this.ParentForm != null)
                    {
                        Point point = this.PointToClient(MousePosition);
                        if (this.CaptionTextRectangle.Contains(point))
                        {
                            GISShare.Win32.API.SendMessage(this.ParentForm.Handle,
                                (int)GISShare.Win32.Msgs.WM_NCLBUTTONDOWN,
                                (uint)GISShare.Win32.HitTests.HTCAPTION,
                                (uint)GISShare.Win32.NativeMethods.MousePositionToLParam(point));
                            return;
                        }
                    }
                    break;
            }
            base.WndProc(ref m);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            this.Invalidate(this.CaptionTextRectangle); 
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            if (this.ParentForm != null)
            {
                if (this.CaptionTextRectangle.Contains(e.Location))
                {
                    if (this.ParentForm.WindowState == FormWindowState.Maximized) this.ParentForm.WindowState = FormWindowState.Normal;
                    else if (this.ParentForm.WindowState == FormWindowState.Normal) this.ParentForm.WindowState = FormWindowState.Maximized;
                }
            }
            //
            base.OnMouseDoubleClick(e);
        }

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
            if (this.HideRibbonPage)
            {
                this.Height = this.IsTopToolbar ? CRT_HIDERIBBONPAGEHEIGHT_TOPTOOLBAR : CRT_HIDERIBBONPAGEHEIGHT_BOTTOMTOOLBAR;
            }
            else
            {
                this.Height = this.IsTopToolbar ? CRT_NORMALHEIGHT_TOPTOOLBAR : CRT_NORMALHEIGHT_BOTTOMTOOLBAR;
                //
                this.m_RibbonPageContainerControl.SetEnvelope(this.PageDisplayRectangle);
            }
            //
            base.OnPaint(e);

            //using (Pen p = new Pen(System.Drawing.Color.Red))
            //{
            //    e.Graphics.DrawRectangle(p, this.CaptionTextRectangle);
            //}
        }

        protected override void OnDraw(PaintEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonControl(
                new GISShare.Controls.WinForm.ObjectRenderEventArgs(e.Graphics, this, this.UsingRectangle));
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderFormNCCaptionText(
                new GISShare.Controls.WinForm.TextRenderEventArgs(e.Graphics, this, this.Enabled && this.IsActive, false, true, this.Text, this.ForeColor, this.Font, this.CaptionTextRectangle, new StringFormat()));
            //
            if (this.RibbonStartButtonItem2007Visible) return;
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderFormNCIcon(
                new GISShare.Controls.WinForm.IconRenderEventArgs(e.Graphics, this, this.Enabled && this.IsActive, this.Icon, this.IconRectangle));
        }

        #region SaveLayoutInfo ���沼���ļ�
        public void SaveLayoutFile(string strFileName)//���浱ǰ����״̬
        {
            XmlDocument doc = new XmlDocument();
            //
            //
            //
            XmlDeclaration declare = doc.CreateXmlDeclaration("1.0", "UTF-8", "yes");//����һ������
            doc.InsertBefore(declare, doc.DocumentElement);//��������ӵ��ĵ�Ԫ�صĶ���
            //
            //
            //
            XmlElement root = doc.CreateElement("RibbonControlEx");//��Ӹ��ڵ�
            root.SetAttribute("IsTopToolbar", this.IsTopToolbar.ToString());
            root.SetAttribute("HideRibbonPage", this.HideRibbonPage.ToString());
            root.SetAttribute("RibbonPageSelectedIndex", this.RibbonPageSelectedIndex.ToString());
            doc.AppendChild(root);
            //
            //
            //
            XmlElement elementParentForm = root.OwnerDocument.CreateElement("ParentForm");
            switch (this.ParentForm.WindowState)
            {
                case FormWindowState.Maximized:
                    elementParentForm.SetAttribute("Location", "300,80");
                    elementParentForm.SetAttribute("Size", "800,600");
                    elementParentForm.SetAttribute("WindowState", "Maximized");
                    break;
                case FormWindowState.Minimized:
                    elementParentForm.SetAttribute("Location", "300,80");
                    elementParentForm.SetAttribute("Size", "800,600");
                    elementParentForm.SetAttribute("WindowState", "Normal");
                    break;
                case FormWindowState.Normal:
                    elementParentForm.SetAttribute("Location", this.ParentForm.Location.X.ToString() + "," + this.ParentForm.Location.Y.ToString());
                    elementParentForm.SetAttribute("Size", this.ParentForm.Size.Width.ToString() + "," + this.ParentForm.Size.Height.ToString());
                    elementParentForm.SetAttribute("WindowState", "Normal");
                    break;
            }
            root.AppendChild(elementParentForm);
            //
            //
            //
            XmlElement elementBasePanels = root.OwnerDocument.CreateElement("ToolbarItems");
            elementBasePanels.SetAttribute("Count", this.ToolbarItems.Count.ToString());
            root.AppendChild(elementBasePanels);
            this.ToolbarItems.SetRecordID();
            foreach (BaseItem one in this.ToolbarItems)
            {
                XmlElement element = elementBasePanels.OwnerDocument.CreateElement("BaseItem");
                element.SetAttribute("RecordID", one.RecordID.ToString());
                element.SetAttribute("Name", one.Name);
                element.SetAttribute("Visible", one.Visible.ToString());
                elementBasePanels.AppendChild(element);
            }
            //
            //
            //
            doc.Save(strFileName);
        }
        #endregion

        #region LoadLayoutFile ���ز����ļ�
        public void LoadLayoutFile(string strFileName, bool loadParentFormLayout)//���ز����ļ��������ݲ����ļ����в���
        {
            #region ��ȡ�����ļ� д�����������Ϣ�����в���
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(strFileName);
            //
            XmlNode xmlNode = xmlDoc.SelectSingleNode("RibbonControlEx");
            if (xmlNode == null) return;
            //
            this.SetRibbonControl((XmlElement)xmlNode);
            //
            XmlNodeList xmlNodeList = xmlNode.ChildNodes;
            if (xmlNodeList == null) return;
            foreach (XmlNode one in xmlNodeList)
            {
                XmlElement xe = (XmlElement)one;
                switch (xe.Name)
                {
                    case "ParentForm":
                        if (loadParentFormLayout) { this.SetParentForm(xe); }
                        break;
                    case "ToolbarItems":
                        if (xe.GetAttribute("Count") != "0") { this.SetToolbarItems(xe); }
                        break;
                    default:
                        break;
                }
            }
            #endregion
        }

        private void SetRibbonControl(XmlElement xmlElement)
        {
            this.IsTopToolbar = bool.Parse(xmlElement.GetAttribute("IsTopToolbar"));
            this.HideRibbonPage = bool.Parse(xmlElement.GetAttribute("HideRibbonPage"));
            this.RibbonPageSelectedIndex = int.Parse(xmlElement.GetAttribute("RibbonPageSelectedIndex"));
        }

        //

        private void SetParentForm(XmlElement xmlElement)//���ø�����Ĳ�����Ϣ
        {
            this.ParentForm.Location = this.ToPoint(xmlElement.GetAttribute("Location"));
            this.ParentForm.Size = this.ToSize(xmlElement.GetAttribute("Size"));
            this.ParentForm.WindowState = this.ToFormWindowState(xmlElement.GetAttribute("WindowState"));
        }

        //

        private void SetToolbarItems(XmlElement xmlElement)
        {
            XmlNodeList xmlNodeList = xmlElement.ChildNodes;
            if (xmlNodeList == null) return;
            foreach (XmlNode one in xmlNodeList)//����
            {
                XmlElement xe = (XmlElement)one;//���ӽڵ�����ת��ΪXmlElement����
                int id = Int32.Parse(xe.GetAttribute("RecordID"));
                string name = xe.GetAttribute("Name");
                bool visible = bool.Parse(xe.GetAttribute("Visible"));
                //
                BaseItem temp = this.ToolbarItems[name];
                if (temp == null) continue;
                WFNew.ISetRecordItemHelper pSetRecordItemHelper = temp as WFNew.ISetRecordItemHelper;
                if (pSetRecordItemHelper != null) pSetRecordItemHelper.SetRecordID(id);
                temp.Visible = visible;
            }
        }

        //

        private FormWindowState ToFormWindowState(string str)
        {
            if (str == FormWindowState.Maximized.ToString()) return FormWindowState.Maximized;
            else if (str == FormWindowState.Minimized.ToString()) return FormWindowState.Minimized;
            else return FormWindowState.Normal;
        }

        private Point ToPoint(string str)
        {
            try
            {
                string[] strList = str.Split(',');
                return new Point(Int32.Parse(strList[0]), Int32.Parse(strList[1]));
            }
            catch { GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show("�����ļ��𻵣�"); return new Point(60, 60); }
        }

        private Size ToSize(string str)
        {
            try
            {
                string[] strList = str.Split(',');
                return new Size(Int32.Parse(strList[0]), Int32.Parse(strList[1]));
            }
            catch { GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show("�����ļ��𻵣�"); return new Size(260, 260); }
        }
        #endregion

        //
        protected virtual void OnRibbonPageSelectedIndexChanged(IntValueChangedEventArgs e)
        {
            if (this.RibbonPageSelectedIndexChanged != null) { this.RibbonPageSelectedIndexChanged(this, e); }
        }

        //
        //
        //   

        #region ˽�������
        class RibbonStartButtonItem2007Ex : RibbonStartButtonItem2007
        {
            private RibbonControlEx m_Owner;

            public RibbonStartButtonItem2007Ex(IApplicationPopup pApplicationPopup, RibbonControlEx ribbonControl)
                : base(pApplicationPopup)
            {
                base.Name = "RibbonStartButtonItem2007Ex";
                base.Text = "RibbonStartButtonItem2007Ex";
                //
                this.m_Owner = ribbonControl;
                //((ISetOwnerHelper)this).SetOwner(this.m_Owner);
            }

            public override bool CancelItemsEvent
            {
                get
                {
                    return !this.m_Owner.IsActive;
                }
            }

            public override Point PopupLoction
            {
                get
                {

                    if (this.m_Owner == null) return base.PopupLoction;
                    ISimplyPopup pSimplyPopup = ((IPopupOwnerHelper)this).GetBasePopup() as ISimplyPopup;
                    if (pSimplyPopup != null) pSimplyPopup.GetPopupPanel().TrySetPopupPanelSize(pSimplyPopup.GetIdealSize());
                    return this.m_Owner.PointToScreen(this.m_Owner.AnchorPoint);
                }
            }

            public override Image Image
            {
                get
                {
                    if (base.Image == null &&
                        this.m_Owner != null &&
                        this.m_Owner.ParentForm != null &&
                        this.m_Owner.ParentForm.ShowIcon &&
                        this.m_Owner.ParentForm.Icon != null) base.Image = this.m_Owner.ParentForm.Icon.ToBitmap();
                    //
                    return base.Image;
                }
                set
                {
                    base.Image = value;
                }
            }

            public override Rectangle DisplayRectangle
            {
                get
                {
                    if (this.m_Owner == null) return base.DisplayRectangle;
                    Rectangle rectangle = this.m_Owner.CaptionRectangle;
                    return new Rectangle(rectangle.Left + 3, rectangle.Top + 3, 42, 42);
                }
            }

            public override bool Visible
            {
                get
                {
                    if (this.m_Owner.eRibbonStyle == RibbonStyle.eOffice2007) return base.Visible;
                    else return false;
                }
                set
                {
                    base.Visible = value;
                }
            }
        }

        class RibbonStartButtonItem2010Ex : RibbonStartButtonItem2010
        {
            private RibbonControlEx m_Owner;

            public RibbonStartButtonItem2010Ex(IApplicationPopup pApplicationPopup, RibbonControlEx ribbonControl)
                : base(pApplicationPopup)
            {
                base.Name = "RibbonStartButtonItem2010Ex";
                base.Text = "RibbonStartButtonItem2010Ex";
                //
                this.m_Owner = ribbonControl;
                //((ISetOwnerHelper)this).SetOwner(this.m_Owner);
            }

            public override bool CancelItemsEvent
            {
                get
                {
                    return !this.m_Owner.IsActive;
                }
            }

            public override Point PopupLoction
            {
                get
                {
                    if (this.m_Owner == null) return base.PopupLoction;
                    ISimplyPopup pSimplyPopup = ((IPopupOwnerHelper)this).GetBasePopup() as ISimplyPopup;
                    if (pSimplyPopup != null) pSimplyPopup.GetPopupPanel().TrySetPopupPanelSize(pSimplyPopup.GetIdealSize());
                    return this.m_Owner.PointToScreen(this.m_Owner.AnchorPoint);
                }
            }

            public override int LeftTopRadius
            {
                get
                {
                    return 0;
                }
                set
                {
                    base.LeftTopRadius = value;
                }
            }

            public override int LeftBottomRadius
            {
                get
                {
                    return 0;
                }
                set
                {
                    base.LeftBottomRadius = value;
                }
            }

            public override int RightTopRadius
            {
                get
                {
                    return 6;
                }
                set
                {
                    base.RightTopRadius = value;
                }
            }

            public override int RightBottomRadius
            {
                get
                {
                    return 0;
                }
                set
                {
                    base.RightBottomRadius = value;
                }
            }

            public override Rectangle DisplayRectangle
            {
                get
                {
                    if (this.m_Owner == null) return base.DisplayRectangle;
                    Rectangle rectangle = this.m_Owner.CaptionRectangle;
                    return new Rectangle(rectangle.Left + 1, rectangle.Bottom + 1, 47, 22);
                }
            }

            public override bool Visible
            {
                get
                {
                    if (this.m_Owner.eRibbonStyle == RibbonStyle.eOffice2010) return base.Visible;
                    else return false;
                }
                set
                {
                    base.Visible = value;
                }
            }
        }

        class RibbonFormButtonStackItem : FormButtonStackItem
        {
            private RibbonControlEx m_Owner;

            public RibbonFormButtonStackItem(RibbonControlEx ribbonControl)
                : base()
            {
                base.Name = "RibbonFormButtonStackItem";
                base.Text = "RibbonFormButtonStackItem";
                //
                this.m_Owner = ribbonControl;
                //((ISetOwnerHelper)this).SetOwner(this.m_Owner);
            }

            public override bool CancelItemsEvent
            {
                get
                {
                    return !this.m_Owner.IsActive;
                }
            }

            public override Rectangle DisplayRectangle
            {
                get
                {
                    if (this.m_Owner == null) return base.DisplayRectangle;
                    Rectangle rectangle = this.m_Owner.CaptionRectangle;
                    return new Rectangle(rectangle.Right - this.Width - 1, rectangle.Top, base.Width, rectangle.Height);
                }
            }

            public override Padding Padding
            {
                get
                {
                    return new Padding(0, 1, 0, 1);
                }
                set
                {
                    base.Padding = value;
                }
            }
        }

        class RibbonQuickAccessToolbarItemEx : RibbonQuickAccessToolbarItem
        {
            private RibbonControlEx m_Owner;

            public RibbonQuickAccessToolbarItemEx(RibbonControlEx ribbonControl)
            {
                base.Name = "RibbonQuickAccessToolbarItemEx";
                base.Text = "RibbonQuickAccessToolbarItemEx";
                //
                this.m_Owner = ribbonControl;
                //((ISetOwnerHelper)this).SetOwner(this.m_Owner);
                this.eQuickAccessToolbarStyle = QuickAccessToolbarStyle.eHalfRound;
                //this.BaseItems.BaseItemAdded += new BaseItemEventHandler(BaseItems_BaseItemAdded);
            }

            public override bool CancelItemsEvent
            {
                get
                {
                    return !this.m_Owner.IsActive;
                }
            }

            public override QuickAccessToolbarStyle eQuickAccessToolbarStyle
            {
                get
                {
                    if (this.m_Owner == null) return base.eQuickAccessToolbarStyle;
                    if (!this.m_Owner.IsTopToolbar &&
                        base.eQuickAccessToolbarStyle == QuickAccessToolbarStyle.eHalfRound) return QuickAccessToolbarStyle.eNone;
                    if (this.m_Owner.eRibbonStyle == RibbonStyle.eOffice2007 &&
                        !this.m_Owner.RibbonStartButtonItem2007Visible &&
                        base.eQuickAccessToolbarStyle == QuickAccessToolbarStyle.eHalfRound) return QuickAccessToolbarStyle.eAllRound;
                    if (this.m_Owner.eRibbonStyle == RibbonStyle.eOffice2010 &&
                        base.eQuickAccessToolbarStyle == QuickAccessToolbarStyle.eHalfRound) return QuickAccessToolbarStyle.eLineSeparator;
                    return base.eQuickAccessToolbarStyle;
                }
                set
                {
                    base.eQuickAccessToolbarStyle = value;
                }
            }

            public override Padding Padding
            {
                get
                {
                    if (this.m_Owner == null) return base.Padding;
                    //
                    int iLeft = 4;
                    //
                    if (this.m_Owner.IsTopToolbar)
                    {
                        if (this.m_Owner.RibbonStartButtonItem2007Visible)
                        {
                            iLeft += 40;
                        }
                        else
                        {
                            if (this.m_Owner != null &&
                                this.m_Owner.ParentForm != null &&
                                this.m_Owner.ParentForm.ShowIcon &&
                                this.m_Owner.ParentForm.Icon != null)
                            {
                                iLeft += 20;
                            }
                        }
                        //
                        switch (this.eQuickAccessToolbarStyle)
                        {
                            case QuickAccessToolbarStyle.eHalfRound:
                                return new Padding(iLeft - 12, 1, 0, 1);
                            case QuickAccessToolbarStyle.eAllRound:
                            case QuickAccessToolbarStyle.eNormal:
                            case QuickAccessToolbarStyle.eLineSeparator:
                                return new Padding(iLeft + 2, 1, 0, 1);
                            case QuickAccessToolbarStyle.eNone:
                            default:
                                return new Padding(iLeft, 1, 0, 1);
                        }
                    }
                    //
                    return new Padding(1);
                }
                set
                {
                    base.Padding = value;
                }
            }

            public override Rectangle DisplayRectangle
            {
                get
                {
                    if (this.m_Owner == null) return base.DisplayRectangle;
                    int iW = this.Size.Width;
                    if (!this.HaveVisibleBaseItem)
                    {
                        if (this.m_Owner != null)
                        {
                            iW = this.Padding.Left + (this.eQuickAccessToolbarStyle == QuickAccessToolbarStyle.eHalfRound ? 23 : 10) + this.Padding.Right;
                        }
                        else
                        {
                            iW = this.Size.Width;
                        }
                    }
                    if (iW < 1) iW = 1;
                    Rectangle rectangle = this.m_Owner.IsTopToolbar ? this.m_Owner.CaptionRectangle : this.m_Owner.BottomCompnonentRectangle;
                    return new Rectangle(rectangle.Left + 1, rectangle.Top + 1, iW, rectangle.Height - 1);
                }
            }

            public override int ColumnDistance
            {
                get
                {
                    return 0;
                }
                set
                {
                    base.ColumnDistance = 0;
                }
            }

            public override bool ShowNomalState
            {
                get
                {
                    return false;
                }
                set
                {
                    base.ShowNomalState = false;
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

            public override bool IsRestrictItems
            {
                get
                {
                    return true;
                }
                set
                {
                    base.IsRestrictItems = value;
                }
            }

            public override bool Overflow
            {
                get
                {
                    return false;
                }
            }

            public override int MinSize
            {
                get
                {
                    Padding p = this.Padding;
                    return p.Left + p.Right;
                }
                set
                {
                    base.MinSize = 10;
                }
            }

            public override int MaxSize
            {
                get
                {
                    if (this.m_Owner == null) return base.MaxSize;
                    int iW = base.MaxSize;
                    if (this.m_Owner.IsTopToolbar)
                    {
                        iW = this.m_Owner.CaptionRectangle.Width - this.m_Owner.RibbonFormButtonStackItemWidth - this.m_Owner.MinCaptionTextWidth - 2;
                        switch (this.eQuickAccessToolbarStyle)
                        {
                            case QuickAccessToolbarStyle.eHalfRound:
                                return this.MinSize < iW ? iW : this.MinSize + 45;
                            case QuickAccessToolbarStyle.eAllRound:
                            case QuickAccessToolbarStyle.eNormal:
                                return this.MinSize < iW ? iW : this.MinSize + 30;
                            case QuickAccessToolbarStyle.eNone:
                            default:
                                return this.MinSize < iW ? iW : this.MinSize + 20;
                        }
                    }
                    else
                    {
                        iW = this.m_Owner.BottomCompnonentRectangle.Width - 10;
                        return this.MinSize < iW ? iW : this.MinSize + 20;
                    }
                }
                set
                {
                    base.MaxSize = 600;
                }
            }

            protected override void OnDraw(PaintEventArgs e)
            {
                if (this.m_Owner.IsTopToolbar)
                {
                    int iDrawItemsCount = this.DrawItemsCount;
                    base.OnDraw(e);
                    if (this.DrawItemsCount <= 0 && iDrawItemsCount > 0) this.UIUpdate();
                }
                else
                {
                    base.OnDraw(e);
                }
            }
        }

        class RibbonPageContentContainerItem : BaseItemStackExItem
        {
            RibbonControlEx m_Owner = null;

            public RibbonPageContentContainerItem(RibbonControlEx ribbonControl)
            {
                base.Name = "RibbonPageContentContainerItem";
                base.Text = "RibbonPageContentContainerItem";
                //
                this.m_Owner = ribbonControl;
                //((ISetOwnerHelper)this).SetOwner(this.m_Owner);
                //this.BaseItems.BaseItemAdded += new BaseItemEventHandler(BaseItems_BaseItemAdded);
            }
            //void BaseItems_BaseItemAdded(object sender, BaseItemEventArgs e)
            //{
            //    //throw new Exception("The method or operation is not implemented.");
            //    this.Refresh();
            //}

            public override bool CancelItemsEvent
            {
                get
                {
                    return !this.m_Owner.IsActive;
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

            public override PNLayoutStyle ePNLayoutStyle
            {
                get
                {
                    return PNLayoutStyle.eTail;
                }
                set
                {
                    base.ePNLayoutStyle = PNLayoutStyle.eTail;
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

            public override Rectangle DisplayRectangle
            {
                get
                {
                    if (this.m_Owner != null)
                    {
                        Rectangle rectangle = this.m_Owner.CaptionRectangle;
                        return new Rectangle
                            (
                            rectangle.Right - (this.m_Owner.RibbonMdiFormButtonStackItemVisible ? this.Width + this.m_Owner.RibbonMdiFormButtonStackItemWidth : this.Width), 
                            rectangle.Bottom + 1,
                            this.HaveVisibleBaseItem ? this.Size.Width : 0, 
                            21
                            );
                    }
                    return base.DisplayRectangle;
                }
            }

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

            public override bool IsRestrictItems
            {
                get
                {
                    return true;
                }
                set
                {
                    base.IsRestrictItems = value;
                }
            }

            //public override Size Size
            //{
            //    get
            //    {
            //        return this.HaveVisibleBaseItem ? base.Size : new Size(0, 21);
            //        //return this.BaseItems.Count > 0 ? base.Size : new Size(0, 21);
            //        //return base.Size;
            //    }
            //    set
            //    {
            //        base.Size = value;
            //    }
            //}

            public override bool Visible
            {
                get
                {
                    return this.HaveVisibleBaseItem;
                    //return this.BaseItems.Count > 0 ? true : false;
                    //return base.Visible;
                }
                set
                {
                    base.Visible = value;
                }
            }

            public override int ColumnDistance
            {
                get
                {
                    return 0;
                }
                set
                {
                    base.ColumnDistance = 0;
                }
            }
        }

        class RibbonMdiFormButtonStackItem : MdiFormButtonStackItem
        {
            MenuStrip m_MenuStrip = null;

            private RibbonControlEx m_Owner;

            public RibbonMdiFormButtonStackItem(RibbonControlEx ribbonControl)
                : base()
            {
                base.Name = "RibbonMdiFormButtonStackItem";
                base.Text = "RibbonMdiFormButtonStackItem";
                //
                this.m_Owner = ribbonControl;
                //((ISetOwnerHelper)this).SetOwner(this.m_Owner);
                //
                this.m_MenuStrip = new MenuStrip();
                this.m_MenuStrip.ItemAdded += new ToolStripItemEventHandler(MenuStrip_ItemAdded);
                this.m_MenuStrip.ItemRemoved += new ToolStripItemEventHandler(MenuStrip_ItemRemoved);
            }
            void MenuStrip_ItemAdded(object sender, ToolStripItemEventArgs e)
            {
                //throw new Exception("The method or operation is not implemented.");
                Rectangle rectangle = this.m_Owner.TopCompnonentRectangle;
                base.Invalidate(Rectangle.FromLTRB(rectangle.Left, rectangle.Top - 24, rectangle.Right, rectangle.Bottom));
            }
            void MenuStrip_ItemRemoved(object sender, ToolStripItemEventArgs e)
            {
                //throw new Exception("The method or operation is not implemented.");
                Rectangle rectangle = this.m_Owner.TopCompnonentRectangle;
                base.Invalidate(Rectangle.FromLTRB(rectangle.Left, rectangle.Top - 24, rectangle.Right, rectangle.Bottom));
            }

            public override bool CancelItemsEvent
            {
                get
                {
                    return !this.m_Owner.IsActive;
                }
            }

            public override Rectangle DisplayRectangle
            {
                get
                {
                    if (this.m_Owner != null)
                    {
                        Rectangle rectangle = this.m_Owner.CaptionRectangle;
                        return new Rectangle
                            (
                            rectangle.Right - this.Width - 1,
                            rectangle.Bottom + 1,
                            this.Width,
                            21
                            );
                    }
                    return base.DisplayRectangle;
                }
            }

            //public override Point Location
            //{
            //    get
            //    {
            //        if (this.m_Owner == null) return base.Location;
            //        Rectangle rectangle = this.m_Owner.CaptionRectangle;
            //        return new Point(rectangle.Right - this.Width - 1, rectangle.Bottom + 1);
            //    }
            //}

            public override Padding Padding
            {
                get
                {
                    return new Padding(2, 1, 0, 0);
                }
                set
                {
                    base.Padding = value;
                }
            }

            public MenuStrip MenuStrip
            {
                get { return m_MenuStrip; }
            }
        }

        class RibbonPageTabButtonContainerItem : TabButtonContainerItem
        {
            RibbonControlEx m_Owner = null;

            public RibbonPageTabButtonContainerItem(RibbonControlEx ribbonControl)
            {
                base.Name = "RibbonPageTabButtonContainerItem";
                base.Text = "RibbonPageTabButtonContainerItem";
                //
                this.m_Owner = ribbonControl;
            }

            public override bool CancelItemsEvent
            {
                get
                {
                    return !this.m_Owner.IsActive;
                }
            }

            public override bool AutoVisible
            {
                get
                {
                    return false;
                }
                set
                {
                    base.AutoVisible = value;
                }
            }

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

            public override PNLayoutStyle ePNLayoutStyle
            {
                get
                {
                    return  PNLayoutStyle.eTail;
                }
                set
                {
                    base.ePNLayoutStyle =  PNLayoutStyle.eTail;
                }
            }

            public override Padding Padding
            {
                get
                {
                    return new Padding(49, 0, 0, 0);
                }
                set
                {
                    base.Padding = new Padding(49, 0, 0, 0);
                }
            }

            public override Orientation eOrientation
            {
                get
                {
                    return  Orientation.Horizontal;
                }
                set
                {
                    base.eOrientation = Orientation.Horizontal;
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

            public override int ColumnDistance
            {
                get
                {
                    return 8;
                }
                set
                {
                    base.ColumnDistance = 8;
                }
            }
        }

        [ToolboxItem(false)]
        class RibbonPageContainerControl : BaseItemControl, IPopupPanel, ITabControlHelper
        {
            private RibbonPageTabButtonContainerItem m_Owner;

            private RibbonPageCollection m_RibbonPageCollection = null;
            public RibbonPageCollection RibbonPageCollection
            {
                get { return m_RibbonPageCollection; }
            }

            public RibbonPageContainerControl(RibbonPageTabButtonContainerItem ribbonPageTabButtonContainerItem)
            {
                this.SetStyle(ControlStyles.Opaque, false);
                this.SetStyle(ControlStyles.UserPaint, true);
                this.SetStyle(ControlStyles.Selectable, false);
                this.SetStyle(ControlStyles.DoubleBuffer, true);
                this.SetStyle(ControlStyles.ResizeRedraw, true);
                this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
                //
                base.Name = "RibbonPageContainerControl";
                base.Text = "RibbonPageContainerControl";
                base.Visible = true;
                //base.BackColor = System.Drawing.Color.Transparent;
                //
                this.m_RibbonPageCollection = new RibbonPageCollection(this);
                //
                this.m_Owner = ribbonPageTabButtonContainerItem;
            }

            #region IPopupPanel
            public void TrySetPopupPanelSize(Size size)
            {
                this.Size = size;
            }
            #endregion

            #region WFNew.ITabControlHelper
            IList WFNew.ITabControlHelper.TabPageList
            {
                get { return this.Controls; }
            }

            WFNew.BaseItemCollection WFNew.ITabControlHelper.TabButtonItemCollection
            {
                get { return this.m_Owner.BaseItems; }
            }
            #endregion

            public override DockStyle Dock
            {
                get
                {
                    return  DockStyle.None;
                }
                set
                {
                    base.Dock = DockStyle.None;
                }
            }

            protected override void OnControlAdded(ControlEventArgs e)
            {
                base.OnControlAdded(e);
                //
                RibbonPage ribbonPage = e.Control as RibbonPage;
                if (ribbonPage == null)
                {
                    this.Controls.Remove(ribbonPage);
                }
                else
                {
                    //ribbonPage.LockDock();
                    //this.m_Owner.BaseItems.Add(ribbonPage.pTabButtonItem as BaseItem);
                    ribbonPage.Dock = DockStyle.Fill;
                }
            }

            protected override void OnControlRemoved(ControlEventArgs e)
            {
                base.OnControlRemoved(e);
                //
                RibbonPage ribbonPage = e.Control as RibbonPage;
                if (ribbonPage != null)
                {
                    //ribbonPage.UnLockDock();
                    //this.m_Owner.BaseItems.Remove(ribbonPage.pTabButtonItem as BaseItem);
                    ribbonPage.Dock = DockStyle.Fill;
                }
            }

            public void SetEnvelope(Rectangle rectangle)
            {
                this.Location = rectangle.Location;
                this.Size = rectangle.Size;
            }

            public override bool LockHeight
            {
                get { return true; }
            }

            public override bool LockWith
            {
                get { return true; }
            }

            public override object Clone()
            {
                return null;
            }
        }

        [ToolboxItem(false)]
        class RibbonPageContainerPopupPanel : BaseItemControl
        {
            public RibbonPageContainerPopupPanel()
            {
                base.Name = "RibbonPageContainerPopupPanel";
                base.Text = "RibbonPageContainerPopupPanel";
                base.Visible = true;
                base.BackColor = System.Drawing.Color.Transparent;
            }

            public override bool LockHeight
            {
                get { return true; }
            }

            public override bool LockWith
            {
                get { return true; }
            }

            public override object Clone()
            {
                return null;
            }
        }

        [ToolboxItem(false)]
        class RibbonPageContainerPopup : BasePopup, IPanelPopup
        {
            private RibbonPageContainerControl m_RibbonPageContainerControl;

            private RibbonPageContainerPopupPanel m_RibbonPageContainerPopupPanel = null;
            private ToolStripControlHost m_ToolStripControlHost = null;

            public RibbonPageContainerPopup(RibbonPageContainerControl ribbonPageContainerControl)
                : base()
            {
                base.Name = "RibbonPageContainerPopup";
                base.Text = "RibbonPageContainerPopup";
                this.m_RibbonPageContainerControl = ribbonPageContainerControl;
                //
                this.m_RibbonPageContainerPopupPanel = new RibbonPageContainerPopupPanel();
                ((ISetOwnerHelper)this.m_RibbonPageContainerPopupPanel).SetOwner(this);
                //
                this.m_ToolStripControlHost = new ToolStripControlHost(this.m_RibbonPageContainerPopupPanel);
                this.m_ToolStripControlHost.Dock = DockStyle.Fill;
                //this.m_ToolStripControlHost.BackColor =
                //    GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.WFNewColorTableEx.RibbonControlBackground;
                this.m_ToolStripControlHost.Margin = new Padding(0);
                this.m_ToolStripControlHost.Padding = new Padding(0);
                base.Items.Add(this.m_ToolStripControlHost);
                //
                this.MaximumSize = SystemInformation.WorkingArea.Size;
                this.Margin = new Padding(0);
                this.Padding = new Padding(2);
                this.DropShadowEnabled = false;
                this.ShowItemToolTips = false;
            }

            #region IPanelPopup
            public Size GetIdealSize() { return this.Size; }

            public IPopupPanel GetPopupPanel() { return this.m_RibbonPageContainerControl; }
            #endregion

            public override bool CustomFiltration
            {
                get
                {
                    return true;
                }
            }

            public override bool Filtration(MouseEventArgs e)
            {
                return this.TabButtonContainMousePoint(e.Location) || base.Filtration(e);
            }
            private bool TabButtonContainMousePoint(Point point)
            {
                if (this.pOwner == null) return false;
                point = this.pOwner.PointToClient(point);
                //
                foreach(ITabPageItem one in this.m_RibbonPageContainerControl.RibbonPageCollection)
                {
                    if (one != null &&
                        one.pTabButtonItem.Enabled && 
                        one.pTabButtonItem.Visible && 
                        !one.pTabButtonItem.Overflow && 
                        one.pTabButtonItem.DisplayRectangle.Contains(point)) return true;
                }
                //
                return false;
            }

            public void SetEnvelope(Rectangle rectangle)
            {
                this.m_RibbonPageContainerPopupPanel.Size = 
                    new Size(rectangle.Width - this.Padding.Left - this.Padding.Right, 
                    rectangle.Height - this.Padding.Top - this.Padding.Bottom);
                this.m_RibbonPageContainerControl.SetEnvelope(this.m_RibbonPageContainerPopupPanel.DisplayRectangle);
            }

            public void HideRibbonPageContainer()
            {
                if (!(this.m_RibbonPageContainerControl.Parent is RibbonControlEx)) return;
                this.m_RibbonPageContainerControl.Parent.Controls.Remove(this.m_RibbonPageContainerControl);
                //
                this.m_RibbonPageContainerPopupPanel.Controls.Add(this.m_RibbonPageContainerControl);
            }

            public void DisplayRibbonPageContainer()
            {
                if (this.m_RibbonPageContainerControl.Parent is RibbonControlEx) return;
                this.m_RibbonPageContainerControl.Parent.Controls.Remove(this.m_RibbonPageContainerControl);
                //
                RibbonControlEx ribbonControl = this.pOwner as RibbonControlEx;
                if (ribbonControl == null) return;
                this.m_RibbonPageContainerControl.SetEnvelope(ribbonControl.PageDisplayRectangle);
                ribbonControl.Controls.Add(this.m_RibbonPageContainerControl);
            }

            #region Clone
            public override object Clone()
            {
                return null;
            }
            #endregion

            #region Radius
            public override bool UseRadius
            {
                get
                {
                    return false;
                }
            }

            public override int LeftTopRadius { get { return 0; } }

            public override int RightTopRadius { get { return 0; } }

            public override int LeftBottomRadius { get { return 0; } }

            public override int RightBottomRadius { get { return 0; } }
            #endregion

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);
                //
                GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonPageContainerPopup
                    (
                    new GISShare.Controls.WinForm.ObjectRenderEventArgs(e.Graphics, this, new Rectangle(0, 0, this.Width, this.Height))
                    );
            }
        }
        #endregion

        public class RibbonPageCollection : TabPageCollection
        {
            internal RibbonPageCollection(ITabControlHelper pTabControlHelper)
                : base(pTabControlHelper) { }

            protected override bool Filtration(ITabPageItem value)
            {
                return value is RibbonPage && base.Filtration(value);
            }
        }
    }
}
