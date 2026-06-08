using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using EDS_V4.Code;
using EDS_V4.ViewModels;

namespace EDS_V4.Views
{
    public partial class TotoForm : Window
    {
        private TotoFormVm viewmodel;

        public TotoForm()
        {
            InitializeComponent();
            viewmodel = new TotoFormVm(null, this);
            DataContext = viewmodel;
        }

        public TotoForm(Player activeplayer)
        {
            InitializeComponent();
            viewmodel = new TotoFormVm(activeplayer, this);
            DataContext = viewmodel;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
