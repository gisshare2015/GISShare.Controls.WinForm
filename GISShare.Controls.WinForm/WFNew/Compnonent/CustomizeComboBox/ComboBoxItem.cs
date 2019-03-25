using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    [DefaultEvent("SelectedIndexChanged")]
    public class ComboBoxItem : CustomizeComboBoxItem, IComboBoxItem
    {
        private const int CTR_IMAGESIZE = 16;
        private const int CONST_COLORREGIONWIDTH = 28;
        private const int CONST_COLORREGIONHEIGHT = 9;
        private const int CTR_BORDERSPASE = 3;
        private const int CTR_NONELEFTSPACE = 1;//
        private const int CTR_SINGLELEFTSPACE = 0;//
        private const int CTR_MIDDLESPACE = 2;//间距

        GISShare.Controls.WinForm.WFNew.View.ViewItemListBoxItem m_ViewItemListBox;

        #region 构造函数
        public ComboBoxItem()
            : base(new GISShare.Controls.WinForm.WFNew.BaseItemHost())
        {
            this.m_ViewItemListBox = new View.ViewItemListBoxItem();
            this.m_ViewItemListBox.BackColor = System.Drawing.SystemColors.Window;
            this.m_ViewItemListBox.ShowOutLine = false;
            this.m_ViewItemListBox.MultipleSelect = false;
            this.m_ViewItemListBox.SelectedIndexChanged += new IntValueChangedHandler(ViewItemListBox_SelectedIndexChanged);
            this.m_ViewItemListBox.MouseUp += new System.Windows.Forms.MouseEventHandler(ListBoxX_MouseUp);
            //
            GISShare.Controls.WinForm.WFNew.BaseItemHost baseItemHost = (GISShare.Controls.WinForm.WFNew.BaseItemHost)((ICustomizeComboBoxItem)this).ControlObject;
            baseItemHost.BackColor = System.Drawing.SystemColors.Window;
            baseItemHost.Dock = System.Windows.Forms.DockStyle.Fill;
            baseItemHost.BaseItemObject = this.m_ViewItemListBox;
            //
            //
            //
            this.DropDownWidth = 120;
            this.DropDownHeight = 120;
            //
            this.Size = new Size(120, 21);
        }
        void ListBoxX_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (this.AutoClosePopup && this.m_ViewItemListBox.ViewItemsRectangle.Contains(e.Location)) this.ClosePopup();
        }
        void ViewItemListBox_SelectedIndexChanged(object sender, IntValueChangedEventArgs e)
        {
            this.OnSelectedIndexChanged(e);
        }

        //public ComboBoxItem(GISShare.Controls.Plugin.WFNew.IComboBoxItemP pBaseItemP)
        //    : this()
        //{
        //    //IPlugin
        //    this.Name = pBaseItemP.Name;
        //    //ISetEntityObject
        //    GISShare.Controls.Plugin.ISetEntityObject pSetEntityObject = pBaseItemP as GISShare.Controls.Plugin.ISetEntityObject;
        //    if (pSetEntityObject != null) pSetEntityObject.SetEntityObject(this);
        //    //IBaseItemP_
        //    this.Checked = pBaseItemP.Checked;
        //    this.Enabled = pBaseItemP.Enabled;
        //    this.Font = pBaseItemP.Font;
        //    this.ForeColor = pBaseItemP.ForeColor;
        //    this.LockHeight = pBaseItemP.LockHeight;
        //    this.LockWith = pBaseItemP.LockWith;
        //    this.Padding = pBaseItemP.Padding;
        //    this.Size = pBaseItemP.Size;
        //    this.Text = pBaseItemP.Text;
        //    this.Visible = pBaseItemP.Visible;
        //    this.Category = pBaseItemP.Category;
        //    this.MinimumSize = pBaseItemP.MinimumSize;
        //    this.UsingViewOverflow = pBaseItemP.UsingViewOverflow;
        //    //ICustomizeComboBoxItemP_
        //    this.MinHeight = pBaseItemP.MinHeight;
        //    this.MinWidth = pBaseItemP.MinWidth;
        //    this.DropDownWidth = pBaseItemP.DropDownWidth;
        //    this.DropDownHeight = pBaseItemP.DropDownHeight;
        //    this.eCustomizeComboBoxStyle = pBaseItemP.eCustomizeComboBoxStyle;
        //    this.eModifySizeStyle = pBaseItemP.eModifySizeStyle;
        //    //IComboBoxItemP
        //    for (int i = 0; i < pBaseItemP.Items.Length; i++)
        //    {
        //        this.Items.Add(pBaseItemP.Items[i]);
        //    }
        //    this.SelectedIndex = pBaseItemP.SelectedIndex;
        //}
        #endregion

        protected override EventStateStyle GetEventStateSupplement(string strEventName)
        {
            switch (strEventName)
            {
                case "SelectedIndexChanged":
                    return this.SelectedIndexChanged != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                default:
                    break;
            }
            //
            return base.GetEventStateSupplement(strEventName);
        }

        protected override bool RelationEventSupplement(string strEventName, EventArgs e)
        {
            switch (strEventName)
            {
                case "SelectedIndexChanged":
                    if (this.SelectedIndexChanged != null) { this.SelectedIndexChanged(this, e as IntValueChangedEventArgs); }
                    return true;
                default:
                    break;
            }
            //
            return base.RelationEventSupplement(strEventName, e);
        }

        #region ICustomizeComboBoxItem
        [Browsable(true), DefaultValue(true), Description("表示弹出框是否自动关闭"), Category("状态")]
        public override bool AutoClosePopup
        {
            get
            {
                return this.CheckedDropDownList ? false : base.AutoClosePopup;
            }
            set
            {
                if (this.CheckedDropDownList) return;
                base.AutoClosePopup = value;
            }
        }

        [Browsable(true), DefaultValue(typeof(CustomizeComboBoxStyle), "eDropDown"), Description("文本框的编辑状态"), Category("外观")]
        public override CustomizeComboBoxStyle eCustomizeComboBoxStyle
        {
            get
            {
                return this.CheckedDropDownList ? CustomizeComboBoxStyle.eDropDownList : base.eCustomizeComboBoxStyle;
            }
            set
            {
                if (this.CheckedDropDownList) return;
                base.eCustomizeComboBoxStyle = value;
            }
        }
        #endregion

        #region IComboBoxItem
        [Browsable(true), Description("选择子项索引改变后触发"), Category("属性已更改")]
        public event IntValueChangedHandler SelectedIndexChanged;

        [Browsable(true), DefaultValue(-1), Description("下来框选择索引"), Category("数据")]
        public int SelectedIndex
        {
            get { return this.m_ViewItemListBox.SelectedIndex; }
            set
            {
                if (this.m_ViewItemListBox.SelectedIndex == value) return;
                this.m_ViewItemListBox.SelectedIndex = value;
                this.Refresh();
            }
        }

        [Browsable(false), Description("下来框选择的值"), Category("数据")]
        public View.ViewItem SelectedItem
        {
            get
            {
                //if (this.Items.Count <= 0)
                //{
                //    this.m_ViewItemListBox.SelectedIndex = -1;
                //    if (this.eCustomizeComboBoxStyle == CustomizeComboBoxStyle.eDropDownList) { this.Text = ""; }
                //} 
                return this.m_ViewItemListBox.SelectedItem;
            }
        }

        bool m_CheckedDropDownList = false;
        [Browsable(true), DefaultValue(false), Description("是否为复选框"), Category("外观")]
        public bool CheckedDropDownList
        {
            get { return m_CheckedDropDownList; }
            set { m_CheckedDropDownList = value; }
        }
        #endregion

        #region 属性
        [Browsable(true),
        Editor(typeof(GISShare.Controls.WinForm.WFNew.Design.ComboBoxItemCollectionEditer), typeof(System.Drawing.Design.UITypeEditor)),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
        Description("其所携带的子项集合"),
        Category("子项")]
        public View.ViewItemCollection Items
        {
            get { return this.m_ViewItemListBox.ViewItems; }
        }

        private bool m_RemoveUnEnabledCheckedItem = false;
        [Browsable(true), DefaultValue(false), Description("为复选框时，是否移除不可用的CheckedItem"), Category("外观")]
        public bool RemoveUnEnabledCheckedItem
        {
            get { return m_RemoveUnEnabledCheckedItem; }
            set { m_RemoveUnEnabledCheckedItem = value; }
        }

        /// <summary>
        /// 添加复选框
        /// </summary>
        /// <param name="bChecked">状态</param>
        /// <param name="strName">名称</param>
        /// <param name="strText">文本</param>
        /// <param name="objValue">属性</param>
        /// <param name="image">图片</param>
        /// <returns>返回复选框</returns>
        public ICheckBoxItem AddCheckedItem(bool bChecked, string strName, string strText, object objValue, System.Drawing.Image image)
        {
           
            return this.m_ViewItemListBox.AddCheckedItem(bChecked, strName, strText, objValue, image);
        }

        /// <summary>
        /// 添加单选框
        /// </summary>
        /// <param name="bChecked">状态</param>
        /// <param name="strName">名称</param>
        /// <param name="strText">文本</param>
        /// <param name="objValue">属性</param>
        /// <param name="image">图片</param>
        /// <returns>返回单选框</returns>
        public IRadioButtonItem AddRadioItem(bool bChecked, string strName, string strText, object objValue, System.Drawing.Image image)
        {

            return this.m_ViewItemListBox.AddRadioItem(bChecked, strName, strText, objValue, image);
        }

        /// <summary>
        /// 获取复选对象集合
        /// </summary>
        /// <param name="bChecked">状态</param>
        /// <returns></returns>
        public IList<ICheckBoxItem> GetCheckedItems(bool bChecked)
        {
            return this.m_ViewItemListBox.GetCheckedItems(bChecked);
        }
        #endregion

        #region Clone
        public override object Clone()
        {
            ComboBoxItem baseItem = new ComboBoxItem();
            baseItem.Checked = this.Checked;
            baseItem.Enabled = this.Enabled;
            baseItem.Font = this.Font;
            baseItem.ForeColor = this.ForeColor;
            baseItem.Name = this.Name;
            baseItem.Site = this.Site;
            baseItem.Size = this.Size;
            baseItem.Tag = this.Tag;
            baseItem.Text = this.Text;
            baseItem.Visible = this.Visible;
            baseItem.MinHeight = this.MinHeight;
            baseItem.MinWidth = this.MinWidth;
            baseItem.DropDownHeight = this.DropDownHeight;
            baseItem.DropDownWidth = this.DropDownWidth;
            baseItem.eModifySizeStyle = this.eModifySizeStyle;
            baseItem.eCustomizeComboBoxStyle = this.eCustomizeComboBoxStyle;
            baseItem.ArrowSize = this.ArrowSize;
            //IComboBoxItem
            foreach (View.ViewItem one in this.Items)
            {
                baseItem.Items.Add(one);
            }
            baseItem.SelectedIndex = this.SelectedIndex;
            if (this.GetEventState("VisibleChanged") == EventStateStyle.eUsed) baseItem.VisibleChanged += new EventHandler(baseItem_VisibleChanged);
            if (this.GetEventState("SizeChanged") == EventStateStyle.eUsed) baseItem.SizeChanged += new EventHandler(baseItem_SizeChanged);
            if (this.GetEventState("Paint") == EventStateStyle.eUsed) baseItem.Paint += new PaintEventHandler(baseItem_Paint);
            if (this.GetEventState("MouseUp") == EventStateStyle.eUsed) baseItem.MouseUp += new MouseEventHandler(baseItem_MouseUp);
            if (this.GetEventState("MouseMove") == EventStateStyle.eUsed) baseItem.MouseMove += new MouseEventHandler(baseItem_MouseMove);
            if (this.GetEventState("MouseLeave") == EventStateStyle.eUsed) baseItem.MouseLeave += new EventHandler(baseItem_MouseLeave);
            if (this.GetEventState("MouseEnter") == EventStateStyle.eUsed) baseItem.MouseEnter += new EventHandler(baseItem_MouseEnter);
            if (this.GetEventState("MouseDown") == EventStateStyle.eUsed) baseItem.MouseDown += new MouseEventHandler(baseItem_MouseDown);
            if (this.GetEventState("MouseDoubleClick") == EventStateStyle.eUsed) baseItem.MouseDoubleClick += new MouseEventHandler(baseItem_MouseDoubleClick);
            if (this.GetEventState("MouseClick") == EventStateStyle.eUsed) baseItem.MouseClick += new MouseEventHandler(baseItem_MouseClick);
            if (this.GetEventState("LocationChanged") == EventStateStyle.eUsed) baseItem.LocationChanged += new EventHandler(baseItem_LocationChanged);
            if (this.GetEventState("EnabledChanged") == EventStateStyle.eUsed) baseItem.EnabledChanged += new EventHandler(baseItem_EnabledChanged);
            if (this.GetEventState("CheckedChanged") == EventStateStyle.eUsed) baseItem.CheckedChanged += new EventHandler(baseItem_CheckedChanged);
            if (this.GetEventState("TextChanged") == EventStateStyle.eUsed) baseItem.TextChanged += new EventHandler(baseItem_TextChanged);
            if (this.GetEventState("KeyDown") == EventStateStyle.eUsed) baseItem.KeyDown += new KeyEventHandler(baseItem_KeyDown);
            if (this.GetEventState("KeyPress") == EventStateStyle.eUsed) baseItem.KeyPress += new KeyPressEventHandler(baseItem_KeyPress);
            if (this.GetEventState("KeyUp") == EventStateStyle.eUsed) baseItem.KeyUp += new KeyEventHandler(baseItem_KeyUp);
            if (this.GetEventState("PopupOpened") == EventStateStyle.eUsed) baseItem.PopupOpened += new EventHandler(baseItem_PopupOpened);
            if (this.GetEventState("PopupClosed") == EventStateStyle.eUsed) baseItem.PopupClosed += new EventHandler(baseItem_PopupClosed);
            if (this.GetEventState("SplitMouseUp") == EventStateStyle.eUsed) baseItem.SplitMouseUp += new MouseEventHandler(baseItem_SplitMouseUp);
            if (this.GetEventState("SplitMouseMove") == EventStateStyle.eUsed) baseItem.SplitMouseMove += new MouseEventHandler(baseItem_SplitMouseMove);
            if (this.GetEventState("SplitMouseDown") == EventStateStyle.eUsed) baseItem.SplitMouseDown += new MouseEventHandler(baseItem_SplitMouseDown);
            if (this.GetEventState("SplitMouseDoubleClick") == EventStateStyle.eUsed) baseItem.SplitMouseDoubleClick += new MouseEventHandler(baseItem_SplitMouseDoubleClick);
            if (this.GetEventState("SplitMouseClick") == EventStateStyle.eUsed) baseItem.SplitMouseClick += new MouseEventHandler(baseItem_SplitMouseClick);
            if (this.GetEventState("TextBoxMouseUp") == EventStateStyle.eUsed) baseItem.TextBoxMouseUp += new MouseEventHandler(baseItem_TextBoxMouseUp);
            if (this.GetEventState("TextBoxMouseMove") == EventStateStyle.eUsed) baseItem.TextBoxMouseMove += new MouseEventHandler(baseItem_TextBoxMouseMove);
            if (this.GetEventState("TextBoxMouseDown") == EventStateStyle.eUsed) baseItem.TextBoxMouseDown += new MouseEventHandler(baseItem_TextBoxMouseDown);
            if (this.GetEventState("TextBoxMouseDoubleClick") == EventStateStyle.eUsed) baseItem.TextBoxMouseDoubleClick += new MouseEventHandler(baseItem_TextBoxMouseDoubleClick);
            if (this.GetEventState("TextBoxMouseClick") == EventStateStyle.eUsed) baseItem.TextBoxMouseClick += new MouseEventHandler(baseItem_TextBoxMouseClick);
            if (this.GetEventState("SelectedIndexChanged") == EventStateStyle.eUsed) baseItem.SelectedIndexChanged += new IntValueChangedHandler(baseItem_SelectedIndexChanged);

            return baseItem;
        }
        void baseItem_CheckedChanged(object sender, EventArgs e)
        {
            this.RelationEvent("CheckedChanged", e);
        }
        void baseItem_EnabledChanged(object sender, EventArgs e)
        {
            this.RelationEvent("EnabledChanged", e);
        }
        void baseItem_LocationChanged(object sender, EventArgs e)
        {
            this.RelationEvent("LocationChanged", e);
        }
        void baseItem_MouseClick(object sender, MouseEventArgs e)
        {
            this.RelationEvent("MouseClick", e);
        }
        void baseItem_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.RelationEvent("MouseDoubleClick", e);
        }
        void baseItem_MouseDown(object sender, MouseEventArgs e)
        {
            this.RelationEvent("MouseDown", e);
        }
        void baseItem_MouseEnter(object sender, EventArgs e)
        {
            this.RelationEvent("MouseEnter", e);
        }
        void baseItem_MouseLeave(object sender, EventArgs e)
        {
            this.RelationEvent("MouseLeave", e);
        }
        void baseItem_MouseMove(object sender, MouseEventArgs e)
        {
            this.RelationEvent("MouseMove", e);
        }
        void baseItem_MouseUp(object sender, MouseEventArgs e)
        {
            this.RelationEvent("MouseUp", e);
        }
        void baseItem_Paint(object sender, PaintEventArgs e)
        {
            this.RelationEvent("Paint", e);
        }
        void baseItem_SizeChanged(object sender, EventArgs e)
        {
            this.RelationEvent("SizeChanged", e);
        }
        void baseItem_VisibleChanged(object sender, EventArgs e)
        {
            this.RelationEvent("VisibleChanged", e);
        }
        void baseItem_KeyUp(object sender, KeyEventArgs e)
        {
            this.RelationEvent("KeyUp", e);
        }
        void baseItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.RelationEvent("KeyPress", e);
        }
        void baseItem_KeyDown(object sender, KeyEventArgs e)
        {
            this.RelationEvent("KeyDown", e);
        }
        void baseItem_TextChanged(object sender, EventArgs e)
        {
            this.RelationEvent("TextChanged", e);
        }
        void baseItem_PopupClosed(object sender, EventArgs e)
        {
            this.RelationEvent("PopupClosed", e);
        }
        void baseItem_PopupOpened(object sender, EventArgs e)
        {
            this.RelationEvent("PopupOpened", e);
        }
        void baseItem_TextBoxMouseClick(object sender, MouseEventArgs e)
        {
            this.RelationEvent("TextBoxMouseClick", e);
        }
        void baseItem_TextBoxMouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.RelationEvent("TextBoxMouseDoubleClick", e);
        }
        void baseItem_TextBoxMouseDown(object sender, MouseEventArgs e)
        {
            this.RelationEvent("TextBoxMouseDown", e);
        }
        void baseItem_TextBoxMouseMove(object sender, MouseEventArgs e)
        {
            this.RelationEvent("TextBoxMouseMove", e);
        }
        void baseItem_TextBoxMouseUp(object sender, MouseEventArgs e)
        {
            this.RelationEvent("TextBoxMouseUp", e);
        }
        void baseItem_SplitMouseClick(object sender, MouseEventArgs e)
        {
            this.RelationEvent("SplitMouseClick(", e);
        }
        void baseItem_SplitMouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.RelationEvent("SplitMouseDoubleClick", e);
        }
        void baseItem_SplitMouseDown(object sender, MouseEventArgs e)
        {
            this.RelationEvent("SplitMouseDown", e);
        }
        void baseItem_SplitMouseMove(object sender, MouseEventArgs e)
        {
            this.RelationEvent("SplitMouseMove", e);
        }
        void baseItem_SplitMouseUp(object sender, MouseEventArgs e)
        {
            this.RelationEvent("SplitMouseUp", e);
        }
        void baseItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.RelationEvent("SelectedIndexChanged", e);
        }
        #endregion

        [Browsable(false)]
        public override object Value
        {
            get
            {
                if (this.CheckedDropDownList)
                {
                    return this.GetCheckedItems(true);
                }
                else
                {
                    return this.SelectedItem;
                }
            }
        }

        //[Browsable(false)]
        //public override string Text
        //{
        //    get
        //    {
        //        if (this.CheckedDropDownList)
        //        {
        //            string strText = "";
        //            IList<ICheckBoxItem> checkBoxItemList = this.GetCheckedItems(true);
        //            foreach (ICheckBoxItem one in checkBoxItemList)
        //            {
        //                strText += one.Text + ";";
        //            }
        //            return strText.TrimEnd(';');
        //        }
        //        else
        //        {
        //            if (this.SelectedItem != null) return this.SelectedItem.Text;
        //            return base.Text;
        //        }
        //    }
        //    set
        //    {
        //        if (this.CheckedDropDownList) return;
        //        if (this.SelectedItem != null) this.SelectedItem.Text = value;
        //        base.Text = value;
        //        this.Refresh();
        //    }
        //}
        [Browsable(false)]
        public override string Text
        {
            get
            {
                string strText = base.Text;
                if (System.String.IsNullOrEmpty(strText) || 
                    this.eCustomizeComboBoxStyle == CustomizeComboBoxStyle.eDropDownList)
                {
                    if (this.CheckedDropDownList)
                    {
                        IList<ICheckBoxItem> checkBoxItemList = this.GetCheckedItems(true);
                        foreach (ICheckBoxItem one in checkBoxItemList)
                        {
                            if (this.RemoveUnEnabledCheckedItem && !one.Enabled) continue;
                            strText += one.Text + ";";
                        }
                        strText = strText == null ? "" : strText.TrimEnd(';');
                    }
                    else
                    {
                        if (this.SelectedItem != null)
                        {
                            strText = this.SelectedItem.Text;
                        }
                        //else
                        //{
                        //    strText = base.Text;
                        //}
                    }
                }
                return strText;
            }
            set
            {
                if (this.CheckedDropDownList) return;
                //if (this.SelectedItem != null) this.SelectedItem.Text = value;
                base.Text = value;
                this.Refresh();
            }
        }

        public override int OffsetX
        {
            get
            {
                if (this.SelectedItem == null) return base.OffsetX;
                //
                if (this.SelectedItem is View.IColorViewItem)
                {
                    switch (this.eBorderStyle)
                    {
                        case BorderStyle.eNone:
                            return CTR_NONELEFTSPACE + CONST_COLORREGIONWIDTH + CTR_MIDDLESPACE;
                        case BorderStyle.eSingle:
                        default:
                            return CTR_SINGLELEFTSPACE + CONST_COLORREGIONWIDTH + CTR_MIDDLESPACE;
                    }
                }
                else if (this.SelectedItem is View.IImageViewItem)
                {
                    View.IImageViewItem pImageViewItem = this.SelectedItem as View.IImageViewItem;
                    if (pImageViewItem.Image == null)
                    {
                        return base.OffsetX;
                    }
                    else
                    {
                        switch (this.eBorderStyle)
                        {
                            case BorderStyle.eNone:
                                return CTR_NONELEFTSPACE + CTR_IMAGESIZE + CTR_MIDDLESPACE;
                            case BorderStyle.eSingle:
                            default:
                                return CTR_SINGLELEFTSPACE + CTR_IMAGESIZE + CTR_MIDDLESPACE;
                        }
                    }
                }
                else
                {
                    return base.OffsetX;
                }
            }
        }

        protected override void OnKeyDown(System.Windows.Forms.KeyEventArgs e)
        {
            if (this.Text.Length <= 0 && e.KeyCode == System.Windows.Forms.Keys.Back)
            {
                ((IInputObjectHelper)this).GetInputRegion().Show();
            }
            base.OnKeyDown(e);
        }

        protected override void OnDraw(System.Windows.Forms.PaintEventArgs e)
        {
            //base.OnDraw(e);
            //
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderCustomizeComboBox(
               new GISShare.Controls.WinForm.ObjectRenderEventArgs(e.Graphics, this, this.DisplayRectangle));
            //
            if (!this.IsInputing)
            {
                GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderTextBoxText(
                    new TextRenderEventArgs(e.Graphics, this, this.Enabled, this.Text, this.ForeColor, this.Font, this.TextRectangle));
            }
            //
            if (this.SelectedItem is View.IColorViewItem)
            {
                using (SolidBrush b = new SolidBrush(((View.IColorViewItem)this.SelectedItem).Color))
                {
                    Rectangle rectangle = this.DisplayRectangle;
                    rectangle = new Rectangle
                        (
                        rectangle.Left + (this.eBorderStyle == GISShare.Controls.WinForm.WFNew.BorderStyle.eNone ? CTR_NONELEFTSPACE : (CTR_BORDERSPASE + CTR_SINGLELEFTSPACE)),
                        (rectangle.Top + rectangle.Bottom - CONST_COLORREGIONHEIGHT) / 2,
                        CONST_COLORREGIONWIDTH,
                        CONST_COLORREGIONHEIGHT
                        );
                    e.Graphics.FillRectangle(b, rectangle);
                    using (Pen p = new Pen(Color.Black))
                    {
                        e.Graphics.DrawRectangle(p, rectangle);
                    }
                }
            }
            else if (this.SelectedItem is View.IImageViewItem)
            {
                Rectangle rectangle = this.DisplayRectangle;
                GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonImage
                    (
                    new ImageRenderEventArgs
                        (
                        e.Graphics,
                        this,
                        this.Enabled,
                        ((View.IImageViewItem)this.SelectedItem).Image,
                        new Rectangle
                            (
                            rectangle.Left + (this.eBorderStyle == GISShare.Controls.WinForm.WFNew.BorderStyle.eNone ? CTR_NONELEFTSPACE : (CTR_BORDERSPASE + CTR_SINGLELEFTSPACE)),
                            (rectangle.Top + rectangle.Bottom - CTR_IMAGESIZE) / 2,
                            CTR_IMAGESIZE,
                            CTR_IMAGESIZE
                            )
                        )
                    );
            }
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonArrow(
                new GISShare.Controls.WinForm.ArrowRenderEventArgs(e.Graphics, this, this.Enabled, ArrowStyle.eToDown, this.ForeColor, this.ArrowRectangle));
        }

        //
        protected virtual void OnSelectedIndexChanged(IntValueChangedEventArgs e)
        {
            if (this.SelectedIndexChanged != null) this.SelectedIndexChanged(this, e);
        }
    }
}
