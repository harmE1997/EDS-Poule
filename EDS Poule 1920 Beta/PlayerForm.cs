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
    public partial class PlayerForm : Form
    {
        public Player player;
        public PlayerManager manager;
        private NumericUpDown[] NUDs;
        private Week[] weeks;
        private Estimations estimations;
        int counter;
        bool ManualInput;
        public PlayerForm()
        {
            NUDs = new NumericUpDown[18];
            weeks = new Week[34];
            estimations = null;
            counter = 0;
            ManualInput = false;
            InitializeComponent();
            fillNudsArray();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            Match[] matches = new Match[9];
            int Indicator = 0;
            while (Indicator < 17)
            {
                Match match = new Match(Convert.ToInt32(NUDs[Indicator].Value), Convert.ToInt32(NUDs[Indicator + 1].Value));
                matches[Indicator / 2] = match;
                Indicator += 2;
            }

            weeks[counter] = new Week((counter + 1), matches);
            if (counter == 33)
            {
                SavePlayer();
            }

            else if (player != null)
            {
                counter++;

                if (player.Weeks[counter] != null)
                {
                    gbWeeks.Text = "Week " + (counter + 1);
                    loadWeek(counter);
                }

                else
                    MessageBox.Show("Can't load week. It doesn't exist");
            }

            else
            {
                counter++;
                gbWeeks.Text = "Week " + (counter + 1);
                foreach (NumericUpDown nud in NUDs)
                    nud.Value = 99;
            }
        }

        private void btnReadFile_Click(object sender, EventArgs e)
        {
            string filename = tbFileName.Text;
            if (string.IsNullOrEmpty(filename) || !System.IO.File.Exists(filename) || ((!filename.EndsWith(".xls")) && (!filename.EndsWith(".xlsx"))))
            {
                MessageBox.Show("Invalid file");
                return;
            }

            ExcelManager em = new ExcelManager();
            if (cbSecondHalf.Checked)
            {
                weeks = em.readPredictions(filename, 1, new ExcelReadSettings(2, Convert.ToInt32(nudAfwijking.Value)), player.Weeks);
            }

            else if (cbFirstHalf.Checked)
            {
                weeks = em.readPredictions(filename, 1, new ExcelReadSettings(1, Convert.ToInt32(nudAfwijking.Value)));
            }

            else
            {
                weeks = em.readPredictions(filename, 1, new ExcelReadSettings(0, Convert.ToInt32(nudAfwijking.Value)));
            }
            estimations = em.ReadEstimations();
            SavePlayer();
            em.Clean();
            MessageBox.Show("Predictions succesfully loaded and saved!");
        }

        private void btnSwitchInput_Click(object sender, EventArgs e)
        {
            switchInput();
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            if (ofdVoorspellingen.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbFileName.Text = ofdVoorspellingen.FileName;
            }
        }
        private void cbSecondHalf_CheckedChanged(object sender, EventArgs e)
        {
            if (cbSecondHalf.Checked)
            {
                cbFirstHalf.Checked = false;
            }
        }

        private void cbFirstHalf_CheckedChanged(object sender, EventArgs e)
        {
            if (cbFirstHalf.Checked)
            {
                cbSecondHalf.Checked = false;
            }
        }
        public void switchInput()
        {
            if (ManualInput)
            {
                gbInputFromFile.Visible = true;
                gbManualInput.Visible = false;
                btnSwitchInput.Text = "Switch to Manual Input";
                ManualInput = false;
            }

            else
            {
                gbInputFromFile.Visible = false;
                gbManualInput.Visible = true;
                btnSwitchInput.Text = "Switch to Input from Excel File";
                ManualInput = true;
            }
        }

        public void loadWeek(int round)
        {
            int loader = 0;
            while (loader < 17)
            {
                NUDs[loader].Value = player.Weeks[round].Matches[loader / 2].ResultA;
                NUDs[loader + 1].Value = player.Weeks[round].Matches[loader / 2].ResultB;
                loader += 2;
            }
        }

        public void loadBonus()
        {
            tbKampioen.Text = player.Questions.Answers[BonusKeys.Kampioen].Answer;
            tbProdeg.Text = player.Questions.Answers[BonusKeys.Prodeg].Answer;
            tbPromovendi1.Text = player.Questions.Answers[BonusKeys.Promovendi].AnswerArray[0];
            tbPromovendi2.Text = player.Questions.Answers[BonusKeys.Promovendi].AnswerArray[1];
            tbFin1.Text = player.Questions.Answers[BonusKeys.Finalisten].AnswerArray[0];
            tbFin2.Text = player.Questions.Answers[BonusKeys.Finalisten].AnswerArray[1];
            tbRonde.Text = player.Questions.Answers[BonusKeys.Ronde].Answer;
            tbTopscorer.Text = player.Questions.Answers[BonusKeys.Topscorer].Answer;
            tbTrainer.Text = player.Questions.Answers[BonusKeys.Trainer].Answer;
            tbWinterkampioen.Text = player.Questions.Answers[BonusKeys.Winterkampioen].Answer;
            tbMostRed.Text = player.Questions.Answers[BonusKeys.Teamrood].Answer;
            tbAssists.Text = player.Questions.Answers[BonusKeys.Assists].Answer;
            tbWorstDefence.Text = player.Questions.Answers[BonusKeys.Defensie].Answer;
            tbDegradant1.Text = player.Questions.Answers[BonusKeys.Degradanten].AnswerArray[0];
            tbDegradant2.Text = player.Questions.Answers[BonusKeys.Degradanten].AnswerArray[1];
        }

        public void loadEstimations()
        {
            nudGoals.Value = player.Estimations.Answers[EstimationKeys.Goals].Answer;
            nudReds.Value = player.Estimations.Answers[EstimationKeys.Reds].Answer;
        }
        public void loadPlayer(Player player)
        {
            tbName.Text = player.Name;
            tbAge.Text = player.Age;
            tbWoonplaats.Text = player.Woonplaats;
        }

        private void SavePlayer()
        {
            try
            {
                string[] finalists = { tbFin1.Text, tbFin2.Text };
                string[] degradanten = { tbDegradant1.Text, tbDegradant2.Text };
                string[] promovendi = { tbPromovendi1.Text, tbPromovendi2.Text };
                BonusQuestions questions = new BonusQuestions(tbKampioen.Text, degradanten, tbTopscorer.Text, tbTrainer.Text
                    , tbWinterkampioen.Text, tbRonde.Text, promovendi, finalists, tbMostRed.Text, tbAssists.Text, tbWorstDefence.Text, 
                     tbProdeg.Text,new int[12] {99,99,99,99,99,99,99,99,99,99,99,99});

                if(estimations == null)
                estimations = new Estimations(Convert.ToInt32(nudReds.Value), Convert.ToInt32(nudGoals.Value));

                Player newplayer = new Player(tbName.Text, tbAge.Text, tbWoonplaats.Text, weeks, questions, estimations);
                if (player != null)
                {
                    manager.removePlayer(player.Name);
                }
                manager.AddPlayer(newplayer);
                manager.SavePlayers();
                this.Dispose();
                this.Close();
            }

            catch
            {
                MessageBox.Show("veld niet ingevuld");
            }
        }

        private void fillNudsArray()
        {
            NUDs[0] = numericUpDown1;
            NUDs[1] = numericUpDown2;
            NUDs[2] = numericUpDown3;
            NUDs[3] = numericUpDown4;
            NUDs[4] = numericUpDown5;
            NUDs[5] = numericUpDown6;
            NUDs[6] = numericUpDown7;
            NUDs[7] = numericUpDown8;
            NUDs[8] = numericUpDown9;
            NUDs[9] = numericUpDown10;
            NUDs[10] = numericUpDown11;
            NUDs[11] = numericUpDown12;
            NUDs[12] = numericUpDown13;
            NUDs[13] = numericUpDown14;
            NUDs[14] = numericUpDown15;
            NUDs[15] = numericUpDown16;
            NUDs[16] = numericUpDown17;
            NUDs[17] = numericUpDown18;
        }
    }
}
