using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm
{
    public interface IListBoxX : WFNew.IBaseItem, WinForm.IItemOwner
    {
        bool AutoMouseMoveSeleced { get; set; }

        int ImageSpace { get; set; }

        int ITSpace { get; set; }

        Rectangle FrameRectangle { get; }

        void InvalidateItem(int iIndex);

        //

        BorderStyle BorderStyle { get;  }

        int ColumnWidth { get;  }

        DrawMode DrawMode { get;  }

        int ItemHeight { get; }

        bool MultiColumn { get; }

        int SelectedIndex { get; }

        ListBox.SelectedIndexCollection SelectedIndices { get; }

        object SelectedItem { get; }

        SelectionMode SelectionMode { get; }

        ListBox.SelectedObjectCollection SelectedItems { get; }

        int TopIndex { get; }

        int IndexFromPoint(Point p);

        int IndexFromPoint(int x, int y);
    }
}
