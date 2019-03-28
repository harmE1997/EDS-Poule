using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace EDS_Poule
{
    public partial class HostForm : Form
    {
        public Player Host;
        private string HostFileName = "EDSHost1920";
        NumericUpDown[] NUDs;
        Week[] weeks;
        int counter;
        public HostForm()
        {
            NUDs = new NumericUpDown[18];
            weeks = new Week[34];
            counter = 0;
            InitializeComponent();
            fillNudsArray();
            try { LoadHost(); }
            catch { createHost(); }
            tbName.Text = "Host";
            loadWeek(counter);
            loadBonus();
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
                string[] finalists = new string[2];
                finalists[0] = tbFin1.Text;
                finalists[1] = tbFin2.Text;
                BonusQuestions questions = new BonusQuestions(tbKampioen.Text, tbDegradant.Text, tbTopscorer.Text, tbTrainer.Text
                    , tbWinterkampioen.Text, Convert.ToInt16(tbRonde.Text), tbDiv1Kampioen.Text, finalists);

                Estimations estimations = new Estimations(Convert.ToInt32(nudReds.Value), Convert.ToInt32(nudGoals.Value));
                Host = new Player(tbName.Text, weeks, questions, estimations);
                SaveHost();
                this.Dispose();
                this.Close();
            }

            else
            {
                counter++;
                gbWeeks.Text = "Week " + (counter + 1);
                loadWeek(counter);
            }
        }

        private void loadWeek(int round)
        {
            int loader = 0;
            while (loader < 17)
            {
                NUDs[loader].Value = Host.Weeks[round].Matches[loader / 2].ResultA;
                NUDs[loader +1].Value = Host.Weeks[round].Matches[loader / 2].ResultB;
                loader += 2;
            }
        }

        private void loadBonus()
        {
            tbKampioen.Text = Host.Questions.Kampioen;
            tbDegradant.Text = Host.Questions.Degradant;
            tbDiv1Kampioen.Text = Host.Questions.KamipoenDivisie1;
            tbFin1.Text = Host.Questions.Bekerfinalisten[0];
            tbFin2.Text = Host.Questions.Bekerfinalisten[1];
            tbRonde.Text = Host.Questions.ChampionRound.ToString();
            tbTopscorer.Text = Host.Questions.Topscorer;
            tbTrainer.Text = Host.Questions.Trainer;
            tbWinterkampioen.Text = Host.Questions.Winterkampioen;
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

        private void SaveHost()
        {
            // filename moet een path zijn!
            FileStream stream = new FileStream(HostFileName, FileMode.Create);
            BinaryFormatter Formatter = new BinaryFormatter();
            Formatter.Serialize(stream, Host);
            stream.Close();
        }

        private void LoadHost()
        {
            if (File.Exists(HostFileName))
            {
                FileStream stream = new FileStream(HostFileName, FileMode.Open);
                BinaryFormatter Formatter = new BinaryFormatter();
                Host = (Player)Formatter.Deserialize(stream);
                stream.Close();
            }

            else
            {
                throw new FileNotFoundException(HostFileName);
            }
        }

        private void createHost()
        {
            Match[] matches = new Match[9];
            for (int i = 0; i < 9; i++)
            {
                matches[i] = new Match(99, 99);
            }

            Week[] weeks = new Week[34];

            for (int x = 0; x < 34; x++)
            {
                weeks[x] = new Week(x + 1, matches);
            }

            string[] finalists = new string[2];
            finalists[0] = "";
            finalists[1] = "";
            BonusQuestions ans = new BonusQuestions("", "", "", "", "", 0, "", finalists);
            Estimations ests = new Estimations(-20,-20);
            Host = new Player("Host", weeks, ans, ests);
        }
    }
}
