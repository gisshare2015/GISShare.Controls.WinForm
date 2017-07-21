using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew.DockPanel
{
    class DockPanelFloatFormButtonStackItem : WFNew.BaseItemStackItem
    {
        private DockPanelFloatFormButtonItem m_MaxButtonItem;
        private DockPanelFloatFormButtonItem m_CloseButtonItem;

        public DockPanelFloatFormButtonStackItem()
        {
            base.Name = "DockPanelFloatFormButtonStackItem";
            base.Text = "DockPanelFloatFormButtonStackItem";
            //
            this.m_MaxButtonItem = new DockPanelFloatFormButtonItem(DockPanelFloatFormButtonItemStyle.eMaxButton);
            this.m_CloseButtonItem = new DockPanelFloatFormButtonItem(DockPanelFloatFormButtonItemStyle.eCloseButton);
            this.BaseItems.Add(this.m_MaxButtonItem);
            this.BaseItems.Add(this.m_CloseButtonItem);
            ((WFNew.ILockCollectionHelper)this.BaseItems).SetLocked(true);
        }

        public override System.Windows.Forms.Orientation eOrientation
        {
            get
            {
                return System.Windows.Forms.Orientation.Horizontal;
            }
            set
            {
                base.eOrientation = System.Windows.Forms.Orientation.Horizontal;
            }
        }

        public override bool IsRestrictItems
        {
            get
            {
                return true;
            }
            set
            {
                base.IsRestrictItems = true;
            }
        }

        public override bool IsStretchItems
        {
            get
            {
                return true;
            }
            set
            {
                base.IsStretchItems = true;
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
