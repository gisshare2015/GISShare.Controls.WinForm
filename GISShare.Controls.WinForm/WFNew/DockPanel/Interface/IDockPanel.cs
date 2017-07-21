using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew.DockPanel
{
    public interface IDockPanel : IBasePanel, IDock
    {
        //Size Size { get; set;}

        //int Height { get; set;}

        //int Width { get; set;}

        Control ParentControl { get; }

        DockPanelStyle eDockPanelStyle { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool GetVisible();

        /// <summary>
        /// 
        /// </summary>
        void ClearBasePanels();

        /// <summary>
        /// 
        /// </summary>
        void RemoveFromParent();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        BasePanel[] GetBasePanels();

        /// <summary>
        /// 获取其上级容器
        /// </summary>
        /// <returns></returns>
        IDockPanelContainer GetDockPanelContainer();

        /// <summary>
        /// 获取其上级容器状态
        /// </summary>
        /// <returns></returns>
        DockPanelContainerStyle GetDockPanelContainerStyle();

        /// <summary>
        /// 获取其终极停靠区
        /// </summary>
        /// <returns></returns>
        IDockArea GetDockArea();

        /// <summary>
        /// 获取其终极停靠区状态
        /// </summary>
        /// <returns></returns>
        DockAreaStyle GetDockAreaStyle();

        /// <summary>
        /// 获取其终极停靠区状态
        /// </summary>
        /// <param name="eDockStyle"></param>
        /// <returns></returns>
        DockAreaStyle GetDockAreaStyle(out DockStyle eDockStyle);

        /// <summary>
        /// 获取停靠许可
        /// </summary>
        /// <param name="bCanDockUp"></param>
        /// <param name="bCanDockLeft"></param>
        /// <param name="bCanDockRight"></param>
        /// <param name="bCanDockBottom"></param>
        /// <param name="bCanDockFill"></param>
        /// <param name="bCanFloat"></param>
        /// <param name="bCanHide"></param>
        /// <param name="bIsBasePanel"></param>
        /// <param name="bIsDocumentPanel"></param>
        void GetDockLicense(ref bool bCanDockUp, ref bool bCanDockLeft, ref bool bCanDockRight, ref bool bCanDockBottom, ref bool bCanDockFill,
            ref bool bCanFloat, ref bool bCanHide, ref bool bCanClose,
            ref bool bIsBasePanel, ref bool bIsDocumentPanel);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pDockPanel"></param>
        /// <param name="eDockStyle"></param>
        /// <returns></returns>
        bool AddDockPanel(IDockPanel pDockPanel, DockStyle eDockStyle);
    }

}
