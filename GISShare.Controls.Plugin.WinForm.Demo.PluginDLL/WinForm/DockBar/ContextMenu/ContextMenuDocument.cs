using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.ContextMenu
{
    class ContextMenuDocument : GISShare.Controls.Plugin.WinForm.DockBar.ContextMenuP
    {
        public ContextMenuDocument()
        {
            this._Name = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.ContextMenu.ContextMenuDocument";
            this._Text = "ContextMenuDocument";
            this._ItemCount = 7;
        }

        Hook.IAppHook m_pAppHook;
        public override void OnCreate(object hook)
        {
            this.m_pAppHook = hook as Hook.IAppHook;
            if (this.m_pAppHook != null)
            {
                this.m_pAppHook.RichTextBox.MouseDown += new System.Windows.Forms.MouseEventHandler(RichTextBox_MouseDown);
            }
        }
        void RichTextBox_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                GISShare.Controls.WinForm.DockBar.ContextMenu contextMenu = this.EntityObject as GISShare.Controls.WinForm.DockBar.ContextMenu;
                if (contextMenu != null)
                {
                    contextMenu.Show(this.m_pAppHook.RichTextBox, e.Location);
                }
            }
        }

        public override void GetItemInfo(int iIndex, IItemDef pItemDef)
        {
            switch (iIndex)
            {
                case 0:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.ContextMenu.Item.MenuItemNew";
                    pItemDef.Group = false;
                    break;
                case 1:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.ContextMenu.Item.MenuItemOpen";
                    pItemDef.Group = false;
                    break;
                case 2:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.ContextMenu.Item.MenuItemSave";
                    pItemDef.Group = false;
                    break;
                case 3:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.ContextMenu.Item.SeparatorItem1";
                    pItemDef.Group = false;
                    break;
                case 4:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.ContextMenu.Item.MenuItemExist";
                    pItemDef.Group = false;
                    break;
                case 5:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.ContextMenu.Item.SeparatorItem2";
                    pItemDef.Group = false;
                    break;
                case 6:
                    pItemDef.ID = "GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WinForm.DockBar.ContextMenu.Item.MenuItemAbout";
                    pItemDef.Group = false;
                    break;
            }
        }
    }
}
