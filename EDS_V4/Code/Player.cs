using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDS_V4.Code
{
    [Serializable]
    public class Player
    {
        public string Name;
        public string Town;
        public int TotalScore { get; private set; }
        public int PreviousScore { get; set; }
        public int WeekMatchesScore { get; private set; }
        public int WeekBonusScore { get; set; }
        public int Ranking { get; set; }
        public int PreviousRanking { get; set; }
        public int RankingDifference { get; set; }
        public Week[] Weeks { get; set; }
        public BonusQuestions Questions { get; set; }

        public Player(string name, string woonplaats, Week[] weeks, BonusQuestions questions)
        {
            Weeks = weeks;
            Name = name;
            Town = woonplaats;
            TotalScore = 0;
            WeekMatchesScore = 0;
            WeekBonusScore = 0;
            Questions = questions;
            PreviousScore = 0;
            RankingDifference = 0;
            PreviousRanking = 0;
            Ranking = 0;
        }

        public void CheckPlayer(Player Host, int currentWeek, Dictionary<string, Topscorer> topscorers)
        {
            TotalScore = 0;
            foreach(var week in Weeks)
            {
                if (week == null)
                {
                    WeekMatchesScore = 0;
                    break;
                }

                if (week.Weeknr > currentWeek)
                    break;
                
                week.Checkweek(Host, Questions, topscorers);
                WeekMatchesScore = week.WeekMatchesScore;
                WeekBonusScore = week.WeekBonusScore;
                TotalScore += week.WeekMatchesScore + week.WeekBonusScore;
                if (week.Weeknr <= currentWeek)
                    PreviousScore = TotalScore - week.WeekMatchesScore - week.WeekBonusScore;                   
            }
        }
    }
}
