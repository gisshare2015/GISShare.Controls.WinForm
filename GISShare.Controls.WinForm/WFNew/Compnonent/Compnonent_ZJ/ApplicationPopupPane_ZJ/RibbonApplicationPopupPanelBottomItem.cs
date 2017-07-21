using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    class RibbonApplicationPopupPanelBottomItem : BaseItemStackExItem, IViewDepend, IBaseItemProperty
    {
        IRibbonApplicationPopupPanelItem m_Owner;

        public RibbonApplicationPopupPanelBottomItem(IRibbonApplicationPopupPanelItem pRibbonApplicationPopupPanelItem)
        {
            base.Name = "GISShare.Controls.WinForm.WFNew.RibbonApplicationPopupPanelBottomItem";
            base.Text = "操作栏";
            base.UsingViewOverflow = false;
            //
            this.m_Owner = pRibbonApplicationPopupPanelItem;
        }

        ViewDependStyle IViewDepend.eViewDependStyle
        {
            get
            {
                return ViewDependStyle.eInOwnerDisplayRectangle;
            }
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

        public override Padding Padding
        {
            get
            {
                return new Padding(1, 2, 1, 2);
            }
            set
            {
                base.Padding = new Padding(1, 2, 1, 2);
            }
        }

        public override bool ReverseLayout
        {
            get
            {
                return true;
            }
            set
            {
                base.ReverseLayout = value;
            }
        }

        public override bool IsRestrictItems
        {
            get
            {
                return false;
            }
            set
            {
                base.IsRestrictItems = false;
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

        public override Orientation eOrientation
        {
            get
            {
                return Orientation.Horizontal;
            }
            set
            {
                base.eOrientation = value;
            }
        }

        public override Rectangle DisplayRectangle
        {
            get
            {
                Rectangle rectangle = this.m_Owner.ItemsRectangle;
                return new Rectangle
                    (
                    rectangle.Left,
                    rectangle.Bottom - this.m_Owner.BottomHeight + this.m_Owner.MROMiddleSpace,
                    rectangle.Width,
                    this.m_Owner.BottomHeight - this.m_Owner.MROMiddleSpace
                    );
            }
        }

        protected override void OnDraw(System.Windows.Forms.PaintEventArgs e)
        {
            this.Relayout(e.Graphics, LayoutStyle.eLayoutPlan, true);
            this.Relayout(e.Graphics, LayoutStyle.eLayoutAuto, false);
            //
            //base.OnDraw(e);
        }
    }
}
