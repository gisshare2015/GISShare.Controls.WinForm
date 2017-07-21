using System;
using System.Collections.Generic;
using System.Text;
using GISShare.Controls.WinForm.WFNew;

namespace GISShare.Controls.Plugin.WinForm.WFNew
{
    public interface IButtonItemP : ISplitButtonItemP, ISubItem
    {
        ButtonStyle eButtonStyle { get;  }
    }
}
