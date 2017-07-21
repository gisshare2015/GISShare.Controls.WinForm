using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew.View
{
    public interface ITextEditViewItem : ITextViewItem, IInputObject
    {
        //bool CanEdit { get; set; }

        object EditObject { get; }
    }
}
