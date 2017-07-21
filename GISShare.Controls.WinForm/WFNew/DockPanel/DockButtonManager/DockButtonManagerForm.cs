using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew.DockPanel
{
    sealed class DockButtonManagerForm : BaseItemForm, WFNew.IOwner, WFNew.IBaseItem, WFNew.ICollectionItem//, WFNew.IBaseItemOwner, WFNew.IBaseItemOwner2
    {
        #region 私有变量
        private Timer m_Timer;                                  //用来检测鼠标指针是否存靠面按钮板内（存在则激活，不存在则不激活）
        //
        private DockPanel m_DockPanel = null;                   //获取包涵当前坐标的停靠面板，以便于布局内部停靠按钮组
        private DockPanelFloatForm m_DockPanelFloatForm = null; //当前的浮动窗体
        //
        private DockShadowForm m_DockShadowForm = null;         //阴影窗体
        //
        private Rectangle m_Rectangle;                          //工作区矩形                          （在Show(...)时已完成设置）
        private Rectangle m_RectangleExceptDockArea;            //排除停靠区后的工作区矩形            （在Show(...)时已完成设置）
        private Point m_ParentWorkRegionCentrePoint;            //工作区中心点，用来绘制内部停靠按钮组（在Show(...)时已完成设置）
        //
        private Rectangle m_RectangleOutTop;                    //浮动窗体顶部停靠阴影矩形框（外部停靠，在Show(...)时已完成设置）
        private Rectangle m_RectangleOutLeft;                   //浮动窗体左边停靠阴影矩形框（外部停靠，在Show(...)时已完成设置）
        private Rectangle m_RectangleOutRight;                  //浮动窗体右边停靠阴影矩形框（外部停靠，在Show(...)时已完成设置）
        private Rectangle m_RectangleOutBottom;                 //浮动窗体底部停靠阴影矩形框（外部停靠，在Show(...)时已完成设置）
        private Rectangle m_RectangleInternalTop;               //浮动窗体顶部停靠阴影矩形框（内部停靠，在Show(...)时已完成设置）
        private Rectangle m_RectangleInternalLeft;              //浮动窗体左边停靠阴影矩形框（内部停靠，在Show(...)时已完成设置）
        private Rectangle m_RectangleInternalRight;             //浮动窗体右边停靠阴影矩形框（内部停靠，在Show(...)时已完成设置）
        private Rectangle m_RectangleInternalBottom;            //浮动窗体底部停靠阴影矩形框（内部停靠，在Show(...)时已完成设置）
        #endregion

        public DockButtonManagerForm()
        {
            this.m_DockShadowForm = new DockShadowForm();
            //
            this.m_BaseItemCollection = new GISShare.Controls.WinForm.WFNew.BaseItemCollection(this);
            this.m_CenterToDockFillButtonItem = new DockButtonItem(DockButtonStyle.eCenterToDockFill);
            this.m_BaseItemCollection.Add(this.m_CenterToDockFillButtonItem);
            this.m_CenterToDocumentUpButtonItem = new DockButtonItem(DockButtonStyle.eCenterToDocumentUp);
            this.m_BaseItemCollection.Add(this.m_CenterToDocumentUpButtonItem);
            this.m_CenterToDocumentLeftButtonItem = new DockButtonItem(DockButtonStyle.eCenterToDocumentLeft);
            this.m_BaseItemCollection.Add(this.m_CenterToDocumentLeftButtonItem);
            this.m_CenterToDocumentRightButtonItem = new DockButtonItem(DockButtonStyle.eCenterToDocumentRight);
            this.m_BaseItemCollection.Add(this.m_CenterToDocumentRightButtonItem);
            this.m_CenterToDocumentBottomButtonItem = new DockButtonItem(DockButtonStyle.eCenterToDocumentBottom);
            this.m_BaseItemCollection.Add(this.m_CenterToDocumentBottomButtonItem);
            this.m_CenterToDockUpButtonItem = new DockButtonItem(DockButtonStyle.eCenterToDockUp);
            this.m_BaseItemCollection.Add(this.m_CenterToDockUpButtonItem);
            this.m_CenterToDockLeftButtonItem = new DockButtonItem(DockButtonStyle.eCenterToDockLeft);
            this.m_BaseItemCollection.Add(this.m_CenterToDockLeftButtonItem);
            this.m_CenterToDockRightButtonItem = new DockButtonItem(DockButtonStyle.eCenterToDockRight);
            this.m_BaseItemCollection.Add(this.m_CenterToDockRightButtonItem);
            this.m_CenterToDockBottomButtonItem = new DockButtonItem(DockButtonStyle.eCenterToDockBottom);
            this.m_BaseItemCollection.Add(this.m_CenterToDockBottomButtonItem);
            this.m_ToDockUpButtonItem = new DockButtonItem(DockButtonStyle.eToDockUp);
            this.m_BaseItemCollection.Add(this.m_ToDockUpButtonItem);
            this.m_ToDockLeftButtonItem = new DockButtonItem(DockButtonStyle.eToDockLeft);
            this.m_BaseItemCollection.Add(this.m_ToDockLeftButtonItem);
            this.m_ToDockRightButtonItem = new DockButtonItem(DockButtonStyle.eToDockRight);
            this.m_BaseItemCollection.Add(this.m_ToDockRightButtonItem);
            this.m_ToDockBottomButtonItem = new DockButtonItem(DockButtonStyle.eToDockBottom);
            this.m_BaseItemCollection.Add(this.m_ToDockBottomButtonItem);
            ((WFNew.ILockCollectionHelper)this.m_BaseItemCollection).SetLocked(true);
            //
            this.Opacity = 0;
            this.KeyPreview = true;
            this.TopMost = true;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Name = "DockButtonManagerForm";
            this.Text = "DockButtonManagerForm";
            this.TransparencyKey = System.Drawing.SystemColors.Control;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            //
            //
            //
            this.m_Timer = new Timer();
            this.m_Timer.Interval = 500;
            this.m_Timer.Tick += new EventHandler(Timer_Tick);
        }

        bool m_bCanDockUp = true;
        bool m_bCanDockLeft = true;
        bool m_bCanDockRight = true;
        bool m_bCanDockBottom = true;
        bool m_bCanDockFill = true;
        bool m_bCanFloat = true;
        bool m_bCanHide = true;
        bool m_bCanClose = true;
        bool m_bIsBasePanel = true;
        bool m_bIsDocumentPanel = true;

        private DockButtonItem m_CenterToDockFillButtonItem;
        //
        private DockButtonItem m_CenterToDocumentUpButtonItem;
        private DockButtonItem m_CenterToDocumentLeftButtonItem;
        private DockButtonItem m_CenterToDocumentRightButtonItem;
        private DockButtonItem m_CenterToDocumentBottomButtonItem;
        //
        private DockButtonItem m_CenterToDockUpButtonItem;
        private DockButtonItem m_CenterToDockLeftButtonItem;
        private DockButtonItem m_CenterToDockRightButtonItem;
        private DockButtonItem m_CenterToDockBottomButtonItem;
        //
        private DockButtonItem m_ToDockUpButtonItem;
        private DockButtonItem m_ToDockLeftButtonItem;
        private DockButtonItem m_ToDockRightButtonItem;
        private DockButtonItem m_ToDockBottomButtonItem;

        private bool HaveDocumentDockArea
        {
            get 
            {
                return this.m_DockPanelFloatForm != null && 
                    this.m_DockPanelFloatForm.DockPanelManager != null &&
                    this.m_DockPanelFloatForm.DockPanelManager.DocumentArea != null &&
                    this.m_DockPanelFloatForm.DockPanelManager.DocumentArea is DocumentDockArea;
            }
        }

        private Rectangle DocumentRectangle
        {
            get 
            {
                if (this.HaveDocumentDockArea) 
                {
                    if (this.m_DockPanelFloatForm.DockPanelManager.DocumentArea is DocumentDockArea)
                    {
                        return ((DocumentDockArea)this.m_DockPanelFloatForm.DockPanelManager.DocumentArea).DockAreaRectangle;
                    }
                    else
                    {
                        return this.m_DockPanelFloatForm.DockPanelManager.DocumentArea.DisplayRectangle;
                    }
                }
                return this.m_Rectangle;
            }
        }

        #region WFNew.IBaseItem
        public WFNew.BaseItemState eBaseItemState
        {
            get
            {
                return GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal;
            }
        }
        #endregion

        #region WFNew.ICollectionItem
        bool WFNew.ICollectionItem.HaveVisibleBaseItem 
        {
            get
            {
                foreach (WFNew.BaseItem one in this.m_BaseItemCollection)
                {
                    if (one.Visible) return true;
                }
                //
                return false;
            }
        }

        WFNew.BaseItemCollection m_BaseItemCollection;
        WFNew.BaseItemCollection WFNew.ICollectionItem.BaseItems 
        {
            get { return this.m_BaseItemCollection; }
        }
        #endregion

        #region 覆盖
        protected override void OnClosing(CancelEventArgs e)//关闭时根据停靠按钮激活状态进行停靠
        {
            this.m_Timer.Stop();
            //
            this.m_DockPanelFloatForm.DockPanelManager.IsDockLayoutState = false;
            //
            Point location = new Point(0, 0);
            if (this.m_DockPanelFloatForm != null &&
                this.m_DockPanelFloatForm.DockPanelManager != null &&
                this.m_DockPanelFloatForm.DockPanelManager.ParentForm != null)
            { 
                location = this.m_DockPanelFloatForm.DockPanelManager.ParentForm.PointToClient(this.m_DockShadowForm.Location);
            }
            //
            if (this.m_ToDockUpButtonItem.eBaseItemState == GISShare.Controls.WinForm.WFNew.BaseItemState.eHot)
            { this.m_DockPanelFloatForm.ToDockArea(false, DockStyle.Top, location); }
            else if (this.m_ToDockLeftButtonItem.eBaseItemState == GISShare.Controls.WinForm.WFNew.BaseItemState.eHot)
            { this.m_DockPanelFloatForm.ToDockArea(false, DockStyle.Left, location); }
            else if (this.m_ToDockRightButtonItem.eBaseItemState == GISShare.Controls.WinForm.WFNew.BaseItemState.eHot)
            { this.m_DockPanelFloatForm.ToDockArea(false, DockStyle.Right, location); }
            else if (this.m_ToDockBottomButtonItem.eBaseItemState == GISShare.Controls.WinForm.WFNew.BaseItemState.eHot)
            { this.m_DockPanelFloatForm.ToDockArea(false, DockStyle.Bottom, location); }
            //——————————————————————————————————————————————————————————
            else if (this.m_DockPanel != null)
            {
                DockAreaStyle eDockAreaStyle = this.m_DockPanel.GetDockAreaStyle();
                if (eDockAreaStyle == DockAreaStyle.eDocumentDockArea)
                {
                    if (this.m_CenterToDockUpButtonItem.eBaseItemState == GISShare.Controls.WinForm.WFNew.BaseItemState.eHot)
                    { this.m_DockPanelFloatForm.ToDockArea(true, DockStyle.Top, location); }
                    else if (this.m_CenterToDockLeftButtonItem.eBaseItemState == GISShare.Controls.WinForm.WFNew.BaseItemState.eHot)
                    { this.m_DockPanelFloatForm.ToDockArea(true, DockStyle.Left, location); }
                    else if (this.m_CenterToDockRightButtonItem.eBaseItemState == GISShare.Controls.WinForm.WFNew.BaseItemState.eHot)
                    { this.m_DockPanelFloatForm.ToDockArea(true, DockStyle.Right, location); }
                    else if (this.m_CenterToDockBottomButtonItem.eBaseItemState == GISShare.Controls.WinForm.WFNew.BaseItemState.eHot)
                    { this.m_DockPanelFloatForm.ToDockArea(true, DockStyle.Bottom, location); }
                    //
                    if (this.m_CenterToDockFillButtonItem.eBaseItemState == GISShare.Controls.WinForm.WFNew.BaseItemState.eHot)
                    { this.m_DockPanel.AddDockPanel(this.m_DockPanelFloatForm.GetIDockPanel(), DockStyle.Fill); }
                    else if (this.m_CenterToDocumentUpButtonItem.eBaseItemState == GISShare.Controls.WinForm.WFNew.BaseItemState.eHot)
                    { this.m_DockPanel.AddDockPanel(this.m_DockPanelFloatForm.GetIDockPanel(), DockStyle.Top); }
                    else if (this.m_CenterToDocumentLeftButtonItem.eBaseItemState == GISShare.Controls.WinForm.WFNew.BaseItemState.eHot)
                    { this.m_DockPanel.AddDockPanel(this.m_DockPanelFloatForm.GetIDockPanel(), DockStyle.Left); }
                    else if (this.m_CenterToDocumentRightButtonItem.eBaseItemState == GISShare.Controls.WinForm.WFNew.BaseItemState.eHot)
                    { this.m_DockPanel.AddDockPanel(this.m_DockPanelFloatForm.GetIDockPanel(), DockStyle.Right); }
                    else if (this.m_CenterToDocumentBottomButtonItem.eBaseItemState == GISShare.Controls.WinForm.WFNew.BaseItemState.eHot)
                    { this.m_DockPanel.AddDockPanel(this.m_DockPanelFloatForm.GetIDockPanel(), DockStyle.Bottom); }
                }
                else if (eDockAreaStyle != DockAreaStyle.eNone)
                {
                    if (this.m_CenterToDockFillButtonItem.eBaseItemState == GISShare.Controls.WinForm.WFNew.BaseItemState.eHot)
                    { this.m_DockPanel.AddDockPanel(this.m_DockPanelFloatForm.GetIDockPanel(), DockStyle.Fill); }
                    else if (this.m_CenterToDockUpButtonItem.eBaseItemState == GISShare.Controls.WinForm.WFNew.BaseItemState.eHot)
                    { this.m_DockPanel.AddDockPanel(this.m_DockPanelFloatForm.GetIDockPanel(), DockStyle.Top); }
                    else if (this.m_CenterToDockLeftButtonItem.eBaseItemState == GISShare.Controls.WinForm.WFNew.BaseItemState.eHot)
                    { this.m_DockPanel.AddDockPanel(this.m_DockPanelFloatForm.GetIDockPanel(), DockStyle.Left); }
                    else if (this.m_CenterToDockRightButtonItem.eBaseItemState == GISShare.Controls.WinForm.WFNew.BaseItemState.eHot)
                    { this.m_DockPanel.AddDockPanel(this.m_DockPanelFloatForm.GetIDockPanel(), DockStyle.Right); }
                    else if (this.m_CenterToDockBottomButtonItem.eBaseItemState == GISShare.Controls.WinForm.WFNew.BaseItemState.eHot)
                    { this.m_DockPanel.AddDockPanel(this.m_DockPanelFloatForm.GetIDockPanel(), DockStyle.Bottom); }
                }
            }
            else
            {
                if (this.m_CenterToDockFillButtonItem.eBaseItemState == GISShare.Controls.WinForm.WFNew.BaseItemState.eHot)
                { this.m_DockPanelFloatForm.ToDockArea(true, DockStyle.Fill, location); }
                else if (this.m_CenterToDockUpButtonItem.eBaseItemState == GISShare.Controls.WinForm.WFNew.BaseItemState.eHot)
                { this.m_DockPanelFloatForm.ToDockArea(true, DockStyle.Top, location); }
                else if (this.m_CenterToDockLeftButtonItem.eBaseItemState == GISShare.Controls.WinForm.WFNew.BaseItemState.eHot)
                { this.m_DockPanelFloatForm.ToDockArea(true, DockStyle.Left, location); }
                else if (this.m_CenterToDockRightButtonItem.eBaseItemState == GISShare.Controls.WinForm.WFNew.BaseItemState.eHot)
                { this.m_DockPanelFloatForm.ToDockArea(true, DockStyle.Right, location); }
                else if (this.m_CenterToDockBottomButtonItem.eBaseItemState == GISShare.Controls.WinForm.WFNew.BaseItemState.eHot)
                { this.m_DockPanelFloatForm.ToDockArea(true, DockStyle.Bottom, location); } 
            }
            //
            e.Cancel = false;
            base.OnClosing(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) { this.m_Timer.Dispose(); this.m_DockShadowForm.Close(); this.m_DockShadowForm.Dispose(); }
            base.Dispose(disposing);
        }
        #endregion

        #region 关联事件
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (this.m_DockPanelFloatForm == null || this.m_DockPanelFloatForm.DockPanelManager == null) return;
            //
            this.SetDockButtonItemBaseItemState();
            //
            bool bRefresh = false;
            Point dockPanelCentrePoint = new Point();
            this.m_DockPanel = this.m_DockPanelFloatForm.DockPanelManager.GetDockPanelContainsMousePoint(MousePosition, ref dockPanelCentrePoint);
            if (this.m_DockPanel != null)
            {
                DockAreaStyle eDockAreaStyle = this.m_DockPanel.GetDockAreaStyle();
                if (eDockAreaStyle == DockAreaStyle.eDocumentDockArea)
                {
                    #region 布局中心区
                    this.m_CenterToDockFillButtonItem.Visible = this.m_bIsDocumentPanel && this.m_bCanDockFill && this.HaveDocumentDockArea;
                    //
                    this.m_CenterToDocumentUpButtonItem.Visible = this.m_bIsDocumentPanel && this.m_bCanDockUp && this.HaveDocumentDockArea;
                    this.m_CenterToDocumentLeftButtonItem.Visible = this.m_bIsDocumentPanel && this.m_bCanDockLeft && this.HaveDocumentDockArea;
                    this.m_CenterToDocumentRightButtonItem.Visible = this.m_bIsDocumentPanel && this.m_bCanDockRight && this.HaveDocumentDockArea;
                    this.m_CenterToDocumentBottomButtonItem.Visible = this.m_bIsDocumentPanel && this.m_bCanDockBottom && this.HaveDocumentDockArea;
                    //
                    this.m_CenterToDockUpButtonItem.Visible = this.m_bIsBasePanel && this.m_bCanDockUp;
                    this.m_CenterToDockLeftButtonItem.Visible = this.m_bIsBasePanel && this.m_bCanDockLeft;
                    this.m_CenterToDockRightButtonItem.Visible = this.m_bIsBasePanel && this.m_bCanDockRight;
                    this.m_CenterToDockBottomButtonItem.Visible = this.m_bIsBasePanel && this.m_bCanDockBottom;
                    //
                    bRefresh |= ((WFNew.ISetBaseItemHelper)this.m_CenterToDockFillButtonItem).SetLocation
                       (
                           dockPanelCentrePoint.X - this.m_CenterToDockFillButtonItem.Width / 2,
                           dockPanelCentrePoint.Y - this.m_CenterToDockFillButtonItem.Height / 2
                       );
                    bRefresh |= ((WFNew.ISetBaseItemHelper)this.m_CenterToDockUpButtonItem).SetLocation
                        (
                            dockPanelCentrePoint.X - this.m_CenterToDockUpButtonItem.Width / 2,
                            dockPanelCentrePoint.Y - this.m_CenterToDocumentUpButtonItem.Height - this.m_CenterToDockUpButtonItem.Height - this.m_CenterToDockFillButtonItem.Height / 2
                        );
                    bRefresh |= ((WFNew.ISetBaseItemHelper)this.m_CenterToDockLeftButtonItem).SetLocation
                        (
                            dockPanelCentrePoint.X - this.m_CenterToDocumentLeftButtonItem.Width - this.m_CenterToDockLeftButtonItem.Width - this.m_CenterToDockFillButtonItem.Width / 2,
                            dockPanelCentrePoint.Y - this.m_CenterToDockLeftButtonItem.Height / 2
                        );
                    bRefresh |= ((WFNew.ISetBaseItemHelper)this.m_CenterToDockRightButtonItem).SetLocation
                        (
                            dockPanelCentrePoint.X + this.m_CenterToDocumentRightButtonItem.Width + this.m_CenterToDockFillButtonItem.Width / 2,
                            dockPanelCentrePoint.Y - this.m_CenterToDockLeftButtonItem.Height / 2
                        );
                    bRefresh |= ((WFNew.ISetBaseItemHelper)this.m_CenterToDockBottomButtonItem).SetLocation
                        (
                            dockPanelCentrePoint.X - this.m_CenterToDockBottomButtonItem.Width / 2,
                            dockPanelCentrePoint.Y + this.m_CenterToDocumentBottomButtonItem.Height + this.m_CenterToDockFillButtonItem.Height / 2
                        );
                    //
                    bRefresh |= ((WFNew.ISetBaseItemHelper)this.m_CenterToDocumentUpButtonItem).SetLocation
                        (
                            dockPanelCentrePoint.X - this.m_CenterToDocumentUpButtonItem.Width / 2,
                            dockPanelCentrePoint.Y - this.m_CenterToDocumentUpButtonItem.Height - this.m_CenterToDockFillButtonItem.Height / 2
                        );
                    bRefresh |= ((WFNew.ISetBaseItemHelper)this.m_CenterToDocumentLeftButtonItem).SetLocation
                        (
                            dockPanelCentrePoint.X - this.m_CenterToDocumentLeftButtonItem.Width - this.m_CenterToDockFillButtonItem.Width / 2,
                            dockPanelCentrePoint.Y - this.m_CenterToDocumentLeftButtonItem.Height / 2
                        );
                    bRefresh |= ((WFNew.ISetBaseItemHelper)this.m_CenterToDocumentRightButtonItem).SetLocation
                        (
                            dockPanelCentrePoint.X + this.m_CenterToDockFillButtonItem.Width / 2,
                            dockPanelCentrePoint.Y - this.m_CenterToDocumentLeftButtonItem.Height / 2
                        );
                    bRefresh |= ((WFNew.ISetBaseItemHelper)this.m_CenterToDocumentBottomButtonItem).SetLocation
                        (
                            dockPanelCentrePoint.X - this.m_CenterToDocumentBottomButtonItem.Width / 2,
                            dockPanelCentrePoint.Y + this.m_CenterToDockFillButtonItem.Height / 2
                        );
                    #endregion
                }
                else if (eDockAreaStyle != DockAreaStyle.eNone)
                {
                    #region 布局中心区
                    this.m_CenterToDockFillButtonItem.Visible = this.m_bIsBasePanel && this.m_bCanDockFill;
                    //
                    this.m_CenterToDocumentUpButtonItem.Visible = false;
                    this.m_CenterToDocumentLeftButtonItem.Visible = false;
                    this.m_CenterToDocumentRightButtonItem.Visible = false;
                    this.m_CenterToDocumentBottomButtonItem.Visible = false;
                    //
                    this.m_CenterToDockUpButtonItem.Visible = this.m_bIsBasePanel && this.m_bCanDockUp;
                    this.m_CenterToDockLeftButtonItem.Visible = this.m_bIsBasePanel && this.m_bCanDockLeft;
                    this.m_CenterToDockRightButtonItem.Visible = this.m_bIsBasePanel && this.m_bCanDockRight;
                    this.m_CenterToDockBottomButtonItem.Visible = this.m_bIsBasePanel && this.m_bCanDockBottom;
                    //
                    bRefresh |= ((WFNew.ISetBaseItemHelper)this.m_CenterToDockFillButtonItem).SetLocation
                        (
                            dockPanelCentrePoint.X - this.m_CenterToDockFillButtonItem.Width / 2,
                            dockPanelCentrePoint.Y - this.m_CenterToDockFillButtonItem.Height / 2
                        );
                    bRefresh |= ((WFNew.ISetBaseItemHelper)this.m_CenterToDockUpButtonItem).SetLocation
                        (
                            dockPanelCentrePoint.X - this.m_CenterToDockUpButtonItem.Width / 2,
                            dockPanelCentrePoint.Y - this.m_CenterToDockUpButtonItem.Height - this.m_CenterToDockFillButtonItem.Height / 2
                        );
                    bRefresh |= ((WFNew.ISetBaseItemHelper)this.m_CenterToDockLeftButtonItem).SetLocation
                        (
                            dockPanelCentrePoint.X - this.m_CenterToDockLeftButtonItem.Width - this.m_CenterToDockFillButtonItem.Width / 2,
                            dockPanelCentrePoint.Y - this.m_CenterToDockLeftButtonItem.Height / 2
                        );
                    bRefresh |= ((WFNew.ISetBaseItemHelper)this.m_CenterToDockRightButtonItem).SetLocation
                        (
                            dockPanelCentrePoint.X + this.m_CenterToDockFillButtonItem.Width / 2,
                            dockPanelCentrePoint.Y - this.m_CenterToDockLeftButtonItem.Height / 2
                        );
                    bRefresh |= ((WFNew.ISetBaseItemHelper)this.m_CenterToDockBottomButtonItem).SetLocation
                        (
                            dockPanelCentrePoint.X - this.m_CenterToDockBottomButtonItem.Width / 2,
                            dockPanelCentrePoint.Y + this.m_CenterToDockFillButtonItem.Height / 2
                        );
                    #endregion
                }
            }
            else if (this.DocumentRectangle.Contains(MousePosition))
            {
                #region 布局中心区
                this.m_CenterToDockFillButtonItem.Visible = this.m_bIsDocumentPanel && this.m_bCanDockFill && this.HaveDocumentDockArea;
                //
                this.m_CenterToDocumentUpButtonItem.Visible = false;// this.m_bIsDocumentPanel && this.m_bCanDockUp && this.HaveDocumentDockArea;
                this.m_CenterToDocumentLeftButtonItem.Visible = false;// this.m_bIsDocumentPanel && this.m_bCanDockLeft && this.HaveDocumentDockArea;
                this.m_CenterToDocumentRightButtonItem.Visible = false;// this.m_bIsDocumentPanel && this.m_bCanDockRight && this.HaveDocumentDockArea;
                this.m_CenterToDocumentBottomButtonItem.Visible = false;// this.m_bIsDocumentPanel && this.m_bCanDockBottom && this.HaveDocumentDockArea;
                //
                this.m_CenterToDockUpButtonItem.Visible = this.m_bIsBasePanel && this.m_bCanDockUp;
                this.m_CenterToDockLeftButtonItem.Visible = this.m_bIsBasePanel && this.m_bCanDockLeft;
                this.m_CenterToDockRightButtonItem.Visible = this.m_bIsBasePanel && this.m_bCanDockRight;
                this.m_CenterToDockBottomButtonItem.Visible = this.m_bIsBasePanel && this.m_bCanDockBottom;                
                //
                bRefresh |= ((WFNew.ISetBaseItemHelper)this.m_CenterToDockFillButtonItem).SetLocation
                    (
                        this.m_ParentWorkRegionCentrePoint.X - this.m_CenterToDockFillButtonItem.Width / 2,
                        this.m_ParentWorkRegionCentrePoint.Y - this.m_CenterToDockFillButtonItem.Height / 2
                    );
                bRefresh |= ((WFNew.ISetBaseItemHelper)this.m_CenterToDockUpButtonItem).SetLocation
                    (
                        this.m_ParentWorkRegionCentrePoint.X - this.m_CenterToDockUpButtonItem.Width / 2,
                        this.m_ParentWorkRegionCentrePoint.Y - this.m_CenterToDockUpButtonItem.Height - this.m_CenterToDockFillButtonItem.Height / 2
                    );
                bRefresh |= ((WFNew.ISetBaseItemHelper)this.m_CenterToDockLeftButtonItem).SetLocation
                    (
                        this.m_ParentWorkRegionCentrePoint.X - this.m_CenterToDockLeftButtonItem.Width - this.m_CenterToDockFillButtonItem.Width / 2,
                        this.m_ParentWorkRegionCentrePoint.Y - this.m_CenterToDockLeftButtonItem.Height / 2
                    );
                bRefresh |= ((WFNew.ISetBaseItemHelper)this.m_CenterToDockRightButtonItem).SetLocation
                    (
                        this.m_ParentWorkRegionCentrePoint.X + this.m_CenterToDockFillButtonItem.Width / 2,
                        this.m_ParentWorkRegionCentrePoint.Y - this.m_CenterToDockLeftButtonItem.Height / 2
                    );
                bRefresh |= ((WFNew.ISetBaseItemHelper)this.m_CenterToDockBottomButtonItem).SetLocation
                    (
                        this.m_ParentWorkRegionCentrePoint.X - this.m_CenterToDockBottomButtonItem.Width / 2,
                        this.m_ParentWorkRegionCentrePoint.Y + this.m_CenterToDockFillButtonItem.Height / 2
                    );
                #endregion
            }
            if (bRefresh) this.Refresh();
            //
            //设置停靠影子
            //
            if (this.m_ToDockUpButtonItem.eBaseItemState == GISShare.Controls.WinForm.WFNew.BaseItemState.eHot) { this.m_DockShadowForm.Show(this.m_RectangleOutTop); }
            else if (this.m_ToDockLeftButtonItem.eBaseItemState == GISShare.Controls.WinForm.WFNew.BaseItemState.eHot) { this.m_DockShadowForm.Show(this.m_RectangleOutLeft); }
            else if (this.m_ToDockRightButtonItem.eBaseItemState == GISShare.Controls.WinForm.WFNew.BaseItemState.eHot) { this.m_DockShadowForm.Show(this.m_RectangleOutRight); }
            else if (this.m_ToDockBottomButtonItem.eBaseItemState == GISShare.Controls.WinForm.WFNew.BaseItemState.eHot) { this.m_DockShadowForm.Show(this.m_RectangleOutBottom); }
            //
            else if (this.m_DockPanel != null)
            {
                
                DockAreaStyle eDockAreaStyle = this.m_DockPanel.GetDockAreaStyle();
                if (eDockAreaStyle == DockAreaStyle.eDocumentDockArea)
                {
                    #region 停靠阴影
                    if (this.m_CenterToDockFillButtonItem.eBaseItemState == GISShare.Controls.WinForm.WFNew.BaseItemState.eHot)
                    {
                        int iWidth = this.m_DockPanel.Width / 2 - 2;
                        int iHeight = this.m_DockPanel.Height / 2 - 2;
                        this.m_DockShadowForm.Show(new Rectangle(dockPanelCentrePoint.X - iWidth, dockPanelCentrePoint.Y - iHeight, this.m_DockPanel.Width - 4, this.m_DockPanel.Height - 4));
                    }
                    //
                    else if (this.m_CenterToDocumentUpButtonItem.eBaseItemState == GISShare.Controls.WinForm.WFNew.BaseItemState.eHot)
                    {
                        int iWidth = this.m_DockPanel.Width / 2 - 2;
                        int iHeight = this.m_DockPanel.Height / 2 - 2;
                        this.m_DockShadowForm.Show(new Rectangle(dockPanelCentrePoint.X - iWidth, dockPanelCentrePoint.Y - iHeight, this.m_DockPanel.Width - 4, iHeight));
                    }
                    else if (this.m_CenterToDocumentLeftButtonItem.eBaseItemState == GISShare.Controls.WinForm.WFNew.BaseItemState.eHot)
                    {
                        int iWidth = this.m_DockPanel.Width / 2 - 2;
                        int iHeight = this.m_DockPanel.Height / 2 - 2;
                        this.m_DockShadowForm.Show(new Rectangle(dockPanelCentrePoint.X - iWidth, dockPanelCentrePoint.Y - iHeight, iWidth, this.m_DockPanel.Height - 4));
                    }
                    else if (this.m_CenterToDocumentRightButtonItem.eBaseItemState == GISShare.Controls.WinForm.WFNew.BaseItemState.eHot)
                    {
                        int iWidth = this.m_DockPanel.Width / 2 - 2;
                        int iHeight = this.m_DockPanel.Height / 2 - 2;
                        this.m_DockShadowForm.Show(new Rectangle(dockPanelCentrePoint.X, dockPanelCentrePoint.Y - iHeight, iWidth, this.m_DockPanel.Height - 4));
                    }
                    else if (this.m_CenterToDocumentBottomButtonItem.eBaseItemState == GISShare.Controls.WinForm.WFNew.BaseItemState.eHot)
                    {
                        int iWidth = this.m_DockPanel.Width / 2 - 2;
                        int iHeight = this.m_DockPanel.Height / 2 - 2;
                        this.m_DockShadowForm.Show(new Rectangle(dockPanelCentrePoint.X - iWidth, dockPanelCentrePoint.Y, this.m_DockPanel.Width - 4, iHeight));
                    }
                    //
                    else if (this.m_CenterToDockUpButtonItem.eBaseItemState == GISShare.Controls.WinForm.WFNew.BaseItemState.eHot)
                    {
                        this.m_DockShadowForm.Show(this.m_RectangleInternalTop);
                    }
                    else if (this.m_CenterToDockLeftButtonItem.eBaseItemState == GISShare.Controls.WinForm.WFNew.BaseItemState.eHot)
                    {
                        this.m_DockShadowForm.Show(this.m_RectangleInternalLeft);
                    }
                    else if (this.m_CenterToDockRightButtonItem.eBaseItemState == GISShare.Controls.WinForm.WFNew.BaseItemState.eHot)
                    {
                        this.m_DockShadowForm.Show(this.m_RectangleInternalRight);
                    }
                    else if (this.m_CenterToDockBottomButtonItem.eBaseItemState == GISShare.Controls.WinForm.WFNew.BaseItemState.eHot)
                    {
                        this.m_DockShadowForm.Show(this.m_RectangleInternalBottom);
                    }
                    #endregion
                }
                else if (eDockAreaStyle != DockAreaStyle.eNone)
                {
                    #region 停靠阴影
                    if (this.m_CenterToDockFillButtonItem.eBaseItemState == GISShare.Controls.WinForm.WFNew.BaseItemState.eHot)
                    {
                        int iWidth = this.m_DockPanel.Width / 2 - 2;
                        int iHeight = this.m_DockPanel.Height / 2 - 2;
                        Rectangle rectangle = new Rectangle(dockPanelCentrePoint.X - iWidth, dockPanelCentrePoint.Y - iHeight, this.m_DockPanel.Width - 4, this.m_DockPanel.Height - 4);
                        this.m_DockShadowForm.Show(rectangle); 
                    }
                    else if (this.m_CenterToDockUpButtonItem.eBaseItemState == GISShare.Controls.WinForm.WFNew.BaseItemState.eHot)
                    {
                        int iWidth = this.m_DockPanel.Width / 2 - 2;
                        int iHeight = this.m_DockPanel.Height / 2 - 2;
                        Rectangle rectangle = new Rectangle(dockPanelCentrePoint.X - iWidth, dockPanelCentrePoint.Y - iHeight, this.m_DockPanel.Width - 4, iHeight);
                        this.m_DockShadowForm.Show(rectangle);
                    }
                    else if (this.m_CenterToDockLeftButtonItem.eBaseItemState == GISShare.Controls.WinForm.WFNew.BaseItemState.eHot)
                    {
                        int iWidth = this.m_DockPanel.Width / 2 - 2;
                        int iHeight = this.m_DockPanel.Height / 2 - 2;
                        Rectangle rectangle = new Rectangle(dockPanelCentrePoint.X - iWidth, dockPanelCentrePoint.Y - iHeight, iWidth, this.m_DockPanel.Height - 4);
                        this.m_DockShadowForm.Show(rectangle);
                    }
                    else if (this.m_CenterToDockRightButtonItem.eBaseItemState == GISShare.Controls.WinForm.WFNew.BaseItemState.eHot)
                    {
                        int iWidth = this.m_DockPanel.Width / 2 - 2;
                        int iHeight = this.m_DockPanel.Height / 2 - 2;
                        Rectangle rectangle = new Rectangle(dockPanelCentrePoint.X, dockPanelCentrePoint.Y - iHeight, iWidth, this.m_DockPanel.Height - 4);
                        this.m_DockShadowForm.Show(rectangle);
                    }
                    else if (this.m_CenterToDockBottomButtonItem.eBaseItemState == GISShare.Controls.WinForm.WFNew.BaseItemState.eHot)
                    {
                        int iWidth = this.m_DockPanel.Width / 2 - 2;
                        int iHeight = this.m_DockPanel.Height / 2 - 2;
                        Rectangle rectangle = new Rectangle(dockPanelCentrePoint.X - iWidth, dockPanelCentrePoint.Y, this.m_DockPanel.Width - 4, iHeight);
                        this.m_DockShadowForm.Show(rectangle);
                    }
                    #endregion
                }
            }
            else
            {
                #region 停靠阴影
                if (this.m_CenterToDockFillButtonItem.eBaseItemState == GISShare.Controls.WinForm.WFNew.BaseItemState.eHot) 
                {
                    this.m_DockShadowForm.Show(this.DocumentRectangle);
                }
                else if (this.m_CenterToDockUpButtonItem.eBaseItemState == GISShare.Controls.WinForm.WFNew.BaseItemState.eHot) 
                {
                    this.m_DockShadowForm.Show(this.m_RectangleInternalTop); 
                }
                else if (this.m_CenterToDockLeftButtonItem.eBaseItemState == GISShare.Controls.WinForm.WFNew.BaseItemState.eHot) 
                {
                    this.m_DockShadowForm.Show(this.m_RectangleInternalLeft);
                }
                else if (this.m_CenterToDockRightButtonItem.eBaseItemState == GISShare.Controls.WinForm.WFNew.BaseItemState.eHot) 
                {
                    this.m_DockShadowForm.Show(this.m_RectangleInternalRight); 
                }
                else if (this.m_CenterToDockBottomButtonItem.eBaseItemState == GISShare.Controls.WinForm.WFNew.BaseItemState.eHot) 
                {
                    this.m_DockShadowForm.Show(this.m_RectangleInternalBottom);
                }
                #endregion
            }
            //
            if (!this.HaveActiveDockButton() && this.m_DockShadowForm.Visible)
            {
                this.m_DockShadowForm.Close();
            }
        }
        #endregion

        protected override void MessageMonitor(MessageInfo messageInfo)
        {
            base.MessageMonitor(messageInfo);
            //
            BaseItem baseItem;
            for (int i = 0; i < this.m_BaseItemCollection.Count; i++)
            {
                baseItem = this.m_BaseItemCollection[i];
                if (baseItem.pOwner != this) continue;
                //
                IMessageChain pMessageChain = baseItem as IMessageChain;
                if (pMessageChain != null)
                {
                    pMessageChain.SendMessage(messageInfo);
                }
            }
        }

        public void Show(DockPanelFloatForm dockPanelForm)//展现停靠布局按钮窗体，并设置相关参数避免后期频繁计算
        {
            this.m_DockPanelFloatForm = dockPanelForm;
            this.m_DockPanelFloatForm.DockPanelManager.IsDockLayoutState = true;
            //
            //获取停靠许可
            //
            IDockPanel pDockPanel = this.m_DockPanelFloatForm.GetIDockPanel();
            if (pDockPanel != null) 
            {
                pDockPanel.GetDockLicense(ref this.m_bCanDockUp, ref this.m_bCanDockLeft, ref this.m_bCanDockRight, ref this.m_bCanDockBottom, ref this.m_bCanDockFill,
                    ref this.m_bCanFloat, ref this.m_bCanHide, ref m_bCanClose,
                    ref this.m_bIsBasePanel, ref this.m_bIsDocumentPanel);
                //
                this.m_CenterToDockFillButtonItem.Visible = false;// this.m_bCanDockFill;
                //
                this.m_CenterToDocumentUpButtonItem.Visible = false;// this.m_bIsDocumentPanel && this.m_bCanDockUp && this.HaveDocumentDockArea;
                this.m_CenterToDocumentLeftButtonItem.Visible = false;// this.m_bIsDocumentPanel && this.m_bCanDockLeft && this.HaveDocumentDockArea;
                this.m_CenterToDocumentRightButtonItem.Visible = false;// this.m_bIsDocumentPanel && this.m_bCanDockRight && this.HaveDocumentDockArea;
                this.m_CenterToDocumentBottomButtonItem.Visible = false;//  this.m_bIsDocumentPanel && this.m_bCanDockBottom && this.HaveDocumentDockArea;
                //
                this.m_CenterToDockUpButtonItem.Visible = false;//  this.m_bIsBasePanel && this.m_bCanDockUp;
                this.m_CenterToDockLeftButtonItem.Visible = false;//  this.m_bIsBasePanel && this.m_bCanDockLeft;
                this.m_CenterToDockRightButtonItem.Visible = false;//  this.m_bIsBasePanel && this.m_bCanDockRight;
                this.m_CenterToDockBottomButtonItem.Visible = false;//  this.m_bIsBasePanel && this.m_bCanDockBottom;
                //
                this.m_ToDockUpButtonItem.Visible = this.m_bIsBasePanel && this.m_bCanDockUp;
                this.m_ToDockLeftButtonItem.Visible = this.m_bIsBasePanel && this.m_bCanDockLeft;
                this.m_ToDockRightButtonItem.Visible = this.m_bIsBasePanel && this.m_bCanDockRight;
                this.m_ToDockBottomButtonItem.Visible = this.m_bIsBasePanel && this.m_bCanDockBottom;
            }
            //
            //展现
            //
            base.Show();
            this.Opacity = 1;
            //
            //
            //
            int iCaptionHeight = 26;
            Size borderSize = new Size(4, 4);
            switch (dockPanelForm.DockPanelManager.ParentForm.FormBorderStyle)
            {
                case FormBorderStyle.None:
                    iCaptionHeight = 0;
                    borderSize = new Size(0, 0);
                    break;
                case FormBorderStyle.Sizable:
                    iCaptionHeight = SystemInformation.CaptionHeight;
                    borderSize = SystemInformation.FrameBorderSize;
                    break;
                case FormBorderStyle.SizableToolWindow:
                    iCaptionHeight = SystemInformation.ToolWindowCaptionHeight;
                    borderSize = SystemInformation.FrameBorderSize;
                    break;
                case FormBorderStyle.Fixed3D:
                    iCaptionHeight = SystemInformation.CaptionHeight;
                    borderSize = SystemInformation.FixedFrameBorderSize;
                    break;
                case FormBorderStyle.FixedDialog:
                    iCaptionHeight = SystemInformation.CaptionHeight;
                    borderSize = SystemInformation.FixedFrameBorderSize;
                    break;
                case FormBorderStyle.FixedSingle:
                    iCaptionHeight = SystemInformation.CaptionHeight;
                    borderSize = SystemInformation.FixedFrameBorderSize;
                    break;
                case FormBorderStyle.FixedToolWindow:
                    iCaptionHeight = SystemInformation.ToolWindowCaptionHeight;
                    borderSize = SystemInformation.FixedFrameBorderSize;
                    break;
                default:
                    iCaptionHeight = SystemInformation.CaptionHeight;
                    borderSize = SystemInformation.FrameBorderSize;
                    break;
            }
            if (dockPanelForm.DockPanelManager.ParentForm is RibbonForm) borderSize = SystemInformation.FrameBorderSize;
            #region 已抛弃
            //int iCaptionHeight = 26;
            //Size borderSize = new Size(4, 4);
            //if (dockPanelForm != null && dockPanelForm.DockPanelManager != null && dockPanelForm.DockPanelManager.ParentForm != null)
            //{
            //    switch (dockPanelForm.DockPanelManager.ParentForm.FormBorderStyle)
            //    {
            //        case FormBorderStyle.Sizable:
            //        case FormBorderStyle.Fixed3D:
            //        case FormBorderStyle.FixedDialog:
            //        case FormBorderStyle.FixedSingle:
            //            iCaptionHeight = SystemInformation.CaptionHeight;
            //            break;
            //        case FormBorderStyle.SizableToolWindow:
            //        case FormBorderStyle.FixedToolWindow:
            //            iCaptionHeight = SystemInformation.ToolWindowCaptionHeight;
            //            break;
            //        case FormBorderStyle.None:
            //        default:
            //            iCaptionHeight = 0;
            //            break;
            //    }
            //    borderSize = new Size
            //        (
            //        (dockPanelForm.DockPanelManager.ParentForm.Width - dockPanelForm.DockPanelManager.ParentForm.DisplayRectangle.Width) / 2,
            //        (dockPanelForm.DockPanelManager.ParentForm.Height - dockPanelForm.DockPanelManager.ParentForm.DisplayRectangle.Height) / 2
            //        );
            //}
            #endregion
            //
            //转化为屏幕坐标
            //
            Point parentFormLocation = dockPanelForm.DockPanelManager.ParentForm.Location;
            //
            Rectangle rectangle = dockPanelForm.DockPanelManager.GetParentFormWorkRegionRectangle(false);
            this.m_Rectangle = new Rectangle
                (
                parentFormLocation.X + rectangle.X + borderSize.Width, 
                parentFormLocation.Y + rectangle.Y + borderSize.Height + iCaptionHeight,
                rectangle.Width - 2 * borderSize.Width,
                rectangle.Height - 2 * borderSize.Height - iCaptionHeight
                );
            this.m_ParentWorkRegionCentrePoint = new Point((this.m_Rectangle.Left + this.m_Rectangle.Right) / 2, (this.m_Rectangle.Top + this.m_Rectangle.Bottom) / 2);
            //
            bool bRefresh = false;
            Rectangle rectangleExceptDockArea = dockPanelForm.DockPanelManager.GetParentFormWorkRegionRectangle(true);
            this.m_RectangleExceptDockArea = new Rectangle
                (
                parentFormLocation.X + rectangleExceptDockArea.X + borderSize.Width, 
                parentFormLocation.Y + rectangleExceptDockArea.Y + borderSize.Height + iCaptionHeight,
                rectangleExceptDockArea.Width - 2 * borderSize.Width,
                rectangleExceptDockArea.Height - 2 * borderSize.Height - iCaptionHeight
                );
            bRefresh |= ((WFNew.ISetBaseItemHelper)this.m_ToDockUpButtonItem).SetLocation
                (
                (this.m_RectangleExceptDockArea.Left + this.m_RectangleExceptDockArea.Right - this.m_ToDockUpButtonItem.Width) / 2, this.m_RectangleExceptDockArea.Top
                );
            bRefresh |= ((WFNew.ISetBaseItemHelper)this.m_ToDockLeftButtonItem).SetLocation
                (
                this.m_RectangleExceptDockArea.Left, (this.m_RectangleExceptDockArea.Top + this.m_RectangleExceptDockArea.Bottom - this.m_ToDockLeftButtonItem.Height) / 2
                );
            bRefresh |= ((WFNew.ISetBaseItemHelper)this.m_ToDockRightButtonItem).SetLocation
                (
                this.m_RectangleExceptDockArea.Right - this.m_ToDockRightButtonItem.Width, (this.m_RectangleExceptDockArea.Top + this.m_RectangleExceptDockArea.Bottom - this.m_ToDockLeftButtonItem.Height) / 2
                );
            bRefresh |= ((WFNew.ISetBaseItemHelper)this.m_ToDockBottomButtonItem).SetLocation
                (
                (this.m_RectangleExceptDockArea.Left + this.m_RectangleExceptDockArea.Right - this.m_ToDockUpButtonItem.Width) / 2, this.m_RectangleExceptDockArea.Bottom - this.m_ToDockBottomButtonItem.Height
                );
            if (bRefresh) this.Refresh();
            //
            //设置相关参数
            //
            int iInternalWidth = 160;
            if (this.m_DockPanelFloatForm.Width > this.m_Rectangle.Width) iInternalWidth = this.m_Rectangle.Width;
            else iInternalWidth = this.m_DockPanelFloatForm.Width;
            int iInternalHeight = 160;
            if (this.m_DockPanelFloatForm.Height > this.m_Rectangle.Height) iInternalHeight = this.m_Rectangle.Height;
            else iInternalHeight = this.m_DockPanelFloatForm.Height;
            //
            this.m_RectangleInternalTop = new Rectangle(this.m_Rectangle.Left, this.m_Rectangle.Top, this.m_Rectangle.Width, iInternalHeight);
            this.m_RectangleInternalLeft = new Rectangle(this.m_Rectangle.Left, this.m_Rectangle.Top, iInternalWidth, this.m_Rectangle.Height);
            this.m_RectangleInternalRight = new Rectangle(this.m_Rectangle.Right - iInternalWidth, this.m_Rectangle.Top, iInternalWidth, this.m_Rectangle.Height);
            this.m_RectangleInternalBottom = new Rectangle(this.m_Rectangle.Left, this.m_Rectangle.Bottom - iInternalHeight, this.m_Rectangle.Width, iInternalHeight);
            //
            int iOutWidth = 160;
            if (this.m_DockPanelFloatForm.Width > this.m_RectangleExceptDockArea.Width) iOutWidth = this.m_RectangleExceptDockArea.Width;
            else iOutWidth = this.m_DockPanelFloatForm.Width;
            int iOutHeight = 160;
            if (this.m_DockPanelFloatForm.Height > this.m_RectangleExceptDockArea.Height) iOutHeight = this.m_RectangleExceptDockArea.Height;
            else iOutHeight = this.m_DockPanelFloatForm.Height;
            //
            this.m_RectangleOutTop = new Rectangle(this.m_RectangleExceptDockArea.Left, this.m_RectangleExceptDockArea.Top, this.m_RectangleExceptDockArea.Width, iOutHeight);
            this.m_RectangleOutLeft = new Rectangle(this.m_RectangleExceptDockArea.Left, this.m_RectangleExceptDockArea.Top, iOutWidth, this.m_RectangleExceptDockArea.Height);
            this.m_RectangleOutRight = new Rectangle(this.m_RectangleExceptDockArea.Right - iOutWidth, this.m_RectangleExceptDockArea.Top, iOutWidth, this.m_RectangleExceptDockArea.Height);
            this.m_RectangleOutBottom = new Rectangle(this.m_RectangleExceptDockArea.Left, this.m_RectangleExceptDockArea.Bottom - iOutHeight, this.m_RectangleExceptDockArea.Width, iOutHeight);
            //
            //开始计时
            //
            this.m_Timer.Start();
        }

        private bool HaveActiveDockButton()//是否存在激活的停靠按钮（将在Timer_Tick(...)函数中被调用）
        {
           foreach(WFNew.BaseItem one in this.m_BaseItemCollection)
           {
               if (one.eBaseItemState == GISShare.Controls.WinForm.WFNew.BaseItemState.eHot) return true;
           }
           return false;
        }

        private void SetDockButtonItemBaseItemState()
        {
            bool bHaveActive = false;
            Point point = this.PointToClient(MousePosition);
            foreach (DockButtonItem one in this.m_BaseItemCollection)
            {
                if (one == null) continue;
                //
                if (bHaveActive) 
                {
                    one.SetBaseItemStateValue(GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal);
                }
                else 
                {
                    if (one.DisplayRectangle.Contains(point))
                    {
                        one.SetBaseItemStateValue(GISShare.Controls.WinForm.WFNew.BaseItemState.eHot);
                        bHaveActive = true;
                    }
                    else 
                    {
                        one.SetBaseItemStateValue(GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal);
                    }
                }
            }
        }
    }

}
