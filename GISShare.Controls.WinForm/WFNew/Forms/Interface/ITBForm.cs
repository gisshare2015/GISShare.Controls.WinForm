using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew.Forms
{
    public interface ITBForm : IForm, ICollectionItem2, ICollectionItem3
    {
        //bool IsMiddleCaptionText { get; set; }

        //string Name { get; }

        //string Text { get; }

        //bool Enabled { get; }

        //Icon Icon { get; }

        //bool IsActive { get; }

        //int CaptionHeight { get; }

        //bool IsDrawIcon { get; }

        //bool CancelDrawNC { get; }

        //bool IsMdiChild { get; }

        //bool HasMenu { get; }

        //System.Windows.Forms.FormWindowState WindowState { get; }

        //System.Windows.Forms.FormBorderStyle FormBorderStyle { get; }

        //Size FrameBorderSize { get; }

        //Rectangle FrameRectangle { get; }

        //Rectangle CaptionRectangle { get; }

        //Rectangle CaptionIconRectangle { get; }

        //Rectangle CaptionTextRectangle { get; }

        //void GetRadiusInfo(out int iLeftTopRadius, out  int iRightTopRadius, out int iLeftBottomRadius, out int iRightBottomRadius);

        bool ShowQuickAccessToolbar { get; set; }

        WFNew.QuickAccessToolbarStyle eQuickAccessToolbarStyle { get; set; }

        BaseItemCollection ToolbarItems { get; }

        int FormButtonStackItemNCWidth { get; }

        int MinCaptionTextWidth { get; }

        Size MinimumSize { get; }

        Size MaximumSize { get; }

        bool IsProcessNCArea { get; }

        bool IsMdiContainer { get; }

        Rectangle RestoreBounds { get; }

        Rectangle NCRectangleEx { get; }

        Rectangle ScreenRectangle { get; }

        void Activate();
    }
}
