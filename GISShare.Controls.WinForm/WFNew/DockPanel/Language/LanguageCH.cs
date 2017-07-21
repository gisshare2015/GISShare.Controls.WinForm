using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew.DockPanel
{
    public class LanguageCH : Language
    {
        public override string CustomizeFormTitle { get { return "自定义"; } }
        public override string CustomizeForm_ButtonCancelText { get { return "关  闭"; } }
        public override string CustomizeForm_TabPageBasePanelText { get { return "基础面板"; } }
        public override string CustomizeForm_TabPageDockPanelText { get { return "停靠面板"; } }
        public override string CustomizeForm_TabPagePanelTreeText { get { return "关系树"; } }

        public override string CustomizeText { get { return "自定义"; } }
        public override string MainFormText { get { return "主窗体"; } }
        public override string DocumentAreaText { get { return "文档区"; } }
        public override string DockAreaText { get { return "停靠区"; } }
        public override string FloatFormText { get { return "浮动窗体"; } }
        public override string RootNodeText { get { return "根节点"; } }
        public override string BinaryNodeText { get { return "双子节点"; } }
        public override string MultipleNodeText { get { return "多子节点"; } }
        public override string BottomNodeText { get { return "终节点"; } }
        public override string SquareBrackets_Left { get { return "【"; } }
        public override string SquareBrackets_Right { get { return "】"; } }
        public override string RoundBrackets_Left { get { return "（"; } }
        public override string RoundBrackets_Right { get { return "）"; } }
        public override string DoubleQuotationMarks_Left { get { return "“"; } }
        public override string DoubleQuotationMarks_Right { get { return "”"; } }

        public override string LayoutManagerText { get { return "布局管理"; } }
        public override string InternalText { get { return "内部"; } }
        public override string OuterText { get { return "外围"; } }
        public override string LayoutToText { get { return "布局到"; } }
        public override string LayoutToTopText { get { return "顶部停靠"; } }
        public override string LayoutToBottomText { get { return "底部停靠"; } }
        public override string LayoutToLeftText { get { return "左边停靠"; } }
        public override string LayoutToRightText { get { return "右边停靠"; } }
        public override string LayoutToFillText { get { return "填充面板"; } }
        public override string FloatDockPanelText { get { return "浮动窗体"; } }
        public override string HideDockPanelText { get { return "隐藏面板"; } }
        public override string ShowDockPanelText { get { return "展现面板"; } }
    }
}
