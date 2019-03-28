﻿using System;
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
                        stat = player.Questions.Kampioen;
                        break;

                    case "cd1":
                        stat = player.Questions.KamipoenDivisie1;
                        break;

                    case "t":
                        stat = player.Questions.Topscorer;
                        break;

                    case "tr":
                        stat = player.Questions.Trainer;
                        break;

                    case "wc":
                        stat = player.Questions.Winterkampioen;
                        break;

                    case "d":
                        stat = player.Questions.Degradant;
                        break;

                    case "rc":
                        stat = player.Questions.ChampionRound.ToString();
                        break;

                    case "cf":
                        stat = player.Questions.Bekerfinalisten[0];
                        UpdateStats(stat, player.Name);
                        stat = player.Questions.Bekerfinalisten[1];
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