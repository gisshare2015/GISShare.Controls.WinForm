using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GISShare.Controls.WinForm
{
    public class WinFormColorTable : System.Windows.Forms.ProfessionalColorTable
    {
        #region ProfessionalColorTable
        public override Color ButtonCheckedGradientBegin
        { get { return Color.FromArgb(253, 241, 176); } }//
        public override Color ButtonCheckedGradientEnd
        { get { return Color.FromArgb(249, 170, 69); } }//
        public override Color ButtonCheckedGradientMiddle
        { get { return Color.FromArgb(249, 200, 69); } }//
        public override Color ButtonCheckedHighlight
        {
            get
            {
                return base.ButtonCheckedHighlight;
            }
        }
        public override Color ButtonCheckedHighlightBorder
        {
            get
            {
                return base.ButtonCheckedHighlightBorder;
            }
        }

        public override Color ButtonPressedBorder
        {
            get { return Color.FromArgb(255, 189, 105); }
        }
        public override Color ButtonPressedGradientBegin
        {
            get { return Color.FromArgb(248, 181, 106); }
        }
        public override Color ButtonPressedGradientEnd
        {
            get { return Color.FromArgb(255, 208, 134); }
        }
        public override Color ButtonPressedGradientMiddle
        {
            get { return Color.FromArgb(251, 140, 60); }
        }
        public override Color ButtonPressedHighlight
        {
            get
            {
                return base.ButtonPressedHighlight;
            }
        }
        public override Color ButtonPressedHighlightBorder
        {
            get
            {
                return base.ButtonPressedHighlightBorder;
            }
        }
        public override Color ButtonSelectedHighlight
        {
            get
            {
                return base.ButtonSelectedHighlight;
            }
        }

        public override Color ButtonSelectedBorder
        {
            get
            {
                return Color.FromArgb(255, 189, 105);// base.ButtonSelectedBorder;
            }
        }
        public override Color ButtonSelectedGradientBegin
        {
            get { return Color.FromArgb(255, 245, 204); }
        }
        public override Color ButtonSelectedGradientEnd
        {
            get { return Color.FromArgb(255, 219, 117); }
        }
        public override Color ButtonSelectedGradientMiddle
        {
            get { return Color.FromArgb(255, 231, 162); }
        }
        public override Color ButtonSelectedHighlightBorder
        {
            get { return Color.FromArgb(255, 189, 105); }
        }

        public override Color CheckBackground
        {
            get { return Color.FromArgb(255, 227, 149); }
        }
        public override Color CheckPressedBackground
        {
            get
            {
                return base.CheckPressedBackground;
            }
        }
        public override Color CheckSelectedBackground
        {
            get { return Color.FromArgb(254, 128, 62); }
        }

        public override Color GripDark
        {
            get { return Color.FromArgb(111, 157, 217); }
        }
        public override Color GripLight
        {
            get { return Color.FromArgb(255, 255, 255); }
        }

        public override Color ImageMarginGradientBegin
        {
            get { return Color.FromArgb(152, 186, 230); }
        }
        public override Color ImageMarginGradientEnd
        {
            get { return Color.FromArgb(227, 239, 255); }
        }
        public override Color ImageMarginGradientMiddle
        {
            get { return Color.FromArgb(222, 236, 255); }
        }

        public override Color ImageMarginRevealedGradientBegin
        {
            get
            {
                return base.ImageMarginRevealedGradientBegin;
            }
        }
        public override Color ImageMarginRevealedGradientEnd
        {
            get
            {
                return base.ImageMarginRevealedGradientEnd;
            }
        }
        public override Color ImageMarginRevealedGradientMiddle
        {
            get
            {
                return base.ImageMarginRevealedGradientMiddle;
            }
        }

        public override Color MenuBorder
        {
            get { return Color.FromArgb(134, 134, 134); }
        }
        public override Color MenuItemBorder
        {
            get { return Color.FromArgb(255, 189, 105); }
        }

        public override Color MenuItemPressedGradientBegin
        {
            get { return Color.FromArgb(227, 239, 255); }
        }
        public override Color MenuItemPressedGradientEnd
        {
            get { return Color.FromArgb(152, 186, 230); }
        }
        public override Color MenuItemPressedGradientMiddle
        {
            get { return Color.FromArgb(222, 236, 255); }
        }

        public override Color MenuItemSelected
        {
            get { return Color.FromArgb(255, 238, 194); }
        }
        public override Color MenuItemSelectedGradientBegin
        {
            get { return Color.FromArgb(255, 213, 103); }
        }
        public override Color MenuItemSelectedGradientEnd
        {
            get { return Color.FromArgb(255, 228, 145); }
        }

        public override Color MenuStripGradientBegin
        {
            get { return Color.FromArgb(191, 219, 255); }
        }
        public override Color MenuStripGradientEnd
        {
            get { return Color.FromArgb(191, 219, 255); }
        }

        public override Color OverflowButtonGradientBegin
        {
            get { return Color.FromArgb(167, 204, 251); }
        }
        public override Color OverflowButtonGradientEnd
        {
            get { return Color.FromArgb(101, 147, 207); }
        }
        public override Color OverflowButtonGradientMiddle
        {
            get { return Color.FromArgb(167, 204, 251); }
        }

        public override Color RaftingContainerGradientBegin
        {
            get { return Color.FromArgb(191, 219, 255); }
        }
        public override Color RaftingContainerGradientEnd
        {
            get { return Color.FromArgb(191, 219, 255); }
        }

        public override Color SeparatorDark
        {
            get { return Color.FromArgb(154, 198, 255); }
        }
        public override Color SeparatorLight
        {
            get { return Color.FromArgb(255, 255, 255); }
        }

        public override Color StatusStripGradientBegin
        {
            get { return Color.FromArgb(215, 229, 247); }
        }
        public override Color StatusStripGradientEnd
        {
            get { return Color.FromArgb(172, 201, 238); }
        }

        public override Color ToolStripBorder
        {
            get { return Color.FromArgb(111, 157, 217); }
        }
        public override Color ToolStripContentPanelGradientBegin
        {
            get { return Color.FromArgb(164, 195, 235); }
        }
        public override Color ToolStripContentPanelGradientEnd
        {
            get { return Color.FromArgb(191, 219, 255); }
        }

        public override Color ToolStripDropDownBackground
        {
            get { return Color.FromArgb(250, 250, 250); }
        }

        public override Color ToolStripGradientBegin
        {
            get { return Color.FromArgb(227, 239, 255); }
        }
        public override Color ToolStripGradientEnd
        {
            get { return Color.FromArgb(152, 186, 230); }
        }
        public override Color ToolStripGradientMiddle
        {
            get { return Color.FromArgb(222, 236, 255); }
        }
        
        public override Color ToolStripPanelGradientBegin
        {
            get { return Color.FromArgb(191, 219, 255); }
        }
        public override Color ToolStripPanelGradientEnd
        {
            get { return Color.FromArgb(191, 219, 255); }
        }
        #endregion

        public virtual Color ButtonomalBorder
        {
            get { return Color.FromArgb(170, 180, 192); }
        }
        public virtual Color ButtonomalGradientBegin
        {
            get { return Color.FromArgb(227, 239, 255); }
        }
        public virtual Color ButtonomalGradientEnd
        {
            get { return Color.FromArgb(152, 186, 230); }
        }
        public virtual Color ButtonomalGradientMiddle
        {
            get { return Color.FromArgb(222, 236, 255); }
        }

        public virtual Color ButtonDisabledBorder
        {
            get { return Color.FromArgb(170, 180, 192); }
        }
        public virtual Color ButtonDisabledGradientBegin
        {
            get { return Color.FromArgb(227, 239, 255); }
        }
        public virtual Color ButtonDisabledGradientEnd
        {
            get { return Color.FromArgb(152, 186, 230); }
        }
        public virtual Color ButtonDisabledGradientMiddle
        {
            get { return Color.FromArgb(222, 236, 255); }
        }

        public virtual Color ButtonCheckedBorder
        { get { return Color.FromArgb(160, 142, 129, 101); } }//

        public virtual Color ToolStripPanelGradientBorder
        {
            get { return Color.FromArgb(170, 180, 192); }
        }

        #region CheckBox And RadioButton
        public virtual Color CheckBoxomalOutLine
        { get { return Color.FromArgb(85, 119, 163); } }//
        public virtual Color CheckBoxomalBackgroundOutBegin
        { get { return Color.FromArgb(241, 241, 241); } }//
        public virtual Color CheckBoxomalBackgroundOutEnd
        { get { return Color.FromArgb(244, 244, 244); } }//
        public virtual Color CheckBoxomalBackgroundMiddleBegin
        { get { return Color.FromArgb(125, 139, 173); } }//
        //{ get { return Color.FromArgb(165, 175, 187); } }//
        public virtual Color CheckBoxomalBackgroundMiddleEnd
        { get { return Color.FromArgb(105, 129, 143); } }//
        //{ get { return Color.FromArgb(212, 216, 221); } }//
        public virtual Color CheckBoxomalBackgroundIntBegin
        { 
            get 
            {
                return this.GripLight;
                //return Color.FromArgb(189, 196, 204);
            } 
        }//
        public virtual Color CheckBoxomalBackgroundIntEnd
        {
            get
            {
                return this.GripDark;
                //return Color.FromArgb(231, 233, 235); 
            }
        }//
        //------------------------------------------------------
        public virtual Color CheckBoxDisabledOutLine
        { get { return Color.FromArgb(174, 177, 181); } }//
        public virtual Color CheckBoxDisabledBackgroundOutBegin
        { get { return Color.FromArgb(244, 244, 244); } }//
        public virtual Color CheckBoxDisabledBackgroundOutEnd
        { get { return Color.FromArgb(255, 255, 255); } }//
        public virtual Color CheckBoxDisabledBackgroundMiddleBegin
        { get { return Color.FromArgb(229, 231, 233); } }//
        public virtual Color CheckBoxDisabledBackgroundMiddleEnd
        { get { return Color.FromArgb(240, 241, 243); } }//
        public virtual Color CheckBoxDisabledBackgroundIntBegin
        { get { return Color.FromArgb(234, 235, 237); } }//
        public virtual Color CheckBoxDisabledBackgroundIntEnd
        { get { return Color.FromArgb(248, 248, 249); } }//
        //------------------------------------------------------
        public virtual Color CheckBoxPressedOutLine
        { get { return Color.FromArgb(85, 119, 163); } }//
        public virtual Color CheckBoxPressedBackgroundOutBegin
        { get { return Color.FromArgb(217, 228, 243); } }//
        public virtual Color CheckBoxPressedBackgroundOutEnd
        { get { return Color.FromArgb(193, 216, 245); } }//
        public virtual Color CheckBoxPressedBackgroundMiddleBegin
        { get { return Color.FromArgb(235, 149, 70); } }//
        public virtual Color CheckBoxPressedBackgroundMiddleEnd
        { get { return Color.FromArgb(248, 200, 142); } }//
        public virtual Color CheckBoxPressedBackgroundIntBegin
        { get { return Color.FromArgb(230, 165, 104); } }//
        public virtual Color CheckBoxPressedBackgroundIntEnd
        { get { return Color.FromArgb(232, 217, 194); } }//
        //------------------------------------------------------
        public virtual Color CheckBoxSelectedOutLine
        { get { return Color.FromArgb(85, 119, 163); } }//
        public virtual Color CheckBoxSelectedBackgroundOutBegin
        { get { return Color.FromArgb(236, 239, 243); } }//
        public virtual Color CheckBoxSelectedBackgroundOutEnd
        { get { return Color.FromArgb(222, 234, 250); } }//
        public virtual Color CheckBoxSelectedBackgroundMiddleBegin
        { get { return Color.FromArgb(246, 216, 142); } }//
        public virtual Color CheckBoxSelectedBackgroundMiddleEnd
        { get { return Color.FromArgb(252, 230, 174); } }//
        public virtual Color CheckBoxSelectedBackgroundIntBegin
        { get { return Color.FromArgb(242, 220, 164); } }//
        public virtual Color CheckBoxSelectedBackgroundIntEnd
        { get { return Color.FromArgb(240, 236, 220); } }//
        #endregion

        #region Text
        public virtual Color ItemText
        { get { return Color.FromArgb(250, 21, 66, 139); } }//
        public virtual Color ItemTextDisabled
        { get { return Color.FromArgb(250, 183, 183, 183); } }//
        public virtual Color ItemTextLight
        { get { return Color.FromArgb(200, 255, 255, 255); } }//
        #endregion

        #region Arrow
        public virtual Color Arrow
        { get { return Color.FromArgb(103, 140, 189); } }//
        public virtual Color ArrowDisabled
        { get { return Color.FromArgb(183, 183, 183); } }//
        public virtual Color ArrowLight
        { get { return Color.FromArgb(200, 255, 255, 255); } }//
        #endregion
    }
}
