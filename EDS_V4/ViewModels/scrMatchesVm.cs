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

        private string outputstring;
        public string OutputText { get => outputstring; set => this.RaiseAndSetIfChanged(ref outputstring, value); }  

        public scrMatchesVm()
        {
            Matches = new List<string>() {"1","2","3","4","5","6","7","8", "MOTW" };
            Weeks = new List<string>() {"1", "2", "3", "4", "5", "6", "7", "8", "9","10",
                "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", 
                "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34"};
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

            var output = "";
            foreach (var result in results)
            {
                output += result.Key + ": " + result.Value + "\n";
            }
            OutputText = output;
        }

        public void GetResultsCommand()
        {
            try
            {
                int fulls = 0;
                int halfs = 0;
                int matchID = matchID = Convert.ToInt16(SelectedMatch);
                var week = Convert.ToInt16(selectedweek) - 1;
                string Names = "";
                ExcelManager em = new ExcelManager();
                var hostweek = em.ReadSingleWeek(GeneralConfiguration.AdminFileLocation, ExcelConfiguration.HostSheet, week, 0);
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
                            Names += player.Name + ", ";
                        }
                    }
                }

                OutputText = "Goede winnaar: " + halfs + "\nHelemaal correct: " + fulls + " " + Names;
            }

            catch (FileNotFoundException) { PopupManager.OnMessage("Excel file does not exist"); }
            catch { PopupManager.OnMessage("Can't open excel file. It's already in use."); }
        }
    }
}
