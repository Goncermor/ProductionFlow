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

namespace Order_Processor
{
    /// <summary>
    /// Lógica interna para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Database DB = new Database();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_ContentRendered(object sender, EventArgs e) => _ = LoadData();

        private async Task LoadData()
        {
            await Task.Delay(2000);
            LoadSplash.Visibility = Visibility.Hidden;
            DataGrid.Items.Add(DB.OrderList[0]);
          
          


        }
    }
}
