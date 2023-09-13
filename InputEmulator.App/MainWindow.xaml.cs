using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace InputEmulator.App
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // TestClick();
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            this.TextBlock.Text = $"{(string)((Button)sender).Content} clicked!";
        }

        public static async Task ApplicationIdle()
        {
            Thread.Sleep(500);

            await Application.Current.Dispatcher.InvokeAsync(() => { }, DispatcherPriority.ApplicationIdle);
        }

        private async Task TestClick()
        {
            Rect rect = new(this.Left, this.Top, this.RenderSize.Width, this.RenderSize.Height);

            Point point = new(rect.Left + 40, rect.Top + 40);

            await ApplicationIdle();

            Input.MoveCursor(point);

            await ApplicationIdle();

            Input.MouseLeftDown();

            await ApplicationIdle();

            Input.MouseLeftUp();

            Point point2 = new(rect.Left + 40, rect.Top + 60);

            Input.MoveCursor(point2);

            await ApplicationIdle();

            Input.MouseLeftDown();

            await ApplicationIdle();

            Input.MouseLeftUp();

            Point point3 = new(rect.Left + 40, rect.Top + 80);

            Input.MoveCursor(point3);

            await ApplicationIdle();

            Input.MouseLeftDown();

            await ApplicationIdle();

            Input.MouseLeftUp();
        }
    }
}
