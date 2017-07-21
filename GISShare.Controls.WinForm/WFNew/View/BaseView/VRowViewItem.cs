using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew.View
{
    public class VRowViewItem : RowViewItem
    {
        public VRowViewItem() { }

        public VRowViewItem(string text)
            : base(text) { }

        public VRowViewItem(string name, string text)
            : base(name, text) { }

        public VRowViewItem(string name, string text, Font font)
            : base(name, text, font) { }

        public override Size MeasureSize(Graphics g)
        {
            if (this.ViewItems.Count > 0)
            {
                int iH = 0;
                int iW = 0;
                Size size;
                foreach (SizeViewItem one in this.ViewItems)
                {
                    size = one.MeasureSize(g);
                    iH += size.Height;
                    if (iW < size.Width) iW = size.Width;
                }
                return new Size(iW > this.Width ? iW : this.Width, iH);
            }
            //
            return base.MeasureSize(g);
        }

        protected override void DrawItem(System.Windows.Forms.PaintEventArgs e)
        {
            //Rectangle rectangle = this.DisplayRectangle;
            IViewItemOwner pViewItemOwner = this.pOwner as IViewItemOwner;
            Rectangle viewItemsRectangle = e.ClipRectangle;// pViewItemOwner == null ? rectangle : pViewItemOwner.ViewItemsRectangle;
            GISShare.Controls.WinForm.WFNew.LayoutEngine.LayoutStackV_Row(e.Graphics, this, viewItemsRectangle, this.DisplayRectangle, ref this._TopViewItemIndex, ref this._BottomViewItemIndex);
            ViewItem viewItem = null;
            IMessageChain pMessageChain;
            for (int i = this.TopViewItemIndex; i <= this.BottomViewItemIndex; i++)
            {
                viewItem = this.ViewItems[i];
                if (viewItem == null) continue;
                Rectangle clipRectangle = Rectangle.Intersect(viewItemsRectangle, viewItem.DisplayRectangle);
                if (clipRectangle.Width > 0 && clipRectangle.Height > 0)
                {
                    pMessageChain = viewItem as IMessageChain;
                    if (pMessageChain != null)
                    {
                        e.Graphics.SetClip(clipRectangle);
                        MessageInfo messageInfo = new MessageInfo(this, MessageStyle.eMSPaint, new PaintEventArgs(e.Graphics, clipRectangle));
                        pMessageChain.SendMessage(messageInfo);
                    }
                }
            }
            #region 已抛弃（布局移植到LayoutEngine）
            //Rectangle rectangle = this.DisplayRectangle;
            //int iH = 0;
            //int iOffsetY = 0;
            ////
            //IViewItemOwner pViewItemOwner = this.pOwner as IViewItemOwner;
            //Rectangle viewItemsRectangle = pViewItemOwner == null ? rectangle : pViewItemOwner.ViewItemsRectangle;
            ////
            //ViewItem viewItem = null;
            //int iCount = this.ViewItems.Count;
            //int iTopY = 0;
            //int iBottomY = 0;
            //int iLeftIndex = 0;
            //int iRightIndex = iCount;
            //for (int i = 0; i < iCount; i++)
            //{
            //    viewItem = this.ViewItems[i];
            //    //
            //    iH = viewItem.MeasureSize(e.Graphics).Height;
            //    iTopY = rectangle.Top + iOffsetY;
            //    iBottomY = iTopY + iH;
            //    if (iBottomY <= viewItemsRectangle.Top)
            //    {
            //        iLeftIndex++;
            //    }
            //    else if (iTopY >= viewItemsRectangle.Bottom)
            //    {
            //        iRightIndex--;
            //    }
            //    else
            //    {
            //        ISetViewItemHelper pSetViewItemHelper = viewItem as ISetViewItemHelper;
            //        if (pSetViewItemHelper != null)
            //        {
            //            pSetViewItemHelper.SetViewItemDisplayRectangle(new Rectangle(rectangle.Left, iTopY, rectangle.Width, iH));
            //            //
            //            Rectangle clipRectangle = Rectangle.Intersect(viewItemsRectangle, viewItem.DisplayRectangle);
            //            if (clipRectangle.Width > 0 && clipRectangle.Height > 0)
            //            {
            //                IMessageChain pMessageChain = viewItem as IMessageChain;
            //                if (pMessageChain != null)
            //                {
            //                    e.Graphics.SetClip(clipRectangle);
            //                    MessageInfo messageInfo = new MessageInfo(this, MessageStyle.eMSPaint, new PaintEventArgs(e.Graphics, clipRectangle));
            //                    pMessageChain.SendMessage(messageInfo);
            //                }
            //            }
            //        }
            //    }
            //    //
            //    iOffsetY += iH;
            //}
            ////
            //this._TopViewItemIndex = iLeftIndex;
            //this._BottomViewItemIndex = iRightIndex - 1;
            #endregion
        }
    }
}
