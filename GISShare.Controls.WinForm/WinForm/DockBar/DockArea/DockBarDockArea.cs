using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.DockBar
{
    [ToolboxItem(false)]
    public class DockBarDockArea : ToolStripPanel
    {
        public DockBarDockArea()
        {
            base.Dock = DockStyle.None;
            base.RenderMode = ToolStripRenderMode.ManagerRenderMode;
        }

        public DockBarDockArea(DockStyle eDockStyle) 
            : this()
        {
            this.Dock = eDockStyle;
        }

        [Browsable(false), Description("停靠区类型"), Category("属性")]
        public DockAreaStyle eDockAreaStyle
        {
            get { return DockAreaStyle.eDockBarDockArea; }
        }

        [Browsable(false), Description("与自身关联的DockBarManager"), Category("关联")]
        public DockBarManager DockBarManager
        { 
            get 
            {
                foreach (IDockBar one in this.Controls) 
                {
                    if (one != null) 
                    {
                        return one.DockBarManager;
                    }

                }
                return null;
            } 
        }

        [Browsable(false), Description("放大的外框区用于接收浮动的工具条"), Category("属性")]
        public virtual Rectangle MaxBounds
        {
            get
            {
                return new Rectangle(- 6, - 6, this.Width + 12, this.Height + 12);
            }
        }

        [Browsable(false), DefaultValue(ToolStripRenderMode.ManagerRenderMode)]
        public new ToolStripRenderMode RenderMode
        {
            get { return base.RenderMode; }
            set { base.RenderMode = value; }
        }

        public override DockStyle Dock
        {
            get
            {
                return base.Dock;
            }
            set
            {
                if (value == DockStyle.Fill) return;
                base.Dock = value;
            }
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            //
            ToolStrip toolStrip = e.Control as ToolStrip;
            switch (this.Dock)
            {
                case DockStyle.Left:
                case DockStyle.Right:
                    toolStrip.LayoutStyle = ToolStripLayoutStyle.VerticalStackWithOverflow;
                    break;
                case DockStyle.Top:
                case DockStyle.Bottom:
                default:
                    toolStrip.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
                    break;
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) 
            {
                if(this.DockBarManager != null) this.DockBarManager.ShowDockBarListMenuStrip(this, e.Location);
            }
            //
            base.OnMouseDown(e);
        }

    }
}
