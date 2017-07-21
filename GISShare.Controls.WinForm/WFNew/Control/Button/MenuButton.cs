using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    [Designer(typeof(GISShare.Controls.WinForm.WFNew.Design.MenuButtonDesigner)), ToolboxItem(true)]
    public class MenuButton : GISShare.Controls.WinForm.WFNew.ButtonN
    {
        public MenuButton()
            : base(new DescriptionMenuPopup())
        {
            this.AutoPlanTextRectangle = true;
            this.TextAlign = ContentAlignment.MiddleLeft;
            this.ImageAlign = ContentAlignment.MiddleLeft;
            this.Padding = new System.Windows.Forms.Padding(6);
            this.eArrowDock = ArrowDock.eRight;
            this.eArrowStyle = ArrowStyle.eToRight;
            this.ArrowSize = new Size(4, 6);
            this.ShowNomalSplitLine = false;
            this.Size = new Size(this.Padding.Left + this.Padding.Right + 23, this.Padding.Top + this.Padding.Bottom + 23);
            this.DropDownDistance = 23;
        }

        public override System.Drawing.Point PopupLoction
        {
            get
            {
                if(this.pOwner == null || this.pOwner.pOwner == null)  return base.PopupLoction;
                IRibbonApplicationPopupPanelItem pRibbonApplicationPopupPanelItem = this.pOwner.pOwner as IRibbonApplicationPopupPanelItem;
                ISimplyPopup pSimplyPopup = ((IPopupOwnerHelper)this).GetBasePopup() as ISimplyPopup;
                Rectangle rectangle = pRibbonApplicationPopupPanelItem.AnchorRectangle;
                rectangle = Rectangle.FromLTRB(rectangle.Left + 1, rectangle.Top + 1, rectangle.Right - 1, rectangle.Bottom - 1);
                if (pSimplyPopup != null) pSimplyPopup.GetPopupPanel().TrySetPopupPanelSize(rectangle.Size);
                return this.pOwner.pOwner.PointToScreen(rectangle.Location);
            }
        }

        public override ButtonStyle eButtonStyle
        {
            get
            {
                return this.HaveVisibleBaseItem ? ButtonStyle.eSplitButton : ButtonStyle.eButton;
            }
            set
            {
                base.eButtonStyle = value;
            }
        }
    }
}
