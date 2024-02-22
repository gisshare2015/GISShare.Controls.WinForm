using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew.View
{
    [Serializable, DefaultProperty("Text"), TypeConverter(typeof(GISShare.Controls.WinForm.WFNew.View.Design.NodeViewItemConverter))]//
    public class NodeViewItem : TextViewItem,        
        IViewList,
        INodeViewList, 
        ITextEditViewItem, IVisibleViewItem, INodeViewItem, INodeViewItem2,
        IOwner, IBaseItemOwner, ISetOwnerHelper,
        IViewItemOwner, IViewItemOwner2, 
        IInputObject,
        ICloneable
    {
        private const int CONST_NODEOFFSET = 9;
        private const int CONST_PTSPACE = 5;
        private const int CONST_PLUSMINUSBUTTONSIZE = 9;

        private NodeViewItemCollection m_NodeViewItemCollection;

        public NodeViewItem()
            : base()
        {
            this.m_NodeViewItemCollection = new NodeViewItemCollection(this);
        }

        public NodeViewItem(string text)
            : this() 
        {
            this.Text = text;
        }

        public NodeViewItem(string name, string text)
            : this()
        {
            this.Name = name;
            this.Text = text;
        }

        #region IOwner
        IOwner m_pOwner = null;
        [Browsable(false), Description("��ȡ��ӵ����"), Category("����")]
        public IOwner pOwner
        {
            get { return m_pOwner; }
        }

        public void Refresh()
        {
            if (this.pOwner != null) this.pOwner.Refresh();
        }

        public void Invalidate(Rectangle rectangle)
        {
            if (this.pOwner != null) this.pOwner.Invalidate(rectangle);
        }

        public Point PointToClient(Point point)
        {
            if (this.pOwner != null) return this.pOwner.PointToClient(point);
            return Point.Empty;
        }

        public Point PointToScreen(Point point)
        {
            if (this.pOwner != null) return this.pOwner.PointToScreen(point);
            return Point.Empty;
        }
        #endregion

        #region ISetOwnerHelper
        bool ISetOwnerHelper.SetOwner(IOwner owner)
        {
            if (this.m_pOwner == owner) return false;
            //
            //
            //
            this.m_pOwner = owner;
            ((IReset)this).Reset();
            //
            //
            //
            return true;
        }
        #endregion

        #region IBaseItemOwner
        [Browsable(false), Description("������չ�־���"), Category("����")]
        Rectangle IBaseItemOwner.ItemsRectangle
        {
            get
            {
                return this.DisplayRectangle;
            }
        }

        [Browsable(false), Description("������ͼ��չ�־���"), Category("����")]
        Rectangle IBaseItemOwner.ItemsViewRectangle
        {
            get
            {
                return this.DisplayRectangle;
                //IBaseItemOwner pBaseItemOwner = pOwner as IBaseItemOwner;
                //return pBaseItemOwner == null ? ((IBaseItemOwner)this).ItemsRectangle : Rectangle.Intersect(pBaseItemOwner.ItemsViewRectangle, ((IBaseItemOwner)this).ItemsRectangle);
            }
        }

        [Browsable(false), Description("��ȡ������ӵ����"), Category("����")]
        WFNew.IBaseItemOwner WFNew.IBaseItemOwner.pBaseItemOwner
        {
            get { return null; }
        }
        #endregion

        #region IViewItemOwner
        Rectangle IViewItemOwner.ViewItemsRectangle
        {
            get
            {
                IViewItemOwner pViewItemOwner = this.pOwner as IViewItemOwner;
                return pViewItemOwner == null ? this.DisplayRectangle : pViewItemOwner.ViewItemsRectangle;
            }
        }
        #endregion

        #region IViewItemOwner2
        IViewItemOwner2 IViewItemOwner2.GetTopViewItemOwner()
        {
            IViewItemOwner2 pViewItemOwner2 = this.pOwner as IViewItemOwner2;
            return pViewItemOwner2 == null ? null : pViewItemOwner2.GetTopViewItemOwner();
        }
        #endregion

        #region ITextEditViewItem
        bool m_CanEdit = false;
        [Browsable(true), DefaultValue(false), Description("�Ƿ���Ա༭"), Category("״̬")]
        public virtual bool CanEdit
        {
            get { return m_CanEdit; }
            set { m_CanEdit = value; }
        }

        bool m_CanSelect = true;
        [Browsable(true), DefaultValue(true), Description("�Ƿ����ѡ��"), Category("״̬")]
        public virtual bool CanSelect
        {
            get { return m_CanSelect; }
            set { m_CanSelect = value; }
        }

        [Browsable(false), Description("��ǰ�༭����"), Category("����")]
        object ITextEditViewItem.EditObject
        {
            get { return this.ITextEditViewItem_EditObject; }
        }
        internal protected virtual object ITextEditViewItem_EditObject
        {
            get { return this; }
        }
        #endregion

        #region IVisibleViewItem
        bool m_Visible = true;
        [Browsable(true), DefaultValue(true), Description("�ɼ�"), Category("״̬")]
        public bool Visible
        {
            get { return m_Visible; }
            set
            {
                if (m_Visible == value) return;
                ((IMessageChain)this).SendMessage(new MessageInfo(this, MessageStyle.eMSVisibleChanged, new BoolValueChangedEventArgs(value)));
                m_Visible = value;
            }
        }
        #endregion

        #region INodeViewItem
        bool m_Enabled = true;
        [Browsable(true), DefaultValue(true), Description("����"), Category("״̬")]
        public bool Enabled
        {
            get { return m_Enabled; }
            set
            {
                if (m_Enabled == value) return;
                ((IMessageChain)this).SendMessage(new MessageInfo(this, MessageStyle.eMSEnabledChanged, new BoolValueChangedEventArgs(value)));
                m_Enabled = value;
            }
        }

        bool m_IsExpanded = false;
        [Browsable(true), DefaultValue(false), Description("�Ƿ��۵�"), Category("״̬")]
        public bool IsExpanded
        {
            get { return m_IsExpanded; }
            set
            {
                if (this.SetIsExpand(value))
                {
                    if (this.Visible) this.Refresh();
                }
            }
        }

        bool m_ShowLines = true;
        [Browsable(true), DefaultValue(true), Description("��ʾ������"), Category("���")]
        public bool ShowLines
        {
            get { return m_ShowLines; }
            set { m_ShowLines = value; }
        }

        bool m_ShowPlusMinus = true;
        [Browsable(true), DefaultValue(true), Description("��ʾ�Ӽ���"), Category("���")]
        public bool ShowPlusMinus
        {
            get { return m_ShowPlusMinus; }
            set { m_ShowPlusMinus = value; }
        }

        bool m_ShowNomalState = false;
        [Browsable(true), DefaultValue(true), Description("��ʾ����״̬"), Category("���")]
        public bool ShowNomalState
        {
            get { return m_ShowNomalState; }
            set { m_ShowNomalState = value; }
        }

        bool m_ShowBaseItemState = true;
        [Browsable(false), DefaultValue(true), Description("�Ƿ���ʾ����״̬"), Category("����")]
        public bool ShowBaseItemState
        {
            get { return m_ShowBaseItemState; }
            set { m_ShowBaseItemState = value; }
        }

        [Browsable(false), Description("�ڵ����"), Category("����")]
        public int NodeDepth
        {
            get
            {
                return this.GetNodeDepth_DG(this.pOwner);
            }
        }
        private int GetNodeDepth_DG(IOwner owner)
        {
            if (owner is INodeViewItem)//.pOwner
            {
                int iDepth = 0;
                iDepth = 1 + this.GetNodeDepth_DG(owner.pOwner);
                return iDepth;
            }
            return 0;
        }

        [Browsable(false), Description("�Ƿ�Ϊ���ڵ�"), Category("����")]
        public bool IsRootNode
        {
            get { return this.pOwner is IViewItemList; }
        }

        [Browsable(false), Description("X��ƫ�������������¹滮�ı�������"), Category("����")]
        public virtual int OffsetX
        { 
            get { return 0; } 
        }

        [Browsable(false), Description("��ȡ�ڵ�ƫ����"), Category("����")]
        public int NodeOffset
        {
            get
            {
                INodeViewItem pNodeViewItem = this.pOwner as INodeViewItem;
                if (pNodeViewItem != null)
                {
                    return pNodeViewItem.NodeOffset + CONST_NODEOFFSET;
                }
                return CONST_NODEOFFSET / 3;
            }
        }

        [Browsable(false), Description("��ȡ�ڵ��ı���ƫ����"), Category("����")]
        public int NodeTextOffset
        {
            get
            {
                int iOffset = this.NodeOffset;
                if (this.ShowPlusMinus) iOffset += CONST_PLUSMINUSBUTTONSIZE + CONST_PTSPACE;
                //
                iOffset += this.OffsetX;
                //
                return iOffset;
            }
        }

        [Browsable(false), Description("��ȡ�丸�ڵ�"), Category("����")]
        public NodeViewItem ParentNode
        {
            get { return this.pOwner as NodeViewItem; }
        }

        [Browsable(false), Description("��ȡ�����ڵĽڵ��б�"), Category("����")]
        public INodeViewList NodeViewList
        {
            get { return this.pOwner as INodeViewList; }
        }

        [Browsable(false), Description("�۵���ťչ�־���"), Category("����")]
        public Rectangle PlusMinusRectangle
        {
            get
            {
                Rectangle rectangle = this.DisplayRectangle;
                if (this.ShowPlusMinus)
                {
                    return new Rectangle(
                        rectangle.Left + this.NodeOffset,
                        (rectangle.Top + rectangle.Bottom - CONST_PLUSMINUSBUTTONSIZE) / 2,
                        CONST_PLUSMINUSBUTTONSIZE,
                        CONST_PLUSMINUSBUTTONSIZE);
                }
                else
                {
                    return new Rectangle(
                        rectangle.Left + this.NodeOffset,
                        (rectangle.Top + rectangle.Bottom - CONST_PLUSMINUSBUTTONSIZE) / 2,
                        0,
                        0);
                }
            }
        }

        [Browsable(false), Description("�ı��༭չ�־���"), Category("����")]
        public Rectangle TextRectangle
        {
            get
            {
                //int iOffset = this.NodeOffset;
                //if (this.ShowPlusMinus) iOffset += CONST_PLUSMINUSBUTTONSIZE + CONST_PTSPACE;
                ////
                //iOffset += this.OffsetX;
                int iOffset = this.NodeTextOffset;
                //
                Rectangle rectangle = this.DisplayRectangle;
                return new Rectangle
                    (
                    rectangle.Left + iOffset,
                    (rectangle.Top + rectangle.Bottom - this.m_TextSize.Height) / 2,
                    this.m_TextSize.Width,
                    this.m_TextSize.Height
                    );
            }
        }

        [Browsable(false), Description("�Ƿ���ڿɼ���"), Category("״̬")]
        public bool HaveVisibleNodeView
        {
            get
            {
                foreach (NodeViewItem one in this.NodeViewItems)
                {
                    if (one.Visible) return true;
                }
                return false;
            }
        }

        public bool SetIsExpand(bool value)
        {
            if (m_IsExpanded == value) return false;
            this.m_IsExpanded = value;
            return true;
        }

        public INodeViewItemTree TryGetNodeViewItemTree()
        {
            return ((IViewItemOwner2)this).GetTopViewItemOwner() as INodeViewItemTree;
            //return this.TryGetDependControl_DG(this.pOwner);
        }
        //private INodeViewItemTree TryGetDependControl_DG(IOwner owner)
        //{
        //    if (owner == null) return null;
        //    //
        //    INodeViewItemTree pNodeViewItemTree = owner as INodeViewItemTree;
        //    if (pNodeViewItemTree == null) return this.TryGetDependControl_DG(owner.pOwner);
        //    return pNodeViewItemTree;
        //}
        #endregion

        #region INodeViewList
        [Browsable(true),
        Editor(typeof(GISShare.Controls.WinForm.WFNew.View.Design.NodeViewItemCollectionEditor2), typeof(System.Drawing.Design.UITypeEditor)),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
        Description("����Я���Ľ�㼯��"),
        Category("���")]
        public NodeViewItemCollection NodeViewItems
        {
            get { return m_NodeViewItemCollection; }
        }

        public void Expand()
        {
            this.SetIsExpand(true);
            //
            this.Refresh();
        }

        public void Collapse()
        {
            this.SetIsExpand(false);
            //
            this.Refresh();
        }

        public void ExpandAll()
        {
            foreach (NodeViewItem one in this.NodeViewItems)
            {
                one.SetIsExpand(true);
                this.ExpandAll_DG(one);
            }
            this.SetIsExpand(true);
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
            this.SetIsExpand(false);
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

        #region IViewList
        WFNew.IFlexibleList IViewList.List
        {
            get { return this.NodeViewItems; }
        }
        #endregion

        #region IInputObject
        string IInputObject.InputText
        {
            get { return this.IInputObject_InputText; }
            set { this.IInputObject_InputText = value; }
        }
        internal protected virtual string IInputObject_InputText
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        Font IInputObject.InputFont
        {
            get { return this.IInputObject_InputFont; }
        }
        internal protected virtual Font IInputObject_InputFont
        {
            get { return base.Font; }
        }

        Color IInputObject.InputForeColor
        {
            get { return this.IInputObject_ForeColor; }
        }
        internal protected virtual Color IInputObject_ForeColor
        {
            get { return base.ForeColor; }
        }

        [Browsable(false), Description("���������ο򣨿ͻ������Σ�"), Category("����")]
        Rectangle IInputObject.InputRegionRectangle
        {
            get { return this.IInputObject_InputRegionRectangle; }
        }
        internal protected virtual Rectangle IInputObject_InputRegionRectangle
        {
            get
            {
                int iOffset = this.NodeOffset;
                if (this.ShowPlusMinus) iOffset += CONST_PLUSMINUSBUTTONSIZE + CONST_PTSPACE;
                //
                iOffset += this.OffsetX;
                //
                Rectangle rectangle = this.DisplayRectangle;
                return new Rectangle
                    (
                    rectangle.Left + iOffset,
                    (rectangle.Top + rectangle.Bottom - this.m_TextSize.Height) / 2,
                    this.m_TextSize.Width,
                    this.m_TextSize.Height
                    );
            }
        }

        [Browsable(false), Description("�Ƿ���������"), Category("״̬")]
        public bool IsInputing
        {
            get 
            {
                IInputObject pInputObject = this.TryGetNodeViewItemTree() as IInputObject;
                if (pInputObject == null) return false;
                return pInputObject.IsInputing; 
            }
        }
        #endregion

        #region INodeViewItem2
        NodeViewStyle m_eNodeViewStyle = NodeViewStyle.eNodeView;
        [Browsable(true), DefaultValue(typeof(NodeViewStyle), "eNodeView"), Description("�ڵ��������"), Category("���")]
        public NodeViewStyle eNodeViewStyle
        {
            get { return m_eNodeViewStyle; }
            set { m_eNodeViewStyle = value; }
        }

        bool m_SystemColor = true;
        [Browsable(true), DefaultValue(true), Description("ʹ��ϵͳ��Ⱦ"), Category("���")]
        public bool SystemColor
        {
            get { return m_SystemColor; }
            set { m_SystemColor = value; }
        }

        private Color m_TitleBorder = Color.FromArgb(174, 179, 185);
        [Browsable(true), Description("����������"), Category("���")]
        public Color TitleBorder
        {
            get { return m_TitleBorder; }
            set { m_TitleBorder = value; }
        }

        private Color m_TitleBackgroundBegin = Color.FromArgb(246, 251, 247);
        [Browsable(true), Description("������ʼɫ"), Category("���")]
        public Color TitleBackgroundBegin
        {
            get { return m_TitleBackgroundBegin; }
            set { m_TitleBackgroundBegin = value; }
        }

        private Color m_TitleBackgroundEnd = Color.FromArgb(214, 224, 234);
        [Browsable(true), Description("������ֹɫ"), Category("���")]
        public Color TitleBackgroundEnd
        {
            get { return m_TitleBackgroundEnd; }
            set { m_TitleBackgroundEnd = value; }
        }
        #endregion

        #region ICloneable
        public virtual object Clone()
        {
            NodeViewItem node = new NodeViewItem();
            node.CanEdit = this.CanEdit;
            node.Enabled = this.Enabled;
            node.Font = this.Font;
            node.ForeColor = this.ForeColor;
            node.Height = this.Height;
            node.IsExpanded = this.IsExpanded;
            node.Name = this.Name;
            foreach (NodeViewItem one in this.NodeViewItems)
            {
                node.NodeViewItems.Add(one.Clone() as NodeViewItem);
            }
            node.ShowLines = this.ShowLines;
            node.ShowNomalState = this.ShowNomalState;
            node.ShowPlusMinus = this.ShowPlusMinus;
            node.Tag = this.Tag;
            node.Text = this.Text;
            node.Visible = this.Visible;
            node.Width = this.Width;
            node.eNodeViewStyle = this.eNodeViewStyle;
            node.SystemColor = this.SystemColor;
            node.TitleBorder = this.TitleBorder;
            node.TitleBackgroundBegin = this.TitleBackgroundBegin;
            node.TitleBackgroundEnd = this.TitleBackgroundEnd;
            //
            return node;
        }
        #endregion

        public override BaseItemState eBaseItemState
        {
            get
            {
                if (!this.Enabled) return BaseItemState.eDisabled;
                return base.eBaseItemState;
            }
        }

        public override Size MeasureSize(Graphics g)
        {
            //int iOffset = this.NodeOffset;
            //if (this.ShowPlusMinus) iOffset += CONST_PLUSMINUSBUTTONSIZE + CONST_PTSPACE;
            ////
            //iOffset += this.OffsetX;
            int iOffset = this.NodeTextOffset;
            //
            SizeF size = g.MeasureString(this.Text, this.Font);
            return new Size(this.Width > 0 ? this.Width : iOffset + (int)size.Width + 1, this.Height > 0 ? this.Height : (int)size.Height + 1);
        }

        private Size m_TextSize = new Size(21, 21);
        protected override void OnDraw(System.Windows.Forms.PaintEventArgs e)
        {
            if (this.Text.Length <= 0)
            {
                this.m_TextSize = new Size(21, 21);
            }
            else
            {
                SizeF size = e.Graphics.MeasureString(this.Text, this.Font);
                this.m_TextSize = new Size((int)size.Width + 1, (int)size.Height + 1);
            }
            //
            #region ����������ע���벻Ҫɾ����
            //Rectangle rectangle = Rectangle.FromLTRB(e.ClipRectangle.Left, e.ClipRectangle.Top, e.ClipRectangle.Right - 1, e.ClipRectangle.Bottom - 1);
            //WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderNodeViewItem(new ObjectRenderEventArgs(e.Graphics, this, rectangle));
            //WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonText
            //    (
            //    new TextRenderEventArgs(
            //        e.Graphics,
            //        this,
            //        true,
            //        true,
            //        this.Text,
            //        this.ForeColor,
            //        this.Font,
            //        this.TextRectangle,
            //        new StringFormat() { Trimming = StringTrimming.EllipsisCharacter })
            //    );
            #endregion
            //
            #region ����������ע���벻Ҫɾ����
            //Rectangle rectangle = Rectangle.FromLTRB(e.ClipRectangle.Left, e.ClipRectangle.Top, e.ClipRectangle.Right - 1, e.ClipRectangle.Bottom - 1);
            //Rectangle textRectangle = this.TextRectangle;
            //int iW = textRectangle.Width;
            //if (this.Width >= 0 && textRectangle.X >= 0)
            //{
            //    iW = this.Width > rectangle.Width ? rectangle.Width : this.Width - this.NodeTextOffset;
            //    //rectangle = new Rectangle(rectangle.X, rectangle.Y, iW - (rectangle.X - this.DisplayRectangle.X), rectangle.Height);
            //}
            //WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderNodeViewItem(new ObjectRenderEventArgs(e.Graphics, this, rectangle));
            //rectangle = new Rectangle(textRectangle.X, textRectangle.Y, iW, textRectangle.Height);
            //WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonText
            //    (
            //    new TextRenderEventArgs(
            //        e.Graphics,
            //        this,
            //        true,
            //        true,
            //        this.Text,
            //        this.ForeColor,
            //        this.Font,
            //        rectangle,
            //        new StringFormat() { Trimming = StringTrimming.EllipsisCharacter })
            //    );
            #endregion
            //
            WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderNodeViewItem(new ObjectRenderEventArgs(
                e.Graphics, 
                this,
                Rectangle.FromLTRB(e.ClipRectangle.Left, e.ClipRectangle.Top, e.ClipRectangle.Right - 1, e.ClipRectangle.Bottom - 1)));
            if (String.IsNullOrEmpty(this.Text)) return;
            WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonText
                (
                new TextRenderEventArgs(
                    e.Graphics,
                    this,
                    true,
                    true,
                    this.Text,
                    false,
                    this.ForeColor,
                    this.ForeColor,
                    this.Font,
                    this.TextRectangle,
                    new StringFormat() { Trimming = StringTrimming.EllipsisCharacter })
                );
            //
            //base.OnDraw(e);
        }

        protected override void MessageMonitor(MessageInfo messageInfo)
        {
            switch (messageInfo.eMessageStyle)
            {
                case MessageStyle.eMSMouseDown:
                    //MessageBox.Show("0");
                    if (this.TriggerExpandCollapse(((System.Windows.Forms.MouseEventArgs)messageInfo.MessageParameter).Location))
                    {
                        //MessageBox.Show("1");
                        this.IsExpanded = !this.IsExpanded;
                    }
                    break;
                default:
                    base.MessageMonitor(messageInfo);
                    break;
            }
        }
        private bool TriggerExpandCollapse(Point point)
        {
            if (this.ShowPlusMinus)
            {
                if (this.PlusMinusRectangle.Contains(point)) return true;
            }
            else
            {
                if (this.CanEdit)
                {
                    if (!this.TextRectangle.Contains(point)) return true;
                }
                else
                {
                    return false;
                }
            }
            //
            return false;
        }
    }
}
