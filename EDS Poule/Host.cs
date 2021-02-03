using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace EDS_Poule
{
    class Host
    {
        private Player host;
        public Host()
        {
            host = null;
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
            BonusQuestions bonus = excelManager.ReadHostBonus();
            host = new Player("host", "22", "zb", weeks, bonus);
        }
    }
}
