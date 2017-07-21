using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace GISShare.Controls.WinForm.WFNew.DockPanel
{
    [Designer(typeof(GISShare.Controls.WinForm.WFNew.DockPanel.Design.DockPanelManagerDesigner)), ToolboxBitmap(typeof(DockPanelManager), "DockPanelManager.bmp")]
    public class DockPanelManager : Component
    {
        #region 私有变量
        private bool m_CompletedAddSetComponent = false;                             //记录是否成完成相关组件的加载和设置
        private bool m_IsDockLayoutState = false;                                    //当前是否是布局状态
        //
        private Form m_ParentForm = null;                                            //它所依附的父窗体
        //
        private DocumentArea m_DocumentArea = null;                        //文档区面板（当为单文档页时添加它，用来管理整个文档区的控件，以减少闪烁）
        //
        private DockPanelHidePanel m_DockPanelHidePanel = null;                      //隐藏面板，展现隐藏后的停靠面板
        //
        private DockPanelHideAreaTop m_DockPanelHideAreaTop = null;                  //顶部隐藏区
        private DockPanelHideAreaLeft m_DockPanelHideAreaLeft = null;                //左边隐藏区
        private DockPanelHideAreaRight m_DockPanelHideAreaRight = null;              //右边隐藏区
        private DockPanelHideAreaBottom m_DockPanelHideAreaBottom = null;            //底部隐藏区
        //
        private BasePanelCollection m_BasePanelCollection = null;                    //基础面板收集器（由系统辅助管理 所新成员必须加载到收集器中否则会出现异常）
        private DockPanelCollection m_DockPanelCollection = null;                    //停靠面板收集器（由系统辅助管理 所新成员必须加载到收集器中否则会出现异常）
        private DockPanelContainerCollection m_DockPanelContainerCollection = null;  //停靠面板容器收集器（由系统自动 添加、删除、注销 资源）
        private DockPanelDockAreaCollection m_DockPanelDockAreaCollection = null;    //停靠面板停靠区收集器（由系统自动 添加、删除、注销 资源）
        private DockPanelFloatFormCollection m_DockPanelFloatFormCollection = null;  //停靠面板浮动窗体收集器（由系统自动 添加、删除、注销 资源）
        #endregion

        public DockPanelManager()
        {
            this.m_BasePanelCollection = new BasePanelCollection();
            this.m_DockPanelCollection = new DockPanelCollection(this);
            this.m_DockPanelContainerCollection = new DockPanelContainerCollection(this);
            this.m_DockPanelDockAreaCollection = new DockPanelDockAreaCollection(this);
            this.m_DockPanelFloatFormCollection = new DockPanelFloatFormCollection(this);
            //在构造函数中加载实例化，以防保存布局文件时出现异常
            this.m_DockPanelHideAreaLeft = new DockPanelHideAreaLeft(this);
            this.m_DockPanelHideAreaRight = new DockPanelHideAreaRight(this);
            this.m_DockPanelHideAreaTop = new DockPanelHideAreaTop(this.DockPanelHideAreaLeft, this.DockPanelHideAreaRight, this);
            this.m_DockPanelHideAreaBottom = new DockPanelHideAreaBottom(this.DockPanelHideAreaLeft, this.DockPanelHideAreaRight, this);
        }

        #region 属性
        bool m_ShowDocumentCloseButton = false;
        [Browsable(true), DefaultValue(false)]
        public bool ShowDocumentCloseButton
        {
            get { return m_ShowDocumentCloseButton; }
            set { m_ShowDocumentCloseButton = value; }
        }

        [Browsable(false)]
        public bool CompletedAddSetComponent
        {
            get { return m_CompletedAddSetComponent; }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Form ParentForm//它所依附的父窗体
        {
            get { return m_ParentForm; }
            set { m_ParentForm = value; }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public DocumentArea DocumentArea//文档区面板（当为单文档页时添加它，用来管理整个文档区的控件，以减少闪烁）
        {
            get { return m_DocumentArea; }
            set { m_DocumentArea = value; }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Bindable(true), Localizable(true)]
        public BasePanelCollection BasePanels//基础面板收集器（由系统辅助管理 所新成员必须加载到收集器中否则会出现异常）
        {
            get { return m_BasePanelCollection; }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Bindable(true), Localizable(true)]
        public DockPanelCollection DockPanels//停靠面板收集器（由系统辅助管理 所新成员必须加载到收集器中否则会出现异常）
        {
            get { return m_DockPanelCollection; }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Bindable(true), Localizable(true)]
        public DockPanelContainerCollection DockPanelContainers//停靠面板容器收集器（由系统自动 添加、删除、注销 资源）
        {
            get { return m_DockPanelContainerCollection; }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Bindable(true), Localizable(true)]
        public DockPanelDockAreaCollection DockPanelDockAreas//停靠面板停靠区收集器（由系统自动 添加、删除、注销 资源）
        {
            get { return m_DockPanelDockAreaCollection; }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Bindable(true), Localizable(true)]
        internal DockPanelFloatFormCollection DockPanelFloatForms//停靠面板浮动窗体收集器（由系统自动 添加、删除、注销 资源）
        {
            get { return m_DockPanelFloatFormCollection; }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Bindable(true), Localizable(true)]
        public ICollection DockPanelFloatForms_Read
        {
            get { return this.DockPanelFloatForms; }
        }

        [Browsable(false)]
        internal bool IsDockLayoutState//当前是否是布局状态
        {
            get { return m_IsDockLayoutState; }
            set { m_IsDockLayoutState = value; }
        }

        [Browsable(false)]
        internal int HideAreaTabButtonGroupItemCount//获取当前隐藏按钮组个数
        {
            get
            {
                return (this.DockPanelHideAreaTop.BaseItems.Count +
                    this.DockPanelHideAreaLeft.BaseItems.Count +
                    this.DockPanelHideAreaRight.BaseItems.Count +
                    this.DockPanelHideAreaBottom.BaseItems.Count);
            }
        }

        [Browsable(false)]
        internal int DocumentAreaIndex//获取DocumentArea所在父窗体的索引
        {
            get
            {
                if (this.DocumentArea == null ||
                    this.ParentForm == null ||
                    !this.ParentForm.Controls.Contains(this.DocumentArea))
                    return -1;
                return this.ParentForm.Controls.GetChildIndex(this.DocumentArea);
            }
        }

        [Browsable(false)]
        internal int LastDockPanelDockAreaIndex//获取最后一个DockPanelDockArea所在父窗体的索引
        {
            get
            {
                int indexOfTop = -1;
                for (int i = this.ParentForm.Controls.Count - 1; i > 0; i--)
                {
                    if (this.ParentForm.Controls[i] is IDockArea)
                    { indexOfTop = i; break; }
                }
                return indexOfTop;
            }
        }

        [Browsable(false)]
        internal DockPanelHidePanel DockPanelHidePanel//隐藏面板，展现隐藏后的停靠面板
        {
            get
            {
                if (this.m_DockPanelHidePanel == null) return null;
                if (this.ParentForm.Controls.IndexOf(this.m_DockPanelHidePanel) != 0)
                { this.ParentForm.Controls.SetChildIndex(this.m_DockPanelHidePanel, 0); }
                return m_DockPanelHidePanel;
            }
        }

        [Browsable(false)]
        internal DockPanelHideAreaTop DockPanelHideAreaTop//顶部隐藏区
        {
            get { return m_DockPanelHideAreaTop; }
        }

        [Browsable(false)]
        internal DockPanelHideAreaLeft DockPanelHideAreaLeft//左边隐藏区
        {
            get { return m_DockPanelHideAreaLeft; }
        }

        [Browsable(false)]
        internal DockPanelHideAreaRight DockPanelHideAreaRight//右边隐藏区
        {
            get { return m_DockPanelHideAreaRight; }
        }

        [Browsable(false)]
        internal DockPanelHideAreaBottom DockPanelHideAreaBottom//底部隐藏区
        {
            get { return m_DockPanelHideAreaBottom; }
        }
        #endregion

        #region 关联事件
        private void ParentForm_ResizeEnd(object sender, EventArgs e)
        {
            int topHeight = 0;
            int bottomHeight = 0;
            int leftWidth = 0;
            int rightWidth = 0;
            this.GetFormHoldRegionSize(false, out topHeight, out bottomHeight, out leftWidth, out rightWidth);
            int minFormHeight = topHeight + bottomHeight + 60;
            int minFormWidth = leftWidth + rightWidth + 60;
            if (this.ParentForm.Height < minFormHeight) { this.ParentForm.Height = minFormHeight; }
            if (this.ParentForm.Width < minFormWidth) { this.ParentForm.Width = minFormWidth; }

        }
        private void GetFormHoldRegionSize(bool bExceptDockArea,
            out int topHeight, out int bottomHeight, out int leftWidth, out int rightWidth)//传出所有控件各种停靠状态的占据尺寸
        {
            topHeight = 0;
            bottomHeight = 0; 
            leftWidth = 0; 
            rightWidth = 0;
            if (bExceptDockArea)
            {
                for (int i = 0; i < this.ParentForm.Controls.Count; i++)
                {
                    if (!this.ParentForm.Controls[i].Visible) continue;
                    if (this.ParentForm.Controls[i] is IDockArea) continue;
                    //
                    switch (this.ParentForm.Controls[i].Dock)
                    {
                        case DockStyle.Top:
                            topHeight += this.ParentForm.Controls[i].Height;
                            break;
                        case DockStyle.Bottom:
                            bottomHeight += this.ParentForm.Controls[i].Height;
                            break;
                        case DockStyle.Left:
                            leftWidth += this.ParentForm.Controls[i].Width;
                            break;
                        case DockStyle.Right:
                            rightWidth += this.ParentForm.Controls[i].Width;
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < this.ParentForm.Controls.Count; i++)
                {
                    if (!this.ParentForm.Controls[i].Visible) continue;
                    //
                    switch (this.ParentForm.Controls[i].Dock)
                    {
                        case DockStyle.Top:
                            topHeight += this.ParentForm.Controls[i].Height;
                            break;
                        case DockStyle.Bottom:
                            bottomHeight += this.ParentForm.Controls[i].Height;
                            break;
                        case DockStyle.Left:
                            leftWidth += this.ParentForm.Controls[i].Width;
                            break;
                        case DockStyle.Right:
                            rightWidth += this.ParentForm.Controls[i].Width;
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        #endregion

        #region 公开函数
        public bool AddSetComponent()//加载并布局相关控件（即：加载四个隐藏区、加载隐藏面板。注：在窗体加载时调用它）
        {
            if (this.m_CompletedAddSetComponent) return false;
            if (this.ParentForm == null) { GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show("父窗体不存在，这样的可能会导致程序崩溃！"); return false; }
            //
            int iIndex = this.LastDockPanelDockAreaIndex + 1;
            this.ParentForm.Controls.Add(this.m_DockPanelHideAreaTop);
            this.ParentForm.Controls.SetChildIndex(this.m_DockPanelHideAreaTop, iIndex);
            this.ParentForm.Controls.Add(this.m_DockPanelHideAreaBottom);
            this.ParentForm.Controls.SetChildIndex(this.m_DockPanelHideAreaBottom, iIndex);
            this.ParentForm.Controls.Add(this.m_DockPanelHideAreaLeft);
            this.ParentForm.Controls.SetChildIndex(this.m_DockPanelHideAreaLeft, iIndex);
            this.ParentForm.Controls.Add(this.m_DockPanelHideAreaRight);
            this.ParentForm.Controls.SetChildIndex(this.m_DockPanelHideAreaRight, iIndex);
            //
            this.m_DockPanelHidePanel = new DockPanelHidePanel(this.ParentForm);
            this.ParentForm.Controls.Add(this.m_DockPanelHidePanel);
            this.ParentForm.Controls.SetChildIndex(this.m_DockPanelHidePanel, 0);
            this.ParentForm.ResizeEnd += new EventHandler(ParentForm_ResizeEnd);
            //
            this.m_CompletedAddSetComponent = true;
            //
            return true;
        }

        public DockPanel GetEmptyDockPanel()//获取一个闲置的停靠面板（如果停靠面板收集器里存在空子项则返回查询到的第一个对象，否则新建一个并讲其加载到收集器中去）
        {
            foreach (DockPanel one in this.DockPanels)
            {
                if (one.BasePanels.Count <= 0) { /*one.BasePanels.Clear();*/ return one; }
            }
            DockPanel dockPanel = new DockPanel();
            dockPanel.Name = "dockPanel" + (this.DockPanels.Count + 1);
            //dockPanel.SetDockPanelManager(this);
            this.DockPanels.Add(dockPanel);//已包含dockPanel.SetDockPanelManager(this);
            return dockPanel;
        }

        public void DockPanelCustomize()
        {
            DockPanelCustomizeForm DockPanelCustomizeForm1 = new DockPanelCustomizeForm(this);
            DockPanelCustomizeForm1.ShowDialog();
        }

        public void Refresh()
        {
            if (this.ParentForm != null) this.ParentForm.Refresh();
            //
            foreach (DockPanelFloatForm one in this.DockPanelFloatForms)
            {
                one.Refresh();
            }
        }

        public void RefreshComponents()
        {
            if (this.DockPanelHideAreaTop != null) this.DockPanelHideAreaTop.Refresh();
            if (this.DockPanelHideAreaLeft != null) this.DockPanelHideAreaLeft.Refresh();
            if (this.DockPanelHideAreaRight != null) this.DockPanelHideAreaRight.Refresh();
            if (this.DockPanelHideAreaBottom != null) this.DockPanelHideAreaBottom.Refresh();
            //
            if (this.DocumentArea != null) this.DocumentArea.Refresh();
            //
            foreach (DockPanelDockArea one in this.DockPanelDockAreas)
            {
                one.Refresh();
            }
            //
            foreach (DockPanelFloatForm one in this.DockPanelFloatForms)
            {
                one.Refresh();
            }
        }

        public void RefreshFloatForm()
        {
            foreach (DockPanelFloatForm one in this.DockPanelFloatForms)
            {
                one.Refresh();
            }
        }

        public void SetDockPanelHideState(bool bHideState)
        {
            foreach (DockPanelDockArea one in this.DockPanelDockAreas)
            {
                this.SetDockPanelHideState_DG(one.ChildNode, bHideState);
            }
        }
        private void SetDockPanelHideState_DG(IBaseNode pBaseNode, bool bHideState)
        {
            if (pBaseNode == null) return;
            //
            switch (pBaseNode.eNodeStyle)
            {
                case NodeStyle.eBinaryNode:
                    IBinaryNode pBinaryNode = pBaseNode as IBinaryNode;
                    if (pBinaryNode != null)
                    {
                        this.SetDockPanelHideState_DG(pBinaryNode.LeftNode, bHideState);
                        this.SetDockPanelHideState_DG(pBinaryNode.RightNode, bHideState);
                    }
                    break;
                case NodeStyle.eMultipleNode:
                    if (pBaseNode is ISetDockPanelHelper) { ((ISetDockPanelHelper)pBaseNode).SetHideState(bHideState); }
                    break;
                default:
                    break;
            }
        }
        #endregion


        #region 内部公开函数
        internal void RelayoutDockAreas(Orientation eOrientation, int iExceptSize)//重新布局（当残留区尺寸小于60时才开始调整布局）
        {
            int topNum; 
            int bottomNum;
            int leftNum;
            int rightNum;
            int otherNum;
            Size workRegionSizeExceptDockArea = this.GetParentFormWorkRegionSize(true, out topNum, out bottomNum, out leftNum, out rightNum, out otherNum);
            if (eOrientation == Orientation.Horizontal) 
            {
                int iNum = leftNum + rightNum;
                int iWidth = 60;
                if (iNum == 0) { iWidth = workRegionSizeExceptDockArea.Width - iExceptSize; }
                else { iWidth = (workRegionSizeExceptDockArea.Width - iExceptSize) / iNum; }
                if (iWidth < 60) iWidth = 60;
                for (int i = 0; i < this.ParentForm.Controls.Count; i++)
                {
                    if (!this.ParentForm.Controls[i].Visible) continue;
                    if (!(this.ParentForm.Controls[i] is IDockArea)) continue;
                    //
                    switch (this.ParentForm.Controls[i].Dock)
                    {
                        case DockStyle.Left:
                        case DockStyle.Right:
                            this.ParentForm.Controls[i].Width = iWidth;
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                int iNum = topNum + bottomNum;
                int iHeight = 60;
                if (iNum == 0) { iHeight = workRegionSizeExceptDockArea.Height - iExceptSize; }
                else { iHeight = (workRegionSizeExceptDockArea.Height - iExceptSize) / iNum; }
                if (iHeight < 60) iHeight = 60;
                for (int i = 0; i < this.ParentForm.Controls.Count; i++)
                {
                    if (!this.ParentForm.Controls[i].Visible) continue;
                    if (!(this.ParentForm.Controls[i] is IDockArea)) continue;
                    //
                    switch (this.ParentForm.Controls[i].Dock)
                    {
                        case DockStyle.Top:
                        case DockStyle.Bottom:
                           this.ParentForm.Controls[i].Height = iHeight;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        internal DockPanelContainer GetEmptyDockPanelContainer()//获取一个闲置的停靠面板容器（如果停靠面板容器收集器里存在空子项则返回查询到的第一个对象，否则新建一个并讲其加载到收集器中去）
        {
            foreach (DockPanelContainer one in this.DockPanelContainers)
            {
                if (one.Panel1.Controls.Count < 1 &&
                    one.Panel2.Controls.Count < 1) 
                {
                    //if (one.IsDisposed) this.DockPanelContainers.Remove(one);
                    return one; 
                }
            }
            DockPanelContainer dockPanelContainer = new DockPanelContainer();
            dockPanelContainer.Name = "dockPanelContainer" + (this.DockPanelContainers.Count + 1);
            //dockPanelContainer.SetDockPanelManager(this);
            this.DockPanelContainers.Add(dockPanelContainer);//已包含dockPanelContainer.SetDockPanelManager(this);
            return dockPanelContainer;
        }

        internal DockPanelDockArea GetEmptyDockPanelDockArea()//获取一个闲置的停靠面板停靠区（如果停靠面板停靠区收集器里存在空子项则返回查询到的第一个对象，否则新建一个并讲其加载到收集器中去）
        {
            foreach (DockPanelDockArea one in this.DockPanelDockAreas)
            {
                if (one.Controls.Count < 1)
                {
                    return one;
                }
            }
            DockPanelDockArea dockPanelDockArea = new DockPanelDockArea();
            dockPanelDockArea.Name = "dockPanelDockArea" + (this.DockPanelDockAreas.Count + 1);
            //dockPanelDockArea.SetDockPanelManager(this);
            this.DockPanelDockAreas.Add(dockPanelDockArea);//已包含dockPanelDockArea.SetDockPanelManager(this);
            return dockPanelDockArea;
        }

        internal DockPanelFloatForm GetEmptyDockPanelFloatForm()//获取一个闲置的停靠面板浮动窗体（如果停靠面板浮动窗体收集器里存在空子项则移除该对象，最后新建一个并讲其加载到收集器中去）
        {
            foreach (DockPanelFloatForm one in this.DockPanelFloatForms)
            {
                if (one.Controls.Count < 1)
                {
                    this.DockPanelFloatForms.Remove(one);
                }
            }
            DockPanelFloatForm DockPanelFloatForm1 = new DockPanelFloatForm();
            DockPanelFloatForm1.Name = "DockPanelFloatForm" + (this.DockPanelFloatForms.Count + 1);
            //DockPanelFloatForm1.SetDockPanelManager(this);
            this.DockPanelFloatForms.Add(DockPanelFloatForm1);//已包含DockPanelFloatForm1.SetDockPanelManager(this);
            return DockPanelFloatForm1;
        }

        internal int GetDockPanelCountFromVisible(bool bExceptEmpty, bool bVisible)//获取停靠面板的数量
        {
            int iCount = 0;
            if (bExceptEmpty)
            {
                for (int j = 0; j < this.DockPanels.Count; j++)
                {
                    if (this.DockPanels[j].BasePanels.Count < 1) continue;
                    if (this.DockPanels[j].GetVisible() == bVisible) iCount++;
                }
            }
            else
            {
                for (int j = 0; j < this.DockPanels.Count; j++)
                {
                    if (this.DockPanels[j].GetVisible() == bVisible) iCount++;
                }
            }
            return iCount;
        }

        internal Rectangle GetParentFormWorkRegionRectangle(bool bExceptDockArea)//获取父窗体工作区的矩形框
        {
            Point point = this.ParentForm.DisplayRectangle.Location;
            Size size = this.ParentForm.Size;
            if (bExceptDockArea)
            {
                for (int i = 0; i < this.ParentForm.Controls.Count; i++)
                {
                    if (!this.ParentForm.Controls[i].Visible) continue;
                    if (this.ParentForm.Controls[i] is IDockArea) continue;
                    //
                    switch (this.ParentForm.Controls[i].Dock)
                    {
                        case DockStyle.Top:
                            point.Y += this.ParentForm.Controls[i].Height;
                            size.Height -= this.ParentForm.Controls[i].Height;
                            break;
                        case DockStyle.Bottom:
                            size.Height -= this.ParentForm.Controls[i].Height;
                            break;
                        case DockStyle.Left:
                            point.X += this.ParentForm.Controls[i].Width;
                            size.Width -= this.ParentForm.Controls[i].Width;
                            break;
                        case DockStyle.Right:
                            size.Width -= this.ParentForm.Controls[i].Width;
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < this.ParentForm.Controls.Count; i++)
                {
                    if (!this.ParentForm.Controls[i].Visible) continue;
                    //
                    switch (this.ParentForm.Controls[i].Dock)
                    {
                        case DockStyle.Top:
                            point.Y += this.ParentForm.Controls[i].Height;
                            size.Height -= this.ParentForm.Controls[i].Height;
                            break;
                        case DockStyle.Bottom:
                            size.Height -= this.ParentForm.Controls[i].Height;
                            break;
                        case DockStyle.Left:
                            point.X += this.ParentForm.Controls[i].Width;
                            size.Width -= this.ParentForm.Controls[i].Width;
                            break;
                        case DockStyle.Right:
                            size.Width -= this.ParentForm.Controls[i].Width;
                            break;
                        default:
                            break;
                    }
                }
            }
            return new Rectangle(point, size);
        }

        internal Size GetParentFormWorkRegionSize(bool bExceptDockArea)//获取父窗体工作区的尺寸
        {
            Size size = this.ParentForm.Size;
            if (bExceptDockArea)
            {
                if (this.DocumentArea != null) return this.DocumentArea.DisplayRectangle.Size;
                //
                for (int i = 0; i < this.ParentForm.Controls.Count; i++)
                {
                    if (!this.ParentForm.Controls[i].Visible) continue;
                    if (this.ParentForm.Controls[i] is IDockArea) continue;
                    //
                    switch (this.ParentForm.Controls[i].Dock)
                    {
                        case DockStyle.Top:
                        case DockStyle.Bottom:
                            size.Height -= this.ParentForm.Controls[i].Height;
                            break;
                        case DockStyle.Left:
                        case DockStyle.Right:
                            size.Width -= this.ParentForm.Controls[i].Width;
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < this.ParentForm.Controls.Count; i++)
                {
                    if (!this.ParentForm.Controls[i].Visible) continue;
                    //
                    switch (this.ParentForm.Controls[i].Dock)
                    {
                        case DockStyle.Top:
                        case DockStyle.Bottom:
                            size.Height -= this.ParentForm.Controls[i].Height;
                            break;
                        case DockStyle.Left:
                        case DockStyle.Right:
                            size.Width -= this.ParentForm.Controls[i].Width;
                            break;
                        default:
                            break;
                    }
                }
            }
            return size;
        }

        internal Size GetParentFormWorkRegionSize(bool bExceptDockArea, DockStyle eExceptDockStyle)//获取父窗体工作区的尺寸（排除某一停靠方向的尺寸）
        {
            Size size = this.ParentForm.Size;
            if (bExceptDockArea)
            {
                for (int i = 0; i < this.ParentForm.Controls.Count; i++)
                {
                    if (!this.ParentForm.Controls[i].Visible) continue;
                    if (this.ParentForm.Controls[i].Dock == eExceptDockStyle) continue;
                    if (this.ParentForm.Controls[i] is IDockArea) continue;
                    //
                    switch (this.ParentForm.Controls[i].Dock)
                    {
                        case DockStyle.Top:
                        case DockStyle.Bottom:
                            size.Height -= this.ParentForm.Controls[i].Height;
                            break;
                        case DockStyle.Left:
                        case DockStyle.Right:
                            size.Width -= this.ParentForm.Controls[i].Width;
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < this.ParentForm.Controls.Count; i++)
                {
                    if (!this.ParentForm.Controls[i].Visible) continue;
                    if (this.ParentForm.Controls[i].Dock == eExceptDockStyle) continue;
                    //
                    switch (this.ParentForm.Controls[i].Dock)
                    {
                        case DockStyle.Top:
                        case DockStyle.Bottom:
                            size.Height -= this.ParentForm.Controls[i].Height;
                            break;
                        case DockStyle.Left:
                        case DockStyle.Right:
                            size.Width -= this.ParentForm.Controls[i].Width;
                            break;
                        default:
                            break;
                    }
                }
            }
            return size;
        }

        internal Size GetParentFormWorkRegionSize(bool bExceptDockArea,
            out int topNum, out int bottomNum, out int leftNum, out int rightNum, out int otherNum)//获取父窗体工作区的尺寸（当bExceptDockArea为true时传出继承于IDockArea控件各种停靠状态的数目，反之传出所有控件各种停靠状态的数目）
        {
            topNum = 0;
            bottomNum = 0;
            leftNum = 0;
            rightNum = 0;
            otherNum = 0;
            Size size = this.ParentForm.Size;
            if (bExceptDockArea)
            {
                for (int i = 0; i < this.ParentForm.Controls.Count; i++)
                {
                    if (!this.ParentForm.Controls[i].Visible) continue;
                    //
                    if (this.ParentForm.Controls[i] is IDockArea)
                    {
                        switch (this.ParentForm.Controls[i].Dock)
                        {
                            case DockStyle.Top:
                                topNum++;
                                break;
                            case DockStyle.Bottom:
                                bottomNum++;
                                break;
                            case DockStyle.Left:
                                leftNum++;
                                break;
                            case DockStyle.Right:
                                rightNum++;
                                break;
                            default:
                                otherNum++;
                                break;
                        }
                    }
                    else
                    {
                        switch (this.ParentForm.Controls[i].Dock)
                        {
                            case DockStyle.Top:
                                size.Height -= this.ParentForm.Controls[i].Height;
                                break;
                            case DockStyle.Bottom:
                                size.Height -= this.ParentForm.Controls[i].Height;
                                break;
                            case DockStyle.Left:
                                size.Width -= this.ParentForm.Controls[i].Width;
                                break;
                            case DockStyle.Right:
                                size.Width -= this.ParentForm.Controls[i].Width;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < this.ParentForm.Controls.Count; i++)
                {
                    if (!this.ParentForm.Controls[i].Visible) continue;
                    //
                    switch (this.ParentForm.Controls[i].Dock)
                    {
                        case DockStyle.Top:
                            size.Height -= this.ParentForm.Controls[i].Height;
                            topNum++;
                            break;
                        case DockStyle.Bottom:
                            size.Height -= this.ParentForm.Controls[i].Height;
                            bottomNum++;
                            break;
                        case DockStyle.Left:
                            size.Width -= this.ParentForm.Controls[i].Width;
                            leftNum++;
                            break;
                        case DockStyle.Right:
                            size.Width -= this.ParentForm.Controls[i].Width;
                            rightNum++;
                            break;
                        default:
                            otherNum++;
                            break;
                    }
                }
            }
            return size;
        }

        internal DockPanel GetDockPanelContainsMousePoint(Point point, ref Point screenPoint)//获取包涵当前鼠标点坐标的停靠面板（在DockButtonManagerForm的Timer_Tick(...)里被调用）
        {
            //首先查询浮动区面板
            foreach (DockPanel one in this.DockPanels)
            {
                if (one.IsHideState) continue;
                if (one.GetDockAreaStyle() != DockAreaStyle.eDockPanelFloatForm) continue;
                if (one.BasePanels.Count <= 0) continue;
                //
                if (one.ContainsMousePoint(point)) 
                {
                    screenPoint = one.GetControlCenterScreenPoint();
                    return one;
                }
            }
            //再次查询停靠区面板
            foreach (DockPanel one in this.DockPanels)
            {
                if (one.IsHideState) continue;
                if (one.GetDockAreaStyle() != DockAreaStyle.eDockPanelDockArea) continue;
                if (one.BasePanels.Count <= 0) continue;
                //
                if (one.ContainsMousePoint(point))
                {
                    screenPoint = one.GetControlCenterScreenPoint();
                    return one;
                }
            }
            //最后查询文档区面板
            foreach (DockPanel one in this.DockPanels)
            {
                if (one.IsHideState) continue;
                if (one.GetDockAreaStyle() != DockAreaStyle.eDocumentDockArea) continue;
                if (one.BasePanels.Count <= 0) continue;
                //
                if (one.ContainsMousePoint(point))
                {
                    screenPoint = one.GetControlCenterScreenPoint();
                    return one;
                }
            }
            return null;
        }
        #endregion

        #region SaveLayoutInfo 保存布局文件
        public void SaveLayoutFile(string strFileName)//保存当前布局状态
        {
            XmlDocument doc = new XmlDocument();
            //
            //
            //
            XmlDeclaration declare = doc.CreateXmlDeclaration("1.0", "UTF-8", "yes");//创建一个声明
            doc.InsertBefore(declare, doc.DocumentElement);//把声明添加到文档元素的顶部
            //
            //
            //
            XmlElement root = doc.CreateElement("DockPanelManager");//添加根节点
            doc.AppendChild(root);
            //
            //
            //
            XmlElement elementParentForm = root.OwnerDocument.CreateElement("ParentForm");
            switch (this.ParentForm.WindowState)
            {
                case FormWindowState.Maximized:
                    elementParentForm.SetAttribute("Location", "300,80");
                    elementParentForm.SetAttribute("Size", "800,600");
                    elementParentForm.SetAttribute("WindowState", "Maximized");
                    break;
                case FormWindowState.Minimized:
                    elementParentForm.SetAttribute("Location", "300,80");
                    elementParentForm.SetAttribute("Size", "800,600");
                    elementParentForm.SetAttribute("WindowState", "Normal");
                    break;
                case FormWindowState.Normal:
                    elementParentForm.SetAttribute("Location", this.ParentForm.Location.X.ToString() + "," + this.ParentForm.Location.Y.ToString());
                    elementParentForm.SetAttribute("Size", this.ParentForm.Size.Width.ToString() + "," + this.ParentForm.Size.Height.ToString());
                    elementParentForm.SetAttribute("WindowState", "Normal");
                    break;
            }
            root.AppendChild(elementParentForm);
            //
            // BasePanels DockPanels DockPanelContainers DockPanelDockAreas DockPanelFloatForms
            //
            #region BasePanels DockPanels DockPanelContainers DockPanelDockAreas DockPanelFloatForms
            //BasePanels
            XmlElement elementBasePanels = root.OwnerDocument.CreateElement("BasePanels");
            elementBasePanels.SetAttribute("Count", this.BasePanels.Count.ToString());
            root.AppendChild(elementBasePanels);
            this.BasePanels.SetRecordID();
            foreach (BasePanel one in this.BasePanels)
            {
                XmlElement element = elementBasePanels.OwnerDocument.CreateElement("BasePanel");
                element.SetAttribute("RecordID", one.RecordID.ToString());
                element.SetAttribute("Name", one.Name);
                element.SetAttribute("DockPanelFloatFormLocation", one.DockPanelFloatFormLocation.X.ToString() + "," + one.DockPanelFloatFormLocation.Y.ToString());
                element.SetAttribute("DockPanelFloatFormSize", one.DockPanelFloatFormSize.Width.ToString() + "," + one.DockPanelFloatFormSize.Height.ToString());
                elementBasePanels.AppendChild(element);
            }
            //DockPanels
            XmlElement elementDockPanels = root.OwnerDocument.CreateElement("DockPanels");
            elementDockPanels.SetAttribute("Count", this.DockPanels.Count.ToString());
            root.AppendChild(elementDockPanels);
            this.DockPanels.SetRecordID();
            foreach (DockPanel one in this.DockPanels)
            {
                XmlElement element = elementDockPanels.OwnerDocument.CreateElement("DockPanel");
                element.SetAttribute("RecordID", one.RecordID.ToString());
                element.SetAttribute("Name", one.Name);
                element.SetAttribute("VisibleEx", one.VisibleEx.ToString());
                element.SetAttribute("bActive", one.bActive.ToString());
                element.SetAttribute("IsHideState", one.IsHideState.ToString());
                element.SetAttribute("BasePanelSelectedIndex", one.BasePanelSelectedIndex.ToString());
                element.SetAttribute("DockPanelFloatFormLocation", one.DockPanelFloatFormLocation.X.ToString() + "," + one.DockPanelFloatFormLocation.Y.ToString());
                element.SetAttribute("DockPanelFloatFormSize", one.DockPanelFloatFormSize.Width.ToString() + "," + one.DockPanelFloatFormSize.Height.ToString());
                elementDockPanels.AppendChild(element);
            }
            //DockPanelContainers
            XmlElement elementDockPanelContainers = root.OwnerDocument.CreateElement("DockPanelContainers");
            elementDockPanelContainers.SetAttribute("Count", this.DockPanelContainers.Count.ToString());
            root.AppendChild(elementDockPanelContainers);
            this.DockPanelContainers.SetRecordID();
            foreach (DockPanelContainer one in this.DockPanelContainers)
            {
                XmlElement element = elementDockPanelContainers.OwnerDocument.CreateElement("DockPanelContainer");
                element.SetAttribute("RecordID", one.RecordID.ToString());
                element.SetAttribute("Name", one.Name);
                element.SetAttribute("Orientation", one.Orientation.ToString());
                element.SetAttribute("SplitterDistance", one.SplitterDistance.ToString());
                element.SetAttribute("Size", one.Size.Width.ToString() + "," + one.Size.Height.ToString());
                element.SetAttribute("DockPanelFloatFormLocation", one.DockPanelFloatFormLocation.X.ToString() + "," + one.DockPanelFloatFormLocation.Y.ToString());
                element.SetAttribute("DockPanelFloatFormSize", one.DockPanelFloatFormSize.Width.ToString() + "," + one.DockPanelFloatFormSize.Height.ToString());
                elementDockPanelContainers.AppendChild(element);
            }
            //DockPanelDockAreas
            XmlElement elementDockPanelDockAreas = root.OwnerDocument.CreateElement("DockPanelDockAreas");
            elementDockPanelDockAreas.SetAttribute("Count", this.DockPanelDockAreas.Count.ToString());
            root.AppendChild(elementDockPanelDockAreas);
            this.DockPanelDockAreas.SetRecordID();
            //foreach (DockPanelDockArea one in this.DockPanelDockAreas)
            //{
            //    XmlElement element = elementDockPanelDockAreas.OwnerDocument.CreateElement("DockPanelDockArea");
            //    element.SetAttribute("RecordID", one.RecordID.ToString());
            //    element.SetAttribute("Name", one.Name);
            //    element.SetAttribute("Dock", one.Dock.ToString());
            //    element.SetAttribute("Location", one.Location.X.ToString() + "," + one.Location.Y.ToString());
            //    element.SetAttribute("Size", one.Size.Width.ToString() + "," + one.Size.Height.ToString());
            //    elementDockPanelDockAreas.AppendChild(element);
            //}
            for (int i = this.ParentForm.Controls.Count - 1; i >= 0; i--)//key
            {
                DockPanelDockArea one = this.ParentForm.Controls[i] as DockPanelDockArea;
                if (one == null) continue;
                XmlElement element = elementDockPanelDockAreas.OwnerDocument.CreateElement("DockPanelDockArea");
                element.SetAttribute("RecordID", one.RecordID.ToString());
                element.SetAttribute("Name", one.Name);
                element.SetAttribute("Dock", one.Dock.ToString());
                element.SetAttribute("Location", one.Location.X.ToString() + "," + one.Location.Y.ToString());
                element.SetAttribute("Size", one.Size.Width.ToString() + "," + one.Size.Height.ToString());
                elementDockPanelDockAreas.AppendChild(element);
            }
            //DockPanelFloatForms
            XmlElement elementDockPanelFloatForms = root.OwnerDocument.CreateElement("DockPanelFloatForms");
            elementDockPanelFloatForms.SetAttribute("Count", this.DockPanelFloatForms.Count.ToString());
            root.AppendChild(elementDockPanelFloatForms);
            this.DockPanelFloatForms.SetRecordID();
            foreach (DockPanelFloatForm one in this.DockPanelFloatForms)
            {
                XmlElement element = elementDockPanelFloatForms.OwnerDocument.CreateElement("DockPanelFloatForm");
                element.SetAttribute("RecordID", one.RecordID.ToString());
                element.SetAttribute("Name", one.Name);
                element.SetAttribute("Location", one.Location.X.ToString() + "," + one.Location.Y.ToString());
                element.SetAttribute("Size", one.Size.Width.ToString() + "," + one.Size.Height.ToString());
                elementDockPanelFloatForms.AppendChild(element);
            }
            #endregion
            //
            //DockNode
            //
            #region DockNode
            XmlElement elementDockNode = root.OwnerDocument.CreateElement("DockNode");
            root.AppendChild(elementDockNode);
            //DockPanel
            foreach (DockPanel one in this.DockPanels)
            {
                XmlElement element = elementDockNode.OwnerDocument.CreateElement("DockPanelNode");
                element.SetAttribute("RecordID", one.RecordID.ToString());
                //element.SetAttribute("Name", one.Name);
                elementDockNode.AppendChild(element);
                //
                XmlElement elementBasePanels2 = element.OwnerDocument.CreateElement("BasePanels");
                elementBasePanels2.SetAttribute("Count", one.BasePanels.Count.ToString());
                element.AppendChild(elementBasePanels2);
                foreach (BasePanel one2 in one.BasePanels)
                {
                    XmlElement elementBasePanel = elementBasePanels.OwnerDocument.CreateElement("BasePanel");
                    elementBasePanel.SetAttribute("RecordID", one2.RecordID.ToString());
                    //elementBasePanel.SetAttribute("Name", one2.Name);
                    elementBasePanels2.AppendChild(elementBasePanel);
                }
            }
            //DockPanelContainer
            foreach (DockPanelContainer one in this.DockPanelContainers)
            {
                XmlElement elementDockPanelContainer = elementDockNode.OwnerDocument.CreateElement("DockPanelContainerNode");
                elementDockPanelContainer.SetAttribute("RecordID", one.RecordID.ToString());
                //elementDockPanelContainer.SetAttribute("Name", one.Name);
                elementDockNode.AppendChild(elementDockPanelContainer);
                //
                IDockPanel pDockPanel1 = one.GetIDockPanelFromPanel1();
                this.GetInfo(pDockPanel1, elementDockPanelContainer);
                IDockPanel pDockPanel2 = one.GetIDockPanelFromPanel2();
                this.GetInfo(pDockPanel2, elementDockPanelContainer);
            }
            //DockPanelDockArea
            foreach (DockPanelDockArea one in this.DockPanelDockAreas)
            {
                XmlElement element = elementDockNode.OwnerDocument.CreateElement("DockPanelDockAreaNode");
                element.SetAttribute("RecordID", one.RecordID.ToString());
                //element.SetAttribute("Name", one.Name);
                elementDockNode.AppendChild(element);
                //
                IDockPanel pDockPanel = one.GetIDockPanel();
                this.GetInfo(pDockPanel, element);
            }
            //DocumentDockArea
            if (this.DocumentArea != null && this.DocumentArea is DocumentDockArea) 
            {
                XmlElement element = elementDockNode.OwnerDocument.CreateElement("DocumentDockAreaNode");
                element.SetAttribute("RecordID", this.DocumentArea.RecordID.ToString());
                //element.SetAttribute("Name", this.DocumentArea.Name);
                elementDockNode.AppendChild(element);
                //
                IDockPanel pDockPanel = ((DocumentDockArea)this.DocumentArea).GetIDockPanel();
                this.GetInfo(pDockPanel, element);
            }
            //DockPanelFloatForm
            foreach (DockPanelFloatForm one in this.DockPanelFloatForms)
            {
                XmlElement element = elementDockNode.OwnerDocument.CreateElement("DockPanelFloatFormNode");
                element.SetAttribute("RecordID", one.RecordID.ToString());
                //element.SetAttribute("Name", one.Name);
                elementDockNode.AppendChild(element);
                //
                IDockPanel pDockPanel = one.GetIDockPanel();
                this.GetInfo(pDockPanel, element);
            }
            #endregion
            //
            //DockPanelHideArea
            //
            #region DockPanelHideArea
            XmlElement elementDockPanelHideArea = root.OwnerDocument.CreateElement("DockPanelHideArea");
            elementDockPanelHideArea.SetAttribute("DockPanelCount", this.HideAreaTabButtonGroupItemCount.ToString());
            root.AppendChild(elementDockPanelHideArea);
            for (int i = 0; i < this.DockPanelHideAreaTop.BaseItems.Count; i++)
            {
                HideAreaTabButtonGroupItem temp = this.DockPanelHideAreaTop.GetHideAreaTabButtonGroupItem(i);
                if (temp == null || temp.DockPanel == null) continue;
                XmlElement element = elementDockPanels.OwnerDocument.CreateElement("DockPanel");
                element.SetAttribute("RecordID", temp.DockPanel.RecordID.ToString());
                //element.SetAttribute("Name", temp.DockPanel.Name);
                //element.SetAttribute("DockPanelHideArea", "DockPanelHideAreaTop");
                elementDockPanelHideArea.AppendChild(element);
            }
            //DockPanelHideAreaLeft
            for (int i = 0; i < this.DockPanelHideAreaLeft.BaseItems.Count; i++)
            {
                HideAreaTabButtonGroupItem temp = this.DockPanelHideAreaLeft.GetHideAreaTabButtonGroupItem(i);
                if (temp == null || temp.DockPanel == null) continue;
                XmlElement element = elementDockPanels.OwnerDocument.CreateElement("DockPanel");
                element.SetAttribute("RecordID", temp.DockPanel.RecordID.ToString());
                //element.SetAttribute("Name", temp.DockPanel.Name);
                //element.SetAttribute("DockPanelHideArea", "DockPanelHideAreaLeft");
                elementDockPanelHideArea.AppendChild(element);
            }
            //DockPanelHideAreaRight
            for (int i = 0; i < this.DockPanelHideAreaRight.BaseItems.Count; i++)
            {
                HideAreaTabButtonGroupItem temp = this.DockPanelHideAreaRight.GetHideAreaTabButtonGroupItem(i);
                if (temp == null || temp.DockPanel == null) continue;
                XmlElement element = elementDockPanels.OwnerDocument.CreateElement("DockPanel");
                element.SetAttribute("RecordID", temp.DockPanel.RecordID.ToString());
                //element.SetAttribute("Name", temp.DockPanel.Name);
                //element.SetAttribute("DockPanelHideArea", "DockPanelHideAreaRight");
                elementDockPanelHideArea.AppendChild(element);
            }
            //DockPanelHideAreaBottom
            for (int i = 0; i < this.DockPanelHideAreaBottom.BaseItems.Count; i++)
            {
                HideAreaTabButtonGroupItem temp = this.DockPanelHideAreaBottom.GetHideAreaTabButtonGroupItem(i);
                if (temp == null || temp.DockPanel == null) continue;
                XmlElement element = elementDockPanels.OwnerDocument.CreateElement("DockPanel");
                element.SetAttribute("RecordID", temp.DockPanel.RecordID.ToString());
                //element.SetAttribute("Name", temp.DockPanel.Name);
                //element.SetAttribute("DockPanelHideArea", "DockPanelHideAreaBottom");
                elementDockPanelHideArea.AppendChild(element);
            }
            #endregion
            //
            //
            //
            doc.Save(strFileName);
        }

        private void GetInfo(IDockPanel pDockPanel, XmlElement element)//获取相关信息
        {
            if (pDockPanel == null) return;
            //
            if (pDockPanel.eDockPanelStyle == DockPanelStyle.eDockPanelContainer)
            {
                XmlElement elementDockPanelContainer = element.OwnerDocument.CreateElement("DockPanelContainer");
                elementDockPanelContainer.SetAttribute("RecordID", pDockPanel.RecordID.ToString());
                //elementDockPanelContainer.SetAttribute("Name", pDockPanel.Name);
                element.AppendChild(elementDockPanelContainer);
            }
            else if (pDockPanel.eDockPanelStyle == DockPanelStyle.eDockPanel)
            {
                XmlElement elementDockPanel = element.OwnerDocument.CreateElement("DockPanel");
                elementDockPanel.SetAttribute("RecordID", pDockPanel.RecordID.ToString());
                //elementDockPanel.SetAttribute("Name", pDockPanel.Name);
                element.AppendChild(elementDockPanel);
            }
            else
            {
                HoldDockPanel holdDockPanel = pDockPanel as HoldDockPanel;
                XmlElement elementDockPanel = element.OwnerDocument.CreateElement("DockPanel");
                elementDockPanel.SetAttribute("RecordID", holdDockPanel.DockPanel.RecordID.ToString());
                //elementDockPanel.SetAttribute("Name", holdDockPanel.DockPanel.Name);
                element.AppendChild(elementDockPanel);
            }
        }
        #endregion

        #region LoadLayoutFile 加载布局文件
        struct SimpleParentFormInfo
        {
            public SimpleParentFormInfo(Point location, Size size, FormWindowState windowState)
            {
                this.m_Location = location;
                this.m_Size = size;
                this.m_WindowState = windowState;
            }

            private Point m_Location;
            public Point Location
            {
                get { return m_Location; }
            }

            private Size m_Size;
            public Size Size
            {
                get { return m_Size; }
            }

            private FormWindowState m_WindowState;
            public FormWindowState WindowState
            {
                get { return m_WindowState; }
            }
        }

        struct SimpleDockPanelInfo//用来存放停靠面板的部分属性信息
        {
            public SimpleDockPanelInfo(int recordID, string name, bool visibleEx, bool active, int basePanelSelectIndex)
            {
                this.m_RecordID = recordID;
                this.m_Name = name;
                this.m_VisibleEx = visibleEx;
                this.m_bActive = active;
                this.m_BasePanelSelectedIndex = basePanelSelectIndex;
            }

            private int m_RecordID; 
            public int RecordID
            {
                get { return m_RecordID; }
            }

            private string m_Name;
            public string Name
            {
                get { return m_Name; }
            }

            private bool m_VisibleEx;
            public bool VisibleEx
            {
                get { return m_VisibleEx; }
            }

            private bool m_bActive;
            public bool bActive
            {
                get { return m_bActive; }
            }

            private int m_BasePanelSelectedIndex;
            public int BasePanelSelectedIndex
            {
                get { return m_BasePanelSelectedIndex; }
            }
        }

        public void LoadLayoutFile(string strFileName, bool loadParentFormLayout)//加载布局文件，并根据布局文件进行布局
        {
            #region 展现所有隐藏的DockPanel项
            //DockPanelHideAreaTop
            for (int i = 0; i < this.DockPanelHideAreaTop.BaseItems.Count; i++)
            {
                HideAreaTabButtonGroupItem temp = this.DockPanelHideAreaTop.GetHideAreaTabButtonGroupItem(i);
                if (temp == null || temp.DockPanel == null) continue;
                //temp.DockPanel.ClickHideButton();
                ((ISetDockPanelHelper)temp.DockPanel).SetHideState(!temp.DockPanel.IsHideState);
            }
            //DockPanelHideAreaLeft
            for (int i = 0; i < this.DockPanelHideAreaLeft.BaseItems.Count; i++)
            {
                HideAreaTabButtonGroupItem temp = this.DockPanelHideAreaLeft.GetHideAreaTabButtonGroupItem(i);
                if (temp == null || temp.DockPanel == null) continue;
                //temp.DockPanel.ClickHideButton();
                ((ISetDockPanelHelper)temp.DockPanel).SetHideState(!temp.DockPanel.IsHideState);
            }
            //DockPanelHideAreaRight
            for (int i = 0; i < this.DockPanelHideAreaRight.BaseItems.Count; i++)
            {
                HideAreaTabButtonGroupItem temp = this.DockPanelHideAreaRight.GetHideAreaTabButtonGroupItem(i);
                if (temp == null || temp.DockPanel == null) continue;
                //temp.DockPanel.ClickHideButton();
                ((ISetDockPanelHelper)temp.DockPanel).SetHideState(!temp.DockPanel.IsHideState);
            }
            //DockPanelHideAreaBottom
            for (int i = 0; i < this.DockPanelHideAreaBottom.BaseItems.Count; i++)
            {
                HideAreaTabButtonGroupItem temp = this.DockPanelHideAreaBottom.GetHideAreaTabButtonGroupItem(i);
                if (temp == null || temp.DockPanel == null) continue;
                //temp.DockPanel.ClickHideButton();
                ((ISetDockPanelHelper)temp.DockPanel).SetHideState(!temp.DockPanel.IsHideState);
            }
            #endregion
            //
            //
            //
            #region 分离所有 DockPanel 和 BasePanel
            foreach (DockPanel one in this.DockPanels)
            {
                one.RemoveFromParent();
                one.ClearBasePanels();
            }
            #endregion
            //
            //
            //
            #region 读取布局文件 写入相关属性信息并进行布局
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(strFileName);
            //
            SimpleParentFormInfo simpleParentFormInfo =
                new SimpleParentFormInfo(new Point(300, 80), new Size(800, 600), FormWindowState.Normal);
            List<SimpleDockPanelInfo> simpleDockPanelInfoList = null;
            //
            XmlNode xmlNode = xmlDoc.SelectSingleNode("DockPanelManager");
            if (xmlNode == null) return;
            XmlNodeList xmlNodeList = xmlNode.ChildNodes;
            if (xmlNodeList == null) return;
            //
            foreach (XmlNode one in xmlNodeList)
            {
                XmlElement xe = (XmlElement)one;
                switch (xe.Name)
                {
                    case "ParentForm":
                        if (loadParentFormLayout) { simpleParentFormInfo = this.SetParentForm(xe); }
                        break;
                    case "BasePanels":
                        this.GetAndSetBasePanelInfo(xe);
                        break;
                    case "DockPanels":
                        simpleDockPanelInfoList = this.GetAndSetDockPanelInfo(xe);
                        break;
                    case "DockPanelContainers":
                        this.GetAndSetDockPanelContainerInfo(xe);
                        break;
                    case "DockPanelDockAreas":
                        this.GetAndSetDockPanelDockAreaInfo(xe);
                        break;
                    case "DockPanelFloatForms":
                        this.GetAndSetDockPanelFloatFormInfo(xe);
                        break;
                    case "DockNode":
                        this.ReadDockNode(xe);
                        break;
                    case "DockPanelHideArea":
                        this.SetDockPanelHideArea(xe);
                        break;
                    default:
                        break;
                }
            }
            //设置DockPanel的相关属性 激活状态、可视化、选择索引
            if (simpleDockPanelInfoList != null)
            {
                foreach (SimpleDockPanelInfo one in simpleDockPanelInfoList)
                {
                    DockPanel dockPanel = this.DockPanels.GetItem(one.RecordID);
                    if (dockPanel == null) continue;
                    if (one.bActive) { ((ISetDockPanelHelper)dockPanel).SetActiveState(dockPanel); }
                    if (!one.VisibleEx) { dockPanel.VisibleEx = false; }
                    dockPanel.BasePanelSelectedIndex = one.BasePanelSelectedIndex;

                }
            }
            //
            if (loadParentFormLayout)
            {
                this.ParentForm.Location = simpleParentFormInfo.Location;
                this.ParentForm.Size = simpleParentFormInfo.Size;
                this.ParentForm.WindowState = simpleParentFormInfo.WindowState;
            }
            #endregion
        }

        //

        private SimpleParentFormInfo SetParentForm(XmlElement xmlElement)//设置父窗体的布局信息
        {
            SimpleParentFormInfo simpleParentFormInfo = new SimpleParentFormInfo(this.ToPoint(xmlElement.GetAttribute("Location")),
                this.ToSize(xmlElement.GetAttribute("Size")), 
                this.ToFormWindowState(xmlElement.GetAttribute("WindowState")));
            //this.ParentForm.Location = this.ToPoint(xmlElement.GetAttribute("Location"));
            //this.ParentForm.Size = this.ToSize(xmlElement.GetAttribute("Size"));
            //this.ParentForm.WindowState = this.ToFormWindowState(xmlElement.GetAttribute("WindowState"));
            return simpleParentFormInfo;
        }

        private void GetAndSetBasePanelInfo(XmlElement xmlElement)//获取/创建并设置基础面板的属性信息
        {
            XmlNodeList xmlNodeList = xmlElement.ChildNodes;
            if (xmlNodeList == null) return;
            //
            foreach (XmlNode one in xmlNodeList)//遍历
            {
                XmlElement xe = (XmlElement)one;//将子节点类型转换为XmlElement类型
                int id = Int32.Parse(xe.GetAttribute("RecordID"));
                string name = xe.GetAttribute("Name");
                Point point = this.ToPoint(xe.GetAttribute("DockPanelFloatFormLocation"));
                Size size = this.ToSize(xe.GetAttribute("DockPanelFloatFormSize"));
                //
                BasePanel temp = this.BasePanels.GetItem(name);
                if (temp == null) { temp = new BasePanel(); temp.Name = name; this.BasePanels.Add(temp); }
                WFNew.ISetRecordItemHelper pSetRecordItemHelper = temp as WFNew.ISetRecordItemHelper;
                if (pSetRecordItemHelper != null) pSetRecordItemHelper.SetRecordID(id);
                temp.DockPanelFloatFormSize = size;
                temp.DockPanelFloatFormLocation = point;
            }
        }

        private List<SimpleDockPanelInfo> GetAndSetDockPanelInfo(XmlElement xmlElement)//获取/创建并设置停靠面板的属性信息，并传出SimpleDockPanelInfo集合供后期使用
        {
            List<SimpleDockPanelInfo> simpleDockPanelInfoList = new List<SimpleDockPanelInfo>();
            //
            XmlNodeList xmlNodeList = xmlElement.ChildNodes;
            if (xmlNodeList == null) return null;
            foreach (XmlNode one in xmlNodeList)//遍历
            {
                XmlElement xe = (XmlElement)one;//将子节点类型转换为XmlElement类型
                int id = Int32.Parse(xe.GetAttribute("RecordID"));
                string name = xe.GetAttribute("Name");
                Point point = this.ToPoint(xe.GetAttribute("DockPanelFloatFormLocation"));
                Size size = this.ToSize(xe.GetAttribute("DockPanelFloatFormSize"));
                bool visibleEx = bool.Parse(xe.GetAttribute("VisibleEx"));
                bool active = bool.Parse(xe.GetAttribute("bActive"));
                int selectIndex = Int32.Parse(xe.GetAttribute("BasePanelSelectedIndex"));
                //bool hideState = bool.Parse(xe.GetAttribute("IsHideState"));
                //
                DockPanel temp = this.DockPanels.GetItem(name);
                if (temp == null) { temp = new DockPanel(); temp.Name = name; this.DockPanels.Add(temp); }
                WFNew.ISetRecordItemHelper pSetRecordItemHelper = temp as WFNew.ISetRecordItemHelper;
                if (pSetRecordItemHelper != null) pSetRecordItemHelper.SetRecordID(id);
                temp.DockPanelFloatFormSize = size;
                temp.DockPanelFloatFormLocation = point;
                //
                simpleDockPanelInfoList.Add(new SimpleDockPanelInfo(id, name, visibleEx, active,selectIndex));
            }
            //
            return simpleDockPanelInfoList;
        }

        private void GetAndSetDockPanelContainerInfo(XmlElement xmlElement)//获取/创建并设置停靠面板容器的属性信息
        {
            XmlNodeList xmlNodeList = xmlElement.ChildNodes;
            if (xmlNodeList == null) return;
            //
            foreach (XmlNode one in xmlNodeList)//遍历
            {
                XmlElement xe = (XmlElement)one;//将子节点类型转换为XmlElement类型
                int id = Int32.Parse(xe.GetAttribute("RecordID"));
                string name = xe.GetAttribute("Name");
                Point point = this.ToPoint(xe.GetAttribute("DockPanelFloatFormLocation"));
                Size size = this.ToSize(xe.GetAttribute("DockPanelFloatFormSize"));
                Size size2 = this.ToSize(xe.GetAttribute("Size"));
                int splitterDistance = Int32.Parse(xe.GetAttribute("SplitterDistance"));
                Orientation orientation = this.ToOrientation(xe.GetAttribute("Orientation"));
                //
                DockPanelContainer temp = this.DockPanelContainers.GetItem(id);
                if (temp == null) { temp = new DockPanelContainer(); temp.Name = name; this.DockPanelContainers.Add(temp); }
                temp.Name = name;
                WFNew.ISetRecordItemHelper pSetRecordItemHelper = temp as WFNew.ISetRecordItemHelper;
                if (pSetRecordItemHelper != null) pSetRecordItemHelper.SetRecordID(id);
                temp.DockPanelFloatFormSize = size;
                temp.DockPanelFloatFormLocation = point;
                temp.Size = size2;
                temp.SplitterDistance = splitterDistance;
                temp.Orientation = orientation;
            }
        }

        private void GetAndSetDockPanelDockAreaInfo(XmlElement xmlElement)//获取/创建并设置停靠面板停靠区的属性信息
        {
            XmlNodeList xmlNodeList = xmlElement.ChildNodes;
            if (xmlNodeList == null) return;
            //
            int iIndex = this.DocumentAreaIndex + 1;
            foreach (XmlNode one in xmlNodeList)//遍历
            {
                XmlElement xe = (XmlElement)one;//将子节点类型转换为XmlElement类型
                int id = Int32.Parse(xe.GetAttribute("RecordID"));
                string name = xe.GetAttribute("Name");
                Point point = this.ToPoint(xe.GetAttribute("Location"));
                Size size = this.ToSize(xe.GetAttribute("Size"));
                DockStyle dock = this.ToDockStyle(xe.GetAttribute("Dock"));
                //
                DockPanelDockArea temp = this.DockPanelDockAreas.GetItem(id);
                if (temp == null) { temp = new DockPanelDockArea(); temp.Name = name; this.DockPanelDockAreas.Add(temp); }
                temp.Name = name;
                WFNew.ISetRecordItemHelper pSetRecordItemHelper = temp as WFNew.ISetRecordItemHelper;
                if (pSetRecordItemHelper != null) pSetRecordItemHelper.SetRecordID(id);
                temp.Size = size;
                temp.Location = point;
                temp.Dock = dock;
                this.ParentForm.Controls.Add(temp);
                if (iIndex < this.ParentForm.Controls.Count - 1) { this.ParentForm.Controls.SetChildIndex(temp, iIndex); }
            }
        }

        private void GetAndSetDockPanelFloatFormInfo(XmlElement xmlElement)//获取/创建并设置停靠面板浮动窗体的属性信息
        {
            XmlNodeList xmlNodeList = xmlElement.ChildNodes;
            if (xmlNodeList == null) return;
            //
            foreach (XmlNode one in xmlNodeList)//遍历
            {
                XmlElement xe = (XmlElement)one;//将子节点类型转换为XmlElement类型
                int id = Int32.Parse(xe.GetAttribute("RecordID"));
                string name = xe.GetAttribute("Name");
                Point point = this.ToPoint(xe.GetAttribute("Location"));
                Size size = this.ToSize(xe.GetAttribute("Size"));
                //
                DockPanelFloatForm temp = this.DockPanelFloatForms.GetItem(id);
                if (temp == null) { temp = new DockPanelFloatForm(); temp.Name = name; this.DockPanelFloatForms.Add(temp); }
                temp.Name = name;
                WFNew.ISetRecordItemHelper pSetRecordItemHelper = temp as WFNew.ISetRecordItemHelper;
                if (pSetRecordItemHelper != null) pSetRecordItemHelper.SetRecordID(id);
                temp.Size = size;
                temp.Location = point;
            }
        }

        //

        private void ReadDockNode(XmlElement xmlElement)//读取布局文件停靠节点信息
        {
            XmlNodeList xmlNodeList = xmlElement.ChildNodes;
            if (xmlNodeList == null) return;
            //
            foreach (XmlNode one in xmlNodeList)//遍历
            {
                XmlElement xe = (XmlElement)one;
                int id = Int32.Parse(xe.GetAttribute("RecordID"));
                switch (xe.Name)
                {
                    case "DockPanelNode":
                        this.SetDockPanelBasePanelNodes(this.DockPanels.GetItem(id), xe);
                        break;
                    case "DockPanelContainerNode":
                        this.SetDockPanelContainerTwoNodes(this.DockPanelContainers.GetItem(id), xe);
                        break;
                    case "DockPanelDockAreaNode":
                        this.SetDockPanelDockAreaNode(this.DockPanelDockAreas.GetItem(id), xe);
                        break;
                    case "DocumentDockAreaNode":
                        this.SetDocumentDockAreaNode(this.DocumentArea as DocumentDockArea, xe);
                        break;
                    case "DockPanelFloatFormNode":
                        this.SetDockPanelFloatFormNode(this.DockPanelFloatForms.GetItem(id), xe);
                        break;
                    default:
                        break;
                }
            }
        }

        private void SetDockPanelBasePanelNodes(DockPanel dockPanel, XmlElement xmlElement)//设置停靠面板内的所有基础面板节点
        {
            if (dockPanel == null) return;
            //
            XmlNode xmlNode = xmlElement.SelectSingleNode("BasePanels");
            if (xmlNode == null) return;
            XmlNodeList xmlNodeList = xmlNode.ChildNodes;
            if (xmlNodeList == null) return;
            //
            foreach (XmlNode one in xmlNodeList)//遍历
            {
                XmlElement xe = (XmlElement)one;//将子节点类型转换为XmlElement类型
                int id = Int32.Parse(xe.GetAttribute("RecordID"));
                //
                dockPanel.BasePanels.Add(this.BasePanels.GetItem(id));
            }
        }

        private void SetDockPanelContainerTwoNodes(DockPanelContainer dockPanelContainer, XmlElement xmlElement)//设置停靠面板容器的两个面板节点
        {
            XmlNodeList xmlNodeList = xmlElement.ChildNodes;
            if (xmlNodeList == null || xmlNodeList.Count <= 0) return;
            //
            XmlElement xe1 = (XmlElement)xmlNodeList.Item(0);
            int id1 = Int32.Parse(xe1.GetAttribute("RecordID"));
            if (xe1.Name == "DockPanelContainer")
            {
                dockPanelContainer.Panel1.Controls.Add(this.DockPanelContainers.GetItem(id1));
            }
            else
            {
                dockPanelContainer.Panel1.Controls.Add(this.DockPanels.GetItem(id1));
            }
            //
            XmlElement xe2 = (XmlElement)xmlNodeList.Item(1);
            int id2 = Int32.Parse(xe2.GetAttribute("RecordID"));
            if (xe2.Name == "DockPanelContainer")
            {
                dockPanelContainer.Panel2.Controls.Add(this.DockPanelContainers.GetItem(id2));
            }
            else
            {
                dockPanelContainer.Panel2.Controls.Add(this.DockPanels.GetItem(id2));
            }
        }

        private void SetDockPanelDockAreaNode(DockPanelDockArea dockPanelDockArea, XmlElement xmlElement)//设置停靠面板停靠区的一个面板节点
        {
            XmlNodeList xmlNodeList = xmlElement.ChildNodes;
            if (xmlNodeList == null || xmlNodeList.Count <= 0) return;
            //
            XmlElement xe = (XmlElement)xmlNodeList.Item(0);
            int id = Int32.Parse(xe.GetAttribute("RecordID"));
            if (xe.Name == "DockPanelContainer")
            {
                dockPanelDockArea.Controls.Add(this.DockPanelContainers.GetItem(id));
            }
            else
            {
                dockPanelDockArea.Controls.Add(this.DockPanels.GetItem(id));
            }
        }

        private void SetDocumentDockAreaNode(DocumentDockArea dockPanelDockArea, XmlElement xmlElement)
        {
            if (dockPanelDockArea == null) return;
            //
            XmlNodeList xmlNodeList = xmlElement.ChildNodes;
            if (xmlNodeList == null || xmlNodeList.Count <= 0) return;
            //
            XmlElement xe = (XmlElement)xmlNodeList.Item(0);
            int id = Int32.Parse(xe.GetAttribute("RecordID"));
            if (xe.Name == "DockPanelContainer")
            {
                dockPanelDockArea.Controls.Add(this.DockPanelContainers.GetItem(id));
            }
            else
            {
                dockPanelDockArea.Controls.Add(this.DockPanels.GetItem(id));
            }
        }

        private void SetDockPanelFloatFormNode(DockPanelFloatForm dockPanelFloatForm, XmlElement xmlElement)//设置停靠面板浮动窗体的一个面板节点
        {
            XmlNodeList xmlNodeList = xmlElement.ChildNodes;
            if (xmlNodeList == null || xmlNodeList.Count <= 0) return;
            //
            XmlElement xe = (XmlElement)xmlNodeList.Item(0);
            int id = Int32.Parse(xe.GetAttribute("RecordID"));
            if (xe.Name == "DockPanelContainer")
            {
                dockPanelFloatForm.Show(this.DockPanelContainers.GetItem(id));
            }
            else
            {
                dockPanelFloatForm.Show(this.DockPanels.GetItem(id));
            }
        }

        //

        private void SetDockPanelHideArea(XmlElement xmlElement)//隐藏该隐藏的面板
        {
            List<int> hideDockPanelRecordIDList = new List<int>();
            //
            XmlNodeList xmlNodeList = xmlElement.ChildNodes;
            if (xmlNodeList == null) return;
            //
            foreach (XmlNode one in xmlNodeList)//遍历
            {
                XmlElement xe = (XmlElement)one;//将子节点类型转换为XmlElement类型
                int id = Int32.Parse(xe.GetAttribute("RecordID"));
                hideDockPanelRecordIDList.Add(id);
            }
            //
            for (int i = 0; i < hideDockPanelRecordIDList.Count; i++)
            {
                DockPanel dockPanel = this.DockPanels.GetItem(hideDockPanelRecordIDList[i]);
                if (dockPanel != null) { ((ISetDockPanelHelper)dockPanel).SetHideState(true); }
            }
        }

        //

        private FormWindowState ToFormWindowState(string str)
        {
            if (str == FormWindowState.Maximized.ToString()) return FormWindowState.Maximized;
            else if (str == FormWindowState.Minimized.ToString()) return FormWindowState.Minimized;
            else return FormWindowState.Normal;
        }

        private Orientation ToOrientation(string str)
        {
            if (str == Orientation.Horizontal.ToString()) return Orientation.Horizontal;
            else return Orientation.Vertical;
        }

        private DockStyle ToDockStyle(string str)
        {
            if (str == DockStyle.Top.ToString()) { return DockStyle.Top; }
            else if (str == DockStyle.Left.ToString()) { return DockStyle.Left; }
            else if (str == DockStyle.Right.ToString()) { return DockStyle.Right; }
            else if (str == DockStyle.Bottom.ToString()) { return DockStyle.Bottom; }
            else if (str == DockStyle.Fill.ToString()) { return DockStyle.Fill; }
            else { return DockStyle.None; }
        }

        private Point ToPoint(string str)
        {
            try
            {
                string[] strList = str.Split(',');
                return new Point(Int32.Parse(strList[0]), Int32.Parse(strList[1]));
            }
            catch { GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show("布局文件损坏！"); return new Point(60, 60); }
        }

        private Size ToSize(string str)
        {
            try
            {
                string[] strList = str.Split(',');
                return new Size(Int32.Parse(strList[0]), Int32.Parse(strList[1]));
            }
            catch { GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show("布局文件损坏！"); return new Size(260, 260); }
        }
        #endregion

        //
        //
        //

        #region 附属类
        /// <summary>
        /// 基础面板收集器（由系统辅助管理 所新成员必须加载到收集器中否则会出现异常）
        /// </summary>
        public class BasePanelCollection : WFNew.FlexibleList<BasePanel>
        {
            private List<BasePanel> innerList = null;

            internal BasePanelCollection()
            {
                this.innerList = new List<BasePanel>();
            }

            public override int Add(BasePanel value)
            {
                if (this.Locked) return -1;
                //
                if (this.innerList.Contains(value)) return -1;
                //
                this.innerList.Add(value);
                //
                return this.innerList.Count - 1;
            }

            public override void Clear()
            {
                if (this.Locked) return;
                //
                this.innerList.Clear();
            }

            public override bool Contains(BasePanel value)
            {
                return this.innerList.Contains(value);
            }

            public override int IndexOf(BasePanel value)
            {
                return this.innerList.IndexOf(value);
            }

            public override void Insert(int index, BasePanel value)
            {
                if (this.Locked) return;
                //
                if (this.innerList.Contains(value)) return;
                //
                this.innerList.Insert(index, value);
            }

            public override void Remove(BasePanel value)
            {
                if (this.Locked) return;
                //
                this.innerList.Remove(value);
            }

            public override void RemoveAt(int index)
            {
                if (this.Locked) return;
                //
                this.innerList.RemoveAt(index);
            }

            [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public override BasePanel this[int index]
            {
                get
                {
                    if ((index < 0) || (index >= this.Count))
                    {
                        return null;
                    }
                    return this.innerList[index];
                }
                set
                {
                    if (this.Locked) return;
                    //
                    this.innerList[index] = value;
                }
            }

            public override int Count
            {
                get
                {
                    return this.innerList.Count;
                }
            }

            public override IEnumerator GetEnumerator()
            {
                return this.innerList.GetEnumerator();
            }

            public override bool ExchangeItemT(BasePanel item1, BasePanel item2)
            {
                if (this.Locked) return false;
                //
                if (item1 == null || item2 == null || item1 == item2) return false;
                //
                int index1 = this.innerList.IndexOf(item1);
                int index2 = this.innerList.IndexOf(item2);
                //
                if (index1 == index2) return false;
                if ((index1 < 0) || (index1 >= this.innerList.Count)) return false;
                if ((index2 < 0) || (index2 >= this.innerList.Count)) return false;
                //
                if (index1 < index2)
                {
                    this.innerList.Remove(item2);
                    this.innerList.Remove(item1);
                    this.innerList.Insert(index1, item2);
                    this.innerList.Insert(index2, item1);
                }
                else
                {
                    this.innerList.Remove(item1);
                    this.innerList.Remove(item2);
                    this.innerList.Insert(index2, item1);
                    this.innerList.Insert(index1, item2);
                }
                //
                return true;
            }

            public BasePanel this[string name]
            {
                get
                {
                    foreach (BasePanel one in this.innerList)
                    {
                        if (one.Name == name) return one;
                    }
                    //
                    return null;
                }
            }

            //
            //
            //

            public void SetRecordID()
            {
                for (int i = 0; i < this.innerList.Count; i++)
                {
                    WFNew.ISetRecordItemHelper pSetRecordItemHelper = this.innerList[i] as WFNew.ISetRecordItemHelper;
                    if (pSetRecordItemHelper != null) pSetRecordItemHelper.SetRecordID(i);
                }
            }

            public BasePanel GetItem(int recordID)
            {
                for (int i = 0; i < this.Count; i++)
                {
                    if (this[i].RecordID == recordID) return this[i];
                }
                return null;
            }

            public BasePanel GetItem(string name)
            {
                for (int i = 0; i < this.Count; i++)
                {
                    if (this[i].Name == name) return this[i];
                }
                return null;
            }
        }

        /// <summary>
        /// 停靠面板收集器（由系统辅助管理 所新成员必须加载到收集器中否则会出现异常）
        /// </summary>
        public class DockPanelCollection : WFNew.FlexibleList<DockPanel>
        {
            private DockPanelManager owner = null;
            private List<DockPanel> innerList = null;

            internal DockPanelCollection(DockPanelManager dockPanelManager)
            {
                this.owner = dockPanelManager;
                this.innerList = new List<DockPanel>();
            }

            public override int Add(DockPanel value)
            {
                if (this.Locked) return -1;
                //
                if (this.innerList.Contains(value)) return -1;
                //
                ((ISetDockPanelManagerHelper)value).SetDockPanelManager(owner);
                this.innerList.Add(value);
                //
                return this.innerList.Count - 1;
            }

            public override void Clear()
            {
                if (this.Locked) return;
                //
                this.innerList.Clear();
            }

            public override bool Contains(DockPanel value)
            {
                return this.innerList.Contains(value);
            }

            public override int IndexOf(DockPanel value)
            {
                return this.innerList.IndexOf(value);
            }

            public override void Insert(int index, DockPanel value)
            {
                if (this.Locked) return;
                //
                if (this.innerList.Contains(value)) return;
                //
                ((ISetDockPanelManagerHelper)value).SetDockPanelManager(owner);
                this.innerList.Insert(index, value);
            }

            public override void Remove(DockPanel value)
            {
                if (this.Locked) return;
                //
                this.innerList.Remove(value);
            }

            public override void RemoveAt(int index)
            {
                if (this.Locked) return;
                //
                this.innerList.RemoveAt(index);
            }

            public override IEnumerator GetEnumerator()
            {
                return this.innerList.GetEnumerator();
            }

            [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public override DockPanel this[int index]
            {
                get
                {
                    if ((index < 0) || (index >= this.Count))
                    {
                        return null;
                    }
                    return this.innerList[index];
                }
                set
                {
                    if (this.Locked) return;
                    //
                    this.innerList[index] = value;
                }
            }

            public override int Count
            {
                get
                {
                    return this.innerList.Count;
                }
            }

            public override bool ExchangeItemT(DockPanel item1, DockPanel item2)
            {
                if (this.Locked) return false;
                //
                if (item1 == null || item2 == null || item1 == item2) return false;
                //
                int index1 = this.innerList.IndexOf(item1);
                int index2 = this.innerList.IndexOf(item2);
                //
                if (index1 == index2) return false;
                if ((index1 < 0) || (index1 >= this.innerList.Count)) return false;
                if ((index2 < 0) || (index2 >= this.innerList.Count)) return false;
                //
                if (index1 < index2)
                {
                    this.innerList.Remove(item2);
                    this.innerList.Remove(item1);
                    this.innerList.Insert(index1, item2);
                    this.innerList.Insert(index2, item1);
                }
                else
                {
                    this.innerList.Remove(item1);
                    this.innerList.Remove(item2);
                    this.innerList.Insert(index2, item1);
                    this.innerList.Insert(index1, item2);
                }
                //
                return true;
            }

            public DockPanel this[string name]
            {
                get
                {
                    foreach (DockPanel one in this.innerList)
                    {
                        if (one.Name == name) return one;
                    }
                    //
                    return null;
                }
            }

            //
            //
            //

            public void SetRecordID()
            {
                for (int i = 0; i < this.innerList.Count; i++)
                {
                    WFNew.ISetRecordItemHelper pSetRecordItemHelper = this.innerList[i] as WFNew.ISetRecordItemHelper;
                    if (pSetRecordItemHelper != null) pSetRecordItemHelper.SetRecordID(i);
                }
            }

            public DockPanel GetItem(int recordID)
            {
                for (int i = 0; i < this.Count; i++)
                {
                    if (this[i].RecordID == recordID) return this[i];
                }
                return null;
            }

            public DockPanel GetItem(string name)
            {
                for (int i = 0; i < this.Count; i++)
                {
                    if (this[i].Name == name) return this[i];
                }
                return null;
            }
        }

        /// <summary>
        /// 停靠面板容器收集器（由系统自动 添加、删除、注销 资源）
        /// </summary>
        public class DockPanelContainerCollection : WFNew.FlexibleList<DockPanelContainer>
        {
            private DockPanelManager owner = null;
            private List<DockPanelContainer> innerList = null;

            internal DockPanelContainerCollection(DockPanelManager dockPanelManager)
            {
                this.owner = dockPanelManager;
                this.innerList = new List<DockPanelContainer>();
            }

            public override int Add(DockPanelContainer value)
            {
                if (this.Locked) return -1;
                //
                if (this.innerList.Contains(value)) return -1;
                //
                ((ISetDockPanelManagerHelper)value).SetDockPanelManager(owner);
                this.innerList.Add(value);
                //
                return this.innerList.Count - 1;
            }

            public override void Clear()
            {
                if (this.Locked) return;
                //
                this.innerList.Clear();
            }

            public override bool Contains(DockPanelContainer value)
            {
                return this.innerList.Contains(value);
            }

            public override int IndexOf(DockPanelContainer value)
            {
                return this.innerList.IndexOf(value);
            }

            public override void Insert(int index, DockPanelContainer value)
            {
                if (this.Locked) return;
                //
                if (this.innerList.Contains(value)) return;
                //
                ((ISetDockPanelManagerHelper)value).SetDockPanelManager(owner);
                this.innerList.Insert(index, value);
            }

            public override void Remove(DockPanelContainer value)
            {
                if (this.Locked) return;
                //
                this.innerList.Remove(value);
            }

            public override void RemoveAt(int index)
            {
                if (this.Locked) return;
                //
                this.innerList.RemoveAt(index);
            }

            public override IEnumerator GetEnumerator()
            {
                return this.innerList.GetEnumerator();
            }

            [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public override DockPanelContainer this[int index]
            {
                get
                {
                    if ((index < 0) || (index >= this.Count))
                    {
                        return null;
                    }
                    return this.innerList[index];
                }
                set
                {
                    if (this.Locked) return;
                    //
                    this.innerList[index] = value;
                }
            }

            public override int Count
            {
                get
                {
                    return this.innerList.Count;
                }
            }

            public override bool ExchangeItemT(DockPanelContainer item1, DockPanelContainer item2)
            {
                if (this.Locked) return false;
                //
                if (item1 == null || item2 == null || item1 == item2) return false;
                //
                int index1 = this.innerList.IndexOf(item1);
                int index2 = this.innerList.IndexOf(item2);
                //
                if (index1 == index2) return false;
                if ((index1 < 0) || (index1 >= this.innerList.Count)) return false;
                if ((index2 < 0) || (index2 >= this.innerList.Count)) return false;
                //
                if (index1 < index2)
                {
                    this.innerList.Remove(item2);
                    this.innerList.Remove(item1);
                    this.innerList.Insert(index1, item2);
                    this.innerList.Insert(index2, item1);
                }
                else
                {
                    this.innerList.Remove(item1);
                    this.innerList.Remove(item2);
                    this.innerList.Insert(index2, item1);
                    this.innerList.Insert(index1, item2);
                }
                //
                return true;
            }

            public DockPanelContainer this[string name]
            {
                get
                {
                    foreach (DockPanelContainer one in this.innerList)
                    {
                        if (one.Name == name) return one;
                    }
                    //
                    return null;
                }
            }

            //
            //
            //

            public void SetRecordID()
            {
                for (int i = 0; i < this.innerList.Count; i++)
                {
                    WFNew.ISetRecordItemHelper pSetRecordItemHelper = this.innerList[i] as WFNew.ISetRecordItemHelper;
                    if (pSetRecordItemHelper != null) pSetRecordItemHelper.SetRecordID(i);
                }
            }

            public DockPanelContainer GetItem(int recordID)
            {
                for (int i = 0; i < this.Count; i++)
                {
                    if (this[i].RecordID == recordID) return this[i];
                }
                return null;
            }
        }

        /// <summary>
        /// 停靠面板停靠区收集器（由系统自动 添加、删除、注销 资源）
        /// </summary>
        public class DockPanelDockAreaCollection : WFNew.FlexibleList<DockPanelDockArea>
        {
            private DockPanelManager owner = null;
            private List<DockPanelDockArea> innerList = null;

            internal DockPanelDockAreaCollection(DockPanelManager dockPanelManager)
            {
                this.owner = dockPanelManager;
                this.innerList = new List<DockPanelDockArea>();
            }

            public override int Add(DockPanelDockArea value)
            {
                if (this.Locked) return -1;
                //
                if (this.innerList.Contains(value)) return -1;
                //
                ((ISetDockPanelManagerHelper)value).SetDockPanelManager(owner);
                this.innerList.Add(value);
                //
                return this.innerList.Count - 1;
            }

            public override void Clear()
            {
                if (this.Locked) return;
                //
                this.innerList.Clear();
            }

            public override bool Contains(DockPanelDockArea value)
            {
                return this.innerList.Contains(value);
            }

            public override int IndexOf(DockPanelDockArea value)
            {
                return this.innerList.IndexOf(value);
            }

            public override void Insert(int index, DockPanelDockArea value)
            {
                if (this.Locked) return;
                //
                if (this.innerList.Contains(value)) return;
                //
                ((ISetDockPanelManagerHelper)value).SetDockPanelManager(owner);
                this.innerList.Insert(index, value);
            }

            public override void Remove(DockPanelDockArea value)
            {
                if (this.Locked) return;
                //
                this.innerList.Remove(value);
            }

            public override void RemoveAt(int index)
            {
                if (this.Locked) return;
                //
                this.innerList.RemoveAt(index);
            }

            public override IEnumerator GetEnumerator()
            {
                return this.innerList.GetEnumerator();
            }

            [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public override DockPanelDockArea this[int index]
            {
                get
                {
                    if ((index < 0) || (index >= this.Count))
                    {
                        return null;
                    }
                    return this.innerList[index];
                }
                set
                {
                    if (this.Locked) return;
                    //
                    this.innerList[index] = value;
                }
            }

            public override int Count
            {
                get
                {
                    return this.innerList.Count;
                }
            }

            public override bool ExchangeItemT(DockPanelDockArea item1, DockPanelDockArea item2)
            {
                if (this.Locked) return false;
                //
                if (item1 == null || item2 == null || item1 == item2) return false;
                //
                int index1 = this.innerList.IndexOf(item1);
                int index2 = this.innerList.IndexOf(item2);
                //
                if (index1 == index2) return false;
                if ((index1 < 0) || (index1 >= this.innerList.Count)) return false;
                if ((index2 < 0) || (index2 >= this.innerList.Count)) return false;
                //
                if (index1 < index2)
                {
                    this.innerList.Remove(item2);
                    this.innerList.Remove(item1);
                    this.innerList.Insert(index1, item2);
                    this.innerList.Insert(index2, item1);
                }
                else
                {
                    this.innerList.Remove(item1);
                    this.innerList.Remove(item2);
                    this.innerList.Insert(index2, item1);
                    this.innerList.Insert(index1, item2);
                }
                //
                return true;
            }

            public DockPanelDockArea this[string name]
            {
                get
                {
                    foreach (DockPanelDockArea one in this.innerList)
                    {
                        if (one.Name == name) return one;
                    }
                    //
                    return null;
                }
            }

            //
            //
            //

            public void SetRecordID()
            {
                for (int i = 0; i < this.innerList.Count; i++)
                {
                    WFNew.ISetRecordItemHelper pSetRecordItemHelper = this.innerList[i] as WFNew.ISetRecordItemHelper;
                    if (pSetRecordItemHelper != null) pSetRecordItemHelper.SetRecordID(i);
                }
            }

            public DockPanelDockArea GetItem(int recordID)
            {
                for (int i = 0; i < this.Count; i++)
                {
                    if (this[i].RecordID == recordID) return this[i];
                }
                return null;
            }
        }

        /// <summary>
        /// 停靠面板浮动窗体收集器（由系统自动 添加、删除、注销 资源）
        /// </summary>
        internal class DockPanelFloatFormCollection : WFNew.FlexibleList<DockPanelFloatForm>
        {
            private DockPanelManager owner = null;
            private List<DockPanelFloatForm> innerList = null;

            internal DockPanelFloatFormCollection(DockPanelManager dockPanelManager)
            {
                this.owner = dockPanelManager;
                this.innerList = new List<DockPanelFloatForm>();
            }

            public override int Add(DockPanelFloatForm value)
            {
                if (this.Locked) return -1;
                //
                if (this.innerList.Contains(value)) return -1;
                //
                ((ISetDockPanelManagerHelper)value).SetDockPanelManager(owner);
                this.innerList.Add(value);
                //
                return this.innerList.Count - 1;
            }

            public override void Clear()
            {
                if (this.Locked) return;
                //
                this.innerList.Clear();
            }

            public override bool Contains(DockPanelFloatForm value)
            {
                return this.innerList.Contains(value);
            }

            public override int IndexOf(DockPanelFloatForm value)
            {
                return this.innerList.IndexOf(value);
            }

            public override  void Insert(int index, DockPanelFloatForm value)
            {
                if (this.Locked) return;
                //
                if (this.innerList.Contains(value)) return;
                //
                ((ISetDockPanelManagerHelper)value).SetDockPanelManager(owner);
                this.innerList.Insert(index, value);
            }

            public override void Remove(DockPanelFloatForm value)
            {
                if (this.Locked) return;
                //
                this.innerList.Remove(value);
            }

            public override void RemoveAt(int index)
            {
                if (this.Locked) return;
                //
                this.innerList.RemoveAt(index);
            }

            public override IEnumerator GetEnumerator()
            {
                return this.innerList.GetEnumerator();
            }

            [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public override DockPanelFloatForm this[int index]
            {
                get
                {
                    if ((index < 0) || (index >= this.Count))
                    {
                        return null;
                    }
                    return this.innerList[index];
                }
                set
                {
                    if (this.Locked) return;
                    //
                    this.innerList[index] = value;
                }
            }

            public override int Count
            {
                get
                {
                    return this.innerList.Count;
                }
            }

            public override bool ExchangeItemT(DockPanelFloatForm item1, DockPanelFloatForm item2)
            {
                if (this.Locked) return false;
                //
                if (item1 == null || item2 == null || item1 == item2) return false;
                //
                int index1 = this.innerList.IndexOf(item1);
                int index2 = this.innerList.IndexOf(item2);
                //
                if (index1 == index2) return false;
                if ((index1 < 0) || (index1 >= this.innerList.Count)) return false;
                if ((index2 < 0) || (index2 >= this.innerList.Count)) return false;
                //
                if (index1 < index2)
                {
                    this.innerList.Remove(item2);
                    this.innerList.Remove(item1);
                    this.innerList.Insert(index1, item2);
                    this.innerList.Insert(index2, item1);
                }
                else
                {
                    this.innerList.Remove(item1);
                    this.innerList.Remove(item2);
                    this.innerList.Insert(index2, item1);
                    this.innerList.Insert(index1, item2);
                }
                //
                return true;
            }

            public DockPanelFloatForm this[string name]
            {
                get
                {
                    foreach (DockPanelFloatForm one in this.innerList)
                    {
                        if (one.Name == name) return one;
                    }
                    //
                    return null;
                }
            }

            //
            //
            //

            public void SetRecordID()
            {
                for (int i = 0; i < this.innerList.Count; i++)
                {
                    WFNew.ISetRecordItemHelper pSetRecordItemHelper = this.innerList[i] as WFNew.ISetRecordItemHelper;
                    if (pSetRecordItemHelper != null) pSetRecordItemHelper.SetRecordID(i);
                }
            }

            public DockPanelFloatForm GetItem(int recordID)
            {
                for (int i = 0; i < this.Count; i++)
                {
                    if (this[i].RecordID == recordID) return this[i];
                }
                return null;
            }
        }
        #endregion

        #region 附属类（已抛弃）
        ///// <summary>
        ///// 基础面板收集器（由系统辅助管理 所新成员必须加载到收集器中否则会出现异常）
        ///// </summary>
        //public class BasePanelCollection : ICollection, IEnumerable
        //{
        //    private List<BasePanel> innerList = null;

        //    public BasePanelCollection()
        //    {
        //        this.innerList = new List<BasePanel>();
        //    }

        //    public void Add(BasePanel value)
        //    {
        //        if (this.innerList.Contains(value))
        //            return;
        //        //
        //        this.innerList.Add(value);
        //    }

        //    public void AddRange(IEnumerable<BasePanel> collection)
        //    {
        //        foreach (BasePanel one in collection)
        //        {
        //            this.Add(one);
        //        }
        //    }

        //    public void Clear()
        //    {
        //        this.innerList.Clear();
        //    }

        //    public bool Contains(BasePanel value)
        //    {
        //        return this.innerList.Contains(value);
        //    }

        //    public int IndexOf(BasePanel value)
        //    {
        //        return this.innerList.IndexOf(value);
        //    }

        //    public void Insert(int index, BasePanel value)
        //    {
        //        if (this.innerList.Contains(value)) return;
        //        //
        //        this.innerList.Insert(index, value);
        //    }

        //    public void Remove(BasePanel value)
        //    {
        //        this.innerList.Remove(value);
        //    }

        //    public void RemoveAt(int index)
        //    {
        //        this.innerList.RemoveAt(index);
        //    }

        //    public IEnumerator GetEnumerator()
        //    {
        //        return this.innerList.GetEnumerator();
        //    }

        //    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //    public virtual BasePanel this[int index]
        //    {
        //        get
        //        {
        //            if ((index < 0) || (index >= this.Count))
        //            {
        //                return null;
        //            }
        //            return this.innerList[index];
        //        }
        //        set
        //        {
        //            this.innerList[index] = value;
        //        }
        //    }

        //    #region ICollection
        //    public int Count
        //    {
        //        get
        //        {
        //            return this.innerList.Count;
        //        }
        //    }

        //    object ICollection.SyncRoot
        //    {
        //        get
        //        {
        //            return this;
        //        }
        //    }

        //    bool ICollection.IsSynchronized
        //    {
        //        get
        //        {
        //            return false;
        //        }
        //    }

        //    void ICollection.CopyTo(Array destination, int index)
        //    {
        //        BasePanel[] destinationArray = new BasePanel[this.Count];
        //        if (this.Count > 0)
        //        {
        //            this.innerList.CopyTo(destinationArray, index);
        //        }
        //        for (int i = 0; i < destinationArray.Length; i++)
        //        {
        //            destination.SetValue(destinationArray[i], i);
        //        }
        //    }
        //    #endregion

        //    //
        //    //
        //    //

        //    public void SetRecordID()
        //    {
        //        for (int i = 0; i < this.Count; i++)
        //        {
        //            this[i].SetRecordID(i);
        //        }
        //    }

        //    public BasePanel GetItem(int recordID)
        //    {
        //        for (int i = 0; i < this.Count; i++)
        //        {
        //            if (this[i].RecordID == recordID) return this[i];
        //        }
        //        return null;
        //    }

        //    public BasePanel GetItem(string name)
        //    {
        //        for (int i = 0; i < this.Count; i++)
        //        {
        //            if (this[i].Name == name) return this[i];
        //        }
        //        return null;
        //    }
        //}

        ///// <summary>
        ///// 停靠面板收集器（由系统辅助管理 所新成员必须加载到收集器中否则会出现异常）
        ///// </summary>
        //public class DockPanelCollection : ICollection, IEnumerable
        //{
        //    private DockPanelManager owner = null;
        //    private List<DockPanel> innerList = null;

        //    public DockPanelCollection(DockPanelManager dockPanelManager)
        //    {
        //        this.owner = dockPanelManager;
        //        this.innerList = new List<DockPanel>();
        //    }

        //    public void Add(DockPanel value)
        //    {
        //        if (this.innerList.Contains(value)) return;
        //        //
        //        ((ISetDockPanelManagerHelper)value).SetDockPanelManager(owner);
        //        this.innerList.Add(value);
        //    }

        //    public void AddRange(IEnumerable<DockPanel> collection)
        //    {
        //        foreach (DockPanel one in collection)
        //        {
        //            this.Add(one);
        //        }
        //    }

        //    public void Clear()
        //    {
        //        this.innerList.Clear();
        //    }

        //    public bool Contains(DockPanel value)
        //    {
        //        return this.innerList.Contains(value);
        //    }

        //    public int IndexOf(DockPanel value)
        //    {
        //        return this.innerList.IndexOf(value);
        //    }

        //    public void Insert(int index, DockPanel value)
        //    {
        //        if (this.innerList.Contains(value)) return;
        //        //
        //        ((ISetDockPanelManagerHelper)value).SetDockPanelManager(owner);
        //        this.innerList.Insert(index, value);
        //    }

        //    public void Remove(DockPanel value)
        //    {
        //        this.innerList.Remove(value);
        //    }

        //    public void RemoveAt(int index)
        //    {
        //        this.innerList.RemoveAt(index);
        //    }

        //    public IEnumerator GetEnumerator()
        //    {
        //        return this.innerList.GetEnumerator();
        //    }

        //    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //    public virtual DockPanel this[int index]
        //    {
        //        get
        //        {
        //            if ((index < 0) || (index >= this.Count))
        //            {
        //                return null;
        //            }
        //            return this.innerList[index];
        //        }
        //        set
        //        {
        //            this.innerList[index] = value;
        //        }
        //    }

        //    #region ICollection
        //    public int Count
        //    {
        //        get
        //        {
        //            return this.innerList.Count;
        //        }
        //    }

        //    object ICollection.SyncRoot
        //    {
        //        get
        //        {
        //            return this;
        //        }
        //    }

        //    bool ICollection.IsSynchronized
        //    {
        //        get
        //        {
        //            return false;
        //        }
        //    }

        //    void ICollection.CopyTo(Array destination, int index)
        //    {
        //        DockPanel[] destinationArray = new DockPanel[this.Count];
        //        if (this.Count > 0)
        //        {
        //            this.innerList.CopyTo(destinationArray, index);
        //        }
        //        for (int i = 0; i < destinationArray.Length; i++)
        //        {
        //            destination.SetValue(destinationArray[i], i);
        //        }
        //    }
        //    #endregion

        //    //
        //    //
        //    //

        //    public void SetRecordID()
        //    {
        //        for (int i = 0; i < this.Count; i++)
        //        {
        //            this[i].SetRecordID(i);
        //        }
        //    }

        //    public DockPanel GetItem(int recordID)
        //    {
        //        for (int i = 0; i < this.Count; i++)
        //        {
        //            if (this[i].RecordID == recordID) return this[i];
        //        }
        //        return null;
        //    }

        //    public DockPanel GetItem(string name)
        //    {
        //        for (int i = 0; i < this.Count; i++)
        //        {
        //            if (this[i].Name == name) return this[i];
        //        }
        //        return null;
        //    }
        //}

        ///// <summary>
        ///// 停靠面板容器收集器（由系统自动 添加、删除、注销 资源）
        ///// </summary>
        //public class DockPanelContainerCollection : ICollection, IEnumerable
        //{
        //    private DockPanelManager owner = null;
        //    private List<DockPanelContainer> innerList = null;

        //    public DockPanelContainerCollection(DockPanelManager dockPanelManager)
        //    {
        //        this.owner = dockPanelManager;
        //        this.innerList = new List<DockPanelContainer>();
        //    }

        //    public void Add(DockPanelContainer value)
        //    {
        //        if (this.innerList.Contains(value)) return;
        //        //
        //        ((ISetDockPanelManagerHelper)value).SetDockPanelManager(owner);
        //        this.innerList.Add(value);
        //    }

        //    public void AddRange(IEnumerable<DockPanelContainer> collection)
        //    {
        //        foreach (DockPanelContainer one in collection)
        //        {
        //            this.Add(one);
        //        }
        //    }

        //    public void Clear()
        //    {
        //        this.innerList.Clear();
        //    }

        //    public bool Contains(DockPanelContainer value)
        //    {
        //        return this.innerList.Contains(value);
        //    }

        //    public int IndexOf(DockPanelContainer value)
        //    {
        //        return this.innerList.IndexOf(value);
        //    }

        //    public void Insert(int index, DockPanelContainer value)
        //    {
        //        if (this.innerList.Contains(value)) return;
        //        //
        //        ((ISetDockPanelManagerHelper)value).SetDockPanelManager(owner);
        //        this.innerList.Insert(index, value);
        //    }

        //    public void Remove(DockPanelContainer value)
        //    {
        //        this.innerList.Remove(value);
        //    }

        //    public void RemoveAt(int index)
        //    {
        //        this.innerList.RemoveAt(index);
        //    }

        //    public IEnumerator GetEnumerator()
        //    {
        //        return this.innerList.GetEnumerator();
        //    }

        //    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //    public virtual DockPanelContainer this[int index]
        //    {
        //        get
        //        {
        //            if ((index < 0) || (index >= this.Count))
        //            {
        //                return null;
        //            }
        //            return this.innerList[index];
        //        }
        //        set
        //        {
        //            this.innerList[index] = value;
        //        }
        //    }

        //    #region ICollection
        //    public int Count
        //    {
        //        get
        //        {
        //            return this.innerList.Count;
        //        }
        //    }

        //    object ICollection.SyncRoot
        //    {
        //        get
        //        {
        //            return this;
        //        }
        //    }

        //    bool ICollection.IsSynchronized
        //    {
        //        get
        //        {
        //            return false;
        //        }
        //    }

        //    void ICollection.CopyTo(Array destination, int index)
        //    {
        //        DockPanelContainer[] destinationArray = new DockPanelContainer[this.Count];
        //        if (this.Count > 0)
        //        {
        //            this.innerList.CopyTo(destinationArray, index);
        //        }
        //        for (int i = 0; i < destinationArray.Length; i++)
        //        {
        //            destination.SetValue(destinationArray[i], i);
        //        }
        //    }
        //    #endregion

        //    //
        //    //
        //    //

        //    public void SetRecordID()
        //    {
        //        for (int i = 0; i < this.Count; i++)
        //        {
        //            this[i].SetRecordID(i);
        //        }
        //    }

        //    public DockPanelContainer GetItem(int recordID)
        //    {
        //        for (int i = 0; i < this.Count; i++)
        //        {
        //            if (this[i].RecordID == recordID) return this[i];
        //        }
        //        return null;
        //    }
        //}

        ///// <summary>
        ///// 停靠面板停靠区收集器（由系统自动 添加、删除、注销 资源）
        ///// </summary>
        //public class DockPanelDockAreaCollection : ICollection, IEnumerable
        //{
        //    private DockPanelManager owner = null;
        //    private List<DockPanelDockArea> innerList = null;

        //    public DockPanelDockAreaCollection(DockPanelManager dockPanelManager)
        //    {
        //        this.owner = dockPanelManager;
        //        this.innerList = new List<DockPanelDockArea>();
        //    }

        //    public void Add(DockPanelDockArea value)
        //    {
        //        if (this.innerList.Contains(value)) return;
        //        //
        //        ((ISetDockPanelManagerHelper)value).SetDockPanelManager(owner);
        //        this.innerList.Add(value);
        //    }

        //    public void AddRange(IEnumerable<DockPanelDockArea> collection)
        //    {
        //        foreach (DockPanelDockArea one in collection)
        //        {
        //            this.Add(one);
        //        }
        //    }

        //    public void Clear()
        //    {
        //        this.innerList.Clear();
        //    }

        //    public bool Contains(DockPanelDockArea value)
        //    {
        //        return this.innerList.Contains(value);
        //    }

        //    public int IndexOf(DockPanelDockArea value)
        //    {
        //        return this.innerList.IndexOf(value);
        //    }

        //    public void Insert(int index, DockPanelDockArea value)
        //    {
        //        if (this.innerList.Contains(value)) return;
        //        //
        //        ((ISetDockPanelManagerHelper)value).SetDockPanelManager(owner);
        //        this.innerList.Insert(index, value);
        //    }

        //    public void Remove(DockPanelDockArea value)
        //    {
        //        this.innerList.Remove(value);
        //    }

        //    public void RemoveAt(int index)
        //    {
        //        this.innerList.RemoveAt(index);
        //    }

        //    public IEnumerator GetEnumerator()
        //    {
        //        return this.innerList.GetEnumerator();
        //    }

        //    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //    public virtual DockPanelDockArea this[int index]
        //    {
        //        get
        //        {
        //            if ((index < 0) || (index >= this.Count))
        //            {
        //                return null;
        //            }
        //            return this.innerList[index];
        //        }
        //        set
        //        {
        //            this.innerList[index] = value;
        //        }
        //    }

        //    #region ICollection
        //    public int Count
        //    {
        //        get
        //        {
        //            return this.innerList.Count;
        //        }
        //    }

        //    object ICollection.SyncRoot
        //    {
        //        get
        //        {
        //            return this;
        //        }
        //    }

        //    bool ICollection.IsSynchronized
        //    {
        //        get
        //        {
        //            return false;
        //        }
        //    }

        //    void ICollection.CopyTo(Array destination, int index)
        //    {
        //        DockPanelDockArea[] destinationArray = new DockPanelDockArea[this.Count];
        //        if (this.Count > 0)
        //        {
        //            this.innerList.CopyTo(destinationArray, index);
        //        }
        //        for (int i = 0; i < destinationArray.Length; i++)
        //        {
        //            destination.SetValue(destinationArray[i], i);
        //        }
        //    }
        //    #endregion

        //    //
        //    //
        //    //

        //    public void SetRecordID()
        //    {
        //        for (int i = 0; i < this.Count; i++)
        //        {
        //            this[i].SetRecordID(i);
        //        }
        //    }

        //    public DockPanelDockArea GetItem(int recordID)
        //    {
        //        for (int i = 0; i < this.Count; i++)
        //        {
        //            if (this[i].RecordID == recordID) return this[i];
        //        }
        //        return null;
        //    }
        //}

        ///// <summary>
        ///// 停靠面板浮动窗体收集器（由系统自动 添加、删除、注销 资源）
        ///// </summary>
        //internal class DockPanelFloatFormCollection : ICollection, IEnumerable
        //{
        //    private DockPanelManager owner = null;
        //    private List<DockPanelFloatForm> innerList = null;

        //    public DockPanelFloatFormCollection(DockPanelManager dockPanelManager)
        //    {
        //        this.owner = dockPanelManager;
        //        this.innerList = new List<DockPanelFloatForm>();
        //    }

        //    public void Add(DockPanelFloatForm value)
        //    {
        //        if (this.innerList.Contains(value)) return;
        //        //
        //        ((ISetDockPanelManagerHelper)value).SetDockPanelManager(owner);
        //        this.innerList.Add(value);
        //    }

        //    public void AddRange(IEnumerable<DockPanelFloatForm> collection)
        //    {
        //        foreach (DockPanelFloatForm one in collection)
        //        {
        //            this.Add(one);
        //        }
        //    }

        //    public void Clear()
        //    {
        //        this.innerList.Clear();
        //    }

        //    public bool Contains(DockPanelFloatForm value)
        //    {
        //        return this.innerList.Contains(value);
        //    }

        //    public int IndexOf(DockPanelFloatForm value)
        //    {
        //        return this.innerList.IndexOf(value);
        //    }

        //    public void Insert(int index, DockPanelFloatForm value)
        //    {
        //        if (this.innerList.Contains(value)) return;
        //        //
        //        ((ISetDockPanelManagerHelper)value).SetDockPanelManager(owner);
        //        this.innerList.Insert(index, value);
        //    }

        //    public void Remove(DockPanelFloatForm value)
        //    {
        //        this.innerList.Remove(value);
        //    }

        //    public void RemoveAt(int index)
        //    {
        //        this.innerList.RemoveAt(index);
        //    }

        //    public IEnumerator GetEnumerator()
        //    {
        //        return this.innerList.GetEnumerator();
        //    }

        //    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //    public virtual DockPanelFloatForm this[int index]
        //    {
        //        get
        //        {
        //            if ((index < 0) || (index >= this.Count))
        //            {
        //                return null;
        //            }
        //            return this.innerList[index];
        //        }
        //        set
        //        {
        //            this.innerList[index] = value;
        //        }
        //    }

        //    #region ICollection
        //    public int Count
        //    {
        //        get
        //        {
        //            return this.innerList.Count;
        //        }
        //    }

        //    object ICollection.SyncRoot
        //    {
        //        get
        //        {
        //            return this;
        //        }
        //    }

        //    bool ICollection.IsSynchronized
        //    {
        //        get
        //        {
        //            return false;
        //        }
        //    }

        //    void ICollection.CopyTo(Array destination, int index)
        //    {
        //        DockPanelFloatForm[] destinationArray = new DockPanelFloatForm[this.Count];
        //        if (this.Count > 0)
        //        {
        //            this.innerList.CopyTo(destinationArray, index);
        //        }
        //        for (int i = 0; i < destinationArray.Length; i++)
        //        {
        //            destination.SetValue(destinationArray[i], i);
        //        }
        //    }
        //    #endregion

        //    //
        //    //
        //    //

        //    public void SetRecordID()
        //    {
        //        for (int i = 0; i < this.Count; i++)
        //        {
        //            this[i].SetRecordID(i);
        //        }
        //    }

        //    public DockPanelFloatForm GetItem(int recordID)
        //    {
        //        for (int i = 0; i < this.Count; i++)
        //        {
        //            if (this[i].RecordID == recordID) return this[i];
        //        }
        //        return null;
        //    }
        //}
        #endregion
    }

}
