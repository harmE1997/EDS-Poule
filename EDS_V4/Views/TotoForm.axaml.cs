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
            bool invalidpredictions = false;
            foreach (var ans in viewmodel.ActivePlayer.Questions.Answers)
            {
                foreach (var ansfield in ans.Value.Answer)
                {
                    if (ansfield == "")
                        invalidpredictions = true;
                }                  
            }

            if (invalidpredictions)
                PopupManager.OnMessage("cannot submit predictions. One or more bonusquestions have not been filled in.");

            else
            {
                viewmodel.PredictionsSubmittedFlag = true;
                Close();
            }
        }
    }
}
