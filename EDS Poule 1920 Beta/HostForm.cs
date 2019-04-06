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
    public struct BonusLocation
    {
        public TextBox text;
        public NumericUpDown nud;
    }

    public partial class HostForm : Form
    {
        public Player Host;
        private string HostFileName = "EDSHost1920";
        NumericUpDown[] NUDs;
        Week[] weeks;
        int counter;
        private Dictionary<string,BonusLocation> Bonuslocs;
        public HostForm()
        {
            NUDs = new NumericUpDown[18];
            weeks = new Week[34];
            counter = 0;
            InitializeComponent();
            fillNudsArray();
            Bonuslocs = new Dictionary<string, BonusLocation>()
            {
                { "Kampioen", new BonusLocation() { text = tbKampioen, nud = nudweek1 }},
                { "Degradant", new BonusLocation() { text = tbDegradant, nud = nudweek2 }},
                { "Kampioendivisie1", new BonusLocation() { text = tbDiv1Kampioen, nud = nudweek3 }},
                { "Championround", new BonusLocation() { text = tbRonde, nud = nudweek4 }},
                { "Topscorer", new BonusLocation() { text = tbTopscorer, nud = nudweek5 }},
                { "Trainer", new BonusLocation() { text = tbTrainer, nud = nudweek6 }},
                { "Winterkampioen", new BonusLocation() { text = tbWinterkampioen, nud = nudweek7 }}
            };

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

                int[] Weeks = new int[8];
                Weeks[0] = Convert.ToInt32(nudweek1.Value);
                Weeks[1] = Convert.ToInt32(nudweek2.Value);
                Weeks[2] = Convert.ToInt32(nudweek3.Value); 
                Weeks[3] = Convert.ToInt32(nudweek4.Value);
                Weeks[4] = Convert.ToInt32(nudweek5.Value);
                Weeks[5] = Convert.ToInt32(nudweek6.Value);
                Weeks[6] = Convert.ToInt32(nudweek7.Value);
                Weeks[7] = Convert.ToInt32(nudweek8.Value);

                BonusQuestions questions = new BonusQuestions(tbKampioen.Text, tbDegradant.Text, tbTopscorer.Text, tbTrainer.Text
                    , tbWinterkampioen.Text, tbRonde.Text, tbDiv1Kampioen.Text, finalists, Weeks);

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
            foreach (var b in Bonuslocs)
            {
                b.Value.text.Text = Host.Questions.Answers[b.Key].Answer;
                b.Value.nud.Value = Host.Questions.Answers[b.Key].WeekAnswered;
            }
            tbFin1.Text = Host.Questions.Answers["Finalisten"].Answer[0];
            tbFin2.Text = Host.Questions.Answers["Finalisten"].Answer[1];
            nudweek8.Value = Host.Questions.Answers["Finalisten"].WeekAnswered;
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
            BonusQuestions ans = new BonusQuestions("", "", "", "", "", "", "", finalists, new int[] { 99,99,99,99,99,99,99,99});
            Estimations ests = new Estimations(-99,-99);
            Host = new Player("Host", weeks, ans, ests);
        }
    }
}
