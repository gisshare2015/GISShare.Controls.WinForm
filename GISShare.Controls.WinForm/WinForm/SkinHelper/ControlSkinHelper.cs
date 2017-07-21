using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm
{
    public class ControlSkinHelper : NativeWindow, WFNew.Forms.IDependItem, WFNew.IBaseItem
    {
        private Control m_HostControl;

        public ControlSkinHelper(Control hostControl)
        {
            this.m_HostControl = hostControl;
            if (this.m_HostControl.Handle != IntPtr.Zero) this.HostControl_HandleCreated(this.m_HostControl, null);
            //
            this.RegisterEventHandlers(hostControl);
        }

        ~ControlSkinHelper()
        {
            this.UnregisterEventHandlers(m_HostControl);
        }

        #region ×¢²á
        protected virtual void RegisterEventHandlers(Control hostControl)
        {
            hostControl.HandleCreated += HostControl_HandleCreated;
            hostControl.HandleDestroyed += HostControl_HandleDestroyed;
            hostControl.Disposed += HostControl_ParentDisposed;
        }
        protected virtual void UnregisterEventHandlers(Control hostControl)
        {
            hostControl.HandleCreated -= HostControl_HandleCreated;
            hostControl.HandleDestroyed -= HostControl_HandleDestroyed;
            hostControl.Disposed -= HostControl_ParentDisposed;
        }
        void HostControl_HandleCreated(object sender, EventArgs e)
        {
            this.AssignHandle(this.m_HostControl.Handle);
        }
        void HostControl_HandleDestroyed(object sender, EventArgs e)
        {
            this.ReleaseHandle();
        }
        void HostControl_ParentDisposed(object sender, EventArgs e)
        {
            if (this.m_HostControl != null) UnregisterEventHandlers(m_HostControl);
            this.m_HostControl = null;
        }
        #endregion

        #region IDependItem
        public System.Windows.Forms.Control DependObject
        {
            get { return this.m_HostControl; }
        }

        public IntPtr DependObjectHandle
        {
            get { return this.m_HostControl.Handle; }
        }
        #endregion

        #region WFNew.IBaseItem
        public string Name
        {
            get
            {
                if (this.m_HostControl == null || this.m_HostControl.IsDisposed) return "NULL";
                return this.m_HostControl.Name;
            }
            set
            {
                if (this.m_HostControl == null || this.m_HostControl.IsDisposed) return;
                this.m_HostControl.Name = value;
            }
        }

        public string Text
        {
            get
            {
                if (this.m_HostControl == null || this.m_HostControl.IsDisposed) return "NULL";
                return this.m_HostControl.Text;
            }
            set
            {
                if (this.m_HostControl == null || this.m_HostControl.IsDisposed) return;
                this.m_HostControl.Text = value;
            }
        }

        public bool Enabled
        {
            get
            {
                if (this.m_HostControl == null || this.m_HostControl.IsDisposed) return false;
                return this.m_HostControl.Enabled;
            }
            set
            {
                if (this.m_HostControl == null || this.m_HostControl.IsDisposed) return;
                this.m_HostControl.Enabled = value;
            }
        }

        public bool Visible
        {
            get
            {
                if (this.m_HostControl == null || this.m_HostControl.IsDisposed) return false;
                return this.m_HostControl.Visible;
            }
            set
            {
                if (this.m_HostControl == null || this.m_HostControl.IsDisposed) return;
                this.m_HostControl.Visible = value;
            }
        }

        public Padding Padding
        {
            get
            {
                if (this.m_HostControl == null || this.m_HostControl.IsDisposed) return new Padding();
                return this.m_HostControl.Padding;
            }
            set
            {
                if (this.m_HostControl == null || this.m_HostControl.IsDisposed) return;
                this.m_HostControl.Padding = value;
            }
        }

        WFNew.RenderStyle m_eRenderStyle = WFNew.RenderStyle.eSystem;
        public virtual WFNew.RenderStyle eRenderStyle
        {
            get { return m_eRenderStyle; }
            set { m_eRenderStyle = value; }
        }

        public Font Font
        {
            get
            {
                if (this.m_HostControl == null || this.m_HostControl.IsDisposed) return new Font("ËÎÌå", 9f);
                return this.m_HostControl.Font;
            }
            set
            {
                if (this.m_HostControl == null || this.m_HostControl.IsDisposed) return;
                this.m_HostControl.Font = value;
            }
        }

        public Color ForeColor
        {
            get
            {
                if (this.m_HostControl == null || this.m_HostControl.IsDisposed) return Color.Black;
                return this.m_HostControl.ForeColor;
            }
            set
            {
                if (this.m_HostControl == null || this.m_HostControl.IsDisposed) return;
                this.m_HostControl.ForeColor = value;
            }
        }

        public int Height
        {
            get
            {
                if (this.m_HostControl == null || this.m_HostControl.IsDisposed) return -1;
                return this.m_HostControl.Height;
            }
            set
            {
                if (this.m_HostControl == null || this.m_HostControl.IsDisposed) return;
                this.m_HostControl.Height = value;
            }
        }

        public int Width
        {
            get
            {
                if (this.m_HostControl == null || this.m_HostControl.IsDisposed) return -1;
                return this.m_HostControl.Width;
            }
            set
            {
                if (this.m_HostControl == null || this.m_HostControl.IsDisposed) return;
                this.m_HostControl.Width = value;
            }
        }

        public Size Size
        {
            get
            {
                if (this.m_HostControl == null || this.m_HostControl.IsDisposed) return new Size();
                return this.m_HostControl.Size;
            }
            set
            {
                if (this.m_HostControl == null || this.m_HostControl.IsDisposed) return;
                this.m_HostControl.Size = value;
            }
        }

        public Point Location
        {
            get
            {
                if (this.m_HostControl == null || this.m_HostControl.IsDisposed) return new Point();
                return this.m_HostControl.Location;
            }
            set
            {
                if (this.m_HostControl == null || this.m_HostControl.IsDisposed) return;
                this.m_HostControl.Location = value;
            }
        }

        public Rectangle DisplayRectangle
        {
            get
            {
                if (this.m_HostControl == null || this.m_HostControl.IsDisposed) return new Rectangle();
                return this.m_HostControl.DisplayRectangle;
            }
        }

        private WFNew.BaseItemState m_eBaseItemState = WFNew.BaseItemState.eNormal;
        protected virtual bool SetBaseItemState(WFNew.BaseItemState baseItemState)
        {
            if (this.m_eBaseItemState == baseItemState) return false;
            this.m_eBaseItemState = baseItemState;
            return true;
        }
        protected virtual bool SetBaseItemStateEx(WFNew.BaseItemState baseItemState)
        {
            if (this.m_eBaseItemState == baseItemState) return false;
            //
            this.m_eBaseItemState = baseItemState;
            this.Refresh();
            //
            return true;
        }
        public virtual WFNew.BaseItemState eBaseItemState
        {
            get
            {
                return m_eBaseItemState;
            }
        }

        public void Refresh()
        {
            if (this.m_HostControl == null || this.m_HostControl.IsDisposed) return;
            this.m_IsUserRefresh = true;
            this.m_HostControl.Refresh();
            this.m_IsUserRefresh = false;
        }

        public void Invalidate(Rectangle rectangle)
        {
            if (this.m_HostControl == null || this.m_HostControl.IsDisposed) return;
            this.m_IsUserRefresh = true;
            this.m_HostControl.Invalidate(rectangle);
            this.m_IsUserRefresh = false;
        }

        public Point PointToClient(Point point)
        {
            if (this.m_HostControl == null || this.m_HostControl.IsDisposed) return new Point(-1, -1);
            return this.m_HostControl.PointToClient(point);
        }

        public Point PointToScreen(Point point)
        {
            if (this.m_HostControl == null || this.m_HostControl.IsDisposed) return new Point();
            return this.m_HostControl.PointToScreen(point);
        }
        #endregion

        bool m_IsUserRefresh = false;
        protected bool IsUserRefresh
        {
            get { return m_IsUserRefresh; }
        }

        protected virtual Color TryGetBackColor()
        {
            return this.TryGetBackColor_DG(this.m_HostControl);
        }
        protected Color TryGetBackColor_DG(Control ctr)
        {
            if (ctr == null || ctr.IsDisposed) return System.Drawing.SystemColors.Control;
            //
            if (ctr.BackColor == Color.Transparent) return TryGetBackColor_DG(ctr.Parent);
            return ctr.BackColor;
        }

        protected IntPtr SetUpPalette(IntPtr dc, bool force, bool realizePalette)
        {
            IntPtr halftonePalette = Graphics.GetHalftonePalette();
            IntPtr ptr2 = GISShare.Win32.API.SelectPalette(new System.Runtime.InteropServices.HandleRef(null, dc), new System.Runtime.InteropServices.HandleRef(null, halftonePalette), force ? 0 : 1);
            if ((ptr2 != IntPtr.Zero) && realizePalette)
            {
                GISShare.Win32.API.RealizePalette(new System.Runtime.InteropServices.HandleRef(null, dc));
            }
            return ptr2;
        }
    }
}
