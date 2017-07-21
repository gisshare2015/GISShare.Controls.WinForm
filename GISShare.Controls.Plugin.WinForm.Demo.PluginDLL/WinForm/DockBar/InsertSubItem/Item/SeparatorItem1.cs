using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.InsertSubItem.Item
{
    class SeparatorItem1 : GISShare.Controls.Plugin.WinForm.DockBar.SeparatorItemP
    {
        public SeparatorItem1()
        {
            this._Name = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.InsertSubItem.Item.SeparatorItem1";
            this._Text = "分隔条";
            this._ToolTipText = this._Text;
            this._Category = "插入项";
        }
    }
}
