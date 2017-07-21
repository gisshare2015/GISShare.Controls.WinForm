using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GISShare.Controls.WinForm
{
    public interface ITreeNodeItem : IImageItem
    {
        Rectangle Bounds { get; }

        bool Checked { get; }
    }
}
