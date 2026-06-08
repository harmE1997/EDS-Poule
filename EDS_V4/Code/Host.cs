using EDS_V4.Excel;
using System.Collections.Generic;
using VoetbalPoolsBase;
using VoetbalPoolsBase.Interfaces;

namespace EDS_V4.Code
{
    public class Host : Player, IHost
    {
        private Dictionary<string, Topscorer> Topscorers;
        public ExcelManager excelManager;
        public bool HostSet = false;

        public Host() : base("", "", null, null)
        {
            excelManager = new ExcelManager();
        }

        public Dictionary<string, Topscorer> GetTopscorers()
        {
            if (Topscorers.Count == 0)
                setTopscorers();
            return Topscorers;
        }

        public void setTopscorers()
        {
            Topscorers = new ExcelManager().readtopscorers(true);
        }

        public void setHost()
        {
            if (!HostSet)
            {
                Topscorers = new Dictionary<string, Topscorer>();
                Weeks = excelManager.ReadPredictions(GeneralConfiguration.AdminFileLocation, ExcelConfiguration.HostSheet, 0, host: true);
                Questions = new(excelManager.ReadBonus(GeneralConfiguration.AdminFileLocation, ExcelConfiguration.HostSheet, true));
                setTopscorers();
                HostSet = true;
            }
        }
    }
}
