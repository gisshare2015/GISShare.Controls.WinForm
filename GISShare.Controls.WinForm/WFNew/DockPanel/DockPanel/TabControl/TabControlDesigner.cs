using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace LiuZhenHong.Controls.DockPanel
{
    public class TabControlDesigner : ControlDesigner
    {
        private TabControl m_TabControl = null;

        private DesignerVerbCollection verbs;

		public override DesignerVerbCollection Verbs
		{
			get
			{
				if( verbs == null )
				{
					verbs = new DesignerVerbCollection();
                    verbs.Add(new DesignerVerb("添加页面", new EventHandler(AddTabPage)));
                    verbs.Add(new DesignerVerb("移除页面", new EventHandler(RemoveTabPage)));
                    verbs.Add(new DesignerVerb("上一个", new EventHandler(BackTabPage)));
                    verbs.Add(new DesignerVerb("下一个", new EventHandler(NextTabPage)));
                    verbs.Add(new DesignerVerb("关于TabControl控件", new EventHandler(AboutTabControl)));
                    verbs.Add(new DesignerVerb("关于...", new EventHandler(ShowInfo)));
				}
				return verbs;
			}
        }

        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            IComponentChangeService service = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
            if (service != null)
            {
                service.ComponentRemoved += new ComponentEventHandler(ComponentRemoved);
            }
            //
            this.m_TabControl = base.Component as TabControl;
            if (this.m_TabControl == null)
            {
                this.DisplayError(new ArgumentException("TabControl == null"));
                return;
            }
        }

        protected override void OnPaintAdornments(PaintEventArgs pea)
        {
            base.OnPaintAdornments(pea);
            //
            if (this.m_TabControl.Height < 16) return;
            //
            using (Pen pen = new Pen(SystemColors.ControlDark, 1))
            {
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                pea.Graphics.DrawRectangle(pen, 0, 0, this.m_TabControl.Width - 1, this.m_TabControl.Height - 1);
                StringFormat drawFormat = new StringFormat();
                drawFormat.FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.FitBlackBox;
                drawFormat.Trimming = StringTrimming.EllipsisCharacter;
                //int iWidth = System.Text.Encoding.GetEncoding("GB2312").GetByteCount(this.m_TabControl.Text) * 7;
                SizeF size = pea.Graphics.MeasureString(this.m_TabControl.Text, this.m_TabControl.Font);
                int iWidth = (int)(size.Width + 1);
                int iHeight = (int)(size.Height + 1);
                pea.Graphics.DrawString(this.m_TabControl.Text,
                    this.m_TabControl.Font,
                    SystemBrushes.ControlText,
                    new Rectangle(this.m_TabControl.DisplayRectangle.X + (this.m_TabControl.DisplayRectangle.Width - iWidth) / 2,
                    this.m_TabControl.DisplayRectangle.Y + (this.m_TabControl.DisplayRectangle.Height - iHeight) / 2, 
                    iWidth, 
                    21),
                    drawFormat);
            }
        }

        private void ComponentRemoved(object sender, ComponentEventArgs cea)
        {
            TabPage tabPage = cea.Component as TabPage;
            if (tabPage != null)
            {
                this.m_TabControl.RemoveTabPage(tabPage);
            }      
        }

        private void AddTabPage(object sender, EventArgs ea)
		{
            IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
            if (host != null)
			{
                TabPage tabPage = host.CreateComponent(typeof(TabPage)) as TabPage;
                tabPage.Text = tabPage.Name;
                this.m_TabControl.TabPages.Add(tabPage);
			}
		}

        private void RemoveTabPage(object sender, EventArgs ea)
		{
            IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
            if (host != null)
            {
                TabPage tabPage = this.m_TabControl.SelectTabPage;//key
                if (tabPage == null) return;
                this.m_TabControl.RemoveTabPage(tabPage);
                host.DestroyComponent(tabPage);
            }
        }

        private void BackTabPage(object sender, EventArgs ea)
        {
            int index = this.m_TabControl.TabPageSelectIndex - 1;
            if (index < 0 || index >= this.m_TabControl.TabPages.Count) return;
            this.m_TabControl.TabPageSelectIndex = index;
            RaiseComponentChanging(TypeDescriptor.GetProperties(Control)["TabPageSelectIndex"]);
            RaiseComponentChanged(TypeDescriptor.GetProperties(Control)["TabPageSelectIndex"], this.m_TabControl.TabPageSelectIndex, index);
        }

        private void NextTabPage(object sender, EventArgs ea)
        {
            int index = this.m_TabControl.TabPageSelectIndex + 1;
            if (index < 0 || index >= this.m_TabControl.TabPages.Count) return;
            this.m_TabControl.TabPageSelectIndex = index;
            RaiseComponentChanging(TypeDescriptor.GetProperties(Control)["TabPageSelectIndex"]);
            RaiseComponentChanged(TypeDescriptor.GetProperties(Control)["TabPageSelectIndex"], this.m_TabControl.TabPageSelectIndex, index);
        }

        //protected override void WndProc(ref Message m)
        //{
        //    try
        //    {
        //        if (this.m_TabControl.Created)
        //        {
        //            switch (m.Msg)
        //            {
        //                case (int)LiuZhenHong.Win32.Msgs.WM_LBUTTONDOWN://0x201
        //                //case (int)LiuZhenHong.Win32.Msgs.WM_LBUTTONUP://0x202
        //                    Point point = LiuZhenHong.Win32.NativeMethods.LParamToMouseLocation((int)m.LParam);
        //                    //
        //                    if (m.HWnd == this.m_TabControl.TabButtonList.NextButton.Handle && this.m_TabControl.TabButtonList.NextButton.ClientRectangle.Contains(point))
        //                    { 
        //                        this.m_TabControl.TabButtonList.NextView();
        //                    }
        //                    //
        //                    if (m.HWnd == this.m_TabControl.TabButtonList.BackButton.Handle && this.m_TabControl.TabButtonList.BackButton.ClientRectangle.Contains(point))
        //                    {
        //                        this.m_TabControl.TabButtonList.BackView();
        //                    }
        //                    //
        //                    foreach(object one in this.m_TabControl.TabPages)
        //                    {
        //                        TabPage temp = one as TabPage;
        //                        if (temp == null) continue;
        //                        if (m.HWnd == temp.TabButton.Handle && temp.TabButton.ClientRectangle.Contains(point))
        //                        {
        //                            int index = this.m_TabControl.IndexOfTabButton(temp.TabButton);
        //                            if (index < 0) continue;
        //                            this.m_TabControl.TabPageSelectIndex = index;
        //                            RaiseComponentChanging(TypeDescriptor.GetProperties(Control)["TabPageSelectIndex"]);
        //                            RaiseComponentChanged(TypeDescriptor.GetProperties(Control)["TabPageSelectIndex"], this.m_TabControl.TabPageSelectIndex, index);
        //                            //
        //                            ISelectionService pSelectionService = GetService(typeof(ISelectionService)) as ISelectionService;
        //                            if (pSelectionService != null)
        //                            { pSelectionService.SetSelectedComponents(new Component[] { temp as Component }, SelectionTypes.Primary); }
        //                            break;
        //                        }
        //                    }
        //                    break;
        //                default:
        //                    break;
        //            }
        //        }
        //    }
        //    finally
        //    {
        //        base.WndProc(ref m);
        //    }
        //}

        protected virtual void AboutTabControl(object sender, EventArgs ea)
        {
            MessageBox.Show("选项卡控件（TabControl）：用来管理选项卡面板控件的集合控件。", "选项卡控件");
        }

        private void ShowInfo(object sender, EventArgs e)
        {
            InfoForm infoForm = new InfoForm();
            infoForm.ShowDialog();
        }

    }
}
