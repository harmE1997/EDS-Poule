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
            int progress = 0;
            foreach (Player player in Players)
            {
                for (int y = 2; y <= Players.Count + 1; y++)
                {
                    if (xlRange.Cells[y, 3].value2 == player.Name)
                    {
                        int rank = Convert.ToInt32(xlRange.Cells[y, 1].value2);
                        xlRange.Cells[y, 2].value2 = rank;
                        xlRange.Cells[y, 1].value2 = player.Ranking;
                        xlRange.Cells[y, 5].value2 = player.TotalScore;
                        xlRange.Cells[y, 6].value2 = player.WeekScore;
                        break;
                    }
                }
                progress++;
                yield return progress;
            }
        }

        public Week[] readPredictions(string filename, int sheet, bool first, bool second, int miss, Week[] Weeks = null)
        {
            Initialise(filename, sheet);
            var weeks = new Week[34];
            if (Weeks != null)
                weeks = Weeks;

            int row = 11;
            int rowcounter = 20;
            int blockchecker = 34;
            int blocks = 0;
            int Home = 7;
            int Out = 8;

            if (first && !second)
            {
                blockchecker = 17;
                weeks = new Week[17];
            }

            if (second && !first)
            {
                blocks = 17;
                rowcounter = 20;
                Home = 16;
                Out = 17;
            }

            row += miss;
            rowcounter += miss;

            while (blocks < blockchecker)
            {
                Match[] fileMatches = new Match[9];
                int rowschecked = 1;
                while (row < rowcounter) // excel is niet 0 based.
                {
                    double x;
                    double y;
                    if (xlRange.Cells[row, Home].Value2 == null || xlRange.Cells[row, Out].Value2 == null)
                    {
                        x = 99;
                        y = 99;
                    }

                    else
                    {
                        x = xlRange.Cells[row, Home].Value2;
                        y = xlRange.Cells[row, Out].Value2;
                    }
                    Match match = new Match(Convert.ToInt32(x), Convert.ToInt32(y));
                    fileMatches[rowschecked] = match;

                    row++;
                    rowschecked++;
                    if (rowschecked == 9)
                        rowschecked = 0;
                }

                weeks[blocks] = new Week(blocks + 1, fileMatches);

                if (blocks == 16 && blockchecker == 34)
                {
                    blocks = 17;
                    row = 11;
                    rowcounter = 20;
                    Home = 16;
                    Out = 17;
                }

                else
                {
                    row++;
                    rowcounter += 10;
                }

                blocks++;
            }

            return weeks;
        }

        public Estimations ReadEstimations()
        {
            int reds = xlRange.Cells[184, 21].Value2;
            int goals = xlRange.Cells[185, 21].Value2;

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
