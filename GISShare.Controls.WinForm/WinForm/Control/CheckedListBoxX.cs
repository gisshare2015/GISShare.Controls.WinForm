using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm
{
    public class CheckedListBoxX : CheckedListBox, ICheckedListBoxX, WinForm.IItemOwner
    {
        private const int CTR_COLORREGIONWIDTH = 28;
        private const int CTR_LEFTGRIPREGIONWIDTH = 16;
        private const int CTR_IMAGEGRIPSEPARATORWIDTH = 1;

        public CheckedListBoxX()
            : base() 
        { }

        #region IBaseItem
        WFNew.RenderStyle m_eRenderStyle = WFNew.RenderStyle.eSystem;
        [Browsable(true), DefaultValue(typeof(WFNew.RenderStyle), "eSystem"), Description("渲染类型"), Category("外观")]
        public virtual WFNew.RenderStyle eRenderStyle
        {
            get { return m_eRenderStyle; }
            set { m_eRenderStyle = value; }
        }

        private WFNew.BaseItemState m_eBaseItemState = WFNew.BaseItemState.eNormal;
        protected virtual bool SetBaseItemState(WFNew.BaseItemState baseItemState)
        {
            if (this.m_eBaseItemState == baseItemState) return false;
            this.m_eBaseItemState = baseItemState;
            return true;
        }
        protected virtual bool SetBaseItemStateEx(WFNew.BaseItemState baseItemState)
        {
            if (this.m_eBaseItemState == baseItemState) return false;
            this.m_eBaseItemState = baseItemState;
            this.Refresh();
            return true;
        }
        [Browsable(false), Description("自身所处的状态（激活、按下、不可用、正常）"), Category("状态")]
        public virtual WFNew.BaseItemState eBaseItemState
        {
            get
            {
                return m_eBaseItemState;
            }
        }

        private bool m_HaveShadow = true;
        [Browsable(true), DefaultValue(true), Description("是否有字体阴影"), Category("状态")]
        public bool HaveShadow
        {
            get { return m_HaveShadow; }
            set { m_HaveShadow = value; }
        }

        private Color m_ShadowColor = System.Drawing.SystemColors.ControlText;
        [Browsable(true), DefaultValue(typeof(Color), "System.Drawing.SystemColors.ControlText"), Description("字体阴影颜色"), Category("外观")]
        public Color ShadowColor
        {
            get { return m_ShadowColor; }
            set { m_ShadowColor = value; }
        }

        private bool m_ForeCustomize = false;
        [Browsable(true), DefaultValue(false), Description("自定义文本色"), Category("状态")]
        public bool ForeCustomize
        {
            get { return m_ForeCustomize; }
            set { m_ForeCustomize = value; }
        }
        #endregion

        #region IOwner
        [Browsable(false), Description("获取其拥有者"), Category("关联")]
        public virtual WFNew.IOwner pOwner { get { return null; } }
        #endregion

        #region IItemOwner
        bool m_ShowGripRegion = false;
        [Browsable(true), DefaultValue(false), Description("是否显示绘制区"), Category("外观")]
        public bool ShowGripRegion
        {
            get { return m_ShowGripRegion; }
            set { m_ShowGripRegion = value; }
        }

        [Browsable(false), Description("值为颜色项时，绘制颜色区所需的宽度"), Category("布局")]
        public int ColorRegionWidth
        {
            get { return CTR_COLORREGIONWIDTH; }
        }

        [Browsable(false), Description("图片绘制区所需的宽度"), Category("布局")]
        public int ImageGripRegionWidth
        {
            get
            {
                return this.ShowGripRegion ? this.ItemHeight : 0;
            }
        }

        [Browsable(false), Description("最左端绘制区所需的宽度"), Category("布局")]
        public int LeftGripRegionWidth
        {
            get
            {
                return CTR_LEFTGRIPREGIONWIDTH;
            }
        }

        WinForm.ItemDrawStyle m_eItemDrawStyle = WinForm.ItemDrawStyle.eCheckBox;
        [Browsable(true), DefaultValue(typeof(WinForm.ItemDrawStyle), "eCheckBox"), Description("记录它的绘制状态"), Category("外观")]
        public WinForm.ItemDrawStyle eItemDrawStyle
        {
            get
            {
                if (this.m_eItemDrawStyle != WinForm.ItemDrawStyle.eCheckBox) return WinForm.ItemDrawStyle.eRadioButton;
                return this.m_eItemDrawStyle;
            }
            set { m_eItemDrawStyle = value; }
        }
        #endregion

        #region IListBoxX
        bool m_AutoMouseMoveSeleced = false;
        [Browsable(true), DefaultValue(false), Description("鼠标移动遂即选中所在项"), Category("状态")]
        public bool AutoMouseMoveSeleced
        {
            get { return m_AutoMouseMoveSeleced; }
            set { m_AutoMouseMoveSeleced = value; }
        }

        int m_ImageSpace = 1;
        [Browsable(true), DefaultValue(1), Description("图片四周的间距"), Category("布局")]
        public int ImageSpace
        {
            get { return m_ImageSpace; }
            set { m_ImageSpace = value; }
        }

        int m_ITSpace = 1;
        [Browsable(true), DefaultValue(1), Description("图片与文本的间距"), Category("布局")]
        public int ITSpace
        {
            get { return m_ITSpace; }
            set { m_ITSpace = value; }
        }

        [Browsable(false), Description("其外框矩形"), Category("布局")]
        public Rectangle FrameRectangle
        {
            get
            {
                return new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            }
        }

        [Browsable(true),
        Editor(typeof(ItemCollectionEditer), typeof(System.Drawing.Design.UITypeEditor)),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
        Description("其所携带的子项集合"),
        Category("子项")]
        public new CheckedListBox.ObjectCollection Items
        {
            get { return base.Items; }
        }

        public void InvalidateItem(int iIndex)
        {
            if (iIndex < 0 || iIndex >= this.Items.Count) return;
            //
            Rectangle rectangle = this.DisplayRectangle;
            int iNum = iIndex - this.TopIndex;
            if (this.MultiColumn)
            {
                int iColumnWidth = this.ColumnWidth <= 0 ? 120 : this.ColumnWidth;
                int iColumnNum = rectangle.Height / this.ItemHeight;
                int iColumnIndex = iNum / iColumnNum;
                int iRowIndex = iNum % iColumnNum;
                this.Invalidate(new Rectangle(rectangle.X + iColumnIndex * iColumnWidth, iRowIndex * this.ItemHeight, iColumnWidth, this.ItemHeight));
            }
            else
            {
                this.Invalidate(new Rectangle(rectangle.X, iNum * this.ItemHeight, rectangle.Width, this.ItemHeight));
            }
        }
        #endregion

        protected override void RefreshItem(int iIndex)
        {
            this.InvalidateItem(iIndex);
        }

        protected override void RefreshItems()
        {
            int iCount = 0;
            //
            Rectangle rectangle = this.DisplayRectangle;
            if (this.MultiColumn)
            {
                int iColumnWidth = this.ColumnWidth <= 0 ? 120 : this.ColumnWidth;
                int iColumnNum = rectangle.Height / this.ItemHeight;
                int iColumnCount = rectangle.Width / iColumnWidth + 1;
                iCount = iColumnNum * iColumnCount + this.TopIndex;
            }
            else
            {
                iCount = rectangle.Height / this.ItemHeight + this.TopIndex;
            }
            //
            iCount = (iCount >= this.Items.Count) ? this.Items.Count - 1 : iCount;
            //
            for (int i = this.TopIndex; i <= iCount; i++)
            {
                this.InvalidateItem(i);
            }
        }

        private IList<int> m_SelectedIndicesPre = new List<int>();
        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            base.OnSelectedIndexChanged(e);
            //
            this.SetSelectedIndicesPre();
        }
        private void SetSelectedIndicesPre()
        {
            foreach (int one in this.m_SelectedIndicesPre)
            {
                if (!this.SelectedIndices.Contains(one))
                    this.InvalidateItem(one);
            }
            foreach (int one in this.SelectedIndices)
            {
                if (!this.m_SelectedIndicesPre.Contains(one))
                    this.InvalidateItem(one);
            }
            //
            this.m_SelectedIndicesPre.Clear();
            foreach (int one in this.SelectedIndices)
            {
                this.m_SelectedIndicesPre.Add(one);
            }
        }

        private int m_MouseLoctionIndex = -1;
        private void SetMouseLoctionIndex(int iIndex)
        {
            if (this.m_MouseLoctionIndex == iIndex) return;
            //
            if (!this.m_IsMouseDown) this.InvalidateItem(m_MouseLoctionIndex);
            //
            m_MouseLoctionIndex = iIndex;
            //
            if (!this.m_IsMouseDown) this.InvalidateItem(m_MouseLoctionIndex);
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (this.AutoMouseMoveSeleced) this.SetMouseLoctionIndex(this.IndexFromPoint(this.PointToClient(Form.MousePosition)));
            //
            base.OnMouseMove(e);
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            //
            if (this.AutoMouseMoveSeleced) this.SetMouseLoctionIndex(-1);
        }

        private bool m_IsMouseDown = false;
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case (int)GISShare.Win32.Msgs.WM_LBUTTONDOWN:
                    //System.Diagnostics.Debug.WriteLine(String.Format("{0}----{1}", this.Name, "WM_LBUTTONDOWN"));
                    this.m_IsMouseDown = true;
                    switch (this.SelectionMode)
                    {
                        case SelectionMode.One:
                            this.SelectedIndex = this.IndexFromPoint(GISShare.Win32.NativeMethods.LParamToMouseLocation((int)m.LParam));
                            break;
                        default:
                            break;
                    }
                    break;
                case (int)GISShare.Win32.Msgs.WM_LBUTTONUP:
                    //System.Diagnostics.Debug.WriteLine(String.Format("{0}----{1}", this.Name, "WM_LBUTTONUP"));
                    base.WndProc(ref m);
                    this.m_IsMouseDown = false;
                    return;
                case (int)GISShare.Win32.Msgs.WM_NCPAINT:
                    //System.Diagnostics.Debug.WriteLine(String.Format("{0}----{1}", this.Name, "WM_NCPAINT"));
                    base.WndProc(ref m);
                    if (this.BorderStyle != BorderStyle.None) this.WmNCPaint(ref m);
                    return;
            }
            //
            base.WndProc(ref m);
        }
        private void WmNCPaint(ref Message m)
        {
            IntPtr iHandle = GISShare.Win32.API.GetWindowDC(m.HWnd);
            try
            {
                Graphics g = Graphics.FromHdc(iHandle);
                //
                this.OnNCPaint(new PaintEventArgs(g, this.DisplayRectangle));
                //
                g.Dispose();
            }
            catch { }
            finally
            {
                GISShare.Win32.API.ReleaseDC(m.HWnd, iHandle);
            }
        }

        protected virtual void OnNCPaint(PaintEventArgs e)
        {
            this.OnNCDraw(e);
        }

        protected virtual void OnNCDraw(PaintEventArgs e)
        {
            GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderListBoxNC
                (
                new ObjectRenderEventArgs(e.Graphics, this, this.DisplayRectangle)
                );
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            //base.OnDrawItem(e);
            //
            if (this.Items.Count > 0)
            {
                if (e.Index < 0 || e.Index >= this.Items.Count) return;
                //
                WFNew.BaseItemState eBaseItemState = GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal;
                if (this.AutoMouseMoveSeleced &&
                    e.Index == this.IndexFromPoint(this.PointToClient(Form.MousePosition)))
                {
                    eBaseItemState = this.m_IsMouseDown ? WFNew.BaseItemState.ePressed : GISShare.Controls.WinForm.WFNew.BaseItemState.eHot;
                }
                //
                if (!this.DrawItemEx(e, eBaseItemState, this.Items[e.Index]))
                {
                    if (!this.DrawItem_ImageItem(e, eBaseItemState, this.Items[e.Index] as WinForm.IImageItem))
                    {
                        if (!this.DrawItem_ColorItem(e, eBaseItemState, this.Items[e.Index] as WinForm.IColorItem))
                        {
                            if (!this.DrawItem_FontItem(e, eBaseItemState, this.Items[e.Index] as WinForm.IFontItem))
                            {
                                if (!this.DrawItem_Item(e, eBaseItemState, this.Items[e.Index] as WinForm.IItem))
                                {
                                    object obj = this.Items[e.Index];
                                    string str = "null";
                                    if (obj != null) str = obj.ToString();
                                    this.DrawItem_Item(e, eBaseItemState, new WinForm.Item(str, str));
                                }
                            }
                        }
                    }
                }
            }
        }
        protected virtual bool DrawItemEx(DrawItemEventArgs e, WFNew.BaseItemState eBaseItemState, object item)
        {
            return false;
        }
        private bool DrawItem_ImageItem(DrawItemEventArgs e, WFNew.BaseItemState eBaseItemState, WinForm.IImageItem item)
        {
            if (item == null) return false;
            //
            if (this.ShowGripRegion)
            {
                #region ShowGripRegionT
                GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderItem
                    (
                    new ItemRenderEventArgs
                        (
                        e.Graphics,
                        item,
                        this,
                        this.GetItemCheckState(e.Index),
                        eBaseItemState,
                        new Rectangle(e.Bounds.X, e.Bounds.Y, this.LeftGripRegionWidth, e.Bounds.Height),
                        new Rectangle(e.Bounds.X, e.Bounds.Y, this.LeftGripRegionWidth + this.ItemHeight + CTR_IMAGEGRIPSEPARATORWIDTH, e.Bounds.Height),
                        Rectangle.FromLTRB(e.Bounds.Left + this.LeftGripRegionWidth + this.ItemHeight + CTR_IMAGEGRIPSEPARATORWIDTH, e.Bounds.Top, e.Bounds.Right, e.Bounds.Bottom),
                        e.Bounds
                        )
                    );
                //
                if (item.Image != null)
                {
                    GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderImage
                        (
                        new GISShare.Controls.WinForm.ImageRenderEventArgs
                            (
                            e.Graphics,
                            this,
                            this.Enabled,
                            item.Image,
                            new Rectangle(e.Bounds.Left + this.LeftGripRegionWidth + this.ImageSpace, e.Bounds.Top + this.ImageSpace, this.ItemHeight - 2 * this.ImageSpace, this.ItemHeight - 2 * this.ImageSpace)
                            )
                         );
                }
                int iH = (int)e.Graphics.MeasureString(item.Text, item.Font).Height;// (int)(e.Graphics.MeasureString(item.Text, item.Font).Height + 1);
                Rectangle rectangle = new Rectangle
                    (
                    e.Bounds.Left + this.LeftGripRegionWidth + this.ItemHeight + CTR_IMAGEGRIPSEPARATORWIDTH + this.ITSpace,
                    (e.Bounds.Top + e.Bounds.Bottom - iH) / 2 + 2,
                    e.Bounds.Width - this.LeftGripRegionWidth - this.ItemHeight - CTR_IMAGEGRIPSEPARATORWIDTH - this.ITSpace,
                    iH
                    );
                GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderText
                    (
                    new GISShare.Controls.WinForm.TextRenderEventArgs
                        (
                        e.Graphics,
                        this,
                        this.Enabled,
                        false,
                        item.Text,
                        false,
                        item.ForeColor,
                        item.ForeColor,
                        item.Font,
                        rectangle
                        )
                    );
                #endregion
            }
            else
            {
                #region ShowGripRegionF
                GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderItem
                    (
                    new ItemRenderEventArgs
                       (
                        e.Graphics,
                        item,
                        this,
                        this.GetItemCheckState(e.Index),
                        eBaseItemState,
                        new Rectangle(e.Bounds.X, e.Bounds.Y, this.LeftGripRegionWidth, e.Bounds.Height),
                        new Rectangle(e.Bounds.X, e.Bounds.Y, this.LeftGripRegionWidth, e.Bounds.Height),
                        Rectangle.FromLTRB(e.Bounds.Left + this.LeftGripRegionWidth, e.Bounds.Top, e.Bounds.Right, e.Bounds.Bottom),
                        e.Bounds)
                    );
                //
                if (item.Image != null)
                {
                    GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderImage
                        (
                        new GISShare.Controls.WinForm.ImageRenderEventArgs
                            (
                            e.Graphics,
                            this,
                            this.Enabled,
                            item.Image,
                            new Rectangle(e.Bounds.Left + this.LeftGripRegionWidth + this.ImageSpace, e.Bounds.Top + this.ImageSpace, this.ItemHeight - 2 * this.ImageSpace, this.ItemHeight - 2 * this.ImageSpace)
                            )
                         );
                    int iH = (int)e.Graphics.MeasureString(item.Text, item.Font).Height;// (int)(e.Graphics.MeasureString(item.Text, item.Font).Height + 1);
                    Rectangle rectangle = new Rectangle
                        (
                        e.Bounds.Left + this.LeftGripRegionWidth + this.ItemHeight + this.ITSpace,
                        (e.Bounds.Top + e.Bounds.Bottom - iH) / 2 + 2,
                        e.Bounds.Width - this.LeftGripRegionWidth - this.ItemHeight - this.ITSpace,
                        iH
                        );
                    GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderText
                        (
                        new GISShare.Controls.WinForm.TextRenderEventArgs
                            (
                            e.Graphics,
                            this, 
                            this.Enabled,
                            false,
                            item.Text,
                            false, item.ForeColor, item.ForeColor,
                            item.Font,
                            rectangle
                            )
                        );
                }
                else
                {
                    int iH = (int)e.Graphics.MeasureString(item.Text, item.Font).Height;// (int)(e.Graphics.MeasureString(item.Text, item.Font).Height + 1);
                    Rectangle rectangle = new Rectangle
                        (
                        e.Bounds.Left + this.LeftGripRegionWidth,
                        (e.Bounds.Top + e.Bounds.Bottom - iH) / 2 + 2,
                        e.Bounds.Width - this.LeftGripRegionWidth,
                        iH
                        );
                    GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderText
                        (
                        new GISShare.Controls.WinForm.TextRenderEventArgs
                            (
                            e.Graphics,
                            this,
                            this.Enabled,
                            false,
                            item.Text,
                            false,
                            item.ForeColor,
                            item.ForeColor,
                            item.Font,
                            rectangle
                            )
                        );
                }
                #endregion
            }
            //
            return true;
        }
        private bool DrawItem_ColorItem(DrawItemEventArgs e, WFNew.BaseItemState eBaseItemState, WinForm.IColorItem item)
        {
            if (item == null) return false;
            //
            int iW;
            if (this.ShowGripRegion)
            {
                iW = this.LeftGripRegionWidth + this.ItemHeight;
                GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderItem
                       (
                       new ItemRenderEventArgs
                           (
                           e.Graphics,
                           item,
                           this,
                           this.GetItemCheckState(e.Index),
                           eBaseItemState,
                           new Rectangle(e.Bounds.X, e.Bounds.Y, this.LeftGripRegionWidth, e.Bounds.Height),
                           new Rectangle(e.Bounds.X, e.Bounds.Y, iW + CTR_IMAGEGRIPSEPARATORWIDTH, e.Bounds.Height),
                           Rectangle.FromLTRB(e.Bounds.Left + iW + CTR_IMAGEGRIPSEPARATORWIDTH, e.Bounds.Top, e.Bounds.Right, e.Bounds.Bottom),
                           e.Bounds
                           )
                       );
            }
            else
            {
                iW = this.LeftGripRegionWidth + CTR_COLORREGIONWIDTH;
                GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderItem
                       (
                       new ItemRenderEventArgs
                           (
                           e.Graphics,
                           item,
                           this,
                           this.GetItemCheckState(e.Index),
                           eBaseItemState,
                           new Rectangle(e.Bounds.X, e.Bounds.Y, this.LeftGripRegionWidth, e.Bounds.Height),
                           new Rectangle(e.Bounds.X, e.Bounds.Y, iW + CTR_IMAGEGRIPSEPARATORWIDTH, e.Bounds.Height),
                           e.Bounds,
                           e.Bounds
                           )
                       );
            }
            int iH = (int)e.Graphics.MeasureString(item.Text, item.Font).Height;// (int)(e.Graphics.MeasureString(item.Text, item.Font).Height + 1);
            Rectangle rectangle = new Rectangle
                (
                e.Bounds.Left + iW + CTR_IMAGEGRIPSEPARATORWIDTH + this.ITSpace,
                (e.Bounds.Top + e.Bounds.Bottom - iH) / 2 + 2,
                e.Bounds.Width - iW - CTR_IMAGEGRIPSEPARATORWIDTH - this.ITSpace,
                iH
                );
            GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderText
                (
                new GISShare.Controls.WinForm.TextRenderEventArgs(e.Graphics, this, this.Enabled, false, item.Text, false, item.ForeColor, item.ForeColor, item.Font, rectangle)
                );
            //
            return true;
        }
        private bool DrawItem_FontItem(DrawItemEventArgs e, WFNew.BaseItemState eBaseItemState, WinForm.IFontItem item)
        {
            if (item == null) return false;
            //
            if (this.ShowGripRegion)
            {
                #region ShowGripRegionT
                GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderItem
                    (
                    new ItemRenderEventArgs
                        (
                        e.Graphics,
                        item,
                        this,
                        this.GetItemCheckState(e.Index),
                        eBaseItemState,
                        new Rectangle(e.Bounds.X, e.Bounds.Y, this.LeftGripRegionWidth, e.Bounds.Height),
                        new Rectangle(e.Bounds.X, e.Bounds.Y, this.LeftGripRegionWidth + this.ItemHeight + CTR_IMAGEGRIPSEPARATORWIDTH, e.Bounds.Height),
                        Rectangle.FromLTRB(e.Bounds.Left + this.LeftGripRegionWidth + this.ItemHeight + CTR_IMAGEGRIPSEPARATORWIDTH, e.Bounds.Top, e.Bounds.Right, e.Bounds.Bottom),
                        e.Bounds
                        )
                    );
                //
                int iH = (int)e.Graphics.MeasureString(item.Text, item.Font).Height;// (int)(e.Graphics.MeasureString(item.Text, item.Font).Height + 1);
                Rectangle rectangle = new Rectangle
                    (
                    e.Bounds.Left + this.LeftGripRegionWidth + this.ItemHeight + CTR_IMAGEGRIPSEPARATORWIDTH + this.ITSpace,
                    (e.Bounds.Top + e.Bounds.Bottom - iH) / 2 + 2,
                    e.Bounds.Width - this.LeftGripRegionWidth - this.ItemHeight - CTR_IMAGEGRIPSEPARATORWIDTH - this.ITSpace,
                    iH
                    );
                GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderText
                    (
                    new GISShare.Controls.WinForm.TextRenderEventArgs(e.Graphics, this, this.Enabled, false, item.Text, false, item.ForeColor, item.ForeColor, item.Font, rectangle)
                    );
                #endregion
            }
            else
            {
                #region ShowGripRegionF
                GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderItem
                    (
                    new ItemRenderEventArgs
                        (e.Graphics,
                        item,
                        this,
                        this.GetItemCheckState(e.Index),
                        eBaseItemState,
                        new Rectangle(e.Bounds.X, e.Bounds.Y, this.LeftGripRegionWidth, e.Bounds.Height),
                        new Rectangle(e.Bounds.X, e.Bounds.Y, this.LeftGripRegionWidth, e.Bounds.Height),
                        Rectangle.FromLTRB(e.Bounds.Left + this.LeftGripRegionWidth, e.Bounds.Top, e.Bounds.Right, e.Bounds.Bottom),
                        e.Bounds)
                    );
                //
                int iH = (int)e.Graphics.MeasureString(item.Text, item.Font).Height;// (int)(e.Graphics.MeasureString(item.Text, item.Font).Height + 1);
                Rectangle rectangle = new Rectangle
                    (
                    e.Bounds.Left + this.LeftGripRegionWidth,
                    (e.Bounds.Top + e.Bounds.Bottom - iH) / 2 + 2,
                    e.Bounds.Width - this.LeftGripRegionWidth,
                    iH
                    );
                GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderText
                    (
                    new GISShare.Controls.WinForm.TextRenderEventArgs(e.Graphics, this, this.Enabled, false, item.Text, false, item.ForeColor, item.ForeColor, item.Font, rectangle)
                    );
                #endregion
            }
            //
            return true;
        }
        private bool DrawItem_Item(DrawItemEventArgs e, WFNew.BaseItemState eBaseItemState, WinForm.IItem item)
        {
            if (item == null) return false;
            //
            if (this.ShowGripRegion)
            {
                #region ShowGripRegionT
                GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderItem
                    (
                    new ItemRenderEventArgs
                        (
                        e.Graphics,
                        item,
                        this,
                        this.GetItemCheckState(e.Index),
                        eBaseItemState,
                        new Rectangle(e.Bounds.X, e.Bounds.Y, this.LeftGripRegionWidth, e.Bounds.Height),
                        new Rectangle(e.Bounds.X, e.Bounds.Y, this.LeftGripRegionWidth + this.ItemHeight + CTR_IMAGEGRIPSEPARATORWIDTH, e.Bounds.Height),
                        Rectangle.FromLTRB(e.Bounds.Left + this.LeftGripRegionWidth + this.ItemHeight + CTR_IMAGEGRIPSEPARATORWIDTH, e.Bounds.Top, e.Bounds.Right, e.Bounds.Bottom),
                        e.Bounds
                        )
                    );
                //
                int iH = (int)e.Graphics.MeasureString(item.Text, e.Font).Height;// (int)(e.Graphics.MeasureString(item.Text, item.Font).Height + 1);
                Rectangle rectangle = new Rectangle
                    (
                    e.Bounds.Left + this.LeftGripRegionWidth + this.ItemHeight + CTR_IMAGEGRIPSEPARATORWIDTH + this.ITSpace,
                    (e.Bounds.Top + e.Bounds.Bottom - iH) / 2 + 2,
                    e.Bounds.Width - this.LeftGripRegionWidth - this.ItemHeight - CTR_IMAGEGRIPSEPARATORWIDTH - this.ITSpace,
                    iH
                    );
                GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderText
                    (
                    new GISShare.Controls.WinForm.TextRenderEventArgs(e.Graphics, this, this.Enabled, false, item.Text, false, e.ForeColor, e.ForeColor, e.Font, rectangle)
                    );
                #endregion
            }
            else
            {
                #region ShowGripRegionF
                GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderItem
                    (
                    new ItemRenderEventArgs
                        (e.Graphics,
                        item,
                        this,
                        this.GetItemCheckState(e.Index),
                        eBaseItemState,
                        new Rectangle(e.Bounds.X, e.Bounds.Y, this.LeftGripRegionWidth, e.Bounds.Height),
                        new Rectangle(e.Bounds.X, e.Bounds.Y, this.LeftGripRegionWidth, e.Bounds.Height),
                        Rectangle.FromLTRB(e.Bounds.Left + this.LeftGripRegionWidth, e.Bounds.Top, e.Bounds.Right, e.Bounds.Bottom),
                        e.Bounds
                        )
                    );
                //
                int iH = (int)e.Graphics.MeasureString(item.Text, e.Font).Height;// (int)(e.Graphics.MeasureString(item.Text, item.Font).Height + 1);
                Rectangle rectangle = new Rectangle
                    (
                    e.Bounds.Left + this.LeftGripRegionWidth,
                    (e.Bounds.Top + e.Bounds.Bottom - iH) / 2 + 2,
                    e.Bounds.Width - this.LeftGripRegionWidth,
                    iH
                    );
                GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderText
                    (
                    new GISShare.Controls.WinForm.TextRenderEventArgs(e.Graphics, this, this.Enabled, false, item.Text, false, e.ForeColor, e.ForeColor, e.Font, rectangle)
                    );
                #endregion
            }
            //
            return true;
        }
    }
}
