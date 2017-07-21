using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace GISShare.Controls.WinForm
{
    [Serializable, DefaultProperty("Text"), TypeConverter(typeof(TitleTreeNodeItemConverter))]
    public class TitleTreeNodeItem : TreeNodeItem, IItem, WinForm.IImageItem
    {
        public TitleTreeNodeItem()
            : base() { }

        public TitleTreeNodeItem(string text)
            : base(text) { }

        public TitleTreeNodeItem(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context) { }

        public TitleTreeNodeItem(string text, TreeNode[] children)
            : base(text, children) { }

        public TitleTreeNodeItem(string text, int imageIndex, int selectedImageIndex)
            : base(text, imageIndex, selectedImageIndex) { }

        public TitleTreeNodeItem(string text, int imageIndex, int selectedImageIndex, TreeNode[] children)
            : base(text, imageIndex, selectedImageIndex, children) { }

        bool m_UsingNodeRegionStyle = false;
        [Browsable(true), DefaultValue(false), Description("使用树节点的结点区布局类型"), Category("外观")]
        public bool UsingNodeRegionStyle
        {
            get { return m_UsingNodeRegionStyle; }
            set { m_UsingNodeRegionStyle = value; }
        }

        bool m_ShowLines = false;
        [Browsable(true), DefaultValue(false), Description("显示连接线"), Category("外观")]
        public bool ShowLines
        {
            get { return m_ShowLines; }
            set { m_ShowLines = value; }
        }

        bool m_ShowPlusMinus = true;
        [Browsable(true), DefaultValue(true), Description("显示折叠符号"), Category("外观")]
        public bool ShowPlusMinus
        {
            get { return m_ShowPlusMinus; }
            set { m_ShowPlusMinus = value; }
        }

        bool m_ShowStateImageOrCheckBox = true;
        [Browsable(true), DefaultValue(true), Description("是否显示复选框和状态图片"), Category("外观")]
        public bool ShowStateImageOrCheckBox
        {
            get { return m_ShowStateImageOrCheckBox; }
            set { m_ShowStateImageOrCheckBox = value; }
        }

        bool m_SystemColor = true;
        [Browsable(true), DefaultValue(true), Description("使用系统渲染"), Category("外观")]
        public bool SystemColor
        {
            get { return m_SystemColor; }
            set { m_SystemColor = value; }
        }

        private Color m_TitleBorder = Color.FromArgb(174, 179, 185);
        [Browsable(true), Description("标题轮廓线"), Category("外观")]
        public Color TitleBorder
        {
            get { return m_TitleBorder; }
            set { m_TitleBorder = value; }
        }

        private Color m_TitleBackgroundBegin = Color.FromArgb(246, 251, 247);
        [Browsable(true), Description("背景起始色"), Category("外观")]
        public Color TitleBackgroundBegin
        {
            get { return m_TitleBackgroundBegin; }
            set { m_TitleBackgroundBegin = value; }
        }

        private Color m_TitleBackgroundEnd = Color.FromArgb(214, 224, 234);
        [Browsable(true), Description("背景终止色"), Category("外观")]
        public Color TitleBackgroundEnd
        {
            get { return m_TitleBackgroundEnd; }
            set { m_TitleBackgroundEnd = value; }
        }

        public override object Clone()
        {
            TitleTreeNodeItem item = new TitleTreeNodeItem();
            item.BackColor = this.BackColor;
            item.Checked = this.Checked;
            item.ContextMenu = this.ContextMenu;
            item.ContextMenuStrip = this.ContextMenuStrip;
            item.ForeColor = this.ForeColor;
            item.ImageIndex = this.ImageIndex;
            item.ImageKey = this.ImageKey;
            item.Name = this.Name;
            item.NodeFont = this.NodeFont;
            item.SelectedImageIndex = this.SelectedImageIndex;
            item.SelectedImageKey = this.SelectedImageKey;
            item.StateImageIndex = this.StateImageIndex;
            item.StateImageKey = this.StateImageKey;
            item.Tag = this.Tag;
            item.Text = this.Text;
            item.ToolTipText = this.ToolTipText;
            item.ShowLines = this.ShowLines;
            item.ShowPlusMinus = this.ShowPlusMinus;
            item.ShowStateImageOrCheckBox = this.ShowStateImageOrCheckBox;
            item.SystemColor = this.SystemColor;
            item.TitleBorder = this.TitleBorder;
            item.TitleBackgroundBegin = this.TitleBackgroundBegin;
            item.TitleBackgroundEnd = this.TitleBackgroundEnd;
            item.UsingNodeRegionStyle = this.UsingNodeRegionStyle;
            for (int i = 0; i < this.Nodes.Count; i++)
            {
                item.Nodes.Add((TreeNode)this.Nodes[i].Clone());
            }

            return item;
        }
    }
}
