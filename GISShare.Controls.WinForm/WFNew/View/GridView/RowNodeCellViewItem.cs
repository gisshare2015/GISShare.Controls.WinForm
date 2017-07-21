using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;

namespace GISShare.Controls.WinForm.WFNew.View
{
    public class RowNodeCellViewItem : RowImageNodeViewItem,
        IRowCellViewItem,
        IRowHeaderItem, IRowHeaderItemHelper,
        IRowNodeCellViewItem
    {
        private const int CONST_MINNODEWIDTH = 18;
        private const int CONST_MINNODEHEIGHT = 18;
        private const int CONST_ROWIDOFFSET_X = 5;

        internal RowNodeCellViewItem(RowCellViewStyle rowCellViewStyle)
            : base()
        {
            this.m_eRowCellViewStyle = rowCellViewStyle;
        }

        #region IRowCellViewItem
        RowCellViewStyle m_eRowCellViewStyle = RowCellViewStyle.eSystemRow;
        [Browsable(false), DefaultValue(typeof(RowCellViewStyle), "eSystemRow"), Description("行类型"), Category("属性")]
        public RowCellViewStyle eRowCellViewStyle
        {
            get { return m_eRowCellViewStyle; }
        }

        public bool ExchangeCellViewItem(int index1, int index2) { return this.ViewItems.ExchangeItem(index1, index2); }
        #endregion

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
            //
            if (ShowRowIndex)
            {
                string strID =( this.m_RowIndex + iRowHeaderStartID ) .ToString();
                SizeF size = e.Graphics.MeasureString(strID, this.Font);
                int iH = (int)size.Height + 1;
                int iW = (int)size.Width + 1;
                WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonText
                    (
                    new TextRenderEventArgs(
                        e.Graphics,
                        this,
                        true,
                        false,
                        strID,
                        this.ForeColor,
                        this.Font,
                        new Rectangle(((IViewItem)this).eViewParameterStyle == ViewParameterStyle.eFocused ? CONST_ROWIDOFFSET_X + (e.ClipRectangle.Left + e.ClipRectangle.Right - iW) / 2 : (e.ClipRectangle.Left + e.ClipRectangle.Right - iW) / 2, (e.ClipRectangle.Top + e.ClipRectangle.Bottom - iH) / 2, iW, iH),
                        new StringFormat() { Trimming = StringTrimming.EllipsisCharacter })
                    );
            }
        }
        #endregion

        #region IRowNodeCellViewItem
        IFlexibleList IRowViewItem.Items { get { return this.ViewItems; } }
        #endregion

        protected override void OnDraw(System.Windows.Forms.PaintEventArgs e)
        {
            if (this.ViewItems.Count > 0)
            {
                WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRowNodeCellViewItem(
                    new ObjectRenderEventArgs(e.Graphics, this,
                        Rectangle.FromLTRB(e.ClipRectangle.Left, e.ClipRectangle.Top, e.ClipRectangle.Right - 1, e.ClipRectangle.Bottom - 1)
                        ));
            }
            else
            {
                base.OnDraw(e);
            }
        }
    }
}
