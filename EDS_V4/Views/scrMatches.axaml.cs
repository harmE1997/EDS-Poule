using Avalonia.Controls;
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
    }
}
