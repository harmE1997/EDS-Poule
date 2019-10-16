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
        private Dictionary<BonusKeys,BonusLocation> Bonuslocs;
        public HostForm()
        {
            NUDs = new NumericUpDown[18]{numericUpDown1 ,numericUpDown2 ,numericUpDown3 ,numericUpDown4, numericUpDown5 , numericUpDown6
            ,numericUpDown7 ,numericUpDown8 ,numericUpDown9 ,numericUpDown10 ,numericUpDown11 ,numericUpDown12 ,numericUpDown13
            ,numericUpDown14 ,numericUpDown15 ,numericUpDown16 ,numericUpDown17,numericUpDown18};

            weeks = new Week[34];
            counter = 0;
            InitializeComponent();
            Bonuslocs = new Dictionary<BonusKeys, BonusLocation>()
            {
                { BonusKeys.Kampioen, new BonusLocation() { text = tbKampioen, nud = nudweek1 }},
                { BonusKeys.Prodeg, new BonusLocation() { text = tbProdeg, nud = nudweek12 }},
                { BonusKeys.Ronde, new BonusLocation() { text = tbRonde, nud = nudweek6 }},
                { BonusKeys.Topscorer, new BonusLocation() { text = tbTopscorer, nud = nudweek3 }},
                { BonusKeys.Trainer, new BonusLocation() { text = tbTrainer, nud = nudweek4 }},
                { BonusKeys.Winterkampioen, new BonusLocation() { text = tbWinterkampioen, nud = nudweek5 }},
                { BonusKeys.Teamrood, new BonusLocation(){ text = tbMostRed, nud = nudweek9 }},
                { BonusKeys.Assists, new BonusLocation(){ text = tbAssists, nud = nudweek10 }},
                { BonusKeys.Defensie, new BonusLocation(){ text = tbWorstDefence, nud = nudweek11}},
            };

            try { LoadHost(); }
            catch { createHost(); }
            tbName.Text = "Host";
            loadWeek(counter);
            loadBonus();
            loadEstimations();
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
                string[] finalists = { tbFin1.Text, tbFin2.Text };
                string[] deg = { tbDegradant1.Text, tbDegradant2.Text };
                string[] pro = { tbPromovendi1.Text, tbPromovendi2.Text };

                int[] Weeks = {
                Convert.ToInt32(nudweek1.Value), Convert.ToInt32(nudweek2.Value), Convert.ToInt32(nudweek3.Value),
                Convert.ToInt32(nudweek4.Value), Convert.ToInt32(nudweek5.Value), Convert.ToInt32(nudweek6.Value),
                Convert.ToInt32(nudweek7.Value), Convert.ToInt32(nudweek8.Value), Convert.ToInt32(nudweek9.Value),
                Convert.ToInt32(nudweek10.Value), Convert.ToInt32(nudweek11.Value), Convert.ToInt32(nudweek12.Value) };

                BonusQuestions questions = new BonusQuestions(tbKampioen.Text, deg, tbTopscorer.Text, tbTrainer.Text
                    , tbWinterkampioen.Text, tbRonde.Text, pro, finalists, tbMostRed.Text, tbAssists.Text, 
                    tbWorstDefence.Text, tbProdeg.Text, Weeks);
                
                Estimations estimations = new Estimations(Convert.ToInt32(nudReds.Value), Convert.ToInt32(nudGoals.Value));
                Host = new Player(tbName.Text, "", "", weeks, questions, estimations);
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
            tbFin1.Text = Host.Questions.Answers[BonusKeys.Finalisten].AnswerArray[0];
            tbFin2.Text = Host.Questions.Answers[BonusKeys.Finalisten].AnswerArray[1];
            nudweek8.Value = Host.Questions.Answers[BonusKeys.Finalisten].WeekAnswered;

            tbPromovendi1.Text = Host.Questions.Answers[BonusKeys.Promovendi].AnswerArray[0];
            tbPromovendi2.Text = Host.Questions.Answers[BonusKeys.Promovendi].AnswerArray[1];
            nudweek7.Value = Host.Questions.Answers[BonusKeys.Promovendi].WeekAnswered;

            tbDegradant1.Text = Host.Questions.Answers[BonusKeys.Degradanten].AnswerArray[0];
            tbDegradant2.Text = Host.Questions.Answers[BonusKeys.Degradanten].AnswerArray[1];
            nudweek2.Value = Host.Questions.Answers[BonusKeys.Degradanten].WeekAnswered;
        }

        private void loadEstimations()
        {
            nudGoals.Value = Host.Estimations.Answers[EstimationKeys.Goals].Answer;
            nudReds.Value = Host.Estimations.Answers[EstimationKeys.Reds].Answer;
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
            //create an empty host with default values
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

            string[] finalists = { "",""};
            string[] deg = { "", "" };
            string[] pro = { "", "" };
            BonusQuestions ans = new BonusQuestions("", deg, "", "", "", "", pro,finalists,"","","", "", new int[] { 99,99,99,99,99,99,99,99,99,99,99,99});
            Estimations ests = new Estimations(-99,-99);
            Host = new Player("Host", "", "", weeks, ans, ests);
        }
    }
}
