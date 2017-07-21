using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew.DockPanel
{
    public abstract class Language
    {
        public static Language LanguageStrategy = new LanguageCH();

        public abstract string CustomizeFormTitle { get; }
        public abstract string CustomizeForm_ButtonCancelText { get; }
        public abstract string CustomizeForm_TabPageBasePanelText { get; }
        public abstract string CustomizeForm_TabPageDockPanelText { get; }
        public abstract string CustomizeForm_TabPagePanelTreeText { get; }

        public abstract string CustomizeText { get; }
        public abstract string MainFormText { get; }
        public abstract string DocumentAreaText { get; }
        public abstract string DockAreaText { get; }
        public abstract string FloatFormText { get; }
        public abstract string RootNodeText { get; }
        public abstract string BinaryNodeText { get; }
        public abstract string MultipleNodeText { get; }
        public abstract string BottomNodeText { get; }
        public abstract string SquareBrackets_Left { get; }
        public abstract string SquareBrackets_Right { get; }
        public abstract string RoundBrackets_Left { get; }
        public abstract string RoundBrackets_Right { get; }
        public abstract string DoubleQuotationMarks_Left { get; }
        public abstract string DoubleQuotationMarks_Right { get; }

        public abstract string LayoutManagerText { get; }
        public abstract string InternalText { get; }
        public abstract string OuterText { get; }
        public abstract string LayoutToText { get; }
        public abstract string LayoutToTopText { get; }
        public abstract string LayoutToBottomText { get; }
        public abstract string LayoutToLeftText { get; }
        public abstract string LayoutToRightText { get; }
        public abstract string LayoutToFillText { get; }
        public abstract string FloatDockPanelText { get; }
        public abstract string HideDockPanelText { get; }
        public abstract string ShowDockPanelText { get; }

    }
}
