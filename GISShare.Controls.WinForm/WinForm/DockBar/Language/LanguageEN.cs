using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.DockBar
{
    public class LanguageEN : Language
    {
        public override string CustomizeFormTitle { get { return "Customize"; } }
        public override string CustomizeForm_ButtonCancelText { get { return "Close"; } }
        public override string CustomizeForm_TabPageBarText { get { return "ToolBar"; } }
        public override string CustomizeForm_TabPageBar_LabelBarText { get { return "Toolbar£º"; } }
        public override string CustomizeForm_TabPageBar_ButtonewText { get { return "New"; } }
        public override string CustomizeForm_TabPageBar_ButtonRenameText { get { return "Rename"; } }
        public override string CustomizeForm_TabPageBar_ButtonDeleteText { get { return "Delete"; } }
        public override string CustomizeForm_TabPageBar_ButtonResetText { get { return "Reset"; } }
        public override string CustomizeForm_TabPageBar_CheckBoxLargeImageText { get { return "Using Large Image"; } }
        public override string CustomizeForm_TabPageBar_CheckBoxToolTipText { get { return "Show Tooltips"; } }
        public override string CustomizeForm_TabPageItemText { get { return "Command"; } }
        public override string CustomizeForm_TabPageItem_LabelCategoryText { get { return "Category:"; } }
        public override string CustomizeForm_TabPageItem_LabelItemText { get { return "Command:"; } }
        public override string CustomizeForm_TabPageItem_LabelTipText { get { return "Add command, the command for the right click."; } }
        //
        public override string CreateOrModifyFormTitle_Create { get { return "Create Toolbar"; } }
        public override string CreateOrModifyFormTitle_Modify { get { return "Rename Toolbar"; } }
        public override string CreateOrModifyForm_LabelameText { get { return "Toolbar Name:"; } }
        public override string CreateOrModifyForm_TextBoxameText { get { return "New Toolbar"; } }
        public override string CreateOrModifyForm_ButtonOkText { get { return "OK"; } }
        public override string CreateOrModifyForm_ButtonCancelText { get { return "Cancel"; } }
        //
        public override string MainMenuText { get { return "MainMenu"; } }
        public override string StatusBarText { get { return "StatusBar"; } }
        public override string CustomizeText { get { return "Customize..."; } }
        public override string AddOrRemoveText { get { return "Add Or Remove Item"; } }
        public override string ResetToolbarText { get { return "Reset Toolbar"; } }
        public override string StandardText { get { return "Standard"; } }
        public override string ResetText { get { return "Reset"; } }
        public override string AddToText { get { return "Add To"; } }
        public override string ItemText { get { return "Item"; } }
        public override string InText { get { return "In "; } }
        public override string PositionText { get { return " Position"; } }
        public override string DoubleQuotationMarks_Left { get { return "\""; } }
        public override string DoubleQuotationMarks_Right { get { return "\""; } }
        //
        public override string DefaultText { get { return "Default"; } }
    }
}
