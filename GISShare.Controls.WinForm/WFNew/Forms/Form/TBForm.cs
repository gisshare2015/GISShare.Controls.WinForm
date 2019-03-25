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
        [Browsable(false), Description("��ȡ����������������޹أ�"), Category("����")]
        public System.Windows.Forms.Control DependObject
        {
            get { return this; }
        }

        [Browsable(false), Description("��ȡ����������ľ����������޹أ�"), Category("����")]
        public IntPtr DependObjectHandle
        {
            get { return this.Handle; }
        }
        #endregion

        #region IOffsetNC
        [Browsable(false), Description("�ǿͻ���X���ƫ����"), Category("����")]
        public int NCOffsetX
        {
            get { return this.m_TBFormSkinHelper.NCOffsetX; }
        }

        [Browsable(false), Description("�ǿͻ���Y���ƫ����"), Category("����")]
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
        [Browsable(false), Description("�ǿͻ���������չ�־���"), Category("����")]
        public Rectangle ItemsRectangleNC
        {
            get { return this.m_TBFormSkinHelper.ItemsRectangleNC; }
        }

        [Browsable(false), Description("�ǿͻ�����������ͼչ�־���"), Category("����")]
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
        [Browsable(false), DefaultValue(false), Description("ȡ���ǿͻ���������������¼�"), Category("״̬")]
        public bool CancelItemsEventNC
        {
            get { return this.m_TBFormSkinHelper.CancelItemsEventNC; }
            set { this.m_TBFormSkinHelper.CancelItemsEventNC = value; }
        }

        [Browsable(false), Description("ȡ���ǿͻ���������Ļ����¼�"), Category("״̬")]
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
        [Browsable(false), Description("����Я������������Ƿ���ڿɼ���"), Category("״̬")]
        bool IUICollectionItemNC.HaveVisibleBaseItemNC
        {
            get
            {
                return ((IUICollectionItemNC)this.m_TBFormSkinHelper).HaveVisibleBaseItemNC;
            }
        }
        #endregion

        #region ITBForm
        [Browsable(true), DefaultValue(false), Description("�����ı�����"), Category("����")]
        public bool IsMiddleCaptionText
        {
            get { return this.m_TBFormSkinHelper.IsMiddleCaptionText; }
            set { this.m_TBFormSkinHelper.IsMiddleCaptionText = value; }
        }

        [Browsable(true), DefaultValue(true), Description("��ʾ��ݹ�����"), Category("״̬")]
        public bool ShowQuickAccessToolbar
        {
            get { return this.m_TBFormSkinHelper.ShowQuickAccessToolbar; }
            set { this.m_TBFormSkinHelper.ShowQuickAccessToolbar = value; }
        }
        
        [Browsable(true), DefaultValue(typeof(WFNew.QuickAccessToolbarStyle), "eAllRound"), Description("��ݹ�������չ�ַ�ʽ"), Category("���")]
        public WFNew.QuickAccessToolbarStyle eQuickAccessToolbarStyle
        {
            get { return this.m_TBFormSkinHelper.eQuickAccessToolbarStyle; }
            set { this.m_TBFormSkinHelper.eQuickAccessToolbarStyle = value; }
        }

        [Browsable(true),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
        Editor(typeof(GISShare.Controls.WinForm.WFNew.Design.BaseItemCollectionEditer), typeof(System.Drawing.Design.UITypeEditor)), 
        Description("���ݹ�������Я���������"), Category("����")]
        public BaseItemCollection ToolbarItems
        {
            get { return this.m_TBFormSkinHelper.ToolbarItems; }
        }

        [Browsable(false), Description("��������"), Category("����")]
        public int CaptionHeight
        {
            get
            {
                return this.m_TBFormSkinHelper.CaptionHeight;
            }
        }

        [Browsable(false), Description("���尴ť�����"), Category("����")]
        public int FormButtonStackItemNCWidth
        {
            get { return this.m_TBFormSkinHelper.FormButtonStackItemNCWidth; }
        }

        [Browsable(false), Description("��С�������ı����"), Category("����")]
        public int MinCaptionTextWidth
        {
            get { return this.m_TBFormSkinHelper.MinCaptionTextWidth; }
        }

        [Browsable(false), Description("�Ƿ���Ʊ���ICON"), Category("״̬")]
        public bool IsDrawIcon
        {
            get { return this.m_TBFormSkinHelper.IsDrawIcon; }
        }

        [Browsable(false), Description("�Ƿ񼤻�"), Category("״̬")]
        public bool IsActive
        {
            get { return this.m_TBFormSkinHelper.IsActive; }
        }

        [Browsable(false), Description("ȡ���ǿͻ����Ļ���"), Category("״̬")]
        public bool CancelDrawNC
        {
            get { return this.m_TBFormSkinHelper.CancelDrawNC; }
        }

        [Browsable(false), Description("���зǿͻ�������"), Category("״̬")]
        public bool IsProcessNCArea
        {
            get
            {
                return this.m_TBFormSkinHelper.IsProcessNCArea;
            }
        }

        [Browsable(false), Description("Я��ϵͳ�˵�"), Category("״̬")]
        public bool HasMenu
        {
            get { return this.m_TBFormSkinHelper.HasMenu; }
        }

        [Browsable(false), Description("�����������ı߿�ߴ�"), Category("����")]
        public Size FrameBorderSize
        {
            get
            {
                return this.m_TBFormSkinHelper.FrameBorderSize;
            }
        }

        [Browsable(false), Description("�ǿͻ�������"), Category("����")]
        public Rectangle NCRectangleEx
        {
            get
            {
                return this.m_TBFormSkinHelper.NCRectangleEx;
            }
        }

        [Browsable(false), Description("��ܾ���"), Category("����")]
        public Rectangle FrameRectangle
        {
            get
            {
                return this.m_TBFormSkinHelper.FrameRectangle;
            }
        }

        [Browsable(false), Description("����������"), Category("����")]
        public Rectangle CaptionRectangle
        {
            get
            {
                return this.m_TBFormSkinHelper.CaptionRectangle;
            }
        }

        [Browsable(false), Description("������ICON����"), Category("����")]
        public Rectangle CaptionIconRectangle
        {
            get
            {
                return this.m_TBFormSkinHelper.CaptionIconRectangle;
            }
        }

        [Browsable(false), Description("�ǿͻ�����ݹ���������"), Category("����")]
        public Rectangle CaptionToolbarRectangle
        {
            get
            {
                return this.m_TBFormSkinHelper.CaptionToolbarRectangle;
            }
        }

        [Browsable(false), Description("�������ı����־���"), Category("����")]
        public Rectangle CaptionTextRectangle
        {
            get
            {
                return this.m_TBFormSkinHelper.CaptionTextRectangle;
            }
        }

        [Browsable(false), Description("��Ļ���ξ���"), Category("����")]
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

        #region SaveLayoutInfo ���沼���ļ�
        public void SaveLayoutFile(string strFileName)//���浱ǰ����״̬
        {
            this.m_TBFormSkinHelper.SaveLayoutFile(strFileName);
        }
        #endregion

        #region LoadLayoutFile ���ز����ļ�
        public void LoadLayoutFile(string strFileName, bool loadFormSizeLayout)//���ز����ļ��������ݲ����ļ����в���
        {
            this.m_TBFormSkinHelper.LoadLayoutFile(strFileName, loadFormSizeLayout);
        }
        #endregion
    }
}
