using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.Popup
{
    [ToolboxItem(false), DefaultEvent("PopupOpened"), Category("GISShare.Controls.WinForm.WFNew")]
    public abstract class BasePopup : ToolStripDropDown
    {
        public event EventHandler PopupOpened;
        public event EventHandler PopupClosed;

        public BasePopup() 
            : base()
        {
            //this.SetStyle(ControlStyles.Opaque, true);//key  减少闪烁非常重要
            //this.SetStyle(ControlStyles.UserPaint, true);
            //this.SetStyle(ControlStyles.Selectable, false);
            //this.SetStyle(ControlStyles.DoubleBuffer, true);
            //this.SetStyle(ControlStyles.ResizeRedraw, true);
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            //base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            //this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = false;
            //
            //this.SetStyle(ControlStyles.UserPaint, true);
            //this.SetStyle(ControlStyles.DoubleBuffer, true);
            //
            SetStyle(ControlStyles.Opaque, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.Selectable, false);
            SetStyle(ControlStyles.ResizeRedraw, false);
            base.UpdateStyles(); 
            //
            base.AutoClose = false;
        }

        public abstract BasePopup TryGetParentBasePopup();

        [Browsable(false), DefaultValue(false), Description("交由.NET系统自动给关闭BasePopup"), Category("行为")]
        public new bool AutoClose
        {
            get { return false; }
            set { base.AutoClose = false; }
        }

        [Browsable(false), DefaultValue(typeof(ToolStripLayoutStyle), "Flow"), Description("子项的布局方式"), Category("布局")]
        public new ToolStripLayoutStyle LayoutStyle
        {
            get { return base.LayoutStyle; }
            set { base.LayoutStyle = value; }
        }

        [Browsable(false), DefaultValue(true), Description("自动管理尺寸"), Category("布局")]
        public override bool AutoSize
        {
            get
            {
                return base.AutoSize;
            }
            set
            {
                base.AutoSize = true;
            }
        }

        public void DismissPopup()
        {
            GISShare.Controls.WinForm.Popup.BasePopupManager.PopupManager.Dismiss(DismissReason.eCustomize);
        }

        private bool m_IsOpened = false;
        [Browsable(false), Description("是否已展开"), Category("状态")]
        public bool IsOpened
        {
            get { return m_IsOpened; }
        }

        private DismissReason m_eDismissReason = DismissReason.eAppClicked;
        [Browsable(false), Description("请求方式"), Category("状态")]
        public DismissReason eDismissReason
        {
            get { return m_eDismissReason; }
        }

        [Browsable(false), Description("是否自定义过滤器"), Category("行为")]
        public virtual bool CustomFiltration
        {
            get { return false; }
        }

        public virtual bool Filtration(System.Windows.Forms.MouseEventArgs e)
        {
            return this.Bounds.Contains(e.Location);
        }

        public void Close(DismissReason eDismissReason)
        {
            base.Close();
            //
            this.m_eDismissReason = eDismissReason;
        }

        protected override void OnOpening(CancelEventArgs e)
        {
            GISShare.Controls.WinForm.Popup.BasePopupManager.PopupManager.DismissChildren(this.TryGetParentBasePopup(), DismissReason.eNewBasePopup);
            //
            base.OnOpening(e);
        }

        protected override void OnOpened(EventArgs e)
        {
            GISShare.Controls.WinForm.Popup.BasePopupManager.PopupManager.Register(this);
            this.m_IsOpened = true;
            //
            this.OnPopupOpened(e);
            //
            base.OnOpened(e);
        }

        protected override void OnClosed(ToolStripDropDownClosedEventArgs e)
        {
            GISShare.Controls.WinForm.Popup.BasePopupManager.PopupManager.Unregister(this);
            this.m_IsOpened = false;
            //
            this.OnPopupClosed(e);
            //
            base.OnClosed(e);
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
