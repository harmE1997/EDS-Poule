using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDS_V4.Code
{
    public static class GeneralConfiguration
    { 
        public static string SaveFileLocation = "";
        public static string AdminFileLocation = "";
    }

    public static class ExcelConfiguration
    {
        public static int StartRow;
        public static int FirstHalfSize;
        public static int HomeColumn;
        public static int OutColumn;
        public static int PostponementColumn;
        public static int HalfWayJump;
        public static int HostSheet;
        public static int RankingSheet;
        public static int TopscorersSheet;
        public static int BlockSize = 9;
        public static int NrBlocks = 34;
    }
}
