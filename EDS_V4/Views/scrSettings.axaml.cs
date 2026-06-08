using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace EDS_V4.Views
{
    public partial class scrSettings : UserControl
    {
        public scrSettings()
        {
            InitializeComponent();
            DataContext = new ViewModels.SettingsVm();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
