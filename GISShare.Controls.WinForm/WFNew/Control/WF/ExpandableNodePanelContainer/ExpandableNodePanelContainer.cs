using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    [ToolboxItem(true), Designer(typeof(GISShare.Controls.WinForm.WFNew.Design.ExpandableNodePanelContainerDesigner))]
    public class ExpandableNodePanelContainer : WFNew.AreaControlCC, ICollectionObjectDesignHelper
    {
        public ExpandableNodePanelContainer()
        {
            this.m_ExpandableNodePanelCollection = new ExpandableNodePanelCollection(this);
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
            return new ExpandableCaptionPanel();
        }
        #endregion

        private bool m_AutoResize = false;
        [Browsable(true), DefaultValue(false), Description("自动调节尺寸"), Category("布局")]
        public bool AutoResize
        {
            get 
            {
                switch (this.Dock) 
                {
                    case DockStyle.Left:
                    case DockStyle.Right:
                    case DockStyle.Fill:
                        return false;
                }
                //
                return m_AutoResize; 
            }
            set
            {
                if (m_AutoResize == value) return;
                //
                m_AutoResize = value;
                this.UpdatePanelPositions();
            }
        }

        [Browsable(true), DefaultValue(typeof(Size), "10, 10"), Description("面板间距"), Category("布局")]
        private Size m_Space = new Size(10, 10);
        public Size Space
        {
            get { return m_Space; }
            set
            {
                if (m_Space == value) return;
                //
                m_Space = value;
                this.UpdatePanelPositions();
            }
        }
        
        private ExpandableNodePanelCollection m_ExpandableNodePanelCollection = null;
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Bindable(true), Localizable(true), Description("ExpandableNodePanel收集器"), Category("集合")]
        public ExpandableNodePanelCollection ExpandableNodePanels
        {
            get { return m_ExpandableNodePanelCollection; }
        }

        #region ICollectionObjectDesignHelper
        System.Collections.IList ICollectionObjectDesignHelper.List { get { return this.ExpandableNodePanels; } }

        bool ICollectionObjectDesignHelper.ExchangeItem(object item1, object item2) { return this.ExpandableNodePanels.ExchangeItem(item1, item2); }
        #endregion

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override bool AutoScroll
        {
            get { return !this.AutoResize; }
            set { base.AutoScroll = value; }
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
            ExpandableNodePanel expandablePanel = e.Control as ExpandableNodePanel;
            if (expandablePanel == null) this.Controls.Remove(e.Control);
            //
            //e.Control.PanelContainer = this;
            this.UpdatePanelPositions();
        }

        protected override void OnControlRemoved(ControlEventArgs e)
        {
            base.OnControlRemoved(e);
            this.UpdatePanelPositions();
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            this.UpdatePanelPositions();
            base.OnLayout(levent);
        }

        protected virtual void UpdatePanelPositions()
        {
            int width = this.Space.Width;
            int y = this.Space.Height + base.AutoScrollPosition.Y;
            foreach (ExpandableNodePanel one in this.ExpandableNodePanels)
            {
                if (!one.Visible) continue;
                //
                one.SetBounds(width, y, 0, 0, BoundsSpecified.Location);
                one.SetBounds(0, 0, base.ClientSize.Width - (2 * this.Space.Width), 0, BoundsSpecified.Width);
                y = (y + one.Height) + this.Space.Height;
            }
            //
            if(this.AutoResize) this.Height = y;
            //
            base.Invalidate();
        }

        //
        //
        //

        public class ExpandableNodePanelCollection : WFNew.FlexibleList<ExpandableNodePanel>
        {
            private Control owner = null;

            internal ExpandableNodePanelCollection(Control ctr)
            {
                this.owner = ctr;
            }

            public override int Add(ExpandableNodePanel value)
            {
                if (this.Locked) return -1;
                //
                this.owner.Controls.Add(value);
                return this.owner.Controls.IndexOf(value);
            }

            public override void Insert(int index, ExpandableNodePanel value)
            {
                if (this.Locked) return;
                //
                if ((index < 0) || (index >= this.owner.Controls.Count)) return;
                //
                this.owner.Controls.Add(value);
                this.owner.Controls.SetChildIndex(value, index);
            }

            public override bool Contains(ExpandableNodePanel value)
            {
                return this.owner.Controls.Contains(value);
            }

            public override int IndexOf(ExpandableNodePanel value)
            {
                return this.owner.Controls.IndexOf(value);
            }

            public override void Remove(ExpandableNodePanel value)
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
            public override ExpandableNodePanel this[int index]
            {
                get
                {
                    return this.owner.Controls[index] as ExpandableNodePanel;
                }
                set
                {
                    if (this.Locked) return;
                    //
                    this.RemoveAt(index);
                    this.Insert(index, value);
                }
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

            public override bool ExchangeItemT(ExpandableNodePanel item1, ExpandableNodePanel item2)
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

            public ExpandableNodePanel this[string name]
            {
                get
                {
                    foreach (ExpandableNodePanel one in this.owner.Controls)
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
