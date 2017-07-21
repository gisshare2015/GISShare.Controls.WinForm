using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew.Forms
{
    class NCFormButtonStackItem : WFNew.BaseItemStackItem, WFNew.IFormButtonStack, INCBaseItem, IViewDepend
    {
        NCFormButtonItem m_MinButton;
        NCFormButtonItem m_MaxButton;
        NCFormButtonItem m_HelpButton;
        NCFormButtonItem m_CloseButton;

        public NCFormButtonStackItem()
        {
            this.m_MinButton = new NCFormButtonItem(WFNew.FormButtonStyle.eMinButton);
            this.m_MinButton.Text = "最小化";
            this.m_MaxButton = new NCFormButtonItem(WFNew.FormButtonStyle.eMaxButton);
            this.m_MaxButton.Text = "最大化";
            this.m_HelpButton = new NCFormButtonItem(WFNew.FormButtonStyle.eHelpButton);
            this.m_HelpButton.Text = "帮助";
            this.m_CloseButton = new NCFormButtonItem(WFNew.FormButtonStyle.eCloseButton);
            this.m_CloseButton.Text = "关闭";
            this.BaseItems.Add(this.m_MinButton);
            this.BaseItems.Add(this.m_MaxButton);
            this.BaseItems.Add(this.m_HelpButton);
            this.BaseItems.Add(this.m_CloseButton);
            //
            this.UsingViewOverflow = false;
        }

        public System.Windows.Forms.Form OperationForm
        {
            get
            {
                //IDependItem pDependItem = this.pOwner as IDependItem;
                //if (pDependItem == null) return this.TryGetDependParentForm();
                //return pDependItem.DependObject as System.Windows.Forms.Form;
                //return this.TryGetDependParentForm();
                IDependItem pDependItem = this.TryGetDependItem_DG(this.pOwner);
                if (pDependItem == null) return this.TryGetDependParentForm();
                return pDependItem.DependObject as System.Windows.Forms.Form;
            }
        }
        private IDependItem TryGetDependItem_DG(IOwner owner)
        {
            if (owner == null) return null;
            //
            IDependItem pDependItem = owner as IDependItem;
            if (pDependItem == null) return this.TryGetDependItem_DG(owner.pOwner);
            return pDependItem;
        }

        #region IOffsetNC
        [Browsable(false), Description("X轴偏移"), Category("布局")]
        public int NCOffsetX
        {
            get
            {
                IOffsetNC pOffsetNC = this.pOwner as IOffsetNC;
                if (pOffsetNC == null) return -1;
                return pOffsetNC.NCOffsetX;
            }
        }

        [Browsable(false), Description("Y轴偏移"), Category("布局")]
        public int NCOffsetY
        {
            get
            {
                IOffsetNC pOffsetNC = this.pOwner as IOffsetNC;
                if (pOffsetNC == null) return -1;
                return pOffsetNC.NCOffsetY;
            }
        }
        #endregion

        #region INCBaseItem
        public IBaseItemOwnerNC GetTopBaseItemOwnerNC()
        {
            return this.TryGetDependControl() as IBaseItemOwnerNC;
        }
        #endregion

        public override Rectangle ItemsViewRectangle
        {
            get
            {
                return base.ItemsRectangle;
            }
        }

        ViewDependStyle IViewDepend.eViewDependStyle
        {
            get
            {
                return ViewDependStyle.eInOwnerDisplayRectangle;
            }
        }

        public override void Refresh()
        {
            IBaseItemOwnerNC pBaseItemOwnerNC = this.pOwner as IBaseItemOwnerNC;
            if (pBaseItemOwnerNC != null)
            {
                pBaseItemOwnerNC.RefreshNC();
            }
            else
            {
                base.Refresh();
            }
        }

        public override bool Overflow
        {
            get
            {
                IBaseItemOwnerNC pBaseItemOwnerNC = this.pOwner as IBaseItemOwnerNC;
                if (pBaseItemOwnerNC == null) return base.Overflow;
                return !pBaseItemOwnerNC.ItemsRectangleNC.Contains(this.DisplayRectangle);
            }
        }

        public override void Invalidate(Rectangle rectangle)
        {
            this.Refresh();
        }

        public override System.Windows.Forms.Orientation eOrientation
        {
            get
            {
                return  System.Windows.Forms.Orientation.Horizontal;
            }
            set
            {
                base.eOrientation =  System.Windows.Forms.Orientation.Horizontal;
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
                base.IsStretchItems = value;
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

        public override int ColumnDistance
        {
            get
            {
                return 0;
            }
            set
            {
                base.ColumnDistance = value;
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
