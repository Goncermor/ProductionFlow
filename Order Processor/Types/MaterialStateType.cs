using System.Runtime.Serialization;

namespace Order_Processor
{
    enum StateType
    {
        [EnumMember(Value = "TD")]
        ToDo,
        [EnumMember(Value = "PR")]
        Production,
        [EnumMember(Value = "QC")]
        QualityControl,
        [EnumMember(Value = "PD")]
        PastDue,
        [EnumMember(Value = "DN")]
        Done
    }
}
