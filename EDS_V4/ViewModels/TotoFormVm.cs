﻿using Microsoft.CodeAnalysis.Operations;
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

namespace EDS_V4.ViewModels
{
    public class TotoFormVm : ViewModelBase
    {
        public bool PredictionsSubmittedFlag = false;

        private int currentWeek;
        public int CurrentWeek { get => currentWeek; set { this.RaiseAndSetIfChanged(ref currentWeek, value); CurrentWeekText = "Week " + value; SetWeek(); } }
        
        private Player activeplayer;
        public Player ActivePlayer { get { return activeplayer; } private set { activeplayer = value; CurrentWeek = 1; } }
        
        public string PlayerName { get { return ActivePlayer.Name; } set { this.RaiseAndSetIfChanged(ref ActivePlayer.Name, value); this.RaisePropertyChanged(); } }
        public string PlayerTown { get { return ActivePlayer.Town; } set { this.RaiseAndSetIfChanged(ref ActivePlayer.Town, value); this.RaisePropertyChanged(); } }

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

        public int Score1A { get => ActivePlayer.Weeks[CurrentWeek].Matches[0].ResultA; set { ActivePlayer.Weeks[CurrentWeek].Matches[0].ResultA = value; this.RaisePropertyChanged(); } }
        public int Score1B { get => ActivePlayer.Weeks[CurrentWeek].Matches[0].ResultB; set { ActivePlayer.Weeks[CurrentWeek].Matches[0].ResultB = value; this.RaisePropertyChanged(); } }
        public int Score2A { get => ActivePlayer.Weeks[CurrentWeek].Matches[1].ResultA; set { ActivePlayer.Weeks[CurrentWeek].Matches[1].ResultA = value; this.RaisePropertyChanged(); } }
        public int Score2B { get => ActivePlayer.Weeks[CurrentWeek].Matches[1].ResultB; set { ActivePlayer.Weeks[CurrentWeek].Matches[1].ResultB = value; this.RaisePropertyChanged(); } }
        public int Score3A { get => ActivePlayer.Weeks[CurrentWeek].Matches[2].ResultA; set { ActivePlayer.Weeks[CurrentWeek].Matches[2].ResultA = value; this.RaisePropertyChanged(); } }
        public int Score3B { get => ActivePlayer.Weeks[CurrentWeek].Matches[2].ResultB; set { ActivePlayer.Weeks[CurrentWeek].Matches[2].ResultB = value; this.RaisePropertyChanged(); } }
        public int Score4A { get => ActivePlayer.Weeks[CurrentWeek].Matches[3].ResultA; set { ActivePlayer.Weeks[CurrentWeek].Matches[3].ResultA = value; this.RaisePropertyChanged(); } }
        public int Score4B { get => ActivePlayer.Weeks[CurrentWeek].Matches[3].ResultB; set { ActivePlayer.Weeks[CurrentWeek].Matches[3].ResultB = value; this.RaisePropertyChanged(); } }
        public int Score5A { get => ActivePlayer.Weeks[CurrentWeek].Matches[4].ResultA; set { ActivePlayer.Weeks[CurrentWeek].Matches[4].ResultA = value; this.RaisePropertyChanged(); } }
        public int Score5B { get => ActivePlayer.Weeks[CurrentWeek].Matches[4].ResultB; set { ActivePlayer.Weeks[CurrentWeek].Matches[4].ResultB = value; this.RaisePropertyChanged(); } }
        public int Score6A { get => ActivePlayer.Weeks[CurrentWeek].Matches[5].ResultA; set { ActivePlayer.Weeks[CurrentWeek].Matches[5].ResultA = value; this.RaisePropertyChanged(); } }
        public int Score6B { get => ActivePlayer.Weeks[CurrentWeek].Matches[5].ResultB; set { ActivePlayer.Weeks[CurrentWeek].Matches[5].ResultB = value; this.RaisePropertyChanged(); } }
        public int Score7A { get => ActivePlayer.Weeks[CurrentWeek].Matches[6].ResultA; set { ActivePlayer.Weeks[CurrentWeek].Matches[6].ResultA = value; this.RaisePropertyChanged(); } }
        public int Score7B { get => ActivePlayer.Weeks[CurrentWeek].Matches[6].ResultB; set { ActivePlayer.Weeks[CurrentWeek].Matches[6].ResultB = value; this.RaisePropertyChanged(); } }
        public int Score8A { get => ActivePlayer.Weeks[CurrentWeek].Matches[7].ResultA; set { ActivePlayer.Weeks[CurrentWeek].Matches[7].ResultA = value; this.RaisePropertyChanged(); } }
        public int Score8B { get => ActivePlayer.Weeks[CurrentWeek].Matches[7].ResultB; set { ActivePlayer.Weeks[CurrentWeek].Matches[7].ResultB = value; this.RaisePropertyChanged(); } }
        public int Score9A { get => ActivePlayer.Weeks[CurrentWeek].Matches[8].ResultA; set { ActivePlayer.Weeks[CurrentWeek].Matches[8].ResultA = value; this.RaisePropertyChanged(); } }
        public int Score9B { get => ActivePlayer.Weeks[CurrentWeek].Matches[8].ResultB; set { ActivePlayer.Weeks[CurrentWeek].Matches[8].ResultB = value; this.RaisePropertyChanged(); } }

