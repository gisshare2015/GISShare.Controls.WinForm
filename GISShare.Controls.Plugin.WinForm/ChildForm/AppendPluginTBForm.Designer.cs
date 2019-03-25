namespace GISShare.Controls.Plugin.WinForm
{
    partial class AppendPluginTBForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AppendPluginTBForm));
            this.btnCancel = new GISShare.Controls.WinForm.WFNew.ButtonItem();
            this.btnOk = new GISShare.Controls.WinForm.WFNew.ButtonItem();
            this.btnOpen = new GISShare.Controls.WinForm.WFNew.ButtonItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.baseItemHost3 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            this.baseItemHost4 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            this.rbUse = new GISShare.Controls.WinForm.WFNew.RadioButtonItem();
            this.baseItemHost5 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            this.rbCut = new GISShare.Controls.WinForm.WFNew.RadioButtonItem();
            this.baseItemHost6 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            this.rbCopy = new GISShare.Controls.WinForm.WFNew.RadioButtonItem();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.baseItemHost7 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            this.lblFileName = new GISShare.Controls.WinForm.WFNew.LabelItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.separator1 = new GISShare.Controls.WinForm.WFNew.SeparatorItem();
            this.baseItemHost1 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            this.baseItemHost2 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            this.baseItemHost8 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.AutoPlanTextRectangle = false;
            this.btnCancel.Font = new System.Drawing.Font("宋体", 9F);
            this.btnCancel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnCancel.Image = null;
            this.btnCancel.LeftBottomRadius = 3;
            this.btnCancel.LeftTopRadius = 3;
            this.btnCancel.Location = new System.Drawing.Point(0, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.RightBottomRadius = 3;
            this.btnCancel.RightTopRadius = 3;
            this.btnCancel.ShowNomalState = true;
            this.btnCancel.Size = new System.Drawing.Size(135, 40);
            this.btnCancel.Tag = null;
            this.btnCancel.Text = "取   消";
            this.btnCancel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnCancel_MouseClick);
            // 
            // btnOk
            // 
            this.btnOk.AutoPlanTextRectangle = false;
            this.btnOk.Font = new System.Drawing.Font("宋体", 9F);
            this.btnOk.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnOk.Image = null;
            this.btnOk.LeftBottomRadius = 3;
            this.btnOk.LeftTopRadius = 3;
            this.btnOk.Location = new System.Drawing.Point(0, 0);
            this.btnOk.Name = "btnOk";
            this.btnOk.RightBottomRadius = 3;
            this.btnOk.RightTopRadius = 3;
            this.btnOk.ShowNomalState = true;
            this.btnOk.Size = new System.Drawing.Size(135, 40);
            this.btnOk.Tag = null;
            this.btnOk.Text = "加   载";
            this.btnOk.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnOk_MouseClick);
            // 
            // btnOpen
            // 
            this.btnOpen.AutoPlanTextRectangle = false;
            this.btnOpen.Font = new System.Drawing.Font("宋体", 9F);
            this.btnOpen.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnOpen.Image = null;
            this.btnOpen.LeftBottomRadius = 3;
            this.btnOpen.LeftTopRadius = 3;
            this.btnOpen.Location = new System.Drawing.Point(0, 0);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.RightBottomRadius = 3;
            this.btnOpen.RightTopRadius = 3;
            this.btnOpen.ShowNomalState = true;
            this.btnOpen.Size = new System.Drawing.Size(80, 34);
            this.btnOpen.Tag = null;
            this.btnOpen.Text = ">>";
            this.btnOpen.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnOpen_MouseClick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 10);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(196, 294);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 83;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.baseItemHost3);
            this.groupBox1.Controls.Add(this.baseItemHost4);
            this.groupBox1.Controls.Add(this.baseItemHost5);
            this.groupBox1.Controls.Add(this.baseItemHost6);
            this.groupBox1.Controls.Add(this.txtFileName);
            this.groupBox1.Controls.Add(this.baseItemHost7);
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.groupBox1.Location = new System.Drawing.Point(230, 12);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(477, 213);
            this.groupBox1.TabIndex = 82;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "插 件";
            // 
            // baseItemHost3
            // 
            this.baseItemHost3.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.baseItemHost3.BackColor = System.Drawing.Color.Transparent;
            this.baseItemHost3.BaseItemObject = this.btnOpen;
            this.baseItemHost3.Location = new System.Drawing.Point(375, 166);
            this.baseItemHost3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.baseItemHost3.Name = "baseItemHost3";
            this.baseItemHost3.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost3.Size = new System.Drawing.Size(80, 34);
            this.baseItemHost3.TabIndex = 87;
            // 
            // baseItemHost4
            // 
            this.baseItemHost4.BaseItemObject = this.rbUse;
            this.baseItemHost4.Location = new System.Drawing.Point(16, 75);
            this.baseItemHost4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.baseItemHost4.Name = "baseItemHost4";
            this.baseItemHost4.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost4.Size = new System.Drawing.Size(70, 24);
            this.baseItemHost4.TabIndex = 88;
            // 
            // rbUse
            // 
            this.rbUse.Checked = true;
            this.rbUse.Font = new System.Drawing.Font("宋体", 9F);
            this.rbUse.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rbUse.Location = new System.Drawing.Point(0, 0);
            this.rbUse.Name = "rbUse";
            this.rbUse.Size = new System.Drawing.Size(70, 24);
            this.rbUse.Tag = null;
            this.rbUse.Text = "使用";
            // 
            // baseItemHost5
            // 
            this.baseItemHost5.BaseItemObject = this.rbCut;
            this.baseItemHost5.Location = new System.Drawing.Point(16, 177);
            this.baseItemHost5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.baseItemHost5.Name = "baseItemHost5";
            this.baseItemHost5.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost5.Size = new System.Drawing.Size(70, 24);
            this.baseItemHost5.TabIndex = 4;
            // 
            // rbCut
            // 
            this.rbCut.Font = new System.Drawing.Font("宋体", 9F);
            this.rbCut.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rbCut.Location = new System.Drawing.Point(0, 0);
            this.rbCut.Name = "rbCut";
            this.rbCut.Size = new System.Drawing.Size(70, 24);
            this.rbCut.Tag = null;
            this.rbCut.Text = "剪切";
            // 
            // baseItemHost6
            // 
            this.baseItemHost6.BaseItemObject = this.rbCopy;
            this.baseItemHost6.Location = new System.Drawing.Point(16, 120);
            this.baseItemHost6.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.baseItemHost6.Name = "baseItemHost6";
            this.baseItemHost6.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost6.Size = new System.Drawing.Size(70, 24);
            this.baseItemHost6.TabIndex = 89;
            // 
            // rbCopy
            // 
            this.rbCopy.Font = new System.Drawing.Font("宋体", 9F);
            this.rbCopy.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rbCopy.Location = new System.Drawing.Point(0, 0);
            this.rbCopy.Name = "rbCopy";
            this.rbCopy.Size = new System.Drawing.Size(70, 24);
            this.rbCopy.Tag = null;
            this.rbCopy.Text = "复制";
            // 
            // txtFileName
            // 
            this.txtFileName.BackColor = System.Drawing.SystemColors.Control;
            this.txtFileName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFileName.Location = new System.Drawing.Point(105, 28);
            this.txtFileName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtFileName.Multiline = true;
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.ReadOnly = true;
            this.txtFileName.Size = new System.Drawing.Size(348, 126);
            this.txtFileName.TabIndex = 0;
            // 
            // baseItemHost7
            // 
            this.baseItemHost7.BaseItemObject = this.lblFileName;
            this.baseItemHost7.Location = new System.Drawing.Point(14, 33);
            this.baseItemHost7.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.baseItemHost7.Name = "baseItemHost7";
            this.baseItemHost7.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost7.Size = new System.Drawing.Size(99, 24);
            this.baseItemHost7.TabIndex = 2;
            // 
            // lblFileName
            // 
            this.lblFileName.Font = new System.Drawing.Font("宋体", 9F);
            this.lblFileName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.lblFileName.Location = new System.Drawing.Point(0, 0);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(99, 24);
            this.lblFileName.Tag = null;
            this.lblFileName.Text = "插件对象：";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // separator1
            // 
            this.separator1.AutoLayout = true;
            this.separator1.eOrientation = System.Windows.Forms.Orientation.Horizontal;
            this.separator1.Font = new System.Drawing.Font("宋体", 9F);
            this.separator1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.separator1.Location = new System.Drawing.Point(145, 161);
            this.separator1.LockHeight = true;
            this.separator1.MinimumSize = new System.Drawing.Size(3, 3);
            this.separator1.Name = "separator1";
            this.separator1.Size = new System.Drawing.Size(331, 3);
            this.separator1.Tag = null;
            this.separator1.Text = "separator1";
            // 
            // baseItemHost1
            // 
            this.baseItemHost1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.baseItemHost1.BackColor = System.Drawing.Color.Transparent;
            this.baseItemHost1.BaseItemObject = this.btnCancel;
            this.baseItemHost1.Location = new System.Drawing.Point(246, 266);
            this.baseItemHost1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.baseItemHost1.Name = "baseItemHost1";
            this.baseItemHost1.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost1.Size = new System.Drawing.Size(135, 40);
            this.baseItemHost1.TabIndex = 86;
            // 
            // baseItemHost2
            // 
            this.baseItemHost2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.baseItemHost2.BackColor = System.Drawing.Color.Transparent;
            this.baseItemHost2.BaseItemObject = this.btnOk;
            this.baseItemHost2.Location = new System.Drawing.Point(549, 266);
            this.baseItemHost2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.baseItemHost2.Name = "baseItemHost2";
            this.baseItemHost2.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost2.Size = new System.Drawing.Size(135, 40);
            this.baseItemHost2.TabIndex = 85;
            // 
            // baseItemHost8
            // 
            this.baseItemHost8.BackColor = System.Drawing.Color.Transparent;
            this.baseItemHost8.BaseItemObject = null;
            this.baseItemHost8.Location = new System.Drawing.Point(218, 242);
            this.baseItemHost8.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.baseItemHost8.Name = "baseItemHost8";
            this.baseItemHost8.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost8.Size = new System.Drawing.Size(496, 4);
            this.baseItemHost8.TabIndex = 87;
            // 
            // AppendPluginTBForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelItemsEventNC = false;
            this.ClientSize = new System.Drawing.Size(752, 350);
            this.Controls.Add(this.baseItemHost8);
            this.Controls.Add(this.baseItemHost1);
            this.Controls.Add(this.baseItemHost2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.Location = new System.Drawing.Point(0, 0);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AppendPluginTBForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "加载插件";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private GISShare.Controls.WinForm.WFNew.LabelItem lblFileName;
        private GISShare.Controls.WinForm.WFNew.ButtonItem btnCancel;
        private GISShare.Controls.WinForm.WFNew.ButtonItem btnOk;
        private GISShare.Controls.WinForm.WFNew.ButtonItem btnOpen;
        private GISShare.Controls.WinForm.WFNew.RadioButtonItem rbCut;
        private GISShare.Controls.WinForm.WFNew.RadioButtonItem rbCopy;
        private GISShare.Controls.WinForm.WFNew.RadioButtonItem rbUse;
        private Controls.WinForm.WFNew.SeparatorItem separator1;
        private Controls.WinForm.WFNew.BaseItemHost baseItemHost1;
        private Controls.WinForm.WFNew.BaseItemHost baseItemHost2;
        private Controls.WinForm.WFNew.BaseItemHost baseItemHost3;
        private Controls.WinForm.WFNew.BaseItemHost baseItemHost4;
        private Controls.WinForm.WFNew.BaseItemHost baseItemHost5;
        private Controls.WinForm.WFNew.BaseItemHost baseItemHost6;
        private Controls.WinForm.WFNew.BaseItemHost baseItemHost7;
        private Controls.WinForm.WFNew.BaseItemHost baseItemHost8;
    }
}