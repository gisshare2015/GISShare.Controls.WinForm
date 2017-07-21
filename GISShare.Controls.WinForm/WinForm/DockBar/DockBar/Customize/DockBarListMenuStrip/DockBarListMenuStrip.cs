using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.DockBar
{
    [ToolboxItem(false)]
    class DockBarListMenuStrip : ContextMenu
    {
        private DockBarManager m_DockBarManager = null;

        public DockBarListMenuStrip(DockBarManager dockBarManager)
            : base()
        {
            this.m_DockBarManager = dockBarManager;
        }

        public new void Show(Point postion)
        {
            this.SetDockBarList();
            //
            base.Show(postion);
        }

        public new void Show(Control control, Point postion)
        {
            this.SetDockBarList();
            //
            base.Show(control, postion);
        }

        private void SetDockBarList()
        {
            if (this.m_DockBarManager.MenuBar != null)
            {
                MenuItem toolStripMenuItem1 = new MenuItem();//"主菜单"
                toolStripMenuItem1.Name = Language.LanguageStrategy.MainMenuText;
                toolStripMenuItem1.Text = Language.LanguageStrategy.MainMenuText;
                toolStripMenuItem1.Image = this.m_DockBarManager.MenuBar.Image;
                toolStripMenuItem1.Click += new EventHandler(this.MenuItemDockBar_Click);
                toolStripMenuItem1.Checked = this.m_DockBarManager.MenuBar.VisibleEx;
                toolStripMenuItem1.Tag = this.m_DockBarManager.MenuBar;
                this.Items.Add(toolStripMenuItem1);
            }
            //
            if (this.m_DockBarManager.StatusBar != null)
            {
                MenuItem toolStripMenuItem4 = new MenuItem();//"状态栏"
                toolStripMenuItem4.Name = Language.LanguageStrategy.StatusBarText;
                toolStripMenuItem4.Text = Language.LanguageStrategy.StatusBarText;
                toolStripMenuItem4.Image = this.m_DockBarManager.StatusBar.Image;
                toolStripMenuItem4.Click += new EventHandler(this.MenuItemDockBar_Click);
                toolStripMenuItem4.Checked = this.m_DockBarManager.StatusBar.VisibleEx;
                toolStripMenuItem4.Tag = this.m_DockBarManager.StatusBar;
                this.Items.Add(toolStripMenuItem4);
            }
            //
            if (
                this.Items.Count > 0 && 
                (this.m_DockBarManager.ToolBars.Count > 0 || this.m_DockBarManager.CustomizeToolBars.Count > 0)
                )
            {
                this.Items.Add(new ToolStripSeparator()); 
            }
            //
            foreach (ToolBar one in this.m_DockBarManager.ToolBars)
            {
                MenuItem toolStripMenuItem2 = new MenuItem();
                toolStripMenuItem2.Name = one.Name;
                toolStripMenuItem2.Text = one.Text;
                toolStripMenuItem2.Image = one.Image;
                toolStripMenuItem2.Click += new EventHandler(this.MenuItemDockBar_Click);
                toolStripMenuItem2.Checked = one.VisibleEx;
                toolStripMenuItem2.Tag = one;
                this.Items.Add(toolStripMenuItem2);
            }
            //
            foreach (CustomizeToolBar one in this.m_DockBarManager.CustomizeToolBars)
            {
                MenuItem toolStripMenuItem3 = new MenuItem();
                toolStripMenuItem3.Name = one.Name;
                toolStripMenuItem3.Text = one.Text;
                toolStripMenuItem3.Image = one.Image;
                toolStripMenuItem3.Click += new EventHandler(this.MenuItemDockBar_Click);
                toolStripMenuItem3.Checked = one.VisibleEx;
                toolStripMenuItem3.Tag = one;
                this.Items.Add(toolStripMenuItem3);
            }
            //
            //
            //
            if (this.Items.Count > 0) this.Items.Add(new SeparatorItem());
            MenuItem menuItemCustomize = new MenuItem();//"自定义..."
            menuItemCustomize.Name = Language.LanguageStrategy.CustomizeText;
            menuItemCustomize.Text = Language.LanguageStrategy.CustomizeText;
            menuItemCustomize.Click += new EventHandler(this.MenuItemCustomize_Click);
            this.Items.Add(menuItemCustomize);
        }

        private void MenuItemDockBar_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;
            if (toolStripMenuItem == null) return;
            IDockBar pDockBar = toolStripMenuItem.Tag as IDockBar;
            if (pDockBar == null) return;
            toolStripMenuItem.Checked = !toolStripMenuItem.Checked;
            pDockBar.VisibleEx = toolStripMenuItem.Checked;
        }

        private void MenuItemCustomize_Click(object sender, EventArgs e)
        {
            this.m_DockBarManager.DockBarCustomize();
        }
    }
}
