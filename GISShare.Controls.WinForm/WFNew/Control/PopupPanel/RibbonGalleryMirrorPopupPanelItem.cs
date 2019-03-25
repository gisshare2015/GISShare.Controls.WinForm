using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    [ToolboxItem(false)]
    class RibbonGalleryMirrorPopupPanelItem : BaseItemStackExItem, IGalleryMirrorPopupPanelItem
    {
        private IGalleryItem m_pGalleryItem;

        public RibbonGalleryMirrorPopupPanelItem(IGalleryItem pGalleryItem)
        { 
            m_pGalleryItem = pGalleryItem;
        }

        #region IPopupPanel
        private Control m_Entity;
        /// <summary>
        /// 依附实体
        /// </summary>
        [Browsable(false), Description("Popup依附实体"), Category("属性")]
        public Control Entity
        {
            get { return m_Entity; }
            set { m_Entity = value; }
        }

        public void TrySetPopupPanelSize(Size size)
        {
            this.Size = size;
            if (this.m_Entity != null) this.m_Entity.Size = size;
        }
        #endregion

        #region IGalleryMirrorPopupPanelItem
        [Browsable(false), Description("它的实体原型"), Category("设计")]
        public IGalleryItem pGalleryItem { get { return this.m_pGalleryItem; } }
        #endregion

        public override int LineDistance
        {
            get
            {
                return this.m_pGalleryItem.LineDistance;
            }
            set
            {
                base.LineDistance = value;
            }
        }

        public override int ColumnDistance
        {
            get
            {
                return this.m_pGalleryItem.LineDistance;
            }
            set
            {
                base.ColumnDistance = value;
            }
        }

        public override System.Windows.Forms.Orientation eOrientation
        {
            get
            {
                return System.Windows.Forms.Orientation.Vertical;
            }
            set
            {
                base.eOrientation = value;
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
                base.IsRestrictItems = value;
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

        public override bool IsStretchItems
        {
            get
            {
                return false;
            }
            set
            {
                base.IsStretchItems = value;
            }
        }

        public override int MinSize
        {
            get
            {
                return m_pGalleryItem.Size.Height;
            }
            set
            {
                base.MinSize = value;
            }
        }

        public override int MaxSize
        {
            get
            {
                return System.Windows.Forms.SystemInformation.WorkingArea.Height / 2;
            }
            set
            {
                base.MaxSize = value;
            }
        }

        public override System.Windows.Forms.Padding Padding
        {
            get
            {
                return new System.Windows.Forms.Padding(8, 1, 8, 1);
            }
            set
            {
                base.Padding = value;
            }
        }

        public override BaseItemCollection BaseItems
        {
            get
            {
                return this.m_pGalleryItem.BaseItems;
            }
        }

        protected override void OnDraw(System.Windows.Forms.PaintEventArgs e)
        {
            if (!this.BaseItems.OwnerEquals(this)) ((ISetOwnerHelper)this.BaseItems).SetOwner(this);//key
            base.Relayout(e.Graphics, LayoutStyle.eLayoutPlan, true);
            base.Relayout(e.Graphics, LayoutStyle.eLayoutAuto, false);
            //
            //base.OnDraw(e);
            //
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonGalleryPopupPanel(
                new GISShare.Controls.WinForm.ObjectRenderEventArgs(e.Graphics, this, this.DisplayRectangle));
        }
    }
}
