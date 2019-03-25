using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm
{
    public partial class HandleViewForm : GISShare.Controls.WinForm.WFNew.Forms.TBForm//Form
    {
        //http://blog.csdn.net/xjh_love_paopao/article/details/2503810

        public HandleViewForm()
        {
            InitializeComponent();
        }

        public void AddQueryWindows(Control control)
        {
            IntPtrNodeViewItem intPtrNodeViewItem = new IntPtrNodeViewItem(control.Name, this.FilterWindowsText(control.Text), control.Handle);
            intPtrNodeViewItem.IsExpanded = true;
            this.ctQueryList.NodeViewItems.Add(intPtrNodeViewItem);
            //
            for (int i = 0; i < control.Controls.Count; i++)
            {
                Control ctr = control.Controls[i];
                IntPtrNodeViewItem node = new IntPtrNodeViewItem(ctr.Name, this.FilterWindowsText(ctr.Text), ctr.Handle);
                node.IsExpanded = true;
                intPtrNodeViewItem.NodeViewItems.Add(node);
                //
                this.AddQueryWindows_DG(ctr, node.NodeViewItems);
            }
            //
            this.ctQueryList.SelectedNode = intPtrNodeViewItem;
        }
        private void AddQueryWindows_DG(Control control, WFNew.View.NodeViewItemCollection nodes)
        {
            for (int i = 0; i < control.Controls.Count; i++)
            {
                Control ctr = control.Controls[i];
                IntPtrNodeViewItem node = new IntPtrNodeViewItem(ctr.Name, this.FilterWindowsText(ctr.Text), ctr.Handle);
                //node.IsExpanded = true;
                nodes.Add(node);
                //
                this.AddQueryWindows_DG(ctr, node.NodeViewItems);
            }
        }
        private string FilterWindowsText(string strWindowsText)
        {
            strWindowsText = strWindowsText.Replace("  ", "");
            if (strWindowsText.Length > 50) strWindowsText = strWindowsText.Substring(0, 20) + " …";
            return strWindowsText.Replace('\n', '↙');
        }

        public void AddQueryWindows(string name, string text, IntPtr hwnd)
        {
            this.ctQueryList.NodeViewItems.Add(new IntPtrNodeViewItem(name, text, hwnd));
        }

        private void btnQuery_MouseClick(object sender, MouseEventArgs e)
        {
            IntPtrNodeViewItem intPtrNodeViewItem = ctQueryList.SelectedNode as IntPtrNodeViewItem;
            if (intPtrNodeViewItem == null) return;
            //
            this.nodeViewItemTree1.NodeViewItems.Clear();
            //
            System.Text.StringBuilder sbClassName = new StringBuilder(255);
            GISShare.Win32.API.GetClassName(intPtrNodeViewItem.IntPtrInfo, sbClassName, 255);
            //
            System.Text.StringBuilder strWindowsText = new StringBuilder(255);
            GISShare.Win32.API.GetWindowText(intPtrNodeViewItem.IntPtrInfo, strWindowsText, 255);
            this.FilterWindowsText(strWindowsText);
            //
            GISShare.Controls.WinForm.WFNew.View.NodeViewItem node = new GISShare.Controls.WinForm.WFNew.View.NodeViewItem();
            node.Name = intPtrNodeViewItem.IntPtrInfo.ToString();
            node.Text = intPtrNodeViewItem.Text + " — 【窗口句柄[HWND]：" + intPtrNodeViewItem.IntPtrInfo.ToInt32() + "】 — 类名称[ClassName]：{" + sbClassName.ToString() + "} — 窗口文本[WindowText]：" + strWindowsText;
            node.IsExpanded = true;
            this.nodeViewItemTree1.NodeViewItems.Add(node);
            //
            GISShare.Win32.API.EnumChildWindows(intPtrNodeViewItem.IntPtrInfo, EnumCP, intPtrNodeViewItem.IntPtrInfo);
            //
            this.lblInfo.Text = "共携带 " + node.NodeViewItems.Count + " 句柄";
            this.toolBarN2.Refresh();
        }
        private bool EnumCP(IntPtr hwnd, IntPtr lParam)
        {
            if (hwnd.ToInt32() <= 0) return false;
            //
            System.Text.StringBuilder sbClassName = new StringBuilder(255);
            GISShare.Win32.API.GetClassName(hwnd, sbClassName, 255);
            //
            if ("Static" == sbClassName.ToString()) return false;
            //
            System.Text.StringBuilder strWindowsText = new StringBuilder(255);
            GISShare.Win32.API.GetWindowText(hwnd, strWindowsText, 255);
            this.FilterWindowsText(strWindowsText);
            //
            GISShare.Controls.WinForm.WFNew.View.NodeViewItem node = new GISShare.Controls.WinForm.WFNew.View.NodeViewItem();
            node.Name = hwnd.ToString();
            node.Text = "【窗口句柄[HWND]：" + hwnd.ToString() + " — 父窗口句柄[lParam]：" + lParam.ToString() + "】 — 类名称[ClassName]：{" + sbClassName.ToString() + "} — 窗口文本[WindowText]：" + strWindowsText;
            node.IsExpanded = true;
            GISShare.Controls.WinForm.WFNew.View.NodeViewItem parentNode = this.nodeViewItemTree1.NodeViewItems.GetNodeViewItemEx(lParam.ToString());
            if (parentNode == null) this.nodeViewItemTree1.NodeViewItems.Add(node);
            else parentNode.NodeViewItems.Add(node);
            //
            //GISShare.Win32.API.EnumChildWindows(hwnd, EnumCP, new IntPtr(hwnd));
            //
            return true;
        }
        private void FilterWindowsText(System.Text.StringBuilder strWindowsText)
        {
            if (strWindowsText.Length > 225) strWindowsText = strWindowsText.Append(" …");
            strWindowsText = strWindowsText.Replace("  ", "");
            strWindowsText = strWindowsText.Replace('\n', '↙');
        }

        private void btnInfo_MouseClick(object sender, MouseEventArgs e)
        {
            GISShare.Controls.WinForm.InfoForm infoForm = new GISShare.Controls.WinForm.InfoForm();
            infoForm.ShowDialog();
        }

        //
        //
        //

        class IntPtrNodeViewItem : WFNew.View.NodeViewItem
        {
            public IntPtrNodeViewItem()
            { }

            public IntPtrNodeViewItem(string name, string text, IntPtr intPtr)
                : base(name, text)
            {
                this.m_IntPtr = intPtr;
            }

            IntPtr m_IntPtr = new IntPtr(0);
            public IntPtr IntPtrInfo
            {
                get { return m_IntPtr; }
                set { m_IntPtr = value; }
            }

            public override string ToString()
            {
                if (this.Text != null && this.Text.Length > 0) return this.Text;
                else if (this.Name != null && this.Name.Length > 0) return this.Name;
                else return base.ToString();
            }
        }
    }
}