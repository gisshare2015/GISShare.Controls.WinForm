using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    [Designer(typeof(GISShare.Controls.WinForm.WFNew.Design.ContextPopupDesigner)), ToolboxItem(true)]
    public class ContextPopup : BasePopup, ISimplyPopup, ICollectionItem, ICollectionItem2, ICollectionObjectDesignHelper
    {
        private ContextPopupPanel m_ContextPopupPanel = null;
        private ToolStripControlHost m_ToolStripControlHost = null;

        #region 构造函数
        public ContextPopup()
        {
            this.m_ContextPopupPanel = new ContextPopupPanel();
            this.m_ContextPopupPanel.Padding = new Padding(2);
            this.m_ContextPopupPanel.LeftTopRadius = 6;
            this.m_ContextPopupPanel.LeftBottomRadius = 6;
            this.m_ContextPopupPanel.RightTopRadius = 6;
            this.m_ContextPopupPanel.RightBottomRadius = 6;
            this.m_ContextPopupPanel.LockWith = true;
            this.m_ContextPopupPanel.LockHeight = true;
            this.m_ContextPopupPanel.IsStretchItems = true;
            this.m_ContextPopupPanel.IsRestrictItems = true;
            this.m_ContextPopupPanel.PreButtonIncreaseIndex = false;
            this.m_ContextPopupPanel.Width = 10;
            this.m_ContextPopupPanel.Height = 20;
            this.m_ContextPopupPanel.ShowOutLine = true;
            //
            this.m_ToolStripControlHost = new ToolStripControlHost(this.m_ContextPopupPanel);
            this.m_ToolStripControlHost.Dock = DockStyle.Fill;
            this.m_ToolStripControlHost.BackColor = base.BackColor;
            this.m_ToolStripControlHost.Margin = new Padding(0);
            this.m_ToolStripControlHost.Padding = new Padding(0);
            base.Items.Add(this.m_ToolStripControlHost);
            //
            this.Margin = new Padding(0);
            this.Padding = new Padding(0);
            this.DropShadowEnabled = false;
            this.ShowItemToolTips = false;
            //
            ((ISetOwnerHelper)(this.m_ContextPopupPanel)).SetOwner(this);
        }

        //public ContextPopup(GISShare.Controls.Plugin.WFNew.IContextPopupP pBaseItemP)
        //    : this()
        //{
        //    //IPlugin
        //    this.Name = pBaseItemP.Name;
        //    //ISetEntityObject
        //    GISShare.Controls.Plugin.ISetEntityObject pSetEntityObject = pBaseItemP as GISShare.Controls.Plugin.ISetEntityObject;
        //    if (pSetEntityObject != null) pSetEntityObject.SetEntityObject(this);
        //    //IBaseItemP_
        //    this.Checked = pBaseItemP.Checked;
        //    this.Enabled = pBaseItemP.Enabled;
        //    this.Font = pBaseItemP.Font;
        //    this.ForeColor = pBaseItemP.ForeColor;
        //    //this.LockHeight = pBaseItemP.LockHeight;
        //    //this.LockWith = pBaseItemP.LockWith;
        //    this.Padding = pBaseItemP.Padding;
        //    this.Size = pBaseItemP.Size;
        //    this.Text = pBaseItemP.Text;
        //    this.Visible = pBaseItemP.Visible;
        //    //this.Category = pBaseItemP.Category;
        //    this.MinimumSize = pBaseItemP.MinimumSize;
        //    //this.UsingViewOverflow = pBaseItemP.UsingViewOverflow;
        //    //IContextPopupP
        //    this.eContextPopupStyle = pBaseItemP.eContextPopupStyle;
        //}
        #endregion

        #region ICollectionObjectDesignHelper
        System.Collections.IList ICollectionObjectDesignHelper.List { get { return this.BaseItems; } }

        bool ICollectionObjectDesignHelper.ExchangeItem(object item1, object item2) { return this.BaseItems.ExchangeItem(item1, item2); }
        #endregion

        [Browsable(true), DefaultValue(typeof(ContextPopupStyle), "eNormal"), Description("Popup面板展现方式"), Category("外观")]
        public ContextPopupStyle eContextPopupStyle
        {
            get { return this.m_ContextPopupPanel.eContextPopupStyle; }
            set { this.m_ContextPopupPanel.eContextPopupStyle = value; }
        }

        public new BaseItemCollection Items
        {
            get { return this.m_ContextPopupPanel.BaseItems; }
        }

        #region ICollectionItem
        public bool HaveVisibleBaseItem
        {
            get
            {
                foreach (BaseItem one in this.m_ContextPopupPanel.BaseItems)
                {
                    if (one.Visible) return true;
                }
                //
                return false;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Description("其所携带的子项集合"), Category("子项")]
        public BaseItemCollection BaseItems
        {
            get { return this.m_ContextPopupPanel.BaseItems; }
        }
        #endregion

        #region ICollectionItem2
        public IBaseItem GetBaseItem(string strName)
        {
            IBaseItem pBaseItem = null;
            foreach (IBaseItem one in this.BaseItems)
            {
                if (one.Name == strName) pBaseItem = one;
                else
                {
                    ICollectionItem2 pCollectionItem2 = one as ICollectionItem2;
                    if (pCollectionItem2 != null)
                    {
                        pBaseItem = pCollectionItem2.GetBaseItem(strName);
                    }
                }
                //
                if (pBaseItem != null) break;
            }
            //
            return null;
        }
        #endregion

        public Size GetIdealSize()
        {
            Graphics g = Graphics.FromHwnd(this.m_ContextPopupPanel.Handle);
            Size size = this.m_ContextPopupPanel.GetIdealSize(g);
            g.Dispose();
            //
            return size;
        }

        public Padding GetPadding()
        {
            return this.m_ContextPopupPanel.Padding;
        }

        public void SetPadding(int iPadding)
        {
            this.m_ContextPopupPanel.Padding = new Padding(iPadding);
        }

        public void SetPadding(int left, int top, int right, int bottom)
        {
            this.m_ContextPopupPanel.Padding = new Padding(left, top, right, bottom);
        }

        public void SetRadius(int iRadius)
        {
            this.m_ContextPopupPanel.LeftTopRadius = iRadius;
            this.m_ContextPopupPanel.LeftBottomRadius = iRadius;
            this.m_ContextPopupPanel.RightTopRadius = iRadius;
            this.m_ContextPopupPanel.RightBottomRadius = iRadius;
        }

        public void SetRadius(int iLeftTopRadius, int iLeftBottomRadius, int iRightTopRadius, int iRightBottomRadius)
        {
            this.m_ContextPopupPanel.LeftTopRadius = iLeftTopRadius;
            this.m_ContextPopupPanel.LeftBottomRadius = iLeftBottomRadius;
            this.m_ContextPopupPanel.RightTopRadius = iRightTopRadius;
            this.m_ContextPopupPanel.RightBottomRadius = iRightBottomRadius;
        }

        public void TrySetPopupPanelSize(Size size)
        {
            this.m_ToolStripControlHost.Size =
                new Size
                (
                this.m_ToolStripControlHost.Padding.Left + size.Width + this.m_ToolStripControlHost.Padding.Right,
                this.m_ToolStripControlHost.Padding.Top + size.Height + this.m_ToolStripControlHost.Padding.Bottom
                );
            this.Size =
                new Size
                (
                this.Padding.Left + size.Width + this.Padding.Right,
                this.Padding.Top + size.Height + this.Padding.Bottom
                );
            this.m_ContextPopupPanel.Size = size;
        }

        public void RefreshPopupPanel()
        {
            this.m_ContextPopupPanel.Refresh();
        }

        public IPopupPanel GetPopupPanel()
        { 
            return this.m_ContextPopupPanel;
        }

        #region Clone
        public override object Clone()
        {           
            return null;
        }
        #endregion

        #region Radius
        public override int LeftTopRadius { get { return m_ContextPopupPanel.LeftTopRadius; } }

        public override int RightTopRadius { get { return m_ContextPopupPanel.RightTopRadius; } }

        public override int LeftBottomRadius { get { return m_ContextPopupPanel.LeftBottomRadius; } }

        public override int RightBottomRadius { get { return m_ContextPopupPanel.RightBottomRadius; } }
        #endregion

        protected override bool Filtration()
        {
            if (this.HaveVisibleBaseItem) 
            {
                this.m_ContextPopupPanel.TopViewItemIndex = 0;
                this.m_ContextPopupPanel.MaxSize = System.Windows.Forms.SystemInformation.WorkingArea.Height;
                return true; 
            }
            else
            {
                return false;
            }
        }

    }
}
