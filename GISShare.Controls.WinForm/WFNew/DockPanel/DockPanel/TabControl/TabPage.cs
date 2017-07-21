using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LiuZhenHong.Controls.DockPanel
{
    [Designer(typeof(TabPageDesigner)), ToolboxItem(false)]
    public class TabPage : Ribbon.BaseItemControl, Ribbon.ITabPageItem, Ribbon.ISetTabPageItemHelper
    {
        public event IsSelectedValueChangedEventHandler IsSelectedValueChanged; //属性IsSelected值改变后事件

        private TabButton m_TabButton = null; //它所对应的TabButton
        public Ribbon.ITabButtonItem pTabButtonItem
        {
            get { return m_TabButton; }
        }

        #region ISetTabPageItemHelper
        void Ribbon.ISetTabPageItemHelper.SetIsSelected(bool bIsSelected)
        {

            this.Visible = bIsSelected;
        }
        #endregion

        public TabPage() 
            : base()
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, false);
            //
            base.Dock = DockStyle.Fill;
            this.m_TabButton = new TabButton(this);
            this.m_TabButton.IsSelectedValueChanged += new IsSelectedValueChangedEventHandler(TabButton_IsSelectedValueChanged);
        }

        public override object Clone()
        {
            return new TabPage();
        }

        public override bool LockHeight
        {
            get { return false; }
        }

        public override bool LockWith
        {
            get { return false; }
        }

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

        [Browsable(false), DefaultValue(DockStyle.Fill), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        [Browsable(false)]
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
                return this.m_TabButton.IsSelected;
            }
        }

        [Browsable(false)]
        internal TabButton TabButton//它所对应的TabButton
        {
            get { return m_TabButton; }
        }
        #endregion

        private void TabButton_IsSelectedValueChanged(object sender, IsSelectedValueChangedEventArgs e)
        {
            this.OnIsSelectedValueChanged(this, new IsSelectedValueChangedEventArgs(e.OldValue, e.NewValue, this.TabButton));
        }

        //事件
        protected virtual void OnIsSelectedValueChanged(object sender, IsSelectedValueChangedEventArgs e)
        { if (IsSelectedValueChanged != null) { this.IsSelectedValueChanged(sender, e); } }

    }
}
