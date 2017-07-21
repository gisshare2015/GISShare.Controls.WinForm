using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IInputObject2 : IInputObject
    {
        bool InputingFilterText { get; set; }
    }
}
