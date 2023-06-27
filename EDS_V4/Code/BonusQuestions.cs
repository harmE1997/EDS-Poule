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

    public class Question
    {
        public string[] Answer { get; set; }
        public int[] WeeksAnswered { get; set; }
        public int Points { get; set; }

        public Question()
        {
            //this parameterless constructor is used for json deserialization. Do not use it for implementations!
        }
    }

    public class Topscorer
    {
        public int Total { get; set; }
        public List<int> Rounds { get; set; }
    }

    public class BonusQuestions
    {
        public Dictionary<BonusKeys, Question> Answers { get; set; }

        public BonusQuestions()
        {
            //this parameterless constructor is used for json deserialization. Do not use it for implementations!
        }

        public BonusQuestions(string[] answers, int[] weeks)
        {
            Answers = new Dictionary<BonusKeys, Question>()
            {
                {BonusKeys.Kampioen, new Question(){Answer = new string[] {answers[0] }, Points = 140, WeeksAnswered = new int[] {weeks[0] } } },
                {BonusKeys.Prodeg, new Question(){Answer = new string[] {answers[1] }, Points = 70, WeeksAnswered = new int[] {weeks[1] } } },
                {BonusKeys.Topscorer, new Question(){Answer = new string[] {answers[2] }, Points = 0, WeeksAnswered = new int[] {weeks[2] } } },
                {BonusKeys.Trainer, new Question(){Answer = new string[] {answers[3] }, Points = 120, WeeksAnswered = new int[] {weeks[3] } } },
                {BonusKeys.Winterkampioen, new Question(){Answer = new string[] {answers[4] }, Points = 90, WeeksAnswered = new int[] {weeks[4] } } },
                {BonusKeys.Ronde, new Question(){Answer = new string[] {answers[5] }, Points = 70, WeeksAnswered = new int[] { weeks[5] } } },
                {BonusKeys.Teamrood, new Question(){Answer = new string[] {answers[6] }, Points = 90, WeeksAnswered = new int[] {weeks[6]} } },
                {BonusKeys.Finalisten, new Question(){Answer = new string[]{answers[7], answers[8] }, Points = 50, WeeksAnswered = new int[]{ weeks[7], weeks[8] } } },
                {BonusKeys.Degradanten, new Question(){Answer = new string[]{answers[9], answers[10] }, Points = 50, WeeksAnswered = new int[]{ weeks[9], weeks[10] } } },
                {BonusKeys.Promovendi, new Question(){Answer = new string[]{answers[11], answers[12] }, Points = 50, WeeksAnswered = new int[]{ weeks[11], weeks[12] } } },
            };
        }

        public int CheckBonus(BonusQuestions HostQuestions, int currentweek, Dictionary<string, Topscorer> topscorers)
        {
            if (HostQuestions == null)
            {
                throw new ArgumentNullException("hostquestions");
            }
            int WeekScore = 0;

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
            return WeekScore;
        }
    }
}
