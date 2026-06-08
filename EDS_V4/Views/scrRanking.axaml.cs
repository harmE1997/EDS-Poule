using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using EDS_V4.ViewModels;

namespace EDS_V4.Views
{
    public partial class scrRanking : UserControl
    {
        public scrRanking()
        {
            InitializeComponent();
            DataContext = new scrRankingVm();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
