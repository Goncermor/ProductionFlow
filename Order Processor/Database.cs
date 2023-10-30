using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Controls;

namespace Order_Processor
{
    class Database
    {
        public List<string> ClientList = new List<string>();
        public List<OrderType> OrderList = new List<OrderType>();
        public Database() {

            if (!File.Exists(Config.DB_File)) File.WriteAllText(Config.DB_File, "{}");
            OrderType[]? ReadValues = JsonSerializer.Deserialize<OrderType[]>(File.ReadAllText(Config.DB_File));
            OrderList.AddRange(ReadValues!);
            foreach (OrderType Order in ReadValues!) ClientList.Add(Order.Client);
        }
        public void SaveDb()
        {
            string SaveData = JsonSerializer.Serialize<OrderType[]>(OrderList.ToArray());
            File.WriteAllText(Config.DB_File, SaveData);
        }
    }
}