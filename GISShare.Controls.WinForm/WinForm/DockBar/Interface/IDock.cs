using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.DockBar
{
    public interface IDock
    {
        /// <summary>
        /// 布局到停靠区
        /// </summary>
        /// <param name="eDockStyle"></param>
        void ToDockArea(DockStyle eDockStyle);

        /// <summary>
        /// 布局到停靠区
        /// </summary>
        /// <param name="eDockStyle"></param>
        /// <param name="row"></param>
        void ToDockArea(DockStyle eDockStyle, int row);

        /// <summary>
        /// 布局到停靠区
        /// </summary>
        /// <param name="eDockStyle"></param>
        /// <param name="location"></param>
        void ToDockArea(DockStyle eDockStyle, Point location);

        /// <summary>
        /// 布局到停靠区
        /// </summary>
        /// <param name="eDockStyle"></param>
        /// <param name="row"></param>
        /// <param name="location"></param>
        void ToDockArea(DockStyle eDockStyle, int row, Point location);
    }
}
