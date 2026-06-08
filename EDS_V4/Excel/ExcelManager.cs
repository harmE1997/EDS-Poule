using EDS_V4.Code;
using System;
using System.Collections.Generic;
using System.IO;
using VoetbalPoolsBase;
using VoetbalPoolsBase.Excel;


namespace EDS_V4.Excel
{
    public class ExcelManager : ExcelBase
    {
        public void ExportPlayersToExcel(List<Player> Players, int weeknr)
        {
            InitialiseWorkbook(GeneralConfiguration.AdminFileLocation, ExcelBaseConfiguration.RankingSheet);
            int y = 3;
            foreach (Player player in Players)
            {
                var playerweek = player.Weeks[weeknr];
                xlRange.Cells[y, 1].value2 = player.Ranking;
                xlRange.Cells[y, 2].value2 = player.PreviousRanking;
                xlRange.Cells[y, 3].value2 = player.RankingDifference;
                xlRange.Cells[y, 4].value2 = player.Name;
                xlRange.Cells[y, 5].value2 = player.Town;
                xlRange.Cells[y, 6].value2 = player.TotalScore;
                xlRange.Cells[y, 7].value2 = playerweek.WeekTotalScore;
                xlRange.Cells[y, 8].value2 = playerweek.WeekMatchesScore;
                xlRange.Cells[y, 9].value2 = playerweek.WeekBonusScore;
                xlRange.Cells[y, 10].value2 = playerweek.WeekPostponementScore;
                y++;
            }
            CleanWorkbook();
        }

        public Dictionary<int, Week> ReadPredictions(string filename, int sheet, int miss, bool firsthalf = false, bool secondhalf = false, Dictionary<int, Week> Weeks = null, bool host = false)
        {
            var weeks = new Dictionary<int, Week>();
            if (Weeks != null)
                weeks = Weeks;

            var StartWeek = 0;
            var Endweek = ExcelConfiguration.NrBlocks;
            if (firsthalf)
                Endweek -= (ExcelConfiguration.NrBlocks - ExcelBaseConfiguration.FirstHalfSize);
            else if (secondhalf)
                StartWeek += ExcelBaseConfiguration.FirstHalfSize;

            try
            {
                if (!File.Exists(filename))
                {
                    PopupManager.ShowMessage("Cannot read host. Admin cannot be found");
                    return weeks;
                }

                InitialiseWorkbook(filename, sheet);
                for (int i = StartWeek; i < Endweek; i++)
                {
                    var matches = ReadBlock(i, ExcelConfiguration.BlockSize, miss, host);
                    if (matches == null)
                    {
                        PopupManager.ShowMessage("Cannot read predictions. Problem at week " + (i + 1));
                        CleanWorkbook();
                        return null;
                    }
                    if (weeks.ContainsKey(i + 1))
                        weeks[i + 1] = new Week(i + 1, matches);
                    else
                        weeks.Add(i + 1, new Week((i + 1), matches));
                }
                CleanWorkbook();
                return weeks;
            }

            catch (Exception e) { CleanWorkbook(); return weeks; }
        }
    }
}
