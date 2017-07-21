using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IForm
    {
        bool IsMiddleCaptionText { get; }

        string Name { get; }

        string Text { get; }

        bool Enabled { get; }

        Icon Icon { get; }

        bool IsActive { get; }

        int CaptionHeight { get; }

        bool IsDrawIcon { get; }

        bool CancelDrawNC { get; }

        bool IsMdiChild { get; }

        bool HasMenu { get; }

        System.Windows.Forms.FormWindowState WindowState { get; }

        System.Windows.Forms.FormBorderStyle FormBorderStyle { get; }

        Size FrameBorderSize { get; }

        Rectangle FrameRectangle { get; }

        Rectangle CaptionRectangle { get; }

        Rectangle CaptionIconRectangle { get; }

        Rectangle CaptionTextRectangle { get; }

        void GetRadiusInfo(out int iLeftTopRadius, out  int iRightTopRadius, out int iLeftBottomRadius, out int iRightBottomRadius);
    }
}
