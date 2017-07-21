using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace GISShare.Controls.WinForm.WFNew.View
{
    class LockRowColumnTitleViewItem : RowColumnTitleViewItem, IRowHeaderItem, IRowHeaderItemHelper
    {
        internal LockRowColumnTitleViewItem()
        {
            base.ShowBaseItemState = false;
        }

        #region IRowHeaderItem
        int m_RowIndex = -1;
        int IRowHeaderItem.RowIndex
        {
            get { return this.m_RowIndex; }
        }

        Rectangle m_RowHeaderRectangle;
        Rectangle IRowHeaderItem.RowHeaderRectangle
        {
            get { return this.m_RowHeaderRectangle; }
        }

        ViewParameterStyle IRowHeaderItem.eViewParameterStyle
        {
            get { return ((IViewItem)this).eViewParameterStyle; }
        }
        #endregion

        #region ISetRowHeaderItemHelper
        void IRowHeaderItemHelper.SetIndex(int index)
        {
            this.m_RowIndex = index;
        }

        void IRowHeaderItemHelper.SetRowHeaderRectangle(Rectangle rectangle)
        {
            this.m_RowHeaderRectangle = rectangle;
        }

        void IRowHeaderItemHelper.DrawRowHeader(System.Windows.Forms.PaintEventArgs e, bool ShowRowIndex, int iRowHeaderStartID)
        {
            WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRowHeaderItem
                        (
                        new ObjectRenderEventArgs(
                            e.Graphics, this, Rectangle.FromLTRB(e.ClipRectangle.Left, e.ClipRectangle.Top, e.ClipRectangle.Right - 1, e.ClipRectangle.Bottom - 1))
                        );
        }
        #endregion

    }
}
