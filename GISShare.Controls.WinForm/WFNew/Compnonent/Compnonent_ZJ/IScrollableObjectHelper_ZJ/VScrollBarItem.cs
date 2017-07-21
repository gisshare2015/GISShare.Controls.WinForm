using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    class VScrollBarItem : ScrollBarItem, IViewDepend, IBaseItemProperty
    {
        public VScrollBarItem()
        {
            base.Name = "GISShare.Controls.WinForm.WFNew.VScrollBarItem";
            base.Text = "��ֱ������";
            base.UsingViewOverflow = false;
            base.AutoGetFocus = true;
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

        ViewDependStyle IViewDepend.eViewDependStyle
        {
            get
            {
                return ViewDependStyle.eInOwnerDisplayRectangle;
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

        public override int Minimum
        {
            get
            {
                IScrollableObjectHelper pScrollableObjectHelper = this.pOwner as IScrollableObjectHelper;
                if (pScrollableObjectHelper != null) return pScrollableObjectHelper.VScrollBarMinimum;
                return base.Minimum;
            }
            set
            {
                base.Minimum = value;
            }
        }

        public override int Maximum
        {
            get
            {
                IScrollableObjectHelper pScrollableObjectHelper = this.pOwner as IScrollableObjectHelper;
                if (pScrollableObjectHelper != null) return pScrollableObjectHelper.VScrollBarMaximum;
                return base.Maximum;
            }
            set
            {
                base.Maximum = value;
            }
        }

        public override bool Visible
        {
            get
            {
                IScrollableObjectHelper pScrollableObjectHelper = this.pOwner as IScrollableObjectHelper;
                if (pScrollableObjectHelper != null) return pScrollableObjectHelper.VScrollBarVisible;
                return base.Visible;
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
                IScrollableObjectHelper pScrollableObjectHelper = this.pOwner as IScrollableObjectHelper;
                if (pScrollableObjectHelper != null) return pScrollableObjectHelper.VScrollBarDisplayRectangle;
                return base.DisplayRectangle;
            }
        }

        protected override void OnValueChanged(IntValueChangedEventArgs e)
        {
            IScrollableObjectHelper pScrollableObjectHelper = this.pOwner as IScrollableObjectHelper;
            if (pScrollableObjectHelper != null) pScrollableObjectHelper.ScrollValueRefresh();
            //
            base.OnValueChanged(e);
        }
    }
}
