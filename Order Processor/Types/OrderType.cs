using System.Text.Json.Serialization;

namespace Order_Processor.Types
{
    class OrderType
    {
        [JsonPropertyName("po")]
        public string PurchaseOrder { get; set; } = "";
        [JsonPropertyName("r")]
        public string Ref { get; set; } = "";
        [JsonPropertyName("n")]
        public string Name { get; set; } = "";
        [JsonPropertyName("c")]
        public string Client { get; set; } = "";
        [JsonPropertyName("ld")]
        public DateTime LimitDate { get; set; }
        [JsonPropertyName("od")]
        public DateTime OrderDate { get; set; }
        [JsonPropertyName("s")]
        public StateType State { get; set; } = StateType.Undefined;
        [JsonPropertyName("d")]
        public string Notes { get; set; } = "";
        [JsonPropertyName("no")]
        public MaterialStateType MaterialState { get; set; } = MaterialStateType.Undefined;


    }
}
