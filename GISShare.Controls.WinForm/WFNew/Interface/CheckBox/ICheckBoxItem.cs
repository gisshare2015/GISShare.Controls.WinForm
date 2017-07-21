using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface ICheckBoxItem : IRadioButtonItem
    {
        CheckState CheckState { get; set; }
    }
}
