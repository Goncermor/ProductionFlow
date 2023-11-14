using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Order_Processor.Helpers
{
    public class EnumHelper
    {
        public static string GetDescription(object EnumValue)
        {
            if (EnumValue is Enum enumValue)
            {
                FieldInfo field = enumValue.GetType().GetField(enumValue.ToString())!;
                DescriptionAttribute? attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute))!;
                return attribute == null ? enumValue.ToString() : attribute.Description;
            }
            return EnumValue.ToString()!;
        }
    }
}
