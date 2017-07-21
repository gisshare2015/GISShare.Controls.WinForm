using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IRibbonControl : IBaseItem2, ITabControl
    {
        int LeftTopRadius { get;set;  }

        int RightTopRadius { get; set; }

        int LeftBottomRadius { get;set;  }

        int RightBottomRadius { get;set;  }

        bool HideRibbonPage { get;set;  }

        bool IsTopToolbar { get;set;  }

        RibbonStyle eRibbonStyle { get;set;  }

        int RibbonPageSelectedIndex { get; set; }

        Form ParentForm { get; set; }

        IRibbonForm RibbonForm { get; }

        bool IsActive { get; }

        Point AnchorPoint { get; }

        Icon Icon { get; }

        int CaptionHeight { get; }

        MenuStrip MenuStrip { get; }

        bool IsApplicationPopupOpened { get; }

        void ShowApplicationPopup();

        void CloseApplicationPopup();

        int PagesPopupSpace { get;set;  }

        Point PagesPopupLoction { get; }

        bool IsPagesOpened { get; }

        void ShowPagesPopup();

        void ClosePagesPopup();

        Rectangle UsingRectangle { get; }
        Rectangle TopCompnonentRectangle { get; }
        Rectangle MiddleCompnonentRectangle { get; }
        Rectangle PageDisplayRectangle { get; }
        Rectangle BottomCompnonentRectangle { get; }
        Rectangle CaptionRectangle { get; }
        Rectangle CaptionTextRectangle { get; }
        Rectangle IconRectangle { get; }

        IApplicationPopup ApplicationPopup { get; }

        BaseItemCollection ToolbarItems { get; }

        BaseItemCollection PageContents { get; }

        //RibbonPage SelectedRibbonPage { get; }

        //bool SetSelectRibbonPage(RibbonPage ribbonPage);

        //RibbonControlEx.RibbonPageCollection RibbonPages { get; }
    }
}
