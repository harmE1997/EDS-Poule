using Microsoft.CodeAnalysis.Operations;
using Avalonia;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Reactive;
using System.Reactive.Linq;
using EDS_V4.Code;
using EDS_V4.Views;
using Avalonia.Controls;

namespace EDS_V4.ViewModels
{
    public class TotoFormVm : ViewModelBase
    {
        public bool PredictionsSubmittedFlag = false;
        private Window totoformWindow;
        private Week activeWeek;
        private List<string> scoreProperties = new List<string>() { 
            "Score1A", "Score1B", "Score2A", "Score2B", "Score3A", "Score3B", "Score14A", "Score4B",
        "Score5A", "Score5B", "Score6A", "Score6B", "Score7A", "Score7B", "Score8A", "Score8B", "Score9A", "Score9B"};


        private int currentWeek;
        public int CurrentWeek { get => currentWeek; set { SetCurrentWeek(value); this.RaisePropertyChanged(); } }
        
        private Player activeplayer;
        public Player ActivePlayer { get => activeplayer; private set { activeplayer = value; CurrentWeek = 1; } }
        
        public string PlayerName { get => activeplayer.Name; set { ActivePlayer.Name = value; this.RaisePropertyChanged(); } }
        public string PlayerTown { get => ActivePlayer.Town;  set { ActivePlayer.Town = value; this.RaisePropertyChanged(); } }

        private string currentweektext;
        public string CurrentWeekText { get => currentweektext; set => this.RaiseAndSetIfChanged(ref currentweektext, value); }

        private string predictionsfilename;
        public string PredictionsFileName { get => predictionsfilename; set => this.RaiseAndSetIfChanged(ref predictionsfilename, value); }

        private int miss;
        public int Miss { get => miss; set => this.RaiseAndSetIfChanged(ref miss, value); }

        private bool firsthalf;
        public bool FirstHalf { get => firsthalf; set => this.RaiseAndSetIfChanged(ref firsthalf, value); }

        private bool secondhalf;
        public bool SecondHalf { get => secondhalf; set => this.RaiseAndSetIfChanged(ref secondhalf, value); }

        public int Score1A { get => activeWeek.Matches[0].ResultA; set { activeWeek.Matches[0].ResultA = value; this.RaisePropertyChanged(); } }
        public int Score1B { get => activeWeek.Matches[0].ResultB; set { activeWeek.Matches[0].ResultB = value; this.RaisePropertyChanged(); } }
        public int Score2A { get => activeWeek.Matches[1].ResultA; set { activeWeek.Matches[1].ResultA = value; this.RaisePropertyChanged(); } }
        public int Score2B { get => activeWeek.Matches[1].ResultB; set { activeWeek.Matches[1].ResultB = value; this.RaisePropertyChanged(); } }
        public int Score3A { get => activeWeek.Matches[2].ResultA; set { activeWeek.Matches[2].ResultA = value; this.RaisePropertyChanged(); } }
        public int Score3B { get => activeWeek.Matches[2].ResultB; set { activeWeek.Matches[2].ResultB = value; this.RaisePropertyChanged(); } }
        public int Score4A { get => activeWeek.Matches[3].ResultA; set { activeWeek.Matches[3].ResultA = value; this.RaisePropertyChanged(); } }
        public int Score4B { get => activeWeek.Matches[3].ResultB; set { activeWeek.Matches[3].ResultB = value; this.RaisePropertyChanged(); } }
        public int Score5A { get => activeWeek.Matches[4].ResultA; set { activeWeek.Matches[4].ResultA = value; this.RaisePropertyChanged(); } }
        public int Score5B { get => activeWeek.Matches[4].ResultB; set { activeWeek.Matches[4].ResultB = value; this.RaisePropertyChanged(); } }
        public int Score6A { get => activeWeek.Matches[5].ResultA; set { activeWeek.Matches[5].ResultA = value; this.RaisePropertyChanged(); } }
        public int Score6B { get => activeWeek.Matches[5].ResultB; set { activeWeek.Matches[5].ResultB = value; this.RaisePropertyChanged(); } }
        public int Score7A { get => activeWeek.Matches[6].ResultA; set { activeWeek.Matches[6].ResultA = value; this.RaisePropertyChanged(); } }
        public int Score7B { get => activeWeek.Matches[6].ResultB; set { activeWeek.Matches[6].ResultB = value; this.RaisePropertyChanged(); } }
        public int Score8A { get => activeWeek.Matches[7].ResultA; set { activeWeek.Matches[7].ResultA = value; this.RaisePropertyChanged(); } }
        public int Score8B { get => activeWeek.Matches[7].ResultB; set { activeWeek.Matches[7].ResultB = value; this.RaisePropertyChanged(); } }
        public int Score9A { get => activeWeek.Matches[8].ResultA; set { activeWeek.Matches[8].ResultA = value; this.RaisePropertyChanged(); } }
        public int Score9B { get => activeWeek.Matches[8].ResultB; set { activeWeek.Matches[8].ResultB = value; this.RaisePropertyChanged(); } }

