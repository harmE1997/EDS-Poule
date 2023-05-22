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
        public int WeekTotalScore { get; set; }
        public int WeekMatchesScore { get; private set; }
        public int WeekBonusScore { get; set; }
        public int WeekPostponementScore { get; set; }
        public int Ranking { get; set; }
        public int PreviousRanking { get; set; }
        public int RankingDifference { get; set; }
        public Dictionary<int, Week> Weeks{ get; set; }
        public BonusQuestions Questions { get; set; }

        public Player(string name, string woonplaats, Dictionary<int, Week> weeks, BonusQuestions questions)
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
            //reset postponement scores
            foreach (var week in Weeks)
            {
                week.Value.WeekPostponementScore = 0;
            }
            
            foreach (var week in Weeks)
            {
                if (week.Value == null)
                {
                    WeekMatchesScore = 0;
                    break;
                }

                if (week.Value.Weeknr > currentWeek)
                    break;

                var posts = week.Value.Checkweek(Host, Questions, topscorers, currentWeek);
                MovePostponementScoresToCorrectWeeks(posts);
                WeekMatchesScore = week.Value.WeekMatchesScore;
                WeekBonusScore = week.Value.WeekBonusScore;
                WeekPostponementScore = week.Value.WeekPostponementScore;
                WeekTotalScore = WeekMatchesScore + WeekBonusScore + WeekPostponementScore;
                TotalScore += WeekTotalScore;
                if (week.Key <= currentWeek)
                    PreviousScore = TotalScore - WeekTotalScore;                   
            }
        }

        private void MovePostponementScoresToCorrectWeeks(Dictionary<int,int> scores) 
        { 
            foreach(var score in scores)
            {
                Weeks[score.Key].WeekPostponementScore += score.Value;
            }
        }
    }
}
