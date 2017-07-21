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
    [Serializable, DefaultProperty("Text"), TypeConverter(typeof(TreeNodeItemConverter))]
    public class TreeNodeItem : TreeNode, IItem, WinForm.IImageItem
    {
        public TreeNodeItem() 
            : base() { }

        public TreeNodeItem(string text)
            : base(text) { }

        public TreeNodeItem(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context) { }

        public TreeNodeItem(string text, TreeNode[] children)
            : base(text, children) { }

        public TreeNodeItem(string text, int imageIndex, int selectedImageIndex)
            : base(text, imageIndex, selectedImageIndex) { }

        public TreeNodeItem(string text, int imageIndex, int selectedImageIndex, TreeNode[] children)
            : base(text, imageIndex, selectedImageIndex, children) { }

        #region IImageItem
        [Browsable(false), DefaultValue(false), Description("展示背景颜色，set无效"), Category("外观")]
        bool WinForm.IItem.ShowBackColor
        {
            get { return !this.BackColor.IsEmpty; }
            set { }
        }

        [Browsable(false), DefaultValue(typeof(Font), "\"宋体\", 9f"), Description("节点字体"), Category("外观")]
        Font WinForm.IFontItem.Font
        {
            get { return this.NodeFont; }
            set { this.NodeFont = value; }
        }

        [Browsable(false), Description("图片，set无效"), Category("外观")]
        Image WinForm.IImageItem.Image
        {
            get
            {
                if (this.TreeView == null || this.TreeView.ImageList == null) return null;
                //
                if (this.IsSelected)
                {
                    if (this.SelectedImageIndex >= 0 && this.SelectedImageIndex < this.TreeView.ImageList.Images.Count) return this.TreeView.ImageList.Images[this.SelectedImageIndex];
                    if (this.TreeView.ImageList.Images.Keys.Contains(this.SelectedImageKey)) return this.TreeView.ImageList.Images[this.SelectedImageKey];
                }
                if (this.ImageIndex >= 0 && this.ImageIndex < this.TreeView.ImageList.Images.Count) return this.TreeView.ImageList.Images[this.ImageIndex];
                if (this.TreeView.ImageList.Images.Keys.Contains(this.ImageKey)) return this.TreeView.ImageList.Images[this.ImageKey];
                return null;
            }
            set { }
        }
        #endregion
    }
}
