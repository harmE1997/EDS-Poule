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
        public int FirstHalfSize = 18;
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
        }

        public Week[] ReadPredictions(string filename, int sheet, ExcelReadSettings Settings, Week[] Weeks = null)
        {
            Initialise(filename, sheet);
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

            return weeks;
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
                Match match = new Match(Convert.ToInt32(x), Convert.ToInt32(y));
                int index = rowschecked + 1;
                if (rowschecked == Settings.BlockSize - 1)
                {
                    index = 0;
                }

                fileMatches[index] = match;                
                rowschecked++;
            }
            return fileMatches;
        }
        public Estimations ReadEstimations()
        {
            int column = 8;
            int reds = Convert.ToInt32(xlRange.Cells[385, column].Value2);
            int goals = Convert.ToInt32(xlRange.Cells[386, column].Value2);

            return new Estimations(reds, goals);
        }
        public void Clean()
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
