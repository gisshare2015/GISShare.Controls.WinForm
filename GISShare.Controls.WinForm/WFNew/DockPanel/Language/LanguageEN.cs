using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew.DockPanel
{
    public class LanguageEN : Language
    {
        public override string CustomizeFormTitle { get { return "Customize"; } }
        public override string CustomizeForm_ButtonCancelText { get { return "Close"; } }
        public override string CustomizeForm_TabPageBasePanelText { get { return "BasePanel"; } }
        public override string CustomizeForm_TabPageDockPanelText { get { return "DockPanel"; } }
        public override string CustomizeForm_TabPagePanelTreeText { get { return "PanelTree"; } }

        public override string CustomizeText { get { return "Customize"; } }
        public override string MainFormText { get { return "MainForm"; } }
        public override string DocumentAreaText { get { return "DocumentArea"; } }
        public override string DockAreaText { get { return "DockArea"; } }
        public override string FloatFormText { get { return "FloatForm"; } }
        public override string RootNodeText { get { return "RootNode"; } }
        public override string BinaryNodeText { get { return "BinaryNode"; } }
        public override string MultipleNodeText { get { return "MultipleNode"; } }
        public override string BottomNodeText { get { return "BottomNode"; } }
        public override string SquareBrackets_Left { get { return "["; } }
        public override string SquareBrackets_Right { get { return "]"; } }
        public override string RoundBrackets_Left { get { return "("; } }
        public override string RoundBrackets_Right { get { return ")"; } }
        public override string DoubleQuotationMarks_Left { get { return "\""; } }
        public override string DoubleQuotationMarks_Right { get { return "\""; } }

        public override string LayoutManagerText { get { return "LayoutManager"; } }
        public override string InternalText { get { return "Internal"; } }
        public override string OuterText { get { return "Outer"; } }
        public override string LayoutToText { get { return "Layout To "; } }
        public override string LayoutToTopText { get { return "Layout To Top"; } }
        public override string LayoutToBottomText { get { return "Layout To Bottom"; } }
        public override string LayoutToLeftText { get { return "Layout To Left"; } }
        public override string LayoutToRightText { get { return "Layout To Right"; } }
        public override string LayoutToFillText { get { return "Layout To Fill"; } }
        public override string FloatDockPanelText { get { return "Float DockPanel"; } }
        public override string HideDockPanelText { get { return "Hide DockPanel"; } }
        public override string ShowDockPanelText { get { return "Show DockPanel"; } }
    }
}
