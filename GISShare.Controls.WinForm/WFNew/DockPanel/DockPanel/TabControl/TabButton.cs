using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace LiuZhenHong.Controls.DockPanel
{
    [ToolboxItem(false)]
    class TabButton : Ribbon.RibbonTabButtonItem
    {
        public event IsSelectedValueChangedEventHandler IsSelectedValueChanged; //属性IsSelected值改变后事件

        public TabButton(TabPage tabPage)
            : base(tabPage) { }

        public override LiuZhenHong.Controls.Ribbon.ImageSizeStyle eImageSizeStyle
        {
            get
            {
                return LiuZhenHong.Controls.Ribbon.ImageSizeStyle.eSystem;
            }
            set
            {
                base.eImageSizeStyle = value;
            }
        }

        protected override void OnTabButtonMouseUp(MouseEventArgs e)
        {
            base.OnTabButtonMouseUp(e);
            //
            this.Selected();
        }

        public override ContentAlignment TextAlign
        {
            get
            {
                return ContentAlignment.MiddleRight;
            }
            set
            {
                base.TextAlign = ContentAlignment.MiddleRight;
            }
        }

        //事件
        protected virtual void OnIsSelectedValueChanged(object sender, IsSelectedValueChangedEventArgs e)
        { if (IsSelectedValueChanged != null) { this.IsSelectedValueChanged(sender, e); } }
    }

    /// <summary>
    /// 委托 IsSelected值改变后
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void IsSelectedValueChangedEventHandler(object sender, IsSelectedValueChangedEventArgs e);

    /// <summary>
    /// IsSelected值改变后事件 参数
    /// </summary>
    public class IsSelectedValueChangedEventArgs : EventArgs
    {
        public IsSelectedValueChangedEventArgs(bool oldValue, bool newValue)
        {
            this._OldValue = oldValue;
            this._NewValue = newValue;
        }

        public IsSelectedValueChangedEventArgs(bool oldValue, bool newValue, object Other)
        {
            this._OldValue = oldValue;
            this._NewValue = newValue;
            this._Other = Other;
        }

        bool _OldValue;
        public bool OldValue
        {
            get { return _OldValue; }
        }

        bool _NewValue;
        public bool NewValue
        {
            get { return _NewValue; }
        }

        object _Other;
        public object Other
        {
            get { return _Other; }
            set { _Other = value; }
        }
    }
}
