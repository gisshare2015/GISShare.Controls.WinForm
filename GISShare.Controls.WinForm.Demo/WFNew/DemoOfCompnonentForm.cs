using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace GISShare.Controls.WinForm.Demo.WFNew
{
    public partial class DemoOfCompnonentForm : GISShare.Controls.WinForm.WFNew.RibbonForm //Form
    {
        private readonly string LayoutFlie_DockPanel = Application.StartupPath + "\\WFNew_DemoOfCompnonentForm_DockPanel.xml";
        private readonly string LayoutFlie_RibbonControl = Application.StartupPath + "\\WFNew_DemoOfCompnonentForm_RibbonControlEx.xml";

        public DemoOfCompnonentForm()
        {
            InitializeComponent();
            //
            //
            //
            #region 创建树
            Assembly assembly = Assembly.LoadFrom(Application.StartupPath + "\\GISShare.Controls.WinForm.Demo.exe");
            if (assembly != null)
            {
                Type[] types = null;
                try
                {
                    //获取程序集中定义的类型
                    types = assembly.GetTypes();
                }
                catch (ReflectionTypeLoadException ex)
                {
                    GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show("反射类型加载异常" + ex.ToString());
                }
                catch (Exception ex)
                {
                    GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show(ex.Message);
                }
                finally
                {
                    foreach (Type one in types)
                    {
                        if (one.FullName.Contains("GISShare.Controls.WinForm.Demo.WFNew.WFNew_JDKJJH"))
                        {
                            try
                            {
                                GISShare.Controls.WinForm.WFNew.Forms.ITBForm pTBForm = Activator.CreateInstance(one) as GISShare.Controls.WinForm.WFNew.Forms.ITBForm;
                                if (pTBForm != null)
                                {
                                    GISShare.Controls.WinForm.WFNew.BaseButtonItem baseButtonItem = new GISShare.Controls.WinForm.WFNew.BaseButtonItem();
                                    baseButtonItem.Name = pTBForm.Name;
                                    baseButtonItem.Text = pTBForm.Text;
                                    baseButtonItem.Margin = new Padding(9, 3, 9, 3);
                                    baseButtonItem.eVAlignmentStyle = GISShare.Controls.WinForm.WFNew.VAlignmentStyle.eStretch;
                                    baseButtonItem.eHAlignmentStyle = GISShare.Controls.WinForm.WFNew.HAlignmentStyle.eStretch;
                                    baseButtonItem.ShowNomalState = true;
                                    baseButtonItem.Tag = one;
                                    baseButtonItem.MouseClick += new MouseEventHandler(baseButtonItem_MouseClick);
                                    GISShare.Controls.WinForm.WFNew.View.SuperViewItem superViewItem = new GISShare.Controls.WinForm.WFNew.View.SuperViewItem(baseButtonItem);
                                    superViewItem.Height = 27;
                                    this.viewItemListBox1.ViewItems.Add(superViewItem);
                                    pTBForm = null;
                                }                                
                            }
                            catch (Exception e)
                            {
                                GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show(one.FullName + "反射生成对象时发生异常（" + e.Message + "）");
                            }
                        }
                    }
                }
            }
            #endregion
        }
        void baseButtonItem_MouseClick(object sender, MouseEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.BaseButtonItem baseButtonItem = sender as GISShare.Controls.WinForm.WFNew.BaseButtonItem;
            if (baseButtonItem != null && baseButtonItem.Tag != null) 
            {
                Form form = Activator.CreateInstance(baseButtonItem.Tag as Type) as Form;
                if (form != null)
                {
                    form.MdiParent = this;
                    form.Show();
                    //form.Location = new Point(0,0);
                }
            }
        }

        private void btnInfo_MouseClick(object sender, MouseEventArgs e)
        {
            GISShare.Controls.WinForm.InfoForm infoForm = new GISShare.Controls.WinForm.InfoForm();
            infoForm.ShowDialog();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //
            if (System.IO.File.Exists(LayoutFlie_RibbonControl)) this.ribbonControlEx1.LoadLayoutFile(LayoutFlie_RibbonControl, true);
            if (System.IO.File.Exists(LayoutFlie_DockPanel)) this.dockPanelManager1.LoadLayoutFile(LayoutFlie_DockPanel, false);
        }

        protected override void OnClosed(EventArgs e)
        {
            if (System.IO.File.Exists(LayoutFlie_DockPanel)) this.dockPanelManager1.SaveLayoutFile(LayoutFlie_DockPanel);
            if (System.IO.File.Exists(LayoutFlie_RibbonControl)) this.ribbonControlEx1.SaveLayoutFile(LayoutFlie_RibbonControl);
            //
            base.OnClosed(e);
        }
    }
}