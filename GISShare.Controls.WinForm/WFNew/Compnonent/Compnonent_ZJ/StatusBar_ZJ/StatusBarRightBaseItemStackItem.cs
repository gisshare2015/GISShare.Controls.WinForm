using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    class StatusBarRightBaseItemStackItem: BaseItemStackItem, IBaseItemProperty, IViewDepend
    {
        private IBaseBarItem owner = null;

        public StatusBarRightBaseItemStackItem(IBaseBarItem pBaseBarItem)
            : base()
        {
            base.Name = "GISShare.Controls.WinForm.WFNew.RightBaseItemStackItem";
            base.Text = "右侧栈";
            base.UsingViewOverflow = false;
            base.Visible = true;
            base.IsRestrictItems = true;
            base.IsStretchItems = true;
            base.LockHeight = false;
            base.LockWith = false;
            //
            this.owner = pBaseBarItem;
            ((ISetOwnerHelper)this).SetOwner(owner as IOwner);
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
            get { return this.owner; }
        }
        #endregion

        #region IViewDepend
        ViewDependStyle IViewDepend.eViewDependStyle
        {
            get
            {
                return ViewDependStyle.eInOwnerDisplayRectangle;
            }
        }
        #endregion

        [Browsable(false)]
        public override Rectangle DisplayRectangle
        {
            get
            {
                Padding padding = this.owner.Padding;
                Rectangle rectangle = base.DisplayRectangle;
                Rectangle rectangle2 = this.owner.DisplayRectangle;
                switch (this.owner.eOrientation)
                {
                    case Orientation.Horizontal:
                        if (this.owner.ReverseLayout)
                        {
                            return Rectangle.FromLTRB(padding.Left + rectangle2.Left, padding.Top + rectangle2.Top, padding.Left + rectangle2.Left + rectangle.Width, rectangle2.Bottom - padding.Bottom);
                        }
                        else
                        {
                            return Rectangle.FromLTRB(rectangle2.Right - rectangle.Width - padding.Right, padding.Top + rectangle2.Top, rectangle2.Right - padding.Right, rectangle2.Bottom - padding.Bottom);
                        }
                    case Orientation.Vertical:
                        if (this.owner.ReverseLayout)
                        {
                            return Rectangle.FromLTRB(padding.Left + rectangle2.Left, padding.Top + rectangle2.Top, rectangle2.Right - padding.Right, padding.Top + rectangle2.Top + rectangle.Height);
                        }
                        else
                        {
                            return Rectangle.FromLTRB(padding.Left + rectangle2.Left, rectangle2.Bottom - rectangle.Height - padding.Bottom, rectangle2.Right - padding.Right, rectangle2.Bottom - padding.Bottom);
                        }
                    default:
                        break;
                }
                return base.DisplayRectangle;
            }
        }

        [Browsable(false)]
        public override Rectangle ItemsViewRectangle
        {
            get
            {
                return Rectangle.Intersect(this.owner.DisplayRectangle, this.ItemsRectangle);
            }
        }

        [Browsable(false)]
        public override bool ReverseLayout
        {
            get { return !this.owner.ReverseLayout; }
        }

        [Browsable(false)]
        public override Orientation eOrientation
        {
            get { return this.owner.eOrientation; }
        }

        [Browsable(false)]
        public override bool Enabled
        {
            get
            {
                return this.owner.Enabled;
            }
        }

        [Browsable(false)]
        public override bool Overflow
        {
            get
            {
                return !this.owner.DisplayRectangle.Contains(this.DisplayRectangle);
            }
        }

        protected override Size Relayout(Graphics g, LayoutStyle eLayoutStyle, bool bSetSize)
        {
            Size size = base.Relayout(g, eLayoutStyle, bSetSize);
            //
            if (!size.IsEmpty)
            {
                this.Size = size;
            }
            //
            return size;
        }
    }
}
