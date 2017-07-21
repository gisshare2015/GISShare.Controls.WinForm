using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IExpandableCaptionPanel : WFNew.IBaseItem, WFNew.IArea
    {
        bool bActive { get; }

        bool IsExpand { get; }

        bool AutoSetIsExpand { get;}

        bool HaveExpandButton { get; }

        bool UseRadius { get; }
        int LeftTopRadius { get; }
        int RightTopRadius { get; }
        int LeftBottomRadius { get; }
        int RightBottomRadius { get; }

        bool ShowCaption { get; set; }

        bool ShowCloseButton { get; set; }
        bool ShowExpandButton { get; set; }
        bool ShowTreeNodeButton { get; set; }
        //bool ShowFrame { get; set; }
        bool UseMaxMinStyle { get; set; }
        bool DisplayCaptionState { get; set; }
        bool IsSimplyDrawCaption { get; set; }
        int CaptionHeight { get; set; }
        Image Image { get; set; }
        //Size ExpandSize { get; set; }
        BaseItemState eCaptionState { get; }

        TabAlignment eCaptionAlignment { get; }

        ExpandButtonStyle eExpandButtonStyle { get; }

        //Rectangle FrameRectangle { get; }

        Rectangle CaptionRectangle { get; }

        Rectangle ImageRectangle { get;}

        Rectangle TitleRectangle { get;}

        //Rectangle RedrawExpandRectangle { get; }

        Rectangle GetTreeNodeButtonRectangle();
        Rectangle GetExpandButtonRectangle();
        Rectangle GetCloseButtonRectangle();
    }
}
