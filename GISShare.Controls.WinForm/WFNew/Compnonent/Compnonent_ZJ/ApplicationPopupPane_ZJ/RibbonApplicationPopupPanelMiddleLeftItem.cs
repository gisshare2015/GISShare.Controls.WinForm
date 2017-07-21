using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    class RibbonApplicationPopupPanelMiddleLeftItem : BaseItemStackExItem, IViewDepend, IBaseItemProperty
    {
        const int CONST_MINWIDTH = 136;
        IRibbonApplicationPopupPanelItem m_Owner;

        public RibbonApplicationPopupPanelMiddleLeftItem(IRibbonApplicationPopupPanelItem pRibbonApplicationPopupPanelItem)
        {
            base.Name = "GISShare.Controls.WinForm.WFNew.RibbonApplicationPopupPanelMiddleLeftItem";
            base.Text = "菜单栏";
            base.UsingViewOverflow = false;
            
            //
            this.m_Owner = pRibbonApplicationPopupPanelItem;
        }

        #region IBaseItemProperty
        [Browsable(false), Description("自身所属状态"), Category("属性")]
        BaseItemStyle IBaseItemProperty.eBaseItemStyle
        {
            get { return BaseItemStyle.eComponentBaseItem; }
        }

        [Browsable(false), Description("获取其依附项（如果，为独立项依附项为其自身）"), Category("关联")]
        IBaseItem3 IBaseItemProperty.DependObject
        {
            get { return this.m_Owner; }
        }
        #endregion

        ViewDependStyle IViewDepend.eViewDependStyle
        {
            get
            {
                return ViewDependStyle.eInOwnerDisplayRectangle;
            }
        }

        public Point AnchorPoint { get { return this.m_Owner.AnchorPoint; } }

        public Rectangle AnchorRectangle { get { return this.m_Owner.AnchorRectangle; } }

        public override Padding Padding
        {
            get
            {
                return new Padding(2, 1, 1, 1);
            }
            set
            {
                base.Padding = new Padding(2, 1, 1, 1);
            }
        }

        public override int LineDistance
        {
            get
            {
                return 0;
            }
            set
            {
                base.LineDistance = 0;
            }
        }

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

        public override bool LockWith
        {
            get
            {
                return true;
            }
            set
            {
                base.LockWith = value;
            }
        }

        public override bool LockHeight
        {
            get
            {
                return true;
            }
            set
            {
                base.LockHeight = value;
            }
        }

        public override bool IsRestrictItems
        {
            get
            {
                return true;
            }
            set
            {
                base.IsRestrictItems = true;
            }
        }

        public override bool IsStretchItems
        {
            get
            {
                return true;
            }
            set
            {
                base.IsStretchItems = true;
            }
        }

        public override int MinSize
        {
            get
            {
                return this.m_Owner.MinMRHeight;
            }
            set
            {
                base.MinSize = this.m_Owner.MinMRHeight;
            }
        }

        public override int MaxSize
        {
            get
            {
                return this.m_Owner.MaxMRHeight;
            }
            set
            {
                base.MaxSize = this.m_Owner.MaxMRHeight;
            }
        }

        public override int RestrictItemsHeight
        {
            get
            {
                return this.m_Owner.MenuItemHeight;
            }
            set
            {
                base.RestrictItemsHeight = value;
            }
        }

        public override Rectangle DisplayRectangle
        {
            get
            {
                Rectangle rectangle = this.m_Owner.ItemsRectangle;
                return new Rectangle(rectangle.X, rectangle.Y, base.DisplayRectangle.Width > CONST_MINWIDTH ? base.DisplayRectangle.Width : CONST_MINWIDTH, this.m_Owner.MRRectangle.Height);
            }
        }

        protected override void OnDraw(PaintEventArgs e)
        {
            this.Relayout(e.Graphics, LayoutStyle.eLayoutPlan, true);
            this.Relayout(e.Graphics, LayoutStyle.eLayoutAuto, false);
            //
            //base.OnDraw(e);
        }
    }
}
