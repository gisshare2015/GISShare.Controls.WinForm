using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    [Designer(typeof(GISShare.Controls.WinForm.WFNew.Design.CollectionItemDesigner))]
    public class DescriptionMenuPopup : BasePopup, ISimplyPopup, ICollectionItem, ICollectionItem2, ICollectionObjectDesignHelper
    {
        private DescriptionMenuPopupPanelItem m_DescriptionMenuPopupPanel = null;
        private ToolStripControlHost m_ToolStripControlHost = null;

        public DescriptionMenuPopup()
        {
            this.m_DescriptionMenuPopupPanel = new DescriptionMenuPopupPanelItem();
            this.m_DescriptionMenuPopupPanel.Padding = new Padding(2);
            this.m_DescriptionMenuPopupPanel.LeftTopRadius = 5;
            this.m_DescriptionMenuPopupPanel.LeftBottomRadius = 6;
            this.m_DescriptionMenuPopupPanel.RightTopRadius = 6;
            this.m_DescriptionMenuPopupPanel.RightBottomRadius = 7;
            this.m_DescriptionMenuPopupPanel.IsStretchItems = true;
            this.m_DescriptionMenuPopupPanel.IsRestrictItems = true;
            this.m_DescriptionMenuPopupPanel.PreButtonIncreaseIndex = false;
            this.m_DescriptionMenuPopupPanel.RestrictItemsHeight = 56;
            this.m_DescriptionMenuPopupPanel.ShowOutLine = true;
            this.m_DescriptionMenuPopupPanel.MinSize = 10;
            this.m_DescriptionMenuPopupPanel.MaxSize = 20;
            //
            this.m_ToolStripControlHost = new ToolStripControlHost(new BaseItemHost(this.m_DescriptionMenuPopupPanel) {
                Width = 10,
                Height = 20
            });
            this.m_DescriptionMenuPopupPanel.Entity = this.m_ToolStripControlHost.Control;
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

        #region ICollectionObjectDesignHelper
        System.Collections.IList ICollectionObjectDesignHelper.List { get { return this.BaseItems; } }

        bool ICollectionObjectDesignHelper.ExchangeItem(object item1, object item2) { return this.BaseItems.ExchangeItem(item1, item2); }
        #endregion

        public new BaseItemCollection Items
        {
            get { return this.m_DescriptionMenuPopupPanel.BaseItems; }
        }

        #region ICollectionItem
        [Browsable(false), Description("其所携带的子项集合中是否存在可见项"), Category("状态")]
        public bool HaveVisibleBaseItem
        {
            get
            {
                foreach (BaseItem one in this.m_DescriptionMenuPopupPanel.BaseItems)
                {
                    if (one.Visible) return true;
                }
                //
                return false;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Description("其所携带的子项集合"), Category("子项")]
        public BaseItemCollection BaseItems
        {
            get { return this.m_DescriptionMenuPopupPanel.BaseItems; }
        }
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

        public Size GetIdealSize()
        {
            //return this.m_DescriptionMenuPopupPanel.Size;
            Graphics g = Graphics.FromHwnd(this.m_ToolStripControlHost.Control.Handle);
            Size size = this.m_DescriptionMenuPopupPanel.GetIdealSize(g);
            g.Dispose();
            //
            return size;
        }

        public Padding GetPadding()
        {
            return this.m_DescriptionMenuPopupPanel.Padding;
        }

        public void SetPadding(int iPadding)
        {
            this.m_DescriptionMenuPopupPanel.Padding = new Padding(iPadding);
        }

        public void SetPadding(int left, int top, int right, int bottom)
        {
            this.m_DescriptionMenuPopupPanel.Padding = new Padding(left, top, right, bottom);
        }

        public void SetRadius(int iRadius)
        {
            this.m_DescriptionMenuPopupPanel.LeftTopRadius = iRadius;
            this.m_DescriptionMenuPopupPanel.LeftBottomRadius = iRadius;
            this.m_DescriptionMenuPopupPanel.RightTopRadius = iRadius;
            this.m_DescriptionMenuPopupPanel.RightBottomRadius = iRadius;
        }

        public void SetRadius(int iLeftTopRadius, int iLeftBottomRadius, int iRightTopRadius, int iRightBottomRadius)
        {
            this.m_DescriptionMenuPopupPanel.LeftTopRadius = iLeftTopRadius;
            this.m_DescriptionMenuPopupPanel.LeftBottomRadius = iLeftBottomRadius;
            this.m_DescriptionMenuPopupPanel.RightTopRadius = iRightTopRadius;
            this.m_DescriptionMenuPopupPanel.RightBottomRadius = iRightBottomRadius;
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
            this.m_ToolStripControlHost.Control.Size = size;
        }

        public void RefreshPopupPanel()
        {
            this.m_DescriptionMenuPopupPanel.Refresh();
        }

        public IPopupPanel GetPopupPanel()
        {
            return this.m_DescriptionMenuPopupPanel;
        }

        //public int VisibleBaseItemCount
        //{
        //    get
        //    {
        //        int iNum = 0;
        //        foreach (BaseItem one in this.m_DescriptionMenuPopupPanel.BaseItems)
        //        {
        //            if (one.Visible) iNum++;
        //        }
        //        return iNum;
        //    }
        //}

        #region Clone
        public override object Clone()
        {           
            return null;
        }
        #endregion

        #region Radius
        public override int LeftTopRadius { get { return m_DescriptionMenuPopupPanel.LeftTopRadius; } }

        public override int RightTopRadius { get { return m_DescriptionMenuPopupPanel.RightTopRadius; } }

        public override int LeftBottomRadius { get { return m_DescriptionMenuPopupPanel.LeftBottomRadius; } }

        public override int RightBottomRadius { get { return m_DescriptionMenuPopupPanel.RightBottomRadius; } }
        #endregion

        protected override bool Filtration()
        {
            if (this.HaveVisibleBaseItem) 
            {
                this.m_DescriptionMenuPopupPanel.TopViewItemIndex = 0;
                this.m_DescriptionMenuPopupPanel.MaxSize = System.Windows.Forms.SystemInformation.WorkingArea.Height;
                return true; 
            }
            else
            {
                return false; 
            }
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
            IButtonItem pButtonItem = this.pOwner as IButtonItem;
            if (pButtonItem != null)
            {
                if (pButtonItem.eButtonStyle == ButtonStyle.eSplitButton) return pButtonItem.SplitRectangle.Contains(this.pOwner.PointToClient(point));
                else return this.pOwner.DisplayRectangle.Contains(this.pOwner.PointToClient(point));
            }
            else
            {
                ISplitButtonItem pSplitButtonItem = this.pOwner as ISplitButtonItem;
                if (pSplitButtonItem != null) return this.pOwner.DisplayRectangle.Contains(this.pOwner.PointToClient(point));
                else this.pOwner.DisplayRectangle.Contains(this.pOwner.PointToClient(point));
            }
            //
            return false;
        }

    }
}
