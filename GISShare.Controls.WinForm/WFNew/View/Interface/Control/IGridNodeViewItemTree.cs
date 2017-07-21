using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace GISShare.Controls.WinForm.WFNew.View
{
    public interface IGridNodeViewItemTree : INodeViewItemTree
    {
        bool ShowRowHeaderID { get; set; }

        int RowHeaderStartID { get; set; }

        int RowHeaderWidth { get; set; }

        bool ShowRowHeader { get; set; }

        bool CanResizeRowHeaderWidth { get; set; }

        bool CanResizeRowHeaderHeight { get; set; }

        Rectangle RowHeaderRectangle { get; }

        int FocusColumnIndex { get; }

        void SetColumnViewItemWidth(int index, int iColumnWidth);
        IRowNodeCellViewItem AddRowViewItem(IRowNodeCellViewItem parentNode, RowCellViewStyle eRowCellViewStyle, params object[] argsobj);
        IRowNodeCellViewItem AddRowViewItem(IRowNodeCellViewItem parentNode, RowCellViewStyle eRowCellViewStyle, int iRowHeight, params object[] argsobj);
        IRowNodeCellViewItem InsertRowViewItem(IRowNodeCellViewItem parentNode, int index, RowCellViewStyle eRowCellViewStyle, params object[] argsobj);
        IRowNodeCellViewItem InsertRowViewItem(IRowNodeCellViewItem parentNode, int index, RowCellViewStyle eRowCellViewStyle, int iRowHeight, params object[] argsobj);
        void DeletetRowViewItem(IRowNodeCellViewItem viewItem);
        void ClearRowViewItem();
        void SetRowViewItemHeight(IRowNodeCellViewItem viewItem, int iRowHeight);
        void SetRowViewItem(IRowNodeCellViewItem viewItem, string strPropertyName, object objValue);
        int GetRowViewItemHeight(IRowNodeCellViewItem viewItem);
        object GetRowViewItem(IRowNodeCellViewItem viewItem, string strPropertyName);
        ICellViewItem GetCellViewItem(IRowNodeCellViewItem viewItem, int iColumn);
        ICellViewItem GetFocusCellViewItem();
    }
}
