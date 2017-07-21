using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew.DockPanel
{
    class DockPanelButtonItem : WFNew.ButtonItem, IDockPanelButtonItem
    {
        public DockPanelButtonItem(DockPanelButtonItemStyle dockPanelButtonItemStyle)
            : base(new DockPanelPopup())
        {
            base.Size = new Size(19, base.Size.Height);
            //
            this.m_eDockPanelButtonItemStyle = dockPanelButtonItemStyle;
        }

        private DockPanelButtonItemStyle m_eDockPanelButtonItemStyle = DockPanelButtonItemStyle.eCloseButton;
        public DockPanelButtonItemStyle eDockPanelButtonItemStyle
        {
            get
            {
                return m_eDockPanelButtonItemStyle;
            }
        }

        public Rectangle GlyphRectangle
        {
            get
            {
                return Util.UtilTX.CreateRectangle(this.DisplayRectangle, 15, 15);
            }
        }

        public bool IsHideState//获取隐藏状态
        {
            get
            {
                if (this.pOwner == null || this.pOwner.pOwner == null) return false;
                DockPanel dockPanel = this.pOwner.pOwner as DockPanel;
                if (dockPanel == null) return false;
                return dockPanel.IsHideState;
            }
        }

        public override bool Visible
        {
            get
            {
                switch (this.eDockPanelButtonItemStyle)
                {
                    case DockPanelButtonItemStyle.eContextButton:
                        return true;
                    case DockPanelButtonItemStyle.eHideButton:
                        if (this.pOwner == null || this.pOwner.pOwner == null) return base.Visible;
                        DockPanel dockPanel = this.pOwner.pOwner as DockPanel;
                        if (dockPanel == null) return base.Visible;
                        if (!dockPanel.CanHide) return false;
                        DockAreaStyle eDockAreaStyle = dockPanel.GetDockAreaStyle();
                        return eDockAreaStyle == DockAreaStyle.eNone || eDockAreaStyle == DockAreaStyle.eDockPanelDockArea;
                    case DockPanelButtonItemStyle.eCloseButton:
                        if (this.pOwner == null || this.pOwner.pOwner == null) return base.Visible;
                        DockPanel dockPanel2 = this.pOwner.pOwner as DockPanel;
                        if (dockPanel2 == null) return base.Visible;
                        BasePanel basePanel = dockPanel2.SelectedBasePanel;
                        if (basePanel == null) return base.Visible;
                        return basePanel.CanClose;
                    default:
                        return base.Visible;
                }
            }
            set
            {
                base.Visible = value;
            }
        }

        public override ButtonStyle eButtonStyle
        {
            get
            {
                return this.eDockPanelButtonItemStyle == DockPanelButtonItemStyle.eContextButton ? ButtonStyle.eDropDownButton : ButtonStyle.eButton;
            }
            set
            {
                base.eButtonStyle = value;
            }
        }

        public override WFNew.DisplayStyle eDisplayStyle
        {
            get
            {
                return WFNew.DisplayStyle.eNone;
            }
            set
            {
                base.eDisplayStyle = WFNew.DisplayStyle.eNone;
            }
        }

        public override System.Drawing.ContentAlignment TextAlign
        {
            get
            {
                return System.Drawing.ContentAlignment.MiddleCenter;
            }
            set
            {
                base.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            }
        }

        public override bool ShowNomalState
        {
            get
            {
                return false;
            }
            set
            {
                base.ShowNomalState = value;
            }
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            DockPanelPopup dockPanelPopup = ((IPopupOwnerHelper)this).GetBasePopup() as DockPanelPopup;
            if (dockPanelPopup != null) dockPanelPopup.SetDockPanel(this.pOwner.pOwner as DockPanel);
            //
            base.OnMouseDown(mevent);
        }

        protected override void OnMouseClick(System.Windows.Forms.MouseEventArgs e)
        {
            base.OnMouseClick(e);
            //
            if (this.pOwner == null || this.pOwner.pOwner == null) return;
            DockPanel dockPanel = this.pOwner.pOwner as DockPanel;
            if (dockPanel == null) return;
            //
            switch (this.eDockPanelButtonItemStyle) 
            {
                case DockPanelButtonItemStyle.eHideButton:
                    ((ISetDockPanelHelper)dockPanel).SetHideState(!dockPanel.IsHideState);
                    break;
                case DockPanelButtonItemStyle.eCloseButton:
                    if (dockPanel.BasePanels.Count <= 0)
                    {
                        GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show("TabPages 中不存在对象！");
                        dockPanel.VisibleEx = false;
                        //
                        if (dockPanel.DockPanelManager != null &&
                            !dockPanel.DockPanelManager.DockPanels.Contains(dockPanel))
                        { dockPanel.Dispose(); }
                    }
                    else if (dockPanel.BasePanels.Count == 1)
                    {
                        dockPanel.Close();// = false;
                    }
                    else
                    {
                        if (dockPanel.IsHideState) //如果是 隐藏面板 则 关闭
                        { dockPanel.Close(); }
                        else
                        { dockPanel.BasePanels.RemoveAt(dockPanel.BasePanelSelectedIndex); }
                    }
                    break;
                case DockPanelButtonItemStyle.eContextButton:
                default:
                    break;
            }
        }

        protected override void OnDraw(PaintEventArgs pevent)
        {
            //base.OnDraw(pevent);
            WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderDockPanelButton(new ObjectRenderEventArgs(pevent.Graphics, this, this.DisplayRectangle));
        }
    }
    
    [ToolboxItem(false)]
    class DockPanelPopup : BaseDockPanelPopup
    {
        protected override void SetPopupItem()
        {
            this.BaseItems.Clear();
            //
            //
            //
            DockPanel dockPanel = this._pDockPanel as DockPanel;
            if (dockPanel == null) return;
            //
            bool bCanDockUp = true;
            bool bCanDockLeft = true;
            bool bCanDockRight = true;
            bool bCanDockBottom = true;
            bool bCanDockFill = true;
            bool bCanFloat = true;
            bool bCanHide = true;
            bool bCanClose = true;
            bool bIsBasePanel = true;
            bool bIsDocumentPanel = true;
            base._pDockPanel.GetDockLicense(ref bCanDockUp, ref bCanDockLeft, ref bCanDockRight, ref bCanDockBottom, ref bCanDockFill, ref bCanFloat, ref bCanHide, ref bCanClose, ref bIsBasePanel, ref bIsDocumentPanel);
            DockAreaStyle eDockAreaStyle = base._pDockPanel.GetDockAreaStyle();
            DockPanelContainerStyle eDockPanelContainerStyle = base._pDockPanel.GetDockPanelContainerStyle();
            //
            //
            //
            if (dockPanel.IsHideState)
            {
                this.CreateAddDockPanelManagerBaseItem(base.BaseItems);
            }
            else
            {
                base.CreateSelectedBasePanelBaseItemList(base.BaseItems);
                //
                if (eDockPanelContainerStyle == DockPanelContainerStyle.eDockPanelFloatForm)
                {
                    base.CreateAddDockPanelBaseItemList(base.BaseItems, bCanDockUp, bCanDockLeft, bCanDockRight, bCanDockBottom, bCanDockFill);//, this._pDockPanel.GetDockAreaStyle()
                }
                //
                if (eDockAreaStyle == DockAreaStyle.eDockPanelDockArea || eDockPanelContainerStyle == DockPanelContainerStyle.eDockPanelFloatForm)
                {
                    this.CreateAddDockPanelManagerBaseItem(base.BaseItems, eDockAreaStyle, eDockPanelContainerStyle, bCanDockUp, bCanDockLeft, bCanDockRight, bCanDockBottom, bCanFloat, bCanHide, bIsBasePanel, bIsDocumentPanel);
                }
            }
            //
            //
            //
            WFNew.SeparatorItem toolStripSeparator = new WFNew.SeparatorItem();
            this.BaseItems.Add(toolStripSeparator);
            base.CreateDockPanelCustomizeBaseItem(this.BaseItems);
        }

        private void CreateAddDockPanelManagerBaseItem(WFNew.BaseItemCollection items)
        {
            WFNew.DropDownButtonItem ribbonDropDownButtonItem = new WFNew.DropDownButtonItem();
            ribbonDropDownButtonItem.Text = Language.LanguageStrategy.LayoutManagerText;//"布局管理";
            this.CreateHideDockPanelBaseItem(ribbonDropDownButtonItem.BaseItems, false, true);
            items.Add(ribbonDropDownButtonItem);
        }

        private void CreateAddDockPanelManagerBaseItem(WFNew.BaseItemCollection items,
            DockAreaStyle eDockAreaStyle, DockPanelContainerStyle eDockPanelContainerStyle,
            bool bCanDockUp, bool bCanDockLeft, bool bCanDockRight, bool bCanDockBottom, bool bCanFloat, bool bCanHide, bool bIsBasePanel, bool bIsDocumentPanel)
        {
            WFNew.SeparatorItem toolStripSeparator = new WFNew.SeparatorItem();
            items.Add(toolStripSeparator);
            WFNew.DropDownButtonItem ribbonDropDownButtonItem = new WFNew.DropDownButtonItem();
            ribbonDropDownButtonItem.Text = Language.LanguageStrategy.LayoutManagerText;//"布局管理";
            items.Add(ribbonDropDownButtonItem);
            //
            if (eDockPanelContainerStyle == DockPanelContainerStyle.eDockPanelFloatForm)
            {
                WFNew.DropDownButtonItem ribbonDropDownButtonItemInternal = new WFNew.DropDownButtonItem();
                ribbonDropDownButtonItemInternal.Text = Language.LanguageStrategy.InternalText;//"内部";
                ribbonDropDownButtonItemInternal.Tag = ribbonDropDownButtonItem.Tag;
                ribbonDropDownButtonItem.BaseItems.Add(ribbonDropDownButtonItemInternal);
                base.CreateAddDockPanelManagerBaseItemInternal(ribbonDropDownButtonItemInternal.BaseItems, bCanDockUp, bCanDockLeft, bCanDockRight, bCanDockBottom, bIsBasePanel);
                //
                WFNew.DropDownButtonItem ribbonDropDownButtonItemOut = new WFNew.DropDownButtonItem();
                ribbonDropDownButtonItemOut.Text = Language.LanguageStrategy.OuterText;//"外围";
                ribbonDropDownButtonItemOut.Tag = ribbonDropDownButtonItem.Tag;
                ribbonDropDownButtonItem.BaseItems.Add(ribbonDropDownButtonItemOut);
                base.CreateAddDockPanelManagerBaseItemOut(ribbonDropDownButtonItemOut.BaseItems, bCanDockUp, bCanDockLeft, bCanDockRight, bCanDockBottom, bIsBasePanel);
                //
                ribbonDropDownButtonItem.BaseItems.Add(new WFNew.SeparatorItem());
                //
                WFNew.DropDownButtonItem ribbonDropDownButtonItemDocument = new WFNew.DropDownButtonItem();
                ribbonDropDownButtonItemDocument.Text = Language.LanguageStrategy.DocumentAreaText;//"文档区";
                ribbonDropDownButtonItemDocument.Tag = ribbonDropDownButtonItem.Tag;
                ribbonDropDownButtonItem.BaseItems.Add(ribbonDropDownButtonItemDocument);
                base.CreateAddDockPanelManagerBaseItemDocument(ribbonDropDownButtonItemDocument.BaseItems, bIsDocumentPanel);
            }
            //
            if (eDockAreaStyle == DockAreaStyle.eDockPanelDockArea)
            {
                base.CreateHideDockPanelBaseItem(ribbonDropDownButtonItem.BaseItems, true, bCanHide);
                base.CreateAddDockPanelManagerBaseItemFloat(ribbonDropDownButtonItem.BaseItems, bCanFloat);
            }
        }
    }
}
