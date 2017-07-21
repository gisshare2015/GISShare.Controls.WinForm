using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.DockBar
{
    public interface ICollectionItemDB
    {
        System.Windows.Forms.ToolStripItem GetItem(string strName);
    }
}
