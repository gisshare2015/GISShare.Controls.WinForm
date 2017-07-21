using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew.DockPanel
{
    abstract class BaseDockPanelPopup : WFNew.ContextPopup
    {
        protected IDockPanel _pDockPanel = null;//当前的IDockPanel

        public BaseDockPanelPopup()
            : base()
        { }

        public BaseDockPanelPopup(IDockPanel pDockPanel)
            : base()
        {
            this._pDockPanel = pDockPanel;
        }

        internal void SetDockPanel(IDockPanel pDockPanel)
        {
            if (this._pDockPanel == pDockPanel) return;
            this._pDockPanel = pDockPanel;
        }

        protected override bool Filtration()
        {
            if (this._pDockPanel != null)
            {
                this.SetPopupItem();
            }
            //
            return base.Filtration();
        }

        /// <summary>
        /// 设置快捷菜单项
        /// </summary>
        protected abstract void SetPopupItem();

        //
        // 设置当前选择的BasePanel
        //
        protected void CreateSelectedBasePanelBaseItemList(WFNew.BaseItemCollection baseItems)
        {
            DockPanel dockPanel = this._pDockPanel as DockPanel;
            if (dockPanel == null || dockPanel.BasePanels.Count < 1) return;
            //if (dockPanel == null || dockPanel.TabButtonList.BaseItems.Count < 1) return;
            for (int i = 0; i < dockPanel.BasePanels.Count; i++)
            {
                BasePanel temp = dockPanel.BasePanels[i] as BasePanel;
                if (temp == null) continue;
                WFNew.DropDownButtonItem ribbonDropDownButtonItem = new WFNew.DropDownButtonItem();
                ribbonDropDownButtonItem.Name = temp.Name;
                ribbonDropDownButtonItem.Text = temp.Text;
                ribbonDropDownButtonItem.Image = temp.Image;
                ribbonDropDownButtonItem.Tag = i;
                ribbonDropDownButtonItem.Checked = temp.IsSelected;
                ribbonDropDownButtonItem.MouseClick += new MouseEventHandler(BaseItemSelectedBasePanel_MouseClick);
                baseItems.Add(ribbonDropDownButtonItem);
            }
        }

        private void BaseItemSelectedBasePanel_MouseClick(object sender, MouseEventArgs e)
        {
            WFNew.DropDownButtonItem ribbonDropDownButtonItem = sender as WFNew.DropDownButtonItem;
            DockPanel dockPanel = this._pDockPanel as DockPanel;
            if (dockPanel == null || 
                ribbonDropDownButtonItem == null ||
                ribbonDropDownButtonItem.Checked) return;
            dockPanel.BasePanelSelectedIndex = (int)ribbonDropDownButtonItem.Tag;
        }

        //
        // 在一系列可用面板内停靠
        //
        protected void CreateAddDockPanelBaseItemList(WFNew.BaseItemCollection baseItems,
            bool bCanDockUp, bool bCanDockLeft, bool bCanDockRight, bool bCanDockBottom, bool bCanDockFill)//, DockAreaStyle eDockAreaStyle
        {
            if (this._pDockPanel.DockPanelManager != null &&
                this._pDockPanel.DockPanelManager.GetDockPanelCountFromVisible(true, true) > 1)
            {
                WFNew.SeparatorItem toolStripSeparator = new WFNew.SeparatorItem();
                this.BaseItems.Add(toolStripSeparator);
                for (int j = 0; j < this._pDockPanel.DockPanelManager.DockPanels.Count; j++)
                {
                    DockPanel dockPanel = this._pDockPanel.DockPanelManager.DockPanels[j];
                    if (dockPanel == null) continue;
                    if (dockPanel.BasePanels.Count < 1) continue;
                    if (!dockPanel.GetVisible()) continue;
                    if (dockPanel == this._pDockPanel) continue;
                    //
                    WFNew.DropDownButtonItem ribbonDropDownButtonItem = new WFNew.DropDownButtonItem();
                    ribbonDropDownButtonItem.Name = dockPanel.Name;
                    ribbonDropDownButtonItem.Text = Language.LanguageStrategy.LayoutToText + Language.LanguageStrategy.DoubleQuotationMarks_Left + dockPanel.Text + Language.LanguageStrategy.DoubleQuotationMarks_Right;
                    ribbonDropDownButtonItem.Image = dockPanel.Image;
                    ribbonDropDownButtonItem.Tag = j;
                    ribbonDropDownButtonItem.MouseClick += new MouseEventHandler(BaseItemSelectedBasePanel_MouseClick);
                    DockAreaStyle eDockAreaStyle = dockPanel.GetDockAreaStyle();
                    this.CreateAddDockPanelBaseItem(ribbonDropDownButtonItem,
                        bCanDockUp, bCanDockLeft, bCanDockRight, bCanDockBottom, bCanDockFill,
                        ((eDockAreaStyle == DockAreaStyle.eDockPanelDockArea || eDockAreaStyle == DockAreaStyle.eDockPanelFloatForm) && dockPanel.IsBasePanel) || (eDockAreaStyle == DockAreaStyle.eDocumentDockArea && dockPanel.IsDocumentPanel));
                    baseItems.Add(ribbonDropDownButtonItem);
                }
            } 
        }

        private void CreateAddDockPanelBaseItem(WFNew.DropDownButtonItem ribbonDropDownButtonItem,
            bool bCanDockUp, bool bCanDockLeft, bool bCanDockRight, bool bCanDockBottom, bool bCanDockFill, bool bParameter)
        {
            WFNew.DropDownButtonItem ribbonDropDownButtonItem_Top = new WFNew.DropDownButtonItem();
            ribbonDropDownButtonItem_Top.Name = "ribbonDropDownButtonItem_Top";
            ribbonDropDownButtonItem_Top.Text = Language.LanguageStrategy.LayoutToTopText;// "顶部停靠";
            ribbonDropDownButtonItem_Top.Enabled = bParameter && bCanDockUp;
            ribbonDropDownButtonItem_Top.Tag = ribbonDropDownButtonItem.Tag;
            ribbonDropDownButtonItem_Top.MouseClick += new MouseEventHandler(BaseItemAddDockPanelTop_MouseClick);
            ribbonDropDownButtonItem.BaseItems.Add(ribbonDropDownButtonItem_Top);
            WFNew.DropDownButtonItem ribbonDropDownButtonItem_Left = new WFNew.DropDownButtonItem();
            ribbonDropDownButtonItem_Left.Name = "ribbonDropDownButtonItem_Left";
            ribbonDropDownButtonItem_Left.Text = Language.LanguageStrategy.LayoutToLeftText;// "左边停靠";
            ribbonDropDownButtonItem_Left.Enabled = bParameter && bCanDockLeft;
            ribbonDropDownButtonItem_Left.Tag = ribbonDropDownButtonItem.Tag;
            ribbonDropDownButtonItem_Left.MouseClick += new MouseEventHandler(BaseItemAddDockPanelLeft_MouseClick);
            ribbonDropDownButtonItem.BaseItems.Add(ribbonDropDownButtonItem_Left);
            WFNew.DropDownButtonItem ribbonDropDownButtonItem_Right = new WFNew.DropDownButtonItem();
            ribbonDropDownButtonItem_Right.Name = "ribbonDropDownButtonItem_Right";
            ribbonDropDownButtonItem_Right.Text = Language.LanguageStrategy.LayoutToRightText;// "右边停靠";
            ribbonDropDownButtonItem_Right.Enabled = bParameter && bCanDockRight;
            ribbonDropDownButtonItem_Right.Tag = ribbonDropDownButtonItem.Tag;
            ribbonDropDownButtonItem_Right.MouseClick += new MouseEventHandler(BaseItemAddDockPanelRight_MouseClick);
            ribbonDropDownButtonItem.BaseItems.Add(ribbonDropDownButtonItem_Right);
            WFNew.DropDownButtonItem ribbonDropDownButtonItem_Bottom = new WFNew.DropDownButtonItem();
            ribbonDropDownButtonItem_Bottom.Name = "ribbonDropDownButtonItem_Bottom";
            ribbonDropDownButtonItem_Bottom.Text = Language.LanguageStrategy.LayoutToBottomText;// "底部停靠";
            ribbonDropDownButtonItem_Bottom.Enabled = bParameter && bCanDockBottom;
            ribbonDropDownButtonItem_Bottom.Tag = ribbonDropDownButtonItem.Tag;
            ribbonDropDownButtonItem_Bottom.MouseClick += new MouseEventHandler(BaseItemAddDockPanelBottom_MouseClick);
            ribbonDropDownButtonItem.BaseItems.Add(ribbonDropDownButtonItem_Bottom);
            //
            WFNew.SeparatorItem toolStripSeparator = new WFNew.SeparatorItem();
            ribbonDropDownButtonItem.BaseItems.Add(toolStripSeparator);
            //
            WFNew.DropDownButtonItem ribbonDropDownButtonItem_Fill = new WFNew.DropDownButtonItem();
            ribbonDropDownButtonItem_Fill.Name = "ribbonDropDownButtonItem_Fill";
            ribbonDropDownButtonItem_Fill.Text = Language.LanguageStrategy.LayoutToFillText;// "填充面板";
            ribbonDropDownButtonItem_Fill.Enabled = bParameter && bCanDockFill;
            ribbonDropDownButtonItem_Fill.Tag = ribbonDropDownButtonItem.Tag;
            ribbonDropDownButtonItem_Fill.MouseClick += new MouseEventHandler(BaseItemAddDockPanelFill_MouseClick);
            ribbonDropDownButtonItem.BaseItems.Add(ribbonDropDownButtonItem_Fill);
        }

        private void BaseItemAddDockPanelTop_MouseClick(object sender, MouseEventArgs e)
        {
            WFNew.DropDownButtonItem ribbonDropDownButtonItem = sender as WFNew.DropDownButtonItem;
            if (ribbonDropDownButtonItem == null || ribbonDropDownButtonItem.Checked) return;
            this._pDockPanel.DockPanelManager.DockPanels[(int)ribbonDropDownButtonItem.Tag].AddDockPanel(this._pDockPanel, DockStyle.Top);
        }

        private void BaseItemAddDockPanelLeft_MouseClick(object sender, MouseEventArgs e)
        {
            WFNew.DropDownButtonItem ribbonDropDownButtonItem = sender as WFNew.DropDownButtonItem;
            if (ribbonDropDownButtonItem == null || ribbonDropDownButtonItem.Checked) return;
            this._pDockPanel.DockPanelManager.DockPanels[(int)ribbonDropDownButtonItem.Tag].AddDockPanel(this._pDockPanel, DockStyle.Left);
        }

        private void BaseItemAddDockPanelRight_MouseClick(object sender, MouseEventArgs e)
        {
            WFNew.DropDownButtonItem ribbonDropDownButtonItem = sender as WFNew.DropDownButtonItem;
            if (ribbonDropDownButtonItem == null || ribbonDropDownButtonItem.Checked) return;
            this._pDockPanel.DockPanelManager.DockPanels[(int)ribbonDropDownButtonItem.Tag].AddDockPanel(this._pDockPanel, DockStyle.Right);
        }

        private void BaseItemAddDockPanelBottom_MouseClick(object sender, MouseEventArgs e)
        {
            WFNew.DropDownButtonItem ribbonDropDownButtonItem = sender as WFNew.DropDownButtonItem;
            if (ribbonDropDownButtonItem == null || ribbonDropDownButtonItem.Checked) return;
            this._pDockPanel.DockPanelManager.DockPanels[(int)ribbonDropDownButtonItem.Tag].AddDockPanel(this._pDockPanel, DockStyle.Bottom);
        }

        private void BaseItemAddDockPanelFill_MouseClick(object sender, MouseEventArgs e)
        {
            WFNew.DropDownButtonItem ribbonDropDownButtonItem = sender as WFNew.DropDownButtonItem;
            if (ribbonDropDownButtonItem == null || ribbonDropDownButtonItem.Checked) return;
            this._pDockPanel.DockPanelManager.DockPanels[(int)ribbonDropDownButtonItem.Tag].AddDockPanel(this._pDockPanel, DockStyle.Fill);
        }

        //
        // 设置自动隐藏
        //
        protected void CreateHideDockPanelBaseItem(WFNew.BaseItemCollection baseItems, bool bIsHideDockPanel, bool bCanHide)
        {
            WFNew.DropDownButtonItem ribbonDropDownButtonItem = null;
            if (bIsHideDockPanel)
            {
                ribbonDropDownButtonItem = new WFNew.DropDownButtonItem();
                ribbonDropDownButtonItem.Name = "ribbonDropDownButtonItem";
                ribbonDropDownButtonItem.Text = Language.LanguageStrategy.HideDockPanelText;// "隐藏面板";
                ribbonDropDownButtonItem.Enabled = bCanHide;
            }
            else
            {
                ribbonDropDownButtonItem = new WFNew.DropDownButtonItem();
                ribbonDropDownButtonItem.Name = "ribbonDropDownButtonItem";
                ribbonDropDownButtonItem.Text = Language.LanguageStrategy.ShowDockPanelText;// "展现面板";
            }
            ribbonDropDownButtonItem.MouseClick += new MouseEventHandler(BaseItemHideDockPanel_MouseClick);
            baseItems.Add(ribbonDropDownButtonItem);
        }

        private void BaseItemHideDockPanel_MouseClick(object sender, MouseEventArgs e)
        {
            DockPanel dockPanel = this._pDockPanel as DockPanel;
            if (dockPanel == null) return;
            //dockPanel.ClickHideButton();
            ((ISetDockPanelHelper)dockPanel).SetHideState(!dockPanel.IsHideState);
        }

        //
        // 浮动窗体
        //
        protected void CreateAddDockPanelManagerBaseItemFloat(WFNew.BaseItemCollection baseItems, bool bCanFloat)
        {
            WFNew.DropDownButtonItem ribbonDropDownButtonItem_Float = new WFNew.DropDownButtonItem();
            ribbonDropDownButtonItem_Float.Name = "ribbonDropDownButtonItem_Float";
            ribbonDropDownButtonItem_Float.Text = Language.LanguageStrategy.FloatDockPanelText;// "浮动窗体";
            ribbonDropDownButtonItem_Float.Enabled = bCanFloat;
            ribbonDropDownButtonItem_Float.MouseClick += new MouseEventHandler(BaseItemToDockPanelFloatForm_MouseClick);
            baseItems.Add(ribbonDropDownButtonItem_Float);
        }

        private void BaseItemToDockPanelFloatForm_MouseClick(object sender, MouseEventArgs e)
        {
            WFNew.DropDownButtonItem ribbonDropDownButtonItem = sender as WFNew.DropDownButtonItem;
            if (ribbonDropDownButtonItem == null || ribbonDropDownButtonItem.Checked) return;
            //System.Threading.Thread.Sleep(2000);
            this._pDockPanel.ToDockPanelFloatForm();
        }


        //
        // 文档区
        //
        protected void CreateAddDockPanelManagerBaseItemDocument(WFNew.BaseItemCollection baseItems, bool bIsDocumentPanel)
        {
            WFNew.DropDownButtonItem ribbonDropDownButtonItem_Document = new WFNew.DropDownButtonItem();
            ribbonDropDownButtonItem_Document.Name = "ribbonDropDownButtonItem_Document";
            ribbonDropDownButtonItem_Document.Text = Language.LanguageStrategy.DocumentAreaText;// "文档区";
            ribbonDropDownButtonItem_Document.Enabled = bIsDocumentPanel;
            ribbonDropDownButtonItem_Document.MouseClick += new MouseEventHandler(BaseItemToDockAreaDocument_MouseClick);
            baseItems.Add(ribbonDropDownButtonItem_Document);
        }

        private void BaseItemToDockAreaDocument_MouseClick(object sender, MouseEventArgs e)
        {
            WFNew.DropDownButtonItem ribbonDropDownButtonItem = sender as WFNew.DropDownButtonItem;
            if (ribbonDropDownButtonItem == null || ribbonDropDownButtonItem.Checked) return;
            this._pDockPanel.ToDockArea(true, DockStyle.Fill);
        }

        //
        // 内部停靠
        //
        protected void CreateAddDockPanelManagerBaseItemInternal(WFNew.BaseItemCollection baseItems,
            bool bCanDockUp, bool bCanDockLeft, bool bCanDockRight, bool bCanDockBottom, bool bIsBasePanel)
        {
            WFNew.DropDownButtonItem ribbonDropDownButtonItem_Top = new WFNew.DropDownButtonItem();
            ribbonDropDownButtonItem_Top.Name = "ribbonDropDownButtonItem_Top";
            ribbonDropDownButtonItem_Top.Text = Language.LanguageStrategy.LayoutToTopText;//  "顶部停靠";
            ribbonDropDownButtonItem_Top.Enabled = bIsBasePanel && bCanDockUp;
            ribbonDropDownButtonItem_Top.MouseClick += new MouseEventHandler(BaseItemToDockAreaInternalTop_MouseClick);
            baseItems.Add(ribbonDropDownButtonItem_Top);
            WFNew.DropDownButtonItem ribbonDropDownButtonItem_Left = new WFNew.DropDownButtonItem();
            ribbonDropDownButtonItem_Left.Name = "ribbonDropDownButtonItem_Left";
            ribbonDropDownButtonItem_Left.Text = Language.LanguageStrategy.LayoutToLeftText;// "左边停靠";
            ribbonDropDownButtonItem_Left.Enabled = bIsBasePanel && bCanDockLeft;
            ribbonDropDownButtonItem_Left.MouseClick += new MouseEventHandler(BaseItemToDockAreaInternalLeft_MouseClick);
            baseItems.Add(ribbonDropDownButtonItem_Left);
            WFNew.DropDownButtonItem ribbonDropDownButtonItem_Right = new WFNew.DropDownButtonItem();
            ribbonDropDownButtonItem_Right.Name = "ribbonDropDownButtonItem_Right";
            ribbonDropDownButtonItem_Right.Text = Language.LanguageStrategy.LayoutToRightText;// "右边停靠";
            ribbonDropDownButtonItem_Right.Enabled = bIsBasePanel && bCanDockRight;
            ribbonDropDownButtonItem_Right.MouseClick += new MouseEventHandler(BaseItemToDockAreaInternalRight_MouseClick);
            baseItems.Add(ribbonDropDownButtonItem_Right);
            WFNew.DropDownButtonItem ribbonDropDownButtonItem_Bottom = new WFNew.DropDownButtonItem();
            ribbonDropDownButtonItem_Bottom.Name = "ribbonDropDownButtonItem_Bottom";
            ribbonDropDownButtonItem_Bottom.Text = Language.LanguageStrategy.LayoutToBottomText;// "底部停靠";
            ribbonDropDownButtonItem_Bottom.Enabled = bIsBasePanel && bCanDockBottom;
            ribbonDropDownButtonItem_Bottom.MouseClick += new MouseEventHandler(BaseItemToDockAreaInternalBottom_MouseClick);
            baseItems.Add(ribbonDropDownButtonItem_Bottom);
        }

        private void BaseItemToDockAreaInternalTop_MouseClick(object sender, MouseEventArgs e)
        {
            WFNew.DropDownButtonItem ribbonDropDownButtonItem = sender as WFNew.DropDownButtonItem;
            if (ribbonDropDownButtonItem == null || ribbonDropDownButtonItem.Checked) return;
            this._pDockPanel.ToDockArea(true, DockStyle.Top);
        }

        private void BaseItemToDockAreaInternalLeft_MouseClick(object sender, MouseEventArgs e)
        {
            WFNew.DropDownButtonItem ribbonDropDownButtonItem = sender as WFNew.DropDownButtonItem;
            if (ribbonDropDownButtonItem == null || ribbonDropDownButtonItem.Checked) return;
            this._pDockPanel.ToDockArea(true, DockStyle.Left);
        }

        private void BaseItemToDockAreaInternalRight_MouseClick(object sender, MouseEventArgs e)
        {
            WFNew.DropDownButtonItem ribbonDropDownButtonItem = sender as WFNew.DropDownButtonItem;
            if (ribbonDropDownButtonItem == null || ribbonDropDownButtonItem.Checked) return;
            this._pDockPanel.ToDockArea(true, DockStyle.Right);
        }

        private void BaseItemToDockAreaInternalBottom_MouseClick(object sender, MouseEventArgs e)
        {
            WFNew.DropDownButtonItem ribbonDropDownButtonItem = sender as WFNew.DropDownButtonItem;
            if (ribbonDropDownButtonItem == null || ribbonDropDownButtonItem.Checked) return;
            this._pDockPanel.ToDockArea(true, DockStyle.Bottom);
        }

        //
        // 外围停靠
        //
        protected void CreateAddDockPanelManagerBaseItemOut(WFNew.BaseItemCollection baseItems,
            bool bCanDockUp, bool bCanDockLeft, bool bCanDockRight, bool bCanDockBottom, bool bIsBasePanel)
        {
            WFNew.DropDownButtonItem ribbonDropDownButtonItem_Top = new WFNew.DropDownButtonItem();
            ribbonDropDownButtonItem_Top.Name = "ribbonDropDownButtonItem_Top";
            ribbonDropDownButtonItem_Top.Text = Language.LanguageStrategy.LayoutToTopText;// "顶部停靠";
            ribbonDropDownButtonItem_Top.Enabled = bIsBasePanel && bCanDockUp;
            ribbonDropDownButtonItem_Top.MouseClick += new MouseEventHandler(BaseItemToDockAreaOutTop_MouseClick);
            baseItems.Add(ribbonDropDownButtonItem_Top);
            WFNew.DropDownButtonItem ribbonDropDownButtonItem_Left = new WFNew.DropDownButtonItem();
            ribbonDropDownButtonItem_Left.Name = "ribbonDropDownButtonItem_Left";
            ribbonDropDownButtonItem_Left.Text = Language.LanguageStrategy.LayoutToLeftText;// "左边停靠";
            ribbonDropDownButtonItem_Left.Enabled = bIsBasePanel && bCanDockLeft;
            ribbonDropDownButtonItem_Left.MouseClick += new MouseEventHandler(BaseItemToDockAreaOutLeft_MouseClick);
            baseItems.Add(ribbonDropDownButtonItem_Left);
            WFNew.DropDownButtonItem ribbonDropDownButtonItem_Right = new WFNew.DropDownButtonItem();
            ribbonDropDownButtonItem_Right.Name = "ribbonDropDownButtonItem_Right";
            ribbonDropDownButtonItem_Right.Text = Language.LanguageStrategy.LayoutToRightText;// "右边停靠";
            ribbonDropDownButtonItem_Right.Enabled = bIsBasePanel && bCanDockRight;
            ribbonDropDownButtonItem_Right.MouseClick += new MouseEventHandler(BaseItemToDockAreaOutRight_MouseClick);
            baseItems.Add(ribbonDropDownButtonItem_Right);
            WFNew.DropDownButtonItem ribbonDropDownButtonItem_Bottom = new WFNew.DropDownButtonItem();
            ribbonDropDownButtonItem_Bottom.Name = "ribbonDropDownButtonItem_Bottom";
            ribbonDropDownButtonItem_Bottom.Text = Language.LanguageStrategy.LayoutToBottomText;// "底部停靠";
            ribbonDropDownButtonItem_Bottom.Enabled = bIsBasePanel && bCanDockBottom;
            ribbonDropDownButtonItem_Bottom.MouseClick += new MouseEventHandler(BaseItemToDockAreaOutBottom_MouseClick);
            baseItems.Add(ribbonDropDownButtonItem_Bottom);
        }

        private void BaseItemToDockAreaOutTop_MouseClick(object sender, MouseEventArgs e)
        {
            WFNew.DropDownButtonItem ribbonDropDownButtonItem = sender as WFNew.DropDownButtonItem;
            if (ribbonDropDownButtonItem == null || ribbonDropDownButtonItem.Checked) return;
            this._pDockPanel.ToDockArea(false, DockStyle.Top);
        }

        private void BaseItemToDockAreaOutLeft_MouseClick(object sender, MouseEventArgs e)
        {
            WFNew.DropDownButtonItem ribbonDropDownButtonItem = sender as WFNew.DropDownButtonItem;
            if (ribbonDropDownButtonItem == null || ribbonDropDownButtonItem.Checked) return;
            this._pDockPanel.ToDockArea(false, DockStyle.Left);
        }

        private void BaseItemToDockAreaOutRight_MouseClick(object sender, MouseEventArgs e)
        {
            WFNew.DropDownButtonItem ribbonDropDownButtonItem = sender as WFNew.DropDownButtonItem;
            if (ribbonDropDownButtonItem == null || ribbonDropDownButtonItem.Checked) return;
            this._pDockPanel.ToDockArea(false, DockStyle.Right);
        }

        private void BaseItemToDockAreaOutBottom_MouseClick(object sender, MouseEventArgs e)
        {
            WFNew.DropDownButtonItem ribbonDropDownButtonItem = sender as WFNew.DropDownButtonItem;
            if (ribbonDropDownButtonItem == null || ribbonDropDownButtonItem.Checked) return;
            this._pDockPanel.ToDockArea(false, DockStyle.Bottom);
        }

        //
        // 浮动窗体
        //
        protected void CreateDockPanelCustomizeBaseItem(WFNew.BaseItemCollection baseItems)
        {
            WFNew.DropDownButtonItem ribbonDropDownButtonItem = new WFNew.DropDownButtonItem();
            ribbonDropDownButtonItem.Name = "ribbonDropDownButtonItem";
            ribbonDropDownButtonItem.Text = Language.LanguageStrategy.CustomizeText;// "自定义";
            ribbonDropDownButtonItem.MouseClick += new MouseEventHandler(BaseItemDockPanelCustomizeForm_MouseClick);
            baseItems.Add(ribbonDropDownButtonItem);
        }

        private void BaseItemDockPanelCustomizeForm_MouseClick(object sender, MouseEventArgs e)
        {
            this._pDockPanel.DockPanelManager.DockPanelCustomize();
        }
    }
}
