using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    class CloseButtonTBCItem : WFNew.BaseButtonItem, ITabButtonContainerButton, IViewDepend, IBaseItemProperty
    {
        ITabButtonContainerItem m_Owner;

        public CloseButtonTBCItem(ITabButtonContainerItem pTabButtonContainerItem)
        {
            base.Name = "GISShare.Controls.WinForm.WFNew.CloseButtonTBCItem";
            base.Text = "关闭表按钮";
            base.UsingViewOverflow = false;
            
            this.m_Owner = pTabButtonContainerItem;
            ((ISetOwnerHelper)this).SetOwner(m_Owner as IOwner);
        }

        #region IBaseItemProperty
        [Browsable(false), Description("自身所属状态"), Category("属性")]
        BaseItemStyle IBaseItemProperty.eBaseItemStyle
        {
            get { return BaseItemStyle.eComponentBaseItem; }
        }

        [Browsable(false), Description("获取其依附项（如果，为独立项依附项为其自身）"), Category("关联")]
        IBaseItem3 IBaseItemProperty.DependObject
        {
            get { return this.m_Owner; }
        }
        #endregion

        public TabButtonContainerButtonStyle eTabButtonContainerButtonStyle
        {
            get
            {
                return TabButtonContainerButtonStyle.eCloseButton;
            }
        }

        public Rectangle GlyphRectangle
        {
            get
            {
                return Util.UtilTX.CreateSquareRectangle(this.DisplayRectangle);
            }
        }

        ViewDependStyle IViewDepend.eViewDependStyle
        {
            get
            {
                return ViewDependStyle.eInOwnerDisplayRectangle;
            }
        }

        public override bool Overflow
        {
            get
            {
                return !this.m_Owner.DrawRectangle.Contains(this.DisplayRectangle);
            }
        }

        public override bool Visible
        {
            get
            {
                return this.m_Owner.eTabButtonContainerStyle == TabButtonContainerStyle.eCloseButton ||
                    this.m_Owner.eTabButtonContainerStyle == TabButtonContainerStyle.eContextButtonAndCloseButton;
            }
            set
            {
                base.Visible = value;
            }
        }

        //protected override bool SetBaseItemStateEx(BaseItemState baseItemState)
        //{
        //    System.Diagnostics.Debug.WriteLine(String.Format("{0}", baseItemState.ToString()));
        //    //
        //    return base.SetBaseItemStateEx(baseItemState);
        //}

        public override Rectangle DisplayRectangle
        {
            get
            {
                return this.m_Owner.CloseButtonRectangle;
            }
        }

        public override WFNew.DisplayStyle eDisplayStyle
        {
            get
            {
                return WFNew.DisplayStyle.eNone;
            }
            set
            {
                base.eDisplayStyle = WFNew.DisplayStyle.eNone;
            }
        }

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

        public override bool ShowNomalState
        {
            get
            {
                return false;
            }
            set
            {
                base.ShowNomalState = value;
            }
        }

        protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            //
            ITabControl pTabControl = this.m_Owner.TryGetTabControl();
            if(pTabControl == null) return;
            ITabButtonItem pTabButtonItem = this.m_Owner.SelectTabButtonItem;
            if (pTabButtonItem == null) return;
            pTabControl.RemoveTabPage(pTabButtonItem.pTabPageItem);
        }

        protected override void OnDraw(System.Windows.Forms.PaintEventArgs e)
        {
            //base.OnDraw(e);
            //
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderTabButtonContainerButton(
                new GISShare.Controls.WinForm.ObjectRenderEventArgs(e.Graphics, this, this.DisplayRectangle));
        }
    }
}
