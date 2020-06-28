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
        public int BlockSize = 9;
        public int TotalBlocks = 34;
        public int FirstHalfSize = 14;
        public int CurrentBlock = 0;
        public int HomeColumn = 7;
        public int OutColumn = 8;
        public int Miss = 0;

        public ExcelReadSettings(int adjustment = 0, int miss = 0)
        {
            Adjustsettings(adjustment);
            StartRow += miss;
        }
        public void Adjustsettings(int adjustment)
        {
            // 1: only first half
            // 2: switch to second half
            if (adjustment == 1)
            {
                TotalBlocks = FirstHalfSize;
            }

            else if (adjustment == 2)
            {
                CurrentBlock = TotalBlocks - (TotalBlocks - FirstHalfSize);
                StartRow = 204;
            }
        }
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

        public Week[] ReadPredictions(string filename, int sheet, ExcelReadSettings Settings, Week[] Weeks = null)
        {
            Initialise(filename, sheet);
            try
            {
                var weeks = new Week[34];
                if (Weeks != null)
                    weeks = Weeks;

                int currentblock = Settings.CurrentBlock;
                while (currentblock < Settings.TotalBlocks)
                {
                    weeks[currentblock] = new Week(currentblock + 1, ReadWeek(Settings));
                    Settings.StartRow += Settings.BlockSize + 1;
                    if (currentblock == (Settings.FirstHalfSize - 1) && Settings.TotalBlocks == 34)
                    {
                        Settings.Adjustsettings(2);
                    }

                    currentblock++;
                }
                Clean();
                return weeks;
            }
            catch { Clean(); return null; }
        }

        private Match[] ReadWeek(ExcelReadSettings Settings)
        {
            Match[] fileMatches = new Match[9];
            int rowschecked = 0;
            while (rowschecked < Settings.BlockSize)
            {
                double x = 99;
                double y = 99;
                int currentRow = Settings.StartRow + rowschecked;

                if (xlRange.Cells[currentRow, Settings.HomeColumn].Value2 != null && xlRange.Cells[currentRow, Settings.OutColumn].Value2 != null)
                {
                    x = xlRange.Cells[currentRow, Settings.HomeColumn].Value2;
                    y = xlRange.Cells[currentRow, Settings.OutColumn].Value2;
                }
                
                bool motw = false;
                if (rowschecked == Settings.BlockSize - 1)
                {
                    motw = true;
                }
                Match match = new Match(Convert.ToInt32(x), Convert.ToInt32(y), motw);
                fileMatches[rowschecked] = match;
                rowschecked++;
            }
            return fileMatches;
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
                    Convert.ToInt32(xlRange.Cells[368, weekcolumn].value2),
                    Convert.ToInt32(xlRange.Cells[379, weekcolumn].value2),
                    Convert.ToInt32(xlRange.Cells[370, weekcolumn].value2),
                    Convert.ToInt32(xlRange.Cells[371, weekcolumn].value2),
                    Convert.ToInt32(xlRange.Cells[372, weekcolumn].value2),
                    Convert.ToInt32(xlRange.Cells[373, weekcolumn].value2),
                    Convert.ToInt32(xlRange.Cells[381, weekcolumn].value2),
                    Convert.ToInt32(xlRange.Cells[377, weekcolumn].value2),
                    Convert.ToInt32(xlRange.Cells[374, weekcolumn].value2),
                    Convert.ToInt32(xlRange.Cells[375, weekcolumn].value2),
                    Convert.ToInt32(xlRange.Cells[376, weekcolumn].value2),
                    Convert.ToInt32(xlRange.Cells[369, weekcolumn].value2)
            };

                string[] degradanten = { xlRange.Cells[379, column].value2.ToString(), xlRange.Cells[380, column].value2.ToString() };
                string[] promovendi = { xlRange.Cells[381, column].value2.ToString(), xlRange.Cells[382, column].value2.ToString() };
                string[] finalists = { xlRange.Cells[377, column].value2.ToString(), xlRange.Cells[378, column].value2.ToString() };
                BonusQuestions bonus = new BonusQuestions
                    (
                    xlRange.Cells[368, column].value2.ToString(),                   
                    xlRange.Cells[369, column].value2.ToString(),
                    xlRange.Cells[370, column].value2.ToString(),
                    xlRange.Cells[371, column].value2.ToString(),
                    xlRange.Cells[372, column].value2.ToString(),                    
                    xlRange.Cells[373, column].value2.ToString(),
                    xlRange.Cells[374, column].value2.ToString(),
                    finalists, promovendi, degradanten,
                    weeks
                    );
                Clean();
                return bonus;
            }
            catch { Clean(); return null; };
        }

        public Topscorer readtopscorer(string name, int round, string filename, int sheet)
        {
            Initialise(filename, sheet);
            Topscorer ts = new Topscorer() { Total=0, Currentround=0 };
            for (int i = 2; i < 17; i++)
            {                
                if (xlRange.Cells[i, 1].value2.ToString() == name)
                    ts.Total = xlRange.Cells[i, 2].value2;
                    ts.Currentround = xlRange.Cells[i, round+2].value2;              
            }
            return ts;
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
