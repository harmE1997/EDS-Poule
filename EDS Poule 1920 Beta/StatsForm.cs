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
        public PlayerManager manager { get; set; }
        List<Stat> stats { get; set; }
        public StatsForm()
        {
            InitializeComponent();
            stats = new List<Stat>();
        }

        //=======================================BonusQuestions==========================================
        private void btnChampions_Click(object sender, EventArgs e) => ActionBonusQuestion("Kampioen");
        private void btnDegrade_Click(object sender, EventArgs e) => ActionBonusQuestion("Degradanten");
        private void btnTopscorers_Click(object sender, EventArgs e) => ActionBonusQuestion("Topscorer");
        private void btnTrainers_Click(object sender, EventArgs e) => ActionBonusQuestion("Trainer");
        private void btnWinterChamps_Click(object sender, EventArgs e) => ActionBonusQuestion("Winterkampioen");
        private void btnChampsDiv1_Click(object sender, EventArgs e) => ActionBonusQuestion("Promovendi");
        private void btnCupFinalists_Click(object sender, EventArgs e) => ActionBonusQuestion("Finalisten");
        private void btnChampionRound_Click(object sender, EventArgs e) => ActionBonusQuestion("Championround");
        private void btnRedCards_Click(object sender, EventArgs e) => ActionBonusQuestion("Teamreds");
        private void btnPlayOffs_Click(object sender, EventArgs e) => ActionBonusQuestion("Prodeg");
        private void btnAssists_Click(object sender, EventArgs e) => ActionBonusQuestion("Assists");
        private void btnGoalsAgainst_Click(object sender, EventArgs e) => ActionBonusQuestion("Worstdefence");

        //=========================================Estimations==========================================
        private void btnNrReds_Click(object sender, EventArgs e) => ActionEstimation("Reds");
        private void btnNrGoals_Click(object sender, EventArgs e) => ActionEstimation("Goals");

        private void ActionBonusQuestion(string Key)
        {
            stats.Clear();
            foreach (Player player in manager.Players)
            {
                var Name = player.Name;
                var answer = player.Questions.Answers[Key];
                if (answer.IsArray)
                {
                    foreach (var e in answer.AnswerArray)
                    {
                        UpdateStats(e, Name);
                    }
                }

                else
                {
                    UpdateStats(answer.Answer, Name);
                }          
            }
            UpdateListBox();
        }

        private void ActionEstimation(string Key)
        {
            stats.Clear();
            foreach (Player player in manager.Players)
            {
                var answer = player.Estimations.Answers[Key].Answer;
                    UpdateStats(answer.ToString(), player.Name);             
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
