using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IRadioButtonItem : ILabelItem
    {
        /// <summary>
        /// ����ڸ�ѡ���ڵ��𷽲Ÿı䣨Checked���ԣ�
        /// </summary>
        bool InsideCheckRectangleTrigger { get; set; }

        Rectangle CheckRectangle { get; }
    }
}
