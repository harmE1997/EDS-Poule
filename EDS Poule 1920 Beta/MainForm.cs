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
        PlayerManager Manager;
        public MainForm()
        {
            Manager = new PlayerManager();
            InitializeComponent();

            try
            {
                Manager.LoadPlayers();
                RefreshRanking();
            }

            catch
            {
                Manager.SavePlayers();
            }
        }

        private void btnNewPlayer_Click(object sender, EventArgs e)
        {
            PlayerForm form = new PlayerForm();
            form.manager = Manager;
            form.Show();
        }

        private void btnRanking_Click(object sender, EventArgs e)
        {
            RefreshRanking();
        }

        private void btnRemovePlayer_Click(object sender, EventArgs e)
        {
            int del = Manager.removePlayer(tbPlayer.Text);
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
            HostForm form = new HostForm();
            form.Show();
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            int.TryParse(cbCheck.Text, out int round);
            HostForm form = new HostForm();
            Manager.checkAllPlayers(form.Host, round);
            RefreshRanking();
        }

        private void btnLoadPlayer_Click(object sender, EventArgs e)
        {
            Player player = Manager.FindPlayer(tbPlayer.Text);
            if (player != null)
            {
                PlayerForm form = new PlayerForm();
                form.manager = Manager;
                form.player = player;
                form.switchInput();
                form.loadWeek(0);
                form.loadBonus();
                form.setName(tbPlayer.Text);
                form.Show();
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

        private void btnMOTW_Click(object sender, EventArgs e)
        {
            if (cbCheck.Text != "All")
            {
                int fulls = 0;
                int halfs = 0;
                HostForm form = new HostForm();
                List<string> names = new List<string>();
                foreach (Player player in Manager.Players)
                {
                    var week = Convert.ToInt16(cbCheck.Text) - 1;
                    if (player.Weeks[week] != null)
                    {
                        int check = player.Weeks[week].CheckMOTW(form.Host);
                        if (check > 0)
                        {
                            halfs++;
                        }

                        if (check == 2)
                        {
                            fulls++;
                            names.Add(player.Name);
                        }
                    }
                }

                string Names = "";
                foreach (string name in names)
                {
                    Names += name + ", ";
                }
                rtbNotes.Text = "Goede winnaar: " + halfs + "\nHelemaal correct: " + fulls + " " + Names;
            }

            else
            {
                MessageBox.Show("Select week please.");
            }
        }

        private void btnRankingToExcel_Click(object sender, EventArgs e)
        {
            string filename = "";
            if (ofdRanking.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filename = ofdRanking.FileName;
                ExcelManager em = new ExcelManager();
                foreach (var i in em.ExportPlayersToExcel(filename, 2, Manager.Players))
                    rtbNotes.Text = "Exported " + i + " out of " + Manager.Players.Count + " players.";
               
                em.Clean();
                MessageBox.Show("Ranking sucessfully exported");
            }

        }

        private void RefreshRanking()
        {
            Manager.RankPlayers();
            lbRanking.Items.Clear();
            lbRanking.Items.Add("#\tName\t\t\t\tTotal\tWeek");
            foreach (Player player in Manager.Players)
            {
                lbRanking.Items.Add(player.PlayerToString());
            }
        }
    }
}
