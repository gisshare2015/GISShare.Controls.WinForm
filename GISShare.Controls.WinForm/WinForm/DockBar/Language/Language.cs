using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.DockBar
{
    public abstract class Language
    {
        public static Language LanguageStrategy = new LanguageCH();
        //
        public abstract string CustomizeFormTitle { get; }
        public abstract string CustomizeForm_ButtonCancelText { get; }
        public abstract string CustomizeForm_TabPageBarText { get; }
        public abstract string CustomizeForm_TabPageBar_LabelBarText { get; }
        public abstract string CustomizeForm_TabPageBar_ButtonewText { get; }
        public abstract string CustomizeForm_TabPageBar_ButtonRenameText { get; }
        public abstract string CustomizeForm_TabPageBar_ButtonDeleteText { get; }
        public abstract string CustomizeForm_TabPageBar_ButtonResetText { get; }
        public abstract string CustomizeForm_TabPageBar_CheckBoxLargeImageText { get; }
        public abstract string CustomizeForm_TabPageBar_CheckBoxToolTipText { get; }
        public abstract string CustomizeForm_TabPageItemText { get; }
        public abstract string CustomizeForm_TabPageItem_LabelCategoryText { get; }
        public abstract string CustomizeForm_TabPageItem_LabelItemText { get; }
        public abstract string CustomizeForm_TabPageItem_LabelTipText { get; }
        //
        public abstract string CreateOrModifyFormTitle_Create { get; }
        public abstract string CreateOrModifyFormTitle_Modify { get; }
        public abstract string CreateOrModifyForm_LabelameText { get; }
        public abstract string CreateOrModifyForm_TextBoxameText { get; }
        public abstract string CreateOrModifyForm_ButtonOkText { get; }
        public abstract string CreateOrModifyForm_ButtonCancelText { get; }
        //
        public abstract string MainMenuText { get; }
        public abstract string StatusBarText { get; }
        public abstract string CustomizeText { get; }
        public abstract string AddOrRemoveText { get; }
        public abstract string ResetToolbarText { get; }
        public abstract string StandardText { get; }
        public abstract string ResetText { get; }
        public abstract string AddToText { get; }
        public abstract string ItemText { get; }
        public abstract string InText { get; }
        public abstract string PositionText { get; }
        public abstract string DoubleQuotationMarks_Left { get; }
        public abstract string DoubleQuotationMarks_Right { get; }
        //
        public abstract string DefaultText { get; }
    }
}
