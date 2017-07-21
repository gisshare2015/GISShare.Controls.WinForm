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
        [Browsable(true), DefaultValue(false), Description("ʹ�����ڵ�Ľ������������"), Category("���")]
        public bool UsingNodeRegionStyle
        {
            get { return m_UsingNodeRegionStyle; }
            set { m_UsingNodeRegionStyle = value; }
        }

        bool m_ShowLines = false;
        [Browsable(true), DefaultValue(false), Description("��ʾ������"), Category("���")]
        public bool ShowLines
        {
            get { return m_ShowLines; }
            set { m_ShowLines = value; }
        }

        bool m_ShowPlusMinus = true;
        [Browsable(true), DefaultValue(true), Description("��ʾ�۵�����"), Category("���")]
        public bool ShowPlusMinus
        {
            get { return m_ShowPlusMinus; }
            set { m_ShowPlusMinus = value; }
        }

        bool m_ShowStateImageOrCheckBox = true;
        [Browsable(true), DefaultValue(true), Description("�Ƿ���ʾ��ѡ���״̬ͼƬ"), Category("���")]
        public bool ShowStateImageOrCheckBox
        {
            get { return m_ShowStateImageOrCheckBox; }
            set { m_ShowStateImageOrCheckBox = value; }
        }

        bool m_SystemColor = true;
        [Browsable(true), DefaultValue(true), Description("ʹ��ϵͳ��Ⱦ"), Category("���")]
        public bool SystemColor
        {
            get { return m_SystemColor; }
            set { m_SystemColor = value; }
        }

        private Color m_TitleBorder = Color.FromArgb(174, 179, 185);
        [Browsable(true), Description("����������"), Category("���")]
        public Color TitleBorder
        {
            get { return m_TitleBorder; }
            set { m_TitleBorder = value; }
        }

        private Color m_TitleBackgroundBegin = Color.FromArgb(246, 251, 247);
        [Browsable(true), Description("������ʼɫ"), Category("���")]
        public Color TitleBackgroundBegin
        {
            get { return m_TitleBackgroundBegin; }
            set { m_TitleBackgroundBegin = value; }
        }

        private Color m_TitleBackgroundEnd = Color.FromArgb(214, 224, 234);
        [Browsable(true), Description("������ֹɫ"), Category("���")]
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
