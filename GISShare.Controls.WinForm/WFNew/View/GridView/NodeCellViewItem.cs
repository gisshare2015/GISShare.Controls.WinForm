using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;

namespace GISShare.Controls.WinForm.WFNew.View
{
    public class NodeCellViewItem : CellViewItem, INodeCellViewItem
    {
        public NodeCellViewItem(CellViewStyle cellViewStyle)
            : base(cellViewStyle)
        {

        }

        #region ISizeViewItem
        [Browsable(true), DefaultValue(-1), Description("宽度（小于零时，由系统操作）"), Category("布局")]
        public override int Width
        {
            get
            {
                if (this.eCellViewStyle == CellViewStyle.eSystem)
                {
                    IViewItemOwner2 pViewItemOwner2 = ((IOwner)this).pOwner as IViewItemOwner2;
                    if (pViewItemOwner2 == null) return base.Width;
                    IColumnViewObject pColumnViewObject = pViewItemOwner2.GetTopViewItemOwner() as IColumnViewObject;
                    if (pColumnViewObject == null) return base.Width;
                    int iIndex = ((IRowViewItem)pViewItemOwner2).List.IndexOf(this);
                    if (iIndex < 0) return base.Width;
                    IColumnViewItem pColumnViewItem = pColumnViewObject.ColumnViewItems[iIndex];
                    int iNodeTextOffset = 0;
                    if (this.StartCellViewItem)
                    {
                        if (pViewItemOwner2.pOwner is INodeViewItem)
                        {
                            iNodeTextOffset = ((INodeViewItem)pViewItemOwner2.pOwner).NodeTextOffset;
                        }
                    }
                    return pColumnViewItem == null ? base.Width : pColumnViewItem.Width - iNodeTextOffset;
                }
                else if (this.eCellViewStyle == CellViewStyle.eSingleCell)
                {
                    IViewItemOwner2 pViewItemOwner2 = ((IOwner)this).pOwner as IViewItemOwner2;
                    if (pViewItemOwner2 == null) return base.Width;
                    IColumnViewObject pColumnViewObject = pViewItemOwner2.GetTopViewItemOwner() as IColumnViewObject;
                    if (pColumnViewObject == null && pColumnViewObject.ColumnViewItems.Count <= 0) return base.Width;
                    int iW = 0;
                    foreach (ISizeViewItem one in pColumnViewObject.ColumnViewItems)
                    {
                        iW += one.Width;
                    }
                    return iW;
                }
                //
                return base.Width;
            }
            set
            {
                base.Width = value;
            }
        }
        #endregion

        #region INodeCellViewItem
        public bool StartCellViewItem
        {
            get
            {
                IRowViewItem pRowViewItem = ((IOwner)this).pOwner as IRowViewItem;
                if (pRowViewItem != null)
                {
                    foreach (ICellViewItem one in pRowViewItem.Items)
                    {
                        if (one.Visible)
                        {
                            return one == this;
                        }
                    }
                }
                //
                return false;
            }
        }
        #endregion
        
        protected override void OnDraw(System.Windows.Forms.PaintEventArgs e)
        {
            Rectangle rectangle = Rectangle.FromLTRB(e.ClipRectangle.Left, e.ClipRectangle.Top, e.ClipRectangle.Right - 1, e.ClipRectangle.Bottom - 1);
            WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderNodeCellViewItem
                (
                new ObjectRenderEventArgs(e.Graphics, this, rectangle)
                );
            //
            if (this.BaseItemObject != null) return;
            //
            if (String.IsNullOrEmpty(this.Text)) return;
            rectangle = this.DisplayRectangle;
            int iH = (int)e.Graphics.MeasureString(this.Text, this.Font).Height + 1;
            WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonText
                (
                new TextRenderEventArgs(
                    e.Graphics,
                    this,
                    true,
                    this.HaveShadow,
                    this.Text,
                    this.ForeCustomize,
                    this.ForeColor,
                    this.ShadowColor,
                    this.Font,
                    new Rectangle(rectangle.Left, (rectangle.Top + rectangle.Bottom - iH) / 2, rectangle.Width, iH),//rectangle.Height
                    new StringFormat() { Trimming = StringTrimming.EllipsisCharacter })
                );
        }
    }
}
