using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    public class DownButtonItem : BaseButtonItem, IUpDownButton, IViewDepend, IBaseItemProperty
    {
        public DownButtonItem()
        {
            base.Name = "GISShare.Controls.WinForm.WFNew.DownButtonItem";
            base.Text = "���°�ť";
            //base.ShowNomalState = true;
            base.LeftBottomRadius = 0;
            base.LeftTopRadius = 0;
            base.RightBottomRadius = 0;
            base.RightTopRadius = 0;
            base.UsingViewOverflow = false;
            
        }

        #region IBaseItemProperty
        [Browsable(false), Description("��������״̬"), Category("����")]
        BaseItemStyle IBaseItemProperty.eBaseItemStyle
        {
            get { return BaseItemStyle.eComponentBaseItem; }
        }

        [Browsable(false), Description("��ȡ������������Ϊ������������Ϊ������"), Category("����")]
        IBaseItem3 IBaseItemProperty.DependObject
        {
            get { return this.pOwner as IBaseItem3; }
        }
        #endregion

        public UpDownButtonStyle eUpDownButtonStyle
        {
            get { return UpDownButtonStyle.eDownButton; }
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
                return pUpDownButtonObjectHelper == null ? false : pUpDownButtonObjectHelper.DownButtonEnabled;
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
                return pUpDownButtonObjectHelper == null ? false : pUpDownButtonObjectHelper.DownButtonVisible;
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
                return pUpDownButtonObjectHelper == null ? base.DisplayRectangle : pUpDownButtonObjectHelper.DownButtonRectangle;
            }
        }

        protected override void OnMouseClick(System.Windows.Forms.MouseEventArgs e)
        {
            base.OnMouseClick(e);
            //this.RelationEvent("MouseClick", e);
            //
            IUpDownButtonObjectHelper pUpDownButtonObjectHelper = this.pOwner as IUpDownButtonObjectHelper;
            if (pUpDownButtonObjectHelper != null) pUpDownButtonObjectHelper.DownButtonOperation();
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            //base.OnPaint(e);
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderUpDownButton(new GISShare.Controls.WinForm.ObjectRenderEventArgs(e.Graphics, this, this.DisplayRectangle));
        }
    }
}
