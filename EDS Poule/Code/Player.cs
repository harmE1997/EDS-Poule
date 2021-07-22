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
        public int RankingDifference { get; set; }
        public Week[] Weeks { get; set; }
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
            RankingDifference = 0;
            PreviousRanking = 0;
            Ranking = 0;
        }

        public string PlayerToString()
        {
            string text = Ranking + "\t" + PreviousRanking + "\t" + TotalScore + "\t" + WeekScore + "\t" + Name;
            return text;
        }

        public void CheckPlayer(Player Host, int currentWeek, Dictionary<string, Topscorer> topscorers, bool recalculateWeeks = false)
        {
            TotalScore = 0;
            foreach (var week in Weeks)
            {
                if (week.Weeknr <= currentWeek)
                {
                    if (week.WeekScore == 0 || recalculateWeeks)
                        week.Checkweek(Host, Questions, topscorers);

                    WeekScore = week.WeekScore;
                    TotalScore += week.WeekScore;
                }
            }

            PreviousScore = TotalScore - WeekScore;
        }
    }
}
