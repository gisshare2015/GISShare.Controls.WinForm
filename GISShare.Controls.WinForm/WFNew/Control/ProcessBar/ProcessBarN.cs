using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    [ToolboxItem(true)]
    public class ProcessBarN : BaseItemControl, IProcessBarItem, IProcessBarItemEvent
    {
        public override bool LockWith
        {
            get { return false; }
        }

        public override bool LockHeight
        {
            get { return false; }
        }

        protected override EventStateStyle GetEventStateSupplement(string strEventName)
        {
            switch (strEventName)
            {
                case "ValueChanged":
                    return this.ValueChanged != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                default:
                    break;
            }
            //
            return base.GetEventStateSupplement(strEventName);
        }

        protected override bool RelationEventSupplement(string strEventName, EventArgs e)
        {
            switch (strEventName)
            {
                case "ValueChanged":
                    if (this.ValueChanged != null) { this.ValueChanged(this, e as IntValueChangedEventArgs); }
                    return true;
                default:
                    break;
            }
            //
            return base.RelationEventSupplement(strEventName, e);
        }

        #region Clone
        public override object Clone()
        {
            ProcessBarN baseItem = new ProcessBarN();
            baseItem.Checked = this.Checked;
            baseItem.Enabled = this.Enabled;
            baseItem.Font = this.Font;
            baseItem.ForeColor = this.ForeColor;
            baseItem.Name = this.Name;
            baseItem.Site = this.Site;
            baseItem.Size = this.Size;
            baseItem.Tag = this.Tag;
            baseItem.Text = this.Text;
            baseItem.Visible = this.Visible;
            //
            baseItem.eOrientation = this.eOrientation;
            baseItem.LeftBottomRadius = this.LeftBottomRadius;
            baseItem.LeftTopRadius = this.LeftTopRadius;
            baseItem.RightBottomRadius = this.RightBottomRadius;
            baseItem.RightTopRadius = this.RightTopRadius;
            baseItem.Value = this.Value;
            baseItem.Minimum = this.Minimum;
            baseItem.Maximum = this.Maximum;
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
            if (this.GetEventState("ValueChanged") == EventStateStyle.eUsed) baseItem.ValueChanged += new IntValueChangedHandler(baseItem_ValueChanged);
            return baseItem;
        }
        void baseItem_ValueChanged(object sender, IntValueChangedEventArgs e)
        {
            this.RelationEvent("ValueChanged", e);
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

        #region IProcessBarItemEvent
        public event IntValueChangedHandler ValueChanged;
        #endregion

        #region IProcessBarItem
        System.Windows.Forms.Orientation m_eOrientation = System.Windows.Forms.Orientation.Horizontal;
        [Browsable(true), DefaultValue(typeof(Orientation), "Horizontal"), Description("进度条的布局方式"), Category("布局")]
        public System.Windows.Forms.Orientation eOrientation
        {
            get { return m_eOrientation; }
            set { m_eOrientation = value; }
        }

        #region Radius
        private int m_LeftTopRadius = 6;
        [Browsable(true), DefaultValue(6), Description("左顶部圆角值"), Category("圆角")]
        public virtual int LeftTopRadius
        {
            get { return m_LeftTopRadius; }
            set
            {
                if (value < 0) return;
                //
                m_LeftTopRadius = value;
            }
        }

        private int m_RightTopRadius = 6;
        [Browsable(true), DefaultValue(6), Description("右顶部圆角值"), Category("圆角")]
        public virtual int RightTopRadius
        {
            get { return m_RightTopRadius; }
            set
            {
                if (value < 0) return;
                //
                m_RightTopRadius = value;
            }
        }

        private int m_LeftBottomRadius = 6;
        [Browsable(true), DefaultValue(6), Description("左底部圆角值"), Category("圆角")]
        public virtual int LeftBottomRadius
        {
            get { return m_LeftBottomRadius; }
            set
            {
                if (value < 0) return;
                //
                m_LeftBottomRadius = value;
            }
        }

        private int m_RightBottomRadius = 6;
        [Browsable(true), DefaultValue(6), Description("右底部圆角值"), Category("圆角")]
        public virtual int RightBottomRadius
        {
            get { return m_RightBottomRadius; }
            set
            {
                if (value < 0) return;
                //
                m_RightBottomRadius = value;
            }
        }
        #endregion

        int m_Value = 0;
        [Browsable(true), DefaultValue(0), Description("值"), Category("属性")]
        public int Value
        {
            get { return m_Value; }
            set
            {
                if (value < Minimum) value = Minimum;
                else if (value > Maximum) value = Maximum;
                if (m_Value == value) return;
                IntValueChangedEventArgs e = new IntValueChangedEventArgs(m_Value, value);
                m_Value = value;
                this.OnValueChanged(e);
                this.Invalidate(this.FrameRectangle);
            }
        }

        int m_Minimum = 0;
        [Browsable(true), DefaultValue(0), Description("最小值"), Category("属性")]
        public int Minimum
        {
            get { return m_Minimum; }
            set
            {
                if (value > Maximum) value = Maximum - 1;
                m_Minimum = value;
            }
        }

        int m_Maximum = 100;
        [Browsable(true), DefaultValue(100), Description("最大值"), Category("属性")]
        public int Maximum
        {
            get { return m_Maximum; }
            set 
            {
                if (value < Minimum) value = Minimum + 1;
                m_Maximum = value; 
            }
        }

        [Browsable(false), Description("当前百分比"), Category("属性")]
        public int Percentage
        {
            get
            {
                if (this.Maximum == this.Minimum) return 100; 
                return this.Value * 100 / (this.Maximum - this.Minimum);
            }
        }

        [Browsable(false), Description("框架矩形"), Category("布局")]
        public Rectangle FrameRectangle
        {
            get 
            {
                Rectangle rectangle = this.DisplayRectangle;
                return Rectangle.FromLTRB(rectangle.Left + this.Padding.Left, 
                    rectangle.Top + this.Padding.Top,
                    rectangle.Right - this.Padding.Right - 1,
                    rectangle.Bottom - this.Padding.Bottom - 1); 
            }
        }
        #endregion

        protected override void OnPaint(PaintEventArgs pevent)
        {
            this.OnDraw(pevent);
            //
            base.OnPaint(pevent);
        }

        protected virtual void OnDraw(PaintEventArgs e)
        {
            WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderProcessBar(new ObjectRenderEventArgs(e.Graphics, this, this.DisplayRectangle));
        }

        //
        protected virtual void OnValueChanged(IntValueChangedEventArgs e)
        {
            if (this.ValueChanged != null) { this.ValueChanged(this, e); }
        }
    }
}
