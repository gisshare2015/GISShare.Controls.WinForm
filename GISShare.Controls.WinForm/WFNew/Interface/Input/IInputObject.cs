using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IInputObject
    {
        bool CanEdit { get; set; }

        bool CanSelect { get; set; }

        string InputText { get; set; }

        Font InputFont { get; }

        Color InputForeColor { get; }

        /// <summary>
        /// Ӧ������Ļ����
        /// </summary>
        Rectangle InputRegionRectangle { get; }

        bool IsInputing { get; }
    }
}
