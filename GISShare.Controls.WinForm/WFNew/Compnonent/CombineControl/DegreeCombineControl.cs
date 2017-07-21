using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    public class DegreeCombineControlItem : BaseItemStackExItem
    {
        DoubleInputBoxItem m_dbDegree = new DoubleInputBoxItem() { Size = new System.Drawing.Size(196, 20), Minimum = -360, Maximum = 360, Value = 0, Visible = true };
        IntegerInputBoxItem m_ibD = new IntegerInputBoxItem() { Size = new System.Drawing.Size(50, 20), Minimum = -360, Maximum = 360, Value = 0, Visible = false };
        LabelItem m_lblD = new LabelItem() { Text = "°", Visible = false };
        IntegerInputBoxItem m_ibM = new IntegerInputBoxItem() { Size = new System.Drawing.Size(36, 20), Minimum = 0, Maximum = 59, Value = 0, Visible = false };
        LabelItem m_lblM = new LabelItem() { Text = "′", Visible = false };
        DoubleInputBoxItem m_dbS = new DoubleInputBoxItem() { Size = new System.Drawing.Size(66, 20), Minimum = 0, Maximum = 59.9999999999999999999999999999999999, Value = 0, Visible = false, FloatLength = 3, ShowFloatLength = 3 };
        LabelItem m_lblS = new LabelItem() { Text = "″ ", Visible = false };
        GlyphButtonItem m_GlyphButtonItem = new GlyphButtonItem() { eGlyphStyle = GlyphStyle.eDirectionLeft, LockHeight = true, LockWith = true, Size = new System.Drawing.Size(18, 18), Margin = new System.Windows.Forms.Padding(2) };

        public DegreeCombineControlItem()
        {
            this.m_dbDegree.ValueChanged += new DoubleValueChangedHandler(m_dbDegree_ValueChanged);
            this.m_ibD.ValueChanged += new IntValueChangedHandler(m_ibD_ValueChanged);
            this.m_ibM.ValueChanged += new IntValueChangedHandler(m_ibM_ValueChanged);
            this.m_dbS.ValueChanged += new DoubleValueChangedHandler(m_dbS_ValueChanged);
            this.m_GlyphButtonItem.Click += new EventHandler(m_GlyphButtonItem_Click);
            //
            this.BaseItems.Add(this.m_dbDegree);
            this.BaseItems.Add(this.m_ibD);
            this.BaseItems.Add(this.m_lblD);
            this.BaseItems.Add(this.m_ibM);
            this.BaseItems.Add(this.m_lblM);
            this.BaseItems.Add(this.m_dbS);
            this.BaseItems.Add(this.m_lblS);
            this.BaseItems.Add(this.m_GlyphButtonItem);
            ((ILockCollectionHelper)this.BaseItems).SetLocked(true);
            //
            this.ShowBackgroud = false;
        }
        void m_GlyphButtonItem_Click(object sender, EventArgs e)
        {
            this.ShowDegree = !this.ShowDegree;
        }
        bool m_CancelValueChanged = false;
        void m_dbDegree_ValueChanged(object sender, DoubleValueChangedEventArgs e)
        {
            if (this.m_CancelValueChanged) return;
            //
            this.m_Value = this.m_dbDegree.Value;
        }
        void m_ibD_ValueChanged(object sender, IntValueChangedEventArgs e)
        {
            if (this.m_CancelValueChanged) return;
            //
            this.m_Value = (double)this.m_ibD.Value + (double)this.m_ibM.Value / 60 + this.m_dbS.Value / 3600D;
        }
        void m_ibM_ValueChanged(object sender, IntValueChangedEventArgs e)
        {
            if (this.m_CancelValueChanged) return;
            //
            this.m_Value = (double)this.m_ibD.Value + (double)this.m_ibM.Value / 60 + this.m_dbS.Value / 3600D;
        }
        void m_dbS_ValueChanged(object sender, DoubleValueChangedEventArgs e)
        {
            if (this.m_CancelValueChanged) return;
            //
            this.m_Value = (double)this.m_ibD.Value + (double)this.m_ibM.Value / 60 + this.m_dbS.Value / 3600D;
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

        [Browsable(true), DefaultValue(-360)]
        public double Minimum
        {
            get { return m_dbDegree.Minimum; }
            set
            {
                m_dbDegree.Minimum = value;
                m_ibD.Minimum = (int)value;
            }
        }
        [Browsable(true), DefaultValue(360)]
        public double Maximum
        {
            get { return m_dbDegree.Maximum; }
            set
            {
                m_dbDegree.Maximum = value;
                m_ibD.Maximum = (int)value;
            }
        }

        double m_Value = 0;
        [Browsable(true), DefaultValue(0)]
        public double Value
        {
            get { return this.m_Value; }
            set 
            {
                this.m_Value = value;
                //
                this.UpdateValue();
            } 
        }

        [Browsable(true), DefaultValue(true)]
        public bool ShowDegree
        {
            get { return this.m_dbDegree.Visible; }
            set
            {
                if (this.m_dbDegree.Visible == value) return;
                //
                this.m_dbDegree.Visible = value;
                //
                this.m_ibD.Visible = !value;
                this.m_lblD.Visible = !value;
                this.m_ibM.Visible = !value;
                this.m_lblM.Visible = !value;
                this.m_dbS.Visible = !value;
                this.m_lblS.Visible = !value;
                //
                this.UpdateValue();
            }
        }
        private void UpdateValue()
        {
            this.m_CancelValueChanged = true;
            if (this.ShowDegree)
            {
                this.m_dbDegree.Value = this.m_Value;
            }
            else
            {
                double d = this.m_Value;
                double dDegree = Math.Truncate(d);//度
                //
                d = d - dDegree;
                double M = Math.Truncate((d) * 60);//分
                //
                double dM = (d) * 60 - M;
                double S = dM * 60;// 秒
                //
                this.m_ibD.Value = (int)dDegree;
                this.m_ibM.Value = (int)M;
                this.m_dbS.Value = S;
            }
            this.m_CancelValueChanged = false;
        }

        [Browsable(true), DefaultValue(true)]
        public bool CanEdit
        {
            get { return this.m_dbDegree.CanEdit; }
            set
            {
                this.m_dbDegree.CanEdit = value;
                this.m_ibD.CanEdit = value;
                this.m_ibM.CanEdit = value;
                this.m_dbS.CanEdit = value;           
            }
        }

        //
        //
        //

        public static void DegreeToDMS(double d, out int outD, out int outM, out double outS)
        {
            double dDegree = Math.Truncate(d);//度
            //
            d = d - dDegree;
            double M = Math.Truncate((d) * 60);//分
            //
            double dM = (d) * 60 - M;
            double S = dM * 60;// 秒
            //
            outD = (int)dDegree;
            outM = (int)M;
            outS = S;
        }


    }
}
