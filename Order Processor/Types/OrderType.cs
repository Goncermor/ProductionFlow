using System.Text.Json.Serialization;

namespace Order_Processor
{
    class OrderType
    {

        [JsonPropertyName("r")]
        public string Ref { get; set; }

        [JsonPropertyName("n")]
        public string Name { get; set; }

        [JsonPropertyName("c")]
        public string Client { get; set; }

        [JsonPropertyName("ld")]
        public DateTime LimitDate { get; set; }

        [JsonPropertyName("od")]
        public DateTime OrderDate { get; set; }

        [JsonPropertyName("s")]
        public StateType State { get; set; }
        [JsonPropertyName("ms")]
        public MaterialStateType MaterialState { get; set; }


    }
}
