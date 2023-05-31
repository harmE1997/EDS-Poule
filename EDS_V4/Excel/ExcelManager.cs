using EDS_V4.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.IO;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ReactiveUI;

namespace EDS_V4.Excel
{
    public class ExcelManager
    {
        private excel.Application xlApp;
        private excel.Workbook xlWorkbook;
        private excel._Worksheet xlWorksheet;
        private excel.Range xlRange;

        public void ExportPlayersToExcel(List<Player> Players)
        {
            InitialiseWorkbook(GeneralConfiguration.AdminFileLocation, ExcelConfiguration.RankingSheet);
            int y = 3;
            foreach (Player player in Players)
            {
                xlRange.Cells[y, 1].value2 = player.Ranking;
                xlRange.Cells[y, 2].value2 = player.PreviousRanking;
                xlRange.Cells[y, 3].value2 = player.RankingDifference;
                xlRange.Cells[y, 4].value2 = player.Name;
                xlRange.Cells[y, 5].value2 = player.Town;
                xlRange.Cells[y, 6].value2 = player.TotalScore;
                xlRange.Cells[y, 7].value2 = player.WeekTotalScore;
                xlRange.Cells[y, 8].value2 = player.WeekMatchesScore;
                xlRange.Cells[y, 9].value2 = player.WeekBonusScore;
                xlRange.Cells[y, 10].value2 = player.WeekPostponementScore;
                y++;
            }
            CleanWorkbook();
        }

