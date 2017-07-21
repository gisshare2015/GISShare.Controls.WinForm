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
    class HideAreaTabButtonGroupItem : WFNew.BaseItemStackItem //Control
    {
        private const int CTR_HIDEAREATABBUTTONGROUPSPACE = 6;

        private DockPanel m_DockPanel = null;                                                                //记录其所对应的停靠面板
        private System.Windows.Forms.TabAlignment m_TabAlignment = System.Windows.Forms.TabAlignment.Bottom; //隐藏按钮组的绘制状态

        public HideAreaTabButtonGroupItem(DockPanel dockPanel)
        {
            base.Visible = true;
            //
            this.m_DockPanel = dockPanel;
            //
            this.SetDockPanelHideAreaControl();
        }
        private void SetDockPanelHideAreaControl()//设置隐藏按钮组并将其加载到对应的隐藏区内（在构造函数里完成）
        {
            DockStyle eDockStyle;
            DockAreaStyle eDockAreaStyle = this.m_DockPanel.GetDockAreaStyle(out eDockStyle);
            switch (eDockStyle)//添加后会自动处理布局信息
            {
                case DockStyle.Top:
                    this.m_TabAlignment = TabAlignment.Top;
                    this.DockPanelManager.DockPanelHideAreaTop.BaseItems.Add(this);
                    break;
                case DockStyle.Left:
                    this.m_TabAlignment = TabAlignment.Left;
                    this.DockPanelManager.DockPanelHideAreaLeft.BaseItems.Add(this);
                    break;
                case DockStyle.Right:
                    this.m_TabAlignment = TabAlignment.Right;
                    this.DockPanelManager.DockPanelHideAreaRight.BaseItems.Add(this);
                    break;
                case DockStyle.Bottom:
                    this.m_TabAlignment = TabAlignment.Bottom;
                    this.DockPanelManager.DockPanelHideAreaBottom.BaseItems.Add(this);
                    break;
                default:
                    break;
            }
            //
            //
            //
            for (int i = 0; i < this.m_DockPanel.BasePanels.Count; i++)
            {
                BasePanel temp = this.m_DockPanel.BasePanels[i] as BasePanel;
                if (temp == null) continue;
                HideAreaTabButtonItem hideAreaTabButtonItem = new HideAreaTabButtonItem(i, temp.Text, temp.Image, this.TabAlignment);
                this.BaseItems.Add(hideAreaTabButtonItem);
                hideAreaTabButtonItem.MouseDown += new MouseEventHandler(HideAreaTabButtonItem_MouseDown);
                hideAreaTabButtonItem.MouseEnter += new EventHandler(HideAreaTabButtonItem_MouseEnter);
                hideAreaTabButtonItem.MouseLeave += new EventHandler(HideAreaTabButtonItem_MouseLeave);
            }
        }
        private void HideAreaTabButtonItem_MouseDown(object sender, MouseEventArgs e)
        {
            HideAreaTabButtonItem hideAreaTabButtonItem = sender as HideAreaTabButtonItem;
            if (hideAreaTabButtonItem == null) return;
            if (this.m_DockPanel.BasePanelSelectedIndex != hideAreaTabButtonItem.BasePanelID) this.m_DockPanel.BasePanelSelectedIndex = hideAreaTabButtonItem.BasePanelID;
            //
            ((ISetDockPanelHelper)this.DockPanel).SetActiveState(this.DockPanel);
        }
        private void HideAreaTabButtonItem_MouseEnter(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            //
            HideAreaTabButtonItem hideAreaTabButtonItem = sender as HideAreaTabButtonItem;
            if (hideAreaTabButtonItem == null) return;
            bool bSame = this.m_DockPanel.BasePanelSelectedIndex == hideAreaTabButtonItem.BasePanelID;
            if (!bSame) this.m_DockPanel.BasePanelSelectedIndex = hideAreaTabButtonItem.BasePanelID;
            if (!bSame ||
                this.m_DockPanel.DockPanelManager.DockPanelHidePanel.DockPanel == null ||
                !this.m_DockPanel.DockPanelManager.DockPanelHidePanel.DockPanel.Visible ||
                this.m_DockPanel.DockPanelManager.DockPanelHidePanel.DockPanel != this.DockPanel)
            {
                //this.ShowDockPanelHidePanel(false);
                this.DockPanel.DockPanelManager.DockPanelHidePanel.Show(this.DockPanel, this);
            }
            else
            {
                this.DockPanel.DockPanelManager.DockPanelHidePanel.RefreshLayout();
            }
        }
        private void HideAreaTabButtonItem_MouseLeave(object sender, EventArgs e)
        {
            if (this.DockPanel.bActive) return;
            //
            System.Threading.Thread.Sleep(200);
            //
            if (this.DockPanel.DockPanelManager.DockPanelHidePanel.ContainsMousePoint(System.Windows.Forms.Form.MousePosition))
            {
                this.DockPanel.DockPanelManager.DockPanelHidePanel.StartTimer(); return;
            }
            //
            this.CloseDockPanelHidePanel();
        }

        #region WFNew.BaseItemStack
        public override bool CanExchangeItem
        {
            get
            {
                return false;
            }
            set
            {
                base.CanExchangeItem = value;
            }
        }

        public override bool LockWith
        {
            get
            {
                return this.eOrientation == Orientation.Horizontal ? true : base.LockWith;
            }
            set
            {
                base.LockWith = value;
            }
        }

        public override bool LockHeight
        {
            get
            {
                return this.eOrientation == Orientation.Vertical ? true : base.LockHeight;
            }
            set
            {
                base.LockHeight = value;
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
                base.IsRestrictItems = value;
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
                base.IsStretchItems = value;
            }
        }

        public override Orientation eOrientation
        {
            get
            {
                switch (this.TabAlignment)
                {
                    case TabAlignment.Top:
                    case TabAlignment.Bottom:
                        return Orientation.Horizontal;
                    case TabAlignment.Left:
                    case TabAlignment.Right:
                        return Orientation.Vertical;
                    default:
                        return base.eOrientation;
                }
            }
            set
            {
                base.eOrientation = value;
            }
        }

        public override Padding Padding
        {
            get
            {
                switch (this.TabAlignment)
                {
                    case TabAlignment.Top:
                    case TabAlignment.Bottom:
                        return new Padding(0, 0, CTR_HIDEAREATABBUTTONGROUPSPACE, 0);
                    case TabAlignment.Left:
                    case TabAlignment.Right:
                        return new Padding(0, 0, 0, CTR_HIDEAREATABBUTTONGROUPSPACE);
                    default:
                        return base.Padding;
                }
            }
            set
            {
                base.Padding = value;
            }
        }
        #endregion

        #region 属性
        [Browsable(false)]
        public System.Windows.Forms.TabAlignment TabAlignment
        {
            get { return m_TabAlignment; }
        }

        [Browsable(false)]
        public DockPanelHideArea DockPanelHideArea//记录其所在的隐藏区
        {
            get { return base.Parent as DockPanelHideArea; }
        }

        [Browsable(false)]
        public DockPanel DockPanel
        {
            get { return m_DockPanel; }
        }

        [Browsable(false)]
        public DockPanelManager DockPanelManager
        {
            get
            {
                if (this.m_DockPanel == null) return null;
                return this.m_DockPanel.DockPanelManager;
            }
        }
        #endregion

        public void ShowDockPanelHidePanel(bool bActive)//展现隐藏面板
        {
            if (bActive) ((ISetDockPanelHelper)this.DockPanel).SetActiveState(null);
            this.DockPanel.DockPanelManager.DockPanelHidePanel.Show(this.DockPanel, this);
        }

        public void CloseDockPanelHidePanel()//关闭隐藏面板
        {
            this.DockPanel.DockPanelManager.DockPanelHidePanel.Close();
        }
    }
}
