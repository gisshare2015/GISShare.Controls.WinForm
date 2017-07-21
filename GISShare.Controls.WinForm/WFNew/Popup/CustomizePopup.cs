using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    public class CustomizePopup : BasePopup, ICustomizePopup
    {
        private const int MODIFYSIZEREGIONSIZE = 13;
        private const int MODIFYSIZEREGIONSIZEEXPAND = 6;

        private ToolStripControlHost m_ToolStripControlHost = null;

        public CustomizePopup(Control ctr)
        {
            this.m_ToolStripControlHost = new ToolStripControlHost(ctr);
            this.m_ToolStripControlHost.Dock = DockStyle.Fill;
            this.m_ToolStripControlHost.BackColor = base.BackColor;
            this.m_ToolStripControlHost.Margin = new Padding(0);
            this.m_ToolStripControlHost.Padding = new Padding(0);
            base.Items.Add(this.m_ToolStripControlHost);
            //
            this.Margin = new Padding(0);
            this.Padding = new Padding(1);
            this.DropShadowEnabled = false;
            this.ShowItemToolTips = false;
        }

        #region Radius
        public override bool UseRadius
        {
            get
            {
                return false;
            }
        }

        public override int LeftTopRadius 
        { get { return -1; } }

        public override int RightTopRadius
        { get { return -1; } }

        public override int LeftBottomRadius 
        { get { return -1; } }

        public override int RightBottomRadius
        { get { return -1; } }
        #endregion

        #region ICustomizePopup
        private int m_MinHeight = 26;
        [Browsable(true), DefaultValue(26), Description("最小高度"), Category("布局")]
        public int MinHeight
        {
            get { return m_MinHeight; }
            set { m_MinHeight = value < 26 ? 26 : value; }
        }

        private int m_MinWidth = 26;
        [Browsable(true), DefaultValue(26), Description("最小宽度"), Category("布局")]
        public int MinWidth
        {
            get { return m_MinWidth; }
            set { m_MinHeight = value < 26 ? 26 : value; }
        }

        ModifySizeStyle m_eModifySizeStyle = ModifySizeStyle.eNone;
        [Browsable(true), DefaultValue(typeof(ModifySizeStyle), "eNone"), Description("修改尺寸的类型"), Category("布局")]
        public ModifySizeStyle eModifySizeStyle
        {
            get { return m_eModifySizeStyle; }
            set 
            {
                if (m_eModifySizeStyle == value) return;
                //
                m_eModifySizeStyle = value;
                //
                switch (this.eModifySizeStyle)
                {
                    case ModifySizeStyle.eHorizontal:
                        this.Padding = new Padding(1, 1, MODIFYSIZEREGIONSIZE, 1);
                        break;
                    case ModifySizeStyle.eVertical:
                        this.Padding = new Padding(1, 1, 1, MODIFYSIZEREGIONSIZE);
                        break;
                    case ModifySizeStyle.eAll:
                        this.Padding = new Padding(1, 1, 1, MODIFYSIZEREGIONSIZE + MODIFYSIZEREGIONSIZEEXPAND);
                        break;
                    default:
                        this.Padding = new Padding(1);
                        break;
                }
            }
        }

        [Browsable(false), Description("尺寸调节区框矩形"), Category("布局")]
        public virtual Rectangle ModifySizeRegionRectangle
        {
            get 
            {
                switch (this.eModifySizeStyle)
                {
                    case ModifySizeStyle.eHorizontal:
                        return new Rectangle(this.Width - MODIFYSIZEREGIONSIZE, 0, MODIFYSIZEREGIONSIZE, this.Height);
                    case ModifySizeStyle.eVertical:
                        return new Rectangle(0, this.Height - MODIFYSIZEREGIONSIZE, this.Width, MODIFYSIZEREGIONSIZE);
                    case ModifySizeStyle.eAll:
                        return new Rectangle(0, this.Height - MODIFYSIZEREGIONSIZE - MODIFYSIZEREGIONSIZEEXPAND, this.Width, MODIFYSIZEREGIONSIZE + MODIFYSIZEREGIONSIZEEXPAND);
                    default:
                        return new Rectangle(0, 0, 0, 0);
                }
            }
        }

        [Browsable(false), Description("其外框矩形"), Category("布局")]
        public Rectangle FrameRectangle
        {
            get
            {
                return new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            }
        }

        [Browsable(false), Description("获取携带的控件对象"), Category("布局")]
        System.Windows.Forms.Control ICustomizePopup.ControlObject
        {
            get { return this.m_ToolStripControlHost.Control; }
        }
        
        public void SetSize(Size size)
        {
            this.m_ToolStripControlHost.Size = 
                new Size
                (
                size.Width < MinWidth ? MinWidth : size.Width - this.Padding.Left - this.Padding.Right,
                size.Height < MinHeight ? MinHeight : size.Height - this.Padding.Top - this.Padding.Bottom
                );
        }
        #endregion

        public override object Clone()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool CustomFiltration
        {
            get
            {
                return true;
            }
        }

        public override bool Filtration(MouseEventArgs e)
        {
            return this.OwnerContainMousePoint(e.Location) || base.Filtration(e);
        }
        private bool OwnerContainMousePoint(Point point)
        {
            if (this.pOwner == null) return false;
            //
            IPopupOwner pPopupOwner = this.pOwner as IPopupOwner;
            if (pPopupOwner != null) return pPopupOwner.PopupTriggerRectangle.Contains(this.pOwner.PointToClient(point));
            else return this.pOwner.DisplayRectangle.Contains(this.pOwner.PointToClient(point));
            ////
            //return false;
        }

        Size m_MouseDownSize = new Size(0, 0);
        Point m_MouseDownPoint = new Point();
        protected override void OnMouseDown(MouseEventArgs mea)
        {
            if (mea.Button == MouseButtons.Left)
            {
                this.m_MouseDownSize = this.Size;
                this.m_MouseDownPoint = mea.Location;
            }
            else
            {
                this.m_MouseDownSize = new Size(0, 0);
                this.m_MouseDownPoint = mea.Location;
            }
            //
            base.OnMouseDown(mea);
        }

        protected override void OnMouseMove(MouseEventArgs mea)
        {
            switch (this.eModifySizeStyle)
            {
                case ModifySizeStyle.eHorizontal:
                    if (this.ModifySizeRegionRectangle.Contains(mea.Location))
                    {
                        this.Cursor = System.Windows.Forms.Cursors.SizeWE;
                        //
                        if (!this.m_MouseDownSize.IsEmpty)
                        {
                            int iW = this.m_MouseDownSize.Width + mea.X - this.m_MouseDownPoint.X;
                            this.SetSize
                                (
                                new Size
                                    (
                                    iW <= this.MinWidth ? this.MinWidth : iW,
                                    this.m_MouseDownSize.Height
                                    )
                                );
                        }
                    }
                    else
                    {
                        this.Cursor = System.Windows.Forms.Cursors.Arrow;
                    }
                    break;
                case ModifySizeStyle.eVertical:
                    if (this.ModifySizeRegionRectangle.Contains(mea.Location))
                    {
                        this.Cursor = System.Windows.Forms.Cursors.SizeNS;
                        //
                        if (!this.m_MouseDownSize.IsEmpty)
                        {
                            int iH = this.m_MouseDownSize.Height + mea.Y - this.m_MouseDownPoint.Y;
                            this.SetSize
                                (
                                new Size
                                    (
                                    this.m_MouseDownSize.Width,
                                    iH <= this.MinHeight ? this.MinHeight : iH
                                    )
                                );
                        }
                    }
                    else
                    {
                        this.Cursor = System.Windows.Forms.Cursors.Arrow;
                    }
                    break;
                case ModifySizeStyle.eAll:
                    Rectangle rectangle = this.ModifySizeRegionRectangle;
                    rectangle = Rectangle.FromLTRB(rectangle.Right - rectangle.Height, rectangle.Top, rectangle.Right, rectangle.Bottom);
                    if (rectangle.Contains(mea.Location))
                    {
                        this.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
                        //
                        if (!this.m_MouseDownSize.IsEmpty)
                        {
                            int iW = this.m_MouseDownSize.Width + mea.X - this.m_MouseDownPoint.X;
                            int iH = this.m_MouseDownSize.Height + mea.Y - this.m_MouseDownPoint.Y;
                            this.SetSize
                                (
                                new Size
                                    (
                                    iW <= this.MinWidth ? this.MinWidth : iW,
                                    iH <= this.MinHeight ? this.MinHeight : iH
                                    )
                                );
                        }
                    }
                    else
                    {
                        this.Cursor = System.Windows.Forms.Cursors.Arrow; 
                    }
                    break;
                default:
                    this.Cursor = System.Windows.Forms.Cursors.Arrow;
                    break;
            }
            //
            base.OnMouseMove(mea);
        }

        protected override void OnMouseUp(MouseEventArgs mea)
        {
            this.m_MouseDownSize = new Size(0, 0);
            this.m_MouseDownPoint = mea.Location;
            //
            base.OnMouseUp(mea);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            this.m_MouseDownSize = new Size(0, 0);
            this.m_MouseDownPoint = new Point(); 
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            //
            base.OnMouseLeave(e);
        }

        protected override void OnDraw(PaintEventArgs e)
        {
            //base.OnDraw(e);
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderCustomizePopup(
                new GISShare.Controls.WinForm.ObjectRenderEventArgs(e.Graphics, this, this.DisplayRectangle));            
        }
    }
}
