using System.Text.Json.Serialization;

namespace Production_Flow.Types
{
    public class OrderType
    {
        [JsonPropertyName("c")]
        public string Client { get; set; } = "";

        [JsonPropertyName("po")]
        public string PurchaseOrder { get; set; } = "";

        [JsonPropertyName("r")]
        public string Ref { get; set; } = "";

        [JsonPropertyName("od")]
        public long OrderDate { get; set; }

        [JsonPropertyName("ld")]
        public long LimitDate { get; set; }

        [JsonPropertyName("s")]
        public StateType State { get; set; }

        [JsonPropertyName("no")]
        public string Notes { get; set; } = "";

    }
}
