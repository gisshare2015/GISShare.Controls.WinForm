using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;
using System.ComponentModel;

namespace GISShare.Controls.WinForm
{
    public class TreeViewX : TreeView, ITreeViewX
    {
        private const int CTR_COLORREGIONWIDTH = 28;
        private const int CTR_PLUSMINUSREGIONWIDTH = 19;
        private const int CTR_LEFTGRIPREGIONWIDTH = 16;
        private const int CTR_IMAGEGRIPREGIONWIDTH = 18;
        private const int CTR_IMAGEGRIPSEPARATORWIDTH = 1;

        public TreeViewX()
        {
            DrawMode = TreeViewDrawMode.OwnerDrawAll;
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
                return this.ImageList == null ? 0 : CTR_IMAGEGRIPREGIONWIDTH; //this.ImageList != null ? this.ItemHeight : 0;
            }
        }

        [Browsable(false), Description("最左端绘制区所需的宽度"), Category("布局")]
        public int LeftGripRegionWidth
        {
            get
            {
                return (this.StateImageList != null || this.CheckBoxes) ? CTR_LEFTGRIPREGIONWIDTH : 0;
                //if (this.StateImageList != null || this.CheckBoxes) 
                //{
                //    if (this.ImageList != null) return this.ItemHeight;
                //    else return CTR_LEFTGRIPREGIONWIDTH;
                //}
                //else return 0;
            }
        }

        [Browsable(false), Description("记录它的绘制状态"), Category("外观")]
        public WinForm.ItemDrawStyle eItemDrawStyle
        {
            get
            {
                if (this.StateImageList != null) return GISShare.Controls.WinForm.ItemDrawStyle.eImageLabel;
                else if (this.CheckBoxes) return GISShare.Controls.WinForm.ItemDrawStyle.eCheckBox;
                return GISShare.Controls.WinForm.ItemDrawStyle.eSimply;
            }
        }
        #endregion

        #region ITreeViewX
        int m_ImageSpace = 1;
        [Browsable(true), DefaultValue(1), Description("图片四周的间距"), Category("布局")]
        public int ImageSpace
        {
            get { return m_ImageSpace; }
            set { m_ImageSpace = value; }
        }

        NodeRegionStyle m_eNodeRegionStyle = NodeRegionStyle.ePlusMinusToRight;
        [Browsable(true), DefaultValue(typeof(WinForm.ItemDrawStyle), "ePlusMinusToRight"), Description("节点区的有效范围类型"), Category("外观")]
        public NodeRegionStyle eNodeRegionStyle
        {
            get { return m_eNodeRegionStyle; }
            set { m_eNodeRegionStyle = value; }
        }

        [Browsable(false), Description("加减区所需的宽度"), Category("布局")]
        public int PlusMinusRegionWidth
        {
            get 
            {
                return (this.ShowLines || this.ShowPlusMinus) ? CTR_PLUSMINUSREGIONWIDTH : 0;
            }
        }

        bool m_AutoMouseMoveSeleced = false;
        [Browsable(true), DefaultValue(false), Description("鼠标移动遂即选中所在项"), Category("状态")]
        public bool AutoMouseMoveSeleced
        {
            get { return m_AutoMouseMoveSeleced; }
            set { m_AutoMouseMoveSeleced = value; }
        }

        [Browsable(false), Description("其外框矩形"), Category("布局")]
        public Rectangle FrameRectangle
        {
            get
            {
                return new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            }
        }
        #endregion

        [Browsable(true),
        Editor(typeof(GISShare.Controls.WinForm.TreeNodeCollectionEditor), typeof(System.Drawing.Design.UITypeEditor)),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
        Description("其所携带的结点集合"),
        Category("结点")]
        public new TreeNodeCollection Nodes
        {
            get { return base.Nodes; }
        }

