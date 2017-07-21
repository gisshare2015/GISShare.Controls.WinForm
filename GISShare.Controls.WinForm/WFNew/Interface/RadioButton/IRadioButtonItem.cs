using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IRadioButtonItem : ILabelItem
    {
        /// <summary>
        /// 鼠标在复选框内弹起方才改变（Checked属性）
        /// </summary>
        bool InsideCheckRectangleTrigger { get; set; }

        Rectangle CheckRectangle { get; }
    }
}
