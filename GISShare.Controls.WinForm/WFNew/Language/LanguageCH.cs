using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public class LanguageCH : Language
    {
        public override string CustomizeTitleText { get { return "自定义功能区"; } }
        public override string TopViewQuickAccessToolbarText { get { return "上方显示工具条"; } }
        public override string BottomViewQuickAccessToolbarText { get { return "下方显示工具条"; } }
        public override string DisplayRibbonPageContainerText { get { return "展现功能区"; } }
        public override string HideRibbonPageContainerText { get { return "隐藏功能区"; } }
        public override string CustomizeQuickAccessToolbarText { get { return "自定义工具条"; } }
        //
        public override string CustomizeItemNotNamedText { get { return "未命名"; } }
    }
}
