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
        private void btnChampions_Click(object sender, EventArgs e) => ActionBonusQuestion(BonusKeys.Kampioen);
        private void btnDegrade_Click(object sender, EventArgs e) => ActionBonusQuestion(BonusKeys.Degradanten);
        private void btnTopscorers_Click(object sender, EventArgs e) => ActionBonusQuestion(BonusKeys.Topscorer);
        private void btnTrainers_Click(object sender, EventArgs e) => ActionBonusQuestion(BonusKeys.Trainer);
        private void btnWinterChamps_Click(object sender, EventArgs e) => ActionBonusQuestion(BonusKeys.Winterkampioen);
        private void btnChampsDiv1_Click(object sender, EventArgs e) => ActionBonusQuestion(BonusKeys.Promovendi);
        private void btnCupFinalists_Click(object sender, EventArgs e) => ActionBonusQuestion(BonusKeys.Finalisten);
        private void btnChampionRound_Click(object sender, EventArgs e) => ActionBonusQuestion(BonusKeys.Ronde);
        private void btnRedCards_Click(object sender, EventArgs e) => ActionBonusQuestion(BonusKeys.Teamrood);
        private void btnPlayOffs_Click(object sender, EventArgs e) => ActionBonusQuestion(BonusKeys.Prodeg);
        private void btnAssists_Click(object sender, EventArgs e) => ActionBonusQuestion(BonusKeys.Assists);
        private void btnGoalsAgainst_Click(object sender, EventArgs e) => ActionBonusQuestion(BonusKeys.Defensie);

        //=========================================Estimations==========================================
        private void btnNrReds_Click(object sender, EventArgs e) => ActionEstimation(EstimationKeys.Reds);
        private void btnNrGoals_Click(object sender, EventArgs e) => ActionEstimation(EstimationKeys.Goals);

        private void ActionBonusQuestion(BonusKeys Key)
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

        private void ActionEstimation(EstimationKeys Key)
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
