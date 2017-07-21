using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    [Designer(typeof(GISShare.Controls.WinForm.WFNew.Design.TabPageDesigner)), ToolboxItem(false)]
    public class TabPage : WFNew.AreaControl, WFNew.ITabPageItem, WFNew.ISetTabPageItemHelper
    {
        public event BoolValueChangedEventHandler TabPageActiveChanged;

        #region ITabPageItem
        private TabButtonTCItem m_TabButtonItem = null; //它所对应的TabButton
        [Browsable(false), Description("携带的TabButtonItem对象"), Category("关联")]
        public WFNew.ITabButtonItem pTabButtonItem
        {
            get { return m_TabButtonItem; }
        }
        #endregion

        #region ISetTabPageItemHelper
        void WFNew.ISetTabPageItemHelper.SetIsSelected(bool bIsSelected)
        {
            this.Visible = bIsSelected;
        }
        #endregion

        protected override EventStateStyle GetEventStateSupplement(string strEventName)
        {
            switch (strEventName)
            {
                case "TabPageActiveChanged":
                    return this.TabPageActiveChanged != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                default:
                    break;
            }
            //
            return base.GetEventStateSupplement(strEventName);
        }

        protected override bool RelationEventSupplement(string strEventName, EventArgs e)
        {
            switch (strEventName)
            {
                case "TabPageActiveChanged":
                    if (this.TabPageActiveChanged != null) { this.TabPageActiveChanged(this, e as BoolValueChangedEventArgs); }
                    return true;
                default:
                    break;
            }
            //
            return base.RelationEventSupplement(strEventName, e);
        }

        public TabPage() 
            : base()
        {
            base.Dock = DockStyle.Fill;
            this.m_TabButtonItem = new TabButtonTCItem(this);
            this.m_TabButtonItem.TabButtonActiveChanged += new GISShare.Controls.WinForm.BoolValueChangedEventHandler(TabButtonItem_TabButtonActiveChanged);
        }
        void TabButtonItem_TabButtonActiveChanged(object sender, GISShare.Controls.WinForm.BoolValueChangedEventArgs e)
        {
            this.OnTabPageActiveChanged(e);
        }

        #region WFNew.IBaseItem
        public override bool LockHeight
        {
            get { return false; }
        }

        public override bool LockWith
        {
            get { return false; }
        }

        public override object Clone()
        {
            TabPage baseItem = new TabPage();
            baseItem.Name = this.Name;
            baseItem.Text = this.Text;
            baseItem.Image = this.Image;
            if (this.GetEventState("TabPageActiveChanged") == EventStateStyle.eUsed) baseItem.TabPageActiveChanged += new BoolValueChangedEventHandler(baseItem_TabPageActiveChanged);
            return baseItem;
        }
        void baseItem_TabPageActiveChanged(object sender, BoolValueChangedEventArgs e)
        {
            this.RelationEvent("TabPageActiveChanged", e);
        }
        #endregion

        #region 属性
        [Browsable(true), Description("图片"), Category("外观")]
        public Image Image
        {
            get
            {
                if (this.pTabButtonItem != null) return this.pTabButtonItem.Image;
                return null;
            }
            set
            {
                if (this.pTabButtonItem != null) this.pTabButtonItem.Image = value;
            }
        }

        [Browsable(false), DefaultValue(DockStyle.Fill), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Description("停靠状态"), Category("布局")]
        public override DockStyle Dock
        {
            get
            {
                return base.Dock;
            }
            set
            {
                base.Dock = DockStyle.Fill;
            }
        }

        [Browsable(false), DefaultValue(true), Description("可见"), Category("状态")]
        public new bool Visible
        {
            get { return base.Visible; }
            set { base.Visible = value; }
        }

        [Browsable(false)]
        public bool IsSelected//该TabPage是否被选中（TabPage与TabButton 共用 IsSelected）
        {
            get
            {
                return this.pTabButtonItem.IsSelected;
            }
        }
        #endregion

        //事件

        protected virtual void OnTabPageActiveChanged(BoolValueChangedEventArgs e)
        {
            if (this.TabPageActiveChanged != null) { this.TabPageActiveChanged(this, e); }
        }

        //
        //
        //

        class TabButtonTCItem : WFNew.TabButtonItem
        {
            public TabButtonTCItem(TabPage tabPage)
                : base(tabPage) { }

            protected override void OnTabButtonMouseUp(MouseEventArgs e)
            {
                base.OnTabButtonMouseUp(e);
                //
                this.Selected();
            }

            protected override void OnCloseButtonMouseUp(MouseEventArgs e)
            {
                base.OnCloseButtonMouseUp(e);
                //
                this.RemoveSelf();
            }
        }
    }
}
