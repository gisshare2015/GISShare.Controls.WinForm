using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;
using System.ComponentModel;

namespace GISShare.Controls.WinForm
{
    public class ListViewX : ListView, IListViewX
    {
        private const int CTR_COLORREGIONWIDTH = 28;
        private const int CTR_PLUSMINUSREGIONWIDTH = 19;
        private const int CTR_LEFTGRIPREGIONWIDTH = 16;
        private const int CTR_IMAGEGRIPREGIONWIDTH = 18;
        private const int CTR_IMAGEGRIPSEPARATORWIDTH = 1;

        private readonly BufferedGraphicsContext m_BufferedGraphicsContext;
        private BufferedGraphics m_BufferedGraphics;
        private Size m_CurrentCacheSize = new Size();

        public ListViewX()
        {
            this.m_BufferedGraphicsContext = BufferedGraphicsManager.Current;
        }

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
            get 
            {
                switch (this.View)
                {
                    case View.LargeIcon:
                    case View.Tile:
                        return false;
                    default:
                        return m_ShowGripRegion;
                }
            }
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
                switch (this.View) 
                {
                    case View.LargeIcon:
                        if (this.LargeImageList != null) return this.LargeImageList.ImageSize.Width;
                        break;
                    case View.Tile:
                        if (this.LargeImageList != null) return this.LargeImageList.ImageSize.Width + 2 * CTR_IMAGEGRIPSEPARATORWIDTH;
                        break;
                    default:
                        if (this.SmallImageList != null) return this.SmallImageList.ImageSize.Width;
                        break;
                }
                return 0;
            }
        }

        [Browsable(false), Description("最左端绘制区所需的宽度"), Category("布局")]
        public int LeftGripRegionWidth
        {
            get
            {
                if (this.StateImageList != null) return StateImageList.ImageSize.Width + 2 * CTR_IMAGEGRIPSEPARATORWIDTH;
                else if (this.StateImageList == null && this.CheckBoxes) return CTR_LEFTGRIPREGIONWIDTH;
                else return 0;
            }
        }

        [Browsable(false), Description("记录它的绘制状态"), Category("外观")]
        public WinForm.ItemDrawStyle eItemDrawStyle
        {
            get
            {
                if (this.StateImageList != null) return GISShare.Controls.WinForm.ItemDrawStyle.eImageLabel;
                if (this.CheckBoxes) return GISShare.Controls.WinForm.ItemDrawStyle.eCheckBox;
                return GISShare.Controls.WinForm.ItemDrawStyle.eSimply;
            }
        }
        #endregion

        #region IListViewX
        int m_ImageSpace = 1;
        [Browsable(true), DefaultValue(1), Description("图片四周的间距"), Category("布局")]
        public int ImageSpace
        {
            get { return m_ImageSpace; }
            set { m_ImageSpace = value; }
        }

        bool m_AutoMouseMoveSeleced = false;
        [Browsable(true), DefaultValue(false), Description("鼠标移动遂即选中所在项"), Category("状态")]
        public bool AutoMouseMoveSeleced
        {
            get { return m_AutoMouseMoveSeleced; }
            set { m_AutoMouseMoveSeleced = value; }
        }

        bool m_AutoDrawSubItem = false;
        [Browsable(true), DefaultValue(false), Description("自动绘制子项（即：使用子项的背景信息）"), Category("外观")]
        public bool AutoDrawSubItem
        {
            get { return m_AutoDrawSubItem; }
            set { m_AutoDrawSubItem = value; }
        }

        private int m_ColumnHeaderHeight = 0;
        [Browsable(false), Description("尾部标题区的高"), Category("布局")]
        public int ColumnHeaderHeight
        {
            get { return m_ColumnHeaderHeight; }
        }

        [Browsable(false), Description("其外框矩形"), Category("布局")]
        public Rectangle FrameRectangle
        {
            get
            {
                return new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            }
        }

        [Browsable(false), Description("是否存在尾部标题区"), Category("布局")]
        public bool HaveEndHeader
        {
            get { return this.View == View.Details && this.EndHeaderRectangle.Width > 0; }
        }

        [Browsable(false), Description("尾部列的矩形框"), Category("布局")]
        public Rectangle EndHeaderRectangle
        {
            get
            {
                int iL = 0;
                foreach (ColumnHeader one in this.Columns)
                {
                    iL += one.Width;
                }
                //
                Rectangle rectangle = this.DisplayRectangle;
                int iW = rectangle.Width - iL;
                switch (this.BorderStyle)
                {
                    case BorderStyle.Fixed3D:
                        return new Rectangle(rectangle.Right - iW + 2, rectangle.Top + 2, iW, this.ColumnHeaderHeight);
                    case BorderStyle.FixedSingle:
                        return new Rectangle(rectangle.Right - iW + 1, rectangle.Top + 1, iW, this.ColumnHeaderHeight);
                    case BorderStyle.None:
                    default:
                        return new Rectangle(rectangle.Right - iW, rectangle.Top, iW, this.ColumnHeaderHeight);
                }
            }
        }

        public void InvalidateItem(ListViewItem item)
        {
            if (item == null) return;
            if (this.AutoDrawSubItem && this.View == View.Details && this.Columns.Count > 0)
            {
                this.Invalidate(new Rectangle(item.Bounds.Left, item.Bounds.Top, this.Columns[0].Width, item.Bounds.Height));
            }
            else
            {
                this.Invalidate(item.Bounds);
            }
        }

        public ListViewItem GetItemAtEx(int x, int y)
        {
            if (!this.AutoDrawSubItem && this.View == View.Details) x = 10;
            //
            return this.GetItemAt(x, y);
        }
        #endregion

        private bool m_IsMouseDown = false;
        private void SetIsMouseDown(bool bValue)
        {
            if (this.m_IsMouseDown == bValue) return;
            //
            this.m_IsMouseDown = bValue;
            //if (this.m_MouseLoctionListViewItem != null) this.Invalidate(this.m_MouseLoctionListViewItem.Bounds);
            this.InvalidateItem(this.m_MouseLoctionListViewItem);
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.SetIsMouseDown(true);
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            this.SetIsMouseDown(false);
            base.OnMouseUp(e);
        }

        private ListViewItem m_MouseLoctionListViewItem = null;
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (!this.m_AutoMouseMoveSeleced) return;
            //
            if (this.m_MouseLoctionListViewItem == null || !this.m_MouseLoctionListViewItem.Bounds.Contains(e.Location))
            {
                ListViewItem item = this.GetItemAtEx(e.Location.X, e.Location.Y);
                if (item != this.m_MouseLoctionListViewItem)
                {
                    //if (this.m_MouseLoctionListViewItem != null) this.Invalidate(this.m_MouseLoctionListViewItem.Bounds);
                    //if (item != null) this.Invalidate(item.Bounds);
                    this.InvalidateItem(this.m_MouseLoctionListViewItem);
                    this.InvalidateItem(item);
                    this.m_MouseLoctionListViewItem = item;
                }
            }
            //
            base.OnMouseMove(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            this.InvalidateItem(this.m_MouseLoctionListViewItem);
            this.m_MouseLoctionListViewItem = null;
            base.OnMouseLeave(e);
        }

        //protected override void OnBeforeLabelEdit(LabelEditEventArgs e)
        //{
        //    base.OnBeforeLabelEdit(e);
        //    //
        //    this.SetIsMouseDown(false);
        //}

        //protected override void OnSelectedIndexChanged(EventArgs e)
        //{
        //    base.OnSelectedIndexChanged(e);
        //    //
        //    this.SetIsMouseDown(false);
        //}

        protected override void OnColumnWidthChanged(ColumnWidthChangedEventArgs e)
        {
            foreach (ListViewItem one in this.SelectedItems)
            {
                this.InvalidateItem(one);
            }
            //
            this.InvalidateItem(this.m_MouseLoctionListViewItem);
            //
            base.OnColumnWidthChanged(e);
        }

        protected override void OnDrawColumnHeader(DrawListViewColumnHeaderEventArgs e)
        {
            if (this.Columns.Count > 0)
            {
                if (e.ColumnIndex == -1)
                {
                    GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderColumnHeaderItem
                            (
                            new ItemRenderEventArgs
                                (
                                e.Graphics,
                                e.Header,
                                this,
                                CheckState.Unchecked,
                                GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal,
                                e.Bounds,
                                e.Bounds,
                                e.Bounds,
                                e.Bounds
                                )
                            );
                }
                else
                {
                    this.m_ColumnHeaderHeight = e.Bounds.Height;
                    //
                    int iImageWidth = 0;
                    Image image = this.SmallImageList != null ? this.GetHeaderSmallImage(e.Header) : null;
                    if (image != null) iImageWidth = CTR_IMAGEGRIPREGIONWIDTH;
                    Rectangle rectangleImageGrip = new Rectangle(e.Bounds.Left + 5, (e.Bounds.Top + e.Bounds.Bottom - iImageWidth) / 2, iImageWidth + CTR_IMAGEGRIPSEPARATORWIDTH, iImageWidth);
                    //
                    GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderColumnHeaderItem
                            (
                            new ItemRenderEventArgs
                                (
                                e.Graphics,
                                e.Header,
                                this,
                                CheckState.Unchecked,
                                GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal,
                                rectangleImageGrip,
                                rectangleImageGrip,
                                e.Bounds,
                                e.Bounds
                                )
                            );
                    //
                    if (image != null)
                    {
                        GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderImage
                           (
                           new GISShare.Controls.WinForm.ImageRenderEventArgs
                               (
                               e.Graphics,
                               this,
                               this.Enabled,
                               image,
                               Util.UtilTX.CreateSquareRectangle(Rectangle.FromLTRB(rectangleImageGrip.Left + this.ImageSpace, rectangleImageGrip.Top + this.ImageSpace, rectangleImageGrip.Right - this.ImageSpace, rectangleImageGrip.Bottom - this.ImageSpace))
                               )
                            );
                    }
                    //
                    int iH = (int)e.Graphics.MeasureString(e.Header.Text, this.Font).Height + 1;
                    Rectangle rectangleText = new Rectangle(rectangleImageGrip.Right, (e.Bounds.Top + e.Bounds.Bottom - iH) / 2 + 2, e.Bounds.Right - rectangleImageGrip.Right, iH);
                    GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderColumnHeaderText
                        (
                        new GISShare.Controls.WinForm.TextRenderEventArgs
                            (
                            e.Graphics,
                            this,
                            this.Enabled,
                            false,
                            e.Header.Text,
                            false,
                            this.ForeColor,
                            this.ForeColor,
                            this.Font,
                            rectangleText
                            )
                        );
                    //
                    if (e.ColumnIndex == (this.Columns.Count - 1))
                    {
                        rectangleText = this.EndHeaderRectangle;
                        if (rectangleText.Width > 0 && rectangleText.Height > 0) this.Invalidate(this.EndHeaderRectangle);
                    }
                }
            }
            //
            base.OnDrawColumnHeader(e);
        }
        private Image GetHeaderSmallImage(ColumnHeader item)//ImageList
        {
            if (item.ImageIndex >= 0 && item.ImageIndex < this.SmallImageList.Images.Count) return this.SmallImageList.Images[item.ImageIndex];
            if (this.SmallImageList.Images.Keys.Contains(item.ImageKey)) return this.SmallImageList.Images[item.ImageKey];
            //
            return null;
        }

        protected override void OnDrawItem(DrawListViewItemEventArgs e)
        {
            if (this.Items.Count > 0)
            {
                WFNew.BaseItemState eBaseItemState = GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal;
                if (this.AutoMouseMoveSeleced && this.ItemBoundsContains(e.Item))
                {
                    eBaseItemState = this.m_IsMouseDown ? GISShare.Controls.WinForm.WFNew.BaseItemState.ePressed : GISShare.Controls.WinForm.WFNew.BaseItemState.eHot;
                }
                //
                switch (this.View)
                {
                    case View.LargeIcon:
                        this.DrawItem_ListViewItem_LI(e, eBaseItemState);
                        break;
                    case View.Tile:
                        this.DrawItem_ListViewItem_T(e, eBaseItemState);
                        break;
                    case View.Details:
                        this.DrawItem_ListViewItem_D(e, eBaseItemState);
                        break;
                    case View.SmallIcon:
                        this.DrawItem_ListViewItem_S(e, eBaseItemState);
                        break;
                    case View.List:
                    default:
                        this.DrawItem_ListViewItem_L(e, eBaseItemState);
                        break;
                }
            }         
            //
            base.OnDrawItem(e);
        }
        private bool ItemBoundsContains(ListViewItem item)
        {
            if (this.AutoDrawSubItem && this.View == View.Details && this.Columns.Count > 0)
            {
                return (new Rectangle(item.Bounds.Left, item.Bounds.Top, this.Columns[0].Width, item.Bounds.Height)).Contains(this.PointToClient(Form.MousePosition));
            }
            else
            {
                return item.Bounds.Contains(this.PointToClient(Form.MousePosition));
            }
        }
        private void DrawItem_ListViewItem_LI(DrawListViewItemEventArgs e, WFNew.BaseItemState eBaseItemState)
        {
            Size size = e.Graphics.MeasureString(e.Item.Text, e.Item.Font == null ? this.Font : e.Item.Font).ToSize();
            size = new Size(size.Width + 1, size.Height + 1);
            //
            Rectangle rectangleLeftGrip = new Rectangle((e.Bounds.Left + e.Bounds.Right - this.LeftGripRegionWidth - CTR_IMAGEGRIPSEPARATORWIDTH - this.ImageGripRegionWidth) / 2, e.Bounds.Y, this.LeftGripRegionWidth, e.Bounds.Height - size.Height);
            Rectangle rectangleImageGrip = new Rectangle(rectangleLeftGrip.Right, e.Bounds.Y, this.ImageGripRegionWidth, rectangleLeftGrip.Height);
            Rectangle rectangleGrip = Rectangle.FromLTRB(e.Bounds.Left, e.Bounds.Top, e.Bounds.Right, rectangleLeftGrip.Bottom);
            //
            GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderListViewItem
                (
                new ItemRenderEventArgs
                    (
                    e.Graphics,
                    e.Item,
                    this,
                    e.Item.Checked ? CheckState.Checked : CheckState.Unchecked,
                    eBaseItemState,
                    rectangleLeftGrip,
                    rectangleGrip,
                    e.Bounds,
                    e.Bounds
                    )
                );
            //
            if (this.LargeImageList != null && this.ImageGripRegionWidth > 1)
            {
                Image image = this.GetItemLargeImage(e.Item);
                if (image != null)
                {
                    GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderImage
                        (
                        new GISShare.Controls.WinForm.ImageRenderEventArgs
                            (
                            e.Graphics,
                            this,
                            this.Enabled,
                            image,
                            Util.UtilTX.CreateSquareRectangle(Rectangle.FromLTRB(rectangleImageGrip.Left + this.ImageSpace, rectangleImageGrip.Top + this.ImageSpace, rectangleImageGrip.Right - this.ImageSpace - CTR_IMAGEGRIPSEPARATORWIDTH, rectangleImageGrip.Bottom - this.ImageSpace))
                            )
                         );
                }
            }
            //
            if (this.StateImageList != null && this.LeftGripRegionWidth > 1)
            {
                Image image = this.GetItemStateImage(e.Item);
                if (image != null)
                {
                    GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderImage
                        (
                        new GISShare.Controls.WinForm.ImageRenderEventArgs
                            (
                            e.Graphics,
                            this,
                            this.Enabled,
                            image,
                            Util.UtilTX.CreateSquareRectangle(Rectangle.FromLTRB(rectangleLeftGrip.Left + this.ImageSpace, rectangleLeftGrip.Top + this.ImageSpace, rectangleLeftGrip.Right - this.ImageSpace, rectangleLeftGrip.Bottom - this.ImageSpace))
                            )
                         );
                }
            }
            //
            Rectangle rectangleText = new Rectangle((e.Bounds.Left + e.Bounds.Right - size.Width) / 2, rectangleImageGrip.Bottom + 1, size.Width, size.Height);
            int iLeft = rectangleText.Left < e.Bounds.Left ? e.Bounds.Left : rectangleText.Left;
            int iTop = rectangleText.Top < e.Bounds.Top ? e.Bounds.Top : rectangleText.Top;
            int iRight = rectangleText.Right > e.Bounds.Right ? e.Bounds.Right : rectangleText.Right;
            int iBottom = rectangleText.Bottom > e.Bounds.Bottom ? e.Bounds.Bottom : rectangleText.Bottom;
            GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderText
                (
                new GISShare.Controls.WinForm.TextRenderEventArgs
                    (
                    e.Graphics,
                    this, 
                    this.Enabled,
                    false,
                    e.Item.Text,
                    false,
                    e.Item.ForeColor.IsEmpty ? this.ForeColor : e.Item.ForeColor,
                    e.Item.ForeColor.IsEmpty ? this.ForeColor : e.Item.ForeColor,
                    e.Item.Font == null ? this.Font : e.Item.Font,
                    Rectangle.FromLTRB(iLeft, iTop, iRight, iBottom)
                    )
                );
        }
        private void DrawItem_ListViewItem_T(DrawListViewItemEventArgs e, WFNew.BaseItemState eBaseItemState)
        {
            Rectangle rectangleLeftGrip = new Rectangle(e.Bounds.X + CTR_IMAGEGRIPSEPARATORWIDTH, e.Bounds.Y, this.LeftGripRegionWidth + CTR_IMAGEGRIPSEPARATORWIDTH, e.Bounds.Height);
            Rectangle rectangleImageGrip = new Rectangle(rectangleLeftGrip.Right, e.Bounds.Y, this.ImageGripRegionWidth + CTR_IMAGEGRIPSEPARATORWIDTH, e.Bounds.Height);
            Rectangle rectangleGrip = Rectangle.FromLTRB(rectangleLeftGrip.Left, e.Bounds.Top, rectangleImageGrip.Right, e.Bounds.Bottom);
            //
            GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderListViewItem
                (
                new ItemRenderEventArgs
                    (
                    e.Graphics,
                    e.Item,
                    this,
                    e.Item.Checked ? CheckState.Checked : CheckState.Unchecked,
                    eBaseItemState,
                    rectangleLeftGrip,
                    rectangleGrip,
                    e.Bounds,
                    e.Bounds
                    )
                );
            //
            if (this.LargeImageList != null && this.ImageGripRegionWidth > 1)
            {
                Image image = this.GetItemLargeImage(e.Item);
                if (image != null)
                {
                    GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderImage
                        (
                        new GISShare.Controls.WinForm.ImageRenderEventArgs
                            (
                            e.Graphics,
                            this,
                            this.Enabled,
                            image,
                            Util.UtilTX.CreateSquareRectangle(Rectangle.FromLTRB(rectangleImageGrip.Left + this.ImageSpace, rectangleImageGrip.Top + this.ImageSpace, rectangleImageGrip.Right - this.ImageSpace - CTR_IMAGEGRIPSEPARATORWIDTH, rectangleImageGrip.Bottom - this.ImageSpace))
                            )
                         );
                }
            }
            //
            if (this.StateImageList != null && this.LeftGripRegionWidth > 1)
            {
                Image image = this.GetItemStateImage(e.Item);
                if (image != null)
                {
                    GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderImage
                        (
                        new GISShare.Controls.WinForm.ImageRenderEventArgs
                            (
                            e.Graphics,
                            this,
                            this.Enabled,
                            image,
                            Util.UtilTX.CreateSquareRectangle(Rectangle.FromLTRB(rectangleLeftGrip.Left + this.ImageSpace, rectangleLeftGrip.Top + this.ImageSpace, rectangleLeftGrip.Right - this.ImageSpace, rectangleLeftGrip.Bottom - this.ImageSpace))
                            )
                         );
                }
            }
            //
            //
            Size size = e.Graphics.MeasureString(e.Item.Text, e.Item.Font == null ? this.Font : e.Item.Font).ToSize();
            size = new Size(size.Width + 1, size.Height + 1);
            if (e.Item.SubItems.Count > 1)
            {
                ListViewItem.ListViewSubItem subItem = e.Item.SubItems[1];
                Size size2 = e.Graphics.MeasureString(subItem.Text, subItem.Font == null ? this.Font : subItem.Font).ToSize();
                size2 = new Size(size2.Width + 1, size2.Height);
                Rectangle rectangleText = new Rectangle(rectangleImageGrip.Right, (e.Bounds.Top + e.Bounds.Bottom - size.Height - size2.Height) / 2 + 2, size.Width, size.Height);
                if (rectangleText.Top < e.Bounds.Top) rectangleText.Y = e.Bounds.Y;
                GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderText
                    (
                    new GISShare.Controls.WinForm.TextRenderEventArgs
                        (
                        e.Graphics,
                        this, 
                        this.Enabled,
                        false,
                        e.Item.Text,
                        false,
                        e.Item.ForeColor.IsEmpty ? this.ForeColor : e.Item.ForeColor,
                        e.Item.ForeColor.IsEmpty ? this.ForeColor : e.Item.ForeColor,
                        e.Item.Font == null ? this.Font : e.Item.Font,
                        rectangleText
                        )
                    );
                //
                GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderText
                    (
                    new GISShare.Controls.WinForm.TextRenderEventArgs
                        (
                        e.Graphics,
                        this, 
                        false,
                        false,
                        subItem.Text,
                        false,
                        subItem.ForeColor.IsEmpty ? this.ForeColor : subItem.ForeColor,
                        subItem.ForeColor.IsEmpty ? this.ForeColor : subItem.ForeColor,
                        subItem.Font == null ? this.Font : subItem.Font,
                        new Rectangle(rectangleText.Left, rectangleText.Bottom, size2.Width, size2.Height)
                        )
                    );
            }
            else
            {
                GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderText
                    (
                    new GISShare.Controls.WinForm.TextRenderEventArgs
                        (
                        e.Graphics,
                        this,
                        this.Enabled,
                        false,
                        e.Item.Text,
                        false,
                        e.Item.ForeColor.IsEmpty ? this.ForeColor : e.Item.ForeColor,
                        e.Item.ForeColor.IsEmpty ? this.ForeColor : e.Item.ForeColor,
                        e.Item.Font == null ? this.Font : e.Item.Font,
                        new Rectangle(rectangleImageGrip.Right, (e.Bounds.Top + e.Bounds.Bottom - size.Height) / 2 + 2, size.Width, size.Height)
                        )
                    );
            }
            //Size size = e.Graphics.MeasureString(e.Item.Text, e.Item.Font == null ? this.Font : e.Item.Font).ToSize();
            //size = new Size(size.Width + 1, size.Height + 1);
            //GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderText
            //    (
            //    new GISShare.Controls.WinForm.TextRenderEventArgs
            //        (
            //        e.Graphics,
            //        this,
            //        false,
            //        e.Item.Text,
            //        e.Item.ForeColor.IsEmpty ? this.ForeColor : e.Item.ForeColor,
            //        e.Item.Font == null ? this.Font : e.Item.Font,
            //        new Rectangle(rectangleImageGrip.Right, (e.Bounds.Top + e.Bounds.Bottom - size.Height) / 2 + 2, size.Width, size.Height)
            //        )
            //    );
        }
        private void DrawItem_ListViewItem_D(DrawListViewItemEventArgs e, WFNew.BaseItemState eBaseItemState)
        {
            Rectangle rectangleBounds = e.Bounds;
            if (this.Columns.Count > 0)
            {
                rectangleBounds = new Rectangle(rectangleBounds.Left, rectangleBounds.Top, this.Columns[0].Width, rectangleBounds.Height);
            }
            //
            Rectangle rectangleLeftGrip = new Rectangle(rectangleBounds.X + CTR_IMAGEGRIPSEPARATORWIDTH, rectangleBounds.Y, this.LeftGripRegionWidth + CTR_IMAGEGRIPSEPARATORWIDTH, rectangleBounds.Height);
            Rectangle rectangleImageGrip = new Rectangle(rectangleLeftGrip.Right, rectangleBounds.Y, this.ImageGripRegionWidth + CTR_IMAGEGRIPSEPARATORWIDTH, rectangleBounds.Height);
            Rectangle rectangleGrip = Rectangle.FromLTRB(rectangleLeftGrip.Left, rectangleBounds.Top, rectangleImageGrip.Right, rectangleBounds.Bottom);
            //
            GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderListViewItem
                (
                new ItemRenderEventArgs
                    (
                    e.Graphics,
                    e.Item,
                    this,
                    e.Item.Checked ? CheckState.Checked : CheckState.Unchecked,
                    eBaseItemState,
                    rectangleLeftGrip,
                    rectangleGrip,
                    this.AutoDrawSubItem ? rectangleBounds : e.Bounds,
                    this.AutoDrawSubItem ? rectangleBounds : e.Bounds
                    )
                );
            //
            if (this.SmallImageList != null && this.ImageGripRegionWidth > 1)
            {
                Image image = this.GetItemSmallImage(e.Item);
                if (image != null)
                {
                    GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderImage
                        (
                        new GISShare.Controls.WinForm.ImageRenderEventArgs
                            (
                            e.Graphics,
                            this,
                            this.Enabled,
                            image,
                            Util.UtilTX.CreateSquareRectangle(Rectangle.FromLTRB(rectangleImageGrip.Left + this.ImageSpace, rectangleImageGrip.Top + this.ImageSpace, rectangleImageGrip.Right - this.ImageSpace - CTR_IMAGEGRIPSEPARATORWIDTH, rectangleImageGrip.Bottom - this.ImageSpace))
                            )
                         );
                }
            }
            //
            if (this.StateImageList != null && this.LeftGripRegionWidth > 1)
            {
                Image image = this.GetItemStateImage(e.Item);
                if (image != null)
                {
                    GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderImage
                        (
                        new GISShare.Controls.WinForm.ImageRenderEventArgs
                            (
                            e.Graphics,
                            this,
                            this.Enabled,
                            image,
                            Util.UtilTX.CreateSquareRectangle(Rectangle.FromLTRB(rectangleLeftGrip.Left + this.ImageSpace, rectangleLeftGrip.Top + this.ImageSpace, rectangleLeftGrip.Right - this.ImageSpace, rectangleLeftGrip.Bottom - this.ImageSpace))
                            )
                         );
                }
            }
            //
            Size size = e.Graphics.MeasureString(e.Item.Text, e.Item.Font == null ? this.Font : e.Item.Font).ToSize();
            size = new Size(size.Width + 1, size.Height + 1);
            Rectangle rectangleText = new Rectangle(rectangleImageGrip.Right, (rectangleBounds.Top + rectangleBounds.Bottom - size.Height) / 2 + 2, size.Width, size.Height);
            int iLeft = rectangleText.Left < rectangleBounds.Left ? rectangleBounds.Left : rectangleText.Left;
            int iTop = rectangleText.Top < rectangleBounds.Top ? rectangleBounds.Top : rectangleText.Top;
            int iRight = rectangleText.Right > rectangleBounds.Right ? rectangleBounds.Right : rectangleText.Right;
            int iBottom = rectangleText.Bottom > rectangleBounds.Bottom ? rectangleBounds.Bottom : rectangleText.Bottom;
            GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderText
                (
                new GISShare.Controls.WinForm.TextRenderEventArgs
                    (
                    e.Graphics,
                    this,
                    this.Enabled,
                    false,
                    e.Item.Text,
                        false,
                        e.Item.ForeColor.IsEmpty ? this.ForeColor : e.Item.ForeColor,
                        e.Item.ForeColor.IsEmpty ? this.ForeColor : e.Item.ForeColor,
                    e.Item.Font == null ? this.Font : e.Item.Font,
                    Rectangle.FromLTRB(iLeft, iTop, iRight, iBottom)
                    )
                );
        }
        private void DrawItem_ListViewItem_S(DrawListViewItemEventArgs e, WFNew.BaseItemState eBaseItemState)
        {
            Rectangle rectangleLeftGrip = new Rectangle(e.Bounds.X, e.Bounds.Y, this.LeftGripRegionWidth, e.Bounds.Height);
            Rectangle rectangleImageGrip = new Rectangle(rectangleLeftGrip.Right - CTR_IMAGEGRIPSEPARATORWIDTH, e.Bounds.Y, this.ImageGripRegionWidth, e.Bounds.Height);
            Rectangle rectangleGrip = Rectangle.FromLTRB(rectangleLeftGrip.Left, e.Bounds.Top, rectangleImageGrip.Right, e.Bounds.Bottom);
            //
            GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderListViewItem
                (
                new ItemRenderEventArgs
                    (
                    e.Graphics,
                    e.Item,
                    this,
                    e.Item.Checked ? CheckState.Checked : CheckState.Unchecked,
                    eBaseItemState,
                    rectangleLeftGrip,
                    rectangleGrip,
                    e.Bounds,
                    e.Bounds
                    )
                );
            //
            if (this.SmallImageList != null && this.ImageGripRegionWidth > 1)
            {
                Image image = this.GetItemSmallImage(e.Item);
                if (image != null)
                {
                    GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderImage
                        (
                        new GISShare.Controls.WinForm.ImageRenderEventArgs
                            (
                            e.Graphics,
                            this,
                            this.Enabled,
                            image,
                            Util.UtilTX.CreateSquareRectangle(Rectangle.FromLTRB(rectangleImageGrip.Left + this.ImageSpace, rectangleImageGrip.Top + this.ImageSpace, rectangleImageGrip.Right - this.ImageSpace - CTR_IMAGEGRIPSEPARATORWIDTH, rectangleImageGrip.Bottom - this.ImageSpace))
                            )
                         );
                }
            }
            //
            if (this.StateImageList != null && this.LeftGripRegionWidth > 1)
            {
                Image image = this.GetItemStateImage(e.Item);
                if (image != null)
                {
                    GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderImage
                        (
                        new GISShare.Controls.WinForm.ImageRenderEventArgs
                            (
                            e.Graphics,
                            this,
                            this.Enabled,
                            image,
                            Util.UtilTX.CreateSquareRectangle(Rectangle.FromLTRB(rectangleLeftGrip.Left + this.ImageSpace, rectangleLeftGrip.Top + this.ImageSpace, rectangleLeftGrip.Right - this.ImageSpace, rectangleLeftGrip.Bottom - this.ImageSpace))
                            )
                         );
                }
            }
            //
            Size size = e.Graphics.MeasureString(e.Item.Text, e.Item.Font == null ? this.Font : e.Item.Font).ToSize();
            size = new Size(size.Width + 1, size.Height + 1);
            GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderText
                (
                new GISShare.Controls.WinForm.TextRenderEventArgs
                    (
                    e.Graphics,
                    this, 
                    this.Enabled,
                    false,
                    e.Item.Text,
                        false,
                        e.Item.ForeColor.IsEmpty ? this.ForeColor : e.Item.ForeColor,
                        e.Item.ForeColor.IsEmpty ? this.ForeColor : e.Item.ForeColor,
                    e.Item.Font == null ? this.Font : e.Item.Font,
                    new Rectangle(rectangleImageGrip.Right, (e.Bounds.Top + e.Bounds.Bottom - size.Height) / 2 + 2, size.Width, size.Height)
                    )
                );
        }
        private void DrawItem_ListViewItem_L(DrawListViewItemEventArgs e, WFNew.BaseItemState eBaseItemState)
        {
            Rectangle rectangleLeftGrip = new Rectangle(e.Bounds.X, e.Bounds.Y, this.LeftGripRegionWidth, e.Bounds.Height);
            Rectangle rectangleImageGrip = new Rectangle(rectangleLeftGrip.Right, e.Bounds.Y, this.ImageGripRegionWidth + CTR_IMAGEGRIPSEPARATORWIDTH, e.Bounds.Height);
            Rectangle rectangleGrip = Rectangle.FromLTRB(rectangleLeftGrip.Left, e.Bounds.Top, rectangleImageGrip.Right, e.Bounds.Bottom);
            //
            GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderListViewItem
                (
                new ItemRenderEventArgs
                    (
                    e.Graphics,
                    e.Item,
                    this,
                    e.Item.Checked ? CheckState.Checked : CheckState.Unchecked,
                    eBaseItemState,
                    rectangleLeftGrip,
                    rectangleGrip,
                    e.Bounds,
                    e.Bounds
                    )
                );
            //
            if (this.SmallImageList != null && this.ImageGripRegionWidth > 1)
            {
                Image image = this.GetItemSmallImage(e.Item);
                if (image != null)
                {
                    GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderImage
                        (
                        new GISShare.Controls.WinForm.ImageRenderEventArgs
                            (
                            e.Graphics,
                            this,
                            this.Enabled,
                            image,
                            Util.UtilTX.CreateSquareRectangle(Rectangle.FromLTRB(rectangleImageGrip.Left + this.ImageSpace, rectangleImageGrip.Top + this.ImageSpace, rectangleImageGrip.Right - this.ImageSpace - CTR_IMAGEGRIPSEPARATORWIDTH, rectangleImageGrip.Bottom - this.ImageSpace))
                            )
                         );
                }
            }
            //
            if (this.StateImageList != null && this.LeftGripRegionWidth > 1)
            {
                Image image = this.GetItemStateImage(e.Item);
                if (image != null)
                {
                    GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderImage
                        (
                        new GISShare.Controls.WinForm.ImageRenderEventArgs
                            (
                            e.Graphics,
                            this,
                            this.Enabled,
                            image,
                            Util.UtilTX.CreateSquareRectangle(Rectangle.FromLTRB(rectangleLeftGrip.Left + this.ImageSpace, rectangleLeftGrip.Top + this.ImageSpace, rectangleLeftGrip.Right - this.ImageSpace, rectangleLeftGrip.Bottom - this.ImageSpace))
                            )
                         );
                }
            }
            //
            Size size = e.Graphics.MeasureString(e.Item.Text, e.Item.Font == null ? this.Font : e.Item.Font).ToSize();
            size = new Size(size.Width + 1, size.Height + 1);
            GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderText
                (
                new GISShare.Controls.WinForm.TextRenderEventArgs
                    (
                    e.Graphics,
                    this,
                    this.Enabled,
                    false,
                    e.Item.Text,
                        false,
                        e.Item.ForeColor.IsEmpty ? this.ForeColor : e.Item.ForeColor,
                        e.Item.ForeColor.IsEmpty ? this.ForeColor : e.Item.ForeColor,
                    e.Item.Font == null ? this.Font : e.Item.Font,
                    new Rectangle(rectangleImageGrip.Right, (e.Bounds.Top + e.Bounds.Bottom - size.Height) / 2 + 2, size.Width, size.Height)
                    )
                );
        }
        private Image GetItemSmallImage(ListViewItem item)//ImageList
        {
            if (item.ImageIndex >= 0 && item.ImageIndex < this.SmallImageList.Images.Count) return this.SmallImageList.Images[item.ImageIndex];
            if (this.SmallImageList.Images.Keys.Contains(item.ImageKey)) return this.SmallImageList.Images[item.ImageKey];
            //
            return null;
        }
        private Image GetItemLargeImage(ListViewItem item)
        {
            if (item.ImageIndex >= 0 && item.ImageIndex < this.LargeImageList.Images.Count) return this.LargeImageList.Images[item.ImageIndex];
            if (this.LargeImageList.Images.Keys.Contains(item.ImageKey)) return this.LargeImageList.Images[item.ImageKey];
            //
            return null;
        }
        private Image GetItemStateImage(ListViewItem item)
        {
            if (item.StateImageIndex >= 0 && item.StateImageIndex < this.StateImageList.Images.Count) return this.StateImageList.Images[item.StateImageIndex];
            if (this.StateImageList.Images.Keys.Contains(item.ImageKey)) return this.StateImageList.Images[item.ImageKey];
            //
            return null;
        }

        protected override void OnDrawSubItem(DrawListViewSubItemEventArgs e)
        {
            if (e.Item.SubItems.IndexOf(e.SubItem) > 0)
            {
                if (this.AutoDrawSubItem)
                {
                    WFNew.BaseItemState eBaseItemState = GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal;
                    if (this.AutoMouseMoveSeleced && e.SubItem.Bounds.Contains(this.PointToClient(Form.MousePosition)))
                    {
                        eBaseItemState = this.m_IsMouseDown ? GISShare.Controls.WinForm.WFNew.BaseItemState.ePressed : GISShare.Controls.WinForm.WFNew.BaseItemState.eHot;
                    }
                    //
                    GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderListViewSubItem
                        (
                        new ItemRenderEventArgs
                            (
                            e.Graphics,
                            e.SubItem,
                            this,
                            CheckState.Unchecked,
                            eBaseItemState,
                            e.SubItem.Bounds,
                            e.SubItem.Bounds,
                            e.SubItem.Bounds,
                            e.SubItem.Bounds
                            )
                        );
                }
                //
                int iH = (int)e.Graphics.MeasureString(e.SubItem.Text, e.SubItem.Font == null ? this.Font : e.SubItem.Font).Height;
                GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderText
                    (
                    new GISShare.Controls.WinForm.TextRenderEventArgs
                        (
                        e.Graphics,
                        this,
                        this.Enabled,
                        false,
                        e.SubItem.Text,
                        false,
                        e.SubItem.ForeColor.IsEmpty ? this.ForeColor : e.SubItem.ForeColor,
                        e.SubItem.ForeColor.IsEmpty ? this.ForeColor : e.SubItem.ForeColor,
                        e.SubItem.Font == null ? this.Font : e.SubItem.Font,
                        new Rectangle(e.SubItem.Bounds.Left, (e.SubItem.Bounds.Top + e.SubItem.Bounds.Bottom - iH) / 2 + 2, e.SubItem.Bounds.Width, iH)
                        )
                    );
            }
            //
            base.OnDrawSubItem(e);
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case (int)GISShare.Win32.Msgs.WM_PAINT:
                    //System.Diagnostics.Debug.WriteLine(String.Format("{0}----{1}", this.Name, "WM_PAINT"));
                    base.WndProc(ref m);
                    this.WmPaintHeader(ref m);
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
        private void WmPaintHeader(ref Message m)
        {
            if (!this.HaveEndHeader) return;
            //
            IntPtr iHandle = GISShare.Win32.API.GetWindowDC(m.HWnd);
            try
            {
                Graphics g = Graphics.FromHdc(iHandle);
                //
                Rectangle rectangle = this.EndHeaderRectangle;
                if (m_BufferedGraphics == null || this.m_CurrentCacheSize != rectangle.Size)
                {
                    if (m_BufferedGraphics != null) m_BufferedGraphics.Dispose();
                    m_BufferedGraphics = this.m_BufferedGraphicsContext.Allocate(g, rectangle);
                    this.m_CurrentCacheSize = rectangle.Size;
                }
                //
                this.OnPaintHeader(new PaintEventArgs(m_BufferedGraphics.Graphics, rectangle));
                //
                if (m_BufferedGraphics != null) m_BufferedGraphics.Render(g);
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
            GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderListViewNC
                (
                new ObjectRenderEventArgs(e.Graphics, this, this.DisplayRectangle)
                );
        }

        protected virtual void OnPaintHeader(PaintEventArgs e)
        {
            this.OnDrawColumnHeader(new DrawListViewColumnHeaderEventArgs(e.Graphics, e.ClipRectangle, -1, null, ListViewItemStates.Default, this.ForeColor, this.BackColor, this.Font));
        }

    }
}
