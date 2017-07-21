using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace GISShare.Controls.WinForm
{
    [ToolboxItem(false)]
    public class VsButton : Button
    {
        bool m_ShowSplit = true;

        public VsButton(bool bShowSplit)
        {
            this.m_ShowSplit = bShowSplit;
        }

        public override Rectangle DisplayRectangle
        {
            get
            {
                if (this.m_ShowSplit)
                    return Rectangle.FromLTRB(base.DisplayRectangle.Left, base.DisplayRectangle.Top, base.DisplayRectangle.Right - 16, base.DisplayRectangle.Bottom);
                return base.DisplayRectangle;
            }
        }

        public Rectangle SplitRectangle
        {
            get
            {
                return Rectangle.FromLTRB(base.DisplayRectangle.Right - 16, base.DisplayRectangle.Top, base.DisplayRectangle.Right, base.DisplayRectangle.Bottom);
            }
        }

        public bool ContainsSplitRectangle(Point point)
        {
            return this.m_ShowSplit && this.SplitRectangle.Contains(point);
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            //
            Rectangle rectangle = this.DisplayRectangle;
            using (Pen p = new Pen(Color.White))
            {
                p.Width = 2;
                pevent.Graphics.DrawLine(p, rectangle.Right, rectangle.Top + 3, rectangle.Right, rectangle.Bottom - 3);
            }
            using (Pen p = new Pen(Color.Black))
            {
                int iC = (rectangle.Top + rectangle.Bottom) / 2;
                pevent.Graphics.DrawLine(p, rectangle.Right + 5, iC - 1, rectangle.Right + 9, iC - 1);
                pevent.Graphics.DrawLine(p, rectangle.Right + 6, iC, rectangle.Right + 8, iC);
                pevent.Graphics.DrawLine(p, rectangle.Right + 7, iC - 1, rectangle.Right + 7, iC + 1);
            }
        }
    }
}
