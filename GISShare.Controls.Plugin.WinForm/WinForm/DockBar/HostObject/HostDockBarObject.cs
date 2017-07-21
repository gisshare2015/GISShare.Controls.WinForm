using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace GISShare.Controls.Plugin.WinForm.DockBar
{
    public class HostDockBarObject : GISShare.Controls.Plugin.IBaseHost, GISShare.Controls.Plugin.IBaseHost2, GISShare.Controls.Plugin.IBaseHost3
    {
        private string m_PluginDLLFolder = null;
        private string[] m_FilterFilePathArray = null;
        private bool m_FilterFilePathArrayTypeRemove = true;
        private string[] m_FilterObjectNameArray = null;
        private bool m_FilterObjectNameArrayTypeRemove = true;
        private object m_Hook = null;
        private GISShare.Controls.WinForm.WFNew.Forms.ITBForm m_pTBForm;
        private GISShare.Controls.WinForm.DockBar.DockBarManager m_DockBarManager;
        private GISShare.Controls.WinForm.WFNew.DockPanel.DockPanelManager m_DockPanelManager;
        //
        private PluginCategoryDictionary m_PluginCategoryDictionary = new PluginCategoryDictionary();

        /// <summary>
        /// pTBForm
        /// </summary>
        public GISShare.Controls.WinForm.WFNew.Forms.ITBForm pTBForm
        {
            get { return m_pTBForm; }
        }

        /// <summary>
        /// DockBarManager
        /// </summary>
        public GISShare.Controls.WinForm.DockBar.DockBarManager DockBarManager
        {
            get { return m_DockBarManager; }
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
        /// 
        /// </summary>
        /// <param name="pluginDLLFolder"></param>
        /// <param name="removeObjectNameArray"></param>
        /// <param name="hook"></param>
        /// <param name="dockBarManager"></param>
        /// <param name="dockPanelManager"></param>
        public HostDockBarObject(string pluginDLLFolder,
            string[] strFilterFilePathArray,
            bool bFilterFilePathArrayTypeRemove,
            string[] strFilterObjectNameArray,
            bool bFilterObjectNameArrayTypeRemove,
            object hook,
            GISShare.Controls.WinForm.WFNew.Forms.ITBForm pTBForm,
            GISShare.Controls.WinForm.DockBar.DockBarManager dockBarManager,
            GISShare.Controls.WinForm.WFNew.DockPanel.DockPanelManager dockPanelManager)
        {
            this.m_PluginDLLFolder = pluginDLLFolder;
            this.m_FilterFilePathArray = strFilterFilePathArray;
            this.m_FilterFilePathArrayTypeRemove = bFilterFilePathArrayTypeRemove;
            this.m_FilterObjectNameArray = strFilterObjectNameArray;
            this.m_FilterObjectNameArrayTypeRemove = bFilterObjectNameArrayTypeRemove;
            //
            this.m_Hook = hook;
            //
            this.m_pTBForm = pTBForm;
            this.m_DockBarManager = dockBarManager;
            this.m_DockPanelManager = dockPanelManager;
        }

        /// <summary>
        /// 装载插件(一旦装载成功再次调用则无效)
        /// </summary>
        public bool RunPluginEngine()
        {
            if (this.PluginCategoryDictionary.Count <= 0)
            {
                //读取插件
                this.GetPluginFromDLLFolder(this.PluginDLLFolder, this.FilterFilePathArray, this.FilterFilePathArrayTypeRemove, this.FilterObjectNameArray, this.FilterObjectNameArrayTypeRemove);
                //加载插件
                this.LoadPlugin(this.PluginCategoryDictionary, this.pTBForm, this.DockBarManager, this.DockPanelManager);
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
        /// 
        /// </summary>
        /// <param name="pluginCategoryDictionary"></param>
        /// <param name="dockBarManager"></param>
        /// <param name="dockPanelManager"></param>
        private void LoadPlugin(PluginCategoryDictionary pluginCategoryDictionary,
            GISShare.Controls.WinForm.WFNew.Forms.ITBForm pTBForm,
            GISShare.Controls.WinForm.DockBar.DockBarManager dockBarManager,
            GISShare.Controls.WinForm.WFNew.DockPanel.DockPanelManager dockPanelManager)
        {
            if (pTBForm != null)
            {
                //eTBForm_NCToolbarItems
                this.LoadPlugin_NC(pluginCategoryDictionary, pluginCategoryDictionary.GetPluginCategory((int)WFNew.Forms.CategoryIndex_4_Style.eTBForm_NCToolbarItems), pTBForm.ToolbarItems);
                //pTBForm.ShowQuickAccessToolbar = pTBForm.ToolbarItems.Count > 0;
            }
            //
            if (dockBarManager != null)
            {
                //DockBarManager.ToolBars
                this.LoadPlugin_ToolBar(pluginCategoryDictionary, pluginCategoryDictionary.GetPluginCategory((int)CategoryIndex_5_Style.eToolBar), dockBarManager.ToolBars, dockBarManager);
                //DockBarManager.MenuBar
                if (dockBarManager.MenuBar == null)
                {
                    PluginCategory pluginCategory = pluginCategoryDictionary.GetPluginCategory((int)CategoryIndex_5_Style.eMenuBar);
                    if (pluginCategory != null && pluginCategory.PluginCollection.Count > 0) 
                    {
                        IPlugin pPlugin = pluginCategory.PluginCollection[0];
                        //
                        GISShare.Controls.Plugin.WinForm.DockBar.IMenuBarP pMenuBarP = pPlugin as GISShare.Controls.Plugin.WinForm.DockBar.IMenuBarP;
                        dockBarManager.MenuBar = Controls.Plugin.WinForm.DockBar.MenuBarP.CreateMenuBar(pMenuBarP);
                        //
                        this.LoadPlugin_SubItem_DG(pluginCategoryDictionary, pPlugin as ISubItem, dockBarManager.MenuBar.Items, dockBarManager);
                        //
                        IPlugin2 pPlugin2_2 = pPlugin as IPlugin2;
                        if (pPlugin2_2 != null)
                        {
                            pPlugin2_2.OnCreate(this.Hook);
                        }
                        //
                        this.OnPluginReflection(
                            new PluginReflectionEventArgs(
                                PluginReflectionStyle.eEfficientlyEntityObject, pPlugin, "依据插件对象创建/组织有效地插件实体对象"));
                        //
                        switch (pMenuBarP.DockStyle)
                        {
                            case System.Windows.Forms.DockStyle.Top:
                            case System.Windows.Forms.DockStyle.Bottom:
                            case System.Windows.Forms.DockStyle.Left:
                            case System.Windows.Forms.DockStyle.Right:
                                dockBarManager.MenuBar.ToDockArea(pMenuBarP.DockStyle, pMenuBarP.Row, pMenuBarP.Location);
                                break;
                            default:
                                dockBarManager.MenuBar.ToDockBarFloatForm();
                                break;
                        }
                    }
                }
                //DockBarManager.StatusBar
                if (dockBarManager.StatusBar == null)
                {
                    PluginCategory pluginCategory = pluginCategoryDictionary.GetPluginCategory((int)CategoryIndex_5_Style.eStatusBar);
                    if (pluginCategory != null && pluginCategory.PluginCollection.Count > 0)
                    {
                        IPlugin pPlugin = pluginCategory.PluginCollection[0];
                        //
                        GISShare.Controls.Plugin.WinForm.DockBar.IStatusBarP pStatusBarP = pPlugin as GISShare.Controls.Plugin.WinForm.DockBar.IStatusBarP;
                        dockBarManager.StatusBar = Controls.Plugin.WinForm.DockBar.StatusBarP.CreateStatusBar(pStatusBarP);
                        //
                        this.LoadPlugin_SubItem_DG(pluginCategoryDictionary, pPlugin as ISubItem, dockBarManager.StatusBar.Items, dockBarManager);
                        //
                        IPlugin2 pPlugin2_2 = pPlugin as IPlugin2;
                        if (pPlugin2_2 != null)
                        {
                            pPlugin2_2.OnCreate(this.Hook);
                        }
                        //
                        this.OnPluginReflection(
                            new PluginReflectionEventArgs(
                                PluginReflectionStyle.eEfficientlyEntityObject, pPlugin, "依据插件对象创建/组织有效地插件实体对象"));
                        //
                        switch (pStatusBarP.DockStyle)
                        {
                            case System.Windows.Forms.DockStyle.Top:
                            case System.Windows.Forms.DockStyle.Bottom:
                            case System.Windows.Forms.DockStyle.Left:
                            case System.Windows.Forms.DockStyle.Right:
                                dockBarManager.StatusBar.ToDockArea(pStatusBarP.DockStyle, pStatusBarP.Row, pStatusBarP.Location);
                                break;
                            default:
                                dockBarManager.StatusBar.ToDockBarFloatForm();
                                break;
                        }
                    }
                }
                //DockBarManager.ContextMenus
                this.LoadPlugin_ContextMenu(pluginCategoryDictionary, pluginCategoryDictionary.GetPluginCategory((int)CategoryIndex_5_Style.eContextMenu), dockBarManager.ContextMenus, dockBarManager);
            }
            //
            if (dockPanelManager != null)
            {
                this.LoadPlugin(pluginCategoryDictionary, pluginCategoryDictionary.GetPluginCategory((int)WFNew.DockPanel.CategoryIndex_2_Style.eDockPanel), dockPanelManager);
            }
            //
            this.LoadPlugin(pluginCategoryDictionary, pluginCategoryDictionary.GetPluginCategory((int)CategoryIndex_0_Style.eInsertSubItem), pTBForm, dockBarManager);
        }
        //
        private void LoadPlugin(PluginCategoryDictionary pluginCategoryDictionary, 
            PluginCategory pluginCategory,
            GISShare.Controls.WinForm.WFNew.Forms.ITBForm pTBForm,
            GISShare.Controls.WinForm.DockBar.DockBarManager dockBarManager)
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
                    case (int)WFNew.Forms.CategoryIndex_4_Style.eTBForm_NCToolbarItems:
                        if (pTBForm != null) this.LoadPlugin_NC_InsertSubItem(pluginCategoryDictionary, one, pTBForm.ToolbarItems);
                        break;
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
                        if (pTBForm != null)
                        {
                            GISShare.Controls.WinForm.WFNew.ICollectionObjectDesignHelper pCollectionObjectDesignHelper = pTBForm.GetBaseItem2(one.DependItem) as GISShare.Controls.WinForm.WFNew.ICollectionObjectDesignHelper;
                            if (pCollectionObjectDesignHelper != null) this.LoadPlugin_NC_InsertSubItem(pluginCategoryDictionary, one, pCollectionObjectDesignHelper.List);
                        }
                        break;
                    //
                    //
                    //
                    case (int)CategoryIndex_5_Style.eMenuBar:
                        if (dockBarManager != null && dockBarManager.MenuBar != null) this.LoadPlugin_InsertSubItem(pluginCategoryDictionary, one, dockBarManager.MenuBar.Items, dockBarManager);
                        break;
                    case (int)CategoryIndex_5_Style.eStatusBar:
                        if (dockBarManager != null && dockBarManager.StatusBar != null) this.LoadPlugin_InsertSubItem(pluginCategoryDictionary, one, dockBarManager.StatusBar.Items, dockBarManager);
                        break;
                    //
                    case (int)CategoryIndex_5_Style.eToolBar:
                        if (dockBarManager != null)
                        {
                            GISShare.Controls.WinForm.WFNew.ICollectionObjectDesignHelper pCollectionObjectDesignHelper = dockBarManager.ToolBars[one.DependItem] as GISShare.Controls.WinForm.WFNew.ICollectionObjectDesignHelper;
                            if (pCollectionObjectDesignHelper != null) this.LoadPlugin_InsertSubItem(pluginCategoryDictionary, one, pCollectionObjectDesignHelper.List, dockBarManager);
                        }
                        break;
                    case (int)CategoryIndex_5_Style.eContextMenu:
                        if (dockBarManager != null)
                        {
                            GISShare.Controls.WinForm.WFNew.ICollectionObjectDesignHelper pCollectionObjectDesignHelper = dockBarManager.ContextMenus[one.DependItem] as GISShare.Controls.WinForm.WFNew.ICollectionObjectDesignHelper;
                            if (pCollectionObjectDesignHelper != null) this.LoadPlugin_InsertSubItem(pluginCategoryDictionary, one, pCollectionObjectDesignHelper.List, dockBarManager);
                        }
                        break;
                    //
                    case (int)CategoryIndex_5_Style.eDropDownButtonItem:
                    case (int)CategoryIndex_5_Style.eSplitButtonItem:
                    case (int)CategoryIndex_5_Style.eMenuItem:
                        if (dockBarManager != null)
                        {
                            System.Windows.Forms.ToolStripDropDownItem toolStripDropDownItem = dockBarManager.GetItem(one.DependItem) as System.Windows.Forms.ToolStripDropDownItem;
                            if (toolStripDropDownItem != null) this.LoadPlugin_InsertSubItem(pluginCategoryDictionary, one, toolStripDropDownItem.DropDownItems, dockBarManager);
                        }
                        break;
                }
            }
        }
        private void LoadPlugin_NC_InsertSubItem(PluginCategoryDictionary pluginCategoryDictionary, IInsertSubItem pInsertSubItem, IList list)
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
                GISShare.Controls.WinForm.WFNew.BaseItem baseItem = this.CreateBaseItem_NC(pPlugin);
                if (baseItem == null) continue;
                //
                if (pPlugin is ISubItem &&
                    baseItem is GISShare.Controls.WinForm.WFNew.ICollectionItem)
                {
                    this.LoadPlugin_NC_SubItem_DG(pluginCategoryDictionary, (ISubItem)pPlugin, ((GISShare.Controls.WinForm.WFNew.ICollectionItem)baseItem).BaseItems);
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
        private void LoadPlugin_InsertSubItem(PluginCategoryDictionary pluginCategoryDictionary, IInsertSubItem pInsertSubItem, IList list, GISShare.Controls.WinForm.DockBar.DockBarManager dockBarManager)
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
                System.Windows.Forms.ToolStripItem toolStripItem = this.CreateBaseItem(pPlugin);
                if (toolStripItem == null) continue;
                //
                if (pPlugin is ISubItem &&
                    toolStripItem is GISShare.Controls.WinForm.WFNew.ICollectionObjectDesignHelper)
                {
                    this.LoadPlugin_SubItem_DG(pluginCategoryDictionary, (ISubItem)pPlugin, ((GISShare.Controls.WinForm.WFNew.ICollectionObjectDesignHelper)toolStripItem).List, dockBarManager);
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
                list.Insert(iInsertIndex + i, toolStripItem);
            }
        }
        //
        private void LoadPlugin_NC(PluginCategoryDictionary pluginCategoryDictionary, PluginCategory pluginCategory, IList list)
        {
            if (pluginCategory == null) return;
            //
            foreach (IPlugin one in pluginCategory.PluginCollection)
            {
                if (one is ISubItem)
                {
                    this.LoadPlugin_NC_SubItem_DG(pluginCategoryDictionary, (ISubItem)one, list);
                }
                //
                this.OnPluginReflection(
                    new PluginReflectionEventArgs(
                        PluginReflectionStyle.eEfficientlyEntityObject, one, "依据插件对象创建/组织有效地插件实体对象"));
            }
        }
        private void LoadPlugin_NC_SubItem_DG(PluginCategoryDictionary pluginCategoryDictionary, ISubItem pSubItem, IList list)
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
                GISShare.Controls.WinForm.WFNew.BaseItem baseItem = this.CreateBaseItem_NC(pPlugin);
                if (baseItem == null) continue;
                //
                if (pPlugin is ISubItem &&
                    baseItem is GISShare.Controls.WinForm.WFNew.ICollectionObjectDesignHelper)
                {
                    this.LoadPlugin_NC_SubItem_DG(pluginCategoryDictionary, (ISubItem)pPlugin, ((GISShare.Controls.WinForm.WFNew.ICollectionObjectDesignHelper)baseItem).List);
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
                list.Add(baseItem);
            }
        }
        private GISShare.Controls.WinForm.WFNew.BaseItem CreateBaseItem_NC(IPlugin pPlugin)
        {
            if (pPlugin == null) return null;
            //
            switch (pPlugin.CategoryIndex)
            {
                #region 客户区按钮
                //
                #region 界面集合对象
                //case (int)WFNew.CategoryIndex_1_Style.eRibbonPageItem:
                //    return Controls.Plugin.WinForm.WFNew.RibbonPageItemP.CreateRibbonPageItem(pPlugin as WFNew.IRibbonPageItemP);
                //case (int)WFNew.CategoryIndex_1_Style.eRibbonBarItem:
                //    Controls.WinForm.WFNew.RibbonBarItem ribbonBarItemWFNew = Controls.Plugin.WinForm.WFNew.RibbonBarItemP.CreateRibbonBarItem(pPlugin as WFNew.IRibbonBarItemP);
                //    ribbonBarItemWFNew.GlyphMouseUp += new System.Windows.Forms.MouseEventHandler(ribbonBarItemWFNew_GlyphMouseUp);
                //    return ribbonBarItemWFNew;
                case (int)WFNew.CategoryIndex_1_Style.eBaseItemStackExItem:
                    return Controls.Plugin.WinForm.WFNew.BaseItemStackExItemP.CreateBaseItemStackExItem(pPlugin as WFNew.IBaseItemStackExItemP);
                case (int)WFNew.CategoryIndex_1_Style.eBaseItemStackItem:
                    return Controls.Plugin.WinForm.WFNew.BaseItemStackItemP.CreateBaseItemStackItem(pPlugin as WFNew.IBaseItemStackItemP);
                case (int)WFNew.CategoryIndex_1_Style.eButtonGroupItem:
                    return Controls.Plugin.WinForm.WFNew.ButtonGroupItemP.CreateButtonGroupItem(pPlugin as WFNew.IButtonGroupItemP);
                case (int)WFNew.CategoryIndex_1_Style.eRibbonGalleryItem:
                    return Controls.Plugin.WinForm.WFNew.RibbonGalleryItemP.CreateRibbonGalleryItem(pPlugin as WFNew.IRibbonGalleryItemP);
                case (int)WFNew.CategoryIndex_1_Style.eRibbonGalleryRowItem:
                    return Controls.Plugin.WinForm.WFNew.RibbonGalleryRowItemP.CreateRibbonGalleryRowItem(pPlugin as WFNew.IRibbonGalleryRowItemP);
                #endregion
                //
                #region 标签对象
                case (int)WFNew.CategoryIndex_1_Style.eLabelItem:
                    return Controls.Plugin.WinForm.WFNew.LabelItemP.CreateLabelItem(pPlugin as WFNew.ILabelItemP);
                case (int)WFNew.CategoryIndex_1_Style.eLabelExItem:
                    return Controls.Plugin.WinForm.WFNew.LabelExItemP.CreateLabelExItem(pPlugin as WFNew.ILabelExItemP);
                case (int)WFNew.CategoryIndex_1_Style.eImageLabelItem:
                    return Controls.Plugin.WinForm.WFNew.ImageLabelItemP.CreateImageLabelItem(pPlugin as WFNew.IImageLabelItemP);
                case (int)WFNew.CategoryIndex_1_Style.eLinkLabelItem:
                    Controls.WinForm.WFNew.LinkLabelItem linkLabelItemWFNew = Controls.Plugin.WinForm.WFNew.LinkLabelItemP.CreateLinkLabelItem(pPlugin as WFNew.ILinkLabelItemP);
                    linkLabelItemWFNew.MouseDown += new System.Windows.Forms.MouseEventHandler(linkLabelItemWFNew_MouseDown);
                    return linkLabelItemWFNew;
                case (int)WFNew.CategoryIndex_1_Style.eImageLinkLabelItem:
                    Controls.WinForm.WFNew.ImageLinkLabelItem imageLinkLabelItemWFNew = Controls.Plugin.WinForm.WFNew.ImageLinkLabelItemP.CreateImageLinkLabelItem(pPlugin as WFNew.IImageLinkLabelItemP);
                    imageLinkLabelItemWFNew.MouseDown += new System.Windows.Forms.MouseEventHandler(imageLinkLabelItemWFNew_MouseDown);
                    return imageLinkLabelItemWFNew;
                #endregion
                //
                #region 按钮对象
                case (int)WFNew.CategoryIndex_1_Style.eBaseButtonItem:
                    Controls.WinForm.WFNew.BaseButtonItem baseButtonItemWFNew = Controls.Plugin.WinForm.WFNew.BaseButtonItemP.CreateBaseButtonItem(pPlugin as WFNew.IBaseButtonItemP);
                    baseButtonItemWFNew.MouseUp += new System.Windows.Forms.MouseEventHandler(baseButtonItemWFNew_MouseUp);
                    return baseButtonItemWFNew;
                case (int)WFNew.CategoryIndex_1_Style.eBaseButtonExItem:
                    Controls.WinForm.WFNew.BaseButtonExItem baseButtonExItemWFNew = Controls.Plugin.WinForm.WFNew.BaseButtonExItemP.CreateBaseButtonExItem(pPlugin as WFNew.IBaseButtonExItemP);
                    baseButtonExItemWFNew.MouseUp += new System.Windows.Forms.MouseEventHandler(baseButtonExItemWFNew_MouseUp);
                    return baseButtonExItemWFNew;
                case (int)WFNew.CategoryIndex_1_Style.eCheckButtonItem:
                    Controls.WinForm.WFNew.CheckButtonItem checkButtonItemWFNew = Controls.Plugin.WinForm.WFNew.CheckButtonItemP.CreateCheckButtonItem(pPlugin as WFNew.ICheckButtonItemP);
                    checkButtonItemWFNew.CheckedChanged += new EventHandler(checkButtonItemWFNew_CheckedChanged);
                    return checkButtonItemWFNew;
                case (int)WFNew.CategoryIndex_1_Style.eCheckButtonExItem:
                    Controls.WinForm.WFNew.CheckButtonExItem checkButtonExItemWFNew = Controls.Plugin.WinForm.WFNew.CheckButtonExItemP.CreateCheckButtonExItem(pPlugin as WFNew.ICheckButtonExItemP);
                    checkButtonExItemWFNew.CheckedChanged += new EventHandler(checkButtonExItemWFNew_CheckedChanged);
                    return checkButtonExItemWFNew;
                case (int)WFNew.CategoryIndex_1_Style.eDescriptionButtonItem:
                    Controls.WinForm.WFNew.DescriptionButtonItem descriptionButtonItemWFNew = Controls.Plugin.WinForm.WFNew.DescriptionButtonItemP.CreateDescriptionButtonItem(pPlugin as WFNew.IDescriptionButtonItemP);
                    descriptionButtonItemWFNew.MouseUp += new System.Windows.Forms.MouseEventHandler(descriptionButtonItemWFNew_MouseUp);
                    return descriptionButtonItemWFNew;
                case (int)WFNew.CategoryIndex_1_Style.eDropDownButtonItem:
                    Controls.WinForm.WFNew.DropDownButtonItem dropDownButtonItemWFNew = Controls.Plugin.WinForm.WFNew.DropDownButtonItemP.CreateDropDownButtonItem(pPlugin as WFNew.IDropDownButtonItemP);
                    dropDownButtonItemWFNew.MouseUp += new System.Windows.Forms.MouseEventHandler(dropDownButtonItemWFNew_MouseUp);
                    return dropDownButtonItemWFNew;
                case (int)WFNew.CategoryIndex_1_Style.eDropDownButtonExItem:
                    Controls.WinForm.WFNew.DropDownButtonExItem dropDownButtonExItemWFNew = Controls.Plugin.WinForm.WFNew.DropDownButtonExItemP.CreateDropDownButtonExItem(pPlugin as WFNew.IDropDownButtonExItemP);
                    dropDownButtonExItemWFNew.MouseUp += new System.Windows.Forms.MouseEventHandler(dropDownButtonExItemWFNew_MouseUp);
                    return dropDownButtonExItemWFNew;
                case (int)WFNew.CategoryIndex_1_Style.eSplitButtonItem:
                    Controls.WinForm.WFNew.SplitButtonItem splitButtonItemWFNew = Controls.Plugin.WinForm.WFNew.SplitButtonItemP.CreateSplitButtonItem(pPlugin as WFNew.ISplitButtonItemP);
                    splitButtonItemWFNew.ButtonMouseUp += new System.Windows.Forms.MouseEventHandler(splitButtonItemWFNew_ButtonMouseUp);
                    return splitButtonItemWFNew;
                case (int)WFNew.CategoryIndex_1_Style.eSplitButtonExItem:
                    Controls.WinForm.WFNew.SplitButtonExItem splitButtonExItemWFNew = Controls.Plugin.WinForm.WFNew.SplitButtonExItemP.CreateSplitButtonExItem(pPlugin as WFNew.ISplitButtonExItemP);
                    splitButtonExItemWFNew.ButtonMouseUp += new System.Windows.Forms.MouseEventHandler(splitButtonItemWFNew_ButtonMouseUp);
                    return splitButtonExItemWFNew;
                case (int)WFNew.CategoryIndex_1_Style.eButtonItem:
                    Controls.WinForm.WFNew.ButtonItem buttonItemWFNew = Controls.Plugin.WinForm.WFNew.ButtonItemP.CreateButtonItem(pPlugin as WFNew.IButtonItemP);
                    buttonItemWFNew.ButtonMouseUp += new System.Windows.Forms.MouseEventHandler(buttonItemWFNew_ButtonMouseUp);
                    return buttonItemWFNew;
                case (int)WFNew.CategoryIndex_1_Style.eButtonExItem:
                    Controls.WinForm.WFNew.ButtonExItem buttonExItemWFNew = Controls.Plugin.WinForm.WFNew.ButtonExItemP.CreateButtonExItem(pPlugin as WFNew.IButtonExItemP);
                    buttonExItemWFNew.ButtonMouseUp += new System.Windows.Forms.MouseEventHandler(buttonExItemWFNew_ButtonMouseUp);
                    return buttonExItemWFNew;
                case (int)WFNew.CategoryIndex_1_Style.eMenuButtonItem:
                    Controls.WinForm.WFNew.MenuButtonItem menuButtonItemWFNew = Controls.Plugin.WinForm.WFNew.MenuButtonItemP.CreateMenuButtonItem(pPlugin as WFNew.IMenuButtonItemP);
                    menuButtonItemWFNew.ButtonMouseUp += new System.Windows.Forms.MouseEventHandler(menuButtonItemWFNew_ButtonMouseUp);
                    return menuButtonItemWFNew;
                case (int)WFNew.CategoryIndex_1_Style.eGlyphButtonItem:
                    Controls.WinForm.WFNew.GlyphButtonItem glyphButtonItem = Controls.Plugin.WinForm.WFNew.GlyphButtonItemP.CreateGlyphButtonItem(pPlugin as WFNew.IGlyphButtonItemP);
                    glyphButtonItem.MouseUp += new System.Windows.Forms.MouseEventHandler(glyphButtonItem_MouseUp);
                    return glyphButtonItem;
                #endregion
                //
                #region 复选框对象
                case (int)WFNew.CategoryIndex_1_Style.eCheckBoxItem:
                    Controls.WinForm.WFNew.CheckBoxItem checkBoxItemWFNew = Controls.Plugin.WinForm.WFNew.CheckBoxItemP.CreateCheckBoxItem(pPlugin as WFNew.ICheckBoxItemP);
                    checkBoxItemWFNew.CheckedChanged += new EventHandler(checkBoxItemWFNew_CheckedChanged);
                    return checkBoxItemWFNew;
                case (int)WFNew.CategoryIndex_1_Style.eImageCheckBoxItem:
                    Controls.WinForm.WFNew.ImageCheckBoxItem imageCheckBoxItemWFNew = Controls.Plugin.WinForm.WFNew.ImageCheckBoxItemP.CreateImageCheckBoxItem(pPlugin as WFNew.IImageCheckBoxItemP);
                    imageCheckBoxItemWFNew.CheckedChanged += new EventHandler(imageCheckBoxItemWFNew_CheckedChanged);
                    return imageCheckBoxItemWFNew;
                #endregion
                //
                //#region 寄宿对象
                //case (int)WFNew.CategoryIndex_1_Style.eControlHostItem:
                //    return Controls.Plugin.WinForm.WFNew.ControlHostItemP.CreateControlHostItem(pPlugin as WFNew.IControlHostItemP);
                //case (int)WFNew.CategoryIndex_1_Style.eControlPanelItem:
                //    return Controls.Plugin.WinForm.WFNew.ControlPanelItemP.CreateControlPanelItem(pPlugin as WFNew.IControlPanelItemP);
                //#endregion
                //
                #region 输入框对象
                case (int)WFNew.CategoryIndex_1_Style.eTextBoxItem:
                    Controls.WinForm.WFNew.TextBoxItem textBoxItemWFNew = Controls.Plugin.WinForm.WFNew.TextBoxItemP.CreateTextBoxItem(pPlugin as WFNew.ITextBoxItemP);
                    textBoxItemWFNew.TextChanged += new EventHandler(textBoxItemWFNew_TextChanged);
                    return textBoxItemWFNew;
                case (int)WFNew.CategoryIndex_1_Style.eIntegerInputBoxItem:
                    Controls.WinForm.WFNew.IntegerInputBoxItem integerInputBoxItemWFNew = Controls.Plugin.WinForm.WFNew.IntegerInputBoxItemP.CreateIntegerInputBoxItem(pPlugin as WFNew.IIntegerInputBoxItemP);
                    integerInputBoxItemWFNew.ValueChanged += new GISShare.Controls.WinForm.IntValueChangedHandler(integerInputBoxItemWFNew_ValueChanged);
                    return integerInputBoxItemWFNew;
                case (int)WFNew.CategoryIndex_1_Style.eDoubleInputBoxItem:
                    Controls.WinForm.WFNew.DoubleInputBoxItem doubleInputBoxItemWFNew = Controls.Plugin.WinForm.WFNew.DoubleInputBoxItemP.CreateDoubleInputBoxItem(pPlugin as WFNew.IDoubleInputBoxItemP);
                    doubleInputBoxItemWFNew.ValueChanged += new GISShare.Controls.WinForm.DoubleValueChangedHandler(doubleInputBoxItemWFNew_ValueChanged);
                    return doubleInputBoxItemWFNew;
                case (int)WFNew.CategoryIndex_1_Style.eButtonTextBoxItem:
                    Controls.WinForm.WFNew.ButtonTextBoxItem buttonTextBoxItem = Controls.Plugin.WinForm.WFNew.ButtonTextBoxItemP.CraeteButtonTextBoxItem(pPlugin as WFNew.IButtonTextBoxItemP);
                    buttonTextBoxItem.ButtonClick += new EventHandler(buttonTextBoxItem_ButtonClick);
                    return buttonTextBoxItem;
                #endregion
                //
                #region 下拉框对象
                case (int)WFNew.CategoryIndex_1_Style.eComboBoxItem:
                    Controls.WinForm.WFNew.ComboBoxItem comboBoxItemWFNew = Controls.Plugin.WinForm.WFNew.ComboBoxItemP.CreateComboBoxItem(pPlugin as WFNew.IComboBoxItemP);
                    comboBoxItemWFNew.SelectedIndexChanged += new GISShare.Controls.WinForm.IntValueChangedHandler(comboBoxItemWFNew_SelectedIndexChanged);
                    return comboBoxItemWFNew;
                case (int)WFNew.CategoryIndex_1_Style.eComboDateItem:
                    Controls.WinForm.WFNew.ComboDateItem comboDateItemWFNew = Controls.Plugin.WinForm.WFNew.ComboDateItemP.CreateComboDateItem(pPlugin as WFNew.IComboDateItemP);
                    comboDateItemWFNew.SelectedDateChanged += new GISShare.Controls.WinForm.PropertyChangedEventHandler(comboDateItemWFNew_SelectedDateChanged);
                    return comboDateItemWFNew;
                case (int)WFNew.CategoryIndex_1_Style.eComboTreeItem:
                    Controls.WinForm.WFNew.ComboTreeItem comboTreeItemWFNew = Controls.Plugin.WinForm.WFNew.ComboTreeItemP.CreateComboTreeItem(pPlugin as WFNew.IComboTreeItemP);
                    comboTreeItemWFNew.SelectedNodeChanged += new GISShare.Controls.WinForm.PropertyChangedEventHandler(comboTreeItemWFNew_SelectedNodeChanged);
                    return comboTreeItemWFNew;
                #endregion
                //
                #region 图片框对象
                case (int)WFNew.CategoryIndex_1_Style.eImageBoxItem:
                    return Controls.Plugin.WinForm.WFNew.ImageBoxItemP.CreateImageBoxItem(pPlugin as WFNew.IImageBoxItemP);
                #endregion
                //
                #region 进度条对象
                case (int)WFNew.CategoryIndex_1_Style.eProcessBarItem:
                    //Controls.WinForm.WFNew.ProcessBarItem processBarItemWFNew = Controls.Plugin.WinForm.WFNew.ProcessBarItemP.CreateProcessBarItem(pPlugin as WFNew.IProcessBarItemP);
                    ////processBarItemWFNew.ValueChanged += new GISShare.Controls.WinForm.IntValueChangedHandler(processBarItemWFNew_ValueChanged);
                    //return processBarItemWFNew;
                    return Controls.Plugin.WinForm.WFNew.ProcessBarItemP.CreateProcessBarItem(pPlugin as WFNew.IProcessBarItemP);
                #endregion
                //
                #region 单选框对象
                case (int)WFNew.CategoryIndex_1_Style.eRadioButtonItem:
                    Controls.WinForm.WFNew.RadioButtonItem radioButtonItemWFNew = Controls.Plugin.WinForm.WFNew.RadioButtonItemP.CreateRadioButtonItem(pPlugin as WFNew.IRadioButtonItemP);
                    radioButtonItemWFNew.CheckedChanged += new EventHandler(radioButtonItemWFNew_CheckedChanged);
                    return radioButtonItemWFNew;
                case (int)WFNew.CategoryIndex_1_Style.eImageRadioButtonItem:
                    Controls.WinForm.WFNew.ImageRadioButtonItem imageRadioButtonItemWFNew = Controls.Plugin.WinForm.WFNew.ImageRadioButtonItemP.CreateImageRadioButtonItem(pPlugin as WFNew.IImageRadioButtonItemP);
                    imageRadioButtonItemWFNew.CheckedChanged += new EventHandler(imageRadioButtonItemWFNew_CheckedChanged);
                    return imageRadioButtonItemWFNew;
                #endregion
                //
                #region 星对象
                case (int)WFNew.CategoryIndex_1_Style.eRatingStarItem:
                    Controls.WinForm.WFNew.RatingStarItem ratingStarItemWFNew = Controls.Plugin.WinForm.WFNew.RatingStarItemP.CreateRatingStarItem(pPlugin as WFNew.IRatingStarItemP);
                    ratingStarItemWFNew.ValueChanged += new GISShare.Controls.WinForm.IntValueChangedHandler(ratingStarItemWFNew_ValueChanged);
                    return ratingStarItemWFNew;
                #endregion
                //
                #region 分隔条对象
                case (int)WFNew.CategoryIndex_1_Style.eSeparatorItem:
                    return Controls.Plugin.WinForm.WFNew.SeparatorItemP.CreateSeparatorItem(pPlugin as WFNew.ISeparatorItemP);
                case (int)WFNew.CategoryIndex_1_Style.eLabelSeparatorItem:
                    return Controls.Plugin.WinForm.WFNew.LabelSeparatorItemP.CreateLabelSeparatorItem(pPlugin as WFNew.ILabelSeparatorItemP);
                case (int)WFNew.CategoryIndex_1_Style.eImageLabelSeparatorItem:
                    return Controls.Plugin.WinForm.WFNew.ImageLabelSeparatorItemP.CreateImageLabelSeparatorItem(pPlugin as WFNew.IImageLabelSeparatorItemP);
                #endregion
                //
                #region 滚动对象
                case (int)WFNew.CategoryIndex_1_Style.eSliderItem:
                    Controls.WinForm.WFNew.SliderItem sliderItemWFNew = Controls.Plugin.WinForm.WFNew.SliderItemP.CreateSliderItem(pPlugin as WFNew.ISliderItemP);
                    sliderItemWFNew.ValueChanged += new GISShare.Controls.WinForm.DoubleValueChangedHandler(sliderItemWFNew_ValueChanged);
                    return sliderItemWFNew;
                case (int)WFNew.CategoryIndex_1_Style.eScrollBarItem:
                    Controls.WinForm.WFNew.ScrollBarItem scrollBarItemWFNew = Controls.Plugin.WinForm.WFNew.ScrollBarItemP.CreateScrollBarItem(pPlugin as WFNew.IScrollBarItemP);
                    scrollBarItemWFNew.ValueChanged += new GISShare.Controls.WinForm.IntValueChangedHandler(scrollBarItemWFNew_ValueChanged);
                    return scrollBarItemWFNew;
                #endregion
                //
                #endregion
                //
                //
                //
                default:
                    break;
            }
            //
            return this.CreateBaseItemDef_NC(pPlugin);
        }
        protected virtual GISShare.Controls.WinForm.WFNew.BaseItem CreateBaseItemDef_NC(IPlugin pPlugin)
        {
            return null;
        }
        //void ribbonBarItemWFNew_GlyphMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        //{
        //    GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = sender as GISShare.Controls.WinForm.WFNew.IBaseItem;
        //    if (pBaseItem == null) return;
        //    //
        //    IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)WFNew.CategoryIndex_1_Style.eRibbonBarItem, pBaseItem.Name) as IEventChain;
        //    if (pEventChain != null) pEventChain.OnTriggerEvent((int)WFNew.Event_1_Style.eGlyphMouseUp, e);
        //}
        void linkLabelItemWFNew_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = sender as GISShare.Controls.WinForm.WFNew.IBaseItem;
            if (pBaseItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)WFNew.CategoryIndex_1_Style.eLinkLabelItem, pBaseItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)WFNew.Event_1_Style.eMouseDown, e);
        }
        void imageLinkLabelItemWFNew_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = sender as GISShare.Controls.WinForm.WFNew.IBaseItem;
            if (pBaseItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)WFNew.CategoryIndex_1_Style.eImageLinkLabelItem, pBaseItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)WFNew.Event_1_Style.eMouseDown, e);
        }
        void baseButtonItemWFNew_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = sender as GISShare.Controls.WinForm.WFNew.IBaseItem;
            if (pBaseItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)WFNew.CategoryIndex_1_Style.eBaseButtonItem, pBaseItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)WFNew.Event_1_Style.eMouseUp, e);
        }
        void baseButtonExItemWFNew_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = sender as GISShare.Controls.WinForm.WFNew.IBaseItem;
            if (pBaseItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)WFNew.CategoryIndex_1_Style.eBaseButtonExItem, pBaseItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)WFNew.Event_1_Style.eMouseUp, e);
        }
        void checkButtonItemWFNew_CheckedChanged(object sender, EventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = sender as GISShare.Controls.WinForm.WFNew.IBaseItem;
            if (pBaseItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)WFNew.CategoryIndex_1_Style.eCheckButtonItem, pBaseItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)WFNew.Event_1_Style.eCheckedChanged, e);
        }
        void checkButtonExItemWFNew_CheckedChanged(object sender, EventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = sender as GISShare.Controls.WinForm.WFNew.IBaseItem;
            if (pBaseItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)WFNew.CategoryIndex_1_Style.eCheckButtonExItem, pBaseItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)WFNew.Event_1_Style.eCheckedChanged, e);
        }
        void descriptionButtonItemWFNew_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = sender as GISShare.Controls.WinForm.WFNew.IBaseItem;
            if (pBaseItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)WFNew.CategoryIndex_1_Style.eDescriptionButtonItem, pBaseItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)WFNew.Event_1_Style.eMouseUp, e);
        }
        void dropDownButtonItemWFNew_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = sender as GISShare.Controls.WinForm.WFNew.IBaseItem;
            if (pBaseItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)WFNew.CategoryIndex_1_Style.eDropDownButtonItem, pBaseItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)WFNew.Event_1_Style.eMouseUp, e);
        }
        void dropDownButtonExItemWFNew_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = sender as GISShare.Controls.WinForm.WFNew.IBaseItem;
            if (pBaseItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)WFNew.CategoryIndex_1_Style.eDropDownButtonExItem, pBaseItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)WFNew.Event_1_Style.eMouseUp, e);
        }
        void splitButtonItemWFNew_ButtonMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = sender as GISShare.Controls.WinForm.WFNew.IBaseItem;
            if (pBaseItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)WFNew.CategoryIndex_1_Style.eSplitButtonItem, pBaseItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)WFNew.Event_1_Style.eButtonMouseUp, e);
        }
        void splitButtonExItemWFNew_ButtonMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = sender as GISShare.Controls.WinForm.WFNew.IBaseItem;
            if (pBaseItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)WFNew.CategoryIndex_1_Style.eSplitButtonExItem, pBaseItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)WFNew.Event_1_Style.eButtonMouseUp, e);
        }
        void buttonItemWFNew_ButtonMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = sender as GISShare.Controls.WinForm.WFNew.IBaseItem;
            if (pBaseItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)WFNew.CategoryIndex_1_Style.eButtonItem, pBaseItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)WFNew.Event_1_Style.eButtonMouseUp, e);
        }
        void buttonExItemWFNew_ButtonMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = sender as GISShare.Controls.WinForm.WFNew.IBaseItem;
            if (pBaseItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)WFNew.CategoryIndex_1_Style.eButtonExItem, pBaseItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)WFNew.Event_1_Style.eButtonMouseUp, e);
        }
        void menuButtonItemWFNew_ButtonMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = sender as GISShare.Controls.WinForm.WFNew.IBaseItem;
            if (pBaseItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)WFNew.CategoryIndex_1_Style.eMenuButtonItem, pBaseItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)WFNew.Event_1_Style.eButtonMouseUp, e);
        }
        void glyphButtonItem_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = sender as GISShare.Controls.WinForm.WFNew.IBaseItem;
            if (pBaseItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)WFNew.CategoryIndex_1_Style.eGlyphButtonItem, pBaseItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)WFNew.Event_1_Style.eMouseUp, e);
        }
        void checkBoxItemWFNew_CheckedChanged(object sender, EventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = sender as GISShare.Controls.WinForm.WFNew.IBaseItem;
            if (pBaseItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)WFNew.CategoryIndex_1_Style.eCheckBoxItem, pBaseItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)WFNew.Event_1_Style.eCheckedChanged, e);
        }
        void textBoxItemWFNew_TextChanged(object sender, EventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = sender as GISShare.Controls.WinForm.WFNew.IBaseItem;
            if (pBaseItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)WFNew.CategoryIndex_1_Style.eTextBoxItem, pBaseItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)WFNew.Event_1_Style.eTextChanged, e);
        }
        void integerInputBoxItemWFNew_ValueChanged(object sender, GISShare.Controls.WinForm.IntValueChangedEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = sender as GISShare.Controls.WinForm.WFNew.IBaseItem;
            if (pBaseItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)WFNew.CategoryIndex_1_Style.eIntegerInputBoxItem, pBaseItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)WFNew.Event_1_Style.eValueChanged, e);
        }
        void doubleInputBoxItemWFNew_ValueChanged(object sender, GISShare.Controls.WinForm.DoubleValueChangedEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = sender as GISShare.Controls.WinForm.WFNew.IBaseItem;
            if (pBaseItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)WFNew.CategoryIndex_1_Style.eDoubleInputBoxItem, pBaseItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)WFNew.Event_1_Style.eValueChanged, e);
        }
        void buttonTextBoxItem_ButtonClick(object sender, EventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = sender as GISShare.Controls.WinForm.WFNew.IBaseItem;
            if (pBaseItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)WFNew.CategoryIndex_1_Style.eButtonTextBoxItem, pBaseItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)WFNew.Event_1_Style.eButtonClick, e);
        }
        void comboBoxItemWFNew_SelectedIndexChanged(object sender, GISShare.Controls.WinForm.IntValueChangedEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = sender as GISShare.Controls.WinForm.WFNew.IBaseItem;
            if (pBaseItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)WFNew.CategoryIndex_1_Style.eComboBoxItem, pBaseItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)WFNew.Event_1_Style.eSelectedIndexChanged, e);
        }
        void comboDateItemWFNew_SelectedDateChanged(object sender, GISShare.Controls.WinForm.PropertyChangedEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = sender as GISShare.Controls.WinForm.WFNew.IBaseItem;
            if (pBaseItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)WFNew.CategoryIndex_1_Style.eComboDateItem, pBaseItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)WFNew.Event_1_Style.eSelectedDateChanged, e);
        }
        void comboTreeItemWFNew_SelectedNodeChanged(object sender, GISShare.Controls.WinForm.PropertyChangedEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = sender as GISShare.Controls.WinForm.WFNew.IBaseItem;
            if (pBaseItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)WFNew.CategoryIndex_1_Style.eComboTreeItem, pBaseItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)WFNew.Event_1_Style.eSelectedNodeChanged, e);
        }
        void imageCheckBoxItemWFNew_CheckedChanged(object sender, EventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = sender as GISShare.Controls.WinForm.WFNew.IBaseItem;
            if (pBaseItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)WFNew.CategoryIndex_1_Style.eImageCheckBoxItem, pBaseItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)WFNew.Event_1_Style.eCheckedChanged, e);
        }
        //void processBarItemWFNew_ValueChanged(object sender, GISShare.Controls.WinForm.IntValueChangedEventArgs e)
        //{
        //    GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = sender as GISShare.Controls.WinForm.WFNew.IBaseItem;
        //    if (pBaseItem == null) return;
        //    //
        //    IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)WFNew.CategoryIndex_1_Style.eProcessBarItem, pBaseItem.Name) as IEventChain;
        //    if (pEventChain != null) pEventChain.OnTriggerEvent((int)WFNew.Event_1_Style.eValueChanged, e);
        //}
        void radioButtonItemWFNew_CheckedChanged(object sender, EventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = sender as GISShare.Controls.WinForm.WFNew.IBaseItem;
            if (pBaseItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)WFNew.CategoryIndex_1_Style.eRadioButtonItem, pBaseItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)WFNew.Event_1_Style.eCheckedChanged, e);
        }
        void imageRadioButtonItemWFNew_CheckedChanged(object sender, EventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = sender as GISShare.Controls.WinForm.WFNew.IBaseItem;
            if (pBaseItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)WFNew.CategoryIndex_1_Style.eImageRadioButtonItem, pBaseItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)WFNew.Event_1_Style.eCheckedChanged, e);
        }
        void ratingStarItemWFNew_ValueChanged(object sender, GISShare.Controls.WinForm.IntValueChangedEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = sender as GISShare.Controls.WinForm.WFNew.IBaseItem;
            if (pBaseItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)WFNew.CategoryIndex_1_Style.eRatingStarItem, pBaseItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)WFNew.Event_1_Style.eValueChanged, e);
        }
        void sliderItemWFNew_ValueChanged(object sender, GISShare.Controls.WinForm.DoubleValueChangedEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = sender as GISShare.Controls.WinForm.WFNew.IBaseItem;
            if (pBaseItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)WFNew.CategoryIndex_1_Style.eSliderItem, pBaseItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)WFNew.Event_1_Style.eValueChanged, e);
        }
        void scrollBarItemWFNew_ValueChanged(object sender, GISShare.Controls.WinForm.IntValueChangedEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.IBaseItem pBaseItem = sender as GISShare.Controls.WinForm.WFNew.IBaseItem;
            if (pBaseItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)WFNew.CategoryIndex_1_Style.eScrollBarItem, pBaseItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)WFNew.Event_1_Style.eValueChanged, e);
        }
        //
        private void LoadPlugin(PluginCategoryDictionary pluginCategoryDictionary, PluginCategory pluginCategory, GISShare.Controls.WinForm.WFNew.DockPanel.DockPanelManager dockPanelManager)
        {
            if (pluginCategory == null) return;
            //
            foreach (IPlugin one in pluginCategory.PluginCollection)
            {
                switch ((WFNew.DockPanel.CategoryIndex_2_Style)one.CategoryIndex)
                {
                    case WFNew.DockPanel.CategoryIndex_2_Style.eDockPanel:
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
                                IPlugin pPlugin = pluginCategoryDictionary.GetPlugin((int)WFNew.DockPanel.CategoryIndex_2_Style.eBasePanel, pItemDef.ID);
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
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)WFNew.DockPanel.CategoryIndex_2_Style.eBasePanel, pBaseItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)WFNew.DockPanel.Event_2_Style.ePanelNodeStateChanged, e);
        }
        //
        private void LoadPlugin_ToolBar(PluginCategoryDictionary pluginCategoryDictionary, PluginCategory pluginCategory, IList list, GISShare.Controls.WinForm.DockBar.DockBarManager dockBarManager)
        {
            if (pluginCategory == null) return;
            //
            foreach (IPlugin one in pluginCategory.PluginCollection)
            {
                switch ((CategoryIndex_5_Style)one.CategoryIndex)
                {
                    case CategoryIndex_5_Style.eToolBar:
                        GISShare.Controls.Plugin.WinForm.DockBar.IToolBarP pToolBarP = one as GISShare.Controls.Plugin.WinForm.DockBar.IToolBarP;
                        GISShare.Controls.WinForm.DockBar.ToolBar toolBar = Controls.Plugin.WinForm.DockBar.ToolBarP.CreateToolBar(pToolBarP);
                        //
                        this.LoadPlugin_SubItem_DG(pluginCategoryDictionary, one as ISubItem, toolBar.Items, dockBarManager);
                        //
                        IPlugin2 pPlugin2 = one as IPlugin2;
                        if (pPlugin2 == null) continue;
                        pPlugin2.OnCreate(this.Hook);
                        //
                        this.OnPluginReflection(
                            new PluginReflectionEventArgs(
                                PluginReflectionStyle.eEfficientlyEntityObject, one, "依据插件对象创建/组织有效地插件实体对象"));
                        //
                        list.Add(toolBar);
                        //
                        switch (pToolBarP.DockStyle)
                        {
                            case System.Windows.Forms.DockStyle.Top:
                            case System.Windows.Forms.DockStyle.Bottom:
                            case System.Windows.Forms.DockStyle.Left:
                            case System.Windows.Forms.DockStyle.Right:
                                toolBar.ToDockArea(pToolBarP.DockStyle, pToolBarP.Row, pToolBarP.Location);
                                break;
                            default:
                                toolBar.ToDockBarFloatForm();
                                break;
                        }
                        break;
                }
            }
        }
        private void LoadPlugin_ContextMenu(PluginCategoryDictionary pluginCategoryDictionary, PluginCategory pluginCategory, IList list, GISShare.Controls.WinForm.DockBar.DockBarManager dockBarManager)
        {
            if (pluginCategory == null) return;
            //
            foreach (IPlugin one in pluginCategory.PluginCollection)
            {
                switch ((CategoryIndex_5_Style)one.CategoryIndex)
                {
                    case CategoryIndex_5_Style.eContextMenu:
                        GISShare.Controls.WinForm.DockBar.ContextMenu contextMenu = Controls.Plugin.WinForm.DockBar.ContextMenuP.CreateContextMenu(one as GISShare.Controls.Plugin.WinForm.DockBar.IContextMenuP);
                        contextMenu.Opened += new EventHandler(contextMenu_Opened);
                        contextMenu.Closed += new System.Windows.Forms.ToolStripDropDownClosedEventHandler(contextMenu_Closed);
                        //
                        this.LoadPlugin_SubItem_DG(pluginCategoryDictionary, one as ISubItem, contextMenu.Items, dockBarManager);
                        //
                        IPlugin2 pPlugin2 = one as IPlugin2;
                        if (pPlugin2 == null) continue;
                        pPlugin2.OnCreate(this.Hook);
                        //
                        this.OnPluginReflection(
                            new PluginReflectionEventArgs(
                                PluginReflectionStyle.eEfficientlyEntityObject, one, "依据插件对象创建/组织有效地插件实体对象"));
                        //
                        list.Add(contextMenu);
                        break;
                }
            }
        }
        void contextMenu_Opened(object sender, EventArgs e)
        {
            System.Windows.Forms.ContextMenu contextMenu = sender as System.Windows.Forms.ContextMenu;
            if (contextMenu == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)CategoryIndex_5_Style.eContextMenu, contextMenu.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)Event_5_Style.eOpened, e);
        }
        void contextMenu_Closed(object sender, System.Windows.Forms.ToolStripDropDownClosedEventArgs e)
        {
            System.Windows.Forms.ContextMenu contextMenu = sender as System.Windows.Forms.ContextMenu;
            if (contextMenu == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)CategoryIndex_5_Style.eContextMenu, contextMenu.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)Event_5_Style.eClosed, e);
        }
        //
        private void LoadPlugin_SubItem_DG(PluginCategoryDictionary pluginCategoryDictionary, ISubItem pSubItem, IList list, GISShare.Controls.WinForm.DockBar.DockBarManager dockBarManager)
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
                System.Windows.Forms.ToolStripItem toolStripItem = this.CreateBaseItem(pPlugin);
                if (toolStripItem == null) continue;
                //
                if (pPlugin is ISubItem &&
                    toolStripItem is GISShare.Controls.WinForm.WFNew.ICollectionObjectDesignHelper)
                {
                    this.LoadPlugin_SubItem_DG(pluginCategoryDictionary, (ISubItem)pPlugin, ((GISShare.Controls.WinForm.WFNew.ICollectionObjectDesignHelper)toolStripItem).List, dockBarManager);
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
                list.Add(toolStripItem);
                //
                GISShare.Controls.WinForm.DockBar.IBaseItemDB pBaseItemDB = toolStripItem as GISShare.Controls.WinForm.DockBar.IBaseItemDB;
                if (pBaseItemDB != null &&
                    pBaseItemDB.Category != null &&
                    pBaseItemDB.Category.Length > 0) 
                {
                    dockBarManager.BaseItems.Add(pBaseItemDB); 
                }
            }
        }
        private System.Windows.Forms.ToolStripItem CreateBaseItem(IPlugin pPlugin)
        {
            if (pPlugin == null) return null;
            //
            switch ((CategoryIndex_5_Style)pPlugin.CategoryIndex)
            {
                case CategoryIndex_5_Style.eMenuItem:
                    GISShare.Controls.WinForm.DockBar.MenuItem menuItem = Controls.Plugin.WinForm.DockBar.MenuItemP.CreateMenuItem(pPlugin as GISShare.Controls.Plugin.WinForm.DockBar.IMenuItemP);
                    menuItem.Click += new EventHandler(menuItem_Click);
                    //menuItem.DropDownOpened += new EventHandler(menuItem_DropDownOpened);
                    //menuItem.DropDownClosed += new EventHandler(menuItem_DropDownClosed);
                    return menuItem;
                case CategoryIndex_5_Style.eDropDownButtonItem:
                    GISShare.Controls.WinForm.DockBar.DropDownButtonItem dropDownButtonItem = Controls.Plugin.WinForm.DockBar.DropDownButtonItemP.CreateDropDownButtonItem(pPlugin as GISShare.Controls.Plugin.WinForm.DockBar.IDropDownButtonItemP);
                    dropDownButtonItem.Click += new EventHandler(dropDownButtonItem_Click);
                    //dropDownButtonItem.DropDownOpened += new EventHandler(dropDownButtonItem_DropDownOpened);
                    //dropDownButtonItem.DropDownClosed += new EventHandler(dropDownButtonItem_DropDownClosed);
                    return dropDownButtonItem;
                case CategoryIndex_5_Style.eSplitButtonItem:
                    GISShare.Controls.WinForm.DockBar.SplitButtonItem splitButtonItem = Controls.Plugin.WinForm.DockBar.SplitButtonItemP.CreateSplitButtonItem(pPlugin as GISShare.Controls.Plugin.WinForm.DockBar.ISplitButtonItemP);
                    splitButtonItem.Click += new EventHandler(splitButtonItem_Click);
                    //splitButtonItem.DropDownOpened += new EventHandler(splitButtonItem_DropDownOpened);
                    //splitButtonItem.DropDownClosed += new EventHandler(splitButtonItem_DropDownClosed);
                    return splitButtonItem;
                    //
                case CategoryIndex_5_Style.eButtonItem:
                    GISShare.Controls.WinForm.DockBar.ButtonItem buttonItem = Controls.Plugin.WinForm.DockBar.ButtonItemP.CreateButtonItem(pPlugin as GISShare.Controls.Plugin.WinForm.DockBar.IButtonItemP);
                    buttonItem.Click += new EventHandler(buttonItem_Click);
                    return buttonItem;
                case CategoryIndex_5_Style.eCheckBoxItem:
                    GISShare.Controls.WinForm.DockBar.CheckBoxItem checkBoxItem = Controls.Plugin.WinForm.DockBar.CheckBoxItemP.CreateCheckBoxItem(pPlugin as GISShare.Controls.Plugin.WinForm.DockBar.ICheckBoxItemP);
                    checkBoxItem.CheckedChanged += new EventHandler(checkBoxItem_CheckedChanged);
                    return checkBoxItem;
                case CategoryIndex_5_Style.eComboBoxItem:
                    GISShare.Controls.WinForm.DockBar.ComboBoxItem comboBoxItem = Controls.Plugin.WinForm.DockBar.ComboBoxItemP.CreateComboBoxItem(pPlugin as GISShare.Controls.Plugin.WinForm.DockBar.IComboBoxItemP);
                    comboBoxItem.SelectedIndexChanged += new EventHandler(comboBoxItem_SelectedIndexChanged);
                    return comboBoxItem;
                case CategoryIndex_5_Style.eLabelItem:
                    GISShare.Controls.WinForm.DockBar.LabelItem labelItem = Controls.Plugin.WinForm.DockBar.LabelItemP.CreateLabelItem(pPlugin as GISShare.Controls.Plugin.WinForm.DockBar.ILabelItemP);
                    labelItem.Click += new EventHandler(labelItem_Click);
                    return labelItem;
                case CategoryIndex_5_Style.eMaskedTextBoxItem:
                    GISShare.Controls.WinForm.DockBar.MaskedTextBoxItem maskedTextBoxItem = Controls.Plugin.WinForm.DockBar.MaskedTextBoxItemP.CreateMaskedTextBoxItem(pPlugin as GISShare.Controls.Plugin.WinForm.DockBar.IMaskedTextBoxItemP);
                    maskedTextBoxItem.TextChanged += new EventHandler(maskedTextBoxItem_TextChanged);
                    return maskedTextBoxItem;
                case CategoryIndex_5_Style.eRadioButtonItem:
                    GISShare.Controls.WinForm.DockBar.RadioButtonItem radioButtonItem = Controls.Plugin.WinForm.DockBar.RadioButtonItemP.CreateRadioButtonItem(pPlugin as GISShare.Controls.Plugin.WinForm.DockBar.IRadioButtonItemP);
                    radioButtonItem.CheckedChanged += new EventHandler(radioButtonItem_CheckedChanged);
                    return radioButtonItem;
                case CategoryIndex_5_Style.eNumericUpDownItem:
                    GISShare.Controls.WinForm.DockBar.NumericUpDownItem numericUpDownItem = Controls.Plugin.WinForm.DockBar.NumericUpDownItemP.CreateNumericUpDownItem(pPlugin as GISShare.Controls.Plugin.WinForm.DockBar.INumericUpDownItemP);
                    numericUpDownItem.ValueChanged += new EventHandler(numericUpDownItem_ValueChanged);
                    return numericUpDownItem;
                case CategoryIndex_5_Style.eProgressBarItem:
                    return Controls.Plugin.WinForm.DockBar.ProgressBarItemP.CreateProgressBarItem(pPlugin as GISShare.Controls.Plugin.WinForm.DockBar.IProgressBarItemP);
                case CategoryIndex_5_Style.eSeparatorItem:
                    return Controls.Plugin.WinForm.DockBar.SeparatorItemP.CreateSeparatorItem(pPlugin as GISShare.Controls.Plugin.WinForm.DockBar.ISeparatorItemP);
                case CategoryIndex_5_Style.eTextBoxItem:
                    GISShare.Controls.WinForm.DockBar.TextBoxItem textBoxItem = Controls.Plugin.WinForm.DockBar.TextBoxItemP.CreateTextBoxItem(pPlugin as GISShare.Controls.Plugin.WinForm.DockBar.ITextBoxItemP);
                    textBoxItem.TextChanged += new EventHandler(textBoxItem_TextChanged);
                    return textBoxItem;
                    //
                default:
                    break;
            }
            return this.CreateBaseItemDef(pPlugin);
        }
        protected virtual System.Windows.Forms.ToolStripItem CreateBaseItemDef(IPlugin pPlugin)
        {
            return null;
        }
        void menuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolStripItem toolStripItem = sender as System.Windows.Forms.ToolStripItem;
            if (toolStripItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)CategoryIndex_5_Style.eMenuItem, toolStripItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)Event_5_Style.eClick, e);
        }
        void menuItem_DropDownOpened(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolStripItem toolStripItem = sender as System.Windows.Forms.ToolStripItem;
            if (toolStripItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)CategoryIndex_5_Style.eMenuItem, toolStripItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)Event_5_Style.eDropDownOpened, e);
        }
        void menuItem_DropDownClosed(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolStripItem toolStripItem = sender as System.Windows.Forms.ToolStripItem;
            if (toolStripItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)CategoryIndex_5_Style.eMenuItem, toolStripItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)Event_5_Style.eDropDownClosed, e);
        }
        void dropDownButtonItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolStripItem toolStripItem = sender as System.Windows.Forms.ToolStripItem;
            if (toolStripItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)CategoryIndex_5_Style.eDropDownButtonItem, toolStripItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)Event_5_Style.eClick, e);
        }
        void dropDownButtonItem_DropDownOpened(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolStripItem toolStripItem = sender as System.Windows.Forms.ToolStripItem;
            if (toolStripItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)CategoryIndex_5_Style.eDropDownButtonItem, toolStripItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)Event_5_Style.eDropDownOpened, e);
        }
        void dropDownButtonItem_DropDownClosed(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolStripItem toolStripItem = sender as System.Windows.Forms.ToolStripItem;
            if (toolStripItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)CategoryIndex_5_Style.eDropDownButtonItem, toolStripItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)Event_5_Style.eDropDownClosed, e);
        }
        void splitButtonItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolStripItem toolStripItem = sender as System.Windows.Forms.ToolStripItem;
            if (toolStripItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)CategoryIndex_5_Style.eSplitButtonItem, toolStripItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)Event_5_Style.eClick, e);
        }
        void splitButtonItem_DropDownOpened(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolStripItem toolStripItem = sender as System.Windows.Forms.ToolStripItem;
            if (toolStripItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)CategoryIndex_5_Style.eSplitButtonItem, toolStripItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)Event_5_Style.eDropDownOpened, e);
        }
        void splitButtonItem_DropDownClosed(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolStripItem toolStripItem = sender as System.Windows.Forms.ToolStripItem;
            if (toolStripItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)CategoryIndex_5_Style.eSplitButtonItem, toolStripItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)Event_5_Style.eDropDownClosed, e);
        }
        void buttonItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolStripItem toolStripItem = sender as System.Windows.Forms.ToolStripItem;
            if (toolStripItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)CategoryIndex_5_Style.eButtonItem, toolStripItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)Event_5_Style.eClick, e);
        }
        void checkBoxItem_CheckedChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolStripItem toolStripItem = sender as System.Windows.Forms.ToolStripItem;
            if (toolStripItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)CategoryIndex_5_Style.eCheckBoxItem, toolStripItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)Event_5_Style.eCheckedChanged, e);
        }
        void comboBoxItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolStripItem toolStripItem = sender as System.Windows.Forms.ToolStripItem;
            if (toolStripItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)CategoryIndex_5_Style.eComboBoxItem, toolStripItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)Event_5_Style.eSelectedIndexChanged, e);
        }
        void labelItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolStripItem toolStripItem = sender as System.Windows.Forms.ToolStripItem;
            if (toolStripItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)CategoryIndex_5_Style.eLabelItem, toolStripItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)Event_5_Style.eClick, e);
        }
        void maskedTextBoxItem_TextChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolStripItem toolStripItem = sender as System.Windows.Forms.ToolStripItem;
            if (toolStripItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)CategoryIndex_5_Style.eMaskedTextBoxItem, toolStripItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)Event_5_Style.eTextChanged, e);
        }
        void radioButtonItem_CheckedChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolStripItem toolStripItem = sender as System.Windows.Forms.ToolStripItem;
            if (toolStripItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)CategoryIndex_5_Style.eRadioButtonItem, toolStripItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)Event_5_Style.eCheckedChanged, e);
        }
        void numericUpDownItem_ValueChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolStripItem toolStripItem = sender as System.Windows.Forms.ToolStripItem;
            if (toolStripItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)CategoryIndex_5_Style.eNumericUpDownItem, toolStripItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)Event_5_Style.eValueChanged, e);
        }
        void textBoxItem_TextChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolStripItem toolStripItem = sender as System.Windows.Forms.ToolStripItem;
            if (toolStripItem == null) return;
            //
            IEventChain pEventChain = this.PluginCategoryDictionary.GetPlugin((int)CategoryIndex_5_Style.eTextBoxItem, toolStripItem.Name) as IEventChain;
            if (pEventChain != null) pEventChain.OnTriggerEvent((int)Event_5_Style.eTextChanged, e);
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
            AppendPluginForm2 appendPluginForm = new AppendPluginForm2(this);
            appendPluginForm.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            appendPluginForm.ShowDialog();
        }

        /// <summary>
        /// 追加插件对象
        /// </summary>
        /// <param name="pluginDLLFile"></param>
        public PluginCategoryDictionary AppendPluginObject(string pluginDLLFile)
        {
            if (pluginDLLFile == null || pluginDLLFile.Length <= 0) return null;
            //
            PluginHandle pluginHandle = new PluginHandle();
            //pluginHandle.PluginReflection -= new PluginReflectionEventHandler(PluginHandle_PluginReflection);
            PluginCategoryDictionary pluginCategoryDictionary = pluginHandle.GetPluginsFromDLLFiles(new string[] { pluginDLLFile }, this.FilterObjectNameArray, this.FilterObjectNameArrayTypeRemove).GetDifferent(this.PluginCategoryDictionary);
            //pluginHandle.PluginReflection += new PluginReflectionEventHandler(PluginHandle_PluginReflection);
            //
            this.LoadPlugin(pluginCategoryDictionary, this.pTBForm, this.DockBarManager, this.DockPanelManager);
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
            #region ITBForm
            if (this.m_pTBForm != null)
            {
                GISShare.Controls.WinForm.WFNew.View.NodeViewItem nodeViewItem1 =
                   new GISShare.Controls.WinForm.WFNew.View.NodeViewItem("快捷工具条", "快捷工具条");
                //
                foreach (GISShare.Controls.WinForm.WFNew.IBaseItem one in this.m_pTBForm.ToolbarItems)
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
            #region DockBarManager
            if (this.m_DockBarManager != null)
            {
                GISShare.Controls.WinForm.WFNew.View.NodeViewItem nodeViewItem1 =
                    new GISShare.Controls.WinForm.WFNew.View.NodeViewItem("浮动工具条管理", "浮动工具条管理");
                nodeViewItem1.IsExpanded = true;
                //
                //
                //
                if (this.m_DockBarManager.MenuBar != null)
                {
                    GISShare.Controls.WinForm.WFNew.View.NodeViewItem nodeViewItem2 =
                        new GISShare.Controls.WinForm.WFNew.View.NodeViewItem("主菜单", "主菜单");
                    foreach (System.Windows.Forms.ToolStripItem one in this.m_DockBarManager.MenuBar.Items)
                    {
                        GISShare.Controls.WinForm.WFNew.View.NodeViewItem node =
                            new GISShare.Controls.WinForm.WFNew.View.NodeViewItem(one.Name, one.Text + "（Name：" + one.Name + "）");
                        nodeViewItem2.NodeViewItems.Add(node);
                        this.UIView_DG2(node, one as System.Windows.Forms.ToolStripDropDownItem);
                    }
                    nodeViewItem1.NodeViewItems.Add(nodeViewItem2);
                }
                //
                //
                //
                if (this.m_DockBarManager.ToolBars.Count > 0)
                {
                    GISShare.Controls.WinForm.WFNew.View.NodeViewItem nodeViewItem2 =
                        new GISShare.Controls.WinForm.WFNew.View.NodeViewItem("工具条", "工具条");
                    //
                    foreach (GISShare.Controls.WinForm.DockBar.ToolBar one in this.m_DockBarManager.ToolBars)
                    {
                        GISShare.Controls.WinForm.WFNew.View.NodeViewItem nodeViewItem2_1 =
                            new GISShare.Controls.WinForm.WFNew.View.NodeViewItem(one.Name, one.Text + "（Name：" + one.Name + "）");
                        foreach (System.Windows.Forms.ToolStripItem one2 in one.Items)
                        {
                            GISShare.Controls.WinForm.WFNew.View.NodeViewItem node =
                                new GISShare.Controls.WinForm.WFNew.View.NodeViewItem(one2.Name, one2.Text + "（Name：" + one2.Name + "）");
                            nodeViewItem2_1.NodeViewItems.Add(node);
                            this.UIView_DG2(node, one2 as System.Windows.Forms.ToolStripDropDownItem);
                        }
                        nodeViewItem2.NodeViewItems.Add(nodeViewItem2_1);
                    }
                    //
                    nodeViewItem1.NodeViewItems.Add(nodeViewItem2);
                }
                //
                //
                //
                if (this.m_DockBarManager.StatusBar != null)
                {
                    GISShare.Controls.WinForm.WFNew.View.NodeViewItem nodeViewItem2 =
                        new GISShare.Controls.WinForm.WFNew.View.NodeViewItem("状态栏", "状态栏");
                    foreach (System.Windows.Forms.ToolStripItem one in this.m_DockBarManager.StatusBar.Items)
                    {
                        GISShare.Controls.WinForm.WFNew.View.NodeViewItem node =
                            new GISShare.Controls.WinForm.WFNew.View.NodeViewItem(one.Name, one.Text + "（Name：" + one.Name + "）");
                        nodeViewItem2.NodeViewItems.Add(node);
                        this.UIView_DG2(node, one as System.Windows.Forms.ToolStripDropDownItem);
                    }
                    nodeViewItem1.NodeViewItems.Add(nodeViewItem2);
                }
                //
                //
                //
                if (this.m_DockBarManager.ContextMenus.Count > 0)
                {
                    GISShare.Controls.WinForm.WFNew.View.NodeViewItem nodeViewItem2 =
                        new GISShare.Controls.WinForm.WFNew.View.NodeViewItem("快捷菜单", "快捷菜单");
                    //
                    foreach (GISShare.Controls.WinForm.DockBar.ContextMenu one in this.m_DockBarManager.ContextMenus)
                    {
                        GISShare.Controls.WinForm.WFNew.View.NodeViewItem nodeViewItem2_1 =
                            new GISShare.Controls.WinForm.WFNew.View.NodeViewItem(one.Name, one.Text + "（Name：" + one.Name + "）");
                        foreach (System.Windows.Forms.ToolStripItem one2 in one.Items)
                        {
                            GISShare.Controls.WinForm.WFNew.View.NodeViewItem node =
                                new GISShare.Controls.WinForm.WFNew.View.NodeViewItem(one2.Name, one2.Text + "（Name：" + one2.Name + "）");
                            nodeViewItem2_1.NodeViewItems.Add(node);
                            this.UIView_DG2(node, one2 as System.Windows.Forms.ToolStripDropDownItem);
                        }
                        nodeViewItem2.NodeViewItems.Add(nodeViewItem2_1);
                    }
                    //
                    nodeViewItem1.NodeViewItems.Add(nodeViewItem2);
                }
                //
                //
                //
                GISShare.Controls.WinForm.WFNew.View.NodeViewItem nodeViewItem3 =
                   new GISShare.Controls.WinForm.WFNew.View.NodeViewItem("浮动窗体（由系统管理）", "浮动窗体（由系统管理）");
                foreach (GISShare.Controls.WinForm.WFNew.IBaseItem one in this.m_DockBarManager.DockBarFloatForms_Read)
                {
                    GISShare.Controls.WinForm.WFNew.View.NodeViewItem node =
                        new GISShare.Controls.WinForm.WFNew.View.NodeViewItem(one.Name, one.Text + "（Name：" + one.Name + "）");
                    nodeViewItem3.NodeViewItems.Add(node);
                    //this.UIView_DG(node, one as GISShare.Controls.WinForm.WFNew.ICollectionItem);
                }
                nodeViewItem1.NodeViewItems.Add(nodeViewItem3);
                //
                //
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
        private void UIView_DG2(GISShare.Controls.WinForm.WFNew.View.NodeViewItem nodeViewItem, System.Windows.Forms.ToolStripDropDownItem toolStripDropDownItem)
        {
            if (nodeViewItem == null || toolStripDropDownItem == null) return;
            //
            foreach (System.Windows.Forms.ToolStripItem one in toolStripDropDownItem.DropDownItems)
            {
                GISShare.Controls.WinForm.WFNew.View.NodeViewItem node =
                    new GISShare.Controls.WinForm.WFNew.View.NodeViewItem(one.Name, one.Text + "（Name：" + one.Name + "）");
                nodeViewItem.NodeViewItems.Add(node);
                //
                this.UIView_DG2(node, one as System.Windows.Forms.ToolStripDropDownItem);
            }
        }

        //受保护
        protected virtual void OnPluginReflection(PluginReflectionEventArgs e)
        {
            if (this.PluginReflection != null) this.PluginReflection(this, e);
        }
    }
}
