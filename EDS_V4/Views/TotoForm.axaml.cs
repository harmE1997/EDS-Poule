using Avalonia.Controls;
using Avalonia.Interactivity;
using EDS_V4.Code;
using EDS_V4.ViewModels;
using System.Xml.Schema;

namespace EDS_V4.Views
{
    public partial class TotoForm : Window
    {
        private TotoFormVm viewmodel;

        public TotoForm()
        {
            InitializeComponent();
            viewmodel = new TotoFormVm(null);
            DataContext = viewmodel;
        }

        public TotoForm(Player activeplayer)
        {
            InitializeComponent();
            viewmodel = new TotoFormVm(activeplayer);
            DataContext = viewmodel;
        }

        public void SubmitCommand(object sender, RoutedEventArgs e)
        {
            Close(viewmodel.ActivePlayer);
        }
    }
}
