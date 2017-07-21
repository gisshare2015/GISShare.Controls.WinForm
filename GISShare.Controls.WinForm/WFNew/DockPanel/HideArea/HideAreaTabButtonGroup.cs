using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LiuZhenHong.Controls.DockPanel
{
    [ToolboxItem(false)]
    class HideAreaTabButtonGroup : Control
    {
        private const int CRT_HEIGHT = 23;

        private DockPanel m_DockPanel = null;                                                                  //记录其所对应的停靠面板
        private HideAreaTabButtonCollection m_HideAreaTabButtons = null;                                       //隐藏按钮收集器
        private System.Windows.Forms.TabAlignment m_AlignmentStyle = System.Windows.Forms.TabAlignment.Bottom; //隐藏按钮组的绘制状态

        public HideAreaTabButtonGroup(DockPanel dockPanel)
        {
            base.Dock = DockStyle.None;
            base.Visible = true;
            //
            this.m_DockPanel = dockPanel;
            this.m_HideAreaTabButtons = new HideAreaTabButtonCollection(this);
            //
            this.SetDockPanelHideAreaControl();
        }
        private void SetDockPanelHideAreaControl()//设置隐藏按钮组并将其加载到对应的隐藏区内（在构造函数里完成）
        {
            DockStyle eDockStyle;
            DockAreaStyle eDockAreaStyle = this.m_DockPanel.GetDockAreaStyle(out eDockStyle);
            switch (eDockStyle)//添加后会自动处理布局信息
            {
                case DockStyle.Top:
                    this.m_AlignmentStyle = TabAlignment.Top;
                    this.DockPanelManager.DockPanelHideAreaTop.AddHideAreaTabButtonGroup(this);
                    break;
                case DockStyle.Left:
                    this.m_AlignmentStyle = TabAlignment.Left;
                    this.DockPanelManager.DockPanelHideAreaLeft.AddHideAreaTabButtonGroup(this);
                    break;
                case DockStyle.Right:
                    this.m_AlignmentStyle = TabAlignment.Right;
                    this.DockPanelManager.DockPanelHideAreaRight.AddHideAreaTabButtonGroup(this);
                    break;
                case DockStyle.Bottom:
                    this.m_AlignmentStyle = TabAlignment.Bottom;
                    this.DockPanelManager.DockPanelHideAreaBottom.AddHideAreaTabButtonGroup(this);
                    break;
                default:
                    break;
            }
            //
            //
            //
            for (int i = 0; i < this.m_DockPanel.BasePanelCount; i++)
            {
                BasePanel temp = this.m_DockPanel.GetBasePanel(i) as BasePanel;
                if (temp == null) continue;
                HideAreaTabButton hideAreaTabButton = new HideAreaTabButton(i, temp.Text, temp.Image, this.eAlignmentStyle);
                this.AddHideAreaTabButton(hideAreaTabButton);
                hideAreaTabButton.MouseClick += new MouseEventHandler(HideAreaTabButton_MouseClick);
                hideAreaTabButton.MouseHover += new EventHandler(HideAreaTabButtonGroupItem_MouseHover);
                hideAreaTabButton.MouseLeave += new EventHandler(HideAreaTabButtonGroupItem_MouseLeave);
            }
        }

        #region 覆盖
        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            //
            HideAreaTabButton hideAreaTabButton = e.Control as HideAreaTabButton;
            if (hideAreaTabButton == null) { this.Controls.Remove(e.Control); return; }
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            int iLayoutSize = this.GetLayoutSize();
            switch (this.eAlignmentStyle)
            {
                case TabAlignment.Top:
                case TabAlignment.Bottom:
                    this.Height = CRT_HEIGHT;
                    this.Width = iLayoutSize;
                    break;
                case TabAlignment.Left:
                case TabAlignment.Right:
                    this.Width = CRT_HEIGHT;
                    this.Height = iLayoutSize;
                    break;
                default:
                    break;
            }
            this.Relayout();
        }
        private int GetLayoutSize()//计算布局尺寸
        {
            int iLayoutSize = 0;
            foreach (object one in this.m_HideAreaTabButtons)
            {
                HideAreaTabButton temp = one as HideAreaTabButton;
                if (temp == null) continue;
                iLayoutSize += temp.LayoutSize();
            }
            return (iLayoutSize + 15);
        }
        private void Relayout()//布局
        {
            switch (this.eAlignmentStyle)
            {
                case TabAlignment.Top:
                case TabAlignment.Bottom:
                    this.RelayoutHorizontal();
                    break;
                case TabAlignment.Left:
                case TabAlignment.Right:
                    this.RelayoutVertical();
                    break;
                default:
                    break;
            }
        }
        private void RelayoutHorizontal()
        {
            if (this.HideAreaTabButtonCount <= 0) return;
            //
            int iLayoutSize = 0;
            for (int i = 0; i < this.m_HideAreaTabButtons.Count; i++)
            {
                HideAreaTabButton temp = this.m_HideAreaTabButtons[i] as HideAreaTabButton;
                if (temp == null) continue;
                temp.Location = new Point(iLayoutSize, 0);
                iLayoutSize += temp.LayoutSize();
            }
        }
        private void RelayoutVertical()
        {
            if (this.HideAreaTabButtonCount <= 0) return;
            //
            int iLayoutSize = 0;
            for (int i = 0; i < this.m_HideAreaTabButtons.Count; i++)
            {
                HideAreaTabButton temp = this.m_HideAreaTabButtons[i] as HideAreaTabButton;
                if (temp == null) continue;
                temp.Location = new Point(0, iLayoutSize);
                iLayoutSize += temp.LayoutSize();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            //
            if (this.DockPanelManager != null)
            {
                e.Graphics.FillRectangle(new SolidBrush(LiuZhenHong.Controls.Renderer.RendererManager.Renderer.ColorTableEx.PanelBackgroundMiddle), this.DisplayRectangle);
            }
        }
        #endregion

        #region 关联事件
        private void HideAreaTabButton_MouseClick(object sender, MouseEventArgs e)
        {
            HideAreaTabButton hideAreaTabButton = sender as HideAreaTabButton;
            if (hideAreaTabButton == null) return;
            if(this.m_DockPanel.BasePanelSelectIndex != hideAreaTabButton.iID) this.m_DockPanel.BasePanelSelectIndex = hideAreaTabButton.iID;
            //
            this.DockPanel.SetActiveState();
        }

        private void HideAreaTabButtonGroupItem_MouseHover(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            //
            HideAreaTabButton hideAreaTabButton = sender as HideAreaTabButton;
            if (hideAreaTabButton == null) return;
            if (this.m_DockPanel.BasePanelSelectIndex != hideAreaTabButton.iID) this.m_DockPanel.BasePanelSelectIndex = hideAreaTabButton.iID;
            //
            this.ShowDockPanelHidePanel(false);
        }

        private void HideAreaTabButtonGroupItem_MouseLeave(object sender, EventArgs e)
        {
            if (this.DockPanel.bActive) return;
            //
            System.Threading.Thread.Sleep(200);
            //
            if (this.DockPanel.DockPanelManager.DockPanelHidePanel.ContainsMousePoint(MousePosition))
            {
                this.DockPanel.DockPanelManager.DockPanelHidePanel.StartTimer(); return;
            }
            //
            this.CloseDockPanelHidePanel();
        }
        #endregion

        #region 属性
        [Browsable(false), DefaultValue(DockStyle.None)]
        public override DockStyle Dock
        {
            get
            {
                return base.Dock;
            }
            set
            {
                base.Dock = DockStyle.None;
            }
        }

        [Browsable(false), DefaultValue(true)]
        public new bool Visible
        {
            get
            {
                return base.Visible;
            }
            set
            {
                base.Visible = true;
            }
        }

        [Browsable(false)]
        public System.Windows.Forms.TabAlignment eAlignmentStyle
        {
            get { return m_AlignmentStyle; }
        }

        [Browsable(false)]
        public int HideAreaTabButtonCount//记录隐藏按钮组内的隐藏按钮个数
        {
            get
            {
                return this.m_HideAreaTabButtons.Count;
            }
        }

        [Browsable(false)]
        public DockPanelHideArea DockPanelHideArea//记录其所在的隐藏区
        {
            get { return base.Parent as DockPanelHideArea; }
        }

        [Browsable(false)]
        public DockPanel DockPanel
        {
            get { return m_DockPanel; }
        }

        [Browsable(false)]
        public DockPanelManager DockPanelManager
        {
            get
            {
                if (this.m_DockPanel == null) return null;
                return this.m_DockPanel.DockPanelManager;
            }
        }
        #endregion

        internal int LayoutSize()//获取布局寸（活动尺寸）
        {
            switch (this.eAlignmentStyle)
            {
                case TabAlignment.Top:
                case TabAlignment.Bottom:
                    return this.Width;
                case TabAlignment.Left:
                case TabAlignment.Right:
                    return this.Height;
                default:
                    return 0;
            }
        }

        #region 公开函数
        public void ShowDockPanelHidePanel(bool bActive)//展现隐藏面板
        {
            if (bActive) this.DockPanel.SetActiveState();
            this.DockPanel.DockPanelManager.DockPanelHidePanel.Show(this.DockPanel, this);
        }

        public void CloseDockPanelHidePanel()//关闭隐藏面板
        {
            this.DockPanel.DockPanelManager.DockPanelHidePanel.Close();
        }

        public void AddHideAreaTabButton(HideAreaTabButton hideAreaTabButton)
        {
            this.m_HideAreaTabButtons.Add(hideAreaTabButton);
        }

        public void RemoveHideAreaTabButton(HideAreaTabButton hideAreaTabButton)
        {
            this.m_HideAreaTabButtons.Remove(hideAreaTabButton);
        }

        public bool ContainHideAreaTabButtons(HideAreaTabButton hideAreaTabButton)
        {
            return this.m_HideAreaTabButtons.Contains(hideAreaTabButton);
        }

        public int IndexOfHideAreaTabButton(HideAreaTabButton hideAreaTabButton)
        {
            return this.m_HideAreaTabButtons.IndexOf(hideAreaTabButton);
        }

        public void ClearHideAreaTabButton()
        {
            this.m_HideAreaTabButtons.Clear();
        }
        #endregion
        //
        //
        //

        /// <summary>
        /// 隐藏按钮收集器
        /// </summary>
        class HideAreaTabButtonCollection : IList, ICollection, IEnumerable
        {
            private HideAreaTabButtonGroup owner;

            public HideAreaTabButtonCollection(HideAreaTabButtonGroup hideAreaTabButtonGroup)
            {
                this.owner = hideAreaTabButtonGroup;
            }

            public int Add(object value)
            {
                HideAreaTabButton temp = value as HideAreaTabButton;
                if (temp == null) return -1;
                this.owner.Controls.Add(temp);
                return this.Count - 1;
            }

            public void Clear()
            {
                this.owner.Controls.Clear();
            }

            public bool Contains(object value)
            {
                return this.owner.Controls.Contains(value as Control);
            }

            public int IndexOf(object value)
            {
                return this.owner.Controls.IndexOf(value as Control);
            }

            public void Insert(int index, object value)
            {
                HideAreaTabButton temp = value as HideAreaTabButton;
                if (temp == null) return;
                this.owner.Controls.Add(temp);
                this.owner.Controls.SetChildIndex(temp, index);
            }

            public void Remove(object value)
            {
                HideAreaTabButton temp = value as HideAreaTabButton;
                if (temp == null) return;
                this.owner.Controls.Remove(temp);
            }

            public void RemoveAt(int index)
            {
                this.Remove(this[index]);
            }

            public int Count
            {
                get
                {
                    return this.owner.Controls.Count;
                }
            }

            public IEnumerator GetEnumerator()
            {
                return this.owner.Controls.GetEnumerator();
            }

            void ICollection.CopyTo(Array destination, int index)
            {
                this.owner.Controls.CopyTo(destination, 0);
            }

            protected virtual object[] GetItems()
            {
                HideAreaTabButton[] destinationArray = new HideAreaTabButton[this.Count];
                if (this.Count > 0)
                {
                    this.owner.Controls.CopyTo(destinationArray, 0);
                }
                return destinationArray;
            }

            public bool IsReadOnly
            {
                get
                {
                    return false;
                }
            }

            // Properties
            [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public virtual object this[int index]
            {
                get
                {
                    if ((index < 0) || (index >= this.Count))
                    {
                        return null;
                    }
                    return this.owner.Controls[index];
                }
                set
                {
                    this.RemoveAt(index);
                    this.Insert(index, value);
                }
            }

            bool ICollection.IsSynchronized
            {
                get
                {
                    return false;
                }
            }

            object ICollection.SyncRoot
            {
                get
                {
                    return this;
                }
            }

            bool IList.IsFixedSize
            {
                get
                {
                    return false;
                }
            }
        }

    }
}
