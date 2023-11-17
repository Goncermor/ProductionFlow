using ModernWpf.Controls;
using Order_Processor.Types;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Order_Processor
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
            DataEditor.LimitDateDatePicker.SelectedDate = DateTime.Now;
            DataEditor.NameBox.Text = "";
            DataEditor.RefBox.Text = "";
            DataEditor.PurchaseOrderBox.Text = "";
            DataEditor.ClientBox.Text = "";
            DataEditor.PurchaseOrderBox.Text = "";
            DataEditor.NotesBox.Text = "";
            DataEditor.StateComboBox.SelectedIndex = -1;
            DataEditor.MaterialStateComboBox.SelectedIndex = -1;
            ContentDialogResult Result = await DataEditor.ShowAsync();
            if (Result == ContentDialogResult.Primary)
            {
                OrderType NewOrder = new OrderType() {
                    Client = DataEditor.ClientBox.Text,
                    Name = DataEditor.NameBox.Text,
                    Ref = DataEditor.RefBox.Text,
                    PurchaseOrder = DataEditor.PurchaseOrderBox.Text,
                    State = (Types.StateType)DataEditor.StateComboBox.SelectedIndex,
                    MaterialState = (Types.MaterialStateType)DataEditor.MaterialStateComboBox.SelectedIndex,
                    LimitDate = ((DateTimeOffset)DataEditor.LimitDateDatePicker.SelectedDate.Value).ToUnixTimeSeconds(),
                    OrderDate = DateTimeOffset.Now.ToUnixTimeSeconds(),
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
                Sb.AppendLine($"Nome: {SelectedLine.Name}");
                Sb.AppendLine($"Ref: {SelectedLine.Ref}");
                Sb.AppendLine($"PO: {SelectedLine.PurchaseOrder}");
                Sb.AppendLine($"Cliente: {SelectedLine.Client}");
                Sb.AppendLine($"Data da encomenda: {DateTimeOffset.FromUnixTimeSeconds(SelectedLine.OrderDate).ToString("MM/dd/yyyy")}");
                Sb.AppendLine($"Data de Entrega: {DateTimeOffset.FromUnixTimeSeconds(SelectedLine.LimitDate).ToString("MM/dd/yyyy")}");
                Sb.AppendLine($"Estado: {Helpers.EnumHelper.GetDescription(SelectedLine.State)}");
                Sb.AppendLine($"Estado Material: {Helpers.EnumHelper.GetDescription(SelectedLine.MaterialState)}");
                Sb.AppendLine($"Notas: {SelectedLine.Notes}\n");
            }
            Clipboard.SetText(Sb.ToString());
        }
        private async void MenuEdit_Click(object sender, RoutedEventArgs e)
        {
            DataEditor.IsEditMode = true;
            int CurrentItem = Database.OrderList.IndexOf((Types.OrderType)DataGrid.SelectedItem);
            DataEditor.NameBox.Text = Database.OrderList[CurrentItem].Name;
            DataEditor.RefBox.Text = Database.OrderList[CurrentItem].Ref;
            DataEditor.PurchaseOrderBox.Text = Database.OrderList[CurrentItem].PurchaseOrder;
            DataEditor.ClientBox.Text = Database.OrderList[CurrentItem].Client;
            DataEditor.PurchaseOrderBox.Text = Database.OrderList[CurrentItem].PurchaseOrder;
            DataEditor.LimitDateDatePicker.SelectedDate = DateTimeOffset.FromUnixTimeSeconds(Database.OrderList[CurrentItem].LimitDate).Date;
            DataEditor.NotesBox.Text = Database.OrderList[CurrentItem].Notes;
            DataEditor.StateComboBox.SelectedIndex = (int)Database.OrderList[CurrentItem].State;
            DataEditor.MaterialStateComboBox.SelectedIndex = (int)Database.OrderList[CurrentItem].MaterialState;
            ContentDialogResult Result = await DataEditor.ShowAsync();
            if (Result == ContentDialogResult.Primary)
            {
                Database.OrderList[CurrentItem].Client = DataEditor.ClientBox.Text;
                Database.OrderList[CurrentItem].Name = DataEditor.NameBox.Text;
                Database.OrderList[CurrentItem].Ref = DataEditor.RefBox.Text;
                Database.OrderList[CurrentItem].PurchaseOrder = DataEditor.PurchaseOrderBox.Text;
                Database.OrderList[CurrentItem].State = (Types.StateType)DataEditor.StateComboBox.SelectedIndex;
                Database.OrderList[CurrentItem].MaterialState = (Types.MaterialStateType)DataEditor.MaterialStateComboBox.SelectedIndex;
                Database.OrderList[CurrentItem].LimitDate = ((DateTimeOffset)DataEditor.LimitDateDatePicker.SelectedDate.Value).ToUnixTimeSeconds();
                Database.OrderList[CurrentItem].OrderDate = DateTimeOffset.Now.ToUnixTimeSeconds();
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
    }
}
