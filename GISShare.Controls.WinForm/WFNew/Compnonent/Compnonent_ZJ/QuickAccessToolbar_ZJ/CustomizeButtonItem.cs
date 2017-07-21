using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    class CustomizeButtonItem : DropDownButtonItem, IViewDepend, IBaseItemProperty
    {
        private IQuickAccessToolbarItem owner;

        private BaseButtonItem m_TopOrBottomViewQuickAccessToolbar;
        private BaseButtonItem m_DisplayOrHideRibbonPageContainer;
        private DropDownButtonItem m_CustomizeQuickAccessToolbar;

        public CustomizeButtonItem(IQuickAccessToolbarItem pQuickAccessToolbarItem)
            : base()
        {
            base.Name = "GISShare.Controls.WinForm.WFNew.CustomizeButtonItem";
            base.Text = "自定义按钮";
            base.UsingViewOverflow = false;
            
            //
            this.ShowNomalState = false;
            this.owner = pQuickAccessToolbarItem;
            ((ISetOwnerHelper)this).SetOwner(owner as IOwner);
            //
            this.SetPopupPadding(2, 2, 2, 2);
            //this.setr
            //
            LabelSeparatorItem ribbonLabelSeparatorItem = new LabelSeparatorItem();
            ribbonLabelSeparatorItem.Name = GISShare.Controls.WinForm.WFNew.Language.LanguageStrategy.CustomizeTitleText;
            ribbonLabelSeparatorItem.Text = GISShare.Controls.WinForm.WFNew.Language.LanguageStrategy.CustomizeTitleText;
            ribbonLabelSeparatorItem.TextAlign = ContentAlignment.MiddleCenter;
            this.BaseItems.Add(ribbonLabelSeparatorItem);
            //
            this.m_TopOrBottomViewQuickAccessToolbar = new BaseButtonItem();
            this.m_TopOrBottomViewQuickAccessToolbar.Name = GISShare.Controls.WinForm.WFNew.Language.LanguageStrategy.BottomViewQuickAccessToolbarText;
            this.m_TopOrBottomViewQuickAccessToolbar.Text = this.m_TopOrBottomViewQuickAccessToolbar.Name;
            this.m_TopOrBottomViewQuickAccessToolbar.MouseClick += new MouseEventHandler(TopOrBottomViewQuickAccessToolbar_MouseClick);
            this.BaseItems.Add(this.m_TopOrBottomViewQuickAccessToolbar);
            //
            this.m_DisplayOrHideRibbonPageContainer = new BaseButtonItem();
            this.m_DisplayOrHideRibbonPageContainer.Name = GISShare.Controls.WinForm.WFNew.Language.LanguageStrategy.HideRibbonPageContainerText;
            this.m_DisplayOrHideRibbonPageContainer.Text = this.m_DisplayOrHideRibbonPageContainer.Name;
            this.m_DisplayOrHideRibbonPageContainer.MouseClick += new MouseEventHandler(DisplayOrHideRibbonPageContainer_MouseClick);
            this.BaseItems.Add(this.m_DisplayOrHideRibbonPageContainer);
            //
            this.BaseItems.Add(new SeparatorItem());
            //
            this.m_CustomizeQuickAccessToolbar = new DropDownButtonItem();
            this.m_CustomizeQuickAccessToolbar.Name = GISShare.Controls.WinForm.WFNew.Language.LanguageStrategy.CustomizeQuickAccessToolbarText;
            this.m_CustomizeQuickAccessToolbar.Text = GISShare.Controls.WinForm.WFNew.Language.LanguageStrategy.CustomizeQuickAccessToolbarText;
            this.m_CustomizeQuickAccessToolbar.eContextPopupStyle = ContextPopupStyle.eSuper;
            //IDropDownPopup pDropDownPopup = ((IPopupOwnerHelper)this.m_CustomizeQuickAccessToolbar).GetBasePopup() as IDropDownPopup;
            //if (pDropDownPopup != null) pDropDownPopup.eContextPopupStyle = ContextPopupStyle.eSuper;
            this.BaseItems.Add(this.m_CustomizeQuickAccessToolbar);
            ((ILockCollectionHelper)this.BaseItems).SetLocked(true);
        }
        void TopOrBottomViewQuickAccessToolbar_MouseClick(object sender, MouseEventArgs e)
        {
            IRibbonControl pRibbonControl = this.owner.pOwner as IRibbonControl;
            if (pRibbonControl == null) return;
            pRibbonControl.IsTopToolbar = !pRibbonControl.IsTopToolbar;
            this.m_TopOrBottomViewQuickAccessToolbar.Name = pRibbonControl.IsTopToolbar ? GISShare.Controls.WinForm.WFNew.Language.LanguageStrategy.BottomViewQuickAccessToolbarText : GISShare.Controls.WinForm.WFNew.Language.LanguageStrategy.TopViewQuickAccessToolbarText;
            this.m_TopOrBottomViewQuickAccessToolbar.Text = this.m_TopOrBottomViewQuickAccessToolbar.Name;
            //throw new Exception("The method or operation is not implemented.");
        }
        void DisplayOrHideRibbonPageContainer_MouseClick(object sender, MouseEventArgs e)
        {
            IRibbonControl pRibbonControl = this.owner.pOwner as IRibbonControl;
            if (pRibbonControl == null) return;
            pRibbonControl.HideRibbonPage = !pRibbonControl.HideRibbonPage;
            this.m_DisplayOrHideRibbonPageContainer.Name = pRibbonControl.HideRibbonPage ? GISShare.Controls.WinForm.WFNew.Language.LanguageStrategy.DisplayRibbonPageContainerText : GISShare.Controls.WinForm.WFNew.Language.LanguageStrategy.HideRibbonPageContainerText;
            this.m_DisplayOrHideRibbonPageContainer.Text = this.m_DisplayOrHideRibbonPageContainer.Name;
            //throw new Exception("The method or operation is not implemented.");
        }

        #region IBaseItemProperty
        [Browsable(false), Description("自身所属状态"), Category("属性")]
        BaseItemStyle IBaseItemProperty.eBaseItemStyle
        {
            get { return BaseItemStyle.eComponentBaseItem; }
        }

        [Browsable(false), Description("获取其依附项（如果，为独立项依附项为其自身）"), Category("关联")]
        IBaseItem3 IBaseItemProperty.DependObject
        {
            get { return this.owner; }
        }
        #endregion

        ViewDependStyle IViewDepend.eViewDependStyle
        {
            get
            {
                return ViewDependStyle.eInOwnerDisplayRectangle;
            }
        }

        public override bool Enabled
        {
            get
            {
                return this.owner.Enabled;
            }
        }

        public override bool Overflow
        {
            get
            {
                return !this.owner.DisplayRectangle.Contains(this.DisplayRectangle);
            }
        }

        public override int LeftTopRadius
        {
            get
            {
                return this.owner.LeftTopRadius;
            }
            set { }
        }

        public override int RightTopRadius
        {
            get
            {
                return this.owner.RightTopRadius;
            }
            set { }
        }

        public override int LeftBottomRadius
        {
            get
            {
                return this.owner.LeftBottomRadius;
            }
            set { }
        }

        public override int RightBottomRadius
        {
            get
            {
                return this.owner.RightBottomRadius;
            }
            set { }
        }

        public override Rectangle DisplayRectangle
        {
            get
            {
                return this.owner.CustomizeButtonRectangle;
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            bool bVisible = this.owner.BaseItems.Count > 0;
            if (bVisible)
            {
                IImageBoxItem pImageItem;
                this.m_CustomizeQuickAccessToolbar.BaseItems.Clear();
                foreach (BaseItem one in this.owner.BaseItems)
                {
                    ISeparatorItem pSeparatorItem = one as ISeparatorItem;
                    if (pSeparatorItem != null && pSeparatorItem.AutoLayout)
                    {
                        this.m_CustomizeQuickAccessToolbar.BaseItems.Add(new GISShare.Controls.WinForm.WFNew.SeparatorItem());
                    }
                    else
                    {
                        CheckButtonItem item = new CheckButtonItem();
                        item.Name = one.Name;
                        item.Text = (one.Text == null || one.Text.Length <= 0) ? GISShare.Controls.WinForm.WFNew.Language.LanguageStrategy.CustomizeItemNotNamedText : one.Text;
                        item.Checked = one.Visible;
                        item.Tag = one;
                        item.eImageSizeStyle = ImageSizeStyle.eSystem;
                        pImageItem = one as IImageBoxItem;
                        if (pImageItem != null) { item.Image = pImageItem.Image; }
                        item.CheckedChanged += new EventHandler(Item_CheckedChanged);
                        this.m_CustomizeQuickAccessToolbar.BaseItems.Add(item);
                    }
                }
            }
            this.BaseItems[3].Visible = bVisible;
            this.m_CustomizeQuickAccessToolbar.Visible = bVisible;
            //
            IRibbonControl pRibbonControl = this.owner.pOwner as IRibbonControl;
            if (pRibbonControl == null) return;
            this.m_TopOrBottomViewQuickAccessToolbar.Name = pRibbonControl.IsTopToolbar ? GISShare.Controls.WinForm.WFNew.Language.LanguageStrategy.BottomViewQuickAccessToolbarText : GISShare.Controls.WinForm.WFNew.Language.LanguageStrategy.TopViewQuickAccessToolbarText;
            this.m_TopOrBottomViewQuickAccessToolbar.Text = this.m_TopOrBottomViewQuickAccessToolbar.Name;
            this.m_DisplayOrHideRibbonPageContainer.Name = pRibbonControl.HideRibbonPage ? GISShare.Controls.WinForm.WFNew.Language.LanguageStrategy.DisplayRibbonPageContainerText : GISShare.Controls.WinForm.WFNew.Language.LanguageStrategy.HideRibbonPageContainerText;
            this.m_DisplayOrHideRibbonPageContainer.Text = this.m_DisplayOrHideRibbonPageContainer.Name;
            //
            base.OnMouseDown(e);
        }
        void Item_CheckedChanged(object sender, EventArgs e)
        {
            CheckButtonItem ribbonCheckButtonItem = sender as CheckButtonItem;
            if (ribbonCheckButtonItem == null) return;
            BaseItem baseItem = ribbonCheckButtonItem.Tag as BaseItem;
            if (baseItem == null) return;
            baseItem.Visible = ribbonCheckButtonItem.Checked;
        }

        protected override void OnDraw(PaintEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderCustomizeButton(
                new GISShare.Controls.WinForm.ObjectRenderEventArgs(e.Graphics, this, this.DisplayRectangle));
        }
    }
}
