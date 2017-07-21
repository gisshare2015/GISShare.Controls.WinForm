using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public enum BaseItemState
    {
        eHot = 0,
        ePressed = 1,
        eNormal = 2,
        eDisabled = 3
    }

    public enum RenderStyle
    {
        eSystem = 8,//系统自动规划
        eCustomize = 9//使用自定义
    }

    public enum DisplayStyle
    {
        eImage = 4,
        eText = 5,
        eImageAndText = 6,
        eNone = 7
    }

    public enum ImageSizeStyle
    {
        eSystem = 8,//系统自动规划
        eCustomize = 9,//使用自定义
        eStretch = 10,//拉伸至IT矩形区
        eDefault = 88//使用默认
    }

    public enum PNLayoutStyle
    {
        eBothEnds = 11,
        eHead = 12,
        eTail = 13
    }

    public enum ButtonStyle
    {
        eLabel = 14,
        eButton = 15,
        eCheckButton = 16,
        eDropDownButton = 17,
        eSplitButton = 18
    }

    public enum QuickAccessToolbarStyle
    {
        eAllRound = 19,
        eHalfRound = 20,
        eNormal = 2,
        eLineSeparator = 21,
        eNone = 7
    }

    public enum ContextPopupStyle
    {
        eSimply = 22,
        eNormal = 2,
        eSuper = 23
    }

    public enum RibbonStyle
    {
        eOffice2007 = 24,
        eOffice2010 = 25
    }

    public enum FormButtonStyle
    {
        eMinButton = 41,
        eMaxButton = 42,
        eHelpButton = 43,
        eCloseButton = 44,
        //
        eMdiMinButton = 45,
        eMdiMaxButton = 46,
        eMdiCloseButton = 47
    }

    public enum GalleryScrollButtonStyle
    {
        eScrollUpButton = 48,
        eScrollDownButton = 49,
        eScrollDropDownButton = 50
    }

    public enum LTBRButtonStyle
    {
        eLeftButton = 51,
        eTopButton = 52,
        eBottomButton = 53,
        eRightButton = 54
    }

    public enum ModifySizeStyle
    {
        eHorizontal = 55,
        eVertical = 56,
        eAll = 57,
        eNone = 7
    }

    public enum CustomizeComboBoxStyle
    {
        eDropDown = 58,
        eDropDownList = 59
    }

    public enum TabButtonContainerButtonStyle
    {
        eCloseButton = 44,
        eContextButton = 63
    }

    public enum TabButtonContainerStyle
    {
        eContextButton = 64,
        eCloseButton = 65,
        eContextButtonAndCloseButton = 66,
        ePreButtonAndNextButton = 67
    }

    public enum SliderButtonStyle
    {
        eMinusButton = 68,
        ePlusButton = 69,
        eSliderButton = 70
    }

    public enum ScrollBarButtonStyle
    {
        eMinusButton = 68,
        ePlusButton = 69,
        eScrollButton = 71
    }

    public enum ViewParameterStyle
    {
        eSelected = 72,
        eFocused = 73,
        eNone = 7
    }

    public enum EventStateStyle
    {
        eUsed = 74,
        eUnused = 75,
        eNotExist = 76,
        eUnknown = 77
    }

    public enum BorderStyle
    {
        eNone = 7,
        eSingle = 78
    }

    /// <summary>
    /// 水平布局方式（重要）
    /// </summary>
    public enum HAlignmentStyle
    {
        eCenter = 79,
        eStretch = 80,
        eLeft = 81,
        eRight = 82
    }

    /// <summary>
    /// 竖直布局方式（重要）
    /// </summary>
    public enum VAlignmentStyle
    {
        eCenter = 79,
        eStretch = 80,
        eTop = 83,
        eBottom = 84
    }

    /// <summary>
    /// 布局的方式（重要）
    /// </summary>
    public enum LayoutStyle
    {
        eLayout = 85,//强制布局：对所有的子项进行布局和设置，并返回合理的集合UI尺寸
        eLayoutPlan = 86,//布局规划：只对子项布局进行规划而不对话子项进行设置，并返回合理的集合UI尺寸
        eLayoutAuto = 87//布局自动：只布局和设置集合对象拥有者和子项对象拥有者相同的子项，并返回合理的集合UI尺寸
    }

    public enum UpDownButtonStyle
    {
        eUpButton = 89,
        eDownButton = 90
    }

    public enum ArrowDock
    {
        eUp,
        eLeft,
        eRight,
        eDown,
        eNone
    }

    public enum ArrowStyle
    {
        eToUp,
        eToLeft,
        eToRight,
        eToDown
    }

    public enum CollapseSplitPanelStyles
    {
        SplitPanel,
        CollapsablePanel,
        CollapsableSplitPanel
    }

    public enum ExpandButtonStyle
    {
        eTopToBottom,
        eBottomToTop,
        eLeftToRight,
        eRightToLeft
    }

    public enum ToolBarStyle
    {
        eMenuBar,
        eToolBar,
        eStatusBar
    }
        
    public enum GlyphStyle
    {
        eArrowUp,
        eArrowDown,
        eArrowLeft,
        eArrowRight,
        eDirectionUp,
        eDirectionDown,
        eDirectionLeft,
        eDirectionRight,
        ePlus,
        eMinus,
        ePointsHorizontal,
        ePointsVertical
    }

    /// <summary>
    /// 子项视图依附于父类的模式（重要）
    /// </summary>
    public enum ViewDependStyle
    {
        eInOwnerItemsRectangle,//说明该对象由的显示范围受到拥有者（Owner）ItemsRectangle属性的影响
        eInOwnerDisplayRectangle,//说明该对象由的显示范围受到拥有者（Owner）DisplayRectangle属性的影响
        eUnrestraint//标明该对象显示不受约束
    }

    /// <summary>
    /// 解散Popup的状态（重要）
    /// </summary>
    public enum DismissPopupStyle
    {
        eIsDependBasePopup,//只要其根目录在BasePopup上则解散Popup
        eIsBasePopupItem,//是BasePopup的直接子项成员则解散Popup
        eNoDismiss//不解散
    }

    /// <summary>
    /// 指示这个IBaseItem的性质，指示该对象是否为组件项（组件项无法抢夺焦点，也无法截获到键盘和滚轮事件，但是消息依然有效。）（重要）
    /// </summary>
    public enum BaseItemStyle
    {
        eIndependentBaseItem,//一个孤立的BaseItem
        eIndependentBaseItemControl,//一个孤立的BaseItemControl
        eIndependentBasePopup,//一个孤立的BasePopup
        eComponentBaseItem//一个组件BaseItem
    }

    ///// <summary>
    ///// 子项布局管理模式（重要）
    ///// </summary>
    //public enum LayoutManagerStyle
    //{
    //    eManagerItemLocation,//由父类管理子项的Location属性
    //    eManagerItemSize,//由父类管理子项的Size属性
    //    eManagerItemLocationAndSize,//由父类管理子项的Location和Size属性
    //    eNoManagerItem//不管理子项
    //}
}
