using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace EDS_V4.Views
{
    public partial class SideMenu : UserControl
    {
        public SideMenu()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
