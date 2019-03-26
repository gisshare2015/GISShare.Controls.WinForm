namespace GISShare.Controls.WinForm.Demo.WFNew.WFNew_JDKJJH
{
    partial class DemoOfComboBoxForm
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
            GISShare.Controls.WinForm.WFNew.View.TextViewItem textViewItem1 = new GISShare.Controls.WinForm.WFNew.View.TextViewItem();
            GISShare.Controls.WinForm.WFNew.View.ImageViewItem imageViewItem1 = new GISShare.Controls.WinForm.WFNew.View.ImageViewItem();
            GISShare.Controls.WinForm.WFNew.View.ColorViewItem colorViewItem1 = new GISShare.Controls.WinForm.WFNew.View.ColorViewItem();
            this.comboBoxN1 = new GISShare.Controls.WinForm.WFNew.ComboBoxItem();
            this.baseItemHost1 = new GISShare.Controls.WinForm.WFNew.BaseItemHost();
            this.SuspendLayout();
            // 
            // comboBoxN1
            // 
            this.comboBoxN1.DropDownHeight = 118;
            this.comboBoxN1.DropDownWidth = 201;
            this.comboBoxN1.eModifySizeStyle = GISShare.Controls.WinForm.WFNew.ModifySizeStyle.eAll;
            this.comboBoxN1.Font = new System.Drawing.Font("宋体", 9F);
            this.comboBoxN1.ForeColor = System.Drawing.SystemColors.ControlText;
            textViewItem1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            textViewItem1.ForeColor = System.Drawing.SystemColors.ControlText;
            textViewItem1.Name = null;
            textViewItem1.Text = "文本视图项（TextViewItem）";
            imageViewItem1.Font = new System.Drawing.Font("宋体", 9F);
            imageViewItem1.ForeColor = System.Drawing.SystemColors.ControlText;
            imageViewItem1.Image = null;
            imageViewItem1.Name = null;
            imageViewItem1.Text = "图片视图项（ImageViewItem）";
            colorViewItem1.Color = System.Drawing.Color.Red;
            colorViewItem1.Font = new System.Drawing.Font("宋体", 9F);
            colorViewItem1.ForeColor = System.Drawing.SystemColors.ControlText;
            colorViewItem1.Name = null;
            colorViewItem1.Text = "颜色视图项（ColorViewItem）";
            this.comboBoxN1.Items.Add(textViewItem1);
            this.comboBoxN1.Items.Add(imageViewItem1);
            this.comboBoxN1.Items.Add(colorViewItem1);
            this.comboBoxN1.Location = new System.Drawing.Point(0, 0);
            this.comboBoxN1.LockHeight = true;
            this.comboBoxN1.Name = "comboBoxN1";
            this.comboBoxN1.ShowDropDownNum = 0;
            this.comboBoxN1.Size = new System.Drawing.Size(399, 20);
            this.comboBoxN1.Tag = null;
            this.comboBoxN1.Text = null;
            // 
            // baseItemHost1
            // 
            this.baseItemHost1.BaseItemObject = this.comboBoxN1;
            this.baseItemHost1.Location = new System.Drawing.Point(18, 18);
            this.baseItemHost1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.baseItemHost1.Name = "baseItemHost1";
            this.baseItemHost1.Padding = new System.Windows.Forms.Padding(0);
            this.baseItemHost1.Size = new System.Drawing.Size(399, 20);
            this.baseItemHost1.TabIndex = 0;
            // 
            // DemoOfComboBoxNForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 262);
            this.Controls.Add(this.baseItemHost1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "DemoOfComboBoxNForm";
            this.ShowIcon = false;
            this.Text = "ComboBoxN控件";
            this.ResumeLayout(false);

        }

        #endregion

        private GISShare.Controls.WinForm.WFNew.ComboBoxItem comboBoxN1;
        private GISShare.Controls.WinForm.WFNew.BaseItemHost baseItemHost1;
    }
}