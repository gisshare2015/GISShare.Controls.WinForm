using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm
{
    public interface ICheckedListBoxX : IListBoxX
    {
        bool GetItemChecked(int iIndex);

        CheckState GetItemCheckState(int iIndex);

        void SetItemChecked(int iIndex, bool value);

        void SetItemCheckState(int iIndex, CheckState value);
    }
}
