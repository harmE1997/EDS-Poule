using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDS_Poule
{
    [Serializable]
    public class Player : IComparable<Player>
    {
        public string Name { get; private set; }
        public int TotalScore { get; private set; }
        public int WeekScore { get; private set; }
        public int Ranking { get; private set; }
        public Week[] Weeks { get; private set; }
        public BonusQuestions Questions { get; private set; }
        public Estimations Estimations { get; private set; }

        public Player(string name, Week[] weeks, BonusQuestions questions, Estimations estimations)
        {
            Weeks = weeks;
            Name = name;
            TotalScore = 0;
            WeekScore = 0;
            Questions = questions;
            Estimations = estimations;
        }

        public void SetRanking(int rank)
        {
            Ranking = rank;
        }
        public string PlayerToString()
        {
            string text = Ranking + "\t" + Name;
            if (Name == "Bas Vos" || Name == "Gert Staal" || Name == "Jaco Vink" || Name == "Piet Visée")
            {
                text += "\t\t\t";
            }

            else if (Name == "Danny Hoogerwaard" || Name == "Tim van den Boogaard"
                || Name == "Gijsbert Wiggelinkhuijsen" || Name == "Peter van der Meijden" || Name == "Noud van den Heuvel"
                || Name == "Herman van Wissen" || Name == "Sandra van der Poel" || Name == "Vincent van den Bighelaar"
                || Name == "Ferdi van de Zwaluw" || Name == "Marijke van Thienen-Enkelaar")
            {
                text += "\t";
            }

            else
            {
                text += "\t\t";
            }
            text += "\t" + TotalScore + "\t" + WeekScore;
            return text;
        }

        public int CompareTo(Player other)
        {
            if (other != null)
            {
                return TotalScore.CompareTo(other.TotalScore);
            }

            else
            {
                throw new ArgumentNullException("OtherPlayer");
            }
        }

        public void checkPlayer(Player Host, int currentWeek)
        {
            TotalScore = 0;
            for (int i = 0; i < 34; i++)
            {
                if (Weeks[i] != null)
                {
                    int weekscore = Weeks[i].checkweek(Host);
                    TotalScore += weekscore;
                    if (i == (currentWeek - 1))
                        WeekScore = weekscore;
                }
            }

            TotalScore += Questions.checkBonus(Host);
        }
    }
}
