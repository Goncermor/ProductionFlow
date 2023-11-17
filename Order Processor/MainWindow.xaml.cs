using System.Text;
using System.Windows;
using System.Windows.Controls;
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

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            DataEditor.IsEditMode = false;
            DataEditor.LimitDateDatePicker.SelectedDate = DateTime.Now;
            DataEditor.NameBox.Text = "";
            DataEditor.RefBox.Text = "";
            DataEditor.PurchaseOrderBox.Text = "";
            DataEditor.ClientBox.Text = "";
            DataEditor.PurchaseOrderBox.Text = "";
            DataEditor.NotesBox.Text = "";
            DataEditor.StateComboBox.SelectedIndex = (int)Types.StateType.Undefined;
            DataEditor.MaterialStateComboBox.SelectedIndex = (int)Types.MaterialStateType.Undefined;
            DataEditor.ShowAsync();
        }

        #endregion










        #region Context Menu
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
                Sb.AppendLine($"Data da encomenda: {SelectedLine.OrderDate.ToString("MM/dd/yyyy")}");
                Sb.AppendLine($"Data de Entrega: {SelectedLine.LimitDate.ToString("MM/dd/yyyy")}");
                Sb.AppendLine($"Estado: {Helpers.EnumHelper.GetDescription(SelectedLine.State)}");
                Sb.AppendLine($"Estado Material: {Helpers.EnumHelper.GetDescription(SelectedLine.MaterialState)}");
                Sb.AppendLine($"Notas: {SelectedLine.Notes}\n");
            }
            Clipboard.SetText(Sb.ToString());
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGrid.SelectedItems.Count == 1)
                MenuItem_Edit.IsEnabled = true;
            else
                MenuItem_Edit.IsEnabled = false;
        }

        private void MenuEdit_Click(object sender, RoutedEventArgs e)
        {
            DataEditor.IsEditMode = true;
            Types.OrderType CurrentItem = (Types.OrderType)DataGrid.SelectedItem;
            DataEditor.NameBox.Text = CurrentItem.Name;
            DataEditor.RefBox.Text = CurrentItem.Ref;
            DataEditor.PurchaseOrderBox.Text = CurrentItem.PurchaseOrder;
            DataEditor.ClientBox.Text = CurrentItem.Client;
            DataEditor.PurchaseOrderBox.Text = CurrentItem.PurchaseOrder;
            DataEditor.LimitDateDatePicker.SelectedDate = DateTimeOffset.FromUnixTimeSeconds(CurrentItem.LimitDate).Date;
            DataEditor.NotesBox.Text = CurrentItem.Notes;
            DataEditor.StateComboBox.SelectedIndex = (int)CurrentItem.State;
            DataEditor.MaterialStateComboBox.SelectedIndex = (int)CurrentItem.MaterialState;
            DataEditor.ShowAsync();
        }

        private void MenuDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

    }
}
