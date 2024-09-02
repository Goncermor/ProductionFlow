using System.ComponentModel;
using System.Runtime.Serialization;

namespace Production_Flow.Types
{
    public enum StateType
    {
        [Description("Por Fazer")]
        [EnumMember(Value = "TD")]
        ToDo,
        [Description("Aguarda Material")]
        [EnumMember(Value = "AM")]
        AwaitingMaterial,
        [Description("Produção")]
        [EnumMember(Value = "PR")]
        Production,
        [Description("Controlo de Qualidade")]
        [EnumMember(Value = "QC")]
        QualityControl,
        [Description("Atrazado")]
        [EnumMember(Value = "PD")]
        PastDue,
        [Description("Enviado")]
        [EnumMember(Value = "ST")]
        Sent
    }
}
