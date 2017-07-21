using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.DockBar
{
    public enum CategoryIndex_5_Style : int
    {
        eContextMenu = 1500,
        //
        eMenuBar,
        eToolBar,
        eStatusBar,
        //
        eMenuItem,
        eDropDownButtonItem,
        eSplitButtonItem,
        //
        eButtonItem,
        eCheckBoxItem,
        eComboBoxItem,
        eLabelItem,
        eMaskedTextBoxItem,
        eRadioButtonItem,
        eNumericUpDownItem,
        eProgressBarItem,
        eSeparatorItem,
        eTextBoxItem
    }

    public enum Event_5_Style
    {
        eClick = 500,
        //
        eMouseDown,
        eMouseUp,
        eMouseMove,
        eMouseClick,
        eMouseDoubleClick,
        //
        eButtonMouseDown,
        eButtonMouseUp,
        eButtonMouseMove,
        eButtonMouseClick,
        eButtonMouseDoubleClick,
        //
        eCheckedChanged,
        eTextChanged,
        eValueChanged,
        eSelectedIndexChanged,
        //
        eOpened,
        eClosed,
        eDropDownOpened,
        eDropDownClosed
    }
}
