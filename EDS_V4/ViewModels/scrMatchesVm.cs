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
    public class MatchField : IComparable<MatchField>
    {
        public string Result { get; set; }
        public int NrPredictions { get; set; }
        public string Names { get; set; }

        public int CompareTo(MatchField? other)
        {
            return string.Compare(other.Result, Result);
        }
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
            var week = Convert.ToInt16(selectedweek);
            Dictionary<string, MatchField> results = new Dictionary<string, MatchField>();

            foreach (Player p in scrPlayersVm.PlayerManager.Players)
            {
                if (p.Weeks[week] == null)
                    continue;

                if (p.Weeks[week].Matches[0].ResultA == 99)
                    continue;

                int matchID = 8;
                if (SelectedMatch != "MOTW")
                    matchID = Convert.ToInt16(SelectedMatch) - 1;

                var match = p.Weeks[week].Matches[matchID];
                if (results.ContainsKey(match.Winner))
                    results[match.Winner].NrPredictions++;
                else
                    results.Add(match.Winner, new MatchField() { Result = match.Winner, NrPredictions=1, Names=""});
                
                if (results.ContainsKey(match.MatchToString()))
                    results[match.MatchToString()].NrPredictions++;
                else
                    results.Add(match.MatchToString(), new MatchField() { Result = match.MatchToString(), NrPredictions = 1, Names = "" });
                
                if(results[match.MatchToString()].Names == "")
                    results[match.MatchToString()].Names += p.Name;
                else
                    results[match.MatchToString()].Names += "\n" + p.Name;
            }

            var output = new List<MatchField>();
            foreach (var result in results)
            {
                output.Add(result.Value);
            }
            output.Sort();
            Outputs = output;
        }
    }
}
