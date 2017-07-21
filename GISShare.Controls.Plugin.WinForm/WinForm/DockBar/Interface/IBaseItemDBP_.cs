using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace GISShare.Controls.Plugin.WinForm.DockBar
{
    public interface IBaseItemDBP_ : IPlugin, IPlugin2
    {
        ToolStripItemDisplayStyle DisplayStyle { get;  }
        bool DoubleClickEnabled { get; }
        bool Enabled { get; }
        Font Font { get; }
        Color ForeColor { get; }
        Image Image { get; }
        ContentAlignment ImageAlign { get; }
        //int ImageIndex { get; }
        //string ImageKey { get; }
        ToolStripItemImageScaling ImageScaling { get; }
        Color ImageTransparentColor { get; }
        Padding Margin { get; }
        MergeAction MergeAction { get; }
        int MergeIndex { get; }
        ToolStripItemOverflow Overflow { get; }
        Padding Padding { get; }
        RightToLeft RightToLeft { get; }
        bool RightToLeftAutoMirrorImage { get; }
        Size Size { get; }
        string Text { get; }
        ContentAlignment TextAlign { get; }
        ToolStripTextDirection TextDirection { get; }
        TextImageRelation TextImageRelation { get; }
        string ToolTipText { get; }
        bool Visible { get; }
        //
        string Category { get; }
    }
}