        public string Champion { get => ActivePlayer.Questions.Answers[BonusKeys.Kampioen].Answer[0]; set { this.RaiseAndSetIfChanged(ref ActivePlayer.Questions.Answers[BonusKeys.Kampioen].Answer[0], value.ToLower()); this.RaisePropertyChanged(); } }
        public string Nr16 { get => ActivePlayer.Questions.Answers[BonusKeys.Prodeg].Answer[0]; set { this.RaiseAndSetIfChanged(ref ActivePlayer.Questions.Answers[BonusKeys.Prodeg].Answer[0], value.ToLower()); this.RaisePropertyChanged(); } }
        public string Topscorer { get => ActivePlayer.Questions.Answers[BonusKeys.Topscorer].Answer[0]; set { this.RaiseAndSetIfChanged(ref ActivePlayer.Questions.Answers[BonusKeys.Topscorer].Answer[0], value.ToLower()); this.RaisePropertyChanged(); } }
        public string Trainer { get => ActivePlayer.Questions.Answers[BonusKeys.Trainer].Answer[0]; set { this.RaiseAndSetIfChanged(ref ActivePlayer.Questions.Answers[BonusKeys.Trainer].Answer[0], value.ToLower()); this.RaisePropertyChanged(); } }
        public string WinterChampion { get => ActivePlayer.Questions.Answers[BonusKeys.Winterkampioen].Answer[0]; set { this.RaiseAndSetIfChanged(ref ActivePlayer.Questions.Answers[BonusKeys.Winterkampioen].Answer[0], value.ToLower()); this.RaisePropertyChanged(); } }
        public string Ronde { get => ActivePlayer.Questions.Answers[BonusKeys.Ronde].Answer[0]; set { this.RaiseAndSetIfChanged(ref ActivePlayer.Questions.Answers[BonusKeys.Ronde].Answer[0], value.ToLower()); this.RaisePropertyChanged(); } }
        public string TeamRood { get => ActivePlayer.Questions.Answers[BonusKeys.Teamrood].Answer[0]; set { this.RaiseAndSetIfChanged(ref ActivePlayer.Questions.Answers[BonusKeys.Teamrood].Answer[0], value.ToLower()); this.RaisePropertyChanged(); } }
        public string[] Bekerfinalisten { 
            get => ActivePlayer.Questions.Answers[BonusKeys.Finalisten].Answer; 
            set { 
                this.RaiseAndSetIfChanged(ref ActivePlayer.Questions.Answers[BonusKeys.Finalisten].Answer[0], value[0].ToLower()); 
                this.RaiseAndSetIfChanged(ref ActivePlayer.Questions.Answers[BonusKeys.Finalisten].Answer[1], value[1].ToLower()); 
                this.WhenAnyValue(x => x.ActivePlayer).Subscribe(a => this.RaisePropertyChanged()); } }
        public string[] Degradanten { 
            get => ActivePlayer.Questions.Answers[BonusKeys.Degradanten].Answer; 
            set { 
                this.RaiseAndSetIfChanged(ref ActivePlayer.Questions.Answers[BonusKeys.Degradanten].Answer[0], value[0].ToLower());
                this.RaiseAndSetIfChanged(ref ActivePlayer.Questions.Answers[BonusKeys.Degradanten].Answer[1], value[1].ToLower());
                this.WhenAnyValue(x => x.ActivePlayer).Subscribe(a => this.RaisePropertyChanged()); } }
        public string[] Promovendi { 
            get => ActivePlayer.Questions.Answers[BonusKeys.Promovendi].Answer; 
            set { 
                this.RaiseAndSetIfChanged(ref ActivePlayer.Questions.Answers[BonusKeys.Promovendi].Answer[0], value[0].ToLower());
                this.RaiseAndSetIfChanged(ref ActivePlayer.Questions.Answers[BonusKeys.Promovendi].Answer[1], value[1].ToLower());
                this.WhenAnyValue(x => x.ActivePlayer).Subscribe(a => this.RaisePropertyChanged()); } }

        public ReactiveCommand<Unit,Unit> NextWeekCommand { get; set; }
        public ReactiveCommand<Unit, Unit> PreviousWeekCommand { get; set; }

        public TotoFormVm(Player activeplayer)
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
            return new Player("", "", weeks, new BonusQuestions("", "", "", "", "", "", "", new string[2], new string[2], new string[2], new int[13]));
        }

        private void SetWeek()
        {
            Score1A = ActivePlayer.Weeks[CurrentWeek].Matches[0].ResultA;
            Score1B = ActivePlayer.Weeks[CurrentWeek].Matches[0].ResultB;
            Score2A = ActivePlayer.Weeks[CurrentWeek].Matches[1].ResultA;
            Score2B = ActivePlayer.Weeks[CurrentWeek].Matches[1].ResultB;
            Score3A = ActivePlayer.Weeks[CurrentWeek].Matches[2].ResultA;
            Score3B = ActivePlayer.Weeks[CurrentWeek].Matches[2].ResultB;
            Score4A = ActivePlayer.Weeks[CurrentWeek].Matches[3].ResultA;
            Score4B = ActivePlayer.Weeks[CurrentWeek].Matches[3].ResultB;
            Score5A = ActivePlayer.Weeks[CurrentWeek].Matches[4].ResultA;
            Score5B = ActivePlayer.Weeks[CurrentWeek].Matches[4].ResultB;
            Score6A = ActivePlayer.Weeks[CurrentWeek].Matches[5].ResultA;
            Score6B = ActivePlayer.Weeks[CurrentWeek].Matches[5].ResultB;
            Score7A = ActivePlayer.Weeks[CurrentWeek].Matches[6].ResultA;
            Score7B = ActivePlayer.Weeks[CurrentWeek].Matches[6].ResultB;
            Score8A = ActivePlayer.Weeks[CurrentWeek].Matches[7].ResultA;
            Score8B = ActivePlayer.Weeks[CurrentWeek].Matches[7].ResultB;
            Score9A = ActivePlayer.Weeks[CurrentWeek].Matches[8].ResultA;
            Score9B = ActivePlayer.Weeks[CurrentWeek].Matches[8].ResultB;
        }
    }
}
