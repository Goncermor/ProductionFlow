using ModernWpf.Controls;
using Production_Flow.Types;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Production_Flow
{
    public partial class MainWindow : Window
    {
        

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_ContentRendered(object sender, EventArgs e) => _ = LoadData();

        #region Database Manager

        private async Task LoadData()
        {
            await Task.Delay(1000);
            LoadSplash.Visibility = Visibility.Hidden;


            DataGrid.ItemsSource = Database.OrderList;
            DataEditor.ClientList = Database.ClientList;
        }

        private async void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            DataEditor.IsEditMode = false;
            DataEditor.PriceBox.Text = "";
            DataEditor.RefBox.Text = "";
            DataEditor.PurchaseOrderBox.Text = "";
            DataEditor.ClientBox.Text = "";
            DataEditor.PurchaseOrderBox.Text = "";
            DataEditor.NotesBox.Text = "";
            DataEditor.AmountBox.Text = "";
            DataEditor.StateComboBox.SelectedIndex = -1;
            DataEditor.MaterialStateComboBox.SelectedIndex = -1;
            ContentDialogResult Result = await DataEditor.ShowAsync();
            if (Result == ContentDialogResult.Primary)
            {
                OrderType NewOrder = new OrderType() {
                    Client = DataEditor.ClientBox.Text,
                    Price = DataEditor.PriceBox.Text,
                    Amount = DataEditor.AmountBox.Text,
                    Ref = DataEditor.RefBox.Text,
                    PurchaseOrder = DataEditor.PurchaseOrderBox.Text,
                    State = (Types.StateType)DataEditor.StateComboBox.SelectedIndex,
                    MaterialState = (Types.MaterialStateType)DataEditor.MaterialStateComboBox.SelectedIndex,
                    LimitDate = ((DateTimeOffset)DataEditor.LimitDateDatePicker.SelectedDate.Value).ToUnixTimeSeconds(),
                    OrderDate = ((DateTimeOffset)DataEditor.OrderDatePicker.SelectedDate.Value).ToUnixTimeSeconds(),
                    Notes = DataEditor.NotesBox.Text
                };
                Database.OrderList.Add(NewOrder);
                if (!Database.ClientList.Contains(DataEditor.ClientBox.Text)) Database.ClientList.Add(DataEditor.ClientBox.Text);
                Database.SaveDb();
                DataGrid.Items.Refresh();
            }

        }

        #endregion










        #region Context Menu
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGrid.SelectedItems.Count == 1)
                MenuItem_Edit.IsEnabled = true;
            else
                MenuItem_Edit.IsEnabled = false;
        }


        private void MenuCopy_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder Sb = new StringBuilder();
            foreach (object SelectedItem in DataGrid.SelectedItems)
            {
                Types.OrderType SelectedLine = (Types.OrderType)SelectedItem;
                Sb.AppendLine($"Cliente: {SelectedLine.Client}");
                Sb.AppendLine($"PO: {SelectedLine.PurchaseOrder}");
                Sb.AppendLine($"Ref: {SelectedLine.Ref}");
                Sb.AppendLine($"Qtd: {SelectedLine.Amount}");
                Sb.AppendLine($"Preço/U: {SelectedLine.Price}");
                Sb.AppendLine($"Data da Encomenda: {DateTimeOffset.FromUnixTimeSeconds(SelectedLine.OrderDate).ToString("MM/dd/yyyy")}");
                Sb.AppendLine($"Data de Entrega: {DateTimeOffset.FromUnixTimeSeconds(SelectedLine.LimitDate).ToString("MM/dd/yyyy")}");
                Sb.AppendLine($"Estado Material: {Helpers.EnumHelper.GetDescription(SelectedLine.MaterialState)}");
                Sb.AppendLine($"Estado: {Helpers.EnumHelper.GetDescription(SelectedLine.State)}");
                Sb.AppendLine($"Notas: {SelectedLine.Notes}\n");
            }
            Clipboard.SetText(Sb.ToString());
        }
        private async void MenuEdit_Click(object sender, RoutedEventArgs e)
        {
            DataEditor.IsEditMode = true;
            int CurrentItem = Database.OrderList.IndexOf((Types.OrderType)DataGrid.SelectedItem);
            DataEditor.PriceBox.Text = Database.OrderList[CurrentItem].Price;
            DataEditor.RefBox.Text = Database.OrderList[CurrentItem].Ref;
            DataEditor.AmountBox.Text = Database.OrderList[CurrentItem].Amount;
            DataEditor.PurchaseOrderBox.Text = Database.OrderList[CurrentItem].PurchaseOrder;
            DataEditor.ClientBox.Text = Database.OrderList[CurrentItem].Client;
            DataEditor.PurchaseOrderBox.Text = Database.OrderList[CurrentItem].PurchaseOrder;
            DataEditor.LimitDateDatePicker.SelectedDate = DateTimeOffset.FromUnixTimeSeconds(Database.OrderList[CurrentItem].LimitDate).Date;
            DataEditor.OrderDatePicker.SelectedDate = DateTimeOffset.FromUnixTimeSeconds(Database.OrderList[CurrentItem].OrderDate).Date;
            DataEditor.NotesBox.Text = Database.OrderList[CurrentItem].Notes;
            DataEditor.StateComboBox.SelectedIndex = (int)Database.OrderList[CurrentItem].State;
            DataEditor.MaterialStateComboBox.SelectedIndex = (int)Database.OrderList[CurrentItem].MaterialState;
            ContentDialogResult Result = await DataEditor.ShowAsync();
            if (Result == ContentDialogResult.Primary)
            {
                Database.OrderList[CurrentItem].Client = DataEditor.ClientBox.Text;
                Database.OrderList[CurrentItem].Price = DataEditor.PriceBox.Text;
                Database.OrderList[CurrentItem].Amount = DataEditor.AmountBox.Text;
                Database.OrderList[CurrentItem].Ref = DataEditor.RefBox.Text;
                Database.OrderList[CurrentItem].PurchaseOrder = DataEditor.PurchaseOrderBox.Text;
                Database.OrderList[CurrentItem].State = (Types.StateType)DataEditor.StateComboBox.SelectedIndex;
                Database.OrderList[CurrentItem].MaterialState = (Types.MaterialStateType)DataEditor.MaterialStateComboBox.SelectedIndex;
                Database.OrderList[CurrentItem].LimitDate = ((DateTimeOffset)DataEditor.LimitDateDatePicker.SelectedDate.Value).ToUnixTimeSeconds();
                Database.OrderList[CurrentItem].OrderDate = ((DateTimeOffset)DataEditor.OrderDatePicker.SelectedDate.Value).ToUnixTimeSeconds();
                Database.OrderList[CurrentItem].Notes = DataEditor.NotesBox.Text;
                if (!Database.ClientList.Contains(DataEditor.ClientBox.Text)) Database.ClientList.Add(DataEditor.ClientBox.Text);
                Database.SaveDb();
                DataGrid.Items.Refresh();
            }
        }
        private void MenuDelete_Click(object sender, RoutedEventArgs e)
        {
            foreach (Types.OrderType Order in DataGrid.SelectedItems)
            {
                Database.OrderList.Remove(Order);
            }
            DataGrid.Items.Refresh();
            Database.SaveDb();
        }
        #endregion

        private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            
            // e.Row.Background = new SolidColorBrush(Color.FromRgb(255,0,0));
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
