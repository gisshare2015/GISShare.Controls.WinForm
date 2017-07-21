using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.DockBar
{
    class CustomizeToolBarForm : Form
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
            this.label_Name = new System.Windows.Forms.Label();
            this.textBox_Name = new System.Windows.Forms.TextBox();
            this.button_Ok = new GISShare.Controls.WinForm.ButtonX();
            this.button_Cancel = new GISShare.Controls.WinForm.ButtonX();
            this.SuspendLayout();
            // 
            // label_Name
            // 
            this.label_Name.AutoSize = true;
            this.label_Name.ForeColor = WinFormRenderer.WinFormRendererStrategy.WinFormColorTable.ItemText;
            this.label_Name.Location = new System.Drawing.Point(10, 9);
            this.label_Name.Name = "label_Name";
            this.label_Name.Size = new System.Drawing.Size(77, 12);
            this.label_Name.TabIndex = 0;
            this.label_Name.Text = "工具条名称：";
            // 
            // textBox_Name
            // 
            this.textBox_Name.Location = new System.Drawing.Point(13, 28);
            this.textBox_Name.Name = "textBox_Name";
            this.textBox_Name.Size = new System.Drawing.Size(315, 21);
            this.textBox_Name.TabIndex = 1;
            // 
            // button_Ok
            // 
            this.button_Ok.AutoPlanTextRectangle = false;
            this.button_Ok.BackColor = System.Drawing.Color.Transparent;
            this.button_Ok.Location = new System.Drawing.Point(144, 54);
            this.button_Ok.Name = "button_Ok";
            this.button_Ok.Size = new System.Drawing.Size(90, 21);
            this.button_Ok.TabIndex = 2;
            this.button_Ok.Text = "确 定";
            this.button_Ok.UseVisualStyleBackColor = true;
            this.button_Ok.Click += new System.EventHandler(this.button_Ok_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.AutoPlanTextRectangle = false;
            this.button_Cancel.BackColor = System.Drawing.Color.Transparent;
            this.button_Cancel.Location = new System.Drawing.Point(239, 54);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(90, 21);
            this.button_Cancel.TabIndex = 3;
            this.button_Cancel.Text = "取 消";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // CustomizeToolBarForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 81);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_Ok);
            this.Controls.Add(this.textBox_Name);
            this.Controls.Add(this.label_Name);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CustomizeToolBarForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CustomizeToolBarForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_Name;
        private System.Windows.Forms.TextBox textBox_Name;
        private GISShare.Controls.WinForm.ButtonX button_Ok;
        private GISShare.Controls.WinForm.ButtonX button_Cancel;

        //
        //
        //
        //
        //

        private bool m_IsCreateCustomizeToolBar = true;
        private ToolBar m_ToolBar = null;
        private DockBarManager m_DockBarManager = null;

        public CustomizeToolBarForm(bool isCreateCustomizeToolBar, DockBarManager dockBarManager, string name, ToolBar toolBar)
        {
            InitializeComponent();
            //
            this.label_Name.Text = Language.LanguageStrategy.CreateOrModifyForm_LabelameText;
            this.button_Ok.Text = Language.LanguageStrategy.CreateOrModifyForm_ButtonOkText;
            this.button_Cancel.Text = Language.LanguageStrategy.CreateOrModifyForm_ButtonCancelText;
            //
            this.m_ToolBar = toolBar;
            this.m_DockBarManager = dockBarManager;
            this.m_IsCreateCustomizeToolBar = isCreateCustomizeToolBar;
            //
            if (this.m_IsCreateCustomizeToolBar) { this.Text = Language.LanguageStrategy.CreateOrModifyFormTitle_Create; this.textBox_Name.Text = name; }// "新建工具栏"
            else { this.Text = Language.LanguageStrategy.CreateOrModifyFormTitle_Modify; this.textBox_Name.Text = this.m_ToolBar.Text; }//  "重命名工具栏"
        }

        private void button_Ok_Click(object sender, EventArgs e)
        {
            if (this.m_IsCreateCustomizeToolBar) { this.m_DockBarManager.GetEmptyCustomizeToolBar(this.textBox_Name.Text); }
            else { this.m_ToolBar.Text = this.textBox_Name.Text; }//this.Text = "重命名工具栏"; 
            //
            this.Close();
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}