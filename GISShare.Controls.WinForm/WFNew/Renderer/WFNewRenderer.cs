using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms.VisualStyles;
using System.Drawing.Drawing2D;

namespace GISShare.Controls.WinForm.WFNew
{
    public abstract class WFNewRenderer
    {
        public static WFNewRenderer WFNewRendererStrategy = new WFNewRenderer2007();

        public abstract WFNewColorTable WFNewColorTable { get; }

        #region Form
        public abstract void OnRenderFormNC(ObjectRenderEventArgs e);

        public abstract void OnRenderFormNCIcon(IconRenderEventArgs e);

        public abstract void OnRenderFormNCCaptionText(TextRenderEventArgs e);

        public abstract void OnRenderForm(ObjectRenderEventArgs e);
        #endregion

        #region RibbonControl
        public abstract void OnRenderRibbonControl(ObjectRenderEventArgs e);

        //public abstract void OnRenderRibbonControlCaptionIcon(IconRenderEventArgs e);

        //public abstract void OnRenderRibbonControlCaptionText(TextRenderEventArgs e);
        #endregion

        #region RibbonStartButton2007
        public abstract void OnRenderRibbonStartButton2007(ObjectRenderEventArgs e);
        #endregion

        #region RibbonStartButton2010
        public abstract void OnRenderRibbonStartButton2010(ObjectRenderEventArgs e);
        #endregion

        #region RibbonApplicationPopupPanel
        public abstract void OnRenderRibbonApplicationPopupPanel(ObjectRenderEventArgs e);
        #endregion

        #region DescriptionMenuPopupPanel
        public abstract void OnRenderDescriptionMenuPopupPanel(ObjectRenderEventArgs e);
        #endregion

        #region FormButton RibbonControl MinButton MaxButton HelpButton CloseButton
        public abstract void OnRenderFormButton(ObjectRenderEventArgs e);
        #endregion

        #region RibbonQuickAccessToolbar
        public abstract void OnRenderRibbonQuickAccessToolbar(ObjectRenderEventArgs e);

        public abstract void OnRenderCustomizeButton(ObjectRenderEventArgs e);
        #endregion

        #region RibbonStatusBar
        public abstract void OnRenderRibbonStatusBar(ObjectRenderEventArgs e);
        #endregion

        #region BaseBar
        public abstract void OnRenderRibbonBaseBar(ObjectRenderEventArgs e);

        public abstract void OnRenderOverflowButton(ObjectRenderEventArgs e);
        #endregion

        public abstract void OnRenderRibbonToolBar(ObjectRenderEventArgs e);

        #region RibbonPageContainerPopup
        public abstract void OnRenderRibbonPageContainerPopup(ObjectRenderEventArgs e);
        #endregion

        #region RibbonPage
        public abstract void OnRenderRibbonPage(ObjectRenderEventArgs e);
        #endregion

        public abstract void OnRenderTabButtonContainerButton(ObjectRenderEventArgs e);

        #region TabButton
        public abstract void OnRenderTabButton(ObjectRenderEventArgs e);
        #endregion

        #region RibbonBar
        public abstract void OnRenderRibbonBar(ObjectRenderEventArgs e);

        public abstract void OnRenderRibbonBarPopup(ObjectRenderEventArgs e);
        #endregion

        #region Label
        public abstract void OnRenderLabel(ObjectRenderEventArgs e);
        #endregion

        #region LinkLabel
        public abstract void OnRenderLinkLabel(ObjectRenderEventArgs e);
        #endregion

        #region LabelSeparator
        public abstract void OnRenderLabelSeparator(ObjectRenderEventArgs e);
        #endregion

        #region RadioButton
        public abstract void OnRenderRadioButton(ObjectRenderEventArgs e);
        #endregion

        #region CheckBox
        public abstract void OnRenderCheckBox(ObjectRenderEventArgs e);
        #endregion

        #region BaseButton
        public abstract void OnRenderBaseButton(ObjectRenderEventArgs e);
        #endregion 

        #region GlyphButton
        public abstract void OnRenderGlyphButton(ObjectRenderEventArgs e);
        #endregion

        #region DescriptionButton
        public abstract void OnRenderDescriptionButton(ObjectRenderEventArgs e);
        #endregion

        #region CheckButton
        public abstract void OnRenderCheckButton(ObjectRenderEventArgs e);
        #endregion

        #region DropDownButton
        public abstract void OnRenderDropDownButton(ObjectRenderEventArgs e);
        #endregion

        #region SplitButton
        public abstract void OnRenderSplitButton(ObjectRenderEventArgs e);
        #endregion

        #region ProcessBar
        public abstract void OnRenderProcessBar(ObjectRenderEventArgs e);
        #endregion

        #region Slider
        public abstract void OnRenderSlider(ObjectRenderEventArgs e);
        #endregion

        #region SliderButton
        public abstract void OnRenderSliderButton(ObjectRenderEventArgs e);
        #endregion

        #region ScrollBar
        public abstract void OnRenderScrollBar(ObjectRenderEventArgs e);
        #endregion

        #region Star
        public abstract void OnRenderStar(ObjectRenderEventArgs e);
        #endregion

        #region ScrollBarButton
        public abstract void OnRenderScrollBarButton(ObjectRenderEventArgs e);
        #endregion

