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
            this.btnCancel = new GISShare.Controls.WinForm.WFNew.ButtonN();
            this.btnOk = new GISShare.Controls.WinForm.WFNew.ButtonN();
            this.btnOpen = new GISShare.Controls.WinForm.WFNew.ButtonN();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbUse = new GISShare.Controls.WinForm.WFNew.RadioButtonN();
            this.rbCut = new GISShare.Controls.WinForm.WFNew.RadioButtonN();
            this.rbCopy = new GISShare.Controls.WinForm.WFNew.RadioButtonN();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.lblFileName = new GISShare.Controls.WinForm.WFNew.LabelN();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.separator1 = new GISShare.Controls.WinForm.WFNew.Separator();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.AutoPlanTextRectangle = false;
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.Image = null;
            this.btnCancel.LeftBottomRadius = 3;
            this.btnCancel.LeftTopRadius = 3;
            this.btnCancel.Location = new System.Drawing.Point(164, 177);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(0);
            this.btnCancel.RightBottomRadius = 3;
            this.btnCancel.RightTopRadius = 3;
            this.btnCancel.Size = new System.Drawing.Size(90, 27);
            this.btnCancel.TabIndex = 86;
            this.btnCancel.Text = "取   消";
            this.btnCancel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnCancel_MouseClick);
            // 
            // btnOk
            // 
            this.btnOk.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOk.AutoPlanTextRectangle = false;
            this.btnOk.BackColor = System.Drawing.Color.Transparent;
            this.btnOk.Image = null;
            this.btnOk.LeftBottomRadius = 3;
            this.btnOk.LeftTopRadius = 3;
            this.btnOk.Location = new System.Drawing.Point(366, 177);
            this.btnOk.Name = "btnOk";
            this.btnOk.Padding = new System.Windows.Forms.Padding(0);
            this.btnOk.RightBottomRadius = 3;
            this.btnOk.RightTopRadius = 3;
            this.btnOk.Size = new System.Drawing.Size(90, 27);
            this.btnOk.TabIndex = 85;
            this.btnOk.Text = "加   载";
            this.btnOk.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnOk_MouseClick);
            // 
            // btnOpen
            // 
            this.btnOpen.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOpen.AutoPlanTextRectangle = false;
            this.btnOpen.BackColor = System.Drawing.Color.Transparent;
            this.btnOpen.Image = null;
            this.btnOpen.LeftBottomRadius = 3;
            this.btnOpen.LeftTopRadius = 3;
            this.btnOpen.Location = new System.Drawing.Point(250, 111);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Padding = new System.Windows.Forms.Padding(0);
            this.btnOpen.RightBottomRadius = 3;
            this.btnOpen.RightTopRadius = 3;
            this.btnOpen.Size = new System.Drawing.Size(53, 23);
            this.btnOpen.TabIndex = 87;
            this.btnOpen.Text = ">>";
            this.btnOpen.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnOpen_MouseClick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(8, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(131, 197);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 83;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbUse);
            this.groupBox1.Controls.Add(this.btnOpen);
            this.groupBox1.Controls.Add(this.rbCut);
            this.groupBox1.Controls.Add(this.rbCopy);
            this.groupBox1.Controls.Add(this.txtFileName);
            this.groupBox1.Controls.Add(this.lblFileName);
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.groupBox1.Location = new System.Drawing.Point(153, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(318, 142);
            this.groupBox1.TabIndex = 82;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "插 件";
            // 
            // rbUse
            // 
            this.rbUse.Checked = true;
            this.rbUse.Location = new System.Drawing.Point(11, 50);
            this.rbUse.Name = "rbUse";
            this.rbUse.Padding = new System.Windows.Forms.Padding(0);
            this.rbUse.Size = new System.Drawing.Size(47, 16);
            this.rbUse.TabIndex = 88;
            this.rbUse.Text = "使用";
            // 
            // rbCut
            // 
            this.rbCut.Location = new System.Drawing.Point(11, 118);
            this.rbCut.Name = "rbCut";
            this.rbCut.Padding = new System.Windows.Forms.Padding(0);
            this.rbCut.Size = new System.Drawing.Size(47, 16);
            this.rbCut.TabIndex = 4;
            this.rbCut.Text = "剪切";
            // 
            // rbCopy
            // 
            this.rbCopy.Location = new System.Drawing.Point(11, 80);
            this.rbCopy.Name = "rbCopy";
            this.rbCopy.Padding = new System.Windows.Forms.Padding(0);
            this.rbCopy.Size = new System.Drawing.Size(47, 16);
            this.rbCopy.TabIndex = 3;
            this.rbCopy.Text = "复制";
            // 
            // txtFileName
            // 
            this.txtFileName.BackColor = System.Drawing.SystemColors.Control;
            this.txtFileName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFileName.Location = new System.Drawing.Point(70, 19);
            this.txtFileName.Multiline = true;
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.ReadOnly = true;
            this.txtFileName.Size = new System.Drawing.Size(233, 85);
            this.txtFileName.TabIndex = 0;
            // 
            // lblFileName
            // 
            this.lblFileName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.lblFileName.Location = new System.Drawing.Point(9, 22);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Padding = new System.Windows.Forms.Padding(0);
            this.lblFileName.Size = new System.Drawing.Size(66, 16);
            this.lblFileName.TabIndex = 2;
            this.lblFileName.Text = "插件对象：";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // separator1
            // 
            this.separator1.BackColor = System.Drawing.Color.Transparent;
            this.separator1.eOrientation = System.Windows.Forms.Orientation.Horizontal;
            this.separator1.Location = new System.Drawing.Point(145, 161);
            this.separator1.LockHeight = true;
            this.separator1.Name = "separator1";
            this.separator1.Padding = new System.Windows.Forms.Padding(0);
            this.separator1.Size = new System.Drawing.Size(331, 3);
            this.separator1.TabIndex = 87;
            this.separator1.Text = "separator1";
            // 
            // AppendPluginTBForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 215);
            this.Controls.Add(this.separator1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.Location = new System.Drawing.Point(0, 0);
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
        private GISShare.Controls.WinForm.WFNew.LabelN lblFileName;
        private GISShare.Controls.WinForm.WFNew.ButtonN btnCancel;
        private GISShare.Controls.WinForm.WFNew.ButtonN btnOk;
        private GISShare.Controls.WinForm.WFNew.ButtonN btnOpen;
        private GISShare.Controls.WinForm.WFNew.RadioButtonN rbCut;
        private GISShare.Controls.WinForm.WFNew.RadioButtonN rbCopy;
        private GISShare.Controls.WinForm.WFNew.RadioButtonN rbUse;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private Controls.WinForm.WFNew.Separator separator1;
    }
}