using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace GISShare.Controls.WinForm.WFNew
{
    public abstract class WFNewColorTable
    {
        #region RibbonArea
        public abstract Color RibbonAreaOutLine { get; }
        public abstract Color RibbonAreaBackground { get; }
        //------------------------------------------------------
        public abstract Color RibbonAreaDisabledOutLine { get; }
        public abstract Color RibbonAreaDisabledBackground { get; }
        #endregion

        #region Form
        public abstract Color FormActiveOutLine { get; }
        public abstract Color FormActiveIntLine { get; }
        public abstract Color FormActiveCaptionBegin { get; }
        public abstract Color FormActiveCaptionEnd { get; }
        //------------------------------------------------------
        public abstract Color FormUnActiveOutLine { get; }
        public abstract Color FormUnActiveIntLine { get; }
        public abstract Color FormUnActiveCaptionBegin { get; }
        public abstract Color FormUnActiveCaptionEnd { get; }
        //
        public abstract Color FormCaptionText { get; }
        public abstract Color FormDisabledCaptionText { get; }
        #endregion

        #region RibbonControl
        public abstract Color RibbonControlOutLine { get; }
        public abstract Color RibbonControlPagesBackgroundBegin { get; }
        public abstract Color RibbonControlPagesBackgroundEnd { get; }
        //------------------------------------------------------
        public abstract Color RibbonControlDisabledOutLine { get; }
        public abstract Color RibbonControlPagesDisabledBackgroundBegin { get; }
        public abstract Color RibbonControlPagesDisabledBackgroundEnd { get; }
        #endregion

        #region StartButton2007
        public abstract Color StartButton2007NomalLight { get; }
        public abstract Color StartButton2007NomalBackgroundCenter { get; }
        public abstract Color StartButton2007NomalBackgroundDark { get; }
        public abstract Color StartButton2007NomalBackgroundLight { get; }
        //------------------------------------------------------
        public abstract Color StartButton2007CheckedLight { get; }
        public abstract Color StartButton2007CheckedBackgroundCenter { get; }
        public abstract Color StartButton2007CheckedBackgroundDark { get; }
        public abstract Color StartButton2007CheckedBackgroundLight { get; }
        //------------------------------------------------------
        public abstract Color StartButton2007DisabledLight { get; }
        public abstract Color StartButton2007DisabledBackgroundCenter { get; }
        public abstract Color StartButton2007DisabledBackgroundDark { get; }
        public abstract Color StartButton2007DisabledBackgroundLight { get; }
        //------------------------------------------------------
        public abstract Color StartButton2007PressedLight { get; }
        public abstract Color StartButton2007PressedBackgroundCenter { get; }
        public abstract Color StartButton2007PressedBackgroundDark { get; }
        public abstract Color StartButton2007PressedBackgroundLight { get; }
        //------------------------------------------------------
        public abstract Color StartButton2007SelectedLight { get; }
        public abstract Color StartButton2007SelectedBackgroundCenter { get; }
        public abstract Color StartButton2007SelectedBackgroundDark { get; }
        public abstract Color StartButton2007SelectedBackgroundLight { get; }
        #endregion

        #region StartButton2010
        public abstract Color StartButton2010GraphicBackground { get; }
        public abstract Color StartButton2010GraphicArrow { get; }
        public abstract Color StartButton2010GraphicDarkLine { get; }
        #endregion

        #region RibbonApplicationPopupPanel
        public abstract Color RibbonApplicationPopupPanelBorber { get; }
        public abstract Color RibbonApplicationPopupPanelBackground { get; }
        public abstract Color RibbonApplicationPopupPanelMenuItemsBackground { get; }
        public abstract Color RibbonApplicationPopupPanelBackgroundBegin { get; }
        public abstract Color RibbonApplicationPopupPanelBackgroundEnd { get; }
        public abstract Color RibbonApplicationPopupPanelSeparatorLight { get; }
        public abstract Color RibbonApplicationPopupPanelSeparatorDark { get; }
        #endregion

        #region DescriptionMenuPopupPanel
        public abstract Color DescriptionMenuPopupPanelBorber { get; }
        public abstract Color DescriptionMenuPopupPanelBackground { get; }
        #endregion

        #region QuickAccessToolbar
        public abstract Color QuickAccessToolbarBorberLight { get; }
        public abstract Color QuickAccessToolbarBorberDark { get; }
        public abstract Color QuickAccessToolbarGripBegin { get; }
        public abstract Color QuickAccessToolbarGripEnd { get; }
        #endregion

        #region BaseBar
        public abstract Color BaseBarBackgroundBegin { get; }
        public abstract Color BaseBarBackgroundEnd { get; }
        #endregion

        #region TabButton
        public abstract Color TabButtonCheckedBegin { get; }
        public abstract Color TabButtonCheckedEnd { get; }
        public abstract Color TabButtonCheckedBorderIn { get; }
        //------------------------------------------------------
        public abstract Color TabButtonDisabledBegin { get; }
        public abstract Color TabButtonDisabledEnd { get; }
        public abstract Color TabButtonDisabledBorderIn { get; }
        public abstract Color TabButtonDisabledBorderOut { get; }
        //------------------------------------------------------
        public abstract Color TabButtonPressedBegin { get; }
        public abstract Color TabButtonPressedEnd { get; }
        public abstract Color TabButtonPressedBorderOut { get; }
        //------------------------------------------------------
        public abstract Color TabButtonSelectedCenter { get; }
        public abstract Color TabButtonSelectedGlow { get; }
        public abstract Color TabButtonSelectedBorderOut { get; }
        public abstract Color TabButtonSelectedBorderIn { get; }
        public abstract Color TabButtonCheckedSelectedBorderOut { get; }
        public abstract Color TabButtonCheckedSelectedBorderIn { get; }
        #endregion

        #region RibbonBar
        public abstract Color RibbonBarNomalBegin { get; }
        public abstract Color RibbonBarNomalEnd { get; }
        public abstract Color RibbonBarNomalBorderIn { get; }
        public abstract Color RibbonBarNomalBorderOut { get; }
        //------------------------------------------------------
        public abstract Color RibbonBarCheckedBegin { get; }
        public abstract Color RibbonBarCheckedEnd { get; }
        public abstract Color RibbonBarCheckedBorderIn { get; }
        public abstract Color RibbonBarCheckedBorderOut { get; }
        //------------------------------------------------------
        public abstract Color RibbonBarDisabledBorderIn { get; }
        public abstract Color RibbonBarDisabledBorderOut { get; }
        //------------------------------------------------------
        public abstract Color RibbonBarPressedBegin { get; }
        public abstract Color RibbonBarPressedEnd { get; }
        public abstract Color RibbonBarPressedBorderIn { get; }
        public abstract Color RibbonBarPressedBorderOut { get; }
        //------------------------------------------------------
        public abstract Color RibbonBarSelectedBegin { get; }
        public abstract Color RibbonBarSelectedEnd { get; }
        public abstract Color RibbonBarSelectedBorderIn { get; }
        public abstract Color RibbonBarSelectedBorderOut { get; }
        #endregion

        #region RibbonBarGlyph
        public abstract Color RibbonBarGlyphCheckedBackground { get; }
        public abstract Color RibbonBarGlyphCheckedBorderIn { get; }
        public abstract Color RibbonBarGlyphCheckedBorderOut { get; }
        //------------------------------------------------------
        public abstract Color RibbonBarGlyphDisabledBackground { get; }
        public abstract Color RibbonBarGlyphDisabledBorderIn { get; }
        public abstract Color RibbonBarGlyphDisabledBorderOut { get; }
        //------------------------------------------------------
        public abstract Color RibbonBarGlyphPressedBackground { get; }
        public abstract Color RibbonBarGlyphPressedBorderIn { get; }
        public abstract Color RibbonBarGlyphPressedBorderOut { get; }
        //------------------------------------------------------
        public abstract Color RibbonBarGlyphSelectedBackground { get; }
        public abstract Color RibbonBarGlyphSelectedBorderIn { get; }
        public abstract Color RibbonBarGlyphSelectedBorderOut { get; }
        #endregion

        #region RibbonBarTitle
        public abstract Color RibbonBarTitleBackground { get; }
        public abstract Color RibbonBarTitleCheckedBackground { get; }
        public abstract Color RibbonBarTitleDisabledBackground { get; }
        public abstract Color RibbonBarTitlePressedBackground { get; }
        public abstract Color RibbonBarTitleSelectedBackground { get; }
        #endregion

        #region LinkLabel
        public abstract Color LinkLabelomal { get; }
        public abstract Color LinkLabelPressed { get; }
        public abstract Color LinkLabelSelected { get; }
        public abstract Color LinkLabelDisabled { get; }
        public abstract Color LinkLabelVisited { get; }
        #endregion

        #region Glyph
        public abstract Color Glyph { get; }
        public abstract Color GlyphDisabled { get; }
        public abstract Color GlyphLight { get; }
        #endregion

        #region Arrow
        public abstract Color Arrow { get; }
        public abstract Color ArrowDisabled { get; }
        public abstract Color ArrowLight { get; }
        #endregion

        #region Text
        public abstract Color ItemText { get; }
        public abstract Color ItemTextDisabled { get; }
        public abstract Color ItemTextLight { get; }
        #endregion

        #region Button
        public abstract Color Buttonomal { get; }
        public abstract Color ButtonomalCenter { get; }
        public abstract Color ButtonomalOut { get; }
        public abstract Color ButtonomalGlossyBegin { get; }
        public abstract Color ButtonomalGlossyEnd { get; }
        public abstract Color ButtonomalBorderIn { get; }
        public abstract Color ButtonomalBorderOut { get; }
        //------------------------------------------------------
        public abstract Color ButtonChecked { get; }
        public abstract Color ButtonCheckedCenter { get; }
        public abstract Color ButtonCheckedOut { get; }
        public abstract Color ButtonCheckedGlossyBegin { get; }
        public abstract Color ButtonCheckedGlossyEnd { get; }
        public abstract Color ButtonCheckedBorderIn { get; }
        public abstract Color ButtonCheckedBorderOut { get; }
        //------------------------------------------------------
        public abstract Color ButtonDisabled { get; }
        public abstract Color ButtonDisabledCenter { get; }
        public abstract Color ButtonDisabledOut { get; }
        public abstract Color ButtonDisabledGlossyBegin { get; }
        public abstract Color ButtonDisabledGlossyEnd { get; }
        public abstract Color ButtonDisabledBorderIn { get; }
        public abstract Color ButtonDisabledBorderOut { get; }
        //------------------------------------------------------
        public abstract Color ButtonPressed { get; }
        public abstract Color ButtonPressedCenter { get; }
        public abstract Color ButtonPressedOut { get; }
        public abstract Color ButtonPressedGlossyBegin { get; }
        public abstract Color ButtonPressedGlossyEnd { get; }
        public abstract Color ButtonPressedBorderIn { get; }
        public abstract Color ButtonPressedBorderOut { get; }
        //------------------------------------------------------
        public abstract Color ButtonSelected { get; }
        public abstract Color ButtonSelectedCenter { get; }
        public abstract Color ButtonSelectedOut { get; }
        public abstract Color ButtonSelectedGlossyBegin { get; }
        public abstract Color ButtonSelectedGlossyEnd { get; }
        public abstract Color ButtonSelectedBorderIn { get; }
        public abstract Color ButtonSelectedBorderOut { get; }
        #endregion

        #region ButtonGroup
        public abstract Color ButtonGroupNomalBegin { get; }
        public abstract Color ButtonGroupNomalEnd { get; }
        public abstract Color ButtonGroupNomalGlossyBegin { get; }
        public abstract Color ButtonGroupNomalGlossyEnd { get; }
        public abstract Color ButtonGroupNomalBorderIn { get; }
        public abstract Color ButtonGroupNomalBorderOut { get; }
        //------------------------------------------------------
        public abstract Color ButtonGroupCheckedBegin { get; }
        public abstract Color ButtonGroupCheckedEnd { get; }
        public abstract Color ButtonGroupCheckedGlossyBegin { get; }
        public abstract Color ButtonGroupCheckedGlossyEnd { get; }
        public abstract Color ButtonGroupCheckedBorderIn { get; }
        public abstract Color ButtonGroupCheckedBorderOut { get; }
        //------------------------------------------------------
        public abstract Color ButtonGroupDisabledBegin { get; }
        public abstract Color ButtonGroupDisabledEnd { get; }
        public abstract Color ButtonGroupDisabledGlossyBegin { get; }
        public abstract Color ButtonGroupDisabledGlossyEnd { get; }
        public abstract Color ButtonGroupDisabledBorderIn { get; }
        public abstract Color ButtonGroupDisabledBorderOut { get; }
        //------------------------------------------------------
        public abstract Color ButtonGroupPressedBegin { get; }
        public abstract Color ButtonGroupPressedEnd { get; }
        public abstract Color ButtonGroupPressedGlossyBegin { get; }
        public abstract Color ButtonGroupPressedGlossyEnd { get; }
        public abstract Color ButtonGroupPressedBorderIn { get; }
        public abstract Color ButtonGroupPressedBorderOut { get; }
        //------------------------------------------------------
        public abstract Color ButtonGroupSelectedBegin { get; }
        public abstract Color ButtonGroupSelectedEnd { get; }
        public abstract Color ButtonGroupSelectedGlossyBegin { get; }
        public abstract Color ButtonGroupSelectedGlossyEnd { get; }
        public abstract Color ButtonGroupSelectedBorderIn { get; }
        public abstract Color ButtonGroupSelectedBorderOut { get; }
        #endregion

        #region Gallery
        public abstract Color GalleryNomalBorder { get; }
        public abstract Color GalleryNomalBackground { get; }
        //------------------------------------------------------
        public abstract Color GalleryCheckedBorder { get; }
        public abstract Color GalleryCheckedBackground { get; }
        //------------------------------------------------------
        public abstract Color GalleryDisabledBorder { get; }
        public abstract Color GalleryDisabledBackground { get; }
        //------------------------------------------------------
        public abstract Color GalleryPressedBorder { get; }
        public abstract Color GalleryPressedBackground { get; }
        //------------------------------------------------------
        public abstract Color GallerySelectedBorder { get; }
        public abstract Color GallerySelectedBackground { get; }
        #endregion

        #region RibbonGalleryPopupPanel
        public abstract Color RibbonGalleryPopupPanelBorber { get; }
        public abstract Color RibbonGalleryPopupPanelBackground { get; }
        #endregion

        #region BaseItemStackEx
        public abstract Color BaseItemStackExNomalBorder { get; }
        public abstract Color BaseItemStackExNomalBackground { get; }
        //------------------------------------------------------
        public abstract Color BaseItemStackExCheckedBorder { get; }
        public abstract Color BaseItemStackExCheckedBackground { get; }
        //------------------------------------------------------
        public abstract Color BaseItemStackExDisabledBorder { get; }
        public abstract Color BaseItemStackExDisabledBackground { get; }
        //------------------------------------------------------
        public abstract Color BaseItemStackExPressedBorder { get; }
        public abstract Color BaseItemStackExPressedBackground { get; }
        //------------------------------------------------------
        public abstract Color BaseItemStackExSelectedBorder { get; }
        public abstract Color BaseItemStackExSelectedBackground { get; }
        #endregion

        #region PopupPanel
        public abstract Color PopupPanelBorder { get; }
        public abstract Color PopupPanelBackground { get; }
        public abstract Color PopupPanelImageGripBackground { get; }
        public abstract Color PopupPanelImageGripSeparator { get; }
        public abstract Color PopupPanelCheckGripBegin { get; }
        public abstract Color PopupPanelCheckGripEnd { get; }
        #endregion

        #region ToolTipPopupPanel
        public abstract Color ToolTipPopupPanelBegin { get; }
        public abstract Color ToolTipPopupPanelEnd { get; }
        #endregion

        #region PopupItemButtonChecked
        public abstract Color PopupItemButtonCheckedBorder { get; }
        public abstract Color PopupItemButtonCheckedBackgound { get; }
        //------------------------------------------------------
        public abstract Color PopupItemButtonCheckedDisabledBorder { get; }
        public abstract Color PopupItemButtonCheckedDisabledBackgound { get; }
        #endregion

        #region CheckBox And RadioButton
        public abstract Color CheckBoxomalOutLine { get; }
        public abstract Color CheckBoxomalBackgroundOutBegin { get; }
        public abstract Color CheckBoxomalBackgroundOutEnd { get; }
        public abstract Color CheckBoxomalBackgroundMiddleBegin { get; }
        public abstract Color CheckBoxomalBackgroundMiddleEnd { get; }
        public abstract Color CheckBoxomalBackgroundIntBegin { get; }
        public abstract Color CheckBoxomalBackgroundIntEnd { get; }
        //------------------------------------------------------
        public abstract Color CheckBoxDisabledOutLine { get; }
        public abstract Color CheckBoxDisabledBackgroundOutBegin { get; }
        public abstract Color CheckBoxDisabledBackgroundOutEnd { get; }
        public abstract Color CheckBoxDisabledBackgroundMiddleBegin { get; }
        public abstract Color CheckBoxDisabledBackgroundMiddleEnd { get; }
        public abstract Color CheckBoxDisabledBackgroundIntBegin { get; }
        public abstract Color CheckBoxDisabledBackgroundIntEnd { get; }
        //------------------------------------------------------
        public abstract Color CheckBoxPressedOutLine { get; }
        public abstract Color CheckBoxPressedBackgroundOutBegin { get; }
        public abstract Color CheckBoxPressedBackgroundOutEnd { get; }
        public abstract Color CheckBoxPressedBackgroundMiddleBegin { get; }
        public abstract Color CheckBoxPressedBackgroundMiddleEnd { get; }
        public abstract Color CheckBoxPressedBackgroundIntBegin { get; }
        public abstract Color CheckBoxPressedBackgroundIntEnd { get; }
        //------------------------------------------------------
        public abstract Color CheckBoxSelectedOutLine { get; }
        public abstract Color CheckBoxSelectedBackgroundOutBegin { get; }
        public abstract Color CheckBoxSelectedBackgroundOutEnd { get; }
        public abstract Color CheckBoxSelectedBackgroundMiddleBegin { get; }
        public abstract Color CheckBoxSelectedBackgroundMiddleEnd { get; }
        public abstract Color CheckBoxSelectedBackgroundIntBegin { get; }
        public abstract Color CheckBoxSelectedBackgroundIntEnd { get; }
        #endregion

        #region Separator
        public abstract Color SeparatorBackground { get; }
        public abstract Color SeparatorLight { get; }
        public abstract Color SeparatorDark { get; }
        #endregion

        #region TextBox
        public abstract Color TextBoxomalText { get; }
        public abstract Color TextBoxomalBorder { get; }
        public abstract Color TextBoxomalBackground { get; }

        public abstract Color TextBoxSelectedText { get; }
        public abstract Color TextBoxSelectedBorder { get; }
        public abstract Color TextBoxSelectedBackground { get; }

        public abstract Color TextBoxDisabledText { get; }
        public abstract Color TextBoxDisabledBorder { get; }
        public abstract Color TextBoxDisabledBackground { get; }
        #endregion

        #region ProcessBar
        public abstract Color ProcessBarBorderIn { get; }
        public abstract Color ProcessBarBorderOut { get; }
        public abstract Color ProcessBarValueBegin { get; }
        public abstract Color ProcessBarValueEnd { get; }
        public abstract Color ProcessBarBackgroundBegin { get; }
        public abstract Color ProcessBarBackgroundEnd { get; }
        #endregion

        #region ScrollBar
        public abstract Color ScrollBarBackgroundBegin { get; }
        public abstract Color ScrollBarBackgroundEnd { get; }
        #endregion

        #region ViewItem
        #endregion

        #region Star
        public abstract Color RatingStarBorder { get; }
        public abstract Color RatingStarSelectedBegin { get; }
        public abstract Color RatingStarSelectedEnd { get; }
        #endregion


    }
}