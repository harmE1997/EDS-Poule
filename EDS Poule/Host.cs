using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace EDS_Poule
{
    public class Host
    {
        private Player host;
        private Dictionary<string, Topscorer> Topscorers;

        public Host()
        {
            host = null;
            Topscorers = new Dictionary<string, Topscorer>();
        }

        public Dictionary<string, Topscorer> getTopscorers()
        {
            if (Topscorers.Count == 0)
                setTopscorers();
            return Topscorers;
        }

        public void setTopscorers()
        { 
            Topscorers = new ExcelManager().readtopscorers();
        }

        public Player getHost()
        {
            if (host == null)
            {
                setHost();
            }

            return host;
        }

        public void setHost()
        { 
            ExcelManager excelManager = new ExcelManager();
            int sheet = Convert.ToInt32(ConfigurationManager.AppSettings.Get("HostSheet"));
            Week[] weeks = excelManager.ReadPredictions(ConfigurationManager.AppSettings.Get("AdminLocation"), sheet);
            BonusQuestions bonus = excelManager.ReadBonus();
            host = new Player("host", "22", "zb", weeks, bonus);
        }
    }
}
