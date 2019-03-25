using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew.Forms
{
    class NCCustomizeButtonItem : WFNew.DropDownButtonItem, INCBaseItem, IViewDepend
    {
        private WFNew.IQuickAccessToolbarItem owner;

        public NCCustomizeButtonItem(WFNew.IQuickAccessToolbarItem pQuickAccessToolbarItem)
            : base()
        {
            this.UsingViewOverflow = false;
            this.ShowNomalState = false;
            this.owner = pQuickAccessToolbarItem;
            ((WFNew.ISetOwnerHelper)this).SetOwner(owner as WFNew.IOwner);
            //
            this.SetPopupPadding(2, 2, 2, 2);
            WFNew.IPanelPopup pPanelPopup = ((WFNew.IPopupOwnerHelper)this).GetBasePopup() as WFNew.IPanelPopup;
            if (pPanelPopup != null)
            {
                WFNew.ContextPopupPanelItem pContextPopupPanel = pPanelPopup.GetPopupPanel() as WFNew.ContextPopupPanelItem;
                if (pContextPopupPanel != null) pContextPopupPanel.eContextPopupStyle = GISShare.Controls.WinForm.WFNew.ContextPopupStyle.eSuper;
            }
            //this.setr
            //
            WFNew.LabelSeparatorItem ribbonLabelSeparatorItem = new WFNew.LabelSeparatorItem();
            ribbonLabelSeparatorItem.Name = "自定义...";
            ribbonLabelSeparatorItem.Text = "自定义...";
            ribbonLabelSeparatorItem.Size = new Size(160, 23);
            ribbonLabelSeparatorItem.TextAlign = ContentAlignment.MiddleCenter;
            this.BaseItems.Add(ribbonLabelSeparatorItem);
        }

        #region IOffsetNC
        public int NCOffsetX
        {
            get
            {
                IOffsetNC pOffsetNC = this.pOwner as IOffsetNC;
                if (pOffsetNC == null) return -1;
                return pOffsetNC.NCOffsetX;
            }
        }

        public int NCOffsetY
        {
            get
            {
                IOffsetNC pOffsetNC = this.pOwner as IOffsetNC;
                if (pOffsetNC == null) return -1;
                return pOffsetNC.NCOffsetY;
            }
        }
        #endregion

        #region INCBaseItem
        public IBaseItemOwnerNC GetTopBaseItemOwnerNC()
        {
            return this.TryGetDependControl() as IBaseItemOwnerNC;
        }
        #endregion

        ViewDependStyle IViewDepend.eViewDependStyle
        {
            get
            {
                return ViewDependStyle.eInOwnerDisplayRectangle;
            }
        }

        public override void Refresh()
        {
            IBaseItemOwnerNC pBaseItemOwnerNC = this.pOwner as IBaseItemOwnerNC;
            if (pBaseItemOwnerNC != null)
            {
                pBaseItemOwnerNC.RefreshNC();
            }
            else
            {
                base.Refresh();
            }
        }

        public override bool Enabled
        {
            get
            {
                return this.owner.Enabled;
            }
        }

        public override bool Overflow
        {
            get
            {
                return !this.owner.DisplayRectangle.Contains(this.DisplayRectangle);
            }
        }

        public override int LeftTopRadius
        {
            get
            {
                return this.owner.LeftTopRadius;
            }
            set { }
        }

        public override int RightTopRadius
        {
            get
            {
                return this.owner.RightTopRadius;
            }
            set { }
        }

        public override int LeftBottomRadius
        {
            get
            {
                return this.owner.LeftBottomRadius;
            }
            set { }
        }

        public override int RightBottomRadius
        {
            get
            {
                return this.owner.RightBottomRadius;
            }
            set { }
        }

        public override Rectangle DisplayRectangle
        {
            get
            {
                return this.owner.CustomizeButtonRectangle;
            }
        }

        //public override Point PopupLoction
        //{
        //    get
        //    {
        //        IOffsetNC pOffsetNC = this.pOwner as IOffsetNC;
        //        if (pOffsetNC == null) return base.PopupLoction;
        //        //
        //        Point point = base.PopupLoction;
        //        return new Point(point.X + pOffsetNC.NCOffsetX, point.Y + pOffsetNC.NCOffsetY);
        //    }
        //}

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            this.ClearBaseItems();
            //
            foreach (WFNew.BaseItem one in this.owner.BaseItems)
            {
                ISeparatorItem pSeparatorItem = one as ISeparatorItem;
                if (pSeparatorItem != null && pSeparatorItem.AutoLayout)
                {
                    this.BaseItems.Add(new GISShare.Controls.WinForm.WFNew.SeparatorItem());
                }
                else
                {
                    WFNew.CheckButtonItem baseItem = new GISShare.Controls.WinForm.WFNew.CheckButtonItem();
                    baseItem.Name = one.Name;
                    baseItem.Text = (one.Text == null || one.Text.Length <= 0) ? GISShare.Controls.WinForm.WFNew.Language.LanguageStrategy.CustomizeItemNotNamedText : one.Text;
                    baseItem.Checked = one.Visible;
                    baseItem.Tag = one;
                    if (one is WFNew.IBaseButtonItem) baseItem.Image = ((WFNew.IBaseButtonItem)one).Image;
                    baseItem.CheckedChanged += new EventHandler(BaseItem_CheckedChanged);
                    this.BaseItems.Add(baseItem);
                }
            }
            //
            base.OnMouseDown(mevent);
        }
        void BaseItem_CheckedChanged(object sender, EventArgs e)
        {
            WFNew.BaseItem baseItem = sender as WFNew.BaseItem;
            if (baseItem != null)
            {
                WFNew.BaseItem item = baseItem.Tag as WFNew.BaseItem;
                if (item != null)
                {
                    item.Visible = baseItem.Checked;
                    for (int i = 0; i < 4; i++)
                    {
                        this.pOwner.Refresh();
                    }
                }
            }
        }
        private void ClearBaseItems()
        {
            for (int i = this.BaseItems.Count - 1; i > 0; i--)
            {
                this.BaseItems[i].CheckedChanged -= new EventHandler(BaseItem_CheckedChanged);
                this.BaseItems[i].Dispose();
                this.BaseItems.RemoveAt(i);
            }
        }

        protected override void OnDraw(PaintEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderCustomizeButton(
                   new GISShare.Controls.WinForm.ObjectRenderEventArgs(e.Graphics, this, this.DisplayRectangle));
        }
    }
}
