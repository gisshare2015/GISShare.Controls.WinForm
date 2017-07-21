using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

namespace GISShare.Controls.WinForm.WFNew
{
    [DefaultEvent("ValueChanged")]
    public class RatingStarItem : BaseItem, IRatingStarItem, IRatingStarItemEvent
    {
        #region 构造函数
        public RatingStarItem() { }

        //public RatingStarItem(GISShare.Controls.Plugin.WFNew.IRatingStarItemP pBaseItemP)
        //{
        //    //IPlugin
        //    this.Name = pBaseItemP.Name;
        //    //ISetEntityObject
        //    GISShare.Controls.Plugin.ISetEntityObject pSetEntityObject = pBaseItemP as GISShare.Controls.Plugin.ISetEntityObject;
        //    if (pSetEntityObject != null) pSetEntityObject.SetEntityObject(this);
        //    //IBaseItemP_
        //    this.Checked = pBaseItemP.Checked;
        //    this.Enabled = pBaseItemP.Enabled;
        //    this.Font = pBaseItemP.Font;
        //    this.ForeColor = pBaseItemP.ForeColor;
        //    this.LockHeight = pBaseItemP.LockHeight;
        //    this.LockWith = pBaseItemP.LockWith;
        //    this.Padding = pBaseItemP.Padding;
        //    this.Size = pBaseItemP.Size;
        //    this.Text = pBaseItemP.Text;
        //    this.Visible = pBaseItemP.Visible;
        //    this.Category = pBaseItemP.Category;
        //    this.MinimumSize = pBaseItemP.MinimumSize;
        //    this.UsingViewOverflow = pBaseItemP.UsingViewOverflow;
        //    //IRatingStarItemP
        //    this.StarCount = pBaseItemP.StarCount;
        //    this.Value = pBaseItemP.Value;
        //    this.StarSize = pBaseItemP.StarSize;
        //    this.StarSpace = pBaseItemP.StarSpace;
        //}
        #endregion

        #region IRatingStarItemEvent
        public event IntValueChangedHandler ValueChanged;
        #endregion

        #region IRatingStarItem
        int m_Value = 0;
        [Browsable(true), DefaultValue(0), Description("值"), Category("属性")]
        public int Value
        {
            get { return m_Value; }
            set
            {
                if (value < 0) value = 0;
                if (value > this.StarCount) value = this.StarCount;
                IntValueChangedEventArgs e = new IntValueChangedEventArgs(this.m_Value, value);
                m_Value = value;
                this.OnValueChanged(e);
                this.Refresh();
            }
        }

        int m_StarCount = 5;
        [Browsable(true), DefaultValue(5), Description("星的个数"), Category("属性")]
        public int StarCount
        {
            get { return m_StarCount; }
            set
            {
                if (value <= 0) return;
                m_StarCount = value;
                if (this.StarCount > this.Value) this.Value = this.StarCount;
            }
        }

        int m_StarSize = 14;
        [Browsable(true), DefaultValue(14), Description("星的尺寸"), Category("属性")]
        public int StarSize
        {
            get { return m_StarSize; }
            set
            {
                if (value < 6) return;
                m_StarSize = value;
            }
        }

        int m_StarSpace = 3;
        [Browsable(true), DefaultValue(3), Description("星星的间隙"), Category("属性")]
        public int StarSpace
        {
            get { return m_StarSpace; }
            set
            {
                if (value <= 0) return;
                m_StarSpace = value;
            }
        }
        #endregion

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
            RatingStarItem baseItem = new RatingStarItem();
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
            baseItem.Value = this.Value;
            baseItem.StarCount = this.StarCount;
            baseItem.StarSize = this.StarSize;
            baseItem.StarSpace = this.StarSpace;
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

        public override Size MeasureSize(Graphics g)
        {
            if (this.StarCount <= 0) return new Size(this.Padding.Left + this.StarSize + this.Padding.Right, this.Padding.Top + this.StarSize + this.Padding.Bottom);
            //
            return new Size
                (
                this.Padding.Left + this.StarCount * this.StarSize + (this.StarCount - 1) * this.StarSpace + this.Padding.Left, 
                this.Padding.Top + this.StarSize + this.Padding.Bottom
                );
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            //
            Rectangle rectangle = this.DisplayRectangle;
            //
            for (int i = 0; i < this.StarCount; i++)
            {
                if (new Rectangle(rectangle.Left + i * (this.StarSize + this.StarSpace + 1), (rectangle.Top + rectangle.Bottom - this.StarSize) / 2, this.StarSize + 1, this.StarSize).Contains(e.Location))
                {
                    if (this.Value == i + 1)
                    {
                        this.Value--;
                    }
                    else
                    {
                        this.Value = i + 1;
                    }
                    break;
                }
            }
        }

        protected override void OnDraw(PaintEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderStar(
                new GISShare.Controls.WinForm.ObjectRenderEventArgs(e.Graphics, this, this.DisplayRectangle));
        }

        //
        //
        //

        protected virtual void OnValueChanged(IntValueChangedEventArgs e)
        {
            if (this.ValueChanged != null) this.ValueChanged(this, e);
        }
    }
}
