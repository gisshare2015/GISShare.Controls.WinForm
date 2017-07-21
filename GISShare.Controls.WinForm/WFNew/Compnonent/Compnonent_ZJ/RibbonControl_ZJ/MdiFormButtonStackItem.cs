using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    public class MdiFormButtonStackItem : BaseItemStackItem, IFormButtonStack, IBaseItemProperty
    {
        MdiFormListButtonItemMFBS m_MdiFormListButton;
        FormButtonItem m_MinButton;
        FormButtonItem m_MaxButton;
        FormButtonItem m_CloseButton;

        public MdiFormButtonStackItem()
        {
            base.Name = "GISShare.Controls.WinForm.WFNew.MdiFormButtonStackItem";
            base.Text = "子窗体按钮栈";
            
            //
            this.m_MdiFormListButton = new MdiFormListButtonItemMFBS();
            this.BaseItems.Add(this.m_MdiFormListButton);
            this.m_MinButton = new FormButtonItem(FormButtonStyle.eMdiMinButton);
            this.BaseItems.Add(this.m_MinButton);
            this.m_MaxButton = new FormButtonItem(FormButtonStyle.eMdiMaxButton);
            this.BaseItems.Add(this.m_MaxButton);
            this.m_CloseButton = new FormButtonItem(FormButtonStyle.eMdiCloseButton);
            this.BaseItems.Add(this.m_CloseButton);
        }

        public override bool Visible
        {
            get
            {
                if (this.OperationForm != null)
                {
                    if (this.OperationForm.IsMdiContainer && this.HaveMaximizedFormWindowState())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                return base.Visible;
            }
            set
            {
                base.Visible = false;
            }
        }
        private bool HaveMaximizedFormWindowState()
        {
            foreach(Form one in this.OperationForm.MdiChildren)
            {
                if (one.WindowState == FormWindowState.Maximized) return true;
            }
            //
            return false;
        }
        
        public System.Windows.Forms.Form OperationForm
        {
            get { return this.TryGetDependParentForm(); }
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