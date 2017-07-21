using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace GISShare.Controls.WinForm.WFNew.View
{
    public interface IGridViewItemListBox
    {
        bool ShowRowHeaderID { get; set; }

        int RowHeaderStartID { get; set; }

        int RowHeaderWidth { get; set; }

        bool ShowRowHeader { get; set; }

        bool CanResizeRowHeaderWidth { get; set; }

        bool CanResizeRowHeaderHeight { get; set; }

        Rectangle RowHeaderRectangle { get; }

        int FocusRowIndex { get; }

        int FocusColumnIndex { get; }

        int RowCount { get; }
        
        void SetColumnViewItemWidth(int index, int iColumnWidth);
        void AddRowViewItem(RowCellViewStyle eRowCellViewStyle, params object[] argsobj);
        void AddRowViewItem(RowCellViewStyle eRowCellViewStyle, int iRowHeight, params object[] argsobj);
        void InsertRowViewItem(int index, RowCellViewStyle eRowCellViewStyle, params object[] argsobj);
        void InsertRowViewItem(int index, RowCellViewStyle eRowCellViewStyle, int iRowHeight, params object[] argsobj);
        void DeletetRowViewItem(int index);
        void ClearRowViewItem();
        bool ExchangeRowViewItem(int index1, int index2);
        void SetRowViewItemHeight(int index, int iRowHeight);
        void SetRowViewItem(int iRowIndex, string strPropertyName, object objValue);
        int GetRowViewItemHeight(int index);
        object GetRowViewItem(int iRowIndex, string strPropertyName);
        ICellViewItem GetCellViewItem(int iRow, int iColumn);
        ICellViewItem GetFocusCellViewItem();
    }
}
