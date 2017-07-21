using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IBaseItem2 : IBaseItem, ICloneable
    {
        bool Checked { get; set; }

        bool Overflow { get; }

        bool LockHeight { get; }

        bool LockWith { get; }

        object Tag { get; set; }

        Control Parent { get; }

        IOwner pOwner { get; }
    }
}