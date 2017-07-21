using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface ISuperToolTip
    {
        bool AutoClose { get; set; }

        int Interval { get; set; }

        int OffsetX { get; set; }

        int OffsetY { get; set; }

        bool SetToolTip(IBaseItem2 pBaseItem);
        bool SetToolTip(IBaseItem2 pBaseItem, ITipInfo pTipInfo);
        bool RemoveToolTip(IBaseItem2 pBaseItem);
        //
        //bool SetToolTip(Forms.INCBaseItem pNCBaseItem);
        //bool SetToolTip(Forms.INCBaseItem pNCBaseItem, ITipInfo pTipInfo);
        //bool RemoveToolTip(Forms.INCBaseItem pNCBaseItem);
        //
        bool SetToolTipEx(System.Windows.Forms.Control control);
        bool SetToolTipEx(System.Windows.Forms.Control control, ITipInfo pTipInfo);
        bool RemoveToolTipEx(System.Windows.Forms.Control control);

        void ClearToolTip();
    }
}
