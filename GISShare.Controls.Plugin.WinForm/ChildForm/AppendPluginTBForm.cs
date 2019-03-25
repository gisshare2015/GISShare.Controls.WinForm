using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.Plugin.WinForm
{
    public partial class AppendPluginTBForm : GISShare.Controls.WinForm.WFNew.Forms.TBForm // Form
    {
        private IBaseHost3 m_pBaseHost3;
        private GISShare.Controls.WinForm.WFNew.View.ViewItemListBoxItem m_ViewItemListBox;

        public AppendPluginTBForm(IBaseHost3 pBaseHost3)
        {
            InitializeComponent();
            //
            //
            //
            this.m_pBaseHost3 = pBaseHost3;
            this.m_pBaseHost3.PluginReflection += new GISShare.Controls.Plugin.PluginReflectionEventHandler(BaseHost3_PluginReflection);
        }
        void BaseHost3_PluginReflection(object sender, GISShare.Controls.Plugin.PluginReflectionEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.View.ViewItem viewItem = new GISShare.Controls.WinForm.WFNew.View.ViewItem();
            viewItem.Text = e.Info;
            if (e.Plugin != null)
            {
                if (this.m_pBaseHost3.PluginCategoryDictionary.ContainsPlugin(e.Plugin.Name))
                {
                    viewItem.Text = "冲突的插件对象";
                }
                viewItem.Text += "【目录索引（CategoryIndex）：" + e.Plugin.CategoryIndex + "；名称（Name）：" + e.Plugin.Name + "】";
            }
            this.m_ViewItemListBox.ViewItems.Add(viewItem);
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
                GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show("请选择要加载的插件！");
                return;
            }
            //
            if (!System.IO.File.Exists(this.txtFileName.Text))
            {
                GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show("文件“" + strFileName + "”已不存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show("文件“" + strFileNameNew + "”已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show("删除文件“" + strFileName + "”失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                //
                strFileName = strFileNameNew;
            }
            //
            this.m_ViewItemListBox = new Controls.WinForm.WFNew.View.ViewItemListBoxItem();
            this.m_ViewItemListBox.ShowHScrollBar = true;
            //
            this.m_pBaseHost3.AppendPluginObject(strFileName);
            //
            if (MessageBox.Show("加载插件完成，是否查看加载信息？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                GISShare.Controls.WinForm.WFNew.Forms.TBForm form = new GISShare.Controls.WinForm.WFNew.Forms.TBForm();
                form.Text = "插件加载信息";
                form.Controls.Add(new GISShare.Controls.WinForm.WFNew.BaseItemHost(this.m_ViewItemListBox) { Dock = DockStyle.Fill });
                form.Owner = this;
                form.ShowIcon = false;
                form.Size = new Size(this.Width + 100, this.Height + 100);
                form.MinimizeBox = false;
                //form.MaximizeBox = false;
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
