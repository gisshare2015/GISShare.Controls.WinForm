using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew.View
{
    public class NodeViewItemTreeItem : BaseItem, 
        ICollectionItem, ICollectionItem2, ICollectionItem3, IUICollectionItem, 
        IScrollableObjectHelper,
        IInputObject, IInputObjectHelper, 
        IViewList,
        IViewListEnumerator,
        IViewLayoutList,
        IViewItemOwner, IViewItemOwner2,
        IViewItemList, IViewItemListEvent, IViewItemList2,
        INodeViewItemTree, INodeViewItemTreeEvent
    {
        private const int CONST_SCROLBARBARSIZE = 13;
        private const int CONST_VIEWRECTANGLESPACE = 1;
        //
        private const int CTR_INPUTREGIONOFFSETY = 1;
        //
        private NodeViewItemCollection m_NodeViewItemCollection;
        //
        private VScrollBarItem m_VScrollBarItem;
        private HScrollBarItem m_HScrollBarItem;
        private BaseItemCollection m_BaseItemCollection;
        //
        private IInputRegion m_InputRegion = null;

        public NodeViewItemTreeItem()
        {
            base.AutoGetFocus = true;
            //base.BackColor = System.Drawing.SystemColors.Window;
            //
            this.m_BaseItemCollection = new BaseItemCollection(this);
            this.m_VScrollBarItem = new VScrollBarItem();
            this.m_BaseItemCollection.Add(this.m_VScrollBarItem);
            this.m_HScrollBarItem = new HScrollBarItem();
            this.m_BaseItemCollection.Add(this.m_HScrollBarItem);
            ((ILockCollectionHelper)this.m_BaseItemCollection).SetLocked(false);
            //
            this.m_NodeViewItemCollection = new NodeViewItemCollection(this);
            //
            this.m_InputRegion = new InputRegion(this);
            this.m_InputRegion.InputEnd += new CancelEventHandler(InputRegion_InputEnd);
            this.m_InputRegion.PopupClosed += new EventHandler(InputRegion_PopupClosed);
        }
        void InputRegion_InputEnd(object sender, CancelEventArgs e)
        {
            this.OnInputEnd(e);
        }
        void InputRegion_PopupClosed(object sender, EventArgs e)
        {
            this.OnViewItemEdited(new ViewItemEventArgs(this.m_InputRegion.Tag as IViewItem));
            //this.Refresh();
        }

        protected override EventStateStyle GetEventStateSupplement(string strEventName)
        {
            switch (strEventName)
            {
                case "SelectedNodeChanged":
                    return this.SelectedNodeChanged != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
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
                case "SelectedNodeChanged":
                    if (this.SelectedNodeChanged != null) { this.SelectedNodeChanged(this, e as PropertyChangedEventArgs); }
                    return true;
                default:
                    break;
            }
            //
            return base.RelationEventSupplement(strEventName, e);
        }

        #region BaseItem
        public override bool LockHeight
        {
            get { return false; }
        }

        public override bool LockWith
        {
            get { return false; }
        }

        public override System.Drawing.Size MeasureSize(System.Drawing.Graphics g)
        {
            int iCount = this.GetNodeViewItemCount();
            if (iCount <= 0) return this.Size;
            //
            int iW = 0, iH = 0;
            NodeViewItem nodeViewItem = null;
            System.Drawing.Size size;
            for (int i = 0; i < iCount; i++)
            {
                nodeViewItem = this.GetNodeViewItem(i);
                if (nodeViewItem != null)
                {
                    size = nodeViewItem.MeasureSize(g);
                    iW += size.Width;
                    iH += size.Height;
                }
            }
            return new System.Drawing.Size(iW, iH);
        }

        public override object Clone()
        {
            NodeViewItemTreeItem baseItem = new NodeViewItemTreeItem();
            return baseItem;
        }
        #endregion

        #region IBaseItemOwner
        [Browsable(false), Description("Items显示矩形框"), Category("布局")]
        public override Rectangle ItemsRectangle
        {
            get
            {
                Rectangle rectangle = this.DisplayRectangle;
                if (this.ShowOutLine) return Rectangle.FromLTRB(rectangle.Left + 1, rectangle.Top + 1, rectangle.Right - 1, rectangle.Bottom - 1);
                return rectangle;
            }
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

        #region IScrollableObjectHelper
        [Browsable(false), Description("水平滚动条最小值"), Category("外观")]
        public int HScrollBarMinimum
        {
            get { return 0; }
        }

        int m_HScrollBarMaximum = 0;
        [Browsable(false), Description("水平滚动条最大值"), Category("外观")]
        public int HScrollBarMaximum
        {
            get { return this.m_HScrollBarMaximum; }
        }

        private bool m_HScrollBarVisible = false;//不要对其直接赋值
        [Browsable(false), Description("水平滚动条是否可见"), Category("外观")]
        public bool HScrollBarVisible
        {
            get
            {
                return this.m_HScrollBarVisible;
            }
        }

        [Browsable(false), Description("水平滚动条显示矩形框"), Category("布局")]
        Rectangle IScrollableObjectHelper.HScrollBarDisplayRectangle
        {
            get
            {
                Rectangle rectangle = this.ItemsRectangle;
                return Rectangle.FromLTRB
                    (
                    rectangle.Left,
                    rectangle.Bottom - (this.HScrollBarVisible ? CONST_SCROLBARBARSIZE : 0),
                    rectangle.Right - (this.VScrollBarVisible ? CONST_SCROLBARBARSIZE : 0),
                    rectangle.Bottom
                    );
            }
        }

        //

        [Browsable(false), Description("竖直滚动条最小值"), Category("外观")]
        public int VScrollBarMinimum
        {
            get { return 0; }
        }

        private int m_VScrollBarMaximum = 0;
        [Browsable(false), Description("竖直滚动条最大值"), Category("外观")]
        public int VScrollBarMaximum
        {
            get { return this.m_VScrollBarMaximum; }
        }

        private bool m_VScrollBarVisible = false;//不要对其直接赋值
        [Browsable(false), Description("竖直滚动条是否可见"), Category("外观")]
        public bool VScrollBarVisible
        {
            get
            {
                return this.m_VScrollBarVisible;
            }
        }

        [Browsable(false), Description("竖直滚动条显示矩形框"), Category("布局")]
        Rectangle IScrollableObjectHelper.VScrollBarDisplayRectangle
        {
            get
            {
                Rectangle rectangle = this.ItemsRectangle;
                return Rectangle.FromLTRB
                    (
                    rectangle.Right - (this.VScrollBarVisible ? CONST_SCROLBARBARSIZE : 0),
                    rectangle.Top,
                    rectangle.Right,
                    rectangle.Bottom - (this.HScrollBarVisible ? CONST_SCROLBARBARSIZE : 0)
                    );
            }
        }

        //

        void IScrollableObjectHelper.ScrollValueRefresh()
        {
            this.Invalidate(this.DisplayRectangle);
        }
        #endregion

        #region IInputObject
        [Browsable(false), Description("输入框文本"), Category("外观")]
        string IInputObject.InputText
        {
            get
            {
                IInputObject pInputObject = this.SelectedNode as IInputObject;
                if (pInputObject == null) return this.Text;
                return pInputObject.InputText;
            }
            set
            {
                IInputObject pInputObject = this.SelectedNode as IInputObject;
                if (pInputObject == null) this.Text = value;
                pInputObject.InputText = value;
            }
        }

        [Browsable(false), Description("输入框文本字体"), Category("外观")]
        Font IInputObject.InputFont
        {
            get
            {
                IInputObject pInputObject = this.SelectedNode as IInputObject;
                if (pInputObject == null) return this.Font;
                return pInputObject.InputFont;
            }
        }

        [Browsable(false), Description("输入框文本颜色"), Category("外观")]
        Color IInputObject.InputForeColor
        {
            get
            {
                IInputObject pInputObject = this.SelectedNode as IInputObject;
                if (pInputObject == null) return this.ForeColor;
                return pInputObject.InputForeColor;
            }
        }

        [Browsable(false), Description("输入区矩形框（屏幕坐标）"), Category("布局")]
        Rectangle IInputObject.InputRegionRectangle
        {
            get
            {
                IInputObject pInputObject = this.SelectedNode as IInputObject;
                if (pInputObject == null) return Rectangle.Empty;
                //
                Rectangle rectangle = Rectangle.Intersect(this.ViewItemsRectangle, pInputObject.InputRegionRectangle);
                int iH = this.m_InputRegion.GetInputRegionSize().Height;
                return new Rectangle
                    (
                    this.PointToScreen(new Point(rectangle.Left, (rectangle.Top + rectangle.Bottom - iH) / 2 + CTR_INPUTREGIONOFFSETY)),
                    new Size(rectangle.Width, iH)
                    );
                //return new Rectangle
                //    (
                //    this.PointToScreen(new Point(rectangle.Left, (rectangle.Top + rectangle.Bottom - iH) / 2)),
                //    new Size(rectangle.Width, iH)
                //    );
            }
        }

        [Browsable(false), Description("是否正在输入"), Category("状态")]
        public bool IsInputing
        {
            get { return this.m_InputRegion.IsOpened; }
        }
        #endregion

        #region IInputObjectHelper
        IInputRegion IInputObjectHelper.GetInputRegion()
        {
            return this.m_InputRegion;
        }
        #endregion

        #region IViewList
        WFNew.IFlexibleList IViewList.List
        {
            get { return this.NodeViewItems; }
        }
        #endregion

        #region IViewListEnumerator
        IViewItem IViewListEnumerator.GetViewItem(int index) { return this.GetNodeViewItem(index); }

        int IViewListEnumerator.GetViewItemCount() { return this.GetNodeViewItemCount(); }
        #endregion

        #region IViewLayoutList
        [Browsable(false), Description("顶部ViewItem索引号"), Category("布局")]
        public int TopViewItemIndex
        {
            get { return this.m_VScrollBarItem.GetEffectiveValue(); }
        }

        private int m_BottomViewItemIndex = 0;
        [Browsable(false), Description("底部部ViewItem索引号"), Category("布局")]
        public int BottomViewItemIndex
        {
            get { return m_BottomViewItemIndex; }
        }

        public ViewItem TryGetViewItemFromPoint(Point point)
        {
            return this.TryGetViewItemFromPoint(point);
        }
        #endregion

        #region IViewItemOwner
        [Browsable(false), Description("ViewItems显示矩形框"), Category("布局")]
        public Rectangle ViewItemsRectangle
        {
            get
            {
                Rectangle rectangle = this.ItemsRectangle;
                return Rectangle.FromLTRB
                    (
                    rectangle.Left,
                    rectangle.Top,
                    rectangle.Right - (this.VScrollBarVisible ? CONST_SCROLBARBARSIZE : 0) - 1,
                    rectangle.Bottom - (this.HScrollBarVisible ? CONST_SCROLBARBARSIZE : 0) - 1
                    );
            }
        }
        #endregion

        #region IViewItemOwner2
        IViewItemOwner2 IViewItemOwner2.GetTopViewItemOwner()
        {
            return this;
        }
        #endregion

        #region IViewItemList
        private bool m_ShowOutLine = true;
        [Browsable(true), DefaultValue(true), Description("显示外框线"), Category("外观")]
        public virtual bool ShowOutLine
        {
            get { return m_ShowOutLine; }
            set { m_ShowOutLine = value; }
        }

        private Color m_BackColor = System.Drawing.SystemColors.Window;
        [Browsable(true), Description("背景色"), Category("外观")]
        public Color BackColor
        {
            get { return m_BackColor; }
            set { m_BackColor = value; }
        }


        [Browsable(false), Description("左侧偏移量"), Category("布局")]
        public int LeftOffset
        {
            get { return this.m_HScrollBarItem.GetEffectiveValue(); }
        }

        [Browsable(false), Description("框架矩形框"), Category("布局")]
        public Rectangle FrameRectangle
        {
            get
            {
                Rectangle rectangle = this.DisplayRectangle;
                return new Rectangle(rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
            }
        }
        #endregion

        #region IViewItemList2
        /// <summary>
        /// 获取输入状态的文本（结合InputEnd事件用）
        /// </summary>
        /// <returns></returns>
        public string TryGetInputingText()
        {
            return this.m_InputRegion.TryGetInputingText();
        }

        /// <summary>
        /// 尝试获取焦点对象
        /// </summary>
        /// <returns></returns>
        public ViewItem TryGetFocusViewItem()
        {
            ViewItem viewItem = this.SelectedNode;
            //
            if (viewItem is IViewList)
            {
                IRowViewItem pRowViewItem = (IRowViewItem)viewItem;
                if (pRowViewItem.List.Count > 0)
                {
                    ViewItem viewItem2 = pRowViewItem.List[pRowViewItem.SelectedIndex] as ViewItem;
                    return viewItem2 != null ? viewItem2 : viewItem;
                }
                else
                {
                    return viewItem;
                }
            }
            else
            {
                return viewItem;
            }
        }
        #endregion

        #region IViewItemListEvent
        [Browsable(true), Description("编辑结束后触发（InputingFilterText == false 时有效）"), Category("属性已更改")]
        public event CancelEventHandler InputEnd;

        [Browsable(true), Description("ViewItem编辑结束后触发"), Category("属性已更改")]
        public event ViewItemEventHandler ViewItemEdited;
        #endregion

        #region INodeViewItemTree
        bool m_CanEdit = false;
        [Browsable(true), DefaultValue(false), Description("是否可以编辑"), Category("状态")]
        public virtual bool CanEdit
        {
            get { return m_CanEdit; }
            set { m_CanEdit = value; }
        }

        NodeViewItem m_SelectedNode = null;//不要对其直接赋值
        [Browsable(false), Description("选择NodeViewItem"), Category("属性")]
        public NodeViewItem SelectedNode
        {
            get { return m_SelectedNode; }
            set
            {
                if (this.m_SelectedNode == value) return;
                //
                PropertyChangedEventArgs e = new PropertyChangedEventArgs(typeof(NodeViewItem), this.m_SelectedNode, value);
                //
                ISetViewItemHelper pSetViewItemHelper = this.m_SelectedNode as ISetViewItemHelper;
                if (pSetViewItemHelper != null)
                {
                    if(pSetViewItemHelper.SetViewParameterStyle(ViewParameterStyle.eNone)) this.Invalidate(this.m_SelectedNode.DisplayRectangle);
                }
                this.m_SelectedNode = value;
                pSetViewItemHelper = this.m_SelectedNode as ISetViewItemHelper;
                if (pSetViewItemHelper != null)
                {
                    if (pSetViewItemHelper.SetViewParameterStyle(ViewParameterStyle.eSelected)) this.Invalidate(this.m_SelectedNode.DisplayRectangle);
                }
                //
                this.OnSelectedNodeChanged(e);
            }
        }

        public NodeViewItem TryGetNodeViewItemFromPoint(Point point)
        {
            NodeViewItem nodeViewItem;
            for (int i = this.TopViewItemIndex; i <= this.BottomViewItemIndex; i++)
            {
                nodeViewItem = this.GetNodeViewItem(i);
                if (nodeViewItem != null && nodeViewItem.DisplayRectangle.Contains(point)) return nodeViewItem;
            }
            return null;
        }
        #endregion

        #region INodeViewItemTreeEvent
        [Browsable(true), Description("选择索引改变后触发"), Category("属性已更改")]
        public event PropertyChangedEventHandler SelectedNodeChanged;
        #endregion

        #region INodeViewList
        [Browsable(true),
        Editor(typeof(GISShare.Controls.WinForm.WFNew.View.Design.NodeViewItemCollectionEditor2), typeof(System.Drawing.Design.UITypeEditor)),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
        Description("其所携带的结点集合"),
        Category("结点")]
        public NodeViewItemCollection NodeViewItems
        {
            get { return m_NodeViewItemCollection; }
        }

        public void Expand()
        {
            foreach (NodeViewItem one in this.NodeViewItems)
            {
                one.SetIsExpand(true);
            }
            this.Refresh();
        }

        public void Collapse()
        {
            foreach (NodeViewItem one in this.NodeViewItems)
            {
                one.SetIsExpand(false);
            }
            this.Refresh();
        }

        public void ExpandAll()
        {
            foreach (NodeViewItem one in this.NodeViewItems)
            {
                one.SetIsExpand(true);
                this.ExpandAll_DG(one);
            }
            //
            this.Refresh();
        }
        private void ExpandAll_DG(NodeViewItem nodeViewItem)
        {
            foreach (NodeViewItem one in nodeViewItem.NodeViewItems)
            {
                one.SetIsExpand(true);
                this.ExpandAll_DG(one);
            }
            nodeViewItem.SetIsExpand(true);
        }

        public void CollapseAll()
        {
            foreach (NodeViewItem one in this.NodeViewItems)
            {
                one.SetIsExpand(false);
                this.CollapseAll_DG(one);
            }
            //
            this.Refresh();
        }
        private void CollapseAll_DG(NodeViewItem nodeViewItem)
        {
            foreach (NodeViewItem one in nodeViewItem.NodeViewItems)
            {
                one.SetIsExpand(false);
                this.CollapseAll_DG(one);
            }
            nodeViewItem.SetIsExpand(false);
        }
        #endregion

        #region 属性
        /// <summary>
        /// 添加复选框
        /// </summary>
        /// <param name="pNodeViewItem">父节点</param>
        /// <param name="bChecked">状态</param>
        /// <param name="strName">名称</param>
        /// <param name="strText">文本</param>
        /// <param name="objValue">属性</param>
        /// <param name="image">图片</param>
        /// <returns>返回复选框</returns>
        public ICheckBoxItem AddCheckedItem(INodeViewItem pNodeViewItem, bool bChecked, string strName, string strText, object objValue, System.Drawing.Image image)
        {
            ICheckBoxItem pCheckBoxItem = null;
            if (image != null)
            {
                pCheckBoxItem = new ImageCheckBoxItem()
                {
                    Checked = bChecked,
                    Name = strName,
                    Text = strText,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Image = image,
                    ImageAlign = ContentAlignment.MiddleLeft,
                    eImageSizeStyle = ImageSizeStyle.eSystem,
                    eHAlignmentStyle = GISShare.Controls.WinForm.WFNew.HAlignmentStyle.eStretch,
                    eVAlignmentStyle = GISShare.Controls.WinForm.WFNew.VAlignmentStyle.eStretch,
                    Tag = objValue
                };
            }
            else
            {
                pCheckBoxItem = new CheckBoxItem()
                {
                    Checked = bChecked,
                    Name = strName,
                    Text = strText,
                    TextAlign = ContentAlignment.MiddleLeft,
                    eHAlignmentStyle = GISShare.Controls.WinForm.WFNew.HAlignmentStyle.eStretch,
                    eVAlignmentStyle = GISShare.Controls.WinForm.WFNew.VAlignmentStyle.eStretch,
                    Tag = objValue
                };
            }
            SuperNodeViewItem superNodeViewItem = new SuperNodeViewItem() { Name = strName, Text = strText, BaseItemObject = (GISShare.Controls.WinForm.WFNew.BaseItem)pCheckBoxItem };
            if (pNodeViewItem != null)
            {
                pNodeViewItem.NodeViewItems.Add(superNodeViewItem);
            }
            else
            {
                this.NodeViewItems.Add(superNodeViewItem);
            }
            return pCheckBoxItem;
        }

        /// <summary>
        /// 添加单选框
        /// </summary>
        /// <param name="pNodeViewItem">父节点</param>
        /// <param name="bChecked">状态</param>
        /// <param name="strName">名称</param>
        /// <param name="strText">文本</param>
        /// <param name="objValue">属性</param>
        /// <param name="image">图片</param>
        /// <returns>返回单选框</returns>
        public IRadioButtonItem AddRadioItem(INodeViewItem pNodeViewItem, bool bChecked, string strName, string strText, object objValue, System.Drawing.Image image)
        {
            IRadioButtonItem pRadioButtonItem = null;
            if (image != null)
            {
                pRadioButtonItem = new ImageRadioButtonItem
                {
                    Checked = bChecked,
                    Name = strName,
                    Text = strText,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Image = image,
                    ImageAlign = ContentAlignment.MiddleLeft,
                    eImageSizeStyle = ImageSizeStyle.eSystem,
                    eHAlignmentStyle = GISShare.Controls.WinForm.WFNew.HAlignmentStyle.eStretch,
                    eVAlignmentStyle = GISShare.Controls.WinForm.WFNew.VAlignmentStyle.eStretch,
                    Tag = objValue
                };
            }
            else
            {
                pRadioButtonItem = new RadioButtonItem()
                {
                    Checked = bChecked,
                    Name = strName,
                    Text = strText,
                    TextAlign = ContentAlignment.MiddleLeft,
                    eHAlignmentStyle = GISShare.Controls.WinForm.WFNew.HAlignmentStyle.eStretch,
                    eVAlignmentStyle = GISShare.Controls.WinForm.WFNew.VAlignmentStyle.eStretch,
                    Tag = objValue
                };
            }
            SuperNodeViewItem superNodeViewItem = new SuperNodeViewItem() { Name = strName, Text = strText, BaseItemObject = (GISShare.Controls.WinForm.WFNew.BaseItem)pRadioButtonItem };
            if (pNodeViewItem != null)
            {
                pNodeViewItem.NodeViewItems.Add(superNodeViewItem);
            }
            else
            {
                this.NodeViewItems.Add(superNodeViewItem);
            }
            return pRadioButtonItem;
        }

        /// <summary>
        /// 获取复选对象集合
        /// </summary>
        /// <param name="bChecked">状态</param>
        /// <returns></returns>
        public IList<ICheckBoxItem> GetCheckedItems(bool bChecked)
        {
            IList<ICheckBoxItem> checkBoxItemList = new List<ICheckBoxItem>();
            GetCheckedItems_DG(checkBoxItemList, this.NodeViewItems, bChecked);
            return checkBoxItemList;
        }
        private void GetCheckedItems_DG(IList<ICheckBoxItem> checkBoxItemList, NodeViewItemCollection nodeViewItemCollection, bool bChecked)
        {
            foreach (GISShare.Controls.WinForm.WFNew.View.NodeViewItem one in nodeViewItemCollection)
            {
                if (one is ISuperViewItem)
                {
                    ISuperViewItem pSuperViewItem = (ISuperViewItem)one;
                    if (pSuperViewItem.BaseItemObject is ICheckBoxItem)
                    {
                        ICheckBoxItem pCheckBoxItem = (ICheckBoxItem)pSuperViewItem.BaseItemObject;
                        if (pCheckBoxItem.Checked == bChecked)
                        {
                            checkBoxItemList.Add(pCheckBoxItem);
                        }
                    }
                }
                //
                this.GetCheckedItems_DG(checkBoxItemList, one.NodeViewItems, bChecked);
            }
        }
        #endregion

        #region 修改消息链条
        protected override void MessageMonitor(MessageInfo messageInfo)
        {
            switch (messageInfo.eMessageStyle)
            {
                case MessageStyle.eMSViewInfo:
                    this.MSViewInfo(messageInfo);
                    break;
                case MessageStyle.eMSPaint:
                    this.MSPaint(messageInfo);
                    break;
                //
                case MessageStyle.eMSLostFocus:
                    this.MSLostFocus(messageInfo);
                    break;
                case MessageStyle.eMSKeyDown:
                    this.MSKeyDown(messageInfo);
                    break;
                case MessageStyle.eMSKeyUp:
                    this.MSKeyUp(messageInfo);
                    break;
                case MessageStyle.eMSKeyPress:
                    this.MSKeyPress(messageInfo);
                    break;
                case MessageStyle.eMSMouseWheel:
                    this.MSMouseWheel(messageInfo);
                    break;
                //
                case MessageStyle.eMSMouseDown:
                    this.MSMouseDown(messageInfo);
                    break;
                case MessageStyle.eMSMouseUp:
                    this.MSMouseUp(messageInfo);
                    break;
                case MessageStyle.eMSMouseMove:
                    this.MSMouseMove(messageInfo);
                    break;
                case MessageStyle.eMSMouseClick:
                    this.MSMouseClick(messageInfo);
                    break;
                case MessageStyle.eMSMouseDoubleClick:
                    this.MSMouseDoubleClick(messageInfo);
                    break;
                case MessageStyle.eMSMouseEnter:
                    this.MSMouseEnter(messageInfo);
                    break;
                case MessageStyle.eMSMouseLeave:
                    this.MSMouseLeave(messageInfo);
                    break;
                //
                case MessageStyle.eMSEnabledChanged:
                    this.MSEnabledChanged(messageInfo);
                    break;
                case MessageStyle.eMSVisibleChanged:
                    this.MSVisibleChanged(messageInfo);
                    break;
                default:
                    base.MessageMonitor(messageInfo);
                    break;
            }
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
        private void MSViewInfo(MessageInfo messageInfo)
        {

        }
        private void MSPaint(MessageInfo messageInfo)
        {
            PaintEventArgs e = messageInfo.MessageParameter as PaintEventArgs;
            if (e == null) return;
            //
            #region 绘制节点项
            Rectangle rectangle = this.ViewItemsRectangle;
            rectangle = Rectangle.FromLTRB(rectangle.Left + CONST_VIEWRECTANGLESPACE, rectangle.Top + CONST_VIEWRECTANGLESPACE, rectangle.Right, rectangle.Bottom);
            //
            int iLeftOffset = this.LeftOffset;
            int iRightOffset = this.HScrollBarMaximum - iLeftOffset;
            if (iRightOffset == 0) iRightOffset = 0;
            //
            int iCount = GISShare.Controls.WinForm.WFNew.LayoutEngine.LayoutStackV_ListBox
                (e.Graphics,
                this,
                rectangle,
                this.TopViewItemIndex,
                iLeftOffset,
                this.m_HScrollBarMaximum - iLeftOffset,
                ref this.m_BottomViewItemIndex,
                ref this.m_HScrollBarMaximum
                );
            //
            rectangle = Rectangle.Intersect(rectangle, e.ClipRectangle);//Key
            //
            this.m_VScrollBarMaximum = iCount - (this.m_BottomViewItemIndex - this.TopViewItemIndex);
            //
            this.SetHVScrollBarVisible(this.HScrollBarMaximum > this.HScrollBarMinimum, this.VScrollBarMaximum > this.VScrollBarMinimum);
            //
            if (this.m_BottomViewItemIndex < iCount) this.m_BottomViewItemIndex++;
            NodeViewItem nodeViewItem;
            IMessageChain pMessageChain;
            for (int i = this.TopViewItemIndex; i < this.m_BottomViewItemIndex; i++)
            {
                nodeViewItem = this.GetNodeViewItem(i);
                if (nodeViewItem != null)
                {
                    pMessageChain = nodeViewItem as IMessageChain;
                    if (pMessageChain != null)
                    {
                        Rectangle clipRectangle = Rectangle.Intersect(rectangle, nodeViewItem.DisplayRectangle);
                        e.Graphics.SetClip(clipRectangle);
                        //pMessageChain.SendMessage(messageInfo);//new MessageInfo(this, MessageStyle.eMSPaint, new PaintEventArgs(e.Graphics, clipRectangle))
                        pMessageChain.SendMessage(new MessageInfo(this, MessageStyle.eMSPaint, new PaintEventArgs(e.Graphics, clipRectangle)));//messageInfo
                    }
                }
            }
            #region 已抛弃（布局移植到LayoutEngine）
            //Rectangle rectangle = this.ViewItemsRectangle;
            //rectangle = Rectangle.FromLTRB(rectangle.Left + CONST_VIEWRECTANGLESPACE, rectangle.Top + CONST_VIEWRECTANGLESPACE, rectangle.Right, rectangle.Bottom);
            ////
            //int iLeftOffset = this.LeftOffset;
            //int iRightOffset = this.HScrollBarMaximum - iLeftOffset;
            //if (iRightOffset == 0) iRightOffset = 0;
            ////
            //this.m_HScrollBarMaximum = 0;
            ////
            //int iW_W;
            //NodeViewItem nodeViewItem = null;
            //Size nodeViewItemSize;
            //int iItemHeight = 0;
            //int iNum = this.TopViewItemIndex;
            //int iCount = this.GetNodeViewItemCount();
            //ISetViewItemHelper pSetViewItemHelper;
            //IMessageChain pMessageChain;
            //for (; iNum < iCount; iNum++)
            //{
            //    nodeViewItem = this.GetNodeViewItem(iNum);
            //    if (nodeViewItem != null)
            //    {
            //        nodeViewItemSize = nodeViewItem.MeasureSize(e.Graphics);
            //        iW_W = nodeViewItemSize.Width - rectangle.Width;
            //        if (iW_W > this.m_HScrollBarMaximum) this.m_HScrollBarMaximum = iW_W;
            //        //
            //        pSetViewItemHelper = nodeViewItem as ISetViewItemHelper;
            //        if (pSetViewItemHelper != null)
            //        {
            //            pSetViewItemHelper.SetViewItemDisplayRectangle
            //                (
            //                Rectangle.FromLTRB
            //                   (rectangle.Left - iLeftOffset,
            //                   rectangle.Top + iItemHeight,
            //                   rectangle.Right + iRightOffset,
            //                   rectangle.Top + iItemHeight + nodeViewItemSize.Height)
            //                );
            //            //System.Diagnostics.Debug.WriteLine(iLeftOffset);
            //        }
            //        iItemHeight += nodeViewItem.Height;
            //        if (iItemHeight >= rectangle.Height) break;
            //    }
            //}
            //for (int i = iNum; i < iCount; i++) 
            //{
            //    nodeViewItem = this.GetNodeViewItem(i);
            //    if (nodeViewItem != null)
            //    {
            //        nodeViewItemSize = nodeViewItem.MeasureSize(e.Graphics);
            //        iW_W = nodeViewItemSize.Width - rectangle.Width;
            //        if (iW_W > this.m_HScrollBarMaximum) this.m_HScrollBarMaximum = iW_W;
            //    }
            //}
            ////
            //this.m_VScrollBarMaximum = iCount - (iNum - this.TopViewItemIndex);
            ////
            //this.SetHVScrollBarVisible(this.HScrollBarMaximum > this.HScrollBarMinimum, this.VScrollBarMaximum > this.VScrollBarMinimum);
            ////
            //if (iNum < iCount) iNum++;
            //for (int i = this.TopViewItemIndex; i < iNum; i++)
            //{
            //    nodeViewItem = this.GetNodeViewItem(i);
            //    if (nodeViewItem != null)
            //    {
            //        pMessageChain = nodeViewItem as IMessageChain;
            //        if (pMessageChain != null)
            //        {
            //            Rectangle clipRectangle = Rectangle.Intersect(rectangle, nodeViewItem.DisplayRectangle);
            //            e.Graphics.SetClip(clipRectangle);
            //            pMessageChain.SendMessage(new MessageInfo(this, MessageStyle.eMSPaint, new PaintEventArgs(e.Graphics, clipRectangle)));
            //        }
            //    }
            //}
            ////
            //this.m_BottomViewItemIndex = iNum;
            #endregion
            #endregion
            //
            e.Graphics.SetClip(e.ClipRectangle);
        }
        private void SetHVScrollBarVisible(bool bHScrollBarVisible, bool bVScrollBarVisible)
        {
            bool bH = this.m_HScrollBarVisible == bHScrollBarVisible;
            bool bV = this.m_VScrollBarVisible == bVScrollBarVisible;
            if (bH && bV) return;
            if (!bH) this.m_HScrollBarVisible = bHScrollBarVisible;
            if (!bV) this.m_VScrollBarVisible = bVScrollBarVisible;
            this.Invalidate(this.ItemsRectangle);
        }
        private NodeViewItem GetNodeViewItem(int index)//根据可视化索引获取可视化节点
        {
            int iNum = 0;
            return this.GetNodeViewItem_DG(this.NodeViewItems, index, ref iNum);
        }
        private NodeViewItem GetNodeViewItem_DG(NodeViewItemCollection nodeViewItems, int index, ref int iNum)
        {
            NodeViewItem nodeViewItem = null;
            foreach (NodeViewItem one in nodeViewItems)
            {
                if (one.Visible)
                {
                    if (iNum == index) return one;
                    iNum++;
                    if (one.IsExpanded)
                    {
                        nodeViewItem = this.GetNodeViewItem_DG(one.NodeViewItems, index, ref iNum);
                        if (nodeViewItem != null) return nodeViewItem;
                    }
                }
            }
            return nodeViewItem;
        }
        private int GetNodeViewItemCount()//获取可视化节点个数
        {
            return this.GetNodeViewItemCount_DG(this.NodeViewItems);
        }
        private int GetNodeViewItemCount_DG(NodeViewItemCollection nodeViewItems)
        {
            int iNum = 0;
            foreach (NodeViewItem one in nodeViewItems)
            {
                if (one.Visible)
                {
                    iNum++;
                    if (one.IsExpanded)
                    {
                        iNum += this.GetNodeViewItemCount_DG(one.NodeViewItems);
                    }
                }
            }
            return iNum;
        }
        private void MSLostFocus(MessageInfo messageInfo)
        {
            ViewItem viewItem = this.SelectedNode;
            if (viewItem != null)
            {
                IMessageChain pMessageChain = viewItem as IMessageChain;
                if (pMessageChain != null)
                {
                    pMessageChain.SendMessage(messageInfo);
                }
            }
        }
        //private int m_KeyDownKeyValue = -1;
        private void MSKeyDown(MessageInfo messageInfo)
        {
            //KeyEventArgs e = messageInfo.MessageParameter as KeyEventArgs;
            //if (e == null) return;
            ////
            ViewItem viewItem = this.SelectedNode;
            if (viewItem != null)
            {
                IMessageChain pMessageChain = viewItem as IMessageChain;
                if (pMessageChain != null)
                {
                    pMessageChain.SendMessage(messageInfo);
                }
            }
        }
        private void MSKeyUp(MessageInfo messageInfo)
        {
            ViewItem viewItem = this.SelectedNode;
            if (viewItem != null)
            {
                IMessageChain pMessageChain = viewItem as IMessageChain;
                if (pMessageChain != null)
                {
                    pMessageChain.SendMessage(messageInfo);
                }
            }
            ////
            //this.m_KeyDownKeyValue = -1;
        }
        private void MSKeyPress(MessageInfo messageInfo)
        {
            ViewItem viewItem = this.SelectedNode;
            if (viewItem != null)
            {
                IMessageChain pMessageChain = viewItem as IMessageChain;
                if (pMessageChain != null)
                {
                    pMessageChain.SendMessage(messageInfo);
                }
            }
        }
        private void MSMouseWheel(MessageInfo messageInfo)
        {
            ViewItem viewItem = this.SelectedNode;
            if (viewItem != null)
            {
                IMessageChain pMessageChain = viewItem as IMessageChain;
                if (pMessageChain != null)
                {
                    pMessageChain.SendMessage(messageInfo);
                }
            }
        }
        private void MSMouseDown(MessageInfo messageInfo)
        {
            MouseEventArgs e = messageInfo.MessageParameter as MouseEventArgs;
            if (e == null) return;
            //
            if (this.ViewItemsRectangle.Contains(e.Location))
            {
                //if (!this.Focused) this.Focus();
                ////
                if (this.Enabled && this.CanEdit)
                {
                    ITextEditViewItem pTextEditViewItem = this.SelectedNode as ITextEditViewItem;
                    if (pTextEditViewItem != null &&
                        pTextEditViewItem.CanEdit &&
                        pTextEditViewItem.InputRegionRectangle.Contains(e.Location))
                    {
                        Rectangle rectangle = ((IInputObject)this).InputRegionRectangle;
                        if (rectangle.Width > 6 && rectangle.Height > 6)
                        {
                            this.m_InputRegion.Tag = pTextEditViewItem.EditObject;
                            if (this.m_InputRegion.Tag is IViewItem)
                            {
                                this.m_InputRegion.ShowInputRegion();
                                return;
                            }
                        }
                    }
                }
                //
                NodeViewItem nodeViewItem = null;
                for (int i = this.TopViewItemIndex; i <= this.BottomViewItemIndex; i++)
                {
                    nodeViewItem = this.GetNodeViewItem(i);
                    if (nodeViewItem != null &&
                        nodeViewItem.Enabled &&
                        nodeViewItem.DisplayRectangle.Contains(e.Location))
                    {
                        IMessageChain pMessageChain = nodeViewItem as IMessageChain;
                        if (pMessageChain != null)
                        {
                            //MessageBox.Show("A1");
                            pMessageChain.SendMessage(messageInfo);
                        }
                        //
                        if (this.ViewItemsRectangle.Contains(e.Location))
                        {
                            //MessageBox.Show(e.Location.ToString() + " | " + nodeViewItem.PlusMinusRectangle.ToString());
                            if (nodeViewItem.NodeViewItems.Count <= 0 ||
                                (nodeViewItem.ShowPlusMinus &&
                                !nodeViewItem.PlusMinusRectangle.Contains(e.Location)))
                            {
                                //MessageBox.Show("3");
                                this.SelectedNode = nodeViewItem;
                                ISetViewItemHelper pSetViewItemHelper = this.SelectedNode as ISetViewItemHelper;
                                if (pSetViewItemHelper != null)
                                {
                                    //MessageBox.Show("4");
                                    if (pSetViewItemHelper.SetViewParameterStyle(ViewParameterStyle.eFocused)) this.Invalidate(this.m_SelectedNode.DisplayRectangle);
                                }
                            }
                        }
                        //
                        break;
                    }
                }
            }
            #region 已抛弃（勿删除）
            //MouseEventArgs e = messageInfo.MessageParameter as MouseEventArgs;
            //if (e == null) return;
            ////
            //if (this.ViewItemsRectangle.Contains(e.Location))
            //{
            //    //Control ctr = this.TryGetDependControl();
            //    //if (ctr != null && !ctr.Focused) ctr.Focus();
            //    ////
            //    NodeViewItem nodeViewItem = null;
            //    for (int i = this.TopViewItemIndex; i <= this.BottomViewItemIndex; i++)
            //    {
            //        nodeViewItem = this.GetNodeViewItem(i);
            //        if (nodeViewItem != null &&
            //            nodeViewItem.Enabled &&
            //            nodeViewItem.DisplayRectangle.Contains(e.Location))
            //        {
            //            IMessageChain pMessageChain = nodeViewItem as IMessageChain;
            //            if (pMessageChain != null)
            //            {
            //                pMessageChain.SendMessage(messageInfo);
            //            }
            //            //
            //            if (this.ViewItemsRectangle.Contains(e.Location))
            //            {
            //                if (nodeViewItem.NodeViewItems.Count <= 0 ||
            //                    (nodeViewItem.ShowPlusMinus &&
            //                    !nodeViewItem.PlusMinusRectangle.Contains(e.Location)))
            //                {
            //                    this.SelectedNode = nodeViewItem;
            //                    ISetViewItemHelper pSetViewItemHelper = this.SelectedNode as ISetViewItemHelper;
            //                    if (pSetViewItemHelper != null)
            //                    {
            //                        if (pSetViewItemHelper.SetViewParameterStyle(ViewParameterStyle.eFocused)) this.Invalidate(this.m_SelectedNode.DisplayRectangle);
            //                    }
            //                }
            //            }
            //            //
            //            break;
            //        }
            //    }
            //    //
            //    if (this.Enabled)
            //    {
            //        if (this.SelectedNode != null &&
            //            this.SelectedNode.Enabled &&
            //            this.SelectedNode.Visible &&
            //            this.SelectedNode.CanEdit)
            //        {
            //            ITextEditViewItem pTextEditViewItem = this.SelectedNode as ITextEditViewItem;
            //            if (pTextEditViewItem != null && pTextEditViewItem.InputRegionRectangle.Contains(e.Location))
            //            {
            //                Rectangle rectangle = ((IInputObject)this).InputRegionRectangle;
            //                if (rectangle.Width > 6 && rectangle.Height > 6)
            //                {
            //                    this.m_InputRegion.Tag = pTextEditViewItem.EditObject;
            //                    if (this.m_InputRegion.Tag is IViewItem)
            //                    {
            //                        this.m_InputRegion.ShowInputRegion();
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
            #endregion
        }
        private void MSMouseUp(MessageInfo messageInfo)
        {
            NodeViewItem nodeViewItem = null;
            for (int i = this.TopViewItemIndex; i <= this.BottomViewItemIndex; i++)
            {
                nodeViewItem = this.GetNodeViewItem(i);
                if (nodeViewItem == null || !nodeViewItem.Enabled) continue;
                //
                IMessageChain pMessageChain = nodeViewItem as IMessageChain;
                if (pMessageChain != null)
                {
                    pMessageChain.SendMessage(messageInfo);
                }
            }
        }
        private void MSMouseMove(MessageInfo messageInfo)
        {
            NodeViewItem nodeViewItem = null;
            for (int i = this.TopViewItemIndex; i <= this.BottomViewItemIndex; i++)
            {
                nodeViewItem = this.GetNodeViewItem(i);
                if (nodeViewItem == null || !nodeViewItem.Enabled) continue;
                //
                IMessageChain pMessageChain = nodeViewItem as IMessageChain;
                if (pMessageChain != null)
                {
                    pMessageChain.SendMessage(messageInfo);
                }
            }
        }
        private void MSMouseClick(MessageInfo messageInfo)
        {
            MouseEventArgs e = messageInfo.MessageParameter as MouseEventArgs;
            if (e == null) return;
            //
            if (this.ViewItemsRectangle.Contains(e.Location))
            {
                NodeViewItem nodeViewItem = null;
                for (int i = this.TopViewItemIndex; i <= this.BottomViewItemIndex; i++)
                {
                    nodeViewItem = this.GetNodeViewItem(i);
                    if (nodeViewItem != null &&
                        nodeViewItem.Enabled &&
                        nodeViewItem.DisplayRectangle.Contains(e.Location))
                    {
                        IMessageChain pMessageChain = nodeViewItem as IMessageChain;
                        if (pMessageChain != null)
                        {
                            pMessageChain.SendMessage(messageInfo);
                        }
                        //
                        break;
                    }
                }
            }
        }
        private void MSMouseDoubleClick(MessageInfo messageInfo)
        {
            MouseEventArgs e = messageInfo.MessageParameter as MouseEventArgs;
            if (e == null) return;
            //
            if (this.ViewItemsRectangle.Contains(e.Location))
            {
                NodeViewItem nodeViewItem = null;
                for (int i = this.TopViewItemIndex; i <= this.BottomViewItemIndex; i++)
                {
                    nodeViewItem = this.GetNodeViewItem(i);
                    if (nodeViewItem != null &&
                        nodeViewItem.Enabled &&
                        nodeViewItem.DisplayRectangle.Contains(e.Location))
                    {
                        IMessageChain pMessageChain = nodeViewItem as IMessageChain;
                        if (pMessageChain != null)
                        {
                            pMessageChain.SendMessage(messageInfo);
                        }
                        //
                        break;
                    }
                }
            }
        }
        private void MSMouseEnter(MessageInfo messageInfo)
        {

        }
        private void MSMouseLeave(MessageInfo messageInfo)
        {
            for (int i = this.TopViewItemIndex; i <= this.BottomViewItemIndex; i++)
            {
                IMessageChain pMessageChain = this.GetNodeViewItem(i) as IMessageChain;
                if (pMessageChain != null)
                {
                    pMessageChain.SendMessage(messageInfo);
                }
            }
        }
        private void MSEnabledChanged(MessageInfo messageInfo)
        {
            for (int i = this.TopViewItemIndex; i <= this.BottomViewItemIndex; i++)
            {
                IMessageChain pMessageChain = this.GetNodeViewItem(i) as IMessageChain;
                if (pMessageChain != null)
                {
                    pMessageChain.SendMessage(messageInfo);
                }
            }
        }
        private void MSVisibleChanged(MessageInfo messageInfo)
        {
            for (int i = this.TopViewItemIndex; i <= this.BottomViewItemIndex; i++)
            {
                IMessageChain pMessageChain = this.GetNodeViewItem(i) as IMessageChain;
                if (pMessageChain != null)
                {
                    pMessageChain.SendMessage(messageInfo);
                }
            }
        }
        #endregion

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (this.m_VScrollBarVisible &&
                this.m_VScrollBarMaximum > this.VScrollBarMinimum &&
                this.m_VScrollBarItem.Value >= this.VScrollBarMinimum &&
                this.m_VScrollBarItem.Value <= this.m_VScrollBarMaximum)
            {
                if (e.Delta < 0) this.m_VScrollBarItem.Value++;
                else if (e.Delta > 0) this.m_VScrollBarItem.Value--;
            }
            else if (this.m_HScrollBarVisible &&
                this.m_HScrollBarMaximum > this.HScrollBarMinimum &&
                this.m_HScrollBarItem.Value >= this.HScrollBarMinimum &&
                this.m_HScrollBarItem.Value <= this.m_HScrollBarMaximum)
            {
                if (e.Delta < 0) this.m_HScrollBarItem.Value++;
                else if (e.Delta > 0) this.m_HScrollBarItem.Value--;
            }
            //
            base.OnMouseWheel(e);
        }

        protected override void OnDraw(PaintEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderViewItemList(
                new ObjectRenderEventArgs(e.Graphics, this, this.DisplayRectangle));
        }

        //
        protected virtual void OnSelectedNodeChanged(PropertyChangedEventArgs e)
        {
            if (this.SelectedNodeChanged != null) { this.SelectedNodeChanged(this, e); }
        }

        protected virtual void OnInputEnd(CancelEventArgs e)
        {
            if (this.InputEnd != null) { this.InputEnd(this, e); }
        }

        protected virtual void OnViewItemEdited(ViewItemEventArgs e)
        {
            if (this.ViewItemEdited != null) { this.ViewItemEdited(this, e); }
        }
    }
}

