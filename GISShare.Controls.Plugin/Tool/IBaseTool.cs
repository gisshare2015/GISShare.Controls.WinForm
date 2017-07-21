using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin
{
    public interface IBaseTool
    {
        /// <summary>
        /// 鼠标指针 状态
        /// </summary>
        int Cursor { get; }

        /// <summary>
        /// Tool按钮的激活状态设置
        /// </summary>
        /// <returns></returns>
        bool Deactivate();

        /// <summary>
        /// 鼠标双击时触发的事件
        /// </summary>
        void OnDblClick();

        /// <summary>
        /// 鼠标点击右键弹出快捷菜单时触发的事件
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        bool OnContextMenu(int x, int y);

        /// <summary>
        /// 鼠标在地图上移动是触发的事件
        /// </summary>
        /// <param name="button"></param>
        /// <param name="shift"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        void OnMouseMove(int button, int shift, int x, int y);

        /// <summary>
        /// 鼠标按下时触发的事件
        /// </summary>
        /// <param name="button"></param>
        /// <param name="shift"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        void OnMouseDown(int button, int shift, int x, int y);

        /// <summary>
        /// 鼠标弹起时触发的事件
        /// </summary>
        /// <param name="button"></param>
        /// <param name="shift"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        void OnMouseUp(int button, int shift, int x, int y);

        /// <summary>
        /// 刷新时触发的事件
        /// </summary>
        /// <param name="hDC"></param>
        void Refresh(int hDC);

        /// <summary>
        /// 键盘按下时触发的事件
        /// </summary>
        /// <param name="keyCode"></param>
        /// <param name="shift"></param>
        void OnKeyDown(int keyCode, int shift);

        /// <summary>
        /// 键盘叹气时触发的事件
        /// </summary>
        /// <param name="keyCode"></param>
        /// <param name="shift"></param>
        void OnKeyUp(int keyCode, int shift);
    }
}
