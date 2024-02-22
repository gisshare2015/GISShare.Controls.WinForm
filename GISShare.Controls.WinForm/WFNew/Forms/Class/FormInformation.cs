using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew.Forms
{
    public class FormInformation
    {
        /// <summary>
        /// 是否为 XP以上系统
        /// </summary>
        public static readonly bool IsVista = Environment.OSVersion.Version.Major >= 6 && System.Windows.Forms.VisualStyles.VisualStyleInformation.IsEnabledByUser;

        /// <summary>
        /// Calculates the border size for the given form.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <returns></returns>
        public static Size GetBorderSize(Form form)
        {
            if (form == null || form.IsDisposed) return new Size(-1, -1);

            if (form.IsMdiChild && form.WindowState == FormWindowState.Minimized) return SystemInformation.FixedFrameBorderSize;

            Size border = new Size(0, 0);

            // Check for Caption
            Int32 style = GISShare.Win32.API.GetWindowLong(form.Handle, GISShare.Win32.GWLIndex.GWL_STYLE);
            bool caption = (style & (int)(GISShare.Win32.WindowStyles.WS_CAPTION)) != 0;
            int factor = SystemInformation.BorderMultiplierFactor - 1;

            switch (form.FormBorderStyle)
            {
                case FormBorderStyle.FixedToolWindow:
                case FormBorderStyle.FixedSingle:
                case FormBorderStyle.FixedDialog:
                    border = SystemInformation.FixedFrameBorderSize;
                    break;
                case FormBorderStyle.SizableToolWindow:
                case FormBorderStyle.Sizable:
                    if (IsVista)
                    {
                        border = SystemInformationX.FrameBorderSize;
                    }
                    else
                    {
                        border = SystemInformation.FixedFrameBorderSize + (caption ? (SystemInformation.BorderSize + new Size(factor, factor)) : new Size(factor, factor));
                    }
                    break;
                case FormBorderStyle.Fixed3D:
                    border = SystemInformation.FixedFrameBorderSize + SystemInformation.Border3DSize;
                    break;
            }

            return border;
        }

        /// <summary>
        /// Gets the size for <see cref="CaptionButton"/> the given form.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <returns></returns>
        public static Size GetCaptionButtonSize(Form form)
        {
            if (form == null || form.IsDisposed) return new Size(-1, -1);
            //
            if (form.IsMdiChild && form.WindowState == FormWindowState.Minimized) return SystemInformation.CaptionButtonSize;
            //
            switch (form.FormBorderStyle)
            {
                case FormBorderStyle.None:
                    return new Size(0, 0);
                case FormBorderStyle.FixedToolWindow:
                case FormBorderStyle.SizableToolWindow:
                    return SystemInformation.ToolWindowCaptionButtonSize;
                default:
                    return SystemInformation.CaptionButtonSize;
            }
            //return ((form.FormBorderStyle != FormBorderStyle.SizableToolWindow) && form.FormBorderStyle != FormBorderStyle.FixedToolWindow) ? SystemInformation.CaptionButtonSize : SystemInformation.ToolWindowCaptionButtonSize;
        }

        /// <summary>
        /// Gets the height of the caption.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <returns></returns>
        public static int GetCaptionHeight(Form form)
        {
            if (form == null || form.IsDisposed) return -1;
            //
            if (form.IsMdiChild && form.WindowState == FormWindowState.Minimized) return SystemInformation.CaptionHeight;
            //
            switch (form.FormBorderStyle) 
            {
                case FormBorderStyle.None:
                    return 0;
                case FormBorderStyle.FixedToolWindow:
                case FormBorderStyle.SizableToolWindow:
                    return SystemInformation.ToolWindowCaptionHeight + 1;
                default:
                    return SystemInformation.CaptionHeight + 2;
            }
            //return (form.FormBorderStyle != FormBorderStyle.SizableToolWindow && form.FormBorderStyle != FormBorderStyle.FixedToolWindow) ? SystemInformation.CaptionHeight : SystemInformation.ToolWindowCaptionHeight;
        }

        /// <summary>
        /// Gets a value indicating whether the given form has a system menu.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <returns></returns>
        public static bool HasMenu(Form form)
        {
            if (form == null || form.IsDisposed) return false;
            //
            return form.FormBorderStyle == FormBorderStyle.Sizable || 
                form.FormBorderStyle == FormBorderStyle.Fixed3D ||
                form.FormBorderStyle == FormBorderStyle.FixedSingle;
        }

        /// <summary>
        /// Gets the screen rect of the given form
        /// </summary>
        /// <param name="form">The form.</param>
        /// <returns></returns>
        public static Rectangle GetScreenRect(Form form)
        {
            if (form == null || form.IsDisposed) return new Rectangle();
            //
            return (form.Parent != null) ? form.Parent.RectangleToScreen(form.Bounds) : form.Bounds;
        }
    }
}
