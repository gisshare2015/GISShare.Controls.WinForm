using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew.View
{
    class DataRowCellViewItem : RowCellViewItem, IDataRowCellViewItem
    {
        internal DataRowCellViewItem(RowCellViewStyle rowCellViewStyle, object objDataItem)
            : base(rowCellViewStyle)
        {
            this.m_DataItem = objDataItem;
        }

        #region IDataRowCellViewItem
        object m_DataItem;
        public object DataItem
        {
            get { return m_DataItem; }
        }
        #endregion
    }
}
