using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    public class UpButtonItem : BaseButtonItem, IUpDownButton, IViewDepend, IBaseItemProperty
    {
        public UpButtonItem()
        {
            base.Name = "GISShare.Controls.WinForm.WFNew.UpButtonItem";
            base.Text = "向上按钮";
            //base.ShowNomalState = true;
            base.LeftBottomRadius = 0;
            base.LeftTopRadius = 0;
            base.RightBottomRadius = 0;
            base.RightTopRadius = 0;
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

        public UpDownButtonStyle eUpDownButtonStyle
        {
            get { return UpDownButtonStyle.eUpButton; }
        }

        public override DismissPopupStyle eDismissPopupStyle
        {
            get
            {
                return DismissPopupStyle.eNoDismiss;
            }
        }

        ViewDependStyle IViewDepend.eViewDependStyle
        {
            get
            {
                return ViewDependStyle.eInOwnerDisplayRectangle;
            }
        }

        public override bool Overflow
        {
            get
            {
                return false;
            }
        }

        public override bool Enabled
        {
            get
            {
                IUpDownButtonObjectHelper pUpDownButtonObjectHelper = this.pOwner as IUpDownButtonObjectHelper;
                return pUpDownButtonObjectHelper == null ? false : pUpDownButtonObjectHelper.UpButtonEnabled;
            }
            set
            {
                base.Enabled = value;
            }
        }

        public override bool Visible
        {
            get
            {
                IUpDownButtonObjectHelper pUpDownButtonObjectHelper = this.pOwner as IUpDownButtonObjectHelper;
                return pUpDownButtonObjectHelper == null ? false : pUpDownButtonObjectHelper.UpButtonVisible;
            }
            set
            {
                base.Visible = value;
            }
        }

        public override System.Drawing.Rectangle DisplayRectangle
        {
            get
            {
                IUpDownButtonObjectHelper pUpDownButtonObjectHelper = this.pOwner as IUpDownButtonObjectHelper;
                return pUpDownButtonObjectHelper == null ? base.DisplayRectangle : pUpDownButtonObjectHelper.UpButtonRectangle;
            }
        }

        protected override void OnMouseClick(System.Windows.Forms.MouseEventArgs e)
        {
            base.OnMouseClick(e);
            //this.RelationEvent("MouseClick", e);
            //
            IUpDownButtonObjectHelper pUpDownButtonObjectHelper = this.pOwner as IUpDownButtonObjectHelper;
            if (pUpDownButtonObjectHelper != null) pUpDownButtonObjectHelper.UpButtonOperation();
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            //base.OnPaint(e);
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderUpDownButton(new GISShare.Controls.WinForm.ObjectRenderEventArgs(e.Graphics, this, this.DisplayRectangle));
        }
    }
}
