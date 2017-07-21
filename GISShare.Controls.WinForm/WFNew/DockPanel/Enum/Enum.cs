using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew.DockPanel
{
    public enum PanelEnumStyle
    {
        eBasePanel = 0,
        eDockPanel = 1,
        eHoldDockPanel = 2,
        eDockPanelContainer = 3,
        eDockPanelDockArea = 4,
        eDocumentDockArea = 5,
        eDockPanelFloatForm = 6,
        eNone = 7
    }

    public enum BasePanelStyle
    {
        eBasePanel = PanelEnumStyle.eBasePanel,
        eDockPanel = PanelEnumStyle.eDockPanel,
        eHoldDockPanel = PanelEnumStyle.eHoldDockPanel,
        eDockPanelContainer = PanelEnumStyle.eDockPanelContainer
    }

    public enum DockPanelStyle
    {
        eDockPanel = 1,
        eHoldDockPanel = 2,
        eDockPanelContainer = 3
    }

    public enum DockPanelContainerStyle
    {
        eDockPanelContainer = PanelEnumStyle.eDockPanelContainer,
        eDockPanelDockArea = PanelEnumStyle.eDockPanelDockArea,
        eDocumentDockArea = PanelEnumStyle.eDocumentDockArea,
        eDockPanelFloatForm = PanelEnumStyle.eDockPanelFloatForm,
        eNone = PanelEnumStyle.eNone
    }

    public enum DockAreaStyle
    {
        eDockPanelDockArea = PanelEnumStyle.eDockPanelDockArea,
        eDocumentDockArea = PanelEnumStyle.eDocumentDockArea,
        eDockPanelFloatForm = PanelEnumStyle.eDockPanelFloatForm,
        eNone = PanelEnumStyle.eNone
    }

    public enum NodeEnumStyle
    {
        eBaseNode = -1,                                                                                                         //基础节点接口，无实例对象
        eBottomNode = PanelEnumStyle.eBasePanel,                                                                                //BasePanel
        eMultipleNode = PanelEnumStyle.eDockPanel,                                                                              //DockPanel
        eBinaryNode = PanelEnumStyle.eDockPanelContainer,                                                                       //DockPanelContainer
        eRootNode = PanelEnumStyle.eDockPanelFloatForm | PanelEnumStyle.eDocumentDockArea | PanelEnumStyle.eDockPanelDockArea  //IDockArea
    }

    public enum NodeStyle
    {
        eBaseNode = NodeEnumStyle.eBaseNode,         //基础节点接口，无实例对象
        eBottomNode = NodeEnumStyle.eBaseNode,       //BasePanel
        eMultipleNode = NodeEnumStyle.eMultipleNode, //DockPanel
        eBinaryNode = NodeEnumStyle.eBinaryNode,     //DockPanelContainer
        eRootNode = NodeEnumStyle.eRootNode          //IDockArea
    }

    public enum PanelStateEnumStyle
    {
        eShow = 0,
        eClose = 1,
        eHide = 2,
        eRemove = 3,
        eNone =4
    }

    public enum PanelNodeState
    {
        /// <summary>
        /// BasePanel【BasePanel属性VisibleEx和IsSelected皆为true】，DockPanel【DockPanel的VisibleEx属性为true】，DockPanelContainer【DockPanelContainer的VisibleEx属性为true】
        /// </summary>
        eShow = PanelStateEnumStyle.eShow,    //BasePanel【BasePanel属性VisibleEx和IsSelected皆为true】，           DockPanel【DockPanel的VisibleEx属性为true】，  DockPanelContainer【DockPanelContainer的VisibleEx属性为true】
        /// <summary>
        /// BasePanel【BasePanel的容器DockPanel的VisibleEx属性为false】，DockPanel【DockPanel的VisibleEx属性为false】，DockPanelContainer【DockPanelContainer的VisibleEx属性为false】
        /// </summary>
        eClose = PanelStateEnumStyle.eClose,  //BasePanel【BasePanel的容器DockPanel的VisibleEx属性为false】，       DockPanel【DockPanel的VisibleEx属性为false】， DockPanelContainer【DockPanelContainer的VisibleEx属性为false】
        /// <summary>
        /// BasePanel【BasePanel属性VisibleEx为true，但属性IsSelected为false】，DockPanel【不存在该枚举值】， DockPanelContainer【不存在该枚举值】
        /// </summary>
        eHide = PanelStateEnumStyle.eHide,    //BasePanel【BasePanel属性VisibleEx为true，但属性IsSelected为false】，DockPanel【不存在该枚举值】，                  DockPanelContainer【不存在该枚举值】
        /// <summary>
        /// BasePanel【BasePanel不在DockPanel容器中】，DockPanel【不存在该枚举值】，DockPanelContainer【不存在该枚举值】
        /// </summary>
        eRemove = PanelStateEnumStyle.eRemove //BasePanel【BasePanel不在DockPanel容器中】，                         DockPanel【不存在该枚举值】，                  DockPanelContainer【不存在该枚举值】
    }

    public enum PanelNodeStyle
    {
        eBasePanel = PanelEnumStyle.eBasePanel,
        eDockPanel = PanelEnumStyle.eDockPanel
    }

    public enum DockButtonStyle
    {
        eCenterToDockFill,
        eCenterToDocumentUp,
        eCenterToDocumentLeft,
        eCenterToDocumentRight,
        eCenterToDocumentBottom,
        eCenterToDockUp,
        eCenterToDockLeft,
        eCenterToDockRight,
        eCenterToDockBottom,
        eToDockUp,
        eToDockLeft,
        eToDockRight,
        eToDockBottom
    }

    public enum DockPanelButtonItemStyle
    {
        eCloseButton = 44,
        eContextButton,
        eHideButton
    }

    public enum DockPanelFloatFormButtonItemStyle
    {
        eMaxButton = 42,
        eCloseButton = 44
    }
}
