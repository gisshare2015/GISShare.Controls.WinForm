using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace GISShare.Controls.WinForm.WFNew
{
    [Designer(typeof(GISShare.Controls.WinForm.WFNew.Design.MenuButtonItemDesigner))]
    public class MenuButtonItem : ButtonItem
    {
        #region ¹¹Ôìº¯Êý
        public MenuButtonItem()
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

        public MenuButtonItem(string strText)
            : base(new DescriptionMenuPopup())
        {
            this.Text = strText;
        }

        public MenuButtonItem(string strName, string strText)
            : base(new DescriptionMenuPopup())
        {
            this.Name = strName;
            this.Text = strText;
        }

        public MenuButtonItem(string strText, Image image)
            : base(new DescriptionMenuPopup())
        {
            this.Text = strText;
            this.Image = image;
        }

        public MenuButtonItem(string strName, string strText, Image image)
            : base(new DescriptionMenuPopup())
        {
            this.Name = strName;
            this.Text = strText;
            this.Image = image;
        }

        //public MenuButtonItem(GISShare.Controls.Plugin.WFNew.IMenuButtonItemP pBaseItemP)
        //    : base(new DescriptionMenuPopup())
        //{
        //    //IPlugin
        //    this.Name = pBaseItemP.Name;
        //    //ISetEntityObject
        //    GISShare.Controls.Plugin.ISetEntityObject pSetEntityObject = pBaseItemP as GISShare.Controls.Plugin.ISetEntityObject;
        //    if (pSetEntityObject != null) pSetEntityObject.SetEntityObject(this);
        //    //IBaseItemP_
        //    this.Checked = pBaseItemP.Checked;
        //    this.Enabled = pBaseItemP.Enabled;
        //    this.Font = pBaseItemP.Font;
        //    this.ForeColor = pBaseItemP.ForeColor;
        //    this.LockHeight = pBaseItemP.LockHeight;
        //    this.LockWith = pBaseItemP.LockWith;
        //    this.Padding = pBaseItemP.Padding;
        //    this.Size = pBaseItemP.Size;
        //    this.Text = pBaseItemP.Text;
        //    this.Visible = pBaseItemP.Visible;
        //    this.Category = pBaseItemP.Category;
        //    this.MinimumSize = pBaseItemP.MinimumSize;
        //    this.UsingViewOverflow = pBaseItemP.UsingViewOverflow;
        //    //ILabelItemP
        //    this.TextAlign = pBaseItemP.TextAlign;
        //    //IImageBoxItemP
        //    this.eImageSizeStyle = pBaseItemP.eImageSizeStyle;
        //    this.Image = pBaseItemP.Image;
        //    this.ImageAlign = pBaseItemP.ImageAlign;
        //    this.ImageSize = pBaseItemP.ImageSize;
        //    //IImageLabelItemP
        //    this.AutoPlanTextRectangle = pBaseItemP.AutoPlanTextRectangle;
        //    this.ITSpace = pBaseItemP.ITSpace;
        //    this.eDisplayStyle = pBaseItemP.eDisplayStyle;
        //    //IBaseButtonItemP
        //    this.LeftBottomRadius = pBaseItemP.LeftBottomRadius;
        //    this.LeftTopRadius = pBaseItemP.LeftTopRadius;
        //    this.RightBottomRadius = pBaseItemP.RightBottomRadius;
        //    this.RightTopRadius = pBaseItemP.RightTopRadius;
        //    this.ShowNomalState = pBaseItemP.ShowNomalState;
        //    //IDropDownButtonItemP
        //    this.DropDownDistance = pBaseItemP.DropDownDistance;
        //    this.eArrowStyle = pBaseItemP.eArrowStyle;
        //    this.eArrowDock = pBaseItemP.eArrowDock;
        //    this.eContextPopupStyle = pBaseItemP.eContextPopupStyle;
        //    this.ArrowSize = pBaseItemP.ArrowSize;
        //    this.PopupSpace = pBaseItemP.PopupSpace;
        //    //ISplitButtonItemP
        //    this.ShowNomalSplitLine = pBaseItemP.ShowNomalSplitLine;
        //    //IButtonItemP
        //    //this.eButtonStyle = pBaseItemP.eButtonStyle;
        //}
        #endregion

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
