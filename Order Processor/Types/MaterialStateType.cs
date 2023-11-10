using System.ComponentModel;
using System.Runtime.Serialization;

namespace Order_Processor.Types
{
    enum MaterialStateType
    {
        [Description("Não definido")]
        [EnumMember(Value = "")]
        Undefined,
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
