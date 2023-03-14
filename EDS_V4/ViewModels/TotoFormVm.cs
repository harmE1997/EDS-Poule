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

namespace EDS_V4.ViewModels
{
    public class TotoFormVm : ViewModelBase
    { 
        public Player ActivePlayer { get; private set; }
        private int currentWeek;

        public string PlayerName { get => ActivePlayer.Name; set => this.RaiseAndSetIfChanged(ref ActivePlayer.Name, value); }
        public string PlayerTown { get => ActivePlayer.Town; set => this.RaiseAndSetIfChanged(ref ActivePlayer.Town, value); }
        public string CurrentWeekText { get => "Week " + currentWeek; set => this.WhenAnyValue(x => x.currentWeek).Subscribe(a => this.RaisePropertyChanged()); }

        public int Score1A { get => ActivePlayer.Weeks[currentWeek - 1].Matches[0].ResultA; set { ActivePlayer.Weeks[currentWeek - 1].Matches[0].ResultA = value; this.WhenAnyValue(x => x.currentWeek).Subscribe(a => this.RaisePropertyChanged()); } }
        public int Score1B { get => ActivePlayer.Weeks[currentWeek - 1].Matches[0].ResultB; set { ActivePlayer.Weeks[currentWeek - 1].Matches[0].ResultB = value; this.WhenAnyValue(x => x.currentWeek).Subscribe(a => this.RaisePropertyChanged()); } }
        public int Score2A { get => ActivePlayer.Weeks[currentWeek - 1].Matches[1].ResultA; set { ActivePlayer.Weeks[currentWeek - 1].Matches[1].ResultA = value; this.WhenAnyValue(x => x.currentWeek).Subscribe(a => this.RaisePropertyChanged()); } }
        public int Score2B { get => ActivePlayer.Weeks[currentWeek - 1].Matches[1].ResultB; set { ActivePlayer.Weeks[currentWeek - 1].Matches[1].ResultB = value; this.WhenAnyValue(x => x.currentWeek).Subscribe(a => this.RaisePropertyChanged()); } }
        public int Score3A { get => ActivePlayer.Weeks[currentWeek - 1].Matches[2].ResultA; set { ActivePlayer.Weeks[currentWeek - 1].Matches[2].ResultA = value; this.WhenAnyValue(x => x.currentWeek).Subscribe(a => this.RaisePropertyChanged()); } }
        public int Score3B { get => ActivePlayer.Weeks[currentWeek - 1].Matches[2].ResultB; set { ActivePlayer.Weeks[currentWeek - 1].Matches[2].ResultB = value; this.WhenAnyValue(x => x.currentWeek).Subscribe(a => this.RaisePropertyChanged()); } }
        public int Score4A { get => ActivePlayer.Weeks[currentWeek - 1].Matches[3].ResultA; set { ActivePlayer.Weeks[currentWeek - 1].Matches[3].ResultA = value; this.WhenAnyValue(x => x.currentWeek).Subscribe(a => this.RaisePropertyChanged()); } }
        public int Score4B { get => ActivePlayer.Weeks[currentWeek - 1].Matches[3].ResultB; set { ActivePlayer.Weeks[currentWeek - 1].Matches[3].ResultB = value; this.WhenAnyValue(x => x.currentWeek).Subscribe(a => this.RaisePropertyChanged()); } }
        public int Score5A { get => ActivePlayer.Weeks[currentWeek - 1].Matches[4].ResultA; set { ActivePlayer.Weeks[currentWeek - 1].Matches[4].ResultA = value; this.WhenAnyValue(x => x.currentWeek).Subscribe(a => this.RaisePropertyChanged()); } }
        public int Score5B { get => ActivePlayer.Weeks[currentWeek - 1].Matches[4].ResultB; set { ActivePlayer.Weeks[currentWeek - 1].Matches[4].ResultB = value; this.WhenAnyValue(x => x.currentWeek).Subscribe(a => this.RaisePropertyChanged()); } }
        public int Score6A { get => ActivePlayer.Weeks[currentWeek - 1].Matches[5].ResultA; set { ActivePlayer.Weeks[currentWeek - 1].Matches[5].ResultA = value; this.WhenAnyValue(x => x.currentWeek).Subscribe(a => this.RaisePropertyChanged()); } }
        public int Score6B { get => ActivePlayer.Weeks[currentWeek - 1].Matches[5].ResultB; set { ActivePlayer.Weeks[currentWeek - 1].Matches[5].ResultB = value; this.WhenAnyValue(x => x.currentWeek).Subscribe(a => this.RaisePropertyChanged()); } }
        public int Score7A { get => ActivePlayer.Weeks[currentWeek - 1].Matches[6].ResultA; set { ActivePlayer.Weeks[currentWeek - 1].Matches[6].ResultA = value; this.WhenAnyValue(x => x.currentWeek).Subscribe(a => this.RaisePropertyChanged()); } }
        public int Score7B { get => ActivePlayer.Weeks[currentWeek - 1].Matches[6].ResultB; set { ActivePlayer.Weeks[currentWeek - 1].Matches[6].ResultB = value; this.WhenAnyValue(x => x.currentWeek).Subscribe(a => this.RaisePropertyChanged()); } }
        public int Score8A { get => ActivePlayer.Weeks[currentWeek - 1].Matches[7].ResultA; set { ActivePlayer.Weeks[currentWeek - 1].Matches[7].ResultA = value; this.WhenAnyValue(x => x.currentWeek).Subscribe(a => this.RaisePropertyChanged()); } }
        public int Score8B { get => ActivePlayer.Weeks[currentWeek - 1].Matches[7].ResultB; set { ActivePlayer.Weeks[currentWeek - 1].Matches[7].ResultB = value; this.WhenAnyValue(x => x.currentWeek).Subscribe(a => this.RaisePropertyChanged()); } }
        public int Score9A { get => ActivePlayer.Weeks[currentWeek - 1].Matches[8].ResultA; set { ActivePlayer.Weeks[currentWeek - 1].Matches[8].ResultA = value; this.WhenAnyValue(x => x.currentWeek).Subscribe(a => this.RaisePropertyChanged()); } }
        public int Score9B { get => ActivePlayer.Weeks[currentWeek - 1].Matches[8].ResultB; set { ActivePlayer.Weeks[currentWeek - 1].Matches[8].ResultB = value; this.WhenAnyValue(x => x.currentWeek).Subscribe(a => this.RaisePropertyChanged()); } }

