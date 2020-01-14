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
    public partial class PlayerForm : Form
    {
        public Player Player;
        public PlayerManager manager;
        private NumericUpDown[] NUDs;
        private Week[] weeks;
        private int counter;
        private bool ManualInput;
        public PlayerForm()
        {
            weeks = new Week[34];
            counter = 0;
            ManualInput = false;
            InitializeComponent();

            NUDs = new NumericUpDown[18]{numericUpDown1 ,numericUpDown2 ,numericUpDown3 ,numericUpDown4, numericUpDown5 , numericUpDown6
            ,numericUpDown7 ,numericUpDown8 ,numericUpDown9 ,numericUpDown10 ,numericUpDown11 ,numericUpDown12 ,numericUpDown13
            ,numericUpDown14 ,numericUpDown15 ,numericUpDown16 ,numericUpDown17,numericUpDown18};
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

            else if (Player != null)
            {
                counter++;

                if (Player.Weeks[counter] != null)
                {
                    gbWeeks.Text = "Week " + (counter + 1);
                    LoadWeek(counter);
                }

                else
                {
                    MessageBox.Show("Can't load week. It doesn't exist");
                    SavePlayer();
                }
                
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
            if ((!filename.EndsWith(".xls")) && (!filename.EndsWith(".xlsx")))
            {
                MessageBox.Show("Invalid file");
                return;
            }

            ExcelManager em = new ExcelManager();
            if (cbSecondHalf.Checked)
            {
                weeks = em.ReadPredictions(filename, 1, new ExcelReadSettings(2, Convert.ToInt32(nudAfwijking.Value)), Player.Weeks);
            }

            else if (cbFirstHalf.Checked)
            {
                weeks = em.ReadPredictions(filename, 1, new ExcelReadSettings(1, Convert.ToInt32(nudAfwijking.Value)));
            }

            else
            {
                weeks = em.ReadPredictions(filename, 1, new ExcelReadSettings(0, Convert.ToInt32(nudAfwijking.Value)));
            }
            var estimations = em.ReadEstimations();
            SavePlayer(estimations);
            em.Clean();
            MessageBox.Show("Predictions succesfully loaded and saved!");
        }

        private void btnSwitchInput_Click(object sender, EventArgs e)
        {
            SwitchInput();
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
        private void SwitchInput()
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

        private void LoadWeek(int round)
        {
            int loader = 0;
            while (loader < 17)
            {
                NUDs[loader].Value = Player.Weeks[round].Matches[loader / 2].ResultA;
                NUDs[loader + 1].Value = Player.Weeks[round].Matches[loader / 2].ResultB;
                loader += 2;
            }
        }

        private void LoadBonus()
        {
            tbKampioen.Text = Player.Questions.Answers[BonusKeys.Kampioen].Answer;
            tbProdeg.Text = Player.Questions.Answers[BonusKeys.Prodeg].Answer;
            tbPromovendi1.Text = Player.Questions.Answers[BonusKeys.Promovendi].AnswerArray[0];
            tbPromovendi2.Text = Player.Questions.Answers[BonusKeys.Promovendi].AnswerArray[1];
            tbFin1.Text = Player.Questions.Answers[BonusKeys.Finalisten].AnswerArray[0];
            tbFin2.Text = Player.Questions.Answers[BonusKeys.Finalisten].AnswerArray[1];
            tbRonde.Text = Player.Questions.Answers[BonusKeys.Ronde].Answer;
            tbTopscorer.Text = Player.Questions.Answers[BonusKeys.Topscorer].Answer;
            tbTrainer.Text = Player.Questions.Answers[BonusKeys.Trainer].Answer;
            tbWinterkampioen.Text = Player.Questions.Answers[BonusKeys.Winterkampioen].Answer;
            tbMostRed.Text = Player.Questions.Answers[BonusKeys.Teamrood].Answer;
            tbAssists.Text = Player.Questions.Answers[BonusKeys.Assists].Answer;
            tbWorstDefence.Text = Player.Questions.Answers[BonusKeys.Defensie].Answer;
            tbDegradant1.Text = Player.Questions.Answers[BonusKeys.Degradanten].AnswerArray[0];
            tbDegradant2.Text = Player.Questions.Answers[BonusKeys.Degradanten].AnswerArray[1];
        }

        private void LoadEstimations()
        {
            nudGoals.Value = Player.Estimations.Answers[EstimationKeys.Goals].Answer;
            nudReds.Value = Player.Estimations.Answers[EstimationKeys.Reds].Answer;
        }
        public void LoadPlayer(Player player)
        {
            Player = player;
            tbName.Text = player.Name;
            tbAge.Text = player.Age;
            tbWoonplaats.Text = player.Woonplaats;
            LoadBonus();
            LoadEstimations();
            LoadWeek(0);
            SwitchInput();
        }

        private void SavePlayer(Estimations ests = null)
        {
            try
            {
                string[] finalists = { tbFin1.Text, tbFin2.Text };
                string[] degradanten = { tbDegradant1.Text, tbDegradant2.Text };
                string[] promovendi = { tbPromovendi1.Text, tbPromovendi2.Text };
                BonusQuestions questions = new BonusQuestions(tbKampioen.Text, degradanten, tbTopscorer.Text, tbTrainer.Text
                    , tbWinterkampioen.Text, tbRonde.Text, promovendi, finalists, tbMostRed.Text, tbAssists.Text, tbWorstDefence.Text, 
                     tbProdeg.Text,new int[12] {99,99,99,99,99,99,99,99,99,99,99,99});

                if(ests == null)
                ests = new Estimations(Convert.ToInt32(nudReds.Value), Convert.ToInt32(nudGoals.Value));

                Player newplayer = new Player(tbName.Text, tbAge.Text, tbWoonplaats.Text, weeks, questions, ests);
                if (Player != null)
                {
                    manager.RemovePlayer(Player.Name);
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
    }
}
