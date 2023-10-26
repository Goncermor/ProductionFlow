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

        public string Ref { get; set; }
        public string Name { get; set; }
        public string Client { get; set; }
        public DateTime LimitDate { get; set; }
        public DateTime OrderDate { get; set; }
        public StateType State { get; set; }
        public MaterialStateType MaterialState { get; set; }


    }
}
