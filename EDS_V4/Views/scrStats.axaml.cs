using Avalonia.Controls;
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
    }
}
