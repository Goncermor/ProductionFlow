using System.ComponentModel;
using System.Runtime.Serialization;

namespace Production_Flow.Types
{
    public enum MaterialStateType
    {
        [Description("Encomendado")]
        [EnumMember(Value = "OR")]
        Orderd,
        [Description("Atrazado")]
        [EnumMember(Value = "LT")]
        Late,
        [Description("Pronto")]
        [EnumMember(Value = "DN")]
        Done
    }
}
