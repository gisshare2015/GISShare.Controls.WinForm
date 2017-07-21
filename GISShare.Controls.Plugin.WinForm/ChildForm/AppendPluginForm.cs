using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.Plugin.WinForm
{
    public partial class AppendPluginForm : Form
    {
        private IBaseHost3 m_pBaseHost3;
        private GISShare.Controls.WinForm.ListBoxX m_ListBoxX;

        public AppendPluginForm(IBaseHost3 pBaseHost3)
        {
            InitializeComponent();
            //
            //
            //
            this.m_pBaseHost3 = pBaseHost3;
            this.m_pBaseHost3.PluginReflection += new PluginReflectionEventHandler(BaseHost3_PluginReflection);
        }
        void BaseHost3_PluginReflection(object sender, PluginReflectionEventArgs e)
        {
            GISShare.Controls.WinForm.Item item = new GISShare.Controls.WinForm.Item();
            item.Text = e.Info;
            if (e.Plugin != null)
            {
                if (this.m_pBaseHost3.PluginCategoryDictionary.ContainsPlugin(e.Plugin.Name))
                {
                    item.Text = "冲突的插件对象";
                }
                item.Text += "【目录索引（CategoryIndex）：" + e.Plugin.CategoryIndex + "；名称（Name）：" + e.Plugin.Name + "】";
            }
            this.m_ListBoxX.Items.Add(item);
        }

        private void btnOpen_MouseClick(object sender, MouseEventArgs e)
        {
            this.openFileDialog1.Filter = "插件(*.dll)|*.dll;";
            this.openFileDialog1.AddExtension = true;
            this.openFileDialog1.CheckFileExists = true;
            this.openFileDialog1.CheckPathExists = true;
            this.openFileDialog1.FileName = "插件";
            this.openFileDialog1.Multiselect = false;
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtFileName.Text = this.openFileDialog1.FileName;
            }
        }

        private void btnOk_MouseClick(object sender, MouseEventArgs e)
        {
            string strFileName = this.txtFileName.Text;
            //
            if (strFileName.Length <= 0)
            {
                MessageBox.Show("请选择要加载的插件！");
                return;
            }
            //
            if (!System.IO.File.Exists(this.txtFileName.Text))
            {
                MessageBox.Show("文件“" + strFileName + "”已不存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //
            if (this.rbCopy.Checked || this.rbCut.Checked)
            {
                if (!System.IO.Directory.Exists(this.m_pBaseHost3.PluginDLLFolder))
                {
                    System.IO.Directory.CreateDirectory(this.m_pBaseHost3.PluginDLLFolder);
                }
                string strFileNameNew = this.m_pBaseHost3.PluginDLLFolder + "\\" + System.IO.Path.GetFileName(strFileName);
                if (System.IO.File.Exists(strFileNameNew))
                {
                    MessageBox.Show("文件“" + strFileNameNew + "”已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                System.IO.File.Copy(strFileName, strFileNameNew);
                //
                if (this.rbCut.Checked)
                {
                    try
                    {
                        System.IO.File.Delete(strFileName);
                    }
                    catch
                    {
                        MessageBox.Show("删除文件“" + strFileName + "”失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                //
                strFileName = strFileNameNew;
            }
            //
            this.m_ListBoxX = new Controls.WinForm.ListBoxX();
            this.m_ListBoxX.Dock = DockStyle.Fill;
            this.m_ListBoxX.HorizontalScrollbar = true;
            this.m_ListBoxX.ItemHeight = 18;
            this.m_ListBoxX.AutoMouseMoveSeleced = true;
            //
            this.m_pBaseHost3.AppendPluginObject(strFileName);
            //
            if (MessageBox.Show("加载插件完成，是否查看加载信息？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                Form form = new Form();
                form.Text = "插件加载信息";
                form.Controls.Add(this.m_ListBoxX);
                form.Owner = this;
                form.Size = new Size(this.Width + 100, this.Height + 100);
                form.ShowInTaskbar = false;
                form.StartPosition = FormStartPosition.Manual;
                form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
                form.Location = new Point(this.Location.X + (this.Width - form.Width) / 2, this.Location.Y + (this.Height - form.Height) / 2);
                form.Show();
            }
        }

        private void btnCancel_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }
    }
}
