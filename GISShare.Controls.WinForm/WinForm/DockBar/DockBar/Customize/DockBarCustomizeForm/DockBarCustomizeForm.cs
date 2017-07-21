using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.DockBar
{
    class DockBarCustomizeForm : Form//GISShare.Controls.WinForm.FormX //
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            GISShare.Controls.WinForm.WinFormColorTable winFormColorTable1 = new GISShare.Controls.WinForm.WinFormColorTable();
            this.tabControl1 = new GISShare.Controls.WinForm.TabControlX();
            this.tabPage_Bar = new System.Windows.Forms.TabPage();
            this.checkBox_ShowLargeImage = new GISShare.Controls.WinForm.CheckBoxX();
            this.checkBox_ShowItemToolTips = new GISShare.Controls.WinForm.CheckBoxX();
            this.button_ResetBar = new GISShare.Controls.WinForm.ButtonX();
            this.button_DeleteBar = new GISShare.Controls.WinForm.ButtonX();
            this.button_RenameBar = new GISShare.Controls.WinForm.ButtonX();
            this.button_NewBar = new GISShare.Controls.WinForm.ButtonX();
            this.label_Bar = new System.Windows.Forms.Label();
            this.checkedListBox_Bar = new GISShare.Controls.WinForm.CheckedListBoxX();
            this.tabPage_Item = new System.Windows.Forms.TabPage();
            this.baseItemListBox_Item = new GISShare.Controls.WinForm.ListBoxX();
            this.label_Tip = new System.Windows.Forms.Label();
            this.listBox_Category = new GISShare.Controls.WinForm.ListBoxX();
            this.label_Item = new System.Windows.Forms.Label();
            this.label_Category = new System.Windows.Forms.Label();
            this.button_Cancel = new GISShare.Controls.WinForm.ButtonX();
            this.tabControl1.SuspendLayout();
            this.tabPage_Bar.SuspendLayout();
            this.tabPage_Item.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage_Bar);
            this.tabControl1.Controls.Add(this.tabPage_Item);
            this.tabControl1.Location = new System.Drawing.Point(9, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(403, 286);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage_Bar
            // 
            this.tabPage_Bar.Controls.Add(this.checkBox_ShowLargeImage);
            this.tabPage_Bar.Controls.Add(this.checkBox_ShowItemToolTips);
            this.tabPage_Bar.Controls.Add(this.button_ResetBar);
            this.tabPage_Bar.Controls.Add(this.button_DeleteBar);
            this.tabPage_Bar.Controls.Add(this.button_RenameBar);
            this.tabPage_Bar.Controls.Add(this.button_NewBar);
            this.tabPage_Bar.Controls.Add(this.label_Bar);
            this.tabPage_Bar.Controls.Add(this.checkedListBox_Bar);
            this.tabPage_Bar.Location = new System.Drawing.Point(4, 26);
            this.tabPage_Bar.Name = "tabPage_Bar";
            this.tabPage_Bar.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Bar.Size = new System.Drawing.Size(395, 256);
            this.tabPage_Bar.TabIndex = 0;
            this.tabPage_Bar.Text = "工具栏";
            this.tabPage_Bar.UseVisualStyleBackColor = true;
            // 
            // checkBox_ShowLargeImage
            // 
            this.checkBox_ShowLargeImage.AutoSize = true;
            this.checkBox_ShowLargeImage.Location = new System.Drawing.Point(7, 220);
            this.checkBox_ShowLargeImage.Name = "checkBox_ShowLargeImage";
            this.checkBox_ShowLargeImage.Size = new System.Drawing.Size(84, 16);
            this.checkBox_ShowLargeImage.TabIndex = 6;
            this.checkBox_ShowLargeImage.Text = "使用大图标";
            this.checkBox_ShowLargeImage.UseVisualStyleBackColor = true;
            this.checkBox_ShowLargeImage.VOffset = -1;
            // 
            // checkBox_ShowItemToolTips
            // 
            this.checkBox_ShowItemToolTips.AutoSize = true;
            this.checkBox_ShowItemToolTips.Checked = true;
            this.checkBox_ShowItemToolTips.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_ShowItemToolTips.Location = new System.Drawing.Point(7, 237);
            this.checkBox_ShowItemToolTips.Name = "checkBox_ShowItemToolTips";
            this.checkBox_ShowItemToolTips.Size = new System.Drawing.Size(96, 16);
            this.checkBox_ShowItemToolTips.TabIndex = 3;
            this.checkBox_ShowItemToolTips.Text = "显示提示标签";
            this.checkBox_ShowItemToolTips.UseVisualStyleBackColor = true;
            this.checkBox_ShowItemToolTips.VOffset = -1;
            // 
            // button_ResetBar
            // 
            this.button_ResetBar.AutoPlanTextRectangle = false;
            this.button_ResetBar.BackColor = System.Drawing.Color.Transparent;
            this.button_ResetBar.Enabled = false;
            this.button_ResetBar.Location = new System.Drawing.Point(307, 109);
            this.button_ResetBar.Name = "button_ResetBar";
            this.button_ResetBar.Size = new System.Drawing.Size(83, 23);
            this.button_ResetBar.TabIndex = 5;
            this.button_ResetBar.Text = "重置";
            this.button_ResetBar.UseVisualStyleBackColor = true;
            // 
            // button_DeleteBar
            // 
            this.button_DeleteBar.AutoPlanTextRectangle = false;
            this.button_DeleteBar.BackColor = System.Drawing.Color.Transparent;
            this.button_DeleteBar.Enabled = false;
            this.button_DeleteBar.Location = new System.Drawing.Point(307, 80);
            this.button_DeleteBar.Name = "button_DeleteBar";
            this.button_DeleteBar.Size = new System.Drawing.Size(83, 23);
            this.button_DeleteBar.TabIndex = 4;
            this.button_DeleteBar.Text = "删除";
            this.button_DeleteBar.UseVisualStyleBackColor = true;
            // 
            // button_RenameBar
            // 
            this.button_RenameBar.AutoPlanTextRectangle = false;
            this.button_RenameBar.BackColor = System.Drawing.Color.Transparent;
            this.button_RenameBar.Enabled = false;
            this.button_RenameBar.Location = new System.Drawing.Point(307, 51);
            this.button_RenameBar.Name = "button_RenameBar";
            this.button_RenameBar.Size = new System.Drawing.Size(83, 23);
            this.button_RenameBar.TabIndex = 3;
            this.button_RenameBar.Text = "重命名";
            this.button_RenameBar.UseVisualStyleBackColor = true;
            // 
            // button_NewBar
            // 
            this.button_NewBar.AutoPlanTextRectangle = false;
            this.button_NewBar.BackColor = System.Drawing.Color.Transparent;
            this.button_NewBar.Location = new System.Drawing.Point(307, 22);
            this.button_NewBar.Name = "button_NewBar";
            this.button_NewBar.Size = new System.Drawing.Size(83, 23);
            this.button_NewBar.TabIndex = 2;
            this.button_NewBar.Text = "新建";
            this.button_NewBar.UseVisualStyleBackColor = true;
            // 
            // label_Bar
            // 
            this.label_Bar.AutoSize = true;
            winFormColorTable1.UseSystemColors = false;
            this.label_Bar.ForeColor = winFormColorTable1.ItemText;
            this.label_Bar.Location = new System.Drawing.Point(7, 7);
            this.label_Bar.Name = "label_Bar";
            this.label_Bar.Size = new System.Drawing.Size(53, 12);
            this.label_Bar.TabIndex = 1;
            this.label_Bar.Text = "工具条：";
            // 
            // checkedListBox_Bar
            // 
            this.checkedListBox_Bar.FormattingEnabled = true;
            this.checkedListBox_Bar.Location = new System.Drawing.Point(7, 22);
            this.checkedListBox_Bar.Name = "checkedListBox_Bar";
            this.checkedListBox_Bar.AutoMouseMoveSeleced = true;
            this.checkedListBox_Bar.Size = new System.Drawing.Size(295, 196);
            this.checkedListBox_Bar.TabIndex = 0;
            // 
            // tabPage_Item
            // 
            this.tabPage_Item.Controls.Add(this.baseItemListBox_Item);
            this.tabPage_Item.Controls.Add(this.label_Tip);
            this.tabPage_Item.Controls.Add(this.listBox_Category);
            this.tabPage_Item.Controls.Add(this.label_Item);
            this.tabPage_Item.Controls.Add(this.label_Category);
            this.tabPage_Item.Location = new System.Drawing.Point(4, 26);
            this.tabPage_Item.Name = "tabPage_Item";
            this.tabPage_Item.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Item.Size = new System.Drawing.Size(395, 256);
            this.tabPage_Item.TabIndex = 1;
            this.tabPage_Item.Text = "命令";
            this.tabPage_Item.UseVisualStyleBackColor = true;
            // 
            // baseItemListBox_Item
            // 
            this.baseItemListBox_Item.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.baseItemListBox_Item.FormattingEnabled = true;
            this.baseItemListBox_Item.ImageSpace = 2;
            this.baseItemListBox_Item.ItemHeight = 21;
            this.baseItemListBox_Item.Location = new System.Drawing.Point(165, 22);
            this.baseItemListBox_Item.Name = "baseItemListBox_Item";
            this.baseItemListBox_Item.ShowGripRegion = true;
            this.baseItemListBox_Item.AutoMouseMoveSeleced = true;
            this.baseItemListBox_Item.Size = new System.Drawing.Size(222, 208);
            this.baseItemListBox_Item.TabIndex = 5;
            // 
            // label_Tip
            // 
            this.label_Tip.AutoSize = true;
            this.label_Tip.ForeColor = winFormColorTable1.ItemText;
            this.label_Tip.Location = new System.Drawing.Point(10, 236);
            this.label_Tip.Name = "label_Tip";
            this.label_Tip.Size = new System.Drawing.Size(269, 12);
            this.label_Tip.TabIndex = 4;
            this.label_Tip.Text = "若要添加命令，请在对应的命令上单击鼠标右键。";
            // 
            // listBox_Category
            // 
            this.listBox_Category.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.listBox_Category.FormattingEnabled = true;
            this.listBox_Category.ItemHeight = 21;
            this.listBox_Category.Location = new System.Drawing.Point(6, 22);
            this.listBox_Category.Name = "listBox_Category";
            this.listBox_Category.AutoMouseMoveSeleced = true;
            this.listBox_Category.Size = new System.Drawing.Size(150, 208);
            this.listBox_Category.TabIndex = 2;
            // 
            // label_Item
            // 
            this.label_Item.AutoSize = true;
            this.label_Item.ForeColor = winFormColorTable1.ItemText;
            this.label_Item.Location = new System.Drawing.Point(165, 7);
            this.label_Item.Name = "label_Item";
            this.label_Item.Size = new System.Drawing.Size(41, 12);
            this.label_Item.TabIndex = 1;
            this.label_Item.Text = "命令：";
            // 
            // label_Category
            // 
            this.label_Category.AutoSize = true;
            this.label_Category.ForeColor = winFormColorTable1.ItemText;
            this.label_Category.Location = new System.Drawing.Point(7, 7);
            this.label_Category.Name = "label_Category";
            this.label_Category.Size = new System.Drawing.Size(41, 12);
            this.label_Category.TabIndex = 0;
            this.label_Category.Text = "类别：";
            // 
            // button_Cancel
            // 
            this.button_Cancel.AutoPlanTextRectangle = false;
            this.button_Cancel.BackColor = System.Drawing.Color.Transparent;
            this.button_Cancel.Location = new System.Drawing.Point(310, 300);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(90, 28);
            this.button_Cancel.TabIndex = 2;
            this.button_Cancel.Text = "关  闭";
            this.button_Cancel.UseVisualStyleBackColor = true;
            // 
            // DockBarCustomizeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 333);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DockBarCustomizeForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "自定义";
            this.tabControl1.ResumeLayout(false);
            this.tabPage_Bar.ResumeLayout(false);
            this.tabPage_Bar.PerformLayout();
            this.tabPage_Item.ResumeLayout(false);
            this.tabPage_Item.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private GISShare.Controls.WinForm.TabControlX tabControl1;
        private System.Windows.Forms.TabPage tabPage_Bar;
        private System.Windows.Forms.TabPage tabPage_Item;
        private GISShare.Controls.WinForm.CheckedListBoxX checkedListBox_Bar;
        private GISShare.Controls.WinForm.ButtonX button_DeleteBar;
        private GISShare.Controls.WinForm.ButtonX button_RenameBar;
        private GISShare.Controls.WinForm.ButtonX button_NewBar;
        private System.Windows.Forms.Label label_Bar;
        private GISShare.Controls.WinForm.ButtonX button_ResetBar;
        private GISShare.Controls.WinForm.ListBoxX listBox_Category;
        private System.Windows.Forms.Label label_Item;
        private System.Windows.Forms.Label label_Category;
        private System.Windows.Forms.Label label_Tip;
        private GISShare.Controls.WinForm.ButtonX button_Cancel;
        private GISShare.Controls.WinForm.CheckBoxX checkBox_ShowItemToolTips;
        private GISShare.Controls.WinForm.CheckBoxX checkBox_ShowLargeImage;
        private GISShare.Controls.WinForm.ListBoxX baseItemListBox_Item;

        //
        //
        //
        //
        //

        private bool m_Stop_CheckedListBoxBarItemCheck = false;
        private IDockBar m_pDockBar = null;
        private DockBarManager m_DockBarManager = null;

        public DockBarCustomizeForm(DockBarManager dockBarManager)
        {
            InitializeComponent();
            //
            this.Text = Language.LanguageStrategy.CustomizeFormTitle;
            this.button_Cancel.Text = Language.LanguageStrategy.CustomizeForm_ButtonCancelText;
            this.tabPage_Bar.Text = Language.LanguageStrategy.CustomizeForm_TabPageBarText;
            this.label_Bar.Text = Language.LanguageStrategy.CustomizeForm_TabPageBar_LabelBarText;
            this.button_NewBar.Text = Language.LanguageStrategy.CustomizeForm_TabPageBar_ButtonewText;
            this.button_RenameBar.Text = Language.LanguageStrategy.CustomizeForm_TabPageBar_ButtonRenameText;
            this.button_DeleteBar.Text = Language.LanguageStrategy.CustomizeForm_TabPageBar_ButtonDeleteText;
            this.button_ResetBar.Text = Language.LanguageStrategy.CustomizeForm_TabPageBar_ButtonResetText;
            this.checkBox_ShowLargeImage.Text = Language.LanguageStrategy.CustomizeForm_TabPageBar_CheckBoxLargeImageText;
            this.checkBox_ShowItemToolTips.Text = Language.LanguageStrategy.CustomizeForm_TabPageBar_CheckBoxToolTipText;
            this.tabPage_Item.Text = Language.LanguageStrategy.CustomizeForm_TabPageItemText;
            this.label_Category.Text = Language.LanguageStrategy.CustomizeForm_TabPageItem_LabelCategoryText;
            this.label_Item.Text = Language.LanguageStrategy.CustomizeForm_TabPageItem_LabelItemText;
            this.label_Tip.Text = Language.LanguageStrategy.CustomizeForm_TabPageItem_LabelTipText;
            //
            this.m_DockBarManager = dockBarManager;
            //
            this.SetDockBarCustomizeForm();
            //
            //
            //
            this.checkedListBox_Bar.SelectedIndexChanged += new System.EventHandler(this.checkedListBox_Bar_SelectedIndexChanged);
            this.checkedListBox_Bar.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBox_Bar_ItemCheck);
            this.button_ResetBar.Click += new System.EventHandler(this.button_ResetBar_Click);
            this.button_DeleteBar.Click += new System.EventHandler(this.button_DeleteBar_Click);
            this.button_RenameBar.Click += new System.EventHandler(this.button_RenameBar_Click);
            this.button_NewBar.Click += new System.EventHandler(this.button_NewBar_Click);
            this.baseItemListBox_Item.MouseDown += new System.Windows.Forms.MouseEventHandler(this.baseItemListBox_Item_MouseDown);
            this.checkBox_ShowLargeImage.CheckedChanged += new System.EventHandler(this.checkBox_ShowLargeImage_CheckedChanged);
            this.checkBox_ShowItemToolTips.CheckedChanged += new System.EventHandler(this.checkBox_ShowItemToolTips_CheckedChanged);
            //
            this.listBox_Category.SelectedIndexChanged += new System.EventHandler(this.listBox_Category_SelectedIndexChanged);
            //
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
        }
        private void SetDockBarCustomizeForm()
        {
            this.checkedListBox_Bar.Items.Clear();
            if (this.m_DockBarManager.MenuBar != null) this.checkedListBox_Bar.Items.Add(Language.LanguageStrategy.MainMenuText, this.m_DockBarManager.MenuBar.VisibleEx);//"主菜单"
            if (this.m_DockBarManager.StatusBar != null) this.checkedListBox_Bar.Items.Add(Language.LanguageStrategy.StatusBarText, this.m_DockBarManager.StatusBar.VisibleEx);//"状态栏"
            foreach (ToolBar one in this.m_DockBarManager.ToolBars)
            {
                this.checkedListBox_Bar.Items.Add(one.Text, one.VisibleEx);
            }
            foreach (CustomizeToolBar one in this.m_DockBarManager.CustomizeToolBars)
            {
                this.checkedListBox_Bar.Items.Add(one.Text, one.VisibleEx);
            }
            this.checkBox_ShowLargeImage.Checked = this.m_DockBarManager.ShowLargeImage;
            this.checkBox_ShowItemToolTips.Checked =   this.m_DockBarManager.ShowItemToolTips;
            //
            //
            //
            this.listBox_Category.Items.Clear();
            foreach (IBaseItemDB one in this.m_DockBarManager.BaseItems)
            {
                if (this.listBox_Category.Items.Contains(one.Category)) continue;
                this.listBox_Category.Items.Add(one.Category);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //
            this.m_DockBarManager.SetIsCustomize(true);
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            //
            this.m_DockBarManager.SetIsCustomize(false);
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkedListBox_Bar_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = this.checkedListBox_Bar.SelectedIndex;
            if (index == 0)
            {
                if (this.m_DockBarManager.MenuBar != null)
                {
                    this.m_pDockBar = this.m_DockBarManager.MenuBar;
                }
                else if (this.m_DockBarManager.StatusBar != null)
                {
                    this.m_pDockBar = this.m_DockBarManager.StatusBar;
                }
                else if (index < this.m_DockBarManager.ToolBars.Count)
                {
                    this.m_pDockBar = this.m_DockBarManager.ToolBars[index];
                }
                else
                {
                    this.m_pDockBar = this.m_DockBarManager.CustomizeToolBars[index];
                }
            }
            else if (index == 1)
            {
                if (this.m_DockBarManager.MenuBar != null &&
                    this.m_DockBarManager.StatusBar != null)
                {
                    this.m_pDockBar = this.m_DockBarManager.StatusBar;
                }
                else if (index < this.m_DockBarManager.ToolBars.Count)
                {
                    this.m_pDockBar = this.m_DockBarManager.ToolBars[index];
                }
                else
                {
                    this.m_pDockBar = this.m_DockBarManager.CustomizeToolBars[index];
                }
            }
            else
            {
                if (this.m_DockBarManager.MenuBar != null) index--;
                if (this.m_DockBarManager.StatusBar != null) index--; 
                if (index < this.m_DockBarManager.ToolBars.Count)
                {
                    this.m_pDockBar = this.m_DockBarManager.ToolBars[index];
                }
                else
                {
                    index -= this.m_DockBarManager.ToolBars.Count;
                    this.m_pDockBar = this.m_DockBarManager.CustomizeToolBars[index];
                }
            }
            //
            if (this.m_pDockBar != null)
            {
                switch (this.m_pDockBar.eDockBarStyle)
                {
                    case DockBarStyle.eMenuBar:
                    case DockBarStyle.eToolBar:
                    case DockBarStyle.eStatusBar:
                        this.button_RenameBar.Enabled = false;
                        this.button_DeleteBar.Enabled = false;
                        this.button_ResetBar.Enabled = true;
                        break;
                    case DockBarStyle.eCustomizeToolBar:
                        this.button_RenameBar.Enabled = true;
                        this.button_DeleteBar.Enabled = true;
                        this.button_ResetBar.Enabled = true;
                        break;
                }
            }
            else
            {
                this.button_RenameBar.Enabled = false;
                this.button_DeleteBar.Enabled = false;
                this.button_ResetBar.Enabled = false;
            }
        }

        private void checkedListBox_Bar_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (this.m_Stop_CheckedListBoxBarItemCheck) return;
            //
            if (this.m_pDockBar == null) return;
            //
            switch (e.NewValue) 
            {
                case CheckState.Checked:
                    this.m_pDockBar.VisibleEx = true;
                    break;
                default:
                    this.m_pDockBar.VisibleEx = false;
                    break;
            }
        }

        private void button_NewBar_Click(object sender, EventArgs e)
        {
            CustomizeToolBarForm CustomizeToolBarForm1 = new CustomizeToolBarForm(true, this.m_DockBarManager, Language.LanguageStrategy.CreateOrModifyForm_TextBoxameText, null);//"新建工具条"
            CustomizeToolBarForm1.ShowDialog();
            this.ResetCheckedListBoxBar();
        }

        private void button_RenameBar_Click(object sender, EventArgs e)
        {
            CustomizeToolBarForm CustomizeToolBarForm1 = new CustomizeToolBarForm(false, this.m_DockBarManager, "", this.m_pDockBar as ToolBar);//"重命名工具条"
            CustomizeToolBarForm1.ShowDialog();
            this.ResetCheckedListBoxBar();
        }

        private void button_DeleteBar_Click(object sender, EventArgs e)
        {
            CustomizeToolBar toolBar = this.m_pDockBar as CustomizeToolBar;
            toolBar.VisibleEx = false;
            toolBar.RemoveFromParent();
            this.m_DockBarManager.CustomizeToolBars.Remove(toolBar);
            for (int i = 0; i < toolBar.Items.Count - 1; i++)
            {
                toolBar.Items[i].Dispose();
            }
            ((ICustomize)toolBar).CustomizeBaseItems.Clear();
            toolBar.Dispose();
            //
            this.ResetCheckedListBoxBar();
        }

        private void ResetCheckedListBoxBar()
        {
            this.m_Stop_CheckedListBoxBarItemCheck = true;
            //
            this.checkedListBox_Bar.Items.Clear();
            if (this.m_DockBarManager.MenuBar != null) this.checkedListBox_Bar.Items.Add(Language.LanguageStrategy.MainMenuText, this.m_DockBarManager.MenuBar.VisibleEx);//"主菜单"
            if (this.m_DockBarManager.StatusBar != null) this.checkedListBox_Bar.Items.Add(Language.LanguageStrategy.StatusBarText, this.m_DockBarManager.StatusBar.VisibleEx);//"状态栏"
            foreach (ToolBar one in this.m_DockBarManager.ToolBars)
            {
                this.checkedListBox_Bar.Items.Add(one.Text, one.VisibleEx);
            }
            foreach (CustomizeToolBar one in this.m_DockBarManager.CustomizeToolBars)
            {
                this.checkedListBox_Bar.Items.Add(one.Text, one.VisibleEx);
            }
            this.button_RenameBar.Enabled = false;
            this.button_DeleteBar.Enabled = false;
            this.button_ResetBar.Enabled = false;
            //
            this.m_Stop_CheckedListBoxBarItemCheck = false;
        }

        private void button_ResetBar_Click(object sender, EventArgs e)
        {
            if (this.m_pDockBar == null) return;
            //
            this.m_pDockBar.Reset();
        }

        private void checkBox_ShowLargeImage_CheckedChanged(object sender, EventArgs e)
        {
            this.m_DockBarManager.ShowLargeImage = this.checkBox_ShowLargeImage.Checked;
        }

        private void checkBox_ShowItemToolTips_CheckedChanged(object sender, EventArgs e)
        {
            this.m_DockBarManager.ShowItemToolTips = this.checkBox_ShowItemToolTips.Checked;
        }

        private void listBox_Category_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listBox_Category.SelectedIndex < 0 || this.listBox_Category.SelectedIndex >= this.listBox_Category.Items.Count) return;
            string strCategory = this.listBox_Category.SelectedItem.ToString();
            this.baseItemListBox_Item.Items.Clear();
            foreach (IBaseItemDB one in this.m_DockBarManager.BaseItems)
            {
                if (one.Category != strCategory) continue;
                this.baseItemListBox_Item.Items.Add(one);
            }
        }

        private void baseItemListBox_Item_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            //
            if(this.baseItemListBox_Item.SelectedIndex < 0 || this.baseItemListBox_Item.SelectedIndex >= this.baseItemListBox_Item.Items.Count) return;
            IBaseItemDB pBaseItem = this.baseItemListBox_Item.Items[this.baseItemListBox_Item.SelectedIndex] as IBaseItemDB;
            if (pBaseItem == null) return;
            InsertBaseItemMenuStrip insertBaseItemMenuStrip = new InsertBaseItemMenuStrip(this.m_DockBarManager, pBaseItem);
            insertBaseItemMenuStrip.Show(this.baseItemListBox_Item, e.Location);
        }
    }
}