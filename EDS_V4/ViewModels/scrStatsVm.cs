using EDS_V4.Code;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace EDS_V4.ViewModels
{
    public class StatsField 
    {
        public string Name { get; set; }
        public int Number { get; set; }

        public string Names { get; set; }
    }
    public class scrStatsVm : ViewModelBase
    {
        private List<string> output;
        public List<string> Output { get => output; set => this.RaiseAndSetIfChanged(ref output, value); }
        private List<Stat> stats;

        private List<StatsField> statsFields;
        public List<StatsField> StatsFields { get => statsFields; set => this.RaiseAndSetIfChanged(ref statsFields, value); }

        public ReactiveCommand<Unit, Unit> ChampionsCommand { get; set; }
        public ReactiveCommand<Unit, Unit> DegradantenCommand { get; set; }
        public ReactiveCommand<Unit, Unit> PromovendiCommand { get; set; }
        public ReactiveCommand<Unit, Unit> NacompetitieCommand { get; set; }
        public ReactiveCommand<Unit, Unit> WinterkampioenCommand { get; set; }
        public ReactiveCommand<Unit, Unit> RondeCommand { get; set; }
        public ReactiveCommand<Unit, Unit> TopscorersCommand { get; set; }
        public ReactiveCommand<Unit, Unit> RodekaartenCommand { get; set; }
        public ReactiveCommand<Unit, Unit> TrainersCommand { get; set; }
        public ReactiveCommand<Unit, Unit> BekerfinalistenCommand { get; set; }

        public scrStatsVm()
        {
            stats = new List<Stat>();
            StatsFields = new List<StatsField>();
            ChampionsCommand = ReactiveCommand.Create(() => { this.ActionBonusQuestion(BonusKeys.Kampioen); });
            DegradantenCommand = ReactiveCommand.Create(() => { this.ActionBonusQuestion(BonusKeys.Degradanten); });
            PromovendiCommand = ReactiveCommand.Create(() => { this.ActionBonusQuestion(BonusKeys.Promovendi); });
            NacompetitieCommand = ReactiveCommand.Create(() => { this.ActionBonusQuestion(BonusKeys.Prodeg); });
            WinterkampioenCommand = ReactiveCommand.Create(() => { this.ActionBonusQuestion(BonusKeys.Winterkampioen); });
            RondeCommand = ReactiveCommand.Create(() => { this.ActionBonusQuestion(BonusKeys.Ronde); });
            TopscorersCommand = ReactiveCommand.Create(() => { this.ActionBonusQuestion(BonusKeys.Topscorer); });
            RodekaartenCommand = ReactiveCommand.Create(() => { this.ActionBonusQuestion(BonusKeys.Teamrood); });
            TrainersCommand = ReactiveCommand.Create(() => { this.ActionBonusQuestion(BonusKeys.Trainer); });
            BekerfinalistenCommand = ReactiveCommand.Create(() => { this.ActionBonusQuestion(BonusKeys.Finalisten); });
        }

        public void ActionBonusQuestion(BonusKeys Key)
        {
            stats.Clear();
            foreach (Player player in scrPlayersVm.PlayerManager.Players)
            {
                var Name = player.Name;
                var answer = player.Questions.Answers[Key];
                foreach (var e in answer.Answer)
                {
                    UpdateStats(e, Name);
                }
            }
            UpdateListBox();
        }

        private void UpdateStats(string stat, string playername)
        {
            Stat existingStat = null;
            foreach (Stat oldstat in stats)
            {
                if (oldstat.Name == stat)
                {
                    existingStat = oldstat;
                }
            }

            if (existingStat == null)
            {
                Stat newstat = new Stat(stat, playername);
                stats.Add(newstat);
            }

            else
            {
                existingStat.Add(playername);
            }
        }

        private void UpdateListBox()
        {
            List<StatsField> newoutput = new List<StatsField>();
            stats.Sort();
            foreach (Stat stat in stats)
            {
                var field = new StatsField() { Name = stat.Name, Number = stat.Number };
                foreach (var name in stat.Names)
                {
                    field.Names += name + "\n";
                }
                newoutput.Add(field);
            }

            StatsFields = newoutput;
        }
    }
}
