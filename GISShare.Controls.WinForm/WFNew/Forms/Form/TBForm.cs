using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace GISShare.Controls.WinForm.WFNew.Forms
{
    public class TBForm : BaseItemForm, IDependItem, 
        WFNew.IOwner, IOwnerNC, IBaseItemOwnerNC, IBaseItemOwnerNC2, ITBForm, IFormEvent, IUICollectionItemNC
    {
        private TBFormSkinHelper m_TBFormSkinHelper;

        public TBForm()
            : base()
        {
            this.m_TBFormSkinHelper = new TBFormSkinHelper(this);
        }

        protected override void OnLoad(EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                switch (this.StartPosition)
                {
                    case FormStartPosition.CenterParent:
                        if (this.Owner != null)
                        {
                            this.Location = new Point
                                (
                                (this.Owner.Bounds.Left + this.Owner.Bounds.Right - this.Width) / 2,
                                (this.Owner.Bounds.Top + this.Owner.Bounds.Bottom - this.Height) / 2
                                );
                        }
                        else
                        {
                            this.Location = new Point
                                (
                                (SystemInformation.WorkingArea.Left + SystemInformation.WorkingArea.Right - this.Width) / 2,
                                (SystemInformation.WorkingArea.Top + SystemInformation.WorkingArea.Bottom - this.Height) / 2
                                );
                        }
                        break;
                    case FormStartPosition.CenterScreen:
                        this.Location = new Point
                            (
                            (SystemInformation.WorkingArea.Left + SystemInformation.WorkingArea.Right - this.Width) / 2,
                            (SystemInformation.WorkingArea.Top + SystemInformation.WorkingArea.Bottom - this.Height) / 2
                            );
                        break;
                    case FormStartPosition.WindowsDefaultLocation:
                        break;
                    default:
                        break;
                }
            }
            //
            base.OnLoad(e);
        }

        #region IDependItem
        [Browsable(false), Description("获取其依附对象（与此类无关）"), Category("关联")]
        public System.Windows.Forms.Control DependObject
        {
            get { return this; }
        }

        [Browsable(false), Description("获取其依附对象的句柄（与此类无关）"), Category("关联")]
        public IntPtr DependObjectHandle
        {
            get { return this.Handle; }
        }
        #endregion

        #region IOffsetNC
        [Browsable(false), Description("非客户区X轴的偏移量"), Category("布局")]
        public int NCOffsetX
        {
            get { return this.m_TBFormSkinHelper.NCOffsetX; }
        }

        [Browsable(false), Description("非客户区Y轴的偏移量"), Category("布局")]
        public int NCOffsetY
        {
            get { return this.m_TBFormSkinHelper.NCOffsetY; }
        }
        #endregion

        #region IOwnerNC
        public Point PointToClientNC(Point point)
        {
            return this.m_TBFormSkinHelper.PointToClientNC(point);
        }

        public Point PointToScreenNC(Point point)
        {
            return this.m_TBFormSkinHelper.PointToScreenNC(point);
        }
        #endregion

        #region IBaseItemOwnerNC
        [Browsable(false), Description("非客户区其子项展现矩形"), Category("布局")]
        public Rectangle ItemsRectangleNC
        {
            get { return this.m_TBFormSkinHelper.ItemsRectangleNC; }
        }

        [Browsable(false), Description("非客户区其子项视图展现矩形"), Category("布局")]
        public Rectangle ItemsViewRectangleNC
        {
            get { return this.m_TBFormSkinHelper.ItemsViewRectangleNC; }
        }

        public void RefreshNC()
        {
            this.m_TBFormSkinHelper.RefreshNC();
        }

        public bool RefreshExNC()
        {
            return this.m_TBFormSkinHelper.RefreshExNC();
        }
        #endregion

        #region IBaseItemOwnerNC2
        [Browsable(false), DefaultValue(false), Description("取消非客户区其子项的所有事件"), Category("状态")]
        public bool CancelItemsEventNC
        {
            get { return this.m_TBFormSkinHelper.CancelItemsEventNC; }
            set { this.m_TBFormSkinHelper.CancelItemsEventNC = value; }
        }

        [Browsable(false), Description("取消非客户区其子项的绘制事件"), Category("状态")]
        public bool CancelItemsDrawEventNC
        {
            get { return this.m_TBFormSkinHelper.CancelItemsDrawEventNC; }
        }
        #endregion

        #region ICollectionItem2
        IBaseItem ICollectionItem2.GetBaseItem(string strName)
        {
            return ((ICollectionItem2)this.m_TBFormSkinHelper).GetBaseItem(strName);
        }
        #endregion

        #region ICollectionItem3
        public IBaseItem GetBaseItem2(string strName)
        {
            return this.m_TBFormSkinHelper.GetBaseItem2(strName);
        }
        #endregion

        #region IUICollectionItemNC
        [Browsable(false), Description("其所携带的子项集合中是否存在可见项"), Category("状态")]
        bool IUICollectionItemNC.HaveVisibleBaseItemNC
        {
            get
            {
                return ((IUICollectionItemNC)this.m_TBFormSkinHelper).HaveVisibleBaseItemNC;
            }
        }
        #endregion

        #region ITBForm
        [Browsable(true), DefaultValue(false), Description("标题文本居中"), Category("布局")]
        public bool IsMiddleCaptionText
        {
            get { return this.m_TBFormSkinHelper.IsMiddleCaptionText; }
            set { this.m_TBFormSkinHelper.IsMiddleCaptionText = value; }
        }

        [Browsable(true), DefaultValue(true), Description("显示快捷工具条"), Category("状态")]
        public bool ShowQuickAccessToolbar
        {
            get { return this.m_TBFormSkinHelper.ShowQuickAccessToolbar; }
            set { this.m_TBFormSkinHelper.ShowQuickAccessToolbar = value; }
        }
        
        [Browsable(true), DefaultValue(typeof(WFNew.QuickAccessToolbarStyle), "eAllRound"), Description("快捷工具条的展现方式"), Category("外观")]
        public WFNew.QuickAccessToolbarStyle eQuickAccessToolbarStyle
        {
            get { return this.m_TBFormSkinHelper.eQuickAccessToolbarStyle; }
            set { this.m_TBFormSkinHelper.eQuickAccessToolbarStyle = value; }
        }

        [Browsable(true),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
        Editor(typeof(GISShare.Controls.WinForm.WFNew.Design.BaseItemCollectionEditer), typeof(System.Drawing.Design.UITypeEditor)), 
        Description("其快捷工具条所携带的子项集合"), Category("子项")]
        public BaseItemCollection ToolbarItems
        {
            get { return this.m_TBFormSkinHelper.ToolbarItems; }
        }

        [Browsable(false), Description("标题区高"), Category("布局")]
        public int CaptionHeight
        {
            get
            {
                return this.m_TBFormSkinHelper.CaptionHeight;
            }
        }

        [Browsable(false), Description("窗体按钮区宽度"), Category("布局")]
        public int FormButtonStackItemNCWidth
        {
            get { return this.m_TBFormSkinHelper.FormButtonStackItemNCWidth; }
        }

        [Browsable(false), Description("最小标题区文本宽度"), Category("布局")]
        public int MinCaptionTextWidth
        {
            get { return this.m_TBFormSkinHelper.MinCaptionTextWidth; }
        }

        [Browsable(false), Description("是否绘制标题ICON"), Category("状态")]
        public bool IsDrawIcon
        {
            get { return this.m_TBFormSkinHelper.IsDrawIcon; }
        }

        [Browsable(false), Description("是否激活"), Category("状态")]
        public bool IsActive
        {
            get { return this.m_TBFormSkinHelper.IsActive; }
        }

        [Browsable(false), Description("取消非客户区的绘制"), Category("状态")]
        public bool CancelDrawNC
        {
            get { return this.m_TBFormSkinHelper.CancelDrawNC; }
        }

        [Browsable(false), Description("进行非客户区绘制"), Category("状态")]
        public bool IsProcessNCArea
        {
            get
            {
                return this.m_TBFormSkinHelper.IsProcessNCArea;
            }
        }

        [Browsable(false), Description("携带系统菜单"), Category("状态")]
        public bool HasMenu
        {
            get { return this.m_TBFormSkinHelper.HasMenu; }
        }

        [Browsable(false), Description("窗体外轮廓的边框尺寸"), Category("布局")]
        public Size FrameBorderSize
        {
            get
            {
                return this.m_TBFormSkinHelper.FrameBorderSize;
            }
        }

        [Browsable(false), Description("非客户区矩形"), Category("布局")]
        public Rectangle NCRectangleEx
        {
            get
            {
                return this.m_TBFormSkinHelper.NCRectangleEx;
            }
        }

        [Browsable(false), Description("框架矩形"), Category("布局")]
        public Rectangle FrameRectangle
        {
            get
            {
                return this.m_TBFormSkinHelper.FrameRectangle;
            }
        }

        [Browsable(false), Description("标题区矩形"), Category("布局")]
        public Rectangle CaptionRectangle
        {
            get
            {
                return this.m_TBFormSkinHelper.CaptionRectangle;
            }
        }

        [Browsable(false), Description("标题区ICON矩形"), Category("布局")]
        public Rectangle CaptionIconRectangle
        {
            get
            {
                return this.m_TBFormSkinHelper.CaptionIconRectangle;
            }
        }

        [Browsable(false), Description("非客户区快捷工具条矩形"), Category("布局")]
        public Rectangle CaptionToolbarRectangle
        {
            get
            {
                return this.m_TBFormSkinHelper.CaptionToolbarRectangle;
            }
        }

        [Browsable(false), Description("标题区文本布局矩形"), Category("布局")]
        public Rectangle CaptionTextRectangle
        {
            get
            {
                return this.m_TBFormSkinHelper.CaptionTextRectangle;
            }
        }

        [Browsable(false), Description("屏幕矩形矩形"), Category("布局")]
        public Rectangle ScreenRectangle
        {
            get { return this.m_TBFormSkinHelper.ScreenRectangle; }
        }

        public virtual void GetRadiusInfo(out int iLeftTopRadius, out  int iRightTopRadius, out int iLeftBottomRadius, out int iRightBottomRadius)
        {
            this.m_TBFormSkinHelper.GetRadiusInfo(out iLeftTopRadius, out  iRightTopRadius, out iLeftBottomRadius, out iRightBottomRadius);
        }
        #endregion

        #region IFormEvent
        public event PaintEventHandler NCPaint
        {
            add
            {
                this.m_TBFormSkinHelper.NCPaint += value;
            }
            remove
            {
                this.m_TBFormSkinHelper.NCPaint -= value;
            }
        }
        #endregion

        #region SaveLayoutInfo 保存布局文件
        public void SaveLayoutFile(string strFileName)//保存当前布局状态
        {
            this.m_TBFormSkinHelper.SaveLayoutFile(strFileName);
        }
        #endregion

        #region LoadLayoutFile 加载布局文件
        public void LoadLayoutFile(string strFileName, bool loadFormSizeLayout)//加载布局文件，并根据布局文件进行布局
        {
            this.m_TBFormSkinHelper.LoadLayoutFile(strFileName, loadFormSizeLayout);
        }
        #endregion
    }
}
