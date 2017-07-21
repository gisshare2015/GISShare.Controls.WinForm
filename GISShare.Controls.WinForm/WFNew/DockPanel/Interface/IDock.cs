using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew.DockPanel
{
    public interface IDock
    {
        /// <summary>
        /// 布局到停靠区
        /// </summary>
        /// <param name="bInteral"></param>
        /// <param name="eDockStyle"></param>
        bool ToDockArea(bool bInteral, DockStyle eDockStyle);

        /// <summary>
        /// 布局到停靠区
        /// </summary>
        /// <param name="bInteral"></param>
        /// <param name="eDockStyle"></param>
        /// <param name="location"></param>
        bool ToDockArea(bool bInteral, DockStyle eDockStyle, Point location);
    }
}
