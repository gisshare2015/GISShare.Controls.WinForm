using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm
{
    public delegate void BoolValueChangedEventHandler(object sender, BoolValueChangedEventArgs e);

    public class BoolValueChangedEventArgs : EventArgs
    {
        public BoolValueChangedEventArgs(bool newValue)
        {
            this.m_OldValue = !newValue;
            this.m_NewValue = newValue;
        }

        public BoolValueChangedEventArgs(bool oldValue, bool newValue)
        {
            this.m_OldValue = oldValue;
            this.m_NewValue = newValue;
        }

        bool m_OldValue;
        public bool OldValue
        {
            get { return m_OldValue; }
        }

        bool m_NewValue;
        public bool NewValue
        {
            get { return m_NewValue; }
        }
    }

    //
    //
    //

    public delegate void IntValueChangedHandler(object sender, IntValueChangedEventArgs e);

    public class IntValueChangedEventArgs : EventArgs
    {
        public IntValueChangedEventArgs(int oldValue, int newValue)
        {
            this.m_OldValue = oldValue;
            this.m_NewValue = newValue;
        }

        int m_OldValue = -1;
        public int OldValue
        {
            get { return m_OldValue; }
        }

        int m_NewValue = -1;
        public int NewValue
        {
            get { return m_NewValue; }
        }
    }

    //
    //
    //

    public delegate void FloatValueChangedHandler(object sender, FloatValueChangedEventArgs e);

    public class FloatValueChangedEventArgs : EventArgs
    {
        public FloatValueChangedEventArgs(float oldValue, float newValue)
        {
            this.m_OldValue = oldValue;
            this.m_NewValue = newValue;
        }

        float m_OldValue = -1;
        public float OldValue
        {
            get { return m_OldValue; }
        }

        float m_NewValue = -1;
        public float NewValue
        {
            get { return m_NewValue; }
        }
    }

    //
    //
    //


    public delegate void DoubleValueChangedHandler(object sender, DoubleValueChangedEventArgs e);

    public class DoubleValueChangedEventArgs : EventArgs
    {
        public DoubleValueChangedEventArgs(double oldValue, double newValue)
        {
            this.m_OldValue = oldValue;
            this.m_NewValue = newValue;
        }

        double m_OldValue = -1;
        public double OldValue
        {
            get { return m_OldValue; }
        }

        double m_NewValue = -1;
        public double NewValue
        {
            get { return m_NewValue; }
        }
    }

    //
    //
    //

    /// <summary>
    /// 属性改变委托
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void PropertyChangedEventHandler(object sender, PropertyChangedEventArgs e);

    /// <summary>
    /// 属性改变 参数
    /// </summary>
    public class PropertyChangedEventArgs : EventArgs
    {
        public PropertyChangedEventArgs(Type propertyType, object oldValue, object newValue)
        {
            this.m_PropertyType = propertyType;
            this.m_OldValue = oldValue;
            this.m_NewValue = newValue;
        }

        Type m_PropertyType;
        public Type PropertyType
        {
            get { return m_PropertyType; }
        }

        object m_OldValue;
        public object OldValue
        {
            get { return m_NewValue; }
        }

        object m_NewValue;
        public object NewValue
        {
            get { return m_NewValue; }
        }
    }
}
