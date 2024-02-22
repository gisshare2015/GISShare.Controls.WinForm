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
    [ToolboxBitmap(typeof(TextBoxItem), "TextBoxItem.bmp"),
    ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
    public class TextBoxItem : ToolStripControlHost, IBaseItemDB, WFNew.IObjectDesignHelper
    {
        public new event EventHandler TextChanged;

        #region 构造函数
        public TextBoxItem()
            : base(CreateControlInstance()) 
        {
            base.AutoSize = false;
            base.Size = new Size(100, 15);
            base.Text = "TextBoxItem";
            this.Image = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("GISShare.Controls.WinForm.WinForm.DockBar.MaskedTextBoxItem.bmp"));
            //
            TextBox textBox = this.TextBoxControl;
            if (textBox != null) { textBox.TextChanged += new EventHandler(TextBox_TextChanged); }
        }
        void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.OnTextChanged(e);
        }        
        private static Control CreateControlInstance()
        {
            ToolStripTextBoxControl textBox = new ToolStripTextBoxControl();
            textBox.Dock = DockStyle.Fill;
            //
            return textBox;
        }

        //public TextBoxItem(GISShare.Controls.Plugin.WinForm.DockBar.ITextBoxItemP pBaseItemDBP)
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
        //    this.PasswordChar = pBaseItemDBP.PasswordChar;
        //}
        #endregion

        public TextBox TextBoxControl
        {
            get { return base.Control as TextBox; }
        }

        public char PasswordChar
        {
            get
            {
                TextBox textBox = this.Control as TextBox;
                if (textBox != null) return textBox.PasswordChar;
                return '\0';
            }
            set
            {
                TextBox textBox = this.Control as TextBox;
                if (textBox != null) textBox.PasswordChar = value;
            }
        }

        public override string Text
        {
            get
            {
                TextBox textBox = this.Control as TextBox;
                if (textBox != null) return textBox.Text;
                return base.Text;
            }
            set
            {
                TextBox textBox = this.Control as TextBox;
                if (textBox != null)
                {
                    textBox.Text = value;
                }
                else
                {
                    base.Text = value;
                }
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
            TextBoxItem item = new TextBoxItem();
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
            item.Click += new EventHandler(item_Click);
            item.DoubleClick += new EventHandler(item_DoubleClick);
            item.EnabledChanged += new EventHandler(item_EnabledChanged);
            item.MouseDown += new MouseEventHandler(item_MouseDown);
            //item.MouseEnter += new EventHandler(item_MouseEnter);
            //item.MouseHover += new EventHandler(item_MouseHover);
            //item.MouseLeave += new EventHandler(item_MouseLeave);
            item.MouseMove += new MouseEventHandler(item_MouseMove);
            item.MouseUp += new MouseEventHandler(item_MouseUp);
            item.TextChanged += new EventHandler(item_TextChanged);
            item.VisibleChanged += new EventHandler(item_VisibleChanged);
            item.KeyDown += new KeyEventHandler(item_KeyDown);
            item.KeyPress += new KeyPressEventHandler(item_KeyPress);
            item.KeyUp += new KeyEventHandler(item_KeyUp);
            return item;
        }
        void item_TextChanged(object sender, EventArgs e)
        {
            this.OnTextChanged(e);
        }
        void item_DoubleClick(object sender, EventArgs e)
        {
            this.OnDoubleClick(e);
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

        //事件
        protected virtual void OnTextChanged(MaskInputRejectedEventArgs e)
        {
            if (this.TextChanged != null) { this.TextChanged(this, e); }
        }

        //
        //
        //

        class ToolStripTextBoxControl : TextBox, ITextBoxItemDB
        {
            ToolStripControlHost m_Owner;

            public ToolStripTextBoxControl()
                : base() { }

            public void SetOwner(ToolStripControlHost owner)
            {
                this.m_Owner = owner;
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

            private bool m_HaveShadow = true;
            [Browsable(true), DefaultValue(true), Description("是否有字体阴影"), Category("状态")]
            public bool HaveShadow
            {
                get { return m_HaveShadow; }
                set { m_HaveShadow = value; }
            }

            private Color m_ShadowColor = System.Drawing.SystemColors.ControlText;
            [Browsable(true), DefaultValue(typeof(Color), "System.Drawing.SystemColors.ControlText"), Description("字体阴影颜色"), Category("外观")]
            public Color ShadowColor
            {
                get { return m_ShadowColor; }
                set { m_ShadowColor = value; }
            }

            private bool m_ForeCustomize = false;
            [Browsable(true), DefaultValue(false), Description("自定义文本色"), Category("状态")]
            public bool ForeCustomize
            {
                get { return m_ForeCustomize; }
                set { m_ForeCustomize = value; }
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

            [Browsable(false)]
            public override bool Multiline
            {
                get
                {
                    return false;
                }
                set
                {
                    base.Multiline = value;
                }
            }

            protected override void OnMouseDown(MouseEventArgs mevent)
            {
                this.SetBaseItemState(this.ContainsFocus ? WFNew.BaseItemState.eHot : GISShare.Controls.WinForm.WFNew.BaseItemState.ePressed);
                base.OnMouseDown(mevent);
            }

            protected override void OnMouseUp(MouseEventArgs mevent)
            {
                if (this.DisplayRectangle.Contains(mevent.Location)) { this.SetBaseItemState(WFNew.BaseItemState.eHot); }
                else { this.SetBaseItemState(WFNew.BaseItemState.eNormal); }
                base.OnMouseUp(mevent);
            }

            protected override void OnMouseLeave(EventArgs e)
            {
                this.SetBaseItemState(WFNew.BaseItemState.eNormal);
                base.OnMouseLeave(e);
            }

            protected override void OnMouseEnter(EventArgs e)
            {
                this.SetBaseItemState(WFNew.BaseItemState.eHot);
                base.OnMouseEnter(e);
            }

            protected override void WndProc(ref Message m)
            {
                switch (m.Msg)
                {
                    case (int)GISShare.Win32.Msgs.WM_NCPAINT:
                        //System.Diagnostics.Debug.WriteLine(String.Format("{0}----{1}", this.Name, "WM_NCPAINT"));
                        base.WndProc(ref m);
                        if (this.BorderStyle != BorderStyle.None) this.WmNCPaint(ref m);
                        return;

                }
                //
                base.WndProc(ref m);
            }
            private void WmNCPaint(ref Message m)
            {
                IntPtr iHandle = GISShare.Win32.API.GetWindowDC(m.HWnd);
                try
                {
                    Graphics g = Graphics.FromHdc(iHandle);
                    //
                    this.OnNCPaint(new PaintEventArgs(g, this.DisplayRectangle));
                    //
                    g.Dispose();
                }
                catch { }
                finally
                {
                    GISShare.Win32.API.ReleaseDC(m.HWnd, iHandle);
                }
            }

            protected virtual void OnNCPaint(PaintEventArgs e)
            {
                this.OnNCDraw(e);
            }

            protected virtual void OnNCDraw(PaintEventArgs e)
            {
                GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderTextBoxC
                    (
                    new ObjectRenderEventArgs(e.Graphics, this, this.DisplayRectangle)
                    );
            }
        }

    }
}
