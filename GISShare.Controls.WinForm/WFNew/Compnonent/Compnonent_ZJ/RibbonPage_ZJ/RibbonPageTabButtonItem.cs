using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    public class RibbonPageTabButtonItem : TabButtonItem
    {
        public RibbonPageTabButtonItem(IRibbonPageItem pRibbonPageItem)
            : base(pRibbonPageItem)
        {
            base.Name = "GISShare.Controls.WinForm.WFNew.RibbonPageTabButtonItem";
            base.Text = "功能区页面表按钮";
            
        }

        public override int OffsetValue
        {
            get
            {
                return 0;
            }
        }

        public override Padding Padding
        {
            get
            {
                return new Padding(3, 4, this.ShowCloseButton ? 4 : 0, 2);
            }
            set
            {
                base.Padding = value;
            }
        }

        protected override void OnTabButtonMouseDown(MouseEventArgs e)
        {
            base.OnTabButtonMouseDown(e);
            //
            IRibbonControl pRibbonControl = this.TryGetDependRibbonControl();
            if (pRibbonControl == null || !pRibbonControl.HideRibbonPage) return;
            pRibbonControl.ShowPagesPopup();
        }

        protected override void OnTabButtonMouseUp(MouseEventArgs e)
        {
            base.OnTabButtonMouseUp(e);
            //
            this.Selected();
        }

        protected override void OnCloseButtonMouseUp(MouseEventArgs e)
        {
            base.OnCloseButtonMouseUp(e);
            //
            this.RemoveSelf();
        }
    }
}
