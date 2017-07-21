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
            base.Text = "�Ӵ����б�ť";
            
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
