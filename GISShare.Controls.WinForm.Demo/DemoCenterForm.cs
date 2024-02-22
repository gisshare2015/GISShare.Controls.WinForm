using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.Demo
{
    public partial class DemoCenterForm : GISShare.Controls.WinForm.WFNew.Forms.TBForm //Form
    {
        GISShare.Controls.WinForm.WFNew.SuperToolTip m_SuperToolTip = new GISShare.Controls.WinForm.WFNew.SuperToolTip();

        public DemoCenterForm()
        {
            InitializeComponent();
            this.m_SuperToolTip.SetToolTip(this.btnInfo, new GISShare.Controls.WinForm.WFNew.TipInfo(this.btnInfo.Image, this.btnInfo.Text, "可查看系统说明"));
            this.m_SuperToolTip.SetToolTip(this.btnInfo, new GISShare.Controls.WinForm.WFNew.TipInfo(this.btnHandleViewForm.Image, this.btnHandleViewForm.Text, "用于监测某一句柄内部相关信息"));
            //
            //
            //
            #region 关于
            this.lblAbout.Text =
@"
                   关   于                 

1.该组件为个人业余作品，主要实现对原有VS控件
  丰富与优化，为您进行简单WinForm应用程序的
  开发提供便捷；  

2.该组件开发框架为.NET Framework 2.0，未借用
  其它第三方组件，您可以直接使用；
                         
3.该组件可用于.NET Framework 2.0  、.NET 
  Framework 3.0 和 .NET Framework 3.5；

4.该组件不支持.NET Framework 3.5 Client 
  Profile 、 .NET Framework 4.0 和 
  .NET Framework 4.0 Client Profile 平台。

5.由于该组件是个人作品，其中定会存在许多的不
  足与问题，由此给你带来的不便深表歉意；

6.关于组件的使用及其存在的问题您可以联系我，
  在工作之余我会尽量为您解决。


  版权所有，欢迎使用！
                         即时分享 
                         GISShare";
            #endregion
            //
            #region 寄语
            this.lblInfo.Text =
@"                  寄   语                 
    GISShare.Controls 2.x 控件集是对之前
GISShare.Controls 1.x 的升级和优化，但它
与前者存在着很大的不同。主要表现在三大方面：
一是控件的变化，主要体现在新控件的增加（如：
TBForm、RibbonControl、ViewItemListBox等）和
原有控件的升级和优化（如：DockPanelManager等
）；二是控件分类目录的变化，将Controls目录再
分为WFNew和WinForm，其中WFNew里的控件是较为
对立的新控件，而WinForm则是对原有VS控件的重
绘或简单的修改；三是设计结构的变化，而这一点
在WFNew目录下表现最为明显，还希望使用者能稍
加注意以便于控件的使用。由于，设计者对于图形
界面的设计和绘制不甚精通，即便在借鉴了不少开
源项目的界面绘制后依然不是很满意。还希望有高
手们通过重写本组件的颜色表（ColorTable）和绘
制方法（Renderer）使界面更加的清晰靓丽，在此
表示感谢。由于新版本为笔者个人设计与实现的，
虽然也花费了较长的时间和精力，但无可避免的会
在使用过程中存在或多或少的问题与不足。由此给
您带来的不变，还请见谅！如若有使用或者更改等
方面的需要可以与本人联系，在工作之余我会尽量
完善使之趋于完美。

  版权所有，欢迎使用！                         
                         即时分享 
                         GISShare";
            #endregion
        }

        #region 快捷工具条
        private void btnInfo_MouseClick(object sender, MouseEventArgs e)
        {
            GISShare.Controls.WinForm.InfoForm infoForm = new GISShare.Controls.WinForm.InfoForm();
            infoForm.ShowDialog();
        }

        private void btnHandleViewForm_MouseClick(object sender, MouseEventArgs e)
        {
            GISShare.Controls.WinForm.HandleViewForm handleViewForm = new GISShare.Controls.WinForm.HandleViewForm();
            handleViewForm.AddQueryWindows(this);
            handleViewForm.ShowDialog();
        }
        #endregion

        #region WFNew控件
        private void btnCollapsableSplitPanel_MouseClick(object sender, MouseEventArgs e)
        {
            GISShare.Controls.WinForm.Demo.WFNew.DemoOfCollapsableSplitPanelForm demoOfCollapsableSplitPanelForm = new GISShare.Controls.WinForm.Demo.WFNew.DemoOfCollapsableSplitPanelForm();
            demoOfCollapsableSplitPanelForm.Owner = this;
            demoOfCollapsableSplitPanelForm.Show();
        }

        private void btnExpandableCaptionPanel_MouseClick(object sender, MouseEventArgs e)
        {
            GISShare.Controls.WinForm.Demo.WFNew.DemoOfExpandableCaptionPanelForm demoOfExpandableCaptionPanelForm = new GISShare.Controls.WinForm.Demo.WFNew.DemoOfExpandableCaptionPanelForm();
            demoOfExpandableCaptionPanelForm.Show();
        }

        private void btnExpandablePanelContainer_MouseClick(object sender, MouseEventArgs e)
        {
            GISShare.Controls.WinForm.Demo.WFNew.DemoOfExpandablePanelContainerForm demoOfExpandablePanelContainerForm = new GISShare.Controls.WinForm.Demo.WFNew.DemoOfExpandablePanelContainerForm();
            demoOfExpandablePanelContainerForm.Show();
        }

        private void btnExpandableNodePanelContainer_MouseClick(object sender, MouseEventArgs e)
        {
            GISShare.Controls.WinForm.Demo.WFNew.DemoOfExpandableNodePanelContainerForm demoOfExpandableNodePanelContainerForm = new GISShare.Controls.WinForm.Demo.WFNew.DemoOfExpandableNodePanelContainerForm();
            demoOfExpandableNodePanelContainerForm.Show();
        }

        private void btnTabControl_MouseClick(object sender, MouseEventArgs e)
        {
            GISShare.Controls.WinForm.Demo.WFNew.DemoOfTabControlForm demoOfTabControlForm = new GISShare.Controls.WinForm.Demo.WFNew.DemoOfTabControlForm();
            demoOfTabControlForm.Show();
        }

        private void btnDockPanelManager_MouseClick(object sender, MouseEventArgs e)
        {
            GISShare.Controls.WinForm.Demo.WFNew.DemoOfDockPanelManagerForm demoOfDockPanelManagerForm = new GISShare.Controls.WinForm.Demo.WFNew.DemoOfDockPanelManagerForm();
            demoOfDockPanelManagerForm.Show();
        }

        private void btnRibbonControl_MouseClick(object sender, MouseEventArgs e)
        {
            GISShare.Controls.WinForm.Demo.WFNew.DemoOfRibbonControlForm demoOfRibbonControlForm = new GISShare.Controls.WinForm.Demo.WFNew.DemoOfRibbonControlForm();
            demoOfRibbonControlForm.Show();
        }

        private void btnViewItemListBox_MouseClick(object sender, MouseEventArgs e)
        {
            GISShare.Controls.WinForm.Demo.WFNew.DemoOfViewItemListBoxForm demoOfViewItemListBoxForm = new GISShare.Controls.WinForm.Demo.WFNew.DemoOfViewItemListBoxForm();
            demoOfViewItemListBoxForm.Show();
        }

        private void btnNodeViewItemTree_MouseClick(object sender, MouseEventArgs e)
        {
            GISShare.Controls.WinForm.Demo.WFNew.DemoOfNodeViewItemTreeForm demoOfNodeViewItemTreeForm = new GISShare.Controls.WinForm.Demo.WFNew.DemoOfNodeViewItemTreeForm();
            demoOfNodeViewItemTreeForm.Show();
        }

        private void btnGridViewItemListBox_Click(object sender, EventArgs e)
        {
            GISShare.Controls.WinForm.Demo.WFNew.DemoOfGridViewItemListBoxForm demoOfGridViewItemListBoxForm = new GISShare.Controls.WinForm.Demo.WFNew.DemoOfGridViewItemListBoxForm();
            demoOfGridViewItemListBoxForm.Show();
        }

        private void btnGridNodeViewItemTree_Click(object sender, EventArgs e)
        {
            GISShare.Controls.WinForm.Demo.WFNew.DemoOfGridNodeViewItemTreeForm demoOfGridNodeViewItemTreeForm = new GISShare.Controls.WinForm.Demo.WFNew.DemoOfGridNodeViewItemTreeForm();
            demoOfGridNodeViewItemTreeForm.Show();
        }

        private void btnCompnonent_MouseClick(object sender, MouseEventArgs e)
        {
            GISShare.Controls.WinForm.Demo.WFNew.DemoOfCompnonentForm demoOfCompnonentForm = new GISShare.Controls.WinForm.Demo.WFNew.DemoOfCompnonentForm();
            demoOfCompnonentForm.Show();
        }

        private void btnImageZoomableBox_Click(object sender, EventArgs e)
        {
            //GISShare.Controls.WinForm.Demo.WFNew.DemoOfImageZoomableBoxForm demoOfImageZoomableBoxForm = new GISShare.Controls.WinForm.Demo.WFNew.DemoOfImageZoomableBoxForm();
            //demoOfImageZoomableBoxForm.Show();
        }
        #endregion

        #region WinForm目录
        private void btnRadioButtonX_MouseClick(object sender, MouseEventArgs e)
        {
            GISShare.Controls.WinForm.Demo.WinForm.DemoOfRadioButtonXForm demoOfRadioButtonXForm = new GISShare.Controls.WinForm.Demo.WinForm.DemoOfRadioButtonXForm();
            demoOfRadioButtonXForm.Show();
        }

        private void btnCheckBoxX_MouseClick(object sender, MouseEventArgs e)
        {
            GISShare.Controls.WinForm.Demo.WinForm.DemoOfCheckBoxXForm demoOfCheckBoxXForm = new GISShare.Controls.WinForm.Demo.WinForm.DemoOfCheckBoxXForm();
            demoOfCheckBoxXForm.Show();
        }

        private void btnButtonX_MouseClick(object sender, MouseEventArgs e)
        {
            GISShare.Controls.WinForm.Demo.WinForm.DemoOfButtonXForm demoOfButtonXForm = new GISShare.Controls.WinForm.Demo.WinForm.DemoOfButtonXForm();
            demoOfButtonXForm.Show();
        }

        private void btnListBoxX_MouseClick(object sender, MouseEventArgs e)
        {
            GISShare.Controls.WinForm.Demo.WinForm.DemoOfListBoxXForm demoOfListBoxXForm = new GISShare.Controls.WinForm.Demo.WinForm.DemoOfListBoxXForm();
            demoOfListBoxXForm.Show();
        }

        private void btnCheckedListBoxX_MouseClick(object sender, MouseEventArgs e)
        {
            GISShare.Controls.WinForm.Demo.WinForm.DemoOfCheckedListBoxXForm demoOfCheckedListBoxXForm = new GISShare.Controls.WinForm.Demo.WinForm.DemoOfCheckedListBoxXForm();
            demoOfCheckedListBoxXForm.Show();
        }

        private void btnListViewX_MouseClick(object sender, MouseEventArgs e)
        {
            GISShare.Controls.WinForm.Demo.WinForm.DemoOfListViewXForm demoOfListViewXForm = new GISShare.Controls.WinForm.Demo.WinForm.DemoOfListViewXForm();
            demoOfListViewXForm.Show();
        }

        private void btnTreeViewX_MouseClick(object sender, MouseEventArgs e)
        {
            GISShare.Controls.WinForm.Demo.WinForm.DemoOfTreeViewXForm demoOfTreeViewXForm = new GISShare.Controls.WinForm.Demo.WinForm.DemoOfTreeViewXForm();
            demoOfTreeViewXForm.Show();
        }

        private void btnDataGridViewX_MouseClick(object sender, MouseEventArgs e)
        {
            GISShare.Controls.WinForm.Demo.WinForm.DemoOfDataGridViewXForm demoOfDataGridViewXForm = new GISShare.Controls.WinForm.Demo.WinForm.DemoOfDataGridViewXForm();
            demoOfDataGridViewXForm.Show();
        }

        private void btnTabControlX_MouseClick(object sender, MouseEventArgs e)
        {
            GISShare.Controls.WinForm.Demo.WinForm.DemoOfTabControlXForm demoOfTabControlXForm = new GISShare.Controls.WinForm.Demo.WinForm.DemoOfTabControlXForm();
            demoOfTabControlXForm.Show();
        }

        private void btnDockBarManager_MouseClick(object sender, MouseEventArgs e)
        {
            GISShare.Controls.WinForm.Demo.WinForm.DemoOfDockBarManagerForm demoOfDockBarManagerForm = new GISShare.Controls.WinForm.Demo.WinForm.DemoOfDockBarManagerForm();
            demoOfDockBarManagerForm.Show();
        }
        #endregion

        #region 全局钩子
        GISShare.Win32.GlobalMouseHook m_GlobalMouseHook = null;
        private void btnCraeteMouseHook_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.m_GlobalMouseHook == null)
            {
                GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show("鼠标监听已开启，您移动鼠标查看监听效果。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //
                this.m_GlobalMouseHook = new GISShare.Win32.GlobalMouseHook();
                this.m_GlobalMouseHook.CreatHook();
                this.m_GlobalMouseHook.MouseMove += new MouseEventHandler(GlobalMouseHook_MouseMove);
            }
        }
        void GlobalMouseHook_MouseMove(object sender, MouseEventArgs e)
        {
            this.lbMouseHook.SelectedIndex = this.lbMouseHook.ViewItems.Add
                (
                new GISShare.Controls.WinForm.WFNew.View.ViewItem
                    ("当前鼠标位置[" + System.DateTime.Now.ToLongTimeString() + "]：X:" + e.X + " Y:" + e.Y)
                );
        }

        private void btnDisposeMouseHook_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.m_GlobalMouseHook != null)
            {
                GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show("鼠标监听已关闭。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //
                this.m_GlobalMouseHook.Dispose();
                this.m_GlobalMouseHook = null;
                this.lbMouseHook.ViewItems.Clear();
            }
        }

        GISShare.Win32.GlobalKeyboardHook m_GlobalKeyboardHook = null;
        private void btnCreateKeyHook_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.m_GlobalKeyboardHook == null)
            {
                GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show("键盘监听已开启，您按下键盘任意键查看监听效果。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //
                this.m_GlobalKeyboardHook = new GISShare.Win32.GlobalKeyboardHook();
                this.m_GlobalKeyboardHook.CreatHook();
                this.m_GlobalKeyboardHook.KeyDown += new KeyEventHandler(GlobalKeyboardHook_KeyDown);
            }
        }
        void GlobalKeyboardHook_KeyDown(object sender, KeyEventArgs e)
        {
            this.lbKeyHook.SelectedIndex = this.lbKeyHook.ViewItems.Add
                (
                new GISShare.Controls.WinForm.WFNew.View.ViewItem
                    ("当前键盘按下值[" + System.DateTime.Now.ToLongTimeString() + "]：" + e.KeyCode.ToString())
                );
        }

        private void btnDisposeKeyHook_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.m_GlobalKeyboardHook != null)
            {
                GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show("键盘监听已关闭。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //
                this.m_GlobalKeyboardHook.Dispose();
                this.m_GlobalKeyboardHook = null;
                this.lbKeyHook.ViewItems.Clear();
            }
        }
        #endregion

        protected override void OnClosing(CancelEventArgs e)
        {
            this.btnDisposeMouseHook_MouseClick(null, null);
            this.btnDisposeKeyHook_MouseClick(null, null);
            //
            base.OnClosing(e);
        }
    }
}