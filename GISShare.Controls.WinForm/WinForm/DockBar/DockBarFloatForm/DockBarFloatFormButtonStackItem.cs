using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.DockBar
{
    public class DockBarFloatFormButtonStackItem : WFNew.BaseItemStackItem, WFNew.IFormButtonStack
    {
        DockBarFloatFormButtonItem m_ContextButton;
        DockBarFloatFormButtonItem m_CloseButton;

        public DockBarFloatFormButtonStackItem()
        {
            base.Name = "DockBarFloatFormButtonStackItem";
            base.Text = "DockBarFloatFormButtonStackItem";
            //
            this.m_ContextButton = new DockBarFloatFormButtonItem(DockBarFloatFormButtonStyle.eContextButton);
            this.m_CloseButton = new DockBarFloatFormButtonItem(DockBarFloatFormButtonStyle.eCloseButton);
            this.BaseItems.Add(this.m_ContextButton);
            this.BaseItems.Add(this.m_CloseButton);
            ((WFNew.ILockCollectionHelper)this.BaseItems).SetLocked(true);
        }
        
        public System.Windows.Forms.Form OperationForm
        {
            get { return this.TryGetDependParentForm(); }
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
