using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    [ToolboxItem(true), DefaultEvent("SelectedDateTimeChanged")]
    public class TimeMonthCalendarCombineControl : AreaControl
    {
        MonthCalendar m_MonthCalendar = new MonthCalendar();
        DateTimeCombineControl m_DateTimeCombineControl = new DateTimeCombineControl();

        [Browsable(true), Description("选择时间改变后触发（参数中的oldValue无效）"), Category("属性已更改")]
        public event PropertyChangedEventHandler SelectedDateTimeChanged;

        public TimeMonthCalendarCombineControl()
        {
            this.SetStyle(ControlStyles.FixedWidth, true);
            this.SetStyle(ControlStyles.FixedHeight, true);
            //
            this.m_MonthCalendar.BackColor = System.Drawing.SystemColors.Window;
            this.m_MonthCalendar.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_MonthCalendar.MaxSelectionCount = 1;
            this.m_MonthCalendar.DateSelected += new DateRangeEventHandler(MonthCalendar_DateSelected);
            //
            this.m_DateTimeCombineControl.Height = 20;
            this.m_DateTimeCombineControl.ShowYear = false;
            this.m_DateTimeCombineControl.ShowMonth = false;
            this.m_DateTimeCombineControl.ShowDay = false;
            this.m_DateTimeCombineControl.Dock = System.Windows.Forms.DockStyle.Top;
            ((ILockCollectionHelper)this.m_DateTimeCombineControl.BaseItems).SetLocked(false);
            this.m_DateTimeCombineControl.BaseItems.Insert(0, new LabelItem() { Text = " 时间：" });
            ((ILockCollectionHelper)this.m_DateTimeCombineControl.BaseItems).SetLocked(true);
            this.m_DateTimeCombineControl.SelectedDateTimeChanged += new PropertyChangedEventHandler(DateTimeCombineControl_SelectedDateTimeChanged);
            //
            this.ShowOutLine = true;
            this.Width = 1 + this.m_MonthCalendar.Width + 1;
            this.Height = 1 + this.m_MonthCalendar.Height + 1 + this.m_DateTimeCombineControl.Height + 1;
            this.Controls.Add(this.m_DateTimeCombineControl);
            this.Controls.Add(this.m_MonthCalendar);
            //
            this.DateTime = System.DateTime.Now;
            this.m_MonthCalendar.SelectionEnd = this.m_DateTime;
            this.m_DateTimeCombineControl.DateTime = this.m_DateTime;
        }
        void MonthCalendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            DateTime date = e.End;
            DateTime time = this.m_DateTime;
            this.DateTime = new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second, time.Millisecond);
        }
        void DateTimeCombineControl_SelectedDateTimeChanged(object sender, PropertyChangedEventArgs e)
        {
            DateTime date = this.m_DateTime;
            DateTime time = this.m_DateTimeCombineControl.DateTime;
            this.DateTime = new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second, time.Millisecond);
        }
        
        public override System.Drawing.Rectangle DisplayRectangle
        {
            get
            {
                System.Drawing.Rectangle rectangle = base.DisplayRectangle;
                rectangle.Inflate(-1, -1);
                return rectangle;
            }
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            int iW = 1 + this.m_MonthCalendar.Width + 1;
            int iH = 1 + this.m_MonthCalendar.Height + 1 + this.m_DateTimeCombineControl.Height + 1;
            if (this.Width != iW) this.Width = iW;
            if (this.Height != iH) this.Height = iH;
        }

        DateTime m_DateTime;
        public DateTime DateTime
        {
            get { return m_DateTime; }
            set 
            {
                if (this.m_DateTime == value) return;
                DateTime dateTime = this.m_DateTime;
                m_DateTime = value;
                this.m_DateTimeCombineControl.DateTime = value;
                this.m_MonthCalendar.SelectionRange = new SelectionRange(value, value);
                this.OnSelectedDateTimeChanged(new PropertyChangedEventArgs(typeof(DateTime), dateTime, this.m_DateTime));
            }
        }

        public override object Clone()
        {
            return new TimeMonthCalendarCombineControl();
        }

        //
        protected virtual void OnSelectedDateTimeChanged(PropertyChangedEventArgs e)
        {
            if (this.SelectedDateTimeChanged != null) this.SelectedDateTimeChanged(this, e);
        }
    }
}
