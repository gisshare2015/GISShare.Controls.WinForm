using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IDescriptionButtonItem : IBaseButtonItem
    {
        Color DescriptionForeColor { get; set; }

        Font DescriptionFont { get; set; }

        string Description { get; set; }

        int TDSpace { get; set; }

        Rectangle TDRectangle { get; }

        Rectangle DescriptionRectangle { get; }
    }
}
