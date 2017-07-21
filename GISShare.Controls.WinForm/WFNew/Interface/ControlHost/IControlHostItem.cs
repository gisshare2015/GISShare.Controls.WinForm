using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IControlHostItem : IBaseItem2
    {
        System.Drawing.Rectangle ControlEnvelope { get;}

        System.Windows.Forms.Control ControlObject { get; }
    }
}
