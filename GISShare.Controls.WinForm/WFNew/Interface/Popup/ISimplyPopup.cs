using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface ISimplyPopup : IBaseItem2, IPanelPopup, ICollectionItem
    {
        //bool HaveVisibleBaseItem { get; }

        Padding GetPadding();

        void SetPadding(int iPadding);

        void SetPadding(int left, int top, int right, int bottom);

        void SetRadius(int iRadius);

        void SetRadius(int iLeftTopRadius, int iLeftBottomRadius, int iRightTopRadius, int iRightBottomRadius);

        void TrySetPopupPanelSize(Size size);

        void RefreshPopupPanel();
    }
}
