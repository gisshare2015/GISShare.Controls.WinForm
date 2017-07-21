using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IFormButton : IBaseButtonItem
    {
        FormButtonStyle eFormButtonStyle { get;  }

        System.Windows.Forms.Form OperationForm { get; }

        Rectangle GlyphRectangle { get; }
    }
}
