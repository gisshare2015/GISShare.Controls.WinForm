using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew.View
{
    class DataRowNodeCellViewItem : RowNodeCellViewItem
    {
        internal DataRowNodeCellViewItem(RowCellViewStyle rowCellViewStyle, object objDataItem)
            : base(rowCellViewStyle)
        {
            this.m_DataItem = objDataItem;
        }

        object m_DataItem;
        public object DataItem
        {
            get { return m_DataItem; }
        }
    }
}
