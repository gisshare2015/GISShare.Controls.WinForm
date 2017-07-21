using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew.Forms
{
    class NCFormButtonItem : WFNew.BaseButtonItem, WFNew.IFormButton, INCBaseItem
    {
        private const int CRT_MINSIZE = 6;

        public NCFormButtonItem(WFNew.FormButtonStyle formButtonStyle)
        {
            this.m_eFormButtonStyle = formButtonStyle;
            //this.UsingViewOverflow = false;
        }

        public System.Windows.Forms.Form OperationForm
        {
            get
            {
                ////IDependItem pDependItem = this.GetTopOwner() as IDependItem;
                ////if (pDependItem == null) return this.TryGetDependParentForm();
                ////return pDependItem.DependObject as System.Windows.Forms.Form;
                //return this.TryGetDependParentForm();
                IDependItem pDependItem = this.TryGetDependItem_DG(this.pOwner);
                if (pDependItem == null) return this.TryGetDependParentForm();
                return pDependItem.DependObject as System.Windows.Forms.Form;
            }
        }
        private IDependItem TryGetDependItem_DG(IOwner owner)
        {
            if (owner == null) return null;
            //
            IDependItem pDependItem = owner as IDependItem;
            if (pDependItem == null) return this.TryGetDependItem_DG(owner.pOwner);
            return pDependItem;
        }

        private WFNew.FormButtonStyle m_eFormButtonStyle = WFNew.FormButtonStyle.eCloseButton;
        public WFNew.FormButtonStyle eFormButtonStyle
        {
            get { return m_eFormButtonStyle; }
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

        public override bool Visible
        {
            get
            {
                Form form = this.OperationForm;
                switch (this.eFormButtonStyle)
                {
                    case WFNew.FormButtonStyle.eMinButton:
                        if (form != null) return form.MinimizeBox || (form.IsMdiChild && form.WindowState == FormWindowState.Minimized);
                        return base.Visible;
                    case WFNew.FormButtonStyle.eMaxButton:
                        if (form != null) return form.MaximizeBox || (form.IsMdiChild && form.WindowState == FormWindowState.Minimized);
                        return base.Visible;
                    case WFNew.FormButtonStyle.eHelpButton:
                        if (form != null) return form.HelpButton && !(form.IsMdiChild && form.WindowState == FormWindowState.Minimized);
                        return base.Visible;
                    case WFNew.FormButtonStyle.eCloseButton:
                        if (form != null) return form.ControlBox || (form.IsMdiChild && form.WindowState == FormWindowState.Minimized);
                        return base.Visible;
                    case WFNew.FormButtonStyle.eMdiMinButton:
                    case WFNew.FormButtonStyle.eMdiMaxButton:
                    case WFNew.FormButtonStyle.eMdiCloseButton:
                        if (form != null)
                        {
                            if (form.IsMdiContainer && this.HaveMaximizedFormWindowState(form))
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
        private bool HaveMaximizedFormWindowState(Form form)
        {
            foreach (System.Windows.Forms.Form one in form.MdiChildren)
            {
                if (one.WindowState == System.Windows.Forms.FormWindowState.Maximized) return true;
            }
            //
            return false;
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

        public override Rectangle DisplayRectangle
        {
            get
            {
                Form form = this.OperationForm;
                switch (this.eFormButtonStyle)
                {
                    case WFNew.FormButtonStyle.eMdiMinButton:
                    case WFNew.FormButtonStyle.eMdiMaxButton:
                    case WFNew.FormButtonStyle.eMdiCloseButton:
                        return new Rectangle(this.Location, new System.Drawing.Size(20, this.Size.Height));
                    default:
                        if (form == null)
                        {
                            return new Rectangle(this.Location, new System.Drawing.Size(22, this.Size.Height));
                        }
                        else
                        {
                            if (form.IsMdiChild && form.WindowState == FormWindowState.Minimized)
                            { 
                                return new Rectangle(this.Location, new System.Drawing.Size(SystemInformation.CaptionButtonSize.Width, this.Size.Height));
                            }
                            //
                            switch (form.FormBorderStyle)
                            {
                                case FormBorderStyle.FixedToolWindow:
                                case FormBorderStyle.SizableToolWindow:
                                    return new Rectangle(this.Location, new System.Drawing.Size(SystemInformation.ToolWindowCaptionButtonSize.Width, this.Size.Height));
                                case FormBorderStyle.Sizable:
                                case FormBorderStyle.Fixed3D:
                                case FormBorderStyle.FixedDialog:
                                case FormBorderStyle.FixedSingle:
                                    //System.Diagnostics.Debug.WriteLine(new Rectangle(this.Location, new System.Drawing.Size(SystemInformation.CaptionButtonSize.Width, this.Size.Height)) + " - " + this.pOwner.DisplayRectangle);
                                    return new Rectangle(this.Location, new System.Drawing.Size(SystemInformation.CaptionButtonSize.Width, this.Size.Height));
                                default:
                                    return new Rectangle(this.Location, new System.Drawing.Size(22, this.Size.Height));
                            }
                        }
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
                case WFNew.FormButtonStyle.eMinButton:
                    form = this.OperationForm;
                    if (form != null && form.IsMdiChild && form.WindowState == System.Windows.Forms.FormWindowState.Minimized)
                        form.WindowState = System.Windows.Forms.FormWindowState.Normal;
                    else
                        form.WindowState = System.Windows.Forms.FormWindowState.Minimized;
                    break;
                case WFNew.FormButtonStyle.eMaxButton:
                    form = this.OperationForm;
                    if (form != null && form.WindowState != System.Windows.Forms.FormWindowState.Maximized)
                        this.OperationForm.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    else
                        this.OperationForm.WindowState = System.Windows.Forms.FormWindowState.Normal;
                    break;
                case WFNew.FormButtonStyle.eHelpButton:
                    break;
                case WFNew.FormButtonStyle.eCloseButton:
                    this.OperationForm.Close();
                    break;
                case WFNew.FormButtonStyle.eMdiMinButton:
                    form = this.GetMaximizedFormWindow();
                    if (form != null) form.WindowState = System.Windows.Forms.FormWindowState.Minimized;
                    this.pBaseItemOwner.Refresh();
                    break;
                case WFNew.FormButtonStyle.eMdiMaxButton:
                    form = this.GetMaximizedFormWindow();
                    if (form != null) form.WindowState = System.Windows.Forms.FormWindowState.Normal;
                    this.pBaseItemOwner.Refresh();
                    break;
                case WFNew.FormButtonStyle.eMdiCloseButton:
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

        #region IOffsetNC
        [Browsable(false), Description("X轴偏移"), Category("布局")]
        public int NCOffsetX
        {
            get 
            {
                IOffsetNC pOffsetNC = this.pOwner as IOffsetNC;
                if (pOffsetNC == null) return -1;
                return pOffsetNC.NCOffsetX;
            }
        }

        [Browsable(false), Description("Y轴偏移"), Category("布局")]
        public int NCOffsetY
        {
            get
            {
                IOffsetNC pOffsetNC = this.pOwner as IOffsetNC;
                if (pOffsetNC == null) return -1;
                return pOffsetNC.NCOffsetY;
            }
        }
        #endregion

        #region INCBaseItem
        public IBaseItemOwnerNC GetTopBaseItemOwnerNC() 
        {
            return this.TryGetDependControl() as IBaseItemOwnerNC;
        }
        #endregion

        public override void Refresh()
        {
            IBaseItemOwnerNC pBaseItemOwnerNC = this.pOwner as IBaseItemOwnerNC;
            if (pBaseItemOwnerNC != null)
            {
                pBaseItemOwnerNC.RefreshNC();
            }
            else
            {
                base.Refresh();
            }
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
