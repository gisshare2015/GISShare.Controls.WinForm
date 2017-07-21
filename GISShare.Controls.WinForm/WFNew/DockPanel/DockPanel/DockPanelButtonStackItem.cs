using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew.DockPanel
{
    class DockPanelButtonStackItem : WFNew.BaseItemStackItem
    {
        DockPanelButtonItem m_ContextButtonItem;
        DockPanelButtonItem m_HideButtonItem;
        DockPanelButtonItem m_CloseButtonItem;

        public DockPanelButtonStackItem()
        {
            base.Name = "DockPanelButtonStackItem";
            base.Text = "DockPanelButtonStackItem";
            //
            this.m_ContextButtonItem = new DockPanelButtonItem(DockPanelButtonItemStyle.eContextButton);
            this.m_ContextButtonItem.Text = "自定义";
            this.m_HideButtonItem = new DockPanelButtonItem(DockPanelButtonItemStyle.eHideButton);
            this.m_HideButtonItem.Text = "自动隐藏";
            this.m_CloseButtonItem = new DockPanelButtonItem(DockPanelButtonItemStyle.eCloseButton);
            this.m_CloseButtonItem.Text = "关闭";
            this.BaseItems.Add(this.m_ContextButtonItem);
            this.BaseItems.Add(this.m_HideButtonItem);
            this.BaseItems.Add(this.m_CloseButtonItem);
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

        public override bool IsRestrictItems
        {
            get
            {
                return false;
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
