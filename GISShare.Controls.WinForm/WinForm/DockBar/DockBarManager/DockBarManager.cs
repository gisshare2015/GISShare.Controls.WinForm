using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace GISShare.Controls.WinForm.DockBar
{
    [Designer(typeof(DockBarManagerDesigner)), ToolboxBitmap(typeof(DockBarManager), "DockBarManager.bmp")]
    public class DockBarManager : Component, ICollectionItemDB
    {
        private bool m_IsCustomize = false;
        private bool m_ShowItemToolTips = true;
        private bool m_ShowLargeImage = false;
        private Form m_ParentForm = null;                                                 //它所依附的父窗体
        //
        private DockBarDockAreaTop m_DockBarDockAreaTop = null;
        private DockBarDockAreaLeft m_DockBarDockAreaLeft = null;
        private DockBarDockAreaRight m_DockBarDockAreaRight = null;
        private DockBarDockAreaBottom m_DockBarDockAreaBottom = null;
        //
        private MenuBar m_MenuBar = null;
        private ToolBarCollection m_ToolBarCollection = null;
        private CustomizeToolBarCollection m_CustomizeToolBarCollection = null;
        private StatusBar m_StatusBar = null;
        private ContextMenuCollection m_ContextMenuCollection = null;
        private DockBarFloatFormCollection m_DockBarFloatFormCollection = null;
        private BaseItemCollection m_BaseItemCollection = null;

        public Form ParentForm
        {
            get { return m_ParentForm; }
            set { m_ParentForm = value; }
        }

        public DockBarDockAreaTop DockBarDockAreaTop
        {
            get { return m_DockBarDockAreaTop; }
            set
            {
                m_DockBarDockAreaTop = value;
                //if (m_DockBarDockAreaTop != null) m_DockBarDockAreaTop.SetDockBarManager(this);
            }
        }

        public DockBarDockAreaLeft DockBarDockAreaLeft
        {
            get { return m_DockBarDockAreaLeft; }
            set
            {
                m_DockBarDockAreaLeft = value;
                //if (m_DockBarDockAreaLeft != null) m_DockBarDockAreaLeft.SetDockBarManager(this); 
            }
        }

        public DockBarDockAreaRight DockBarDockAreaRight
        {
            get { return m_DockBarDockAreaRight; }
            set 
            {
                m_DockBarDockAreaRight = value;
                //if (m_DockBarDockAreaRight != null) m_DockBarDockAreaRight.SetDockBarManager(this); 
            }
        }

        public DockBarDockAreaBottom DockBarDockAreaBottom
        {
            get { return m_DockBarDockAreaBottom; }
            set
            {
                m_DockBarDockAreaBottom = value;
                //if (m_DockBarDockAreaBottom != null) m_DockBarDockAreaBottom.SetDockBarManager(this);
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), Bindable(true), Localizable(true)]
        public MenuBar MenuBar
        {
            get { return m_MenuBar; }
            set
            {
                m_MenuBar = value;
                //if (m_MenuBar != null) m_MenuBar.SetDockBarManager(this);
                ISetDockBarManagerHelper pSetDockBarManagerHelper = m_MenuBar as ISetDockBarManagerHelper;
                if (pSetDockBarManagerHelper != null) pSetDockBarManagerHelper.SetDockBarManager(this);
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Bindable(true), Localizable(true)]
        public ToolBarCollection ToolBars
        {
            get { return m_ToolBarCollection; }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Bindable(true), Localizable(true)]
        internal CustomizeToolBarCollection CustomizeToolBars
        {
            get { return m_CustomizeToolBarCollection; }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), Bindable(true), Localizable(true)]
        public StatusBar StatusBar
        {
            get { return m_StatusBar; }
            set
            {
                m_StatusBar = value;
                //if (m_StatusBar != null) m_StatusBar.SetDockBarManager(this);
                ISetDockBarManagerHelper pSetDockBarManagerHelper = m_StatusBar as ISetDockBarManagerHelper;
                if (pSetDockBarManagerHelper != null) pSetDockBarManagerHelper.SetDockBarManager(this);
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Bindable(true), Localizable(true)]
        public ContextMenuCollection ContextMenus
        {
            get { return m_ContextMenuCollection; }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Bindable(true), Localizable(true)]
        public ICollection DockBarFloatForms_Read
        {
            get { return this.DockBarFloatForms; }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Bindable(true), Localizable(true)]
        internal DockBarFloatFormCollection DockBarFloatForms
        {
            get { return m_DockBarFloatFormCollection; }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Bindable(true), Localizable(true)]
        public BaseItemCollection BaseItems
        {
            get { return m_BaseItemCollection; }
        }
        
        public DockBarManager()
        {
            this.m_ToolBarCollection = new ToolBarCollection(this);
            this.m_CustomizeToolBarCollection = new CustomizeToolBarCollection(this);
            this.m_ContextMenuCollection = new ContextMenuCollection(this);
            this.m_DockBarFloatFormCollection = new DockBarFloatFormCollection(this);
            this.m_BaseItemCollection = new BaseItemCollection();

            ToolStripManager.Renderer = WinForm.WinFormRenderer.WinFormRendererStrategy;
        }

        [Browsable(false), DefaultValue(true)]
        public bool ShowItemToolTips
        {
            get { return this.m_ShowItemToolTips; }
            set 
            {
                this.m_ShowItemToolTips = value; 
                //
                if (this.MenuBar != null) { this.MenuBar.SetShowItemToolTips(this.m_ShowItemToolTips); }
                if (this.StatusBar != null) { this.StatusBar.SetShowItemToolTips(this.m_ShowItemToolTips); }
                foreach (ToolBar one in this.ToolBars) { one.SetShowItemToolTips(this.m_ShowItemToolTips); }
                foreach (CustomizeToolBar one in this.CustomizeToolBars) { one.SetShowItemToolTips(this.m_ShowItemToolTips); }
                foreach (ContextMenu one in this.ContextMenus) { one.SetShowItemToolTips(this.m_ShowItemToolTips); }
            }
        }

        [Browsable(false), DefaultValue(true)]
        public bool ShowLargeImage
        {
            get { return m_ShowLargeImage; }
            set
            {
                m_ShowLargeImage = value;
                //
                if (value)
                {
                    if (this.MenuBar != null) { this.MenuBar.SetImageScalingSize(new Size(32, 32)); }
                    if (this.StatusBar != null) { this.StatusBar.SetImageScalingSize(new Size(32, 32)); }
                    foreach (ToolBar one in this.ToolBars) { one.SetImageScalingSize(new Size(32, 32)); }
                    foreach (CustomizeToolBar one in this.CustomizeToolBars) { one.SetImageScalingSize(new Size(32, 32)); }
                    foreach (ContextMenu one in this.ContextMenus) { one.SetImageScalingSize(new Size(32, 32)); }
                }
                else
                {
                    if (this.MenuBar != null) { this.MenuBar.SetImageScalingSize(new Size(16, 16)); }
                    if (this.StatusBar != null) { this.StatusBar.SetImageScalingSize(new Size(16, 16)); }
                    foreach (ToolBar one in this.ToolBars) { one.SetImageScalingSize(new Size(16, 16)); }
                    foreach (CustomizeToolBar one in this.CustomizeToolBars) { one.SetImageScalingSize(new Size(16, 16)); }
                    foreach (ContextMenu one in this.ContextMenus) { one.SetImageScalingSize(new Size(16, 16)); }
                }
            }
        }

        public void ShowDockBarListMenuStrip(Control control, Point postion)
        {
            DockBarListMenuStrip dockBarListMenuStrip = new DockBarListMenuStrip(this);
            dockBarListMenuStrip.Show(control, postion);
        }

        public bool IsCustomize
        {
            get { return m_IsCustomize; }
        }

        public void DockBarCustomize()
        {
            DockBarCustomizeForm DockBarCustomizeForm1 = new DockBarCustomizeForm(this);
            DockBarCustomizeForm1.Owner = this.ParentForm;
            DockBarCustomizeForm1.Show();
        }

        public void Refresh()
        {
            if (this.ParentForm != null) this.ParentForm.Refresh();
            //
            foreach(DockBarFloatForm one in this.DockBarFloatForms)
            {
                one.Refresh();
            }
        }

        public void RefreshComponents()
        {
            if (this.DockBarDockAreaTop != null) this.DockBarDockAreaTop.Refresh();
            if (this.DockBarDockAreaLeft != null) this.DockBarDockAreaLeft.Refresh();
            if (this.DockBarDockAreaRight != null) this.DockBarDockAreaRight.Refresh();
            if (this.DockBarDockAreaBottom != null) this.DockBarDockAreaBottom.Refresh();
            //
            foreach (DockBarFloatForm one in this.DockBarFloatForms)
            {
                one.Refresh();
            }
        }

        public void RefreshFloatForm()
        {
            foreach (DockBarFloatForm one in this.DockBarFloatForms)
            {
                one.Refresh();
            }
        }

        internal void SetIsCustomize(bool bCustomize)
        {
            this.m_IsCustomize = bCustomize;
        }

        internal ToolBar GetEmptyCustomizeToolBar(string text)
        {
            CustomizeToolBar toolBar = new CustomizeToolBar();
            this.CustomizeToolBars.Add(toolBar);
            toolBar.Text = text;
            toolBar.Name = "customizeToolBar" + this.CustomizeToolBars.Count;
            this.DockBarDockAreaTop.Join(toolBar);
            return toolBar;
        }

        internal DockBarFloatForm GetEmptyDockBarFloatForm()
        {
            DockBarFloatForm dockBarFloatForm = new DockBarFloatForm();
            this.DockBarFloatForms.Add(dockBarFloatForm);
            dockBarFloatForm.Name = "dockBarFloatForm" + this.DockBarFloatForms.Count.ToString();
            return dockBarFloatForm;
        }

        public ToolStripItem GetToolStripItem(string name)
        {
            if (this.MenuBar != null)
            {
                foreach (ToolStripItem one in this.MenuBar.Items)
                {
                    if (one.Name == name) return one;
                    //
                    ToolStripDropDownItem toolStripDropDownItem = one as ToolStripDropDownItem;
                    if (toolStripDropDownItem == null) continue;
                    ToolStripItem toolStripItem = this.GetToolStripItem(toolStripDropDownItem.DropDownItems, name);
                    if (toolStripItem != null) return toolStripItem;
                }
            }
            foreach (ToolStrip one in this.ToolBars)
            {
                foreach (ToolStripItem one2 in one.Items)
                {
                    if (one2.Name == name) return one2;
                    //
                    ToolStripDropDownItem toolStripDropDownItem = one2 as ToolStripDropDownItem;
                    if (toolStripDropDownItem == null) continue;
                    ToolStripItem toolStripItem = this.GetToolStripItem(toolStripDropDownItem.DropDownItems, name);
                    if (toolStripItem != null) return toolStripItem;
                }
            }
            foreach (ToolStrip one in this.CustomizeToolBars)
            {
                foreach (ToolStripItem one2 in one.Items)
                {
                    if (one2.Name == name) return one2;
                    //
                    ToolStripDropDownItem toolStripDropDownItem = one2 as ToolStripDropDownItem;
                    if (toolStripDropDownItem == null) continue;
                    ToolStripItem toolStripItem = this.GetToolStripItem(toolStripDropDownItem.DropDownItems, name);
                    if (toolStripItem != null) return toolStripItem;
                }
            }
            if (this.StatusBar != null)
            {
                foreach (ToolStripItem one in this.StatusBar.Items)
                {
                    if (one.Name == name) return one;
                    //
                    ToolStripDropDownItem toolStripDropDownItem = one as ToolStripDropDownItem;
                    if (toolStripDropDownItem == null) continue;
                    ToolStripItem toolStripItem = this.GetToolStripItem(toolStripDropDownItem.DropDownItems, name);
                    if (toolStripItem != null) return toolStripItem;
                }
            }
            foreach (ContextMenu one in this.ContextMenus)
            {
                foreach (ToolStripItem one2 in one.Items)
                {
                    if (one2.Name == name) return one2;
                    //
                    ToolStripDropDownItem toolStripDropDownItem = one2 as ToolStripDropDownItem;
                    if (toolStripDropDownItem == null) continue;
                    ToolStripItem toolStripItem = this.GetToolStripItem(toolStripDropDownItem.DropDownItems, name);
                    if (toolStripItem != null) return toolStripItem;
                }
            }
            //
            return null;
        }
        private ToolStripItem GetToolStripItem(ToolStripItemCollection items, string name) 
        {
            foreach (ToolStripItem one in items)
            {
                if (one.Name == name) return one;
                //
                ToolStripDropDownItem toolStripDropDownItem = one as ToolStripDropDownItem;
                if (toolStripDropDownItem == null) continue;
                ToolStripItem toolStripItem = this.GetToolStripItem(toolStripDropDownItem.DropDownItems, name);
                if (toolStripItem != null) return toolStripItem;
            }
            //
            return null;
        }

        #region ICollectionItemDB
        public ToolStripItem GetItem(string strName)
        {
            ToolStripItem toolStripItem = null;
            //
            foreach (ToolStripItem one in this.MenuBar.Items)
            {
                if (one.Name == strName) toolStripItem = one;
                else
                {
                    ToolStripDropDownItem toolStripDropDownItem2 = one as ToolStripDropDownItem;
                    if (toolStripDropDownItem2 != null)
                    {
                        toolStripItem = this.GetItem_DG(toolStripDropDownItem2, strName);
                    }
                }
                if (toolStripItem != null) return toolStripItem;
            }
            //
            foreach (ToolStripItem one in this.StatusBar.Items)
            {
                if (one.Name == strName) toolStripItem = one;
                else
                {
                    ToolStripDropDownItem toolStripDropDownItem2 = one as ToolStripDropDownItem;
                    if (toolStripDropDownItem2 != null)
                    {
                        toolStripItem = this.GetItem_DG(toolStripDropDownItem2, strName);
                    }
                }
                if (toolStripItem != null) return toolStripItem;
            }
            //
            foreach (ToolBar toolBar in this.ToolBars)
            {
                foreach (ToolStripItem one in toolBar.Items)
                {
                    if (one.Name == strName) toolStripItem = one;
                    else
                    {
                        ToolStripDropDownItem toolStripDropDownItem2 = one as ToolStripDropDownItem;
                        if (toolStripDropDownItem2 != null)
                        {
                            toolStripItem = this.GetItem_DG(toolStripDropDownItem2, strName);
                        }
                    }
                    if (toolStripItem != null) return toolStripItem;
                }
            }
            //
            foreach (ContextMenu contextMenu in this.ContextMenus)
            {
                foreach (ToolStripItem one in contextMenu.Items)
                {
                    if (one.Name == strName) toolStripItem = one;
                    else
                    {
                        ToolStripDropDownItem toolStripDropDownItem2 = one as ToolStripDropDownItem;
                        if (toolStripDropDownItem2 != null)
                        {
                            toolStripItem = this.GetItem_DG(toolStripDropDownItem2, strName);
                        }
                    }
                    if (toolStripItem != null) return toolStripItem;
                }
            }
            //
            return toolStripItem;
        }
        private ToolStripItem GetItem_DG(ToolStripDropDownItem toolStripDropDownItem, string strName)
        {
            ToolStripItem toolStripItem = null;
            foreach (ToolStripItem one in toolStripDropDownItem.DropDownItems)
            {
                if (one.Name == strName) toolStripItem = one;
                else
                {
                    ToolStripDropDownItem toolStripDropDownItem2 = one as ToolStripDropDownItem;
                    if (toolStripDropDownItem2 != null)
                    {
                        toolStripItem = this.GetItem_DG(toolStripDropDownItem2, strName);
                    }
                }
            }
            return toolStripItem;
        }
        #endregion

        #region SaveLayoutInfo 保存布局文件
        public void SaveLayoutFile(string strFileName)//保存当前布局状态
        {
            XmlDocument doc = new XmlDocument();
            //
            //
            //
            XmlDeclaration declare = doc.CreateXmlDeclaration("1.0", "UTF-8", "yes");//创建一个声明
            doc.InsertBefore(declare, doc.DocumentElement);//把声明添加到文档元素的顶部
            //
            //
            //
            XmlElement root = doc.CreateElement("DockBarManager");//添加根节点
            root.SetAttribute("ShowLargeImage", this.ShowLargeImage.ToString());
            root.SetAttribute("ShowItemToolTips", this.ShowItemToolTips.ToString());
            doc.AppendChild(root);
            //
            //
            //
            XmlElement elementParentForm = root.OwnerDocument.CreateElement("ParentForm");
            switch (this.ParentForm.WindowState)
            {
                case FormWindowState.Maximized:
                    elementParentForm.SetAttribute("Location", "300,80");
                    elementParentForm.SetAttribute("Size", "800,600");
                    elementParentForm.SetAttribute("WindowState", "Maximized");
                    break;
                case FormWindowState.Minimized:
                    elementParentForm.SetAttribute("Location", "300,80");
                    elementParentForm.SetAttribute("Size", "800,600");
                    elementParentForm.SetAttribute("WindowState", "Normal");
                    break;
                case FormWindowState.Normal:
                    elementParentForm.SetAttribute("Location", this.ParentForm.Location.X.ToString() + "," + this.ParentForm.Location.Y.ToString());
                    elementParentForm.SetAttribute("Size", this.ParentForm.Size.Width.ToString() + "," + this.ParentForm.Size.Height.ToString());
                    elementParentForm.SetAttribute("WindowState", "Normal");
                    break;
            }
            root.AppendChild(elementParentForm);
            //
            // MenuBar ToolBars CustomizeToolBars StatusBar ContextMenu DockBarFloatForms
            //
            #region MenuBar ToolBars CustomizeToolBars StatusBar ContextMenu DockBarFloatForms
            //MenuBar
            XmlElement elementMenuBar = root.OwnerDocument.CreateElement("MenuBar");
            root.AppendChild(elementMenuBar);
            if (this.MenuBar != null)
            {
                XmlElement element = elementMenuBar.OwnerDocument.CreateElement("MenuBar");
                element.SetAttribute("RecordID", this.MenuBar.RecordID.ToString());
                element.SetAttribute("Name", this.MenuBar.Name);
                //element.SetAttribute("VisibleEx", this.MenuBar.VisibleEx.ToString());
                element.SetAttribute("DockBarFloatFormLocation", this.MenuBar.DockBarFloatFormLocation.X.ToString() + "," + this.MenuBar.DockBarFloatFormLocation.Y.ToString());
                element.SetAttribute("DockBarFloatFormSize", this.MenuBar.DockBarFloatFormSize.Width.ToString() + "," + this.MenuBar.DockBarFloatFormSize.Height.ToString());
                elementMenuBar.AppendChild(element);
                elementMenuBar.SetAttribute("Count", "1");
            }
            else
            {
                elementMenuBar.SetAttribute("Count", "0");
            }
            //ToolBars
            XmlElement elementToolBars = root.OwnerDocument.CreateElement("ToolBars");
            elementToolBars.SetAttribute("Count", this.ToolBars.Count.ToString());
            root.AppendChild(elementToolBars);
            this.ToolBars.SetRecordID();
            foreach (ToolBar one in this.ToolBars)
            {
                XmlElement element = elementToolBars.OwnerDocument.CreateElement("ToolBar");
                element.SetAttribute("RecordID", one.RecordID.ToString());
                element.SetAttribute("Name", one.Name);
                //element.SetAttribute("VisibleEx", one.VisibleEx.ToString());
                element.SetAttribute("DockBarFloatFormLocation", one.DockBarFloatFormLocation.X.ToString() + "," + one.DockBarFloatFormLocation.Y.ToString());
                element.SetAttribute("DockBarFloatFormSize", one.DockBarFloatFormSize.Width.ToString() + "," + one.DockBarFloatFormSize.Height.ToString());
                elementToolBars.AppendChild(element);
            }
            //CustomizeToolBars
            XmlElement elementCustomizeToolBars = root.OwnerDocument.CreateElement("CustomizeToolBars");
            elementCustomizeToolBars.SetAttribute("Count", this.CustomizeToolBars.Count.ToString());
            root.AppendChild(elementCustomizeToolBars);
            this.CustomizeToolBars.SetRecordID();
            foreach (CustomizeToolBar one in this.CustomizeToolBars)
            {
                XmlElement element = elementCustomizeToolBars.OwnerDocument.CreateElement("CustomizeToolBar");
                element.SetAttribute("RecordID", one.RecordID.ToString());
                element.SetAttribute("Name", one.Name);
                element.SetAttribute("Text", one.Text);
                //element.SetAttribute("VisibleEx", one.VisibleEx.ToString());
                element.SetAttribute("DockBarFloatFormLocation", one.DockBarFloatFormLocation.X.ToString() + "," + one.DockBarFloatFormLocation.Y.ToString());
                element.SetAttribute("DockBarFloatFormSize", one.DockBarFloatFormSize.Width.ToString() + "," + one.DockBarFloatFormSize.Height.ToString());
                elementCustomizeToolBars.AppendChild(element);
            }
            //StatusBar
            XmlElement elementStatusBar = root.OwnerDocument.CreateElement("StatusBar");
            root.AppendChild(elementStatusBar);
            if (this.StatusBar != null)
            {
                XmlElement element = elementStatusBar.OwnerDocument.CreateElement("StatusBar");
                element.SetAttribute("RecordID", this.StatusBar.RecordID.ToString());
                element.SetAttribute("Name", this.StatusBar.Name);
                //element.SetAttribute("VisibleEx", this.StatusBar.VisibleEx.ToString());
                element.SetAttribute("DockBarFloatFormLocation", this.StatusBar.DockBarFloatFormLocation.X.ToString() + "," + this.StatusBar.DockBarFloatFormLocation.Y.ToString());
                element.SetAttribute("DockBarFloatFormSize", this.StatusBar.DockBarFloatFormSize.Width.ToString() + "," + this.StatusBar.DockBarFloatFormSize.Height.ToString());
                elementStatusBar.AppendChild(element);
                elementStatusBar.SetAttribute("Count", "1");
            }
            else
            {
                elementStatusBar.SetAttribute("Count", "0");
            }
            //ContextMenus
            XmlElement elementContextMenus = root.OwnerDocument.CreateElement("ContextMenus");
            elementContextMenus.SetAttribute("Count", this.ContextMenus.Count.ToString());
            root.AppendChild(elementContextMenus);
            this.ContextMenus.SetRecordID();
            foreach (ContextMenu one in this.ContextMenus)
            {
                XmlElement element = elementContextMenus.OwnerDocument.CreateElement("ContextMenu");
                element.SetAttribute("RecordID", one.RecordID.ToString());
                element.SetAttribute("Name", one.Name);
                elementContextMenus.AppendChild(element);
            }
            //DockBarFloatForms
            XmlElement elementDockBarFloatForms = root.OwnerDocument.CreateElement("DockBarFloatForms");
            elementDockBarFloatForms.SetAttribute("Count", this.DockBarFloatForms.Count.ToString());
            root.AppendChild(elementDockBarFloatForms);
            this.DockBarFloatForms.SetRecordID();
            foreach (DockBarFloatForm one in this.DockBarFloatForms)
            {
                XmlElement element = elementDockBarFloatForms.OwnerDocument.CreateElement("DockBarFloatForm");
                element.SetAttribute("RecordID", one.RecordID.ToString());
                element.SetAttribute("Name", one.Name);
                element.SetAttribute("Location", one.Location.X.ToString() + "," + one.Location.Y.ToString());
                element.SetAttribute("Size", one.Size.Width.ToString() + "," + one.Size.Height.ToString());
                elementDockBarFloatForms.AppendChild(element);
            }
            #endregion
            //
            //
            //
            #region Customize
            XmlElement elementCustomize = root.OwnerDocument.CreateElement("Customize");
            root.AppendChild(elementCustomize);
            //MenuBar
            int iNum = 0;
            XmlElement elementMenuBar2 = elementCustomize.OwnerDocument.CreateElement("MenuBar");
            elementCustomize.AppendChild(elementMenuBar2);
            if (this.MenuBar != null)
            {
                ICustomize pCustomize = this.MenuBar as ICustomize;
                if (pCustomize != null && pCustomize.CustomizeBaseItems.Count > 0)
                {
                    XmlElement elementCustomizeMenuBar = elementCustomize.OwnerDocument.CreateElement("MenuBar");
                    elementCustomizeMenuBar.SetAttribute("RecordID", this.MenuBar.RecordID.ToString());
                    //elementCustomizeMenuBar.SetAttribute("Name", this.MenuBar.Name);
                    elementCustomizeMenuBar.SetAttribute("Count", pCustomize.CustomizeBaseItems.Count.ToString());
                    foreach (ToolStripItem one in pCustomize.CustomizeBaseItems)
                    {
                        XmlElement elementItem = elementCustomizeMenuBar.OwnerDocument.CreateElement("Item");
                        elementItem.SetAttribute("Name", one.Name);
                        elementItem.SetAttribute("Index", this.MenuBar.Items.IndexOf(one).ToString());
                        elementCustomizeMenuBar.AppendChild(elementItem);
                    }
                    elementMenuBar2.AppendChild(elementCustomizeMenuBar);
                    iNum = 1;
                }
            }
            elementMenuBar2.SetAttribute("Count", iNum.ToString());
            //ToolBar
            iNum = 0;
            XmlElement elementToolBars2 = root.OwnerDocument.CreateElement("ToolBars");
            elementCustomize.AppendChild(elementToolBars2);
            foreach (ToolBar one in this.ToolBars)
            {
                ICustomize pCustomize = one as ICustomize;
                if (pCustomize == null || pCustomize.CustomizeBaseItems.Count <= 0) continue;
                //
                XmlElement elementCustomizeToolBar = elementCustomize.OwnerDocument.CreateElement("ToolBar");
                elementCustomizeToolBar.SetAttribute("RecordID", one.RecordID.ToString());
                //elementCustomizeToolBar.SetAttribute("Name", one.Name);
                elementCustomizeToolBar.SetAttribute("Count", pCustomize.CustomizeBaseItems.Count.ToString());
                foreach (ToolStripItem one2 in pCustomize.CustomizeBaseItems)
                {
                    XmlElement elementItem = elementCustomizeToolBar.OwnerDocument.CreateElement("Item");
                    elementItem.SetAttribute("Name", one2.Name);
                    elementItem.SetAttribute("Index", one.Items.IndexOf(one2).ToString());
                    elementCustomizeToolBar.AppendChild(elementItem);
                }
                elementToolBars2.AppendChild(elementCustomizeToolBar);
                iNum++;
            }
            elementToolBars2.SetAttribute("Count", iNum.ToString());
            //CustomizeToolBars
            iNum = 0;
            XmlElement elementCustomizeToolBars2 = root.OwnerDocument.CreateElement("CustomizeToolBars");
            elementCustomize.AppendChild(elementCustomizeToolBars2);
            foreach (CustomizeToolBar one in this.CustomizeToolBars)
            {
                ICustomize pCustomize = one as ICustomize;
                if (pCustomize == null || pCustomize.CustomizeBaseItems.Count <= 0) continue;
                //
                XmlElement elementCustomizeCustomizeToolBar = elementCustomize.OwnerDocument.CreateElement("CustomizeToolBar");
                elementCustomizeCustomizeToolBar.SetAttribute("RecordID", one.RecordID.ToString());
                //elementCustomizeCustomizeToolBar.SetAttribute("Name", one.Name);
                elementCustomizeCustomizeToolBar.SetAttribute("Count", pCustomize.CustomizeBaseItems.Count.ToString());
                foreach (ToolStripItem one2 in pCustomize.CustomizeBaseItems)
                {
                    XmlElement elementItem = elementCustomizeCustomizeToolBar.OwnerDocument.CreateElement("Item");
                    elementItem.SetAttribute("Name", one2.Name);
                    elementItem.SetAttribute("Index", one.Items.IndexOf(one2).ToString());
                    elementCustomizeCustomizeToolBar.AppendChild(elementItem);
                }
                elementCustomizeToolBars2.AppendChild(elementCustomizeCustomizeToolBar);
                iNum++;

            }
            elementCustomizeToolBars2.SetAttribute("Count", iNum.ToString());
            //StatusBar
            iNum = 0;
            XmlElement elementStatusBar2 = elementCustomize.OwnerDocument.CreateElement("StatusBar");
            elementCustomize.AppendChild(elementStatusBar2);
            if (this.StatusBar != null)
            {
                ICustomize pCustomize = this.StatusBar as ICustomize;
                if (pCustomize != null && pCustomize.CustomizeBaseItems.Count > 0)
                {
                    XmlElement elementCustomizeStatusBar = elementCustomize.OwnerDocument.CreateElement("StatusBar");
                    elementCustomizeStatusBar.SetAttribute("RecordID", this.StatusBar.RecordID.ToString());
                    //elementCustomizeStatusBar.SetAttribute("Name", this.StatusBar.Name);
                    elementCustomizeStatusBar.SetAttribute("Count", pCustomize.CustomizeBaseItems.Count.ToString());
                    foreach (ToolStripItem one in pCustomize.CustomizeBaseItems)
                    {
                        XmlElement elementItem = elementCustomizeStatusBar.OwnerDocument.CreateElement("Item");
                        elementItem.SetAttribute("Name", one.Name);
                        elementItem.SetAttribute("Index", this.StatusBar.Items.IndexOf(one).ToString());
                        elementCustomizeStatusBar.AppendChild(elementItem);
                    }
                    elementStatusBar2.AppendChild(elementCustomizeStatusBar);
                    iNum = 1;
                }
            }
            elementStatusBar2.SetAttribute("Count", iNum.ToString());
            //CustomizeToolBars
            iNum = 0;
            XmlElement elementContextMenus2 = root.OwnerDocument.CreateElement("ContextMenus");
            elementCustomize.AppendChild(elementContextMenus2);
            foreach (ContextMenu one in this.ContextMenus)
            {
                ICustomize pCustomize = one as ICustomize;
                if (pCustomize == null || pCustomize.CustomizeBaseItems.Count <= 0) continue;
                //
                XmlElement elementCustomizeContextMenu = elementCustomize.OwnerDocument.CreateElement("ContextMenu");
                elementCustomizeContextMenu.SetAttribute("RecordID", one.RecordID.ToString());
                //elementCustomizeContextMenu.SetAttribute("Name", one.Name);
                elementCustomizeContextMenu.SetAttribute("Count", pCustomize.CustomizeBaseItems.Count.ToString());
                foreach (ToolStripItem one2 in pCustomize.CustomizeBaseItems)
                {
                    XmlElement elementItem = elementCustomizeContextMenu.OwnerDocument.CreateElement("Item");
                    elementItem.SetAttribute("Name", one2.Name);
                    elementItem.SetAttribute("Index", one.Items.IndexOf(one2).ToString());
                    elementCustomizeContextMenu.AppendChild(elementItem);
                }
                elementContextMenus2.AppendChild(elementCustomizeContextMenu);
                iNum++;

            }
            elementContextMenus2.SetAttribute("Count", iNum.ToString());
            //CustomizeItem
            iNum = 0;
            XmlElement elementCustomizeItem = elementCustomize.OwnerDocument.CreateElement("CustomizeItem");
            elementCustomize.AppendChild(elementCustomizeItem);
            if (this.MenuBar != null)
            {
                foreach (ToolStripItem one in this.MenuBar.Items)
                {
                    this.GetCustomizeInfo(one as ICustomize, elementCustomizeItem, ref iNum);
                }
            }
            foreach (ToolBar one in this.ToolBars)
            {
                foreach (ToolStripItem one2 in one.Items)
                {
                    this.GetCustomizeInfo(one2 as ICustomize, elementCustomizeItem, ref iNum);
                }
            }
            foreach (CustomizeToolBar one in this.CustomizeToolBars)
            {
                foreach (ToolStripItem one2 in one.Items)
                {
                    this.GetCustomizeInfo(one2 as ICustomize, elementCustomizeItem, ref iNum);
                }
            }
            if (this.StatusBar != null)
            {
                foreach (ToolStripItem one in this.StatusBar.Items)
                {
                    this.GetCustomizeInfo(one as ICustomize, elementCustomizeItem, ref iNum);
                }
            }
            foreach (ContextMenu one in this.ContextMenus)
            {
                foreach (ToolStripItem one2 in one.Items)
                {
                    this.GetCustomizeInfo(one2 as ICustomize, elementCustomizeItem, ref iNum);
                }
            }
            elementCustomizeItem.SetAttribute("Count", iNum.ToString());
            #endregion
            //
            //
            //
            #region Dock
            XmlElement elementDock = root.OwnerDocument.CreateElement("Dock");
            root.AppendChild(elementDock);
            //DockBarDockAreaTop
            XmlElement elementDockBarDockAreaTop = elementDock.OwnerDocument.CreateElement("DockBarDockAreaTop");
            elementDockBarDockAreaTop.SetAttribute("Count", this.DockBarDockAreaTop.Rows.Length.ToString());
            elementDock.AppendChild(elementDockBarDockAreaTop);
            for (int i = 0; i < this.DockBarDockAreaTop.Rows.Length; i++)
            {
                ToolStripPanelRow toolStripPanelRow = this.DockBarDockAreaTop.Rows[i];
                for (int j = toolStripPanelRow.Controls.Length - 1; j >= 0; j--)
                {
                    IDockBar pDockBar = toolStripPanelRow.Controls[j] as IDockBar;
                    if (pDockBar == null) continue;
                    XmlElement element = elementDockBarDockAreaTop.OwnerDocument.CreateElement("DockBar");
                    element.SetAttribute("RecordID", pDockBar.RecordID.ToString());
                    //element.SetAttribute("Name", pDockBar.Name);
                    element.SetAttribute("Visible", pDockBar.Visible.ToString());
                    element.SetAttribute("DockBarStyle", pDockBar.eDockBarStyle.ToString());
                    element.SetAttribute("Location", pDockBar.Location.X.ToString() + "," + pDockBar.Location.Y.ToString());
                    element.SetAttribute("Row", i.ToString());
                    //element.SetAttribute("Column", j.ToString());
                    elementDockBarDockAreaTop.AppendChild(element);
                }
            }
            //DockBarDockAreaLeft
            XmlElement elementDockBarDockAreaLeft = elementDock.OwnerDocument.CreateElement("DockBarDockAreaLeft");
            elementDockBarDockAreaLeft.SetAttribute("Count", this.DockBarDockAreaLeft.Rows.Length.ToString());
            elementDock.AppendChild(elementDockBarDockAreaLeft);
            for (int i = 0; i < this.DockBarDockAreaLeft.Rows.Length; i++)
            {
                ToolStripPanelRow toolStripPanelRow = this.DockBarDockAreaLeft.Rows[i];
                for (int j = toolStripPanelRow.Controls.Length - 1; j >= 0; j--)
                {
                    IDockBar pDockBar = toolStripPanelRow.Controls[j] as IDockBar;
                    if (pDockBar == null) continue;
                    XmlElement element = elementDockBarDockAreaLeft.OwnerDocument.CreateElement("DockBar");
                    element.SetAttribute("RecordID", pDockBar.RecordID.ToString());
                    //element.SetAttribute("Name", pDockBar.Name);
                    element.SetAttribute("Visible", pDockBar.Visible.ToString());
                    element.SetAttribute("DockBarStyle", pDockBar.eDockBarStyle.ToString());
                    element.SetAttribute("Location", pDockBar.Location.X.ToString() + "," + pDockBar.Location.Y.ToString());
                    element.SetAttribute("Row", i.ToString());
                    //element.SetAttribute("Column", j.ToString());
                    elementDockBarDockAreaLeft.AppendChild(element);
                }
            }
            //DockBarDockAreaRight
            XmlElement elementDockBarDockAreaRight = elementDock.OwnerDocument.CreateElement("DockBarDockAreaRight");
            elementDockBarDockAreaRight.SetAttribute("Count", this.DockBarDockAreaRight.Rows.Length.ToString());
            elementDock.AppendChild(elementDockBarDockAreaRight);
            for (int i = 0; i < this.DockBarDockAreaRight.Rows.Length; i++)
            {
                ToolStripPanelRow toolStripPanelRow = this.DockBarDockAreaRight.Rows[i];
                for (int j = toolStripPanelRow.Controls.Length - 1; j >= 0; j--)
                {
                    IDockBar pDockBar = toolStripPanelRow.Controls[j] as IDockBar;
                    if (pDockBar == null) continue;
                    XmlElement element = elementDockBarDockAreaRight.OwnerDocument.CreateElement("DockBar");
                    element.SetAttribute("RecordID", pDockBar.RecordID.ToString());
                    //element.SetAttribute("Name", pDockBar.Name);
                    element.SetAttribute("Visible", pDockBar.Visible.ToString());
                    element.SetAttribute("DockBarStyle", pDockBar.eDockBarStyle.ToString());
                    element.SetAttribute("Location", pDockBar.Location.X.ToString() + "," + pDockBar.Location.Y.ToString());
                    element.SetAttribute("Row", i.ToString());
                    //element.SetAttribute("Column", j.ToString());
                    elementDockBarDockAreaRight.AppendChild(element);
                }
            }
            //DockBarDockAreaBottom
            XmlElement elementDockBarDockAreaBottom = elementDock.OwnerDocument.CreateElement("DockBarDockAreaBottom");
            elementDockBarDockAreaBottom.SetAttribute("Count", this.DockBarDockAreaBottom.Rows.Length.ToString());
            elementDock.AppendChild(elementDockBarDockAreaBottom);
            for (int i = 0; i < this.DockBarDockAreaBottom.Rows.Length; i++)
            {
                ToolStripPanelRow toolStripPanelRow = this.DockBarDockAreaBottom.Rows[i];
                for (int j = toolStripPanelRow.Controls.Length - 1; j >= 0; j--)
                {
                    IDockBar pDockBar = toolStripPanelRow.Controls[j] as IDockBar;
                    if (pDockBar == null) continue;
                    XmlElement element = elementDockBarDockAreaBottom.OwnerDocument.CreateElement("DockBar");
                    element.SetAttribute("RecordID", pDockBar.RecordID.ToString());
                    //element.SetAttribute("Name", pDockBar.Name);
                    element.SetAttribute("Visible", pDockBar.Visible.ToString());
                    element.SetAttribute("DockBarStyle", pDockBar.eDockBarStyle.ToString());
                    element.SetAttribute("Location", pDockBar.Location.X.ToString() + "," + pDockBar.Location.Y.ToString());
                    element.SetAttribute("Row", i.ToString());
                    //element.SetAttribute("Column", j.ToString());
                    elementDockBarDockAreaBottom.AppendChild(element);
                }
            }
            //DockBarFloatForms
            XmlElement elementDockBarFloatForms2 = elementDock.OwnerDocument.CreateElement("DockBarFloatForms");
            elementDockBarFloatForms2.SetAttribute("Count", this.DockBarFloatForms.Count.ToString());
            elementDock.AppendChild(elementDockBarFloatForms2);
            foreach (DockBarFloatForm one in this.DockBarFloatForms)
            {
                XmlElement element = elementDockBarFloatForms2.OwnerDocument.CreateElement("DockBarFloatForm");
                element.SetAttribute("RecordID", one.RecordID.ToString());
                //element.SetAttribute("Name", one.Name);
                element.SetAttribute("DockBarRecordID", one.pDockBar.RecordID.ToString());
                //element.SetAttribute("DockBarName", one.pDockBar.Name);
                element.SetAttribute("DockBarStyle", one.pDockBar.eDockBarStyle.ToString());
                elementDockBarFloatForms2.AppendChild(element);
            }
            #endregion
            //
            //
            //
            #region InvisibilityItems

            #endregion
            //
            //
            //
            doc.Save(strFileName);
        }

        private void GetCustomizeInfo(ICustomize pCustomize, XmlElement element, ref int iNum)//获取相关信息
        {
            if (pCustomize == null) return;
            //
            if (pCustomize.CustomizeBaseItems.Count > 0)
            {
                XmlElement elementCustomize = element.OwnerDocument.CreateElement("CustomizeItem");
                elementCustomize.SetAttribute("Name", pCustomize.Name);
                elementCustomize.SetAttribute("Count", pCustomize.CustomizeBaseItems.Count.ToString());
                foreach (ToolStripItem one in pCustomize.CustomizeBaseItems)
                {
                    XmlElement elementItem = elementCustomize.OwnerDocument.CreateElement("Item");
                    elementItem.SetAttribute("Name", one.Name);
                    elementItem.SetAttribute("Index", pCustomize.Items.IndexOf(one).ToString());
                    elementCustomize.AppendChild(elementItem);
                }
                element.AppendChild(elementCustomize);
                iNum++;
            }
            //
            foreach (ToolStripItem one in pCustomize.Items) 
            {
                this.GetCustomizeInfo(one as ICustomize, element, ref iNum);
            }
        }
        #endregion

        #region LoadLayoutFile 加载布局文件
        public void LoadLayoutFile(string strFileName, bool loadParentFormLayout)//加载布局文件，并根据布局文件进行布局
        {
            #region 取消 MenuBar ToolBars CustomizeToolBars StatusBar 布局
            if (this.MenuBar != null) this.MenuBar.RemoveFromParent();
            foreach (ToolBar one in this.ToolBars)
            {
                one.RemoveFromParent();
            }
            foreach (CustomizeToolBar one in this.CustomizeToolBars)
            {
                one.RemoveFromParent();
            }
            if (this.StatusBar != null) this.StatusBar.RemoveFromParent();
            #endregion
            //
            //
            //
            #region 读取布局文件 写入相关属性信息并进行布局
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(strFileName);
            //
            XmlNode xmlNode = xmlDoc.SelectSingleNode("DockBarManager");
            if (xmlNode == null) return;
            //
            this.SetDockBarManager((XmlElement)xmlNode);
            //
            XmlNodeList xmlNodeList = xmlNode.ChildNodes;
            if (xmlNodeList == null) return;
            foreach (XmlNode one in xmlNodeList)
            {
                XmlElement xe = (XmlElement)one;
                switch (xe.Name)
                {
                    case "ParentForm":
                        if (loadParentFormLayout) { this.SetParentForm(xe); }
                        break;
                    case "MenuBar":
                        if (xe.GetAttribute("Count") != "0" && this.MenuBar != null) { this.GetAndSetMenuBarInfo(xe); }
                        break;
                    case "ToolBars":
                        if (xe.GetAttribute("Count") != "0") { this.GetAndSetToolBarsInfo(xe); }
                        break;
                    case "CustomizeToolBars":
                        if (xe.GetAttribute("Count") != "0") { this.GetAndSetCustomizeToolBarsInfo(xe); }
                        break;
                    case "StatusBar":
                        if (xe.GetAttribute("Count") != "0" && this.StatusBar != null) { this.GetAndSetStatusBarInfo(xe); }
                        break;
                    case "ContextMenus":
                        if (xe.GetAttribute("Count") != "0") { this.GetAndSetContextMenusInfo(xe); }
                        break;
                    case "DockBarFloatForms":
                        if (xe.GetAttribute("Count") != "0") { this.GetAndSetDockBarFloatFormsInfo(xe); }
                        break;
                    case "Customize":
                        this.ReadCustomize(xe);
                        break;
                    case "Dock":
                        this.ReadDock(xe);
                        break;
                    default:
                        break;
                }
            }
            #endregion
        }

        //

        private void SetDockBarManager(XmlElement xmlElement)
        {
            this.ShowLargeImage = bool.Parse(xmlElement.GetAttribute("ShowLargeImage"));
            this.ShowItemToolTips = bool.Parse(xmlElement.GetAttribute("ShowItemToolTips"));
        }

        //

        private void SetParentForm(XmlElement xmlElement)//设置父窗体的布局信息
        {
            this.ParentForm.Location = this.ToPoint(xmlElement.GetAttribute("Location"));
            this.ParentForm.Size = this.ToSize(xmlElement.GetAttribute("Size"));
            this.ParentForm.WindowState = this.ToFormWindowState(xmlElement.GetAttribute("WindowState"));
        }

        private void GetAndSetMenuBarInfo(XmlElement xmlElement)
        {
            XmlNodeList xmlNodeList = xmlElement.ChildNodes;
            if (xmlNodeList == null) return;
            foreach (XmlNode one in xmlNodeList)//遍历
            {
                XmlElement xe = (XmlElement)one;//将子节点类型转换为XmlElement类型
                int id = Int32.Parse(xe.GetAttribute("RecordID"));
                //string name = xe.GetAttribute("Name");
                Point point = this.ToPoint(xe.GetAttribute("DockBarFloatFormLocation"));
                Size size = this.ToSize(xe.GetAttribute("DockBarFloatFormSize"));
                //
                WFNew.ISetRecordItemHelper pSetRecordItemHelper = this.MenuBar as WFNew.ISetRecordItemHelper;
                if (pSetRecordItemHelper != null) pSetRecordItemHelper.SetRecordID(id);
                this.MenuBar.DockBarFloatFormSize = size;
                this.MenuBar.DockBarFloatFormLocation = point;
            }
        }

        private void GetAndSetToolBarsInfo(XmlElement xmlElement)
        {
            XmlNodeList xmlNodeList = xmlElement.ChildNodes;
            if (xmlNodeList == null) return;
            foreach (XmlNode one in xmlNodeList)//遍历
            {
                XmlElement xe = (XmlElement)one;//将子节点类型转换为XmlElement类型
                int id = Int32.Parse(xe.GetAttribute("RecordID"));
                string name = xe.GetAttribute("Name");
                Point point = this.ToPoint(xe.GetAttribute("DockBarFloatFormLocation"));
                Size size = this.ToSize(xe.GetAttribute("DockBarFloatFormSize"));
                //
                ToolBar temp = this.ToolBars.GetItem(name);
                if (temp == null) { temp = new ToolBar(); temp.Name = name; this.ToolBars.Add(temp); }
                WFNew.ISetRecordItemHelper pSetRecordItemHelper = temp as WFNew.ISetRecordItemHelper;
                if (pSetRecordItemHelper != null) pSetRecordItemHelper.SetRecordID(id);
                temp.DockBarFloatFormSize = size;
                temp.DockBarFloatFormLocation = point;
            }
        }

        private void GetAndSetCustomizeToolBarsInfo(XmlElement xmlElement)
        {
            XmlNodeList xmlNodeList = xmlElement.ChildNodes;
            if (xmlNodeList == null) return;
            foreach (XmlNode one in xmlNodeList)//遍历
            {
                XmlElement xe = (XmlElement)one;//将子节点类型转换为XmlElement类型
                int id = Int32.Parse(xe.GetAttribute("RecordID"));
                string name = xe.GetAttribute("Name");
                string text = xe.GetAttribute("Text");
                Point point = this.ToPoint(xe.GetAttribute("DockBarFloatFormLocation"));
                Size size = this.ToSize(xe.GetAttribute("DockBarFloatFormSize"));
                //
                //ToolBar temp = this.CustomizeToolBars.GetItem(id);
                //if (temp == null) { temp = new ToolBar(); temp.Name = name; this.CustomizeToolBars.Add(temp); }
                CustomizeToolBar temp = new CustomizeToolBar();
                this.CustomizeToolBars.Add(temp);
                WFNew.ISetRecordItemHelper pSetRecordItemHelper = temp as WFNew.ISetRecordItemHelper;
                if (pSetRecordItemHelper != null) pSetRecordItemHelper.SetRecordID(id);
                temp.Name = name;
                temp.Text = text; 
                temp.DockBarFloatFormSize = size;
                temp.DockBarFloatFormLocation = point;
            }
        }

        private void GetAndSetStatusBarInfo(XmlElement xmlElement)
        {
            XmlNodeList xmlNodeList = xmlElement.ChildNodes;
            if (xmlNodeList == null) return;
            foreach (XmlNode one in xmlNodeList)//遍历
            {
                XmlElement xe = (XmlElement)one;//将子节点类型转换为XmlElement类型
                int id = Int32.Parse(xe.GetAttribute("RecordID"));
                //string name = xe.GetAttribute("Name");
                Point point = this.ToPoint(xe.GetAttribute("DockBarFloatFormLocation"));
                Size size = this.ToSize(xe.GetAttribute("DockBarFloatFormSize"));
                // 
                WFNew.ISetRecordItemHelper pSetRecordItemHelper = this.StatusBar as WFNew.ISetRecordItemHelper;
                if (pSetRecordItemHelper != null) pSetRecordItemHelper.SetRecordID(id);
                this.StatusBar.DockBarFloatFormSize = size;
                this.StatusBar.DockBarFloatFormLocation = point;
            }
        }

        private void GetAndSetContextMenusInfo(XmlElement xmlElement)
        {
            XmlNodeList xmlNodeList = xmlElement.ChildNodes;
            if (xmlNodeList == null) return;
            foreach (XmlNode one in xmlNodeList)//遍历
            {
                XmlElement xe = (XmlElement)one;//将子节点类型转换为XmlElement类型
                int id = Int32.Parse(xe.GetAttribute("RecordID"));
                string name = xe.GetAttribute("Name");
                //
                ContextMenu temp = this.ContextMenus.GetItem(name);
                if (temp == null) { temp = new ContextMenu(); temp.Name = name; this.ContextMenus.Add(temp); }
                WFNew.ISetRecordItemHelper pSetRecordItemHelper = temp as WFNew.ISetRecordItemHelper;
                if (pSetRecordItemHelper != null) pSetRecordItemHelper.SetRecordID(id);
            }
        }

        private void GetAndSetDockBarFloatFormsInfo(XmlElement xmlElement)
        {
            XmlNodeList xmlNodeList = xmlElement.ChildNodes;
            if (xmlNodeList == null) return;
            foreach (XmlNode one in xmlNodeList)//遍历
            {
                XmlElement xe = (XmlElement)one;//将子节点类型转换为XmlElement类型
                int id = Int32.Parse(xe.GetAttribute("RecordID"));
                string name = xe.GetAttribute("Name");
                Point point = this.ToPoint(xe.GetAttribute("Location"));
                Size size = this.ToSize(xe.GetAttribute("Size"));
                //
                //DockBarFloatForm temp = this.DockBarFloatForms.GetItem(id);
                //if (temp == null) { temp = new ToolBar(); temp.Name = name; this.DockBarFloatForms.Add(temp); }
                DockBarFloatForm temp = new DockBarFloatForm();
                this.DockBarFloatForms.Add(temp);
                WFNew.ISetRecordItemHelper pSetRecordItemHelper = temp as WFNew.ISetRecordItemHelper;
                if (pSetRecordItemHelper != null) pSetRecordItemHelper.SetRecordID(id);
                temp.Name = name;
                temp.Size = size;
                temp.Location = point;
            }
        }

        //

        private void ReadCustomize(XmlElement xmlElement)
        {
            XmlNodeList xmlNodeList = xmlElement.ChildNodes;
            if (xmlNodeList == null) return;
            foreach (XmlNode one in xmlNodeList)//遍历
            {
                XmlElement xe = (XmlElement)one;
                switch (xe.Name)
                {
                    case "MenuBar":
                        if (xe.GetAttribute("Count") != "0") { this.GetAndSetCustomizeMenuBar(xe); }
                        break;
                    case "ToolBars":
                        if (xe.GetAttribute("Count") != "0") { this.GetAndSetCustomizeToolBars(xe); }
                        break;
                    case "CustomizeToolBars":
                        if (xe.GetAttribute("Count") != "0") { this.GetAndSetCustomizeCustomizeToolBars(xe); }
                        break;
                    case "StatusBar":
                        if (xe.GetAttribute("Count") != "0") { this.GetAndSetCustomizeStatusBar(xe); }
                        break;
                    case "ContextMenus":
                        if (xe.GetAttribute("Count") != "0") { this.GetAndSetCustomizeContextMenus(xe); }
                        break;
                    case "CustomizeItem":
                        if (xe.GetAttribute("Count") != "0") { this.GetAndSetCustomizeCustomizeItem(xe); }
                        break;
                    default:
                        break;
                }
            }
        }

        private void GetAndSetCustomizeMenuBar(XmlElement xmlElement)
        {
            XmlNodeList xmlNodeList = xmlElement.ChildNodes;
            if (xmlNodeList == null) return;
            foreach (XmlNode one in xmlNodeList)//遍历
            {
                XmlElement xe = (XmlElement)one;//将子节点类型转换为XmlElement类型
                //int id = Int32.Parse(xe.GetAttribute("RecordID"));
                //
                this.SetBaseItem(this.MenuBar as ICustomize, xe);
            }
        }

        private void GetAndSetCustomizeToolBars(XmlElement xmlElement)
        {
            XmlNodeList xmlNodeList = xmlElement.ChildNodes;
            if (xmlNodeList == null) return;
            foreach (XmlNode one in xmlNodeList)//遍历
            {
                XmlElement xe = (XmlElement)one;//将子节点类型转换为XmlElement类型
                int id = Int32.Parse(xe.GetAttribute("RecordID"));
                //
                this.SetBaseItem(this.ToolBars.GetItem(id) as ICustomize, xe);
            }
        }

        private void GetAndSetCustomizeCustomizeToolBars(XmlElement xmlElement)
        {
            XmlNodeList xmlNodeList = xmlElement.ChildNodes;
            if (xmlNodeList == null) return;
            foreach (XmlNode one in xmlNodeList)//遍历
            {
                XmlElement xe = (XmlElement)one;//将子节点类型转换为XmlElement类型
                int id = Int32.Parse(xe.GetAttribute("RecordID"));
                //
                this.SetBaseItem(this.CustomizeToolBars.GetItem(id) as ICustomize, xe);
            }
        }

        private void GetAndSetCustomizeStatusBar(XmlElement xmlElement)
        {
            XmlNodeList xmlNodeList = xmlElement.ChildNodes;
            if (xmlNodeList == null) return;
            foreach (XmlNode one in xmlNodeList)//遍历
            {
                XmlElement xe = (XmlElement)one;//将子节点类型转换为XmlElement类型
                int id = Int32.Parse(xe.GetAttribute("RecordID"));
                //
                this.SetBaseItem(this.StatusBar as ICustomize, xe);
            }
        }

        private void GetAndSetCustomizeContextMenus(XmlElement xmlElement)
        {
            XmlNodeList xmlNodeList = xmlElement.ChildNodes;
            if (xmlNodeList == null) return;
            foreach (XmlNode one in xmlNodeList)//遍历
            {
                XmlElement xe = (XmlElement)one;//将子节点类型转换为XmlElement类型
                int id = Int32.Parse(xe.GetAttribute("RecordID"));
                //
                this.SetBaseItem(this.ContextMenus.GetItem(id) as ICustomize, xe);
            }
        }

        private void GetAndSetCustomizeCustomizeItem(XmlElement xmlElement)
        {
            XmlNodeList xmlNodeList = xmlElement.ChildNodes;
            if (xmlNodeList == null) return;
            foreach (XmlNode one in xmlNodeList)//遍历
            {
                XmlElement xe = (XmlElement)one;//将子节点类型转换为XmlElement类型
                string name = xe.GetAttribute("Name");
                //
                this.SetBaseItem(this.GetToolStripItem(name) as ICustomize, xe);
            }
        }

        private void SetBaseItem(ICustomize pCustomize, XmlElement xmlElement)
        {
            if(pCustomize == null) return;
            //
            int id = -1;
            string name = "";
            string motherBody = "";
            IBaseItemDB pBaseItem = null;
            XmlNodeList xmlNodeList = xmlElement.ChildNodes;
            foreach (XmlNode one in xmlNodeList)//遍历
            {
                XmlElement xe = (XmlElement)one;
                switch (one.Name)
                {
                    case "Item":
                        name = xe.GetAttribute("Name");
                        id = Int32.Parse(xe.GetAttribute("Index"));
                        if (name.Contains("[GUID]")) { motherBody = name.Remove(name.LastIndexOf("[GUID]")); }
                        else { motherBody = name; }
                        pBaseItem = pCustomize.AddCustomizeBaseItemEx(id, this.BaseItems.GetItem(motherBody));
                        if (pBaseItem != null) { pBaseItem.Name = name; }
                        break;
                    default:
                        break;
                }
            }
        }

        //

        private void ReadDock(XmlElement xmlElement)
        {
            XmlNodeList xmlNodeList = xmlElement.ChildNodes;
            if (xmlNodeList == null) return;
            foreach (XmlNode one in xmlNodeList)//遍历
            {
                XmlElement xe = (XmlElement)one;
                switch (xe.Name)
                {
                    case "DockBarDockAreaTop":
                        if (xe.GetAttribute("Count") != "0") { this.GetAndSetDockBarDockAreaTop(xe); }
                        break;
                    case "DockBarDockAreaLeft":
                        if (xe.GetAttribute("Count") != "0") { this.GetAndSetDockBarDockAreaLeft(xe); }
                        break;
                    case "DockBarDockAreaRight":
                        if (xe.GetAttribute("Count") != "0") { this.GetAndSetDockBarDockAreaRight(xe); }
                        break;
                    case "DockBarDockAreaBottom":
                        if (xe.GetAttribute("Count") != "0") { this.GetAndSetDockBarDockAreaBottom(xe); }
                        break;
                    case "DockBarFloatForms":
                        if (xe.GetAttribute("Count") != "0") { this.GetAndSetDockBarFloatForms(xe); }
                        break;
                    default:
                        break;
                }
            }
        }

        private void GetAndSetDockBarDockAreaTop(XmlElement xmlElement)
        {
            XmlNodeList xmlNodeList = xmlElement.ChildNodes;
            if (xmlNodeList == null) return;
            foreach (XmlNode one in xmlNodeList)//遍历
            {
                XmlElement xe = (XmlElement)one;//将子节点类型转换为XmlElement类型
                int id = Int32.Parse(xe.GetAttribute("RecordID"));
                bool visible = bool.Parse(xe.GetAttribute("Visible"));
                Point point = this.ToPoint(xe.GetAttribute("Location"));
                int iRow = Int32.Parse(xe.GetAttribute("Row"));
                //int iColumn = Int32.Parse(xe.GetAttribute("Column"));
                string strDockBarStyle = xe.GetAttribute("DockBarStyle");
                ToolBar toolBar = null;
                switch (strDockBarStyle)
                {
                    case "eMenuBar":
                        if (this.MenuBar != null) { this.MenuBar.ToDockArea(DockStyle.Top, iRow); this.MenuBar.Visible = visible; }
                        break;
                    case "eToolBar":
                        toolBar = this.ToolBars.GetItem(id);
                        if (toolBar != null) { toolBar.ToDockArea(DockStyle.Top, iRow, point); toolBar.Visible = visible; }
                        break;
                    case "eCustomizeToolBar":
                        toolBar = this.CustomizeToolBars.GetItem(id);
                        if (toolBar != null) { toolBar.ToDockArea(DockStyle.Top, iRow, point); toolBar.Visible = visible; }
                        break;
                    case "eStatusBar":
                        if (this.StatusBar != null) { this.StatusBar.ToDockArea(DockStyle.Top, iRow); this.StatusBar.Visible = visible; }
                        break;
                }
            }
        }

        private void GetAndSetDockBarDockAreaLeft(XmlElement xmlElement)
        {
            XmlNodeList xmlNodeList = xmlElement.ChildNodes;
            if (xmlNodeList == null) return;
            foreach (XmlNode one in xmlNodeList)//遍历
            {
                XmlElement xe = (XmlElement)one;//将子节点类型转换为XmlElement类型
                int id = Int32.Parse(xe.GetAttribute("RecordID"));
                bool visible = bool.Parse(xe.GetAttribute("Visible"));
                Point point = this.ToPoint(xe.GetAttribute("Location"));
                int iRow = Int32.Parse(xe.GetAttribute("Row"));
                //int iColumn = Int32.Parse(xe.GetAttribute("Column"));
                string strDockBarStyle = xe.GetAttribute("DockBarStyle");
                ToolBar toolBar = null;
                switch (strDockBarStyle)
                {
                    case "eMenuBar":
                        if (this.MenuBar != null) { this.MenuBar.ToDockArea(DockStyle.Left, iRow); this.MenuBar.Visible = visible; }
                        break;
                    case "eToolBar":
                        toolBar = this.ToolBars.GetItem(id);
                        if (toolBar != null) { toolBar.ToDockArea(DockStyle.Left, iRow, point); toolBar.Visible = visible; }
                        break;
                    case "eCustomizeToolBar":
                        toolBar = this.CustomizeToolBars.GetItem(id);
                        if (toolBar != null) { toolBar.ToDockArea(DockStyle.Left, iRow, point); toolBar.Visible = visible; }
                        break;
                    case "eStatusBar":
                        if (this.StatusBar != null) { this.StatusBar.ToDockArea(DockStyle.Left, iRow); this.StatusBar.Visible = visible; }
                        break;
                }
            }
        }

        private void GetAndSetDockBarDockAreaRight(XmlElement xmlElement)
        {
            XmlNodeList xmlNodeList = xmlElement.ChildNodes;
            if (xmlNodeList == null) return;
            foreach (XmlNode one in xmlNodeList)//遍历
            {
                XmlElement xe = (XmlElement)one;//将子节点类型转换为XmlElement类型
                int id = Int32.Parse(xe.GetAttribute("RecordID"));
                bool visible = bool.Parse(xe.GetAttribute("Visible"));
                Point point = this.ToPoint(xe.GetAttribute("Location"));
                int iRow = Int32.Parse(xe.GetAttribute("Row"));
                //int iColumn = Int32.Parse(xe.GetAttribute("Column"));
                string strDockBarStyle = xe.GetAttribute("DockBarStyle");
                ToolBar toolBar = null;
                switch (strDockBarStyle)
                {
                    case "eMenuBar":
                        if (this.MenuBar != null) { this.MenuBar.ToDockArea(DockStyle.Right, iRow); this.MenuBar.Visible = visible; }
                        break;
                    case "eToolBar":
                        toolBar = this.ToolBars.GetItem(id);
                        if (toolBar != null) { toolBar.ToDockArea(DockStyle.Right, iRow, point); toolBar.Visible = visible; }
                        break;
                    case "eCustomizeToolBar":
                        toolBar = this.CustomizeToolBars.GetItem(id);
                        if (toolBar != null) { toolBar.ToDockArea(DockStyle.Right, iRow, point); toolBar.Visible = visible; }
                        break;
                    case "eStatusBar":
                        if (this.StatusBar != null) { this.StatusBar.ToDockArea(DockStyle.Right, iRow); this.StatusBar.Visible = visible; }
                        break;
                }
            }
        }

        private void GetAndSetDockBarDockAreaBottom(XmlElement xmlElement)
        {
            XmlNodeList xmlNodeList = xmlElement.ChildNodes;
            if (xmlNodeList == null) return;
            foreach (XmlNode one in xmlNodeList)//遍历
            {
                XmlElement xe = (XmlElement)one;//将子节点类型转换为XmlElement类型
                int id = Int32.Parse(xe.GetAttribute("RecordID"));
                bool visible = bool.Parse(xe.GetAttribute("Visible"));
                Point point = this.ToPoint(xe.GetAttribute("Location"));
                int iRow = Int32.Parse(xe.GetAttribute("Row"));
                //int iColumn = Int32.Parse(xe.GetAttribute("Column"));
                string strDockBarStyle = xe.GetAttribute("DockBarStyle");
                ToolBar toolBar = null;
                switch (strDockBarStyle)
                {
                    case "eMenuBar":
                        if (this.MenuBar != null) { this.MenuBar.ToDockArea(DockStyle.Bottom, iRow); this.MenuBar.Visible = visible; }
                        break;
                    case "eToolBar":
                        toolBar = this.ToolBars.GetItem(id);
                        if (toolBar != null) { toolBar.ToDockArea(DockStyle.Bottom, iRow, point); toolBar.Visible = visible; }
                        break;
                    case "eCustomizeToolBar":
                        toolBar = this.CustomizeToolBars.GetItem(id);
                        if (toolBar != null) { toolBar.ToDockArea(DockStyle.Bottom, iRow, point); toolBar.Visible = visible; }
                        break;
                    case "eStatusBar":
                        if (this.StatusBar != null) { this.StatusBar.ToDockArea(DockStyle.Bottom, iRow); this.StatusBar.Visible = visible; }
                        break;
                }
            }
        }

        private void GetAndSetDockBarFloatForms(XmlElement xmlElement)
        {
            XmlNodeList xmlNodeList = xmlElement.ChildNodes;
            if (xmlNodeList == null) return;
            foreach (XmlNode one in xmlNodeList)//遍历
            {
                XmlElement xe = (XmlElement)one;//将子节点类型转换为XmlElement类型
                int id = Int32.Parse(xe.GetAttribute("RecordID"));
                int iDockBarId = Int32.Parse(xe.GetAttribute("DockBarRecordID"));
                string strDockBarStyle = xe.GetAttribute("DockBarStyle");
                DockBarFloatForm dockBarFloatForm = this.DockBarFloatForms.GetItem(id);
                if (dockBarFloatForm == null) return;
                ToolBar toolBar = null;
                switch (strDockBarStyle)
                {
                    case "eMenuBar":
                        if (this.MenuBar != null) this.MenuBar.ToDockBarFloatForm(dockBarFloatForm);
                        break;
                    case "eToolBar":
                        toolBar = this.ToolBars.GetItem(iDockBarId);
                        if (toolBar != null) toolBar.ToDockBarFloatForm(dockBarFloatForm);
                        break;
                    case "eCustomizeToolBar":
                        toolBar = this.CustomizeToolBars.GetItem(iDockBarId);
                        if (toolBar != null) toolBar.ToDockBarFloatForm(dockBarFloatForm);
                        break;
                    case "eStatusBar":
                        if (this.StatusBar != null) this.StatusBar.ToDockBarFloatForm(dockBarFloatForm);
                        break;
                }
            }
        }

        //

        private FormWindowState ToFormWindowState(string str)
        {
            if (str == FormWindowState.Maximized.ToString()) return FormWindowState.Maximized;
            else if (str == FormWindowState.Minimized.ToString()) return FormWindowState.Minimized;
            else return FormWindowState.Normal;
        }

        private Point ToPoint(string str)
        {
            try
            {
                string[] strList = str.Split(',');
                return new Point(Int32.Parse(strList[0]), Int32.Parse(strList[1]));
            }
            catch { GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show("布局文件损坏！"); return new Point(60, 60); }
        }

        private Size ToSize(string str)
        {
            try
            {
                string[] strList = str.Split(',');
                return new Size(Int32.Parse(strList[0]), Int32.Parse(strList[1]));
            }
            catch { GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show("布局文件损坏！"); return new Size(260, 260); }
        }
        #endregion

        //
        //
        //

        #region 附件类
        public class BaseItemCollection : WFNew.FlexibleList<IBaseItemDB>
        {
            private List<IBaseItemDB> innerList = null;

            internal BaseItemCollection()
            {
                this.innerList = new List<IBaseItemDB>();
            }

            public override int Add(IBaseItemDB value)
            {
                if (this.Locked) return -1;
                //
                if (this.innerList.Contains(value)) return -1;
                //
                this.innerList.Add(value);
                //
                return this.innerList.Count - 1;
            }

            public override void Clear()
            {
                if (this.Locked) return;
                //
                this.innerList.Clear();
            }

            public override bool Contains(IBaseItemDB value)
            {
                return this.innerList.Contains(value);
            }

            public override int IndexOf(IBaseItemDB value)
            {
                return this.innerList.IndexOf(value);
            }

            public override void Insert(int index, IBaseItemDB value)
            {
                if (this.Locked) return;
                //
                if (this.innerList.Contains(value)) return;
                //
                this.innerList.Insert(index, value);
            }

            public override void Remove(IBaseItemDB value)
            {
                if (this.Locked) return;
                //
                this.innerList.Remove(value);
            }

            public override void RemoveAt(int index)
            {
                if (this.Locked) return;
                //
                this.innerList.RemoveAt(index);
            }

            public override IEnumerator GetEnumerator()
            {
                return this.innerList.GetEnumerator();
            }

            [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public override IBaseItemDB this[int index]
            {
                get
                {
                    if ((index < 0) || (index >= this.Count))
                    {
                        return null;
                    }
                    return this.innerList[index];
                }
                set
                {
                    if (this.Locked) return;
                    //
                    this.innerList[index] = value;
                }
            }

            public override int Count
            {
                get
                {
                    return this.innerList.Count;
                }
            }

            public override bool ExchangeItemT(IBaseItemDB item1, IBaseItemDB item2)
            {
                if (this.Locked) return false;
                //
                if (item1 == null || item2 == null || item1 == item2) return false;
                //
                int index1 = this.innerList.IndexOf(item1);
                int index2 = this.innerList.IndexOf(item2);
                //
                if (index1 == index2) return false;
                if ((index1 < 0) || (index1 >= this.innerList.Count)) return false;
                if ((index2 < 0) || (index2 >= this.innerList.Count)) return false;
                //
                if (index1 < index2)
                {
                    this.innerList.Remove(item2);
                    this.innerList.Remove(item1);
                    this.innerList.Insert(index1, item2);
                    this.innerList.Insert(index2, item1);
                }
                else
                {
                    this.innerList.Remove(item1);
                    this.innerList.Remove(item2);
                    this.innerList.Insert(index2, item1);
                    this.innerList.Insert(index1, item2);
                }
                //
                return true;
            }

            public IBaseItemDB this[string name]
            {
                get
                {
                    foreach (IBaseItemDB one in this.innerList)
                    {
                        if (one.Name == name) return one;
                    }
                    //
                    return null;
                }
            }

            //
            //
            //

            //public void SetRecordID()
            //{
            //    for (int i = 0; i < this.Count; i++)
            //    {
            //        this[i].SetRecordID(i);
            //    }
            //}

            //public IBaseItemDB GetItem(int recordID)
            //{
            //    for (int i = 0; i < this.Count; i++)
            //    {
            //        if (this[i].RecordID == recordID) return this[i];
            //    }
            //    return null;
            //}

            public IBaseItemDB GetItem(string name)
            {
                for (int i = 0; i < this.Count; i++)
                {
                    if (this[i].Name == name) return this[i];
                }
                return null;
            }
        }

        public class ToolBarCollection : WFNew.FlexibleList<ToolBar>
        {
            private DockBarManager owner = null;
            private List<ToolBar> innerList = null;

            internal ToolBarCollection(DockBarManager dockBarManager)
            {
                this.owner = dockBarManager;
                this.innerList = new List<ToolBar>();
            }

            public override int Add(ToolBar value)
            {
                if (this.Locked) return -1;
                //
                if (this.innerList.Contains(value)) return -1;
                //
                value.SetShowItemToolTips(this.owner.ShowItemToolTips);
                if (this.owner.ShowLargeImage) { value.SetImageScalingSize(new Size(32, 32)); }
                else { value.SetImageScalingSize(new Size(16, 16)); }
                ISetDockBarManagerHelper pSetDockBarManagerHelper = value as ISetDockBarManagerHelper;
                if (pSetDockBarManagerHelper != null) pSetDockBarManagerHelper.SetDockBarManager(owner);
                //value.SetDockBarManager(owner);
                this.innerList.Add(value);
                //
                return this.innerList.Count - 1;
            }

            public override void Clear()
            {
                if (this.Locked) return;
                //
                this.innerList.Clear();
            }

            public override bool Contains(ToolBar value)
            {
                return this.innerList.Contains(value);
            }

            public override int IndexOf(ToolBar value)
            {
                return this.innerList.IndexOf(value);
            }

            public override void Insert(int index, ToolBar value)
            {
                if (this.Locked) return;
                //
                if (this.innerList.Contains(value)) return;
                //
                value.SetShowItemToolTips(this.owner.ShowItemToolTips);
                if (this.owner.ShowLargeImage) { value.SetImageScalingSize(new Size(32, 32)); }
                else { value.SetImageScalingSize(new Size(16, 16)); }
                ISetDockBarManagerHelper pSetDockBarManagerHelper = value as ISetDockBarManagerHelper;
                if (pSetDockBarManagerHelper != null) pSetDockBarManagerHelper.SetDockBarManager(owner);
                //value.SetDockBarManager(owner);
                this.innerList.Insert(index, value);
            }

            public override void Remove(ToolBar value)
            {
                if (this.Locked) return;
                //
                this.innerList.Remove(value);
            }

            public override void RemoveAt(int index)
            {
                if (this.Locked) return;
                //
                this.innerList.RemoveAt(index);
            }

            public override IEnumerator GetEnumerator()
            {
                return this.innerList.GetEnumerator();
            }

            [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public override ToolBar this[int index]
            {
                get
                {
                    if ((index < 0) || (index >= this.Count))
                    {
                        return null;
                    }
                    return this.innerList[index];
                }
                set
                {
                    if (this.Locked) return;
                    //
                    this.innerList[index] = value;
                }
            }

            public override int Count
            {
                get
                {
                    return this.innerList.Count;
                }
            }

            public override bool ExchangeItemT(ToolBar item1, ToolBar item2)
            {
                if (this.Locked) return false;
                //
                if (item1 == null || item2 == null || item1 == item2) return false;
                //
                int index1 = this.innerList.IndexOf(item1);
                int index2 = this.innerList.IndexOf(item2);
                //
                if (index1 == index2) return false;
                if ((index1 < 0) || (index1 >= this.innerList.Count)) return false;
                if ((index2 < 0) || (index2 >= this.innerList.Count)) return false;
                //
                if (index1 < index2)
                {
                    this.innerList.Remove(item2);
                    this.innerList.Remove(item1);
                    this.innerList.Insert(index1, item2);
                    this.innerList.Insert(index2, item1);
                }
                else
                {
                    this.innerList.Remove(item1);
                    this.innerList.Remove(item2);
                    this.innerList.Insert(index2, item1);
                    this.innerList.Insert(index1, item2);
                }
                //
                return true;
            }

            public ToolBar this[string name]
            {
                get
                {
                    foreach (ToolBar one in this.innerList)
                    {
                        if (one.Name == name) return one;
                    }
                    //
                    return null;
                }
            }

            //
            //
            //

            public void SetRecordID()
            {
                for (int i = 0; i < this.innerList.Count; i++)
                {
                    WFNew.ISetRecordItemHelper pSetRecordItemHelper = this.innerList[i] as WFNew.ISetRecordItemHelper;
                    if (pSetRecordItemHelper != null) pSetRecordItemHelper.SetRecordID(i);
                }
            }

            public ToolBar GetItem(int recordID)
            {
                for (int i = 0; i < this.Count; i++)
                {
                    if (this[i].RecordID == recordID) return this[i];
                }
                return null;
            }

            public ToolBar GetItem(string name)
            {
                for (int i = 0; i < this.Count; i++)
                {
                    if (this[i].Name == name) return this[i];
                }
                return null;
            }
        }

        public class ContextMenuCollection : WFNew.FlexibleList<ContextMenu>
        {
            private DockBarManager owner = null;
            private List<ContextMenu> innerList = null;

            internal ContextMenuCollection(DockBarManager dockBarManager)
            {
                this.owner = dockBarManager;
                this.innerList = new List<ContextMenu>();
            }

            public override int Add(ContextMenu value)
            {
                if (this.Locked) return -1;
                //
                if (this.innerList.Contains(value)) return -1;
                //
                value.SetShowItemToolTips(this.owner.ShowItemToolTips);
                if (this.owner.ShowLargeImage) { value.SetImageScalingSize(new Size(32, 32)); }
                else { value.SetImageScalingSize(new Size(16, 16)); }
                this.innerList.Add(value);
                //
                return this.innerList.Count - 1;
            }

            public override void Clear()
            {
                if (this.Locked) return;
                //
                this.innerList.Clear();
            }

            public override bool Contains(ContextMenu value)
            {
                return this.innerList.Contains(value);
            }

            public override int IndexOf(ContextMenu value)
            {
                return this.innerList.IndexOf(value);
            }

            public override void Insert(int index, ContextMenu value)
            {
                if (this.Locked) return;
                //
                if (this.innerList.Contains(value)) return;
                //
                value.SetShowItemToolTips(this.owner.ShowItemToolTips);
                if (this.owner.ShowLargeImage) { value.SetImageScalingSize(new Size(32, 32)); }
                else { value.SetImageScalingSize(new Size(16, 16)); }
                this.innerList.Insert(index, value);
            }

            public override void Remove(ContextMenu value)
            {
                if (this.Locked) return;
                //
                this.innerList.Remove(value);
            }

            public override void RemoveAt(int index)
            {
                if (this.Locked) return;
                //
                this.innerList.RemoveAt(index);
            }

            public override IEnumerator GetEnumerator()
            {
                return this.innerList.GetEnumerator();
            }

            [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public override ContextMenu this[int index]
            {
                get
                {
                    if ((index < 0) || (index >= this.Count))
                    {
                        return null;
                    }
                    return this.innerList[index];
                }
                set
                {
                    if (this.Locked) return;
                    //
                    this.innerList[index] = value;
                }
            }

            public override int Count
            {
                get
                {
                    return this.innerList.Count;
                }
            }

            public override bool ExchangeItemT(ContextMenu item1, ContextMenu item2)
            {
                if (this.Locked) return false;
                //
                if (item1 == null || item2 == null || item1 == item2) return false;
                //
                int index1 = this.innerList.IndexOf(item1);
                int index2 = this.innerList.IndexOf(item2);
                //
                if (index1 == index2) return false;
                if ((index1 < 0) || (index1 >= this.innerList.Count)) return false;
                if ((index2 < 0) || (index2 >= this.innerList.Count)) return false;
                //
                if (index1 < index2)
                {
                    this.innerList.Remove(item2);
                    this.innerList.Remove(item1);
                    this.innerList.Insert(index1, item2);
                    this.innerList.Insert(index2, item1);
                }
                else
                {
                    this.innerList.Remove(item1);
                    this.innerList.Remove(item2);
                    this.innerList.Insert(index2, item1);
                    this.innerList.Insert(index1, item2);
                }
                //
                return true;
            }

            public ContextMenu this[string name]
            {
                get
                {
                    foreach (ContextMenu one in this.innerList)
                    {
                        if (one.Name == name) return one;
                    }
                    //
                    return null;
                }
            }

            //
            //
            //

            public void SetRecordID()
            {
                for (int i = 0; i < this.innerList.Count; i++)
                {
                    WFNew.ISetRecordItemHelper pSetRecordItemHelper = this.innerList[i] as WFNew.ISetRecordItemHelper;
                    if (pSetRecordItemHelper != null) pSetRecordItemHelper.SetRecordID(i);
                }
            }

            public ContextMenu GetItem(int recordID)
            {
                for (int i = 0; i < this.Count; i++)
                {
                    if (this[i].RecordID == recordID) return this[i];
                }
                return null;
            }

            public ContextMenu GetItem(string name)
            {
                for (int i = 0; i < this.Count; i++)
                {
                    if (this[i].Name == name) return this[i];
                }
                return null;
            }
        }

        internal class CustomizeToolBarCollection : WFNew.FlexibleList<CustomizeToolBar>
        {
            private DockBarManager owner = null;
            private List<CustomizeToolBar> innerList = null;

            internal CustomizeToolBarCollection(DockBarManager dockBarManager)
            {
                this.owner = dockBarManager;
                this.innerList = new List<CustomizeToolBar>();
            }

            public override int Add(CustomizeToolBar value)
            {
                if (this.Locked) return -1;
                //
                if (this.innerList.Contains(value)) return -1;
                //
                value.SetShowItemToolTips(this.owner.ShowItemToolTips);
                if (this.owner.ShowLargeImage) { value.SetImageScalingSize(new Size(32, 32)); }
                else { value.SetImageScalingSize(new Size(16, 16)); }
                ISetDockBarManagerHelper pSetDockBarManagerHelper = value as ISetDockBarManagerHelper;
                if (pSetDockBarManagerHelper != null) pSetDockBarManagerHelper.SetDockBarManager(owner);
                //value.SetDockBarManager(owner);
                this.innerList.Add(value);
                //
                return this.innerList.Count - 1;
            }

            public override void Clear()
            {
                if (this.Locked) return;
                //
                this.innerList.Clear();
            }

            public override bool Contains(CustomizeToolBar value)
            {
                return this.innerList.Contains(value);
            }

            public override int IndexOf(CustomizeToolBar value)
            {
                return this.innerList.IndexOf(value);
            }

            public override void Insert(int index, CustomizeToolBar value)
            {
                if (this.Locked) return;
                //
                if (this.innerList.Contains(value)) return;
                //
                value.SetShowItemToolTips(this.owner.ShowItemToolTips);
                if (this.owner.ShowLargeImage) { value.SetImageScalingSize(new Size(32, 32)); }
                else { value.SetImageScalingSize(new Size(16, 16)); }
                ISetDockBarManagerHelper pSetDockBarManagerHelper = value as ISetDockBarManagerHelper;
                if (pSetDockBarManagerHelper != null) pSetDockBarManagerHelper.SetDockBarManager(owner);
                //value.SetDockBarManager(owner);
                this.innerList.Insert(index, value);
            }

            public override void Remove(CustomizeToolBar value)
            {
                if (this.Locked) return;
                //
                this.innerList.Remove(value);
            }

            public override void RemoveAt(int index)
            {
                if (this.Locked) return;
                //
                this.innerList.RemoveAt(index);
            }

            public override IEnumerator GetEnumerator()
            {
                return this.innerList.GetEnumerator();
            }

            [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public override CustomizeToolBar this[int index]
            {
                get
                {
                    if ((index < 0) || (index >= this.Count))
                    {
                        return null;
                    }
                    return this.innerList[index];
                }
                set
                {
                    if (this.Locked) return;
                    //
                    this.innerList[index] = value;
                }
            }

            public override int Count
            {
                get
                {
                    return this.innerList.Count;
                }
            }

            public override bool ExchangeItemT(CustomizeToolBar item1, CustomizeToolBar item2)
            {
                if (this.Locked) return false;
                //
                if (item1 == null || item2 == null || item1 == item2) return false;
                //
                int index1 = this.innerList.IndexOf(item1);
                int index2 = this.innerList.IndexOf(item2);
                //
                if (index1 == index2) return false;
                if ((index1 < 0) || (index1 >= this.innerList.Count)) return false;
                if ((index2 < 0) || (index2 >= this.innerList.Count)) return false;
                //
                if (index1 < index2)
                {
                    this.innerList.Remove(item2);
                    this.innerList.Remove(item1);
                    this.innerList.Insert(index1, item2);
                    this.innerList.Insert(index2, item1);
                }
                else
                {
                    this.innerList.Remove(item1);
                    this.innerList.Remove(item2);
                    this.innerList.Insert(index2, item1);
                    this.innerList.Insert(index1, item2);
                }
                //
                return true;
            }

            public CustomizeToolBar this[string name]
            {
                get
                {
                    foreach (CustomizeToolBar one in this.innerList)
                    {
                        if (one.Name == name) return one;
                    }
                    //
                    return null;
                }
            }

            //
            //
            //

            public void SetRecordID()
            {
                for (int i = 0; i < this.innerList.Count; i++)
                {
                    WFNew.ISetRecordItemHelper pSetRecordItemHelper = this.innerList[i] as WFNew.ISetRecordItemHelper;
                    if (pSetRecordItemHelper != null) pSetRecordItemHelper.SetRecordID(i);
                }
            }

            public CustomizeToolBar GetItem(int recordID)
            {
                for (int i = 0; i < this.Count; i++)
                {
                    if (this[i].RecordID == recordID) return this[i];
                }
                return null;
            }

            public CustomizeToolBar GetItem(string name)
            {
                for (int i = 0; i < this.Count; i++)
                {
                    if (this[i].Name == name) return this[i];
                }
                return null;
            }
        }

        internal class DockBarFloatFormCollection : WFNew.FlexibleList<DockBarFloatForm>
        {
            private DockBarManager owner = null;
            private List<DockBarFloatForm> innerList = null;

            internal DockBarFloatFormCollection(DockBarManager dockPanelManager)
            {
                this.owner = dockPanelManager;
                this.innerList = new List<DockBarFloatForm>();
            }

            public override int Add(DockBarFloatForm value)
            {
                if (this.Locked) return -1;
                //
                if (this.innerList.Contains(value)) return -1;
                //
                ISetDockBarManagerHelper pSetDockBarManagerHelper = value as ISetDockBarManagerHelper;
                if (pSetDockBarManagerHelper != null) pSetDockBarManagerHelper.SetDockBarManager(owner);
                this.innerList.Add(value);
                //
                return this.innerList.Count - 1;
            }

            public override void Clear()
            {
                if (this.Locked) return;
                //
                this.innerList.Clear();
            }

            public override bool Contains(DockBarFloatForm value)
            {
                return this.innerList.Contains(value);
            }

            public override int IndexOf(DockBarFloatForm value)
            {
                return this.innerList.IndexOf(value);
            }

            public override void Insert(int index, DockBarFloatForm value)
            {
                if (this.Locked) return;
                //
                if (this.innerList.Contains(value)) return;
                //
                ISetDockBarManagerHelper pSetDockBarManagerHelper = value as ISetDockBarManagerHelper;
                if (pSetDockBarManagerHelper != null) pSetDockBarManagerHelper.SetDockBarManager(owner);
                this.innerList.Insert(index, value);
            }

            public override void Remove(DockBarFloatForm value)
            {
                if (this.Locked) return;
                //
                this.innerList.Remove(value);
            }

            public override void RemoveAt(int index)
            {
                if (this.Locked) return;
                //
                this.innerList.RemoveAt(index);
            }

            public override IEnumerator GetEnumerator()
            {
                return this.innerList.GetEnumerator();
            }

            [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public override DockBarFloatForm this[int index]
            {
                get
                {
                    if ((index < 0) || (index >= this.Count))
                    {
                        return null;
                    }
                    return this.innerList[index];
                }
                set
                {
                    if (this.Locked) return;
                    //
                    this.innerList[index] = value;
                }
            }

            public override int Count
            {
                get
                {
                    return this.innerList.Count;
                }
            }

            public override bool ExchangeItemT(DockBarFloatForm item1, DockBarFloatForm item2)
            {
                if (this.Locked) return false;
                //
                if (item1 == null || item2 == null || item1 == item2) return false;
                //
                int index1 = this.innerList.IndexOf(item1);
                int index2 = this.innerList.IndexOf(item2);
                //
                if (index1 == index2) return false;
                if ((index1 < 0) || (index1 >= this.innerList.Count)) return false;
                if ((index2 < 0) || (index2 >= this.innerList.Count)) return false;
                //
                if (index1 < index2)
                {
                    this.innerList.Remove(item2);
                    this.innerList.Remove(item1);
                    this.innerList.Insert(index1, item2);
                    this.innerList.Insert(index2, item1);
                }
                else
                {
                    this.innerList.Remove(item1);
                    this.innerList.Remove(item2);
                    this.innerList.Insert(index2, item1);
                    this.innerList.Insert(index1, item2);
                }
                //
                return true;
            }

            public DockBarFloatForm this[string name]
            {
                get
                {
                    foreach (DockBarFloatForm one in this.innerList)
                    {
                        if (one.Name == name) return one;
                    }
                    //
                    return null;
                }
            }

            //
            //
            //

            public void SetRecordID()
            {
                for (int i = 0; i < this.innerList.Count; i++)
                {
                    WFNew.ISetRecordItemHelper pSetRecordItemHelper = this.innerList[i] as WFNew.ISetRecordItemHelper;
                    if (pSetRecordItemHelper != null) pSetRecordItemHelper.SetRecordID(i);
                }
            }

            public DockBarFloatForm GetItem(int recordID)
            {
                for (int i = 0; i < this.Count; i++)
                {
                    if (this[i].RecordID == recordID) return this[i];
                }
                return null;
            }
        }
        #endregion
    }
}
