using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IPopupPanel
    {
        /// <summary>
        /// ÒΐΈ½ΚµΜε
        /// </summary>
        Control Entity { get; set; }

        void TrySetPopupPanelSize(Size size);
    }
}