        private bool m_IsMouseDown = false;
        private void SetIsMouseDown(bool bValue)
        {
            if (this.m_IsMouseDown == bValue) return;
            //
            this.m_IsMouseDown = bValue;
            if (this.m_MouseLoctionNode != null) this.Invalidate(this.GetNodeRectangle(this.m_MouseLoctionNode));
        }
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case (int)GISShare.Win32.Msgs.WM_LBUTTONDOWN:
                    //System.Diagnostics.Debug.WriteLine(String.Format("{0}----{1}", this.Name, "WM_LBUTTONDOWN"));
                    this.SetIsMouseDown(true);
                    break;
                case (int)GISShare.Win32.Msgs.WM_LBUTTONUP:
                    //System.Diagnostics.Debug.WriteLine(String.Format("{0}----{1}", this.Name, "WM_LBUTTONUP"));
                    this.SetIsMouseDown(false);
                    break;
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
            GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderTreeViewNC
                (
                new ObjectRenderEventArgs(e.Graphics, this, this.DisplayRectangle)
                );
        }

        protected override void OnBeforeSelect(TreeViewCancelEventArgs e)
        {
            base.OnBeforeSelect(e);
            //
            this.SetIsMouseDown(false);
        }

        protected override void OnBeforeLabelEdit(NodeLabelEditEventArgs e)
        {
            base.OnBeforeLabelEdit(e);
            //
            this.SetIsMouseDown(false);
        }

        private TreeNode m_MouseLoctionNode = null;
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (this.AutoMouseMoveSeleced)
            {
                if (this.m_MouseLoctionNode == null ||
                    e.Y < this.m_MouseLoctionNode.Bounds.Top || e.Y > this.m_MouseLoctionNode.Bounds.Bottom)
                {
                    TreeNode node = this.GetNodeAt(e.Location.X, e.Location.Y);
                    if (node != this.m_MouseLoctionNode)
                    {
                        if (this.m_MouseLoctionNode != null) this.Invalidate(this.GetNodeRectangle(this.m_MouseLoctionNode));
                        if (node != null) this.Invalidate(this.GetNodeRectangle(node));
                        this.m_MouseLoctionNode = node;
                    }
                }
            }
            //
            base.OnMouseMove(e);
        }
        private Rectangle GetNodeRectangle(TreeNode node)
        {
            switch (this.eNodeRegionStyle) 
            {
                case NodeRegionStyle.eText:
                    //return node.Bounds;
                case NodeRegionStyle.eTextToRight:
                    return Rectangle.FromLTRB(node.Bounds.Left, node.Bounds.Top, this.DisplayRectangle.Right, node.Bounds.Bottom);
                default:
                    return Rectangle.FromLTRB(this.DisplayRectangle.Left, node.Bounds.Top, this.DisplayRectangle.Right, node.Bounds.Bottom);
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (this.AutoMouseMoveSeleced)
            {
                if (this.m_MouseLoctionNode != null) this.Invalidate(this.GetNodeRectangle(this.m_MouseLoctionNode));
                this.m_MouseLoctionNode = null;
            }
            //
            base.OnMouseLeave(e);
        }

        protected override void OnDrawNode(DrawTreeNodeEventArgs e)
        {
            if (this.Nodes.Count > 0)
            {
                //
                WFNew.BaseItemState eBaseItemState = GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal;
                if (this.AutoMouseMoveSeleced && e.Node == this.GetNodeAt(this.PointToClient(Form.MousePosition)))
                {
                    eBaseItemState = this.m_IsMouseDown || e.Node.IsEditing ? WFNew.BaseItemState.ePressed : GISShare.Controls.WinForm.WFNew.BaseItemState.eHot;
                }
                //
                this.DrawItem_TreeNodeItem(e, eBaseItemState);
            }
            //
            base.OnDrawNode(e);
        }
        private bool DrawItem_TreeNodeItem(DrawTreeNodeEventArgs e, WFNew.BaseItemState eBaseItemState)
        {
            Size size = e.Graphics.MeasureString(e.Node.Text, e.Node.NodeFont == null ? this.Font : e.Node.NodeFont).ToSize();
            size = new Size(size.Width + 1, size.Height + 1);
            //
            Rectangle rectangleDispaly = this.DisplayRectangle;
            Rectangle rectangleImageGrip = Rectangle.FromLTRB(e.Node.Bounds.Left - this.ImageGripRegionWidth, e.Node.Bounds.Top, e.Node.Bounds.Left, e.Node.Bounds.Bottom);
            Rectangle rectangleLeftGrip = Rectangle.FromLTRB(rectangleImageGrip.Left - LeftGripRegionWidth - CTR_IMAGEGRIPSEPARATORWIDTH, e.Bounds.Top, rectangleImageGrip.Left - CTR_IMAGEGRIPSEPARATORWIDTH, e.Bounds.Bottom);
            Rectangle rectangleNodeBounds = Rectangle.FromLTRB(e.Node.Bounds.Left, e.Bounds.Top, rectangleDispaly.Right, e.Bounds.Bottom);
            switch (this.eNodeRegionStyle) 
            {
                case NodeRegionStyle.eRow:
                    rectangleNodeBounds = Rectangle.FromLTRB(rectangleDispaly.Left, e.Bounds.Top, rectangleDispaly.Right, e.Bounds.Bottom);
                    break;
                case NodeRegionStyle.ePlusMinusToRight:
                    int iL = rectangleLeftGrip.Left - this.PlusMinusRegionWidth;
                    rectangleNodeBounds = Rectangle.FromLTRB(iL < 0 ? rectangleDispaly.Left : iL, e.Bounds.Top, rectangleDispaly.Right, e.Bounds.Bottom);
                    break;
                case NodeRegionStyle.eGripToRight:
                    rectangleNodeBounds = Rectangle.FromLTRB(rectangleLeftGrip.Left < 0 ? rectangleDispaly.Left : rectangleLeftGrip.Left, e.Bounds.Top, rectangleDispaly.Right, e.Bounds.Bottom);
                    break;
                case NodeRegionStyle.eTextToRight:
                    rectangleNodeBounds = Rectangle.FromLTRB(e.Node.Bounds.Left, e.Bounds.Top, rectangleDispaly.Right, e.Bounds.Bottom);
                    break;
                case NodeRegionStyle.eText:
                    rectangleNodeBounds = new Rectangle(e.Node.Bounds.Left, e.Bounds.Top, size.Width, e.Bounds.Height);// e.Node.Bounds;
                    break;
                default:
                    rectangleNodeBounds = Rectangle.FromLTRB(e.Node.Bounds.Left, e.Bounds.Top, rectangleDispaly.Right, e.Bounds.Bottom);
                    break;
            }
            GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderTreeNodeItem
                    (
                    new TreeNodeItemRenderEventArgs
                        (
                        e.Graphics,
                        e.Node,
                        this,
                        e.Node.Checked ? CheckState.Checked : CheckState.Unchecked,
                        eBaseItemState,
                        Rectangle.FromLTRB(rectangleLeftGrip.Left - this.PlusMinusRegionWidth, e.Bounds.Top, rectangleLeftGrip.Left, e.Bounds.Bottom),
                        rectangleLeftGrip,
                        Rectangle.FromLTRB(rectangleDispaly.Left, e.Bounds.Top, rectangleImageGrip.Right, e.Bounds.Bottom),
                        rectangleNodeBounds,
                        Rectangle.FromLTRB(rectangleDispaly.Left, e.Bounds.Top, rectangleDispaly.Right, e.Bounds.Bottom)
                        )
                    );
            //
            if (this.ImageList != null && this.ImageGripRegionWidth > 1)
            {
                Image image = null;
                if (this.SelectedNode == e.Node) image = this.GetNodeSelectedImage(e.Node);
                else image = this.GetNodeImage(e.Node);
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
                Image image = this.GetNodeStateImage(e.Node);
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
            GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderText
                (
                new GISShare.Controls.WinForm.TextRenderEventArgs
                    (
                    e.Graphics,
                    this,
                    this.Enabled,
                    false,
                    e.Node.Text,
                    e.Node.ForeColor.IsEmpty ? this.ForeColor : e.Node.ForeColor,
                    e.Node.NodeFont == null ? this.Font : e.Node.NodeFont,
                    new Rectangle(e.Node.Bounds.Left, (e.Bounds.Top + e.Bounds.Bottom - size.Height) / 2 + 2, size.Width, size.Height)
                    )
                );
            //
            return true;
        }
        private Image GetNodeImage(TreeNode node)
        {
            if (node.ImageIndex >= 0 && node.ImageIndex < this.ImageList.Images.Count) return this.ImageList.Images[node.ImageIndex];
            if (this.ImageList.Images.Keys.Contains(node.ImageKey)) return this.ImageList.Images[node.ImageKey];
            //
            return null;
        }
        private Image GetNodeStateImage(TreeNode node)
        {
            if (node.StateImageIndex >= 0 && node.StateImageIndex < this.StateImageList.Images.Count) return this.StateImageList.Images[node.StateImageIndex];
            if (this.StateImageList.Images.Keys.Contains(node.ImageKey)) return this.StateImageList.Images[node.ImageKey];
            //
            return null;
        }
        private Image GetNodeSelectedImage(TreeNode node)
        {
            if (node.SelectedImageIndex >= 0 && node.SelectedImageIndex < this.ImageList.Images.Count) return this.ImageList.Images[node.SelectedImageIndex];
            if (this.ImageList.Images.Keys.Contains(node.SelectedImageKey)) return this.ImageList.Images[node.SelectedImageKey];
            //
            return this.GetNodeImage(node);
        }

    }
}
