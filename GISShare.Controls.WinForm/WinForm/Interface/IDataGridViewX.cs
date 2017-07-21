using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm
{
    public interface IDataGridViewX : IFrameControlItem, WinForm.IItemOwner
    {
        bool AutoMouseMoveSeleced { get; set; }

        //
        DataGridViewCell CurrentCell { get; }
        DataGridViewRow CurrentRow { get; }
        DataGridViewColumnCollection Columns { get; }
        DataGridViewRowCollection Rows { get; }
        void InvalidateCell(DataGridViewCell dataGridViewCell);
        void InvalidateCell(int columnIndex, int rowIndex);
        void InvalidateColumn(int columnIndex);
        void InvalidateRow(int rowIndex);
    }
}
