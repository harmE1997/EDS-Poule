﻿using EDS_Poule.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace EDS_Poule.Code
{
    public class Host : Player
    {
        private Dictionary<string, Topscorer> Topscorers;
        public ExcelManager excelManager;
        public bool HostSet = false;

        public Host() : base("", "", "", null, null)
        { 
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

        public void setHost()
        {
            if (!HostSet)
            {
                Topscorers = new Dictionary<string, Topscorer>();
                int sheet = Convert.ToInt32(ConfigurationManager.AppSettings.Get("HostSheet"));
                Weeks = excelManager.ReadPredictions(ConfigurationManager.AppSettings.Get("AdminLocation"), sheet);
                Questions = excelManager.ReadBonus();
                setTopscorers();
                HostSet = true;
            }
        }
    }
}
