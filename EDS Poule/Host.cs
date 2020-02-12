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
        private ExcelManager excelManager;
        public Host()
        {
            excelManager = new ExcelManager();
        }

        public Player getHost()
        {
            int sheet = Convert.ToInt32(ConfigurationManager.AppSettings.Get("HostSheet"));
            Week[] weeks = excelManager.ReadPredictions(ConfigurationManager.AppSettings.Get("AdminLocation"), sheet, new ExcelReadSettings());
            Estimations ests = excelManager.ReadEstimations(ConfigurationManager.AppSettings.Get("AdminLocation"), sheet);
            BonusQuestions bonus = excelManager.ReadHostBonus(ConfigurationManager.AppSettings.Get("AdminLocation"), sheet);
            Player player = new Player("host", "22", "zb",weeks,bonus,ests);
            return player;
        }
    }
}
