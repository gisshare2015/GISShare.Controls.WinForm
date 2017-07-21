using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    class TextBoxButtonItem : GlyphButtonItem, IBaseItemProperty
    {
        public TextBoxButtonItem()
            : base()
        {
            this.LeftBottomRadius = 0;
            this.RightBottomRadius = 0;
            this.LeftTopRadius = 0;
            this.RightTopRadius = 0;
            base.UsingViewOverflow = false;
            
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
            get { return this.pOwner as IBaseItem3; }
        }
        #endregion

        public override DismissPopupStyle eDismissPopupStyle
        {
            get
            {
                return DismissPopupStyle.eNoDismiss;
            }
        }

        public TabButtonContainerButtonStyle eTabButtonContainerButtonStyle
        {
            get
            {
                return TabButtonContainerButtonStyle.eContextButton;
            }
        }

        public override System.Drawing.Rectangle DisplayRectangle
        {
            get
            {
                IButtonTextBoxItem pButtonTextBoxItem = this.pOwner as IButtonTextBoxItem;
                return pButtonTextBoxItem == null ? base.DisplayRectangle : pButtonTextBoxItem.GlyphButtonRectangle;
            }
        }
    }
}
