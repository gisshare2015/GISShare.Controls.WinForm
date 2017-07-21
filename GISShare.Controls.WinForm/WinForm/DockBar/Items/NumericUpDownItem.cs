using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.DockBar
{
    [ToolboxBitmap(typeof(NumericUpDownItem), "NumericUpDownItem.bmp"),
    ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
    public class NumericUpDownItem : ToolStripControlHost, IBaseItemDB, WFNew.IObjectDesignHelper
    {
        public event EventHandler ValueChanged;

        #region 构造函数
        public NumericUpDownItem()
            : base(CreateControlInstance()) 
        {
            base.Size = new Size(100, 15);
            base.Text = "NumericUpDownItem";
            this.Image = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("GISShare.Controls.WinForm.WinForm.DockBar.NumericUpDownItem.bmp"));
            //
            NumericUpDown numericUpDown = this.NumericUpDownControl;
            if (numericUpDown != null) { numericUpDown.ValueChanged += new EventHandler(NumericUpDown_ValueChanged); }
        }
        private static Control CreateControlInstance()
        {
            ToolStripNumericUpDownControl numericUpDown = new ToolStripNumericUpDownControl();
            //
            return numericUpDown;
        }
        private void NumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            this.OnValueChanged(e);
        }

        //public NumericUpDownItem(GISShare.Controls.Plugin.WinForm.DockBar.INumericUpDownItemP pBaseItemDBP)
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
        //    this.Maximum = pBaseItemDBP.Maximum;
        //    this.Minimum = pBaseItemDBP.Minimum;
        //    this.Value = pBaseItemDBP.Value;
        //    this.Increment = pBaseItemDBP.Increment;
        //}
        #endregion
        public NumericUpDown NumericUpDownControl
        {
            get { return base.Control as NumericUpDown; }
        }

        public decimal Maximum
        {
            get
            {
                NumericUpDown numericUpDown = this.Control as NumericUpDown;
                if (numericUpDown != null) return numericUpDown.Maximum;
                return int.MaxValue;
            }
            set
            {
                NumericUpDown numericUpDown = this.Control as NumericUpDown;
                if (numericUpDown != null) numericUpDown.Maximum = value;
            }
        }

        public decimal Minimum
        {
            get
            {
                NumericUpDown numericUpDown = this.Control as NumericUpDown;
                if (numericUpDown != null) return numericUpDown.Minimum;
                return int.MinValue;
            }
            set
            {
                NumericUpDown numericUpDown = this.Control as NumericUpDown;
                if (numericUpDown != null) numericUpDown.Minimum = value;
            }
        }

        public decimal Value
        {
            get
            {
                NumericUpDown numericUpDown = this.Control as NumericUpDown;
                if (numericUpDown != null) return numericUpDown.Value;
                return -1;
            }
            set
            {
                NumericUpDown numericUpDown = this.Control as NumericUpDown;
                if (numericUpDown != null) numericUpDown.Value = value;
            }
        }

        public decimal Increment
        {
            get
            {
                NumericUpDown numericUpDown = this.Control as NumericUpDown;
                if (numericUpDown != null) return numericUpDown.Increment;
                return 1;
            }
            set
            {
                NumericUpDown numericUpDown = this.Control as NumericUpDown;
                if (numericUpDown != null) numericUpDown.Increment = value;
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
            NumericUpDownItem item = new NumericUpDownItem();
            item.NumericUpDownControl.Name = this.NumericUpDownControl.Name;
            item.NumericUpDownControl.Text = this.NumericUpDownControl.Text;
            item.NumericUpDownControl.Tag = this.NumericUpDownControl.Tag;
            item.NumericUpDownControl.Value = this.NumericUpDownControl.Value;
            item.NumericUpDownControl.Maximum = this.NumericUpDownControl.Maximum;
            item.NumericUpDownControl.Minimum = this.NumericUpDownControl.Minimum;
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
            item.ValueChanged += new EventHandler(item_ValueChanged);
            item.EnabledChanged += new EventHandler(item_EnabledChanged);
            item.VisibleChanged += new EventHandler(item_VisibleChanged);
            item.KeyDown += new KeyEventHandler(item_KeyDown);
            item.KeyPress += new KeyPressEventHandler(item_KeyPress);
            item.KeyUp += new KeyEventHandler(item_KeyUp);
            return item;
        }
        void item_ValueChanged(object sender, EventArgs e)
        {
            this.OnValueChanged(e);
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
        protected virtual void OnValueChanged(EventArgs e)
        {
            if (this.ValueChanged != null) { this.ValueChanged(this, e); }
        }

        //
        //
        //

        class ToolStripNumericUpDownControl : NumericUpDown, ITextBoxItemDB
        {
            UpDownButtonsSkinHelper m_UpDownButtonsSkinHelper;
            //
            public ToolStripNumericUpDownControl()
                : base()
            {
                this.m_UpDownButtonsSkinHelper = new UpDownButtonsSkinHelper(this.Controls[0]);
                //
                Control control = this.Controls[0] as Control;
                if (control != null)
                {
                    control.MouseDown += new MouseEventHandler(Control_MouseDown);
                    control.MouseUp += new MouseEventHandler(Control_MouseUp);
                    control.MouseEnter += new EventHandler(Control_MouseEnter);
                    control.MouseLeave += new EventHandler(Control_MouseLeave);
                }
                control = this.Controls[1] as Control;
                if (control != null) 
                {
                    control.MouseDown += new MouseEventHandler(Control_MouseDown);
                    control.MouseUp += new MouseEventHandler(Control_MouseUp);
                    control.MouseEnter += new EventHandler(Control_MouseEnter);
                    control.MouseLeave += new EventHandler(Control_MouseLeave);
                }
            }
            void Control_MouseLeave(object sender, EventArgs e)
            {
                this.SetBaseItemStateEx(WFNew.BaseItemState.eNormal);
            }
            void Control_MouseEnter(object sender, EventArgs e)
            {
                this.SetBaseItemStateEx(WFNew.BaseItemState.eHot);
            }
            void Control_MouseUp(object sender, MouseEventArgs e)
            {
                if (this.DisplayRectangle.Contains(e.Location)) { this.SetBaseItemStateEx(WFNew.BaseItemState.eHot); }
                else { this.SetBaseItemStateEx(WFNew.BaseItemState.eNormal); }
            }
            void Control_MouseDown(object sender, MouseEventArgs e)
            {
                this.SetBaseItemStateEx(this.ContainsFocus ? WFNew.BaseItemState.eHot : GISShare.Controls.WinForm.WFNew.BaseItemState.ePressed);
            }

            #region WFNew.IBaseItem
            WFNew.RenderStyle m_eRenderStyle = WFNew.RenderStyle.eSystem;
            [Browsable(true), DefaultValue(typeof(WFNew.RenderStyle), "eSystem"), Description("渲染类型"), Category("外观")]
            public virtual WFNew.RenderStyle eRenderStyle
            {
                get { return m_eRenderStyle; }
                set { m_eRenderStyle = value; }
            }

            private WFNew.BaseItemState m_eBaseItemState = WFNew.BaseItemState.eNormal;
            protected virtual bool SetBaseItemState(WFNew.BaseItemState baseItemState)
            {
                if (this.m_eBaseItemState == baseItemState) return false;
                this.m_eBaseItemState = baseItemState;
                return true;
            }
            protected virtual bool SetBaseItemStateEx(WFNew.BaseItemState baseItemState)
            {
                if (this.m_eBaseItemState == baseItemState) return false;
                this.m_eBaseItemState = baseItemState;
                this.Refresh();
                return true;
            }
            [Browsable(false), Description("自身所处的状态（激活、按下、不可用、正常）"), Category("状态")]
            public virtual WFNew.BaseItemState eBaseItemState
            {
                get
                {
                    return m_eBaseItemState;
                }
            }
            #endregion

            #region IBaseTextBoxItem
            [Browsable(false), Description("其外框矩形"), Category("布局")]
            public Rectangle FrameRectangle
            {
                get
                {
                    return new Rectangle(0, 0, this.Width - 1, this.Height - 1);
                }
            }
            #endregion

            protected override void OnMouseDown(MouseEventArgs mevent)
            {
                this.SetBaseItemStateEx(this.ContainsFocus ? WFNew.BaseItemState.eHot : GISShare.Controls.WinForm.WFNew.BaseItemState.ePressed);
                base.OnMouseDown(mevent);
            }

            protected override void OnMouseUp(MouseEventArgs mevent)
            {
                if (this.DisplayRectangle.Contains(mevent.Location)) { this.SetBaseItemStateEx(WFNew.BaseItemState.eHot); }
                else { this.SetBaseItemStateEx(WFNew.BaseItemState.eNormal); }
                base.OnMouseUp(mevent);
            }

            protected override void OnMouseLeave(EventArgs e)
            {
                this.SetBaseItemStateEx(WFNew.BaseItemState.eNormal);
                base.OnMouseLeave(e);
            }

            protected override void OnMouseEnter(EventArgs e)
            {
                this.SetBaseItemStateEx(WFNew.BaseItemState.eHot);
                base.OnMouseEnter(e);
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);
                //
                this.OnDraw(e);
            }

            protected virtual void OnDraw(PaintEventArgs e)
            {
                GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderTextBoxC
                    (
                    new ObjectRenderEventArgs(e.Graphics, this, this.DisplayRectangle)
                    );
            }
        }
    }
}
