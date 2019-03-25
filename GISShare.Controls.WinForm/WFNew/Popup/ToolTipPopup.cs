using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace GISShare.Controls.WinForm.WFNew
{
    class ToolTipPopup : BasePopup, IToolTipPopup
    {
        private ToolStripControlHost m_ToolStripControlHost = null;
        private ToolTipPopupPanelItem m_ToolTipPopupPanel;

        public ToolTipPopup()
        {
            this.m_ToolTipPopupPanel = new ToolTipPopupPanelItem();
            this.m_ToolTipPopupPanel.Padding = new Padding(2);
            this.m_ToolTipPopupPanel.Size = new System.Drawing.Size(18, 18);
            //this.m_ToolTipPopupPanel.Dock = DockStyle.Fill;
            this.m_ToolTipPopupPanel.LeftTopRadius = 6;
            this.m_ToolTipPopupPanel.LeftBottomRadius = 6;
            this.m_ToolTipPopupPanel.RightTopRadius = 6;
            this.m_ToolTipPopupPanel.RightBottomRadius = 6;
            this.m_ToolTipPopupPanel.LockWith = true;
            this.m_ToolTipPopupPanel.LockHeight = true;
            this.m_ToolTipPopupPanel.IsStretchItems = true;
            this.m_ToolTipPopupPanel.IsRestrictItems = true;
            //
            this.m_ToolStripControlHost = new ToolStripControlHost(
                new BaseItemHost(this.m_ToolTipPopupPanel) {
                    Width = 10, 
                    Height = 20
                }
            );
            this.m_ToolTipPopupPanel.Entity = this.m_ToolStripControlHost.Control;
            this.m_ToolStripControlHost.Dock = DockStyle.Fill;
            this.m_ToolStripControlHost.BackColor = base.BackColor;
            this.m_ToolStripControlHost.Margin = new Padding(0);
            this.m_ToolStripControlHost.Padding = new Padding(0);
            this.Items.Add(this.m_ToolStripControlHost);
            //
            this.MinimumSize = new Size(18, 18);
            this.Margin = new Padding(0);
            this.Padding = new Padding(0);
            this.DropShadowEnabled = false;
            this.ShowItemToolTips = false;
            //
            ((ISetOwnerHelper)this).SetOwner((IOwner)this.m_ToolStripControlHost.Control);
        }

        public Size GetIdealSize()
        {
            Graphics g = Graphics.FromHwnd(this.m_ToolStripControlHost.Control.Handle);
            Size size = this.m_ToolTipPopupPanel.GetIdealSize(g);
            g.Dispose();
            //
            return size;
        }

        public IPopupPanel GetPopupPanel()
        {
            return this.m_ToolTipPopupPanel;
        }

        #region Clone
        public override object Clone()
        {
            return null;
        }
        #endregion

        //#region Radius
        //public override bool UseRadius
        //{
        //    get
        //    {
        //        return false;
        //    }
        //}

        //public override int LeftTopRadius { get { return 0; } }

        //public override int RightTopRadius { get { return 0; } }

        //public override int LeftBottomRadius { get { return 0; } }

        //public override int RightBottomRadius { get { return 0; } }
        //#endregion

        #region Radius
        public override int LeftTopRadius { get { return m_ToolTipPopupPanel.LeftTopRadius; } }

        public override int RightTopRadius { get { return m_ToolTipPopupPanel.RightTopRadius; } }

        public override int LeftBottomRadius { get { return m_ToolTipPopupPanel.LeftBottomRadius; } }

        public override int RightBottomRadius { get { return m_ToolTipPopupPanel.RightBottomRadius; } }
        #endregion

        #region IPanelPopup
        public bool SetTipInfo(ITipInfo pTipInfo)
        {
            return this.m_ToolTipPopupPanel.SetTipInfo(pTipInfo); 
        }
        #endregion

        public override bool Filtration(MouseEventArgs e)
        {
            return true;
        }
    }
}
