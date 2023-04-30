using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace EDS_V4.Code
{
    [Serializable]
    public enum BonusKeys
    {
        Kampioen,
        Degradanten,
        Topscorer,
        Trainer,
        Winterkampioen,
        Ronde,
        Promovendi,
        Finalisten,
        Teamrood,
        Assists,
        Defensie,
        Prodeg
    }

    [Serializable]
    public class Question
    {
        public string[] Answer;
        public int[] WeeksAnswered;
        public int Points;      
    }

    public class Topscorer
    {
        public int Total;
        public List<int> Rounds;
    }

    [Serializable]
    public class BonusQuestions
    {
        public Dictionary<BonusKeys, Question> Answers { get; private set; }
        public int WeekScore { get; private set; }

        public BonusQuestions(string kampioen, string prodeg, string topscorer, string trainer,
            string winterkampioen, string championround, string teamReds, string[] finalisten, string[] degradanten, string[] promovendi,
             int[] weeks)
        {
            Answers = new Dictionary<BonusKeys, Question>()
            {
                {BonusKeys.Kampioen, new Question(){Answer = new string[] {kampioen }, Points = 140, WeeksAnswered = new int[] {weeks[0] } } },
                {BonusKeys.Prodeg, new Question(){Answer = new string[] {prodeg }, Points = 70, WeeksAnswered = new int[] {weeks[1] } } },
                {BonusKeys.Topscorer, new Question(){Answer = new string[] {topscorer }, Points = 0, WeeksAnswered = new int[] {weeks[2] } } },
                {BonusKeys.Trainer, new Question(){Answer = new string[] {trainer }, Points = 120, WeeksAnswered = new int[] {weeks[3] } } },
                {BonusKeys.Winterkampioen, new Question(){Answer = new string[] {winterkampioen }, Points = 90, WeeksAnswered = new int[] {weeks[4] } } },
                {BonusKeys.Ronde, new Question(){Answer = new string[] {championround }, Points = 70, WeeksAnswered = new int[] { weeks[5] } } },
                {BonusKeys.Teamrood, new Question(){Answer = new string[] {teamReds }, Points = 90, WeeksAnswered = new int[] {weeks[6]} } },
                {BonusKeys.Finalisten, new Question(){Answer = finalisten, Points = 50, WeeksAnswered = new int[]{ weeks[7], weeks[8] } } },
                {BonusKeys.Degradanten, new Question(){Answer = degradanten, Points = 50, WeeksAnswered = new int[]{ weeks[9], weeks[10] } } },
                {BonusKeys.Promovendi, new Question(){Answer = promovendi, Points = 50, WeeksAnswered = new int[]{ weeks[11], weeks[12] } } },

            };
        }

        public void CheckBonus(BonusQuestions HostQuestions, int currentweek, Dictionary<string, Topscorer> topscorers)
        {
            if (HostQuestions == null)
            {
                throw new ArgumentNullException("hostquestions");
            }
            WeekScore = 0;

            //check all questions except topscorers
            foreach (var a in Answers)
            {
                var ans = HostQuestions.Answers[a.Key];
                if (ans.WeeksAnswered[0] > 0)
                {
                    for (int i = 0; i < ans.Answer.Length; i++)
                    {
                        if (a.Value.Answer.Contains(ans.Answer[i]) && ans.WeeksAnswered[i] == currentweek)
                        {
                            WeekScore += a.Value.Points;
                        }
                    }
                }
            }

            //check the topscorers
            var ansscorer = topscorers[Answers[BonusKeys.Topscorer].Answer[0]];
            WeekScore += ansscorer.Rounds[currentweek - 1] * 5;
        }
    }
}
