using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace EDS_V4.Views
{
    public partial class isoPopup : Window
    {
        public isoPopup()
        {
            InitializeComponent();
            Height = 200;
            PopupManager.MessageEvent += ShowMessage;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public void btnCloseClick(object sender, RoutedEventArgs e)
        {
            this.FindControl<TextBlock>("tbMessage").Text = "";
            this.Hide();
        }

        public void ShowMessage(string arg)
        {
            var tb = this.FindControl<TextBlock>("tbMessage");
            tb.Text = arg;
            Show();
        }
    }
}
