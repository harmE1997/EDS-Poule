using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using EDS_V4.ViewModels;

namespace EDS_V4.Views
{
    public partial class scrStats : UserControl
    {
        public scrStats()
        {
            InitializeComponent();
            DataContext = new scrStatsVm();
        }
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
