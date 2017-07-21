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
    public partial class PluginDemoForm : GISShare.Controls.WinForm.WFNew.Forms.TBForm// Form
    {
        public PluginDemoForm()
        {
            Plugin.WinForm.WFNew.Ribbon.HostRibbonFrameworkSerializableObject o = new WFNew.Ribbon.HostRibbonFrameworkSerializableObject();
            Plugin.SubItemSerializableObject page = new SubItemSerializableObject() { ID = "page", Group = true };
            Plugin.SubItemSerializableObject bar = new SubItemSerializableObject() { ID = "bar", Group = true };
            Plugin.ItemDefSerializableObject item = new ItemDefSerializableObject() { ID = "item", Group = true };
            bar.Items.Add(item);
            page.Items.Add(bar);
            o.RibbonPages.Items.Add(page);
            System.Xml.Serialization.XmlSerializer xmlSerialization = new System.Xml.Serialization.XmlSerializer(o.GetType());
            using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
            {
                using (System.IO.TextWriter textWriter = new System.IO.StreamWriter(memoryStream))
                {
                    System.Xml.XmlWriter xmlWriter = new System.Xml.XmlTextWriter(textWriter);
                    xmlSerialization.Serialize(xmlWriter, o);
                    //
                    memoryStream.Seek(0, System.IO.SeekOrigin.Begin);
                    //
                    using (System.IO.TextReader textReader = new System.IO.StreamReader(memoryStream))
                    {
                        string s = textReader.ReadToEnd();
                        textReader.Close();
                    }
                    textWriter.Close();
                }
                memoryStream.Close();
            }
            //
            InitializeComponent();
        }

        private void dbRibbonHostForm_MouseClick(object sender, MouseEventArgs e)
        {
            RibbonHostForm ribbonHostForm = new RibbonHostForm();
            ribbonHostForm.Show();
        }

        private void dbDockBarHostForm_MouseClick(object sender, MouseEventArgs e)
        {
            DockBarHostForm dockBarHostForm = new DockBarHostForm();
            dockBarHostForm.Show();
        }

        private void dbDockBarHostTBForm_MouseClick(object sender, MouseEventArgs e)
        {
            DockBarHostTBForm dockBarHostTBForm = new DockBarHostTBForm();
            dockBarHostTBForm.Show();
        }
    }
}
