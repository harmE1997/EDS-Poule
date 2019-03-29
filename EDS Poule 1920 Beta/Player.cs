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
            int maxtabs = 3;
            int rangesize = 8;
            decimal NrTabs = maxtabs - (Math.Floor(Convert.ToDecimal(Name.Length) / rangesize));
            string text = Ranking + "\t" + Name;

            if (NrTabs < 1)
            {
                NrTabs = 1;
            }

            for (int i = 0; i < NrTabs; i++)
            {
                text += "\t";
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
            TotalScore += Estimations.checkEstimations(Host);
        }
    }
}
