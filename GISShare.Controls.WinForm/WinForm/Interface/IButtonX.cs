using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm
{
    public interface IButtonX : WFNew.IBaseItem
    {
        WFNew.ImageSizeStyle eImageSizeStyle { get;set; }

        Size ImageSize { get;set; }

        bool ShowNomalState { get; set; }

        bool Checked { get; set; }

        bool AutoPlanTextRectangle { get; set; }

        int ITSpace { get; set; }

        int LeftTopRadius { get;set;  }

        int RightTopRadius { get; set; }

        int LeftBottomRadius { get; set;  }

        int RightBottomRadius { get; set;  }

        Image Image { get; }

        ContentAlignment ImageAlign { get; }

        ContentAlignment TextAlign { get;  }

        Rectangle TextRectangle { get;}

        Rectangle ImageRectangle { get;}

        //Rectangle TextRectangle { get;}

        Rectangle ITDrawRectangle { get; }//IT = Image Text

        Rectangle ButtonRectangle { get; }
    }
}
