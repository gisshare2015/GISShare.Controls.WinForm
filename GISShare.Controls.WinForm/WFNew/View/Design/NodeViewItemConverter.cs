using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;

namespace GISShare.Controls.WinForm.WFNew.View.Design
{
    public class NodeViewItemConverter : TypeConverter
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
            if ((destinationType == typeof(InstanceDescriptor)) && (value is NodeViewItem))
            {
                NodeViewItem node = (NodeViewItem)value;
                System.Reflection.MemberInfo member = null;
                object[] arguments = new object[] { };
                member = typeof(NodeViewItem).GetConstructor(new Type[] { });
                if (member != null)
                {
                    return new InstanceDescriptor(member, arguments, false);
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
