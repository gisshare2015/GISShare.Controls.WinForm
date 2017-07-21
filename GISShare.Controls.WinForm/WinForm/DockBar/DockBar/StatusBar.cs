using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.DockBar
{
    [ToolboxItem(false), Designer(typeof(StatusBarDesigner)), ToolboxBitmap(typeof(StatusBar), "DockBar.bmp")]
    public class StatusBar : System.Windows.Forms.StatusStrip, IDockBar, ICollectionItemDB, ICustomize, IDock, WFNew.IRecordItem, WFNew.ISetRecordItemHelper, ISetDockBarManagerHelper, WFNew.IObjectDesignHelper, WFNew.ICollectionObjectDesignHelper
    {
        private const int CRT_MINWIDTH = 50;

        #region 构造函数
        public StatusBar()
            : base()
        {
            this.m_FlexibleToolStripItemCollection = new FlexibleToolStripItemCollection(base.Items);
            //
            base.GripStyle = ToolStripGripStyle.Hidden;
            base.Dock = DockStyle.None;
            base.RenderMode = ToolStripRenderMode.ManagerRenderMode;
        }

        //public StatusBar(GISShare.Controls.Plugin.WinForm.DockBar.IStatusBarP pDockBarP)
        //    : this()
        //{
        //    //IPlugin
        //    this.Name = pDockBarP.Name;
        //    //ISetEntityObject
        //    GISShare.Controls.Plugin.ISetEntityObject pSetEntityObject = pDockBarP as GISShare.Controls.Plugin.ISetEntityObject;
        //    if (pSetEntityObject != null) pSetEntityObject.SetEntityObject(this);
        //    //IDockBarP_
        //    this.CanFloat = pDockBarP.CanFloat;
        //    this.CanOverflow = pDockBarP.CanOverflow;
        //    this.DockBarFloatFormLocation = pDockBarP.DockBarFloatFormLocation;
        //    this.DockBarFloatFormSize = pDockBarP.DockBarFloatFormSize;
        //    this.eDockBarGripStyle = pDockBarP.eDockBarGripStyle;
        //    this.GripMargin = pDockBarP.GripMargin;
        //    this.GripStyle = pDockBarP.GripStyle;
        //    this.Image = pDockBarP.Image;
        //    this.Location = pDockBarP.Location;
        //    this.Text = pDockBarP.Text;
        //    this.Visible = pDockBarP.Visible;
        //}
        #endregion

        #region WFNew.ICollectionObjectDesignHelper
        System.Collections.IList WFNew.ICollectionObjectDesignHelper.List { get { return this.m_FlexibleToolStripItemCollection; } }

        bool WFNew.ICollectionObjectDesignHelper.ExchangeItem(object item1, object item2) { return this.m_FlexibleToolStripItemCollection.ExchangeItem(item1, item2); }
        #endregion

        #region ICollectionItemDB
        public ToolStripItem GetItem(string strName)
        {
            ToolStripItem toolStripItem = null;
            foreach (ToolStripItem one in this.Items)
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
            MenuItem menuItem = pBaseItem as MenuItem;
            if (menuItem != null) { item = menuItem.CloneToButtonItem() as ToolStripItem; }
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

        #region IDock
        public void ToDockArea(DockStyle eDockStyle)
        {
            switch (eDockStyle)
            {
                case DockStyle.Top:
                    this.RemoveFromParent();
                    this.DockBarManager.DockBarDockAreaTop.Join(this);
                    break;
                case DockStyle.Right:
                    this.RemoveFromParent();
                    this.DockBarManager.DockBarDockAreaRight.Join(this);
                    break;
                case DockStyle.Left:
                    this.RemoveFromParent();
                    this.DockBarManager.DockBarDockAreaLeft.Join(this);
                    break;
                case DockStyle.Bottom:
                    this.RemoveFromParent();
                    this.DockBarManager.DockBarDockAreaBottom.Join(this);
                    break;
            }
        }

        public void ToDockArea(DockStyle eDockStyle, int row)
        {
            switch (eDockStyle)
            {
                case DockStyle.Top:
                    this.RemoveFromParent();
                    this.DockBarManager.DockBarDockAreaTop.Join(this, row);
                    break;
                case DockStyle.Right:
                    this.RemoveFromParent();
                    this.DockBarManager.DockBarDockAreaRight.Join(this, row);
                    break;
                case DockStyle.Left:
                    this.RemoveFromParent();
                    this.DockBarManager.DockBarDockAreaLeft.Join(this, row);
                    break;
                case DockStyle.Bottom:
                    this.RemoveFromParent();
                    this.DockBarManager.DockBarDockAreaBottom.Join(this, row);
                    break;
            }
        }

        public void ToDockArea(DockStyle eDockStyle, Point location)
        {
            switch (eDockStyle)
            {
                case DockStyle.Top:
                    this.RemoveFromParent();
                    this.DockBarManager.DockBarDockAreaTop.Join(this, location);
                    break;
                case DockStyle.Right:
                    this.RemoveFromParent();
                    this.DockBarManager.DockBarDockAreaRight.Join(this, location);
                    break;
                case DockStyle.Left:
                    this.RemoveFromParent();
                    this.DockBarManager.DockBarDockAreaLeft.Join(this, location);
                    break;
                case DockStyle.Bottom:
                    this.RemoveFromParent();
                    this.DockBarManager.DockBarDockAreaBottom.Join(this, location);
                    break;
            }
        }

        public void ToDockArea(DockStyle eDockStyle, int row, Point location)
        {
            switch (eDockStyle)
            {
                case DockStyle.Top:
                    this.RemoveFromParent();
                    this.DockBarManager.DockBarDockAreaTop.Join(this, row);
                    this.Location = location;
                    break;
                case DockStyle.Right:
                    this.RemoveFromParent();
                    this.DockBarManager.DockBarDockAreaRight.Join(this, row);
                    this.Location = location;
                    break;
                case DockStyle.Left:
                    this.RemoveFromParent();
                    this.DockBarManager.DockBarDockAreaLeft.Join(this, row);
                    this.Location = location;
                    break;
                case DockStyle.Bottom:
                    this.RemoveFromParent();
                    this.DockBarManager.DockBarDockAreaBottom.Join(this, row);
                    this.Location = location;
                    break;
            }
        }
        #endregion

        #region IRecordItem
        private int m_RecordID = 0;
        [Browsable(false), Description("自身的ID号（由系统管理，主要在记录布局文件时使用，常规状态下它是无效的）"), Category("属性")]
        public int RecordID
        {
            get { return m_RecordID; }
        }
        #endregion

        #region IDockBar
        [Browsable(true), Description("VisibleEx设置前触发"), Category("属性更改前")]
        public event BoolValueChangedEventHandler BeforeVisibleExValueSeted; //VisibleEx设置前触发
        [Browsable(true), Description("VisibleEx设置后触发"), Category("属性已更改")]
        public event BoolValueChangedEventHandler AfterVisibleExValueSeted;  //VisibleEx设置后触发

        [Browsable(false), DefaultValue(true), Description("获取最大宽度"), Category("布局")]
        public int MaxWidth
        {
            get
            {
                int iMaxWidth = this.Padding.Left + this.Padding.Right;
                foreach (ToolStripItem one in this.Items)
                {
                    if (!one.Visible/* || one is AddOrRemoveDropDownItem*/) continue;
                    //
                    iMaxWidth += one.Margin.Left + one.Width + one.Margin.Right;
                }
                //
                return iMaxWidth < CRT_MINWIDTH ? CRT_MINWIDTH : iMaxWidth;
            }
        }

        [Browsable(false), DefaultValue(true), Description("所在停靠区的行数"), Category("布局")]
        public int LineMaxWidth
        {
            get
            {
                int iLeft = this.Padding.Left + 2;
                int iMaxWidth = 0;
                int iTemp = 0;
                foreach (ToolStripItem one in this.Items)
                {
                    if (!one.Visible/* || one is AddOrRemoveDropDownItem*/) continue;
                    //
                    if (one.Bounds.Left <= iLeft)
                    {
                        if (iTemp > iMaxWidth) iMaxWidth = iTemp;
                        iTemp = 0;
                    }
                    iTemp += one.Margin.Left + one.Width + one.Margin.Right;
                }
                //
                if (iMaxWidth == 0) { iMaxWidth = iTemp; }
                //
                iMaxWidth += this.Padding.Left + this.Padding.Right;
                //
                return iMaxWidth < CRT_MINWIDTH ? CRT_MINWIDTH : iMaxWidth;
            }
        }

        private Image m_Image = null;
        [Browsable(true), Description("图片"), Category("外观")]
        public Image Image
        {
            get { return m_Image; }
            set { m_Image = value; }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Description("设置该控件的可视状态，用来替代Visible属性（请不要使用Visible属性）"), Category("状态")]
        public bool VisibleEx//设置该控件的可视状态，用来替代Visible属性（请不要使用Visible属性）
        {
            get
            {
                if (this.Parent == null) { return false; }
                else { return base.Visible; }
            }
            set
            {
                bool bOldValue = this.VisibleEx;
                this.OnBeforeVisibleExValueSeted(new BoolValueChangedEventArgs(bOldValue, value));
                base.Visible = value;
                if (value)
                {
                    if (this.Parent == null) { this.ToDockBarFloatForm(); }
                }
                this.OnAfterVisibleExValueSeted(new BoolValueChangedEventArgs(bOldValue, value));
            }
        }

        private bool m_CanFloat = true;                                     //是否可以浮动
        [Browsable(true), DefaultValue(true), Description("是否可以浮动"), Category("状态")]
        public bool CanFloat
        {
            get { return m_CanFloat; }
            set { m_CanFloat = value; }
        }

        [Browsable(false), Description("绘制更多的箭头"), Category("状态")]
        public bool DrawMoreArrow
        {
            get
            {
                return false;
            }
        }

        private Point m_DockBarFloatFormLocation = new Point(150, 150);     //
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Description("记录浮动工具条浮动的位置"), Category("布局")]
        public Point DockBarFloatFormLocation
        {
            get { return m_DockBarFloatFormLocation; }
            set { m_DockBarFloatFormLocation = value; }
        }

        private Size m_DockBarFloatFormSize = new Size(-1, -1);
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Description("记录浮动工具条浮动的尺寸"), Category("布局")]
        public Size DockBarFloatFormSize
        {
            get { return m_DockBarFloatFormSize; }
            set { m_DockBarFloatFormSize = value; }
        }

        //[Browsable(false)]
        //public string Describe
        //{ get { return "MenuBar"; } }

        [Browsable(false), Description("工具条类型"), Category("属性")]
        public DockBarStyle eDockBarStyle
        { get { return DockBarStyle.eStatusBar; } }

        [Browsable(false), Description("所在停靠区类型"), Category("属性")]
        public DockAreaStyle eDockAreaStyle
        {
            get
            {
                IDockArea pDockArea = this.Parent as IDockArea;
                if (pDockArea == null) return DockAreaStyle.eNone;
                return pDockArea.eDockAreaStyle;
            }
        }

        [Browsable(false), Description("所在停靠区"), Category("属性")]
        public DockBarDockArea DockBarDockArea
        { get { return this.Parent as DockBarDockArea; } }

        private DockBarManager m_DockBarManager = null;
        [Browsable(false), Description("与自身关联的DockBarManager"), Category("关联")]
        public DockBarManager DockBarManager
        {
            get { return m_DockBarManager; }
        }

        private DockBarGripStyles m_eDockBarGripStyle = DockBarGripStyles.Default;
        [Browsable(true), DefaultValue(DockBarGripStyles.Default), Description("工具条手柄类型"), Category("布局")]
        public DockBarGripStyles eDockBarGripStyle
        {
            get { return m_eDockBarGripStyle; }
            set { m_eDockBarGripStyle = value; }
        }

        public void RemoveFromParent()
        {
            if (this.Parent == null) return;
            this.Parent.Controls.Remove(this);
        }

        public bool ToDockBarFloatForm()
        {
            DockBarFloatForm DockBarFloatForm1 = this.DockBarManager.GetEmptyDockBarFloatForm();
            DockBarFloatForm1.Show(this);
            return true;
        }

        public bool ToDockBarFloatForm(Point location)
        {
            DockBarFloatForm DockBarFloatForm1 = this.DockBarManager.GetEmptyDockBarFloatForm();
            DockBarFloatForm1.Show(this, location);
            return true;
        }
        #endregion

        #region WFNew.ISetRecordItemHelper
        void WFNew.ISetRecordItemHelper.SetRecordID(int id)//设置RecordID，由系统管理（在保存布局时设置该属性）
        {
            this.m_RecordID = id;
        }
        #endregion

        internal bool ToDockBarFloatForm(DockBarFloatForm dockBarFloatForm)
        {
            dockBarFloatForm.Show(this);
            return true;
        }

        internal void SetShowItemToolTips(bool bShowItemToolTips)
        {
            base.ShowItemToolTips = bShowItemToolTips;
        }

        internal void SetImageScalingSize(Size imageScalingSize)
        {
            base.ImageScalingSize = imageScalingSize;
            this.Height += 1;
        }
        
        #region ISetDockBarManagerHelper
        void ISetDockBarManagerHelper.SetDockBarManager(DockBarManager dockBarManager)//设置DockBarManager，由系统管理（添加到DockBarCollection时，调用该函数）
        {
            this.m_DockBarManager = dockBarManager;
        }
        #endregion

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
        
        [Browsable(false), DefaultValue(ToolStripRenderMode.ManagerRenderMode)]
        public new ToolStripRenderMode RenderMode
        {
            get { return base.RenderMode; }
            set { base.RenderMode = value; }
        }

        [Browsable(true), DefaultValue(DockStyle.None)]
        public override DockStyle Dock
        {
            get
            {
                return base.Dock;
            }
            set
            {
                base.Dock = value;
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.DockBarManager.ShowDockBarListMenuStrip(this, e.Location);
            }
            //
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs mea)
        {
            if (this.CanFloat &&
                //this.Cursor == Cursors.SizeAll &&
                this.DockBarDockArea != null)
            {
                Point point = this.DockBarDockArea.PointToClient(MousePosition);
                if (!this.DockBarDockArea.MaxBounds.Contains(point))
                {
                    this.ToDockBarFloatForm(MousePosition);
                    return;
                }
            }
            //
            base.OnMouseMove(mea);
        }

        //事件
        protected virtual void OnBeforeVisibleExValueSeted(BoolValueChangedEventArgs e)
        { if (this.BeforeVisibleExValueSeted != null) this.BeforeVisibleExValueSeted(this, e); }

        protected virtual void OnAfterVisibleExValueSeted(BoolValueChangedEventArgs e)
        { if (this.AfterVisibleExValueSeted != null) this.AfterVisibleExValueSeted(this, e); }
    }
}
