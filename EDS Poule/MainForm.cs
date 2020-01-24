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
    public partial class MainForm : Form
    {
        private PlayerManager Manager;
        private PlayerForm playerForm;
        private HostForm hostForm;
        public MainForm()
        {
            Manager = new PlayerManager();
            InitializeComponent();
            Manager.LoadPlayers();
            RefreshRanking();
            playerForm = new PlayerForm();
            hostForm = new HostForm();
        }

       
        private void btnNewPlayer_Click(object sender, EventArgs e)
        {
            playerForm = new PlayerForm();
            playerForm.manager = Manager;
            playerForm.Show();
        }

        private void btnRanking_Click(object sender, EventArgs e)
        {
            RefreshRanking();
        }

        private void btnRemovePlayer_Click(object sender, EventArgs e)
        {
            int del = Manager.RemovePlayer(tbPlayer.Text);
            if (del == 0)
            {
                MessageBox.Show("Player is removed.");
                RefreshRanking();
            }

            else
            {
                MessageBox.Show("Can't remove player. Player doesn't exist.");
            }
        }

        private void btnHost_Click(object sender, EventArgs e)
        {
            hostForm = new HostForm();
            hostForm.Show();
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            int.TryParse(cbCheck.Text, out int round);
            Manager.CheckAllPlayers(hostForm.Host, round);
            RefreshRanking();
        }

        private void btnLoadPlayer_Click(object sender, EventArgs e)
        {
            Player player = Manager.FindPlayer(tbPlayer.Text);
            if (player != null)
            {
                playerForm = new PlayerForm();
                playerForm.manager = Manager;
                playerForm.LoadPlayer(player);
                playerForm.Show();
            }

            else
            {
                MessageBox.Show("can't find player");
            }
        }

        private void btnStats_Click(object sender, EventArgs e)
        {
            StatsForm form = new StatsForm();
            form.manager = Manager;
            form.Show();

        }

        private void btnMatch_Click(object sender, EventArgs e)
        {
            int fulls = 0;
            int halfs = 0;
            int matchID = GetMatchID();
            var week = Convert.ToInt16(cbCheck.Text) - 1;
            string Names = "";
            foreach (Player player in Manager.Players)
            {
                if (player.Weeks[week] != null)
                {
                    int check = player.Weeks[week].CheckMatch(hostForm.Host, matchID);
                    if (check > 0)
                    {
                        halfs++;
                    }

                    if (check == 2)
                    {
                        fulls++;
                        Names += player.Name + ", ";
                    }
                }
            }

            rtbNotes.Text = "Goede winnaar: " + halfs + "\nHelemaal correct: " + fulls + " " + Names;
        }

        private void btnGetMatch_Click(object sender, EventArgs e)
        {
            int As = 0;
            int Bs = 0;
            int Ds = 0;
            int matchID = GetMatchID();
            var week = Convert.ToInt16(cbCheck.Text) - 1;

            foreach (Player p in Manager.Players)
            {
                if (p.Weeks[week] == null)
                {
                    continue;
                }
                string res = p.Weeks[week].GetMatch(matchID);
                switch (res)
                {
                    case "A":
                        As++;
                        break;
                    case "B":
                        Bs++;
                        break;
                    case "D":
                        Ds++;
                        break;
                    default:
                        break;
                }
            }

            rtbNotes.Text = "Winst A: " + As + "\nWinst B: " + Bs + "\nGelijkspel: " + Ds;
        }

        private void btnRankingToExcel_Click(object sender, EventArgs e)
        {
            string filename = "";
            if (ofdRanking.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filename = ofdRanking.FileName;
                ExcelManager em = new ExcelManager();
                foreach (var i in em.ExportPlayersToExcel(filename, 2, Manager.Players))
                {
                    rtbNotes.Text = "Exported " + (i - 2) + " out of " + Manager.Players.Count + " players.";
                }

                em.Clean();
                MessageBox.Show("Ranking sucessfully exported");
            }

        }

        private void RefreshRanking()
        {
            //sort players by score
            Manager.RankPlayers(true);
            Manager.RankPlayers(false);
            lbRanking.Items.Clear();
            lbRanking.Items.Add("#\t#Last\tTotal\tWeek\tName");
            foreach (Player player in Manager.Players)
            {
                lbRanking.Items.Add(player.PlayerToString());
            }
        }

        private int GetMatchID()
        {
            string matchid = cbMatches.Text;
            if (!int.TryParse(matchid, out int matchID))
            {
                matchID = 0; // MOTW's matchID is 0.
            }

            return matchID;
        }
    }
}
