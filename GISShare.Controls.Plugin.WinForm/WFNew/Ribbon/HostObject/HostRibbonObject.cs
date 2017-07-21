using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace GISShare.Controls.Plugin.WinForm.WFNew.Ribbon
{
    public class HostRibbonObject : GISShare.Controls.Plugin.IBaseHost, GISShare.Controls.Plugin.IBaseHost2, GISShare.Controls.Plugin.IBaseHost3
    {
        private string m_HostFrameworkFileName = null;
        private string m_PluginDLLFolder = null;
        private string[] m_FilterFilePathArray = null;
        private bool m_FilterFilePathArrayTypeRemove = true;
        private string[] m_FilterObjectNameArray = null;
        private bool m_FilterObjectNameArrayTypeRemove = true;
        private object m_Hook = null;
        private GISShare.Controls.WinForm.WFNew.RibbonControl m_RibbonControl;
        private GISShare.Controls.WinForm.WFNew.RibbonStatusBar m_RibbonStatusBar;
        private GISShare.Controls.WinForm.WFNew.ContextPopupManager m_ContextPopupManager;
        private GISShare.Controls.WinForm.WFNew.DockPanel.DockPanelManager m_DockPanelManager;
        //
        private HostRibbonFrameworkSerializableObject m_HostRibbonFrameworkSerializableObject = null;
        private PluginCategoryDictionary m_PluginCategoryDictionary = new PluginCategoryDictionary();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pluginDLLFolder"></param>
        /// <param name="removeObjectNameArray"></param>
        /// <param name="hook"></param>
        /// <param name="ribbonControl"></param>
        /// <param name="ribbonStatusBar"></param>
        /// <param name="contextPopupManager"></param>
        /// <param name="dockPanelManager"></param>
        public HostRibbonObject(string strHostFrameworkFileName,
            string pluginDLLFolder,
            string[] strFilterFilePathArray,
            bool bFilterFilePathArrayTypeRemove,
            string[] strFilterObjectNameArray,
            bool bFilterObjectNameArrayTypeRemove,
            object hook,
            GISShare.Controls.WinForm.WFNew.RibbonControl ribbonControl,
            GISShare.Controls.WinForm.WFNew.RibbonStatusBar ribbonStatusBar,
            GISShare.Controls.WinForm.WFNew.ContextPopupManager contextPopupManager,
            GISShare.Controls.WinForm.WFNew.DockPanel.DockPanelManager dockPanelManager)
        {
            this.m_HostFrameworkFileName = strHostFrameworkFileName;
            this.m_PluginDLLFolder = pluginDLLFolder;
            this.m_FilterFilePathArray = strFilterFilePathArray;
            this.m_FilterFilePathArrayTypeRemove = bFilterFilePathArrayTypeRemove;
            this.m_FilterObjectNameArray = strFilterObjectNameArray;
            this.m_FilterObjectNameArrayTypeRemove = bFilterObjectNameArrayTypeRemove;
            //
            this.m_Hook = hook;
            //
            this.m_RibbonControl = ribbonControl;
            this.m_RibbonStatusBar = ribbonStatusBar;
            this.m_ContextPopupManager = contextPopupManager;
            this.m_DockPanelManager = dockPanelManager;
            //
            #region 装载宿主可序列化的框架对象
            if (!System.String.IsNullOrEmpty(this.m_HostFrameworkFileName))
            {
                if (System.IO.File.Exists(this.m_HostFrameworkFileName))
                {
                    using (System.Xml.XmlReader xmlReader = new System.Xml.XmlTextReader(this.m_HostFrameworkFileName))
                    {
                        System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(HostRibbonFrameworkSerializableObject));
                        this.m_HostRibbonFrameworkSerializableObject = xmlSerializer.Deserialize(xmlReader) as HostRibbonFrameworkSerializableObject;
                        xmlReader.Close();
                    }
                }
            }
            #endregion
        }

        /// <summary>
        /// RibbonControl
        /// </summary>
        public GISShare.Controls.WinForm.WFNew.RibbonControl RibbonControl
        {
            get { return m_RibbonControl; }
        }

        /// <summary>
        /// RibbonStatusBar
        /// </summary>
        public GISShare.Controls.WinForm.WFNew.RibbonStatusBar RibbonStatusBar
        {
            get { return m_RibbonStatusBar; }
        }

        /// <summary>
        /// ContextPopupManager
        /// </summary>
        public GISShare.Controls.WinForm.WFNew.ContextPopupManager ContextPopupManager
        {
            get { return m_ContextPopupManager; }
        }

        /// <summary>
        /// DockPanelManager
        /// </summary>
        public GISShare.Controls.WinForm.WFNew.DockPanel.DockPanelManager DockPanelManager
        {
            get { return m_DockPanelManager; }
            set { m_DockPanelManager = value; }
        }

        /// <summary>
        /// 装载插件(一旦装载成功再次调用则无效)
        /// </summary>
        public bool RunPluginEngine()
        {
            if (this.PluginCategoryDictionary.Count <= 0)
            {
                //读取插件
                this.GetPluginFromDLLFolder(this.PluginDLLFolder, this.FilterFilePathArray, this.m_FilterFilePathArrayTypeRemove, this.FilterObjectNameArray, this.m_FilterObjectNameArrayTypeRemove);
                //加载插件
                this.LoadPlugin(this.PluginCategoryDictionary, this.RibbonControl, this.RibbonStatusBar, this.ContextPopupManager, this.DockPanelManager);
                //
                return true;
            }
            //
            return false;
        }

        /// <summary>
        /// 加载插件
        /// </summary>
        private void GetPluginFromDLLFolder(string pluginDLLFolder, string[] strFilterFilePathArray, bool bFilterFilePathArrayTypeRemove, string[] strFilterObjectNameArray, bool bFilterObjectNameArrayTypeRemove)
        {
            PluginHandle pluginHandle = new PluginHandle();
            pluginHandle.PluginReflection += new PluginReflectionEventHandler(PluginHandle_PluginReflection);
            if (pluginDLLFolder == null || pluginDLLFolder.Length <= 0) return;
            this.m_PluginCategoryDictionary.Add(pluginHandle.GetPluginsFromDLLFolders(new string[] { pluginDLLFolder }, strFilterFilePathArray, bFilterFilePathArrayTypeRemove, strFilterObjectNameArray, bFilterObjectNameArrayTypeRemove));
        }
        void PluginHandle_PluginReflection(object sender, PluginReflectionEventArgs e)
        {
            this.OnPluginReflection(e);
        }

        /// <summary>
        /// 从跟结点向下检索
        /// </summary>
        private void LoadPlugin(PluginCategoryDictionary pluginCategoryDictionary,
            GISShare.Controls.WinForm.WFNew.RibbonControl ribbonControl,
            GISShare.Controls.WinForm.WFNew.RibbonStatusBar ribbonStatusBar,
            GISShare.Controls.WinForm.WFNew.ContextPopupManager contextPopupManager,
            GISShare.Controls.WinForm.WFNew.DockPanel.DockPanelManager dockPanelManager)
        {
            if (ribbonControl != null)
            {
                //RibbonControlEx.ToolbarItems
                if (this.m_HostRibbonFrameworkSerializableObject != null && this.m_HostRibbonFrameworkSerializableObject.ToolbarItems.ItemCount > 0)
                {
                    this.LoadPlugin_SubItem_DG(pluginCategoryDictionary, (ISubItem)this.m_HostRibbonFrameworkSerializableObject.ToolbarItems, ribbonControl.ToolbarItems);
                }
                else
                {
                    this.LoadPlugin(pluginCategoryDictionary, pluginCategoryDictionary.GetPluginCategory((int)CategoryIndex_3_Style.eRibbonControlEx_ToolbarItems), ribbonControl.ToolbarItems);
                }
                //RibbonControlEx.PageContents
                if (this.m_HostRibbonFrameworkSerializableObject != null && this.m_HostRibbonFrameworkSerializableObject.PageContents.ItemCount > 0)
                {
                    this.LoadPlugin_SubItem_DG(pluginCategoryDictionary, (ISubItem)this.m_HostRibbonFrameworkSerializableObject.PageContents, ribbonControl.PageContents);
                }
                else
                {
                    this.LoadPlugin(pluginCategoryDictionary, pluginCategoryDictionary.GetPluginCategory((int)CategoryIndex_3_Style.eRibbonControlEx_PageContents), ribbonControl.PageContents);
                }
                //RibbonControlEx.RibbonPages
                if (this.m_HostRibbonFrameworkSerializableObject != null && this.m_HostRibbonFrameworkSerializableObject.RibbonPages.ItemCount > 0)
                {
                    foreach (ItemDefSerializableObject one in this.m_HostRibbonFrameworkSerializableObject.RibbonPages.Items)
                    {
                        GISShare.Controls.WinForm.WFNew.RibbonPageItem ribbonPageItem = new Controls.WinForm.WFNew.RibbonPageItem() { Name = one.ID, Text = one.ID };                        
                        if (one is SubItemSerializableObject)
                        {
                            SubItemSerializableObject subItemSerializableObject = (SubItemSerializableObject)one;
                            foreach (ItemDefSerializableObject one2 in subItemSerializableObject.Items)
                            {
                                if (one2 is SubItemSerializableObject)
                                {
                                    GISShare.Controls.WinForm.WFNew.RibbonBarItem ribbonBarItem = new Controls.WinForm.WFNew.RibbonBarItem() { Name = one2.ID, Text = one2.ID };
                                    ribbonBarItem.IsRestrictItems = true;
                                    this.LoadPlugin_SubItem_DG(pluginCategoryDictionary, (ISubItem)one2, ribbonBarItem.BaseItems);
                                    ribbonPageItem.BaseItems.Add(ribbonBarItem);
                                    ribbonBarItem.Visible = ribbonBarItem.HaveVisibleBaseItem;
                                }
                            }
                            ribbonControl.RibbonPages.Add(ribbonPageItem);
                        }
                        ribbonPageItem.Visible = ribbonPageItem.HaveVisibleBaseItem;
                    }
                }
                else
                {
                    this.LoadPlugin(pluginCategoryDictionary, pluginCategoryDictionary.GetPluginCategory((int)CategoryIndex_3_Style.eRibbonControlEx_RibbonPages), ribbonControl.RibbonPages);
                }
                //
                //RibbonControlEx.ApplicationPopup.MenuItems
                if (this.m_HostRibbonFrameworkSerializableObject != null && this.m_HostRibbonFrameworkSerializableObject.MenuItems.ItemCount > 0)
                {
                    this.LoadPlugin_SubItem_DG(pluginCategoryDictionary, (ISubItem)this.m_HostRibbonFrameworkSerializableObject.MenuItems, ribbonControl.ApplicationPopup.MenuItems);
                }
                else
                {
                    this.LoadPlugin(pluginCategoryDictionary, pluginCategoryDictionary.GetPluginCategory((int)CategoryIndex_3_Style.eRibbonControlEx_ApplicationPopup_MenuItems), ribbonControl.ApplicationPopup.MenuItems);
                }
                //RibbonControlEx.ApplicationPopup.RecordItems
                if (this.m_HostRibbonFrameworkSerializableObject != null && this.m_HostRibbonFrameworkSerializableObject.RecordItems.ItemCount > 0)
                {
                    this.LoadPlugin_SubItem_DG(pluginCategoryDictionary, (ISubItem)this.m_HostRibbonFrameworkSerializableObject.RecordItems, ribbonControl.ApplicationPopup.RecordItems);
                }
                else
                {
                    this.LoadPlugin(pluginCategoryDictionary, pluginCategoryDictionary.GetPluginCategory((int)CategoryIndex_3_Style.eRibbonControlEx_ApplicationPopup_RecordItems), ribbonControl.ApplicationPopup.RecordItems);
                }
                //RibbonControlEx.ApplicationPopup.OperationItems
                if (this.m_HostRibbonFrameworkSerializableObject != null && this.m_HostRibbonFrameworkSerializableObject.OperationItems.ItemCount > 0)
                {
                    this.LoadPlugin_SubItem_DG(pluginCategoryDictionary, (ISubItem)this.m_HostRibbonFrameworkSerializableObject.OperationItems, ribbonControl.ApplicationPopup.OperationItems);
                }
                else
                {
                    this.LoadPlugin(pluginCategoryDictionary, pluginCategoryDictionary.GetPluginCategory((int)CategoryIndex_3_Style.eRibbonControlEx_ApplicationPopup_OperationItems), ribbonControl.ApplicationPopup.OperationItems);
                }
            }
            //
            if (ribbonStatusBar != null)
            {
                if (this.m_HostRibbonFrameworkSerializableObject != null && this.m_HostRibbonFrameworkSerializableObject.StatusbarItems.ItemCount > 0)
                {
                    this.LoadPlugin_SubItem_DG(pluginCategoryDictionary, (ISubItem)this.m_HostRibbonFrameworkSerializableObject.ToolbarItems, ribbonStatusBar.BaseItems);
                }
                else
                {
                    this.LoadPlugin(pluginCategoryDictionary, pluginCategoryDictionary.GetPluginCategory((int)CategoryIndex_3_Style.eRibbonStatusBar_BaseItems), ribbonStatusBar.BaseItems);
                }
            }
            //
            if (contextPopupManager != null)
            {
                if (this.m_HostRibbonFrameworkSerializableObject != null && this.m_HostRibbonFrameworkSerializableObject.ContextPopups.ItemCount > 0)
                {
                    foreach (ItemDefSerializableObject one in this.m_HostRibbonFrameworkSerializableObject.ContextPopups.Items)
                    {
                        GISShare.Controls.WinForm.WFNew.ContextPopup contextPopup = new GISShare.Controls.WinForm.WFNew.ContextPopup() { Name = one.ID, Text = one.ID };
                        this.LoadPlugin_SubItem_DG(pluginCategoryDictionary, (ISubItem)one, contextPopup.BaseItems);
                        contextPopupManager.Add(contextPopup);
                    }
                }
                else
                {
                    this.LoadPlugin(pluginCategoryDictionary, pluginCategoryDictionary.GetPluginCategory((int)CategoryIndex_1_Style.eContextPopup), contextPopupManager);
                }
            }
            //
            if (dockPanelManager != null)
            {
                this.LoadPlugin(pluginCategoryDictionary, pluginCategoryDictionary.GetPluginCategory((int)DockPanel.CategoryIndex_2_Style.eDockPanel), dockPanelManager);
            }
            //
            this.LoadPlugin(pluginCategoryDictionary, pluginCategoryDictionary.GetPluginCategory((int)CategoryIndex_0_Style.eInsertSubItem), ribbonControl, ribbonStatusBar, contextPopupManager);
        }
        //
        private void LoadPlugin(PluginCategoryDictionary pluginCategoryDictionary,
            PluginCategory pluginCategory,
            GISShare.Controls.WinForm.WFNew.RibbonControl ribbonControl,
            GISShare.Controls.WinForm.WFNew.RibbonStatusBar ribbonStatusBar,
            GISShare.Controls.WinForm.WFNew.ContextPopupManager contextPopupManager)
        {
            if (pluginCategory == null) return;
            //
            List<IInsertSubItem> pInsertSubItemList = new List<IInsertSubItem>();
            foreach (IPlugin one in pluginCategory.PluginCollection)
            {
                IInsertSubItem pInsertSubItem = one as IInsertSubItem;
                if (pInsertSubItem == null) continue;
                int i = 0;
                for (; i < pInsertSubItemList.Count; i++)
                {
                    if (pInsertSubItem.LoadingIndex <= pInsertSubItemList[i].LoadingIndex) break;
                }
                pInsertSubItemList.Insert(i, pInsertSubItem);
            }
            //
            foreach (IInsertSubItem one in pInsertSubItemList)
            {
                switch (one.InsertCategoryIndex)
                {
                    case (int)WFNew.Ribbon.CategoryIndex_3_Style.eRibbonControlEx_ToolbarItems:
                        if (ribbonControl != null) this.LoadPlugin_InsertSubItem(pluginCategoryDictionary, one, ribbonControl.ToolbarItems);
                        break;
                    case (int)WFNew.Ribbon.CategoryIndex_3_Style.eRibbonControlEx_PageContents:
                        if (ribbonControl != null) this.LoadPlugin_InsertSubItem(pluginCategoryDictionary, one, ribbonControl.PageContents);
                        break;
                    case (int)WFNew.Ribbon.CategoryIndex_3_Style.eRibbonControlEx_RibbonPages:
                        if (ribbonControl != null) this.LoadPlugin_InsertSubItem(pluginCategoryDictionary, one, ribbonControl.RibbonPages);
                        break;
                    case (int)WFNew.Ribbon.CategoryIndex_3_Style.eRibbonControlEx_ApplicationPopup_MenuItems:
                        if (ribbonControl != null) this.LoadPlugin_InsertSubItem(pluginCategoryDictionary, one, ribbonControl.ApplicationPopup.MenuItems);
                        break;
                    case (int)WFNew.Ribbon.CategoryIndex_3_Style.eRibbonControlEx_ApplicationPopup_RecordItems:
                        if (ribbonControl != null) this.LoadPlugin_InsertSubItem(pluginCategoryDictionary, one, ribbonControl.ApplicationPopup.RecordItems);
                        break;
                    case (int)WFNew.Ribbon.CategoryIndex_3_Style.eRibbonControlEx_ApplicationPopup_OperationItems:
                        if (ribbonControl != null) this.LoadPlugin_InsertSubItem(pluginCategoryDictionary, one, ribbonControl.ApplicationPopup.OperationItems);
                        break;
                    //
                    case (int)WFNew.Ribbon.CategoryIndex_3_Style.eRibbonStatusBar_BaseItems:
                        if (ribbonStatusBar != null) this.LoadPlugin_InsertSubItem(pluginCategoryDictionary, one, ribbonStatusBar.BaseItems);
                        break;
                    //
                    //
                    //
                    case (int)WFNew.CategoryIndex_1_Style.eContextPopup:
                        if (contextPopupManager != null) 
                        {
                            GISShare.Controls.WinForm.WFNew.ICollectionObjectDesignHelper pCollectionObjectDesignHelper = contextPopupManager[one.DependItem] as GISShare.Controls.WinForm.WFNew.ICollectionObjectDesignHelper;
                            if (pCollectionObjectDesignHelper != null) this.LoadPlugin_InsertSubItem(pluginCategoryDictionary, one, pCollectionObjectDesignHelper.List);
                        }
                        break;
                    //
                    //
                    //
                    case (int)WFNew.CategoryIndex_1_Style.eRibbonPageItem:
                    case (int)WFNew.CategoryIndex_1_Style.eRibbonBarItem:
                    case (int)WFNew.CategoryIndex_1_Style.eBaseItemStackExItem:
                    case (int)WFNew.CategoryIndex_1_Style.eBaseItemStackItem:
                    case (int)WFNew.CategoryIndex_1_Style.eButtonGroupItem:
                    case (int)WFNew.CategoryIndex_1_Style.eRibbonGalleryItem:
                    case (int)WFNew.CategoryIndex_1_Style.eRibbonGalleryRowItem:
                    //
                    case (int)WFNew.CategoryIndex_1_Style.eDropDownButtonItem:
                    case (int)WFNew.CategoryIndex_1_Style.eDropDownButtonExItem:
                    case (int)WFNew.CategoryIndex_1_Style.eSplitButtonItem:
                    case (int)WFNew.CategoryIndex_1_Style.eSplitButtonExItem:
                    case (int)WFNew.CategoryIndex_1_Style.eButtonItem:
                    case (int)WFNew.CategoryIndex_1_Style.eButtonExItem:
                    case (int)WFNew.CategoryIndex_1_Style.eMenuButtonItem:
                        GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = null;
                        if (pBaseItem == null && ribbonControl != null)
                        {
                            pBaseItem = ribbonControl.GetBaseItem2(one.DependItem);
                        }
                        if (pBaseItem == null && ribbonStatusBar != null)
                        {
                            pBaseItem = ribbonStatusBar.GetBaseItem2(one.DependItem);
                        }
                        if (pBaseItem == null && contextPopupManager != null)
                        {
                            pBaseItem = contextPopupManager.GetBaseItem2(one.DependItem);
                        }
                        if (pBaseItem != null)
                        {
                            GISShare.Controls.WinForm.WFNew.ICollectionObjectDesignHelper pCollectionObjectDesignHelper = pBaseItem as GISShare.Controls.WinForm.WFNew.ICollectionObjectDesignHelper;
                            if (pCollectionObjectDesignHelper != null) this.LoadPlugin_InsertSubItem(pluginCategoryDictionary, one, pCollectionObjectDesignHelper.List);
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        private void LoadPlugin_InsertSubItem(PluginCategoryDictionary pluginCategoryDictionary, IInsertSubItem pInsertSubItem, IList list)
        {
            int iInsertIndex = pInsertSubItem.InsertIndex;
            if (iInsertIndex < 0) iInsertIndex = 0;
            if (iInsertIndex > list.Count) iInsertIndex = list.Count;
            //
            for (int i = 0; i < pInsertSubItem.ItemCount; i++)
            {
                IItemDef pItemDef = new ItemDef();
                pInsertSubItem.GetItemInfo(i, pItemDef);
                //
                IPlugin pPlugin = pluginCategoryDictionary.GetPlugin(pItemDef.ID);
                if (pPlugin == null) continue;
                //
                GISShare.Controls.WinForm.WFNew.BaseItem baseItem = this.CreateBaseItem(pPlugin);
                if (baseItem == null) continue;
                //
                if (pPlugin is ISubItem &&
                    baseItem is GISShare.Controls.WinForm.WFNew.ICollectionItem)
                {
                    this.LoadPlugin_SubItem_DG(pluginCategoryDictionary, (ISubItem)pPlugin, ((GISShare.Controls.WinForm.WFNew.ICollectionItem)baseItem).BaseItems);
                }
                //
                IPlugin2 pPlugin2 = pPlugin as IPlugin2;
                if (pPlugin2 == null) continue;
                pPlugin2.OnCreate(this.Hook);
                //
                this.OnPluginReflection(
                    new PluginReflectionEventArgs(
                        PluginReflectionStyle.eEfficientlyEntityObject, pPlugin, "依据插件对象创建/组织有效地插件实体对象"));
                //
                list.Insert(iInsertIndex + i, baseItem);
            }
        }
        //
        private void LoadPlugin(PluginCategoryDictionary pluginCategoryDictionary, PluginCategory pluginCategory, GISShare.Controls.WinForm.WFNew.ContextPopupManager contextPopupManager)
        {
            if (pluginCategory == null) return;
            //
            foreach (IPlugin one in pluginCategory.PluginCollection)
            {
                switch ((CategoryIndex_1_Style)one.CategoryIndex)
                {
                    case CategoryIndex_1_Style.eContextPopup:
                        GISShare.Controls.WinForm.WFNew.ContextPopup contextPopup = GISShare.Controls.Plugin.WinForm.WFNew.ContextPopupP.CreateContextPopup(one as GISShare.Controls.Plugin.WinForm.WFNew.IContextPopupP);
                        contextPopup.PopupOpened += new EventHandler(contextPopup_PopupOpened);
                        contextPopup.PopupClosed += new EventHandler(contextPopup_PopupClosed);
                        if (one is ISubItem)
                        {
                            this.LoadPlugin_SubItem_DG(pluginCategoryDictionary, (ISubItem)one, contextPopup.BaseItems);
                        }
                        //
                        IPlugin2 pPlugin2 = one as IPlugin2;
                        if (pPlugin2 == null) continue;
                        pPlugin2.OnCreate(this.Hook);
                        //
                        this.OnPluginReflection(
                            new PluginReflectionEventArgs(
                                PluginReflectionStyle.eEfficientlyEntityObject, one, "依据插件对象创建/组织有效地插件实体对象"));
                        //
                        contextPopupManager.Add(contextPopup);
                        break;
                    default:
                        break;
                }
            }
        }
        void contextPopup_PopupOpened(object sender, EventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = sender as GISShare.Controls.WinForm.WFNew.IBaseItem;
            if (pBaseItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)CategoryIndex_1_Style.eContextPopup, pBaseItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)Event_1_Style.ePopupOpened, e);
        }
        void contextPopup_PopupClosed(object sender, EventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = sender as GISShare.Controls.WinForm.WFNew.IBaseItem;
            if (pBaseItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)CategoryIndex_1_Style.eContextPopup, pBaseItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)Event_1_Style.ePopupClosed, e);
        }
        //
        private void LoadPlugin(PluginCategoryDictionary pluginCategoryDictionary, PluginCategory pluginCategory, GISShare.Controls.WinForm.WFNew.DockPanel.DockPanelManager dockPanelManager)
        {
            if (pluginCategory == null) return;
            //
            foreach (IPlugin one in pluginCategory.PluginCollection)
            {
                switch ((DockPanel.CategoryIndex_2_Style)one.CategoryIndex)
                {
                    case DockPanel.CategoryIndex_2_Style.eDockPanel:
                        GISShare.Controls.Plugin.WinForm.WFNew.DockPanel.IDockPanelP pDockPanelP = one as GISShare.Controls.Plugin.WinForm.WFNew.DockPanel.IDockPanelP;
                        if (pDockPanelP == null) break;
                        GISShare.Controls.WinForm.WFNew.DockPanel.DockPanel dockPanel = GISShare.Controls.Plugin.WinForm.WFNew.DockPanel.DockPanelP.CreateDockPanel(pDockPanelP);
                        ISubItem pSubItem = one as ISubItem;
                        if (pSubItem != null)
                        {
                            for (int i = 0; i < pSubItem.ItemCount; i++)
                            {
                                IItemDef pItemDef = new ItemDef();
                                pSubItem.GetItemInfo(i, pItemDef);
                                //
                                IPlugin pPlugin = pluginCategoryDictionary.GetPlugin((int)DockPanel.CategoryIndex_2_Style.eBasePanel, pItemDef.ID);
                                if (pPlugin == null) continue;
                                //
                                GISShare.Controls.WinForm.WFNew.DockPanel.BasePanel basePanel = GISShare.Controls.Plugin.WinForm.WFNew.DockPanel.BasePanelP.CreateBasePanel(pPlugin as GISShare.Controls.Plugin.WinForm.WFNew.DockPanel.IBasePanelP);
                                basePanel.PanelNodeStateChanged += new GISShare.Controls.WinForm.WFNew.DockPanel.PanelNodeStateChangedEventHandler(basePanel_PanelNodeStateChanged);
                                dockPanel.BasePanels.Add(basePanel);
                                //
                                IPlugin2 pPlugin2 = pPlugin as IPlugin2;
                                if (pPlugin2 == null) continue;
                                pPlugin2.OnCreate(this.Hook);
                                //
                                this.OnPluginReflection(
                                    new PluginReflectionEventArgs(
                                        PluginReflectionStyle.eEfficientlyEntityObject, pPlugin, "依据插件对象创建/组织有效地插件实体对象"));
                                //
                                dockPanelManager.BasePanels.Add(basePanel);
                            }
                        }
                        //
                        IPlugin2 pPlugin2_2 = one as IPlugin2;
                        if (pPlugin2_2 == null) continue;
                        pPlugin2_2.OnCreate(this.Hook);
                        //
                        this.OnPluginReflection(
                            new PluginReflectionEventArgs(
                                PluginReflectionStyle.eEfficientlyEntityObject, one, "依据插件对象创建/组织有效地插件实体对象"));
                        //
                        dockPanelManager.DockPanels.Add(dockPanel);
                        switch (pDockPanelP.DockStyle)
                        {
                            case System.Windows.Forms.DockStyle.Top:
                            case System.Windows.Forms.DockStyle.Bottom:
                            case System.Windows.Forms.DockStyle.Left:
                            case System.Windows.Forms.DockStyle.Right:
                            case System.Windows.Forms.DockStyle.Fill:
                                dockPanel.ToDockArea(pDockPanelP.Interal, pDockPanelP.DockStyle);
                                break;
                            case System.Windows.Forms.DockStyle.None:
                            default:
                                dockPanel.ToDockPanelFloatForm();
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        void basePanel_PanelNodeStateChanged(object sender, Controls.WinForm.WFNew.DockPanel.PanelNodeStateChangedEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = sender as GISShare.Controls.WinForm.WFNew.IBaseItem;
            if (pBaseItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)DockPanel.CategoryIndex_2_Style.eBasePanel, pBaseItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)DockPanel.Event_2_Style.ePanelNodeStateChanged, e);
        }
        //
        private void LoadPlugin(PluginCategoryDictionary pluginCategoryDictionary, PluginCategory pluginCategory, IList list)
        {
            if (pluginCategory == null) return;
            //
            foreach (IPlugin one in pluginCategory.PluginCollection)
            {
                if (one is ISubItem)
                {
                    this.LoadPlugin_SubItem_DG(pluginCategoryDictionary, (ISubItem)one, list);
                }
                //
                this.OnPluginReflection(
                    new PluginReflectionEventArgs(
                        PluginReflectionStyle.eEfficientlyEntityObject, one, "依据插件对象创建/组织有效地插件实体对象"));
            }
        }
        private void LoadPlugin_SubItem_DG(PluginCategoryDictionary pluginCategoryDictionary, ISubItem pSubItem, IList list)
        {
            if (pSubItem == null) return;
            //
            for (int i = 0; i < pSubItem.ItemCount; i++)
            {
                IItemDef pItemDef = new ItemDef();
                pSubItem.GetItemInfo(i, pItemDef);
                //
                IPlugin pPlugin = pluginCategoryDictionary.GetPlugin(pItemDef.ID);
                if (pPlugin == null) continue;
                //
                GISShare.Controls.WinForm.WFNew.BaseItem baseItem = this.CreateBaseItem(pPlugin);
                if (baseItem == null) continue;
                //
                if (pPlugin is ISubItem &&
                    baseItem is GISShare.Controls.WinForm.WFNew.ICollectionItem)
                {
                    this.LoadPlugin_SubItem_DG(pluginCategoryDictionary, (ISubItem)pPlugin, ((GISShare.Controls.WinForm.WFNew.ICollectionItem)baseItem).BaseItems);
                }
                //
                IPlugin2 pPlugin2 = pPlugin as IPlugin2;
                if (pPlugin2 == null) continue;
                pPlugin2.OnCreate(this.Hook);
                //
                this.OnPluginReflection(
                    new PluginReflectionEventArgs(
                        PluginReflectionStyle.eEfficientlyEntityObject, pPlugin, "依据插件对象创建/组织有效地插件实体对象"));
                //
                if (pItemDef.Group) list.Add(new GISShare.Controls.WinForm.WFNew.SeparatorItem() { AutoLayout = true });
                list.Add(baseItem);
            }
        }
        private GISShare.Controls.WinForm.WFNew.BaseItem CreateBaseItem(IPlugin pPlugin)
        {
            if (pPlugin == null) return null;
            //
            switch ((CategoryIndex_1_Style)pPlugin.CategoryIndex)
            {
                #region 界面集合对象
                case CategoryIndex_1_Style.eRibbonPageItem:
                    return Controls.Plugin.WinForm.WFNew.RibbonPageItemP.CreateRibbonPageItem(pPlugin as IRibbonPageItemP);
                case CategoryIndex_1_Style.eRibbonBarItem:
                    Controls.WinForm.WFNew.RibbonBarItem ribbonBarItem = Controls.Plugin.WinForm.WFNew.RibbonBarItemP.CreateRibbonBarItem(pPlugin as IRibbonBarItemP);
                    ribbonBarItem.GlyphMouseUp += new System.Windows.Forms.MouseEventHandler(ribbonBarItem_GlyphMouseUp);
                    return ribbonBarItem;
                case CategoryIndex_1_Style.eBaseItemStackExItem:
                    return Controls.Plugin.WinForm.WFNew.BaseItemStackExItemP.CreateBaseItemStackExItem(pPlugin as IBaseItemStackExItemP);
                case CategoryIndex_1_Style.eBaseItemStackItem:
                    return Controls.Plugin.WinForm.WFNew.BaseItemStackItemP.CreateBaseItemStackItem(pPlugin as IBaseItemStackItemP);
                case CategoryIndex_1_Style.eButtonGroupItem:
                    return Controls.Plugin.WinForm.WFNew.ButtonGroupItemP.CreateButtonGroupItem(pPlugin as IButtonGroupItemP);
                case CategoryIndex_1_Style.eRibbonGalleryItem:
                    return Controls.Plugin.WinForm.WFNew.RibbonGalleryItemP.CreateRibbonGalleryItem(pPlugin as IRibbonGalleryItemP);
                case CategoryIndex_1_Style.eRibbonGalleryRowItem:
                    return Controls.Plugin.WinForm.WFNew.RibbonGalleryRowItemP.CreateRibbonGalleryRowItem(pPlugin as IRibbonGalleryRowItemP);
                #endregion
                //
                #region 标签对象
                case CategoryIndex_1_Style.eLabelItem:
                    return Controls.Plugin.WinForm.WFNew.LabelItemP.CreateLabelItem(pPlugin as ILabelItemP);
                case CategoryIndex_1_Style.eLabelExItem:
                    return Controls.Plugin.WinForm.WFNew.LabelExItemP.CreateLabelExItem(pPlugin as ILabelExItemP);
                case CategoryIndex_1_Style.eImageLabelItem:
                    return Controls.Plugin.WinForm.WFNew.ImageLabelItemP.CreateImageLabelItem(pPlugin as IImageLabelItemP);
                case CategoryIndex_1_Style.eLinkLabelItem:
                    Controls.WinForm.WFNew.LinkLabelItem linkLabelItem = Controls.Plugin.WinForm.WFNew.LinkLabelItemP.CreateLinkLabelItem(pPlugin as ILinkLabelItemP);
                    linkLabelItem.MouseDown += new System.Windows.Forms.MouseEventHandler(linkLabelItem_MouseDown);
                    return linkLabelItem;
                case CategoryIndex_1_Style.eImageLinkLabelItem:
                    Controls.WinForm.WFNew.ImageLinkLabelItem imageLinkLabelItem = Controls.Plugin.WinForm.WFNew.ImageLinkLabelItemP.CreateImageLinkLabelItem(pPlugin as IImageLinkLabelItemP);
                    imageLinkLabelItem.MouseDown += new System.Windows.Forms.MouseEventHandler(imageLinkLabelItem_MouseDown);
                    return imageLinkLabelItem;
                #endregion
                //
                #region 按钮对象
                case CategoryIndex_1_Style.eBaseButtonItem:
                    Controls.WinForm.WFNew.BaseButtonItem baseButtonItem = Controls.Plugin.WinForm.WFNew.BaseButtonItemP.CreateBaseButtonItem(pPlugin as IBaseButtonItemP);
                    baseButtonItem.MouseUp += new System.Windows.Forms.MouseEventHandler(baseButtonItem_MouseUp);
                    return baseButtonItem;
                case CategoryIndex_1_Style.eBaseButtonExItem:
                    Controls.WinForm.WFNew.BaseButtonExItem baseButtonExItem = Controls.Plugin.WinForm.WFNew.BaseButtonExItemP.CreateBaseButtonExItem(pPlugin as IBaseButtonExItemP);
                    baseButtonExItem.MouseUp += new System.Windows.Forms.MouseEventHandler(baseButtonExItem_MouseUp);
                    return baseButtonExItem;
                case CategoryIndex_1_Style.eCheckButtonItem:
                    Controls.WinForm.WFNew.CheckButtonItem checkButtonItem = Controls.Plugin.WinForm.WFNew.CheckButtonItemP.CreateCheckButtonItem(pPlugin as ICheckButtonItemP);
                    checkButtonItem.CheckedChanged += new EventHandler(checkButtonItem_CheckedChanged);
                    return checkButtonItem;
                case CategoryIndex_1_Style.eCheckButtonExItem:
                    Controls.WinForm.WFNew.CheckButtonExItem checkButtonExItem = Controls.Plugin.WinForm.WFNew.CheckButtonExItemP.CreateCheckButtonExItem(pPlugin as ICheckButtonExItemP);
                    checkButtonExItem.CheckedChanged += new EventHandler(checkButtonExItem_CheckedChanged);
                    return checkButtonExItem;
                case CategoryIndex_1_Style.eDescriptionButtonItem:
                    Controls.WinForm.WFNew.DescriptionButtonItem descriptionButtonItem = Controls.Plugin.WinForm.WFNew.DescriptionButtonItemP.CreateDescriptionButtonItem(pPlugin as IDescriptionButtonItemP);
                    descriptionButtonItem.MouseUp += new System.Windows.Forms.MouseEventHandler(descriptionButtonItem_MouseUp);
                    return descriptionButtonItem;
                case CategoryIndex_1_Style.eDropDownButtonItem:
                    Controls.WinForm.WFNew.DropDownButtonItem dropDownButtonItem = Controls.Plugin.WinForm.WFNew.DropDownButtonItemP.CreateDropDownButtonItem(pPlugin as IDropDownButtonItemP);
                    dropDownButtonItem.MouseUp += new System.Windows.Forms.MouseEventHandler(dropDownButtonItem_MouseUp);
                    return dropDownButtonItem;
                case CategoryIndex_1_Style.eDropDownButtonExItem:
                    Controls.WinForm.WFNew.DropDownButtonExItem dropDownButtonExItem = Controls.Plugin.WinForm.WFNew.DropDownButtonExItemP.CreateDropDownButtonExItem(pPlugin as IDropDownButtonExItemP);
                    dropDownButtonExItem.MouseUp += new System.Windows.Forms.MouseEventHandler(dropDownButtonExItem_MouseUp);
                    return dropDownButtonExItem;
                case CategoryIndex_1_Style.eSplitButtonItem:
                    Controls.WinForm.WFNew.SplitButtonItem splitButtonItem = Controls.Plugin.WinForm.WFNew.SplitButtonItemP.CreateSplitButtonItem(pPlugin as ISplitButtonItemP);
                    splitButtonItem.ButtonMouseUp += new System.Windows.Forms.MouseEventHandler(splitButtonItem_ButtonMouseUp);
                    return splitButtonItem;
                case CategoryIndex_1_Style.eSplitButtonExItem:
                    Controls.WinForm.WFNew.SplitButtonExItem splitButtonExItem = Controls.Plugin.WinForm.WFNew.SplitButtonExItemP.CreateSplitButtonExItem(pPlugin as ISplitButtonExItemP);
                    splitButtonExItem.ButtonMouseUp += new System.Windows.Forms.MouseEventHandler(splitButtonItem_ButtonMouseUp);
                    return splitButtonExItem;
                case CategoryIndex_1_Style.eButtonItem:
                    Controls.WinForm.WFNew.ButtonItem buttonItem = Controls.Plugin.WinForm.WFNew.ButtonItemP.CreateButtonItem(pPlugin as IButtonItemP);
                    buttonItem.ButtonMouseUp += new System.Windows.Forms.MouseEventHandler(buttonItem_ButtonMouseUp);
                    return buttonItem;
                case CategoryIndex_1_Style.eButtonExItem:
                    Controls.WinForm.WFNew.ButtonExItem buttonExItem = Controls.Plugin.WinForm.WFNew.ButtonExItemP.CreateButtonExItem(pPlugin as IButtonExItemP);
                    buttonExItem.ButtonMouseUp += new System.Windows.Forms.MouseEventHandler(buttonExItem_ButtonMouseUp);
                    return buttonExItem;
                case CategoryIndex_1_Style.eMenuButtonItem:
                    Controls.WinForm.WFNew.MenuButtonItem menuButtonItem = Controls.Plugin.WinForm.WFNew.MenuButtonItemP.CreateMenuButtonItem(pPlugin as IMenuButtonItemP);
                    menuButtonItem.ButtonMouseUp += new System.Windows.Forms.MouseEventHandler(menuButtonItem_ButtonMouseUp);
                    return menuButtonItem;
                case CategoryIndex_1_Style.eGlyphButtonItem:
                    Controls.WinForm.WFNew.GlyphButtonItem glyphButtonItem = Controls.Plugin.WinForm.WFNew.GlyphButtonItemP.CreateGlyphButtonItem(pPlugin as IGlyphButtonItemP);
                    glyphButtonItem.MouseUp += new System.Windows.Forms.MouseEventHandler(glyphButtonItem_MouseUp);
                    return glyphButtonItem;
                #endregion
                //
                #region 复选框对象
                case CategoryIndex_1_Style.eCheckBoxItem:
                    Controls.WinForm.WFNew.CheckBoxItem checkBoxItem = Controls.Plugin.WinForm.WFNew.CheckBoxItemP.CreateCheckBoxItem(pPlugin as ICheckBoxItemP);
                    checkBoxItem.CheckedChanged += new EventHandler(checkBoxItem_CheckedChanged);
                    return checkBoxItem;
                case CategoryIndex_1_Style.eImageCheckBoxItem:
                    Controls.WinForm.WFNew.ImageCheckBoxItem imageCheckBoxItem = Controls.Plugin.WinForm.WFNew.ImageCheckBoxItemP.CreateImageCheckBoxItem(pPlugin as IImageCheckBoxItemP);
                    imageCheckBoxItem.CheckedChanged += new EventHandler(imageCheckBoxItem_CheckedChanged);
                    return imageCheckBoxItem;
                #endregion
                //
                #region 寄宿对象
                case CategoryIndex_1_Style.eControlHostItem:
                    return Controls.Plugin.WinForm.WFNew.ControlHostItemP.CreateControlHostItem(pPlugin as IControlHostItemP);
                case CategoryIndex_1_Style.eControlPanelItem:
                    return Controls.Plugin.WinForm.WFNew.ControlPanelItemP.CreateControlPanelItem(pPlugin as IControlPanelItemP);
                #endregion
                //
                #region 输入框对象
                case CategoryIndex_1_Style.eTextBoxItem:
                    Controls.WinForm.WFNew.TextBoxItem textBoxItem = Controls.Plugin.WinForm.WFNew.TextBoxItemP.CreateTextBoxItem(pPlugin as ITextBoxItemP);
                    textBoxItem.TextChanged += new EventHandler(textBoxItem_TextChanged);
                    return textBoxItem;
                case CategoryIndex_1_Style.eIntegerInputBoxItem:
                    Controls.WinForm.WFNew.IntegerInputBoxItem integerInputBoxItem = Controls.Plugin.WinForm.WFNew.IntegerInputBoxItemP.CreateIntegerInputBoxItem(pPlugin as IIntegerInputBoxItemP);
                    integerInputBoxItem.ValueChanged += new GISShare.Controls.WinForm.IntValueChangedHandler(integerInputBoxItem_ValueChanged);
                    return integerInputBoxItem;
                case CategoryIndex_1_Style.eDoubleInputBoxItem:
                    Controls.WinForm.WFNew.DoubleInputBoxItem doubleInputBoxItem = Controls.Plugin.WinForm.WFNew.DoubleInputBoxItemP.CreateDoubleInputBoxItem(pPlugin as IDoubleInputBoxItemP);
                    doubleInputBoxItem.ValueChanged += new GISShare.Controls.WinForm.DoubleValueChangedHandler(doubleInputBoxItem_ValueChanged);
                    return doubleInputBoxItem;
                case CategoryIndex_1_Style.eButtonTextBoxItem:
                    Controls.WinForm.WFNew.ButtonTextBoxItem buttonTextBoxItem = Controls.Plugin.WinForm.WFNew.ButtonTextBoxItemP.CraeteButtonTextBoxItem(pPlugin as IButtonTextBoxItemP);
                    buttonTextBoxItem.ButtonClick += new EventHandler(buttonTextBoxItem_ButtonClick);
                    return buttonTextBoxItem;
                #endregion
                //
                #region 下拉框对象
                case CategoryIndex_1_Style.eComboBoxItem:
                    Controls.WinForm.WFNew.ComboBoxItem comboBoxItem = Controls.Plugin.WinForm.WFNew.ComboBoxItemP.CreateComboBoxItem(pPlugin as IComboBoxItemP);
                    comboBoxItem.SelectedIndexChanged += new GISShare.Controls.WinForm.IntValueChangedHandler(comboBoxItem_SelectedIndexChanged);
                    return comboBoxItem;
                case CategoryIndex_1_Style.eComboDateItem:
                    Controls.WinForm.WFNew.ComboDateItem comboDateItem = Controls.Plugin.WinForm.WFNew.ComboDateItemP.CreateComboDateItem(pPlugin as IComboDateItemP);
                    comboDateItem.SelectedDateChanged += new GISShare.Controls.WinForm.PropertyChangedEventHandler(comboDateItem_SelectedDateChanged);
                    return comboDateItem;
                case CategoryIndex_1_Style.eComboTreeItem:
                    Controls.WinForm.WFNew.ComboTreeItem comboTreeItem = Controls.Plugin.WinForm.WFNew.ComboTreeItemP.CreateComboTreeItem(pPlugin as IComboTreeItemP);
                    comboTreeItem.SelectedNodeChanged += new GISShare.Controls.WinForm.PropertyChangedEventHandler(comboTreeItem_SelectedNodeChanged);
                    return comboTreeItem;
                #endregion
                //
                #region 图片框对象
                case CategoryIndex_1_Style.eImageBoxItem:
                    return Controls.Plugin.WinForm.WFNew.ImageBoxItemP.CreateImageBoxItem(pPlugin as IImageBoxItemP);
                #endregion
                //
                #region 进度条对象
                case CategoryIndex_1_Style.eProcessBarItem:
                    //Controls.WinForm.WFNew.ProcessBarItem processBarItem = Controls.Plugin.WinForm.WFNew.ProcessBarItemP.CreateProcessBarItem(pPlugin as IProcessBarItemP);
                    //processBarItem.ValueChanged += new GISShare.Controls.WinForm.IntValueChangedHandler(processBarItem_ValueChanged);
                    //return processBarItem;
                    return Controls.Plugin.WinForm.WFNew.ProcessBarItemP.CreateProcessBarItem(pPlugin as IProcessBarItemP);
                #endregion
                //
                #region 单选框对象
                case CategoryIndex_1_Style.eRadioButtonItem:
                    Controls.WinForm.WFNew.RadioButtonItem radioButtonItem = Controls.Plugin.WinForm.WFNew.RadioButtonItemP.CreateRadioButtonItem(pPlugin as IRadioButtonItemP);
                    radioButtonItem.CheckedChanged += new EventHandler(radioButtonItem_CheckedChanged);
                    return radioButtonItem;
                case CategoryIndex_1_Style.eImageRadioButtonItem:
                    Controls.WinForm.WFNew.ImageRadioButtonItem imageRadioButtonItem = Controls.Plugin.WinForm.WFNew.ImageRadioButtonItemP.CreateImageRadioButtonItem(pPlugin as IImageRadioButtonItemP);
                    imageRadioButtonItem.CheckedChanged += new EventHandler(imageRadioButtonItem_CheckedChanged);
                    return imageRadioButtonItem;
                #endregion
                //
                #region 星对象
                case CategoryIndex_1_Style.eRatingStarItem:
                    Controls.WinForm.WFNew.RatingStarItem ratingStarItem = Controls.Plugin.WinForm.WFNew.RatingStarItemP.CreateRatingStarItem(pPlugin as IRatingStarItemP);
                    ratingStarItem.ValueChanged += new GISShare.Controls.WinForm.IntValueChangedHandler(ratingStarItem_ValueChanged);
                    return ratingStarItem;
                #endregion
                //
                #region 分隔条对象
                case CategoryIndex_1_Style.eSeparatorItem:
                    return Controls.Plugin.WinForm.WFNew.SeparatorItemP.CreateSeparatorItem (pPlugin as ISeparatorItemP);
                case CategoryIndex_1_Style.eLabelSeparatorItem:
                    return Controls.Plugin.WinForm.WFNew.LabelSeparatorItemP.CreateLabelSeparatorItem(pPlugin as ILabelSeparatorItemP);
                case CategoryIndex_1_Style.eImageLabelSeparatorItem:
                    return Controls.Plugin.WinForm.WFNew.ImageLabelSeparatorItemP.CreateImageLabelSeparatorItem(pPlugin as IImageLabelSeparatorItemP);
                #endregion
                //
                #region 滚动对象
                case CategoryIndex_1_Style.eSliderItem:
                    Controls.WinForm.WFNew.SliderItem sliderItem = Controls.Plugin.WinForm.WFNew.SliderItemP.CreateSliderItem(pPlugin as ISliderItemP);
                    sliderItem.ValueChanged += new GISShare.Controls.WinForm.DoubleValueChangedHandler(sliderItem_ValueChanged);
                    return sliderItem;
                case CategoryIndex_1_Style.eScrollBarItem:
                    Controls.WinForm.WFNew.ScrollBarItem scrollBarItem = Controls.Plugin.WinForm.WFNew.ScrollBarItemP.CreateScrollBarItem(pPlugin as IScrollBarItemP);
                    scrollBarItem.ValueChanged += new GISShare.Controls.WinForm.IntValueChangedHandler(scrollBarItem_ValueChanged);
                    return scrollBarItem;
                #endregion
                //
                default:
                    break;
            }
            //
            return this.CreateBaseItemDef(pPlugin);
        }
        protected virtual GISShare.Controls.WinForm.WFNew.BaseItem CreateBaseItemDef(IPlugin pPlugin)//可提供自定义对象
        {
            return null;
        }
        void ribbonBarItem_GlyphMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = sender as GISShare.Controls.WinForm.WFNew.IBaseItem;
            if (pBaseItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)CategoryIndex_1_Style.eRibbonBarItem, pBaseItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)Event_1_Style.eGlyphMouseUp, e);
        }
        void linkLabelItem_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = sender as GISShare.Controls.WinForm.WFNew.IBaseItem;
            if (pBaseItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)CategoryIndex_1_Style.eLinkLabelItem, pBaseItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)Event_1_Style.eMouseDown, e);
        }
        void imageLinkLabelItem_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = sender as GISShare.Controls.WinForm.WFNew.IBaseItem;
            if (pBaseItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)CategoryIndex_1_Style.eImageLinkLabelItem, pBaseItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)Event_1_Style.eMouseDown, e);
        }
        void baseButtonItem_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = sender as GISShare.Controls.WinForm.WFNew.IBaseItem;
            if (pBaseItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)CategoryIndex_1_Style.eBaseButtonItem, pBaseItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)Event_1_Style.eMouseUp, e);
        }
        void baseButtonExItem_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = sender as GISShare.Controls.WinForm.WFNew.IBaseItem;
            if (pBaseItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)CategoryIndex_1_Style.eBaseButtonExItem, pBaseItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)Event_1_Style.eMouseUp, e);
        }
        void checkButtonItem_CheckedChanged(object sender, EventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = sender as GISShare.Controls.WinForm.WFNew.IBaseItem;
            if (pBaseItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)CategoryIndex_1_Style.eCheckButtonItem, pBaseItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)Event_1_Style.eCheckedChanged, e);
        }
        void checkButtonExItem_CheckedChanged(object sender, EventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = sender as GISShare.Controls.WinForm.WFNew.IBaseItem;
            if (pBaseItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)CategoryIndex_1_Style.eCheckButtonExItem, pBaseItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)Event_1_Style.eCheckedChanged, e);
        }
        void descriptionButtonItem_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = sender as GISShare.Controls.WinForm.WFNew.IBaseItem;
            if (pBaseItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)CategoryIndex_1_Style.eDescriptionButtonItem, pBaseItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)Event_1_Style.eMouseUp, e);
        }
        void dropDownButtonItem_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = sender as GISShare.Controls.WinForm.WFNew.IBaseItem;
            if (pBaseItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)CategoryIndex_1_Style.eDropDownButtonItem, pBaseItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)Event_1_Style.eMouseUp, e);
        }
        void dropDownButtonExItem_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = sender as GISShare.Controls.WinForm.WFNew.IBaseItem;
            if (pBaseItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)CategoryIndex_1_Style.eDropDownButtonExItem, pBaseItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)Event_1_Style.eMouseUp, e);
        }
        void splitButtonItem_ButtonMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = sender as GISShare.Controls.WinForm.WFNew.IBaseItem;
            if (pBaseItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)CategoryIndex_1_Style.eSplitButtonItem, pBaseItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)Event_1_Style.eButtonMouseUp, e);
        }
        void splitButtonExItem_ButtonMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = sender as GISShare.Controls.WinForm.WFNew.IBaseItem;
            if (pBaseItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)CategoryIndex_1_Style.eSplitButtonExItem, pBaseItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)Event_1_Style.eButtonMouseUp, e);
        }
        void buttonItem_ButtonMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = sender as GISShare.Controls.WinForm.WFNew.IBaseItem;
            if (pBaseItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)CategoryIndex_1_Style.eButtonItem, pBaseItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)Event_1_Style.eButtonMouseUp, e);
        }
        void buttonExItem_ButtonMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = sender as GISShare.Controls.WinForm.WFNew.IBaseItem;
            if (pBaseItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)CategoryIndex_1_Style.eButtonExItem, pBaseItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)Event_1_Style.eButtonMouseUp, e);
        }
        void menuButtonItem_ButtonMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = sender as GISShare.Controls.WinForm.WFNew.IBaseItem;
            if (pBaseItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)CategoryIndex_1_Style.eMenuButtonItem, pBaseItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)Event_1_Style.eButtonMouseUp, e);
        }
        void glyphButtonItem_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = sender as GISShare.Controls.WinForm.WFNew.IBaseItem;
            if (pBaseItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)CategoryIndex_1_Style.eGlyphButtonItem, pBaseItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)Event_1_Style.eMouseUp, e);
        }
        void checkBoxItem_CheckedChanged(object sender, EventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = sender as GISShare.Controls.WinForm.WFNew.IBaseItem;
            if (pBaseItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)CategoryIndex_1_Style.eCheckBoxItem, pBaseItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)Event_1_Style.eCheckedChanged, e);
        }
        void textBoxItem_TextChanged(object sender, EventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = sender as GISShare.Controls.WinForm.WFNew.IBaseItem;
            if (pBaseItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)CategoryIndex_1_Style.eTextBoxItem, pBaseItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)Event_1_Style.eTextChanged, e);
        }
        void integerInputBoxItem_ValueChanged(object sender, GISShare.Controls.WinForm.IntValueChangedEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = sender as GISShare.Controls.WinForm.WFNew.IBaseItem;
            if (pBaseItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)CategoryIndex_1_Style.eIntegerInputBoxItem, pBaseItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)Event_1_Style.eValueChanged, e);
        }
        void doubleInputBoxItem_ValueChanged(object sender, GISShare.Controls.WinForm.DoubleValueChangedEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = sender as GISShare.Controls.WinForm.WFNew.IBaseItem;
            if (pBaseItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)CategoryIndex_1_Style.eDoubleInputBoxItem, pBaseItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)Event_1_Style.eValueChanged, e);
        }
        void buttonTextBoxItem_ButtonClick(object sender, EventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = sender as GISShare.Controls.WinForm.WFNew.IBaseItem;
            if (pBaseItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)CategoryIndex_1_Style.eButtonTextBoxItem, pBaseItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)Event_1_Style.eButtonClick, e);
        }
        void comboBoxItem_SelectedIndexChanged(object sender, GISShare.Controls.WinForm.IntValueChangedEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = sender as GISShare.Controls.WinForm.WFNew.IBaseItem;
            if (pBaseItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)CategoryIndex_1_Style.eComboBoxItem, pBaseItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)Event_1_Style.eSelectedIndexChanged, e);
        }
        void comboDateItem_SelectedDateChanged(object sender, GISShare.Controls.WinForm.PropertyChangedEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = sender as GISShare.Controls.WinForm.WFNew.IBaseItem;
            if (pBaseItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)CategoryIndex_1_Style.eComboDateItem, pBaseItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)Event_1_Style.eSelectedDateChanged, e);
        }
        void comboTreeItem_SelectedNodeChanged(object sender, GISShare.Controls.WinForm.PropertyChangedEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = sender as GISShare.Controls.WinForm.WFNew.IBaseItem;
            if (pBaseItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)CategoryIndex_1_Style.eComboTreeItem, pBaseItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)Event_1_Style.eSelectedNodeChanged, e);
        }
        void imageCheckBoxItem_CheckedChanged(object sender, EventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = sender as GISShare.Controls.WinForm.WFNew.IBaseItem;
            if (pBaseItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)CategoryIndex_1_Style.eImageCheckBoxItem, pBaseItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)Event_1_Style.eCheckedChanged, e);
        }
        //void processBarItem_ValueChanged(object sender, GISShare.Controls.WinForm.IntValueChangedEventArgs e)
        //{
        //    GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = sender as GISShare.Controls.WinForm.WFNew.IBaseItem;
        //    if (pBaseItem == null) return;
        //    //
        //    IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)CategoryIndex_1_Style.eProcessBarItem, pBaseItem.Name) as IEventChain;
        //    if (pEventChain != null) pEventChain.OnTriggerEvent((int)Event_1_Style.eValueChanged, e);
        //}
        void radioButtonItem_CheckedChanged(object sender, EventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = sender as GISShare.Controls.WinForm.WFNew.IBaseItem;
            if (pBaseItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)CategoryIndex_1_Style.eRadioButtonItem, pBaseItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)Event_1_Style.eCheckedChanged, e);
        }
        void imageRadioButtonItem_CheckedChanged(object sender, EventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = sender as GISShare.Controls.WinForm.WFNew.IBaseItem;
            if (pBaseItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)CategoryIndex_1_Style.eImageRadioButtonItem, pBaseItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)Event_1_Style.eCheckedChanged, e);
        }
        void ratingStarItem_ValueChanged(object sender, GISShare.Controls.WinForm.IntValueChangedEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = sender as GISShare.Controls.WinForm.WFNew.IBaseItem;
            if (pBaseItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)CategoryIndex_1_Style.eRatingStarItem, pBaseItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)Event_1_Style.eValueChanged, e);
        }
        void sliderItem_ValueChanged(object sender, GISShare.Controls.WinForm.DoubleValueChangedEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = sender as GISShare.Controls.WinForm.WFNew.IBaseItem;
            if (pBaseItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)CategoryIndex_1_Style.eSliderItem, pBaseItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)Event_1_Style.eValueChanged, e);
        }
        void scrollBarItem_ValueChanged(object sender, GISShare.Controls.WinForm.IntValueChangedEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = sender as GISShare.Controls.WinForm.WFNew.IBaseItem;
            if (pBaseItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)CategoryIndex_1_Style.eScrollBarItem, pBaseItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)Event_1_Style.eValueChanged, e);
        }

        #region IBaseHost
        /// <summary>
        /// 钩子信息
        /// </summary>
        public object Hook
        {
            get { return this.m_Hook; }
        }

        /// <summary>
        /// 插件目录字典
        /// </summary>
        public PluginCategoryDictionary PluginCategoryDictionary
        {
            get { return m_PluginCategoryDictionary; }
        }

        /// <summary>
        /// 提供其它操作
        /// </summary>
        /// <param name="obj">参数</param>
        /// <returns>返回</returns>
        public virtual object OtherOperation(object obj)
        {
            return null;
        }
        #endregion

        #region IBaseHost2
        /// <summary>
        /// 追加插件对象
        /// </summary>
        public void AppendPlugin()
        {
            AppendPluginTBForm appendPluginTBForm = new AppendPluginTBForm(this);
            appendPluginTBForm.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            appendPluginTBForm.ShowDialog();
        }

        public PluginCategoryDictionary AppendPluginObject(string pluginDLLFile)
        {
            if (pluginDLLFile == null || pluginDLLFile.Length <= 0) return null;
            //
            PluginHandle pluginHandle = new PluginHandle();
            //pluginHandle.PluginReflection -= new PluginReflectionEventHandler(PluginHandle_PluginReflection);
            PluginCategoryDictionary pluginCategoryDictionary = pluginHandle.GetPluginsFromDLLFiles(new string[] { pluginDLLFile }, this.FilterObjectNameArray, this.m_FilterObjectNameArrayTypeRemove).GetDifferent(this.PluginCategoryDictionary);
            //pluginHandle.PluginReflection += new PluginReflectionEventHandler(PluginHandle_PluginReflection);
            //
            this.LoadPlugin(pluginCategoryDictionary, this.m_RibbonControl, this.m_RibbonStatusBar, this.m_ContextPopupManager, this.DockPanelManager);
            //
            this.PluginCategoryDictionary.Add(pluginCategoryDictionary);
            //
            return pluginCategoryDictionary;
        }
        #endregion

        #region IBaseHost3
        /// <summary>
        /// 记录插件加载和反射的事件
        /// </summary>
        public event PluginReflectionEventHandler PluginReflection;

        /// <summary>
        /// 插件文件夹
        /// </summary>
        public string PluginDLLFolder
        {
            get { return m_PluginDLLFolder; }
        }

        /// <summary>
        /// 排除项文件
        /// </summary>
        public string[] FilterFilePathArray
        {
            get { return m_FilterFilePathArray; }
        }

        public bool FilterFilePathArrayTypeRemove
        {
            get { return m_FilterFilePathArrayTypeRemove; }
        }

        /// <summary>
        /// 排除对象名称列表
        /// </summary>
        public string[] FilterObjectNameArray
        {
            get { return m_FilterObjectNameArray; }
        }

        public bool FilterObjectNameArrayTypeRemove
        {
            get { return m_FilterObjectNameArrayTypeRemove; }
        }
        #endregion

        public GISShare.Controls.WinForm.WFNew.View.NodeViewItem UIView()
        {
            GISShare.Controls.WinForm.WFNew.View.NodeViewItem nodeViewItem =
                new Controls.WinForm.WFNew.View.NodeViewItem("宿主对象", "宿主对象");
            nodeViewItem.IsExpanded = true;
            //
            #region RibbonControl
            if (this.m_RibbonControl != null)
            {
                GISShare.Controls.WinForm.WFNew.View.NodeViewItem nodeViewItem1 =
                    new GISShare.Controls.WinForm.WFNew.View.NodeViewItem("功能区控件", "功能区控件（RibbonControl）");
                nodeViewItem1.IsExpanded = true;
                //
                //
                //
                GISShare.Controls.WinForm.WFNew.View.NodeViewItem nodeViewItem2 =
                    new GISShare.Controls.WinForm.WFNew.View.NodeViewItem("快捷工具条", "快捷工具条");
                foreach (GISShare.Controls.WinForm.WFNew.IBaseItem one in this.m_RibbonControl.ToolbarItems)
                {
                    GISShare.Controls.WinForm.WFNew.View.NodeViewItem node =
                        new GISShare.Controls.WinForm.WFNew.View.NodeViewItem(one.Name, one.Text + "（Name：" + one.Name + "）");
                    nodeViewItem2.NodeViewItems.Add(node);
                    this.UIView_DG(node, one as GISShare.Controls.WinForm.WFNew.ICollectionItem);
                }
                nodeViewItem1.NodeViewItems.Add(nodeViewItem2);
                //
                //
                //
                GISShare.Controls.WinForm.WFNew.View.NodeViewItem nodeViewItem3 =
                    new GISShare.Controls.WinForm.WFNew.View.NodeViewItem("功能区面板右侧按钮列表", "功能区面板右侧按钮列表");
                foreach (GISShare.Controls.WinForm.WFNew.IBaseItem one in this.m_RibbonControl.PageContents)
                {
                    GISShare.Controls.WinForm.WFNew.View.NodeViewItem node = new GISShare.Controls.WinForm.WFNew.View.NodeViewItem(one.Name, one.Text + "（Name：" + one.Name + "）");
                    nodeViewItem3.NodeViewItems.Add(node);
                    this.UIView_DG(node, one as GISShare.Controls.WinForm.WFNew.ICollectionItem);
                }
                nodeViewItem1.NodeViewItems.Add(nodeViewItem3);
                //
                //
                //
                GISShare.Controls.WinForm.WFNew.View.NodeViewItem nodeViewItem4 =
                    new GISShare.Controls.WinForm.WFNew.View.NodeViewItem("应用程序快捷菜单", "应用程序快捷菜单");
                nodeViewItem4.IsExpanded = true;
                //
                GISShare.Controls.WinForm.WFNew.View.NodeViewItem nodeViewItem4_1 =
                    new GISShare.Controls.WinForm.WFNew.View.NodeViewItem("菜单栏", "菜单栏");
                foreach (GISShare.Controls.WinForm.WFNew.IBaseItem one in this.m_RibbonControl.ApplicationPopup.MenuItems)
                {
                    GISShare.Controls.WinForm.WFNew.View.NodeViewItem node = 
                        new GISShare.Controls.WinForm.WFNew.View.NodeViewItem(one.Name, one.Text + "（Name：" + one.Name + "）");
                    nodeViewItem4_1.NodeViewItems.Add(node);
                    this.UIView_DG(node, one as GISShare.Controls.WinForm.WFNew.ICollectionItem);
                }
                nodeViewItem4.NodeViewItems.Add(nodeViewItem4_1);
                //
                GISShare.Controls.WinForm.WFNew.View.NodeViewItem nodeViewItem4_2 =
                    new GISShare.Controls.WinForm.WFNew.View.NodeViewItem("记录栏", "记录栏");
                foreach (GISShare.Controls.WinForm.WFNew.IBaseItem one in this.m_RibbonControl.ApplicationPopup.RecordItems)
                {
                    GISShare.Controls.WinForm.WFNew.View.NodeViewItem node = new GISShare.Controls.WinForm.WFNew.View.NodeViewItem(one.Name, one.Text + "（Name：" + one.Name + "）");
                    nodeViewItem4_2.NodeViewItems.Add(node);
                    this.UIView_DG(node, one as GISShare.Controls.WinForm.WFNew.ICollectionItem);
                }
                nodeViewItem4.NodeViewItems.Add(nodeViewItem4_2);
                //
                GISShare.Controls.WinForm.WFNew.View.NodeViewItem nodeViewItem4_3 =
                    new GISShare.Controls.WinForm.WFNew.View.NodeViewItem("操作栏", "操作栏");
                foreach (GISShare.Controls.WinForm.WFNew.IBaseItem one in this.m_RibbonControl.ApplicationPopup.OperationItems)
                {
                    GISShare.Controls.WinForm.WFNew.View.NodeViewItem node =
                        new GISShare.Controls.WinForm.WFNew.View.NodeViewItem(one.Name, one.Text + "（Name：" + one.Name + "）");
                    nodeViewItem4_3.NodeViewItems.Add(node);
                    this.UIView_DG(node, one as GISShare.Controls.WinForm.WFNew.ICollectionItem);
                }
                nodeViewItem4.NodeViewItems.Add(nodeViewItem4_3);
                //
                nodeViewItem1.NodeViewItems.Add(nodeViewItem4);
                //
                //
                //
                GISShare.Controls.WinForm.WFNew.View.NodeViewItem nodeViewItem5 = 
                    new GISShare.Controls.WinForm.WFNew.View.NodeViewItem("功能区面板集合", "功能区面板集合");
                foreach (GISShare.Controls.WinForm.WFNew.IBaseItem one in this.m_RibbonControl.RibbonPages)
                {
                    GISShare.Controls.WinForm.WFNew.View.NodeViewItem node = 
                        new GISShare.Controls.WinForm.WFNew.View.NodeViewItem(one.Name, one.Text + "（Name：" + one.Name + "）");
                    nodeViewItem5.NodeViewItems.Add(node);
                    this.UIView_DG(node, one as GISShare.Controls.WinForm.WFNew.ICollectionItem);
                }
                nodeViewItem1.NodeViewItems.Add(nodeViewItem5);
                //
                //
                //
                nodeViewItem.NodeViewItems.Add(nodeViewItem1);
            }
            #endregion
            //
            #region RibbonStatusBar
            if (this.m_RibbonStatusBar != null)
            {
                GISShare.Controls.WinForm.WFNew.View.NodeViewItem nodeViewItem1 =
                   new GISShare.Controls.WinForm.WFNew.View.NodeViewItem("状态栏", "状态栏");
                //
                foreach (GISShare.Controls.WinForm.WFNew.IBaseItem one in this.m_RibbonStatusBar.BaseItems)
                {
                    GISShare.Controls.WinForm.WFNew.View.NodeViewItem node =
                        new GISShare.Controls.WinForm.WFNew.View.NodeViewItem(one.Name, one.Text + "（Name：" + one.Name + "）");
                    nodeViewItem1.NodeViewItems.Add(node);
                    this.UIView_DG(node, one as GISShare.Controls.WinForm.WFNew.ICollectionItem);
                }
                //
                nodeViewItem.NodeViewItems.Add(nodeViewItem1);
            }
            #endregion
            //
            #region ContextPopupManager
            if (this.m_ContextPopupManager != null)
            {
                GISShare.Controls.WinForm.WFNew.View.NodeViewItem nodeViewItem1 =
                   new GISShare.Controls.WinForm.WFNew.View.NodeViewItem("快捷菜单管理", "快捷菜单管理");
                //
                foreach (GISShare.Controls.WinForm.WFNew.BasePopup one in this.m_ContextPopupManager)
                {
                    GISShare.Controls.WinForm.WFNew.View.NodeViewItem node =
                        new GISShare.Controls.WinForm.WFNew.View.NodeViewItem(one.Name, one.Text + "（Name：" + one.Name + "）");
                    nodeViewItem1.NodeViewItems.Add(node);
                    this.UIView_DG(node, one as GISShare.Controls.WinForm.WFNew.ICollectionItem);
                }
                //
                nodeViewItem.NodeViewItems.Add(nodeViewItem1);
            }
            #endregion
            //
            #region DockPanelManager
            if (this.m_DockPanelManager != null)
            {
                GISShare.Controls.WinForm.WFNew.View.NodeViewItem nodeViewItem1 =
                   new GISShare.Controls.WinForm.WFNew.View.NodeViewItem("浮动面板管理", "浮动面板管理");
                nodeViewItem1.IsExpanded = true;
                //
                //
                //
                GISShare.Controls.WinForm.WFNew.View.NodeViewItem nodeViewItem2 =
                   new GISShare.Controls.WinForm.WFNew.View.NodeViewItem("基础面板", "基础面板");
                foreach (GISShare.Controls.WinForm.WFNew.IBaseItem one in this.m_DockPanelManager.BasePanels)
                {
                    GISShare.Controls.WinForm.WFNew.View.NodeViewItem node =
                        new GISShare.Controls.WinForm.WFNew.View.NodeViewItem(one.Name, one.Text + "（Name：" + one.Name + "）");
                    nodeViewItem2.NodeViewItems.Add(node);
                    //this.UIView_DG(node, one as GISShare.Controls.WinForm.WFNew.ICollectionItem);
                }
                nodeViewItem1.NodeViewItems.Add(nodeViewItem2);
                //
                GISShare.Controls.WinForm.WFNew.View.NodeViewItem nodeViewItem3 =
                   new GISShare.Controls.WinForm.WFNew.View.NodeViewItem("停靠面板（由系统管理）", "停靠面板（由系统管理）");
                foreach (GISShare.Controls.WinForm.WFNew.IBaseItem one in this.m_DockPanelManager.DockPanels)
                {
                    GISShare.Controls.WinForm.WFNew.View.NodeViewItem node =
                        new GISShare.Controls.WinForm.WFNew.View.NodeViewItem(one.Name, one.Text + "（Name：" + one.Name + "）");
                    nodeViewItem3.NodeViewItems.Add(node);
                    //this.UIView_DG(node, one as GISShare.Controls.WinForm.WFNew.ICollectionItem);
                }
                nodeViewItem1.NodeViewItems.Add(nodeViewItem3);
                //
                GISShare.Controls.WinForm.WFNew.View.NodeViewItem nodeViewItem4 =
                   new GISShare.Controls.WinForm.WFNew.View.NodeViewItem("浮动窗体（由系统管理）", "浮动窗体（由系统管理）");
                foreach (GISShare.Controls.WinForm.WFNew.IBaseItem one in this.m_DockPanelManager.DockPanelFloatForms_Read)
                {
                    GISShare.Controls.WinForm.WFNew.View.NodeViewItem node =
                        new GISShare.Controls.WinForm.WFNew.View.NodeViewItem(one.Name, one.Text + "（Name：" + one.Name + "）");
                    nodeViewItem4.NodeViewItems.Add(node);
                    //this.UIView_DG(node, one as GISShare.Controls.WinForm.WFNew.ICollectionItem);
                }
                nodeViewItem1.NodeViewItems.Add(nodeViewItem4);
                //
                //
                //
                nodeViewItem.NodeViewItems.Add(nodeViewItem1);
            }
            #endregion
            //
            return nodeViewItem;            
        }
        private void UIView_DG(GISShare.Controls.WinForm.WFNew.View.NodeViewItem nodeViewItem, GISShare.Controls.WinForm.WFNew.ICollectionItem pCollectionItem)
        {
            if (nodeViewItem == null || pCollectionItem == null) return;
            //
            foreach (GISShare.Controls.WinForm.WFNew.IBaseItem one in pCollectionItem.BaseItems)
            {
                GISShare.Controls.WinForm.WFNew.View.NodeViewItem node = 
                    new GISShare.Controls.WinForm.WFNew.View.NodeViewItem(one.Name, one.Text + "（Name：" + one.Name + "）");
                nodeViewItem.NodeViewItems.Add(node);
                //
                this.UIView_DG(node, one as GISShare.Controls.WinForm.WFNew.ICollectionItem);
            }
        }
        
        //受保护
        protected virtual void OnPluginReflection(PluginReflectionEventArgs e)
        {
            if (this.PluginReflection != null) this.PluginReflection(this, e);
        }
    }
}
