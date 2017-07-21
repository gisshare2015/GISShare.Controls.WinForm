using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.DockBar
{
    internal interface ICustomize
    {
        //int RecordID { get; }

        string Name { get; }

        string Text { get; set; }

        FlexibleToolStripItemCollection Items { get; }

        List<ToolStripItem> CustomizeBaseItems { get; }

        IBaseItemDB AddCustomizeBaseItem(int index, IBaseItemDB pBaseItem);

        IBaseItemDB AddCustomizeBaseItemEx(int index, IBaseItemDB pBaseItem);

        void RemoveCustomizeBaseItem(IBaseItemDB pBaseItem);

        void ClearCustomizeBaseItems();

        void Reset();
    }
}
