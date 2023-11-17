using System.Windows;
using System.Windows.Media.Animation;

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
            await Task.Delay(500);
            Storyboard? Sb = this.FindResource("WriteSignature") as Storyboard;
            Sb?.Begin();

            await Task.Delay(3000);
            Database.Load();
            this.Hide();
            Application.Current.Run(new MainWindow());
        }
    }
}
