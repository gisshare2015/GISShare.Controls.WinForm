using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace GISShare.Controls.WinForm.WFNew
{
    public class WFNewColorTable2007 : WFNewColorTable
    {
        #region RibbonArea
        public override Color RibbonAreaOutLine
        { get { return Color.FromArgb(59, 90, 130); } }//1
        public override Color RibbonAreaBackground
        { get { return Color.FromArgb(191, 219, 255); } }//1
        //------------------------------------------------------
        public override Color RibbonAreaDisabledOutLine
        { get { return Color.FromArgb(151, 165, 183); } }//1
        public override Color RibbonAreaDisabledBackground
        { get { return Color.FromArgb(191, 219, 255); } }//1
        #endregion

        #region Form
        public override Color FormActiveOutLine
        { get { return Color.FromArgb(59, 90, 130); } }//1
        //public override Color FormActiveMiddleLine
        //{ get { return Color.FromArgb(177, 198, 225); } }//1
        public override Color FormActiveIntLine
        { get { return Color.FromArgb(194, 217, 247); } }//1
        public override Color FormActiveCaptionBegin
        { get { return Color.FromArgb(202, 222, 247); } }//1
        public override Color FormActiveCaptionEnd
        { get { return Color.FromArgb(227, 240, 253); } }//1
        //public override Color FormActiveTopLineAreaBegin
        //{ get { return Color.FromArgb(228, 235, 246); } }//1
        //public override Color FormActiveTopLineAreaEnd
        //{ get { return Color.FromArgb(217, 231, 249); } }//1
        //public override Color FormActiveLineAreaBackground
        //{ get { return Color.FromArgb(191, 219, 255); } }//1
        //------------------------------------------------------
        public override Color FormUnActiveOutLine
        { get { return Color.FromArgb(151, 165, 183); } }//1
        //public override Color FormUnActiveMiddleLine
        //{ get { return Color.FromArgb(204, 214, 226); } }//1
        public override Color FormUnActiveIntLine
        { get { return Color.FromArgb(212, 222, 236); } }//1
        public override Color FormUnActiveCaptionBegin
        { get { return Color.FromArgb(217, 226, 236); } }//1
        public override Color FormUnActiveCaptionEnd
        { get { return Color.FromArgb(226, 232, 239); } }//1
        //public override Color FormUnActiveTopLineAreaBegin
        //{ get { return Color.FromArgb(227, 231, 236); } }//1
        //public override Color FormUnActiveTopLineAreaEnd
        //{ get { return Color.FromArgb(222, 229, 237); } }//1
        //public override Color FormUnActiveLineAreaBackground
        //{ get { return Color.FromArgb(204, 216, 231); } }//1
        //
        public override Color FormCaptionText
        { get { return Color.FromArgb(59, 90, 130); } }//1
        public override Color FormDisabledCaptionText
        { get { return Color.FromArgb(183, 183, 183); } }//1
        #endregion

        #region RibbonControl
        public override Color RibbonControlOutLine
        { get { return Color.FromArgb(185, 208, 237); } }//1
        //public override Color RibbonControlBackground
        //{ get { return Color.FromArgb(191, 219, 255); } }//1
        public override Color RibbonControlPagesBackgroundBegin
        { get { return Color.FromArgb(200, 217, 237); } }//1
        public override Color RibbonControlPagesBackgroundEnd
        { get { return Color.FromArgb(231, 242, 255); } }//1
        //------------------------------------------------------
        public override Color RibbonControlDisabledOutLine
        { get { return Color.FromArgb(197, 209, 222); } }//
        //public override Color RibbonControlDisabledBackground
        //{ get { return Color.FromArgb(191, 219, 255); } }//1
        public override Color RibbonControlPagesDisabledBackgroundBegin
        { get { return Color.FromArgb(232, 235, 239); } }//
        public override Color RibbonControlPagesDisabledBackgroundEnd
        { get { return Color.FromArgb(234, 237, 241); } }//
        #endregion

        #region StartButton2007
        public override Color StartButton2007NomalLight
        { get { return Color.FromArgb(255, 255, 255, 255); } }//1
        public override Color StartButton2007NomalBackgroundCenter
        { get { return Color.FromArgb(255, 153, 171, 198); } }//1
        public override Color StartButton2007NomalBackgroundDark
        { get { return Color.FromArgb(255, 124, 140, 164); } }//1
        public override Color StartButton2007NomalBackgroundLight
        { get { return Color.FromArgb(255, 255, 255, 255); } }//1
        //------------------------------------------------------
        public override Color StartButton2007CheckedLight
        { get { return Color.FromArgb(255, 249, 170, 69); } }//
        public override Color StartButton2007CheckedBackgroundCenter
        { get { return Color.FromArgb(255, 253, 234, 157); } }//
        public override Color StartButton2007CheckedBackgroundDark
        { get { return Color.FromArgb(255, 248, 219, 183); } }//
        public override Color StartButton2007CheckedBackgroundLight
        { get { return Color.FromArgb(255, 254, 209, 142); } }//
        //------------------------------------------------------
        public override Color StartButton2007DisabledLight
        { get { return Color.FromArgb(255, 224, 228, 232); } }
        public override Color StartButton2007DisabledBackgroundCenter
        { get { return Color.FromArgb(255, 232, 235, 239); } }//
        public override Color StartButton2007DisabledBackgroundDark
        { get { return Color.FromArgb(255, 240, 243, 146); } }//
        public override Color StartButton2007DisabledBackgroundLight
        { get { return Color.FromArgb(255, 234, 237, 241); } }//
        //------------------------------------------------------
        public override Color StartButton2007PressedLight
        { get { return Color.FromArgb(255, 240, 133, 0); } }//1
        public override Color StartButton2007PressedBackgroundCenter
        { get { return Color.FromArgb(255, 206, 132, 16); } }//1
        public override Color StartButton2007PressedBackgroundDark
        { get { return Color.FromArgb(255, 206, 132, 16); } }//1
        public override Color StartButton2007PressedBackgroundLight
        { get { return Color.FromArgb(255, 245, 118, 3); } }//1
        //------------------------------------------------------
        public override Color StartButton2007SelectedLight
        { get { return Color.FromArgb(255, 255, 245, 43); } }//1
        public override Color StartButton2007SelectedBackgroundCenter
        { get { return Color.FromArgb(255, 249, 209, 46); } }//1
        public override Color StartButton2007SelectedBackgroundDark
        { get { return Color.FromArgb(255, 223, 170, 26); } }//1
        public override Color StartButton2007SelectedBackgroundLight
        { get { return Color.FromArgb(255, 255, 239, 54); } }//1
        #endregion

        #region StartButton2010
        public override Color StartButton2010GraphicBackground
        { get { return Color.White; } }//
        public override Color StartButton2010GraphicArrow
        { get { return Color.Snow; } }//
        public override Color StartButton2010GraphicDarkLine
        { get { return Color.FromArgb(103, 140, 189); } }//
        #endregion

        #region RibbonApplicationPopupPanel
        public override Color RibbonApplicationPopupPanelBorber
        { get { return Color.FromArgb(134, 134, 134); } }//
        public override Color RibbonApplicationPopupPanelBackground
        { get { return Color.FromArgb(239, 245, 250); } }//
        public override Color RibbonApplicationPopupPanelMenuItemsBackground
        { get { return Color.FromArgb(252, 252, 252); } }//
        public override Color RibbonApplicationPopupPanelBackgroundBegin
        { get { return Color.FromArgb(187, 206, 232); } }//
        public override Color RibbonApplicationPopupPanelBackgroundEnd
        { get { return Color.FromArgb(214, 228, 246); } }//
        public override Color RibbonApplicationPopupPanelSeparatorLight
        { get { return Color.FromArgb(250, 251, 253); } }//
        public override Color RibbonApplicationPopupPanelSeparatorDark
        { get { return Color.FromArgb(207, 219, 235); } }//
        #endregion

        #region DescriptionMenuPopupPanel
        public override Color DescriptionMenuPopupPanelBorber
        { get { return Color.FromArgb(134, 134, 134); } }//
        public override Color DescriptionMenuPopupPanelBackground
        { get { return Color.FromArgb(239, 245, 250); } }//
        #endregion

        #region QuickAccessToolbar
        public override Color QuickAccessToolbarBorberLight
        { get { return Color.FromArgb(242, 246, 251); } }//
        public override Color QuickAccessToolbarBorberDark
        { get { return Color.FromArgb(182, 202, 226); } }//
        public override Color QuickAccessToolbarGripBegin
        { get { return Color.FromArgb(201, 217, 238); } }//
        public override Color QuickAccessToolbarGripEnd
        { get { return Color.FromArgb(224, 235, 249); } }//
        #endregion

        #region BaseBar
        public override Color BaseBarBackgroundBegin
        { get { return Color.FromArgb(215, 229, 247); } }//227, 239, 255
        public override Color BaseBarBackgroundEnd
        { get { return Color.FromArgb(172, 201, 238); } }//152, 186, 230
        #endregion

        #region TabButton
        public override Color TabButtonCheckedBegin
        { get { return Color.FromArgb(235, 243, 254); } }//
        public override Color TabButtonCheckedEnd
        { get { return Color.FromArgb(225, 234, 246); } }//
        public override Color TabButtonCheckedBorderIn
        { get { return Color.White; } }//
        //------------------------------------------------------
        public override Color TabButtonDisabledBegin
        { get { return Color.FromArgb(232, 235, 239); } }//
        public override Color TabButtonDisabledEnd
        { get { return Color.FromArgb(234, 237, 241); } }//
        public override Color TabButtonDisabledBorderIn
        { get { return Color.FromArgb(241, 243, 245); } }//
        public override Color TabButtonDisabledBorderOut
        { get { return Color.FromArgb(197, 209, 222); } }//
        //------------------------------------------------------
        public override Color TabButtonPressedBegin
        { get { return Color.FromArgb(200, 217, 237); } }//
        public override Color TabButtonPressedEnd
        { get { return Color.FromArgb(231, 242, 255); } }//
        public override Color TabButtonPressedBorderOut
        { get { return Color.FromArgb(141, 178, 227); } }//
        //------------------------------------------------------
        public override Color TabButtonSelectedCenter
        { get { return Color.Transparent; } }//
        public override Color TabButtonSelectedGlow
        { get { return Color.FromArgb(225, 210, 165); } }//
        public override Color TabButtonSelectedBorderOut
        { get { return Color.FromArgb(141, 178, 227); } }//
        public override Color TabButtonSelectedBorderIn
        { get { return Color.White; } }//
        public override Color TabButtonCheckedSelectedBorderOut
        { get { return Color.Gold; } }//
        public override Color TabButtonCheckedSelectedBorderIn
        { get { return Color.White; } }//
        #endregion

        #region RibbonBar
        public override Color RibbonBarNomalBegin
        { get { return Color.FromArgb(200, 217, 237); } }//
        public override Color RibbonBarNomalEnd
        { get { return Color.FromArgb(231, 242, 255); } }//
        public override Color RibbonBarNomalBorderIn
        { get { return Color.FromArgb(185, 208, 237); } }//
        public override Color RibbonBarNomalBorderOut
        { get { return Color.FromArgb(227, 237, 251); } }//
        //------------------------------------------------------
        public override Color RibbonBarCheckedBegin
        { get { return Color.FromArgb(253, 234, 157); } }//
        public override Color RibbonBarCheckedEnd
        { get { return Color.FromArgb(249, 170, 69); } }//
        public override Color RibbonBarCheckedBorderIn
        { get { return Color.FromArgb(249, 198, 90); } }//
        public override Color RibbonBarCheckedBorderOut
        { get { return Color.FromArgb(160, 142, 129, 101); } }//
        //------------------------------------------------------
        public override Color RibbonBarDisabledBorderIn
        { get { return Color.FromArgb(241, 243, 245); } }//
        public override Color RibbonBarDisabledBorderOut
        { get { return Color.FromArgb(197, 209, 222); } }//
        //------------------------------------------------------
        public override Color RibbonBarPressedBegin
        { get { return Color.FromArgb(118, 153, 200); } }//
        public override Color RibbonBarPressedEnd
        { get { return Color.FromArgb(184, 215, 253); } }//
        public override Color RibbonBarPressedBorderIn
        { get { return Color.FromArgb(227, 237, 251); } }//
        public override Color RibbonBarPressedBorderOut
        { get { return Color.FromArgb(100, 21, 66, 139); } }//
        //------------------------------------------------------
        public override Color RibbonBarSelectedBegin
        { get { return Color.FromArgb(100, 255, 255, 255); } }//
        public override Color RibbonBarSelectedEnd
        { get { return Color.FromArgb(0, 255, 255, 255); } }//
        public override Color RibbonBarSelectedBorderIn
        { get { return Color.FromArgb(227, 237, 251); } }//
        public override Color RibbonBarSelectedBorderOut
        { get { return Color.FromArgb(185, 208, 237); } }//
        #endregion

        #region RibbonBarGlyph
        public override Color RibbonBarGlyphCheckedBackground
        { get { return Color.FromArgb(253, 234, 157); } }//
        public override Color RibbonBarGlyphCheckedBorderIn
        { get { return Color.FromArgb(249, 198, 90); } }//
        public override Color RibbonBarGlyphCheckedBorderOut
        { get { return Color.FromArgb(160, 142, 129, 101); } }//
        //------------------------------------------------------
        public override Color RibbonBarGlyphDisabledBackground
        { get { return Color.FromArgb(224, 228, 232); } }//
        public override Color RibbonBarGlyphDisabledBorderIn
        { get { return Color.FromArgb(241, 243, 245); } }//
        public override Color RibbonBarGlyphDisabledBorderOut
        { get { return Color.FromArgb(197, 209, 222); } }//
        //------------------------------------------------------
        public override Color RibbonBarGlyphPressedBackground
        { get { return Color.FromArgb(248, 143, 44); } }//
        public override Color RibbonBarGlyphPressedBorderIn
        { get { return Color.FromArgb(249, 198, 90); } }//
        public override Color RibbonBarGlyphPressedBorderOut
        { get { return Color.FromArgb(142, 129, 101); } }//
        //------------------------------------------------------
        public override Color RibbonBarGlyphSelectedBackground
        { get { return Color.FromArgb(253, 241, 176); } }//
        public override Color RibbonBarGlyphSelectedBorderIn
        { get { return Color.FromArgb(255, 242, 199); } }//
        public override Color RibbonBarGlyphSelectedBorderOut
        { get { return Color.FromArgb(194, 169, 120); } }//
        #endregion

        #region RibbonBarTitle
        public override Color RibbonBarTitleBackground
        { get { return Color.FromArgb(194, 217, 240); } }//
        public override Color RibbonBarTitleCheckedBackground
        { get { return Color.FromArgb(253, 234, 157); } }//
        public override Color RibbonBarTitleDisabledBackground
        { get { return Color.FromArgb(224, 228, 232); } }//
        public override Color RibbonBarTitlePressedBackground
        { get { return Color.FromArgb(194, 217, 240); } }//
        public override Color RibbonBarTitleSelectedBackground
        { get { return Color.FromArgb(194, 217, 240); } }//
        #endregion

        #region LinkLabel
        public override Color LinkLabelomal
        { get { return Color.FromArgb(0, 0, 255); } }//1
        public override Color LinkLabelPressed
        { get { return Color.Red; } }//
        public override Color LinkLabelSelected
        { get { return Color.Purple; } }//1
        public override Color LinkLabelDisabled
        { get { return Color.FromArgb(143, 140, 127); } }//1
        public override Color LinkLabelVisited
        { get { return Color.FromArgb(128, 0, 128); } }//1
        #endregion

        #region Glyph
        public override Color Glyph
        { get { return Color.FromArgb(250, 103, 140, 189); } }//
        public override Color GlyphDisabled
        { get { return Color.FromArgb(250, 183, 183, 183); } }//
        public override Color GlyphLight
        { get { return Color.FromArgb(200, 255, 255, 255); } }//
        #endregion

        #region Arrow
        public override Color Arrow
        { get { return Color.FromArgb(103, 140, 189); } }//
        public override Color ArrowDisabled
        { get { return Color.FromArgb(183, 183, 183); } }//
        public override Color ArrowLight
        { get { return Color.FromArgb(200, 255, 255, 255); } }//
        #endregion

        #region Text
        public override Color ItemText
        { get { return Color.FromArgb(250, 21, 66, 139); } }//
        public override Color ItemTextDisabled
        { get { return Color.FromArgb(250, 183, 183, 183); } }//
        public override Color ItemTextLight
        { get { return Color.FromArgb(200, 255, 255, 255); } }//
        #endregion

        #region Button
        public override Color Buttonomal
        { get { return Color.FromArgb(250, 186, 209, 240); } }//
        public override Color ButtonomalCenter
        { get { return Color.FromArgb(255, 207, 224, 247); } }//
        public override Color ButtonomalOut
        { get { return Color.FromArgb(255, 193, 213, 241); } }//
        public override Color ButtonomalGlossyBegin
        { get { return Color.FromArgb(160, 222, 235, 254); } }//
        public override Color ButtonomalGlossyEnd
        { get { return Color.FromArgb(160, 203, 222, 246); } }//
        public override Color ButtonomalBorderIn
        { get { return Color.FromArgb(255, 227, 237, 251); } }//
        public override Color ButtonomalBorderOut
        { get { return Color.FromArgb(255, 185, 208, 237); } }//
        //------------------------------------------------------
        public override Color ButtonChecked
        { get { return Color.FromArgb(255, 249, 170, 69); } }//
        public override Color ButtonCheckedCenter
        { get { return Color.FromArgb(255, 253, 234, 157); } }//
        public override Color ButtonCheckedOut
        { get { return Color.FromArgb(255, 249, 170, 69); } }//
        public override Color ButtonCheckedGlossyBegin
        { get { return Color.FromArgb(255, 248, 219, 183); } }//
        public override Color ButtonCheckedGlossyEnd
        { get { return Color.FromArgb(255, 254, 209, 142); } }//
        public override Color ButtonCheckedBorderIn
        { get { return Color.FromArgb(255, 249, 198, 90); } }//
        public override Color ButtonCheckedBorderOut
        { get { return Color.FromArgb(160, 142, 129, 101); } }//
        //------------------------------------------------------
        public override Color ButtonDisabled
        { get { return Color.FromArgb(255, 224, 228, 232); } }
        public override Color ButtonDisabledCenter
        { get { return Color.FromArgb(255, 232, 235, 239); } }//
        public override Color ButtonDisabledOut
        { get { return Color.FromArgb(255, 224, 228, 232); } }//
        public override Color ButtonDisabledGlossyBegin
        { get { return Color.FromArgb(255, 240, 243, 146); } }//
        public override Color ButtonDisabledGlossyEnd
        { get { return Color.FromArgb(255, 234, 237, 241); } }//
        public override Color ButtonDisabledBorderIn
        { get { return Color.FromArgb(255, 241, 243, 245); } }//
        public override Color ButtonDisabledBorderOut
        { get { return Color.FromArgb(255, 197, 209, 222); } }//
        //------------------------------------------------------
        public override Color ButtonPressed
        { get { return Color.FromArgb(255, 255, 128, 0); } }//
        public override Color ButtonPressedCenter
        { get { return Color.FromArgb(255, 253, 241, 176); } }//
        public override Color ButtonPressedOut
        { get { return Color.FromArgb(255, 248, 143, 44); } }//
        public override Color ButtonPressedGlossyBegin
        { get { return Color.FromArgb(255, 253, 213, 168); } }//
        public override Color ButtonPressedGlossyEnd
        { get { return Color.FromArgb(255, 251, 176, 98); } }//
        public override Color ButtonPressedBorderIn
        { get { return Color.FromArgb(255, 249, 198, 90); } }//
        public override Color ButtonPressedBorderOut
        { get { return Color.FromArgb(255, 142, 129, 101); } }//
        //------------------------------------------------------
        public override Color ButtonSelected
        { get { return Color.FromArgb(255, 255, 214, 78); } }//
        public override Color ButtonSelectedCenter
        { get { return Color.FromArgb(255, 255, 234, 172); } }//
        public override Color ButtonSelectedOut
        { get { return Color.FromArgb(255, 255, 214, 70); } }//
        public override Color ButtonSelectedGlossyBegin
        { get { return Color.FromArgb(255, 255, 253, 219); } }//
        public override Color ButtonSelectedGlossyEnd
        { get { return Color.FromArgb(255, 255, 231, 147); } }//
        public override Color ButtonSelectedBorderIn
        { get { return Color.FromArgb(255, 255, 242, 199); } }//
        public override Color ButtonSelectedBorderOut
        { get { return Color.FromArgb(255, 194, 169, 120); } }//
        #endregion

        #region ButtonGroup
        public override Color ButtonGroupNomalBegin
        { get { return Color.FromArgb(202, 220, 240); } }//
        public override Color ButtonGroupNomalEnd
        { get { return Color.FromArgb(208, 225, 247); } }//
        public override Color ButtonGroupNomalGlossyBegin
        { get { return Color.FromArgb(188, 208, 233); } }//
        public override Color ButtonGroupNomalGlossyEnd
        { get { return Color.FromArgb(0, 255, 255, 255); } }//
        public override Color ButtonGroupNomalBorderIn
        { get { return Color.FromArgb(21, 255, 255, 255); } }//
        public override Color ButtonGroupNomalBorderOut
        { get { return Color.FromArgb(158, 186, 225); } }//
        //------------------------------------------------------
        public override Color ButtonGroupCheckedBegin
        { get { return Color.FromArgb(253, 234, 157); } }//
        public override Color ButtonGroupCheckedEnd
        { get { return Color.FromArgb(249, 170, 69); } }//
        public override Color ButtonGroupCheckedGlossyBegin
        { get { return Color.FromArgb(248, 219, 183); } }//
        public override Color ButtonGroupCheckedGlossyEnd
        { get { return Color.FromArgb(254, 209, 142); } }//
        public override Color ButtonGroupCheckedBorderIn
        { get { return Color.FromArgb(249, 198, 90); } }//
        public override Color ButtonGroupCheckedBorderOut
        { get { return Color.FromArgb(160, 142, 129, 101); } }//
        //------------------------------------------------------
        public override Color ButtonGroupDisabledBegin
        { get { return Color.FromArgb(232, 235, 239); } }//
        public override Color ButtonGroupDisabledEnd
        { get { return Color.FromArgb(224, 228, 232); } }//
        public override Color ButtonGroupDisabledGlossyBegin
        { get { return Color.FromArgb(240, 243, 146); } }//
        public override Color ButtonGroupDisabledGlossyEnd
        { get { return Color.FromArgb(234, 237, 241); } }//
        public override Color ButtonGroupDisabledBorderIn
        { get { return Color.FromArgb(241, 243, 245); } }//
        public override Color ButtonGroupDisabledBorderOut
        { get { return Color.FromArgb(197, 209, 222); } }//
        //------------------------------------------------------
        public override Color ButtonGroupPressedBegin
        { get { return Color.FromArgb(253, 241, 176); } }//
        public override Color ButtonGroupPressedEnd
        { get { return Color.FromArgb(248, 143, 44); } }//
        public override Color ButtonGroupPressedGlossyBegin
        { get { return Color.FromArgb(253, 213, 168); } }//
        public override Color ButtonGroupPressedGlossyEnd
        { get { return Color.FromArgb(251, 176, 98); } }//
        public override Color ButtonGroupPressedBorderIn
        { get { return Color.FromArgb(249, 198, 90); } }//
        public override Color ButtonGroupPressedBorderOut
        { get { return Color.FromArgb(142, 129, 101); } }//
        //------------------------------------------------------
        public override Color ButtonGroupSelectedBegin
        { get { return Color.FromArgb(255, 234, 172); } }//
        public override Color ButtonGroupSelectedEnd
        { get { return Color.FromArgb(255, 214, 70); } }//
        public override Color ButtonGroupSelectedGlossyBegin
        { get { return Color.FromArgb(255, 253, 219); } }//
        public override Color ButtonGroupSelectedGlossyEnd
        { get { return Color.FromArgb(255, 231, 147); } }//
        public override Color ButtonGroupSelectedBorderIn
        { get { return Color.FromArgb(255, 242, 199); } }//
        public override Color ButtonGroupSelectedBorderOut
        { get { return Color.FromArgb(194, 169, 120); } }//
        #endregion

        #region Gallery
        public override Color GalleryNomalBorder
        { get { return Color.FromArgb(255, 185, 208, 237); } }//
        public override Color GalleryNomalBackground
        { get { return Color.FromArgb(255, 212, 230, 248); } }//
        //------------------------------------------------------
        public override Color GalleryCheckedBorder
        { get { return Color.FromArgb(255, 249, 198, 90); } }//
        public override Color GalleryCheckedBackground
        { get { return Color.FromArgb(255, 236, 243, 251); } }//
        //------------------------------------------------------
        public override Color GalleryDisabledBorder
        { get { return Color.FromArgb(255, 197, 209, 222); } }//
        public override Color GalleryDisabledBackground
        { get { return Color.FromArgb(255, 224, 228, 232); } }
        //------------------------------------------------------
        public override Color GalleryPressedBorder
        { get { return Color.FromArgb(255, 185, 208, 237); } }//
        public override Color GalleryPressedBackground
        { get { return Color.FromArgb(255, 236, 243, 251); } }//
        //------------------------------------------------------
        public override Color GallerySelectedBorder
        { get { return Color.FromArgb(255, 185, 208, 237); } }//
        public override Color GallerySelectedBackground
        { get { return Color.FromArgb(255, 236, 243, 251); } }//
        #endregion

        #region RibbonGalleryPopupPanel
        public override Color RibbonGalleryPopupPanelBorber
        { get { return Color.FromArgb(134, 134, 134); } }//
        public override Color RibbonGalleryPopupPanelBackground
        { get { return Color.FromArgb(239, 245, 250); } }//
        #endregion

        #region BaseItemStackEx
        public override Color BaseItemStackExNomalBorder
        { get { return Color.FromArgb(255, 185, 208, 237); } }//
        public override Color BaseItemStackExNomalBackground
        { get { return Color.FromArgb(255, 212, 230, 248); } }//
        //------------------------------------------------------
        public override Color BaseItemStackExCheckedBorder
        { get { return Color.FromArgb(255, 249, 198, 90); } }//
        public override Color BaseItemStackExCheckedBackground
        { get { return Color.FromArgb(255, 236, 243, 251); } }//
        //------------------------------------------------------
        public override Color BaseItemStackExDisabledBorder
        { get { return Color.FromArgb(255, 197, 209, 222); } }//
        public override Color BaseItemStackExDisabledBackground
        { get { return Color.FromArgb(255, 224, 228, 232); } }
        //------------------------------------------------------
        public override Color BaseItemStackExPressedBorder
        { get { return Color.FromArgb(255, 185, 208, 237); } }//
        public override Color BaseItemStackExPressedBackground
        { get { return Color.FromArgb(255, 236, 243, 251); } }//
        //------------------------------------------------------
        public override Color BaseItemStackExSelectedBorder
        { get { return Color.FromArgb(255, 185, 208, 237); } }//
        public override Color BaseItemStackExSelectedBackground
        { get { return Color.FromArgb(255, 236, 243, 251); } }//
        #endregion

        #region PopupPanel
        public override Color PopupPanelBorder
        { get { return Color.FromArgb(134, 134, 134); } }//
        public override Color PopupPanelBackground
        { get { return Color.FromArgb(250, 250, 250); } }//
        public override Color PopupPanelImageGripBackground
        { get { return Color.FromArgb(233, 238, 238); } }//
        public override Color PopupPanelImageGripSeparator
        { get { return Color.FromArgb(197, 197, 197); } }//
        public override Color PopupPanelCheckGripBegin
        { get { return Color.FromArgb(255, 255, 255); } }//
        public override Color PopupPanelCheckGripEnd
        { get { return Color.FromArgb(223, 233, 239); } }//
        #endregion

        #region ToolTipPopupPanel
        public override Color ToolTipPopupPanelBegin
        { get { return Color.FromArgb(251, 255, 255); } }//
        public override Color ToolTipPopupPanelEnd
        { get { return Color.FromArgb(235, 238, 247); } }//
        #endregion

        #region PopupItemButtonChecked
        //public override Color PopupItemButtonChecked
        //{ get { return Color.FromArgb(103, 140, 189); } }//
        public override Color PopupItemButtonCheckedBorder
        { get { return Color.FromArgb(249, 198, 90); } }//
        public override Color PopupItemButtonCheckedBackgound
        { get { return Color.FromArgb(253, 241, 176); } }//
        //------------------------------------------------------
        //public override Color PopupItemButtonCheckedDisabled
        //{ get { return Color.FromArgb(183, 183, 183); } }//
        public override Color PopupItemButtonCheckedDisabledBorder
        { get { return Color.FromArgb(183, 183, 183); } }//
        public override Color PopupItemButtonCheckedDisabledBackgound
        { get { return Color.FromArgb(255, 255, 255); } }//
        #endregion

        #region CheckBox And RadioButton
        public override Color CheckBoxomalOutLine
        { get { return Color.FromArgb(171, 193, 222); } }//
        public override Color CheckBoxomalBackgroundOutBegin
        { get { return Color.FromArgb(241, 241, 241); } }//
        public override Color CheckBoxomalBackgroundOutEnd
        { get { return Color.FromArgb(244, 244, 244); } }//
        public override Color CheckBoxomalBackgroundMiddleBegin
        { get { return Color.FromArgb(165, 175, 187); } }//
        public override Color CheckBoxomalBackgroundMiddleEnd
        { get { return Color.FromArgb(212, 216, 221); } }//
        public override Color CheckBoxomalBackgroundIntBegin
        { get { return Color.FromArgb(189, 196, 204); } }//
        public override Color CheckBoxomalBackgroundIntEnd
        { get { return Color.FromArgb(231, 233, 235); } }//
        //------------------------------------------------------
        public override Color CheckBoxDisabledOutLine
        { get { return Color.FromArgb(174, 177, 181); } }//
        public override Color CheckBoxDisabledBackgroundOutBegin
        { get { return Color.FromArgb(244, 244, 244); } }//
        public override Color CheckBoxDisabledBackgroundOutEnd
        { get { return Color.FromArgb(255, 255, 255); } }//
        public override Color CheckBoxDisabledBackgroundMiddleBegin
        { get { return Color.FromArgb(229, 231, 233); } }//
        public override Color CheckBoxDisabledBackgroundMiddleEnd
        { get { return Color.FromArgb(240, 241, 243); } }//
        public override Color CheckBoxDisabledBackgroundIntBegin
        { get { return Color.FromArgb(234, 235, 237); } }//
        public override Color CheckBoxDisabledBackgroundIntEnd
        { get { return Color.FromArgb(248, 248, 249); } }//
        //------------------------------------------------------
        public override Color CheckBoxPressedOutLine
        { get { return Color.FromArgb(85, 119, 163); } }//
        public override Color CheckBoxPressedBackgroundOutBegin
        { get { return Color.FromArgb(217, 228, 243); } }//
        public override Color CheckBoxPressedBackgroundOutEnd
        { get { return Color.FromArgb(193, 216, 245); } }//
        public override Color CheckBoxPressedBackgroundMiddleBegin
        { get { return Color.FromArgb(235, 149, 70); } }//
        public override Color CheckBoxPressedBackgroundMiddleEnd
        { get { return Color.FromArgb(248, 200, 142); } }//
        public override Color CheckBoxPressedBackgroundIntBegin
        { get { return Color.FromArgb(230, 165, 104); } }//
        public override Color CheckBoxPressedBackgroundIntEnd
        { get { return Color.FromArgb(232, 217, 194); } }//
        //------------------------------------------------------
        public override Color CheckBoxSelectedOutLine
        { get { return Color.FromArgb(85, 119, 163); } }//
        public override Color CheckBoxSelectedBackgroundOutBegin
        { get { return Color.FromArgb(236, 239, 243); } }//
        public override Color CheckBoxSelectedBackgroundOutEnd
        { get { return Color.FromArgb(222, 234, 250); } }//
        public override Color CheckBoxSelectedBackgroundMiddleBegin
        { get { return Color.FromArgb(246, 216, 142); } }//
        public override Color CheckBoxSelectedBackgroundMiddleEnd
        { get { return Color.FromArgb(252, 230, 174); } }//
        public override Color CheckBoxSelectedBackgroundIntBegin
        { get { return Color.FromArgb(242, 220, 164); } }//
        public override Color CheckBoxSelectedBackgroundIntEnd
        { get { return Color.FromArgb(240, 236, 220); } }//
        #endregion

        #region Separator
        public override Color SeparatorBackground
        { get { return Color.FromArgb(218, 230, 238); } }//
        public override Color SeparatorLight
        { get { return Color.FromArgb(250, 251, 253); } }//
        public override Color SeparatorDark
        { get { return Color.FromArgb(150, 180, 218); } }//
        #endregion

        #region TextBox
        public override Color TextBoxomalText
        { get { return Color.FromArgb(255, 21, 66, 139); } }//
        public override Color TextBoxomalBorder
        { get { return Color.FromArgb(255, 185, 208, 237); } }//
        public override Color TextBoxomalBackground
        { get { return Color.FromArgb(255, 234, 242, 251); } }//

        public override Color TextBoxSelectedText
        { get { return Color.FromArgb(255, 21, 66, 139); } }//
        public override Color TextBoxSelectedBorder
        { get { return Color.FromArgb(255, 150, 180, 218); } }//
        public override Color TextBoxSelectedBackground
        { get { return SystemColors.Window; } }//

        public override Color TextBoxDisabledText
        { get { return Color.FromArgb(255, 21, 66, 139); } }//
        public override Color TextBoxDisabledBorder
        { get { return Color.FromArgb(255, 197, 209, 222); } }//
        public override Color TextBoxDisabledBackground
        { get { return Color.FromArgb(255, 224, 228, 232); } }
        #endregion

        #region ProcessBar
        public override Color ProcessBarBorderIn
        { get { return Color.FromArgb(255, 188, 208, 233); } }
        public override Color ProcessBarBorderOut
        { get { return Color.FromArgb(255, 129, 150, 171); } }
        public override Color ProcessBarValueBegin
        { get { return Color.FromArgb(255, 153, 186, 105); } }
        public override Color ProcessBarValueEnd
        { get { return Color.FromArgb(255, 215, 230, 199); } }
        public override Color ProcessBarBackgroundBegin
        { get { return Color.FromArgb(255, 201, 203, 215); } }
        public override Color ProcessBarBackgroundEnd
        { get { return Color.FromArgb(255, 219, 228, 237); } }
        #endregion

        #region ScrollBar
        public override Color ScrollBarBackgroundBegin
        { get { return Color.FromArgb(255, 240, 240, 240); } }
        public override Color ScrollBarBackgroundEnd
        { get { return Color.FromArgb(255, 251, 251, 251); } }
        #endregion

        #region RatingStar
        public override Color RatingStarBorder
        { get { return Color.FromArgb(255, 187, 196, 205); } }//
        public override Color RatingStarSelectedBegin
        { get { return Color.FromArgb(255, 255, 252, 202); } }
        public override Color RatingStarSelectedEnd
        { get { return Color.FromArgb(255, 255, 217, 156); } }
        #endregion

    }
}