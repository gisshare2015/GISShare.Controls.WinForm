using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew.DockPanel
{
    public interface IBaseNode
    {
        //int RecordID { get; }

        string Name { get; }

        string Text { get; }

        string Describe { get; }

        NodeStyle eNodeStyle { get; }
    }
}