        public string Champion { get => ActivePlayer.Questions.Answers[BonusKeys.Kampioen].Answer[0]; set {ActivePlayer.Questions.Answers[BonusKeys.Kampioen].Answer[0] = value.ToLower(); this.RaisePropertyChanged(); } }
        public string Nr16 { get => ActivePlayer.Questions.Answers[BonusKeys.Prodeg].Answer[0]; set { ActivePlayer.Questions.Answers[BonusKeys.Prodeg].Answer[0] = value.ToLower(); this.RaisePropertyChanged(); } }
        public string Topscorer { get => ActivePlayer.Questions.Answers[BonusKeys.Topscorer].Answer[0]; set { ActivePlayer.Questions.Answers[BonusKeys.Topscorer].Answer[0] = value.ToLower(); this.RaisePropertyChanged(); } }
        public string Trainer { get => ActivePlayer.Questions.Answers[BonusKeys.Trainer].Answer[0]; set { ActivePlayer.Questions.Answers[BonusKeys.Trainer].Answer[0] = value.ToLower(); this.RaisePropertyChanged(); } }
        public string WinterChampion { get => ActivePlayer.Questions.Answers[BonusKeys.Winterkampioen].Answer[0]; set { ActivePlayer.Questions.Answers[BonusKeys.Winterkampioen].Answer[0] = value.ToLower(); this.RaisePropertyChanged(); } }
        public string Ronde { get => ActivePlayer.Questions.Answers[BonusKeys.Ronde].Answer[0]; set { ActivePlayer.Questions.Answers[BonusKeys.Ronde].Answer[0] = value.ToLower(); this.RaisePropertyChanged(); } }
        public string TeamRood { get => ActivePlayer.Questions.Answers[BonusKeys.Teamrood].Answer[0]; set { ActivePlayer.Questions.Answers[BonusKeys.Teamrood].Answer[0] = value.ToLower(); this.RaisePropertyChanged(); } }
        public string[] Bekerfinalisten { 
            get => ActivePlayer.Questions.Answers[BonusKeys.Finalisten].Answer; 
            set { 
                ActivePlayer.Questions.Answers[BonusKeys.Finalisten].Answer[0] = value[0].ToLower(); 
                ActivePlayer.Questions.Answers[BonusKeys.Finalisten].Answer[1] = value[1].ToLower();
                this.RaisePropertyChanged();
            } }
        public string[] Degradanten { 
            get => ActivePlayer.Questions.Answers[BonusKeys.Degradanten].Answer; 
            set { 
                ActivePlayer.Questions.Answers[BonusKeys.Degradanten].Answer[0] = value[0].ToLower();
                ActivePlayer.Questions.Answers[BonusKeys.Degradanten].Answer[1] = value[1].ToLower(); 
                this.RaisePropertyChanged();
            } }
        public string[] Promovendi { 
            get => ActivePlayer.Questions.Answers[BonusKeys.Promovendi].Answer; 
            set { 
                ActivePlayer.Questions.Answers[BonusKeys.Promovendi].Answer[0] = value[0].ToLower();
                ActivePlayer.Questions.Answers[BonusKeys.Promovendi].Answer[1] = value[1].ToLower();
                this.RaisePropertyChanged();
            } }

        public ReactiveCommand<Unit,Unit> NextWeekCommand { get; set; }
        public ReactiveCommand<Unit,Unit> PreviousWeekCommand { get; set; }

        public TotoFormVm(Player activeplayer, Window totoformwindow)
        {
            if (activeplayer == null)
                this.activeplayer = ActivePlayer = CreateDefaultActivePlayer();
            else
                this.activeplayer = ActivePlayer = activeplayer;
            CurrentWeek = 1;        

            var NextWeekCommandCanExecute = this.WhenAnyValue(
                x => x.CurrentWeek,
                (a) => { return a < 34; }).ObserveOn(RxApp.MainThreadScheduler);        

            var PreviousWeekCommandCanExecute = this.WhenAnyValue(
                x => x.CurrentWeek,
                (a) => { return a > 1; }).ObserveOn(RxApp.MainThreadScheduler);

            NextWeekCommand = ReactiveCommand.Create(() => { this.ChangeWeek(1); }, NextWeekCommandCanExecute);
            PreviousWeekCommand = ReactiveCommand.Create(() => { this.ChangeWeek(-1); }, PreviousWeekCommandCanExecute);
            Miss = 0;
            FirstHalf = false;
            SecondHalf = false;
            predictionsfilename = "";
            PredictionsSubmittedFlag = false;
            totoformWindow = totoformwindow;
        }

        public void SubmitCommand()
        {
            bool invalidpredictions = false;
            foreach (var ans in ActivePlayer.Questions.Answers)
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
                PredictionsSubmittedFlag = true;
                totoformWindow.Close();
            }
        }

        public void ReadPredictionsFromExcel()
        {
            Excel.ExcelManager em = new Excel.ExcelManager();
            if (!FirstHalf || !SecondHalf)
            {
                ActivePlayer.Weeks = em.ReadPredictions(PredictionsFileName, 1, Miss,FirstHalf, SecondHalf, ActivePlayer.Weeks);
                CurrentWeek = 1;
                PopupManager.OnMessage("Predictions read");
            }
        }

        private void ChangeWeek(int change)
        {
            CurrentWeek += change;
        }

        private Player CreateDefaultActivePlayer()
        {
            var weeks = new Dictionary<int, Week>();
            for (int i = 1; i<= 34; i++)
            {
                var matches = new Match[9];
                for(int x = 0; x < 9; x++)
                {
                    matches[x] = new Match(99, 99, x==8);             
                }

                weeks.Add(i,new Week(i, matches));
            }
            return new Player("", "", weeks, new BonusQuestions(new string[13], new int[13]));
        }

        private void SetCurrentWeek(int value)
        {
            currentWeek = value;
            CurrentWeekText = "Week " + value;
            activeWeek = ActivePlayer.Weeks[value];
            foreach (var prop in scoreProperties)
                this.RaisePropertyChanged(prop);
        }
    }
}
