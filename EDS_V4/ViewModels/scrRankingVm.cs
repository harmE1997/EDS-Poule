using EDS_V4.Code;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDS_V4.ViewModels
{
    public class RankingField
    {
        public int Rank { get; set; }
        public int PreviousRank { get; set; }
        public int Total { get; set; }
        public int WeekTotal { get; set; }
        public int Matches { get; set; }
        public int Bonus { get; set; }
        public int Postponement { get; set; }
        public string Name { get; set; }
    }
    public class scrRankingVm : ViewModelBase
    {
        private Host host;

        private List<RankingField> ranking;
        public List<RankingField> Ranking { get => ranking; set => this.RaiseAndSetIfChanged(ref ranking, value); }

        private List<int> weeks;
        public List<int> Weeks { get => weeks; set => this.RaiseAndSetIfChanged(ref weeks, value); }

        private int selectedweek;
        public int SelectedWeek { get => selectedweek; set => this.RaiseAndSetIfChanged(ref selectedweek, value); }

        public scrRankingVm()
        {
            host = new Host();
            Weeks = new List<int>() {1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34};
            SelectedWeek = Weeks[0];
            Ranking = new List<RankingField>();
            SettingsVm.SettingsEvent += RefreshRanking;
        }

        public void CalculateNewRanking()
        {
            try
            {
                host.setHost();
                scrPlayersVm.PlayerManager.CheckAllPlayers(host, selectedweek);
                RefreshRanking();
            }

            catch (FileNotFoundException) { PopupManager.ShowMessage("Excel file does not exist"); }
            catch (Exception e){ PopupManager.ShowMessage(e.Message); }
        }

        public void ExportRanking()
        {
            var excelManager = new Excel.ExcelManager();

            excelManager.ExportPlayersToExcel(scrPlayersVm.PlayerManager.Players, SelectedWeek);
            PopupManager.ShowMessage("Ranking sucessfully exported");
        }

        public void GetAverageScore()
        {
            int res = scrPlayersVm.PlayerManager.GetAverageScore(SelectedWeek);
            PopupManager.ShowMessage("Average score: " + res);
        }

        public void ResetHost()
        {
            host.HostSet = false;
            host.setHost();
            PopupManager.ShowMessage("Host was reset succesfully");
        }

        private void RefreshRanking()
        {
            //sort players by score
            scrPlayersVm.PlayerManager.RankPlayers(true);
            scrPlayersVm.PlayerManager.RankPlayers(false);
            List<RankingField> rank = new List<RankingField>();
            foreach (Player player in scrPlayersVm.PlayerManager.Players)
            {
                var playerweek = player.Weeks[SelectedWeek];
                rank.Add(new RankingField() { Rank = player.Ranking, PreviousRank = player.PreviousRanking, Total = player.TotalScore, 
                    Matches = playerweek.WeekMatchesScore, Name = player.Name, Bonus=playerweek.WeekBonusScore, 
                    Postponement=playerweek.WeekPostponementScore, WeekTotal=playerweek.WeekTotalScore }) ;
            }

            Ranking = rank;
        }
    }
}
