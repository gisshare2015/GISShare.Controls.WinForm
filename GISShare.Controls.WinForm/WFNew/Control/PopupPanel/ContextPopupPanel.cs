using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    [ToolboxItem(true)]
    public class ContextPopupPanel : BaseItemStackEx, IContextPopupPanelItem
    {
        #region IPopupPanel
        public void TrySetPopupPanelSize(Size size)
        {
            this.Size = size;
        }
        #endregion

        #region IContextPopupPanelItem
        private int m_CheckRegionWidth = 23;
        [Browsable(true), DefaultValue(23), Description("勾选区尺寸"), Category("布局")]
        public int CheckRegionWidth
        {
            get { return m_CheckRegionWidth; }
            set
            {
                if (value < 16) return;
                m_CheckRegionWidth = value; 
            }
        }

        private int m_ImageRegionWidth = 23;
        [Browsable(true), DefaultValue(23), Description("图片区尺寸"), Category("布局")]
        public int ImageRegionWidth
        {
            get { return m_ImageRegionWidth; }
            set
            {
                if (value < 16) return;
                m_ImageRegionWidth = value;
            }
        }

        private ContextPopupStyle m_ContextPopupStyle = ContextPopupStyle.eNormal;
        [Browsable(true), DefaultValue(typeof(ContextPopupStyle), "eNormal"), Description("Popup面板展现方式"), Category("外观")]
        public ContextPopupStyle eContextPopupStyle
        {
            get { return m_ContextPopupStyle; }
            set { m_ContextPopupStyle = value; }
        }

        [Browsable(false), Description("勾选、图片、实体绘制矩形框"), Category("布局")]
        public Rectangle CIEDrawRectangle
        {
            get 
            { 
                return this.ItemsRectangle; 
            }
        }

        [Browsable(false), Description("勾选列表区域"), Category("布局")]
        public Rectangle CheckRectangle
        {
            get
            {
                switch (this.eContextPopupStyle)
                {
                    case ContextPopupStyle.eSuper:
                        Rectangle rectangle = this.CIEDrawRectangle;
                        return new Rectangle(rectangle.X, rectangle.Y, this.CheckRegionWidth, rectangle.Height);
                    default:
                        return new Rectangle();
                }
            }
        }

        [Browsable(false), Description("图片列表区域"), Category("布局")]
        public Rectangle ImageRectangle
        {
            get
            {
                Rectangle rectangle = this.CIEDrawRectangle;
                switch (this.eContextPopupStyle)
                {
                    case ContextPopupStyle.eNormal:
                        return new Rectangle(rectangle.X, rectangle.Y, this.ImageRegionWidth, rectangle.Height);
                    case ContextPopupStyle.eSuper:
                        return new Rectangle(rectangle.X + this.CheckRegionWidth, rectangle.Y, this.ImageRegionWidth, rectangle.Height);
                    default:
                        return new Rectangle();
                }
            }
        }

        [Browsable(false), Description("实体区域"), Category("布局")]
        public Rectangle EntityRectangle
        {
            get
            {
                Rectangle rectangle = this.CIEDrawRectangle;
                switch (this.eContextPopupStyle)
                {
                    case ContextPopupStyle.eNormal:
                        return new Rectangle(rectangle.X + this.ImageRegionWidth, rectangle.Y, rectangle.Width - this.ImageRegionWidth, rectangle.Height);
                    case ContextPopupStyle.eSuper:
                        return new Rectangle(rectangle.X + this.CheckRegionWidth + ImageRegionWidth, rectangle.Y, rectangle.Width - this.CheckRegionWidth - this.ImageRegionWidth, rectangle.Height);
                    case ContextPopupStyle.eSimply:
                    default:
                        return rectangle;
                }
            }
        }
        #endregion

        [Browsable(false)]
        public override Orientation eOrientation
        {
            get
            {
                return Orientation.Vertical;
            }
            set
            {
                base.eOrientation = Orientation.Vertical;
            }
        }

        public override int RestrictItemsHeight
        {
            get
            {
                switch (this.eContextPopupStyle)
                {
                    case ContextPopupStyle.eSuper:
                        return this.CheckRegionWidth > this.ImageRegionWidth ? this.CheckRegionWidth : this.ImageRegionWidth;
                    case ContextPopupStyle.eNormal:
                        return this.ImageRegionWidth;
                    case ContextPopupStyle.eSimply:
                    default:
                        return base.RestrictItemsHeight;
                }
            }
            set
            {
                base.RestrictItemsHeight = value;
            }
        }

        #region Clone
         public override object Clone()
         {
             ContextPopupPanel baseItem = new ContextPopupPanel();
             baseItem.Checked = this.Checked;
             baseItem.Enabled = this.Enabled;
             baseItem.Font = this.Font;
             baseItem.ForeColor = this.ForeColor;
             baseItem.Name = this.Name;
             baseItem.Site = this.Site;
             baseItem.Size = this.Size;
             baseItem.Tag = this.Tag;
             baseItem.Text = this.Text;
             baseItem.LeftBottomRadius = this.LeftBottomRadius;
             baseItem.LeftTopRadius = this.LeftTopRadius;
             baseItem.Padding = this.Padding;
             baseItem.RightBottomRadius = this.RightBottomRadius;
             baseItem.RightTopRadius = this.RightTopRadius;
             baseItem.Visible = this.Visible;
             //
             baseItem.MinSize = this.MinSize;
             baseItem.MaxSize = this.MaxSize;
             baseItem.IsStretchItems = this.IsStretchItems;
             baseItem.IsRestrictItems = this.IsRestrictItems;
             baseItem.LineDistance = this.LineDistance;
             baseItem.ColumnDistance = this.ColumnDistance;
             baseItem.eOrientation = this.eOrientation;
             //
             //baseItem.ShowNomalOutLineState = this.ShowNomalOutLineState;
             //baseItem.ShowNomalBackgroudState = this.ShowNomalBackgroudState;
             //baseItem.ShowOutLineState = this.ShowOutLineState;
             //baseItem.ShowBackgroudState = this.ShowBackgroudState;
             //baseItem.AutoTopViewItemIndex = this.AutoTopViewItemIndex;
             baseItem.PreButtonIncreaseIndex = this.PreButtonIncreaseIndex;
             //
             baseItem.ShowBackgroud = this.ShowBackgroud;
             baseItem.ShowOutLine = this.ShowOutLine;
             foreach (BaseItem one in this.BaseItems)
             {
                 baseItem.BaseItems.Add(one.Clone() as BaseItem);
             }
             baseItem.TopViewItemIndex = this.TopViewItemIndex;
             //
             baseItem.CheckRegionWidth = this.CheckRegionWidth;
             baseItem.ImageRegionWidth = this.ImageRegionWidth;
             baseItem.eContextPopupStyle = this.eContextPopupStyle;
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
             if (this.GetEventState("TopViewItemIndexChanged") == EventStateStyle.eUsed) baseItem.TopViewItemIndexChanged += new IntValueChangedHandler(baseItem_TopViewItemIndexChanged);
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
         void baseItem_TopViewItemIndexChanged(object sender, IntValueChangedEventArgs e)
         {
             this.RelationEvent("TopViewItemIndexChanged", e);
         }
         #endregion

        protected override void OnDraw(PaintEventArgs e)
        {
            base.Relayout(e.Graphics, LayoutStyle.eLayoutPlan, true);
            base.Relayout(e.Graphics, LayoutStyle.eLayoutAuto, false);
            //
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderContextPopupPanel(
                new GISShare.Controls.WinForm.ObjectRenderEventArgs(e.Graphics, this, this.DisplayRectangle));
        }
    }
}
