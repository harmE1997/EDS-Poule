using System.Collections.Generic;
using VoetbalPoolsBase;
using VoetbalPoolsBase.Interfaces;

namespace EDS_V4.Code
{
    public class Player : PlayerBase<BonusQuestions>
    {
        public int PreviousScore { get; set; }
        public int PreviousRanking { get; set; }
        public Dictionary<int, Week> Weeks { get; set; }

        public Player()
        {
            //this parameterless constructor is used for json deserialization. Do not use it for implementations!
        }
        public Player(string name, string woonplaats, Dictionary<int, Week> weeks, BonusQuestions questions)
        {
            Weeks = weeks;
            PreviousScore = 0;
            PreviousRanking = 0;
        }

        public override void CheckPlayer(IHost Host, Dictionary<string, Topscorer> topscorers, int startWeek, int endWeek, bool periodCalculation)
        {
            TotalScore = 0;
            var host = Host as Player;
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

                if (week.Value.Weeknr < startWeek || week.Value.Weeknr > endWeek)
                    continue;

                var posts = week.Value.Checkweek(host, Questions, topscorers, endWeek, periodCalculation);
                MovePostponementScoresToCorrectWeeks(posts);
                week.Value.SetTotalScore();
                TotalScore += week.Value.WeekTotalScore;
                if (week.Key <= endWeek)
                    PreviousScore = TotalScore - week.Value.WeekTotalScore;
            }
        }

        private void MovePostponementScoresToCorrectWeeks(Dictionary<int, int> scores)
        {
            foreach (var score in scores)
            {
                Weeks[score.Key].WeekPostponementScore += score.Value;
            }
        }
    }
}
