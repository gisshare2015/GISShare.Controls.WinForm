using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    [DefaultEvent("CheckedChanged"), ToolboxItem(true)]
    public class CheckBoxN : BaseItemControl, ICheckBoxItem
    {
        private const int CRT_CHECKSIZE = 12;
        private const int CRT_CHECKSPACE = 1;

        #region ICheckBoxItem
        [Browsable(false), Description("勾选区绘制矩形框"), Category("布局")]
        public virtual Rectangle CheckRectangle
        {
            get
            {
                Rectangle rectangle = this.DisplayRectangle;
                return new Rectangle
                    (
                    rectangle.X + this.Padding.Left,
                    rectangle.Y + this.Padding.Top + (rectangle.Height - this.Padding.Top - this.Padding.Bottom - CRT_CHECKSIZE) / 2,
                    CRT_CHECKSIZE,
                    CRT_CHECKSIZE
                    );
            }
        }

        bool m_InsideCheckRectangleTrigger = false;
        /// <summary>
        /// 鼠标在复选框内弹起方才改变（Checked属性）
        /// </summary>
        [Browsable(true), DefaultValue(false), Description("鼠标在复选框内弹起方才改变（Checked属性）"), Category("属性")]
        public bool InsideCheckRectangleTrigger
        {
            get { return m_InsideCheckRectangleTrigger; }
            set { m_InsideCheckRectangleTrigger = value; }
        }

        private ContentAlignment m_TextAlign = ContentAlignment.MiddleLeft;
        [Browsable(true), DefaultValue(typeof(ContentAlignment), "MiddleLeft"), Description("文本布局的方式"), Category("外观")]
        public virtual ContentAlignment TextAlign//BH
        {
            get
            {
                return m_TextAlign;
            }
            set { m_TextAlign = value; }
        }

        private CheckState m_CheckState = CheckState.Unchecked;
        [Browsable(true), DefaultValue(typeof(CheckState), "Unchecked"), Description("选中的状态"), Category("状态")]
        public CheckState CheckState
        {
            get
            {
                return m_CheckState;
            }
            set
            {
                if (m_CheckState == value) return;
                //
                m_CheckState = value;
                if (value == CheckState.Checked && !this.Checked) this.Checked = true;
                else if ((value == CheckState.Unchecked || value == CheckState.Indeterminate) && this.Checked) this.Checked = false;
            }
        }

        private Rectangle m_TextRectangle;
        [Browsable(false), Description("文本绘制矩形框"), Category("布局")]
        public virtual Rectangle TextRectangle
        {
            get
            {
                Rectangle rectangle = m_TextRectangle;
                Rectangle displayRectangle = this.DisplayRectangle;
                //
                int iLeftEx = this.CheckRectangle.Right + CRT_CHECKSPACE;
                //
                int iTop = rectangle.Top;
                int iLeft = rectangle.Left;
                int iRight = rectangle.Right;
                int iBottom = rectangle.Bottom;
                if (displayRectangle.Top > iTop) iTop = displayRectangle.Top;
                if (iLeftEx > iLeft) iLeft = iLeftEx;
                if (displayRectangle.Right < iRight) iRight = displayRectangle.Right;
                if (displayRectangle.Bottom < iBottom) iBottom = displayRectangle.Bottom;
                return Rectangle.FromLTRB(iLeft, iTop, iRight, iBottom);
            }
        }
        #endregion

        protected override bool RefreshBaseItemState
        {
            get
            {
                return true;
            }
        }

        public override bool Checked
        {
            get
            {
                return base.Checked;
            }
            set
            {
                base.Checked = value;
                if (value && this.CheckState != CheckState.Checked) this.CheckState = CheckState.Checked;
                else if (!value && this.CheckState == CheckState.Checked) this.CheckState = CheckState.Unchecked;
            }
        }

        public override bool LockHeight
        {
            get
            {
                return false;
            }
        }

        public override bool LockWith
        {
            get
            {
                return false;
            }
        }

        #region Clone
        public override object Clone()
        {
            CheckBoxN baseItem = new CheckBoxN();
            baseItem.Checked = this.Checked;
            baseItem.Enabled = this.Enabled;
            baseItem.Font = this.Font;
            baseItem.ForeColor = this.ForeColor;
            baseItem.Name = this.Name;
            baseItem.Site = this.Site;
            baseItem.Size = this.Size;
            baseItem.Tag = this.Tag;
            baseItem.Text = this.Text;
            baseItem.Padding = this.Padding;
            baseItem.TextAlign = this.TextAlign;
            baseItem.Visible = this.Visible;
            //
            baseItem.CheckState = this.CheckState;
            if (this.GetEventState("VisibleChanged") == EventStateStyle.eUsed) baseItem.VisibleChanged += new EventHandler(baseItem_VisibleChanged);
            if (this.GetEventState("SizeChanged") == EventStateStyle.eUsed) baseItem.SizeChanged += new EventHandler(baseItem_SizeChanged);
            if (this.GetEventState("Paint") == EventStateStyle.eUsed) baseItem.Paint += new PaintEventHandler(baseItem_Paint);
            if (this.GetEventState("MouseUp") == EventStateStyle.eUsed) baseItem.MouseUp += new MouseEventHandler(baseItem_MouseUp);
            if (this.GetEventState("MouseMove") == EventStateStyle.eUsed) baseItem.MouseMove += new MouseEventHandler(baseItem_MouseMove);
            if (this.GetEventState("MouseLeave") == EventStateStyle.eUsed) baseItem.MouseLeave += new EventHandler(baseItem_MouseLeave);
            if (this.GetEventState("MouseEnter") == EventStateStyle.eUsed) baseItem.MouseEnter += new EventHandler(baseItem_MouseEnter);
            if (this.GetEventState("MouseDown") == EventStateStyle.eUsed) baseItem.MouseDown += new MouseEventHandler(baseItem_MouseDown);
            if (this.GetEventState("MouseDoubleClick") == EventStateStyle.eUsed) baseItem.MouseDoubleClick += new MouseEventHandler(baseItem_MouseDoubleClick);
            if (this.GetEventState("MouseClick") == EventStateStyle.eUsed) baseItem.MouseClick += new MouseEventHandler(baseItem_MouseClick);
            if (this.GetEventState("LocationChanged") == EventStateStyle.eUsed) baseItem.LocationChanged += new EventHandler(baseItem_LocationChanged);
            if (this.GetEventState("EnabledChanged") == EventStateStyle.eUsed) baseItem.EnabledChanged += new EventHandler(baseItem_EnabledChanged);
            if (this.GetEventState("CheckedChanged") == EventStateStyle.eUsed) baseItem.CheckedChanged += new EventHandler(baseItem_CheckedChanged);
            return baseItem;
        }
        void baseItem_CheckedChanged(object sender, EventArgs e)
        {
            this.RelationEvent("CheckedChanged", e);
        }
        void baseItem_EnabledChanged(object sender, EventArgs e)
        {
            this.RelationEvent("EnabledChanged", e);
        }
        void baseItem_LocationChanged(object sender, EventArgs e)
        {
            this.RelationEvent("LocationChanged", e);
        }
        void baseItem_MouseClick(object sender, MouseEventArgs e)
        {
            this.RelationEvent("MouseClick", e);
        }
        void baseItem_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.RelationEvent("MouseDoubleClick", e);
        }
        void baseItem_MouseDown(object sender, MouseEventArgs e)
        {
            this.RelationEvent("MouseDown", e);
        }
        void baseItem_MouseEnter(object sender, EventArgs e)
        {
            this.RelationEvent("MouseEnter", e);
        }
        void baseItem_MouseLeave(object sender, EventArgs e)
        {
            this.RelationEvent("MouseLeave", e);
        }
        void baseItem_MouseMove(object sender, MouseEventArgs e)
        {
            this.RelationEvent("MouseMove", e);
        }
        void baseItem_MouseUp(object sender, MouseEventArgs e)
        {
            this.RelationEvent("MouseUp", e);
        }
        void baseItem_Paint(object sender, PaintEventArgs e)
        {
            this.RelationEvent("Paint", e);
        }
        void baseItem_SizeChanged(object sender, EventArgs e)
        {
            this.RelationEvent("SizeChanged", e);
        }
        void baseItem_VisibleChanged(object sender, EventArgs e)
        {
            this.RelationEvent("VisibleChanged", e);
        }
        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            this.m_TextRectangle = this.GetTextRectangle(e.Graphics);
            //
            this.OnDraw(e);
            //
            base.OnPaint(e);
        }
        protected Rectangle GetTextRectangle(Graphics g)
        {
            Rectangle rectangle = this.DisplayRectangle;
            SizeF size = g.MeasureString(this.Text, this.Font);
            int iWidth = (int)(size.Width + 1);
            int iHeight = (int)(size.Height + 1);
            return new Rectangle(rectangle.Left + CRT_CHECKSIZE + CRT_CHECKSPACE,
                        (rectangle.Top + rectangle.Bottom - iHeight) / 2,
                        iWidth,
                        iHeight);
        }

        protected virtual void OnDraw(PaintEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderCheckBox(
                new GISShare.Controls.WinForm.ObjectRenderEventArgs(e.Graphics, this, this.DisplayRectangle));
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonText(
                new GISShare.Controls.WinForm.TextRenderEventArgs(e.Graphics, this, this.Enabled, this.Text, this.ForeColor, this.Font, this.TextRectangle));
        }

        protected override void OnCheckedChanged(EventArgs e)
        {
            this.Refresh();
            base.OnCheckedChanged(e);
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            if (mevent.Button == MouseButtons.Left)
            {
                if (!this.InsideCheckRectangleTrigger || this.CheckRectangle.Contains(mevent.Location))
                {
                    this.Checked = !this.Checked;
                }
            }
            base.OnMouseUp(mevent);
        }
    }
}
