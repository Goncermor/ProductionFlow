using ModernWpf.Controls;
using System.Data;

namespace Order_Processor.Controls
{
    public partial class DataEditor : ContentDialog
    {
        public List<string> ClientList = new List<string>();
        public DataEditor()
        {
            InitializeComponent();
           
        }

        private void ClientBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                sender.ItemsSource = ClientList.Where(x => x.IndexOf(sender.Text, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            }
        }
    }
}
