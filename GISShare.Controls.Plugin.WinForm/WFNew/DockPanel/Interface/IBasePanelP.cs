using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.WFNew.DockPanel
{
    public interface IBasePanelP : IBaseItemP_
    {
        bool CanDockUp { get; }

        bool CanDockLeft { get; }

        bool CanDockRight { get; }

        bool CanDockBottom { get; }

        bool CanDockFill { get; }

        bool CanFloat { get; }

        bool CanHide { get; }

        bool CanClose { get; }

        bool IsBasePanel { get; }

        bool IsDocumentPanel { get; }

        bool VisibleEx { get; }

        System.Drawing.Image Image { get; }

        System.Drawing.Point DockPanelFloatFormLocation { get; }

        System.Drawing.Size DockPanelFloatFormSize { get; }

        System.Windows.Forms.Control[] ChildControls { get; }
    }
}
