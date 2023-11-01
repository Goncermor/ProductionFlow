using System.ComponentModel;
using System.Runtime.Serialization;

namespace Order_Processor.Types
{
    enum StateType
    {
        [Description("Por Fazer")]
        [EnumMember(Value = "TD")]
        ToDo,
        [Description("Produção")]
        [EnumMember(Value = "PR")]
        Production,
        [Description("Controlo de Qualidade")]
        [EnumMember(Value = "QC")]
        QualityControl,
        [Description("Atrazado")]
        [EnumMember(Value = "PD")]
        PastDue,
        [Description("Feito")]
        [EnumMember(Value = "DN")]
        Done
    }
}
