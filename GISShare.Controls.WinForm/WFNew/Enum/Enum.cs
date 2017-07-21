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
        eSystem = 8,//ϵͳ�Զ��滮
        eCustomize = 9//ʹ���Զ���
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
        eSystem = 8,//ϵͳ�Զ��滮
        eCustomize = 9,//ʹ���Զ���
        eStretch = 10,//������IT������
        eDefault = 88//ʹ��Ĭ��
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
    /// ˮƽ���ַ�ʽ����Ҫ��
    /// </summary>
    public enum HAlignmentStyle
    {
        eCenter = 79,
        eStretch = 80,
        eLeft = 81,
        eRight = 82
    }

    /// <summary>
    /// ��ֱ���ַ�ʽ����Ҫ��
    /// </summary>
    public enum VAlignmentStyle
    {
        eCenter = 79,
        eStretch = 80,
        eTop = 83,
        eBottom = 84
    }

    /// <summary>
    /// ���ֵķ�ʽ����Ҫ��
    /// </summary>
    public enum LayoutStyle
    {
        eLayout = 85,//ǿ�Ʋ��֣������е�������в��ֺ����ã������غ���ļ���UI�ߴ�
        eLayoutPlan = 86,//���ֹ滮��ֻ������ֽ��й滮�����Ի�����������ã������غ���ļ���UI�ߴ�
        eLayoutAuto = 87//�����Զ���ֻ���ֺ����ü��϶���ӵ���ߺ��������ӵ������ͬ����������غ���ļ���UI�ߴ�
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
    /// ������ͼ�����ڸ����ģʽ����Ҫ��
    /// </summary>
    public enum ViewDependStyle
    {
        eInOwnerItemsRectangle,//˵���ö����ɵ���ʾ��Χ�ܵ�ӵ���ߣ�Owner��ItemsRectangle���Ե�Ӱ��
        eInOwnerDisplayRectangle,//˵���ö����ɵ���ʾ��Χ�ܵ�ӵ���ߣ�Owner��DisplayRectangle���Ե�Ӱ��
        eUnrestraint//�����ö�����ʾ����Լ��
    }

    /// <summary>
    /// ��ɢPopup��״̬����Ҫ��
    /// </summary>
    public enum DismissPopupStyle
    {
        eIsDependBasePopup,//ֻҪ���Ŀ¼��BasePopup�����ɢPopup
        eIsBasePopupItem,//��BasePopup��ֱ�������Ա���ɢPopup
        eNoDismiss//����ɢ
    }

    /// <summary>
    /// ָʾ���IBaseItem�����ʣ�ָʾ�ö����Ƿ�Ϊ����������޷����ό�㣬Ҳ�޷��ػ񵽼��̺͹����¼���������Ϣ��Ȼ��Ч��������Ҫ��
    /// </summary>
    public enum BaseItemStyle
    {
        eIndependentBaseItem,//һ��������BaseItem
        eIndependentBaseItemControl,//һ��������BaseItemControl
        eIndependentBasePopup,//һ��������BasePopup
        eComponentBaseItem//һ�����BaseItem
    }

    ///// <summary>
    ///// ����ֹ���ģʽ����Ҫ��
    ///// </summary>
    //public enum LayoutManagerStyle
    //{
    //    eManagerItemLocation,//�ɸ�����������Location����
    //    eManagerItemSize,//�ɸ�����������Size����
    //    eManagerItemLocationAndSize,//�ɸ�����������Location��Size����
    //    eNoManagerItem//����������
    //}
}
