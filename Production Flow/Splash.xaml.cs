using System.Windows;
using System.Windows.Media.Animation;

namespace Production_Flow
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
            Storyboard? ShowSb = this.FindResource("Show") as Storyboard;
            ShowSb?.Begin();
            await Task.Delay(800);
            Storyboard? SignatureSb = this.FindResource("WriteSignature") as Storyboard;
            SignatureSb?.Begin();

            await Task.Delay(3000);
            Database.Load();
            this.Hide();
            Application.Current.Run(new MainWindow());
        }
    }
}
