using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.ContextMenu.Item
{
    class MenuItemAbout : GISShare.Controls.Plugin.WinForm.DockBar.MenuItemP
    {
        public MenuItemAbout()
        {
            this._Name = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.ContextMenu.Item.MenuItemAbout";
            this._Text = "关于";
            this._ToolTipText = this._Text;
            this._Category = "快捷菜单";
            this._Image = new System.Drawing.Bitmap(this.GetType().Assembly.GetManifestResourceStream("GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.Image.Info.ico"));
        }

        public override void OnTriggerEvent(int iEventStyle, EventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show(
                "这是基于GISShare.Controls.WinForm控件，插件结构开发的小例子！",
                "信息",
                System.Windows.Forms.MessageBoxButtons.OK,
                System.Windows.Forms.MessageBoxIcon.Information);
        }
    }
}
