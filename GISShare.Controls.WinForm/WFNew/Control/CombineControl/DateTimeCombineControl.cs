using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    [Designer(typeof(System.Windows.Forms.Design.ControlDesigner)), ToolboxItem(true)]
    public class DateTimeCombineControl : BaseItemStackEx
    {
        IntegerInputBoxItem m_ibYear = new IntegerInputBoxItem() { Size = new System.Drawing.Size(50, 20), Minimum = 1, Maximum = 9998, Value = 2015 };
        LabelItem m_lblYear = new LabelItem() { Text = "年" };
        IntegerInputBoxItem m_ibMonth = new IntegerInputBoxItem() { Size = new System.Drawing.Size(36, 20), Minimum = 1, Maximum = 12, Value = 1 };
        LabelItem m_lblMonth = new LabelItem() { Text = "月" };
        IntegerInputBoxItem m_ibDay = new IntegerInputBoxItem() { Size = new System.Drawing.Size(36, 20), Minimum = 1, Maximum = 31, Value = 1 };
        LabelItem m_lblDay = new LabelItem() { Text = "日 " };
        IntegerInputBoxItem m_ibHour = new IntegerInputBoxItem() { Size = new System.Drawing.Size(36, 20), Minimum = 0, Maximum = 23, Value = 12 };
        LabelItem m_lblHour = new LabelItem() { Text = "时" };
        IntegerInputBoxItem m_ibMinute = new IntegerInputBoxItem() { Size = new System.Drawing.Size(36, 20), Minimum = 0, Maximum = 59, Value = 0 };
        LabelItem m_lblMinute = new LabelItem() { Text = "分" };
        IntegerInputBoxItem m_ibSecond = new IntegerInputBoxItem() { Size = new System.Drawing.Size(36, 20), Minimum = 0, Maximum = 59, Value = 0 };
        LabelItem m_lblSecond = new LabelItem() { Text = "秒" };

        [Browsable(true), Description("选择时间改变后触发（参数中的oldValue无效）"), Category("属性已更改")]
        public event PropertyChangedEventHandler SelectedDateTimeChanged;

        public DateTimeCombineControl()
        {
            this.m_ibYear.ValueChanged += new IntValueChangedHandler(IntValue_ValueChanged);
            this.m_ibMonth.ValueChanged += new IntValueChangedHandler(IntValue_ValueChanged);
            this.m_ibDay.ValueChanged += new IntValueChangedHandler(IntValue_ValueChanged);
            this.m_ibHour.ValueChanged += new IntValueChangedHandler(IntValue_ValueChanged);
            this.m_ibMinute.ValueChanged += new IntValueChangedHandler(IntValue_ValueChanged);
            this.m_ibSecond.ValueChanged += new IntValueChangedHandler(IntValue_ValueChanged);
            //
            this.BaseItems.Add(this.m_ibYear);
            this.BaseItems.Add(this.m_lblYear);
            this.BaseItems.Add(this.m_ibMonth);
            this.BaseItems.Add(this.m_lblMonth);
            this.BaseItems.Add(this.m_ibDay);
            this.BaseItems.Add(this.m_lblDay);
            this.BaseItems.Add(this.m_ibHour);
            this.BaseItems.Add(this.m_lblHour);
            this.BaseItems.Add(this.m_ibMinute);
            this.BaseItems.Add(this.m_lblMinute);
            this.BaseItems.Add(this.m_ibSecond);
            this.BaseItems.Add(this.m_lblSecond);
            ((ILockCollectionHelper)this.BaseItems).SetLocked(true);
            //
            this.ShowBackgroud = false;
            this.DateTime = System.DateTime.Now;
        }
        void IntValue_ValueChanged(object sender, IntValueChangedEventArgs e)
        {
            if (this.m_CancelEvent) return;
            //
            DateTime dateTime = this.DateTime;
            //
            if (sender == this.m_ibYear || sender == this.m_ibMonth)
            {
                switch (this.m_ibMonth.Value)
                {
                    case 1:
                    case 3:
                    case 5:
                    case 7:
                    case 8:
                    case 10:
                    case 12:
                        this.m_ibDay.Maximum = 31;
                        break;
                    case 4:
                    case 6:
                    case 9:
                    case 11:
                        this.m_ibDay.Maximum = 30;
                        break;
                    case 2:
                        if (System.DateTime.IsLeapYear(this.m_ibYear.Value))
                        {
                            this.m_ibDay.Maximum = 29;
                        }
                        else
                        {
                            this.m_ibDay.Maximum = 28;
                        }
                        break;
                    default:
                        this.m_ibDay.Maximum = 31;
                        break;
                }
            }
            //
            this.OnSelectedDateTimeChanged(new PropertyChangedEventArgs(typeof(DateTime), dateTime, this.DateTime));
        }

        [Browsable(false)]
        public override System.Windows.Forms.Orientation eOrientation
        {
            get
            {
                return System.Windows.Forms.Orientation.Horizontal;
            }
            set
            {
                base.eOrientation = value;
            }
        }

        [Browsable(false)]
        public override int ColumnDistance
        {
            get
            {
                return 0;
            }
            set
            {
                base.ColumnDistance = value;
            }
        }

        [Browsable(false)]
        public override bool IsRestrictItems
        {
            get
            {
                return true;
            }
            set
            {
                base.IsRestrictItems = value;
            }
        }

        [Browsable(false)]
        public override bool IsStretchItems
        {
            get
            {
                return true;
            }
            set
            {
                base.IsStretchItems = value;
            }
        }

        [Browsable(true)]
        bool m_CancelEvent = false;
        public System.DateTime DateTime
        {
            get
            {
                return new DateTime(this.m_ibYear.Value, this.m_ibMonth.Value, this.m_ibDay.Value, this.m_ibHour.Value, this.m_ibMinute.Value, this.m_ibSecond.Value);
            }
            set
            {
                DateTime dateTime = this.DateTime;
                if (dateTime == value) return;
                //
                this.m_CancelEvent = true;
                //
                this.m_ibYear.Value = value.Year;
                this.m_ibMonth.Value = value.Month;
                this.m_ibDay.Value = value.Day;
                this.m_ibHour.Value = value.Hour;
                this.m_ibMinute.Value = value.Minute;
                this.m_ibSecond.Value = value.Second;
                //
                this.m_CancelEvent = false;
                //
                //this.OnSelectedDateTimeChanged(new PropertyChangedEventArgs(typeof(DateTime), dateTime, value));
                IntValue_ValueChanged(this.m_ibYear, null);
            }
        }

        [Browsable(true), DefaultValue(true), Description("是否显示年"), Category("属性")]
        public bool ShowYear
        {
            get
            {
                return this.m_ibYear.Visible;
            }
            set
            {
                this.m_ibYear.Visible = value;
                this.m_lblYear.Visible = value;
            }
        }
        [Browsable(true), DefaultValue(true), Description("是否显示月"), Category("属性")]
        public bool ShowMonth
        {
            get
            {
                return this.m_ibMonth.Visible;
            }
            set
            {
                this.m_ibMonth.Visible = value;
                this.m_lblMonth.Visible = value;
            }
        }
        [Browsable(true), DefaultValue(true), Description("是否显示日"), Category("属性")]
        public bool ShowDay
        {
            get
            {
                return this.m_ibDay.Visible;
            }
            set
            {
                this.m_ibDay.Visible = value;
                this.m_lblDay.Visible = value;
            }
        }
        [Browsable(true), DefaultValue(true), Description("是否显示时"), Category("属性")]
        public bool ShowHour
        {
            get
            {
                return this.m_ibHour.Visible;
            }
            set
            {
                this.m_ibHour.Visible = value;
                this.m_lblHour.Visible = value;
            }
        }
        [Browsable(true), DefaultValue(true), Description("是否显示分"), Category("属性")]
        public bool ShowMinute
        {
            get
            {
                return this.m_ibMinute.Visible;
            }
            set
            {
                this.m_ibMinute.Visible = value;
                this.m_lblMinute.Visible = value;
            }
        }
        [Browsable(true), DefaultValue(true), Description("是否显示秒"), Category("属性")]
        public bool ShowSecond
        {
            get
            {
                return this.m_ibSecond.Visible;
            }
            set
            {
                this.m_ibSecond.Visible = value;
                this.m_lblSecond.Visible = value;
            }
        }

        [Browsable(true), DefaultValue(true)]
        public bool CanEdit
        {
            get { return this.m_ibYear.CanEdit; }
            set
            {
                this.m_ibYear.CanEdit = value;
                this.m_ibMonth.CanEdit = value;
                this.m_ibDay.CanEdit = value;
                this.m_ibHour.CanEdit = value;
                this.m_ibMinute.CanEdit = value;
                this.m_ibSecond.CanEdit = value;
            }
        }

        //
        protected virtual void OnSelectedDateTimeChanged(PropertyChangedEventArgs e)
        {
            if (this.SelectedDateTimeChanged != null) this.SelectedDateTimeChanged(this, e);
        }
    }
}
