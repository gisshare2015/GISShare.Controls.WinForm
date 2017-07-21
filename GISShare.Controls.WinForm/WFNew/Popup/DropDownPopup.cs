using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    [ToolboxItem(false)]
    public class DropDownPopup : ContextPopup, IDropDownPopup
    {
        public DropDownPopup()
            : base()
        { }

        public override bool CustomFiltration
        {
            get
            {
                return true;
            }
        }

        public override bool Filtration(MouseEventArgs e)
        {
            return this.OwnerContainMousePoint(e.Location) || base.Filtration(e);
        }
        private bool OwnerContainMousePoint(Point point)
        {
            if (this.pOwner == null) return false;
            //
            IPopupOwner pPopupOwner = this.pOwner as IPopupOwner;
            if (pPopupOwner != null) return pPopupOwner.PopupTriggerRectangle.Contains(this.pOwner.PointToClient(point));
            else return this.pOwner.DisplayRectangle.Contains(this.pOwner.PointToClient(point));
            //IButtonItem pButtonItem = this.pOwner as IButtonItem;
            //if (pButtonItem != null)
            //{
            //    if (pButtonItem.eButtonStyle == ButtonStyle.eSplitButton) return pButtonItem.SplitRectangle.Contains(this.pOwner.PointToClient(point));
            //    else return this.pOwner.DisplayRectangle.Contains(this.pOwner.PointToClient(point));
            //}
            //else
            //{
            //    ISplitButtonItem pSplitButtonItem = this.pOwner as ISplitButtonItem;
            //    if (pSplitButtonItem != null) return this.pOwner.DisplayRectangle.Contains(this.pOwner.PointToClient(point));
            //    else this.pOwner.DisplayRectangle.Contains(this.pOwner.PointToClient(point));
            //}
            ////
            //return false;
        }
    }
}
