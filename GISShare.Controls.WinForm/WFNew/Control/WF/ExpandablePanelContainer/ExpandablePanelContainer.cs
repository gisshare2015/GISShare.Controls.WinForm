using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    [ToolboxItem(true), Designer(typeof(GISShare.Controls.WinForm.WFNew.Design.ExpandablePanelContainerDesigner))]
    public class ExpandablePanelContainer : WFNew.AreaControl, ICollectionObjectDesignHelper
    {
        public event IntValueChangedHandler ExpandablePanelSelectedIndexChanged;

        public ExpandablePanelContainer()
            : base()
        {
            this.m_ExpandablePanelCollection = new ExpandablePanelCollection(this);
        }

        #region IOwner
        public override bool LockHeight
        {
            get { return false; }
        }

        public override bool LockWith
        {
            get { return false; }
        }

        public override object Clone()
        {
            return new ExpandablePanelContainer();
        }
        #endregion

        #region ICollectionObjectDesignHelper
        System.Collections.IList ICollectionObjectDesignHelper.List { get { return this.ExpandablePanels; } }

        bool ICollectionObjectDesignHelper.ExchangeItem(object item1, object item2) { return this.ExpandablePanels.ExchangeItem(item1, item2); }
        #endregion

        bool m_IsCaptionExpandArea = false;
        [Browsable(true), DefaultValue(false), Description("点击标题区实现折叠效果"), Category("状态")]
        public bool IsCaptionExpandArea
        {
            get { return m_IsCaptionExpandArea; }
            set { m_IsCaptionExpandArea = value; }
        }

        private Orientation m_eOrientation = Orientation.Vertical;
        [Browsable(true), DefaultValue(Orientation.Vertical), Description("布局方式"), Category("布局")]
        public Orientation eOrientation
        {
            get { return m_eOrientation; }
            set
            {
                if (m_eOrientation == value) return;
                //
                m_eOrientation = value;
                //
                this.Relayout();
            }
        }

        private ExpandablePanelCollection m_ExpandablePanelCollection = null;
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Bindable(true), Localizable(true), Description("ExpandablePanel收集器"), Category("集合")]
        public ExpandablePanelCollection ExpandablePanels
        {
            get { return m_ExpandablePanelCollection; }
        }

        [Browsable(true), DefaultValue(0), Description("伸展面板选择索引"), Category("属性")]
        public int ExpandablePanelSelectedIndex
        {
            get
            {
                int iIndex = -1;
                for (int i = 0; i < this.ExpandablePanels.Count; i++)
                {
                    ExpandablePanel one = this.ExpandablePanels[i];
                    if (one == null || !one.Visible || !one.IsExpand) continue;
                    //
                    if (iIndex == -1) iIndex = i;
                    else ((ISetExpandableCaptionPanelHelper)one).SetIsExpand(false);
                }
                if (iIndex == -1)
                {
                    foreach (ExpandablePanel one in this.ExpandablePanels)
                    {
                        iIndex++;
                        if (one == null || !one.Visible) continue;
                        //
                        ((ISetExpandableCaptionPanelHelper)one).SetIsExpand(true);
                        break;
                    }
                }
                return iIndex;
            }
            set
            {
                if (value < 0 || value >= this.ExpandablePanels.Count) return;
                //
                int iOld = this.ExpandablePanelSelectedIndex;
                if (iOld == value) return;
                //
                for (int i = 0; i < this.ExpandablePanels.Count; i++)
                {
                    ExpandablePanel one = this.ExpandablePanels[i];
                    if (one == null || !one.Visible) continue;
                    //
                    ((ISetExpandableCaptionPanelHelper)one).SetIsExpand(value == i);
                }
                //
                this.OnExpandablePanelSelectedIndexChanged(new IntValueChangedEventArgs(iOld, value));
            }
        }

        [Browsable(true), DefaultValue(false), Description("当前选中的伸展面板"), Category("属性")]
        public ExpandablePanel SelectedExpandablePanel
        {
            get
            {
                ExpandablePanel expandablePanel = null;
                foreach (ExpandablePanel one in this.ExpandablePanels)
                {
                    if (one == null || !one.Visible || !one.IsExpand) continue;
                    //
                    if (expandablePanel == null) expandablePanel = one;
                    else ((ISetExpandableCaptionPanelHelper)one).SetIsExpand(false);
                }
                if (expandablePanel == null) 
                {
                    foreach (ExpandablePanel one in this.ExpandablePanels)
                    {
                        if (one == null || !one.Visible) continue;
                        //
                        ((ISetExpandableCaptionPanelHelper)one).SetIsExpand(true);
                        expandablePanel = one;
                        break;
                    }
                }
                return expandablePanel;
            }
        }

        public void SetSelectedExpandablePanel(ExpandablePanel expandablePanel)
        {
            this.ExpandablePanelSelectedIndex = this.ExpandablePanels.IndexOf(expandablePanel);
        }

        public override Rectangle DisplayRectangle
        {
            get
            {
                return this.ShowOutLine ? new Rectangle(1, 1, base.DisplayRectangle.Width - 2, base.DisplayRectangle.Height - 2) : base.DisplayRectangle;
            }
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            //
            ExpandablePanel expandablePanel = e.Control as ExpandablePanel;
            if (expandablePanel == null) this.Controls.Remove(e.Control);
            //
            if (this.ExpandablePanels.Count > 0)
            {
                this.ResetDefaultIndex();
                //
                switch (this.eOrientation)
                {
                    case Orientation.Vertical:
                        this.Width++;
                        this.Width--;
                        break;
                    case Orientation.Horizontal:
                    default:
                        this.Height++;
                        this.Height--;
                        break;
                }
            }
        }

        protected override void OnControlRemoved(ControlEventArgs e)
        {
            base.OnControlRemoved(e);
            //
            if (this.ExpandablePanels.Count > 0) this.ResetDefaultIndex();
        }

        public void ResetDefaultIndex()
        {
            ExpandablePanel expandablePanel = null;
            foreach (ExpandablePanel one in this.ExpandablePanels)
            {
                if (one == null || !one.Visible || !one.IsExpand) continue;
                //
                if (expandablePanel == null) expandablePanel = one;
                else ((ISetExpandableCaptionPanelHelper)one).SetIsExpand(false);
            }
            if (expandablePanel == null)
            {
                foreach (ExpandablePanel one in this.ExpandablePanels)
                {
                    if (one == null || !one.Visible) continue;
                    //
                    ((ISetExpandableCaptionPanelHelper)one).SetIsExpand(true);
                    expandablePanel = one;
                    break;
                }
            }
            //
            if (expandablePanel != null) ((ISetExpandableCaptionPanelHelper)expandablePanel).ResetSize();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            //
            this.Relayout();
        }
        private void Relayout()
        {
            switch (this.eOrientation)
            {
                case Orientation.Horizontal:
                    foreach (ISetExpandableCaptionPanelHelper one in this.ExpandablePanels)
                    {
                        if (one != null) { one.SetDockStyle(DockStyle.Left); }
                    }
                    break;
                case Orientation.Vertical:
                    foreach (ISetExpandableCaptionPanelHelper one in this.ExpandablePanels)
                    {
                        if (one != null) { one.SetDockStyle(DockStyle.Top); }
                    }
                    break;
                default:
                    break;
            }
            //
            ISetExpandableCaptionPanelHelper pSetExpandableCaptionPanelHelper = this.SelectedExpandablePanel as ISetExpandableCaptionPanelHelper;
            if (pSetExpandableCaptionPanelHelper == null) return;
            pSetExpandableCaptionPanelHelper.ResetSize();
        }

        public Size GetWorkRegionSize()
        {
            int iCaptionHeight = 0;
            ExpandablePanel expandablePanel = this.SelectedExpandablePanel;
            switch (this.eOrientation)
            {
                case Orientation.Vertical:
                    foreach (ExpandablePanel one in this.ExpandablePanels)
                    {
                        if (one == null || !one.Visible || one == expandablePanel) continue;
                        iCaptionHeight += one.CaptionHeight + 2;
                    }
                    return new Size(this.DisplayRectangle.Width, this.DisplayRectangle.Height - iCaptionHeight);
                case Orientation.Horizontal:
                default:
                    foreach (ExpandablePanel one in this.ExpandablePanels)
                    {
                        if (one == null || !one.Visible || one == expandablePanel) continue;
                        iCaptionHeight += one.CaptionHeight + 2;
                    }
                    return new Size(this.DisplayRectangle.Width - iCaptionHeight, this.DisplayRectangle.Height);
            }
        }

        //
        protected virtual void OnExpandablePanelSelectedIndexChanged(IntValueChangedEventArgs e)
        {
            if (this.ExpandablePanelSelectedIndexChanged != null) this.ExpandablePanelSelectedIndexChanged(this, e);
        }
        
        //
        //
        //

        public class ExpandablePanelCollection : WFNew.FlexibleList<ExpandablePanel>
        {
            private Control owner = null;

            internal ExpandablePanelCollection(Control ctr)
            {
                this.owner = ctr;
            }

            public override int Add(ExpandablePanel value)
            {
                if (this.Locked) return -1;
                //
                this.owner.Controls.Add(value);
                return this.owner.Controls.IndexOf(value);
            }

            public override void Insert(int index, ExpandablePanel value)
            {
                if (this.Locked) return;
                //
                if ((index < 0) || (index >= this.owner.Controls.Count)) return;
                //
                this.owner.Controls.Add(value);
                this.owner.Controls.SetChildIndex(value, index);
            }

            public override bool Contains(ExpandablePanel value)
            {
                return this.owner.Controls.Contains(value);
            }

            public override int IndexOf(ExpandablePanel value)
            {
                return this.owner.Controls.IndexOf(value);
            }

            public override void Remove(ExpandablePanel value)
            {
                if (this.Locked) return;
                //
                this.owner.Controls.Remove(value);
            }

            public override void RemoveAt(int index)
            {
                if (this.Locked) return;
                //
                this.owner.Controls.RemoveAt(index);
            }

            public override void Clear()
            {
                if (this.Locked) return;
                //
                this.owner.Controls.Clear();
            }

            [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public override ExpandablePanel this[int index]
            {
                get
                {
                    return this.owner.Controls[index] as ExpandablePanel;
                }
                set
                {
                    if (this.Locked) return;
                    //
                    this.RemoveAt(index);
                    this.Insert(index, value);
                }
            }

            public override bool ExchangeItemT(ExpandablePanel item1, ExpandablePanel item2)
            {
                if (this.Locked) return false;
                //
                if (item1 == null || item2 == null || item1 == item2) return false;
                //
                int index1 = this.owner.Controls.IndexOf(item1);
                int index2 = this.owner.Controls.IndexOf(item2);
                //
                if (index1 == index2) return false;
                if ((index1 < 0) || (index1 >= this.owner.Controls.Count)) return false;
                if ((index2 < 0) || (index2 >= this.owner.Controls.Count)) return false;
                //
                if (index1 < index2)
                {
                    this.owner.Controls.SetChildIndex(item2, index1);
                    this.owner.Controls.SetChildIndex(item1, index2);
                }
                else
                {
                    this.owner.Controls.SetChildIndex(item1, index2);
                    this.owner.Controls.SetChildIndex(item2, index1);
                }
                //
                return true;
            }

            public override int Count
            {
                get
                {
                    return this.owner.Controls.Count;
                }
            }

            public override IEnumerator GetEnumerator()
            {
                return this.owner.Controls.GetEnumerator();
            }

            public ExpandablePanel this[string name]
            {
                get
                {
                    foreach (ExpandablePanel one in this.owner.Controls)
                    {
                        if (one.Name == name) return one;
                    }
                    //
                    return null;
                }
            }
        }
    }

}
