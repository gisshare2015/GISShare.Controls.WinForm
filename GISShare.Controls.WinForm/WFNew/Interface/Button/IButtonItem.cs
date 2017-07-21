using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IButtonItem : ISplitButtonItem
    {
        ButtonStyle eButtonStyle { get; set; }
    }
}
