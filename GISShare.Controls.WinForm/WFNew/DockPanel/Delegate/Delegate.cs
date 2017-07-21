using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew.DockPanel
{
    //public delegate void VisibleExValueSetEventHandler(object sender, VisibleExValueSetEventArgs e);

    //public class VisibleExValueSetEventArgs : EventArgs
    //{
    //    public VisibleExValueSetEventArgs(bool oldValue, bool newValue)
    //    {
    //        this._OldValue = oldValue;
    //        this._NewValue = newValue;
    //    }

    //    //public VisibleExValueSetEventArgs(bool oldValue, bool newValue, object other)
    //    //{
    //    //    this._OldValue = oldValue;
    //    //    this._NewValue = newValue;
    //    //    this._Other = other;
    //    //}

    //    bool _OldValue;
    //    //public bool OldValue
    //    //{
    //    //    get { return _OldValue; }
    //    //}

    //    bool _NewValue;
    //    public bool NewValue
    //    {
    //        get { return _NewValue; }
    //    }

    //    object _Other;
    //    //public object Other
    //    //{
    //    //    get { return _Other; }
    //    //    set { _Other = value; }
    //    //}
    //}

    //
    //
    //

    public delegate void PanelNodeStateChangedEventHandler(object sender, PanelNodeStateChangedEventArgs e);

    public class PanelNodeStateChangedEventArgs : EventArgs
    {
        public PanelNodeStateChangedEventArgs(PanelNodeState oldValue, PanelNodeState newValue)
        {
            this._OldValue = oldValue;
            this._NewValue = newValue;
        }

        public PanelNodeStateChangedEventArgs(PanelNodeState oldValue, PanelNodeState newValue, object other)
        {
            this._OldValue = oldValue;
            this._NewValue = newValue;
            this._Other = other;
        }

        PanelNodeState _OldValue;
        public PanelNodeState OldValue
        {
            get { return _OldValue; }
        }

        PanelNodeState _NewValue;
        public PanelNodeState NewValue
        {
            get { return _NewValue; }
        }

        object _Other;
        public object Other
        {
            get { return _Other; }
            set { _Other = value; }
        }
    }
}
