using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    class RibbonGalleryPopup : BasePopup, IPanelPopup
    {
        private RibbonGalleryMirrorPopupPanelItem m_RibbonGalleryMirrorPopupPanel = null;
        private ToolStripControlHost m_ToolStripControlHost = null;

        public RibbonGalleryPopup(IGalleryItem ribbonGalleryItem)
        {
            this.m_RibbonGalleryMirrorPopupPanel = new RibbonGalleryMirrorPopupPanelItem(ribbonGalleryItem);
            this.m_RibbonGalleryMirrorPopupPanel.LeftTopRadius = 5;
            this.m_RibbonGalleryMirrorPopupPanel.LeftBottomRadius = 6;
            this.m_RibbonGalleryMirrorPopupPanel.RightTopRadius = 6;
            this.m_RibbonGalleryMirrorPopupPanel.RightBottomRadius = 7;
            //
            this.m_ToolStripControlHost = new ToolStripControlHost(new BaseItemHost(this.m_RibbonGalleryMirrorPopupPanel));
            this.m_RibbonGalleryMirrorPopupPanel.Entity = this.m_ToolStripControlHost.Control;
            this.m_ToolStripControlHost.Dock = DockStyle.Fill;
            //this.m_ToolStripControlHost.BackColor = base.BackColor;
            this.m_ToolStripControlHost.Margin = new Padding(0);
            this.m_ToolStripControlHost.Padding = new Padding(0);
            base.Items.Add(this.m_ToolStripControlHost);
            //
            this.Margin = new Padding(0);
            this.Padding = new Padding(0);
            this.DropShadowEnabled = false;
            this.ShowItemToolTips = false;
            //
            ((ISetOwnerHelper)(this.m_ToolStripControlHost.Control)).SetOwner(this);
        }

        #region IPanelPopup
        public Size GetIdealSize()
        {
            Graphics g = Graphics.FromHwnd(this.m_ToolStripControlHost.Control.Handle);
            Size size = this.m_RibbonGalleryMirrorPopupPanel.GetIdealSize(g);
            g.Dispose();
            //
            return size;
        }

        public IPopupPanel GetPopupPanel() { return this.m_RibbonGalleryMirrorPopupPanel; }
        #endregion

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
                return true;
            }
        }

        public override int LeftTopRadius { get { return m_RibbonGalleryMirrorPopupPanel.LeftTopRadius; } }

        public override int RightTopRadius { get { return m_RibbonGalleryMirrorPopupPanel.RightTopRadius; } }

        public override int LeftBottomRadius { get { return m_RibbonGalleryMirrorPopupPanel.LeftBottomRadius; } }

        public override int RightBottomRadius { get { return m_RibbonGalleryMirrorPopupPanel.RightBottomRadius; } }
        #endregion
    }
}
