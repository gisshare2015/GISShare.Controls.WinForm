using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace GISShare.Controls.WinForm.WFNew
{
    [Designer(typeof(GISShare.Controls.WinForm.WFNew.Design.SplitPanelDesigner))]
    public class SplitPanel : WFNew.AreaControl, GISShare.Controls.WinForm.WFNew.ISplitPanel
    {
        private Size m_OutSize;                      //外部最小矩形框      <不要对其直接赋值>
        private int m_InternalMinWidth = 25;         //内部最小尺寸        <不要对其直接赋值>
        private int m_OuterMinWidth = 100;           //外部最小尺寸        <不要对其直接赋值>
        private int m_SplitLineWidth = 4;            //分割线宽度          <不要对其直接赋值>
        private bool m_bIsMouseDown = false;         //记录鼠标按下状态
        private SplitLineForm m_SplitLineForm = null;//分割线窗体

        public SplitPanel()
            : base()
        {
            base.Name = "SplitPanel";
            base.Dock = DockStyle.Left;
            base.Size = new Size(this.OuterMinWidth, this.OuterMinWidth);
        }

        [Browsable(false), Description("外部预留的最小的尺寸"), Category("布局")]
        public Size OutSize
        {
            get { return m_OutSize; }
            internal set { m_OutSize = value; }
        }

        #region WFNew.IBaseItem
        public override object Clone()
        {
            SplitPanel baseItem = new SplitPanel();
            baseItem.InternalMinWidth = this.InternalMinWidth;
            baseItem.OuterMinWidth = this.OuterMinWidth;
            baseItem.SplitLineWidth = this.SplitLineWidth;
            return baseItem;
        }

        public override bool LockHeight
        {
            get { return false; }
        }

        public override bool LockWith
        {
            get { return false; }
        }
        #endregion

        #region ISplitPanel
        [Browsable(false), Description("面板矩形框"), Category("布局")]
        public Rectangle PanelRectangle
        {
            get { return new Rectangle(0, 0, this.Width, this.Height); }
        }

        [Browsable(false), Description("分割区矩用户区形框（x、y 为用户区坐标）"), Category("布局")]
        public virtual Rectangle SplitterRectangle//分割区矩用户区形框（x、y 为用户区坐标）
        {
            get
            {
                switch (this.SplitPanelDock)
                {
                    case DockStyle.Top:
                        return new Rectangle(new Point(base.DisplayRectangle.X, base.DisplayRectangle.Height - this.SplitLineWidth), new Size(base.DisplayRectangle.Width, this.SplitLineWidth));
                    case DockStyle.Left:
                        return new Rectangle(new Point(base.DisplayRectangle.Width - this.SplitLineWidth, base.DisplayRectangle.Y), new Size(this.SplitLineWidth, base.DisplayRectangle.Height));
                    case DockStyle.Right:
                        return new Rectangle(new Point(base.DisplayRectangle.X, base.DisplayRectangle.Y), new Size(this.SplitLineWidth, base.DisplayRectangle.Height));
                    case DockStyle.Bottom:
                        return new Rectangle(new Point(base.DisplayRectangle.X, 0), new Size(base.DisplayRectangle.Width, this.SplitLineWidth));
                    case DockStyle.Fill:
                    default:
                        return new Rectangle(0, 0, 0, 0);
                }
            }
        }

        [Browsable(false), Description("分割区屏幕区矩形框（x、y 为屏幕坐标）"), Category("布局")]
        public virtual Rectangle SplitterScreenRectangle//分割区屏幕区矩形框（x、y 为屏幕坐标）
        {
            get
            {
                switch (this.SplitPanelDock)
                {
                    case DockStyle.Top:
                        return new Rectangle(PointToScreen(new Point(base.DisplayRectangle.X, base.DisplayRectangle.Height - this.SplitLineWidth)), new Size(base.DisplayRectangle.Width, this.SplitLineWidth));
                    case DockStyle.Left:
                        return new Rectangle(PointToScreen(new Point(base.DisplayRectangle.Width - this.SplitLineWidth, base.DisplayRectangle.Y)), new Size(this.SplitLineWidth, base.DisplayRectangle.Height));
                    case DockStyle.Right:
                        return new Rectangle(PointToScreen(new Point(base.DisplayRectangle.X, base.DisplayRectangle.Y)), new Size(this.SplitLineWidth, base.DisplayRectangle.Height));
                    case DockStyle.Bottom:
                        return new Rectangle(PointToScreen(new Point(base.DisplayRectangle.X, 0)), new Size(base.DisplayRectangle.Width, this.SplitLineWidth));
                    case DockStyle.Fill:
                    default:
                        return new Rectangle(0, 0, 0, 0);
                }
            }
        }
        #endregion

        #region ISplitPanel2
        [Browsable(false), Description("内部最小尺寸"), Category("布局")]
        public int InternalMinWidth
        {
            get
            {
                if (m_InternalMinWidth < this.SplitLineWidth) return this.SplitLineWidth;
                return m_InternalMinWidth;
            }
            set
            {
                if (value < this.SplitLineWidth) { GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show("InternalMinWidth不能小于SplitLineWidth"); return; }
                m_InternalMinWidth = value;
            }
        }

        [Browsable(false), Description("外部最小尺寸"), Category("布局")]
        public int OuterMinWidth
        {
            get
            {
                if (m_OuterMinWidth < 0) return 0;
                return m_OuterMinWidth;
            }
            set
            {
                if (value < 0) { GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show("OuterMinWidth不能小于0"); return; }
                m_OuterMinWidth = value;
            }
        }

        [Browsable(true), DefaultValue(4), Description("分割区尺寸"), Category("布局")]
        public int SplitLineWidth
        {
            get
            {
                if (m_SplitLineWidth < 1) return 1;
                return m_SplitLineWidth;
            }
            set
            {
                if (value < 1) { GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show("SplitLineWidth不能小于1"); return; }
                m_SplitLineWidth = value;
            }
        }

        [Browsable(false), DefaultValue(DockStyle.Left), Description("记录面板的停靠方式（依托于Dock属性，请不要赋DockStyle.None值）"), Category("布局")]
        public virtual DockStyle SplitPanelDock//SplitPanelDock记录面板的停靠方式（依托于Dock属性），请不要赋DockStyle.None值
        {
            get { return this.Dock; }
            set
            {
                if (value == DockStyle.None) return;
                this.Dock = value;
            }
        }
        #endregion

        [Browsable(false)]
        public override Rectangle DisplayRectangle//用户区矩形框
        {
            get
            {
                switch (this.SplitPanelDock)
                {
                    case DockStyle.Top:
                        return new Rectangle(base.DisplayRectangle.X, base.DisplayRectangle.Y, base.DisplayRectangle.Width, base.DisplayRectangle.Height - this.SplitLineWidth);
                    case DockStyle.Left:
                        return new Rectangle(base.DisplayRectangle.X, base.DisplayRectangle.Y, base.DisplayRectangle.Width - this.SplitLineWidth, base.DisplayRectangle.Height);
                    case DockStyle.Right:
                        return new Rectangle(base.DisplayRectangle.X + this.SplitLineWidth, base.DisplayRectangle.Y, base.DisplayRectangle.Width - this.SplitLineWidth, base.DisplayRectangle.Height);
                    case DockStyle.Bottom:
                        return new Rectangle(base.DisplayRectangle.X, base.DisplayRectangle.Y + this.SplitLineWidth, base.DisplayRectangle.Width, base.DisplayRectangle.Height - this.SplitLineWidth);
                    case DockStyle.Fill:
                    default:
                        return base.DisplayRectangle;
                }
            }
        }

        [Browsable(false), DefaultValue(DockStyle.Left)]
        public override DockStyle Dock//请不要随意设置DockStyle.None值
        {
            get { return base.Dock; }
            set
            {
                //if (value == DockStyle.None) return;
                base.Dock = value;
            }
        }

        [Browsable(false)]
        public override Cursor Cursor//显示鼠标指针状态
        {
            get
            {
                if (this.DisplayRectangle.Contains(this.PointToClient(MousePosition))) return base.Cursor;
                //
                switch (this.SplitPanelDock)
                {
                    case DockStyle.Top:
                    case DockStyle.Bottom:
                        return Cursors.HSplit;
                    case DockStyle.Left:
                    case DockStyle.Right:
                        return Cursors.VSplit;
                    default:
                        return base.Cursor;
                }
            }
            set
            {
                base.Cursor = value;
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            //
            if (this.DisplayRectangle.Contains(e.Location)) return;
            //
            if (this.m_SplitLineForm != null)
            {
                this.m_SplitLineForm.Close();
                this.m_SplitLineForm = null;
            }
            this.m_bIsMouseDown = true;
            this.m_SplitLineForm = new SplitLineForm();//key
            this.m_SplitLineForm.Show(this.SplitPanelDock, this.SplitterScreenRectangle);
            //
            this.SetOutSize(); 
        }

        protected virtual void SetOutSize()//在鼠标按下时计算OutSize
        {
            this.OutSize = new Size(this.Parent.Width - (this.OuterMinWidth + this.SplitLineWidth), this.Parent.Height - (this.OuterMinWidth + this.SplitLineWidth));
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (this.m_bIsMouseDown)
            {
                this.ShowSplitLine(this.m_SplitLineForm, e.Location);
            }
        }

        protected virtual void ShowSplitLine(Form splitterForm, Point location)//在鼠标运动时显示SplitLine
        {
            Point pPoint = PointToScreen(location);
            switch (this.SplitPanelDock)
            {
                case DockStyle.Top:
                    if (location.Y >= this.InternalMinWidth && location.Y <= this.OutSize.Height) { splitterForm.Top = pPoint.Y; }
                    break;
                case DockStyle.Left:
                    if (location.X >= this.InternalMinWidth && location.X <= this.OutSize.Width) { splitterForm.Left = pPoint.X; }
                    break;
                case DockStyle.Right:
                    if (location.X <= this.Width - this.InternalMinWidth && location.X >= -(this.OutSize.Width - this.Width)) { splitterForm.Left = pPoint.X; }
                    break;
                case DockStyle.Bottom:
                    if (location.Y <= this.Height - this.InternalMinWidth && location.Y >= -(this.OutSize.Height - this.Height)) { splitterForm.Top = pPoint.Y; }
                    break;
                default:
                    break;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            //
            if (this.m_SplitLineForm != null)
            {
                this.m_SplitLineForm.Close();
                this.m_SplitLineForm = null;
            }
            if (!this.m_bIsMouseDown) return;
            this.m_bIsMouseDown = false;
            //
            this.SetSplitPanelSize(e.Location);
        }
        protected virtual void SetSplitPanelSize(Point point)//在鼠标弹起后设置SplitPanel的尺寸
        {
            switch (this.SplitPanelDock)
            {
                case DockStyle.Top:
                    if (point.Y >= this.InternalMinWidth && point.Y <= this.OutSize.Height) { this.Height = point.Y; }
                    else if (point.Y <= this.InternalMinWidth) { this.Height = this.InternalMinWidth; }
                    else if (point.Y >= this.OutSize.Height) { this.Height = this.OutSize.Height; }
                    break;
                case DockStyle.Left:
                    if (point.X >= this.InternalMinWidth && point.X <= this.OutSize.Width) { this.Width = point.X; }
                    else if (point.X <= this.InternalMinWidth) { this.Width = this.InternalMinWidth; }
                    else if (point.X >= this.OutSize.Width) { this.Width = this.OutSize.Width; }
                    break;
                case DockStyle.Right:
                    if (point.X <= this.Width - this.InternalMinWidth && point.X >= -(this.OutSize.Width - this.Width)) { this.Width -= point.X; }
                    else if (point.X >= this.Width - this.InternalMinWidth) { this.Width = this.InternalMinWidth; }
                    else if (point.X <= -(this.OutSize.Width - this.Width)) { this.Width = this.OutSize.Width; }
                    break;
                case DockStyle.Bottom:
                    if (point.Y <= this.Height - this.InternalMinWidth && point.Y >= -(this.OutSize.Height - this.Height)) { this.Height -= point.Y; }
                    else if (point.Y >= this.Height - this.InternalMinWidth) { this.Height = this.InternalMinWidth; }
                    else if (point.Y <= -(this.OutSize.Height - this.Height)) { this.Height = this.OutSize.Height; }
                    break;
                default:
                    break;
            }
            //
            this.Refresh();
        }


    }
}
