using System.Text.Json.Serialization;

namespace Order_Processor
{
    class OrderType
    { 
        public OrderType(string _Ref, string _Name, string _Client, DateTime _LimtDate, DateTime _OrderDate, StateType _State, MaterialStateType _MaterialState)
        {
            Ref = _Ref;
            Name = _Name;
            Client = _Client;
            LimitDate = _LimtDate;
            OrderDate = _OrderDate;
            State = _State;
            MaterialState = _MaterialState;
        }

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
