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
        public List<Types.OrderType> OrderList = new List<Types.OrderType>();
        public Database() {

            if (!File.Exists(Config.DB_File)) File.WriteAllText(Config.DB_File, "[]");
            Types.OrderType[]? ReadValues = JsonSerializer.Deserialize<Types.OrderType[]>(File.ReadAllText(Config.DB_File));
            OrderList.AddRange(ReadValues!);
            foreach (Types.OrderType Order in ReadValues!) ClientList.Add(Order.Client);
        }
        public void SaveDb()
        {
            string SaveData = JsonSerializer.Serialize<Types.OrderType[]>(OrderList.ToArray());
            File.WriteAllText(Config.DB_File, SaveData);
        }
    }
}