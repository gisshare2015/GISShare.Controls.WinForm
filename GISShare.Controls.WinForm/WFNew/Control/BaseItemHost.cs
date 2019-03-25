using GISShare.Controls.WinForm.WFNew.View;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    [ToolboxItem(true), Designer(typeof(GISShare.Controls.WinForm.WFNew.Design.BaseItemHostDesigner))]
    public class BaseItemHost : BaseItemControl, IBaseItemHost, ICollectionObjectDesignHelper
    {
        private BaseItem m_BaseItemObject;
        private IRibbonControl m_pRibbonControl;
        private BaseItemCollection m_BaseItemCollection;

        public BaseItemHost()
        {
            this.m_BaseItemCollection = new BaseItemCollection(this);
            this.m_BaseItemCollection.ItemAdded += new ItemEventHandler(BaseItemCollection_ItemAdded);
        }
        public BaseItemHost(BaseItem baseItem) : this()
        {
            if (baseItem != null)
            {
                this.m_BaseItemCollection.Add(baseItem);
                ((ILockCollectionHelper)this.m_BaseItemCollection).SetLocked(true);
            }
        }
        void BaseItemCollection_ItemAdded(object sender, ItemEventArgs e)
        {
            this.m_BaseItemObject = e.Item as BaseItem;
            this.m_pRibbonControl = this.m_BaseItemObject as IRibbonControl;
        }

        #region ICollectionObjectDesignHelper
        System.Collections.IList ICollectionObjectDesignHelper.List { get { return this.m_BaseItemCollection; } }

        bool ICollectionObjectDesignHelper.ExchangeItem(object item1, object item2) { return this.m_BaseItemCollection.ExchangeItem(item1, item2); }
        #endregion

        #region IBaseItemHost
        [Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), Description("其所依附的子项"), Category("子项")]
        public BaseItem BaseItemObject
        {
            get
            {
                return this.m_BaseItemObject;
            }
            set
            {
                this.m_BaseItemCollection.Add(value);
            }
        }
        #endregion

        protected override void WndProc(ref Message m)
        {
            if (this.m_pRibbonControl != null && 
                this.m_pRibbonControl.ParentForm != null)
            {
                switch (m.Msg)
                {
                    case (int)GISShare.Win32.Msgs.WM_LBUTTONDOWN:
                        Point point = this.PointToClient(MousePosition);
                        if (this.m_pRibbonControl.CaptionTextRectangle.Contains(point))
                        {
                            GISShare.Win32.API.SendMessage(this.m_pRibbonControl.ParentForm.Handle,
                                (int)GISShare.Win32.Msgs.WM_NCLBUTTONDOWN,
                                (uint)GISShare.Win32.HitTests.HTCAPTION,
                                (uint)GISShare.Win32.NativeMethods.MousePositionToLParam(point));
                            return;
                        }
                        break;
                }
            }
            //
            base.WndProc(ref m);
        }

        protected override void MessageMonitor(MessageInfo messageInfo)
        {
            base.MessageMonitor(messageInfo);
            if (this.m_BaseItemObject == null) return;
            //
            switch (messageInfo.eMessageStyle)
            {
                case MessageStyle.eMSPaint:
                    Rectangle displayRectangle = this.DisplayRectangle;
                    Rectangle displayRectangle2 = this.m_BaseItemObject.DisplayRectangle;
                    if (displayRectangle != displayRectangle2)
                    {
                        Size size = displayRectangle2.Size;
                        if (this.m_BaseItemObject.LockWith)
                        {
                            if (!this.LockWith)
                            {
                                switch(this.Dock ){
                                    case System.Windows.Forms.DockStyle.Top:
                                    case System.Windows.Forms.DockStyle.Bottom:
                                    case System.Windows.Forms.DockStyle.Fill:
                                        break;
                                    default:
                                        this.Width = displayRectangle2.Width;
                                        break;
                                }
                            }
                        }
                        else
                        {
                            size.Width = displayRectangle.Width;
                        }
                        if (this.m_BaseItemObject.LockHeight)
                        {
                            if (!this.LockHeight)
                            {
                                switch (this.Dock)
                                {
                                    case System.Windows.Forms.DockStyle.Left:
                                    case System.Windows.Forms.DockStyle.Right:
                                    case System.Windows.Forms.DockStyle.Fill:
                                        break;
                                    default:
                                        this.Height = displayRectangle2.Height;
                                        break;
                                }
                            }
                        }
                        else
                        {
                            size.Height = displayRectangle.Height;
                        }
                        displayRectangle = new Rectangle(displayRectangle.Location, size);
                        if (displayRectangle != displayRectangle2)
                        {
                            if (this.m_BaseItemObject is ISetBaseItemHelper)
                            {
                                ((ISetBaseItemHelper)this.m_BaseItemObject).SetDisplayRectangle(displayRectangle);
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
            //
            IMessageChain pMessageChain = this.m_BaseItemObject as IMessageChain;
            if (pMessageChain != null)
            {
                pMessageChain.SendMessage(messageInfo);
            }
        }

        #region Clone
        public override object Clone()
        {
            BaseItemHost baseItem = new BaseItemHost();
            if (this.m_BaseItemObject != null) 
            {
                baseItem.BaseItemObject = (BaseItem)this.m_BaseItemObject.Clone();
            }
            return baseItem;
        }
        #endregion
    }

}
