using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Windows.UI.WebUI;

namespace Order_Processor
{
    public partial class MainWindow : Window
    {
        Database DB;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_ContentRendered(object sender, EventArgs e) => _ = LoadData();

        private async Task LoadData()
        {
            DB = new Database();
            await Task.Delay(2000);
            LoadSplash.Visibility = Visibility.Hidden;
            DataGrid.ItemsSource = DB.OrderList;




        }
    }
}
