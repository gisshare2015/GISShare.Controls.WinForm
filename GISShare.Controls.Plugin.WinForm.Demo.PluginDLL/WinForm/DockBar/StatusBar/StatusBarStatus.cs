using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.StatusBar
{
    class StatusBarStatus : GISShare.Controls.Plugin.WinForm.DockBar.StatusBarP
    {
        public StatusBarStatus()
        {
            this._Name = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.StatusBar.StatusBarStatus";
            this._Text = "状态栏";
            this._ItemCount = 1;
        }

        public override void GetItemInfo(int iIndex, IItemDef pItemDef)
        {
            switch (iIndex)
            {
                case 0:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.StatusBar.Item.LabelInfo";
                    pItemDef.Group = false;
                    break;
            }
        }
    }
}
