using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    public class FormButtonStackItem : BaseItemStackItem, IFormButtonStack, IViewDepend, IBaseItemProperty
    {
        FormButtonItem m_MinButton;
        FormButtonItem m_MaxButton;
        FormButtonItem m_HelpButton;
        FormButtonItem m_CloseButton;

        public FormButtonStackItem()
        {
            base.Name = "GISShare.Controls.WinForm.WFNew.FormButtonStackItem";
            base.Text = "窗体按钮栈";
            base.UsingViewOverflow = false;            
            //
            this.m_MinButton = new FormButtonItem(FormButtonStyle.eMinButton) { Text = "最小化" };
            this.m_MaxButton = new FormButtonItem(FormButtonStyle.eMaxButton) { Text = "最大化" };
            this.m_HelpButton = new FormButtonItem(FormButtonStyle.eHelpButton) { Text = "帮助" };
            this.m_CloseButton = new FormButtonItem(FormButtonStyle.eCloseButton) { Text = "关闭" };
            this.BaseItems.Add(this.m_MinButton);
            this.BaseItems.Add(this.m_MaxButton);
            this.BaseItems.Add(this.m_HelpButton);
            this.BaseItems.Add(this.m_CloseButton);
        }
        
        public System.Windows.Forms.Form OperationForm
        {
            get { return this.TryGetDependParentForm() as System.Windows.Forms.Form; }
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
                return  System.Windows.Forms.Orientation.Horizontal;
            }
            set
            {
                base.eOrientation =  System.Windows.Forms.Orientation.Horizontal;
            }
        }

        public override bool LockWith
        {
            get
            {
                return true;
            }
            set
            {
                base.LockWith = value;
            }
        }

        public override int ColumnDistance
        {
            get
            {
                return 1;
            }
            set
            {
                base.ColumnDistance = 1;
            }
        }
    }
}
