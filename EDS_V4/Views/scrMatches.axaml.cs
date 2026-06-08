using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using EDS_V4.ViewModels;

namespace EDS_V4.Views
{
    public partial class scrMatches : UserControl
    {
        public scrMatches()
        {
            InitializeComponent();
            DataContext = new scrMatchesVm();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
