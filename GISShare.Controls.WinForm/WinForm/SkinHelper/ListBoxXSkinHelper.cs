using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm
{
    /// <summary>
    /// ÎÞÐ§
    /// </summary>
    public class ListBoxXSkinHelper : ControlSkinHelper, IListBoxX, WinForm.IItemOwner
    {
        private const int CTR_COLORREGIONWIDTH = 28;
        private const int CTR_LEFTGRIPREGIONWIDTH = 16;
        private const int CTR_IMAGEGRIPSEPARATORWIDTH = 1;

        private ListBox m_HostListBox;

        public ListBoxXSkinHelper(ListBox hostListBox)
            : base(hostListBox)
        {
            this.m_HostListBox = hostListBox;
            //
            this.m_HostListBox.MouseMove += new MouseEventHandler(HostListBox_MouseMove);
            this.m_HostListBox.MouseLeave += new EventHandler(HostListBox_MouseLeave);
            //
            this.m_HostListBox.SelectedIndexChanged += new EventHandler(HostListBox_SelectedIndexChanged);
            this.m_HostListBox.DrawItem += new DrawItemEventHandler(HostListBox_DrawItem);
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
        void HostListBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.AutoMouseMoveSeleced) this.SetMouseLoctionIndex(this.IndexFromPoint(this.PointToClient(Form.MousePosition)));
        }
        void HostListBox_MouseLeave(object sender, EventArgs e)
        {
            if (this.AutoMouseMoveSeleced) this.SetMouseLoctionIndex(-1);
        }
        //
        private IList<int> m_SelectedIndicesPre = new List<int>();
        void HostListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
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
        //
        void HostListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (this.Items == null || this.Items.Count <= 0) return;
            //
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
                        this.SelectedIndices.Contains(e.Index) ? CheckState.Checked : CheckState.Unchecked,
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
                        this.SelectedIndices.Contains(e.Index) ? CheckState.Checked : CheckState.Unchecked,
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
                            false, item.ForeColor, item.ForeColor,
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
                           this.SelectedIndices.Contains(e.Index) ? CheckState.Checked : CheckState.Unchecked,
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
                           this.SelectedIndices.Contains(e.Index) ? CheckState.Checked : CheckState.Unchecked,
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
                        this.SelectedIndices.Contains(e.Index) ? CheckState.Checked : CheckState.Unchecked,
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
                        this.SelectedIndices.Contains(e.Index) ? CheckState.Checked : CheckState.Unchecked,
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
                        this.SelectedIndices.Contains(e.Index) ? CheckState.Checked : CheckState.Unchecked,
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
                        this.SelectedIndices.Contains(e.Index) ? CheckState.Checked : CheckState.Unchecked,
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

        #region ×¢²á
        protected override void UnregisterEventHandlers(Control hostControl)
        {
            base.UnregisterEventHandlers(hostControl);
            //
            if (this.m_HostListBox == null || this.m_HostListBox.IsDisposed) return;
            //
            this.m_HostListBox.MouseMove -= new MouseEventHandler(HostListBox_MouseMove);
            this.m_HostListBox.MouseLeave -= new EventHandler(HostListBox_MouseLeave);
            this.m_HostListBox.SelectedIndexChanged -= new EventHandler(HostListBox_SelectedIndexChanged);
            this.m_HostListBox.DrawItem -= new DrawItemEventHandler(HostListBox_DrawItem);
        }
        #endregion

        #region IOwner
        public virtual WFNew.IOwner pOwner { get { return null; } }
        #endregion

        #region IItemOwner
        bool m_ShowGripRegion = false;
        public bool ShowGripRegion
        {
            get { return m_ShowGripRegion; }
            set { m_ShowGripRegion = value; }
        }

        public int ColorRegionWidth
        {
            get { return CTR_COLORREGIONWIDTH; }
        }

        public int ImageGripRegionWidth
        {
            get
            {
                return this.ShowGripRegion ? this.ItemHeight : 0;
            }
        }

        public int LeftGripRegionWidth
        {
            get
            {
                switch (this.eItemDrawStyle)
                {
                    case WinForm.ItemDrawStyle.eRadioButton:
                    case WinForm.ItemDrawStyle.eCheckBox:
                        return CTR_LEFTGRIPREGIONWIDTH;
                    default:
                        return 0;
                }
            }
        }

        WinForm.ItemDrawStyle m_eItemDrawStyle = WinForm.ItemDrawStyle.eSimply;
        public WinForm.ItemDrawStyle eItemDrawStyle
        {
            get { return m_eItemDrawStyle; }
            set { m_eItemDrawStyle = value; }
        }
        #endregion

        #region IListBoxX
        bool m_AutoMouseMoveSeleced = false;
        public bool AutoMouseMoveSeleced
        {
            get { return m_AutoMouseMoveSeleced; }
            set { m_AutoMouseMoveSeleced = value; }
        }

        int m_ImageSpace = 1;
        public int ImageSpace
        {
            get { return m_ImageSpace; }
            set { m_ImageSpace = value; }
        }

        int m_ITSpace = 1;
        public int ITSpace
        {
            get { return m_ITSpace; }
            set { m_ITSpace = value; }
        }

        public Rectangle FrameRectangle
        {
            get
            {
                return new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            }
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

        //

        public BorderStyle BorderStyle
        {
            get
            {
                if (this.m_HostListBox == null || this.m_HostListBox.IsDisposed) return BorderStyle.Fixed3D;
                return this.m_HostListBox.BorderStyle;
            }
        }

        public int ColumnWidth
        {
            get
            {
                if (this.m_HostListBox == null || this.m_HostListBox.IsDisposed) return 120;
                return this.m_HostListBox.ColumnWidth;
            }
        }

        public DrawMode DrawMode
        {
            get
            {
                if (this.m_HostListBox == null || this.m_HostListBox.IsDisposed) return DrawMode.OwnerDrawFixed;
                return this.m_HostListBox.DrawMode;
            }
        }

        public int ItemHeight
        {
            get
            {
                if (this.m_HostListBox == null || this.m_HostListBox.IsDisposed) return 12;
                return this.m_HostListBox.ItemHeight;
            }
        }

        public bool MultiColumn
        {
            get
            {
                if (this.m_HostListBox == null || this.m_HostListBox.IsDisposed) return false;
                return this.m_HostListBox.MultiColumn;
            }
        }

        public int SelectedIndex
        {
            get
            {
                if (this.m_HostListBox == null || this.m_HostListBox.IsDisposed) return -1;
                return this.m_HostListBox.SelectedIndex;
            }
            set
            {
                if (this.m_HostListBox == null || this.m_HostListBox.IsDisposed) return;
                this.m_HostListBox.SelectedIndex = value;
            }
        }

        public ListBox.SelectedIndexCollection SelectedIndices
        {
            get
            {
                if (this.m_HostListBox == null || this.m_HostListBox.IsDisposed) return null;
                return this.m_HostListBox.SelectedIndices;
            }
        }

        public object SelectedItem
        {
            get
            {
                if (this.m_HostListBox == null || this.m_HostListBox.IsDisposed) return null;
                return this.m_HostListBox.SelectedItem;
            }
        }

        public SelectionMode SelectionMode
        {
            get
            {
                if (this.m_HostListBox == null || this.m_HostListBox.IsDisposed) return SelectionMode.One;
                return this.m_HostListBox.SelectionMode;
            }
        }

        public ListBox.SelectedObjectCollection SelectedItems
        {
            get
            {
                if (this.m_HostListBox == null || this.m_HostListBox.IsDisposed) return null;
                return this.m_HostListBox.SelectedItems;
            }
        }

        public int TopIndex
        {
            get
            {
                if (this.m_HostListBox == null || this.m_HostListBox.IsDisposed) return -1;
                return this.m_HostListBox.TopIndex;
            }
        }

        public int IndexFromPoint(Point p)
        {
            if (this.m_HostListBox == null || this.m_HostListBox.IsDisposed) return -1;
            return this.m_HostListBox.IndexFromPoint(p);
        }

        public int IndexFromPoint(int x, int y)
        {
            if (this.m_HostListBox == null || this.m_HostListBox.IsDisposed) return -1;
            return this.m_HostListBox.IndexFromPoint(x, y);
        }
        #endregion

        public ListBox.ObjectCollection Items
        {
            get
            {
                if (this.m_HostListBox == null || this.m_HostListBox.IsDisposed) return null;
                return this.m_HostListBox.Items;
            }
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
                            int iIndex = this.IndexFromPoint(GISShare.Win32.NativeMethods.LParamToMouseLocation((int)m.LParam));
                            if (iIndex >= 0 && iIndex < this.Items.Count) this.SelectedIndex = iIndex;
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
    }
}
