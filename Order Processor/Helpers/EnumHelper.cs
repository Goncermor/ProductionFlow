using System.ComponentModel;
using System.Reflection;

namespace Order_Processor.Helpers
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

        public static IEnumerable<Types.EnumItem> GetDescriptionList(Type TargetEnum)
        {
            List<Types.EnumItem> Values = new List<Types.EnumItem>();
            Values.AddRange(Enum.GetValues(TargetEnum).Cast<Enum>().Select((e) => new Types.EnumItem { Value = e, Description = GetDescription(e)}).ToList());
            return Values.ToList();
            
        }
    }
}
