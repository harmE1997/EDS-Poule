using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDS_Poule
{
    [Serializable]
    public class Player
    {
        public string Name { get; private set; }
        public string Age { get; private set; }
        public string Woonplaats { get; private set; }
        public int TotalScore { get; private set; }
        public int PreviousScore { get; set; }
        public int WeekScore { get; private set; }
        public int Ranking { get; set; }
        public int PreviousRanking { get; set; }
        public Week[] Weeks { get; private set; }
        public BonusQuestions Questions { get; set; }

        public Player(string name, string age, string woonplaats, Week[] weeks, BonusQuestions questions)
        {
            Weeks = weeks;
            Name = name;
            Age = age;
            Woonplaats = woonplaats;
            TotalScore = 0;
            WeekScore = 0;
            Questions = questions;
            PreviousScore = 0;
        }

        public string PlayerToString()
        {
            string text = Ranking + "\t" + PreviousRanking + "\t" + TotalScore + "\t" + WeekScore + "\t" + Name;
            return text;
        }

        public void CheckPlayer(Player Host, int currentWeek, Dictionary<string, Topscorer> topscorers)
        {
            TotalScore = 0;
            WeekScore = 0;
            for (int i = 0; i < currentWeek; i++)
            {
                if (Weeks[i] != null)
                {
                    int weekscore = Weeks[i].Checkweek(Host);
                    TotalScore += weekscore;
                    if (i == (currentWeek - 1))
                        WeekScore = weekscore;
                }
            }

            TotalScore += Questions.CheckBonus(Host.Questions, currentWeek, topscorers);
            WeekScore += Questions.WeekScore;
            PreviousScore = TotalScore - WeekScore;
        }
    }
}