        public Dictionary<int, Week> ReadPredictions(string filename, int sheet, int miss, bool firsthalf = false, bool secondhalf = false, Dictionary<int, Week> Weeks = null)
        {
            var weeks = new Dictionary<int, Week>();
            if (Weeks != null)
                weeks = Weeks;

            var StartWeek = 0;
            var Endweek = ExcelConfiguration.NrBlocks;
            if (firsthalf)
                Endweek -= (ExcelConfiguration.NrBlocks - ExcelConfiguration.FirstHalfSize);
            if (secondhalf)
                StartWeek += ExcelConfiguration.FirstHalfSize;
            
            try
            {
                InitialiseWorkbook(filename, sheet);
                for (int i = StartWeek; i < Endweek; i++)
                {
                    var matches = ReadSingleWeek(filename, sheet, i, miss);
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

        public Match[] ReadSingleWeek(string filename, int sheet, int week, int miss, bool initializationRequired = false)
        {
            Match[] Week = new Match[9];

            int startrow = ExcelConfiguration.StartRow + (ExcelConfiguration.BlockSize + 1) * (week) + miss;
            if (week >= ExcelConfiguration.FirstHalfSize)
                startrow += ExcelConfiguration.HalfWayJump;

            if (initializationRequired)
                InitialiseWorkbook(filename, sheet);
            try
            {
                for (int rowschecked = 0; rowschecked < ExcelConfiguration.BlockSize; rowschecked++)
                {
                    double a = 99;
                    double b = 99;
                    double p = 0; 
                    int currentRow = startrow + rowschecked;
                
                    var pt = xlRange.Cells[currentRow, ExcelConfiguration.PostponementColumn].Value2;
                    var at = xlRange.Cells[currentRow, ExcelConfiguration.HomeColumn].Value2;
                    var bt = xlRange.Cells[currentRow, ExcelConfiguration.OutColumn].Value2;
                    

                    if (at != null && bt != null)
                    {
                        a = at;
                        b = bt;
                    }

                    if (pt != null)
                        p = pt;

                    bool motw = false;
                    if (rowschecked == ExcelConfiguration.BlockSize - 1)
                    {
                        motw = true;
                    }

                    Match match = new Match(Convert.ToInt16(a), Convert.ToInt16(b), motw, Convert.ToInt16(p));
                    Week[rowschecked] = match;
                }
                return Week;
            }
            catch (Exception e) { return Week; }
            finally 
            { 
                if(initializationRequired)
                    CleanWorkbook(); 
            }
        }

        public BonusQuestions ReadBonus()
        {
            int column = 7;
            int weekcolumn = 10;
            InitialiseWorkbook(GeneralConfiguration.AdminFileLocation, ExcelConfiguration.HostSheet);
            try
            {
                int[] weeks =
                {
                    Convert.ToInt32(xlRange.Cells[366, weekcolumn].value2),
                    Convert.ToInt32(xlRange.Cells[367, weekcolumn].value2),
                    Convert.ToInt32(xlRange.Cells[368, weekcolumn].value2),
                    Convert.ToInt32(xlRange.Cells[369, weekcolumn].value2),
                    Convert.ToInt32(xlRange.Cells[370, weekcolumn].value2),
                    Convert.ToInt32(xlRange.Cells[371, weekcolumn].value2),
                    Convert.ToInt32(xlRange.Cells[372, weekcolumn].value2),
                    Convert.ToInt32(xlRange.Cells[373, weekcolumn].value2),
                    Convert.ToInt32(xlRange.Cells[374, weekcolumn].value2),
                    Convert.ToInt32(xlRange.Cells[375, weekcolumn].value2),
                    Convert.ToInt32(xlRange.Cells[376, weekcolumn].value2),
                    Convert.ToInt32(xlRange.Cells[377, weekcolumn].value2),
                    Convert.ToInt32(xlRange.Cells[378, weekcolumn].value2),
                };

                string[] degradanten = { Convert.ToString(xlRange.Cells[375, column].value2), Convert.ToString(xlRange.Cells[376, column].value2) };
                string[] promovendi = { Convert.ToString(xlRange.Cells[377, column].value2), Convert.ToString(xlRange.Cells[378, column].value2) };
                string[] finalists = { Convert.ToString(xlRange.Cells[373, column].value2), Convert.ToString(xlRange.Cells[374, column].value2) };
                BonusQuestions bonus = new BonusQuestions
                    (
                    Convert.ToString(xlRange.Cells[366, column].value2),
                    Convert.ToString(xlRange.Cells[367, column].value2),
                    Convert.ToString(xlRange.Cells[368, column].value2),
                    Convert.ToString(xlRange.Cells[369, column].value2),
                    Convert.ToString(xlRange.Cells[370, column].value2),
                    Convert.ToString(xlRange.Cells[371, column].value2),
                    Convert.ToString(xlRange.Cells[372, column].value2),
                    finalists, degradanten, promovendi, 
                    weeks
                    );
                return bonus;
            }
            catch {return null; }
            finally{ CleanWorkbook(); }
        }

        public Dictionary<string, Topscorer> readtopscorers()
        {
            Dictionary<string, Topscorer> scorers = new Dictionary<string, Topscorer>();
            InitialiseWorkbook(GeneralConfiguration.AdminFileLocation, ExcelConfiguration.TopscorersSheet);
            try
            {
                int i = 2;
                while (true)
                {
                    Topscorer ts = new Topscorer() { Total = 0, Rounds = new List<int>() };
                    string name = Convert.ToString(xlRange.Cells[i, 1].value2);
                    if (string.IsNullOrEmpty(name))
                        break;
                    ts.Total = Convert.ToInt32(xlRange.Cells[i, 3].value2);
                    for (int x = 0; x < 34; x++)
                    {
                        var round = Convert.ToInt32(xlRange.Cells[i, x + 4].value2);
                        ts.Rounds.Add(round);
                    }
                    scorers.Add(name, ts);
                    i++;
                }
                return scorers;
            }

            catch
            {
                return scorers;
            }
            finally { CleanWorkbook(); }
        }

        private void InitialiseWorkbook(string filename, int sheet)
        {
            if (!File.Exists(filename))
                throw new FileNotFoundException();

            xlApp = new excel.Application();
            xlWorkbook = xlApp.Workbooks.Open(filename);
            xlWorksheet = xlWorkbook.Sheets[sheet];
            xlRange = xlWorksheet.UsedRange;
        }

        private void CleanWorkbook()
        {
            //cleanup
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //release com objects to fully kill excel process from running in the background
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);

            //close and release
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);

            //quit and release
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);
        }
    }
}
