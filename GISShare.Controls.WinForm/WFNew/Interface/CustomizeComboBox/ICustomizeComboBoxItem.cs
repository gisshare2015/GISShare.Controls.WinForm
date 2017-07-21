using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface ICustomizeComboBoxItem : ITextBoxItem, IPopupOwner
    {
        bool AutoClosePopup { get; set; }

        int MinHeight { get; set; }

        int MinWidth { get; set; }

        int DropDownWidth { get; set; }

        int DropDownHeight { get; set; }

        CustomizeComboBoxStyle eCustomizeComboBoxStyle { get; set; }

        ModifySizeStyle eModifySizeStyle { get; set; }

        object Value { get; }

        int OffsetX { get; }

        int ShowDropDownNum { get; }

        BaseItemState eSplitState { get; }

        Rectangle SplitRectangle { get; }

        Size ArrowSize { get; }

        Rectangle ArrowRectangle { get; }

        System.Windows.Forms.Control ControlObject { get; }
    }
}
