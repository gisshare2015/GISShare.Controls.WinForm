using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew.Forms
{
    class NCOverflowButtonItem : WFNew.OverflowButtonItem, INCBaseItem, IViewDepend
    {
        public NCOverflowButtonItem(WFNew.IBaseBarItem pBaseBarItem)
            : base(pBaseBarItem) { this.UsingViewOverflow = false; }

        #region IOffsetNC
        public int NCOffsetX
        {
            get
            {
                IOffsetNC pOffsetNC = this.pOwner as IOffsetNC;
                if (pOffsetNC == null) return -1;
                return pOffsetNC.NCOffsetX;
            }
        }

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

        //public override Point PopupLoction
        //{
        //    get
        //    {
        //        IOffsetNC pOffsetNC = this.pOwner as IOffsetNC;
        //        if (pOffsetNC == null) return base.PopupLoction;
        //        //
        //        Point point = base.PopupLoction;
        //        return new Point(point.X + pOffsetNC.NCOffsetX, point.Y + pOffsetNC.NCOffsetY);
        //    }
        //}

        //protected override void OnDraw(PaintEventArgs e)
        //{
        //    GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderOverflowButton(
        //        new GISShare.Controls.WinForm.ObjectRenderEventArgs(e.Graphics, this, this.DisplayRectangle));
        //}
    }
}
