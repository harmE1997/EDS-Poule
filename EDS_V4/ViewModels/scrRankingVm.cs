using EDS_V4.Code;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EDS_V4.ViewModels
{
    public class RankingField
    {
        public int Rank { get; set; }
        public int PreviousRank { get; set; }
        public int RankingDifference { get; set; }
        public string Name { get; set; }
        public int Total { get; set; }
        public int WeekTotal { get; set; }
        public int Matches { get; set; }
        public int Bonus { get; set; }
        public int Postponement { get; set; }
        
    }
    public class scrRankingVm : ViewModelBase
    {
        private Host host;
        private const string lastWeekCheckedFileName = "LastWeekChecked.JSON";

        private List<RankingField> ranking;
        public List<RankingField> Ranking { get => ranking; set => this.RaiseAndSetIfChanged(ref ranking, value); }

        private List<int> weeks;
        public List<int> Weeks { get => weeks; set => this.RaiseAndSetIfChanged(ref weeks, value); }

        private int startweek;
        public int StartWeek { get => startweek; set => this.RaiseAndSetIfChanged(ref startweek, value); }

        private int endweek;
        public int EndWeek { get => endweek; set => this.RaiseAndSetIfChanged(ref endweek, value); }

        private bool periodCalculation;
        public bool PeriodCalculation { get => periodCalculation; set => this.RaiseAndSetIfChanged(ref periodCalculation, value); }

        public scrRankingVm()
        {
            host = new Host();
            Weeks = new List<int>() {1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34};
            if (File.Exists(lastWeekCheckedFileName))
            {
                string input = File.ReadAllText(lastWeekCheckedFileName);
                StartWeek = JsonSerializer.Deserialize<KeyValuePair<int, int>>(input, new JsonSerializerOptions { WriteIndented = true }).Key;
                EndWeek = JsonSerializer.Deserialize<KeyValuePair<int, int>>(input, new JsonSerializerOptions { WriteIndented = true }).Value;
            }

            else
            {
                StartWeek = Weeks[0];
                EndWeek = Weeks[0];
            }
            Ranking = new List<RankingField>();
            SettingsVm.SettingsEvent += RefreshRanking;
        }

        public void CalculateNewRanking()
        {
            try
            {
                host.setHost();
                scrPlayersVm.PlayerManager.CheckAllPlayers(host, StartWeek, EndWeek, PeriodCalculation);
                string output = JsonSerializer.Serialize(new KeyValuePair<int,int>(StartWeek, EndWeek), new JsonSerializerOptions { WriteIndented = false });
                File.WriteAllText(lastWeekCheckedFileName, output);
                RefreshRanking();
                PopupManager.ShowMessage("New ranking calculated");
            }

            catch (FileNotFoundException) { PopupManager.ShowMessage("Excel file does not exist"); }
            catch (Exception e){ PopupManager.ShowMessage(e.Message); }
        }

        public void ExportRanking()
        {
            var excelManager = new Excel.ExcelManager();

            excelManager.ExportPlayersToExcel(scrPlayersVm.PlayerManager.Players, EndWeek);
            PopupManager.ShowMessage("Ranking sucessfully exported");
        }

        public void GetAverageScore()
        {
            int res = scrPlayersVm.PlayerManager.GetAverageScore(EndWeek);
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
                var playerweek = player.Weeks[EndWeek];
                rank.Add(new RankingField() { Rank = player.Ranking, PreviousRank = player.PreviousRanking, RankingDifference = player.RankingDifference, Name = player.Name, Total = player.TotalScore, 
                    WeekTotal=playerweek.WeekTotalScore, Matches = playerweek.WeekMatchesScore, 
                    Bonus=playerweek.WeekBonusScore, Postponement=playerweek.WeekPostponementScore }) ;
            }

            Ranking = rank;
        }
    }
}
