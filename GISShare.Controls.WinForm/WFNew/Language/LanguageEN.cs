using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public class LanguageEN : Language
    {
        public override string CustomizeTitleText { get { return "Ribbon Customize"; } }
        public override string TopViewQuickAccessToolbarText { get { return "Top QuickAccessToolbar"; } }
        public override string BottomViewQuickAccessToolbarText { get { return "Bottom QuickAccessToolbar"; } }
        public override string DisplayRibbonPageContainerText { get { return "Display RibbonPageContainer"; } }
        public override string HideRibbonPageContainerText { get { return "Hide RibbonPageContainer"; } }
        public override string CustomizeQuickAccessToolbarText { get { return "Customize QuickAccessToolbar"; } }
        //
        public override string CustomizeItemNotNamedText { get { return "Unnaming"; } }
    }
}
