using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    [Designer(typeof(GISShare.Controls.WinForm.WFNew.Design.RibbonStartButton2010Designer)), ToolboxItem(true)]
    public class RibbonStartButton2010 : GISShare.Controls.WinForm.WFNew.BaseButtonN, IStartButtonItem, IPopupOwnerHelper, ICollectionObjectDesignHelper, IPopupObjectDesignHelper 
    {
        private IApplicationPopup m_RibbonApplicationPopup;

        public RibbonStartButton2010(IApplicationPopup pApplicationPopup)
            : base()
        {
            this.m_RibbonApplicationPopup = pApplicationPopup;
            ((ISetOwnerHelper)this.m_RibbonApplicationPopup).SetOwner(this);
            //
            this.m_RibbonApplicationPopup.PopupOpened += new EventHandler(RibbonApplicationPopup_PopupOpened);
            this.m_RibbonApplicationPopup.PopupClosed += new EventHandler(RibbonApplicationPopup_PopupClosed);
        }
        void RibbonApplicationPopup_PopupOpened(object sender, EventArgs e)
        {
            this.OnPopupOpened(e);
        }
        void RibbonApplicationPopup_PopupClosed(object sender, EventArgs e)
        {
            this.OnPopupClosed(e);
            //
            if (!this.Contains(this.PointToClient(System.Windows.Forms.Form.MousePosition)))
            {
                this.Refresh();
                //������Ϣ
                ((IMessageChain)this).SendMessage(new MessageInfo(this, MessageStyle.eMSMouseLeave, e));
            }
        }

        public RibbonStartButton2010()
            : this(new RibbonApplicationPopup()) { }

        #region ICollectionObjectDesignHelper
        System.Collections.IList ICollectionObjectDesignHelper.List { get { return this.BaseItems; } }

        bool ICollectionObjectDesignHelper.ExchangeItem(object item1, object item2) { return this.BaseItems.ExchangeItem(item1, item2); }
        #endregion

        #region IPopupOwnerHelper
        IBasePopup IPopupOwnerHelper.GetBasePopup()
        {
            return this.m_RibbonApplicationPopup;
        }
        #endregion

        #region IPopupOwner2
        [Browsable(true), Description("������Popup����"), Category("�����˵�")]
        public event EventHandler PopupOpened;
        [Browsable(true), Description("���ر�Popup����"), Category("�����˵�")]
        public event EventHandler PopupClosed;

        public Padding GetPopupPadding()
        {
            return this.m_RibbonApplicationPopup.GetPadding();
        }

        public void SetPopupPadding(int iPadding)
        {
            this.m_RibbonApplicationPopup.SetPadding(iPadding);
        }

        public void SetPopupRadius(int iRadius)
        {
            this.m_RibbonApplicationPopup.SetRadius(iRadius);
        }

        public void SetPopupRadius(int iLeftTopRadius, int iLeftBottomRadius, int iRightTopRadius, int iRightBottomRadius)
        {
            this.m_RibbonApplicationPopup.SetRadius(iLeftTopRadius, iLeftBottomRadius, iRightTopRadius, iRightBottomRadius);
        }

        public void SetPopupPadding(int left, int top, int right, int bottom)
        {
            this.m_RibbonApplicationPopup.SetPadding(left, top, right, bottom);
        }

        [Browsable(false), Description("�Ƿ����Զ�����Popup��չ��"), Category("��Ϊ")]
        public virtual bool IsAutoMouseTrigger
        {
            get { return false; }
        }

        private int m_PopupSpace = 0;
        [Browsable(true), DefaultValue(0), Description("�����˵�����Я���ߵļ��"), Category("����")]
        public int PopupSpace
        {
            get { return m_PopupSpace; }
            set { m_PopupSpace = value; }
        }

        [Browsable(false), Description("�����˵�������㣨��Ļ���꣩"), Category("����")]
        public virtual Point PopupLoction
        {
            get
            {
                Size size = this.m_RibbonApplicationPopup.GetIdealSize();
                this.m_RibbonApplicationPopup.TrySetPopupPanelSize(size);
                Rectangle rectangle = this.DisplayRectangle;
                Point point;
                if (this.IsPopupItem)
                {
                    //point = this.PointToScreen(new Point(rectangle.Right, rectangle.Top));
                    //point.X += PopupSpace;
                    //if (System.Windows.Forms.SystemInformation.WorkingArea.Width - point.X >= size.Width) return point;
                    //point = this.PointToScreen(new Point(rectangle.Left, rectangle.Top));
                    //point.X = point.X - size.Width - PopupSpace;
                    //return point;
                    point = this.PointToScreen(new Point(rectangle.Right, rectangle.Top));
                    Padding padding = this.m_RibbonApplicationPopup.GetPadding();
                    point.X += PopupSpace + padding.Right;
                    if (System.Windows.Forms.SystemInformation.WorkingArea.Width - point.X >= size.Width) return point;
                    point = this.PointToScreen(new Point(rectangle.Left, rectangle.Top));
                    point.X = point.X - size.Width - PopupSpace - padding.Right;
                    return point;
                }
                else
                {
                    point = this.PointToScreen(new Point(rectangle.Left, rectangle.Bottom));
                    point.Y += PopupSpace;
                    return point;
                }
            }
        }

        [Browsable(false), Description("�����˵��ļ�����"), Category("����")]
        public virtual Rectangle PopupTriggerRectangle
        {
            get
            {
                return this.DisplayRectangle;
            }
        }

        [Browsable(false), Description("�Ƿ���չ��������"), Category("״̬")]
        public bool IsOpened
        {
            get { return this.m_RibbonApplicationPopup.IsOpened; }
        }

        protected override bool RefreshBaseItemState
        {
            get
            {
                return !this.IsOpened;
            }
        }

        public override BaseItemState eBaseItemState
        {
            get
            {
                if (this.IsOpened) return BaseItemState.ePressed;
                return base.eBaseItemState;
            }
        }

        public void ShowPopup()
        {
            if (this.IsOpened) return;
            //
            this.m_RibbonApplicationPopup.Show(this.PopupLoction);
        }

        public void ClosePopup()
        {
            if (!this.IsOpened) return;
            //
            this.m_RibbonApplicationPopup.Close();
        }
        
        public void RefreshPopupPanel()
        {
            this.m_RibbonApplicationPopup.RefreshPopupPanel();
        }
        #endregion

        #region ICollectionItem
        [Browsable(false), Description("����Я������������Ƿ���ڿɼ���"), Category("״̬")]
        public bool HaveVisibleBaseItem
        {
            get
            {
                foreach (BaseItem one in this.BaseItems)
                {
                    if (one.Visible) return true;
                }
                //
                return false;
            }
        }

        [Browsable(false), Description("��Я�������һ����ɢ���齨���ϣ������������޷��Ƴ�����ӣ�û����Ҫ�벻Ҫ�޸��ڲ���Ա�����Է��������������"), Category("����")]
        public BaseItemCollection BaseItems
        {
            get { return this.m_RibbonApplicationPopup.BaseItems; }
        }
        #endregion

        #region IStartButtonItem
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Description("�˵���Я���������"), Category("����")]
        public BaseItemCollection MenuItems
        {
            get { return this.m_RibbonApplicationPopup.MenuItems; }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Description("��¼��Я���������"), Category("����")]
        public BaseItemCollection RecordItems
        {
            get { return this.m_RibbonApplicationPopup.RecordItems; }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Description("������ť��Я���������"), Category("����")]
        public BaseItemCollection OperationItems
        {
            get { return this.m_RibbonApplicationPopup.OperationItems; }
        }
        #endregion

        [Browsable(false)]
        public override DisplayStyle eDisplayStyle
        {
            get
            {
                return DisplayStyle.eImage;
            }
            set
            {
                base.eDisplayStyle = DisplayStyle.eImage;
            }
        }

        [Browsable(false)]
        public override System.Drawing.ContentAlignment ImageAlign
        {
            get
            {
                return System.Drawing.ContentAlignment.MiddleCenter;
            }
            set
            {
                base.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            }
        }

        [Browsable(false)]
        public override System.Drawing.ContentAlignment TextAlign
        {
            get
            {
                return System.Drawing.ContentAlignment.MiddleCenter;
            }
            set
            {
                base.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            }
        }

        [Browsable(false)]
        public override bool ShowNomalState
        {
            get
            {
                return true;
            }
            set
            {
                base.ShowNomalState = true;
            }
        }

        protected override void OnDraw(System.Windows.Forms.PaintEventArgs e)
        {
            //base.OnDraw(e);
            //
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonStartButton2010(
                new GISShare.Controls.WinForm.ObjectRenderEventArgs(e.Graphics, this, this.DisplayRectangle));
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);
            //
            if (this.PopupTriggerRectangle.Contains(mevent.Location)) this.ShowPopup();
        }

        //

        protected virtual void OnPopupOpened(EventArgs e)
        {
            if (this.PopupOpened != null) this.PopupOpened(this, e);
        }

        protected virtual void OnPopupClosed(EventArgs e)
        {
            if (this.PopupClosed != null) this.PopupClosed(this, e);
        }
    }
}
