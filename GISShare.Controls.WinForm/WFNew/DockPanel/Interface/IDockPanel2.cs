using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew.DockPanel
{
    public interface IDockPanel2 : WFNew.ITabControl, WFNew.IBaseItem, IDockPanel, IMultipleNode
    {
        event BoolValueChangedEventHandler DockPanelActiveChanged; 

        int BasePanelSelectedIndex { get; set; }

        bool bActive { get; }

        bool IsHideState { get; }

        Image Image { get; }

        BasePanel SelectedBasePanel { get; }

        GISShare.Controls.WinForm.WFNew.DockPanel.DockPanel.BasePanelCollection BasePanels { get; }

        Rectangle CaptionRectangle { get; }

        Rectangle ImageRectangle { get; }

        Rectangle TitleRectangle { get; }

        //Rectangle FrameRectangle { get; }
        
        bool SetSelectBasePanel(BasePanel basePanel);
    }
}
