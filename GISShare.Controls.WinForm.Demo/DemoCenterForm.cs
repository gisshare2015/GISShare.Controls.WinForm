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
            this.m_SuperToolTip.SetToolTip(this.btnInfo, new GISShare.Controls.WinForm.WFNew.TipInfo(this.btnInfo.Image, this.btnInfo.Text, "�ɲ鿴ϵͳ˵��"));
            this.m_SuperToolTip.SetToolTip(this.btnInfo, new GISShare.Controls.WinForm.WFNew.TipInfo(this.btnHandleViewForm.Image, this.btnHandleViewForm.Text, "���ڼ��ĳһ����ڲ������Ϣ"));
            //
            //
            //
            #region ����
            this.lblAbout.Text =
@"
                   ��   ��                 

1.�����Ϊ����ҵ����Ʒ����Ҫʵ�ֶ�ԭ��VS�ؼ�
  �ḻ���Ż���Ϊ�����м�WinFormӦ�ó����
  �����ṩ��ݣ�  

2.������������Ϊ.NET Framework 2.0��δ����
  ���������������������ֱ��ʹ�ã�
                         
3.�����������.NET Framework 2.0  ��.NET 
  Framework 3.0 �� .NET Framework 3.5��

4.�������֧��.NET Framework 3.5 Client 
  Profile �� .NET Framework 4.0 �� 
  .NET Framework 4.0 Client Profile ƽ̨��

5.���ڸ�����Ǹ�����Ʒ�����ж���������Ĳ�
  �������⣬�ɴ˸�������Ĳ������Ǹ�⣻

6.���������ʹ�ü�����ڵ�������������ϵ�ң�
  �ڹ���֮���һᾡ��Ϊ�������


  ��Ȩ���У���ӭʹ�ã�
                         ��ʱ���� 
                         GISShare";
            #endregion
            //
            #region ����
            this.lblInfo.Text =
@"                  ��   ��                 
    GISShare.Controls 2.x �ؼ����Ƕ�֮ǰ
GISShare.Controls 1.x ���������Ż�������
��ǰ�ߴ����źܴ�Ĳ�ͬ����Ҫ�����������棺
һ�ǿؼ��ı仯����Ҫ�������¿ؼ������ӣ��磺
TBForm��RibbonControl��ViewItemListBox�ȣ���
ԭ�пؼ����������Ż����磺DockPanelManager��
�������ǿؼ�����Ŀ¼�ı仯����ControlsĿ¼��
��ΪWFNew��WinForm������WFNew��Ŀؼ��ǽ�Ϊ
�������¿ؼ�����WinForm���Ƕ�ԭ��VS�ؼ�����
���򵥵��޸ģ�������ƽṹ�ı仯������һ��
��WFNewĿ¼�±�����Ϊ���ԣ���ϣ��ʹ��������
��ע���Ա��ڿؼ���ʹ�á����ڣ�����߶���ͼ��
�������ƺͻ��Ʋ�����ͨ�������ڽ���˲��ٿ�
Դ��Ŀ�Ľ�����ƺ���Ȼ���Ǻ����⡣��ϣ���и�
����ͨ����д���������ɫ��ColorTable���ͻ�
�Ʒ�����Renderer��ʹ������ӵ������������ڴ�
��ʾ��л�������°汾Ϊ���߸��������ʵ�ֵģ�
��ȻҲ�����˽ϳ���ʱ��;��������޿ɱ���Ļ�
��ʹ�ù����д��ڻ����ٵ������벻�㡣�ɴ˸�
�������Ĳ��䣬������£�������ʹ�û��߸��ĵ�
�������Ҫ�����뱾����ϵ���ڹ���֮���һᾡ��
����ʹ֮����������

  ��Ȩ���У���ӭʹ�ã�                         
                         ��ʱ���� 
                         GISShare";
            #endregion
        }

        #region ��ݹ�����
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

        #region WFNew�ؼ�
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

        #region WinFormĿ¼
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

        #region ȫ�ֹ���
        GISShare.Win32.GlobalMouseHook m_GlobalMouseHook = null;
        private void btnCraeteMouseHook_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.m_GlobalMouseHook == null)
            {
                GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show("�������ѿ��������ƶ����鿴����Ч����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    ("��ǰ���λ��[" + System.DateTime.Now.ToLongTimeString() + "]��X:" + e.X + " Y:" + e.Y)
                );
        }

        private void btnDisposeMouseHook_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.m_GlobalMouseHook != null)
            {
                GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show("�������ѹرա�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show("���̼����ѿ����������¼���������鿴����Ч����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    ("��ǰ���̰���ֵ[" + System.DateTime.Now.ToLongTimeString() + "]��" + e.KeyCode.ToString())
                );
        }

        private void btnDisposeKeyHook_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.m_GlobalKeyboardHook != null)
            {
                GISShare.Controls.WinForm.WFNew.Forms.TBMessageBox.Show("���̼����ѹرա�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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