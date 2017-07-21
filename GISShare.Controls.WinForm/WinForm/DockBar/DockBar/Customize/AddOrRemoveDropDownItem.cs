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
    class AddOrRemoveDropDownItem : DropDownButtonItem //System.Windows.Forms.ToolStripDropDownButton
    {
        private IDockBar m_pDockBar = null;
        private MenuItem m_DockBarMenuItem = null;
        private MenuItem m_CustomizeManager = null;

        public AddOrRemoveDropDownItem()
            : base()//"添加或移除按钮"
        {
            base.Name = "AddOrRemoveDropDownItem";
            base.Text = Language.LanguageStrategy.AddOrRemoveText;
            base.Overflow = ToolStripItemOverflow.Always;
            base.Visible = true;
            //
            this.m_DockBarMenuItem = new MenuItem();//"标准"
            this.m_DockBarMenuItem.Name = Language.LanguageStrategy.StandardText;
            this.m_DockBarMenuItem.Text = Language.LanguageStrategy.StandardText;
            this.m_CustomizeManager = new MenuItem();//"自定义..."
            this.m_CustomizeManager.Name = Language.LanguageStrategy.CustomizeText;
            this.m_CustomizeManager.Text = Language.LanguageStrategy.CustomizeText;
            this.m_CustomizeManager.Click += new EventHandler(CustomizeManager_Click);
            this.DropDownItems.Add(this.m_DockBarMenuItem);
            this.DropDownItems.Add(new SeparatorItem());
            this.DropDownItems.Add(this.m_CustomizeManager);
        }

        public AddOrRemoveDropDownItem(IDockBar dockBar)
            : this()//"添加或移除按钮"
        {
            this.m_pDockBar = dockBar;
        }

        public new ToolStripItemOverflow Overflow
        {
            get { return base.Overflow; }
            set { base.Overflow = ToolStripItemOverflow.Always; }
        }

        private void CustomizeManager_Click(object sender, EventArgs e)
        {
            this.m_pDockBar.DockBarManager.DockBarCustomize();
        }

        protected override void OnClick(EventArgs e)
        {
            if (this.m_pDockBar == null) this.m_pDockBar = this.Owner as IDockBar;
            //
            this.SetMenuItemList();
            //
            base.OnClick(e);
        }
        private void SetMenuItemList()
        {
            this.m_DockBarMenuItem.DropDownItems.Clear();
            foreach (ToolStripItem one in this.m_pDockBar.Items)
            {
                if (one is AddOrRemoveDropDownItem) continue;
                //
                MenuItem item = new MenuItem();
                item.Name = one.Text;
                item.Text = one.Text;
                item.Click += new EventHandler(DockBarItem_Click);
                item.Checked = one.Visible;
                item.Tag = one;
                this.m_DockBarMenuItem.DropDownItems.Add(item);
                //
                //this.SetMenuItemList(item, one as ToolStripDropDownItem);
            }
            //
            //if (this.m_pDockBar.eDockBarStyle != DockBarStyle.eCustomizeToolBar)
            //{
            //    if(this.m_DockBarMenuItem.DropDownItems.Count > 0) this.m_DockBarMenuItem.DropDownItems.Add(new System.Windows.Forms.ToolStripSeparator());
            //    System.Windows.Forms.ToolStripMenuItem item2 = new ToolStripMenuItem(Language.LanguageStrategy.ResetToolbarText, null, new EventHandler(ResetDockBarItem_Click));//"重置工具栏"
            //    this.m_DockBarMenuItem.DropDownItems.Add(item2);
            //}
            if (this.m_DockBarMenuItem.DropDownItems.Count > 0) this.m_DockBarMenuItem.DropDownItems.Add(new System.Windows.Forms.ToolStripSeparator());
            MenuItem item2 = new MenuItem();//"重置工具栏"
            item2.Name = Language.LanguageStrategy.ResetToolbarText;
            item2.Text = Language.LanguageStrategy.ResetToolbarText;
            item2.Click += new EventHandler(ResetDockBarItem_Click);
            this.m_DockBarMenuItem.DropDownItems.Add(item2);
        }
        //private void SetMenuItemList(ToolStripMenuItem toolStripMenuItem, ToolStripDropDownItem toolStripDropDownItem) 
        //{
        //    if (toolStripDropDownItem == null) return;
        //    //
        //    foreach (ToolStripItem one in toolStripDropDownItem.DropDownItems)
        //    {
        //        System.Windows.Forms.ToolStripMenuItem item = new ToolStripMenuItem(one.Text, null, new EventHandler(DockBarItem_Click));
        //        item.Checked = one.Visible;
        //        item.Tag = one;
        //        toolStripMenuItem.DropDownItems.Add(item);
        //        //
        //        this.SetMenuItemList(item, one as ToolStripDropDownItem);
        //    }
        //}
        private void DockBarItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem one = sender as ToolStripMenuItem;
            if (one == null) return;
            ToolStripItem item = one.Tag as ToolStripItem;
            if (item == null) return;
            one.Checked = !one.Checked;
            item.Visible = one.Checked;
            //
            //
            //
            IDockArea pDockArea = this.m_pDockBar.Parent as IDockArea;
            if (pDockArea == null) return;
            if (pDockArea.eDockAreaStyle != DockAreaStyle.eDockBarFloatForm) return;
            DockBarFloatForm dockBarFloatForm = this.m_pDockBar.Parent as DockBarFloatForm;
            if (dockBarFloatForm == null) return;
            if (item.Visible) { dockBarFloatForm.Width += item.Margin.Left + item.Width + item.Margin.Right; }
            else { dockBarFloatForm.Width -= item.Margin.Left + item.Width + item.Margin.Right; }
            dockBarFloatForm.ResetSize();
        }
        private void ResetDockBarItem_Click(object sender, EventArgs e)
        {
            this.m_pDockBar.Reset();
            //
            //
            //
            IDockArea pDockArea = this.m_pDockBar.Parent as IDockArea;
            if (pDockArea == null) return;
            if (pDockArea.eDockAreaStyle != DockAreaStyle.eDockBarFloatForm) return;
            DockBarFloatForm dockBarFloatForm = this.m_pDockBar.Parent as DockBarFloatForm;
            if (dockBarFloatForm == null) return;
            dockBarFloatForm.ResetSize();
        }
    }

    [ToolboxItem(false)]
    class AddOrRemoveDropDownList : System.Windows.Forms.ToolStripDropDown
    {
        public AddOrRemoveDropDownList(IDockBar dockBar)
        {
            this.DropShadowEnabled = false;
            //
            AddOrRemoveDropDownItem item = new AddOrRemoveDropDownItem(dockBar);
            item.Overflow = ToolStripItemOverflow.Always;
            this.Items.Add(item);
        }
    }
}
