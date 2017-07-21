using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    public sealed class FormButtonItem : BaseButtonItem, IFormButton, IBaseItemProperty
    {
        public FormButtonItem(FormButtonStyle formButtonStyle)
        {
            base.Name = "GISShare.Controls.WinForm.WFNew.FormButtonItem";
            base.Text = "窗体按钮";
            //
            this.m_eFormButtonStyle = formButtonStyle;
        }

        public System.Windows.Forms.Form OperationForm
        {
            get { return this.TryGetDependParentForm(); }
        }

        private FormButtonStyle m_eFormButtonStyle = FormButtonStyle.eCloseButton;
        public FormButtonStyle eFormButtonStyle
        {
            get 
            {                
                return m_eFormButtonStyle; 
            }
        }

        public Rectangle GlyphRectangle
        {
            get
            {
                int iOffset = 6;
                Rectangle rectangle = this.DisplayRectangle;
                int iD = rectangle.Width - rectangle.Height;
                if (iD > 0) { iOffset = (int)(rectangle.Height / 3); }
                else { iOffset = (int)(rectangle.Width / 3); }
                //
                if (iD == 0)
                {
                    return Rectangle.FromLTRB(rectangle.Left + iOffset - 1, rectangle.Top + iOffset - 1, rectangle.Right - iOffset, rectangle.Bottom - iOffset);
                }
                else if (iD > 0)
                {
                    int iAve = iD / 2;
                    return Rectangle.FromLTRB(rectangle.Left + iAve + iOffset - 1, rectangle.Top + iOffset - 1, rectangle.Right - iAve - iOffset, rectangle.Bottom - iOffset);
                }
                else
                {
                    int iAve = -iD / 2;
                    return Rectangle.FromLTRB(rectangle.Left + iOffset - 1, rectangle.Top + iAve + iOffset - 1, rectangle.Right - iOffset, rectangle.Bottom - iAve - iOffset);
                }
            }
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
            get { return this.pOwner as IBaseItem3; }
        }
        #endregion

        public override bool Visible
        {
            get
            {
                switch (this.eFormButtonStyle)
                {
                    case FormButtonStyle.eMinButton:
                        if (this.OperationForm != null) return this.OperationForm.MinimizeBox;
                        return base.Visible;
                    case FormButtonStyle.eMaxButton:
                        if (this.OperationForm != null) return this.OperationForm.MaximizeBox;
                        return base.Visible;
                    case FormButtonStyle.eHelpButton:
                        if (this.OperationForm != null) return this.OperationForm.HelpButton;
                        return base.Visible;
                    case FormButtonStyle.eCloseButton:
                        if (this.OperationForm != null) return this.OperationForm.ControlBox;
                        return base.Visible;
                    case FormButtonStyle.eMdiMinButton:
                    case FormButtonStyle.eMdiMaxButton:
                    case FormButtonStyle.eMdiCloseButton:
                        if (this.OperationForm != null)
                        {
                            if (this.OperationForm.IsMdiContainer && this.HaveMaximizedFormWindowState())
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        return base.Visible;
                    default:
                        return base.Visible;
                }
            }
            set
            {
                base.Visible = value;
            }
        }
        private bool HaveMaximizedFormWindowState()
        {
            foreach (System.Windows.Forms.Form one in this.OperationForm.MdiChildren)
            {
                if (one.WindowState == System.Windows.Forms.FormWindowState.Maximized) return true;
            }
            //
            return false;
        }

        public override DisplayStyle eDisplayStyle
        {
            get
            {
                return DisplayStyle.eNone;
            }
            set
            {
                base.eDisplayStyle = DisplayStyle.eNone;
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

        public override System.Drawing.Rectangle DisplayRectangle
        {
            get
            {
                switch(this.eFormButtonStyle)
                {
                    case FormButtonStyle.eMdiMinButton:
                    case FormButtonStyle.eMdiMaxButton:
                    case FormButtonStyle.eMdiCloseButton:
                        return new Rectangle(this.Location, new System.Drawing.Size(20, 20));
                    default:
                        return new Rectangle(this.Location, new System.Drawing.Size(System.Windows.Forms.SystemInformation.CaptionButtonSize.Width, base.Size.Height));
                }
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
            if (this.OperationForm == null) return;
            //
            System.Windows.Forms.Form form;
            switch (this.eFormButtonStyle)
            {
                case FormButtonStyle.eMinButton:
                    form = this.OperationForm;
                    if (form != null && form.IsMdiChild && form.WindowState == System.Windows.Forms.FormWindowState.Minimized)
                        form.WindowState = System.Windows.Forms.FormWindowState.Normal;
                    else
                        form.WindowState = System.Windows.Forms.FormWindowState.Minimized;
                    break;
                case FormButtonStyle.eMaxButton:
                    form = this.OperationForm;
                    if (form != null && form.WindowState != System.Windows.Forms.FormWindowState.Maximized)
                        this.OperationForm.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    else
                        this.OperationForm.WindowState = System.Windows.Forms.FormWindowState.Normal;
                    break;
                case FormButtonStyle.eHelpButton:
                    break;
                case FormButtonStyle.eCloseButton:
                    this.OperationForm.Close();
                    break;
                case FormButtonStyle.eMdiMinButton:
                    form = this.GetMaximizedFormWindow();
                    if (form != null) form.WindowState = System.Windows.Forms.FormWindowState.Minimized;
                    this.pBaseItemOwner.Refresh();
                    break;
                case FormButtonStyle.eMdiMaxButton:
                    form = this.GetMaximizedFormWindow();
                    if (form != null) form.WindowState = System.Windows.Forms.FormWindowState.Normal;
                    this.pBaseItemOwner.Refresh();
                    break;
                case FormButtonStyle.eMdiCloseButton:
                    form = this.GetMaximizedFormWindow();
                    if (form != null) form.Close();
                    this.pBaseItemOwner.Refresh();
                    break;
                default:
                    break;
            }
        }
        private System.Windows.Forms.Form GetMaximizedFormWindow()
        {
            foreach (System.Windows.Forms.Form one in this.OperationForm.MdiChildren)
            {
                if (one.WindowState == System.Windows.Forms.FormWindowState.Maximized) return one;
            }
            //
            return null;
        }

        protected override void OnDraw(System.Windows.Forms.PaintEventArgs e)
        {
            //base.OnDraw(e);
            //
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderFormButton(
                new GISShare.Controls.WinForm.ObjectRenderEventArgs(e.Graphics, this, this.DisplayRectangle));
        }
    }
}
