using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IExpandableCaptionPanelButtonItem : WFNew.IBaseButtonItem
    {
        bool IsExpand { get; }

        bool UseMaxMinStyle { get; }

        ExpandButtonStyle eExpandButtonStyle { get; }

        TabAlignment CaptionAlignment { get; }

        ExpandableCaptionPanelButtonItemStyle eExpandableCaptionPanelButtonItemStyle { get;  }

        Rectangle GlyphRectangle { get; }
    }

    public enum ExpandableCaptionPanelButtonItemStyle
    {
        eCloseButton = 44,
        eExpandButton,
        eTreeNodeButton
    }
}
