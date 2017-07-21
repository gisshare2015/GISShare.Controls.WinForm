using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    internal class ScrollDropDownButtonItem : GalleryScrollButtonItem, IBaseItemProperty
    {
        private IGalleryItem owner = null;

        public ScrollDropDownButtonItem(IGalleryItem pGalleryItem)
            : base(GalleryScrollButtonStyle.eScrollDropDownButton)
        {
            base.Name = "GISShare.Controls.WinForm.WFNew.ScrollDropDownButtonItem";
            base.Text = "画廊列表按钮";
            //
            this.owner = pGalleryItem;
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

        public override bool Enabled
        {
            get
            {
                return base.Enabled && this.owner.Enabled && this.owner.TopViewItemIndex >= 0 && !this.owner.IsOpened;
            }
            set
            {
                base.Enabled = value;
            }
        }

        public override int LeftTopRadius
        {
            get
            {
                return 0;
            }
            set { }
        }

        public override int RightTopRadius
        {
            get
            {
                return 0;
            }
            set { }
        }

        public override int LeftBottomRadius
        {
            get
            {
                return 0;
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

        public override bool Overflow
        {
            get
            {
                return !this.owner.ScrollRectangle.Contains(this.DisplayRectangle);
            }
        }

        public override Rectangle DisplayRectangle
        {
            get
            {
                return this.owner.ScrollDropDownButtonRectangle;
            }
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            this.owner.ShowPopup();
            ////
            //base.OnMouseDown(mevent);
            //this.RelationEvent("MouseDown", mevent);
        }
    }
}
