using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;

namespace GISShare.Controls.WinForm
{
    public class TreeNodeItemConverter : TypeConverter
    {
        // Methods
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return ((destinationType == typeof(InstanceDescriptor)) || base.CanConvertTo(context, destinationType));
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == null)
            {
                throw new ArgumentNullException("destinationType");
            }
            if ((destinationType == typeof(InstanceDescriptor)) && (value is TreeNodeItem))
            {
                TreeNodeItem node = (TreeNodeItem)value;
                System.Reflection.MemberInfo member = null;
                object[] arguments = null;
                if ((node.ImageIndex == -1) || (node.SelectedImageIndex == -1))
                {
                    if (node.Nodes.Count == 0)
                    {
                        member = typeof(TreeNodeItem).GetConstructor(new Type[] { typeof(string) });
                        arguments = new object[] { node.Text };
                    }
                    else
                    {
                        member = typeof(TreeNodeItem).GetConstructor(new Type[] { typeof(string), typeof(TreeNode[]) });
                        TreeNode[] dest = new TreeNode[node.Nodes.Count];
                        node.Nodes.CopyTo(dest, 0);
                        arguments = new object[] { node.Text, dest };
                    }
                }
                else if (node.Nodes.Count == 0)
                {
                    member = typeof(TreeNodeItem).GetConstructor(new Type[] { typeof(string), typeof(int), typeof(int) });
                    arguments = new object[] { node.Text, node.ImageIndex, node.SelectedImageIndex };
                }
                else
                {
                    member = typeof(TreeNodeItem).GetConstructor(new Type[] { typeof(string), typeof(int), typeof(int), typeof(TreeNode[]) });
                    TreeNode[] nodeArray2 = new TreeNode[node.Nodes.Count];
                    node.Nodes.CopyTo(nodeArray2, 0);
                    arguments = new object[] { node.Text, node.ImageIndex, node.SelectedImageIndex, nodeArray2 };
                }
                if (member != null)
                {
                    return new InstanceDescriptor(member, arguments, false);
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
