using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IImageCheckBoxItem : IImageLabelItem, ICheckBoxItem
    {
        int CDSpace { get; set; }//CD = CheckRectangle DrawRectangle
    }
}
