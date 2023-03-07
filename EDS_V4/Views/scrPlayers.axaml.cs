using Avalonia.Controls;

namespace EDS_V4.Views
{
    public partial class scrPlayers : UserControl
    {
        public scrPlayers()
        {
            InitializeComponent();
            DataContext = new ViewModels.scrPlayersVm();
        }
    }
}
