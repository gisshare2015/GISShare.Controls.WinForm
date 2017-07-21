using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    class MdiFormListButtonItemMFBS : MdiFormListButtonItem, IBaseItemProperty
    {
        public MdiFormListButtonItemMFBS()
        {
            base.Name = "GISShare.Controls.WinForm.WFNew.MdiFormListButtonItemMFBS";
            base.Text = "子窗体列表按钮";
            
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

        public override DisplayStyle eDisplayStyle
        {
            get
            {
                return DisplayStyle.eImage;
            }
            set
            {
                base.eDisplayStyle = value;
            }
        }

        public override System.Drawing.Rectangle DisplayRectangle
        {
            get
            {
                return new System.Drawing.Rectangle(this.Location, new System.Drawing.Size(20, 20));
                //return base.DisplayRectangle;
            }
        }
    }
}
