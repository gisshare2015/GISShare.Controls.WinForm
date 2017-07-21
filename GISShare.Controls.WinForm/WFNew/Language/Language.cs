using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public abstract class Language
    {
        public static Language LanguageStrategy = new LanguageCH();
        //
        public abstract string CustomizeTitleText { get;}
        public abstract string TopViewQuickAccessToolbarText { get;}
        public abstract string BottomViewQuickAccessToolbarText { get;}
        public abstract string DisplayRibbonPageContainerText { get;}
        public abstract string HideRibbonPageContainerText { get;}
        public abstract string CustomizeQuickAccessToolbarText { get;}
        //
        public abstract string CustomizeItemNotNamedText { get;}
        ////
        //public abstract string SeparatorItemDescribe { get; }
    }
}
