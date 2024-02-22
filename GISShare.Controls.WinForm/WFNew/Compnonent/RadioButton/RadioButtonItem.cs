using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    [DefaultEvent("CheckedChanged")]
    public class RadioButtonItem : AreaItem, IRadioButtonItem
    {
        private const int CRT_CHECKSIZE = 11;
        private const int CRT_CHECKSPACE = 1;
        
        #region 构造函数
        public RadioButtonItem() { }

        public RadioButtonItem(string strText)
        {
            this.Text = strText;
        }

        public RadioButtonItem(string strName, string strText)
        {
            this.Name = strName;
            this.Text = strText;
        }

        public RadioButtonItem(string strName, string strText, bool bChecked)
        {
            this.Name = strName;
            this.Text = strText;
            this.Checked = bChecked;
        }

        //public RadioButtonItem(GISShare.Controls.Plugin.WFNew.IRadioButtonItemP pBaseItemP)
        //{
        //    //IPlugin
        //    this.Name = pBaseItemP.Name;
        //    //ISetEntityObject
        //    GISShare.Controls.Plugin.ISetEntityObject pSetEntityObject = pBaseItemP as GISShare.Controls.Plugin.ISetEntityObject;
        //    if (pSetEntityObject != null) pSetEntityObject.SetEntityObject(this);
        //    //IBaseItemP_
        //    this.Checked = pBaseItemP.Checked;
        //    this.Enabled = pBaseItemP.Enabled;
        //    this.Font = pBaseItemP.Font;
        //    this.ForeColor = pBaseItemP.ForeColor;
        //    this.LockHeight = pBaseItemP.LockHeight;
        //    this.LockWith = pBaseItemP.LockWith;
        //    this.Padding = pBaseItemP.Padding;
        //    this.Size = pBaseItemP.Size;
        //    this.Text = pBaseItemP.Text;
        //    this.Visible = pBaseItemP.Visible;
        //    this.Category = pBaseItemP.Category;
        //    this.MinimumSize = pBaseItemP.MinimumSize;
        //    this.UsingViewOverflow = pBaseItemP.UsingViewOverflow;
        //}
        #endregion

        #region IRadioButtonItem
        [Browsable(false), Description("勾选区绘制矩形框"), Category("布局")]
        public virtual Rectangle CheckRectangle
        {
            get
            {
                Rectangle rectangle = this.DisplayRectangle;
                return new Rectangle
                    (
                    rectangle.X + this.Padding.Left,
                    rectangle.Y + this.Padding.Top + (rectangle.Height - this.Padding.Top - this.Padding.Bottom - CRT_CHECKSIZE) / 2,
                    CRT_CHECKSIZE,
                    CRT_CHECKSIZE
                    );
            }
        }

        bool m_InsideCheckRectangleTrigger = false;
        /// <summary>
        /// 鼠标在复选框内弹起方才改变（Checked属性）
        /// </summary>
        [Browsable(true), DefaultValue(false), Description("鼠标在复选框内弹起方才改变（Checked属性）"), Category("属性")]
        public bool InsideCheckRectangleTrigger
        {
            get { return m_InsideCheckRectangleTrigger; }
            set { m_InsideCheckRectangleTrigger = value; }
        }

        private ContentAlignment m_TextAlign = ContentAlignment.MiddleLeft;
        [Browsable(true), DefaultValue(typeof(ContentAlignment), "MiddleLeft"), Description("文本布局的方式"), Category("外观")]
        public virtual ContentAlignment TextAlign//BH
        {
            get
            {
                return m_TextAlign;
            }
            set { m_TextAlign = value; }
        }

        private Rectangle m_TextRectangle;
        [Browsable(false), Description("文本绘制矩形框"), Category("布局")]
        public virtual Rectangle TextRectangle
        {
            get
            {
                Rectangle rectangle = m_TextRectangle;
                Rectangle displayRectangle = this.DisplayRectangle;
                //
                int iLeftEx = this.CheckRectangle.Right + CRT_CHECKSPACE;
                //
                int iTop = rectangle.Top;
                int iLeft = rectangle.Left;
                int iRight = rectangle.Right;
                int iBottom = rectangle.Bottom;
                if (displayRectangle.Top > iTop) iTop = displayRectangle.Top;
                if (iLeftEx > iLeft) iLeft = iLeftEx;
                if (displayRectangle.Right < iRight) iRight = displayRectangle.Right;
                if (displayRectangle.Bottom < iBottom) iBottom = displayRectangle.Bottom;
                return Rectangle.FromLTRB(iLeft, iTop, iRight, iBottom);
            }
        }
        #endregion

        protected override bool RefreshBaseItemState
        {
            get
            {
                return true;
            }
        }

        public override bool LockHeight
        {
            get
            {
                return false;
            }
        }

        public override bool LockWith
        {
            get
            {
                return false;
            }
        }

        public override Size MeasureSize(Graphics g)//有待完善
        {
            SizeF sizeF = g.MeasureString(this.Text, this.Font);
            return new Size(this.Padding.Left + CRT_CHECKSIZE + CRT_CHECKSPACE + (int)sizeF.Width + 1 + this.Padding.Right, this.Padding.Top + (int)sizeF.Height + 1 + this.Padding.Bottom);
        }

        #region Clone
        public override object Clone()
        {
            RadioButtonItem baseItem = new RadioButtonItem();
            baseItem.Checked = this.Checked;
            baseItem.Enabled = this.Enabled;
            baseItem.Font = this.Font;
            baseItem.ForeColor = this.ForeColor;
            baseItem.Name = this.Name;
            baseItem.Site = this.Site;
            baseItem.Size = this.Size;
            baseItem.Tag = this.Tag;
            baseItem.Text = this.Text;
            baseItem.Padding = this.Padding;
            baseItem.TextAlign = this.TextAlign;
            baseItem.Visible = this.Visible;
            if (this.GetEventState("VisibleChanged") == EventStateStyle.eUsed) baseItem.VisibleChanged += new EventHandler(baseItem_VisibleChanged);
            if (this.GetEventState("SizeChanged") == EventStateStyle.eUsed) baseItem.SizeChanged += new EventHandler(baseItem_SizeChanged);
            if (this.GetEventState("Paint") == EventStateStyle.eUsed) baseItem.Paint += new PaintEventHandler(baseItem_Paint);
            if (this.GetEventState("MouseUp") == EventStateStyle.eUsed) baseItem.MouseUp += new MouseEventHandler(baseItem_MouseUp);
            if (this.GetEventState("MouseMove") == EventStateStyle.eUsed) baseItem.MouseMove += new MouseEventHandler(baseItem_MouseMove);
            if (this.GetEventState("MouseLeave") == EventStateStyle.eUsed) baseItem.MouseLeave += new EventHandler(baseItem_MouseLeave);
            if (this.GetEventState("MouseEnter") == EventStateStyle.eUsed) baseItem.MouseEnter += new EventHandler(baseItem_MouseEnter);
            if (this.GetEventState("MouseDown") == EventStateStyle.eUsed) baseItem.MouseDown += new MouseEventHandler(baseItem_MouseDown);
            if (this.GetEventState("MouseDoubleClick") == EventStateStyle.eUsed) baseItem.MouseDoubleClick += new MouseEventHandler(baseItem_MouseDoubleClick);
            if (this.GetEventState("MouseClick") == EventStateStyle.eUsed) baseItem.MouseClick += new MouseEventHandler(baseItem_MouseClick);
            if (this.GetEventState("LocationChanged") == EventStateStyle.eUsed) baseItem.LocationChanged += new EventHandler(baseItem_LocationChanged);
            if (this.GetEventState("EnabledChanged") == EventStateStyle.eUsed) baseItem.EnabledChanged += new EventHandler(baseItem_EnabledChanged);
            if (this.GetEventState("CheckedChanged") == EventStateStyle.eUsed) baseItem.CheckedChanged += new EventHandler(baseItem_CheckedChanged);
            return baseItem;
        }
        void baseItem_CheckedChanged(object sender, EventArgs e)
        {
            this.RelationEvent("CheckedChanged", e);
        }
        void baseItem_EnabledChanged(object sender, EventArgs e)
        {
            this.RelationEvent("EnabledChanged", e);
        }
        void baseItem_LocationChanged(object sender, EventArgs e)
        {
            this.RelationEvent("LocationChanged", e);
        }
        void baseItem_MouseClick(object sender, MouseEventArgs e)
        {
            this.RelationEvent("MouseClick", e);
        }
        void baseItem_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.RelationEvent("MouseDoubleClick", e);
        }
        void baseItem_MouseDown(object sender, MouseEventArgs e)
        {
            this.RelationEvent("MouseDown", e);
        }
        void baseItem_MouseEnter(object sender, EventArgs e)
        {
            this.RelationEvent("MouseEnter", e);
        }
        void baseItem_MouseLeave(object sender, EventArgs e)
        {
            this.RelationEvent("MouseLeave", e);
        }
        void baseItem_MouseMove(object sender, MouseEventArgs e)
        {
            this.RelationEvent("MouseMove", e);
        }
        void baseItem_MouseUp(object sender, MouseEventArgs e)
        {
            this.RelationEvent("MouseUp", e);
        }
        void baseItem_Paint(object sender, PaintEventArgs e)
        {
            this.RelationEvent("Paint", e);
        }
        void baseItem_SizeChanged(object sender, EventArgs e)
        {
            this.RelationEvent("SizeChanged", e);
        }
        void baseItem_VisibleChanged(object sender, EventArgs e)
        {
            this.RelationEvent("VisibleChanged", e);
        }
        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            this.m_TextRectangle = this.GetTextRectangle(e.Graphics);
            //
            this.OnDraw(e);
            //
            //base.OnPaint(e);
        }
        protected Rectangle GetTextRectangle(Graphics g)
        {
            Rectangle rectangle = this.DisplayRectangle;
            SizeF size = g.MeasureString(this.Text, this.Font);
            int iWidth = (int)(size.Width + 1);
            int iHeight = (int)(size.Height + 1);
            switch (this.TextAlign)
            {
                case ContentAlignment.TopLeft:
                    return new Rectangle(rectangle.Left + CRT_CHECKSIZE + CRT_CHECKSPACE,
                        rectangle.Top,
                        iWidth,
                        iHeight);
                case ContentAlignment.TopCenter:
                    return new Rectangle((rectangle.Left + CRT_CHECKSIZE + CRT_CHECKSPACE + rectangle.Right - iWidth) / 2,
                        rectangle.Top,
                        iWidth,
                        iHeight);
                case ContentAlignment.TopRight:
                    return new Rectangle(rectangle.Right - iWidth,
                        rectangle.Top,
                        iWidth,
                        iHeight);
                //
                case ContentAlignment.MiddleLeft:
                    return new Rectangle(rectangle.Left + CRT_CHECKSIZE + CRT_CHECKSPACE,
                        (rectangle.Top + rectangle.Bottom - iHeight) / 2,
                        iWidth,
                        iHeight);
                case ContentAlignment.MiddleCenter:
                    return new Rectangle((rectangle.Left + CRT_CHECKSIZE + CRT_CHECKSPACE + rectangle.Right - iWidth) / 2,
                        (rectangle.Top + rectangle.Bottom - iHeight) / 2,
                        iWidth,
                        iHeight);
                case ContentAlignment.MiddleRight:
                    return new Rectangle(rectangle.Right - iWidth,
                        (rectangle.Top + rectangle.Bottom - iHeight) / 2,
                        iWidth,
                        iHeight);
                //
                case ContentAlignment.BottomLeft:
                    return new Rectangle(rectangle.Left + CRT_CHECKSIZE + CRT_CHECKSPACE,
                        rectangle.Bottom - iHeight,
                        iWidth,
                        iHeight);
                case ContentAlignment.BottomCenter:
                    return new Rectangle((rectangle.Left + CRT_CHECKSIZE + CRT_CHECKSPACE + rectangle.Right - iWidth) / 2,
                        rectangle.Bottom - iHeight,
                        iWidth,
                        iHeight);
                case ContentAlignment.BottomRight:
                    return new Rectangle(rectangle.Right - iWidth,
                        rectangle.Bottom - iHeight,
                        iWidth,
                        iHeight);
                default:
                    return new Rectangle(0, 0, 0, 0);
            }
        }

        protected override void OnDraw(PaintEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRadioButton(
                new GISShare.Controls.WinForm.ObjectRenderEventArgs(e.Graphics, this, this.DisplayRectangle));
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonText(
                new GISShare.Controls.WinForm.TextRenderEventArgs(e.Graphics, this, this.Enabled, this.HaveShadow, this.Text, this.ForeCustomize,  this.ForeColor, this.ShadowColor, this.Font, this.TextRectangle));
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            if (mevent.Button == MouseButtons.Left)
            {
                if (!this.InsideCheckRectangleTrigger || this.CheckRectangle.Contains(mevent.Location))
                {
                    if (!this.Checked)
                    {
                        this.Checked = true;
                        if (this.pOwner is View.ISuperViewItem &&
                             this.pOwner != null &&
                             this.pOwner.pOwner != null &&
                             this.pOwner.pOwner is View.IViewList)
                        {
                            View.IViewList pViewList = (View.IViewList)this.pOwner.pOwner;
                            foreach (object one in pViewList.List)
                            {
                                View.ISuperViewItem pSuperViewItem = one as View.ISuperViewItem;
                                if (pSuperViewItem == null ||
                                    pSuperViewItem.BaseItemObject == null ||
                                    pSuperViewItem.BaseItemObject is ICheckBoxItem) continue;
                                IRadioButtonItem pRadioButtonItem = pSuperViewItem.BaseItemObject as IRadioButtonItem;
                                if (pRadioButtonItem != null && pRadioButtonItem != this)
                                {
                                    pRadioButtonItem.Checked = false;
                                }
                            }
                        }
                        else
                        {
                            IUICollectionItem pUICollectionItem = this.pOwner as IUICollectionItem;
                            if (pUICollectionItem != null)
                            {
                                foreach (BaseItem one in pUICollectionItem.BaseItems)
                                {
                                    if (one is ICheckBoxItem) continue;
                                    IRadioButtonItem pRadioButtonItem = one as IRadioButtonItem;
                                    if (pRadioButtonItem != null && pRadioButtonItem != this)
                                    {
                                        pRadioButtonItem.Checked = false;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            base.OnMouseUp(mevent);
        }
    }
}
