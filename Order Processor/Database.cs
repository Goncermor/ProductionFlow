using System.IO;
using System.Text.Json;
namespace Order_Processor
{
    class Database
    {
        public static List<string> ClientList = new List<string>();
        public static List<Types.OrderType> OrderList = new List<Types.OrderType>();

        public static void Load()
        {
            if (!File.Exists(Config.DB_File)) File.WriteAllText(Config.DB_File, "[]");
            Types.OrderType[]? ReadValues = JsonSerializer.Deserialize<Types.OrderType[]>(File.ReadAllText(Config.DB_File));
            OrderList.AddRange(ReadValues!);
            foreach (Types.OrderType Order in ReadValues!) ClientList.Add(Order.Client);
        }
        public static void SaveDb()
        {
            string SaveData = JsonSerializer.Serialize<Types.OrderType[]>(OrderList.ToArray());
            File.WriteAllText(Config.DB_File, SaveData);
        }
    }
}