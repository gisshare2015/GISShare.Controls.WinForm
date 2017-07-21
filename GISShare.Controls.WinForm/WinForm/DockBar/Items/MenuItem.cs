using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace GISShare.Controls.WinForm.DockBar
{
    [ToolboxBitmap(typeof(MenuItem), "MenuItem.bmp"),
    ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
    public class MenuItem : System.Windows.Forms.ToolStripMenuItem, IBaseItemDB, ICollectionItemDB, ICustomize, WFNew.IObjectDesignHelper, WFNew.ICollectionObjectDesignHelper
    {
        #region 构造函数
        public MenuItem()
            : base()
        {
            this.m_FlexibleToolStripItemCollection = new FlexibleToolStripItemCollection(base.DropDownItems);
            //
            base.Text = "MenuItem";
        }

        public MenuItem(Image image)
            : base(image)
        {
            this.m_FlexibleToolStripItemCollection = new FlexibleToolStripItemCollection(base.DropDownItems);
        }

        public MenuItem(string text)
            : base(text)
        {
            this.m_FlexibleToolStripItemCollection = new FlexibleToolStripItemCollection(base.DropDownItems);
        }

        public MenuItem(string text, Image image)
            : base(text, image)
        {
            this.m_FlexibleToolStripItemCollection = new FlexibleToolStripItemCollection(base.DropDownItems);
        }

        public MenuItem(string text, Image image, EventHandler onClick)
            : base(text, image, onClick)
        {
            this.m_FlexibleToolStripItemCollection = new FlexibleToolStripItemCollection(base.DropDownItems);
        }

        public MenuItem(string text, Image image, EventHandler onClick, string name)
            : base(text, image, onClick, name)
        {
            this.m_FlexibleToolStripItemCollection = new FlexibleToolStripItemCollection(base.DropDownItems);
        }

        //public MenuItem(GISShare.Controls.Plugin.WinForm.DockBar.IMenuItemP pBaseItemDBP)
        //    : base()
        //{
        //    this.m_FlexibleToolStripItemCollection = new FlexibleToolStripItemCollection(base.DropDownItems);
        //    //
        //    //
        //    //
        //    //IPlugin
        //    this.Name = pBaseItemDBP.Name;
        //    //ISetEntityObject
        //    GISShare.Controls.Plugin.ISetEntityObject pSetEntityObject = pBaseItemDBP as GISShare.Controls.Plugin.ISetEntityObject;
        //    if (pSetEntityObject != null) pSetEntityObject.SetEntityObject(this);
        //    //IBaseItemP_
        //    this.Category = pBaseItemDBP.Category;
        //    this.DisplayStyle = pBaseItemDBP.DisplayStyle;
        //    this.DoubleClickEnabled = pBaseItemDBP.DoubleClickEnabled;
        //    this.Enabled = pBaseItemDBP.Enabled;
        //    this.Font = pBaseItemDBP.Font;
        //    this.ForeColor = pBaseItemDBP.ForeColor;
        //    this.Image = pBaseItemDBP.Image;
        //    this.ImageAlign = pBaseItemDBP.ImageAlign;
        //    //this.ImageIndex = pBaseItemDBP.ImageIndex;
        //    //this.ImageKey = pBaseItemDBP.ImageKey;
        //    this.ImageScaling = pBaseItemDBP.ImageScaling;
        //    this.ImageTransparentColor = pBaseItemDBP.ImageTransparentColor;
        //    this.Margin = pBaseItemDBP.Margin;
        //    this.MergeAction = pBaseItemDBP.MergeAction;
        //    this.MergeIndex = pBaseItemDBP.MergeIndex;
        //    this.Overflow = pBaseItemDBP.Overflow;
        //    this.Padding = pBaseItemDBP.Padding;
        //    this.RightToLeft = pBaseItemDBP.RightToLeft;
        //    this.RightToLeftAutoMirrorImage = pBaseItemDBP.RightToLeftAutoMirrorImage;
        //    this.Size = pBaseItemDBP.Size;
        //    this.Text = pBaseItemDBP.Text;
        //    this.TextAlign = pBaseItemDBP.TextAlign;
        //    this.TextDirection = pBaseItemDBP.TextDirection;
        //    this.TextImageRelation = pBaseItemDBP.TextImageRelation;
        //    this.ToolTipText = pBaseItemDBP.ToolTipText;
        //    this.Visible = pBaseItemDBP.Visible;
        //    //
        //    this.Checked = pBaseItemDBP.Checked;
        //    this.CheckState = pBaseItemDBP.CheckState;
        //    this.CheckOnClick = pBaseItemDBP.CheckOnClick;
        //    this.ShortcutKeyDisplayString = pBaseItemDBP.ShortcutKeyDisplayString;
        //    this.ShortcutKeys = pBaseItemDBP.ShortcutKeys;
        //    this.ShowShortcutKeys = pBaseItemDBP.ShowShortcutKeys;
        //}
        #endregion

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Description("其所携带的子项集合"), Category("子项")]
        public new FlexibleToolStripItemCollection DropDownItems { get { return this.Items; } }

        #region WFNew.IObjectDesignHelper
        public void Refresh()
        {
            this.Invalidate(this.Bounds);
        }
        #endregion

        #region WFNew.ICollectionObjectDesignHelper
        System.Collections.IList WFNew.ICollectionObjectDesignHelper.List { get { return this.m_FlexibleToolStripItemCollection; } }

        bool WFNew.ICollectionObjectDesignHelper.ExchangeItem(object item1, object item2) { return this.m_FlexibleToolStripItemCollection.ExchangeItem(item1, item2); }
        #endregion

        #region WinForm.IFontItem
        bool m_ShowBackColor = false;
        [Browsable(false), DefaultValue(false), Description("显示自定义列表区的背景色"), Category("外观")]
        public bool ShowBackColor
        {
            get { return m_ShowBackColor; }
            set { m_ShowBackColor = value; }
        }
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

        #region IBaseItemDB
        private string m_Category = Language.LanguageStrategy.DefaultText;//"默认";
        [Browsable(true), DefaultValue("默认"), Description("该项所处的分类"), Category("属性")]
        public string Category
        {
            get { return m_Category; }
            set { m_Category = value; }
        }
        #endregion

        #region ICustomize
        FlexibleToolStripItemCollection m_FlexibleToolStripItemCollection;
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Description("其所携带的子项集合"), Category("子项")]
        public FlexibleToolStripItemCollection Items
        {
            get { return this.m_FlexibleToolStripItemCollection; }
        }

        private List<ToolStripItem> m_CustomizeBaseItems = new List<ToolStripItem>();
        List<ToolStripItem> ICustomize.CustomizeBaseItems//不要随意调用它
        {
            get { return m_CustomizeBaseItems; }
        }

        IBaseItemDB ICustomize.AddCustomizeBaseItem(int index, IBaseItemDB pBaseItem)
        {
            if (pBaseItem == null) return null;
            if (index < 0) index = 0;
            if (index > this.DropDownItems.Count) index = this.DropDownItems.Count;
            //
            ToolStripItem item = pBaseItem.Clone() as ToolStripItem;
            if (item == null) return item as IBaseItemDB;
            this.m_CustomizeBaseItems.Add(item);
            this.DropDownItems.Insert(index, item);
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
            this.DropDownItems.Insert(index, item);
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
            this.DropDownItems.Remove(item);
            item.Dispose();
        }

        void ICustomize.ClearCustomizeBaseItems()
        {
            foreach (ToolStripItem one in this.m_CustomizeBaseItems)
            {
                this.DropDownItems.Remove(one);
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
            for (int i = 0; i < this.DropDownItems.Count; i++)
            {
                this.DropDownItems[i].Visible = true;
                //
                ICustomize pCustomize = this.DropDownItems[i] as ICustomize;
                if (pCustomize == null) continue;
                pCustomize.Reset();
            }
        }
        #endregion

        #region Clone
        public virtual ToolStripItem Clone()
        {
            MenuItem item = new MenuItem();
            item.Category = this.Category;
            item.Name = this.Name + "[GUID]" + System.Guid.NewGuid().ToString();
            item.Text = this.Text;
            item.DisplayStyle = this.DisplayStyle;
            item.ImageAlign = this.ImageAlign;
            item.ImageIndex = this.ImageIndex;
            item.ImageKey = this.ImageKey;
            item.ImageScaling = this.ImageScaling;
            item.ImageTransparentColor = this.ImageTransparentColor;
            item.TextImageRelation = this.TextImageRelation;
            if (this.Image != null) item.Image = this.Image.Clone() as Image;
            item.ToolTipText = this.ToolTipText;
            item.Tag = this.Tag;
            item.BackgroundImage = this.BackgroundImage;
            item.BackgroundImageLayout = this.BackgroundImageLayout;
            item.BackColor = this.BackColor;
            item.CheckOnClick = this.CheckOnClick;
            item.DoubleClickEnabled = this.DoubleClickEnabled;
            item.DropDownClosed += new EventHandler(item_DropDownClosed);
            item.DropDownItemClicked += new ToolStripItemClickedEventHandler(item_DropDownItemClicked);
            item.DropDownOpened += new EventHandler(item_DropDownOpened);
            item.CheckedChanged += new EventHandler(item_CheckedChanged);
            item.Click += new EventHandler(item_Click);
            item.EnabledChanged += new EventHandler(item_EnabledChanged);
            item.MouseDown += new MouseEventHandler(item_MouseDown);
            //item.MouseEnter += new EventHandler(item_MouseEnter);
            //item.MouseHover += new EventHandler(item_MouseHover);
            //item.MouseLeave += new EventHandler(item_MouseLeave);
            item.MouseMove += new MouseEventHandler(item_MouseMove);
            item.MouseUp += new MouseEventHandler(item_MouseUp);
            item.VisibleChanged += new EventHandler(item_VisibleChanged);
            return item;
        }
        public virtual ToolStripItem CloneToButtonItem()
        {
            ButtonItem item = new ButtonItem();
            item.Category = this.Category;
            item.Name = this.Name + "[GUID]" + System.Guid.NewGuid().ToString();
            item.Text = this.Text;
            //item.DisplayStyle = this.DisplayStyle;
            item.ImageAlign = this.ImageAlign;
            item.ImageIndex = this.ImageIndex;
            item.ImageKey = this.ImageKey;
            item.ImageScaling = this.ImageScaling;
            item.ImageTransparentColor = this.ImageTransparentColor;
            item.TextImageRelation = this.TextImageRelation;
            if (this.Image != null) item.Image = this.Image.Clone() as Image;
            item.ToolTipText = this.ToolTipText;
            item.Tag = this.Tag;
            item.BackgroundImage = this.BackgroundImage;
            item.BackgroundImageLayout = this.BackgroundImageLayout;
            item.BackColor = this.BackColor;
            item.CheckOnClick = this.CheckOnClick;
            item.DoubleClickEnabled = this.DoubleClickEnabled;
            //item.DropDownClosed += new EventHandler(item_DropDownClosed);
            //item.DropDownItemClicked += new ToolStripItemClickedEventHandler(item_DropDownItemClicked);
            //item.DropDownOpened += new EventHandler(item_DropDownOpened);
            item.CheckedChanged += new EventHandler(item_CheckedChanged);
            item.Click += new EventHandler(item_Click);
            item.EnabledChanged += new EventHandler(item_EnabledChanged);
            item.MouseDown += new MouseEventHandler(item_MouseDown);
            //item.MouseEnter += new EventHandler(item_MouseEnter);
            //item.MouseHover += new EventHandler(item_MouseHover);
            //item.MouseLeave += new EventHandler(item_MouseLeave);
            item.MouseMove += new MouseEventHandler(item_MouseMove);
            item.MouseUp += new MouseEventHandler(item_MouseUp);
            item.VisibleChanged += new EventHandler(item_VisibleChanged);
            return item;
        }
        public virtual ToolStripItem CloneToDropDownButtonItem()
        {
            DropDownButtonItem item = new DropDownButtonItem();
            item.Category = this.Category;
            item.Name = this.Name + "[GUID]" + System.Guid.NewGuid().ToString();
            item.Text = this.Text;
            item.DisplayStyle = this.DisplayStyle;
            item.ImageAlign = this.ImageAlign;
            item.ImageIndex = this.ImageIndex;
            item.ImageKey = this.ImageKey;
            item.ImageScaling = this.ImageScaling;
            item.ImageTransparentColor = this.ImageTransparentColor;
            item.TextImageRelation = this.TextImageRelation;
            if (this.Image != null) item.Image = this.Image.Clone() as Image;
            item.ToolTipText = this.ToolTipText;
            item.Tag = this.Tag;
            item.BackgroundImage = this.BackgroundImage;
            item.BackgroundImageLayout = this.BackgroundImageLayout;
            item.BackColor = this.BackColor;
            //item.CheckOnClick = this.CheckOnClick;
            item.DoubleClickEnabled = this.DoubleClickEnabled;
            item.DropDownClosed += new EventHandler(item_DropDownClosed);
            item.DropDownItemClicked += new ToolStripItemClickedEventHandler(item_DropDownItemClicked);
            item.DropDownOpened += new EventHandler(item_DropDownOpened);
            //item.CheckedChanged += new EventHandler(item_CheckedChanged);
            item.Click += new EventHandler(item_Click);
            item.EnabledChanged += new EventHandler(item_EnabledChanged);
            item.MouseDown += new MouseEventHandler(item_MouseDown);
            //item.MouseEnter += new EventHandler(item_MouseEnter);
            //item.MouseHover += new EventHandler(item_MouseHover);
            //item.MouseLeave += new EventHandler(item_MouseLeave);
            item.MouseMove += new MouseEventHandler(item_MouseMove);
            item.MouseUp += new MouseEventHandler(item_MouseUp);
            item.VisibleChanged += new EventHandler(item_VisibleChanged);
            return item;
        }
        public virtual ToolStripItem CloneToSplitButtonItem()
        {
            SplitButtonItem item = new SplitButtonItem();
            item.Category = this.Category;
            item.Name = this.Name + "[GUID]" + System.Guid.NewGuid().ToString();
            item.Text = this.Text;
            item.DisplayStyle = this.DisplayStyle;
            item.ImageAlign = this.ImageAlign;
            item.ImageIndex = this.ImageIndex;
            item.ImageKey = this.ImageKey;
            item.ImageScaling = this.ImageScaling;
            item.ImageTransparentColor = this.ImageTransparentColor;
            item.TextImageRelation = this.TextImageRelation;
            if (this.Image != null) item.Image = this.Image.Clone() as Image;
            item.ToolTipText = this.ToolTipText;
            item.Tag = this.Tag;
            item.BackgroundImage = this.BackgroundImage;
            item.BackgroundImageLayout = this.BackgroundImageLayout;
            item.BackColor = this.BackColor;
            //item.CheckOnClick = this.CheckOnClick;
            item.DoubleClickEnabled = this.DoubleClickEnabled;
            item.DropDownClosed += new EventHandler(item_DropDownClosed);
            item.DropDownItemClicked += new ToolStripItemClickedEventHandler(item_DropDownItemClicked);
            item.DropDownOpened += new EventHandler(item_DropDownOpened);
            //item.CheckedChanged += new EventHandler(item_CheckedChanged);
            item.Click += new EventHandler(item_Click);
            item.EnabledChanged += new EventHandler(item_EnabledChanged);
            item.MouseDown += new MouseEventHandler(item_MouseDown);
            //item.MouseEnter += new EventHandler(item_MouseEnter);
            //item.MouseHover += new EventHandler(item_MouseHover);
            //item.MouseLeave += new EventHandler(item_MouseLeave);
            item.MouseMove += new MouseEventHandler(item_MouseMove);
            item.MouseUp += new MouseEventHandler(item_MouseUp);
            item.VisibleChanged += new EventHandler(item_VisibleChanged);
            return item;
        }
        void item_DropDownOpened(object sender, EventArgs e)
        {
            this.OnDropDownOpened(e);
        }
        void item_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            this.OnDropDownItemClicked(e);
        }
        void item_DropDownClosed(object sender, EventArgs e)
        {
            this.OnDropDownClosed(e);
        }
        void item_DoubleClick(object sender, EventArgs e)
        {
            this.OnDoubleClick(e);
        }
        void item_CheckedChanged(object sender, EventArgs e)
        {
            base.OnCheckedChanged(e);
        }
        void item_Click(object sender, EventArgs e)
        {
            this.OnClick(e);
        }
        void item_EnabledChanged(object sender, EventArgs e)
        {
            this.OnEnabledChanged(e);
        }
        void item_MouseUp(object sender, MouseEventArgs e)
        {
            this.OnMouseUp(e);
        }
        void item_MouseMove(object sender, MouseEventArgs e)
        {
            this.OnMouseMove(e);
        }
        void item_MouseDown(object sender, MouseEventArgs e)
        {
            this.OnMouseDown(e);
        }
        void item_VisibleChanged(object sender, EventArgs e)
        {
            this.OnVisibleChanged(e);
        }
        #endregion
    }
}