        public ReactiveCommand<Unit,Unit> NextWeekCommand { get; set; }
        public ReactiveCommand<Unit, Unit> PreviousWeekCommand { get; set; }

        public TotoFormVm(Player activeplayer)
        {
            if (activeplayer == null)
                CreateDefaultActivePlayer();
            else
                ActivePlayer = activeplayer;
            currentWeek = 1;        

            var NextWeekCommandCanExecute = this.WhenAnyValue(
                x => x.currentWeek,
                (a) => { return a < 34; }).ObserveOn(RxApp.MainThreadScheduler);        

            var PreviousWeekCommandCanExecute = this.WhenAnyValue(
                x => x.currentWeek,
                (a) => { return a > 1; }).ObserveOn(RxApp.MainThreadScheduler);

            NextWeekCommand = ReactiveCommand.Create(() => { this.ChangeWeek(1); }, NextWeekCommandCanExecute);
            PreviousWeekCommand = ReactiveCommand.Create(() => { this.ChangeWeek(-1); }, PreviousWeekCommandCanExecute);
        }

        private void ChangeWeek(int change)
        {
            currentWeek += change;
        }

        private void CreateDefaultActivePlayer()
        {
            var weeks = new Week[34];
            for (int i = 0; i< 34; i++)
            {
                var matches = new Match[9];
                for(int x = 0; x < 9; x++)
                {
                    matches[x] = new Match(99, 99, x==8);             
                }

                weeks[i] = new Week(i, matches);
            }
            ActivePlayer = new Player("", "", weeks, new BonusQuestions("", "", "", "", "", "", "", new string[2], new string[2], new string[2], new int[10]));
        }
    }
}
