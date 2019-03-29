using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EDS_Poule
{
    public partial class StatsForm : Form
    {
        public PlayerManager manager;
        List<Stat> stats;
        public StatsForm()
        {
            InitializeComponent();
            stats = new List<Stat>();
        }

        private void btnChampions_Click(object sender, EventArgs e) => action("c");
        private void btnDegrade_Click(object sender, EventArgs e) => action("d");
        private void btnTopscorers_Click(object sender, EventArgs e) => action("t");
        private void btnTrainers_Click(object sender, EventArgs e) => action("tr");
        private void btnWinterChamps_Click(object sender, EventArgs e) => action("wc");
        private void btnChampsDiv1_Click(object sender, EventArgs e) => action("cd1");
        private void btnCupFinalists_Click(object sender, EventArgs e) => action("cf");
        private void btnChampionRound_Click(object sender, EventArgs e) => action("rc");

        private void action(string field)
        {
            stats.Clear();
            foreach (Player player in manager.Players)
            {
                string stat = "";
                switch (field)
                {
                    case "c":
                        stat = player.Questions.Answers["Kampioen"].Answer;
                        break;

                    case "cd1":
                        stat = player.Questions.Answers["Kampioendivisie1"].Answer;
                        break;

                    case "t":
                        stat = player.Questions.Answers["Topscorer"].Answer;
                        break;

                    case "tr":
                        stat = player.Questions.Answers["Trainer"].Answer;
                        break;

                    case "wc":
                        stat = player.Questions.Answers["Winterkampioen"].Answer;
                        break;

                    case "d":
                        stat = player.Questions.Answers["Degradant"].Answer;
                        break;

                    case "rc":
                        stat = player.Questions.Answers["Championround"].Answer;
                        break;

                    case "cf":
                        stat = player.Questions.Answers["Finalisten"].Answer[0];
                        UpdateStats(stat, player.Name);
                        stat = player.Questions.Answers["Finalisten"].Answer[1];
                        break;
                }
                UpdateStats(stat, player.Name);               
            }
            UpdateListBox();
        }

        private void UpdateStats(string stat, string playername)
        {
            Stat existingStat = null;
            foreach (Stat oldstat in stats)
            {
                if (oldstat.Name == stat)
                {
                    existingStat = oldstat;
                }
            }

            if (existingStat == null)
            {
                Stat newstat = new Stat(stat, playername);
                stats.Add(newstat);
            }

            else
            {
                existingStat.Add(playername);
            }
        }

        private void UpdateListBox()
        {
            lbStats.Items.Clear();
            stats.Sort();
            stats.Reverse();
            foreach (Stat stat in stats)
            {
                lbStats.Items.Add(stat.statToString());
                foreach (string name in stat.Names)
                {
                    lbStats.Items.Add(name);
                }

                lbStats.Items.Add("=================================================================================================");
            }
        }

        
    }
}
