using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public delegate object GetServiceCallBack(Type type);

    //
    //
    //

    /// <summary>
    /// ���ڼ�������
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void ItemEventHandler(object sender, ItemEventArgs e);

    public class ItemEventArgs : EventArgs
    {
        public ItemEventArgs(object item)
        {
            this.m_Item = item;
        }

        private object m_Item = null;
        public object Item
        {
            get { return m_Item; }
        }
    }

}
