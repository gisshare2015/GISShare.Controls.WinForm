using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm
{
    public interface IListViewX : IFrameControlItem, WinForm.IItemOwner
    {
        bool CheckBoxes { get; }
        View View { get; }
        ImageList LargeImageList { get; }
        ImageList SmallImageList { get; }
        ImageList StateImageList { get; }
        ListViewItem GetItemAt(int x, int y);
        //

        int ImageSpace { get; set; }

        bool AutoMouseMoveSeleced { get; set; }

        bool AutoDrawSubItem { get; set; }

        bool HaveEndHeader { get; }

        int ColumnHeaderHeight { get; }

        Rectangle EndHeaderRectangle { get; }

        ListViewItem GetItemAtEx(int x, int y);

        void InvalidateItem(ListViewItem item);
    }
}
