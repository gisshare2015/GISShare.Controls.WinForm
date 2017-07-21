using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IImageLabelItem : ILabelItem, IImageBoxItem
    {
        bool AutoPlanTextRectangle { get; set; }

        int ITSpace { get; set; }

        DisplayStyle eDisplayStyle { get;set; }

        Rectangle DrawRectangle { get; }//IT = Image Text

        Rectangle ITDrawRectangle { get; }//IT = Image Text
    }
}
