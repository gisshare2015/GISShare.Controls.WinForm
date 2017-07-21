using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm
{
    public interface ICheckBoxX : IRadioButtonX
    {
        CheckState CheckState { get; }
    }
}
