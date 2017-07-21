using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    class OverflowButtonItem : DropDownButtonItem, IOverflowButton, IViewDepend, IBaseItemProperty
    {
        private IBaseBarItem owner = null;

        public OverflowButtonItem(IBaseBarItem pBaseBarItem)
            : base()
        {
            base.Name = "GISShare.Controls.WinForm.WFNew.OverflowButtonItem";
            base.Text = "溢出按钮";
            base.UsingViewOverflow = false;            
            //
            this.ShowNomalState = false;
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

        public bool ReverseLayout
        {
            get { return this.owner.ReverseLayout; }
        }

        public Orientation eOrientation
        {
            get { return this.owner.eOrientation; }
        }

        ViewDependStyle IViewDepend.eViewDependStyle
        {
            get
            {
                return ViewDependStyle.eInOwnerDisplayRectangle;
            }
        }

        public override Point PopupLoction
        {
            get
            {
                Rectangle rectangle = this.DisplayRectangle;
                Point point = this.PointToScreen(new Point(rectangle.Right, rectangle.Top));
                point.X += PopupSpace;
                return point;
            }
        }

        public override bool Enabled
        {
            get
            {
                return this.owner.Enabled;
            }
        }

        public override bool Visible
        {
            get
            {
                return this.owner.OverflowItemsCount > 0 && this.owner.ShowOverflowButton;
            }
        }

        public override bool Overflow
        {
            get
            {
                return !this.owner.DisplayRectangle.Contains(this.DisplayRectangle);
            }
        }

        public override int LeftTopRadius
        {
            get
            {
                return this.owner.LeftTopRadius;
            }
            set { }
        }

        public override int RightTopRadius
        {
            get
            {
                return this.owner.RightTopRadius;
            }
            set { }
        }

        public override int LeftBottomRadius
        {
            get
            {
                return this.owner.LeftBottomRadius;
            }
            set { }
        }

        public override int RightBottomRadius
        {
            get
            {
                return this.owner.RightBottomRadius;
            }
            set { }
        }

        public override Rectangle DisplayRectangle
        {
            get
            {
                return this.owner.OverflowButtonRectangle;
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.ClearBaseItems();
            //
            IImageLabelItem pImageLabelItem;
            for (int i = this.owner.BaseItems.Count - 1; i >= 0; i--)
            {
                if (!this.owner.BaseItems[i].Overflow) break;
                this.BaseItems.Insert(0, this.owner.BaseItems[i] as BaseItem);//.Clone() as BaseItem
                //
                pImageLabelItem = this.BaseItems[0] as IImageLabelItem;
                if (pImageLabelItem != null)
                {
                    pImageLabelItem.eImageSizeStyle = ImageSizeStyle.eSystem;
                    pImageLabelItem.eDisplayStyle = DisplayStyle.eImageAndText;
                }
            }
            //
            base.OnMouseDown(e);
        }

        protected override void OnPopupClosed(EventArgs e)
        {
            this.ClearBaseItems();
            //
            base.OnPopupClosed(e);
        }
        private void ClearBaseItems()
        {
            foreach (BaseItem one in this.BaseItems)
            {
                ((ISetOwnerHelper)one).SetOwner(this.owner as IOwner);
            }
            //
            this.BaseItems.Clear();
        }

        protected override void OnDraw(PaintEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderOverflowButton(
                new GISShare.Controls.WinForm.ObjectRenderEventArgs(e.Graphics, this, this.DisplayRectangle));
        }
    }
}
