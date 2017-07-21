using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    public class ControlPanelItem : BaseItem, IControlPanelItem
    {
        HostPanel m_HostPanel = null;

        #region 构造函数
        public ControlPanelItem()
        {
            this.m_HostPanel = new HostPanel(this);
        }

        //public ControlPanelItem(GISShare.Controls.Plugin.WFNew.IControlPanelItemP pBaseItemP)
        //    : this()
        //{
        //    //IPlugin
        //    this.Name = pBaseItemP.Name;
        //    //ISetEntityObject
        //    GISShare.Controls.Plugin.ISetEntityObject pSetEntityObject = pBaseItemP as GISShare.Controls.Plugin.ISetEntityObject;
        //    if (pSetEntityObject != null) pSetEntityObject.SetEntityObject(this);
        //    //IBaseItemP_
        //    this.Checked = pBaseItemP.Checked;
        //    this.Enabled = pBaseItemP.Enabled;
        //    this.Font = pBaseItemP.Font;
        //    this.ForeColor = pBaseItemP.ForeColor;
        //    this.LockHeight = pBaseItemP.LockHeight;
        //    this.LockWith = pBaseItemP.LockWith;
        //    this.Padding = pBaseItemP.Padding;
        //    this.Size = pBaseItemP.Size;
        //    this.Text = pBaseItemP.Text;
        //    this.Visible = pBaseItemP.Visible;
        //    this.Category = pBaseItemP.Category;
        //    this.MinimumSize = pBaseItemP.MinimumSize;
        //    this.UsingViewOverflow = pBaseItemP.UsingViewOverflow;
        //    //IControlHostItemP
        //    if (this.ControlObject != null && pBaseItemP.ChildControls != null)
        //    {
        //        for (int i = pBaseItemP.ChildControls.Length - 1; i >= 0; i--)
        //        {
        //            this.ControlObject.Controls.Add(pBaseItemP.ChildControls[i]);
        //        }
        //    }
        //}
        #endregion

        #region IControlPanelItem
        [Browsable(false), Description("其所在的包罗矩形框"), Category("布局")]
        public virtual Rectangle ControlEnvelope
        {
            get
            {
                Rectangle rectangle = this.DisplayRectangle;
                //
                return new Rectangle(
                    rectangle.X + this.Padding.Left,
                    rectangle.Y + this.Padding.Top,
                    rectangle.Width - this.Padding.Left - this.Padding.Right,
                    rectangle.Height - this.Padding.Top - this.Padding.Bottom
                    );
            }
        }

        [Browsable(true), Description("其所携带的控件实体对象，返回面板实体"), Category("设计")]
        public System.Windows.Forms.Control ControlObject
        {
            get { return this.m_HostPanel; }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Description("面板所携带的控件集合"), Category("布局")]
        public System.Windows.Forms.Control.ControlCollection Controls
        {
            get
            {
                if (this.m_HostPanel == null) return null;
                return this.m_HostPanel.Controls;
            }
        }
        #endregion

        public override System.Drawing.Size MeasureSize(System.Drawing.Graphics g)
        {
            return this.Size;
        }

        public override bool LockHeight
        {
            get { return false; }
        }

        public override bool LockWith
        {
            get { return false; }
        }

        #region Clone
        public override object Clone()
        {
            ControlPanelItem baseItem = new ControlPanelItem();
            baseItem.Checked = this.Checked;
            baseItem.Enabled = this.Enabled;
            baseItem.Font = this.Font;
            baseItem.ForeColor = this.ForeColor;
            baseItem.Name = this.Name;
            baseItem.Site = this.Site;
            baseItem.Size = this.Size;
            baseItem.Tag = this.Tag;
            baseItem.Text = this.Text;
            baseItem.Padding = this.Padding;
            baseItem.Visible = this.Visible;
            if (this.GetEventState("VisibleChanged") == EventStateStyle.eUsed) baseItem.VisibleChanged += new EventHandler(baseItem_VisibleChanged);
            if (this.GetEventState("SizeChanged") == EventStateStyle.eUsed) baseItem.SizeChanged += new EventHandler(baseItem_SizeChanged);
            if (this.GetEventState("Paint") == EventStateStyle.eUsed) baseItem.Paint += new PaintEventHandler(baseItem_Paint);
            if (this.GetEventState("MouseUp") == EventStateStyle.eUsed) baseItem.MouseUp += new MouseEventHandler(baseItem_MouseUp);
            if (this.GetEventState("MouseMove") == EventStateStyle.eUsed) baseItem.MouseMove += new MouseEventHandler(baseItem_MouseMove);
            if (this.GetEventState("MouseLeave") == EventStateStyle.eUsed) baseItem.MouseLeave += new EventHandler(baseItem_MouseLeave);
            if (this.GetEventState("MouseEnter") == EventStateStyle.eUsed) baseItem.MouseEnter += new EventHandler(baseItem_MouseEnter);
            if (this.GetEventState("MouseDown") == EventStateStyle.eUsed) baseItem.MouseDown += new MouseEventHandler(baseItem_MouseDown);
            if (this.GetEventState("MouseDoubleClick") == EventStateStyle.eUsed) baseItem.MouseDoubleClick += new MouseEventHandler(baseItem_MouseDoubleClick);
            if (this.GetEventState("MouseClick") == EventStateStyle.eUsed) baseItem.MouseClick += new MouseEventHandler(baseItem_MouseClick);
            if (this.GetEventState("LocationChanged") == EventStateStyle.eUsed) baseItem.LocationChanged += new EventHandler(baseItem_LocationChanged);
            if (this.GetEventState("EnabledChanged") == EventStateStyle.eUsed) baseItem.EnabledChanged += new EventHandler(baseItem_EnabledChanged);
            if (this.GetEventState("CheckedChanged") == EventStateStyle.eUsed) baseItem.CheckedChanged += new EventHandler(baseItem_CheckedChanged);
            return baseItem;
        }
        void baseItem_CheckedChanged(object sender, EventArgs e)
        {
            this.RelationEvent("CheckedChanged", e);
        }
        void baseItem_EnabledChanged(object sender, EventArgs e)
        {
            this.RelationEvent("EnabledChanged", e);
        }
        void baseItem_LocationChanged(object sender, EventArgs e)
        {
            this.RelationEvent("LocationChanged", e);
        }
        void baseItem_MouseClick(object sender, MouseEventArgs e)
        {
            this.RelationEvent("MouseClick", e);
        }
        void baseItem_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.RelationEvent("MouseDoubleClick", e);
        }
        void baseItem_MouseDown(object sender, MouseEventArgs e)
        {
            this.RelationEvent("MouseDown", e);
        }
        void baseItem_MouseEnter(object sender, EventArgs e)
        {
            this.RelationEvent("MouseEnter", e);
        }
        void baseItem_MouseLeave(object sender, EventArgs e)
        {
            this.RelationEvent("MouseLeave", e);
        }
        void baseItem_MouseMove(object sender, MouseEventArgs e)
        {
            this.RelationEvent("MouseMove", e);
        }
        void baseItem_MouseUp(object sender, MouseEventArgs e)
        {
            this.RelationEvent("MouseUp", e);
        }
        void baseItem_Paint(object sender, PaintEventArgs e)
        {
            this.RelationEvent("Paint", e);
        }
        void baseItem_SizeChanged(object sender, EventArgs e)
        {
            this.RelationEvent("SizeChanged", e);
        }
        void baseItem_VisibleChanged(object sender, EventArgs e)
        {
            this.RelationEvent("VisibleChanged", e);
        }
        #endregion

        //protected override void OnBaseItemOwnerChanged()
        //{
        //    if (this.m_HostPanel == null) return;
        //    //
        //    System.Windows.Forms.Control control = this.m_HostPanel.Parent;
        //    if (control != null && m_HostPanel != null) control.Controls.Remove(this.m_HostPanel);
        //    //
        //    this.ControlObject.Visible = false;
        //    //
        //    control = this.Parent;
        //    if (control != null && this.m_HostPanel != null) control.Controls.Add(m_HostPanel);
        //    //base.OnBaseItemOwnerChanged();
        //}

        protected override void MessageMonitor(MessageInfo messageInfo)
        {
            base.MessageMonitor(messageInfo);
            //
            if (this.m_HostPanel == null) return;
            //
            switch (messageInfo.eMessageStyle)
            {
                case MessageStyle.eMSViewInfo:
                    ViewInfo viewInfo = messageInfo.MessageParameter as ViewInfo;
                    if (viewInfo != null)
                    {
                        this.m_HostPanel.Visible = viewInfo.Visible && !viewInfo.Overflow;
                        this.m_HostPanel.Enabled = viewInfo.Enabled;
                    }
                    break;
            }
        }

        protected override void OnLocationChanged(EventArgs e)
        {
            if (this.m_HostPanel != null) { this.m_HostPanel.Location = this.m_HostPanel.Location; }
            base.OnLocationChanged(e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            if (this.m_HostPanel != null) { this.m_HostPanel.Size = this.ControlEnvelope.Size; }
            base.OnSizeChanged(e);
        }

        //
        //
        //

        class HostPanel : Panel
        {
            ControlPanelItem m_Owner = null;

            public HostPanel(ControlPanelItem ribbonControlPanelItem)
            {
                this.m_Owner = ribbonControlPanelItem;
            }

            public override DockStyle Dock
            {
                get
                {
                    return DockStyle.None;
                }
                set
                {
                    base.Dock = DockStyle.None;
                }
            }
        }
    }
}
