using System.Runtime.Serialization;

namespace Order_Processor
{
    enum MaterialStateType
    {
        [EnumMember(Value = "OR")]
        Orderd,
        [EnumMember(Value = "LT")]
        Late,
        [EnumMember(Value = "DN")]
        Done
    }
}
