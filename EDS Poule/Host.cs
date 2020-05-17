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
            if (host != null)
            {
                return host;
            }
            ExcelManager excelManager = new ExcelManager();
            int sheet = Convert.ToInt32(ConfigurationManager.AppSettings.Get("HostSheet"));
            Week[] weeks = excelManager.ReadPredictions(ConfigurationManager.AppSettings.Get("AdminLocation"), sheet, new ExcelReadSettings());
            BonusQuestions bonus = excelManager.ReadHostBonus(ConfigurationManager.AppSettings.Get("AdminLocation"), sheet);
            host = new Player("host", "22", "zb",weeks,bonus);
            return host;
        }
    }
}
