using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    class RibbonBarPopup : BasePopup, IPanelPopup
    {
        private RibbonBarMirrorPopupPanel m_RibbonBarMirrorPopupPanel = null;
        private ToolStripControlHost m_ToolStripControlHost = null;

        public RibbonBarPopup(IRibbonBarItem ribbonBarItem)
        {
            this.m_RibbonBarMirrorPopupPanel = new RibbonBarMirrorPopupPanel(ribbonBarItem);
            //
            this.m_ToolStripControlHost = new ToolStripControlHost(this.m_RibbonBarMirrorPopupPanel);
            this.m_ToolStripControlHost.Dock = DockStyle.Fill;
            //this.m_ToolStripControlHost.BackColor = base.BackColor;
            this.m_ToolStripControlHost.Margin = new Padding(0);
            this.m_ToolStripControlHost.Padding = new Padding(0);
            base.Items.Add(this.m_ToolStripControlHost);
            //
            this.Margin = new Padding(0);
            this.Padding = new Padding(3);
            this.DropShadowEnabled = false;
            this.ShowItemToolTips = false;
            //
            ((ISetOwnerHelper)(this.m_RibbonBarMirrorPopupPanel)).SetOwner(this);
        }

        #region IPanelPopup
        public Size GetIdealSize()
        {
            Graphics g = Graphics.FromHwnd(this.m_RibbonBarMirrorPopupPanel.Handle);
            Size size = this.m_RibbonBarMirrorPopupPanel.GetIdealSize(g);
            g.Dispose();
            //
            return size;
        }

        public IPopupPanel GetPopupPanel() { return this.m_RibbonBarMirrorPopupPanel; }
        #endregion

        public override bool CustomFiltration
        {
            get
            {
                return true;
            }
        }

        public override bool Filtration(MouseEventArgs e)
        {
            return this.OwnerContainMousePoint(e.Location) || base.Filtration(e);
        }
        private bool OwnerContainMousePoint(Point point)
        {
            if (this.pOwner == null) return false;
            //
            IPopupOwner pPopupOwner = this.pOwner as IPopupOwner;
            if (pPopupOwner != null) return pPopupOwner.PopupTriggerRectangle.Contains(this.pOwner.PointToClient(point));
            else return this.pOwner.DisplayRectangle.Contains(this.pOwner.PointToClient(point));
            //if (this.pOwner == null) return false;
            ////
            //return this.pOwner.DisplayRectangle.Contains(this.pOwner.PointToClient(point));
        }

        #region Clone
        public override object Clone()
        {
            return null;
        }
        #endregion

        #region Radius
        public override bool UseRadius
        {
            get
            {
                return false;
            }
        }

        public override int LeftTopRadius { get { return m_RibbonBarMirrorPopupPanel.LeftTopRadius; } }

        public override int RightTopRadius { get { return m_RibbonBarMirrorPopupPanel.RightTopRadius; } }

        public override int LeftBottomRadius { get { return m_RibbonBarMirrorPopupPanel.LeftBottomRadius; } }

        public override int RightBottomRadius { get { return m_RibbonBarMirrorPopupPanel.RightBottomRadius; } }
        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            //
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonBarPopup
                (
                new GISShare.Controls.WinForm.ObjectRenderEventArgs(e.Graphics, this, new Rectangle(0, 0, this.Width, this.Height))
                );
        }

    }
}
