using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDS_V4.Code
{
    public class Player
    {
        public string Name { get; set; }
        public string Town { get; set; }
        public int TotalScore { get; set; }
        public int PreviousScore { get; set; }
        public int Ranking { get; set; }
        public int PreviousRanking { get; set; }
        public int RankingDifference { get; set; }
        public Dictionary<int, Week> Weeks{ get; set; }
        public BonusQuestions Questions { get; set; }

        public Player()
        { 
            //this parameterless constructor is used for json deserialization. Do not use it for implementations!
        }
        public Player(string name, string woonplaats, Dictionary<int, Week> weeks, BonusQuestions questions)
        {
            Weeks = weeks;
            Name = name;
            Town = woonplaats;
            TotalScore = 0;
            Questions = questions;
            PreviousScore = 0;
            RankingDifference = 0;
            PreviousRanking = 0;
            Ranking = 0;
        }

        public void CheckPlayer(Player Host, int startWeek, int endWeek, Dictionary<string, Topscorer> topscorers, bool periodCalculation)
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
                    week.Value.WeekMatchesScore = 0;
                    break;
                }

                if (week.Value.Weeknr < startWeek|| week.Value.Weeknr > endWeek)
                    continue;

                var posts = week.Value.Checkweek(Host, Questions, topscorers, endWeek, periodCalculation);
                MovePostponementScoresToCorrectWeeks(posts);
                week.Value.SetTotalScore();
                TotalScore += week.Value.WeekTotalScore;
                if (week.Key <= endWeek)
                    PreviousScore = TotalScore - week.Value.WeekTotalScore;                   
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
