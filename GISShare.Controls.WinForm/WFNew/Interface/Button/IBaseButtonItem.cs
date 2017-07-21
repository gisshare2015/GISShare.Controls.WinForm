using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IBaseButtonItem : IImageLabelItem
    {
        bool ShowNomalState { get; set; }

        int LeftTopRadius { get;set;  }

        int RightTopRadius { get; set; }

        int LeftBottomRadius { get;set;  }

        int RightBottomRadius { get;set;  }

        bool NomalChecked { get;  }//是否正常绘制选中状态（当选中时有效）

        Rectangle CheckRectangle { get; }

        Rectangle ButtonRectangle { get; }
    }
}
