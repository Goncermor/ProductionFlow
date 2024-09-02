using System.Text.Json.Serialization;

namespace Production_Flow.Types
{
    public class OrderType
    {
        [JsonPropertyName("po")]
        public string PurchaseOrder { get; set; } = "";
        [JsonPropertyName("r")]
        public string Ref { get; set; } = "";
        [JsonPropertyName("p")]
        public string Price { get; set; } = "";
        [JsonPropertyName("a")]
        public string Amount { get; set; } = "";
        [JsonPropertyName("c")]
        public string Client { get; set; } = "";
        [JsonPropertyName("ld")]
        public long LimitDate { get; set; }
        [JsonPropertyName("od")]
        public long OrderDate { get; set; }
        [JsonPropertyName("s")]
        public StateType State { get; set; }
        [JsonPropertyName("ms")]
        public MaterialStateType MaterialState { get; set; }
        [JsonPropertyName("no")]
        public string Notes { get; set; } = "";

    }
}
