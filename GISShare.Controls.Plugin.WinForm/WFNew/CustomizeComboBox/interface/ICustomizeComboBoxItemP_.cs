using System;
using System.Collections.Generic;
using System.Text;
using GISShare.Controls.WinForm.WFNew;

namespace GISShare.Controls.Plugin.WinForm.WFNew
{
    public interface ICustomizeComboBoxItemP_ : ITextBoxItemP
    {
        int MinHeight { get;  }

        int MinWidth { get;  }

        int DropDownWidth { get;  }

        int DropDownHeight { get; }

        bool AutoClosePopup { get; }

        CustomizeComboBoxStyle eCustomizeComboBoxStyle { get; }

        ModifySizeStyle eModifySizeStyle { get;}
    }
}
