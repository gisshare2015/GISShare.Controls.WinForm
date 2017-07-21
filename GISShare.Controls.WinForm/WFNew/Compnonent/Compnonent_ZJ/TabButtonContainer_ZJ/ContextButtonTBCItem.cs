using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    class ContextButtonTBCItem : WFNew.DropDownButtonItem, ITabButtonContainerButton, IViewDepend, IBaseItemProperty
    {
        ITabButtonContainerItem m_Owner;

        public ContextButtonTBCItem(ITabButtonContainerItem pTabButtonContainerItem)
        {
            base.Name = "GISShare.Controls.WinForm.WFNew.ContextButtonTBCItem";
            base.Text = "上下文表按钮";
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
                return TabButtonContainerButtonStyle.eContextButton;
            }
        }

        ViewDependStyle IViewDepend.eViewDependStyle
        {
            get
            {
                return ViewDependStyle.eInOwnerDisplayRectangle;
            }
        }

        public Rectangle GlyphRectangle
        {
            get
            {
                Rectangle rectangle = this.DisplayRectangle;
                rectangle.Inflate(-2, -2);
                return Util.UtilTX.CreateSquareRectangle(rectangle);
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
                return this.m_Owner.eTabButtonContainerStyle == TabButtonContainerStyle.eContextButton ||
                    this.m_Owner.eTabButtonContainerStyle == TabButtonContainerStyle.eContextButtonAndCloseButton;
            }
            set
            {
                base.Visible = value;
            }
        }

        public override Rectangle DisplayRectangle
        {
            get
            {
                return this.m_Owner.ContextButtonRectangle;
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

        protected override void OnDraw(System.Windows.Forms.PaintEventArgs e)
        {
            //base.OnDraw(e);
            //
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderTabButtonContainerButton(
                new GISShare.Controls.WinForm.ObjectRenderEventArgs(e.Graphics, this, this.DisplayRectangle));
        }

        protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs mevent)
        {
            this.BaseItems.Clear();
            foreach(BaseItem one in this.m_Owner.BaseItems)
            {
                BaseButtonItem item = new BaseButtonItem();
                item.Name = one.Name;
                item.Text = one.Text;
                item.Tag = one;
                ITabButtonItem pTabButtonItem = one as ITabButtonItem;
                if (pTabButtonItem != null)
                {
                    item.Image = pTabButtonItem.Image;
                    item.Checked = pTabButtonItem.IsSelected;
                }
                item.MouseClick += new System.Windows.Forms.MouseEventHandler(Item_MouseClick);
                this.BaseItems.Add(item);
            }
            //
            base.OnMouseDown(mevent);
        }
        void Item_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            BaseButtonItem item = sender as BaseButtonItem;
            if (item == null) return;
            ITabButtonItem pTabButtonItem = item.Tag as ITabButtonItem;
            if (pTabButtonItem == null) return;
            pTabButtonItem.Selected();
            if (this.m_Owner.AutoShowOverflowTabButton && pTabButtonItem.Overflow)
            {
                IBaseItem5 pBaseItem5 = pTabButtonItem as IBaseItem5;
                if (pBaseItem5 != null) pBaseItem5.MoveTo(0);
            }
        }
    }
}
