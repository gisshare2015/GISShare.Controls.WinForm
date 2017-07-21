using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.Plugin.WinForm.Demo
{
    public partial class RibbonHostForm : GISShare.Controls.Plugin.WinForm.WFNew.Ribbon.HostRibbonForm, Hook.IAppHook
    {
        private readonly string LayoutFlie_DockPanel = Application.StartupPath + "\\Ribbon_DockPanel.xml";
        private readonly string LayoutFlie_RibbonControl = Application.StartupPath + "\\Ribbon_RibbonControl.xml";

        public RibbonHostForm()
        {
            InitializeComponent();
            //
            //
            //
            this.dockPanelManager1.AddSetComponent();//设置组件
            //
            //
            //
            string[] removeObjectNameArray = new string[] 
            {
                //你可以尝试取消注释看看效果哦
                //"GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.WFNew.Ribbon.RibbonControlEx_RibbonPages.BaseItem.RibbonPageFile_BaseItem.RibbonBarDockPanelManager"
            };
            //
            //
            //
            this.PluginReflection += new PluginReflectionEventHandler(RibbonHostForm_PluginReflection);
            this.RunPluginEngine
                (
                System.String.Empty,
                Application.StartupPath + "\\Plugin",
                new string[] { "GISShare.Controls.Plugin.dll", "GISShare.Controls.Plugin.WinForm.dll" },
                true,
                removeObjectNameArray,
                true,
                this, 
                this.ribbonControl1,
                this.ribbonStatusBar1, 
                this.contextPopupManager1,
                this.dockPanelManager1
                );
            this.PluginReflection -= new PluginReflectionEventHandler(RibbonHostForm_PluginReflection);
            //
            //
            //
            if (System.IO.File.Exists(LayoutFlie_RibbonControl)) this.ribbonControl1.LoadLayoutFile(LayoutFlie_RibbonControl, true);
            if (System.IO.File.Exists(LayoutFlie_DockPanel)) this.dockPanelManager1.LoadLayoutFile(LayoutFlie_DockPanel, false);
        }
        void RibbonHostForm_PluginReflection(object sender, PluginReflectionEventArgs e)
        {
            this.richTextBox1.Text += e.Info;
            if (e.Plugin != null) this.richTextBox1.Text += "【目录索引（CategoryIndex）：" + e.Plugin.CategoryIndex + "；名称（Name）：" + e.Plugin.Name + "】";
            this.richTextBox1.Text += "\n";
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            //if (System.IO.File.Exists(LayoutFlie_DockPanel)) 
                this.dockPanelManager1.SaveLayoutFile(LayoutFlie_DockPanel);
            //if (System.IO.File.Exists(LayoutFlie_RibbonControl))
                this.ribbonControl1.SaveLayoutFile(LayoutFlie_RibbonControl);
            //
            base.OnFormClosed(e);
        }

        #region IAppHook
        public IBaseHost Host
        {
            get { return this; }
        }

        public System.Windows.Forms.RichTextBox RichTextBox
        {
            get { return this.richTextBox1; }
        }

        public GISShare.Controls.WinForm.WFNew.DockPanel.DockPanelManager DockPanelManager
        {
            get { return this.dockPanelManager1; }
        }

        private string m_FileName;
        public string FileName
        {
            get { return m_FileName; }
            set { m_FileName = value; }
        }
        #endregion
    }
}
