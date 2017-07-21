using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;

namespace GISShare.Controls.WinForm.WFNew.View
{
    public class VRowImageNodeViewItem : ImageNodeViewItem, IRowViewItem
    {
        VRowViewItem m_pRowViewItem;

        public VRowImageNodeViewItem()
            : base()
        {
            this.m_pRowViewItem = new VRowViewItem();
            this.m_pRowViewItem.Text = "VRowViewItem";
            this.m_pRowViewItem.ShowBaseItemState = false;
            ((ISetOwnerHelper)this.m_pRowViewItem).SetOwner(this);
        }

        public VRowImageNodeViewItem(string text)
            : this()
        {
            this.Text = text;
        }

        public VRowImageNodeViewItem(string name, string text)
            : this()
        {
            this.Name = name; 
            this.Text = text;
        }

        public VRowImageNodeViewItem(string text, Image image)
            : this()
        {
            this.Text = text;
            this.Image = image;
        }

        public VRowImageNodeViewItem(string name, string text, Image image)
            : this()
        {
            this.Name = name; 
            this.Text = text;
            this.Image = image;
        }

        #region IRowViewItem
        [Browsable(false), DefaultValue(-1), Description("选择ViewItem索引"), Category("属性")]
        public int SelectedIndex
        {
            get { return this.m_pRowViewItem.SelectedIndex; }
            set { this.m_pRowViewItem.SelectedIndex = value; }
        }

        IFlexibleList IRowViewItem.Items { get { return this.ViewItems; } }
        #endregion

        #region IViewLayoutList
        [Browsable(false), Description("最左边ViewItem索引号"), Category("布局")]
        public int TopViewItemIndex
        {
            get { return this.m_pRowViewItem.TopViewItemIndex; }
        }

        [Browsable(false), Description("最右边ViewItem索引号"), Category("布局")]
        public int BottomViewItemIndex
        {
            get { return this.m_pRowViewItem.BottomViewItemIndex; }
        }

        public ViewItem TryGetViewItemFromPoint(Point point)
        {
            return this.m_pRowViewItem.TryGetViewItemFromPoint(point);
        }
        #endregion

        [Browsable(true),
        Editor(typeof(GISShare.Controls.WinForm.WFNew.View.Design.SizeViewItemCollectionEditer), typeof(System.Drawing.Design.UITypeEditor)),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
        Description("其所携带的子项集合"),
        Category("子项")]
        public SizeViewItemCollection ViewItems
        {
            get { return this.m_pRowViewItem.ViewItems; }
        }

        public override int Height
        {
            get
            {
                return this.m_pRowViewItem.Height;
            }
            set
            {
                base.Height = value;
                this.m_pRowViewItem.Height = value;
            }
        }

        public override bool CanEdit
        {
            get
            {
                if (this.ViewItems.Count > 0)
                {
                    ITextEditViewItem pTextEditViewItem = this.ViewItems[this.SelectedIndex] as ITextEditViewItem;
                    return pTextEditViewItem == null ? false : pTextEditViewItem.CanEdit;
                }
                else
                {
                    return base.CanEdit;
                }
            }
            set
            {
                base.CanEdit = value;
            }
        }

        protected internal override object ITextEditViewItem_EditObject
        {
            get
            {
                if (this.ViewItems.Count > 0 && this.SelectedIndex >= 0)
                {
                    IInputObject pInputObject = this.ViewItems[this.SelectedIndex] as IInputObject;
                    if (pInputObject != null)
                    {
                        return pInputObject;
                    }
                    else
                    {
                        return base.ITextEditViewItem_EditObject;
                    }
                }
                else
                {
                    return base.ITextEditViewItem_EditObject;
                }
            }
        }

        protected internal override string IInputObject_InputText
        {
            get
            {
                if (this.ViewItems.Count > 0 && this.SelectedIndex >= 0)
                {
                    IInputObject pInputObject = this.ViewItems[this.SelectedIndex] as IInputObject;
                    if (pInputObject != null)
                    {
                        return pInputObject.InputText;
                    }
                    else
                    {
                        return base.IInputObject_InputText;
                    }
                }
                else
                {
                    return base.IInputObject_InputText;
                }
            }
            set
            {
                if (this.ViewItems.Count > 0 && this.SelectedIndex >= 0)
                {
                    IInputObject pInputObject = this.ViewItems[this.SelectedIndex] as IInputObject;
                    if (pInputObject != null)
                    {
                        pInputObject.InputText = value;
                    }
                    else
                    {
                        base.IInputObject_InputText = value;
                    }
                }
                else
                {
                    base.IInputObject_InputText = value;
                }
            }
        }

