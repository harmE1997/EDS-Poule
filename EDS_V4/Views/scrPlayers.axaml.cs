using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace EDS_V4.Views
{
    public partial class scrPlayers : UserControl
    {
        public scrPlayers()
        {
            InitializeComponent();
            DataContext = new ViewModels.scrPlayersVm();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
