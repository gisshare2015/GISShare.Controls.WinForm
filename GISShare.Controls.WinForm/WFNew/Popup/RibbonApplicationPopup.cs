using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    [Designer(typeof(GISShare.Controls.WinForm.WFNew.Design.CollectionItemDesigner))]
    public class RibbonApplicationPopup : BasePopup, ICollectionItem, ICollectionItem2, IApplicationPopup, ICollectionObjectDesignHelper, IRibbonApplicationObjectDesignHelper
    {
        private RibbonApplicationPopupPanelItem m_RibbonApplicationPopupPanel = null;
        private ToolStripControlHost m_ToolStripControlHost = null;

        public RibbonApplicationPopup()
        {
            this.m_RibbonApplicationPopupPanel = new RibbonApplicationPopupPanelItem();
            this.m_RibbonApplicationPopupPanel.Padding = new Padding(2);
            this.m_RibbonApplicationPopupPanel.LeftTopRadius = 5;
            this.m_RibbonApplicationPopupPanel.LeftBottomRadius = 6;
            this.m_RibbonApplicationPopupPanel.RightTopRadius = 6;
            this.m_RibbonApplicationPopupPanel.RightBottomRadius = 7;
            //
            this.m_ToolStripControlHost = new ToolStripControlHost(new BaseItemHost(this.m_RibbonApplicationPopupPanel) { Width = 10, Height = 20 });
            this.m_RibbonApplicationPopupPanel.Entity = this.m_ToolStripControlHost.Control;
            this.m_ToolStripControlHost.Dock = DockStyle.Fill;
            this.m_ToolStripControlHost.BackColor = base.BackColor;
            this.m_ToolStripControlHost.Margin = new Padding(0);
            this.m_ToolStripControlHost.Padding = new Padding(0);
            base.Items.Add(this.m_ToolStripControlHost);
            //
            this.Margin = new Padding(0);
            this.Padding = new Padding(0);
            this.DropShadowEnabled = false;
            this.ShowItemToolTips = false;
            //
            ((ISetOwnerHelper)(this.m_ToolStripControlHost.Control)).SetOwner(this);
        }

        [Browsable(false), 
        DefaultValue(false), 
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), 
        Description("��ʾ�������ʾ��ǩ"), Category("��Ϊ")]
        public new bool ShowItemToolTips
        {
            get { return base.ShowItemToolTips; }
            set { base.ShowItemToolTips = value; }
        }

        [Browsable(false), 
        DefaultValue(typeof(Size), "10, 20"), 
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), 
        Description("�ߴ�"), 
        Category("����")]
        public new Size Size
        {
            get { return base.Size; }
            set { base.Size = value; }
        }

        [Browsable(false), 
        DefaultValue(typeof(Padding), "0"), 
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), 
        Description("��ȡ�����ÿؼ��ڵĿհ�"), 
        Category("����")]
        public new Padding Padding
        {
            get { return base.Padding; }
            set { base.Padding = value; }
        }

        [Browsable(false), 
        DefaultValue(false), 
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), 
        Description("�Ƿ������ά��ӰЧ��"), 
        Category("״��")]
        public new bool DropShadowEnabled
        {
            get { return base.DropShadowEnabled; }
            set { base.DropShadowEnabled = value; }
        }

        [Browsable(false),
        DefaultValue(""),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        Description("����"),
        Category("����")]
        public new string Name
        {
            get { return base.Name; }
            set { base.Name = value; }
        }

        #region ICollectionObjectDesignHelper
        System.Collections.IList ICollectionObjectDesignHelper.List { get { return ((ICollectionItem)this).BaseItems; } }

        bool ICollectionObjectDesignHelper.ExchangeItem(object item1, object item2) { return false; }
        #endregion

        #region Clone
        public override object Clone()
        {
            return null;
        }
        #endregion

        #region IBasePopup2
        public override int LeftTopRadius { get { return m_RibbonApplicationPopupPanel.LeftTopRadius; } }

        public override int RightTopRadius { get { return m_RibbonApplicationPopupPanel.RightTopRadius; } }

        public override int LeftBottomRadius { get { return m_RibbonApplicationPopupPanel.LeftBottomRadius; } }

        public override int RightBottomRadius { get { return m_RibbonApplicationPopupPanel.RightBottomRadius; } }
        #endregion

        #region ICollectionItem
        public bool HaveVisibleBaseItem
        {
            get
            {
                foreach (BaseItem one in this.m_RibbonApplicationPopupPanel.MenuItems)
                {
                    if (one.Visible) return true;
                }
                //
                return false;
            }
        }

        [Browsable(false), Description("��Я�������һ����ɢ���齨���ϣ������������޷��Ƴ�����ӣ�û����Ҫ�벻Ҫ�޸��ڲ���Ա�����Է��������������"), Category("����")]
        BaseItemCollection ICollectionItem.BaseItems
        {
            get { return ((ICollectionItem)this.m_RibbonApplicationPopupPanel).BaseItems; }
        }
        #endregion

        #region ICollectionItem2
        IBaseItem ICollectionItem2.GetBaseItem(string strName)
        {
            IBaseItem pBaseItem = null;
            foreach (IBaseItem one in ((ICollectionItem)this).BaseItems)
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

        #region IApplicationPopup
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Description("�˵���Я���������"), Category("����")]
        public BaseItemCollection MenuItems
        {
            get { return this.m_RibbonApplicationPopupPanel.MenuItems; }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Description("��¼��Я���������"), Category("����")]
        public BaseItemCollection RecordItems
        {
            get { return this.m_RibbonApplicationPopupPanel.RecordItems; }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Description("������ť��Я���������"), Category("����")]
        public BaseItemCollection OperationItems
        {
            get { return this.m_RibbonApplicationPopupPanel.OperationItems; }
        }

        public Size GetIdealSize()
        {
            Graphics g = Graphics.FromHwnd(this.m_ToolStripControlHost.Control.Handle);
            Size size = this.m_RibbonApplicationPopupPanel.GetIdealSize(g);
            g.Dispose();
            //
            return size;
        }

        public Padding GetPadding()
        {
            return this.m_RibbonApplicationPopupPanel.Padding;
        }

        public void SetPadding(int iPadding)
        {
            this.m_RibbonApplicationPopupPanel.Padding = new Padding(iPadding);
        }

        public void SetPadding(int left, int top, int right, int bottom)
        {
            this.m_RibbonApplicationPopupPanel.Padding = new Padding(left, top, right, bottom);
        }

        public void SetRadius(int iRadius)
        {
            this.m_RibbonApplicationPopupPanel.LeftTopRadius = iRadius;
            this.m_RibbonApplicationPopupPanel.LeftBottomRadius = iRadius;
            this.m_RibbonApplicationPopupPanel.RightTopRadius = iRadius;
            this.m_RibbonApplicationPopupPanel.RightBottomRadius = iRadius;
        }

        public void SetRadius(int iLeftTopRadius, int iLeftBottomRadius, int iRightTopRadius, int iRightBottomRadius)
        {
            this.m_RibbonApplicationPopupPanel.LeftTopRadius = iLeftTopRadius;
            this.m_RibbonApplicationPopupPanel.LeftBottomRadius = iLeftBottomRadius;
            this.m_RibbonApplicationPopupPanel.RightTopRadius = iRightTopRadius;
            this.m_RibbonApplicationPopupPanel.RightBottomRadius = iRightBottomRadius;
        }

        public void TrySetPopupPanelSize(Size size)
        {
            this.m_ToolStripControlHost.Size =
                new Size
                (
                this.m_ToolStripControlHost.Padding.Left + size.Width + this.m_ToolStripControlHost.Padding.Right,
                this.m_ToolStripControlHost.Padding.Top + size.Height + this.m_ToolStripControlHost.Padding.Bottom
                );
            this.Size =
                new Size
                (
                this.Padding.Left + size.Width + this.Padding.Right,
                this.Padding.Top + size.Height + this.Padding.Bottom
                );
            //this.m_RibbonApplicationPopupPanel.Size = size;
        }

        public void RefreshPopupPanel()
        {
            this.m_RibbonApplicationPopupPanel.Refresh();
        }

        public IPopupPanel GetPopupPanel()
        {
            return this.m_RibbonApplicationPopupPanel;
        }
        #endregion
        
        protected override bool Filtration()
        {
            foreach (BaseItem one in this.m_RibbonApplicationPopupPanel.MenuItems)
            {
                if (one.Visible) return true;
            }
            return false;
        }

        public override bool CustomFiltration
        {
            get
            {
                return true;
            }
        }

        public override bool Filtration(MouseEventArgs e)
        {
            return this.OwnerContainMousePoint(e.Location) || base.Filtration(e);
        }
        private bool OwnerContainMousePoint(Point point)
        {
            if (this.pOwner == null) return false;
            //
            IPopupOwner pPopupOwner = this.pOwner as IPopupOwner;
            if (pPopupOwner != null) return pPopupOwner.PopupTriggerRectangle.Contains(this.pOwner.PointToClient(point));
            else return this.pOwner.DisplayRectangle.Contains(this.pOwner.PointToClient(point));
        }
    }
}
