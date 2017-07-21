using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace GISShare.Controls.WinForm.WFNew.View
{
    /// <summary>
    /// ViewItem 委托
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void ViewItemEventHandler(object sender, ViewItemEventArgs e);

    /// <summary>
    /// ViewItem 参数
    /// </summary>
    public class ViewItemEventArgs : EventArgs
    {
        public ViewItemEventArgs(IViewItem viewItem)
        {
            this.m_pViewItem = viewItem;
        }

        IViewItem m_pViewItem;
        public IViewItem pViewItem
        {
            get { return m_pViewItem; }
        }
    }

    //
    //
    //

    public delegate void RowHeaderItemDrawEventHandler(object sender, RowHeaderItemDrawEventArgs e);

    public class RowHeaderItemDrawEventArgs : PaintEventArgs
    {
        public RowHeaderItemDrawEventArgs(Graphics graphics, Rectangle clipRect, IRowHeaderItem pRowHeaderItem)
            : base(graphics, clipRect)
        {
            this.m_pRowHeaderItem = pRowHeaderItem;
        }

        IRowHeaderItem m_pRowHeaderItem;
        public IRowHeaderItem RowHeaderItem
        {
            get { return m_pRowHeaderItem; }
        }
    }
}