        protected internal override Color IInputObject_ForeColor
        {
            get
            {
                if (this.ViewItems.Count > 0 && this.SelectedIndex >= 0)
                {
                    IInputObject pInputObject = this.ViewItems[this.SelectedIndex] as IInputObject;
                    if (pInputObject != null)
                    {
                        return pInputObject.InputForeColor;
                    }
                    else
                    {
                        return base.IInputObject_ForeColor;
                    }
                }
                else
                {
                    return base.IInputObject_ForeColor;
                }
            }
        }

        protected internal override Font IInputObject_InputFont
        {
            get
            {
                if (this.ViewItems.Count > 0 && this.SelectedIndex >= 0)
                {
                    IInputObject pInputObject = this.ViewItems[this.SelectedIndex] as IInputObject;
                    if (pInputObject != null)
                    {
                        return pInputObject.InputFont;
                    }
                    else
                    {
                        return base.IInputObject_InputFont;
                    }
                }
                else
                {
                    return base.IInputObject_InputFont;
                }
            }
        }

        protected internal override Rectangle IInputObject_InputRegionRectangle
        {
            get
            {
                if (this.ViewItems.Count > 0 && this.SelectedIndex >= 0)
                {
                    IInputObject pInputObject = this.ViewItems[this.SelectedIndex] as IInputObject;
                    if (pInputObject != null)
                    {
                        return pInputObject.InputRegionRectangle;
                    }
                    else
                    {
                        return base.IInputObject_InputRegionRectangle;
                    }
                }
                else
                {
                    return base.IInputObject_InputRegionRectangle;
                }
            }
        }

        protected override void MessageMonitor(MessageInfo messageInfo)
        {
            base.MessageMonitor(messageInfo);
            //
            switch (messageInfo.eMessageStyle)
            {
                case MessageStyle.eMSPaint:
                    ISetViewItemHelper pSetViewItemHelper = this.m_pRowViewItem as ISetViewItemHelper;
                    if (pSetViewItemHelper != null)
                    {
                        pSetViewItemHelper.SetViewItemDisplayRectangle(
                            Rectangle.FromLTRB(this.TextRectangle.Left, this.DisplayRectangle.Top, this.DisplayRectangle.Right, this.DisplayRectangle.Bottom));
                    }
                    break;
                default:
                    break;
            }
            //
            IMessageChain pMessageChain = this.m_pRowViewItem as IMessageChain;
            if (pMessageChain != null)
            {
                pMessageChain.SendMessage(messageInfo);
            }
        }

        protected override void OnViewParameterStyleChanged(ViewParameterStyle viewParameterStyle)
        {
            if (viewParameterStyle == ViewParameterStyle.eNone)
            {
                this.SelectedIndex = -1;
            }
            //
            //base.OnViewParameterStyleChanged(viewParameterStyle);
        }

        public override Size MeasureSize(Graphics g)
        {
            if (this.ViewItems.Count > 0)
            {
                //int iH = 0;
                //foreach (ViewItem one in this.ViewItems)
                //{
                //    iH += one.MeasureSize(g).Height;
                //}
                //return new Size(this.Width, iH);
                Size size = this.m_pRowViewItem.MeasureSize(g);
                return new Size(this.NodeTextOffset + size.Width, size.Height);
            }
            //
            return base.MeasureSize(g);
        }
        protected override void OnDraw(System.Windows.Forms.PaintEventArgs e)
        {
            if (this.ViewItems.Count > 0)
            {
                WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderNodeViewItem(
                    new ObjectRenderEventArgs(e.Graphics, this,
                        Rectangle.FromLTRB(e.ClipRectangle.Left, e.ClipRectangle.Top, e.ClipRectangle.Right - 1, e.ClipRectangle.Bottom - 1)
                        ));
            }
            else
            {
                base.OnDraw(e);
            }
        }
    }
}
