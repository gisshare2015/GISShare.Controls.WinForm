using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IRibbonForm : IForm
    {
        //string Name { get;set; }

        //string Text { get; set;}

        //bool IsActive { get; }

        //FormBorderStyle FormBorderStyle { get; }

        //Rectangle FrameRectangle { get; }

        //Size FrameBorderSize { get; }

        IRibbonControl RibbonControl { get; set; }
    }
}
