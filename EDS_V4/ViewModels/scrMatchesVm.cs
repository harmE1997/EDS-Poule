using EDS_V4.Code;
using EDS_V4.Excel;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDS_V4.ViewModels
{
    public class MatchField
    {
        public string Result { get; set; }
        public int NrPredictions { get; set; }
        public string Names { get; set; }

    }
    public class scrMatchesVm : ViewModelBase
    {
        private List<string> matches;
        public List<string> Matches { get => matches; set => this.RaiseAndSetIfChanged(ref matches, value); }

        private List<string> weeks;
        public List<string> Weeks { get => weeks; set => this.RaiseAndSetIfChanged(ref weeks, value); }

        private string selectedweek;
        public string SelectedWeek { get => selectedweek; set => this.RaiseAndSetIfChanged(ref selectedweek, value); }

        private string selectedmatch;
        public string SelectedMatch { get => selectedmatch; set => this.RaiseAndSetIfChanged(ref selectedmatch, value); }

        private List<MatchField> outputs;
        public List<MatchField> Outputs { get => outputs; set => this.RaiseAndSetIfChanged(ref outputs, value); }  


        public scrMatchesVm()
        {
            Matches = new List<string>() {"1","2","3","4","5","6","7","8", "MOTW" };
            SelectedMatch = Matches[0];
            Weeks = new List<string>() {"1", "2", "3", "4", "5", "6", "7", "8", "9","10",
                "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", 
                "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34"};
            SelectedWeek = Weeks[0];
            Outputs = new List<MatchField>();
        }

        public void GetPredictionsCommand()
        {
            var week = Convert.ToInt16(selectedweek) - 1;
            Dictionary<string, int> results = new Dictionary<string, int>();

            foreach (Player p in scrPlayersVm.PlayerManager.Players)
            {
                if (p.Weeks[week] == null)
                    continue;

                int matchID = 8;
                if (SelectedMatch != "MOTW")
                    matchID = Convert.ToInt16(SelectedMatch) - 1;

                var match = p.Weeks[week].Matches[matchID];
                if (results.ContainsKey(match.Winner))
                    results[match.Winner]++;
                else
                    results.Add(match.Winner, 1);
                
                if (results.ContainsKey(match.MatchToString()))
                    results[match.MatchToString()]++;
                else
                    results.Add(match.MatchToString(), 1);
            }

            var output = new List<MatchField>();
            foreach (var result in results)
            {
                output.Add(new MatchField() { Result = result.Key, NrPredictions = result.Value, Names=""});
            }
            Outputs = output;
        }

        public void GetResultsCommand()
        {
            try
            {
                int fulls = 0;
                int halfs = 0;
                int matchID = matchID = Convert.ToInt16(SelectedMatch);
                var week = Convert.ToInt16(selectedweek);
                string Names = "";
                ExcelManager em = new ExcelManager();
                var hostweek = em.InitializeAndReadSingleWeek(GeneralConfiguration.AdminFileLocation, ExcelConfiguration.HostSheet, week, 0);
                foreach (Player player in scrPlayersVm.PlayerManager.Players)
                {
                    if (player.Weeks[week] != null)
                    {
                        int check = player.Weeks[week].CheckMatchOnResultOnly(hostweek, matchID);
                        if (check > 0)
                            halfs++;

                        if (check == 2)
                        {
                            fulls++;
                            Names += player.Name + "\n";
                        }
                    }
                }

                var output = new List<MatchField>();
                output.Add(new MatchField() { Result = "Goede winnaar", NrPredictions = halfs, Names= "" });
                output.Add(new MatchField() { Result = "Helemaal correct", NrPredictions = fulls, Names = Names });
                Outputs = output;
            }

            catch (FileNotFoundException) { PopupManager.OnMessage("Excel file does not exist"); }
            catch(Exception e) { PopupManager.OnMessage("Can't get results. Unknown error."); }
        }
    }
}
