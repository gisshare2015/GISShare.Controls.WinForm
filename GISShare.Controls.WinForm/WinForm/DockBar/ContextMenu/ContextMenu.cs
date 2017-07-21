using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace GISShare.Controls.WinForm.DockBar
{
    [ToolboxItem(false), Designer(typeof(ContextMenuDesigner)), ToolboxBitmap(typeof(ContextMenu), "ContextMenu.bmp")]
    public class ContextMenu : ContextMenuStrip, ICollectionItemDB, ICustomize, WFNew.IRecordItem, WFNew.ISetRecordItemHelper, WFNew.IObjectDesignHelper, WFNew.ICollectionObjectDesignHelper, WFNew.IPopupObjectDesignHelper
    {
        #region 构造函数
        public ContextMenu() 
            : base()
        {
            this.m_FlexibleToolStripItemCollection = new FlexibleToolStripItemCollection(base.Items);
            //
            base.RenderMode = ToolStripRenderMode.ManagerRenderMode;
        }

        //public ContextMenu(GISShare.Controls.Plugin.WinForm.DockBar.IContextMenuP pContextMenuP)
        //    : this()
        //{
        //    //IPlugin
        //    this.Name = pContextMenuP.Name;
        //    //ISetEntityObject
        //    GISShare.Controls.Plugin.ISetEntityObject pSetEntityObject = pContextMenuP as GISShare.Controls.Plugin.ISetEntityObject;
        //    if (pSetEntityObject != null) pSetEntityObject.SetEntityObject(this);
        //    //IContextMenuP
        //    this.Text = pContextMenuP.Text;
        //    this.Enabled = pContextMenuP.Enabled;
        //    this.Visible = pContextMenuP.Visible;
        //    this.DropShadowEnabled = pContextMenuP.DropShadowEnabled;
        //}
        #endregion

        #region WFNew.ICollectionObjectDesignHelper
        System.Collections.IList WFNew.ICollectionObjectDesignHelper.List { get { return this.m_FlexibleToolStripItemCollection; } }

        bool WFNew.ICollectionObjectDesignHelper.ExchangeItem(object item1, object item2) { return this.m_FlexibleToolStripItemCollection.ExchangeItem(item1, item2); }
        #endregion

        #region WFNew.IPopupObjectDesignHelper
        void WFNew.IPopupObjectDesignHelper.ShowPopup() 
        {
            this.Show(3, 3);
        }

        void WFNew.IPopupObjectDesignHelper.ClosePopup()
        {
            this.Close();
        }
        #endregion

        #region WFNew.IRecordItem
        private int m_RecordID = 0;
        [Browsable(false), Description("自身的ID号（由系统管理，主要在记录布局文件时使用，常规状态下它是无效的）"), Category("属性")]
        public int RecordID
        {
            get { return m_RecordID; }
        }
        #endregion

        #region WFNew.ISetRecordItemHelper
        void WFNew.ISetRecordItemHelper.SetRecordID(int id)//设置RecordID，由系统管理（在保存布局时设置该属性）
        {
            this.m_RecordID = id;
        }
        #endregion

        #region ICollectionItemDB
        public ToolStripItem GetItem(string strName)
        {
            ToolStripItem toolStripItem = null;
            foreach(ToolStripItem one in this.Items)
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
                if (toolStripItem != null) break;
            }
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
                if (toolStripItem != null) break;
            }
            return toolStripItem;
        }
        #endregion

        #region ICustomize
        FlexibleToolStripItemCollection m_FlexibleToolStripItemCollection;
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Description("其所携带的子项集合"), Category("子项")]
        public new FlexibleToolStripItemCollection Items
        {
            get { return this.m_FlexibleToolStripItemCollection; }
        }

        private List<ToolStripItem> m_CustomizeBaseItems = new List<ToolStripItem>();
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Description("自定义子项集合"), Category("子项")]
        List<ToolStripItem> ICustomize.CustomizeBaseItems//不要随意调用它
        {
            get { return m_CustomizeBaseItems; }
        }

        IBaseItemDB ICustomize.AddCustomizeBaseItem(int index, IBaseItemDB pBaseItem)
        {
            if (pBaseItem == null) return null;
            if (index < 0) index = 0;
            if (index > this.Items.Count) index = this.Items.Count;
            //
            ToolStripItem item = pBaseItem.Clone() as ToolStripItem;
            if (item == null) return item as IBaseItemDB;
            this.m_CustomizeBaseItems.Add(item);
            this.Items.Insert(index, item);
            //
            return item as IBaseItemDB;
        }

        IBaseItemDB ICustomize.AddCustomizeBaseItemEx(int index, IBaseItemDB pBaseItem)
        {
            if (pBaseItem == null) return null;
            if (index < 0) index = 0;
            if (index > this.Items.Count) index = this.Items.Count;
            //
            ToolStripItem item = null;
            ButtonItem buttonItem = pBaseItem as ButtonItem;
            SplitButtonItem splitButtonItem = pBaseItem as SplitButtonItem;
            DropDownButtonItem dropDownButtonItem = pBaseItem as DropDownButtonItem;
            if (buttonItem != null) { item = buttonItem.CloneToMenuItem() as ToolStripItem; }
            else if (splitButtonItem != null) { item = splitButtonItem.CloneToMenuItem() as ToolStripItem; }
            else if (dropDownButtonItem != null) { item = dropDownButtonItem.CloneToMenuItem() as ToolStripItem; }
            else { item = pBaseItem.Clone() as ToolStripItem; }
            if (item == null) return item as IBaseItemDB;
            this.m_CustomizeBaseItems.Add(item);
            this.Items.Insert(index, item);
            //
            return item as IBaseItemDB;
        }

        void ICustomize.RemoveCustomizeBaseItem(IBaseItemDB pBaseItem)
        {
            if (pBaseItem == null) return;
            //
            ToolStripItem item = pBaseItem as ToolStripItem;
            if (item == null) return;
            this.m_CustomizeBaseItems.Remove(item);
            this.Items.Remove(item);
            item.Dispose();
        }

        void ICustomize.ClearCustomizeBaseItems()
        {
            foreach (ToolStripItem one in this.m_CustomizeBaseItems)
            {
                this.Items.Remove(one);
            }
            // 
            foreach (ToolStripItem one in this.m_CustomizeBaseItems)
            {
                one.Dispose();
            }
            this.m_CustomizeBaseItems.Clear();
        }

        public void Reset()
        {
            ((ICustomize)this).ClearCustomizeBaseItems();
            //
            for (int i = 0; i < this.Items.Count; i++)
            {
                this.Items[i].Visible = true;
                //
                ICustomize pCustomize = this.Items[i] as ICustomize;
                if (pCustomize == null) continue;
                pCustomize.Reset();
            }
        }
        #endregion

        [Browsable(false), DefaultValue(ToolStripRenderMode.ManagerRenderMode)]
        public new ToolStripRenderMode RenderMode
        {
            get { return base.RenderMode; }
            set { base.RenderMode = value; }
        }

        internal void SetShowItemToolTips(bool bShowItemToolTips)
        {
            base.ShowItemToolTips = bShowItemToolTips;
        }

        internal void SetImageScalingSize(Size imageScalingSize)
        {
            base.ImageScalingSize = imageScalingSize;
        }

        [Browsable(false)]
        public new bool ShowItemToolTips
        {
            get { return base.ShowItemToolTips; }
            set { }
        }

        [Browsable(false)]
        public new Size ImageScalingSize
        {
            get { return base.ImageScalingSize; }
            set { }
        }
    }
}