        #region ButtonGroup
        public abstract void OnRenderButtonGroup(ObjectRenderEventArgs e);
        #endregion

        #region Separator
        public abstract void OnRenderSeparator(ObjectRenderEventArgs e);
        #endregion

        #region RibbonGallery
        public abstract void OnRenderRibbonGallery(ObjectRenderEventArgs e);
        #endregion

        #region RibbonGalleryPopupPanel
        public abstract void OnRenderRibbonGalleryPopupPanel(ObjectRenderEventArgs e);
        #endregion

        #region GalleryScrollButton RibbonGallery ScrollUpButton ScrollDownButton ScrollDropDownButton
        public abstract void OnRenderGalleryScrollButton(ObjectRenderEventArgs e);
        #endregion

        #region TextBox
        public abstract void OnRenderTextBox(ObjectRenderEventArgs e);
        #endregion

        #region ImageAreaBox
        public abstract void OnRenderImageAreaBox(ObjectRenderEventArgs e);
        #endregion

        #region CustomizeComboBox
        public abstract void OnRenderCustomizeComboBox(ObjectRenderEventArgs e);
        #endregion

        #region CustomizePopup
        public abstract void OnRenderCustomizePopup(ObjectRenderEventArgs e);
        #endregion

        #region BaseItemStackEx
        public abstract void OnRenderBaseItemStackEx(ObjectRenderEventArgs e);
        #endregion

        #region LTBRButton BaseItemStackEx Top Bottom Left Right Button
        public abstract void OnRenderLTBRButton(ObjectRenderEventArgs e);
        #endregion

        #region ContextPopupPanel
        public abstract void OnRenderContextPopupPanel(ObjectRenderEventArgs e);
        #endregion

        #region RibbonText
        public abstract void OnRenderRibbonText(TextRenderEventArgs e);

        public abstract void OnRenderLinkLabelText(TextRenderEventArgs e);

        public abstract void OnRenderTextBoxText(TextRenderEventArgs e);
        #endregion

        public abstract void OnRenderUpDownButton(ObjectRenderEventArgs e);

        #region RibbonImage
        public abstract void OnRenderRibbonImage(ImageRenderEventArgs e);
        public abstract void OnRenderRibbonImage(ImageRenderEventArgsF e);
        #endregion

        #region RibbonArrow
        public abstract void OnRenderRibbonArrow(ArrowRenderEventArgs e);
        #endregion

        #region RibbonCheck
        public abstract void OnRenderContextPopupItemButtonChecked(CheckedRenderEventArgs e);
        #endregion

        public abstract void OnRenderRibbonArea(ObjectRenderEventArgs e);

        public abstract void OnRenderToolTipPopupPanel(ObjectRenderEventArgs e);

        //
        //
        //

        #region ViewItemList
        public abstract void OnRenderViewItemList(ObjectRenderEventArgs e);
        #endregion

        #region ViewItem
        public abstract void OnRenderViewItem(ObjectRenderEventArgs e);
        #endregion

        #region ColorViewItem
        public abstract void OnRenderColorViewItem(ObjectRenderEventArgs e);
        #endregion

        #region ImageViewItem
        public abstract void OnRenderImageViewItem(ObjectRenderEventArgs e);
        #endregion

        #region NodeViewItem
        public abstract void OnRenderNodeViewItem(ObjectRenderEventArgs e);
        #endregion

        #region RowNodeCellViewItem
        public abstract void OnRenderRowNodeCellViewItem(ObjectRenderEventArgs e);
        #endregion

        //

        #region TitleViewItem
        public abstract void OnRenderTitleViewItem(ObjectRenderEventArgs e);
        #endregion

        #region ColumnViewItem
        public abstract void OnRenderColumnViewItem(ObjectRenderEventArgs e);
        #endregion

        #region RowHeaderItem
        public abstract void OnRenderRowHeaderItem(ObjectRenderEventArgs e);
        #endregion

        #region RowCellViewItem
        public abstract void OnRenderRowCellViewItem(ObjectRenderEventArgs e);
        #endregion

        #region CellViewItem
        public abstract void OnRenderCellViewItem(ObjectRenderEventArgs e);
        #endregion

        #region NodeCellViewItem
        public abstract void OnRenderNodeCellViewItem(ObjectRenderEventArgs e);
        #endregion

        //
        //
        //

        public abstract void OnRenderDockPanelButton(ObjectRenderEventArgs e);
        public abstract void OnRenderDockPanel(ObjectRenderEventArgs e);
        public abstract void OnRenderDockPanelFloatFormButton(ObjectRenderEventArgs e);
        public abstract void OnRenderDockPanelFloatForm(ObjectRenderEventArgs e);
        public abstract void OnRenderSplitLine(ObjectRenderEventArgs e);
        public abstract void OnRenderDockButton(ObjectRenderEventArgs e);

        //
        //
        //

        public abstract void OnRenderCollapsableSplitPanel(ObjectRenderEventArgs e);

        //
        //
        //

        public abstract void OnRenderExpandableCaptionPanel(ObjectRenderEventArgs e);
        public abstract void OnRenderExpandableCaptionPanelButton(ObjectRenderEventArgs e);

    }
}
