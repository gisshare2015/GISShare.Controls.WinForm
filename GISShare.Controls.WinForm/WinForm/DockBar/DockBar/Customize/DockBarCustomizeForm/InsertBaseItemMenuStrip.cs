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
    class InsertBaseItemMenuStrip : ContextMenu
    {
        private IBaseItemDB m_pBaseItem = null;
        private DockBarManager m_DockBarManager = null;

        public InsertBaseItemMenuStrip(DockBarManager dockBarManager, IBaseItemDB pBaseItem)
            : base()
        {
            this.m_pBaseItem = pBaseItem;
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
            int i = 0;
            foreach (CustomizeToolBar one in this.m_DockBarManager.CustomizeToolBars)
            {
                MenuItem toolStripMenuItem3 = new MenuItem();//("添加到“" + one.Text + "”");
                toolStripMenuItem3.Name = Language.LanguageStrategy.AddToText + Language.LanguageStrategy.DoubleQuotationMarks_Left + one.Text + Language.LanguageStrategy.DoubleQuotationMarks_Right;
                toolStripMenuItem3.Text = toolStripMenuItem3.Name;
                toolStripMenuItem3.Tag = one;
                for (i = 0; i < one.Items.Count; i++)
                {
                    MenuItem item = new MenuItem();//("第 " + i.ToString() + " 位", null, InsertBaseItem_Click);
                    item.Name = Language.LanguageStrategy.InText + i.ToString() + Language.LanguageStrategy.PositionText;
                    item.Text = item.Name;
                    item.Click += new EventHandler(InsertBaseItem_Click);
                    item.Tag = i;
                    toolStripMenuItem3.DropDownItems.Add(item);
                    //
                    this.SetDockBarList(toolStripMenuItem3.DropDownItems, one.Items[i] as ICustomize);
                }
                //
                if (i > 0)
                {
                    toolStripMenuItem3.DropDownItems.Add(new SeparatorItem());
                    MenuItem resetItem = new MenuItem();//"重置"
                    resetItem.Name = Language.LanguageStrategy.ResetText;
                    resetItem.Text = resetItem.Name;
                    resetItem.Click += new EventHandler(ResetItem_Click);
                    toolStripMenuItem3.DropDownItems.Add(resetItem);
                }
                //
                this.Items.Add(toolStripMenuItem3);
            }
            //
            foreach (ToolBar one in this.m_DockBarManager.ToolBars)
            {
                MenuItem toolStripMenuItem2 = new MenuItem();//("添加到“" + one.Text + "”");
                toolStripMenuItem2.Name = Language.LanguageStrategy.AddToText + Language.LanguageStrategy.DoubleQuotationMarks_Left + one.Text + Language.LanguageStrategy.DoubleQuotationMarks_Right;
                toolStripMenuItem2.Text = toolStripMenuItem2.Name;
                toolStripMenuItem2.Tag = one;
                for (i = 0; i < one.Items.Count; i++)
                {
                    MenuItem item = new MenuItem();//("第 " + i.ToString() + " 位", null, InsertBaseItem_Click);
                    item.Name = Language.LanguageStrategy.InText + i.ToString() + Language.LanguageStrategy.PositionText;
                    item.Text = item.Name;
                    item.Click += new EventHandler(InsertBaseItem_Click);
                    item.Tag = i;
                    toolStripMenuItem2.DropDownItems.Add(item);
                    //
                    this.SetDockBarList(toolStripMenuItem2.DropDownItems, one.Items[i] as ICustomize);
                }
                //
                if (i > 0)
                {
                    toolStripMenuItem2.DropDownItems.Add(new SeparatorItem());
                    MenuItem resetItem = new MenuItem();//"重置"
                    resetItem.Name = Language.LanguageStrategy.ResetText;
                    resetItem.Text = resetItem.Name;
                    resetItem.Click += new EventHandler(ResetItem_Click);
                    toolStripMenuItem2.DropDownItems.Add(resetItem);
                }
                //
                this.Items.Add(toolStripMenuItem2);
            }
            //
            if (this.Items.Count > 0 && this.m_DockBarManager.ContextMenus.Count > 0) { this.Items.Add(new SeparatorItem()); }
            //
            foreach (ContextMenu one in this.m_DockBarManager.ContextMenus)
            {
                MenuItem toolStripMenuItem5 = new MenuItem();//("添加到“" + one.Text + "”");
                toolStripMenuItem5.Name = Language.LanguageStrategy.AddToText + Language.LanguageStrategy.DoubleQuotationMarks_Left + one.Text + Language.LanguageStrategy.DoubleQuotationMarks_Right;
                toolStripMenuItem5.Text = toolStripMenuItem5.Name;
                toolStripMenuItem5.Tag = one;
                for (i = 0; i < one.Items.Count; i++)
                {
                    MenuItem item = new MenuItem();//("第 " + i.ToString() + " 位", null, InsertBaseItem_Click);
                    item.Name = Language.LanguageStrategy.InText + i.ToString() + Language.LanguageStrategy.PositionText;
                    item.Text = item.Name;
                    item.Click += new EventHandler(InsertBaseItem_Click);
                    item.Tag = i;
                    toolStripMenuItem5.DropDownItems.Add(item);
                    //
                    this.SetDockBarList(toolStripMenuItem5.DropDownItems, one.Items[i] as ICustomize);
                }
                MenuItem item5 = new MenuItem();//("第 " + i.ToString() + " 位", null, InsertBaseItem_Click);
                item5.Name = Language.LanguageStrategy.InText + i.ToString() + Language.LanguageStrategy.PositionText;
                item5.Text = item5.Name;
                item5.Click += new EventHandler(InsertBaseItem_Click);
                item5.Tag = i;
                toolStripMenuItem5.DropDownItems.Add(item5);
                //
                if (i > 0)
                {
                    toolStripMenuItem5.DropDownItems.Add(new SeparatorItem());
                    MenuItem resetItem = new MenuItem();//"重置"
                    resetItem.Name = Language.LanguageStrategy.ResetText;
                    resetItem.Text = resetItem.Name;
                    resetItem.Click += new EventHandler(ResetItem_Click);
                    toolStripMenuItem5.DropDownItems.Add(resetItem);
                }
                //
                this.Items.Add(toolStripMenuItem5);

            }
            //
            if (this.Items.Count > 0 && (this.m_DockBarManager.MenuBar != null || this.m_DockBarManager.StatusBar != null)) { this.Items.Add(new SeparatorItem()); }
            //
            if (this.m_DockBarManager.MenuBar != null)
            {
                //ToolStripMenuItem toolStripMenuItem1 = new ToolStripMenuItem("添加到“" + this.m_DockBarManager.MenuBar.Text + "”");
                MenuItem toolStripMenuItem1 = new MenuItem();//("添加到“主菜单”");
                toolStripMenuItem1.Name = Language.LanguageStrategy.AddToText + Language.LanguageStrategy.DoubleQuotationMarks_Left + Language.LanguageStrategy.MainMenuText + Language.LanguageStrategy.DoubleQuotationMarks_Right;
                toolStripMenuItem1.Text = toolStripMenuItem1.Name;
                toolStripMenuItem1.Tag = this.m_DockBarManager.MenuBar;
                for (i = 0; i < this.m_DockBarManager.MenuBar.Items.Count; i++)
                {
                    MenuItem item = new MenuItem();//("第 " + i.ToString() + " 位", null, InsertBaseItem_Click);
                    item.Name = Language.LanguageStrategy.InText + i.ToString() + Language.LanguageStrategy.PositionText;
                    item.Text = item.Name;
                    item.Click += new EventHandler(InsertBaseItem_Click);
                    item.Tag = i;
                    toolStripMenuItem1.DropDownItems.Add(item);
                    //
                    this.SetDockBarList(toolStripMenuItem1.DropDownItems, this.m_DockBarManager.MenuBar.Items[i] as ICustomize);
                }
                MenuItem item1 = new MenuItem();//("第 " + i.ToString() + " 位", null, InsertBaseItem_Click);
                item1.Name = Language.LanguageStrategy.InText + i.ToString() + Language.LanguageStrategy.PositionText;
                item1.Text = item1.Name;
                item1.Click += new EventHandler(InsertBaseItem_Click);
                item1.Tag = i;
                toolStripMenuItem1.DropDownItems.Add(item1);
                //
                if (i > 0)
                {
                    toolStripMenuItem1.DropDownItems.Add(new SeparatorItem());
                    MenuItem resetItem = new MenuItem();//"重置"
                    resetItem.Name = Language.LanguageStrategy.ResetText;
                    resetItem.Text = resetItem.Name;
                    resetItem.Click += new EventHandler(ResetItem_Click);
                    toolStripMenuItem1.DropDownItems.Add(resetItem);
                }
                //
                this.Items.Add(toolStripMenuItem1);
            }
            //
            if (this.m_DockBarManager.StatusBar != null)
            {
                //ToolStripMenuItem toolStripMenuItem4 = new ToolStripMenuItem("添加到“" + this.m_DockBarManager.StatusBar.Text + "”");
                MenuItem toolStripMenuItem4 = new MenuItem();//("添加到“状态栏”");
                toolStripMenuItem4.Name = Language.LanguageStrategy.AddToText + Language.LanguageStrategy.DoubleQuotationMarks_Left + Language.LanguageStrategy.StatusBarText + Language.LanguageStrategy.DoubleQuotationMarks_Right;
                toolStripMenuItem4.Text = toolStripMenuItem4.Name;
                toolStripMenuItem4.Tag = this.m_DockBarManager.StatusBar;
                for (i = 0; i < this.m_DockBarManager.StatusBar.Items.Count; i++)
                {
                    MenuItem item = new MenuItem();//("第 " + i.ToString() + " 位", null, InsertBaseItem_Click);
                    item.Name = Language.LanguageStrategy.InText + i.ToString() + Language.LanguageStrategy.PositionText;
                    item.Text = item.Name;
                    item.Click += new EventHandler(InsertBaseItem_Click);
                    item.Tag = i;
                    toolStripMenuItem4.DropDownItems.Add(item);
                    //
                    this.SetDockBarList(toolStripMenuItem4.DropDownItems, this.m_DockBarManager.StatusBar.Items[i] as ICustomize);
                }
                MenuItem item4 = new MenuItem();//("第 " + i.ToString() + " 位", null, InsertBaseItem_Click);
                item4.Name = Language.LanguageStrategy.InText + i.ToString() + Language.LanguageStrategy.PositionText;
                item4.Text = item4.Name;
                item4.Click += new EventHandler(InsertBaseItem_Click);
                item4.Tag = i;
                toolStripMenuItem4.DropDownItems.Add(item4);
                //
                if (i > 0)
                {
                    toolStripMenuItem4.DropDownItems.Add(new SeparatorItem());
                    MenuItem resetItem = new MenuItem();//"重置"
                    resetItem.Name = Language.LanguageStrategy.ResetText;
                    resetItem.Text = resetItem.Name;
                    resetItem.Click += new EventHandler(ResetItem_Click);
                    toolStripMenuItem4.DropDownItems.Add(resetItem);
                }
                //
                this.Items.Add(toolStripMenuItem4);
            }
        }
        private void SetDockBarList(FlexibleToolStripItemCollection items, ICustomize pCustomize)
        {
            if (pCustomize != null)
            {
                int i = 0;
                MenuItem toolStripMenuItem = new MenuItem();//("项“" + pCustomize.Text + "”");
                toolStripMenuItem.Name = Language.LanguageStrategy.AddToText + Language.LanguageStrategy.DoubleQuotationMarks_Left + pCustomize.Text + Language.LanguageStrategy.DoubleQuotationMarks_Right;
                toolStripMenuItem.Text = toolStripMenuItem.Name;
                toolStripMenuItem.Tag = pCustomize;
                for (i = 0; i < ((pCustomize is ToolBar) ? pCustomize.Items.Count - 1: pCustomize.Items.Count); i++)
                {
                    MenuItem item = new MenuItem();//("第 " + i.ToString() + " 位", null, InsertBaseItem_Click);
                    item.Name = Language.LanguageStrategy.InText + i.ToString() + Language.LanguageStrategy.PositionText;
                    item.Text = item.Name;
                    item.Click += new EventHandler(InsertBaseItem_Click);
                    item.Tag = i;
                    toolStripMenuItem.DropDownItems.Add(item);
                    //
                    this.SetDockBarList(toolStripMenuItem.DropDownItems, pCustomize.Items[i] as ICustomize);
                }
                MenuItem item2 = new MenuItem();//("第 " + i.ToString() + " 位", null, InsertBaseItem_Click);
                item2.Name = Language.LanguageStrategy.InText + i.ToString() + Language.LanguageStrategy.PositionText;
                item2.Text = item2.Name;
                item2.Click += new EventHandler(InsertBaseItem_Click);
                item2.Tag = i;
                toolStripMenuItem.DropDownItems.Add(item2);
                //
                if (i > 0)
                {
                    toolStripMenuItem.DropDownItems.Add(new SeparatorItem());
                    MenuItem resetItem = new MenuItem();//"重置"
                    resetItem.Name = Language.LanguageStrategy.ResetText;
                    resetItem.Text = resetItem.Name;
                    resetItem.Click += new EventHandler(ResetItem_Click);
                    toolStripMenuItem.DropDownItems.Add(resetItem);
                }
                //
                items.Add(new SeparatorItem());
                items.Add(toolStripMenuItem);
                items.Add(new SeparatorItem());
            }
        }

        private void InsertBaseItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;
            if (toolStripMenuItem == null || toolStripMenuItem.OwnerItem == null) return;
            int index = (int)toolStripMenuItem.Tag;
            ICustomize pCustomize = toolStripMenuItem.OwnerItem.Tag as ICustomize;
            if (pCustomize == null) return;
            pCustomize.AddCustomizeBaseItemEx(index, this.m_pBaseItem);
        }

        private void ResetItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;
            if (toolStripMenuItem == null || toolStripMenuItem.OwnerItem == null) return;
            ICustomize pCustomize = toolStripMenuItem.OwnerItem.Tag as ICustomize;
            if (pCustomize == null) return;
            pCustomize.Reset();
        }

    }
}
