using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.IO;

namespace EDS_Poule
{
    public class ExcelReadSettings
    {
        public int StartRow = 12;
        public readonly int BlockSize = 9;
        public readonly int NrBlocks = 34;
        public readonly int FirstHalfSize = 14;
        public readonly int HomeColumn = 7;
        public readonly int OutColumn = 8;
        public int Miss = 0;
        public int HalfWayJump = 12;
    }

    public class ExcelManager
    {
        excel.Application xlApp;
        excel.Workbook xlWorkbook;
        excel._Worksheet xlWorksheet;
        excel.Range xlRange;

        private void Initialise(string filename, int sheet)
        {
            if (!File.Exists(filename))
                throw new FileNotFoundException();

            xlApp = new excel.Application();
            xlWorkbook = xlApp.Workbooks.Open(filename);
            xlWorksheet = xlWorkbook.Sheets[sheet];
            xlRange = xlWorksheet.UsedRange;
        }

        public IEnumerable<int> ExportPlayersToExcel(string filename, int sheet, List<Player> Players)
        {
            Initialise(filename, sheet);
            int y = 2;
            foreach (Player player in Players)
            {
                xlRange.Cells[y, 1].value2 = player.Ranking;
                xlRange.Cells[y, 2].value2 = player.PreviousRanking;
                xlRange.Cells[y, 3].value2 = player.Name;
                xlRange.Cells[y, 4].value2 = player.Woonplaats;
                xlRange.Cells[y, 5].value2 = player.TotalScore;
                xlRange.Cells[y, 6].value2 = player.WeekScore;
                y++;
                yield return y;
            }
            Clean();
        }

        public Week[] ReadPredictions(string filename, int sheet, bool firsthalf = false, bool secondhalf=false, Week[] Weeks = null)
        {
            var Settings = new ExcelReadSettings();
            var weeks = new Week[34];
            var StartWeek = 0;
            var Endweek = Settings.NrBlocks;
            if (firsthalf)
                Endweek -= (Settings.NrBlocks - Settings.FirstHalfSize);
            if (secondhalf)
                StartWeek += Settings.FirstHalfSize;
            
            for (int i = StartWeek; i < Endweek; i++)
            {
                var matches = ReadSingleWeek(filename, sheet, (i + 1));
                weeks[i] = new Week((i + 1), matches);
            }
            return weeks;
        }

        public Match[] ReadSingleWeek(string filename, int sheet, int week)
        {
            Match[] Week = new Match[9];
            Initialise(filename, sheet);
            var Settings = new ExcelReadSettings();
            int startrow = Settings.StartRow + (Settings.BlockSize+1) * (week - 1) + Settings.Miss;
            if (week > Settings.FirstHalfSize)
                startrow += Settings.HalfWayJump;
            try
            {
                for (int rowschecked = 0; rowschecked < Settings.BlockSize; rowschecked++)
                {
                    double x = 99;
                    double y = 99;
                    int currentRow = Settings.StartRow + rowschecked;

                    if (xlRange.Cells[currentRow, Settings.HomeColumn].Value2 != null && xlRange.Cells[currentRow, Settings.OutColumn].Value2 != null)
                    {
                        try
                        {
                            x = xlRange.Cells[currentRow, Settings.HomeColumn].Value2;
                            y = xlRange.Cells[currentRow, Settings.OutColumn].Value2;
                        }
                        catch { };
                    }

                    bool motw = false;
                    if (rowschecked == Settings.BlockSize - 1)
                    {
                        motw = true;
                    }

                    Match match = new Match(Convert.ToInt32(x), Convert.ToInt32(y), motw);
                    Week[rowschecked] = match;
                }
                Clean();
                return Week;
            }
            catch { Clean(); return Week; } 
        }

        public BonusQuestions ReadHostBonus(string filename, int sheet)
        {
            int column = 7;
            int weekcolumn = 10;
            Initialise(filename, sheet);
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
                    Convert.ToInt32(xlRange.Cells[375, weekcolumn].value2),
                    Convert.ToInt32(xlRange.Cells[377, weekcolumn].value2),
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
                    finalists, promovendi, degradanten,
                    weeks
                    );
                Clean();
                return bonus;
            }
            catch { Clean(); return null; };
        }

        public Dictionary<string, Topscorer> readtopscorers(int round, string filename, int sheet)
        {
            Initialise(filename, sheet);
            Topscorer ts = new Topscorer() { Total=0, Currentround=0 };
            Dictionary<string, Topscorer> topscorers = new Dictionary<string, Topscorer>();
            for (int i = 2; i < 12; i++)
            {
                string name = Convert.ToString(xlRange.Cells[i, 1].value2);
                ts.Total = Convert.ToInt32(xlRange.Cells[i, 3].value2);
                ts.Currentround = Convert.ToInt32(xlRange.Cells[i, round + 3].value2);
                topscorers.Add(name, ts);
            }

            Clean();
            return topscorers;
        }

        private void Clean()
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
