using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.Demo.Hook
{
    public interface IAppHook
    {
        IBaseHost Host { get; }
        System.Windows.Forms.RichTextBox RichTextBox { get; }
        GISShare.Controls.WinForm.WFNew.DockPanel.DockPanelManager DockPanelManager { get; }
        string FileName { get; set; }

        void UIView();
    }
}
