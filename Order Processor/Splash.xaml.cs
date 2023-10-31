using System.Windows;

namespace Order_Processor
{
    public partial class Splash : Window
    {
        public Splash()
        {
            InitializeComponent();
        }

        private void Window_ContentRendered(object sender, EventArgs e) => _ = Wait();
        private async Task Wait()
        {
            await Task.Delay(000);
            this.Hide();
            Application.Current.Run(new MainWindow());
        }
    }
}
