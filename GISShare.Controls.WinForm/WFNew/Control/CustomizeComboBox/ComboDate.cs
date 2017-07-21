using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    [ToolboxItem(true), DefaultEvent("SelectedDateChanged")]
    public class ComboDate : CustomizeComboBox, IComboDateItem
    {
        System.Windows.Forms.MonthCalendar m_MonthCalendar;

        public ComboDate()
            : base(new System.Windows.Forms.MonthCalendar())
        {
            this.m_MonthCalendar = ((ICustomizeComboBoxItem)this).ControlObject as System.Windows.Forms.MonthCalendar;
            this.m_MonthCalendar.BackColor = System.Drawing.SystemColors.Window;
            this.m_MonthCalendar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_MonthCalendar.MaxSelectionCount = 1;
            this.m_MonthCalendar.DateSelected += new DateRangeEventHandler(MonthCalendar_DateSelected);
            //
            this.ShowDropDownNum = 1;
            base.DropDownWidth = 222;
        }
        void MonthCalendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            this.OnSelectedDateChanged(new PropertyChangedEventArgs(typeof(DateTime), e.Start, e.End));
            //
            if(this.AutoClosePopup) this.ClosePopup();
        }

        protected override EventStateStyle GetEventStateSupplement(string strEventName)
        {
            switch (strEventName)
            {
                case "SelectedDateChanged":
                    return this.SelectedDateChanged != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
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
                case "SelectedDateChanged":
                    if (this.SelectedDateChanged != null) { this.SelectedDateChanged(this, e as PropertyChangedEventArgs); }
                    return true;
                default:
                    break;
            }
            //
            return base.RelationEventSupplement(strEventName, e);
        }

        #region IComboDateItem
        [Browsable(true), Description("选择时间改变后触发（参数中的oldValue无效）"), Category("属性已更改")]
        public event PropertyChangedEventHandler SelectedDateChanged;

        [Browsable(true), Description("选择时间"), Category("数据")]
        public DateTime SelectedDate
        {
            get 
            {
                return this.m_MonthCalendar.SelectionRange.End;
            }
            set
            {
                this.m_MonthCalendar.SelectionRange = new SelectionRange(value, value);
            }
        }
        #endregion

        #region Clone
        public override object Clone()
        {
            ComboDate baseItem = new ComboDate();
            baseItem.Checked = this.Checked;
            baseItem.Enabled = this.Enabled;
            baseItem.Font = this.Font;
            baseItem.ForeColor = this.ForeColor;
            baseItem.Name = this.Name;
            baseItem.Site = this.Site;
            baseItem.Size = this.Size;
            baseItem.Tag = this.Tag;
            baseItem.Text = this.Text;
            baseItem.Visible = this.Visible;
            baseItem.MinHeight = this.MinHeight;
            baseItem.MinWidth = this.MinWidth;
            baseItem.DropDownHeight = this.DropDownHeight;
            baseItem.DropDownWidth = this.DropDownWidth;
            baseItem.eModifySizeStyle = this.eModifySizeStyle;
            baseItem.eCustomizeComboBoxStyle = this.eCustomizeComboBoxStyle;
            baseItem.ArrowSize = this.ArrowSize;
            //IComboDateItem
            baseItem.SelectedDate = this.SelectedDate;
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
            if (this.GetEventState("TextChanged") == EventStateStyle.eUsed) baseItem.TextChanged += new EventHandler(baseItem_TextChanged);
            if (this.GetEventState("KeyDown") == EventStateStyle.eUsed) baseItem.KeyDown += new KeyEventHandler(baseItem_KeyDown);
            if (this.GetEventState("KeyPress") == EventStateStyle.eUsed) baseItem.KeyPress += new KeyPressEventHandler(baseItem_KeyPress);
            if (this.GetEventState("KeyUp") == EventStateStyle.eUsed) baseItem.KeyUp += new KeyEventHandler(baseItem_KeyUp);
            if (this.GetEventState("PopupOpened") == EventStateStyle.eUsed) baseItem.PopupOpened += new EventHandler(baseItem_PopupOpened);
            if (this.GetEventState("PopupClosed") == EventStateStyle.eUsed) baseItem.PopupClosed += new EventHandler(baseItem_PopupClosed);
            if (this.GetEventState("SplitMouseUp") == EventStateStyle.eUsed) baseItem.SplitMouseUp += new MouseEventHandler(baseItem_SplitMouseUp);
            if (this.GetEventState("SplitMouseMove") == EventStateStyle.eUsed) baseItem.SplitMouseMove += new MouseEventHandler(baseItem_SplitMouseMove);
            if (this.GetEventState("SplitMouseDown") == EventStateStyle.eUsed) baseItem.SplitMouseDown += new MouseEventHandler(baseItem_SplitMouseDown);
            if (this.GetEventState("SplitMouseDoubleClick") == EventStateStyle.eUsed) baseItem.SplitMouseDoubleClick += new MouseEventHandler(baseItem_SplitMouseDoubleClick);
            if (this.GetEventState("SplitMouseClick") == EventStateStyle.eUsed) baseItem.SplitMouseClick += new MouseEventHandler(baseItem_SplitMouseClick);
            if (this.GetEventState("TextBoxMouseUp") == EventStateStyle.eUsed) baseItem.TextBoxMouseUp += new MouseEventHandler(baseItem_TextBoxMouseUp);
            if (this.GetEventState("TextBoxMouseMove") == EventStateStyle.eUsed) baseItem.TextBoxMouseMove += new MouseEventHandler(baseItem_TextBoxMouseMove);
            if (this.GetEventState("TextBoxMouseDown") == EventStateStyle.eUsed) baseItem.TextBoxMouseDown += new MouseEventHandler(baseItem_TextBoxMouseDown);
            if (this.GetEventState("TextBoxMouseDoubleClick") == EventStateStyle.eUsed) baseItem.TextBoxMouseDoubleClick += new MouseEventHandler(baseItem_TextBoxMouseDoubleClick);
            if (this.GetEventState("TextBoxMouseClick") == EventStateStyle.eUsed) baseItem.TextBoxMouseClick += new MouseEventHandler(baseItem_TextBoxMouseClick);
            if (this.GetEventState("SelectedDateChanged") == EventStateStyle.eUsed) baseItem.SelectedDateChanged += new PropertyChangedEventHandler(baseItem_SelectedDateChanged);
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
        void baseItem_KeyUp(object sender, KeyEventArgs e)
        {
            this.RelationEvent("KeyUp", e);
        }
        void baseItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.RelationEvent("KeyPress", e);
        }
        void baseItem_KeyDown(object sender, KeyEventArgs e)
        {
            this.RelationEvent("KeyDown", e);
        }
        void baseItem_TextChanged(object sender, EventArgs e)
        {
            this.RelationEvent("TextChanged", e);
        }
        void baseItem_PopupClosed(object sender, EventArgs e)
        {
            this.RelationEvent("PopupClosed", e);
        }
        void baseItem_PopupOpened(object sender, EventArgs e)
        {
            this.RelationEvent("PopupOpened", e);
        }
        void baseItem_TextBoxMouseClick(object sender, MouseEventArgs e)
        {
            this.RelationEvent("TextBoxMouseClick", e);
        }
        void baseItem_TextBoxMouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.RelationEvent("TextBoxMouseDoubleClick", e);
        }
        void baseItem_TextBoxMouseDown(object sender, MouseEventArgs e)
        {
            this.RelationEvent("TextBoxMouseDown", e);
        }
        void baseItem_TextBoxMouseMove(object sender, MouseEventArgs e)
        {
            this.RelationEvent("TextBoxMouseMove", e);
        }
        void baseItem_TextBoxMouseUp(object sender, MouseEventArgs e)
        {
            this.RelationEvent("TextBoxMouseUp", e);
        }
        void baseItem_SplitMouseClick(object sender, MouseEventArgs e)
        {
            this.RelationEvent("SplitMouseClick(", e);
        }
        void baseItem_SplitMouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.RelationEvent("SplitMouseDoubleClick", e);
        }
        void baseItem_SplitMouseDown(object sender, MouseEventArgs e)
        {
            this.RelationEvent("SplitMouseDown", e);
        }
        void baseItem_SplitMouseMove(object sender, MouseEventArgs e)
        {
            this.RelationEvent("SplitMouseMove", e);
        }
        void baseItem_SplitMouseUp(object sender, MouseEventArgs e)
        {
            this.RelationEvent("SplitMouseUp", e);
        }
        void baseItem_SelectedDateChanged(object sender, PropertyChangedEventArgs e)
        {
            this.RelationEvent("SelectedDateChanged", e);
        }
        #endregion

        public override object Value
        {
            get
            {
                return this.m_MonthCalendar.SelectionRange.End;
            }
        }

        [Browsable(false)]
        public override string Text
        {
            get
            {
                return this.m_MonthCalendar.SelectionRange.End.ToShortDateString();
            }
            set
            {
                DateTime dtDate;
                if (DateTime.TryParse(value, out dtDate))
                {
                    this.m_MonthCalendar.SelectionRange = new SelectionRange(dtDate, dtDate);
                }
            }
        }

        [Browsable(false), DefaultValue(26), Description("最小高度（无效）"), Category("布局")]
        public new int MinHeight
        {
            get
            {
                return 26;
            }
            set { }
        }

        [Browsable(false), DefaultValue(26), Description("最小宽度（无效）"), Category("布局")]
        public new int MinWidth
        {
            get
            {
                return 26;
            }
            set { }
        }

        [Browsable(false), Description("弹出框的宽度（无效）"), Category("布局")]//DefaultValue(120), 
        public new int DropDownWidth
        {
            get
            {
                return -1;
            }
            set { }
        }

        [Browsable(false), Description("弹出框的高度（无效）"), Category("布局")]//DefaultValue(120), 
        public new int DropDownHeight
        {
            get
            {
                return -1;
            }
            set { }
        }

        [Browsable(false), DefaultValue(typeof(ModifySizeStyle), "eNone"), Description("下拉菜单修改尺寸的类型（无效）"), Category("布局")]
        public new ModifySizeStyle eModifySizeStyle
        {
            get
            {
                return ModifySizeStyle.eNone;
            }
            set { }
        }

        //
        protected virtual void OnSelectedDateChanged(PropertyChangedEventArgs e)
        {
            if (this.SelectedDateChanged != null) this.SelectedDateChanged(this, e);
        }
    }
}
