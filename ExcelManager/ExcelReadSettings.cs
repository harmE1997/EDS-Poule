using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace EDS_Poule
{
    public class ExcelReadSettings
    {
        public readonly int StartRow = Convert.ToInt32(ConfigurationManager.AppSettings.Get("StartRow"));
        public readonly int BlockSize = Convert.ToInt32(ConfigurationManager.AppSettings.Get("BlockSize"));
        public readonly int NrBlocks = Convert.ToInt32(ConfigurationManager.AppSettings.Get("NrBlocks"));
        public readonly int FirstHalfSize = Convert.ToInt32(ConfigurationManager.AppSettings.Get("FirstHalfSize"));
        public readonly int HomeColumn = Convert.ToInt32(ConfigurationManager.AppSettings.Get("HomeClumn"));
        public readonly int OutColumn = Convert.ToInt32(ConfigurationManager.AppSettings.Get("OutColumn"));
        public int Miss = 0;
        public readonly int HalfWayJump = Convert.ToInt32(ConfigurationManager.AppSettings.Get("HalfWayJump"));

        public readonly string AdminFileName = ConfigurationManager.AppSettings.Get("AdminLocation");
        public readonly int Hostsheet = Convert.ToInt32(ConfigurationManager.AppSettings.Get("HostSheet"));
        public readonly int Rankingheet = Convert.ToInt32(ConfigurationManager.AppSettings.Get("Rankingsheet"));
        public readonly int Topscorerssheet = Convert.ToInt32(ConfigurationManager.AppSettings.Get("Topscorerssheet"));
    }
}
