using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm
{
    public interface ITreeViewX : IFrameControlItem, WinForm.IItemOwner
    {
        int ItemHeight { get; }
        bool ShowLines { get; }
        bool ShowPlusMinus { get; }
        bool ShowRootLines { get; }
        bool CheckBoxes { get; }
        TreeViewDrawMode DrawMode { get; }
        TreeNode TopNode { get; }
        TreeNode SelectedNode { get; }
        ImageList ImageList { get; }
        ImageList StateImageList { get; }
        //

        int ImageSpace { get; set; }

        bool AutoMouseMoveSeleced { get; set; }

        NodeRegionStyle eNodeRegionStyle { get; set; }

        int PlusMinusRegionWidth { get; }

    }
}
