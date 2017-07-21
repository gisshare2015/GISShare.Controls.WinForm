using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.DockBar
{
    [ToolboxBitmap(typeof(RadioButtonItem), "RadioButtonItem.bmp"),
    ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
    public class RadioButtonItem : ToolStripControlHost, IBaseItemDB, WFNew.IObjectDesignHelper
    {
        public event EventHandler CheckedChanged;

        #region 构造函数
        public RadioButtonItem()
            : base(CreateControlInstance())
        {
            //base.Name = "RadioButtonItem";
            base.Size = new Size(100, 15);
            base.Text = "RadioButtonItem";
            this.Image = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("GISShare.Controls.WinForm.WinForm.DockBar.RadioButtonItem.bmp"));
            //
            RadioButton radioButton = this.RadioButtonControl;
            if (radioButton != null) { radioButton.CheckedChanged += new EventHandler(RadioButton_CheckedChanged); }
        }
        private static Control CreateControlInstance()
        {
            ToolStripRadioButtonControl radioButton = new ToolStripRadioButtonControl();
            //
            return radioButton;
        }
        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            this.OnCheckedChanged(e);
        }

        //public RadioButtonItem(GISShare.Controls.Plugin.WinForm.DockBar.IRadioButtonItemP pBaseItemDBP)
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
        //    this.VOffset = pBaseItemDBP.VOffset;
        //    this.Checked = pBaseItemDBP.Checked;
        //}
        #endregion

        public RadioButton RadioButtonControl
        { get { return base.Control as RadioButton; } }

        public bool Checked
        {
            get
            {
                RadioButton radioButton = this.Control as RadioButton;
                if (radioButton != null) return radioButton.Checked;
                return false;
            }
            set
            {
                RadioButton radioButton = this.Control as RadioButton;
                if (radioButton != null) radioButton.Checked = value;
            }
        }

        public int VOffset
        {
            get
            {
                ToolStripRadioButtonControl radioButton = this.Control as ToolStripRadioButtonControl;
                if (radioButton != null) return radioButton.VOffset;
                return -1;
            }
            set
            {
                ToolStripRadioButtonControl radioButton = this.Control as ToolStripRadioButtonControl;
                if (radioButton != null) radioButton.VOffset = value;
            }
        }

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

        #region Clone
        public virtual ToolStripItem Clone()
        {
            RadioButtonItem item = new RadioButtonItem();
            item.RadioButtonControl.Name = this.RadioButtonControl.Name;
            item.RadioButtonControl.Text = this.RadioButtonControl.Text;
            item.RadioButtonControl.Tag = this.RadioButtonControl.Tag;
            item.RadioButtonControl.Checked = this.RadioButtonControl.Checked;
            //
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
            item.DoubleClickEnabled = this.DoubleClickEnabled;
            item.CheckedChanged += new EventHandler(item_CheckedChanged);
            item.EnabledChanged += new EventHandler(item_EnabledChanged);
            item.VisibleChanged += new EventHandler(item_VisibleChanged);
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
        void item_CheckedChanged(object sender, EventArgs e)
        {
            this.OnCheckedChanged(e);
        }
        void item_EnabledChanged(object sender, EventArgs e)
        {
            this.OnEnabledChanged(e);
        }
        void item_VisibleChanged(object sender, EventArgs e)
        {
            this.OnVisibleChanged(e);
        }
        #endregion

        //事件
        protected virtual void OnCheckedChanged(EventArgs e)
        {
            if (this.CheckedChanged != null) { this.CheckedChanged(this, e); }
        }

        //
        //
        //

        class ToolStripRadioButtonControl : RadioButtonX
        {
            public ToolStripRadioButtonControl()
                : base()
            {
                base.BackColor = System.Drawing.Color.Transparent;
                base.VOffset = 3;
            }
        }
    }
}
