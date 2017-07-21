using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    class RibbonApplicationPopupPanelMiddleRightItem : BaseItemStackExItem, IViewDepend, IBaseItemProperty
    {
        IRibbonApplicationPopupPanelItem m_Owner;

        public RibbonApplicationPopupPanelMiddleRightItem(IRibbonApplicationPopupPanelItem pRibbonApplicationPopupPanelItem)
        {
            base.Name = "GISShare.Controls.WinForm.WFNew.RibbonApplicationPopupPanelMiddleRightItem";
            base.Text = "记录栏";
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

        public override Padding Padding
        {
            get
            {
                return new Padding(2, 1, 2, 1);
            }
            set
            {
                base.Padding = new Padding(2, 1, 2, 1);
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
                return this.m_Owner.RecordItemHeight;
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
                int iW = (int)(this.m_Owner.MenuItemsWidth * this.m_Owner.RecordItemsWidthFactor);
                Rectangle rectangle = this.m_Owner.ItemsRectangle;
                return new Rectangle(rectangle.Right - iW, rectangle.Y, iW, this.m_Owner.MRRectangle.Height);
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
