using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace EDS_V4.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            isoPopup popup = new isoPopup();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}