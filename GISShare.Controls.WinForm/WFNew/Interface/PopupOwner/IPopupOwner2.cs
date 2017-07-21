using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IPopupOwner2 : IPopupOwner
    {
        Padding GetPopupPadding();

        void SetPopupPadding(int iPadding);

        void SetPopupPadding(int left, int top, int right, int bottom);

        void SetPopupRadius(int iRadius);

        void SetPopupRadius(int iLeftTopRadius, int iLeftBottomRadius, int iRightTopRadius, int iRightBottomRadius);

        void RefreshPopupPanel();
    }
}
