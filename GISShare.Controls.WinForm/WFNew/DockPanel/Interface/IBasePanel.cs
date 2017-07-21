using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace GISShare.Controls.WinForm.WFNew.DockPanel
{
    public interface IBasePanel : WFNew.IBaseItem, WFNew.IRecordItem
    {
        event EventHandler Opened;
        event EventHandler Closed;
        event BoolValueChangedEventHandler BeforeVisibleExValueSeted;
        event BoolValueChangedEventHandler AfterVisibleExValueSeted;

        //int RecordID { get; }

        //string Name { get; }

        //string Describe { get; }

        bool VisibleEx { get; set; }

        bool CanDockUp { get; }

        bool CanDockLeft { get;}

        bool CanDockRight { get;}

        bool CanDockBottom { get;}

        bool CanDockFill { get;}

        bool CanFloat { get;}

        bool CanHide { get;}

        bool CanClose { get;}

        bool IsBasePanel { get;}

        bool IsDocumentPanel { get;}

        BasePanelStyle eBasePanelStyle { get; }

        DockPanelManager DockPanelManager { get; }

        Point DockPanelFloatFormLocation { get; set; }

        Size DockPanelFloatFormSize { get; set; }

        /// <summary>
        /// 使其失去焦点
        /// </summary>
        void LostFocusEx();

        /// <summary>
        /// 展现面板
        /// </summary>
        void Open();

        /// <summary>
        /// 关闭面板
        /// </summary>
        void Close();

        /// <summary>
        /// 转化为浮动面板
        /// </summary>
        /// <returns></returns>
        bool ToDockPanelFloatForm();

        /// <summary>
        /// 转化为浮动面板
        /// </summary>
        /// <param name="moustPoint"></param>
        /// <returns></returns>
        bool ToDockPanelFloatForm(Point moustPoint);
    }

}
