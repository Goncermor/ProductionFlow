using ModernWpf.Controls;
using Production_Flow.Types;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Production_Flow
{
    public partial class MainWindow : Window
    {
        public ICollectionView CollectionView { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            CollectionView = CollectionViewSource.GetDefaultView(Database.OrderList);
            CollectionView.Filter = Object =>
             {
                 if (ShowSent.IsChecked == false) // == false and not !... because IsChecked is nullbale bool
                 {
                     Types.OrderType Item = (Types.OrderType)Object;
                     if (Item.State == StateType.Sent) return false;
                     else return true;
                 }
                 else return true;
             };
        }

        private void Window_ContentRendered(object sender, EventArgs e) => _ = LoadData();

        #region Database Manager

        private async Task LoadData()
        {
            await Task.Delay(1000);
            LoadSplash.Visibility = Visibility.Hidden;

            DataGrid.ItemsSource = CollectionView;
            DataEditor.ClientList = Database.ClientList;
        }

        private async void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            DataEditor.IsEditMode = false;
            DataEditor.ClientBox.Text = "";
            DataEditor.PurchaseOrderBox.Text = "";
            DataEditor.RefBox.Text = "";
            DataEditor.NotesBox.Text = "";
            DataEditor.StateComboBox.SelectedIndex = -1;

            ContentDialogResult Result = await DataEditor.ShowAsync();
            if (Result == ContentDialogResult.Primary)
            {
                OrderType NewOrder = new OrderType()
                {
                    Client = DataEditor.ClientBox.Text,
                    PurchaseOrder = DataEditor.PurchaseOrderBox.Text,
                    Ref = DataEditor.RefBox.Text,
                    OrderDate = ((DateTimeOffset)DataEditor.OrderDatePicker.SelectedDate.Value).ToUnixTimeSeconds(),
                    LimitDate = ((DateTimeOffset)DataEditor.LimitDateDatePicker.SelectedDate.Value).ToUnixTimeSeconds(),
                    State = (Types.StateType)DataEditor.StateComboBox.SelectedIndex,
                    Notes = DataEditor.NotesBox.Text
                };
                Database.OrderList.Add(NewOrder);
                if (!Database.ClientList.Contains(DataEditor.ClientBox.Text)) Database.ClientList.Add(DataEditor.ClientBox.Text);
                Database.SaveDb();
                CollectionView.Refresh();
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
            string CopyData = "";
            foreach (Types.OrderType SelectedItem in DataGrid.SelectedItems)
            {
                CopyData += SelectedItem.Client + "\t";
                CopyData += SelectedItem.PurchaseOrder + "\t";
                CopyData += SelectedItem.Ref + "\t";
                CopyData += DateTimeOffset.FromUnixTimeSeconds(SelectedItem.OrderDate).ToString("MM/dd/yyyy") + "\t";
                CopyData += DateTimeOffset.FromUnixTimeSeconds(SelectedItem.LimitDate).ToString("MM/dd/yyyy") + "\t";
                CopyData += Helpers.EnumHelper.GetDescription(SelectedItem.State) + "\t";
                // Operation
                CopyData += $"\"{SelectedItem.Notes}\"";
            }
            Clipboard.SetText(CopyData);
        }
        private async void MenuEdit_Click(object sender, RoutedEventArgs e)
        {
            DataEditor.IsEditMode = true;
            int CurrentItem = Database.OrderList.IndexOf((Types.OrderType)DataGrid.SelectedItem);
            DataEditor.ClientBox.Text = Database.OrderList[CurrentItem].Client;
            DataEditor.PurchaseOrderBox.Text = Database.OrderList[CurrentItem].PurchaseOrder;
            DataEditor.RefBox.Text = Database.OrderList[CurrentItem].Ref;
            DataEditor.OrderDatePicker.SelectedDate = DateTimeOffset.FromUnixTimeSeconds(Database.OrderList[CurrentItem].OrderDate).Date;
            DataEditor.LimitDateDatePicker.SelectedDate = DateTimeOffset.FromUnixTimeSeconds(Database.OrderList[CurrentItem].LimitDate).Date;
            DataEditor.NotesBox.Text = Database.OrderList[CurrentItem].Notes;
            DataEditor.StateComboBox.SelectedIndex = (int)Database.OrderList[CurrentItem].State;
            ContentDialogResult Result = await DataEditor.ShowAsync();
            if (Result == ContentDialogResult.Primary)
            {
                Database.OrderList[CurrentItem].Client = DataEditor.ClientBox.Text;
                Database.OrderList[CurrentItem].Ref = DataEditor.RefBox.Text;
                Database.OrderList[CurrentItem].PurchaseOrder = DataEditor.PurchaseOrderBox.Text;
                Database.OrderList[CurrentItem].State = (Types.StateType)DataEditor.StateComboBox.SelectedIndex;
                Database.OrderList[CurrentItem].LimitDate = ((DateTimeOffset)DataEditor.LimitDateDatePicker.SelectedDate.Value).ToUnixTimeSeconds();
                Database.OrderList[CurrentItem].OrderDate = ((DateTimeOffset)DataEditor.OrderDatePicker.SelectedDate.Value).ToUnixTimeSeconds();
                Database.OrderList[CurrentItem].Notes = DataEditor.NotesBox.Text;
                if (!Database.ClientList.Contains(DataEditor.ClientBox.Text)) Database.ClientList.Add(DataEditor.ClientBox.Text);
                Database.SaveDb();
                CollectionView.Refresh();
            }
        }
        private void MenuDelete_Click(object sender, RoutedEventArgs e)
        {
            foreach (Types.OrderType Order in DataGrid.SelectedItems)
            {
                Database.OrderList.Remove(Order);
            }
            CollectionView.Refresh();
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

        private void ShowSent_Click(object sender, RoutedEventArgs e)
        {
            CollectionView.Refresh();
        }
    }
}
