using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew.DockPanel
{
    [ToolboxItem(false)]
    sealed class DockPanelHideAreaTop : DockPanelHideArea
    {
        private DockPanelHideAreaLeft m_DockPanelHideAreaLeft = null;   //左边隐藏区
        private DockPanelHideAreaRight m_DockPanelHideAreaRight = null; //右边隐藏区

        public DockPanelHideAreaTop(DockPanelHideAreaLeft dockPanelHideAreaLeft,
            DockPanelHideAreaRight dockPanelHideAreaRight,
            DockPanelManager dockPanelManager)
            : base(DockStyle.Top, dockPanelManager)
        {
            this.m_DockPanelHideAreaLeft = dockPanelHideAreaLeft;
            this.m_DockPanelHideAreaRight = dockPanelHideAreaRight;
            //
            this.m_DockPanelHideAreaLeft.ExistHideAreaTabButtonGroupItem +=
                new EventHandler(DockPanelHideAreaLeftAndRight_ExistOrEmptyHideAreaTabButtonGroupItem);
            this.m_DockPanelHideAreaLeft.EmptyHideAreaTabButtonGroupItem +=
                new EventHandler(DockPanelHideAreaLeftAndRight_ExistOrEmptyHideAreaTabButtonGroupItem);
            this.m_DockPanelHideAreaRight.ExistHideAreaTabButtonGroupItem +=
                new EventHandler(DockPanelHideAreaLeftAndRight_ExistOrEmptyHideAreaTabButtonGroupItem);
            this.m_DockPanelHideAreaRight.EmptyHideAreaTabButtonGroupItem +=
                new EventHandler(DockPanelHideAreaLeftAndRight_ExistOrEmptyHideAreaTabButtonGroupItem);
        }
        private void DockPanelHideAreaLeftAndRight_ExistOrEmptyHideAreaTabButtonGroupItem(object sender, EventArgs e)//留出或取消 左上角和右上角 的空格区
        {
            this.Refresh();
        }

        [Browsable(false), DefaultValue(DockStyle.Top), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override DockStyle Dock
        {
            get
            {
                return base.Dock;
            }
            set
            {
                base.Dock = DockStyle.Top;
            }
        }

        public override Rectangle DisplayRectangle
        {
            get
            {
                if (this.m_DockPanelHideAreaLeft.BaseItems.Count > 0 && this.m_DockPanelHideAreaRight.BaseItems.Count > 0)
                {
                    return new Rectangle(base.DisplayRectangle.X + 25, base.DisplayRectangle.Y, base.DisplayRectangle.Width - 50, base.DisplayRectangle.Height);
                }
                else if (this.m_DockPanelHideAreaLeft.BaseItems.Count > 0 && this.m_DockPanelHideAreaRight.BaseItems.Count <= 0)
                {
                    return new Rectangle(base.DisplayRectangle.X + 25, base.DisplayRectangle.Y, base.DisplayRectangle.Width - 27, base.DisplayRectangle.Height); 
                }
                else if (this.m_DockPanelHideAreaLeft.BaseItems.Count <= 0 && this.m_DockPanelHideAreaRight.BaseItems.Count > 0)
                {
                    return new Rectangle(base.DisplayRectangle.X + 2, base.DisplayRectangle.Y, base.DisplayRectangle.Width - 27, base.DisplayRectangle.Height);
                }
                else
                {
                    return new Rectangle(base.DisplayRectangle.X + 2, base.DisplayRectangle.Y, base.DisplayRectangle.Width - 4, base.DisplayRectangle.Height);
                }
            }
        }
    }
}
