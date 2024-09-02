using System.ComponentModel;
using System.Reflection;

namespace Production_Flow.Helpers
{
    public static class EnumHelper
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

        public static string[] GetDescriptionList(Type TargetEnum)
        {
            List<string> List = new List<string>();
            foreach (object Item in Enum.GetValues(TargetEnum))
                List.Add(GetDescription(Item));
            return List.ToArray();
        }
    }
}
