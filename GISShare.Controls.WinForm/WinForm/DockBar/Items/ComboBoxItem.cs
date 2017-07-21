using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.DockBar
{
    [ToolboxBitmap(typeof(ComboBoxItem), "ComboBoxItem.bmp"),
    ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
    public class ComboBoxItem : System.Windows.Forms.ToolStripComboBox, IBaseItemDB, WFNew.IObjectDesignHelper
    {
        #region 构造函数
        public ComboBoxItem()
            : base()
        {
            base.Text = "ComboBoxItem";
        }

        //public ComboBoxItem(GISShare.Controls.Plugin.WinForm.DockBar.IComboBoxItemP pBaseItemDBP)
        //    : this()
        //{
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
        //    this.DropDownHeight = pBaseItemDBP.DropDownHeight;
        //    this.DropDownWidth = pBaseItemDBP.DropDownWidth;
        //    this.DropDownStyle = pBaseItemDBP.DropDownStyle;
        //    if (pBaseItemDBP.Items != null)
        //    {
        //        foreach (object one in pBaseItemDBP.Items)
        //        {
        //            this.Items.Add(one);
        //        }
        //    }
        //    this.SelectedIndex = pBaseItemDBP.SelectedIndex;
        //}
        #endregion

        #region WFNew.IObjectDesignHelper
        public void Refresh()
        {
            this.Invalidate(this.Bounds);
        }
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

        #region IBaseItemDB
        private string m_Category = Language.LanguageStrategy.DefaultText;//"默认";
        [Browsable(true), DefaultValue("默认"), Description("该项所处的分类"), Category("属性")]
        public string Category
        {
            get { return m_Category; }
            set { m_Category = value; }
        }
        #endregion

        public virtual ToolStripItem Clone()
        {
            ComboBoxItem item = new ComboBoxItem();
            foreach (object one in this.Items)
            {
                item.Items.Add(one);
            }
            //
            item.Category = this.Category;
            item.Name = this.Name + "[GUID]" + System.Guid.NewGuid().ToString();
            item.Text = this.Text;
            item.DisplayStyle = this.DisplayStyle;
            item.DropDownStyle = this.DropDownStyle;
            item.DropDownHeight = this.DropDownHeight;
            item.DropDownWidth = this.DropDownWidth;
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
            item.DoubleClickEnabled = this.DoubleClickEnabled;
            item.Click += new EventHandler(item_Click);
            item.EnabledChanged += new EventHandler(item_EnabledChanged);
            item.MouseDown += new MouseEventHandler(item_MouseDown);
            //item.MouseEnter += new EventHandler(item_MouseEnter);
            //item.MouseHover += new EventHandler(item_MouseHover);
            //item.MouseLeave += new EventHandler(item_MouseLeave);
            item.MouseMove += new MouseEventHandler(item_MouseMove);
            item.MouseUp += new MouseEventHandler(item_MouseUp);
            item.VisibleChanged += new EventHandler(item_VisibleChanged);
            item.SelectedIndexChanged += new EventHandler(item_SelectedIndexChanged);
            item.DropDown += new EventHandler(item_DropDown);
            item.DropDownClosed += new EventHandler(item_DropDownClosed);
            item.KeyDown += new KeyEventHandler(item_KeyDown);
            item.KeyPress += new KeyPressEventHandler(item_KeyPress);
            item.KeyUp += new KeyEventHandler(item_KeyUp);
            return item;
        }
        void item_KeyUp(object sender, KeyEventArgs e)
        {
            this.OnKeyUp(e);
        }
        void item_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.OnKeyPress(e);
        }
        void item_KeyDown(object sender, KeyEventArgs e)
        {
            this.OnKeyDown(e);
        }
        void item_DropDownClosed(object sender, EventArgs e)
        {
            this.OnDropDownClosed(e);
        }
        void item_DropDown(object sender, EventArgs e)
        {
            this.OnDropDown(e);
        }
        void item_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.OnSelectedIndexChanged(e);
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
        //void item_MouseLeave(object sender, EventArgs e)
        //{
        //    this.OnMouseLeave(e);
        //}
        //void item_MouseHover(object sender, EventArgs e)
        //{
        //    this.OnMouseHover(e);
        //}
        //void item_MouseEnter(object sender, EventArgs e)
        //{
        //    this.OnMouseEnter(e);
        //}
        void item_MouseDown(object sender, MouseEventArgs e)
        {
            this.OnMouseDown(e);
        }
        void item_VisibleChanged(object sender, EventArgs e)
        {
            this.OnVisibleChanged(e);
        }
    }
}
